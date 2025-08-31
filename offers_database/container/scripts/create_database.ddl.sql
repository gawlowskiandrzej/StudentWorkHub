-- =========================================================
-- Clean drop in dependency order
-- =========================================================
DROP TABLE IF EXISTS public.benefits_junction;
DROP TABLE IF EXISTS public.employment_types_junction;
DROP TABLE IF EXISTS public.employment_schedules_junction;
DROP TABLE IF EXISTS public.languages_junction;
DROP TABLE IF EXISTS public.education_levels_junction;
DROP TABLE IF EXISTS public.experience_levels_junction;
DROP TABLE IF EXISTS public.skills_junction;

DROP TABLE IF EXISTS public.internal_offers;
DROP TABLE IF EXISTS public.external_offers;

DROP TABLE IF EXISTS public.offers;
DROP TABLE IF EXISTS public.location_details;

DROP TABLE IF EXISTS public.benefits;
DROP TABLE IF EXISTS public.employment_types;
DROP TABLE IF EXISTS public.employment_schedules;
DROP TABLE IF EXISTS public.postal_codes;
DROP TABLE IF EXISTS public.streets;
DROP TABLE IF EXISTS public.cities;
DROP TABLE IF EXISTS public.languages;
DROP TABLE IF EXISTS public.education_levels;
DROP TABLE IF EXISTS public.experience_levels;
DROP TABLE IF EXISTS public.skills;
DROP TABLE IF EXISTS public.salary_periods;
DROP TABLE IF EXISTS public.currencies;
DROP TABLE IF EXISTS public.companies;
DROP TABLE IF EXISTS public.sources;

-- =========================================================
-- Core reference tables
-- =========================================================
CREATE TABLE public.sources (
    id SMALLINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    base_url VARCHAR(256) NOT NULL,
    CONSTRAINT uq_sources_name UNIQUE (name),
    CONSTRAINT uq_sources_base_url UNIQUE (base_url)
);

CREATE TABLE public.companies (
    id INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    logo_url TEXT,
    CONSTRAINT uq_companies_name UNIQUE (name),
    -- fixed: unique on logo_url (was mistakenly on name)
    CONSTRAINT uq_companies_logo_url UNIQUE (logo_url)
);

-- =========================================================
-- Lookup tables
-- =========================================================
CREATE TABLE public.currencies (
    id SMALLINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    currency VARCHAR(3) NOT NULL,
    CONSTRAINT uq_currencies_currency UNIQUE (currency)
);

CREATE TABLE public.salary_periods (
    id SMALLINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    period VARCHAR(30) NOT NULL,
    CONSTRAINT uq_salary_periods_period UNIQUE (period)
);

CREATE TABLE public.skills (
    id INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    skill VARCHAR(50) NOT NULL,
    CONSTRAINT uq_skills_skill UNIQUE (skill)
);

CREATE TABLE public.experience_levels (
    id SMALLINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    level VARCHAR(50) NOT NULL,
    CONSTRAINT uq_experience_levels_level UNIQUE (level)
);

CREATE TABLE public.education_levels (
    id SMALLINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    level VARCHAR(50) NOT NULL,
    CONSTRAINT uq_education_levels_level UNIQUE (level)
);

CREATE TABLE public.languages (
    id SMALLINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    language VARCHAR(50) NOT NULL,
    CONSTRAINT uq_languages_language UNIQUE (language)
);

CREATE TABLE public.cities (
    id INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    city VARCHAR(100) NOT NULL,
    CONSTRAINT uq_cities_city UNIQUE (city)
);

CREATE TABLE public.streets (
    id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    street VARCHAR(150) NOT NULL,
    CONSTRAINT uq_streets_street UNIQUE (street)
);

CREATE TABLE public.postal_codes (
    id INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    postal_code VARCHAR(6) NOT NULL,
    CONSTRAINT uq_postal_codes_postal_code UNIQUE (postal_code)
);

CREATE TABLE public.employment_schedules (
    id SMALLINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    schedule VARCHAR(30) NOT NULL,
    CONSTRAINT uq_employment_schedules_schedule UNIQUE (schedule)
);

CREATE TABLE public.employment_types (
    id SMALLINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    type VARCHAR(30) NOT NULL,
    CONSTRAINT uq_employment_types_type UNIQUE (type)
);

CREATE TABLE public.benefits (
    id SMALLINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    benefit VARCHAR(50) NOT NULL,
    CONSTRAINT uq_benefits_benefit UNIQUE (benefit)
);

