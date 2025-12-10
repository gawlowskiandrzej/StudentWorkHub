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
END;
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

CREATE OR REPLACE FUNCTION public.get_user_json(p_user_id BIGINT)
RETURNS JSONB
LANGUAGE plpgsql
AS $$
DECLARE
    result JSONB;
BEGIN
    SELECT
        (
            to_jsonb(u)
              - 'password'
              - 'remember_token'
              - 'first_name_id'
              - 'second_name_id'
              - 'last_name_id'
        )
        ||
        jsonb_build_object(
            'first_name',  fn.first_name,
            'second_name', sn.second_name,
            'last_name',   ln.last_name
        )
    INTO result
    FROM public.users u
    JOIN public.first_names  fn ON fn.id = u.first_name_id
    LEFT JOIN public.second_names sn ON sn.id = u.second_name_id
    JOIN public.last_names  ln ON ln.id = u.last_name_id
    WHERE u.id = p_user_id;

    RETURN result;
END;
$$;

CREATE OR REPLACE FUNCTION public.user_has_permission(
    p_user_id BIGINT,
    p_permission_name VARCHAR
)
RETURNS BOOLEAN
LANGUAGE plpgsql
AS
$$
DECLARE
    v_has_permission BOOLEAN;
BEGIN
    SELECT EXISTS (
        SELECT 1
        FROM public.users u
        JOIN public.roles r
            ON r.id = u.role_id
        JOIN public.role_permissions_roles_junction rprj
            ON rprj.role_id = r.id
        JOIN public.role_permissions rp
            ON rp.id = rprj.role_permission_id
        WHERE u.id = p_user_id
          AND rp.permission_name = p_permission_name
    )
    INTO v_has_permission;

    RETURN COALESCE(v_has_permission, FALSE);
EXCEPTION
    WHEN OTHERS THEN
        RETURN FALSE;
END;
$$;
