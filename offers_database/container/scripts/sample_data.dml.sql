-- ---------------------------------------------------------
-- Sources
-- ---------------------------------------------------------
INSERT INTO sources (name, base_url) VALUES
    ('Pracuj.pl', 'https://www.pracuj.pl/praca/'),
    ('OlxPraca', 'https://www.olx.pl/oferta/praca/'),
    ('justjoin.it', 'https://justjoin.it/job-offer/'),
    ('Jooble', 'https://pl.jooble.org/desc/')
ON CONFLICT (name) DO NOTHING;

-- ---------------------------------------------------------
-- Companies
-- ---------------------------------------------------------
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

INSERT INTO language_levels (level) VALUES
    ('a1'), ('a2'), ('b1'), ('b2'), ('c1'), ('c2')
ON CONFLICT (level) DO NOTHING;

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
    ('Tax Law'), ('Econometrics'), ('Microcontrollers'), ('C')
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
    ('33', NULL, NULL, NULL, NULL, NULL),
    (NULL, 52.2297, 21.0122, (SELECT id FROM cities WHERE city='Warsaw'), NULL, NULL),
    (NULL, 50.0647, 19.9450, (SELECT id FROM cities WHERE city='Krakow'), NULL, NULL),
    (NULL, 54.3520, 18.6466, (SELECT id FROM cities WHERE city='Gdansk'), NULL, NULL);

-- ---------------------------------------------------------
-- Leading categories and sub-categories
-- ---------------------------------------------------------
INSERT INTO leading_categories (id, leading_category) VALUES
    (1, 'Information-Technology'),
    (2, 'Finance'),
    (3, 'Engineering'),
    (4, 'Healthcare'),
    (5, 'Education'),
    (6, 'Design'),
    (7, 'Research'),
    (8, 'Legal'),
    (9, 'Marketing'),
    (10, 'Veterinary'),
    (11, 'Aerospace'),
    (12, 'Marine')
ON CONFLICT (id) DO NOTHING;

INSERT INTO sub_categories (id, sub_category) VALUES
    (1101, 'programming'),
    (1102, 'data-analytics'),
    (1103, 'cloud'),
    (1104, 'project-management'),
    (2101, 'accounting'),
    (2102, 'auditing'),
    (2103, 'tax-law'),
    (2104, 'econometrics'),
    (3101, 'civil-engineering'),
    (3102, 'architecture'),
    (3103, 'electrical-engineering'),
    (3104, 'embedded-systems'),
    (4101, 'pharmacy'),
    (4102, 'clinical-research'),
    (5101, 'teaching'),
    (6101, 'graphic-design'),
    (7101, 'statistics'),
    (9101, 'marketing-analytics'),
    (10001, 'veterinary'),
    (11001, 'aerospace-engineering'),
    (12001, 'marine-biology')
ON CONFLICT (id) DO NOTHING;

INSERT INTO leading_sub_categories (leading_category_id, sub_category_id) VALUES
    (1,1101),(1,1102),(1,1103),(1,1104),
    (2,2101),(2,2102),(2,2103),(2,2104),
    (3,3101),(3,3102),(3,3103),(3,3104),(3,1104),
    (4,4101),(4,4102),
    (5,5101),
    (6,6101),
    (7,7101),
    (8,2103),
    (9,9101),
    (10,10001),
    (11,11001),
    (12,12001)
ON CONFLICT (leading_category_id, sub_category_id) DO NOTHING;

-- =========================================================
-- External offers
-- =========================================================

-- 1) Software Engineer (Python) — IT / programming, cloud
WITH new_offer AS (
    INSERT INTO offers (
        job_title, description,
        salary_from, salary_to, is_gross,
        is_remote, is_hybrid,
        published, expires, is_urgent, is_for_ukrainians,
        source_id, company_id, location_detail_id, currency_id, salary_period_id, leading_category_id
    )
    VALUES (
        'Software Engineer (Python)',
        'Develop backend services and data pipelines.',
        15000, 22000, TRUE,
        TRUE, FALSE,
        '2025-08-10 10:00+02', '2026-12-31 23:59+01', FALSE, TRUE,
        (SELECT id FROM sources WHERE name='justjoin.it'),
        (SELECT id FROM companies WHERE name='Vistula Tech'),
        (SELECT id FROM location_details ORDER BY id LIMIT 1 OFFSET 0),
        (SELECT id FROM currencies WHERE currency='PLN'),
        (SELECT id FROM salary_periods WHERE period='monthly'),
        (1)
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, 'nearmap-technical-lead-with-react-warszawa-javascript', '2026-12-31 23:59+01' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id, experience_level_id, experience_months)
    SELECT n.id, s.id, (SELECT id FROM experience_levels WHERE level='Intermediate'), 24
    FROM new_offer n
    JOIN skills s ON s.skill IN ('Python','Django','PostgreSQL','AWS')
),
edj AS (
    INSERT INTO education_levels_junction (offer_id, education_level_id)
    SELECT n.id, el.id FROM new_offer n
    JOIN education_levels el ON el.level IN ('Bachelor')
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id, language_level_id)
    SELECT n.id, l.id, ll.id
    FROM new_offer n
    JOIN (VALUES ('Polish','c1'),('English','b2')) v(lang, lvl)
         ON TRUE
    JOIN languages l ON l.language=v.lang
    JOIN language_levels ll ON ll.level=v.lvl
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
),
scj AS (
    INSERT INTO sub_categories_junction (offer_id, sub_category_id)
    SELECT n.id, sc.id FROM new_offer n
    JOIN sub_categories sc ON sc.id IN (1101,1103)
)
SELECT id FROM new_offer;

