-- =========================================================
-- Test data for Jobs DB (PostgreSQL)
-- =========================================================
-- ---------------------------------------------------------
-- Sources
-- ---------------------------------------------------------
INSERT INTO sources (name, base_url) VALUES
    ('Pracuj.pl', 'https://www.pracuj.pl/praca/'),
    ('OlxPraca', 'https://www.olx.pl/oferta/praca/'),
    ('justjoin.it', 'https://justjoin.it/job-offer/'),
    ('Jooble', 'https://pl.jooble.org/desc/')
ON CONFLICT (name) DO NOTHING;

INSERT INTO companies (name, logo_url) VALUES
    ('Vistula Tech', 'https://img.freepik.com/darmowe-wektory/ptak-kolorowe-logo-wektor-gradientu_343694-1365.jpg?semt=ais_hybrid&w=740&q=80'),
    ('Medicus Clinic', NULL),
    ('Lex & Partners', 'https://i.pinimg.com/736x/ab/3d/e2/ab3de2f5cc08f507f728f39c66e596b8.jpg'),
    ('GreenBuild Engineers', NULL),
    ('Nord Finance', NULL),
    ('EduFuture School', 'https://lh4.googleusercontent.com/proxy/6qm_KP5JvOtCLNxtVxamjzGZqBMmrDlRxEjluxrycU7Xp0oN0-M29blGB-l5ESVonKHjGUIxOUDE7UxRTGj6qBRMIXQ8hfYKQvw98DHQ7ZkDeWRKqunpGA'),
    ('PharmaNova', NULL),
    ('AeroPol', NULL),
    ('AgroTech Labs', NULL),
    ('BlueWave Energy', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ7mfRLevCDPB67iyEezFxzy215M7uCusyOig&s'),
    ('City Hospital Warsaw', NULL),
    ('Ocean Research Institute', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSVkgBNu9NL5Zc6g1xfAXC_ujb07mAnXmK9NQ&s'),
    ('ArtVision Studio', NULL),
    ('FoodTech Polska', 'https://i.fbcd.co/products/resized/resized-750-500/35fa221b9125551fc13446133b8d980337655e15f27b10dfb205db407ab13975.jpg'),
    ('AutoDrive Systems', NULL),
    ('VetCare Center', NULL)
ON CONFLICT (name) DO NOTHING;

-- ---------------------------------------------------------
-- Lookups
-- ---------------------------------------------------------
INSERT INTO currencies (currency) VALUES
    ('PLN'), ('EUR'), ('USD')
ON CONFLICT (currency) DO NOTHING;

INSERT INTO salary_periods (period) VALUES
    ('monthly'), ('yearly'), ('hourly'), ('daily')
ON CONFLICT (period) DO NOTHING;

INSERT INTO experience_levels (level) VALUES
    ('Junior'), ('Intermediate'), ('Senior'), ('Lead')
ON CONFLICT (level) DO NOTHING;

INSERT INTO education_levels (level) VALUES
    ('Bachelor'), ('Master'), ('Doctorate'), ('Postgraduate')
ON CONFLICT (level) DO NOTHING;

INSERT INTO languages (language) VALUES
    ('Polish'), ('English'), ('German'), ('Ukrainian'), ('French')
ON CONFLICT (language) DO NOTHING;

INSERT INTO employment_types (type) VALUES
    ('Contract of mandate'), ('Contract for specific work'), ('B2B'), ('Internship'), ('Managerial contract')
ON CONFLICT (type) DO NOTHING;

INSERT INTO employment_schedules (schedule) VALUES
    ('Full-time'), ('Part-time'), ('Temporary contract'), ('Freelance')
ON CONFLICT (schedule) DO NOTHING;

INSERT INTO benefits (benefit) VALUES
    ('Private healthcare'),
    ('Multisport'),
    ('Remote work'),
    ('Training budget'),
    ('Company car'),
    ('Meal vouchers'),
    ('Life insurance'),
    ('Stock options')
ON CONFLICT (benefit) DO NOTHING;

INSERT INTO skills (skill) VALUES
    ('Python'), ('Django'), ('PostgreSQL'), ('Java'), ('Spring'),
    ('SQL'), ('Excel'), ('IFRS'), ('AutoCAD'), ('Civil Engineering'),
    ('Architecture'), ('Mathematics Didactics'), ('Pharmacology'),
    ('Clinical Trials'), ('MATLAB'), ('CAD'), ('Electrical Engineering'),
    ('Project Management'), ('Scrum'), ('AWS'), ('Azure'), ('Photoshop'),
    ('Illustrator'), ('Statistics'), ('R'), ('SPSS'), ('Auditing'),
    ('Tax Law'), ('Econometrics'), ('Microcontrollers')
ON CONFLICT (skill) DO NOTHING;

-- ---------------------------------------------------------
-- Cities / Streets / Postal Codes
-- ---------------------------------------------------------
INSERT INTO cities (city) VALUES
    ('Warsaw'), ('Krakow'), ('Gdansk'), ('Wroclaw'), ('Poznan'),
    ('Lodz'), ('Lublin'), ('Szczecin'), ('Katowice')
ON CONFLICT (city) DO NOTHING;

INSERT INTO streets (street) VALUES
    ('Marszalkowska'), ('Dluga'), ('Grunwaldzka'), ('Piotrkowska'),
    ('Krupnicza'), ('Polwiejska'), ('Legnicka'), ('Starowiejska')
ON CONFLICT (street) DO NOTHING;

INSERT INTO postal_codes (postal_code) VALUES
    ('00-001'), ('31-002'), ('80-003'), ('50-004'), ('61-005'),
    ('90-006'), ('20-007'), ('70-008'), ('40-009')
ON CONFLICT (postal_code) DO NOTHING;

-- ---------------------------------------------------------
-- Location details
-- ---------------------------------------------------------
INSERT INTO location_details (building_number, latitude, longitude, city_id, street_id, postal_code_id) VALUES
    ('10', 52.2297, 21.0122,
        (SELECT id FROM cities WHERE city='Warsaw'),
        (SELECT id FROM streets WHERE street='Marszalkowska'),
        (SELECT id FROM postal_codes WHERE postal_code='00-001')
    ),
    ('5', NULL, NULL,
        (SELECT id FROM cities WHERE city='Krakow'),
        (SELECT id FROM streets WHERE street='Dluga'),
        (SELECT id FROM postal_codes WHERE postal_code='31-002')
    ),
    ('12', 54.3520, 18.6466,
        (SELECT id FROM cities WHERE city='Gdansk'),
        NULL,
        (SELECT id FROM postal_codes WHERE postal_code='80-003')
    ),
    ('45', 51.1079, 17.0385,
        NULL,
        (SELECT id FROM streets WHERE street='Legnicka'),
        (SELECT id FROM postal_codes WHERE postal_code='50-004')
    ),
    ('3', 52.4064, 16.9252,
        (SELECT id FROM cities WHERE city='Poznan'),
        (SELECT id FROM streets WHERE street='Polwiejska'),
        NULL
    ),
    (NULL, 51.7592, 19.4550,
        (SELECT id FROM cities WHERE city='Lodz'),
        (SELECT id FROM streets WHERE street='Piotrkowska'),
        (SELECT id FROM postal_codes WHERE postal_code='90-006')
    ),
    ('2', 51.2465, 22.5684,
        (SELECT id FROM cities WHERE city='Lublin'),
        (SELECT id FROM streets WHERE street='Krupnicza'),
        (SELECT id FROM postal_codes WHERE postal_code='20-007')
    ),
    ('8', 53.4289, 14.5530,
        NULL,
        (SELECT id FROM streets WHERE street='Starowiejska'),
        NULL
    ),
    ('33', NULL, NULL,
        NULL,
        NULL,
        NULL
    ),
    (NULL, 52.2297, 21.0122, 
        (SELECT id FROM cities WHERE city='Warsaw'),
        NULL,
        NULL
    ),
    (NULL, 50.0647, 19.9450,
        (SELECT id FROM cities WHERE city='Krakow'),
        NULL,
        NULL
    ),
    (NULL, 54.3520, 18.6466,
        (SELECT id FROM cities WHERE city='Gdansk'),
        NULL,
        NULL
    );

-- =========================================================
-- External Offers
-- =========================================================

WITH new_offer AS (
    INSERT INTO offers (
        job_title, description,
        salary_from, salary_to, is_gross,
        is_remote, is_hybrid,
        experience_years, published, expires, is_urgent, is_for_ukrainians,
        source_id, company_id, location_detail_id, currency_id, salary_period_id
    )
    VALUES (
        'Software Engineer (Python)',
        'Develop backend services and data pipelines.',
        15000, 22000, TRUE,
        TRUE, FALSE,
        2, '2025-08-10 10:00+02', '2026-12-31 23:59+01', FALSE, TRUE,
        (SELECT id FROM sources WHERE name='justjoin.it'),
        (SELECT id FROM companies WHERE name='Vistula Tech'),
        (SELECT id FROM location_details ORDER BY id LIMIT 1 OFFSET 0),
        (SELECT id FROM currencies WHERE currency='PLN'),
        (SELECT id FROM salary_periods WHERE period='monthly')
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, 'nearmap-technical-lead-with-react-warszawa-javascript', '2026-12-31 23:59+01' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id)
    SELECT n.id, s.id FROM new_offer n
    JOIN skills s ON s.skill IN ('Python','Django','PostgreSQL','AWS')
),
exj AS (
    INSERT INTO experience_levels_junction (offer_id, experience_level_id)
    SELECT n.id, e.id FROM new_offer n
    JOIN experience_levels e ON e.level IN ('Junior','Intermediate')
),
edj AS (
    INSERT INTO education_levels_junction (offer_id, education_level_id)
    SELECT n.id, el.id FROM new_offer n
    JOIN education_levels el ON el.level IN ('Bachelor')
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id)
    SELECT n.id, l.id FROM new_offer n
    JOIN languages l ON l.language IN ('Polish','English')
),
etj AS (
    INSERT INTO employment_types_junction (offer_id, employment_type_id)
    SELECT n.id, t.id FROM new_offer n
    JOIN employment_types t ON t.type IN ('B2B','Contract of mandate')
),
esj AS (
    INSERT INTO employment_schedules_junction (offer_id, employment_schedule_id)
    SELECT n.id, s.id FROM new_offer n
    JOIN employment_schedules s ON s.schedule IN ('Full-time')
),
bj AS (
    INSERT INTO benefits_junction (offer_id, benefit_id)
    SELECT n.id, b.id FROM new_offer n
    JOIN benefits b ON b.benefit IN ('Private healthcare','Training budget','Remote work')
)
SELECT id FROM new_offer;

WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, experience_years, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id
    )
    VALUES (
        'Financial Analyst (IFRS)',
        'Group reporting and variance analysis.',
        12000, 18000, TRUE,
        NULL, TRUE,
        3, '2025-07-28 09:00+02', '2026-06-30 23:59+02', FALSE, FALSE,
        (SELECT id FROM sources WHERE name='Pracuj.pl'),
        (SELECT id FROM companies WHERE name='Nord Finance'),
        (SELECT id FROM location_details ORDER BY id LIMIT 1 OFFSET 4),
        (SELECT id FROM currencies WHERE currency='PLN'),
        (SELECT id FROM salary_periods WHERE period='monthly')
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, 'pracownik-magazynu-magnice-pow-wroclawski,oferta,1004311769?s=1f7c2c91&searchId=MTc1NjY3Mzg0MDI2NS4wNjI1', '2026-06-30 23:59+02' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id)
    SELECT n.id, s.id FROM new_offer n
    JOIN skills s ON s.skill IN ('Excel','IFRS','SQL')
),
exj AS (
    INSERT INTO experience_levels_junction (offer_id, experience_level_id)
    SELECT n.id, e.id FROM new_offer n
    JOIN experience_levels e ON e.level IN ('Intermediate')
),
edj AS (
    INSERT INTO education_levels_junction (offer_id, education_level_id)
    SELECT n.id, el.id FROM new_offer n
    JOIN education_levels el ON el.level IN ('Master')
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id)
    SELECT n.id, l.id FROM new_offer n
    JOIN languages l ON l.language IN ('Polish','English','German')
),
etj AS (
    INSERT INTO employment_types_junction (offer_id, employment_type_id)
    SELECT n.id, t.id FROM new_offer n
    JOIN employment_types t ON t.type IN ('Contract of mandate')
),
esj AS (
    INSERT INTO employment_schedules_junction (offer_id, employment_schedule_id)
    SELECT n.id, s.id FROM new_offer n
    JOIN employment_schedules s ON s.schedule IN ('Full-time','Part-time')
),
bj AS (
    INSERT INTO benefits_junction (offer_id, benefit_id)
    SELECT n.id, b.id FROM new_offer n
    JOIN benefits b ON b.benefit IN ('Private healthcare','Life insurance')
)
SELECT id FROM new_offer;

WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, experience_years, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id
    )
    VALUES (
        'Civil Engineer',
        'Site supervision and structural design.',
        11000, 16000, TRUE,
        FALSE, NULL,
        2, '2025-08-01 08:30+02', '2026-09-30 23:59+02', TRUE, TRUE,
        (SELECT id FROM sources WHERE name='Pracuj.pl'),
        (SELECT id FROM companies WHERE name='GreenBuild Engineers'),
        (SELECT id FROM location_details ORDER BY id LIMIT 1 OFFSET 3),
        (SELECT id FROM currencies WHERE currency='PLN'),
        (SELECT id FROM salary_periods WHERE period='monthly')
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, 'pracownik-magazynu-magnice-pow-wroclawski,oferta,1004311769?s=1f7c2c91&searchId=MTc1NjY3Mzg0MDI2NS4wNjI1', '2026-09-30 23:59+02' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id)
    SELECT n.id, s.id FROM new_offer n
    JOIN skills s ON s.skill IN ('AutoCAD','Civil Engineering','Project Management')
),
exj AS (
    INSERT INTO experience_levels_junction (offer_id, experience_level_id)
    SELECT n.id, e.id FROM new_offer n
    JOIN experience_levels e ON e.level IN ('Junior','Intermediate')
),
edj AS (
    INSERT INTO education_levels_junction (offer_id, education_level_id)
    SELECT n.id, el.id FROM new_offer n
    JOIN education_levels el ON el.level IN ('Bachelor','Master')
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id)
    SELECT n.id, l.id FROM new_offer n
    JOIN languages l ON l.language IN ('Polish','English')
),
etj AS (
    INSERT INTO employment_types_junction (offer_id, employment_type_id)
    SELECT n.id, t.id FROM new_offer n
    JOIN employment_types t ON t.type IN ('Contract for specific work','B2B')
),
esj AS (
    INSERT INTO employment_schedules_junction (offer_id, employment_schedule_id)
    SELECT n.id, s.id FROM new_offer n
    JOIN employment_schedules s ON s.schedule IN ('Full-time')
),
bj AS (
    INSERT INTO benefits_junction (offer_id, benefit_id)
    SELECT n.id, b.id FROM new_offer n
    JOIN benefits b ON b.benefit IN ('Private healthcare','Meal vouchers')
)
SELECT id FROM new_offer;

WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, experience_years, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id
    )
    VALUES (
        'Architect',
        'Concept and detailed design for mixed-use buildings.',
        9000, 14000, TRUE,
        FALSE, TRUE,
        3, '2025-08-05 11:00+02', '2026-05-31 23:59+02', FALSE, FALSE,
        (SELECT id FROM sources WHERE name='Pracuj.pl'),
        (SELECT id FROM companies WHERE name='GreenBuild Engineers'),
        (SELECT id FROM location_details ORDER BY id LIMIT 1 OFFSET 2),
        (SELECT id FROM currencies WHERE currency='PLN'),
        (SELECT id FROM salary_periods WHERE period='monthly')
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, 'pracownik-magazynu-magnice-pow-wroclawski,oferta,1004311769?s=1f7c2c91&searchId=MTc1NjY3Mzg0MDI2NS4wNjI1', '2026-05-31 23:59+02' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id)
    SELECT n.id, s.id FROM new_offer n
    JOIN skills s ON s.skill IN ('Architecture','CAD','Project Management')
),
exj AS (
    INSERT INTO experience_levels_junction (offer_id, experience_level_id)
    SELECT n.id, e.id FROM new_offer n
    JOIN experience_levels e ON e.level IN ('Intermediate')
),
edj AS (
    INSERT INTO education_levels_junction (offer_id, education_level_id)
    SELECT n.id, el.id FROM new_offer n
    JOIN education_levels el ON el.level IN ('Master')
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id)
    SELECT n.id, l.id FROM new_offer n
    JOIN languages l ON l.language IN ('Polish','English','German')
),
esj AS (
    INSERT INTO employment_schedules_junction (offer_id, employment_schedule_id)
    SELECT n.id, s.id FROM new_offer n
    JOIN employment_schedules s ON s.schedule IN ('Full-time','Freelance')
)
SELECT id FROM new_offer;

WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, experience_years, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id
    )
    VALUES (
        'Mathematics Teacher (High School)',
        NULL,
        NULL, NULL, NULL,
        FALSE, NULL,
        1, '2025-08-12 12:00+02', '2026-02-28 23:59+01', FALSE, TRUE,
        (SELECT id FROM sources WHERE name='OlxPraca'),
        (SELECT id FROM companies WHERE name='EduFuture School'),
        (SELECT id FROM location_details ORDER BY id LIMIT 1 OFFSET 6),
        NULL, NULL
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, 'kwalifikowany-dowodca-zmiany-ochrony-widzew-CID4-ID16ZTYP.html', '2026-02-28 23:59+01' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id)
    SELECT n.id, s.id FROM new_offer n
    JOIN skills s ON s.skill IN ('Mathematics Didactics','Project Management')
),
edj AS (
    INSERT INTO education_levels_junction (offer_id, education_level_id)
    SELECT n.id, el.id FROM new_offer n
    JOIN education_levels el ON el.level IN ('Master','Postgraduate')
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id)
    SELECT n.id, l.id FROM new_offer n
    JOIN languages l ON l.language IN ('Polish','Ukrainian')
),
esj AS (
    INSERT INTO employment_schedules_junction (offer_id, employment_schedule_id)
    SELECT n.id, s.id FROM new_offer n
    JOIN employment_schedules s ON s.schedule IN ('Full-time','Part-time')
)
SELECT id FROM new_offer;

WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, experience_years, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id
    )
    VALUES (
        'Pharmacist',
        'Dispensing medicines and patient counseling.',
        8500, 10500, TRUE,
        FALSE, NULL,
        2, '2025-07-20 09:30+02', '2026-03-31 23:59+02', TRUE, FALSE,
        (SELECT id FROM sources WHERE name='Jooble'),
        (SELECT id FROM companies WHERE name='PharmaNova'),
        (SELECT id FROM location_details ORDER BY id LIMIT 1 OFFSET 5),
        (SELECT id FROM currencies WHERE currency='PLN'),
        (SELECT id FROM salary_periods WHERE period='monthly')
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, '-5483726541865494826?ckey=asd&rgn=-1&pos=1&elckey=915678177178058315&pageType=20&p=1&sid=1844751898038482488&jobAge=45&relb=100&brelb=100&bscr=19999.800001641575&scr=19999.800001641575&searchTestGroup=1_1342_1&iid=-8222011219895262805', '2026-03-31 23:59+02' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id)
    SELECT n.id, (SELECT id FROM skills WHERE skill='Pharmacology') FROM new_offer n
),
exj AS (
    INSERT INTO experience_levels_junction (offer_id, experience_level_id)
    SELECT n.id, (SELECT id FROM experience_levels WHERE level='Intermediate') FROM new_offer n
),
edj AS (
    INSERT INTO education_levels_junction (offer_id, education_level_id)
    SELECT n.id, (SELECT id FROM education_levels WHERE level='Master') FROM new_offer n
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id)
    SELECT n.id, (SELECT id FROM languages WHERE language='Polish') FROM new_offer n
),
etj AS (
    INSERT INTO employment_types_junction (offer_id, employment_type_id)
    SELECT n.id, (SELECT id FROM employment_types WHERE type='Contract of mandate') FROM new_offer n
),
esj AS (
    INSERT INTO employment_schedules_junction (offer_id, employment_schedule_id)
    SELECT n.id, (SELECT id FROM employment_schedules WHERE schedule='Full-time') FROM new_offer n
)
SELECT id FROM new_offer;

WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, experience_years, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id
    )
    VALUES (
        'Clinical Research Associate',
        'Monitor clinical trials and ensure protocol compliance.',
        15000, 21000, TRUE,
        TRUE, NULL,
        3, '2025-08-15 10:15+02', '2026-08-31 23:59+02', FALSE, FALSE,
        (SELECT id FROM sources WHERE name='Pracuj.pl'),
        (SELECT id FROM companies WHERE name='PharmaNova'),
        NULL,
        (SELECT id FROM currencies WHERE currency='PLN'),
        (SELECT id FROM salary_periods WHERE period='monthly')
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, 'pracownik-magazynu-magnice-pow-wroclawski,oferta,1004311769?s=1f7c2c91&searchId=MTc1NjY3Mzg0MDI2NS4wNjI1', '2026-08-31 23:59+02' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id)
    SELECT n.id, s.id FROM new_offer n
    JOIN skills s ON s.skill IN ('Clinical Trials','Statistics','SPSS')
),
edj AS (
    INSERT INTO education_levels_junction (offer_id, education_level_id)
    SELECT n.id, el.id FROM new_offer n
    JOIN education_levels el ON el.level IN ('Bachelor','Master')
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id)
    SELECT n.id, l.id FROM new_offer n
    JOIN languages l ON l.language IN ('English')
),
bj AS (
    INSERT INTO benefits_junction (offer_id, benefit_id)
    SELECT n.id, b.id FROM new_offer n
    JOIN benefits b ON b.benefit IN ('Private healthcare','Training budget','Life insurance')
)
SELECT id FROM new_offer;

WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, experience_years, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id
    )
    VALUES (
        'Data Analyst',
        'Build dashboards and perform ad-hoc analysis.',
        12000, 17000, TRUE,
        TRUE, FALSE,
        2, '2025-08-18 09:45+02', '2026-07-31 23:59+02', FALSE, TRUE,
        (SELECT id FROM sources WHERE name='justjoin.it'),
        (SELECT id FROM companies WHERE name='Nord Finance'),
        (SELECT id FROM location_details ORDER BY id LIMIT 1 OFFSET 1),
        (SELECT id FROM currencies WHERE currency='PLN'),
        (SELECT id FROM salary_periods WHERE period='monthly')
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, 'nearmap-technical-lead-with-react-warszawa-javascrip', '2026-07-31 23:59+02' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id)
    SELECT n.id, s.id FROM new_offer n
    JOIN skills s ON s.skill IN ('SQL','Excel','Statistics','R')
),
exj AS (
    INSERT INTO experience_levels_junction (offer_id, experience_level_id)
    SELECT n.id, e.id FROM new_offer n
    JOIN experience_levels e ON e.level IN ('Junior','Intermediate')
),
edj AS (
    INSERT INTO education_levels_junction (offer_id, education_level_id)
    SELECT n.id, el.id FROM new_offer n
    JOIN education_levels el ON el.level IN ('Bachelor')
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id)
    SELECT n.id, l.id FROM new_offer n
    JOIN languages l ON l.language IN ('Polish','English')
)
SELECT id FROM new_offer;

WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, experience_years, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id
    )
    VALUES (
        'Electrical Engineer',
        'Power systems design and commissioning.',
        13000, NULL, TRUE,
        NULL, TRUE,
        4, '2025-07-30 14:00+02', '2026-04-30 23:59+02', TRUE, FALSE,
        (SELECT id FROM sources WHERE name='Pracuj.pl'),
        (SELECT id FROM companies WHERE name='BlueWave Energy'),
        (SELECT id FROM location_details ORDER BY id LIMIT 1 OFFSET 7),
        (SELECT id FROM currencies WHERE currency='PLN'),
        (SELECT id FROM salary_periods WHERE period='monthly')
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, 'pracownik-magazynu-magnice-pow-wroclawski,oferta,1004311769?s=1f7c2c91&searchId=MTc1NjY3Mzg0MDI2NS4wNjI1', '2026-04-30 23:59+02' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id)
    SELECT n.id, s.id FROM new_offer n
    JOIN skills s ON s.skill IN ('Electrical Engineering','CAD','Project Management')
),
exj AS (
    INSERT INTO experience_levels_junction (offer_id, experience_level_id)
    SELECT n.id, e.id FROM new_offer n
    JOIN experience_levels e ON e.level IN ('Intermediate','Senior')
),
edj AS (
    INSERT INTO education_levels_junction (offer_id, education_level_id)
    SELECT n.id, el.id FROM new_offer n
    JOIN education_levels el ON el.level IN ('Master')
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id)
    SELECT n.id, l.id FROM new_offer n
    JOIN languages l ON l.language IN ('Polish','English')
),
bj AS (
    INSERT INTO benefits_junction (offer_id, benefit_id)
    SELECT n.id, b.id FROM new_offer n
    JOIN benefits b ON b.benefit IN ('Private healthcare','Company car')
)
SELECT id FROM new_offer;

WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, experience_years, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id
    )
    VALUES (
        'Project Manager (R&D)',
        'Lead cross-functional R&D projects.',
        17000, 23000, TRUE,
        TRUE, TRUE,
        5, '2025-08-02 15:00+02', '2026-10-31 23:59+01', FALSE, FALSE,
        (SELECT id FROM sources WHERE name='OlxPraca'),
        (SELECT id FROM companies WHERE name='AutoDrive Systems'),
        (SELECT id FROM location_details ORDER BY id LIMIT 1 OFFSET 8),
        (SELECT id FROM currencies WHERE currency='PLN'),
        (SELECT id FROM salary_periods WHERE period='monthly')
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, 'kwalifikowany-dowodca-zmiany-ochrony-widzew-CID4-ID16ZTYP.html', '2026-10-31 23:59+01' FROM new_offer
)
SELECT id FROM new_offer;

WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, experience_years, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id
    )
    VALUES (
        'Scrum Master',
        'Coach teams and facilitate Scrum ceremonies.',
        NULL, NULL, NULL,
        TRUE, TRUE,
        3, '2025-08-06 08:00+02', '2026-01-31 23:59+01', FALSE, TRUE,
        (SELECT id FROM sources WHERE name='justjoin.it'),
        (SELECT id FROM companies WHERE name='Vistula Tech'),
        NULL,
        NULL, NULL
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, 'nearmap-technical-lead-with-react-warszawa-javascrip', '2026-01-31 23:59+01' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id)
    SELECT n.id, s.id FROM new_offer n
    JOIN skills s ON s.skill IN ('Scrum','Project Management')
),
exj AS (
    INSERT INTO experience_levels_junction (offer_id, experience_level_id)
    SELECT n.id, e.id FROM new_offer n
    JOIN experience_levels e ON e.level IN ('Intermediate')
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id)
    SELECT n.id, l.id FROM new_offer n
    JOIN languages l ON l.language IN ('English','Polish')
)
SELECT id FROM new_offer;

WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, experience_years, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id
    )
    VALUES (
        'Cloud Engineer (AWS/Azure)',
        'Build IaC and manage cloud workloads.',
        18000, 26000, TRUE,
        TRUE, FALSE,
        4, '2025-07-25 16:45+02', '2026-11-30 23:59+01', TRUE, FALSE,
        (SELECT id FROM sources WHERE name='justjoin.it'),
        (SELECT id FROM companies WHERE name='Vistula Tech'),
        (SELECT id FROM location_details ORDER BY id LIMIT 1 OFFSET 0),
        (SELECT id FROM currencies WHERE currency='PLN'),
        (SELECT id FROM salary_periods WHERE period='monthly')
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, 'nearmap-technical-lead-with-react-warszawa-javascrip', '2026-11-30 23:59+01' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id)
    SELECT n.id, s.id FROM new_offer n
    JOIN skills s ON s.skill IN ('AWS','Azure','Python','PostgreSQL')
),
exj AS (
    INSERT INTO experience_levels_junction (offer_id, experience_level_id)
    SELECT n.id, e.id FROM new_offer n
    JOIN experience_levels e ON e.level IN ('Senior')
),
edj AS (
    INSERT INTO education_levels_junction (offer_id, education_level_id)
    SELECT n.id, el.id FROM new_offer n
    JOIN education_levels el ON el.level IN ('Bachelor')
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id)
    SELECT n.id, l.id FROM new_offer n
    JOIN languages l ON l.language IN ('English')
),
bj AS (
    INSERT INTO benefits_junction (offer_id, benefit_id)
    SELECT n.id, b.id FROM new_offer n
    JOIN benefits b ON b.benefit IN ('Private healthcare','Training budget','Stock options')
)
SELECT id FROM new_offer;

WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, experience_years, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id
    )
    VALUES (
        'Graphic Designer',
        'Branding and marketing materials.',
        7000, 10000, TRUE,
        TRUE, TRUE,
        2, '2025-08-07 13:10+02', '2026-03-31 23:59+02', FALSE, FALSE,
        (SELECT id FROM sources WHERE name='OlxPraca'),
        (SELECT id FROM companies WHERE name='ArtVision Studio'),
        (SELECT id FROM location_details ORDER BY id LIMIT 1 OFFSET 2),
        (SELECT id FROM currencies WHERE currency='PLN'),
        (SELECT id FROM salary_periods WHERE period='monthly')
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, 'kwalifikowany-dowodca-zmiany-ochrony-widzew-CID4-ID16ZTYP.html', '2026-03-31 23:59+02' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id)
    SELECT n.id, s.id FROM new_offer n
    JOIN skills s ON s.skill IN ('Photoshop','Illustrator')
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id)
    SELECT n.id, l.id FROM new_offer n
    JOIN languages l ON l.language IN ('Polish')
),
etj AS (
    INSERT INTO employment_types_junction (offer_id, employment_type_id)
    SELECT n.id, t.id FROM new_offer n
    JOIN employment_types t ON t.type IN ('Contract for specific work','Freelance')
)
SELECT id FROM new_offer;

WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, experience_years, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id
    )
    VALUES (
        'Biostatistician',
        'Statistical analysis in clinical trials.',
        16000, 22000, TRUE,
        TRUE, NULL,
        3, '2025-07-18 10:00+02', '2026-09-30 23:59+02', FALSE, TRUE,
        (SELECT id FROM sources WHERE name='Jooble'),
        (SELECT id FROM companies WHERE name='Ocean Research Institute'),
        NULL,
        (SELECT id FROM currencies WHERE currency='PLN'),
        (SELECT id FROM salary_periods WHERE period='monthly')
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, '-5483726541865494826?ckey=asd&rgn=-1&pos=1&elckey=915678177178058315&pageType=20&p=1&sid=1844751898038482488&jobAge=45&relb=100&brelb=100&bscr=19999.800001641575&scr=19999.800001641575&searchTestGroup=1_1342_1&iid=-8222011219895262805', '2026-09-30 23:59+02' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id)
    SELECT n.id, (SELECT id FROM skills WHERE skill='Statistics') FROM new_offer n
),
exj AS (
    INSERT INTO experience_levels_junction (offer_id, experience_level_id)
    SELECT n.id, (SELECT id FROM experience_levels WHERE level='Intermediate') FROM new_offer n
),
edj AS (
    INSERT INTO education_levels_junction (offer_id, education_level_id)
    SELECT n.id, (SELECT id FROM education_levels WHERE level='Master') FROM new_offer n
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id)
    SELECT n.id, (SELECT id FROM languages WHERE language='English') FROM new_offer n
)
SELECT id FROM new_offer;

WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, experience_years, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id
    )
    VALUES (
        'Auditor',
        'Plan audits and evaluate internal controls.',
        11000, 15000, TRUE,
        FALSE, TRUE,
        2, '2025-08-09 09:20+02', '2026-08-31 23:59+02', FALSE, FALSE,
        (SELECT id FROM sources WHERE name='Pracuj.pl'),
        (SELECT id FROM companies WHERE name='Nord Finance'),
        (SELECT id FROM location_details ORDER BY id LIMIT 1 OFFSET 1),
        (SELECT id FROM currencies WHERE currency='PLN'),
        (SELECT id FROM salary_periods WHERE period='monthly')
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, 'pracownik-magazynu-magnice-pow-wroclawski,oferta,1004311769?s=1f7c2c91&searchId=MTc1NjY3Mzg0MDI2NS4wNjI1', '2026-08-31 23:59+02' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id)
    SELECT n.id, s.id FROM new_offer n
    JOIN skills s ON s.skill IN ('Auditing','Excel')
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id)
    SELECT n.id, l.id FROM new_offer n
    JOIN languages l ON l.language IN ('Polish','English')
),
edj AS (
    INSERT INTO education_levels_junction (offer_id, education_level_id)
    SELECT n.id, el.id FROM new_offer n
    JOIN education_levels el ON el.level IN ('Bachelor')
)
SELECT id FROM new_offer;

WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, experience_years, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id
    )
    VALUES (
        'Tax Lawyer',
        'Corporate tax advisory and litigation support.',
        18000, 28000, TRUE,
        TRUE, TRUE,
        5, '2025-08-11 10:40+02', '2026-12-31 23:59+01', TRUE, FALSE,
        (SELECT id FROM sources WHERE name='Pracuj.pl'),
        (SELECT id FROM companies WHERE name='Lex & Partners'),
        (SELECT id FROM location_details ORDER BY id LIMIT 1 OFFSET 0),
        (SELECT id FROM currencies WHERE currency='PLN'),
        (SELECT id FROM salary_periods WHERE period='monthly')
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, 'pracownik-magazynu-magnice-pow-wroclawski,oferta,1004311769?s=1f7c2c91&searchId=MTc1NjY3Mzg0MDI2NS4wNjI1', '2026-12-31 23:59+01' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id)
    SELECT n.id, s.id FROM new_offer n
    JOIN skills s ON s.skill IN ('Tax Law','Project Management')
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id)
    SELECT n.id, l.id FROM new_offer n
    JOIN languages l ON l.language IN ('Polish','English','German')
),
exj AS (
    INSERT INTO experience_levels_junction (offer_id, experience_level_id)
    SELECT n.id, e.id FROM new_offer n
    JOIN experience_levels e ON e.level IN ('Senior','Lead')
),
edj AS (
    INSERT INTO education_levels_junction (offer_id, education_level_id)
    SELECT n.id, el.id FROM new_offer n
    JOIN education_levels el ON el.level IN ('Master')
)
SELECT id FROM new_offer;

WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, experience_years, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id
    )
    VALUES (
        'Econometrician',
        'Model building and forecasting for risk.',
        40000, 60000, TRUE,
        TRUE, NULL,
        3, '2025-07-27 12:00+02', '2026-07-31 23:59+02', FALSE, TRUE,
        (SELECT id FROM sources WHERE name='Jooble'),
        (SELECT id FROM companies WHERE name='Nord Finance'),
        NULL,
        (SELECT id FROM currencies WHERE currency='USD'),
        (SELECT id FROM salary_periods WHERE period='yearly')
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, '-5483726541865494826?ckey=asd&rgn=-1&pos=1&elckey=915678177178058315&pageType=20&p=1&sid=1844751898038482488&jobAge=45&relb=100&brelb=100&bscr=19999.800001641575&scr=19999.800001641575&searchTestGroup=1_1342_1&iid=-8222011219895262805', '2026-07-31 23:59+02' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id)
    SELECT n.id, s.id FROM new_offer n
    JOIN skills s ON s.skill IN ('Econometrics','R','SQL')
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id)
    SELECT n.id, l.id FROM new_offer n
    JOIN languages l ON l.language IN ('English')
)
SELECT id FROM new_offer;

WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, experience_years, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id
    )
    VALUES (
        'Marine Biologist',
        'Field research aboard research vessels.',
        NULL, NULL, NULL,
        FALSE, NULL,
        2, '2025-08-03 08:50+02', '2026-06-30 23:59+02', FALSE, FALSE,
        (SELECT id FROM sources WHERE name='Jooble'),
        (SELECT id FROM companies WHERE name='Ocean Research Institute'),
        (SELECT id FROM location_details ORDER BY id LIMIT 1 OFFSET 10),
        NULL, NULL
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, '-5483726541865494826?ckey=asd&rgn=-1&pos=1&elckey=915678177178058315&pageType=20&p=1&sid=1844751898038482488&jobAge=45&relb=100&brelb=100&bscr=19999.800001641575&scr=19999.800001641575&searchTestGroup=1_1342_1&iid=-8222011219895262805', '2026-06-30 23:59+02' FROM new_offer
)
SELECT id FROM new_offer;

WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, experience_years, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id
    )
    VALUES (
        'Marketing Analyst',
        'Analyze campaign performance and market trends.',
        250, 350, FALSE,
        TRUE, TRUE,
        2, '2025-08-14 14:30+02', '2026-05-31 23:59+02', TRUE, TRUE,
        (SELECT id FROM sources WHERE name='OlxPraca'),
        (SELECT id FROM companies WHERE name='FoodTech Polska'),
        (SELECT id FROM location_details ORDER BY id LIMIT 1 OFFSET 2),
        (SELECT id FROM currencies WHERE currency='EUR'),
        (SELECT id FROM salary_periods WHERE period='daily')
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, 'kwalifikowany-dowodca-zmiany-ochrony-widzew-CID4-ID16ZTYP.html', '2026-05-31 23:59+02' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id)
    SELECT n.id, s.id FROM new_offer n
    JOIN skills s ON s.skill IN ('SQL','Excel','Statistics')
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id)
    SELECT n.id, l.id FROM new_offer n
    JOIN languages l ON l.language IN ('English','Polish','French')
)
SELECT id FROM new_offer;

WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, experience_years, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id
    )
    VALUES (
        'Veterinarian',
        'Small animal practice with surgery rotations.',
        60, 90, TRUE,
        FALSE, NULL,
        1, '2025-08-19 09:10+02', '2026-11-30 23:59+01', FALSE, TRUE,
        (SELECT id FROM sources WHERE name='Pracuj.pl'),
        (SELECT id FROM companies WHERE name='VetCare Center'),
        (SELECT id FROM location_details ORDER BY id LIMIT 1 OFFSET 11),
        (SELECT id FROM currencies WHERE currency='PLN'),
        (SELECT id FROM salary_periods WHERE period='hourly')
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, 'pracownik-magazynu-magnice-pow-wroclawski,oferta,1004311769?s=1f7c2c91&searchId=MTc1NjY3Mzg0MDI2NS4wNjI1', '2026-11-30 23:59+01' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id)
    SELECT n.id, s.id FROM new_offer n
    JOIN skills s ON s.skill IN ('Clinical Trials','Project Management')
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id)
    SELECT n.id, l.id FROM new_offer n
    JOIN languages l ON l.language IN ('Polish','Ukrainian')
)
SELECT id FROM new_offer;

WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, experience_years, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id
    )
    VALUES (
        'Embedded Systems Engineer',
        'Develop firmware for automotive controllers.',
        16000, 23000, TRUE,
        NULL, TRUE,
        3, '2025-08-20 11:20+02', '2026-09-30 23:59+02', TRUE, FALSE,
        (SELECT id FROM sources WHERE name='justjoin.it'),
        (SELECT id FROM companies WHERE name='AutoDrive Systems'),
        (SELECT id FROM location_details ORDER BY id LIMIT 1 OFFSET 8),
        (SELECT id FROM currencies WHERE currency='PLN'),
        (SELECT id FROM salary_periods WHERE period='monthly')
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, 'nearmap-technical-lead-with-react-warszawa-javascrip', '2026-09-30 23:59+02' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id)
    SELECT n.id, s.id FROM new_offer n
    JOIN skills s ON s.skill IN ('Microcontrollers','C','Project Management')
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id)
    SELECT n.id, l.id FROM new_offer n
    JOIN languages l ON l.language IN ('English','Polish')
),
exj AS (
    INSERT INTO experience_levels_junction (offer_id, experience_level_id)
    SELECT n.id, e.id FROM new_offer n
    JOIN experience_levels e ON e.level IN ('Intermediate')
)
SELECT id FROM new_offer;

WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, experience_years, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id
    )
    VALUES (
        'Aerospace Engineer',
        NULL,
        17000, 26000, TRUE,
        FALSE, TRUE,
        4, '2025-08-21 10:00+02', '2026-08-31 23:59+02', FALSE, FALSE,
        (SELECT id FROM sources WHERE name='Pracuj.pl'),
        (SELECT id FROM companies WHERE name='AeroPol'),
        (SELECT id FROM location_details ORDER BY id LIMIT 1 OFFSET 9),
        (SELECT id FROM currencies WHERE currency='PLN'),
        (SELECT id FROM salary_periods WHERE period='monthly')
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, 'pracownik-magazynu-magnice-pow-wroclawski,oferta,1004311769?s=1f7c2c91&searchId=MTc1NjY3Mzg0MDI2NS4wNjI1', '2026-08-31 23:59+02' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id)
    SELECT n.id, s.id FROM new_offer n
    JOIN skills s ON s.skill IN ('MATLAB','Project Management')
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id)
    SELECT n.id, l.id FROM new_offer n
    JOIN languages l ON l.language IN ('English')
),
edj AS (
    INSERT INTO education_levels_junction (offer_id, education_level_id)
    SELECT n.id, el.id FROM new_offer n
    JOIN education_levels el ON el.level IN ('Bachelor','Master')
)
SELECT id FROM new_offer;