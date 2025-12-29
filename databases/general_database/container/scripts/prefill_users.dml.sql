INSERT INTO public.roles (id, role_name)
OVERRIDING SYSTEM VALUE
VALUES (1, 'User');

COPY public.first_names (id, first_name) FROM stdin;
1	User01
2	User02
3	User03
4	User04
5	User05
6	User06
7	User07
8	User08
9	User09
10	User10
\.

COPY public.last_names (id, last_name) FROM stdin;
1	Test
\.

COPY public.second_names (id, second_name) FROM stdin;
1	Jan
2	Anna
3	Maria
4	Piotr
5	Kamil
6	Zofia
\.

COPY public.users (id, email, password, remember_token, phone, first_name_id, second_name_id, last_name_id, role_id) FROM stdin;
8	user08@a.pl	argon2id$v=1$6ptixpI8+jlOiHhexwThAA==$weLC2dOtOzoBE+RLw/YM6kIsnR7tAr4yzeZsg82+Qv8=	\N	\N	8	\N	1	1
1	user01@a.pl	argon2id$v=1$jUUDMJpp3jkKcSp//5zMJQ==$5B5s0GbWNGwfsZsh8/hplEoFVYquipVUER2sr9mpqF0=	ycIzHF7xMtUxT/hjerIMqEA68ewtWNYm82DK6XUOTJQ=	+48111111111	1	1	1	1
2	user02@a.pl	argon2id$v=1$aX8IK1E98EumZbgBekmFXg==$4ngKj5vq33hQlNnXBSxzqxGiY9O55oxdHwp9hmC/cZo=	Svc2bgFSruwCWULJchY6nc1AEvtyUSRs1eiqXVciWPY=	\N	2	2	1	1
3	user03@a.pl	argon2id$v=1$c7j62pPfrZC/xig3xapm4A==$RkVJpJgavKVwe3I/XsYurkDu4Z3tbU1S0fPSWmF17EI=	\N	+48222222222	3	\N	1	1
4	user04@a.pl	argon2id$v=1$O04zfLP0ZADL/VXXf+TLJg==$pMMYH8EWL8+lkfHCrxNlbmxbG0dQtScsz/udTp4oN8Y=	\N	+48333333333	4	3	1	1
5	user05@a.pl	argon2id$v=1$zLV+vVFaQR32XM7wMjLp/Q==$kKOoH6J5awR0PMtTo4KC3PVAnTCd22OdjCdOSCjntbE=	\N	\N	5	4	1	1
6	user06@a.pl	argon2id$v=1$RxVzLnOLZI2xwaImjsJOBQ==$K5v+K8Tk2UUzKA6nbOMpHUpNlJjV7/AqkmasvaibFec=	\N	+48444444444	6	\N	1	1
7	user07@a.pl	argon2id$v=1$Cu1QdIJFV3xV234GXKWrxA==$1Qc/5ITN5qmWtQjWMfhFXvSNwkKLeP/H4Be2xONiNQw=	\N	+48555555555	7	5	1	1
9	user09@a.pl	argon2id$v=1$YrsQVTMKkBtdnLYgIIXmKQ==$AxpJvHDptpw4bGRcu8MotEz9ydKaVVokU6IysjN5wuY=	5Rf2X3LIpx4EkWH6tOwQ3ujwPC29mWZi/olxfnKwYVg=	\N	9	6	1	1
10	user10@a.pl	argon2id$v=1$wcTBF2hUmsMfngUQqrmXsw==$A99pLakTgZWgfEXn/t/QaQwIxaj5HBCIYFo3L7Q09Lg=	n20g+/kTEnLeLu099RHIaLWAmC4RjWH8saefqQPOS04=	+48666666666	10	\N	1	1
\.