-- 2) Financial Analyst (IFRS) — Finance / accounting
WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id, leading_category_id
    )
    VALUES (
        'Financial Analyst (IFRS)',
        'Group reporting and variance analysis.',
        12000, 18000, TRUE,
        NULL, TRUE,
        '2025-07-28 09:00+02', '2026-06-30 23:59+02',
        FALSE, FALSE,
        (SELECT id FROM sources WHERE name='Pracuj.pl'),
        (SELECT id FROM companies WHERE name='Nord Finance'),
        (SELECT id FROM location_details ORDER BY id LIMIT 1 OFFSET 4),
        (SELECT id FROM currencies WHERE currency='PLN'),
        (SELECT id FROM salary_periods WHERE period='monthly'),
        (2)
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, 'pracownik-magazynu-magnice-pow-wroclawski,oferta,1004311769?s=1f7c2c91&searchId=MTc1NjY3Mzg0MDI2NS4wNjI1', '2026-06-30 23:59+02' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id, experience_level_id, experience_months)
    SELECT n.id, s.id, (SELECT id FROM experience_levels WHERE level='Intermediate'), 36
    FROM new_offer n
    JOIN skills s ON s.skill IN ('Excel','IFRS','SQL')
),
edj AS (
    INSERT INTO education_levels_junction (offer_id, education_level_id)
    SELECT n.id, el.id FROM new_offer n
    JOIN education_levels el ON el.level IN ('Master')
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id, language_level_id)
    SELECT n.id, l.id, ll.id FROM new_offer n
    JOIN (VALUES ('Polish','c1'),('English','b2'),('German','b1')) v(lang,lvl)
      ON TRUE
    JOIN languages l ON l.language=v.lang
    JOIN language_levels ll ON ll.level=v.lvl
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
),
scj AS (
    INSERT INTO sub_categories_junction (offer_id, sub_category_id)
    SELECT n.id, (2101) FROM new_offer n
)
SELECT id FROM new_offer;

-- 3) Civil Engineer — Engineering / civil-engineering
WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id, leading_category_id
    )
    VALUES (
        'Civil Engineer',
        'Site supervision and structural design.',
        11000, 16000, TRUE,
        FALSE, NULL,
        '2025-08-01 08:30+02', '2026-09-30 23:59+02', TRUE, TRUE,
        (SELECT id FROM sources WHERE name='Pracuj.pl'),
        (SELECT id FROM companies WHERE name='GreenBuild Engineers'),
        (SELECT id FROM location_details ORDER BY id LIMIT 1 OFFSET 3),
        (SELECT id FROM currencies WHERE currency='PLN'),
        (SELECT id FROM salary_periods WHERE period='monthly'),
        (3)
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, 'pracownik-magazynu-magnice-pow-wroclawski,oferta,1004311769?s=1f7c2c91&searchId=MTc1NjY3Mzg0MDI2NS4wNjI1', '2026-09-30 23:59+02' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id, experience_level_id, experience_months)
    SELECT n.id, s.id, (SELECT id FROM experience_levels WHERE level='Intermediate'), 24
    FROM new_offer n
    JOIN skills s ON s.skill IN ('AutoCAD','Civil Engineering','Project Management')
),
edj AS (
    INSERT INTO education_levels_junction (offer_id, education_level_id)
    SELECT n.id, el.id FROM new_offer n
    JOIN education_levels el ON el.level IN ('Bachelor','Master')
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id, language_level_id)
    SELECT n.id, l.id, ll.id FROM new_offer n
    JOIN (VALUES ('Polish','c1'),('English','b2')) v(lang,lvl)
     ON TRUE
    JOIN languages l ON l.language=v.lang
    JOIN language_levels ll ON ll.level=v.lvl
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
),
scj AS (
    INSERT INTO sub_categories_junction (offer_id, sub_category_id)
    SELECT n.id, (3101) FROM new_offer n
)
SELECT id FROM new_offer;

-- 4) Architect — Engineering / architecture
WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id, leading_category_id
    )
    VALUES (
        'Architect',
        'Concept and detailed design for mixed-use buildings.',
        9000, 14000, TRUE,
        FALSE, TRUE,
        '2025-08-05 11:00+02', '2026-05-31 23:59+02', FALSE, FALSE,
        (SELECT id FROM sources WHERE name='Pracuj.pl'),
        (SELECT id FROM companies WHERE name='GreenBuild Engineers'),
        (SELECT id FROM location_details ORDER BY id LIMIT 1 OFFSET 2),
        (SELECT id FROM currencies WHERE currency='PLN'),
        (SELECT id FROM salary_periods WHERE period='monthly'),
        (3)
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, 'pracownik-magazynu-magnice-pow-wroclawski,oferta,1004311769?s=1f7c2c91&searchId=MTc1NjY3Mzg0MDI2NS4wNjI1', '2026-05-31 23:59+02' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id, experience_level_id, experience_months)
    SELECT n.id, s.id, (SELECT id FROM experience_levels WHERE level='Intermediate'), 36
    FROM new_offer n
    JOIN skills s ON s.skill IN ('Architecture','CAD','Project Management')
),
edj AS (
    INSERT INTO education_levels_junction (offer_id, education_level_id)
    SELECT n.id, el.id FROM new_offer n
    JOIN education_levels el ON el.level IN ('Master')
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id, language_level_id)
    SELECT n.id, l.id, ll.id FROM new_offer n
    JOIN (VALUES ('Polish','c1'),('English','b2'),('German','b1')) v(lang,lvl)
     ON TRUE
    JOIN languages l ON l.language=v.lang
    JOIN language_levels ll ON ll.level=v.lvl
),
esj AS (
    INSERT INTO employment_schedules_junction (offer_id, employment_schedule_id)
    SELECT n.id, s.id FROM new_offer n
    JOIN employment_schedules s ON s.schedule IN ('Full-time','Freelance')
),
scj AS (
    INSERT INTO sub_categories_junction (offer_id, sub_category_id)
    SELECT n.id, (3102) FROM new_offer n
)
SELECT id FROM new_offer;

