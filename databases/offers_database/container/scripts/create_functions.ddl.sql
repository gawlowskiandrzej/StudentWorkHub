CREATE OR REPLACE FUNCTION public.get_dictionaries_text()
RETURNS JSONB
LANGUAGE sql
STABLE
AS $$
SELECT jsonb_build_object(
    'skill.experienceLevel',
        COALESCE((SELECT jsonb_agg(level ORDER BY id) FROM public.experience_levels), '[]'::jsonb),
    'languages.language',
        COALESCE((SELECT jsonb_agg(language ORDER BY id) FROM public.languages), '[]'::jsonb),
    'languages.level',
        COALESCE((SELECT jsonb_agg(level ORDER BY id) FROM public.language_levels), '[]'::jsonb),
    'salary.currency',
        COALESCE((SELECT jsonb_agg(currency ORDER BY id) FROM public.currencies), '[]'::jsonb),
    'salary.period',
        COALESCE((SELECT jsonb_agg(period ORDER BY id) FROM public.salary_periods), '[]'::jsonb),
    'employment.types',
        COALESCE((SELECT jsonb_agg(type ORDER BY id) FROM public.employment_types), '[]'::jsonb),
    'employment.schedules',
        COALESCE((SELECT jsonb_agg(schedule ORDER BY id) FROM public.employment_schedules), '[]'::jsonb),
    'category.leadingCategory',
        COALESCE((SELECT jsonb_agg(leading_category ORDER BY id) FROM public.leading_categories), '[]'::jsonb)
);
$$;

CREATE OR REPLACE FUNCTION public.get_sources_base_urls()
RETURNS TEXT[]
LANGUAGE sql
STABLE
AS $$
    SELECT COALESCE(
        array_agg(base_url ORDER BY id),
        ARRAY[]::text[]
    )
    FROM public.sources;
$$;

CREATE OR REPLACE FUNCTION public.search_external_offers_by_keywords(
  p_keywords             TEXT[] DEFAULT NULL,
  p_leading_category     TEXT DEFAULT NULL,
  p_salary_from          NUMERIC DEFAULT NULL,
  p_salary_to            NUMERIC DEFAULT NULL,
  p_salary_currency      TEXT DEFAULT NULL,
  p_location_city        TEXT DEFAULT NULL,

  -- NEW: filters by name (mapped to IDs inside)
  p_salary_period        TEXT DEFAULT NULL,
  p_employment_schedule  TEXT DEFAULT NULL,
  p_employment_type      TEXT DEFAULT NULL,

  p_is_remote            BOOLEAN DEFAULT NULL,
  p_is_hybrid            BOOLEAN DEFAULT NULL,
  p_user_latitude        DOUBLE PRECISION DEFAULT NULL,
  p_user_longitude       DOUBLE PRECISION DEFAULT NULL,
  p_distance_limit_km    DOUBLE PRECISION DEFAULT NULL,
  p_limit                INTEGER DEFAULT NULL,
  p_offset               INTEGER DEFAULT 0
)
RETURNS SETOF JSONB
LANGUAGE plpgsql
STABLE
AS $$
DECLARE
  v_salary_period_id       SMALLINT;
  v_employment_schedule_id SMALLINT;
  v_employment_type_id     SMALLINT;
