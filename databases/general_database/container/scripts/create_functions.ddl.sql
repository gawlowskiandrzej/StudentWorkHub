CREATE TYPE public.preference_dto AS (
    leading_category_id     SMALLINT,
    salary_from             NUMERIC(8,2),
    salary_to               NUMERIC(8,2),
    employment_type_ids     SMALLINT[],
    job_status_id           SMALLINT,
    language_ids            SMALLINT[],
    language_level_ids      SMALLINT[],
    city_id                 BIGINT,
    work_types              TEXT[],

    skill_names             TEXT[],
    skill_experience_months SMALLINT[],
    skill_entry_dates       TIMESTAMPTZ[]
);

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
              - 'id'
              - 'password'
              - 'remember_token'
              - 'first_name_id'
              - 'second_name_id'
              - 'last_name_id'
              - 'role_id'
        )
        ||
        jsonb_build_object(
            'first_name',  fn.first_name,
            'second_name', sn.second_name,
            'last_name',   ln.last_name,
            'role',        r.role_name
        )
    INTO result
    FROM public.users u
    JOIN public.first_names fn ON fn.id = u.first_name_id
    LEFT JOIN public.second_names sn ON sn.id = u.second_name_id
    JOIN public.last_names ln ON ln.id = u.last_name_id
    JOIN public.roles r ON r.id = u.role_id
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

CREATE OR REPLACE FUNCTION public.get_search_history_json(
    p_user_id BIGINT,
    p_limit   INTEGER
)
RETURNS jsonb
LANGUAGE sql
STABLE
AS $$
    SELECT COALESCE(
        jsonb_agg(to_jsonb(sub) - 'search_date' - 'id'),
        '[]'::jsonb
    )
    FROM (
        SELECT *
        FROM public.search_histories
        WHERE user_id = p_user_id
        ORDER BY search_date DESC, id DESC
        LIMIT GREATEST(COALESCE(p_limit, 0), 0)
    ) AS sub;
$$;

CREATE OR REPLACE FUNCTION public.get_user_preferences(p_user_id BIGINT)
RETURNS public.preference_dto
LANGUAGE sql
STABLE
AS $$
    SELECT ROW(
        p.leading_category_id,
        p.salary_from,
        p.salary_to,
        p.employment_type_ids,
        p.job_status_id,
        p.language_ids,
        p.language_level_ids,
        p.city_id,

        COALESCE(wt.work_types, '{}'::text[]),

        COALESCE(sk.skill_names, '{}'::text[]),
        COALESCE(sk.skill_experience_months, '{}'::smallint[]),
        COALESCE(sk.skill_entry_dates, '{}'::timestamptz[])
    )::public.preference_dto
    FROM public.preferences p

    LEFT JOIN LATERAL (
        SELECT array_agg(w.work_type ORDER BY w.work_type) AS work_types
        FROM public.work_types_junction j
        JOIN public.work_types w ON w.id = j.work_type_id
        WHERE j.user_id = p.user_id
    ) wt ON TRUE

    LEFT JOIN LATERAL (
        SELECT
            array_agg(s.skill_name ORDER BY s.skill_name)              AS skill_names,
            array_agg(us.experience_months ORDER BY s.skill_name)      AS skill_experience_months,
            array_agg(us.entry_date ORDER BY s.skill_name)             AS skill_entry_dates
        FROM public.user_skills us
        JOIN public.skills s ON s.id = us.skill_id
        WHERE us.user_id = p.user_id
    ) sk ON TRUE

    WHERE p.user_id = p_user_id;
$$;