-- 5) Mathematics Teacher — Education / teaching
WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id, leading_category_id
    )
    VALUES (
        'Mathematics Teacher (High School)',
        NULL,
        NULL, NULL, NULL,
        FALSE, NULL,
        '2025-08-12 12:00+02', '2026-02-28 23:59+01', FALSE, TRUE,
        (SELECT id FROM sources WHERE name='OlxPraca'),
        (SELECT id FROM companies WHERE name='EduFuture School'),
        (SELECT id FROM location_details ORDER BY id LIMIT 1 OFFSET 6),
        NULL, NULL,
        (5)
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, 'kwalifikowany-dowodca-zmiany-ochrony-widzew-CID4-ID16ZTYP.html', '2026-02-28 23:59+01' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id, experience_level_id, experience_months)
    SELECT n.id, s.id, (SELECT id FROM experience_levels WHERE level='Junior'), 12
    FROM new_offer n
    JOIN skills s ON s.skill IN ('Mathematics Didactics','Project Management')
),
edj AS (
    INSERT INTO education_levels_junction (offer_id, education_level_id)
    SELECT n.id, el.id FROM new_offer n
    JOIN education_levels el ON el.level IN ('Master','Postgraduate')
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id, language_level_id)
    SELECT n.id, l.id, ll.id FROM new_offer n
    JOIN (VALUES ('Polish','c1'),('Ukrainian','b2')) v(lang,lvl)
     ON TRUE
    JOIN languages l ON l.language=v.lang
    JOIN language_levels ll ON ll.level=v.lvl
),
esj AS (
    INSERT INTO employment_schedules_junction (offer_id, employment_schedule_id)
    SELECT n.id, s.id FROM new_offer n
    JOIN employment_schedules s ON s.schedule IN ('Full-time','Part-time')
),
scj AS (
    INSERT INTO sub_categories_junction (offer_id, sub_category_id)
    SELECT n.id, (5101) FROM new_offer n
)
SELECT id FROM new_offer;

-- 6) Pharmacist — Healthcare / pharmacy
WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id, leading_category_id
    )
    VALUES (
        'Pharmacist',
        'Dispensing medicines and patient counseling.',
        8500, 10500, TRUE,
        FALSE, NULL,
        '2025-07-20 09:30+02', '2026-03-31 23:59+02', TRUE, FALSE,
        (SELECT id FROM sources WHERE name='Jooble'),
        (SELECT id FROM companies WHERE name='PharmaNova'),
        (SELECT id FROM location_details ORDER BY id LIMIT 1 OFFSET 5),
        (SELECT id FROM currencies WHERE currency='PLN'),
        (SELECT id FROM salary_periods WHERE period='monthly'),
        (4)
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, '-5483726541865494826?ckey=asd&rgn=-1&pos=1&elckey=915678177178058315&pageType=20&p=1&sid=1844751898038482488&jobAge=45&relb=100&brelb=100&bscr=19999.800001641575&scr=19999.800001641575&searchTestGroup=1_1342_1&iid=-8222011219895262805', '2026-03-31 23:59+02' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id, experience_level_id, experience_months)
    SELECT n.id, (SELECT id FROM skills WHERE skill='Pharmacology'),
           (SELECT id FROM experience_levels WHERE level='Intermediate'), 24
    FROM new_offer n
),
edj AS (
    INSERT INTO education_levels_junction (offer_id, education_level_id)
    SELECT n.id, (SELECT id FROM education_levels WHERE level='Master') FROM new_offer n
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id, language_level_id)
    SELECT n.id, (SELECT id FROM languages WHERE language='Polish'),
           (SELECT id FROM language_levels WHERE level='c1')
    FROM new_offer n
),
etj AS (
    INSERT INTO employment_types_junction (offer_id, employment_type_id)
    SELECT n.id, (SELECT id FROM employment_types WHERE type='Contract of mandate')
    FROM new_offer n
),
esj AS (
    INSERT INTO employment_schedules_junction (offer_id, employment_schedule_id)
    SELECT n.id, (SELECT id FROM employment_schedules WHERE schedule='Full-time')
    FROM new_offer n
),
scj AS (
    INSERT INTO sub_categories_junction (offer_id, sub_category_id)
    SELECT n.id, (4101) FROM new_offer n
)
SELECT id FROM new_offer;

