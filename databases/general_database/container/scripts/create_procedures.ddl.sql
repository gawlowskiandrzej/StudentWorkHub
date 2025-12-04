CREATE OR REPLACE PROCEDURE public.standard_add_user(
    IN  p_email       text,
    IN  p_password    text,
    IN  p_first_name  text,
    IN  p_last_name   text,
    IN  p_second_name text DEFAULT NULL,
    IN  p_phone       text DEFAULT NULL,
    OUT o_success     boolean DEFAULT NULL,
    OUT o_message     text    DEFAULT NULL,
    OUT o_user_id     bigint  DEFAULT NULL
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_first_name_id  bigint;
    v_second_name_id bigint;
    v_last_name_id   bigint;
    v_phone_id       bigint;
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

    -- Phone format validation (only if provided)
    IF p_phone IS NOT NULL AND btrim(p_phone) <> '' THEN
        IF p_phone !~ '^\+[0-9]{7,15}$' THEN
            o_success := false;
            o_message := 'Phone format is invalid. Expected +[7-15 digits].';
            o_user_id := NULL;
            RETURN;
        END IF;
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

    -- Insert or get optional second_name
    IF p_second_name IS NOT NULL AND btrim(p_second_name) <> '' THEN
        INSERT INTO public.second_names(second_name)
        VALUES (p_second_name)
        ON CONFLICT (second_name) DO UPDATE
            SET second_name = EXCLUDED.second_name
        RETURNING id INTO v_second_name_id;
    ELSE
        v_second_name_id := NULL;
    END IF;

    -- Insert or get optional phone
    IF p_phone IS NOT NULL AND btrim(p_phone) <> '' THEN
        INSERT INTO public.phones(phone)
        VALUES (p_phone)
        ON CONFLICT (phone) DO UPDATE
            SET phone = EXCLUDED.phone
        RETURNING id INTO v_phone_id;
    ELSE
        v_phone_id := NULL;
    END IF;

    -- Insert user row
    BEGIN
        INSERT INTO public.users(
            email,
            password,
            remember_token,
            first_name_id,
            second_name_id,
            last_name_id,
            phone_id
        )
        VALUES (
            p_email,
            p_password,
            NULL,
            v_first_name_id,
            v_second_name_id,
            v_last_name_id,
            v_phone_id
        )
        RETURNING id INTO o_user_id;

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
