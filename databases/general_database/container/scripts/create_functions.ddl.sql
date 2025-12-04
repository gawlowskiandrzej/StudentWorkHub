CREATE OR REPLACE FUNCTION get_user_password(p_email TEXT)
RETURNS TABLE(user_id BIGINT, password TEXT)
LANGUAGE sql
AS $$
    SELECT u.id, u.password
    FROM public.users u
    WHERE u.email = p_email
    LIMIT 1;
$$;