-- 7) Clinical Research Associate — Healthcare / clinical-research
WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id, leading_category_id
    )
    VALUES (
        'Clinical Research Associate',
        'Monitor clinical trials and ensure protocol compliance.',
        15000, 21000, TRUE,
        TRUE, NULL,
        '2025-08-15 10:15+02', '2026-08-31 23:59+02', FALSE, FALSE,
        (SELECT id FROM sources WHERE name='Pracuj.pl'),
        (SELECT id FROM companies WHERE name='PharmaNova'),
        NULL,
        (SELECT id FROM currencies WHERE currency='PLN'),
        (SELECT id FROM salary_periods WHERE period='monthly'),
        (4)
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, 'pracownik-magazynu-magnice-pow-wroclawski,oferta,1004311769?s=1f7c2c91&searchId=MTc1NjY3Mzg0MDI2NS4wNjI1', '2026-08-31 23:59+02' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id, experience_level_id, experience_months)
    SELECT n.id, s.id, (SELECT id FROM experience_levels WHERE level='Intermediate'), 36
    FROM new_offer n
    JOIN skills s ON s.skill IN ('Clinical Trials','Statistics','SPSS')
),
edj AS (
    INSERT INTO education_levels_junction (offer_id, education_level_id)
    SELECT n.id, el.id FROM new_offer n
    JOIN education_levels el ON el.level IN ('Bachelor','Master')
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id, language_level_id)
    SELECT n.id, (SELECT id FROM languages WHERE language='English'),
           (SELECT id FROM language_levels WHERE level='b2')
    FROM new_offer n
),
bj AS (
    INSERT INTO benefits_junction (offer_id, benefit_id)
    SELECT n.id, b.id FROM new_offer n
    JOIN benefits b ON b.benefit IN ('Private healthcare','Training budget','Life insurance')
),
scj AS (
    INSERT INTO sub_categories_junction (offer_id, sub_category_id)
    SELECT n.id, (4102) FROM new_offer n
)
SELECT id FROM new_offer;

-- 8) Data Analyst — IT / data-analytics
WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id, leading_category_id
    )
    VALUES (
        'Data Analyst',
        'Build dashboards and perform ad-hoc analysis.',
        12000, 17000, TRUE,
        TRUE, FALSE,
        '2025-08-18 09:45+02', '2026-07-31 23:59+02', FALSE, TRUE,
        (SELECT id FROM sources WHERE name='justjoin.it'),
        (SELECT id FROM companies WHERE name='Nord Finance'),
        (SELECT id FROM location_details ORDER BY id LIMIT 1 OFFSET 1),
        (SELECT id FROM currencies WHERE currency='PLN'),
        (SELECT id FROM salary_periods WHERE period='monthly'),
        (1)
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, 'nearmap-technical-lead-with-react-warszawa-javascrip', '2026-07-31 23:59+02' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id, experience_level_id, experience_months)
    SELECT n.id, s.id, (SELECT id FROM experience_levels WHERE level='Intermediate'), 24
    FROM new_offer n
    JOIN skills s ON s.skill IN ('SQL','Excel','Statistics','R')
),
edj AS (
    INSERT INTO education_levels_junction (offer_id, education_level_id)
    SELECT n.id, el.id FROM new_offer n
    JOIN education_levels el ON el.level IN ('Bachelor')
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id, language_level_id)
    SELECT n.id, l.id, ll.id FROM new_offer n
    JOIN (VALUES ('Polish','c1'),('English','b2')) v(lang,lvl) ON TRUE
    JOIN languages l ON l.language=v.lang
    JOIN language_levels ll ON ll.level=v.lvl
),
scj AS (
    INSERT INTO sub_categories_junction (offer_id, sub_category_id)
    SELECT n.id, (1102) FROM new_offer n
)
SELECT id FROM new_offer;

-- 9) Electrical Engineer — Engineering / electrical-engineering
WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id, leading_category_id
    )
    VALUES (
        'Electrical Engineer',
        'Power systems design and commissioning.',
        13000, NULL, TRUE,
        NULL, TRUE,
        '2025-07-30 14:00+02', '2026-04-30 23:59+02', TRUE, FALSE,
        (SELECT id FROM sources WHERE name='Pracuj.pl'),
        (SELECT id FROM companies WHERE name='BlueWave Energy'),
        (SELECT id FROM location_details ORDER BY id LIMIT 1 OFFSET 7),
        (SELECT id FROM currencies WHERE currency='PLN'),
        (SELECT id FROM salary_periods WHERE period='monthly'),
        (3)
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, 'pracownik-magazynu-magnice-pow-wroclawski,oferta,1004311769?s=1f7c2c91&searchId=MTc1NjY3Mzg0MDI2NS4wNjI1', '2026-04-30 23:59+02' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id, experience_level_id, experience_months)
    SELECT n.id, s.id, (SELECT id FROM experience_levels WHERE level='Senior'), 48
    FROM new_offer n
    JOIN skills s ON s.skill IN ('Electrical Engineering','CAD','Project Management')
),
edj AS (
    INSERT INTO education_levels_junction (offer_id, education_level_id)
    SELECT n.id, el.id FROM new_offer n
    JOIN education_levels el ON el.level IN ('Master')
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id, language_level_id)
    SELECT n.id, l.id, ll.id FROM new_offer n
    JOIN (VALUES ('Polish','c1'),('English','b2')) v(lang,lvl) ON TRUE
    JOIN languages l ON l.language=v.lang
    JOIN language_levels ll ON ll.level=v.lvl
),
bj AS (
    INSERT INTO benefits_junction (offer_id, benefit_id)
    SELECT n.id, b.id FROM new_offer n
    JOIN benefits b ON b.benefit IN ('Private healthcare','Company car')
),
scj AS (
    INSERT INTO sub_categories_junction (offer_id, sub_category_id)
    SELECT n.id, (3103) FROM new_offer n
)
SELECT id FROM new_offer;