-- =========================================================
-- Location details
-- =========================================================
CREATE TABLE public.location_details (
    id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    building_number VARCHAR(10),
    latitude DOUBLE PRECISION,
    longitude DOUBLE PRECISION,
    city_id INTEGER,
    street_id BIGINT,
    postal_code_id INTEGER,
    CONSTRAINT fk_location_city
        FOREIGN KEY (city_id) REFERENCES public.cities(id) ON DELETE SET NULL,
    CONSTRAINT fk_location_street
        FOREIGN KEY (street_id) REFERENCES public.streets(id) ON DELETE SET NULL,
    CONSTRAINT fk_location_postal
        FOREIGN KEY (postal_code_id) REFERENCES public.postal_codes(id) ON DELETE SET NULL
);

-- =========================================================
-- Offers (central entity) - cleaned to use ONLY junctions for M:N
-- =========================================================
CREATE TABLE public.offers (
    id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,

    job_title VARCHAR(100) NOT NULL,
    description TEXT,

    salary_from NUMERIC(8,2),
    salary_to NUMERIC(8,2),
    is_gross BOOLEAN,

    is_remote BOOLEAN,
    is_hybrid BOOLEAN,

    experience_years SMALLINT,
    published TIMESTAMPTZ NOT NULL,
    expires TIMESTAMPTZ,
    is_urgent BOOLEAN NOT NULL,
    is_for_ukrainians BOOLEAN NOT NULL,

    source_id SMALLINT NOT NULL,
    company_id INTEGER NOT NULL,
    location_detail_id BIGINT,

    currency_id SMALLINT,
    salary_period_id SMALLINT,

    CONSTRAINT fk_offers_source
        FOREIGN KEY (source_id) REFERENCES public.sources(id),
    CONSTRAINT fk_offers_company
        FOREIGN KEY (company_id) REFERENCES public.companies(id),
    CONSTRAINT fk_offers_location
        FOREIGN KEY (location_detail_id) REFERENCES public.location_details(id),

    CONSTRAINT fk_offers_currency
        FOREIGN KEY (currency_id) REFERENCES public.currencies(id),
    CONSTRAINT fk_offers_salary_period
        FOREIGN KEY (salary_period_id) REFERENCES public.salary_periods(id)
);

-- =========================================================
-- Subtypes of offers
-- =========================================================
CREATE TABLE public.external_offers (
    offer_id BIGINT PRIMARY KEY,
    query_string TEXT NOT NULL,
    offer_lifespan_expiration TIMESTAMPTZ NOT NULL,
    CONSTRAINT fk_external_offers_offer
        FOREIGN KEY (offer_id) REFERENCES public.offers(id) ON DELETE CASCADE
);

CREATE TABLE public.internal_offers (
    offer_id BIGINT PRIMARY KEY,
    CONSTRAINT fk_internal_offers_offer
        FOREIGN KEY (offer_id) REFERENCES public.offers(id) ON DELETE CASCADE
);

-- =========================================================
-- Junction tables (M:N) - authoritative links to lookups
-- =========================================================
CREATE TABLE public.skills_junction (
    offer_id BIGINT NOT NULL,
    skill_id INTEGER NOT NULL,
    PRIMARY KEY (offer_id, skill_id),
    CONSTRAINT fk_skills_jun_skill
        FOREIGN KEY (skill_id) REFERENCES public.skills(id) ON DELETE CASCADE,
    CONSTRAINT fk_skills_jun_offer
        FOREIGN KEY (offer_id) REFERENCES public.offers(id) ON DELETE CASCADE
);

CREATE TABLE public.experience_levels_junction (
    offer_id BIGINT NOT NULL,
    experience_level_id SMALLINT NOT NULL,
    PRIMARY KEY (offer_id, experience_level_id),
    CONSTRAINT fk_experience_levels_jun_experience_level
        FOREIGN KEY (experience_level_id) REFERENCES public.experience_levels(id) ON DELETE CASCADE,
    CONSTRAINT fk_experience_levels_jun_offer
        FOREIGN KEY (offer_id) REFERENCES public.offers(id) ON DELETE CASCADE
);

CREATE TABLE public.education_levels_junction (
    offer_id BIGINT NOT NULL,
    education_level_id SMALLINT NOT NULL,
    PRIMARY KEY (offer_id, education_level_id),
    CONSTRAINT fk_education_level_jun_education_level
        FOREIGN KEY (education_level_id) REFERENCES public.education_levels(id) ON DELETE CASCADE,
    CONSTRAINT fk_education_levels_jun_offer
        FOREIGN KEY (offer_id) REFERENCES public.offers(id) ON DELETE CASCADE
);