BEGIN
  -- Map "name" -> "id" (case-insensitive). If provided but not found -> return empty set.
  IF p_salary_period IS NOT NULL THEN
    SELECT sp.id
      INTO v_salary_period_id
    FROM public.salary_periods sp
    WHERE lower(sp.period) = lower(p_salary_period)
    LIMIT 1;

    IF v_salary_period_id IS NULL THEN
      RETURN;
    END IF;
  END IF;

  IF p_employment_schedule IS NOT NULL THEN
    SELECT es.id
      INTO v_employment_schedule_id
    FROM public.employment_schedules es
    WHERE lower(es.schedule) = lower(p_employment_schedule)
    LIMIT 1;

    IF v_employment_schedule_id IS NULL THEN
      RETURN;
    END IF;
  END IF;

  IF p_employment_type IS NOT NULL THEN
    SELECT et.id
      INTO v_employment_type_id
    FROM public.employment_types et
    WHERE lower(et.type) = lower(p_employment_type)
    LIMIT 1;

    IF v_employment_type_id IS NULL THEN
      RETURN;
    END IF;
  END IF;

  RETURN QUERY
  WITH base AS (
    SELECT
      o.id,
      o.job_title,
      o.description,
      o.salary_from,
      o.salary_to,
      o.is_gross,
      o.is_remote,
      o.is_hybrid,
      o.published,
      o.expires,
      o.is_urgent,
      o.is_for_ukrainians,

      s.name     AS source_name,
      s.base_url AS source_base_url,
      eo.query_string,

      c.name     AS company_name,
      c.logo_url AS company_logo_url,

      ld.building_number,
      ld.latitude,
      ld.longitude,
      ci.city,
      st.street,
      pc.postal_code,

      cur.currency,
      sp.period,
      lc.leading_category,

      CASE
        WHEN p_user_latitude IS NOT NULL AND p_user_longitude IS NOT NULL
         AND ld.latitude IS NOT NULL AND ld.longitude IS NOT NULL
        THEN 2 * 6371 * ASIN(
               SQRT(
                 POWER(SIN(RADIANS((p_user_latitude - ld.latitude) / 2)), 2) +
                 COS(RADIANS(p_user_latitude)) * COS(RADIANS(ld.latitude)) *
                 POWER(SIN(RADIANS((p_user_longitude - ld.longitude) / 2)), 2)
               )
             )
        ELSE NULL
      END AS distance_km
    FROM public.offers o
    JOIN public.companies c        ON c.id = o.company_id
    JOIN public.sources   s        ON s.id = o.source_id
    JOIN public.external_offers   eo ON eo.offer_id = o.id
    LEFT JOIN public.location_details  ld ON ld.id = o.location_detail_id
    LEFT JOIN public.cities            ci ON ci.id = ld.city_id
    LEFT JOIN public.streets           st ON st.id = ld.street_id
    LEFT JOIN public.postal_codes      pc ON pc.id = ld.postal_code_id
    LEFT JOIN public.currencies        cur ON cur.id = o.currency_id
    LEFT JOIN public.salary_periods    sp  ON sp.id = o.salary_period_id
    LEFT JOIN public.leading_categories lc ON lc.id = o.leading_category_id
    WHERE
      (
        p_keywords IS NULL
        OR EXISTS (
          SELECT 1
          FROM unnest(p_keywords) AS kw
          WHERE
            o.job_title   ILIKE '%' || kw || '%' OR
            c.name        ILIKE '%' || kw || '%' OR
            o.description ILIKE '%' || kw || '%' OR
            EXISTS (
              SELECT 1
              FROM public.skills_junction sj
              JOIN public.skills s2 ON s2.id = sj.skill_id
              WHERE sj.offer_id = o.id
                AND s2.skill ILIKE '%' || kw || '%'
            )
        )
      )
      AND (
        p_leading_category IS NULL
        OR lower(lc.leading_category) = lower(p_leading_category)
      )
      AND (
            (p_salary_from IS NULL AND p_salary_to IS NULL)
            OR (
                (
                (p_salary_from IS NULL OR o.salary_from IS NULL OR o.salary_from = 0 OR o.salary_from >= p_salary_from)
                AND
                (p_salary_to   IS NULL OR o.salary_to   IS NULL OR o.salary_to   = 0 OR o.salary_to   <= p_salary_to)
                )
                OR
                (
                (p_salary_from IS NULL OR o.salary_from IS NULL OR o.salary_from = 0 OR o.salary_from >= (p_salary_from / 160.0))
                AND
                (p_salary_to   IS NULL OR o.salary_to   IS NULL OR o.salary_to   = 0 OR o.salary_to   <= (p_salary_to   / 160.0))
                )
            )
        )
      AND (
        p_salary_currency IS NULL
        OR lower(cur.currency) = lower(p_salary_currency)
      )

      AND (p_location_city IS NULL OR ci.city ILIKE '%' || p_location_city || '%')

      -- NEW: salary period by ID (single-valued on offers)
      AND (v_salary_period_id IS NULL OR o.salary_period_id = v_salary_period_id)

      -- NEW: employment type by ID (many-to-many)
      AND (
        v_employment_type_id IS NULL
        OR EXISTS (
          SELECT 1
          FROM public.employment_types_junction etj
          WHERE etj.offer_id = o.id
            AND etj.employment_type_id = v_employment_type_id
        )
      )

      -- NEW: employment schedule by ID (many-to-many)
      AND (
        v_employment_schedule_id IS NULL
        OR EXISTS (
          SELECT 1
          FROM public.employment_schedules_junction esj
          WHERE esj.offer_id = o.id
            AND esj.employment_schedule_id = v_employment_schedule_id
        )
      )

      AND (p_is_remote IS NULL OR o.is_remote = p_is_remote)
      AND (p_is_hybrid IS NULL OR o.is_hybrid = p_is_hybrid)
  ),
  filtered AS (
    SELECT b.*
    FROM base b
    WHERE (p_distance_limit_km IS NULL OR b.distance_km <= p_distance_limit_km OR b.distance_km IS NULL)
  )
  SELECT jsonb_build_object(
           'id', b.id,
           'source', b.source_name,
           'url', COALESCE(b.source_base_url, '') || COALESCE(b.query_string, ''),
           'jobTitle', b.job_title,
           'company', jsonb_build_object(
               'name', b.company_name,
               'logoUrl', b.company_logo_url
           ),
           'description', b.description,
           'salary', jsonb_build_object(
               'from', b.salary_from,
               'to',   b.salary_to,
               'currency', b.currency,
               'period',   b.period,
               'type', CASE
                         WHEN b.is_gross IS NULL THEN NULL
                         WHEN b.is_gross THEN 'brutto'
                         ELSE 'netto'
                       END
           ),
           'location', jsonb_build_object(
               'buildingNumber', b.building_number,
               'street', b.street,
               'city', b.city,
               'postalCode', b.postal_code,
               'coordinates', jsonb_build_object(
                   'latitude',  b.latitude,
                   'longitude', b.longitude
               ),
               'isRemote', b.is_remote,
               'isHybrid', b.is_hybrid
           ),
           'category', jsonb_build_object(
               'leadingCategory', b.leading_category,
               'subCategories',
                   (SELECT CASE WHEN COUNT(*) = 0 THEN NULL
                                ELSE jsonb_agg(sc.sub_category ORDER BY sc.sub_category)
                           END
                    FROM public.sub_categories_junction scj
                    JOIN public.sub_categories sc ON sc.id = scj.sub_category_id
                    WHERE scj.offer_id = b.id)
           ),
           'requirements', jsonb_build_object(
               'skills',
                   COALESCE((
                     SELECT jsonb_agg(
                              jsonb_build_object(
                                'skill', s.skill,
                                'experienceMonths', sj.experience_months,
                                'experienceLevel',
                                  CASE
                                    WHEN el.level IS NULL THEN '[]'::jsonb
                                    ELSE jsonb_build_array(el.level)
                                  END
                              )
                              ORDER BY s.skill
                            )
                     FROM public.skills_junction sj
                     JOIN public.skills s ON s.id = sj.skill_id
                     LEFT JOIN public.experience_levels el ON el.id = sj.experience_level_id
                     WHERE sj.offer_id = b.id
                   ), '[]'::jsonb),
               'education',
                   (SELECT CASE WHEN COUNT(*) = 0 THEN NULL
                                ELSE jsonb_agg(el.level ORDER BY el.level)
                           END
                    FROM public.education_levels_junction ej
                    JOIN public.education_levels el ON el.id = ej.education_level_id
                    WHERE ej.offer_id = b.id),
               'languages',
                   COALESCE((
                     SELECT jsonb_agg(
                              jsonb_build_object(
                                'language', l.language,
                                'level', COALESCE(ll.level, '')
                              )
                              ORDER BY l.language
                            )
                     FROM public.languages_junction lj
                     JOIN public.languages l ON l.id = lj.language_id
                     LEFT JOIN public.language_levels ll ON ll.id = lj.language_level_id
                     WHERE lj.offer_id = b.id
                   ), '[]'::jsonb)
           ),
           'employment', jsonb_build_object(
               'types',
                   COALESCE((
                     SELECT jsonb_agg(et.type ORDER BY et.type)
                     FROM public.employment_types_junction etj
                     JOIN public.employment_types et ON et.id = etj.employment_type_id
                     WHERE etj.offer_id = b.id
                   ), '[]'::jsonb),
               'schedules',
                   COALESCE((
                     SELECT jsonb_agg(es.schedule ORDER BY es.schedule)
                     FROM public.employment_schedules_junction esj
                     JOIN public.employment_schedules es ON es.id = esj.employment_schedule_id
                     WHERE esj.offer_id = b.id
                   ), '[]'::jsonb)
           ),
           'dates', jsonb_build_object(
               'published', to_char(b.published, 'YYYY-MM-DD HH24:MI:SS'),
               'expires', CASE
                            WHEN b.expires IS NULL THEN NULL
                            ELSE to_char(b.expires, 'YYYY-MM-DD HH24:MI:SS')
                          END
           ),
           'benefits',
               (SELECT CASE WHEN COUNT(*) = 0 THEN NULL
                            ELSE jsonb_agg(be.benefit ORDER BY be.benefit)
                       END
                FROM public.benefits_junction bj
                JOIN public.benefits be ON be.id = bj.benefit_id
                WHERE bj.offer_id = b.id),
           'isUrgent', b.is_urgent,
           'isForUkrainians', b.is_for_ukrainians
         ) AS uos
  FROM filtered b
  ORDER BY b.id ASC
  LIMIT COALESCE(p_limit, 9223372036854775807)
  OFFSET COALESCE(p_offset, 0);

