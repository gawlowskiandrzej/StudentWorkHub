DROP TABLE IF EXISTS public.weights;
DROP TABLE IF EXISTS public.search_histories;
DROP TABLE IF EXISTS public.users;

DROP TABLE IF EXISTS public.role_permissions_roles_junction;
DROP TABLE IF EXISTS public.role_permissions;
DROP TABLE IF EXISTS public.roles;

DROP TABLE IF EXISTS public.first_names;
DROP TABLE IF EXISTS public.second_names;
DROP TABLE IF EXISTS public.last_names;


-- =========================================================
-- Users
-- =========================================================

CREATE TABLE public.first_names (
    id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    first_name VARCHAR(100) NOT NULL,

    CONSTRAINT uq_first_names_first_name
        UNIQUE (first_name)
);

CREATE TABLE public.second_names (
    id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    second_name VARCHAR(100) NOT NULL,

    CONSTRAINT uq_second_names_second_name
        UNIQUE (second_name)
);

CREATE TABLE public.last_names (
    id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    last_name VARCHAR(100) NOT NULL,

    CONSTRAINT uq_last_names_last_name
        UNIQUE (last_name)
);

CREATE TABLE public.roles (
    id SMALLINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    role_name VARCHAR(100) NOT NULL,

    CONSTRAINT uq_roles_role_name
        UNIQUE (role_name)
);

CREATE TABLE public.role_permissions (
    id SMALLINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY, 
    permission_name VARCHAR(100) NOT NULL,

    CONSTRAINT uq_role_permissions_permission_name
        UNIQUE (permission_name)
);

CREATE TABLE public.role_permissions_roles_junction (
    role_id SMALLINT NOT NULL,
    role_permission_id SMALLINT NOT NULL,

    CONSTRAINT pk_role_permissions_roles_junction
        PRIMARY KEY (role_id, role_permission_id),

    CONSTRAINT fk_rprj_roles
        FOREIGN KEY (role_id)
        REFERENCES public.roles (id)
        ON DELETE CASCADE,

    CONSTRAINT fk_rprj_role_permissions
        FOREIGN KEY (role_permission_id)
        REFERENCES public.role_permissions (id)
        ON DELETE CASCADE
);

CREATE TABLE public.users (
    id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    email VARCHAR(255) NULL,
    password TEXT NULL,
    remember_token TEXT NULL,
    phone VARCHAR(16) NULL,

    first_name_id BIGINT NOT NULL,
    second_name_id BIGINT NULL,
    last_name_id BIGINT NOT NULL,
    role_id SMALLINT NOT NULL DEFAULT 1,

    CHECK (email ~* '^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$'),
    CHECK (phone ~ '^\+[0-9]{7,15}$'),

    CONSTRAINT uq_users_email
        UNIQUE (email),

    CONSTRAINT uq_users_remember_token
        UNIQUE (remember_token),

    CONSTRAINT uq_users_phone
        UNIQUE (phone),

    CONSTRAINT fk_users_first_names
        FOREIGN KEY (first_name_id)
        REFERENCES public.first_names (id)
        ON DELETE RESTRICT,

    CONSTRAINT fk_users_second_names
        FOREIGN KEY (second_name_id)
        REFERENCES public.second_names (id)
        ON DELETE RESTRICT,

    CONSTRAINT fk_users_last_names
        FOREIGN KEY (last_name_id)
        REFERENCES public.last_names (id)
        ON DELETE RESTRICT,

    CONSTRAINT fk_users_roles
        FOREIGN KEY (role_id)
        REFERENCES public.roles (id)
        ON DELETE RESTRICT
);

-- =========================================================
-- Search history
-- =========================================================

CREATE TABLE public.search_histories (
    id INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    keywords VARCHAR(1024) NULL,
    distance INTEGER NOT NULL DEFAULT 0,
    is_remote BOOLEAN NULL,
    is_hybrid BOOLEAN NULL,

    leading_category_id SMALLINT NULL,
    city_id INTEGER NULL,

    search_date TIMESTAMPTZ NOT NULL DEFAULT NOW(),

    user_id BIGINT NOT NULL,
    CONSTRAINT fk_users_search_histories
        FOREIGN KEY (user_id)
            REFERENCES public.users(id)
            ON DELETE CASCADE,

    salary_from NUMERIC(8,2) NOT NULL DEFAULT 0.0,
    salary_to NUMERIC(8,2) NOT NULL DEFAULT 0.0,
    salary_period_id SMALLINT NULL,
    salary_currency_id SMALLINT NULL,
    salary_type_id SMALLINT NULL,

    employment_schedule_ids SMALLINT[] NOT NULL DEFAULT '{}',
    employment_type_ids SMALLINT[] NOT NULL DEFAULT '{}'
);

-- =========================================================
-- Preferences
-- =========================================================

-- =========================================================
-- Ranking algorithm weights
-- =========================================================

CREATE TABLE public.weights (
    user_id BIGINT PRIMARY KEY,
    order_by_option TEXT[] DEFAULT ARRAY[''],
    mean_value_ids TEXT[] DEFAULT ARRAY[''],
    vector REAL[] DEFAULT ARRAY[0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5],
    mean_dist REAL[] DEFAULT ARRAY[0.5],
    means_value_sum REAL[] DEFAULT ARRAY[0.0],
    means_value_ssum DOUBLE PRECISION[] DEFAULT ARRAY[0.0],
    means_value_count INTEGER[] DEFAULT ARRAY[0],
    means_weight_sum REAL[] DEFAULT ARRAY[0.0],
    means_weight_ssum DOUBLE PRECISION[] DEFAULT ARRAY[0.0],
    means_weight_count INTEGER[] DEFAULT ARRAY[0],

    CONSTRAINT fk_weights_users
        FOREIGN KEY (user_id)
            REFERENCES public.users(id)
            ON DELETE CASCADE
);
