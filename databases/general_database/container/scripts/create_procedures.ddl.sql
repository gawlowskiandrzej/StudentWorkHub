CREATE OR REPLACE PROCEDURE public.standard_add_user(
    IN  p_email      text,
    IN  p_password   text,
    IN  p_first_name text,
    IN  p_last_name  text,
    OUT o_success    boolean,
    OUT o_message    text,
    OUT o_user_id    bigint
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_first_name_id bigint;
    v_last_name_id  bigint;

    -- diagnostics
    v_sqlstate   text;
    v_constraint text;
    v_schema     text;
    v_table      text;
    v_detail     text;
    v_hint       text;
    v_context    text;
BEGIN
    -- Email must not be empty
    IF p_email IS NULL OR btrim(p_email) = '' THEN
        o_success := false;
        o_message := 'Email must not be empty.';
        o_user_id := NULL;
        RETURN;
    END IF;

    -- Password (hash) must not be empty
    IF p_password IS NULL OR btrim(p_password) = '' THEN
        o_success := false;
        o_message := 'Password must not be empty.';
        o_user_id := NULL;
        RETURN;
    END IF;

    -- Email format validation
    IF p_email !~* '^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$' THEN
        o_success := false;
        o_message := 'Email format is invalid.';
        o_user_id := NULL;
        RETURN;
    END IF;

    -- Insert or get first_name
    INSERT INTO public.first_names(first_name)
    VALUES (p_first_name)
    ON CONFLICT (first_name) DO UPDATE
        SET first_name = EXCLUDED.first_name
    RETURNING id INTO v_first_name_id;

    -- Insert or get last_name
    INSERT INTO public.last_names(last_name)
    VALUES (p_last_name)
    ON CONFLICT (last_name) DO UPDATE
        SET last_name = EXCLUDED.last_name
    RETURNING id INTO v_last_name_id;

    -- Insert user row
    BEGIN
        INSERT INTO public.users(
            email,
            password,
            first_name_id,
            last_name_id
        )
        VALUES (
            p_email,
            p_password,
            v_first_name_id,
            v_last_name_id
        )
        RETURNING id INTO o_user_id;

        INSERT INTO public.weights(user_id)
        VALUES (o_user_id);

        INSERT INTO public.preferences(user_id)
        VALUES (o_user_id);

        o_success := true;
        o_message := 'User created successfully.';
        RETURN;

    EXCEPTION
        WHEN unique_violation THEN
            o_success := false;
            o_message := 'User with this email already exists.';
            o_user_id := NULL;
            RETURN;

        WHEN check_violation THEN
            o_success := false;
            o_message := 'Check constraint violation while inserting user.';
            o_user_id := NULL;
            RETURN;

        WHEN foreign_key_violation THEN
            GET STACKED DIAGNOSTICS
                v_sqlstate   = RETURNED_SQLSTATE,
                v_constraint = CONSTRAINT_NAME,
                v_schema     = SCHEMA_NAME,
                v_table      = TABLE_NAME,
                v_detail     = PG_EXCEPTION_DETAIL,
                v_hint       = PG_EXCEPTION_HINT;

            o_success := false;
            o_message :=
                format(
                    'Foreign key violation (SQLSTATE %s) on %I.%I, constraint %I. %s%s%s',
                    v_sqlstate,
                    coalesce(v_schema, 'public'),
                    coalesce(v_table, '?'),
                    coalesce(v_constraint, '?'),
                    SQLERRM,
                    CASE WHEN v_detail IS NOT NULL THEN E'\nDETAIL: ' || v_detail ELSE '' END,
                    CASE WHEN v_hint   IS NOT NULL THEN E'\nHINT: '   || v_hint   ELSE '' END
                );
            o_user_id := NULL;
            RETURN;

        WHEN others THEN
            GET STACKED DIAGNOSTICS
                v_sqlstate = RETURNED_SQLSTATE,
                v_detail   = PG_EXCEPTION_DETAIL,
                v_hint     = PG_EXCEPTION_HINT,
                v_context  = PG_EXCEPTION_CONTEXT;

            o_success := false;
            o_message :=
                format(
                    'Unexpected error (SQLSTATE %s): %s%s%s%s',
                    v_sqlstate,
                    SQLERRM,
                    CASE WHEN v_detail  IS NOT NULL THEN E'\nDETAIL: '  || v_detail  ELSE '' END,
                    CASE WHEN v_hint    IS NOT NULL THEN E'\nHINT: '    || v_hint    ELSE '' END,
                    CASE WHEN v_context IS NOT NULL THEN E'\nCONTEXT: ' || v_context ELSE '' END
                );
            o_user_id := NULL;
            RETURN;
    END;
END;
$$;


CREATE OR REPLACE PROCEDURE public.set_user_remember_token(
    IN  p_user_id   BIGINT,
    IN  p_token     TEXT,
    OUT p_success   BOOLEAN
)
LANGUAGE plpgsql
AS $$
BEGIN
    -- user_id must not be NULL
    IF p_user_id IS NULL THEN
        p_success := FALSE;
        RETURN;
    END IF;

    -- Token can be NULL (clear), but cannot be empty/whitespace if provided
    IF p_token IS NOT NULL AND length(btrim(p_token)) = 0 THEN
        p_success := FALSE;
        RETURN;
    END IF;

    BEGIN
        UPDATE public.users
        SET remember_token = p_token   -- can be NULL (will clear token)
        WHERE id = p_user_id;

        IF NOT FOUND THEN
            p_success := FALSE;
        ELSE
            p_success := TRUE;
        END IF;

    EXCEPTION
        WHEN unique_violation THEN
            p_success := FALSE;
    END;
END;
$$;

CREATE OR REPLACE PROCEDURE public.set_user_password(
    IN  p_user_id BIGINT,
    IN  p_password TEXT,
    OUT p_success  BOOLEAN
)
LANGUAGE plpgsql
AS $$
BEGIN
    -- user_id must not be NULL
    IF p_user_id IS NULL THEN
        p_success := FALSE;
        RETURN;
    END IF;

    -- Password must not be NULL or empty
    IF p_password IS NULL OR btrim(p_password) = '' THEN
        p_success := FALSE;
        RETURN;
    END IF;

    BEGIN
        UPDATE public.users
        SET password = p_password
        WHERE id = p_user_id;

        IF NOT FOUND THEN
            p_success := FALSE;
        ELSE
            p_success := TRUE;
        END IF;

    EXCEPTION
        WHEN others THEN
            p_success := FALSE;
    END;
END;
$$;

CREATE OR REPLACE PROCEDURE public.set_user_phone(
    IN  p_user_id BIGINT,
    IN  p_phone   TEXT,
    OUT p_success BOOLEAN
)
LANGUAGE plpgsql
AS $$
BEGIN
    -- user_id must not be NULL
    IF p_user_id IS NULL THEN
        p_success := FALSE;
        RETURN;
    END IF;

    -- Phone can be NULL (clear), but if provided cannot be empty and must match pattern
    IF p_phone IS NOT NULL THEN
        IF btrim(p_phone) = '' THEN
            p_success := FALSE;
            RETURN;
        END IF;

        IF p_phone !~ '^\+[0-9]{7,15}$' THEN
            p_success := FALSE;
            RETURN;
        END IF;
    END IF;

    BEGIN
        UPDATE public.users
        SET phone = p_phone
        WHERE id = p_user_id;

        IF NOT FOUND THEN
            p_success := FALSE;
        ELSE
            p_success := TRUE;
        END IF;

    EXCEPTION
        WHEN unique_violation THEN
            -- phone must be unique
            p_success := FALSE;

        WHEN check_violation THEN
            -- phone did not match CHECK constraint
            p_success := FALSE;

        WHEN others THEN
            p_success := FALSE;
    END;
END;
$$;

CREATE OR REPLACE PROCEDURE public.set_user_first_name(
    IN  p_user_id     BIGINT,
    IN  p_first_name  TEXT,
    OUT p_success     BOOLEAN
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_first_name_id BIGINT;
BEGIN
    -- user_id must not be NULL
    IF p_user_id IS NULL THEN
        p_success := FALSE;
        RETURN;
    END IF;

    -- first_name must not be NULL or empty
    IF p_first_name IS NULL OR btrim(p_first_name) = '' THEN
        p_success := FALSE;
        RETURN;
    END IF;

    -- Insert or get first_name from dictionary
    INSERT INTO public.first_names(first_name)
    VALUES (p_first_name)
    ON CONFLICT (first_name) DO UPDATE
        SET first_name = EXCLUDED.first_name
    RETURNING id INTO v_first_name_id;

    BEGIN
        UPDATE public.users
        SET first_name_id = v_first_name_id
        WHERE id = p_user_id;

        IF NOT FOUND THEN
            p_success := FALSE;
        ELSE
            p_success := TRUE;
        END IF;

    EXCEPTION
        WHEN foreign_key_violation THEN
            p_success := FALSE;

        WHEN others THEN
            p_success := FALSE;
    END;
END;
$$;

CREATE OR REPLACE PROCEDURE public.set_user_second_name(
    IN  p_user_id      BIGINT,
    IN  p_second_name  TEXT,
    OUT p_success      BOOLEAN
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_second_name_id BIGINT;
BEGIN
    -- user_id must not be NULL
    IF p_user_id IS NULL THEN
        p_success := FALSE;
        RETURN;
    END IF;

    -- second_name must not be NULL or empty
    IF p_second_name IS NULL OR btrim(p_second_name) = '' THEN
        p_success := FALSE;
        RETURN;
    END IF;

    -- Insert or get second_name from dictionary
    INSERT INTO public.second_names(second_name)
    VALUES (p_second_name)
    ON CONFLICT (second_name) DO UPDATE
        SET second_name = EXCLUDED.second_name
    RETURNING id INTO v_second_name_id;

    BEGIN
        UPDATE public.users
        SET second_name_id = v_second_name_id
        WHERE id = p_user_id;

        IF NOT FOUND THEN
            p_success := FALSE;
        ELSE
            p_success := TRUE;
        END IF;

    EXCEPTION
        WHEN foreign_key_violation THEN
            p_success := FALSE;

        WHEN others THEN
            p_success := FALSE;
    END;
END;
$$;

CREATE OR REPLACE PROCEDURE public.set_user_last_name(
    IN  p_user_id    BIGINT,
    IN  p_last_name  TEXT,
    OUT p_success    BOOLEAN
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_last_name_id BIGINT;
BEGIN
    -- user_id must not be NULL
    IF p_user_id IS NULL THEN
        p_success := FALSE;
        RETURN;
    END IF;

    -- last_name must not be NULL or empty
    IF p_last_name IS NULL OR btrim(p_last_name) = '' THEN
        p_success := FALSE;
        RETURN;
    END IF;

    -- Insert or get last_name from dictionary
    INSERT INTO public.last_names(last_name)
    VALUES (p_last_name)
    ON CONFLICT (last_name) DO UPDATE
        SET last_name = EXCLUDED.last_name
    RETURNING id INTO v_last_name_id;

    BEGIN
        UPDATE public.users
        SET last_name_id = v_last_name_id
        WHERE id = p_user_id;

        IF NOT FOUND THEN
            p_success := FALSE;
        ELSE
            p_success := TRUE;
        END IF;

    EXCEPTION
        WHEN foreign_key_violation THEN
            p_success := FALSE;

        WHEN others THEN
            p_success := FALSE;
    END;
END;
$$;

CREATE OR REPLACE PROCEDURE public.delete_user(
    IN  p_user_id BIGINT,
    OUT p_success BOOLEAN
)
LANGUAGE plpgsql
AS $$
BEGIN
    -- user_id must not be NULL
    IF p_user_id IS NULL THEN
        p_success := FALSE;
        RETURN;
    END IF;

    BEGIN
        DELETE FROM public.users
        WHERE id = p_user_id;

        IF NOT FOUND THEN
            p_success := FALSE;
        ELSE
            p_success := TRUE;
        END IF;

    EXCEPTION
        WHEN others THEN
            p_success := FALSE;
    END;
END;
$$;

CREATE OR REPLACE PROCEDURE public.set_weights_order_by_option(
    IN  p_user_id BIGINT,
    IN  p_order_by_option TEXT[],
    OUT p_success BOOLEAN
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_row_count INTEGER;
BEGIN
    UPDATE public.weights
    SET order_by_option = p_order_by_option
    WHERE user_id = p_user_id;

    GET DIAGNOSTICS v_row_count = ROW_COUNT;
    p_success := (v_row_count = 1);
END;
$$;

CREATE OR REPLACE PROCEDURE public.set_weights_mean_value_ids(
    IN  p_user_id BIGINT,
    IN  p_mean_value_ids TEXT[],
    OUT p_success BOOLEAN
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_row_count INTEGER;
BEGIN
    UPDATE public.weights
    SET mean_value_ids = p_mean_value_ids
    WHERE user_id = p_user_id;

    GET DIAGNOSTICS v_row_count = ROW_COUNT;
    p_success := (v_row_count = 1);
END;
$$;

CREATE OR REPLACE PROCEDURE public.set_weights_vector(
    IN  p_user_id BIGINT,
    IN  p_vector REAL[],
    OUT p_success BOOLEAN
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_row_count INTEGER;
BEGIN
    UPDATE public.weights
    SET vector = p_vector
    WHERE user_id = p_user_id;

    GET DIAGNOSTICS v_row_count = ROW_COUNT;
    p_success := (v_row_count = 1);
END;
$$;

CREATE OR REPLACE PROCEDURE public.set_weights_mean_dist(
    IN  p_user_id BIGINT,
    IN  p_mean_dist REAL[],
    OUT p_success BOOLEAN
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_row_count INTEGER;
BEGIN
    UPDATE public.weights
    SET mean_dist = p_mean_dist
    WHERE user_id = p_user_id;

    GET DIAGNOSTICS v_row_count = ROW_COUNT;
    p_success := (v_row_count = 1);
END;
$$;

CREATE OR REPLACE PROCEDURE public.set_weights_means_value_sum(
    IN  p_user_id BIGINT,
    IN  p_means_value_sum REAL[],
    OUT p_success BOOLEAN
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_row_count INTEGER;
BEGIN
    UPDATE public.weights
    SET means_value_sum = p_means_value_sum
    WHERE user_id = p_user_id;

    GET DIAGNOSTICS v_row_count = ROW_COUNT;
    p_success := (v_row_count = 1);
END;
$$;

CREATE OR REPLACE PROCEDURE public.set_weights_means_value_ssum(
    IN  p_user_id BIGINT,
    IN  p_means_value_ssum DOUBLE PRECISION[],
    OUT p_success BOOLEAN
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_row_count INTEGER;
BEGIN
    UPDATE public.weights
    SET means_value_ssum = p_means_value_ssum
    WHERE user_id = p_user_id;

    GET DIAGNOSTICS v_row_count = ROW_COUNT;
    p_success := (v_row_count = 1);
END;
$$;

CREATE OR REPLACE PROCEDURE public.set_weights_means_value_count(
    IN  p_user_id BIGINT,
    IN  p_means_value_count INTEGER[],
    OUT p_success BOOLEAN
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_row_count INTEGER;
BEGIN
    UPDATE public.weights
    SET means_value_count = p_means_value_count
    WHERE user_id = p_user_id;

    GET DIAGNOSTICS v_row_count = ROW_COUNT;
    p_success := (v_row_count = 1);
END;
$$;

CREATE OR REPLACE PROCEDURE public.set_weights_means_weight_sum(
    IN  p_user_id BIGINT,
    IN  p_means_weight_sum REAL[],
    OUT p_success BOOLEAN
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_row_count INTEGER;
BEGIN
    UPDATE public.weights
    SET means_weight_sum = p_means_weight_sum
    WHERE user_id = p_user_id;

    GET DIAGNOSTICS v_row_count = ROW_COUNT;
    p_success := (v_row_count = 1);
END;
$$;

CREATE OR REPLACE PROCEDURE public.set_weights_means_weight_ssum(
    IN  p_user_id BIGINT,
    IN  p_means_weight_ssum DOUBLE PRECISION[],
    OUT p_success BOOLEAN
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_row_count INTEGER;
BEGIN
    UPDATE public.weights
    SET means_weight_ssum = p_means_weight_ssum
    WHERE user_id = p_user_id;

    GET DIAGNOSTICS v_row_count = ROW_COUNT;
    p_success := (v_row_count = 1);
END;
$$;

CREATE OR REPLACE PROCEDURE public.set_weights_means_weight_count(
    IN  p_user_id BIGINT,
    IN  p_means_weight_count INTEGER[],
    OUT p_success BOOLEAN
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_row_count INTEGER;
BEGIN
    UPDATE public.weights
    SET means_weight_count = p_means_weight_count
    WHERE user_id = p_user_id;

    GET DIAGNOSTICS v_row_count = ROW_COUNT;
    p_success := (v_row_count = 1);
END;
$$;

CREATE OR REPLACE PROCEDURE public.insert_search_history(
    OUT o_result                  BOOLEAN,
    IN  p_user_id                 BIGINT,
    IN  p_keywords                VARCHAR(1024) DEFAULT NULL,
    IN  p_distance                INTEGER       DEFAULT 0,
    IN  p_is_remote               BOOLEAN       DEFAULT NULL,
    IN  p_is_hybrid               BOOLEAN       DEFAULT NULL,
    IN  p_leading_category_id     SMALLINT      DEFAULT NULL,
    IN  p_city_id                 INTEGER       DEFAULT NULL,
    IN  p_salary_from             NUMERIC(8,2)  DEFAULT 0.0,
    IN  p_salary_to               NUMERIC(8,2)  DEFAULT 0.0,
    IN  p_salary_period_id        SMALLINT      DEFAULT NULL,
    IN  p_salary_currency_id      SMALLINT      DEFAULT NULL,
    IN  p_employment_schedule_ids SMALLINT[]    DEFAULT '{}',
    IN  p_employment_type_ids     SMALLINT[]    DEFAULT '{}'
)
LANGUAGE plpgsql
AS $$
BEGIN
    o_result := FALSE;

    IF p_user_id IS NULL THEN
        RAISE EXCEPTION 'user_id cannot be null';
    END IF;

    INSERT INTO public.search_histories (
        keywords,
        distance,
        is_remote,
        is_hybrid,
        leading_category_id,
        city_id,
        user_id,
        salary_from,
        salary_to,
        salary_period_id,
        salary_currency_id,
        employment_schedule_ids,
        employment_type_ids
    )
    VALUES (
        p_keywords,
        COALESCE(p_distance, 0),
        p_is_remote,
        p_is_hybrid,
        p_leading_category_id,
        p_city_id,
        p_user_id,
        COALESCE(p_salary_from, 0.0),
        COALESCE(p_salary_to, 0.0),
        p_salary_period_id,
        p_salary_currency_id,
        COALESCE(p_employment_schedule_ids, '{}'),
        COALESCE(p_employment_type_ids, '{}')
    );

    o_result := TRUE;
    RETURN;

EXCEPTION
    WHEN OTHERS THEN
        o_result := FALSE;
        RETURN;
END;
$$;

CREATE OR REPLACE PROCEDURE public.set_leading_category_preference(
    IN  p_user_id BIGINT,
    IN  p_leading_category_id SMALLINT,
    OUT p_success BOOLEAN
)
LANGUAGE plpgsql
AS $$
BEGIN
    IF p_leading_category_id IS NULL OR p_leading_category_id < 1 THEN
        p_success := FALSE;
        RETURN;
    END IF;

    UPDATE public.preferences
    SET leading_category_id = p_leading_category_id
    WHERE user_id = p_user_id;

    p_success := FOUND;

EXCEPTION
    WHEN OTHERS THEN
        p_success := FALSE;
END;
$$;

CREATE OR REPLACE PROCEDURE public.set_salary_range_preference (
    IN  p_user_id BIGINT,
    IN  p_salary_from NUMERIC(8,2),
    IN  p_salary_to   NUMERIC(8,2),
    OUT p_success BOOLEAN
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_current_from NUMERIC(8,2);
    v_current_to   NUMERIC(8,2);

    v_new_from NUMERIC(8,2);
    v_new_to   NUMERIC(8,2);

    v_any_change BOOLEAN := FALSE;
    v_tmp NUMERIC(8,2);
BEGIN
    p_success := FALSE;

    SELECT salary_from, salary_to
    INTO v_current_from, v_current_to
    FROM public.preferences
    WHERE user_id = p_user_id;

    IF NOT FOUND THEN
        RETURN;
    END IF;

    v_new_from := v_current_from;
    v_new_to   := v_current_to;

    IF p_salary_from IS NOT NULL AND p_salary_from >= 1 THEN
        v_new_from := p_salary_from;
        v_any_change := TRUE;
    END IF;

    IF p_salary_to IS NOT NULL AND p_salary_to >= 1 THEN
        v_new_to := p_salary_to;
        v_any_change := TRUE;
    END IF;

    IF NOT v_any_change THEN
        RETURN;
    END IF;

    IF v_new_from IS NOT NULL AND v_new_to IS NOT NULL AND v_new_to < v_new_from THEN
        v_tmp := v_new_from;
        v_new_from := v_new_to;
        v_new_to := v_tmp;
    END IF;

    UPDATE public.preferences
    SET salary_from = v_new_from,
        salary_to   = v_new_to
    WHERE user_id = p_user_id;

    p_success := FOUND;

EXCEPTION
    WHEN OTHERS THEN
        p_success := FALSE;
END;
$$;

CREATE OR REPLACE PROCEDURE public.set_employment_type_preference (
    IN  p_user_id BIGINT,
    IN  p_employment_type_ids SMALLINT[],
    OUT p_success BOOLEAN
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_new_ids SMALLINT[];
BEGIN
    p_success := FALSE;

    IF p_employment_type_ids IS NULL THEN
        RETURN;
    END IF;

    SELECT COALESCE(array_agg(val ORDER BY first_ord), '{}'::SMALLINT[])
    INTO v_new_ids
    FROM (
        SELECT val, MIN(ord) AS first_ord
        FROM unnest(p_employment_type_ids) WITH ORDINALITY u(val, ord)
        WHERE val IS NOT NULL AND val >= 1
        GROUP BY val
    ) s;

    UPDATE public.preferences
    SET employment_type_ids = v_new_ids
    WHERE user_id = p_user_id;

    p_success := FOUND;

EXCEPTION
    WHEN OTHERS THEN
        p_success := FALSE;
END;
$$;

CREATE OR REPLACE PROCEDURE public.set_language_preference (
    IN  p_user_id BIGINT,
    IN  p_language_id SMALLINT,
    IN  p_language_level_id SMALLINT,
    OUT p_success BOOLEAN
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_lang_ids  SMALLINT[];
    v_level_ids SMALLINT[];
BEGIN
    p_success := FALSE;

    SELECT language_ids, language_level_ids
    INTO v_lang_ids, v_level_ids
    FROM public.preferences
    WHERE user_id = p_user_id
    FOR UPDATE;

    IF NOT FOUND THEN
        RETURN;
    END IF;

    v_lang_ids  := COALESCE(v_lang_ids,  '{}'::SMALLINT[]);
    v_level_ids := COALESCE(v_level_ids, '{}'::SMALLINT[]);

    IF cardinality(v_lang_ids) <> cardinality(v_level_ids) THEN
        v_lang_ids  := '{}'::SMALLINT[];
        v_level_ids := '{}'::SMALLINT[];
    END IF;

    IF p_language_id IS NULL OR p_language_level_id IS NULL THEN
        IF p_language_id IS NULL OR p_language_id < 1 THEN
            v_lang_ids  := '{}'::SMALLINT[];
            v_level_ids := '{}'::SMALLINT[];
        ELSE
            SELECT
                COALESCE(array_agg(v_lang_ids[i] ORDER BY i),  '{}'::SMALLINT[]),
                COALESCE(array_agg(v_level_ids[i] ORDER BY i), '{}'::SMALLINT[])
            INTO v_lang_ids, v_level_ids
            FROM generate_subscripts(v_lang_ids, 1) g(i)
            WHERE v_lang_ids[i] <> p_language_id;
        END IF;

        UPDATE public.preferences
        SET language_ids = v_lang_ids,
            language_level_ids = v_level_ids
        WHERE user_id = p_user_id;

        p_success := FOUND;
        RETURN;
    END IF;

    IF p_language_id < 1 OR p_language_level_id < 1 THEN
        RETURN;
    END IF;

    IF array_position(v_lang_ids, p_language_id) IS NULL THEN
        v_lang_ids  := array_append(v_lang_ids,  p_language_id);
        v_level_ids := array_append(v_level_ids, p_language_level_id);
    ELSE
        v_level_ids[array_position(v_lang_ids, p_language_id)] := p_language_level_id;
    END IF;

    UPDATE public.preferences
    SET language_ids = v_lang_ids,
        language_level_ids = v_level_ids
    WHERE user_id = p_user_id;

    p_success := FOUND;

EXCEPTION
    WHEN OTHERS THEN
        p_success := FALSE;
END;
$$;

CREATE OR REPLACE PROCEDURE public.set_job_status_preference(
    IN  p_user_id BIGINT,
    IN  p_status_name VARCHAR(15),
    OUT p_success BOOLEAN
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_status_name VARCHAR(15);
    v_status_id   SMALLINT;
BEGIN
    p_success := FALSE;

    v_status_name := btrim(p_status_name);

    IF v_status_name IS NULL OR v_status_name = '' THEN
        RETURN;
    END IF;

    IF char_length(v_status_name) > 15 THEN
        RETURN;
    END IF;

    SELECT js.id
    INTO v_status_id
    FROM public.job_statuses js
    WHERE js.status_name = v_status_name
    LIMIT 1;

    IF NOT FOUND THEN
        BEGIN
            INSERT INTO public.job_statuses (status_name)
            VALUES (v_status_name)
            RETURNING id INTO v_status_id;

        EXCEPTION
            WHEN unique_violation THEN
                SELECT js.id
                INTO v_status_id
                FROM public.job_statuses js
                WHERE js.status_name = v_status_name
                LIMIT 1;
        END;
    END IF;

    UPDATE public.preferences
    SET job_status_id = v_status_id
    WHERE user_id = p_user_id;

    p_success := FOUND;

EXCEPTION
    WHEN OTHERS THEN
        p_success := FALSE;
END;
$$;

CREATE OR REPLACE PROCEDURE public.set_city_preference(
    IN  p_user_id BIGINT,
    IN  p_city_name VARCHAR(256),
    OUT p_success BOOLEAN
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_city_name VARCHAR(256);
    v_city_id   BIGINT;
BEGIN
    p_success := FALSE;

    v_city_name := btrim(p_city_name);

    IF v_city_name IS NULL OR v_city_name = '' THEN
        RETURN;
    END IF;

    IF char_length(v_city_name) > 256 THEN
        RETURN;
    END IF;

    SELECT c.id
    INTO v_city_id
    FROM public.cities c
    WHERE c.city = v_city_name
    LIMIT 1;

    IF NOT FOUND THEN
        BEGIN
            INSERT INTO public.cities (city)
            VALUES (v_city_name)
            RETURNING id INTO v_city_id;

        EXCEPTION
            WHEN unique_violation THEN
                SELECT c.id
                INTO v_city_id
                FROM public.cities c
                WHERE c.city = v_city_name
                LIMIT 1;
        END;
    END IF;

    UPDATE public.preferences
    SET city_id = v_city_id
    WHERE user_id = p_user_id;

    p_success := FOUND;

EXCEPTION
    WHEN OTHERS THEN
        p_success := FALSE;
END;
$$;

CREATE OR REPLACE PROCEDURE public.set_work_types_preference(
    IN  p_user_id BIGINT,
    IN  p_work_types TEXT[],
    IN  p_work_type_ids SMALLINT[],
    OUT p_success BOOLEAN
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_desired_ids SMALLINT[] := '{}'::SMALLINT[];
    v_name TEXT;
    v_id SMALLINT;
BEGIN
    p_success := FALSE;

    IF p_work_types IS NULL AND p_work_type_ids IS NULL THEN
        RETURN;
    END IF;

    PERFORM 1
    FROM public.preferences
    WHERE user_id = p_user_id
    FOR UPDATE;

    IF NOT FOUND THEN
        RETURN;
    END IF;

    SELECT COALESCE(array_agg(val ORDER BY first_ord), '{}'::SMALLINT[])
    INTO v_desired_ids
    FROM (
        SELECT val::SMALLINT AS val, MIN(ord) AS first_ord
        FROM unnest(COALESCE(p_work_type_ids, '{}'::SMALLINT[])) WITH ORDINALITY u(val, ord)
        WHERE val IS NOT NULL AND val >= 1
        GROUP BY val
    ) s;

    IF p_work_types IS NOT NULL THEN
        FOR v_name IN
            SELECT val
            FROM (
                SELECT btrim(val) AS val, MIN(ord) AS first_ord
                FROM unnest(p_work_types) WITH ORDINALITY u(val, ord)
                WHERE val IS NOT NULL
                  AND btrim(val) <> ''
                  AND char_length(btrim(val)) <= 256
                GROUP BY btrim(val)
            ) t
            ORDER BY first_ord
        LOOP
            SELECT wt.id
            INTO v_id
            FROM public.work_types wt
            WHERE wt.work_type = v_name
            LIMIT 1;

            IF NOT FOUND THEN
                BEGIN
                    INSERT INTO public.work_types (work_type)
                    VALUES (v_name)
                    RETURNING id INTO v_id;
                EXCEPTION
                    WHEN unique_violation THEN
                        SELECT wt.id
                        INTO v_id
                        FROM public.work_types wt
                        WHERE wt.work_type = v_name
                        LIMIT 1;
                END;
            END IF;

            IF v_id IS NOT NULL AND v_id >= 1 AND array_position(v_desired_ids, v_id) IS NULL THEN
                v_desired_ids := array_append(v_desired_ids, v_id);
            END IF;
        END LOOP;
    END IF;

    IF cardinality(v_desired_ids) = 0 THEN
        DELETE FROM public.work_types_junction
        WHERE user_id = p_user_id;
    ELSE
        DELETE FROM public.work_types_junction
        WHERE user_id = p_user_id
          AND NOT (work_type_id = ANY (v_desired_ids));

        INSERT INTO public.work_types_junction (work_type_id, user_id)
        SELECT x, p_user_id
        FROM unnest(v_desired_ids) AS x
        ON CONFLICT DO NOTHING;
    END IF;

    p_success := TRUE;

EXCEPTION
    WHEN OTHERS THEN
        p_success := FALSE;
END;
$$;

CREATE OR REPLACE PROCEDURE public.set_skills_preference(
    IN  p_user_id BIGINT,
    IN  p_skill_names TEXT[],
    IN  p_experience_months SMALLINT[],
    OUT p_success BOOLEAN
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_len_names INT;
    v_len_months INT;
    v_i INT;

    v_name TEXT;
    v_months SMALLINT;
    v_skill_id BIGINT;
BEGIN
    p_success := FALSE;

    IF p_skill_names IS NULL OR p_experience_months IS NULL THEN
        RETURN;
    END IF;

    v_len_names := cardinality(p_skill_names);
    v_len_months := cardinality(p_experience_months);

    IF v_len_names IS DISTINCT FROM v_len_months THEN
        RETURN;
    END IF;

    CREATE TEMP TABLE tmp_desired_user_skills (
        skill_id BIGINT PRIMARY KEY,
        experience_months SMALLINT NOT NULL
    ) ON COMMIT DROP;

    FOR v_i IN 1..COALESCE(v_len_names, 0) LOOP
        v_name := btrim(p_skill_names[v_i]);
        v_months := p_experience_months[v_i];

        IF v_name IS NULL OR v_name = '' THEN
            CONTINUE;
        END IF;

        IF char_length(v_name) > 256 THEN
            CONTINUE;
        END IF;

        IF v_months IS NULL OR v_months < 0 THEN
            CONTINUE;
        END IF;

        INSERT INTO public.skills (skill_name)
        VALUES (v_name)
        ON CONFLICT (skill_name)
        DO UPDATE SET skill_name = EXCLUDED.skill_name
        RETURNING id INTO v_skill_id;

        INSERT INTO tmp_desired_user_skills (skill_id, experience_months)
        VALUES (v_skill_id, v_months)
        ON CONFLICT (skill_id)
        DO UPDATE SET experience_months = EXCLUDED.experience_months;
    END LOOP;

    IF (SELECT COUNT(*) FROM tmp_desired_user_skills) = 0 THEN
        DELETE FROM public.user_skills
        WHERE user_id = p_user_id;

        p_success := TRUE;
        RETURN;
    END IF;

    DELETE FROM public.user_skills us
    WHERE us.user_id = p_user_id
      AND NOT EXISTS (
          SELECT 1
          FROM tmp_desired_user_skills d
          WHERE d.skill_id = us.skill_id
      );

    INSERT INTO public.user_skills (user_id, skill_id, experience_months)
    SELECT p_user_id, d.skill_id, d.experience_months
    FROM tmp_desired_user_skills d
    ON CONFLICT (user_id, skill_id)
    DO UPDATE SET experience_months = EXCLUDED.experience_months;

    p_success := TRUE;

EXCEPTION
    WHEN OTHERS THEN
        p_success := FALSE;
END;
$$;