END;
$$;


CREATE OR REPLACE FUNCTION public.get_sources_dict()
RETURNS JSONB
LANGUAGE sql
STABLE
AS $$
    SELECT COALESCE(
        jsonb_object_agg(
            id,
            jsonb_build_object(
                'name', name,
                'base_url', base_url
            ) ORDER BY id
        ),
        '{}'::jsonb
    )
    FROM public.sources;
$$;

CREATE OR REPLACE FUNCTION public.get_companies_dict()
RETURNS JSONB
LANGUAGE sql
STABLE
AS $$
    SELECT COALESCE(
        jsonb_object_agg(
            id,
            jsonb_build_object(
                'name', name,
                'logo_url', logo_url
            ) ORDER BY id
        ),
        '{}'::jsonb
    )
    FROM public.companies;
$$;

CREATE OR REPLACE FUNCTION public.get_currencies_dict()
RETURNS JSONB
LANGUAGE sql
STABLE
AS $$
    SELECT COALESCE(
        jsonb_object_agg(id, currency ORDER BY id),
        '{}'::jsonb
    )
    FROM public.currencies;
$$;

CREATE OR REPLACE FUNCTION public.get_salary_periods_dict()
RETURNS JSONB
LANGUAGE sql
STABLE
AS $$
    SELECT COALESCE(
        jsonb_object_agg(id, period ORDER BY id),
        '{}'::jsonb
    )
    FROM public.salary_periods;
