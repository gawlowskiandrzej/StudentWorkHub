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
            o_success := false;
            o_message := 'Foreign key violation while inserting user.';
            o_user_id := NULL;
            RETURN;

        WHEN others THEN
            o_success := false;
            o_message := 'Unexpected error while inserting user: ' || SQLERRM;
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

CREATE OR REPLACE FUNCTION public.set_weights_order_by_option(
    p_user_id BIGINT,
    p_order_by_option TEXT[]
) RETURNS BOOLEAN
LANGUAGE plpgsql
AS $$
DECLARE
    v_row_count INTEGER;
BEGIN
    UPDATE public.weights
    SET order_by_option = p_order_by_option
    WHERE user_id = p_user_id;

    GET DIAGNOSTICS v_row_count = ROW_COUNT;

    RETURN v_row_count = 1;
END;
$$;

CREATE OR REPLACE FUNCTION public.set_weights_mean_value_ids(
    p_user_id BIGINT,
    p_mean_value_ids TEXT[]
) RETURNS BOOLEAN
LANGUAGE plpgsql
AS $$
DECLARE
    v_row_count INTEGER;
BEGIN
    UPDATE public.weights
    SET mean_value_ids = p_mean_value_ids
    WHERE user_id = p_user_id;

    GET DIAGNOSTICS v_row_count = ROW_COUNT;

    RETURN v_row_count = 1;
END;
$$;

CREATE OR REPLACE FUNCTION public.set_weights_vector(
    p_user_id BIGINT,
    p_vector REAL[]
) RETURNS BOOLEAN
LANGUAGE plpgsql
AS $$
DECLARE
    v_row_count INTEGER;
BEGIN
    UPDATE public.weights
    SET vector = p_vector
    WHERE user_id = p_user_id;

    GET DIAGNOSTICS v_row_count = ROW_COUNT;

    RETURN v_row_count = 1;
END;
$$;

CREATE OR REPLACE FUNCTION public.set_weights_mean_dist(
    p_user_id BIGINT,
    p_mean_dist REAL[]
) RETURNS BOOLEAN
LANGUAGE plpgsql
AS $$
DECLARE
    v_row_count INTEGER;
BEGIN
    UPDATE public.weights
    SET mean_dist = p_mean_dist
    WHERE user_id = p_user_id;

    GET DIAGNOSTICS v_row_count = ROW_COUNT;

    RETURN v_row_count = 1;
END;
$$;

CREATE OR REPLACE FUNCTION public.set_weights_means_value_sum(
    p_user_id BIGINT,
    p_means_value_sum REAL[]
) RETURNS BOOLEAN
LANGUAGE plpgsql
AS $$
DECLARE
    v_row_count INTEGER;
BEGIN
    UPDATE public.weights
    SET means_value_sum = p_means_value_sum
    WHERE user_id = p_user_id;

    GET DIAGNOSTICS v_row_count = ROW_COUNT;

    RETURN v_row_count = 1;
END;
$$;

CREATE OR REPLACE FUNCTION public.set_weights_means_value_ssum(
    p_user_id BIGINT,
    p_means_value_ssum DOUBLE PRECISION[]
) RETURNS BOOLEAN
LANGUAGE plpgsql
AS $$
DECLARE
    v_row_count INTEGER;
BEGIN
    UPDATE public.weights
    SET means_value_ssum = p_means_value_ssum
    WHERE user_id = p_user_id;

    GET DIAGNOSTICS v_row_count = ROW_COUNT;

    RETURN v_row_count = 1;
END;
$$;

CREATE OR REPLACE FUNCTION public.set_weights_means_value_count(
    p_user_id BIGINT,
    p_means_value_count INTEGER[]
) RETURNS BOOLEAN
LANGUAGE plpgsql
AS $$
DECLARE
    v_row_count INTEGER;
BEGIN
    UPDATE public.weights
    SET means_value_count = p_means_value_count
    WHERE user_id = p_user_id;

    GET DIAGNOSTICS v_row_count = ROW_COUNT;

    RETURN v_row_count = 1;
END;
$$;

CREATE OR REPLACE FUNCTION public.set_weights_means_weight_sum(
    p_user_id BIGINT,
    p_means_weight_sum REAL[]
) RETURNS BOOLEAN
LANGUAGE plpgsql
AS $$
DECLARE
    v_row_count INTEGER;
BEGIN
    UPDATE public.weights
    SET means_weight_sum = p_means_weight_sum
    WHERE user_id = p_user_id;

    GET DIAGNOSTICS v_row_count = ROW_COUNT;

    RETURN v_row_count = 1;
END;
$$;

CREATE OR REPLACE FUNCTION public.set_weights_means_weight_ssum(
    p_user_id BIGINT,
    p_means_weight_ssum DOUBLE PRECISION[]
) RETURNS BOOLEAN
LANGUAGE plpgsql
AS $$
DECLARE
    v_row_count INTEGER;
BEGIN
    UPDATE public.weights
    SET means_weight_ssum = p_means_weight_ssum
    WHERE user_id = p_user_id;

    GET DIAGNOSTICS v_row_count = ROW_COUNT;

    RETURN v_row_count = 1;
END;
$$;

CREATE OR REPLACE FUNCTION public.set_weights_means_weight_count(
    p_user_id BIGINT,
    p_means_weight_count INTEGER[]
) RETURNS BOOLEAN
LANGUAGE plpgsql
AS $$
DECLARE
    v_row_count INTEGER;
BEGIN
    UPDATE public.weights
    SET means_weight_count = p_means_weight_count
    WHERE user_id = p_user_id;

    GET DIAGNOSTICS v_row_count = ROW_COUNT;

    RETURN v_row_count = 1;
END;
$$;
