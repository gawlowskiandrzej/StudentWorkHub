-- =========================================================
-- DROP (with CASCADE)
-- =========================================================

DROP TABLE IF EXISTS public.work_types_junction CASCADE;
DROP TABLE IF EXISTS public.user_skills CASCADE;

DROP TABLE IF EXISTS public.weights CASCADE;
DROP TABLE IF EXISTS public.search_histories CASCADE;
DROP TABLE IF EXISTS public.preferences CASCADE;

DROP TABLE IF EXISTS public.skills CASCADE;
DROP TABLE IF EXISTS public.work_types CASCADE;
DROP TABLE IF EXISTS public.cities CASCADE;
DROP TABLE IF EXISTS public.job_statuses CASCADE;

DROP TABLE IF EXISTS public.users CASCADE;

DROP TABLE IF EXISTS public.role_permissions_roles_junction CASCADE;
DROP TABLE IF EXISTS public.role_permissions CASCADE;
DROP TABLE IF EXISTS public.roles CASCADE;

DROP TABLE IF EXISTS public.first_names CASCADE;
DROP TABLE IF EXISTS public.second_names CASCADE;
DROP TABLE IF EXISTS public.last_names CASCADE;

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

    employment_schedule_ids SMALLINT[] NOT NULL DEFAULT '{}'::SMALLINT[],
    employment_type_ids     SMALLINT[] NOT NULL DEFAULT '{}'::SMALLINT[]
);

-- =========================================================
-- Preferences
-- =========================================================

CREATE TABLE public.work_types (
    id SMALLINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    work_type VARCHAR(50) NOT NULL,

    CONSTRAINT uq_work_types_work_type
        UNIQUE (work_type)
);

CREATE TABLE public.cities (
    id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    city VARCHAR(256) NOT NULL,

    CONSTRAINT uq_cities_city
        UNIQUE (city)
);

CREATE TABLE public.job_statuses (
    id SMALLINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    status_name VARCHAR(15) NOT NULL,

    CONSTRAINT uq_job_statuses_status_name
        UNIQUE (status_name)
);

CREATE TABLE public.preferences (
    user_id BIGINT PRIMARY KEY,
    leading_category_id SMALLINT NULL,

    salary_from NUMERIC(8,2) NULL,
    salary_to   NUMERIC(8,2) NULL,

    employment_type_ids SMALLINT[] NOT NULL DEFAULT '{}'::SMALLINT[],
    language_ids        SMALLINT[] NOT NULL DEFAULT '{}'::SMALLINT[],
    language_level_ids  SMALLINT[] NOT NULL DEFAULT '{}'::SMALLINT[],

    job_status_id SMALLINT NULL,
    city_id BIGINT NULL,

    CONSTRAINT fk_preferences_cities
        FOREIGN KEY (city_id)
            REFERENCES public.cities(id)
            ON DELETE RESTRICT,

    CONSTRAINT fk_preferences_job_statuses
        FOREIGN KEY (job_status_id)
            REFERENCES public.job_statuses(id)
            ON DELETE RESTRICT,

    CONSTRAINT fk_preferences_users
        FOREIGN KEY (user_id)
            REFERENCES public.users(id)
            ON DELETE CASCADE
);

CREATE TABLE public.work_types_junction (
    work_type_id SMALLINT NOT NULL,
    user_id BIGINT NOT NULL,

    CONSTRAINT pk_work_types_junction
        PRIMARY KEY (work_type_id, user_id),

    CONSTRAINT fk_work_types_jun_work_types
        FOREIGN KEY (work_type_id)
            REFERENCES public.work_types(id)
            ON DELETE CASCADE,

    CONSTRAINT fk_work_types_jun_preferences
        FOREIGN KEY (user_id)
            REFERENCES public.preferences(user_id)
            ON DELETE CASCADE
);

CREATE TABLE public.skills (
    id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    skill_name VARCHAR(256) NOT NULL,
    CONSTRAINT uq_skills_skill_name UNIQUE (skill_name)
);

CREATE TABLE public.user_skills (
    user_id BIGINT NOT NULL,
    skill_id BIGINT NOT NULL,
    experience_months SMALLINT NOT NULL CHECK (experience_months >= 0),
    entry_date TIMESTAMPTZ NOT NULL DEFAULT NOW(),

    CONSTRAINT pk_user_skills PRIMARY KEY (user_id, skill_id),

    CONSTRAINT fk_user_skills_users
        FOREIGN KEY (user_id)
            REFERENCES public.users(id)
            ON DELETE CASCADE,

    CONSTRAINT fk_user_skills_skills
        FOREIGN KEY (skill_id)
            REFERENCES public.skills(id)
            ON DELETE RESTRICT
);

-- =========================================================
-- Ranking algorithm weights
-- =========================================================

CREATE TABLE public.weights (
    user_id BIGINT PRIMARY KEY,
    order_by_option     TEXT[]               DEFAULT ARRAY['']::TEXT[],
    mean_value_ids      TEXT[]               DEFAULT ARRAY['']::TEXT[],
    vector              REAL[]               DEFAULT ARRAY[0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5]::REAL[],
    mean_dist           REAL[]               DEFAULT ARRAY[0.5]::REAL[],
    means_value_sum     REAL[]               DEFAULT ARRAY[0.0]::REAL[],
    means_value_ssum    DOUBLE PRECISION[]   DEFAULT ARRAY[0.0]::DOUBLE PRECISION[],
    means_value_count   INTEGER[]            DEFAULT ARRAY[0]::INTEGER[],
    means_weight_sum    REAL[]               DEFAULT ARRAY[0.0]::REAL[],
    means_weight_ssum   DOUBLE PRECISION[]   DEFAULT ARRAY[0.0]::DOUBLE PRECISION[],
    means_weight_count  INTEGER[]            DEFAULT ARRAY[0]::INTEGER[],

    CONSTRAINT fk_weights_users
        FOREIGN KEY (user_id)
            REFERENCES public.users(id)
            ON DELETE CASCADE
);

-- =========================================================
-- Indexes
-- =========================================================

CREATE INDEX IF NOT EXISTS idx_users_first_name_id ON public.users (first_name_id);
CREATE INDEX IF NOT EXISTS idx_users_second_name_id ON public.users (second_name_id);
CREATE INDEX IF NOT EXISTS idx_users_last_name_id ON public.users (last_name_id);
CREATE INDEX IF NOT EXISTS idx_users_role_id ON public.users (role_id);

CREATE INDEX IF NOT EXISTS idx_search_histories_user_id ON public.search_histories (user_id);

CREATE INDEX IF NOT EXISTS idx_preferences_city_id ON public.preferences (city_id);
CREATE INDEX IF NOT EXISTS idx_preferences_job_status_id ON public.preferences (job_status_id);

CREATE INDEX IF NOT EXISTS idx_work_types_junction_user_id ON public.work_types_junction (user_id);

CREATE INDEX IF NOT EXISTS idx_user_skills_skill_id ON public.user_skills (skill_id);

CREATE INDEX IF NOT EXISTS idx_rprj_role_permission_id ON public.role_permissions_roles_junction (role_permission_id);