COPY public.search_histories (id, keywords, distance, is_remote, is_hybrid, leading_category_id, city_id, search_date, user_id, salary_from, salary_to, salary_period_id, salary_currency_id, employment_schedule_ids, employment_type_ids) FROM stdin;
1	Programista Python Junior	15	t	f	18	1	2025-12-29 17:46:41.538387+01	1	6000.00	10000.00	4	1	{1,9}	{1,3}
2	Data Scientist Python	30	t	t	8	1	2025-12-29 17:46:50.058816+01	1	12000.00	20000.00	4	1	{1}	{5}
3	Backend Developer Django	20	f	t	18	1	2025-12-29 17:46:54.666935+01	1	10000.00	16000.00	4	1	{1}	{1,5}
4	Cybersecurity Analyst	25	t	f	7	1	2025-12-29 17:47:07.20613+01	1	11000.00	18000.00	4	1	{1}	{1}
5	Inżynier AI ML	50	t	t	35	1	2025-12-29 17:47:19.888854+01	1	14000.00	24000.00	4	1	{1,10}	{5}
6	DevOps AWS	40	t	f	40	1	2025-12-29 17:47:27.388699+01	1	13000.00	22000.00	4	4	{1}	{5}
7	Programista C# .NET Junior	10	f	t	18	2	2025-12-29 17:48:03.193986+01	3	7000.00	11000.00	4	1	{1}	{1,5}
8	Frontend React Junior	15	t	t	18	3	2025-12-29 17:48:38.073247+01	4	6500.00	10500.00	4	1	{1,9}	{1,3}
9	UX UI Designer	20	f	t	2	3	2025-12-29 17:48:41.179352+01	4	6000.00	12000.00	4	1	{9}	{3,4}
10	Staż IT helpdesk	5	f	f	9	4	2025-12-29 17:49:10.14686+01	5	3500.00	5000.00	4	1	{5}	{6,8}
11	Tester manualny	20	t	f	18	4	2025-12-29 17:49:13.001932+01	5	5500.00	9000.00	4	1	{1}	{1,3}
12	Automatyzacja testów Selenium	30	t	t	3	4	2025-12-29 17:49:17.743716+01	5	9000.00	14000.00	4	1	{1}	{5}
13	Inżynier danych ETL	25	t	t	8	5	2025-12-29 17:50:01.38515+01	6	11000.00	18000.00	4	1	{1}	{5}
14	Administrator Linux	30	f	t	40	5	2025-12-29 17:50:04.051856+01	6	9000.00	15000.00	4	1	{1,2}	{1}
15	Programista Java Mid	50	t	f	18	5	2025-12-29 17:50:08.388258+01	6	12000.00	19000.00	4	1	{1}	{5}
16	Programista embedded C	35	f	f	12	5	2025-12-29 17:50:12.380563+01	6	10000.00	17000.00	4	3	{1}	{1}
17	Analityk biznesowy IT	20	t	t	24	6	2025-12-29 17:50:41.347483+01	7	9000.00	16000.00	4	1	{1,9}	{1,5}
18	Scrum Master	30	t	t	44	6	2025-12-29 17:50:45.436655+01	7	12000.00	20000.00	4	2	{1}	{5}
19	Project Manager IT	40	f	t	24	6	2025-12-29 17:50:50.871396+01	7	11000.00	19000.00	4	1	{1}	{1}
20	QA Lead	25	t	f	18	6	2025-12-29 17:50:56.809887+01	7	13000.00	21000.00	4	1	{1}	{5}
21	Programista C++	35	f	f	31	6	2025-12-29 17:51:10.240583+01	7	10000.00	18000.00	4	1	{1}	{1}
22	Programista Python Staż	10	t	f	18	7	2025-12-29 17:51:31.168711+01	8	3000.00	4500.00	4	1	{5}	{6,8}
23	Data Science praktyki	20	t	t	8	7	2025-12-29 17:51:35.733146+01	8	3500.00	5500.00	4	1	{5,9}	{8}
24	Cyberbezpieczeństwo Junior	30	t	f	7	8	2025-12-29 17:52:04.350766+01	9	8000.00	14000.00	4	1	{1}	{1,5}
25	Inżynier Biomedyczny	25	f	t	20	9	2025-12-29 17:52:24.635716+01	10	7000.00	13000.00	4	1	{1}	{1}
26	Informatyka - staż	10	t	f	18	9	2025-12-29 17:52:29.236794+01	10	3500.00	5500.00	4	1	{5}	{6,8}
27	Teleinformatyka praktyki	15	t	t	40	9	2025-12-29 17:52:34.758211+01	10	4000.00	6500.00	4	1	{5,9}	{8}
\.

COPY public.weights (user_id, order_by_option, mean_value_ids, vector, mean_dist, means_value_sum, means_value_ssum, means_value_count, means_weight_sum, means_weight_ssum, means_weight_count) FROM stdin;
1	{""}	{""}	{0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5}	{0.5}	{0}	{0}	{0}	{0}	{0}	{0}
2	{""}	{""}	{0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5}	{0.5}	{0}	{0}	{0}	{0}	{0}	{0}
3	{""}	{""}	{0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5}	{0.5}	{0}	{0}	{0}	{0}	{0}	{0}
4	{""}	{""}	{0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5}	{0.5}	{0}	{0}	{0}	{0}	{0}	{0}
5	{""}	{""}	{0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5}	{0.5}	{0}	{0}	{0}	{0}	{0}	{0}
6	{""}	{""}	{0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5}	{0.5}	{0}	{0}	{0}	{0}	{0}	{0}
7	{""}	{""}	{0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5}	{0.5}	{0}	{0}	{0}	{0}	{0}	{0}
8	{""}	{""}	{0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5}	{0.5}	{0}	{0}	{0}	{0}	{0}	{0}
9	{""}	{""}	{0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5}	{0.5}	{0}	{0}	{0}	{0}	{0}	{0}
10	{""}	{""}	{0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5}	{0.5}	{0}	{0}	{0}	{0}	{0}	{0}
\.

SELECT setval(pg_get_serial_sequence('public.search_histories','id'),
              (SELECT COALESCE(MAX(id),0) FROM public.search_histories), true);

SELECT setval(pg_get_serial_sequence('public.users','id'),
              (SELECT COALESCE(MAX(id),0) FROM public.users), true);

SELECT setval(pg_get_serial_sequence('public.roles','id'),
              (SELECT COALESCE(MAX(id),0) FROM public.roles), true);

SELECT setval(pg_get_serial_sequence('public.first_names','id'),
              (SELECT COALESCE(MAX(id),0) FROM public.first_names), true);

SELECT setval(pg_get_serial_sequence('public.last_names','id'),
              (SELECT COALESCE(MAX(id),0) FROM public.last_names), true);

SELECT setval(pg_get_serial_sequence('public.second_names','id'),
              (SELECT COALESCE(MAX(id),0) FROM public.second_names), true);
