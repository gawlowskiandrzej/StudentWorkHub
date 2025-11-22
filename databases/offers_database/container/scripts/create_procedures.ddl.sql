CREATE TYPE public.external_offer_uos_input AS (
    source_name TEXT,
    source_base_url TEXT,
    query_string TEXT,
    job_title TEXT,
    company_name TEXT,
    company_logo_url TEXT,
    description TEXT,
    salary_from NUMERIC,
    salary_to NUMERIC,
    currency TEXT,
    salary_period TEXT,
    is_gross BOOLEAN,
    building_number TEXT,
    street TEXT,
    city TEXT,
    postal_code TEXT,
    latitude DOUBLE PRECISION,
    longitude DOUBLE PRECISION,
    is_remote BOOLEAN,
    is_hybrid BOOLEAN,
    leading_category TEXT,
    sub_categories TEXT[],
    skills TEXT[],
    skills_experience_months SMALLINT[],
    skills_experience_levels TEXT[],
    education_levels TEXT[],
    languages TEXT[],
    language_levels TEXT[],
    employment_types TEXT[],
    employment_schedules TEXT[],
    published TIMESTAMPTZ,
    expires TIMESTAMPTZ,
    benefits TEXT[],
    is_urgent BOOLEAN,
    is_for_ukrainians BOOLEAN,
    offer_lifespan_expiration TIMESTAMPTZ
);

CREATE TYPE public.batch_result AS (
    idx INTEGER,
    offer_id BIGINT,
    action TEXT,
    error TEXT
);

CREATE OR REPLACE PROCEDURE public.add_source(
        IN p_name VARCHAR(256),
        IN p_base_url VARCHAR(256)
)
LANGUAGE plpgsql
AS $$
BEGIN
        INSERT INTO public.sources (name, base_url)
        VALUES (p_name, p_base_url);
END;
$$;