-- 10) Project Manager (R&D) — Engineering / project-management
WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id, leading_category_id
    )
    VALUES (
        'Project Manager (R&D)',
        'Lead cross-functional R&D projects.',
        17000, 23000, TRUE,
        TRUE, TRUE,
        '2025-08-02 15:00+02', '2026-10-31 23:59+01', FALSE, FALSE,
        (SELECT id FROM sources WHERE name='OlxPraca'),
        (SELECT id FROM companies WHERE name='AutoDrive Systems'),
        (SELECT id FROM location_details ORDER BY id LIMIT 1 OFFSET 8),
        (SELECT id FROM currencies WHERE currency='PLN'),
        (SELECT id FROM salary_periods WHERE period='monthly'),
        (3)
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, 'kwalifikowany-dowodca-zmiany-ochrony-widzew-CID4-ID16ZTYP.html', '2026-10-31 23:59+01' FROM new_offer
),
scj AS (
    INSERT INTO sub_categories_junction (offer_id, sub_category_id)
    SELECT n.id, (1104) FROM new_offer n
)
SELECT id FROM new_offer;

-- 11) Scrum Master — IT / project-management
WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id, leading_category_id
    )
    VALUES (
        'Scrum Master',
        'Coach teams and facilitate Scrum ceremonies.',
        NULL, NULL, NULL,
        TRUE, TRUE,
        '2025-08-06 08:00+02', '2026-01-31 23:59+01', FALSE, TRUE,
        (SELECT id FROM sources WHERE name='justjoin.it'),
        (SELECT id FROM companies WHERE name='Vistula Tech'),
        NULL,
        NULL, NULL,
        (1)
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, 'nearmap-technical-lead-with-react-warszawa-javascrip', '2026-01-31 23:59+01' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id, experience_level_id, experience_months)
    SELECT n.id, s.id, (SELECT id FROM experience_levels WHERE level='Intermediate'), 36
    FROM new_offer n
    JOIN skills s ON s.skill IN ('Scrum','Project Management')
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id, language_level_id)
    SELECT n.id, l.id, ll.id FROM new_offer n
    JOIN (VALUES ('English','b2'),('Polish','c1')) v(lang,lvl) ON TRUE
    JOIN languages l ON l.language=v.lang
    JOIN language_levels ll ON ll.level=v.lvl
),
scj AS (
    INSERT INTO sub_categories_junction (offer_id, sub_category_id)
    SELECT n.id, (1104) FROM new_offer n
)
SELECT id FROM new_offer;

-- 12) Cloud Engineer — IT / cloud
WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id, leading_category_id
    )
    VALUES (
        'Cloud Engineer (AWS/Azure)',
        'Build IaC and manage cloud workloads.',
        18000, 26000, TRUE,
        TRUE, FALSE,
        '2025-07-25 16:45+02', '2026-11-30 23:59+01', TRUE, FALSE,
        (SELECT id FROM sources WHERE name='justjoin.it'),
        (SELECT id FROM companies WHERE name='Vistula Tech'),
        (SELECT id FROM location_details ORDER BY id LIMIT 1 OFFSET 0),
        (SELECT id FROM currencies WHERE currency='PLN'),
        (SELECT id FROM salary_periods WHERE period='monthly'),
        (1)
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, 'nearmap-technical-lead-with-react-warszawa-javascrip', '2026-11-30 23:59+01' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id, experience_level_id, experience_months)
    SELECT n.id, s.id, (SELECT id FROM experience_levels WHERE level='Senior'), 48
    FROM new_offer n
    JOIN skills s ON s.skill IN ('AWS','Azure','Python','PostgreSQL')
),
edj AS (
    INSERT INTO education_levels_junction (offer_id, education_level_id)
    SELECT n.id, el.id FROM new_offer n
    JOIN education_levels el ON el.level IN ('Bachelor')
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id, language_level_id)
    SELECT n.id, (SELECT id FROM languages WHERE language='English'),
           (SELECT id FROM language_levels WHERE level='b2')
    FROM new_offer n
),
bj AS (
    INSERT INTO benefits_junction (offer_id, benefit_id)
    SELECT n.id, b.id FROM new_offer n
    JOIN benefits b ON b.benefit IN ('Private healthcare','Training budget','Stock options')
),
scj AS (
    INSERT INTO sub_categories_junction (offer_id, sub_category_id)
    SELECT n.id, (1103) FROM new_offer n
)
SELECT id FROM new_offer;

-- 13) Graphic Designer — Design / graphic-design
WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id, leading_category_id
    )
    VALUES (
        'Graphic Designer',
        'Branding and marketing materials.',
        7000, 10000, TRUE,
        TRUE, TRUE,
        '2025-08-07 13:10+02', '2026-03-31 23:59+02', FALSE, FALSE,
        (SELECT id FROM sources WHERE name='OlxPraca'),
        (SELECT id FROM companies WHERE name='ArtVision Studio'),
        (SELECT id FROM location_details ORDER BY id LIMIT 1 OFFSET 2),
        (SELECT id FROM currencies WHERE currency='PLN'),
        (SELECT id FROM salary_periods WHERE period='monthly'),
        (6)
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, 'kwalifikowany-dowodca-zmiany-ochrony-widzew-CID4-ID16ZTYP.html', '2026-03-31 23:59+02' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id, experience_level_id, experience_months)
    SELECT n.id, s.id, (SELECT id FROM experience_levels WHERE level='Intermediate'), 24
    FROM new_offer n
    JOIN skills s ON s.skill IN ('Photoshop','Illustrator')
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id, language_level_id)
    SELECT n.id, (SELECT id FROM languages WHERE language='Polish'),
           (SELECT id FROM language_levels WHERE level='c1')
    FROM new_offer n
),
etj AS (
    INSERT INTO employment_types_junction (offer_id, employment_type_id)
    SELECT n.id, t.id FROM new_offer n
    JOIN employment_types t ON t.type IN ('Contract for specific work')
),
esj AS (
    INSERT INTO employment_schedules_junction (offer_id, employment_schedule_id)
    SELECT n.id, s.id FROM new_offer n
    JOIN employment_schedules s ON s.schedule IN ('Freelance')
),
scj AS (
    INSERT INTO sub_categories_junction (offer_id, sub_category_id)
    SELECT n.id, (6101) FROM new_offer n
)
SELECT id FROM new_offer;

