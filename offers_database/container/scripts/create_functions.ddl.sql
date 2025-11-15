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

CREATE OR REPLACE FUNCTION public.search_external_offers_by_keywords(
  p_keywords             TEXT[] DEFAULT NULL,
  p_leading_category     TEXT DEFAULT NULL,
  p_salary_from          NUMERIC DEFAULT NULL,
  p_salary_to            NUMERIC DEFAULT NULL,
  p_salary_currency      TEXT DEFAULT NULL,
  p_location_city        TEXT DEFAULT NULL,
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
BEGIN
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
    LEFT JOIN public.external_offers   eo ON eo.offer_id = o.id
    LEFT JOIN public.location_details  ld ON ld.id = o.location_detail_id
    LEFT JOIN public.cities            ci ON ci.id = ld.city_id
    LEFT JOIN public.streets           st ON st.id = ld.street_id
    LEFT JOIN public.postal_codes      pc ON pc.id = ld.postal_code_id
    LEFT JOIN public.currencies        cur ON cur.id = o.currency_id
    LEFT JOIN public.salary_periods    sp  ON sp.id = o.salary_period_id
    LEFT JOIN public.leading_categories lc ON lc.id = o.leading_category_id
    WHERE
      (
        -- Keyword filter: job title OR company name OR description OR skill name
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
      AND (p_leading_category IS NULL OR lc.leading_category = p_leading_category)
      AND (p_salary_from IS NULL OR o.salary_from IS NULL OR o.salary_from >= p_salary_from)
      AND (p_salary_to   IS NULL OR o.salary_to   IS NULL OR o.salary_to   <= p_salary_to)
      AND (p_salary_currency IS NULL OR cur.currency = p_salary_currency)
      AND (p_location_city IS NULL OR ci.city ILIKE '%' || p_location_city || '%')
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
  ORDER BY
    CASE
      WHEN p_user_latitude IS NOT NULL
       AND p_user_longitude IS NOT NULL
       AND b.distance_km IS NULL THEN 1
      ELSE 0
    END,
    CASE
      WHEN p_user_latitude IS NOT NULL
       AND p_user_longitude IS NOT NULL THEN b.distance_km
    END ASC NULLS LAST,
    b.salary_to ASC NULLS LAST
  LIMIT COALESCE(p_limit, 9223372036854775807)
  OFFSET COALESCE(p_offset, 0);
END;
$$;