$$;

CREATE OR REPLACE FUNCTION public.get_skills_dict()
RETURNS JSONB
LANGUAGE sql
STABLE
AS $$
    SELECT COALESCE(
        jsonb_object_agg(id, skill ORDER BY id),
        '{}'::jsonb
    )
    FROM public.skills;
$$;

CREATE OR REPLACE FUNCTION public.get_experience_levels_dict()
RETURNS JSONB
LANGUAGE sql
STABLE
AS $$
    SELECT COALESCE(
        jsonb_object_agg(id, level ORDER BY id),
        '{}'::jsonb
    )
    FROM public.experience_levels;
$$;

CREATE OR REPLACE FUNCTION public.get_education_levels_dict()
RETURNS JSONB
LANGUAGE sql
STABLE
AS $$
    SELECT COALESCE(
        jsonb_object_agg(id, level ORDER BY id),
        '{}'::jsonb
    )
    FROM public.education_levels;
$$;

CREATE OR REPLACE FUNCTION public.get_languages_dict()
RETURNS JSONB
LANGUAGE sql
STABLE
AS $$
    SELECT COALESCE(
        jsonb_object_agg(id, language ORDER BY id),
        '{}'::jsonb
    )
    FROM public.languages;
$$;

CREATE OR REPLACE FUNCTION public.get_language_levels_dict()
RETURNS JSONB
LANGUAGE sql
STABLE
AS $$
    SELECT COALESCE(
        jsonb_object_agg(id, level ORDER BY id),
        '{}'::jsonb
    )
    FROM public.language_levels;
$$;

CREATE OR REPLACE FUNCTION public.get_cities_dict()
RETURNS JSONB
LANGUAGE sql
STABLE
AS $$
    SELECT COALESCE(
        jsonb_object_agg(id, city ORDER BY id),
        '{}'::jsonb
    )
    FROM public.cities;
$$;

CREATE OR REPLACE FUNCTION public.get_streets_dict()
RETURNS JSONB
LANGUAGE sql
STABLE
AS $$
    SELECT COALESCE(
        jsonb_object_agg(id, street ORDER BY id),
        '{}'::jsonb
    )
    FROM public.streets;
$$;

CREATE OR REPLACE FUNCTION public.get_postal_codes_dict()
RETURNS JSONB
LANGUAGE sql
STABLE
AS $$
    SELECT COALESCE(
        jsonb_object_agg(id, postal_code ORDER BY id),
        '{}'::jsonb
    )
    FROM public.postal_codes;