-- 14) Biostatistician — Research / statistics
WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id, leading_category_id
    )
    VALUES (
        'Biostatistician',
        'Statistical analysis in clinical trials.',
        16000, 22000, TRUE,
        TRUE, NULL,
        '2025-07-18 10:00+02', '2026-09-30 23:59+02', FALSE, TRUE,
        (SELECT id FROM sources WHERE name='Jooble'),
        (SELECT id FROM companies WHERE name='Ocean Research Institute'),
        NULL,
        (SELECT id FROM currencies WHERE currency='PLN'),
        (SELECT id FROM salary_periods WHERE period='monthly'),
        (7)
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, '-5483726541865494826?ckey=asd&rgn=-1&pos=1&elckey=915678177178058315&pageType=20&p=1&sid=1844751898038482488&jobAge=45&relb=100&brelb=100&bscr=19999.800001641575&scr=19999.800001641575&searchTestGroup=1_1342_1&iid=-8222011219895262805', '2026-09-30 23:59+02' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id, experience_level_id, experience_months)
    SELECT n.id, (SELECT id FROM skills WHERE skill='Statistics'),
           (SELECT id FROM experience_levels WHERE level='Intermediate'), 36
    FROM new_offer n
),
edj AS (
    INSERT INTO education_levels_junction (offer_id, education_level_id)
    SELECT n.id, (SELECT id FROM education_levels WHERE level='Master') FROM new_offer n
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id, language_level_id)
    SELECT n.id, (SELECT id FROM languages WHERE language='English'),
           (SELECT id FROM language_levels WHERE level='b2')
    FROM new_offer n
),
scj AS (
    INSERT INTO sub_categories_junction (offer_id, sub_category_id)
    SELECT n.id, (7101) FROM new_offer n
)
SELECT id FROM new_offer;

-- 15) Auditor — Finance / auditing
WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id, leading_category_id
    )
    VALUES (
        'Auditor',
        'Plan audits and evaluate internal controls.',
        11000, 15000, TRUE,
        FALSE, TRUE,
        '2025-08-09 09:20+02', '2026-08-31 23:59+02', FALSE, FALSE,
        (SELECT id FROM sources WHERE name='Pracuj.pl'),
        (SELECT id FROM companies WHERE name='Nord Finance'),
        (SELECT id FROM location_details ORDER BY id LIMIT 1 OFFSET 1),
        (SELECT id FROM currencies WHERE currency='PLN'),
        (SELECT id FROM salary_periods WHERE period='monthly'),
        (2)
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, 'pracownik-magazynu-magnice-pow-wroclawski,oferta,1004311769?s=1f7c2c91&searchId=MTc1NjY3Mzg0MDI2NS4wNjI1', '2026-08-31 23:59+02' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id, experience_level_id, experience_months)
    SELECT n.id, s.id, (SELECT id FROM experience_levels WHERE level='Intermediate'), 24
    FROM new_offer n
    JOIN skills s ON s.skill IN ('Auditing','Excel')
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id, language_level_id)
    SELECT n.id, l.id, ll.id FROM new_offer n
    JOIN (VALUES ('Polish','c1'),('English','b2')) v(lang,lvl) ON TRUE
    JOIN languages l ON l.language=v.lang
    JOIN language_levels ll ON ll.level=v.lvl
),
edj AS (
    INSERT INTO education_levels_junction (offer_id, education_level_id)
    SELECT n.id, el.id FROM new_offer n
    JOIN education_levels el ON el.level IN ('Bachelor')
),
scj AS (
    INSERT INTO sub_categories_junction (offer_id, sub_category_id)
    SELECT n.id, (2102) FROM new_offer n
)
SELECT id FROM new_offer;

-- 16) Tax Lawyer — Legal / tax-law
WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id, leading_category_id
    )
    VALUES (
        'Tax Lawyer',
        'Corporate tax advisory and litigation support.',
        18000, 28000, TRUE,
        TRUE, TRUE,
        '2025-08-11 10:40+02', '2026-12-31 23:59+01', TRUE, FALSE,
        (SELECT id FROM sources WHERE name='Pracuj.pl'),
        (SELECT id FROM companies WHERE name='Lex & Partners'),
        (SELECT id FROM location_details ORDER BY id LIMIT 1 OFFSET 0),
        (SELECT id FROM currencies WHERE currency='PLN'),
        (SELECT id FROM salary_periods WHERE period='monthly'),
        (8)
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, 'pracownik-magazynu-magnice-pow-wroclawski,oferta,1004311769?s=1f7c2c91&searchId=MTc1NjY3Mzg0MDI2NS4wNjI1', '2026-12-31 23:59+01' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id, experience_level_id, experience_months)
    SELECT n.id, s.id, (SELECT id FROM experience_levels WHERE level='Lead'), 60
    FROM new_offer n
    JOIN skills s ON s.skill IN ('Tax Law','Project Management')
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id, language_level_id)
    SELECT n.id, l.id, ll.id FROM new_offer n
    JOIN (VALUES ('Polish','c1'),('English','b2'),('German','b1')) v(lang,lvl) ON TRUE
    JOIN languages l ON l.language=v.lang
    JOIN language_levels ll ON ll.level=v.lvl
),
edj AS (
    INSERT INTO education_levels_junction (offer_id, education_level_id)
    SELECT n.id, el.id FROM new_offer n
    JOIN education_levels el ON el.level IN ('Master')
),
scj AS (
    INSERT INTO sub_categories_junction (offer_id, sub_category_id)
    SELECT n.id, 2103 FROM new_offer n
)
SELECT id FROM new_offer;

