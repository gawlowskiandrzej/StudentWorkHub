DROP TABLE IF EXISTS public.search_histories_employment_types_junction CASCADE;
DROP TABLE IF EXISTS public.search_histories_employment_schedules_junction CASCADE;
DROP TABLE IF EXISTS public.history_salaries CASCADE;
DROP TABLE IF EXISTS public.employment_types CASCADE;
DROP TABLE IF EXISTS public.employment_schedules CASCADE;
DROP TABLE IF EXISTS public.weights CASCADE;
DROP TABLE IF EXISTS public.search_histories CASCADE;
DROP TABLE IF EXISTS public.users CASCADE;

-- =========================================================
-- Users
-- =========================================================

CREATE TABLE public.users (
    id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    email VARCHAR(255) NOT NULL,
    password TEXT NOT NULL,
    remember_token TEXT NULL,
    second_name VARCHAR(100) NULL,
    last_name VARCHAR(100) NOT NULL,
    phone VARCHAR(16) NULL,

    CHECK (email ~* '^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$'),
    CHECK (phone ~ '^\+[0-9]{7,15}$'),

    CONSTRAINT uq_users_email 
        UNIQUE (email)
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
            ON DELETE CASCADE
);

CREATE TABLE public.history_salaries (
    id INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    salary_from NUMERIC(8,2) NULL DEFAULT 0.0,
    salary_to NUMERIC(8,2) NULL DEFAULT 0.0,
    salary_period_id SMALLINT NULL,
    salary_currency_id SMALLINT NULL,
    salary_type_id SMALLINT NULL,

    search_history_id INTEGER NOT NULL,
    CONSTRAINT fk_search_histories_history_salaries
        FOREIGN KEY (search_history_id) 
            REFERENCES public.search_histories(id) 
            ON DELETE CASCADE
);

CREATE TABLE public.employment_schedules (
    id INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    employment_schedule_id SMALLINT NOT NULL,
    
    CONSTRAINT uq_employment_schedules_employment_schedule_id 
        UNIQUE (employment_schedule_id)
);

CREATE TABLE public.search_histories_employment_schedules_junction (
    search_history_id INTEGER NOT NULL,
    employment_schedule_id INTEGER NOT NULL,

    CONSTRAINT pk_search_histories_employment_schedules_junction
        PRIMARY KEY (search_history_id, employment_schedule_id),

    CONSTRAINT fk_search_histories_search_histories_employment_schedules_junction_search_history_id
        FOREIGN KEY (search_history_id)
            REFERENCES public.search_histories(id)
            ON DELETE CASCADE,

    CONSTRAINT fk_employment_schedules_search_histories_employment_schedules_junction_employment_schedule_id
        FOREIGN KEY (employment_schedule_id)
            REFERENCES public.employment_schedules(id)
            ON DELETE CASCADE
);

CREATE TABLE public.employment_types (
    id INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    employment_type_id SMALLINT NOT NULL,
    
    CONSTRAINT uq_employment_types_employment_type_id
        UNIQUE (employment_type_id)
);

CREATE TABLE public.search_histories_employment_types_junction (
    search_history_id INTEGER NOT NULL,
    employment_type_id INTEGER NOT NULL,

    CONSTRAINT pk_search_histories_employment_types_junction
        PRIMARY KEY (search_history_id, employment_type_id),

    CONSTRAINT fk_search_histories_search_histories_employment_types_junction_search_history_id
        FOREIGN KEY (search_history_id)
            REFERENCES public.search_histories(id)
            ON DELETE CASCADE,

    CONSTRAINT fk_employment_types_search_histories_employment_types_junction_employment_type_id
        FOREIGN KEY (employment_type_id)
            REFERENCES public.employment_types(id)
            ON DELETE CASCADE
);

-- =========================================================
-- Preferences
-- =========================================================

-- =========================================================
-- Ranking algorithm weights
-- =========================================================

CREATE TABLE public.weights (
    id INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    order_by_option TEXT[],
    mean_value_ids TEXT[],
    vector REAL[],
    mean_dist REAL[],
    means_value_sum REAL[],
    means_value_ssum DOUBLE PRECISION[],
    means_value_count INTEGER[],
    means_weight_sum REAL[],
    means_weight_ssum DOUBLE PRECISION[],
    means_weight_count INTEGER[],

    user_id BIGINT NOT NULL,
    CONSTRAINT fk_users_weights
        FOREIGN KEY (user_id)
            REFERENCES public.users(id)
            ON DELETE CASCADE
);