$$;

CREATE OR REPLACE FUNCTION public.get_employment_schedules_dict()
RETURNS JSONB
LANGUAGE sql
STABLE
AS $$
    SELECT COALESCE(
        jsonb_object_agg(id, schedule ORDER BY id),
        '{}'::jsonb
    )
    FROM public.employment_schedules;
$$;

CREATE OR REPLACE FUNCTION public.get_employment_types_dict()
RETURNS JSONB
LANGUAGE sql
STABLE
AS $$
    SELECT COALESCE(
        jsonb_object_agg(id, type ORDER BY id),
        '{}'::jsonb
    )
    FROM public.employment_types;
$$;

CREATE OR REPLACE FUNCTION public.get_benefits_dict()
RETURNS JSONB
LANGUAGE sql
STABLE
AS $$
    SELECT COALESCE(
        jsonb_object_agg(id, benefit ORDER BY id),
        '{}'::jsonb
    )
    FROM public.benefits;
$$;

CREATE OR REPLACE FUNCTION public.get_leading_categories_dict()
RETURNS JSONB
LANGUAGE sql
STABLE
AS $$
    SELECT COALESCE(
        jsonb_object_agg(id, leading_category ORDER BY id),
        '{}'::jsonb
    )
    FROM public.leading_categories;
$$;

CREATE OR REPLACE FUNCTION public.get_sub_categories_dict()
RETURNS JSONB
LANGUAGE sql
STABLE
AS $$
    SELECT COALESCE(
        jsonb_object_agg(id, sub_category ORDER BY id),
        '{}'::jsonb
    )
    FROM public.sub_categories;
$$;

CREATE OR REPLACE FUNCTION public.get_external_offer_urls_array()
RETURNS text[]
LANGUAGE sql
STABLE
AS $$
    SELECT array_agg(
        rtrim(s.base_url, '/') || '/' || ltrim(eo.query_string, '/')
        ORDER BY o.id
    )
    FROM public.offers o
    JOIN public.external_offers eo ON eo.offer_id = o.id
    JOIN public.sources s ON s.id = o.source_id;
$$;

CREATE OR REPLACE FUNCTION public.external_offer_exists(
    p_full_url      text,
    p_job_title     text DEFAULT NULL,
    p_company_name  text DEFAULT NULL,
    p_city          text DEFAULT NULL
)
RETURNS boolean
LANGUAGE plpgsql
STABLE
AS $$
DECLARE
    v_source_id   smallint;
    v_base_trim   text;
    v_query_str   text;
BEGIN
    IF p_full_url IS NULL OR btrim(p_full_url) = '' THEN
        RETURN FALSE;
    END IF;

    -- Find matching source by base_url (normalized without trailing slash)
    SELECT s.id, rtrim(s.base_url, '/')
    INTO v_source_id, v_base_trim
    FROM public.sources s
    WHERE p_full_url LIKE rtrim(s.base_url, '/') || '/%'
       OR p_full_url = rtrim(s.base_url, '/')
       OR p_full_url = rtrim(s.base_url, '/') || '/'
    ORDER BY char_length(s.base_url) DESC
    LIMIT 1;

    IF v_source_id IS NULL THEN
        RETURN FALSE;
    END IF;

    -- Extract query_string part (everything after base_url + '/')
    IF p_full_url = v_base_trim OR p_full_url = v_base_trim || '/' THEN
        RETURN FALSE; -- full URL without "rest of link" is not an offer
    END IF;

    v_query_str := substr(p_full_url, char_length(v_base_trim) + 2);
    v_query_str := trim(both '/' from v_query_str);

    IF v_query_str = '' THEN
        RETURN FALSE;
    END IF;

    RETURN EXISTS (
        SELECT 1
        FROM public.offers o
        JOIN public.external_offers eo ON eo.offer_id = o.id
        JOIN public.companies c ON c.id = o.company_id
        LEFT JOIN public.location_details ld ON ld.id = o.location_detail_id
        LEFT JOIN public.cities ci ON ci.id = ld.city_id
        WHERE o.source_id = v_source_id
          AND trim(both '/' from eo.query_string) = v_query_str
          AND (p_job_title    IS NULL OR o.job_title = p_job_title)
          AND (p_company_name IS NULL OR c.name     = p_company_name)
          AND (p_city         IS NULL OR ci.city    = p_city)
    );
END;
$$;