-- 17) Econometrician — Finance / econometrics
WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id, leading_category_id
    )
    VALUES (
        'Econometrician',
        'Model building and forecasting for risk.',
        40000, 60000, TRUE,
        TRUE, NULL,
        '2025-07-27 12:00+02', '2026-07-31 23:59+02', FALSE, TRUE,
        (SELECT id FROM sources WHERE name='Jooble'),
        (SELECT id FROM companies WHERE name='Nord Finance'),
        NULL,
        (SELECT id FROM currencies WHERE currency='USD'),
        (SELECT id FROM salary_periods WHERE period='yearly'),
        (2)
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, '-5483726541865494826?ckey=asd&rgn=-1&pos=1&elckey=915678177178058315&pageType=20&p=1&sid=1844751898038482488&jobAge=45&relb=100&brelb=100&bscr=19999.800001641575&scr=19999.800001641575&searchTestGroup=1_1342_1&iid=-8222011219895262805', '2026-07-31 23:59+02' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id, experience_level_id, experience_months)
    SELECT n.id, s.id, (SELECT id FROM experience_levels WHERE level='Intermediate'), 36
    FROM new_offer n
    JOIN skills s ON s.skill IN ('Econometrics','R','SQL')
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id, language_level_id)
    SELECT n.id, (SELECT id FROM languages WHERE language='English'),
           (SELECT id FROM language_levels WHERE level='b2')
    FROM new_offer n
),
scj AS (
    INSERT INTO sub_categories_junction (offer_id, sub_category_id)
    SELECT n.id, (2104) FROM new_offer n
)
SELECT id FROM new_offer;

-- 18) Marine Biologist — Marine / marine-biology
WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id, leading_category_id
    )
    VALUES (
        'Marine Biologist',
        'Field research aboard research vessels.',
        NULL, NULL, NULL,
        FALSE, NULL,
        '2025-08-03 08:50+02', '2026-06-30 23:59+02', FALSE, FALSE,
        (SELECT id FROM sources WHERE name='Jooble'),
        (SELECT id FROM companies WHERE name='Ocean Research Institute'),
        (SELECT id FROM location_details ORDER BY id LIMIT 1 OFFSET 10),
        NULL, NULL,
        (12)
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, '-5483726541865494826?ckey=asd&rgn=-1&pos=1&elckey=915678177178058315&pageType=20&p=1&sid=1844751898038482488&jobAge=45&relb=100&brelb=100&bscr=19999.800001641575&scr=19999.800001641575&searchTestGroup=1_1342_1&iid=-8222011219895262805', '2026-06-30 23:59+02' FROM new_offer
),
scj AS (
    INSERT INTO sub_categories_junction (offer_id, sub_category_id)
    SELECT n.id, (12001) FROM new_offer n
)
SELECT id FROM new_offer;

-- 19) Marketing Analyst — Marketing / marketing-analytics
WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id, leading_category_id
    )
    VALUES (
        'Marketing Analyst',
        'Analyze campaign performance and market trends.',
        250, 350, FALSE,
        TRUE, TRUE,
        '2025-08-14 14:30+02', '2026-05-31 23:59+02', TRUE, TRUE,
        (SELECT id FROM sources WHERE name='OlxPraca'),
        (SELECT id FROM companies WHERE name='FoodTech Polska'),
        (SELECT id FROM location_details ORDER BY id LIMIT 1 OFFSET 2),
        (SELECT id FROM currencies WHERE currency='EUR'),
        (SELECT id FROM salary_periods WHERE period='daily'),
        (9)
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, 'kwalifikowany-dowodca-zmiany-ochrony-widzew-CID4-ID16ZTYP.html', '2026-05-31 23:59+02' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id, experience_level_id, experience_months)
    SELECT n.id, s.id, (SELECT id FROM experience_levels WHERE level='Intermediate'), 24
    FROM new_offer n
    JOIN skills s ON s.skill IN ('SQL','Excel','Statistics')
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id, language_level_id)
    SELECT n.id, l.id, ll.id FROM new_offer n
    JOIN (VALUES ('English','b2'),('Polish','c1'),('French','b1')) v(lang,lvl) ON TRUE
    JOIN languages l ON l.language=v.lang
    JOIN language_levels ll ON ll.level=v.lvl
),
scj AS (
    INSERT INTO sub_categories_junction (offer_id, sub_category_id)
    SELECT n.id, (9101) FROM new_offer n
)
SELECT id FROM new_offer;