CREATE TABLE public.languages_junction (
    offer_id BIGINT NOT NULL,
    language_id SMALLINT NOT NULL,
    PRIMARY KEY (offer_id, language_id),
    CONSTRAINT fk_languages_jun_language
        FOREIGN KEY (language_id) REFERENCES public.languages(id) ON DELETE CASCADE,
    CONSTRAINT fk_languages_jun_offer
        FOREIGN KEY (offer_id) REFERENCES public.offers(id) ON DELETE CASCADE
);

CREATE TABLE public.employment_schedules_junction (
    offer_id BIGINT NOT NULL,
    employment_schedule_id SMALLINT NOT NULL,
    PRIMARY KEY (offer_id, employment_schedule_id),
    CONSTRAINT fk_employment_schedules_jun_employment_schedule
        FOREIGN KEY (employment_schedule_id) REFERENCES public.employment_schedules(id) ON DELETE CASCADE,
    CONSTRAINT fk_employment_schedules_jun_offer
        FOREIGN KEY (offer_id) REFERENCES public.offers(id) ON DELETE CASCADE
);

CREATE TABLE public.employment_types_junction (
    offer_id BIGINT NOT NULL,
    employment_type_id SMALLINT NOT NULL,
    PRIMARY KEY (offer_id, employment_type_id),
    CONSTRAINT fk_employment_types_jun_employment_type
        FOREIGN KEY (employment_type_id) REFERENCES public.employment_types(id) ON DELETE CASCADE,
    CONSTRAINT fk_employment_types_jun_offer
        FOREIGN KEY (offer_id) REFERENCES public.offers(id) ON DELETE CASCADE
);

CREATE TABLE public.benefits_junction (
    offer_id BIGINT NOT NULL,
    benefit_id SMALLINT NOT NULL,  -- renamed from benefits_id for consistency
    PRIMARY KEY (offer_id, benefit_id),
    CONSTRAINT fk_benefits_jun_benefit
        FOREIGN KEY (benefit_id) REFERENCES public.benefits(id) ON DELETE CASCADE,
    CONSTRAINT fk_benefits_jun_offer
        FOREIGN KEY (offer_id) REFERENCES public.offers(id) ON DELETE CASCADE
);

-- =========================================================
-- Foundational indexes only: PK/UNIQUE (implicit) + FK helpers
-- =========================================================

-- location_details -> lookups
CREATE INDEX IF NOT EXISTS idx_location_details_city_id
    ON public.location_details (city_id);
CREATE INDEX IF NOT EXISTS idx_location_details_street_id
    ON public.location_details (street_id);
CREATE INDEX IF NOT EXISTS idx_location_details_postal_code_id
    ON public.location_details (postal_code_id);

-- offers -> sources, companies, location_details
CREATE INDEX IF NOT EXISTS idx_offers_source_id
    ON public.offers (source_id);
CREATE INDEX IF NOT EXISTS idx_offers_company_id
    ON public.offers (company_id);
CREATE INDEX IF NOT EXISTS idx_offers_location_detail_id
    ON public.offers (location_detail_id);

-- offers -> money/lookups
CREATE INDEX IF NOT EXISTS idx_offers_currency_id
    ON public.offers (currency_id);
CREATE INDEX IF NOT EXISTS idx_offers_salary_period_id
    ON public.offers (salary_period_id);

CREATE INDEX IF NOT EXISTS idx_skills_junction_skill_offer
    ON public.skills_junction (skill_id, offer_id);

CREATE INDEX IF NOT EXISTS idx_experience_levels_junction_level_offer
    ON public.experience_levels_junction (experience_level_id, offer_id);

CREATE INDEX IF NOT EXISTS idx_education_levels_junction_level_offer
    ON public.education_levels_junction (education_level_id, offer_id);

CREATE INDEX IF NOT EXISTS idx_languages_junction_language_offer
    ON public.languages_junction (language_id, offer_id);

CREATE INDEX IF NOT EXISTS idx_employment_schedules_junction_schedule_offer
    ON public.employment_schedules_junction (employment_schedule_id, offer_id);

CREATE INDEX IF NOT EXISTS idx_employment_types_junction_type_offer
    ON public.employment_types_junction (employment_type_id, offer_id);

CREATE INDEX IF NOT EXISTS idx_benefits_junction_benefit_offer
    ON public.benefits_junction (benefit_id, offer_id);