CREATE OR REPLACE PROCEDURE public.upsert_external_offer(
    IN p_source_name TEXT,
    IN p_source_base_url TEXT,
    IN p_query_string TEXT,
    IN p_job_title TEXT,
    IN p_company_name TEXT,
    IN p_company_logo_url TEXT,
    IN p_description TEXT,
    IN p_salary_from NUMERIC,
    IN p_salary_to NUMERIC,
    IN p_currency TEXT,
    IN p_salary_period TEXT,
    IN p_is_gross BOOLEAN,
    IN p_building_number TEXT,
    IN p_street TEXT,
    IN p_city TEXT,
    IN p_postal_code TEXT,
    IN p_latitude DOUBLE PRECISION,
    IN p_longitude DOUBLE PRECISION,
    IN p_is_remote BOOLEAN,
    IN p_is_hybrid BOOLEAN,
    IN p_leading_category TEXT,
    IN p_sub_categories TEXT[],
    IN p_skills TEXT[],
    IN p_skills_experience_months SMALLINT[],
    IN p_skills_experience_levels TEXT[],
    IN p_education_levels TEXT[],
    IN p_languages TEXT[],
    IN p_language_levels TEXT[],
    IN p_employment_types TEXT[],
    IN p_employment_schedules TEXT[],
    IN p_published TIMESTAMPTZ,
    IN p_expires TIMESTAMPTZ,
    IN p_benefits TEXT[],
    IN p_is_urgent BOOLEAN,
    IN p_is_for_ukrainians BOOLEAN,
    IN p_offer_lifespan_expiration TIMESTAMPTZ,
    OUT o_offer_id BIGINT,
    OUT o_action TEXT
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_source_id SMALLINT;
    v_company_id INTEGER;
    v_currency_id SMALLINT;
    v_salary_period_id SMALLINT;
    v_leading_category_id SMALLINT;
    v_city_id INTEGER;
    v_street_id BIGINT;
    v_postal_code_id INTEGER;
    v_location_detail_id BIGINT;
    v_existing_offer_id BIGINT;
    v_lat DOUBLE PRECISION;
    v_lon DOUBLE PRECISION;
    i INTEGER;
    v_skill_id INTEGER;
    v_skill_level_id SMALLINT;
    v_skill_months SMALLINT;
    v_language_id SMALLINT;
    v_language_level_id SMALLINT;
    v_education_level_id SMALLINT;
    v_emp_type_id SMALLINT;
    v_emp_schedule_id SMALLINT;
    v_benefit_id SMALLINT;
    v_sub_category_id BIGINT;
BEGIN
    INSERT INTO public.sources(name, base_url) VALUES (p_source_name, p_source_base_url)
    ON CONFLICT (name) DO UPDATE SET base_url = EXCLUDED.base_url;
    SELECT id INTO v_source_id FROM public.sources WHERE name = p_source_name;

    INSERT INTO public.companies(name, logo_url)
    VALUES (p_company_name, p_company_logo_url)
    ON CONFLICT (name) DO UPDATE
    SET logo_url = CASE
                    WHEN EXCLUDED.logo_url IS NOT NULL THEN EXCLUDED.logo_url
                    ELSE public.companies.logo_url
                END
    RETURNING id INTO v_company_id;

    v_currency_id := NULL;
    IF p_currency IS NOT NULL THEN
        INSERT INTO public.currencies(currency) VALUES (p_currency) ON CONFLICT (currency) DO NOTHING;
        SELECT id INTO v_currency_id FROM public.currencies WHERE currency = p_currency;
    END IF;

    v_salary_period_id := NULL;
    IF p_salary_period IS NOT NULL THEN
        INSERT INTO public.salary_periods(period) VALUES (p_salary_period) ON CONFLICT (period) DO NOTHING;
        SELECT id INTO v_salary_period_id FROM public.salary_periods WHERE period = p_salary_period;
    END IF;

    INSERT INTO public.leading_categories(leading_category) VALUES (p_leading_category)
    ON CONFLICT (leading_category) DO NOTHING;
    SELECT id INTO v_leading_category_id FROM public.leading_categories WHERE leading_category = p_leading_category;

    v_city_id := NULL;
    IF p_city IS NOT NULL THEN
        INSERT INTO public.cities(city) VALUES (p_city) ON CONFLICT (city) DO NOTHING;
        SELECT id INTO v_city_id FROM public.cities WHERE city = p_city;
    END IF;

    v_street_id := NULL;
    IF p_street IS NOT NULL THEN
        INSERT INTO public.streets(street) VALUES (p_street) ON CONFLICT (street) DO NOTHING;
        SELECT id INTO v_street_id FROM public.streets WHERE street = p_street;
    END IF;

    v_postal_code_id := NULL;
    IF p_postal_code IS NOT NULL THEN
        INSERT INTO public.postal_codes(postal_code) VALUES (p_postal_code) ON CONFLICT (postal_code) DO NOTHING;
        SELECT id INTO v_postal_code_id FROM public.postal_codes WHERE postal_code = p_postal_code;
    END IF;

    v_lat := NULL;
    v_lon := NULL;
    IF p_latitude IS NOT NULL AND p_longitude IS NOT NULL THEN
        v_lat := p_latitude;
        v_lon := p_longitude;
    END IF;

    v_location_detail_id := NULL;
    IF p_building_number IS NOT NULL OR v_city_id IS NOT NULL OR v_street_id IS NOT NULL OR v_postal_code_id IS NOT NULL OR v_lat IS NOT NULL OR v_lon IS NOT NULL THEN
        INSERT INTO public.location_details(building_number, latitude, longitude, city_id, street_id, postal_code_id)
        VALUES (p_building_number, v_lat, v_lon, v_city_id, v_street_id, v_postal_code_id)
        RETURNING id INTO v_location_detail_id;
    END IF;

    SELECT e.offer_id INTO v_existing_offer_id
    FROM public.external_offers e
    JOIN public.offers o ON o.id = e.offer_id
    WHERE o.source_id = v_source_id AND e.query_string = p_query_string
    LIMIT 1;

    IF v_existing_offer_id IS NULL THEN
        INSERT INTO public.offers(
            job_title, description, salary_from, salary_to, is_gross, is_remote, is_hybrid,
            published, expires, is_urgent, is_for_ukrainians,
            source_id, company_id, location_detail_id, currency_id, salary_period_id, leading_category_id
        )
        VALUES (
            p_job_title, p_description, p_salary_from, p_salary_to, p_is_gross, p_is_remote, p_is_hybrid,
            p_published, p_expires, p_is_urgent, p_is_for_ukrainians,
            v_source_id, v_company_id, v_location_detail_id, v_currency_id, v_salary_period_id, v_leading_category_id
        )
        RETURNING id INTO o_offer_id;
        INSERT INTO public.external_offers(offer_id, query_string, offer_lifespan_expiration)
        VALUES (o_offer_id, p_query_string, p_offer_lifespan_expiration);
        o_action := 'inserted';
    ELSE
        o_offer_id := v_existing_offer_id;
        UPDATE public.offers SET
            job_title = p_job_title,
            description = p_description,
            salary_from = p_salary_from,
            salary_to = p_salary_to,
            is_gross = p_is_gross,
            is_remote = p_is_remote,
            is_hybrid = p_is_hybrid,
            published = p_published,
            expires = p_expires,
            is_urgent = p_is_urgent,
            is_for_ukrainians = p_is_for_ukrainians,
            source_id = v_source_id,
            company_id = v_company_id,
            location_detail_id = v_location_detail_id,
            currency_id = v_currency_id,
            salary_period_id = v_salary_period_id,
            leading_category_id = v_leading_category_id
        WHERE id = o_offer_id;
        UPDATE public.external_offers SET query_string = p_query_string, offer_lifespan_expiration = p_offer_lifespan_expiration
        WHERE offer_id = o_offer_id;
        DELETE FROM public.skills_junction WHERE offer_id = o_offer_id;
        DELETE FROM public.education_levels_junction WHERE offer_id = o_offer_id;
        DELETE FROM public.languages_junction WHERE offer_id = o_offer_id;
        DELETE FROM public.employment_types_junction WHERE offer_id = o_offer_id;
        DELETE FROM public.employment_schedules_junction WHERE offer_id = o_offer_id;
        DELETE FROM public.benefits_junction WHERE offer_id = o_offer_id;
        DELETE FROM public.sub_categories_junction WHERE offer_id = o_offer_id;
        o_action := 'updated';
    END IF;

    IF p_skills IS NOT NULL THEN
        FOR i IN 1..array_length(p_skills,1) LOOP
            IF p_skills[i] IS NOT NULL THEN
                INSERT INTO public.skills(skill) VALUES (p_skills[i]) ON CONFLICT (skill) DO NOTHING;
                SELECT id INTO v_skill_id FROM public.skills WHERE skill = p_skills[i];
                v_skill_months := NULL;
                IF p_skills_experience_months IS NOT NULL AND i <= array_length(p_skills_experience_months,1) THEN
                    v_skill_months := p_skills_experience_months[i];
                END IF;
                v_skill_level_id := NULL;
                IF p_skills_experience_levels IS NOT NULL AND i <= array_length(p_skills_experience_levels,1) AND p_skills_experience_levels[i] IS NOT NULL THEN
                    INSERT INTO public.experience_levels(level) VALUES (p_skills_experience_levels[i]) ON CONFLICT (level) DO NOTHING;
                    SELECT id INTO v_skill_level_id FROM public.experience_levels WHERE level = p_skills_experience_levels[i];
                END IF;
                INSERT INTO public.skills_junction(offer_id, skill_id, experience_level_id, experience_months)
                VALUES (o_offer_id, v_skill_id, v_skill_level_id, v_skill_months)
                ON CONFLICT (offer_id, skill_id) DO UPDATE SET experience_level_id = EXCLUDED.experience_level_id, experience_months = EXCLUDED.experience_months;
            END IF;
        END LOOP;
    END IF;

    IF p_education_levels IS NOT NULL THEN
        FOR i IN 1..array_length(p_education_levels,1) LOOP
            IF p_education_levels[i] IS NOT NULL THEN
                INSERT INTO public.education_levels(level) VALUES (p_education_levels[i]) ON CONFLICT (level) DO NOTHING;
                SELECT id INTO v_education_level_id FROM public.education_levels WHERE level = p_education_levels[i];
                INSERT INTO public.education_levels_junction(offer_id, education_level_id)
                VALUES (o_offer_id, v_education_level_id)
                ON CONFLICT (offer_id, education_level_id) DO NOTHING;
            END IF;
        END LOOP;
    END IF;

    IF p_languages IS NOT NULL THEN
        FOR i IN 1..array_length(p_languages,1) LOOP
            IF p_languages[i] IS NOT NULL THEN
                INSERT INTO public.languages(language) VALUES (p_languages[i]) ON CONFLICT (language) DO NOTHING;
                SELECT id INTO v_language_id FROM public.languages WHERE language = p_languages[i];
                v_language_level_id := NULL;
                IF p_language_levels IS NOT NULL AND i <= array_length(p_language_levels,1) AND p_language_levels[i] IS NOT NULL THEN
                    INSERT INTO public.language_levels(level) VALUES (p_language_levels[i]) ON CONFLICT (level) DO NOTHING;
                    SELECT id INTO v_language_level_id FROM public.language_levels WHERE level = p_language_levels[i];
                END IF;
                INSERT INTO public.languages_junction(offer_id, language_id, language_level_id)
                VALUES (o_offer_id, v_language_id, v_language_level_id)
                ON CONFLICT (offer_id, language_id) DO UPDATE SET language_level_id = EXCLUDED.language_level_id;
            END IF;
        END LOOP;
    END IF;

    IF p_employment_types IS NOT NULL THEN
        FOR i IN 1..array_length(p_employment_types,1) LOOP
            IF p_employment_types[i] IS NOT NULL THEN
                INSERT INTO public.employment_types(type) VALUES (p_employment_types[i]) ON CONFLICT (type) DO NOTHING;
                SELECT id INTO v_emp_type_id FROM public.employment_types WHERE type = p_employment_types[i];
                INSERT INTO public.employment_types_junction(offer_id, employment_type_id)
                VALUES (o_offer_id, v_emp_type_id)
                ON CONFLICT (offer_id, employment_type_id) DO NOTHING;
            END IF;
        END LOOP;
    END IF;

    IF p_employment_schedules IS NOT NULL THEN
        FOR i IN 1..array_length(p_employment_schedules,1) LOOP
            IF p_employment_schedules[i] IS NOT NULL THEN
                INSERT INTO public.employment_schedules(schedule) VALUES (p_employment_schedules[i]) ON CONFLICT (schedule) DO NOTHING;
                SELECT id INTO v_emp_schedule_id FROM public.employment_schedules WHERE schedule = p_employment_schedules[i];
                INSERT INTO public.employment_schedules_junction(offer_id, employment_schedule_id)
                VALUES (o_offer_id, v_emp_schedule_id)
                ON CONFLICT (offer_id, employment_schedule_id) DO NOTHING;
            END IF;
        END LOOP;
    END IF;

    IF p_benefits IS NOT NULL THEN
        FOR i IN 1..array_length(p_benefits,1) LOOP
            IF p_benefits[i] IS NOT NULL THEN
                INSERT INTO public.benefits(benefit) VALUES (p_benefits[i]) ON CONFLICT (benefit) DO NOTHING;
                SELECT id INTO v_benefit_id FROM public.benefits WHERE benefit = p_benefits[i];
                INSERT INTO public.benefits_junction(offer_id, benefit_id)
                VALUES (o_offer_id, v_benefit_id)
                ON CONFLICT (offer_id, benefit_id) DO NOTHING;
            END IF;
        END LOOP;
    END IF;

    IF p_sub_categories IS NOT NULL THEN
        FOR i IN 1..array_length(p_sub_categories,1) LOOP
            IF p_sub_categories[i] IS NOT NULL THEN
                INSERT INTO public.sub_categories(sub_category) VALUES (p_sub_categories[i]) ON CONFLICT (sub_category) DO NOTHING;
                SELECT id INTO v_sub_category_id FROM public.sub_categories WHERE sub_category = p_sub_categories[i];
                INSERT INTO public.sub_categories_junction(offer_id, sub_category_id)
                VALUES (o_offer_id, v_sub_category_id)
                ON CONFLICT (offer_id, sub_category_id) DO NOTHING;
            END IF;
        END LOOP;
    END IF;
END;
$$;

CREATE OR REPLACE PROCEDURE public.upsert_external_offers_batch(
    IN p_inputs public.external_offer_uos_input[],
    OUT o_results public.batch_result[]
)
LANGUAGE plpgsql
AS $$
DECLARE
    i int;
    r public.external_offer_uos_input;
    v_offer_id BIGINT;
    v_action TEXT;
    v_error TEXT;
    v_results public.batch_result[] := '{}';
BEGIN
    IF p_inputs IS NULL OR array_length(p_inputs, 1) IS NULL THEN
        o_results := v_results;
        RETURN;
    END IF;

    FOR i IN 1..array_length(p_inputs, 1) LOOP
        r := p_inputs[i];
        v_error := NULL;
        v_action := NULL;
        v_offer_id := NULL;

        -- Ten blok BEGIN...END sam w sobie tworzy subtransakcję
        BEGIN
            -- Sprawdzamy, czy element nie jest pusty (np. po błędzie parsowania w C#)
            IF r IS NULL THEN
                v_error := 'Input item at this index was null.';
            ELSE
                -- Wywołujemy procedurę dla pojedynczej oferty
                CALL public.upsert_external_offer(
                    r.source_name,
                    r.source_base_url,
                    r.query_string,
                    r.job_title,
                    r.company_name,
                    r.company_logo_url,
                    r.description,
                    r.salary_from,
                    r.salary_to,
                    r.currency,
                    r.salary_period,
                    r.is_gross,
                    r.building_number,
                    r.street,
                    r.city,
                    r.postal_code,
                    r.latitude,
                    r.longitude,
                    r.is_remote,
                    r.is_hybrid,
                    r.leading_category,
                    r.sub_categories,
                    r.skills,
                    r.skills_experience_months,
                    r.skills_experience_levels,
                    r.education_levels,
                    r.languages,
                    r.language_levels,
                    r.employment_types,
                    r.employment_schedules,
                    r.published,
                    r.expires,
                    r.benefits,
                    r.is_urgent,
                    r.is_for_ukrainians,
                    r.offer_lifespan_expiration,
                    v_offer_id, -- OUT
                    v_action        -- OUT
                );
            END IF;

        EXCEPTION WHEN OTHERS THEN
            -- Kiedy tu trafiamy, subtransakcja (CALL) JEST JUŻ WYCOFANA.
            -- Musimy tylko pobrać komunikat błędu.
            GET STACKED DIAGNOSTICS v_error = MESSAGE_TEXT;
        END;
        -- Koniec subtransakcji (albo pomyślny, albo obsłużony)

        -- Dodajemy wynik (sukces lub błąd) do tablicy wyjściowej
        v_results := v_results || ROW(i, v_offer_id, v_action, v_error)::public.batch_result;

    END LOOP;

    -- Zwracamy całą tablicę wyników
    o_results := v_results;
    
    -- Główna transakcja zostanie zatwierdzona (COMMIT) automatycznie na końcu.
END;
$$;;