-- 20) Veterinarian — Veterinary / veterinary
WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id, leading_category_id
    )
    VALUES (
        'Veterinarian',
        'Small animal practice with surgery rotations.',
        60, 90, TRUE,
        FALSE, NULL,
        '2025-08-19 09:10+02', '2026-11-30 23:59+01', FALSE, TRUE,
        (SELECT id FROM sources WHERE name='Pracuj.pl'),
        (SELECT id FROM companies WHERE name='VetCare Center'),
        (SELECT id FROM location_details ORDER BY id LIMIT 1 OFFSET 11),
        (SELECT id FROM currencies WHERE currency='PLN'),
        (SELECT id FROM salary_periods WHERE period='hourly'),
        (10)
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, 'pracownik-magazynu-magnice-pow-wroclawski,oferta,1004311769?s=1f7c2c91&searchId=MTc1NjY3Mzg0MDI2NS4wNjI1', '2026-11-30 23:59+01' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id, experience_level_id, experience_months)
    SELECT n.id, s.id, (SELECT id FROM experience_levels WHERE level='Junior'), 12
    FROM new_offer n
    JOIN skills s ON s.skill IN ('Clinical Trials','Project Management')
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id, language_level_id)
    SELECT n.id, l.id, ll.id FROM new_offer n
    JOIN (VALUES ('Polish','c1'),('Ukrainian','b2')) v(lang,lvl) ON TRUE
    JOIN languages l ON l.language=v.lang
    JOIN language_levels ll ON ll.level=v.lvl
),
scj AS (
    INSERT INTO sub_categories_junction (offer_id, sub_category_id)
    SELECT n.id, (10001) FROM new_offer n
)
SELECT id FROM new_offer;

-- 21) Embedded Systems Engineer — Engineering / embedded-systems
WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id, leading_category_id
    )
    VALUES (
        'Embedded Systems Engineer',
        'Develop firmware for automotive controllers.',
        16000, 23000, TRUE,
        NULL, TRUE,
        '2025-08-20 11:20+02', '2026-09-30 23:59+02', TRUE, FALSE,
        (SELECT id FROM sources WHERE name='justjoin.it'),
        (SELECT id FROM companies WHERE name='AutoDrive Systems'),
        (SELECT id FROM location_details ORDER BY id LIMIT 1 OFFSET 8),
        (SELECT id FROM currencies WHERE currency='PLN'),
        (SELECT id FROM salary_periods WHERE period='monthly'),
        (3)
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, 'nearmap-technical-lead-with-react-warszawa-javascrip', '2026-09-30 23:59+02' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id, experience_level_id, experience_months)
    SELECT n.id, s.id, (SELECT id FROM experience_levels WHERE level='Intermediate'), 36
    FROM new_offer n
    JOIN skills s ON s.skill IN ('Microcontrollers','C','Project Management')
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id, language_level_id)
    SELECT n.id, l.id, ll.id FROM new_offer n
    JOIN (VALUES ('English','b2'),('Polish','c1')) v(lang,lvl) ON TRUE
    JOIN languages l ON l.language=v.lang
    JOIN language_levels ll ON ll.level=v.lvl
),
scj AS (
    INSERT INTO sub_categories_junction (offer_id, sub_category_id)
    SELECT n.id, (3104) FROM new_offer n
)
SELECT id FROM new_offer;

-- 22) Aerospace Engineer — Aerospace / aerospace-engineering
WITH new_offer AS (
    INSERT INTO offers (
        job_title, description, salary_from, salary_to, is_gross,
        is_remote, is_hybrid, published, expires,
        is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id,
        currency_id, salary_period_id, leading_category_id
    )
    VALUES (
        'Aerospace Engineer',
        NULL,
        17000, 26000, TRUE,
        FALSE, TRUE,
        '2025-08-21 10:00+02', '2026-08-31 23:59+02', FALSE, FALSE,
        (SELECT id FROM sources WHERE name='Pracuj.pl'),
        (SELECT id FROM companies WHERE name='AeroPol'),
        (SELECT id FROM location_details ORDER BY id LIMIT 1 OFFSET 9),
        (SELECT id FROM currencies WHERE currency='PLN'),
        (SELECT id FROM salary_periods WHERE period='monthly'),
        (11)
    )
    RETURNING id
),
eo AS (
    INSERT INTO external_offers (offer_id, query_string, offer_lifespan_expiration)
    SELECT id, 'pracownik-magazynu-magnice-pow-wroclawski,oferta,1004311769?s=1f7c2c91&searchId=MTc1NjY3Mzg0MDI2NS4wNjI1', '2026-08-31 23:59+02' FROM new_offer
),
sj AS (
    INSERT INTO skills_junction (offer_id, skill_id, experience_level_id, experience_months)
    SELECT n.id, s.id, (SELECT id FROM experience_levels WHERE level='Senior'), 48
    FROM new_offer n
    JOIN skills s ON s.skill IN ('MATLAB','Project Management')
),
lj AS (
    INSERT INTO languages_junction (offer_id, language_id, language_level_id)
    SELECT n.id, (SELECT id FROM languages WHERE language='English'),
           (SELECT id FROM language_levels WHERE level='b2')
    FROM new_offer n
),
edj AS (
    INSERT INTO education_levels_junction (offer_id, education_level_id)
    SELECT n.id, el.id FROM new_offer n
    JOIN education_levels el ON el.level IN ('Bachelor','Master')
),
scj AS (
    INSERT INTO sub_categories_junction (offer_id, sub_category_id)
    SELECT n.id, (11001) FROM new_offer n 
)
SELECT id FROM new_offer;
