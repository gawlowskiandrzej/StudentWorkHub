CREATE OR REPLACE FUNCTION get_user_password(p_email TEXT)
RETURNS TABLE(user_id BIGINT, password TEXT)
LANGUAGE sql
AS $$
    SELECT u.id, u.password
    FROM public.users u
    WHERE u.email = p_email
    LIMIT 1;
$$;

CREATE OR REPLACE FUNCTION public.check_user_token(p_token TEXT)
RETURNS BIGINT
LANGUAGE plpgsql
AS $$
DECLARE
    v_user_id BIGINT;
BEGIN
    SELECT u.id
    INTO v_user_id
    FROM public.users u
    WHERE u.remember_token = p_token;

    RETURN v_user_id;
$$;

CREATE OR REPLACE FUNCTION public.get_weights_json(p_user_id BIGINT)
RETURNS JSONB
LANGUAGE plpgsql
AS $$
DECLARE
    result JSONB;
BEGIN
    SELECT to_jsonb(w) - 'user_id'
    INTO result
    FROM public.weights w
    WHERE w.user_id = p_user_id;

    RETURN result;
END;
$$;
