SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;


COPY public.benefits (id, benefit) FROM stdin;
1	Dofinansowanie zajęć sportowych
2	Prywatna opieka medyczna
3	Ubezpieczenie na życie
4	Możliwość pracy zdalnej
5	Elastyczny czas pracy
6	Produkty i usługi firmowe w obniżonych cenach
7	Imprezy integracyjne
8	Brak dress code'u
9	Gry wideo w pracy
10	Kawa / herbata
11	Napoje
12	Miejsce parkingowe dla pracowników
13	Strefa relaksu
20	Owoce
24	Program poleceń pracowniczych
25	dofinansowanie zajęć sportowych
26	prywatna opieka medyczna
27	dofinansowanie nauki języków
28	dofinansowanie szkoleń i kursów
29	ubezpieczenie na życie
30	owoce
31	spotkania integracyjne
32	brak dress code’u
33	kawa / herbata
34	napoje
35	parking dla pracowników
36	program rekomendacji pracowników
37	inicjatywy dobroczynne
38	przyjazna atmosfera
44	elastyczny czas pracy
47	opieka stomatologiczna
48	firmowa drużyna sportowa
49	program emerytalny
50	preferencyjne pożyczki
51	strefa relaksu
52	dodatkowe świadczenia socjalne
53	dofinansowanie wypoczynku
54	wyprawka dla dziecka
55	paczki świąteczne
58	dodatkowy urlop
59	platforma e-learningowa
60	aplikacja do nauki języków eTutor
61	parking dla rowerów
62	platforma kafeteryjna
68	możliwość pracy zdalnej
71	ubezpieczenie szpitalne
72	program wellbeingowy
84	Zniżki na firmowe produkty i usługi
85	Spotkania integracyjne
86	Preferencyjne pożyczki
87	Brak dress code’u
90	Parking dla pracowników
98	dofinansowanie wakacji dzieci
101	pikniki rodzinne
102	Przyczepy kempingowe (Włochy,Chorwacja)
105	Dofinansowanie szkoleń i kursów
108	Firmowa drużyna sportowa
109	Program emerytalny
110	Dodatkowe świadczenia socjalne
111	Dzień wolny z okazji Dnia Energetyka
112	System premiowy
113	Dofinansowanie posiłków
114	Bonusy świąteczne
124	Firmowa biblioteka
129	Paczki świąteczne
130	Inicjatywy dobroczynne
140	zniżki na firmowe produkty i usługi
147	Zniżka na usługi terapeutyczne naszej fundacji
148	Karta paliwowa - zniżki
155	firmowa biblioteka
173	SmartLunch
176	Dofinansowanie nauki języków
192	fundusz oszczędnościowo-inwestycyjny
195	karty przedpłacone
197	możliwość uzyskania uprawnień
225	karta sportowa
237	dofinansowanie biletów do kina, teatru
249	Pikniki rodzinne
250	Platforma Wellbee
251	Pakiet Medicover Sport
264	fundusz socjalny
269	Dofinansowanie do okularów
270	Korzystanie z obiektu
271	dofinansowanie do obiadów pracowniczych 40% zniżki
275	Dofinansowanie wypoczynku
287	gry wideo w pracy
298	Dofinansowanie kursów języków obcych
299	Dofinansowanie szkoleń i kursów zawodowych
302	Produkty i usługi firmowe po obniżonych cenach
304	Pracowniczy program emerytalny
305	Biblioteka firmowa
308	Fundusz świąteczny
309	Prezenty świąteczne
405	Dofinansowanie biletów do kina, teatru
407	Możliwość rozwoju zawodowego
408	Dofinansowanie do zakupu okularów korekcyjnych
611	Świąteczne prezenty
765	Program rekomendacji pracowników
1047	dzień wolny z okazji Dnia Energetyka
1048	system premiowy
1049	dofinansowanie posiłków
1050	bonusy świąteczne
1213	Fundusz socjalny
1257	Świadczenia urlopowe
1421	Dofinansowanie wakacji dzieci
1500	Dofinansowanie usług turystycznych
1503	Inicjatywy charytatywne
1570	Dodatek stażowy
1571	Nagrody jubileuszowe
1572	Wydarzenia integracyjne
1634	Produkty firmowe w niższych cenach
1636	Plan emerytalny
1653	Ubezpieczenie szpitalne
1654	Platforma do nauki języków obcych
1662	Komputer do użytku prywatnego
1678	bony żywieniowe
1693	Przedszkole / żłobek pracowniczy
1702	System Kafeteryjny Worksmile
1712	szkolenia i mentoring
1713	imprezy integracyjne
1714	premie roczne
1715	dofinasowanie posiłków
1724	Karta Lunchpass
1740	własne centrum sportowe
1749	sharing the costs of sports activities
1750	private medical care
1751	sharing the costs of foreign language classes
1752	sharing the costs of professional training &amp; courses
1753	life insurance
1754	fruits
1755	corporate products and services at discounted prices
1756	integration events
1757	retirement pension plan
1758	corporate library
1759	no dress code
1760	coffee / tea
1761	holiday funds
1762	christmas gifts
1763	employee referral program
1798	Możliwość uzyskania uprawnień
1800	Jeden krótszy dzień pracy
1811	Program wellbeingowy
1820	Opieka stomatologiczna
1821	Korporacyjna drużyna sportowa
1827	Bony żywieniowe
1828	Uroczystości urodzinowe
1832	Dodatkowy urlop
1877	zajęcia sportowe
1894	świętowanie urodzin
1896	Platforma kafeteryjna Worksmile
1921	ryczałt na paliwo
1927	trener kompetencji technicznych wewnątrz firmy
1928	Spotkania z HR Business Partnerem
1929	Wspólny obiad firmowy w każdą środę
1939	służbowy telefon do użytku prywatnego
1974	Dodatkowy dzień wolny z okazji Dnia Energetyka - 14 sierpnia
2018	możliwość korzystania ze szkoleń i kursów
2019	Kasa zapomogowo-pożyczkowa
2020	Porady prawne
2034	Karta lunchowa
2044	komputer do użytku prywatnego
2055	stacje ładowania samochodów
2132	Parking rowerowy
2133	Platforma wellbeingowa
2147	dofinansowanie usług turystycznych
2215	system kafeteryjny
2216	lekcje języka angielskiego w godzinach pracy
2217	dofinansowanie do opieki medycznej
2236	dzień wolny z okazji Twoich urodzin
2237	dzień wolny dla rodziców
2302	dodatek stażowy
2303	13 pensja
2324	wyprawka szkolna
2326	regularne badania profilaktyczne
2327	e-kody na zakupy w Biedronce i Hebe
2339	siłownia w biurze
2341	dyżury lekarza w biurze
2350	dofinansowanie do prywatnej edukacji dzieci
2366	zniżki na zakup sprzętu w naszych elektromarketach
2367	opieka fundacji MediaExpert „Włącz się”
2368	premia świąteczna
2369	nagrody jubileuszowe
2370	Baby Expert - wyprawka dla Twojego nowonarodzonego dziecka
2442	bonus świąteczny
2465	dodatkowy dzień wolny od pracy - 25.11. Święto Kolejarza
2466	zniżki na przejazdy koleją
2467	Dofinansowanie do okularów korekcyjnych
2501	pakiet multisport
2547	zniżka na przejazdy kolejowe
2548	dodatkowy dzień wolny w roku - 25.11. Dzień Kolejarza
2570	karta MultiSport lub system kafeteryjny
2571	piłkarzyki
2572	pyszna kawa z polskich palarni
2573	przekąski oraz inne zaopatrzenie
2574	parking na rowery, prysznic
2598	Platforma kafeteryjna - NAIS
2599	Dzień na U - dodatkowy dzień wolny na zadbanie o zdrowie
2767	dostęp do internetowej poradni
2806	Platforma kafeteryjna
2834	Karta Multisport
2835	Parking dla rowerów
2837	Zajęcia z j. angielskiego
2855	Platforma zakupowa Medicover Benefits
2894	pakiet relokacyjny
2897	dostęp do kursów np. Excel, VBA, RPA, Obsługa klienta
2898	nieograniczony dostęp do Udemy Business
2899	bezpłatny czat/rozmowa z terapeutą
2926	Premia
2927	Dofinansowanie do zajęć sportowych
2928	Bilety do kina
2929	Bilety do teatru
2933	Wakacje dzieci
2936	Dofinansowanie do prywatnej opieki zdrowotnej
2937	Pakiet sportowy
2938	Ubezpieczenie grupowe na preferencyjnych warunkach
2939	Zniżki w systemie kafeteryjnym
2940	Dofinansowanie do żłobka/przedszkola
2941	Świadczenia okolicznościowe
2942	Zniżki pracownicze na części i akcesoria samochodowe
2944	Dofinansowanie do karnetu sportowego
2945	Pakiet socjalny
2946	Ubezpieczenie grupowe
2947	Zniżki pracownicze
2949	Zniżki na produkty Grupy Express
2950	Karta paliwowa
2951	Transport pracowniczy
2952	Obiad pracowniczy
2953	Odzież robocza
2954	Pranie odzieży roboczej
2955	Zniżki na produkty
2956	Wsparcie i wdrożenie
2958	Karta Multi Sport
2961	Przyjazna atmosfera w pracy
2962	Opieka medyczna
2963	Karta sportowa
2964	Ubezpieczenie
2968	Dodatki świąteczne
2969	Możliwość rozwoju
2971	Ubezpieczenie zdrowotne
2974	Premia retencyjna
2975	Bonus bożonarodzeniowy
2976	Wczasy pod gruszą
2977	Prezent urodzinowy
2979	Grupowe ubezpieczenie zdrowotne
2980	Darmowa kawa
2981	Darmowa herbata
2982	Darmowa woda
2983	Owocowe czwartki
2984	Premia za polecenie znajomego
2988	Zniżki na zakupy w Mango
2989	Świadczenia socjalne
2993	Możliwość zrobienia kursu na wózki widłowe
2994	Stabilne warunki zatrudnienia
2995	Atrakcyjne wynagrodzenie
2998	System wdrożenia pracownika
2999	Prywatna opieka medyczna Medicover
3000	Pakiet sportowy Medicover Sport
3003	Bonus Bożonarodzeniowy
3006	Atrakcyjne premie uzależnione od stażu pracy
3007	Program premiowy za frekwencję
3008	Obsługa administracyjna on-line
3009	Pre-pensja od Patento
3011	Konkursy z dodatkową premią
3012	Karta MultiSport z dofinansowaniem
3013	Prywatna opieka medyczna Medicover z dofinansowaniem
3014	Stołówka pracownicza i obiady z dofinansowaniem
3016	Bezpłatna kawa i herbata z automatu
3017	Wyprawka dla nowonarodzonego dziecka
3019	Zniżki na zakup sprzętu
3020	Możliwość awansu
3022	Bezpłatny transport pracowniczy
3027	Rozwój osobisty
3028	Praca w pozytywnym zespole
3029	Opieka medyczna Medicover
3030	Dofinansowanie do karty sportowej Multisport
3031	Preferencyjne warunki ubezpieczenia na życie
3033	Bony świąteczne
3034	Dofinansowanie do posiłków
3036	Dofinansowanie do obiadów
3042	Dofinansowanie do kart sportowej (Multisport)
3046	Szkolenie przygotowujące do pracy
3047	Wsparcie koordynatora
3048	Dodatkowe benefity
3050	Stabilne zatrudnienie
3051	Szkolenie stanowiskowe
3052	Przyjazna atmosfera
3053	Darmowe obiady
3057	Premia świąteczna
3058	Dofinansowanie dojazdów do pracy
3059	Prywatna opieka medyczna LUXMED
3060	Ubezpieczenie grupowe Allianz
3061	Premia za polecenie pracownika
3062	Posiłki regeneracyjne
3068	Karta sportowa Medicover
3070	Ubezpieczenie Uniqa
3074	Dodatki przed świętami Bożego Narodzenia
3075	Dodatki przed świętami Wielkanocą
3092	Możliwość nauki języków obcych
3093	Dofinansowanie do wycieczek
3094	Dofinansowanie do wyjazdów wakacyjnych
3095	Dofinansowanie do wydarzeń sportowych
3096	Dofinansowanie do wydarzeń kulturalnych
3097	Program rozwoju pasji
3102	Konkursy pracownicze
3106	Szkolenia
3107	Medicover Sport
3119	Karta sportowa Medicover Sport
3120	Możliwość wypłaty wynagrodzenia na bieżąco
3131	Pracownicze Plany Kapitałowe
3132	Zniżki na zakup produktów
3133	Prywatna opieka zdrowotna
3137	Dofinansowanie do żłobka
3138	Dofinansowanie do przedszkola
3141	Bonusy
3142	Premie
3143	Wyżywienie
3145	Bony przedświąteczne
3174	Premie dwumiesięczne
3175	Świadczenie pieniężne przedświąteczne
3176	Dofinansowanie do karty Multisport
3177	Zniżki na firmowe produkty
3178	Ubezpieczenie grupowe na życie
3179	Wsparcie przy podnoszeniu kwalifikacji zawodowych
3181	Karta MultiSport
3186	Awans wewnętrzny
3187	Płatna przerwa
3190	Świadczenia z ZFŚS
3191	System poleceń pracowniczych
3197	Premie za polecenia
3200	Bony na zakupy
3201	Bony do restauracji
3202	Bony na elektronikę
3203	Wycieczki
3204	Kino
3223	Dofinansowanie do karty sportowej
3224	Dofinansowanie do posiłków pracowniczych
3226	Darmowe przejazdy pracownicze
3228	Dofinansowanie do kart sportowych
3237	Transport z Sochaczewa do Teresina
3239	Opieka medyczna LuxMed
3240	Opieka koordynatora OTTO
3241	Ubezpieczenie NNW
3242	Możliwość cotygodniowych zaliczek
3243	Atrakcyjne premie
3247	Konkursy z premiami
3252	Premie motywacyjne
3263	Konkurencyjne wynagrodzenie
3264	Współpraca z doświadczonym zespołem
3265	Nowoczesne narzędzia pracy
3266	Możliwości rozwoju zawodowego
3267	Dofinansowanie świadczeń socjalnych
3276	Dodatkowa płatna przerwa
3281	Stołówka pracownicza
3283	Dofinansowanie obiadów
3284	Bezpłatna kawa
3285	Bezpłatna herbata
3286	Wyprawka dla dziecka
3288	Zniżki na sprzęt
3297	Darmowa kawa i herbata
3305	Opieka medyczna Luxmed
3309	Zniżki na zakupy w Media Expert
3310	Program Poleceń Pracowniczych
3312	Wsparcie Fundacji Mediaexpert
3314	Darmowy transport
3315	Zakwaterowanie
3316	Opieka polskojęzycznego opiekuna projektu
3317	Dostęp do platformy z kursami językowymi
3322	Kursy językowe
3323	Dofinansowanie kart sportowych
3324	Atrakcyjna oferta grupowego ubezpieczenia w PZU
3325	Dofinansowanie opieki medycznej
3326	Parking pracowniczy
3328	Dostęp do biletów na wybrane wydarzenia kulturalne oraz sportowe
3334	Dofinansowanie do karty Multisport/MyBenefit
3335	Platforma do nauki języka angielskiego
3337	Program Wsparcia Pracowników - bezpłatne i poufne doradztwo dla pracowników i ich najbliższych w kwestiach prawnych, finansowych i psychologicznych
3338	Dofinansowanie do nauki
3339	Wsparcie w działaniach rozwojowych
3342	Obiady za 2 zł
3343	System poleceń pracowniczych - bonus 300 zł netto
3344	Grupowe ubezpieczenie na życie
3346	Możliwość wypłaty części wynagrodzenia w trakcie miesiąca
3347	Opieka koordynatora
3350	Premie okolicznościowe
3355	Zniżki na TaniaKsiazka.pl i Bee.pl
3356	Zniżki na posiłki
3357	Karta podarunkowa
3359	Darmowe dojazdy do pracy
3362	Szkolenie zakładowe
3364	Premie wydajnościowe
3365	Premia frekwencyjna
3366	Dodatki za pracę w godzinach nadliczbowych
3367	Dodatki za pracę w godzinach nocnych
3368	Premie półroczne
3369	Płatne dni świąteczne
3370	Ubezpieczenie od następstw nieszczęśliwych wypadków
3371	Pakiet medyczny
3376	Miejsce parkingowe
3378	Prywatna opieka medyczna Luxmed
3382	Pakiet medyczny Medicover
3383	Rabat do 50% w wybranych sklepach
3384	Długotrwała, stabilna, rozwijająca się współpraca
3385	Ciepłe napoje
3386	Obiady z pysznym i różnorodnym menu za złotówkę
3387	Bezpłatny dojazd autobusem pracowniczym na wybranych trasach
3395	Rabat do wybranych sklepów
3396	Długotrwała, stabilna współpraca
3398	Obiady za złotówkę
3399	Bezpłatny dojazd autobusem pracowniczym
3400	Karty sportowe
3401	Ubezpieczenie medyczne
3402	Dofinansowanie do kolonii
3403	Karty podarunkowe
3404	Rabat na zakupy w hurtowni
3405	Obiad z dofinansowaniem
3406	Premie za polecenie kandydata
3407	Bezpłatny transport
3427	Premie świąteczne
3428	Karta lunch pass
3432	Szkolenia podnoszące kwalifikacje
3433	Program wdrożenia
3434	Szkolenie
3437	Szkolenia wdrożeniowe
3439	Wypłata co dwa tygodnie
3440	Szkolenie w restauracji partnerskiej
3442	Oferta rabatowa na posiłki w restauracji partnerskiej
3446	Szkolenie produktowe
3449	Talony na święta
3454	Odpowiedzialna praca
3455	Praca w zespole
3457	Bonus za polecenie pracownika
3458	Szkolenia techniczne
3459	Kursy specjalistyczne
3464	Dofinansowanie wyjazdów wakacyjnych dla dzieci pracowników
3465	Dofinansowanie do aktywności sportowej
3466	Dofinansowanie do aktywności kulturalnej
3467	Dodatkowe wsparcie finansowe dla dzieci pracowników
3468	Miłe niespodzianki dla dzieci pracowników
3475	Wsparcie Fundacji Mediaexpert „Włącz się”
3476	Baby Expert - wyprawka dla Twojego nowo narodzonego dziecka
3477	Stołówka pracownicza z dofinansowaniem do posiłków
3485	Dobrowolne ubezpieczenie grupowe
3490	Obuwie robocze
3492	Dostęp do pakietu medycznego
3493	Ubezpieczenie na zdrowie
3496	MultiLife
3497	ZFŚS
3502	Prywatna opieka medyczna (Luxmed)
3503	Multisport
3504	Grupowe ubezpieczenie
3505	Dostęp do świeżych owoców
3506	Dostęp do kawy
3507	Dostęp do herbaty
3511	Darmowe owoce
3512	Darmowe lody
3514	Zatrudnienie bezpośrednio u naszego klienta
3515	Premie miesięczną
3516	Pakiet benefitów: karta sportowa, pakiet medyczny, dofinansowanie do dojazdów, dofinansowanie do wypoczynku
3517	Stabilne zatrudnienie w oparciu o umowę o pracę w prężnie rozwijającej się firmie z branży spożywczej
3518	Atrakcyjne wynagrodzenie zasadnicze + motywujący system premiowy
3520	Pakiet benefitów: dofinansowanie do karty MultiSport, prywatna opieka medyczna Luxmed, ubezpieczenie grupowe Open Life
3521	Dodatki stażowe
3524	Możliwość zdobycia doświadczenia i rozwoju zawodowego
3525	Pożyczki na preferencyjnych warunkach
3527	Zaplecze socjalne
3529	Atrakcyjny pakiet benefitów
3530	Samochód służbowy
3531	Możliwość pracy w międzynarodowym środowisku
3532	Zatrudnienie w oparciu o umowę o pracę
3533	Dofinansowanie prywatnej opieki medycznej, również dla Twoich bliskich
3534	Dofinansowanie sportu, rekreacji, wypoczynku i wydarzeń kulturalnych dla Ciebie i rodziny
3535	Program wsparcia pracowników: Kasa Zapomogowo – Pożyczkowa
3536	Możliwość przystąpienia do grupowego ubezpieczenia na życie w PZU
3537	Pracowniczy Program Emerytalny - 3,5% już po roku pracy
3538	Bezpłatny dostęp do firmowej platformy e-learningowej - ponad 100 dostępnych kursów
3539	Parking dla pracowników – tylko 1 zł dziennie
3541	Możliwości rozwoju zawodowego i ciągłego uczenia się
3542	Praca w stabilnej firmie
3543	Możliwość przyuczenia do wykonywanej pracy
3544	Możliwość podwyższania swoich kwalifikacji poprzez system szkoleń
3545	Zatrudnienie w pełnym wymiarze czasu pracy na podstawie umowy o pracę
3546	Trwałość zatrudnienia
3547	Świadczenia z Zakładowego Funduszu Świadczeń Socjalnych
3549	Możliwość przystąpienia do ubezpieczenia grupowego
3550	Stabilną pracę w zespole z doświadczeniem
3551	Elastyczne godziny i możliwość pracy zdalnej w części
3552	Wdrożenie i realne możliwości rozwoju
3553	Pracę przy ciekawych projektach i kontakt z nowoczesnymi systemami
3554	Praktyczne wykorzystanie AI w pracy
3555	Stabilne zatrudnienie na podstawie umowy o pracę
3556	Premie świąteczne dwa razy do roku
3557	Dofinansowanie do wypoczynku
3561	Dofinansowanie do żłobków i przedszkoli
3562	Paczki mikołajkowe dla dzieci
3563	Prezent dla bobasa
3564	Premia rekomendacyjna
3565	Stabilne zatrudnienie w oparciu o umowę o pracę (pełny etat)
3566	Pracę w nowoczesnym środowisku SAP S/4HANA
3567	Międzynarodowe projekty i współpraca z zespołami z różnych krajów
3568	Atrakcyjny pakiet wynagrodzenia i benefity pozapłacowe
3569	Przyjazną atmosferę, nowoczesne biuro i kulturę dzielenia się wiedzą
3570	Możliwość realnego wpływu na rozwój systemu oraz strategię SAP
3571	Stałe możliwości rozwoju: szkolenia, certyfikacje, konferencje
3572	Ruchomy czas pracy
3573	Indywidualny rozkład czasu pracy
3575	Dofinansowanie zajęć sportowo-rekreacyjnych
3576	Pomieszczenie lub stojaki na rowery na terenie urzędu
3577	Miejsce parkingowe na terenie urzędu
3578	Dofinansowanie do wypoczynku pracowników
3579	Zniżki na wypoczynek w ośrodkach wypoczynkowych
3580	Stabilna praca w jednostce administracji państwowej z wieloletnią tradycją
3581	Otwarte i przyjazne środowisko pracy
3582	Dodatek za wieloletnią pracę
3583	Trzynaste wynagrodzenie
3584	Możliwość rozwoju kompetencji zawodowych
3585	Pożyczki pracownicze na preferencyjnych warunkach
3586	Możliwość wykupienia ubezpieczenia zdrowotnego
3587	Możliwość wykupienia polisy na życie w preferencyjnej cenie
3588	Możliwość zakupu pakietów opieki medycznej (Medicover, Luxmed)
3589	Możliwość przystąpienia do Pracowniczych Planów Kapitałowych
3590	Dofinansowanie do zakupu okularów/soczewek korekcyjnych
3617	Salary: PLN 7500 gross
3618	Working hours: 8 a.m. to 6 p.m., Monday to Friday, shift system
3619	Start date: 17.11.2025
3620	Comprehensive health and wellness benefits
3621	Opportunities for professional development and continuous learning
3637	Dofinansowanie do wypoczynku dla Ciebie i Twojej rodziny
3638	Grupowe ubezpieczenie na życie na atrakcyjnych warunkach
3639	Prywatna opieka medyczna opłacona przez pracodawcę w podstawowym zakresie, każdy pakiet możesz dodatkowo rozszerzyć i objąć nim swoją rodzinę
3640	Karta sportowa na atrakcyjnych warunkach z dopłatą pracodawcy
3644	Premia rekomendacyjna- poleć nam pracownika, wypłacimy Ci premię
3671	Wynagrodzenie brutto: od 4 797 do 6 850 PLN
3672	Opis wynagrodzenia: + 10% premii uznaniowej
3673	System wynagrodzenia: Czasowy ze stawką miesięczną
3675	Pakiet szkoleń
3676	Code Review
3677	Sprawdzona ścieżka kariery w IT
3678	Projekty open source
3679	Praca w dynamicznie rozwijającym się zespole
3680	Ciekawe projekty
3681	Kawa
3683	Darty
3684	Opieka zdrowotna
3688	Kursy doszkalające
3689	MultiSport
3693	Program kafeteryjny
3697	Wsparcie merytoryczne
\.


COPY public.cities (id, city) FROM stdin;
1	Poznań
2	Gdynia
3	Wrocław
4	Kraków
5	Opole
6	Warszawa
10	Iwiny
11	Lublin
12	Konstancin-Jeziorna
17	Gdańsk
18	Środa Śląska
19	Łódź
20	Raszyn
24	Marki
26	Mszczonów
27	Komorniki
29	Grudziądz
30	Kowale
32	Gorzów Wielkopolski
33	Krzycko Wielkie
34	Dąbrówka
37	Ostrowiec Świętokrzyski
39	Paproć k. Nowego Tomyśla
51	Białystok
189	Skawina
192	Katowice
218	Śrem
221	Słupsk
222	Toruń
227	Otmuchów
229	Pleszew
236	Chełmno
240	Różan
241	Starogard Gdański
242	Rawicz
243	Rzeszów
245	Gnojnik
251	Bieruń
253	Gliwice
254	Ciecierzyn
258	Lubaczów
269	Olsztyn
314	Bydgoszcz
323	Złotów
340	Zgorzała
341	WARSZAWA
343	Ożarów Mazowiecki
367	Skarbimierz-Osiedle
378	Czerwonak
379	Sady k. Poznania
381	Nowy Sącz
395	Kołaczkowice
396	Kępno
397	Luboń
399	Kielce
400	Bielsko-Biała
402	Szczecin
404	Zabrze
408	Ełk
410	Jedlicze A
411	Zielonka
412	Ostrów Wielkopolski
414	Stalowa Wola
415	Legnica
418	Jawczyce
419	Częstochowa
421	Sulejówek
423	Okunica
424	Zgorzelec
427	Będzin
430	Siemianowice Śląskie
432	Swadzim
435	Świerkówki
436	Robakowo
437	Mogilno
440	Ostrołęka
441	Małopole
442	Nowy Targ
443	Wtórek
444	Niepruszewo
452	Turośń Kościelna
457	Modlniczka
460	Płock
462	Brzezinka
464	Sępólno Krajeńskie
466	Biała Podlaska
467	Łomianki
469	Włocławek
471	Głogoczów
472	Raczyny
473	Gniezno
475	Pyzdry
478	Zielona Góra
482	Koszalin
488	Teresin
491	Pyskowice
492	Wyszków
495	Sopot
496	Szepietowo
498	Tomaszów Mazowiecki
501	Radzymin
502	Turek
508	Syców
509	Pszczyna
510	Pabianice
513	Czechowice-Dziedzice
514	Bielawa
516	Jasin
519	Wieluń
521	Olsztynek
522	Wągrowiec
523	Berlin
524	Gądki
526	Świebodzin
532	Sosnowiec
539	Halberstadt
542	Kołobrzeg
545	Skierniewice
550	Kutno
551	Cegłów
560	Płońsk
561	Emilianów
562	Sędziszów Małopolski
567	Człuchów
569	Brzeg
575	Tarnów
579	Oświęcim
588	Bełchatów
589	Rybnik
599	Leszno
601	Iława
614	Zgierz
638	Radom
640	Biłgoraj
642	Głogów
643	Kalisz
678	Mińsk Mazowiecki
683	Giżycko
692	Piekary Śląskie
\.

COPY public.companies (id, name, logo_url) FROM stdin;
1	STX Next S.A.	https://logos.gpcdn.pl/loga-firm/20055171/2c580000-43a8-f403-8c97-08d82a4aa6ae_280x280.png
2	Idego Group Sp. z o.o.	https://logos.gpcdn.pl/loga-firm/20108013/342a0000-56be-0050-0643-08dc7a3a6377_280x280.jpg
3	SAGITON sp. z o.o.	https://logos.gpcdn.pl/loga-firm/1074094752/f15b0000-56be-0050-1d9d-08dca7e6d81f_280x280.png
4	DATAGROUP POLSKA SPÓŁKA Z OGRANICZONĄ ODPOWIEDZIALNOŚCIĄ	https://logos.gpcdn.pl/loga-firm/20407810/2da80000-56be-0050-36c8-08dc2d4a0f98_280x280.png
5	KBA AUTOMATIC	https://logos.gpcdn.pl/loga-firm/1074117804/ee4d0000-5df0-0015-b88c-08dae81a1a3d_280x280.png
6	Bank Gospodarstwa Krajowego	https://logos.gpcdn.pl/loga-firm/9704743/2c580000-43a8-f403-b400-08d7e7968b30_280x280.png
7	LUX MED Sp. z o.o.	https://logos.gpcdn.pl/loga-firm/18803855/2c580000-43a8-f403-0258-08d6dec4d03b_280x280.png
8	Comtegra S.A.	https://logos.gpcdn.pl/loga-firm/18804304/03000000-bb2f-3863-d45e-08d874fb2d8e_280x280.png
9	Elmark Automatyka S.A.	https://logos.gpcdn.pl/loga-firm/20006901/2c580000-43a8-f403-8add-08d8c91909cf_280x280.png
10	Room99 sp. z o.o.	https://logos.gpcdn.pl/loga-firm/20438335/e04f0000-56be-0050-2cbc-08dd46b2a713_280x280.png
11	Asseco Business Solutions S.A.	https://logos.gpcdn.pl/loga-firm/20002894/2c580000-43a8-f403-1fce-08d910886ffa_280x280.jpg
12	Polskie Sieci Elektroenergetyczne S.A.	https://logos.gpcdn.pl/loga-firm/20022176/2c580000-43a8-f403-bd9b-08d8a01775c9_280x280.png
13	Insert S.A.	https://logos.gpcdn.pl/loga-firm/12776255/03000000-bb2f-3863-bce7-08d8fdab4259_280x280.png
14	Koleje Wielkopolskie sp. z o.o.	https://logos.gpcdn.pl/loga-firm/20111971/03000000-bb2f-3863-179c-08d8c6a5437a_280x280.png
15	Hemmersbach	https://logos.gpcdn.pl/loga-firm/20199758/301d0000-56be-0050-7c1d-08dd67a8a2af_280x280.jpg
16	Würth Polska Sp. z o.o.	https://logos.gpcdn.pl/loga-firm/13351895/2c580000-43a8-f403-8b07-08d8d89fafb7_280x280.jpg
17	SKAT Transport P.S.A.	https://logos.gpcdn.pl/loga-firm/20011980/844f0000-56be-0050-f34e-08ddad690f9a_280x280.png
18	Schuerholz Polska Sp. z o.o.	https://logos.gpcdn.pl/loga-firm/20000553/03000000-bb2f-3863-c353-08d8cc10e61a_280x280.png
19	Intelligent Logistic Solutions Sp. z o.o.	https://logos.gpcdn.pl/loga-firm/20134223/03000000-bb2f-3863-e166-08d8bd21bb32_280x280.jpg
20	Zakład Informatyki Lasów Państwowych	https://logos.gpcdn.pl/loga-firm/20094146/2c580000-43a8-f403-ba63-08d8225533b1_280x280.jpg
21	POLSKIE RADIO - REGIONALNA ROZGŁOŚNIA W LUBLINIE "RADIO LUBLIN" S.A. W LIKWIDACJI	\N
23	Allianz	https://logos.gpcdn.pl/loga-firm/20001585/9b030000-5dac-0015-69d7-08da5a845bb0_280x280.png
24	EDMED SPÓŁKA Z OGRANICZONĄ ODPOWIEDZIALNOŚCIĄ	https://logos.gpcdn.pl/loga-firm/1074197856/d8370000-56be-0050-5929-08dd41fde446_280x280.jpg
25	JSD POLSKA sp. z o.o.	https://logos.gpcdn.pl/loga-firm/1074047118/03000000-bb2f-3863-8a97-08d9088aa697_280x280.png
26	FM Logistic	https://logos.gpcdn.pl/loga-firm/18119536/03000000-bb2f-3863-34df-08da029a6440_280x280.png
27	Eurocash S.A.	https://logos.gpcdn.pl/loga-firm/20000409/03000000-bb2f-3863-424a-08d6dec4f444_280x280.png
28	COMSET SPÓŁKA AKCYJNA	https://logos.gpcdn.pl/loga-firm/20096298/2c580000-43a8-f403-969b-08d88c762682_280x280.png
29	Saica Paper Polska Sp. z o.o.	https://logos.gpcdn.pl/loga-firm/20086976/3c510000-56be-0050-556c-08dd6606b93b_280x280.png
30	DOT2DOT S.A.	https://logos.gpcdn.pl/loga-firm/20257800/78050000-56be-0050-7176-08dd8bbcfb3d_280x280.png
31	GROUP IT sp. z o.o. SPÓŁKA KOMANDYTOWA	\N
32	LUBUSKIE CENTRUM CYFRYZACJI GO CLOUD sp. z o.o.	\N
33	WERNER KENKEL Spółka z o.o.	https://logos.gpcdn.pl/loga-firm/1074087319/1c0b0000-56be-0050-3542-08dd9dc5ae17_280x280.png
34	Codev Marcin Mierzejewski	\N
35	SMYK sp. z o.o.	https://logos.gpcdn.pl/loga-firm/11634828/9c280000-56be-0050-fba6-08de155efd9b_280x280.jpg
36	Wrocławski Park Wodny S.A.	https://logos.gpcdn.pl/loga-firm/20015775/03000000-bb2f-3863-67de-08d8df2547bd_280x280.png
37	DANSTOKER POLAND SPÓŁKA Z OGRANICZONA ODPOWIEDZIALNOŚCIĄ	https://logos.gpcdn.pl/loga-firm/20293444/03000000-bb2f-3863-a686-08d8e30cf394_280x280.png
38	Scalo Sp. z o.o.	https://logos.gpcdn.pl/loga-firm/20048614/03000000-bb2f-3863-d424-08d930062d01_280x280.png
39	Erbacher the food family	https://logos.gpcdn.pl/loga-firm/20000751/2c580000-43a8-f403-862f-08d994716003_280x280.jpg
49	INPROX	https://logos.gpcdn.pl/loga-firm/20273082/03000000-bb2f-3863-d295-08d8d8c89535_280x280.jpg
51	CENTRUM USŁUG INFORMATYCZNYCH W BIAŁYMSTOKU	https://logos.gpcdn.pl/loga-firm/1074033991/2c580000-43a8-f403-61ca-08d951bec70f_280x280.png
57	GF Corp Sp. z o.o. sp. k.	\N
68	Instytut Studiów Programistycznych S.A.	https://logos.gpcdn.pl/loga-firm/20299987/03000000-bb2f-3863-ace0-08d8c68e9f39_280x280.jpg
75	Edge One Solutions Sp. z o.o.	https://logos.gpcdn.pl/loga-firm/20195619/03000000-bb2f-3863-f036-08d9dfe29bf4_280x280.png
144	ASSECO DATA SYSTEMS S.A.	https://logos.gpcdn.pl/loga-firm/20212327/2c580000-43a8-f403-f994-08d8f628206c_280x280.png
191	Light Prestige Spółka z o.o. Sp. kom.	https://logos.gpcdn.pl/loga-firm/20059422/2c580000-43a8-f403-9973-08d929a43258_280x280.png
194	Travcorp Poland Sp z o.o.	https://logos.gpcdn.pl/loga-firm/1074116072/ee4d0000-5df0-0015-e51d-08daf5638412_280x280.png
204	UNIWERSYTECKI SZPITAL KLINICZNY W POZNANIU	https://logos.gpcdn.pl/loga-firm/20269066/342a0000-56be-0050-07cc-08dc64f7e96c_280x280.png
214	ITCHANGE JAKUB POTOCKI sp.k.	\N
215	ABC KOMPUTERY MESKOMP TOMASZ MESJASZ	\N
216	Bones Studio	https://logos.gpcdn.pl/loga-firm/20300182/03000000-bb2f-3863-3dae-08d905680bfe_280x280.jpg
217	BALTIC HUB CONTAINER TERMINAL sp. z o. o.	https://logos.gpcdn.pl/loga-firm/18802925/9b030000-5dac-0015-0778-08db1ff08241_280x280.png
218	G CITY POLAND SPÓŁKA Z OGRANICZONĄ ODPOWIEDZIALNOŚCIĄ	https://logos.gpcdn.pl/loga-firm/17920980/9b030000-5dac-0015-1d57-08db25f7876f_280x280.png
286	Softinet	https://logos.gpcdn.pl/loga-firm/20197678/2c580000-43a8-f403-c6f5-08da08cabf0b_280x280.png
219	Politechnika Gdańska	https://logos.gpcdn.pl/loga-firm/20003297/2c580000-43a8-f403-c379-08d8f2c6c17a_280x280.jpg
220	Gulermak Agir Sanayi Insaat Ve Taahhut A.S. Oddział w Polsce	https://logos.gpcdn.pl/loga-firm/20068159/03000000-bb2f-3863-2917-08d8d745e2cf_280x280.png
221	Capchem Poland sp. z o.o.	https://logos.gpcdn.pl/loga-firm/20410389/03000000-bb2f-3863-801a-08da0356db97_280x280.png
222	Print&amp;Display (Polska) Sp. z o.o.	https://logos.gpcdn.pl/loga-firm/20011940/03000000-bb2f-3863-73c6-08d90f0efc28_280x280.png
223	Robert Bosch Sp. z o.o.	https://logos.gpcdn.pl/loga-firm/15713211/03000000-bb2f-3863-6260-08d8b88d3d06_280x280.png
224	PERLA POLSKA	https://logos.gpcdn.pl/loga-firm/20170412/2da80000-56be-0050-0d46-08dc1758e851_280x280.png
225	Uniwersytet Mikołaja Kopernika w Toruniu	https://logos.gpcdn.pl/loga-firm/20009001/d0510000-56be-0050-fbeb-08ddc9f413f7_280x280.jpg
226	TARASOLA SPÓŁKA Z OGRANICZONĄ ODPOWIEDZIALNOŚCIĄ	https://logos.gpcdn.pl/loga-firm/1074137908/342a0000-56be-0050-47dc-08dcb6af743b_280x280.png
227	B2B.NET S.A.	https://logos.gpcdn.pl/loga-firm/20424689/03000000-bb2f-3863-a2ad-08d9952c4759_280x280.png
229	Kompania Piwowarska	https://logos.gpcdn.pl/loga-firm/20004275/2c580000-43a8-f403-284b-08d8bcad51b4_280x280.jpeg
230	Zakłady Przemysłu Cukierniczego "Otmuchów" S.A.	https://logos.gpcdn.pl/loga-firm/20016534/2c580000-43a8-f403-d33c-08d8eea582b5_280x280.png
232	BANK SPÓŁDZIELCZY W PLESZEWIE	\N
233	Centrum Medyczne ENEL-MED S.A.	https://logos.gpcdn.pl/loga-firm/18802787/5c3e0000-56be-0050-a4a2-08de16ef76c8_280x280.jpg
235	AXA XL Catlin Services SE	https://logos.gpcdn.pl/loga-firm/20307977/03000000-bb2f-3863-ea0d-08d8bedc27dc_280x280.jpg
236	ITENERUM sp. z o.o.	\N
237	ASTEK Polska	\N
238	Urząd Komunikacji Elektronicznej	https://logos.gpcdn.pl/loga-firm/20003437/2c580000-43a8-f403-f92d-08d987fe7c5e_280x280.png
239	Agro-Sieć Maszyny Sp. z o.o.	https://logos.gpcdn.pl/loga-firm/20154790/2c580000-43a8-f403-ef4d-08d8e9451c0b_280x280.png
240	LINKSET sp. z o.o.	https://logos.gpcdn.pl/loga-firm/1074120181/ee4d0000-5df0-0015-23c1-08db9a7967e3_280x280.png
241	Grafton Recruitment	https://logos.gpcdn.pl/loga-firm/76/f15b0000-56be-0050-a7ca-08dc5961ab50_280x280.png
242	PURO Hotels	https://logos.gpcdn.pl/loga-firm/20070747/9b030000-5dac-0015-e79e-08db732ca756_280x280.png
243	Grill-Impex Polska Spółka z ograniczoną odpowiedzialnością	\N
244	Sputnik T.Pastwa spółka komandytowa	\N
245	7Technology Sp. z o.o.	https://logos.gpcdn.pl/loga-firm/20127462/03000000-bb2f-3863-c396-08d89dda2c05_280x280.png
246	Donegal sp. z o.o.	https://logos.gpcdn.pl/loga-firm/20003216/03000000-bb2f-3863-c20b-08d8d4eda283_280x280.jpg
247	TELEDIAGNOSIS sp. z o.o.	https://logos.gpcdn.pl/loga-firm/20411103/ee4d0000-5df0-0015-30f0-08db18b8b48e_280x280.png
248	PAGEN Sp. z o.o.	https://logos.gpcdn.pl/loga-firm/20032069/2c580000-43a8-f403-ee8d-08d976b61608_280x280.png
249	Fix24 Jakub Jankowski	\N
250	H. Cegielski - Fabryka Pojazdów Szynowych Sp. z o.o.	https://logos.gpcdn.pl/loga-firm/20016680/03000000-bb2f-3863-50a1-08d8dfca1238_280x280.png
251	GRIDNET Sp. z o.o.	https://logos.gpcdn.pl/loga-firm/20210705/03000000-bb2f-3863-0e82-08d8d9709172_280x280.png
252	WADOWSCY	https://logos.gpcdn.pl/loga-firm/20004847/03000000-bb2f-3863-4787-08d8b19581c1_280x280.jpg
253	Arche Consulting Sp z o.o.	https://logos.gpcdn.pl/loga-firm/20093617/03000000-bb2f-3863-c3e5-08d930af1ec1_280x280.png
254	Auto Partner S.A.	https://logos.gpcdn.pl/loga-firm/12199078/03000000-bb2f-3863-959a-08d89aa27fb9_280x280.png
256	Gardia Broker Sp. z o.o. Broker ubezpieczeniowy	\N
257	Solinea	https://logos.gpcdn.pl/loga-firm/20210278/03000000-bb2f-3863-1266-08d8beb1660a_280x280.jpg
258	PAWEŁ BORUCH SUNSECO	https://logos.gpcdn.pl/loga-firm/1074194377/441c0000-56be-0050-bcd9-08de1d198b0a_280x280.png
259	DreamITeam Sp.z o.o.	https://logos.gpcdn.pl/loga-firm/20199528/2c580000-43a8-f403-c4f1-08d8f4ff71b5_280x280.png
260	Instytut Historii im. Tadeusza Manteuffla Polskiej Akademii Nauk	\N
261	BANK SPÓŁDZIELCZY w LUBACZOWIE	\N
262	ŁÓDZKA WOJEWÓDZKA KOMENDA OCHOTNICZYCH HUFCÓW PRACY W ŁODZI	\N
263	Instytut Biologii Doświadczalnej im. M. Nenckiego PAN	\N
264	BLISSPOINT.SPACE sp. z o.o.	\N
265	IT Solution	\N
266	PGE Systemy S.A.	https://logos.gpcdn.pl/loga-firm/20120282/03000000-bb2f-3863-4688-08d9e0b76567_280x280.png
267	Zakład Ubezpieczeń Społecznych	https://logos.gpcdn.pl/loga-firm/20185506/2c580000-43a8-f403-8565-08d9a9bb54bf_280x280.jpg
269	cyber_Folks S.A.	https://logos.gpcdn.pl/loga-firm/1074087045/48270000-56be-0050-f501-08dd40ff5479_280x280.png
270	SiDLY Sp. z o. o.	https://logos.gpcdn.pl/loga-firm/20200159/03000000-bb2f-3863-7901-08d812f9258d_280x280.jpg
271	Spark-IT Sp. z o.o.	https://logos.gpcdn.pl/loga-firm/20386127/03000000-bb2f-3863-eb0a-08d8eabee23f_280x280.png
272	Instytut Rybactwa Śródlądowego im. Stanisława Sakowicza-Państwowy Instytut Badawczy	\N
273	JAS-FBG S.A.	https://logos.gpcdn.pl/loga-firm/13360715/03000000-bb2f-3863-3b2e-08d8740239ce_280x280.png
274	MOREBIT SPÓŁKA Z OGRANICZONĄ ODPOWIEDZIALNOŚCIĄ	https://logos.gpcdn.pl/loga-firm/1074136580/ee4d0000-5df0-0015-c87b-08db9418f91b_280x280.png
275	eService Sp. z o.o.	https://logos.gpcdn.pl/loga-firm/16068023/342a0000-56be-0050-5bc3-08dc89282fa8_280x280.png
276	PROMISON sp. z o.o.	\N
277	Unimet Spółka z o.o.	\N
278	No Limit Sp. z o.o.	https://logos.gpcdn.pl/loga-firm/20003350/ec140000-56be-0050-6c87-08de11689ba7_280x280.jpg
279	Politechnika Wrocławska	https://logos.gpcdn.pl/loga-firm/20014448/03000000-bb2f-3863-485f-08d8fa90ba34_280x280.png
280	GK Farmacol	https://logos.gpcdn.pl/loga-firm/20010993/2c580000-43a8-f403-a4a5-08d9783d621d_280x280.png
281	PIXEL Technology sp. z o.o.	https://logos.gpcdn.pl/loga-firm/20213861/03000000-bb2f-3863-4238-08d9923d4568_280x280.png
282	PIEKARNIE LUBASZKA sp. z o.o.	https://logos.gpcdn.pl/loga-firm/20007563/03000000-bb2f-3863-3de8-08d8822b5b47_280x280.png
283	Akademia Wymiaru Sprawiedliwości	\N
284	SĄD APELACYJNY W POZNANIU	https://logos.gpcdn.pl/loga-firm/1074132727/f0470000-56be-0050-e09c-08dda4e46a09_280x280.png
285	SK MSWiA z W-MCO w OLSZTYNIE	https://logos.gpcdn.pl/loga-firm/20043768/03000000-bb2f-3863-7b86-08d900d58c9d_280x280.png
287	4 Wojskowy Szpital Kliniczny z Polikliniką Samodzielny Publiczny Zakład Opieki Zdrowotnej	\N
288	PSE INNOWACJE SP Z O O	https://logos.gpcdn.pl/loga-firm/20247292/2c580000-43a8-f403-bc97-08d848e756b7_280x280.png
289	Primesoft Polska Sp. z o.o.	https://logos.gpcdn.pl/loga-firm/18798269/2c580000-43a8-f403-bfa4-08d8cc391e51_280x280.png
291	EUROCERT sp. z o.o.	https://logos.gpcdn.pl/loga-firm/1074050843/2c580000-43a8-f403-3409-08d97c2d9379_280x280.png
292	TUiR WARTA S.A.	https://logos.gpcdn.pl/loga-firm/13663926/03000000-bb2f-3863-aca6-08d8c217fce7_280x280.png
293	PKO Bank Polski SA	https://logos.gpcdn.pl/loga-firm/20008525/f15b0000-56be-0050-f7ea-08dc8a0b8aee_280x280.png
295	ITHOUSE SPÓŁKA Z OGRANICZONĄ ODPOWIEDZIALNOŚCIĄ	https://logos.gpcdn.pl/loga-firm/1074003506/342a0000-56be-0050-5aac-08dce2b9ae7d_280x280.png
296	Omega Code Sp. z o.o.	https://logos.gpcdn.pl/loga-firm/20096405/a4470000-56be-0050-ffdb-08de162f134b_280x280.png
299	EUVIC S.A.	https://logos.gpcdn.pl/loga-firm/20059659/ee4d0000-5df0-0015-e758-08db9e3aaf78_280x280.png
300	NATEK POLAND	https://logos.gpcdn.pl/loga-firm/20002666/03000000-bb2f-3863-3ada-08d80bb450e6_280x280.png
301	Uniwersytet Dolnośląski DSW	https://logos.gpcdn.pl/loga-firm/20012084/ee4d0000-5df0-0015-af37-08dbd097ce9e_280x280.jpg
302	XTB S.A.	https://logos.gpcdn.pl/loga-firm/18802013/08610000-56be-0050-bdd8-08dcfcd9a710_280x280.png
303	LOTTOMERKURY sp. z o.o.	https://logos.gpcdn.pl/loga-firm/20427066/eafd0000-56be-0050-efe2-08dc1db177ec_280x280.png
304	PEOPLEVIBE SPÓŁKA Z OGRANICZONĄ ODPOWIEDZIALNOŚCIĄ	https://logos.gpcdn.pl/loga-firm/1074107258/ee4d0000-5df0-0015-9fce-08db1bc85c96_280x280.png
305	Polski Standard Płatności S.A.	https://logos.gpcdn.pl/loga-firm/20177690/03000000-bb2f-3863-99d0-08d8a7435281_280x280.png
307	FORCOM Sp. z o.o.	https://logos.gpcdn.pl/loga-firm/18797771/1c3c0000-56be-0050-3653-08ddce6be4c7_280x280.jpg
312	UPVANTA SPÓŁKA Z OGRANICZONĄ ODPOWIEDZIALNOŚCIĄ	https://logos.gpcdn.pl/loga-firm/1074103426/002a0000-56be-0050-763b-08ddd41047f0_280x280.jpg
315	Robert Bosch sp. z o.o. Oddział w Mirkowie	https://logos.gpcdn.pl/loga-firm/20002769/2c580000-43a8-f403-b769-08d8c757717e_280x280.jpg
316	Urząd Miasta Krakowa	https://logos.gpcdn.pl/loga-firm/20291503/03000000-bb2f-3863-1fb2-08d8fa76a68c_280x280.png
317	Zakład Elektroniczny SiMS spółka z o.o. spółka komandytowa	https://logos.gpcdn.pl/loga-firm/20143393/03000000-bb2f-3863-e03e-08d8d8013756_280x280.jpg
318	Toyota Bank Polska S.A.	https://logos.gpcdn.pl/loga-firm/15879287/03000000-bb2f-3863-ae3c-08d8d7db31c1_280x280.png
319	Intelight Sp. z o.o.	https://logos.gpcdn.pl/loga-firm/20313095/ee4d0000-5df0-0015-618f-08da59c0040f_280x280.png
320	Biedronka (Jeronimo Martins Polska S.A.)	https://logos.gpcdn.pl/loga-firm/921/402b0000-56be-0050-bd29-08de187e6eb2_280x280.png
323	Polska Agencja Żeglugi Powietrznej	https://logos.gpcdn.pl/loga-firm/18799094/9b030000-5dac-0015-0ab5-08dba2c99b12_280x280.png
324	Wirtualna Polska Media S.A.	https://logos.gpcdn.pl/loga-firm/20196112/03000000-bb2f-3863-082b-08d8dd6bef1f_280x280.png
326	Media Expert	https://logos.gpcdn.pl/loga-firm/18802262/03000000-bb2f-3863-7d5f-08d8ef8ad0b9_280x280.png
328	Bank Pekao	https://logos.gpcdn.pl/loga-firm/1081/9b030000-5dac-0015-ad6f-08db5c4fc5d8_280x280.png
329	Schindler Polska Sp. z o.o.	https://logos.gpcdn.pl/loga-firm/18803144/2c580000-43a8-f403-48ae-08d8f04a6220_280x280.jpg
332	Be in IT	https://logos.gpcdn.pl/loga-firm/1074061445/342a0000-56be-0050-e282-08dc895ea018_280x280.png
334	Bergman Engineering Sp. z o.o.	\N
336	PKP Polskie Linie Kolejowe S.A	https://logos.gpcdn.pl/loga-firm/20057479/342a0000-56be-0050-789b-08dcba9e3d07_280x280.png
337	BNP Paribas Bank Polska SA	https://logos.gpcdn.pl/loga-firm/20136891/34520000-56be-0050-9e51-08dd50f1fad1_280x280.png
338	SIGTEL sp. z o.o.	\N
339	Agram S.A.	https://logos.gpcdn.pl/loga-firm/20017955/2c580000-43a8-f403-0b3e-08d987f3a464_280x280.png
341	AB Systems sp. z o.o.	https://logos.gpcdn.pl/loga-firm/20022464/2c580000-43a8-f403-ecd5-08d8cf3fdc1f_280x280.png
342	PRAGMAGO SA	https://logos.gpcdn.pl/loga-firm/20004414/50080000-56be-0050-5386-08dd9eae047b_280x280.jpg
343	Green Minds Sp. z o.o.	https://logos.gpcdn.pl/loga-firm/20179183/2c580000-43a8-f403-9cb0-08d95b041164_280x280.png
344	Way2Send Sp. z o.o.	\N
345	KPMG	https://logos.gpcdn.pl/loga-firm/37/03000000-bb2f-3863-b0f4-08d8f364555a_280x280.png
346	WB Electronics S.A.	https://logos.gpcdn.pl/loga-firm/20012726/2c580000-43a8-f403-093d-08d8c1e3c87d_280x280.png
347	RTV EURO AGD	https://logos.gpcdn.pl/loga-firm/11665813/80560000-56be-0050-8bb5-08ddd8c8e6ca_280x280.jpg
348	T-Mobile	https://logos.gpcdn.pl/loga-firm/20008533/9b030000-5dac-0015-2610-08da6eeafa81_280x280.png
349	PKP Intercity S.A.	https://logos.gpcdn.pl/loga-firm/20015607/2c580000-43a8-f403-bcad-08d890930e6e_280x280.png
350	BAKK Sp. z o.o.	https://logos.gpcdn.pl/loga-firm/20157762/03000000-bb2f-3863-01a9-08d8f9974a87_280x280.png
354	SAGES sp. z o.o.	https://logos.gpcdn.pl/loga-firm/20430097/2c580000-43a8-f403-eedd-08d875c8ced9_280x280.png
358	Nationale-Nederlanden	https://logos.gpcdn.pl/loga-firm/18800763/2c580000-43a8-f403-83ba-08d91c6795a5_280x280.png
360	Comarch SA	https://logos.gpcdn.pl/loga-firm/253/03000000-bb2f-3863-d48b-08d8f8cdb974_280x280.png
361	Sii Sp. z o.o.	https://logos.gpcdn.pl/loga-firm/20000761/03000000-bb2f-3863-1665-08d6dec4c54d_280x280.jpeg
366	COIG	https://logos.gpcdn.pl/loga-firm/20004530/03000000-bb2f-3863-7ede-08d88582b418_280x280.jpg
367	ALAB laboratoria Sp z o.o.	https://logos.gpcdn.pl/loga-firm/18798310/bc330000-56be-0050-a0d5-08dd33c64f6e_280x280.png
369	Netia	https://logos.gpcdn.pl/loga-firm/17819798/f15b0000-56be-0050-132c-08dc3c359bee_280x280.png
370	Mitsui High-tec (Europe) sp. z o.o.	https://logos.gpcdn.pl/loga-firm/20427319/005e0000-56be-0050-224c-08ddde47f8d0_280x280.jpg
372	NASK	https://logos.gpcdn.pl/loga-firm/20011564/54330000-56be-0050-f1b2-08dd9c317145_280x280.png
374	Havi Logistics Sp. z o.o.	https://logos.gpcdn.pl/loga-firm/20009881/7c140000-56be-0050-2306-08dd75d7094a_280x280.png
375	Santander Bank Polska	https://logos.gpcdn.pl/loga-firm/282/2c580000-43a8-f403-1a05-08d8deee71c1_280x280.png
376	ALLIANCE SOLUTIONS SPÓŁKA Z OGRANICZONĄ ODPOWIEDZIALNOŚCIĄ	https://logos.gpcdn.pl/loga-firm/1074223039/402b0000-56be-0050-19cf-08de187a101e_280x280.jpg
728	Edu Active	\N
377	PIT-RADWAR S.A.	https://logos.gpcdn.pl/loga-firm/20068387/9b030000-5dac-0015-67a5-08db1a5b8a69_280x280.png
378	CRESTT sp. z o.o.	https://logos.gpcdn.pl/loga-firm/1074032335/2c580000-43a8-f403-7e35-08d84feca65f_280x280.png
379	Meritoros SA	https://logos.gpcdn.pl/loga-firm/20070455/ee4d0000-5df0-0015-0860-08dbd147a0e6_280x280.png
380	IT EXCELLENCE SPÓŁKA AKCYJNA	https://logos.gpcdn.pl/loga-firm/1074199586/a0230000-56be-0050-ac22-08dda2d51432_280x280.png
381	ITNS Polska Sp. z o.o.	https://logos.gpcdn.pl/loga-firm/20097825/2c580000-43a8-f403-9820-08d92bdf8b32_280x280.png
382	Lorenz Services sp. z o.o.	https://logos.gpcdn.pl/loga-firm/20139633/03000000-bb2f-3863-f4e9-08d8f8e33b53_280x280.png
384	RANDLAB SOFTWARE sp. z o.o.	https://logos.gpcdn.pl/loga-firm/1074157434/342a0000-56be-0050-1963-08dc70efb2f8_280x280.png
386	Totalizator Sportowy	https://logos.gpcdn.pl/loga-firm/18803296/03000000-bb2f-3863-65a4-08d8765d18ac_280x280.png
387	TUnŻ Warta S.A	https://logos.gpcdn.pl/loga-firm/18801992/2c580000-43a8-f403-9ab4-08d8f02b537b_280x280.png
388	Rainbow Tours S.A.	https://logos.gpcdn.pl/loga-firm/20056981/03000000-bb2f-3863-3605-08d6dec4e55f_280x280.png
389	DomData S.A.	https://logos.gpcdn.pl/loga-firm/20070685/302b0000-56be-0050-f4bb-08dce4397979_280x280.png
391	ALTEN Polska	https://logos.gpcdn.pl/loga-firm/20062259/2c580000-43a8-f403-1fb3-08d7fbf29184_280x280.png
392	FERCHAU POLAND sp. z o.o.	https://logos.gpcdn.pl/loga-firm/1074093037/9b030000-5dac-0015-69ed-08db083b23f7_280x280.png
395	Capgemini Polska	https://logos.gpcdn.pl/loga-firm/18804842/2c580000-43a8-f403-6783-08d6dec4e4b7_280x280.png
396	PROVIDENT Polska	https://logos.gpcdn.pl/loga-firm/748/b04e0000-56be-0050-2fa0-08dcff0bc7c0_280x280.png
398	KA 4 sp. z o.o. sp.k.	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/1271741795_1_261x203_rev006.jpg
399	DomArtStyl	\N
400	i-want.pl	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/1377767606_1_261x203_rev002.jpg
401	Wrocławskie Zakłady Zielarskie HERBAPOL SA	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/797121318_1_261x203_rev005.jpg
402	Inter -Team Sp. z o.o.	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/699911137_1_261x203_rev004.jpg
403	Karmello Chocolatier	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/963410433_1_261x203_rev003.jpg
404	Randstad Polska Sp. z o.o.	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/595452295_1_261x203_rev017.jpg
406	Grupa Express	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/891236355_1_261x203_rev014.jpg
408	Inter Cars S.A.	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/1016553580_1_261x203.jpg
409	noo.ma	\N
410	Crocs	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/781708297_1_261x203_rev001.jpg
411	Pathfinder Sp. z o. o.	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/727103277_1_261x203_rev007.jpg
412	MyGift	\N
413	TOMADEX S.C., TOMASZ SZULC, ADAM CZAJKA	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/1209620883_1_261x203_rev001.jpg
414	Dystrybutor odzieży sportowej	\N
415	OUTWORKING SA	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/793335725_1_261x203_rev004.jpg
416	DHL Supply Chain	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/811351066_1_261x203.jpg
417	Agata S.A.	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/565805492_1_261x203_rev001.jpg
418	QSense	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/826995135_1_261x203_rev006.jpg
419	K+L Biuro Handlowe Polska Spółka z o.o.	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/1499516263_1_261x203_rev001.jpg
420	DHL eCommerce (Poland) Sp. z o.o	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/548667692_1_261x203.jpg
421	Job Impulse Polska Sp. z o.o.	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/725929225_1_261x203.jpg
422	Euvic Services Sp. z o.o.	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/1128842283_1_261x203_rev005.jpg
423	Adecco Poland Sp. z o.o.	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/1169853095_1_261x203_rev018.jpg
424	FOLIMPEX sp. z o.o. sp. k.	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/750472896_1_261x203_rev003.jpg
425	MANGO	\N
426	SUPON S.A.	\N
431	STEAM Workforce sp. z o.o.	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/504747114_1_261x203_rev019.jpg
432	Jobman Group Sp. z o.o.	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/859868147_1_261x203.jpg
434	Grupa Progres	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/933886801_1_261x203_rev003.jpg
437	FDW SA	\N
439	A.B.Z. Sp. z o.o.	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/1198029728_1_261x203.jpg
440	DSV Services Sp. z o.o.	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/591499022_1_261x203_rev001.jpg
444	Manpower	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/1235380909_1_261x203_rev003.jpg
445	Bogmar BB Sp. z o.o. Sp.k.	\N
446	Schronisko Bukowina	\N
447	Firma produkcyjna	\N
448	DASKO Sp. z o.o.	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/1208062924_1_261x203.jpg
458	Rossmann SDP sp. z o.o.	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/585168836_1_261x203.jpg
459	Zakład Produkcyjny Domów Mobilnych	\N
466	BeFlexi Sp. z o.o.	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/1264695481_1_261x203.jpg
468	Mekonomen Poland	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/1426147955_1_261x203_rev001.jpg
469	MARTOM M. RYCHLIŃSKA I WSPÓLNICY	\N
471	KISZECZKA	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/765691847_1_261x203_rev002.jpg
473	Lewiatan	\N
474	Bricomarche	\N
479	HAMELIN Polska Sp. z o.o.	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/950276709_1_261x203_rev016.jpg
480	GSC Sp. Z O.O.	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/769038881_1_261x203_rev012.jpg
481	Sas Sp. z o.o.	\N
482	Przedsiębiorstwo Handlowe „AGA” Anna Gałka	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/799872099_1_261x203.jpg
483	PF Logo Express	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/545998744_1_261x203_rev008.jpg
484	Dagat-Eco	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/813412692_1_261x203_rev001.jpg
485	Tekaem	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/713706177_1_261x203_rev002.jpg
488	Hebe	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/1468119648_1_261x203.jpg
489	Noo.ma	\N
493	Steam Recruitment sp. z o.o.	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/1192223116_1_261x203_rev004.jpg
494	ABLER Sp. z o.o.	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/1133714872_1_261x203_rev002.jpg
496	Grupa Contrain	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/664423503_1_261x203_rev015.jpg
497	AQS Poland	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/1306911819_1_261x203_rev002.jpg
499	OTTO Work Force	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/1149667574_1_261x203_rev001.jpg
502	Saint-Gobain	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/419316741_1_261x203.jpg
503	Pomel Sp. z o.o.	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/1229016838_1_261x203_rev001.jpg
506	Adams Group	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/1191224251_1_261x203_rev007.jpg
507	M.M. Kraszewscy Sp.J.	\N
509	Axxo Fitness	\N
512	Stimeo Grzegorz Pintal	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/632383243_1_261x203.jpg
513	Lider w branży logistycznej	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/1088520927_1_261x203_rev002.jpg
518	EWL S.A.	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/1066188215_1_261x203_rev007.jpg
521	INTAR Sp. z o.o.	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/568626372_1_261x203.jpg
523	FAKRO SP. Z O.O.	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/570712172_1_261x203_rev006.jpg
527	FedEx Express Poland Sp. z.o.o.	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/429811701_1_261x203_rev008.jpg
528	Interkadra by Synergie	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/1493703970_1_261x203_rev004.jpg
529	Veln HR	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/1496588084_1_261x203_rev004.jpg
530	Alutec KK	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/1220985246_1_261x203_rev001.jpg
531	Trident BMC	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/1294291358_1_261x203_rev004.jpg
533	MGsolutions MGJJ Sp. z o.o. Sp. k.	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/1123677815_1_261x203_rev003.jpg
534	Ania Holding	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/984174471_1_261x203_rev006.jpg
535	Glosel	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/1302361139_1_261x203_rev003.jpg
536	Gi Group	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/1401870972_1_261x203_rev001.jpg
542	Wiodąca firma produkcyjno-handlowa	\N
543	JAGA Recruitment Sp. z o.o.	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/1483777888_1_261x203_rev002.jpg
550	Eurocash Cash &amp; Carry	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/917615704_1_261x203_rev002.jpg
553	LeasingTeam Group	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/1409127260_1_261x203_rev003.jpg
556	TIM GmbH	\N
557	"JAMS" Joanna Lewandowska	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/1223134058_1_261x203_rev008.jpg
560	INTERVIOL	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/947829438_1_261x203_rev002.jpg
564	HAVI	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/1519285029_1_261x203.jpg
565	Pipelife	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/1430233682_1_261x203_rev001.jpg
571	PHU Topaz Sp. z o.o.	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/1520485162_1_261x203.jpg
574	Wutech Sp. z o.o.	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/1502379429_1_261x203_rev001.jpg
575	Bilfinger Industrial Services Polska Sp. z o.o.	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/568241856_1_261x203_rev010.jpg
578	Best Partner	\N
583	SANTEX Sp. z o.o.	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/1083716575_1_261x203_rev005.jpg
585	FairWind	https://img-resizer.prd.01.eu-west-1.eu.olx.org/img-eu-olxpl-production/1127974141_1_261x203_rev002.jpg
590	Gi Group S.A.	/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDEwc_o0POLepFLXCdxQbV5QNHJUHNN4k3XgXRmJAltHCQqXRjQ0yn4VnfM4-KFQQmBXQEUpxcaBjZoJzpIuys0vHrAPAGFBiAtni4T8h5qpZ1Uq52Ep6Bz1Vh0PFGJEjg97XeeJXPa2U2izigtQ0zjPWiEt2Czi9GHqNkYzfM9mn8vDMvwry3ZVkExFgbwtGlM614S-hUQLbT7u7SD02MX5xiTlW50oERwr_jrIRiCqHtaqJ-b4jCuDDR3w8YKQpHCAR3Sg9Sa2c-cNLZqcveJDvF9lAsk_w8WubxFw5eUyGfaH3vVC-2zZdOiur4Ss5Z6-8OMR79p1sqM6dpin1LpoM8udlCrRl7rfBkBtsX_3CfjngFEQBJNc1G6c5hr5zQfxusYbym0xhRJBwDbpqNTnCvwrxuYbzZ1MJSLSyKxSPkB1uFn/952f31ec324e2b01e08bf66a95797441.webp
591	Dijo Baking Sp. z o.o.	/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDCy8_o0POLepFLXCdxQbZyQNHMUHtO6B_QgCU0JQxhSy96WRCF0z_tVSOKoODDTAWMS0JM7RQXRn0jJn9HtWIs_SmGIAzOWiFjiiBmuldmro8G7J-GqqQDj2ImIAWaGWAltymIbxiBj02gwW1zdQ-iYzmF_WCg0IbErNMhxOI2wRswWZ2ttzTJAxh7VRzwm2ABt0IK8F4QO8-pv5G22DVF4Fv-7js7kVVuip--JkvH4B07pJ2G8yyyXXxtlpkZQ9qbWEWZgovfkISeP-A5dKmdAKM0mAZt51pJ77hF1MbDxC6fBjONCeeqeobg-ehVooktsvPXE-1vn9mHncNzyR6h0MwmaleLDQLIJkgAwcSuhHqm1nBOSAlJIyuYOIMw9D9r3_tNKHz8pRtWGQSL3oIEgGPjpG-BcQ/168440412782924900.webp
729	DSA INVESTMENT	\N
730	Szkoła Języków Obcych Lingua House	\N
731	New Telemedicine sp. z o.o.	\N
732	DEEPLAI P.S.A.	\N
593	Michael Page International (Poland) Sp. z o.o.	/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDCy8_o0POLepFLXCdxQbV5QNHAUHtN6B7cgSc9LwprSyZ_Xx6F0z_tVSOKoODDTAWMS0JM7RQXRn0jJn9HtWIs_SmGIAzOWiFjiiBmuldmro8G7J-GqqQDj2ImIAWaGWAltymIbxiBj02gwW1zdQ-iYzmF_WCg0IbErNMhxOI2wRswWZ2ttzTJAxh7VRzwm2ABt0IK8F4QO8-pv5G22DVF4Fv-7js7kVVuip--JkvH4B07pJ2G8yyyXXxtlpkZQ9qbWEWZgovfkISeP-A5dKmdAKM0mAZt51pJ77hF1MbDxC6fBjONCeeqeobg-ehVooktsvPXE-1vn9mHncNzyR6h0MwmaleLDQLIJkgAwcSuhHqm1nBOSAlJIyuYOIMw9D9r3_tNKHz8pRtWGQSL3oIEgGPjpG-BcQ/158581688122072700.webp
596	CWBK	\N
598	BIOCEUM P.S.A.	\N
603	Grupa Azoty S.A.	/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDEwc_o0POLepFLXCdxQbV5QNDIUH1BshzQ0nRkJ107G3AtXRjT0CepViDP4euJQlODDA0VpxcaBjZoJzpIuys0vHrAPAGFBiAtni4T8h5qpZ1Uq52Ep6Bz1Vh0PFGJEjg97XeeJXPa2U2izigtQ0zjPWiEt2Czi9GHqNkYzfM9mn8vDMvwry3ZVkExFgbwtGlM614S-hUQLbT7u7SD02MX5xiTlW50oERwr_jrIRiCqHtaqJ-b4jCuDDR3w8YKQpHCAR3Sg9Sa2c-cNLZqcveJDvF9lAsk_w8WubxFw5eUyGfaH3vVC-2zZdOiur4Ss5Z6-8OMR79p1sqM6dpin1LpoM8udlCrRl7rfBkBtsX_3CfjngFEQBJNc1G6c5hr5zQfxusYbym0xhRJBwDbpqNTnCvwrxuYbzZ1MJSLSyKxSPkB1uFn/79b74bea0fbbfe01f363fa5c094c5e80.webp
605	CELMA INDUKTA Spółka Akcyjna	/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDCy8_o0POLepFLXCdxQbVwQNHMUHtO4h7cgSk9JA1sQS58XBmF0z_tVSOKoODDTAWMS0JM7RQXRn0jJn9HtWIs_SmGIAzOWiFjiiBmuldmro8G7J-GqqQDj2ImIAWaGWAltymIbxiBj02gwW1zdQ-iYzmF_WCg0IbErNMhxOI2wRswWZ2ttzTJAxh7VRzwm2ABt0IK8F4QO8-pv5G22DVF4Fv-7js7kVVuip--JkvH4B07pJ2G8yyyXXxtlpkZQ9qbWEWZgovfkISeP-A5dKmdAKM0mAZt51pJ77hF1MbDxC6fBjONCeeqeobg-ehVooktsvPXE-1vn9mHncNzyR6h0MwmaleLDQLIJkgAwcSuhHqm1nBOSAlJIyuYOIMw9D9r3_tNKHz8pRtWGQSL3oIEgGPjpG-BcQ/162581883658841000.webp
606	DocuSoft	/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDCy8_o0POLepFLXCdxQbVwQNHMUHtM4BvTiCk0IAhoTCN9Xx-F0z_tVSOKoODDTAWMS0JM7RQXRn0jJn9HtWIs_SmGIAzOWiFjiiBmuldmro8G7J-GqqQDj2ImIAWaGWAltymIbxiBj02gwW1zdQ-iYzmF_WCg0IbErNMhxOI2wRswWZ2ttzTJAxh7VRzwm2ABt0IK8F4QO8-pv5G22DVF4Fv-7js7kVVuip--JkvH4B07pJ2G8yyyXXxtlpkZQ9qbWEWZgovfkISeP-A5dKmdAKM0mAZt51pJ77hF1MbDxC6fBjONCeeqeobg-ehVooktsvPXE-1vn9mHncNzyR6h0MwmaleLDQLIJkgAwcSuhHqm1nBOSAlJIyuYOIMw9D9r3_tNKHz8pRtWGQSL3oIEgGPjpG-BcQ/140078817315552600.webp
607	Synthos S.A.	/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDCy8_o0POLepFLXCdxQbZxQNHAUHtO6RrUhCMwIAJpSCZ9VBGF0z_tVSOKoODDTAWMS0JM7RQXRn0jJn9HtWIs_SmGIAzOWiFjiiBmuldmro8G7J-GqqQDj2ImIAWaGWAltymIbxiBj02gwW1zdQ-iYzmF_WCg0IbErNMhxOI2wRswWZ2ttzTJAxh7VRzwm2ABt0IK8F4QO8-pv5G22DVF4Fv-7js7kVVuip--JkvH4B07pJ2G8yyyXXxtlpkZQ9qbWEWZgovfkISeP-A5dKmdAKM0mAZt51pJ77hF1MbDxC6fBjONCeeqeobg-ehVooktsvPXE-1vn9mHncNzyR6h0MwmaleLDQLIJkgAwcSuhHqm1nBOSAlJIyuYOIMw9D9r3_tNKHz8pRtWGQSL3oIEgGPjpG-BcQ/169104257901059800.webp
608	Klingspor Sp. z o.o.	/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDCy8_o0POLepFLXCdxQbVwQNHMUHtO5BjTgiEyIgNgQCZ7WBqF0z_tVSOKoODDTAWMS0JM7RQXRn0jJn9HtWIs_SmGIAzOWiFjiiBmuldmro8G7J-GqqQDj2ImIAWaGWAltymIbxiBj02gwW1zdQ-iYzmF_WCg0IbErNMhxOI2wRswWZ2ttzTJAxh7VRzwm2ABt0IK8F4QO8-pv5G22DVF4Fv-7js7kVVuip--JkvH4B07pJ2G8yyyXXxtlpkZQ9qbWEWZgovfkISeP-A5dKmdAKM0mAZt51pJ77hF1MbDxC6fBjONCeeqeobg-ehVooktsvPXE-1vn9mHncNzyR6h0MwmaleLDQLIJkgAwcSuhHqm1nBOSAlJIyuYOIMw9D9r3_tNKHz8pRtWGQSL3oIEgGPjpG-BcQ/164372075899035300.webp
609	Kujawsko-Pomorska Wojewódzka Komenda OHP	\N
611	WRC Polska Jacek Konieczny	\N
616	Śląska Wojewódzka Komenda Ochotniczych Hufców Pracy w Katowicach	\N
617	REGIONALNE CENTRUM INFORMATYKI Bydgoszcz / 11 Wojskowy Oddział Gospodarczy	\N
618	ŚLĄSKI UNIWERSYTET MEDYCZNY W KATOWICACH	\N
620	Wyższa Szkoła Ekonomiczno-Humanistyczna	\N
621	WRC POLSKA - JACEK KONIECZNY	\N
625	Szpital Wojewódzki im. Jana Pawła II w Bełchatowie	\N
626	ZARZĄD ZIELENI MIEJSKIEJ	\N
630	KOMENDA WOJEWÓDZKA POLICJI W RZESZOWIE	\N
632	GENERALNA DYREKCJA DRÓG KRAJOWYCH I AUTOSTRAD	\N
637	Muzeum Narodowe we Wrocławiu	\N
642	Instytut Biotechnologii Przemysłu Rolno-Spożywczego im. prof. Wacława Dąbrowskiego - Państwowy Instytut Badawczy	\N
644	Instytut Rozrodu Zwierząt i Badań Żywności Polskiej Akademii Nauk	\N
645	WODOCIĄGI LESZCZYŃSKIE sp. z o.o.	\N
646	Narodowy Instytut Onkologii im. Marii Skłodowskiej-Curie Państwowy Instytut Badawczy Oddział w Krakowie	\N
647	ADEX BEAUTY &amp; CARE sp. z o.o.	\N
648	Bioceum.com	\N
650	bioceum	\N
651	Uniwersytet Medyczny w Białymstoku	\N
652	Bioceum	\N
654	ManpowerGroup Sp. z o.o.	/media/6-2mJYa-_fQUmzbKho1Z7o2iuDoIoPmWSVxoPgtQiPwJumDCy8_o0POLepFLXCdxQbZxQNHLUHtP5hvSgCAxLwJtTSF-VRGF0z_tVSOKoODDTAWMS0JM7RQXRn0jJn9HtWIs_SmGIAzOWiFjiiBmuldmro8G7J-GqqQDj2ImIAWaGWAltymIbxiBj02gwW1zdQ-iYzmF_WCg0IbErNMhxOI2wRswWZ2ttzTJAxh7VRzwm2ABt0IK8F4QO8-pv5G22DVF4Fv-7js7kVVuip--JkvH4B07pJ2G8yyyXXxtlpkZQ9qbWEWZgovfkISeP-A5dKmdAKM0mAZt51pJ77hF1MbDxC6fBjONCeeqeobg-ehVooktsvPXE-1vn9mHncNzyR6h0MwmaleLDQLIJkgAwcSuhHqm1nBOSAlJIyuYOIMw9D9r3_tNKHz8pRtWGQSL3oIEgGPjpG-BcQ/176060148944768800.webp
660	Centralna Baza Ofert Pracy	\N
663	POLSKA GRUPA GÓRNICZA S.A.	\N
664	Regionalne Centrum Informatyki Kraków	\N
669	URZĄD MIASTA OŚWIĘCIM	\N
672	EKO-REGION sp. z o.o.	\N
674	Baza Lotnictwa Taktycznego	\N
683	juzjade.pl sp. z o.o.	\N
684	Regionalne Centrum Informatyki	\N
685	Wojskowe Biuro Emerytalne w Lublinie	\N
686	MEWA sp. z o.o. SPÓŁKA KOMANDYTOWA	\N
688	DATECH Systemy i Sieci Komputerowe Damian POŹNIAK	\N
689	EVEREST PRZEDSIĘBIORSTWO INFORMATYCZNE S.C. SZREITER MAREK, SZREITER HALINA	\N
690	GLOBTRAK POLSKA SPÓŁKA Z OGRANICZONĄ ODPOWIEDZIALNOŚCIĄ	\N
692	Optiq	\N
693	Lime Brains	\N
698	agencja Apollo	\N
699	Lingua House	\N
700	EPICVR.pl	\N
701	PUH Kastel	\N
702	Syntcom Polska	\N
703	Human4Human Recruitment	\N
706	Jarosław	\N
707	Machelp	\N
709	PracBaza.pl	\N
710	Instytut Katalizy i Fizykochemii Powierzchni PAN	\N
711	Sprint S.A.	\N
712	Chimide Polska Sp. z o.o.	\N
713	Play Dreams	\N
714	PracaStala.pl	\N
715	Studio Arcan Vision	\N
716	Prt Baza	\N
717	Urząd Miejski w Białymstoku	\N
718	Elkatel system	\N
719	HurtowniaPrzemyslowa.pl	\N
720	OutsourcingIT LTD	\N
721	NaszaSiec.NET Kraków	\N
722	Ukryty	\N
723	JNS Sp. z o.o.	\N
724	Solutions 30 Wschód Sp.z.o.o.	\N
725	TELEKOM USŁUGI SP.Z.O.O.	\N
726	VeritaHR	\N
733	Telecom Sp. z o.o.	\N
734	e-integra sp. z o. o.	\N
735	JSON CREW	\N
737	firma szkoleniowa Katowice	\N
738	Marcin	\N
739	AMTAK	\N
\.


COPY public.leading_categories (id, leading_category) FROM stdin;
328	Zarządzanie projektem/produktem
725	Przedstawiciel handlowy
\.

COPY public.postal_codes (id, postal_code) FROM stdin;
1	61-854
2	81-451
3	54-203
4	31-101
5	45-839
6	00-801
7	02-678
8	02-884
9	02-703
10	52-131
11	20-607
12	05-520
13	54-519
14	61-897
15	53-012
16	02-495
17	80-298
18	55-300
19	94-406
20	05-090
21	20-030
23	00-189
24	05-270
25	02-595
26	96-320
27	62-052
28	31-637
29	86-300
30	80-180
31	60-327
32	66-400
33	64-117
34	06-232
35	02-672
36	50-558
37	27-400
38	53-332
39	64-300
49	80-557
51	15-062
57	51-162
68	03-973
75	02-305
143	80-864
189	32-050
192	40-217
202	60-355
211	01-646
212	40-684
213	04-462
214	80-601
215	04-175
216	80-233
217	01-211
218	63-100
219	02-690
220	02-231
221	76-200
222	87-100
223	20-006
224	02-486
226	00-113
227	48-385
229	63-300
230	00-195
232	50-077
233	04-767
234	00-133
236	86-200
237	00-105
238	00-843
240	06-230
241	83-200
242	63-900
243	35-111
244	00-613
245	32-864
246	01-234
247	61-485
248	03-828
249	31-280
250	40-112
251	43-150
253	44-100
254	21-003
255	31-207
257	00-272
258	37-600
259	90-203
260	02-093
261	01-309
262	04-934
263	00-121
264	01-748
266	61-569
267	01-836
268	20-326
269	10-719
270	40-706
272	01-102
273	01-066
274	35-213
275	03-866
276	50-370
277	40-431
278	93-558
279	03-259
280	02-520
281	61-693
282	10-228
284	50-981
286	60-650
290	00-116
292	00-844
293	00-807
298	53-611
299	00-838
300	03-738
302	00-718
304	60-815
313	31-004
314	85-796
315	02-676
316	01-651
317	00-773
320	02-147
321	02-092
323	77-400
329	01-305
331	52-434
333	03-734
335	02-454
336	20-234
338	31-586
339	40-584
340	05-500
341	02-904
343	05-850
344	02-273
345	02-674
347	00-149
351	02-002
355	00-342
357	31-864
358	02-626
363	40-065
364	02-981
366	02-822
367	49-318
369	01-045
371	03-230
372	00-854
373	60-840
374	04-051
375	02-516
376	31-564
377	01-217
378	62-004
379	62-080
381	33-300
383	03-728
385	90-361
386	61-696
389	02-715
395	50-951
396	19-300
397	62-023
399	179
401	62-310
404	18-210
405	39-120
\.

COPY public.streets (id, street) FROM stdin;
1	Mostowa
2	AL. ZWYCIĘSTWA
3	Legnicka
4	Straszewskiego
5	ul. Technologiczna
6	VARSO 2, ul. Chmielna 73
7	ul. Szturmowa 2
8	Puławska
9	Bukowińska
10	Buforowa
11	ul. Konrada Wallenroda 4c
12	ul. Warszawska 165
13	ul. Jerzmanowska 2
14	ul. Składowa 5
15	Wyścigowa 58
16	Posag 7 Panien
17	Juliusza Słowackiego 199
18	Komorniki, ul. Polna
19	Kinga C. Gillette
20	Sękocin Stary, Leśników
21	Obrońców Pokoju
23	Inflancka 4b
24	ul. Duża
26	Tarczyńska 111A
27	Wiśniowa 11
28	Okulickiego
29	Parkowa
30	Magnacka
31	Marszałkowska
32	Franciszka Walczaka
33	ul. Mórkowska 3
34	Dąbrówka
35	ul. Domaniewska 48
36	Borowska
37	Kolejowa
38	Powstańców Śląskich
39	ul. Leśna 35, Paproć, k. Nowego Tomyśla
49	Narwicka
51	Warszawska
57	Jana Długosza
68	Brukselska
75	Al. Jerozolimskie
143	Jana z Kolna
189	Torowa
192	Wrocławska
202	Przybyszewskiego
211	Edwarda Jelinka
212	gen. Zygmunta Waltera Jankego
213	Strażacka
214	ul. Kontenerowa 7
215	Ostrobramska 75C
216	Gmach Główny, ul. G.Narutowicza 11/12
217	Giełdowa
218	Wiosenna
219	Bokserska
220	Jutrzenki 105
221	Europejska
222	ul. Gagarina 11
223	ul. Hugo Kołłątaja
224	Aleje Jerozolimskie 180, Kopernik Office Buildings, wejście A
226	ul. Emilii Plater 53
227	ul. Nyska 21
229	KRASZEWSKIEGO
230	ul. Słomińskiego 19 lok. 524
232	Kazimierza Wielkiego 3
233	Patriotów
234	Aleja Jana Pawła II 22
235	ul. Giełdowa 7/9
236	ul. Magazynowa 2
237	Twarda
238	Grafton Recruitment Sp z.o.o., rondo Ignacego Daszyńskiego 1
240	Kaszewiec
241	Zblewska
242	Plac Wolności
243	Krakowska
244	Tytusa Chałubińskiego
245	Gnojnik
246	Marcina Kasprzaka
247	28 Czerwca 1956 r. 223/229
248	Mińska
249	ŁOKIETKA
250	Morelowa 25
251	Ekonomiczna 20
253	Jana Pawła II
254	Elizówka, ul. Szafranowa 6
255	ks. Kazimierza Siemaszki
256	Jutrzenki
257	Rynek Starego Miasta
258	Rynek
259	Pomorska
260	ul. Paustera 3
261	Okrętowa
262	Bystrzycka
263	Sienna
264	Szamocka
266	ul. Wierzbięcice 1B
267	Kasprowicza 47
268	Dulęby
269	Michała Oczapowskiego
270	ul. Kolejowa 17
271	T. Chałubińskiego 8
272	Jana Olbrachta 94
273	Żubra
274	Biznesowa
275	Księżnej Anny 3
276	Wybrzeże Wyspiańskiego
277	Szopienicka
278	Piękna
279	Szlachecka 45
280	Wiśniowa
281	Trójpole
282	Aleja Wojska Polskiego
283	Aleje Jerozolimskie
284	Rudolfa Weigla
286	Piątkowska
289	rondo I. Daszyńskiego 1
290	Świętokrzyska 36
292	ul. Grzybowska 49
295	Wyścigowa
296	Przewozowa
298	ul. Strzegomska 55
299	ul. Prosta 67
300	Kijowska 1
302	ul. Czerniakowska 87A
304	Gajowa
309	Prosta 70
313	pl. Wszystkich Świętych 3-4
314	ul. Pod Skarpą 51A
315	Postępu 18b, budynek Mokotów Three
316	Gwiaździsta
317	ul. Dolna 3
320	Wieżowa
321	ul. Żwirki i Wigury 16
323	ul. Za Dworcem 1D
325	ul. Żubra 1
326	Postępu 12A
329	Batalionów Chłopskich
331	Leona Petrażyckiego 57
333	Targowa
334	Kasprzaka 2
335	Szczęsna
336	Mełgiewska
338	ul. Galicyjska 1
339	Brynowska
340	Puszczyka
341	BERNARDYŃSKA
342	ul. Inflancka 4a
343	ul. Poznańska 129/133
344	Muszkieterów 15
345	Marynarska 12
346	Aleje Jerozolimskie 142A
347	Karmelicka
351	Nowogrodzka
355	ul. Topiel 12
357	Al. Jana Pawła II 39A
358	al. Niepodległości 69
363	Mikołowska
364	ul. Zawodzie 22
366	Poleczki
367	Technologiczna 1
369	Kolska
371	Daniszewska 25
372	Jana Pawła II 17
373	ul. Jana Henryka Dąbrowskiego
374	ul. Poligonowa 30
375	Tadeusza Rejtana
376	Aleja Pokoju 62/8
378	Pogodna
379	Rolna 6
381	Henryka Siemiradzkiego
383	ul. Targowa 25
385	Piotrkowska 270
386	Aleje Solidarności 46
388	Grzybowska
389	Puławska 145
392	ul Żwirki i Wigury 16a
393	ul. Inflancka 4A
395	Magazynowa
396	Księcia Witolda 56
397	Wiśniowieckiego
398	Poleczki 23
399	Stoczniowa
400	Hutnicza
401	Piastowska
402	Legionów
403	3 Maja 23
404	Józefów
405	Stachowska
406	Mosiężna
413	Janów
414	Bobrzyńskiego
415	Wschodnia
416	Wrzesińska
419	Zakładowa
421	Jędrzejowska
422	Grudziądzka
423	Stargardzka
424	Mosiężna 3
426	Janówka
428	Weteranów
430	Cicha
432	Welurowa
433	Kasztanowa
434	Jędrzejowska 43a
435	Księcia Witolda
436	Modlińska
437	plac Anny Jagiellonki
438	Wspólna
439	Lubczyńska
440	Zaborska
441	Wróblewskiego
\.

COPY public.location_details (id, building_number, latitude, longitude, city_id, street_id, postal_code_id) FROM stdin;
1	38	52.40751266479492	16.93880844116211	1	1	1
2	98	54.49552917480469	18.53768539428711	2	2	2
3	B4	51.124637603759766	16.986879348754883	3	3	3
4	2	50.05651092529297	19.934123992919922	4	4	4
5	2A	50.68358612060547	17.873929977416992	5	5	5
6	\N	52.22861459167414	20.99851346208569	6	6	6
7	\N	52.1702276	21.0138413	6	7	7
8	474	52.11853790283203	21.017568588256836	6	8	8
9	1B	52.1810302734375	21.026180267333984	6	9	9
10	H-10a	51.043724060058594	17.059825897216797	10	10	10
11	\N	51.24001900063648	22.52640355462126	11	11	11
12	\N	52.11093864136604	21.111103004623715	12	12	12
13	\N	51.126101079433816	16.913172363426778	3	13	13
14	\N	52.405702860195824	16.91553390509625	1	14	14
15	\N	51.06342950505281	17.00419596457848	3	15	15
16	1	52.205528259277344	20.886314392089844	6	16	16
17	\N	54.38321590818158	18.47355256387636	17	17	17
18	17B	51.16850280761719	16.618589401245117	18	18	18
19	9	51.7412109375	19.368833541870117	19	19	19
20	21C	52.11110305786133	20.85918617248535	20	20	20
21	2	51.244667053222656	22.545616149902344	11	21	21
22	21C	52.11110305786133	20.85918617248535	20	20	20
23	\N	52.25568280108792	20.993632208005845	6	23	23
24	39	\N	\N	24	24	24
25	99	\N	\N	6	8	25
26	\N	51.97412995661858	20.540919907925996	26	26	26
27	\N	52.348069361544	16.810509335580598	27	27	27
28	66	50.09375762939453	20.026453018188477	4	28	28
29	56	50.57417678833008	19.318586349487305	29	29	29
30	35	\N	\N	30	30	30
31	3A	52.39876937866211	16.871339797973633	1	31	31
32	42	52.73102569580078	15.239458084106445	32	32	32
33	\N	51.9093805	16.4547111	33	33	33
34	55A	\N	\N	34	34	34
35	\N	52.18409539809073	20.9970563738057	6	35	35
36	99	51.0909423828125	17.03231430053711	3	36	36
37	20	50.93378448486328	21.373096466064453	37	37	37
38	9	51.09702682495117	17.023624420166016	3	38	38
39	\N	52.3028015	16.1411165	39	39	39
40	38	52.40751266479492	16.93880844116211	1	1	1
41	98	54.49552917480469	18.53768539428711	2	2	2
42	B4	51.124637603759766	16.986879348754883	3	3	3
43	2	50.05651092529297	19.934123992919922	4	4	4
44	2A	50.68358612060547	17.873929977416992	5	5	5
45	\N	52.22861459167414	20.99851346208569	6	6	6
46	\N	52.1702276	21.0138413	6	7	7
47	474	52.11853790283203	21.017568588256836	6	8	8
48	1B	52.1810302734375	21.026180267333984	6	9	9
49	2	54.38386535644531	18.6373348236084	17	49	49
50	H-10a	51.043724060058594	17.059825897216797	10	10	10
51	7U	53.13440704345703	23.168764114379883	51	51	51
52	\N	51.24001900063648	22.52640355462126	11	11	11
53	\N	51.126101079433816	16.913172363426778	3	13	13
54	\N	52.405702860195824	16.91553390509625	1	14	14
55	\N	51.06342950505281	17.00419596457848	3	15	15
56	1	52.205528259277344	20.886314392089844	6	16	16
57	42-46	51.134098052978516	17.06325340270996	3	57	57
58	\N	54.38321590818158	18.47355256387636	17	17	17
59	17B	51.16850280761719	16.618589401245117	18	18	18
60	9	51.7412109375	19.368833541870117	19	19	19
61	21C	52.11110305786133	20.85918617248535	20	20	20
62	2	51.244667053222656	22.545616149902344	11	21	21
63	21C	52.11110305786133	20.85918617248535	20	20	20
64	\N	52.25568280108792	20.993632208005845	6	23	23
65	99	\N	\N	6	8	25
66	\N	51.97412995661858	20.540919907925996	26	26	26
67	\N	52.348069361544	16.810509335580598	27	27	27
68	14	52.2248420715332	21.063331604003906	6	68	68
69	56	50.57417678833008	19.318586349487305	29	29	29
70	35	\N	\N	30	30	30
71	3A	52.39876937866211	16.871339797973633	1	31	31
72	42	52.73102569580078	15.239458084106445	32	32	32
73	\N	51.9093805	16.4547111	33	33	33
74	55A	\N	\N	34	34	34
75	132	\N	\N	6	75	75
76	99	51.0909423828125	17.03231430053711	3	36	36
77	9	51.09702682495117	17.023624420166016	3	38	38
78	\N	52.3028015	16.1411165	39	39	39
79	38	52.40751266479492	16.93880844116211	1	1	1
80	98	54.49552917480469	18.53768539428711	2	2	2
81	B4	51.124637603759766	16.986879348754883	3	3	3
82	2	50.05651092529297	19.934123992919922	4	4	4
83	2A	50.68358612060547	17.873929977416992	5	5	5
84	\N	52.22861459167414	20.99851346208569	6	6	6
85	\N	52.1702276	21.0138413	6	7	7
86	474	52.11853790283203	21.017568588256836	6	8	8
87	1B	52.1810302734375	21.026180267333984	6	9	9
88	2	54.38386535644531	18.6373348236084	17	49	49
89	H-10a	51.043724060058594	17.059825897216797	10	10	10
90	7U	53.13440704345703	23.168764114379883	51	51	51
91	\N	51.24001900063648	22.52640355462126	11	11	11
92	\N	52.11093864136604	21.111103004623715	12	12	12
93	\N	51.126101079433816	16.913172363426778	3	13	13
94	\N	52.405702860195824	16.91553390509625	1	14	14
95	\N	51.06342950505281	17.00419596457848	3	15	15
96	1	52.205528259277344	20.886314392089844	6	16	16
97	42-46	51.134098052978516	17.06325340270996	3	57	57
98	\N	54.38321590818158	18.47355256387636	17	17	17
99	17B	51.16850280761719	16.618589401245117	18	18	18
100	9	51.7412109375	19.368833541870117	19	19	19
101	21C	52.11110305786133	20.85918617248535	20	20	20
102	2	51.244667053222656	22.545616149902344	11	21	21
103	21C	52.11110305786133	20.85918617248535	20	20	20
104	\N	52.25568280108792	20.993632208005845	6	23	23
105	99	\N	\N	6	8	25
106	\N	51.97412995661858	20.540919907925996	26	26	26
107	\N	52.348069361544	16.810509335580598	27	27	27
108	14	52.2248420715332	21.063331604003906	6	68	68
109	66	50.09375762939453	20.026453018188477	4	28	28
110	56	50.57417678833008	19.318586349487305	29	29	29
111	35	\N	\N	30	30	30
112	3A	52.39876937866211	16.871339797973633	1	31	31
113	42	52.73102569580078	15.239458084106445	32	32	32
114	\N	51.9093805	16.4547111	33	33	33
115	55A	\N	\N	34	34	34
116	132	\N	\N	6	75	75
117	99	51.0909423828125	17.03231430053711	3	36	36
118	9	51.09702682495117	17.023624420166016	3	38	38
119	\N	52.3028015	16.1411165	39	39	39
120	38	52.40751266479492	16.93880844116211	1	1	1
121	98	54.49552917480469	18.53768539428711	2	2	2
122	B4	51.124637603759766	16.986879348754883	3	3	3
123	2	50.05651092529297	19.934123992919922	4	4	4
124	2A	50.68358612060547	17.873929977416992	5	5	5
125	\N	52.22861459167414	20.99851346208569	6	6	6
126	\N	52.1702276	21.0138413	6	7	7
127	474	52.11853790283203	21.017568588256836	6	8	8
128	1B	52.1810302734375	21.026180267333984	6	9	9
129	2	54.38386535644531	18.6373348236084	17	49	49
130	H-10a	51.043724060058594	17.059825897216797	10	10	10
131	7U	53.13440704345703	23.168764114379883	51	51	51
132	\N	51.24001900063648	22.52640355462126	11	11	11
133	\N	52.11093864136604	21.111103004623715	12	12	12
134	\N	51.126101079433816	16.913172363426778	3	13	13
135	\N	52.405702860195824	16.91553390509625	1	14	14
136	\N	51.06342950505281	17.00419596457848	3	15	15
137	1	52.205528259277344	20.886314392089844	6	16	16
138	42-46	51.134098052978516	17.06325340270996	3	57	57
139	\N	54.38321590818158	18.47355256387636	17	17	17
140	17B	51.16850280761719	16.618589401245117	18	18	18
141	9	51.7412109375	19.368833541870117	19	19	19
142	21C	52.11110305786133	20.85918617248535	20	20	20
143	11	52.15570831298828	21.076324462890625	17	143	143
144	2	51.244667053222656	22.545616149902344	11	21	21
145	\N	52.25568280108792	20.993632208005845	6	23	23
146	39	\N	\N	24	24	24
147	99	\N	\N	6	8	25
148	\N	51.97412995661858	20.540919907925996	26	26	26
149	\N	52.348069361544	16.810509335580598	27	27	27
150	14	52.2248420715332	21.063331604003906	6	68	68
151	66	50.09375762939453	20.026453018188477	4	28	28
152	35	\N	\N	30	30	30
153	3A	52.39876937866211	16.871339797973633	1	31	31
154	42	52.73102569580078	15.239458084106445	32	32	32
155	\N	51.9093805	16.4547111	33	33	33
156	55A	\N	\N	34	34	34
157	\N	52.18409539809073	20.9970563738057	6	35	35
158	132	\N	\N	6	75	75
159	99	51.0909423828125	17.03231430053711	3	36	36
160	9	51.09702682495117	17.023624420166016	3	38	38
161	\N	52.3028015	16.1411165	39	39	39
162	38	52.40751266479492	16.93880844116211	1	1	1
163	98	54.49552917480469	18.53768539428711	2	2	2
164	B4	51.124637603759766	16.986879348754883	3	3	3
165	2	50.05651092529297	19.934123992919922	4	4	4
166	2A	50.68358612060547	17.873929977416992	5	5	5
167	\N	52.22861459167414	20.99851346208569	6	6	6
168	\N	52.1702276	21.0138413	6	7	7
169	474	52.11853790283203	21.017568588256836	6	8	8
170	1B	52.1810302734375	21.026180267333984	6	9	9
171	2	54.38386535644531	18.6373348236084	17	49	49
172	H-10a	51.043724060058594	17.059825897216797	10	10	10
173	7U	53.13440704345703	23.168764114379883	51	51	51
174	7U	53.13440704345703	23.168764114379883	51	51	51
175	\N	51.24001900063648	22.52640355462126	11	11	11
176	\N	51.126101079433816	16.913172363426778	3	13	13
177	\N	52.405702860195824	16.91553390509625	1	14	14
178	\N	51.06342950505281	17.00419596457848	3	15	15
179	1	52.205528259277344	20.886314392089844	6	16	16
180	42-46	51.134098052978516	17.06325340270996	3	57	57
181	\N	54.38321590818158	18.47355256387636	17	17	17
182	17B	51.16850280761719	16.618589401245117	18	18	18
183	9	51.7412109375	19.368833541870117	19	19	19
184	21C	52.11110305786133	20.85918617248535	20	20	20
185	11	52.15570831298828	21.076324462890625	17	143	143
186	2	51.244667053222656	22.545616149902344	11	21	21
187	21C	52.11110305786133	20.85918617248535	20	20	20
188	\N	52.25568280108792	20.993632208005845	6	23	23
189	45	49.980323791503906	19.824874877929688	189	189	189
190	39	\N	\N	24	24	24
191	99	\N	\N	6	8	25
192	54	50.26580810546875	19.04872703552246	192	192	192
193	\N	51.97412995661858	20.540919907925996	26	26	26
194	\N	52.348069361544	16.810509335580598	27	27	27
195	14	52.2248420715332	21.063331604003906	6	68	68
196	66	50.09375762939453	20.026453018188477	4	28	28
197	56	50.57417678833008	19.318586349487305	29	29	29
198	35	\N	\N	30	30	30
199	3A	52.39876937866211	16.871339797973633	1	31	31
200	42	52.73102569580078	15.239458084106445	32	32	32
201	\N	51.9093805	16.4547111	33	33	33
202	49	\N	\N	1	202	202
203	55A	\N	\N	34	34	34
204	\N	52.18409539809073	20.9970563738057	6	35	35
205	132	\N	\N	6	75	75
206	99	51.0909423828125	17.03231430053711	3	36	36
207	\N	52.1702276	21.0138413	6	7	7
208	9	51.09702682495117	17.023624420166016	3	38	38
209	\N	52.3028015	16.1411165	39	39	39
210	\N	52.1702276	21.0138413	6	7	7
211	8	52.17807388305664	21.073030471801758	6	211	211
212	211	50.207733154296875	18.970539093017578	192	212	212
213	53	52.25920486450195	21.12946891784668	6	213	213
214	\N	54.381765597468736	18.712978438942425	17	214	214
215	\N	52.2315458	21.1061513	6	215	215
216	\N	54.371428964168125	18.619134154770844	17	216	216
217	7/9	50.09541702270508	19.95317268371582	6	217	217
218	12	52.072723388671875	17.035316467285156	218	218	218
219	71	\N	\N	6	219	219
220	\N	52.19244170360378	20.92520881412317	6	220	220
221	10	54.46043395996094	17.09033203125	221	221	221
222	\N	53.0184871	18.5708801	222	222	222
223	2	51.22553634643555	22.650012969970703	11	223	223
224	\N	52.2023807	20.936931	6	224	224
225	\N	52.3028015	16.1411165	39	39	39
226	\N	52.23354863356175	21.001854305084187	6	226	226
227	\N	50.4667462	17.1787576	227	227	227
228	\N	52.1702276	21.0138413	6	7	7
229	11	51.89748764038086	17.78725242614746	229	229	229
230	\N	52.25519185996245	20.985301706308128	6	230	230
231	\N	52.1702276	21.0138413	6	7	7
232	\N	51.11075809739451	17.026306946625887	3	232	232
233	303	52.20350646972656	21.16823959350586	6	233	233
234	\N	52.23559419655106	20.99809225407567	6	234	234
235	\N	52.23031039807981	20.974225218309357	6	235	217
236	\N	53.339240657159685	18.462117990111864	236	236	236
237	18	\N	\N	6	237	237
238	\N	52.23019500616744	20.985784991439708	6	238	238
239	\N	52.23571567188366	20.998002269473098	6	234	234
240	5	52.886260986328125	21.425090789794922	240	240	240
241	9	53.96395492553711	18.481874465942383	241	241	241
242	5B	\N	\N	242	242	242
243	154	\N	\N	243	243	243
244	8	\N	\N	6	244	244
245	699	\N	\N	245	245	245
246	325	\N	\N	6	246	246
247	\N	52.3791478239099	16.909509416244354	1	247	247
248	65	52.25143051147461	21.067811965942383	6	248	248
249	83	\N	\N	4	249	249
250	\N	50.27498394288481	19.005262013694203	192	250	250
251	\N	50.10381725325958	19.10738436957248	251	251	251
252	\N	50.10381725325958	19.10738436957248	251	251	251
253	2	\N	\N	253	253	253
254	\N	51.2909458	22.5812171	254	254	254
255	19	50.08539581298828	19.940654754638672	4	255	255
256	139	52.20079803466797	20.936769485473633	6	256	220
257	31	52.249752044677734	21.011268615722656	6	257	257
258	28	\N	\N	258	258	258
259	41	51.777496337890625	19.464324951171875	19	259	259
260	\N	52.213735	20.9822891	6	260	260
261	19A	\N	\N	6	261	261
262	28B	52.206180572509766	21.169601440429688	6	262	262
263	39	52.23187255859375	21.00172233581543	6	263	263
264	3, 5	52.2567253112793	20.95976448059082	6	264	264
265	\N	52.11093864136604	21.111103004623715	12	12	12
266	\N	52.4001879	16.9208406	1	266	266
267	\N	52.28071390021524	20.950710145212163	6	267	267
268	2a	\N	\N	11	268	268
269	10	\N	\N	269	269	269
270	\N	50.22471633682451	18.982845527041945	192	270	270
271	\N	52.2256304719486	21.003174310779613	6	271	244
272	\N	52.23482189897278	20.928219935775726	6	272	272
273	1	\N	\N	6	273	273
274	6	50.073822021484375	21.9659366607666	243	274	274
275	\N	52.25993283653279	21.0742145682979	6	275	275
276	27	51.10789489746094	17.06169891357422	3	276	276
277	77	\N	\N	192	277	277
278	1	51.7376594543457	19.457975387573242	19	278	278
279	\N	52.31442994583054	21.021942365534247	6	279	279
280	50	52.24590301513672	20.993131637573242	6	280	280
281	21	\N	\N	1	281	281
282	37	53.79378128051758	20.48305320739746	269	282	282
283	142 B	52.219505310058594	20.96807098388672	6	283	75
284	5	51.07529067993164	17.023408889770508	3	284	284
285	132	51.07789611816406	16.995990753173828	6	283	75
286	161	52.43588638305664	16.91188621520996	1	286	286
287	132	\N	\N	6	75	75
288	474	52.11881637573242	21.017589569091797	6	8	8
289	\N	52.23021873713528	20.9857447118879	6	289	238
290	\N	52.2340851	21.000051	6	290	290
291	\N	52.22861459167414	20.99851346208569	6	6	6
292	\N	52.2339918	20.9896013	6	292	292
293	94	52.22268295288086	20.97937774658203	6	283	293
294	\N	52.23399180000001	20.9896013	6	292	292
295	58	51.06342950505281	17.00419596457848	3	295	15
296	32	50.29130172729492	18.705472946166992	253	296	253
297	22	\N	\N	6	253	234
298	\N	51.114376886339954	16.992334453115863	3	298	298
299	\N	52.229519856980524	20.983590350758018	6	299	299
300	\N	52.25021251260858	21.042863497285385	6	300	300
301	142B	\N	\N	6	283	75
302	\N	52.2041993	21.0487201	6	302	302
303	\N	52.2340851	21.000051	6	290	290
304	6	52.408714294433594	16.907833099365234	1	304	304
305	3, 5	52.2567253112793	20.95976448059082	6	264	264
306	\N	52.2023807	20.936931	6	224	224
307	142B	\N	\N	6	283	75
308	142B	\N	\N	6	283	75
309	\N	52.229906	20.9800267	6	309	299
310	142B	\N	\N	6	283	75
311	142B	\N	\N	6	283	75
312	105	51.15711212158203	17.153141021728516	6	256	220
313	\N	50.05862391524567	19.936943004628915	4	313	313
314	\N	53.15660613435117	18.106980437446946	314	314	314
315	\N	52.1825210175717	20.998461269449077	6	315	315
316	19	52.28323745727539	20.9805965423584	6	316	316
317	\N	52.20145003316046	21.03388109827202	6	317	317
318	3, 5	52.2567253112793	20.95976448059082	6	264	264
319	\N	52.2023807	20.936931	6	224	224
320	8	52.16248321533203	20.95944595336914	6	320	320
321	\N	52.1894701436823	20.98216825579587	6	321	321
322	\N	52.2023807	20.936931	6	224	224
323	\N	53.35792180041965	17.05590541164512	323	323	323
324	\N	53.35792180041965	17.05590541164512	323	323	323
325	\N	52.25554554794194	20.98077382436764	6	325	273
326	\N	52.176532733609584	20.996248099999775	6	326	315
327	11	52.15570831298828	21.076324462890625	17	143	143
328	132	\N	\N	6	75	75
329	11	\N	\N	6	329	329
330	\N	52.23021873713528	20.9857447118879	6	289	238
331	\N	51.07508682675624	16.965397904527556	3	331	331
332	11	52.15570831298828	21.076324462890625	17	143	143
333	74	\N	\N	6	333	333
334	\N	52.22964086523426	20.975989567075324	6	334	217
335	26	52.21015167236328	20.93212127685547	6	335	335
336	104	51.247947692871094	22.639881134033203	11	336	336
337	14	52.2248649597168	21.06333351135254	6	68	68
338	\N	50.06307676142318	20.00765253557085	4	338	338
339	72	\N	\N	192	339	339
340	1	\N	\N	340	340	340
341	U1	52.19533157348633	21.06261444091797	341	341	341
342	\N	52.256110771233764	20.99503775190103	6	342	23
343	\N	52.20767118905539	20.81643172611833	343	343	343
344	\N	52.179129982599754	20.9364433148305	6	344	344
345	\N	52.18065425126357	20.99306353533842	6	345	345
346	\N	52.218840936388176	20.966573742881646	6	346	75
347	3	52.263946533203125	20.921857833862305	6	347	347
348	132	51.07789611816406	16.995990753173828	6	283	75
349	\N	52.229906	20.9800267	6	309	299
350	132	\N	\N	6	75	75
351	62C	53.13425064086914	23.14788055419922	6	351	351
352	\N	52.25554554794194	20.98077382436764	6	325	273
353	\N	52.2023807	20.936931	6	224	224
354	\N	52.18065425126357	20.99306353533842	6	345	345
355	\N	52.23861572059925	21.025370149294314	6	355	355
356	\N	52.22861459167414	20.99851346208569	6	6	6
357	\N	50.07709628252856	19.99432522365506	4	357	357
358	\N	52.190647288980415	21.01590064057574	6	358	358
359	9	\N	\N	3	38	38
360	9	\N	\N	3	38	38
361	\N	52.190647288980415	21.01590064057574	6	358	358
362	\N	52.2339918	20.9896013	6	292	292
363	100	50.248573303222656	19.00031280517578	192	363	363
364	\N	52.1842574201016	21.083739019090366	6	364	364
365	1	\N	\N	340	340	340
366	13	52.12015151977539	21.01760482788086	6	366	366
367	\N	50.83630705673183	17.39527575768733	367	367	367
368	\N	52.229906	20.9800267	6	309	299
369	12	52.25102615356445	20.975749969482422	6	369	369
370	\N	52.2023807	20.936931	6	224	224
371	\N	52.3115477377508	21.019118235697544	6	371	371
372	\N	52.234148726049945	20.997293144892968	6	372	372
373	29	52.4119987487793	16.908397674560547	1	373	373
374	\N	52.234577385423826	21.087305439614898	6	374	374
375	17	\N	\N	6	375	375
376	\N	50.0657019	19.9936235	4	376	376
377	17	52.22421646118164	20.973539352416992	6	37	377
378	7	\N	\N	378	378	378
379	\N	52.442083267345176	16.733201760158074	379	379	379
381	11	\N	\N	381	381	381
382	132	\N	\N	6	75	75
383	\N	52.2497506	21.0411579	6	383	383
384	\N	52.23021873713528	20.9857447118879	6	289	238
385	\N	51.749323610536	19.461547731291265	19	385	385
386	\N	52.4354571158977	16.924705769605215	1	386	386
387	\N	52.20145003316046	21.03388109827202	6	317	317
388	87	\N	\N	6	388	292
389	\N	52.1808061	21.0233452	6	389	389
390	\N	52.1808061	21.0233452	6	389	389
391	\N	52.22861459167414	20.99851346208569	6	6	6
392	\N	52.18928123855536	20.983044576142667	6	392	321
393	\N	52.25666775187023	20.994469508324617	6	393	23
394	12	52.25102615356445	20.975749969482422	6	369	369
395	\N	51.7095	17.04984	395	\N	\N
396	\N	51.28176	17.99116	396	\N	\N
397	10	52.23487	17.15632	397	395	\N
398	\N	51.11464	17.06436	3	396	395
399	\N	50.87056	20.62798	399	\N	\N
400	\N	49.82198	19.05043	400	\N	\N
401	\N	50.33793	18.58702	253	\N	\N
402	\N	53.43138	14.54948	402	\N	\N
403	119	49.6294	20.69075	381	397	\N
404	\N	50.31558	18.78669	404	\N	\N
405	\N	51.23955	22.55257	11	\N	\N
406	\N	52.41379	16.90266	1	\N	\N
407	\N	52.15695	21.004282	6	398	\N
408	\N	53.80957	22.33548	408	399	396
409	\N	52.269497	21.075594	6	\N	\N
410	\N	51.86824	19.32839	410	\N	\N
411	\N	52.30344	21.160725	411	\N	\N
412	\N	51.65217	17.81083	412	\N	\N
413	100	53.1342	17.99035	314	400	\N
414	\N	50.58448	22.05793	414	\N	\N
415	\N	51.184902	16.163116	415	\N	\N
416	\N	54.31777	18.63075	17	\N	\N
417	\N	51.20701	16.15532	415	\N	\N
418	\N	\N	\N	418	401	\N
419	\N	50.8008	19.12205	419	402	\N
420	\N	52.41667	16.8812	1	\N	\N
421	\N	52.245052	21.294981	421	403	\N
422	\N	52.19282	21.03232	6	\N	\N
423	\N	53.19402	14.93462	423	\N	\N
424	\N	51.14865	15.01585	424	\N	\N
425	\N	50.07567	19.93084	4	\N	\N
426	\N	50.2754	19.06336	192	\N	\N
427	\N	50.332344	19.127983	427	\N	\N
428	\N	52.23876	21.08553	6	\N	\N
429	\N	50.40652	19.1275	427	\N	\N
430	\N	50.33012	19.03136	430	\N	\N
431	3c	51.72091	19.46619	19	404	\N
432	\N	52.44051	16.74803	432	\N	\N
433	\N	52.32129	21.10589	24	\N	\N
434	\N	53.1342	17.99035	314	\N	\N
435	\N	52.57973	16.83561	435	\N	\N
436	17C	52.36314	17.0227	436	405	397
437	\N	52.65346	17.96175	437	\N	\N
438	3	52.72918	15.24028	32	406	\N
439	17C	52.52918	17.59895	436	405	397
440	\N	53.07736	21.55409	440	\N	\N
441	\N	52.47214	21.28653	441	\N	\N
442	\N	49.47464	20.054848	442	\N	\N
443	\N	51.63815	17.89023	443	\N	\N
444	\N	52.38035	16.60985	444	\N	\N
445	\N	52.15695	21.004282	6	398	\N
446	\N	50.31558	18.78669	404	\N	\N
447	\N	52.41379	16.90266	1	\N	\N
448	100	53.1342	17.99035	314	400	\N
449	\N	51.14865	15.01585	424	\N	\N
450	\N	50.58448	22.05793	414	\N	\N
451	\N	52.19307	21.17927	6	\N	\N
452	\N	53.01529	23.05419	452	\N	\N
453	\N	52.245052	21.294981	421	403	\N
454	\N	53.43138	14.54948	402	\N	\N
455	\N	52.23474	21.01522	6	283	399
456	\N	\N	\N	418	401	\N
457	\N	50.10989	19.81753	457	\N	\N
458	\N	50.2754	19.06336	192	\N	\N
459	\N	50.2585	19.04882	192	\N	\N
460	\N	52.546345	19.706537	460	\N	\N
461	\N	50.87056	20.62798	399	\N	\N
462	60	52.04573	21.29599	462	413	\N
463	33	50.02166	19.88934	4	414	\N
464	\N	53.45469	17.53593	464	\N	\N
465	\N	50.40652	19.1275	427	\N	\N
466	\N	52.04282	23.11088	466	\N	\N
467	\N	52.333656	20.886986	467	\N	\N
468	\N	51.80178	19.43928	19	\N	\N
469	\N	52.65975	19.07799	469	\N	\N
470	\N	52.43923	16.74875	432	415	379
471	\N	49.90146	19.87169	471	\N	\N
472	\N	53.0518	19.79057	472	\N	\N
473	\N	52.52918	17.59895	473	\N	\N
474	\N	52.41038	16.9854	1	416	\N
475	29	52.16857	17.69038	475	416	401
476	17C	52.36314	17.0227	436	405	397
477	90/92	51.76513	19.53628	19	419	\N
478	\N	51.94633	15.49585	478	\N	\N
479	\N	52.44051	16.74803	432	\N	\N
480	\N	50.33793	18.58702	253	\N	\N
481	17C	52.52918	17.59895	436	405	397
482	\N	54.19044	16.18948	482	\N	\N
483	\N	51.72091	19.46619	19	421	\N
484	177A	53.00717	18.61052	222	422	\N
485	\N	52.33008	20.99661	6	\N	\N
486	\N	51.109295	17.038603	3	423	\N
487	\N	52.72918	15.24028	32	424	\N
488	\N	52.19938	20.41891	488	\N	\N
489	\N	50.8008	19.12205	419	402	\N
490	\N	52.23876	21.08553	6	\N	\N
491	\N	50.375458	18.63585	491	\N	\N
492	\N	52.59905	21.4592	492	\N	\N
493	\N	52.41667	16.8812	1	\N	\N
494	\N	49.47464	20.054848	442	\N	\N
495	\N	54.44225	18.56291	495	\N	\N
496	32	52.86868	22.54419	496	426	404
497	\N	\N	\N	436	\N	\N
498	\N	51.53174	20.01097	498	\N	\N
499	3c	51.72091	19.46619	19	404	\N
500	\N	51.76513	19.53628	19	\N	\N
501	247	52.41666	21.18333	501	428	\N
502	\N	52.01508	18.49974	502	\N	\N
503	43a	51.72091	19.46619	19	421	\N
504	\N	51.20701	16.15532	415	\N	\N
505	\N	51.72091	19.46619	19	\N	\N
506	\N	51.12232	16.93842	3	\N	\N
507	\N	53.778423	20.48012	269	\N	\N
508	\N	51.30787	17.72016	508	\N	\N
509	\N	49.9791	18.94733	509	\N	\N
510	\N	51.6637	19.35685	510	\N	\N
511	\N	52.65346	17.96175	437	\N	\N
512	\N	51.20701	16.15532	415	\N	\N
513	14	49.91276	19.00687	513	430	\N
514	\N	50.68988	16.62663	514	\N	\N
515	\N	51.10195	17.03667	3	\N	\N
516	\N	52.409496	17.099861	516	\N	\N
517	\N	54.30883	18.56349	17	30	\N
518	\N	50.66987	17.91934	5	\N	\N
519	\N	51.22041	18.57114	519	\N	\N
520	4	53.1527	23.09326	51	432	\N
521	\N	\N	\N	521	\N	\N
522	\N	52.80789	17.1971	522	\N	\N
523	\N	\N	\N	523	\N	\N
524	\N	52.311962	17.046913	524	\N	\N
525	\N	51.7691	19.46617	19	\N	\N
526	\N	52.2479	15.53585	526	\N	\N
527	\N	50.07196	20.08338	4	\N	\N
528	\N	52.38035	16.60985	444	433	\N
529	\N	50.30255	18.77438	404	\N	\N
530	\N	50.66987	17.91934	5	\N	\N
531	\N	51.22041	18.57114	519	\N	\N
532	\N	50.28158	19.11219	532	\N	\N
533	\N	\N	\N	253	\N	\N
534	\N	51.28176	17.99116	396	\N	\N
535	\N	51.23955	22.55257	11	\N	\N
536	\N	50.28158	19.11219	532	\N	\N
537	\N	51.72091	19.46619	19	434	\N
538	56	51.11464	17.06436	3	435	\N
539	\N	\N	\N	539	\N	\N
540	\N	53.77602	20.47703	269	\N	\N
541	\N	\N	\N	523	\N	\N
542	\N	54.17944	15.55643	542	\N	\N
543	\N	52.28055	21.05787	6	\N	\N
544	\N	53.07736	21.55409	440	\N	\N
545	\N	51.95949	20.14328	545	\N	\N
546	\N	54.35013	18.65444	17	\N	\N
547	205E	52.33008	20.99661	6	436	\N
548	\N	51.63815	17.89023	443	\N	\N
549	\N	54.36306	18.63193	17	\N	\N
550	\N	52.23188	19.35362	550	\N	\N
551	\N	52.14713	21.73799	551	437	\N
552	\N	51.759293	19.455877	19	\N	\N
553	\N	52.73253	15.236931	32	\N	\N
554	\N	52.194157	21.034695	6	\N	\N
555	\N	50.34742	18.802328	404	\N	\N
556	\N	51.72091	19.46619	19	\N	\N
557	\N	50.87056	20.62798	399	\N	\N
558	\N	52.23188	19.35362	550	\N	\N
559	\N	52.23474	21.01522	6	\N	\N
560	\N	52.62115	20.37632	560	\N	\N
561	\N	\N	\N	561	\N	\N
562	13B	50.07097	21.70191	562	438	405
563	\N	50.375458	18.63585	491	\N	\N
564	6	53.438667	14.736634	402	439	\N
565	\N	53.01529	23.05419	452	\N	\N
566	\N	51.6637	19.35685	510	\N	\N
567	\N	53.66626	17.36262	567	\N	\N
568	\N	50.28158	19.11219	532	\N	\N
569	\N	\N	\N	569	\N	\N
570	\N	\N	\N	3	\N	\N
571	\N	\N	\N	222	\N	\N
572	\N	\N	\N	4	\N	\N
573	\N	\N	\N	1	\N	\N
574	\N	\N	\N	314	\N	\N
575	\N	\N	\N	575	\N	\N
576	\N	\N	\N	192	\N	\N
577	\N	\N	\N	400	\N	\N
578	\N	\N	\N	400	\N	\N
579	\N	\N	\N	579	\N	\N
580	\N	\N	\N	400	\N	\N
581	\N	\N	\N	222	\N	\N
582	\N	\N	\N	469	\N	\N
583	\N	\N	\N	192	\N	\N
584	\N	\N	\N	1	\N	\N
585	\N	\N	\N	400	\N	\N
586	\N	\N	\N	400	\N	\N
587	\N	\N	\N	469	\N	\N
588	\N	\N	\N	588	\N	\N
589	\N	\N	\N	589	\N	\N
590	\N	\N	\N	588	\N	\N
591	\N	\N	\N	243	\N	\N
592	\N	\N	\N	17	\N	\N
593	\N	\N	\N	3	\N	\N
594	\N	\N	\N	569	\N	\N
595	\N	\N	\N	3	\N	\N
596	\N	\N	\N	19	\N	\N
597	\N	\N	\N	222	\N	\N
598	\N	\N	\N	269	\N	\N
599	\N	\N	\N	599	\N	\N
600	\N	\N	\N	4	\N	\N
601	\N	\N	\N	601	\N	\N
602	\N	\N	\N	1	\N	\N
603	\N	\N	\N	314	\N	\N
604	\N	\N	\N	19	\N	\N
605	\N	\N	\N	51	\N	\N
606	\N	\N	\N	19	\N	\N
607	\N	\N	\N	575	\N	\N
608	\N	\N	\N	192	\N	\N
609	\N	\N	\N	400	\N	\N
610	\N	\N	\N	400	\N	\N
611	\N	\N	\N	579	\N	\N
612	\N	\N	\N	400	\N	\N
613	\N	\N	\N	222	\N	\N
614	\N	\N	\N	614	\N	\N
615	\N	\N	\N	469	\N	\N
616	\N	\N	\N	589	\N	\N
617	\N	\N	\N	589	\N	\N
618	\N	\N	\N	614	\N	\N
619	\N	\N	\N	430	\N	\N
620	\N	\N	\N	192	\N	\N
621	\N	\N	\N	1	\N	\N
622	\N	\N	\N	400	\N	\N
623	2	\N	\N	579	440	\N
624	\N	\N	\N	400	\N	\N
625	\N	\N	\N	469	\N	\N
626	\N	\N	\N	588	\N	\N
627	\N	\N	\N	11	\N	\N
628	\N	\N	\N	1	\N	\N
629	\N	\N	\N	588	\N	\N
630	\N	\N	\N	589	\N	\N
631	\N	\N	\N	588	\N	\N
632	\N	\N	\N	314	\N	\N
633	\N	\N	\N	19	\N	\N
634	\N	\N	\N	243	\N	\N
635	\N	\N	\N	599	\N	\N
636	\N	\N	\N	17	\N	\N
637	\N	\N	\N	589	\N	\N
638	\N	\N	\N	638	\N	\N
639	\N	\N	\N	11	\N	\N
640	\N	\N	\N	640	\N	\N
641	\N	\N	\N	3	\N	\N
642	\N	\N	\N	642	\N	\N
643	\N	\N	\N	643	\N	\N
644	\N	\N	\N	399	\N	\N
645	\N	\N	\N	419	\N	\N
646	\N	\N	\N	412	\N	\N
647	\N	\N	\N	2	\N	\N
648	\N	\N	\N	2	\N	\N
649	\N	\N	\N	2	\N	\N
650	\N	\N	\N	17	\N	\N
651	\N	\N	\N	2	\N	\N
652	\N	\N	\N	11	\N	\N
653	\N	\N	\N	192	\N	\N
654	\N	\N	\N	4	\N	\N
655	\N	\N	\N	314	\N	\N
656	\N	\N	\N	269	\N	\N
657	\N	\N	\N	4	\N	\N
658	\N	\N	\N	19	\N	\N
659	\N	\N	\N	19	\N	\N
660	\N	\N	\N	1	\N	\N
661	\N	\N	\N	192	\N	\N
662	\N	\N	\N	314	\N	\N
663	\N	\N	\N	4	\N	\N
664	\N	\N	\N	4	\N	\N
665	\N	\N	\N	314	\N	\N
666	\N	\N	\N	19	\N	\N
667	\N	\N	\N	4	\N	\N
668	\N	\N	\N	4	\N	\N
669	\N	\N	\N	4	\N	\N
670	\N	\N	\N	222	\N	\N
671	\N	\N	\N	51	\N	\N
672	\N	\N	\N	1	\N	\N
673	\N	\N	\N	478	\N	\N
674	\N	\N	\N	1	\N	\N
675	\N	\N	\N	4	\N	\N
676	\N	\N	\N	638	\N	\N
677	\N	\N	\N	19	441	\N
678	\N	\N	\N	678	\N	\N
679	\N	\N	\N	17	\N	\N
680	\N	\N	\N	4	\N	\N
681	\N	\N	\N	222	\N	\N
682	\N	\N	\N	243	\N	\N
683	\N	\N	\N	683	\N	\N
684	\N	\N	\N	192	\N	\N
685	\N	\N	\N	402	\N	\N
686	\N	\N	\N	11	\N	\N
687	\N	\N	\N	17	\N	\N
688	\N	\N	\N	460	\N	\N
689	\N	\N	\N	4	\N	\N
690	\N	\N	\N	4	\N	\N
691	\N	\N	\N	192	\N	\N
692	\N	\N	\N	692	\N	\N
693	\N	\N	\N	419	\N	\N
\.

COPY public.salary_periods (id, period) FROM stdin;
9	miesięcznie
\.

COPY public.sources (id, name, base_url) FROM stdin;
398	Olxpraca	https://www.olx.pl/oferta/praca/
590	Aplikujpl	https://www.aplikuj.pl/oferta/
1	Pracujpl	https://www.pracuj.pl/praca/
\.


COPY public.offers (id, job_title, description, salary_from, salary_to, is_gross, is_remote, is_hybrid, published, expires, is_urgent, is_for_ukrainians, source_id, company_id, location_detail_id, currency_id, salary_period_id, leading_category_id) FROM stdin;
119	Specjalista / Specjalistka ds. wsparcia i wdrożeń IT	Twój zakres obowiązków, Przyjmowanie i obsługa zgłoszeń od klientów firmy w ramach pierwszej/drugiej linii wsparcia (w zależności od zakresu umiejętności)., Diagnostyka i obsługa incydentów technicznych w ramach posiadanych kompetencji lub...	\N	\N	\N	f	f	2025-10-31 13:47:00+01	2025-11-16 22:59:59+01	t	f	1	281	278	\N	\N	18
120	Informatyk / Serwisant	Twój zakres obowiązków, konfiguracja i instalacja komputerów, laptopów, drukarek, skanerów i innych urządzeń peryferyjnych,, przygotowywanie nowych stanowisk pracy (sprzęt + oprogramowanie),, zakładanie i zarządzanie kontami użytkowników,,...	\N	\N	\N	f	f	2025-10-31 08:42:00+01	2025-11-16 22:59:59+01	f	f	1	282	279	\N	\N	18
121	Informatyk	Twój zakres obowiązków, Zakres wykonywanych zadań na stanowisku pracy  będzie obejmował w szczególności:, , - Utrzymywanie i monitorowanie działania sprzętu komputerowego i systemów informatycznych Uczelni,, - Realizowanie wsparcia technicznego...	\N	\N	\N	f	f	2025-10-29 18:10:27+01	2025-11-28 22:59:59+01	f	f	1	283	280	\N	\N	18
122	Urzędnik – Stażysta, docelowo Technik-Informatyk	Twój zakres obowiązków, Diagnozowanie i rozwiązywanie zagadnień technicznych związanych z oprogramowaniem i sprzętem użytkowanym w sądach apelacji poznańskiej., Praca w charakterze pierwszej linii wsparcia technicznego dla użytkowników systemów...	\N	\N	\N	f	f	2025-10-29 10:47:00+01	2025-11-14 22:59:59+01	t	f	1	284	281	\N	\N	18
123	Starszy Informatyk	Twój zakres obowiązków, Zapewnienie ciągłości pracy i bezpieczeństwa systemów informatycznych w szpitalu...	7500.00	8500.00	f	f	f	2025-10-29 09:27:00+01	2025-11-13 22:59:59+01	t	f	1	285	282	1	9	18
124	Specjalista ds. Sieci i Bezpieczeństwa / Network Security Specialist	Twój zakres obowiązków, Monitorowanie wydajności sieci oraz rozwiązywanie problemów sieciowych;, Współpraca z SOC;, Konfiguracja, wdrażanie i utrzymanie urządzeń sieciowych Fortinet (FortiGate, FortiAnalyzer, FortiManager, punkty dostępowe);,...	12600.00	20000.00	\N	t	\N	2025-10-28 08:59:00+01	2025-11-13 22:59:59+01	f	f	1	286	283	1	9	18
125	Informatyk medyczny	Twój zakres obowiązków, Wsparcie w utrzymaniu serwerów., Wsparcie w zapewnieniu bezpieczeństwa danych pacjentów 4WSK., Wsparcie techniczne dla personelu RCMC, polegające na rozwiązywaniu problemów z komputerami, oprogramowaniem, drukarkami,...	\N	\N	\N	f	f	2025-10-15 10:36:08+02	2025-11-14 22:59:59+01	t	f	1	287	284	\N	\N	18
126	Starszy Analityk IT (Tester/Analityk danych)	Twój zakres obowiązków, Testowanie systemów z obszaru pomiarów i monitorowania jakości energii elektrycznej., Rozwój i utrzymanie dokumentacji testowej., Realizacja testów wewnętrznych oraz testów z klientem....	\N	\N	\N	f	f	2025-11-12 16:00:00+01	2025-12-05 22:59:59+01	f	f	1	288	285	\N	\N	18
127	Kierownik Inżynierii Platform Mobilnych	Twój zakres obowiązków, Będziesz odpowiadać za rozwój i utrzymanie platformy mobilnej, obejmującej aplikacje natywne (iOS/Android), SDK oraz komponenty współdzielone między zespołami., Zorganizujesz pracę całego obszaru mobilnego – zadbasz o...	\N	\N	\N	f	f	2025-11-12 16:00:00+01	2025-11-15 22:59:59+01	f	f	1	289	286	\N	\N	328
12	Starszy Specjalista ds. informatyki (k/m)	Twój zakres obowiązków, utrzymanie bieżącej infrastruktury - II Linia wsparcia dla HelpDesk, monitorowanie statusu zgłoszeń w systemie helpdesk i zapewnienie ich terminowej realizacji, instalacja, konfiguracja i aktualizacja systemów operacyjnych,...	\N	\N	f	f	f	2025-11-12 12:52:22+01	2025-12-12 22:59:59+01	f	f	1	12	133	\N	\N	18
128	Tech Leader / Starszy Programista Fullstack (Python/Angular) | Branża edukacyjna	Twój zakres obowiązków, Udział w definiowaniu integracji modeli LLM w narzędziach wspierających uczniów i nauczycieli, Współpraca z data scientistami, projektantami UX i inżynierami przy wdrażaniu modeli w praktyczne rozwiązania, Projektowanie...	\N	\N	\N	t	\N	2025-11-12 16:00:00+01	2025-11-15 22:59:59+01	t	f	1	75	287	\N	\N	1
129	Kierownik ds. rozwoju i utrzymania systemów IT	Twój zakres obowiązków, Zarządzanie kilkuosobowym zespołem realizującym zadania w obszarze utrzymania i rozwoju systemów IT dla organizacji, Zarządzanie portfolio systemów i aplikacji biznesowych, Występowanie w roli Project Managera dla wybranych...	16000.00	18000.00	f	f	f	2025-11-12 15:32:30+01	2025-12-12 22:59:59+01	f	f	1	291	288	1	9	18
130	Specjalista/ka ds. Utrzymania i Rozwoju Aplikacji Biznesowych	Twój zakres obowiązków, Zapewnienie ciągłości działania oraz rozwój aplikacji biznesowych, Monitorowanie, utrzymanie i optymalizacja wydajności aplikacji oraz infrastruktury systemowej, Analiza i rozwiązywanie incydentów oraz błędów produkcyjnych,...	\N	\N	\N	f	f	2025-11-12 15:28:00+01	2025-11-28 22:59:59+01	f	f	1	292	289	\N	\N	18
131	Specjalista / Specjalistka ds. modelowania danych platform EFM	Twój zakres obowiązków, automatyzujesz procesy z zakresu zaawansowanej analityki,, rozwijasz i optymalizujesz proces wykrywania nadużyć,, przygotowujesz dane do modelowania,, budujesz modele uczenia maszynowego z wykorzystaniem najnowszych technik...	\N	\N	\N	f	f	2025-11-12 15:03:43+01	2025-12-12 22:59:59+01	f	f	1	293	290	\N	\N	8
132	Starszy Specjalista ds. Monitorowania i Obsługi Incydentów ICT (k/m)	Twój zakres obowiązków, Monitorowanie infrastruktury ICT – systemów, aplikacji, sieci, serwerów i procesów biznesowych w aplikacjach z wykorzystaniem narzędzi monitorujących, Analiza i obsługa zdarzeń – alertów z systemów monitoringu oraz zgłoszeń...	\N	\N	\N	f	f	2025-11-12 14:13:00+01	2025-11-22 22:59:59+01	f	f	1	6	291	\N	\N	18
133	Architekt Big Data	Twój zakres obowiązków, Projektowanie architektury obszarów i rozwiązań w organizacji zgodnie z przyjętą strategią;, Zgłaszanie inicjatyw w zakresie poprawy jakości rozwiązań;, Konsultacje na potrzeby realizowanych projektów....	\N	\N	\N	t	\N	2025-11-12 14:50:42+01	2025-12-12 22:59:59+01	f	f	1	295	292	\N	\N	1
134	Analityk biznesowo-systemowy (ubezpieczenia)	Twój zakres obowiązków, Zbieranie, analiza i dokumentacja wymagań (FURPS), Opracowanie przypadków użycia i modeli procesów biznesowych, Współpraca z zespołem programistów i architektów, Weryfikacja rozwiązań informatycznych w kontekście wymagań...	\N	\N	\N	t	\N	2025-11-12 14:47:00+01	2025-11-27 22:59:59+01	t	f	1	296	293	\N	\N	18
135	Programista Big Data	Twój zakres obowiązków, Projektowanie, rozwijanie i utrzymywanie aplikacji zgodnie z dobrymi praktykami programowania i podejściem TDD., Integracja i przetwarzanie danych pochodzących z różnych systemów informatycznych., Projektowanie i...	\N	\N	\N	t	\N	2025-11-12 14:42:59+01	2025-12-12 22:59:59+01	f	f	1	295	294	\N	\N	18
163	Architekt SOC	Twój zakres obowiązków, Projektowanie i rozwój architektury SOC: SIEM, SOAR, EDR/XDR, NDR, TIP, honeypots, log management., Budowa log pipeline: źródła, normalizacja, enrichment, retencja, kosztorysowanie i KPI (MTTD, MTTR, FNR/FPR)., Detection...	\N	\N	\N	f	f	2025-11-12 10:11:00+01	2025-11-15 22:59:59+01	t	f	1	227	322	\N	\N	7
5	Programista Python	Twój zakres obowiązków, projektowanie i implementacja oprogramowania do przetwarzania i analizy obrazu,, praca z systemami wizyjnymi (kamery głębokościowe, termowizyjne, LiDAR),, integracja systemów wizyjnych z pozostałymi komponentami systemu,,...	\N	\N	\N	f	f	2025-11-10 11:40:03+01	2025-12-03 22:59:59+01	f	f	1	5	166	\N	\N	18
9	Młodszy Administrator IT	Twój zakres obowiązków, Bieżące zarządzanie infrastrukturą informatyczną firmy (komputery, serwery, oprogramowanie)., Konfiguracja, aktualizacja i utrzymanie systemów operacyjnych Windows oraz oprogramowania użytkowego., Konfiguracja i...	\N	\N	\N	f	f	2025-11-12 15:09:00+01	2025-12-07 22:59:59+01	t	f	1	9	170	\N	\N	18
37	Informatyk - Administrator sieci	Twój zakres obowiązków, Administracja i utrzymanie sieci firmowej (serwer Inv / Vault, Routery, Switche, Acces Pointy), Monitorowanie i optymalizacja działania sieci (VLAN, VPN, firewall, routing, Wi-Fi, QoS), Zarządzanie dostępem do Internetu,...	\N	\N	f	t	\N	2025-11-11 08:28:00+01	2025-12-06 22:59:59+01	t	f	1	37	37	\N	\N	18
1	Programista Python	Twoje obowiązki, Jako programista Python w STX Next, będziesz częścią zespołu interdyscyplinarnego odpowiedzialnego za budowanie, ulepszanie i utrzymywanie aplikacji internetowych dla naszych międzynarodowych klientów. Będziesz ściśle współpracować z innymi programistami,...	\N	\N	\N	t	\N	2025-11-10 14:28:00+01	2025-12-03 22:59:59+01	t	f	1	1	162	\N	\N	18
45	Konsultant systemów informatycznych	Twój zakres obowiązków, wdrożenia systemów informatycznych dla klientów,, testowanie oprogramowania,, prowadzenie szkoleń i prezentacji,, tworzenie dokumentacji użytkownika,, udzielanie konsultacji telefonicznych, dotyczących obsługi...	\N	\N	\N	f	f	2025-11-12 08:32:52+01	2025-12-12 22:59:59+01	f	f	1	144	185	\N	\N	18
21	Specjalista / Specjalistka ds. informatyki	Twój zakres obowiązków, Instalacja, konfiguracja i konserwacja sprzętu komputerowego i oprogramowania., Zarządzanie i administracja sieciami (LAN, WAN, chmura) oraz serwerami., Monitorowanie wydajności systemów i sieci., Prowadzenie ewidencji...	\N	\N	\N	f	f	2025-11-12 08:30:47+01	2025-12-12 22:59:59+01	t	f	1	21	186	\N	\N	18
24	Informatyk / Administrator systemów i sprzętu komputerowego	Twój zakres obowiązków, Utrzymanie i rozwój infrastruktury IT (komputery, sieć, serwery, drukarki oraz urządzenia audiomertyczne)., Administracja systemami Windows oraz podstawowa obsługa Linux., Wsparcie użytkowników w codziennej pracy (helpdesk,...	\N	\N	\N	f	f	2025-11-12 06:34:25+01	2025-12-05 22:59:59+01	t	f	1	24	190	\N	\N	18
28	Młodszy Specjalista ds. wsparcia IT z j. niemieckim	Twój zakres obowiązków, Rejestrowanie oraz realizacja zgłoszeń w ramach 1 linii wsparcia IT w oparciu o: szkolenie, procedury i instrukcje stanowiskowe,, Dystrybucja zgłoszeń do właściwych linii wsparcia,, Aktywna komunikacja i współpraca ze...	\N	\N	\N	f	f	2025-11-11 14:02:00+01	2025-12-04 22:59:59+01	f	f	1	28	196	\N	\N	18
71	Młodszy specjalista / Młodsza specjalistka ds. wsparcia IT	Twój zakres obowiązków, wsparcie użytkowników w zakresie funkcjonalności i działania systemów informatycznych firmy, przyjmowanie, rejestrowanie i rozwiązywanie zgłoszeń dotyczących usług i systemów informatycznych, konfigurowanie i przekazywanie...	\N	\N	\N	f	f	2025-11-08 13:22:00+01	2025-11-23 22:59:59+01	t	f	1	233	230	\N	\N	18
72	Starszy Inżynier ds. Jakości Systemów Informatycznych	Twój zakres obowiązków, Sporządzanie scenariuszy testowych i przypadków testowych na podstawie wymagań biznesowych;, Testowanie aplikacji na podstawie scenariuszy testowych;, Przeprowadzanie oraz wsparcie procesu testów akceptacyjnych i...	\N	\N	\N	f	f	2025-11-08 10:54:00+01	2025-11-13 22:59:59+01	f	f	1	7	231	\N	\N	18
73	IT Security - Continuous Improvement Manager (umowa na czas określony, 20 miesięcy)	Twoje obowiązki, Śledzenie formalnie zdefiniowanych planów naprawczych do momentu rozwiązania, Bycie centralnym punktem koordynacji w GT dla planów działań IT Security wynikających z audytu, drugiej opinii dotyczącej ryzyka, ustaleń Information Security Assurance.,...	\N	\N	\N	f	f	2025-11-08 10:39:00+01	2025-11-28 22:59:59+01	t	f	1	235	232	\N	\N	7
74	Informatyk - wsparcie techniczne L1	Twój zakres obowiązków, Przyjmowanie i rejestrowanie zgłoszeń serwisowych od użytkowników (telefonicznie, mailowo lub przez system zgłoszeniowy),, Klasyfikacja i wstępna diagnostyka zgłoszeń,, Rozwiązywanie podstawowych problemów technicznych...	5000.00	7000.00	f	f	f	2025-11-08 09:48:17+01	2025-11-23 22:59:59+01	t	f	1	236	233	1	9	18
75	Specjalista ds. sieci i bezpieczeństwa IT	Twój zakres obowiązków, Utrzymanie, rozwój, usuwanie awarii sieci komputerowych fizycznych, wirtualnych, bezprzewodowych i VPN opartych na rozwiązaniach Ruckus, MS Azure, Google Cloud i dedykowanych, Konfigurowanie i stosowanie zapór ogniowych...	15000.00	25200.00	f	f	f	2025-11-07 17:00:00+01	2025-11-23 22:59:59+01	t	f	1	237	234	1	9	18
76	Informatyk do spraw wsparcia teleinformatycznego pracowników UKE	Twój zakres obowiązków, Osoba na tym stanowisku:, Bierze udział w zapewnieniu sprawnego funkcjonowania sprzętu komputerowego znajdującego się na wyposażeniu UKE, Bierze udział we wsparciu pracowników Urzędu w rozwiązywania problemów związanych z...	\N	\N	\N	f	f	2025-11-07 16:00:00+01	2025-11-16 22:59:59+01	f	f	1	238	235	\N	\N	18
77	Informatyk	Twój zakres obowiązków, Instalowanie oprogramowania, serwis oraz konserwacja sprzętu informatycznego, Prace związane z utrzymaniem sieci firmowej oraz monitoringiem CCTV, Zarządzanie infrastrukturą serwerową, Zapewnienie bezpieczeństwa danych...	\N	\N	\N	f	f	2025-11-07 12:41:50+01	2025-12-07 22:59:59+01	f	f	1	239	236	\N	\N	18
78	Młodszy specjalista / Młodsza specjalistka ds. wsparcia IT / HelpDesk	Twój zakres obowiązków, Konfiguracja i przygotowanie komputerów oraz urządzeń mobilnych do wydania użytkownikom;, Przyjmowanie, weryfikacja i obsługa zgłoszeń serwisowych;, Analiza i rozwiązywanie problemów sprzętowych, programowych oraz...	5000.00	6000.00	f	f	f	2025-11-07 10:59:27+01	2025-11-22 22:59:59+01	t	f	1	240	237	1	9	18
79	Młodszy specjalista ds. wsparcia IT z językiem niemieckim	Twój zakres obowiązków, wsparcie techniczne w ramach 1-szej linii kontaktu dla użytkowników, diagnozowanie i rozwiązywanie problemów przez czat, email i telefon,, zarządzanie incydentami: przyjmowanie, rejestrowanie i rozwiązywanie zgłoszeń...	\N	\N	\N	f	f	2025-11-07 10:47:00+01	2025-11-23 22:59:59+01	t	f	1	241	238	\N	\N	18
80	Specjalista ds. Bezpieczeństwa IT	Twój zakres obowiązków, Współtworzenie polityk, standardów i procedur związanych z bezpieczeństwem technologii informacyjnych., Tworzenie, modyfikacja i utrzymanie dokumentacji technicznej oraz procedur dla systemów bezpieczeństwa IT;,...	\N	\N	\N	f	f	2025-11-07 10:41:00+01	2025-11-22 22:59:59+01	f	f	1	242	239	\N	\N	7
81	Informatyk - Specjalista ds. wsparcia IT	Twój zakres obowiązków, obsługa informatyczna procesów firmy, gospodarowanie i nadzór nad zasobami informatycznymi , rozwiązywanie problemów dotyczących systemów informatycznych, tworzenie i zarządzanie profilami informatycznymi pracowników,...	\N	\N	\N	f	f	2025-11-07 10:30:50+01	2025-11-22 22:59:59+01	f	f	1	243	240	\N	\N	18
82	Koordynator ds. IT / Administrator systemów	Twój zakres obowiązków, Administracja siecią komputerową, serwerami i bazami danych., Tworzenie i nadzorowanie polityki wykonywania kopii zapasowych,, Zapewnienie bezpieczeństwa systemów (aktualizacje oprogramowania, monitoring, kontrola...	\N	\N	\N	f	f	2025-11-07 10:30:01+01	2025-12-07 22:59:59+01	t	f	1	244	241	\N	\N	18
83	Wsparcie IT - HelpDesk – Automatyk - Technik utrzymania ruchu	Twój zakres obowiązków, wsparcie dla sprzętu komputerowego oraz oprogramowania stacji roboczej/biurowego, wsparcie użytkowników w zakresie IT (systemy operacyjne, oprogramowanie biurowe Office), nadzorowanie środowisk informatycznych, komputerów...	\N	\N	\N	f	f	2025-11-07 09:55:00+01	2025-11-23 22:59:59+01	f	f	1	245	242	\N	\N	18
84	Specjalista / Specjalistka do spraw wsparcia IT	Twój zakres obowiązków, Utrzymanie i rozwój infrastruktury informatycznej, Zapewnienie prawidłowego działania istniejących systemów informatycznych w firmie (ERP, Microsoft 365, Bitrix24), Bieżące wsparcie dla użytkowników – instalacja i...	\N	\N	\N	f	f	2025-11-07 08:41:17+01	2025-11-30 22:59:59+01	f	f	1	246	243	\N	\N	18
6	Starszy Specjalista ds. Administrowania Systemami Bezpieczeństwa IT (k/m)	Twój zakres obowiązków, Udział w rozwijaniu systemów cyberobrony Banku, Utrzymywanie rozwiązań z zakresu cyberbezpieczeństwa w tym zabezpieczeń środowiska M365, Śledzenie zmian rozwojowych w systemach, Administracja i konfiguracja systemów...	\N	\N	\N	f	f	2025-11-12 16:00:00+01	2025-12-07 22:59:59+01	f	f	1	6	167	\N	\N	7
7	Administrator Systemów Informatycznych Windows	Twój zakres obowiązków, Samodzielna administracja, konfiguracja, utrzymanie, monitoring wybranych systemów IT, aplikacji, serwerów fizycznych i wirtualnych., Udział w projektach infrastrukturalnych i samodzielne wykonywanie prac...	\N	\N	\N	f	f	2025-11-12 16:00:00+01	2025-12-07 22:59:59+01	t	f	1	7	168	\N	\N	18
8	Starszy Inżynier Systemowy (bezpieczeństwo IT)	Twój zakres obowiązków, Analiza wymagań klienta i dobór właściwych urządzeń oraz licencji., Przygotowywanie BOM (Bill of Materials)., Wdrażanie i utrzymanie systemów bezpieczeństwa IT (m.in. NGFW/IPS, NDR, LB/WAF, DNS Security, XDR, DB Security).,...	18000.00	23000.00	f	f	f	2025-11-12 15:30:00+01	2025-11-27 22:59:59+01	f	f	1	8	169	1	9	7
40	Informatyk - Specjalista HelpDesk	Twój zakres obowiązków, bezpośrednia pomoc techniczna pracownikom w problemach ze sprzętem komputerowym i aplikacjach zainstalowanych na komputerach,, rejestracja zleceń serwisowych i ich realizacja w ramach 1-szej linii wsparcia,, pomoc w...	\N	\N	\N	f	f	2025-11-12 14:53:12+01	2025-12-05 22:59:59+01	t	f	1	49	171	\N	\N	18
10	Specjalista / Specjalistka ds. wsparcia IT	Twój zakres obowiązków, Troubleshooting problemów zgłaszanych przez użytkowników zarówno sprzętowych jak i systemowych, Obsługa zgłoszeń serwisowych: instalacja oprogramowania, przygotowanie stanowiska, pracy, wymiana sprzętu oraz urządzeń...	\N	\N	\N	f	f	2025-11-12 13:56:06+01	2025-11-27 22:59:59+01	t	f	1	10	172	\N	\N	18
46	Informatyk w Dziale Rozwoju Aplikacji i Zarządzania Bazami Danych	Twój zakres obowiązków, administracja systemami bazodanowymi, w szczególności typu Microsoft SQL Server, Oracle Database, PostgreSQL;, wykonywanie specjalizowanych raportów na podstawie baz danych;, wykonywanie kopii zapasowych baz danych;,...	\N	\N	\N	f	f	2025-11-12 13:47:00+01	2025-11-24 22:59:59+01	t	f	1	51	173	\N	\N	18
41	Informatyk w Dziale Administracji Usługami Publicznymi	Twój zakres obowiązków, administracja systemami poczty elektronicznej;, administracja serwerami z systemami Linux i Windows;, wsparcie użytkowników administrowanych systemów;, wykonywanie aktualizacji, instalacji poprawek do administrowanych...	\N	\N	\N	f	f	2025-11-12 13:46:00+01	2025-11-24 22:59:59+01	t	f	1	51	174	\N	\N	18
11	Specjalista / Specjalistka ds. Wsparcia IT	Twój zakres obowiązków, Rozwiązywanie problemów zgłaszanych przez użytkowników lub systemy monitorujące, Rejestracja zgłoszeń i realizacja zadań w systemie ticketowym, Nadzór nad systemami monitoringu infrastruktury (serwery, sieci, macierze,...	\N	\N	\N	f	f	2025-11-12 13:23:40+01	2025-12-05 22:59:59+01	f	f	1	11	175	\N	\N	18
13	Inżynier Bezpieczeństwa Sieci (Azure)	Twój zakres obowiązków, Identyfikowanie, analiza i mitygacja podatności w naszych produktach i systemach., Wykonywanie analizy potencjalnych zagrożeń, analiza ryzyk, monitorowanie i wykrywanie zagrożeń, tworzenie i optymalizacja reguł ich...	\N	\N	\N	t	\N	2025-11-12 12:02:02+01	2025-12-12 22:59:59+01	f	f	1	13	176	\N	\N	7
14	Specjalista ds. wsparcia IT (K/M)	Twój zakres obowiązków, udzielanie bezpośredniego (na miejscu), telefonicznego oraz zdalnego wsparcia technicznego dla pracowników administracyjnych Spółki, monitorowanie i rozwiązywanie problemów dotyczących sprzętu i oprogramowania (komputery,...	\N	\N	\N	f	f	2025-11-12 11:52:00+01	2025-12-05 22:59:59+01	f	f	1	14	177	\N	\N	18
15	IT Support - Technik Wsparcia IT	Twój zakres obowiązków, Zapewnianie codziennego wsparcia lokalnego/zdalnego użytkownikom biurowym, Obsługa połączeń przychodzących i odpowiadanie na pytania uzytkowników, Rozwiązywanie problemów i dokumentowanie kroków podjętych w celu rozwiązania...	\N	\N	\N	f	f	2025-11-12 11:21:00+01	2025-11-28 22:59:59+01	t	t	1	15	178	\N	\N	18
16	Technik Informatyk	Twój zakres obowiązków, Administracja siecią komputerową, Administracja serwerami Windows i Linux, Wsparcie użytkowników i pomoc w rozwiązywaniu problemów technicznych, Wsparcie techniczne w zakresie obsługi komputerów, laptopów i drukarek,...	\N	\N	\N	f	f	2025-11-12 11:02:00+01	2025-12-07 22:59:59+01	t	f	1	16	179	\N	\N	18
42	Młodszy administrator systemów informatycznych	Twój zakres obowiązków, Zarządzanie i utrzymanie połączeń VPN, Analiza logów systemowych oraz rozwiązywanie problemów (troubleshooting), Monitorowanie i reagowanie na incydenty (wsparcie użytkowników – Helpdesk), Diagnozowanie i usuwanie problemów...	6000.00	7000.00	f	f	f	2025-11-12 10:12:12+01	2025-11-27 22:59:59+01	t	f	1	57	180	1	9	18
17	Młodszy Administrator IT	Twój zakres obowiązków, 1 linia wsparcia;, Administracja i wsparcie użytkowników;, Zarządzanie środowiskiem wirtualnym;, Zarządzanie systemami Windows Server oraz Linux;, Zarządzanie infrastrukturą LAN/WAN;, Nadzór nad bezpieczeństwem IT;,...	\N	\N	\N	f	f	2025-11-12 09:26:24+01	2025-12-12 22:59:59+01	f	f	1	17	181	\N	\N	18
18	Technik Informatyk	Twój zakres obowiązków, Obsługa domen Microsoft Windows, serwerów (Exchange);, Administracja komponentami sieciowymi/Firewall/VPN;, Administracja profilami użytkowników w Microsoft Active Directory i SAP;, Administracja systemami monitorowania...	\N	\N	\N	f	f	2025-11-12 09:17:00+01	2025-12-05 22:59:59+01	f	f	1	18	182	\N	\N	18
19	Starszy Specjalista ds. Wsparcia IT	Twój zakres obowiązków, Obsługa zgłoszeń IT w systemie Jira: rejestracja, kategoryzacja, priorytetyzacja, przekazywanie i informowanie użytkowników., Zarządzanie użytkownikami i urządzeniami w Active Directory oraz Microsoft Intune., Konfiguracja...	\N	\N	\N	f	f	2025-11-12 09:11:00+01	2025-12-07 22:59:59+01	t	f	1	19	183	\N	\N	18
20	Specjalista / Specjalistka ds. systemów informatycznych	Twój zakres obowiązków, Opis czynności wykonywanych na stanowisku - zakres ważniejszych zadań:, a. zdalne oraz bezpośrednie wsparcie użytkowników, b. diagnozowanie i rozwiązywanie problemów związanych z działaniemsystemów...	\N	\N	\N	f	f	2025-11-12 08:47:46+01	2025-12-12 22:59:59+01	f	f	1	20	184	\N	\N	18
47	Specjalista / Specjalistka ds. administracji i rozwoju systemów informatycznych (eNova)	Administracja i rozwój systemu ERP (preferowana znajomość enova365), Rozwój i utrzymanie integracji z zewnętrznymi systemami (np. Apilo, BaseLinker, etc.), Zarządzanie obszarem danych w organizacji, Tworzenie zapytań i...	\N	\N	\N	f	f	2025-11-12 08:02:29+01	2025-12-12 22:59:59+01	t	f	1	191	189	\N	\N	18
48	Administrator IT	Twoje obowiązki, reprezentowanie działu IT w profesjonalny, przystępny i zorientowany na usługi sposób, zarządzanie zgłoszeniami za pośrednictwem ServiceNow (platforma ITSM), obsługa połączeń przychodzących za pośrednictwem Microsoft Teams, zapewnianie osobistego wsparcia.	\N	\N	\N	f	f	2025-11-11 16:00:00+01	2025-11-22 22:59:59+01	t	f	1	194	192	\N	\N	18
31	Administrator IT	Twój zakres obowiązków, administracja systemami Windows Server, instalacja i konfiguracja oprogramowania oraz urządzeń zgodnie z obowiązującymi procedurami, administracja sieci TCP/IP, konfiguracja routerów, firewall, utm, raportowanie wykonanych...	6000.00	8000.00	f	f	f	2025-11-11 12:45:15+01	2025-11-21 22:59:59+01	t	f	1	31	199	1	9	18
2	Starszy programista AI Python	Twoje obowiązki, Projektowanie, rozwój i utrzymanie bezpiecznych, skalowalnych usług backendowych, API i mikroserwisów, Budowanie odpornych usług Python z frameworkami takimi jak LangGraph, Celery, PydanticAI i wymuszanie typowanych schematów dla jakości danych.,...	140.00	170.00	f	t	\N	2025-11-10 13:33:00+01	2025-12-04 22:59:59+01	f	t	1	2	163	1	9	18
3	Programista Python – Automatyzacja Inżynierii (AEC)	Twoje obowiązki, Projektowanie i rozwój komponentów backendowych w Pythonie, Implementacja logiki obliczeniowej w przepływach pracy AEC i narzędziach automatyzacji, Projektowanie i implementacja kontroli dostępu opartej na rolach (RBAC) i bezpieczne zarządzanie uprawnieniami z...	15000.00	25000.00	f	t	\N	2025-11-10 13:17:00+01	2025-12-07 22:59:59+01	t	f	1	3	164	1	9	18
4	Programista Python	Twoje obowiązki, Projektowanie i rozwój niestandardowego narzędzia ETL przy użyciu języka Python., Współpraca z architektami i analitykami danych w celu zrozumienia wymagań dotyczących danych i przełożenia ich na rozwiązania techniczne., Budowanie i optymalizacja potoków danych w celu zapewnienia wydajności...	\N	\N	\N	t	\N	2025-11-10 12:40:00+01	2025-11-30 22:59:59+01	t	f	1	4	165	\N	\N	18
32	Informatyk	Twój zakres obowiązków, monitorowanie logów i alertów bezpieczeństwa (Wazuh, Elastic Stack, Suricata, Grafana etc),, analiza zdarzeń i incydentów bezpieczeństwa, eskalacja i dokumentowanie przypadków,, udział we wdrożeniach platform SIEM,...	\N	\N	\N	f	f	2025-11-11 12:40:27+01	2025-11-26 22:59:59+01	t	f	1	32	200	\N	\N	18
33	Administrator Baz Danych i Systemów IT (Starszy Informatyk)	Twój zakres obowiązków, Administracja bazami danych SQL Server on-premises (2014-2022) – konfiguracja,  optymalizacja i zapewnianie ciągłości działania w trybie 24/7, Monitoring i optymalizacja środowisk bazodanowych, zarządzanie hurtownią danych,...	\N	\N	\N	f	f	2025-11-11 10:47:00+01	2025-11-26 22:59:59+01	t	f	1	33	201	\N	\N	18
49	Specjalista ds. wdrożenia Clinical Information System i innych systemów informatycznych	Twój zakres obowiązków, Wsparcie procesu wdrożenia systemu CIS i pozostałych systemów objętych projektem., Analiza i dokumentowanie potrzeb użytkowników końcowych (lekarze, pielęgniarki, administracja)., Uzgodnienie integracji z istniejącymi...	\N	\N	\N	f	f	2025-11-11 10:36:00+01	2025-11-27 22:59:59+01	f	f	1	204	202	\N	\N	18
34	Informatyk	Twój zakres obowiązków, Nadzór nad infrastrukturą informatyczną klientów,, Administrowanie systemami informatycznymi,, Obsługa stanowisk roboczych użytkowników,, Wsparcie w zakresie bezpieczeństwa IT (w ograniczonym zakresie),, Dokumentacja...	\N	\N	\N	f	f	2025-11-11 10:34:03+01	2025-11-26 22:59:59+01	t	f	1	34	203	\N	\N	18
35	Specjalista / Specjalistka ds. Wsparcia IT	Twój zakres obowiązków, Obsługa zgłoszeń dotyczących systemów i sprzętu IT firmy oraz wsparcie użytkowników w codziennej pracy, Pierwsza linia wsparcia, Przygotowywanie stanowisk pracy dla nowych i obecnych pracowników, Obsługa klienta...	\N	\N	\N	f	f	2025-11-11 10:11:00+01	2025-12-06 22:59:59+01	t	f	1	35	204	\N	\N	18
44	Konsultant ds. Wdrożeń IT / Analityk IT	Twój zakres obowiązków, Analiza i rozwiązywanie problemów w integracji między systemami uczestników rynku a systemem centralnym, Udzielanie uczestnikom rynku informacji o sposobach integracji z systemem centralnym – zarówno w warstwie biznesowej,...	\N	\N	\N	t	\N	2025-11-11 09:44:00+01	2025-12-04 22:59:59+01	t	f	1	75	205	\N	\N	18
36	Młodszy Specjalista ds. wsparcia IT	Twój zakres obowiązków, wsparcie w budowaniu i utrzymywaniu infrastruktury IT,, utrzymanie oprogramowania używanego w Spółce,, analizowanie i docelowe rozwiązywanie problemów wpływających na niezawodność usług IT,, bieżące wsparcie użytkowników...	6000.00	8000.00	f	f	f	2025-11-11 09:36:00+01	2025-12-04 22:59:59+01	t	f	1	36	206	1	9	18
50	Inżynier ds. Jakości Systemów Informatycznych	Twój zakres obowiązków, Sporządzanie scenariuszy testowych i przypadków testowych na podstawie wymagań biznesowych;, Testowanie aplikacji na podstawie scenariuszy testowych;, Przeprowadzanie oraz wsparcie procesu testów akceptacyjnych i...	\N	\N	\N	f	f	2025-11-11 09:20:00+01	2025-11-16 22:59:59+01	t	f	1	7	207	\N	\N	18
38	Specjalista ds. Bezpieczeństwa IT	Twój zakres obowiązków, utrzymanie i rozwój Systemu Zarządzania Bezpieczeństwem Informacji (ISO 27001) oraz przygotowanie organizacji do recertyfikacji,, udział w projektach związanych z regulacjami DORA i ciągłym doskonaleniem procesów...	\N	\N	\N	f	f	2025-11-11 08:25:00+01	2025-11-13 22:59:59+01	t	f	1	38	208	\N	\N	7
39	Specjalista ds. Bezpieczeństwa IT	Twoje obowiązki, Wdrażanie, utrzymanie i zarządzanie systemami bezpieczeństwa IT, takimi jak: SIEM (preferowany Wazuh), EDR, ESET, Checkpoint, skanery podatności (preferowany BURP) oraz inne., Identyfikacja i analiza zagrożeń oraz incydentów...	\N	\N	\N	t	\N	2025-11-11 07:47:00+01	2025-12-04 22:59:59+01	f	f	1	39	209	\N	\N	7
51	Specjalista ds. Bezpieczeństwa IT (Zespół Monitorowania Bezpieczeństwa)	Twój zakres obowiązków, Nadzór nad stosowaniem polityk, standardów bezpieczeństwa w LUX MED;, Monitorowanie poziomu bezpieczeństwa infrastruktury teleinformatycznej;, Monitorowanie informacji o pojawiających się zagrożeniach;, Obsługa zgłoszeń,...	\N	\N	\N	f	f	2025-11-11 07:34:00+01	2025-12-06 22:59:59+01	t	f	1	7	210	\N	\N	7
52	Informatyk - Młodszy Specjalista HelpDesk	Twój zakres obowiązków, wsparcie działu IT w zakresie konfiguracji sprzętu sieciowego (PC), realizacja zgłoszeń – przyjmowanie oraz weryfikacja zgłoszeń klienckich, aktywna pomoc w licznych projektach Informatycznych...	4700.00	5000.00	\N	f	f	2025-11-10 21:12:14+01	2025-12-10 22:59:59+01	t	f	1	214	211	1	9	18
53	Administrator / Administratorka systemów Informatycznych	Twój zakres obowiązków, zarządzanie infrastrukturą IT firm i przedsiębiorstw, konfigurowanie i administrowaniem siecią komputerową, serwerami Windows/Linux, wdrażanie nowych rozwiązań usprawniających działanie istniejących urządzeń oraz aplikacji,...	4000.00	6000.00	f	f	f	2025-11-10 16:01:53+01	2025-12-10 22:59:59+01	f	f	1	215	212	1	9	18
54	Administrator IT	Twoje obowiązki, Szukamy utalentowanego administratora systemu, który dołączy do naszego zespołu. Twoje zadania będą obejmować odpowiedzialność za konserwację, konfigurację i niezawodne działanie złożonych systemów komputerowych i serwerów. Będziesz...	\N	\N	\N	f	f	2025-11-10 15:30:00+01	2025-12-03 22:59:59+01	t	f	1	216	213	\N	\N	18
55	Koordynator ds. Wsparcia IT	Twój zakres obowiązków, Koordynacja pracy zespołu pierwszej linii wsparcia IT (helpdesk)., Monitorowanie i rozdzielanie zgłoszeń w systemie Service Desk., Współpraca z użytkownikami w celu szybkiego i skutecznego rozwiązywania problemów IT.,...	\N	\N	\N	f	f	2025-11-10 15:12:00+01	2025-11-13 22:59:59+01	f	f	1	217	214	\N	\N	18
56	Starszy Administrator IT	Twój zakres obowiązków, Administracja infrastrukturą serwerową (instalacja, utrzymanie, monitoring, aktualizacje), Administracja serwerami baz danych MS SQL (instalacja, utrzymanie, monitoring, aktualizacje), Administracja kontami użytkowników i...	\N	\N	\N	f	f	2025-11-10 14:23:00+01	2025-11-30 22:59:59+01	t	f	1	218	215	\N	\N	18
57	Specjalista-administrator systemów informatycznych	Twój zakres obowiązków, instalacja  i dokumentowanie i testowanie specjalistycznego oprogramowania na klastrach obliczeniowych, wsparcie użytkowników systemów obliczeniowych w zakresie wdrażanego oprogramowania i działania systemów, instalacja,...	\N	\N	\N	f	f	2025-11-10 13:28:00+01	2025-11-23 22:59:59+01	f	f	1	219	216	\N	\N	18
136	Agent Serwisu Pomocy Technicznej z Językiem Hiszpańskim i Angielskim	Twoje obowiązki, Bądź pierwszym punktem kontaktu dla klientów potrzebujących wsparcia technicznego — przez telefon, e-mail i czat, Rozwiązuj podstawowe problemy informatyczne z pewnością - bądź bohaterem!, Zarządzaj i śledź zgłoszenia serwisowe za pośrednictwem naszego systemu zgłoszeń...	\N	\N	\N	f	f	2025-11-10 13:18:00+01	2025-12-03 22:59:59+01	t	t	1	15	295	\N	\N	18
137	Inżynier MS Fabric z AI	Twój zakres obowiązków, System oparty na technologii chmurowej, zaprojektowany do gromadzenia, analizowania i wizualizacji danych związanych z produkcją zielonej energii. Rozwiązanie umożliwia monitorowanie kluczowych wskaźników, identyfikację...	\N	\N	\N	t	\N	2025-11-12 14:32:00+01	2025-11-23 22:59:59+01	t	f	1	299	296	\N	\N	18
138	Fullstack Developer	Twój zakres obowiązków, Projektowanie i rozwój aplikacji webowych w architekturze mikroserwisowej (Java + Angular)., Tworzenie i utrzymywanie logiki backendowej, Implementacja warstwy frontendowej., Projektowanie i optymalizacja komunikacji...	\N	\N	\N	t	\N	2025-11-12 14:20:00+01	2025-11-23 22:59:59+01	t	t	1	300	297	\N	\N	18
139	Koordynator/Koordynatorka projektów wdrożeniowych IT	Twój zakres obowiązków, Koordynowanie wszystkich faz projektów wdrożeniowych oprogramowania informatycznego na uczelni, od przygotowania wymagań, po odbiór końcowy., Opracowywanie szczegółowych planów projektowych, harmonogramów, budżetów oraz...	\N	\N	\N	f	f	2025-11-12 13:38:00+01	2025-12-05 22:59:59+01	f	f	1	301	298	\N	\N	18
140	Kierownik Zespołu Inżynieryjnego (Onboarding)	Twój zakres obowiązków, Dbanie o realizację zadań programistycznych zgodnie z obowiązującymi standardami technologicznymi,, wspieranie Product Managera w koordynowaniu prac zespołu w obszarze rozwiązań technologicznych,, współpraca z innymi...	25000.00	31000.00	f	t	\N	2025-11-12 14:15:00+01	2025-11-14 22:59:59+01	t	f	1	302	299	1	9	18
141	Analityk SOC - Poziom 1	Twój zakres obowiązków, Ciągłe śledzenie alertów generowanych przez systemy bezpieczeństwa (SIEM, EDR, IDS/IPS, firewall itp.)., Ocena priorytetu alertów oraz ich podstawowa klasyfikacja: prawdziwe zagrożenie, fałszywy alarm, błąd systemowy.,...	4000.00	12000.00	f	f	f	2025-11-12 13:36:28+01	2025-12-12 22:59:59+01	t	f	1	303	300	1	9	7
142	Analityk Jakości Danych	Twój zakres obowiązków, Projektowanie, rozwój i testowanie reguł Data Quality w oparciu o zdefiniowane standardy., Współpraca z analitykami Data Governance i interesariuszami biznesowymi przy definiowaniu wymagań jakości danych., Tworzenie...	100.00	140.00	f	t	\N	2025-11-12 13:08:00+01	2025-12-06 22:59:59+01	t	f	1	304	301	1	1	8
143	Administrator Aplikacji i Systemów IT	Twój zakres obowiązków, Zarządzanie, monitoring i utrzymanie systemu BLIK (serwery aplikacyjne, bazodanowe i inne komponenty infrastruktury), Wdrażanie nowych wersji aplikacji, analiza błędów biznesowych i technicznych, Automatyzacja procesów...	\N	\N	\N	f	f	2025-11-12 13:03:47+01	2025-12-12 22:59:59+01	t	f	1	305	302	\N	\N	18
144	Projektant / Projektantka botów konwersacyjnych (AI)	Twój zakres obowiązków, bierzesz udział w budowie i rozwoju rozwiązań AI dla ponad 10 milionów klientów PKO Banku Polskiego,, projektujesz i programujesz boty konwersacyjne (chatboty/voiceboty) wdrażane w kanale telefonicznym, aplikacji mobilnej i...	\N	\N	\N	f	f	2025-11-12 13:28:26+01	2025-12-12 22:59:59+01	f	f	1	293	303	\N	\N	18
145	Inżynier Oprogramowania Robotyki	Twój zakres obowiązków, projektowanie i implementacja algorytmów nawigacji autonomicznej w środowisku sklepowym,, optymalizacja i integracja istniejących rozwiązań SLAM, estymacji stanu, planowania ruchu,, tworzenie i utrzymanie pakietów ROS2,,...	8000.00	13000.00	\N	f	f	2025-11-12 11:10:06+01	2025-12-12 22:59:59+01	t	t	1	307	304	1	9	3
146	Stanowisko ds. wdrażania oprogramowania UE	Twój zakres obowiązków, koordynowanie wdrażania w Zakładzie oprogramowania udostępnianego przez UE, analizowanie funkcjonalności oprogramowania rekomendowanego przez UE w zakresie możliwości jego wykorzystania w Zakładzie, udział w pracach komisji...	\N	\N	\N	f	f	2025-11-12 11:08:14+01	2025-12-26 22:59:59+01	f	f	1	267	305	\N	\N	18
147	Analityk systemowy	Twój zakres obowiązków, analiza wymagań biznesowych i technicznych,, projektowanie i dokumentowanie procesów oraz rozwiązań systemowych,, definiowanie integracji pomiędzy systemami,, współpraca z zespołami technicznymi i biznesowymi,, tworzenie i...	\N	\N	\N	f	f	2025-11-12 13:09:00+01	2025-11-19 22:59:59+01	t	f	1	227	306	\N	\N	18
148	F/M: Tester Danych	Twój zakres obowiązków, Definiowanie skryptów testowych zgodnie z przyjętymi szablonami, umożliwiających automatyczne wykonywanie testów., Automatyczne generowanie skryptów testowych w oparciu o metadane dostępne w organizacji (model-driven...	100.00	135.00	f	t	\N	2025-11-12 13:09:00+01	2025-12-06 22:59:59+01	t	f	1	304	307	1	1	18
149	F/M: Specjalista ds. Zarządzania Danymi	Twój zakres obowiązków, Definiowanie, utrzymywanie i rozwijanie polityk, standardów oraz procesów Data Governance., Tworzenie i wdrażanie frameworku Data Governance, obejmującego hierarchizację danych, definicje przypadków użycia i standardy...	120.00	150.00	f	t	\N	2025-11-12 13:07:00+01	2025-12-06 22:59:59+01	t	f	1	304	308	1	1	18
150	Analityk Systemowy	Twój zakres obowiązków, prowadzenie analiz biznesowych i systemowych dla lidera rynku consumer finance w Polsce, udział w analizie, tworzeniu i rozwijaniu procesów banku, opracowywanie i analizowanie wymagań dla rozwiązań informatycznych w banku i...	100.00	130.00	f	t	\N	2025-11-12 12:52:00+01	2025-11-20 22:59:59+01	t	f	1	312	309	1	1	18
151	F/M: Tester Automatyzacji Danych	Twój zakres obowiązków, Budowa i rozwój frameworków do automatyzacji testów (szablony skryptów, automatyczne generowanie scenariuszy testowych, procedury przygotowania środowisk)., Automatyczne generowanie skryptów testowych na podstawie...	120.00	160.00	f	t	\N	2025-11-12 12:43:00+01	2025-12-21 22:59:59+01	t	f	1	304	310	1	1	18
152	Inżynier Danych	Twój zakres obowiązków, Projektowanie, rozwój i testowanie komponentów ETL/ELT (ingest, transformacje, ładowanie danych, workflow, dependencies, check-gates)., Zapewnienie skalowalności, wydajności i niezawodności przepływów danych.,...	140.00	165.00	f	t	\N	2025-11-12 12:42:00+01	2025-12-21 22:59:59+01	t	f	1	304	311	1	1	18
153	Płatny Staż w Dziale Cyfryzacji Produkcji	Twój zakres obowiązków, Dokonywanie podstawowej diagnostyki jak i rozwiązywanie problemów informatycznych oraz pomoc w rozwiązywaniu złożonych problemów we współpracy z Local-ITM,, Administracją związaną z IT, zarządzaniem zapasami (CMDB),,...	\N	\N	\N	f	f	2025-11-12 12:35:00+01	2025-12-07 22:59:59+01	f	f	1	315	312	\N	\N	18
154	Programista Symfony / React w Referacie ds. Rozwiązań Własnych w Centrum Obsługi Informatycznej	Twój zakres obowiązków, Do zakresu obowiązków wykonywanych na stanowisku będzie należało:, , a)\tDobór narzędzi i technologii używanych do realizacji rozwiązań własnych,, , b)\tWykonywanie zadań programistycznych na potrzeby Centrum,, ,...	\N	\N	\N	f	f	2025-11-12 12:27:02+01	2025-12-05 22:59:59+01	f	f	1	316	313	\N	\N	18
22	Programista Systemów Informatycznych	Twój zakres obowiązków, Opis czynności wykonywanych na stanowisku - zakres ważniejszych zadań:, a. rozwijanie istniejących już rozwiązań w firmie,, b. projektowanie, wytwarzanie i rozwój oprogramowania,, c. współpracę z zespołem programistów,...	\N	\N	\N	f	f	2025-11-12 08:04:09+01	2025-12-07 22:59:59+01	f	f	1	20	187	\N	\N	18
23	Młodszy Specjalista ds. Wsparcia IT (f/m/d)	Twój zakres obowiązków, Przygotowywanie i regularna aktualizacja raportów dotyczących działania systemów IT oraz zgłoszeń serwisowych, zapewniając ich wysoką jakość, dokładność i terminowość., Przeprowadzanie szczegółowej analizy danych,...	\N	\N	\N	f	f	2025-11-12 08:04:00+01	2025-12-07 22:59:59+01	f	f	1	23	188	\N	\N	18
25	Młodszy specjalista / Młodsza specjalistka ds. wsparcia IT	Twój zakres obowiązków, udzielanie wsparcia IT pracownikom firmy,, udzielanie wsparcia IT użytkownikom lokalnym i zdalnym,, rozwiązywanie problemów ze sprzętem i oprogramowaniem,, przygotowywanie stanowisk pracy dla pracowników,, współpraca z...	6000.00	8000.00	f	f	f	2025-11-11 17:08:37+01	2025-12-04 22:59:59+01	f	f	1	25	191	1	9	18
26	Specjalista ds. Wdrożeń Systemów Informatycznych	Twój zakres obowiązków, Analiza, optymalizacja, nadzór i skuteczna weryfikacja czasu usługi (normy pracy, czasy standardowe) oraz wykorzystania środków pracy., Sporządzanie raportów i analiz dotyczących funkcjonujących procesów., Testowanie i...	\N	\N	\N	f	f	2025-11-11 16:00:00+01	2025-12-01 22:59:59+01	f	f	1	26	193	\N	\N	18
27	Administrator IT (Systemy Windows)	Twój zakres obowiązków, Administracja usługami opartymi o technologie Microsoft - Active Directory, Entra ID, Exchange, DNS, DHCP, DFS., Administracja i utrzymanie serwerów opartych o systemy rodziny Windows Server., Administracja usługami...	\N	\N	\N	f	f	2025-11-11 16:00:00+01	2025-12-04 22:59:59+01	f	f	1	27	194	\N	\N	18
43	Młodszy specjalista / Młodsza specjalistka ds. wsparcia IT / HelpDesk	Twój zakres obowiązków, Świadczenie klientom  pomocy telefonicznej, mailowej bądź przy użyciu systemu ticketowego w zakresie rozwiązywania systemowych problemów technicznych, Współpraca z działem rozwoju developmentu oraz testów, Sprawdzanie...	\N	\N	\N	f	f	2025-11-11 15:49:31+01	2025-11-19 22:59:59+01	t	f	1	68	195	\N	\N	18
29	Analityk ds. Wsparcia i Operacji Systemów Informatycznych (K/M)	Twój zakres obowiązków, Świadczenie wsparcia systemów i sprzętu dla użytkowników,, Zarządzanie zgłoszeniami generowanymi przez wewnętrzny dział Obsługi Klienta, rozwiązywanie ich gdy to możliwe lub przekazywanie do dalszych poziomów działu;,...	5800.00	8000.00	f	f	f	2025-11-11 13:13:00+01	2025-11-26 22:59:59+01	t	f	1	29	197	1	9	18
30	Młodszy Informatyk	Twój zakres obowiązków, Dbanie o sprawne działanie sieci i systemów informatycznych w zakładzie, Nadzór nad oprogramowaniem komputerów; maszyn przemysłowych w firmie, Tworzenie kont użytkowników oraz przypisywanie do odpowiednich grup, Tworzenie...	\N	\N	\N	f	f	2025-11-11 12:57:00+01	2025-12-06 22:59:59+01	f	f	1	30	198	\N	\N	18
58	Specjalista Wsparcia IT	Twój zakres obowiązków, Wsparcie w diagnozowaniu oraz rozwiązywaniu zgłoszeń od użytkowników,, Przygotowywanie komputerów i innych urządzeń końcowych dla użytkowników,, Nadzorowanie poprawnej pracy lokalnej infrastruktury IT,, Prowadzenie...	\N	\N	\N	f	f	2025-11-10 12:11:00+01	2025-11-26 22:59:59+01	t	f	1	220	217	\N	\N	18
59	Młodszy administrator IT	Twój zakres obowiązków, obsługa użytkowników końcowych (helpdesk),, administracja i utrzymanie infrastruktury IT,, prowadzenie dokumentacji i ewidencji sprzętu informatycznego,, zapewnienie bezpieczeństwa systemu i wsparcia technicznego,,...	\N	\N	\N	f	f	2025-11-10 11:52:00+01	2025-11-20 22:59:59+01	f	f	1	221	218	\N	\N	18
60	Administrator IT	Twój zakres obowiązków, nadzór nad infrastrukturą IT, zapewnienie ciągłości działania systemów IT, nadzór nad kopiami bezpieczeństwa, wsparcie użytkowników (35 stanowisk biurowych + 30 stanowisk produkcyjnych), administracja systemami Windows...	\N	\N	\N	f	f	2025-11-10 11:15:00+01	2025-12-07 22:59:59+01	t	f	1	222	219	\N	\N	18
61	Płatny staż – Administrator IT (k/m)	Twój zakres obowiązków, Wsparcie użytkowników systemu zdalnego dostępu;, Monitorowanie wydajności infrastruktury;, Opracowywanie raportów;, Administracja istniejącą infrastrukturą serwerową;, Automatyzacja zadań przy pomocy skryptów;, Podstawowe...	\N	\N	\N	f	f	2025-11-10 11:13:00+01	2025-12-05 22:59:59+01	f	t	1	223	220	\N	\N	18
62	Specjalista ds. wsparcia IT	Twój zakres obowiązków, Monitorowanie i realizacja zgłoszeń zarejestrowanych w systemie zgłoszeniowym, Konfiguracja sprzętu komputerowego (notebooki, komputery, terminale i drukarki przemysłowe), Pomoc techniczna w zakresie IT dla użytkowników,...	\N	\N	\N	f	f	2025-11-10 10:11:00+01	2025-12-03 22:59:59+01	f	f	1	224	221	\N	\N	18
63	Specjalista Inżynieryjno-Techniczny	Twój zakres obowiązków, wdrożenie, utrzymanie i rozwój infrastruktury HPC wykorzystywanej w projekcie, instalacja, konfiguracja i optymalizacja oprogramowania naukowego, bieżące monitorowanie stanu sprzętu i systemów oraz usuwanie usterek,...	\N	\N	\N	f	f	2025-11-10 10:01:00+01	2025-11-30 22:59:59+01	t	f	1	225	222	\N	\N	18
64	Administrator IT	Twój zakres obowiązków, first line support, administracja sieci LAN/WAN/WLAN routing statyczny, NAT, VLAN, VPN - IPsec i, SSL, administracja urządzeniami brzegowymi, administracja serwerami MS Windows + AD, administrowanie systemami ERP,...	\N	\N	\N	f	f	2025-11-10 09:57:00+01	2025-11-26 22:59:59+01	f	f	1	226	223	\N	\N	18
65	Administrator IT	Twój zakres obowiązków, Administracja systemami Linux (Debian/Ubuntu)., Utrzymanie i konfiguracja kontenerów LXC/LXD., Obsługa serwerów w środowisku on-premise (IPMI/iDRAC, aktualizacje firmware)., Monitorowanie infrastruktury (Zabbix, Better...	\N	\N	\N	t	\N	2025-11-10 08:26:00+01	2025-11-13 22:59:59+01	t	f	1	227	224	\N	\N	18
66	Specjalista ds. zarządzania, ryzyka i zgodności (bezpieczeństwo IT)	Twoje obowiązki, Identyfikacja, analiza, ocena i dokumentowanie ryzyk (operacyjnych, finansowych, IT)., Opracowywanie strategii ich ograniczania, stałe monitorowanie oraz raportowanie do zarządu., Monitorowanie zmian w przepisach (np. RODO,...	\N	\N	\N	t	\N	2025-11-10 08:05:00+01	2025-12-03 22:59:59+01	f	f	1	39	225	\N	\N	7
67	Specjalista ds. informatyki przemysłowej	Twój zakres obowiązków, wsparcie Zespołu Utrzymania Ruchu w realizacji zadań na terenie komórek organizacyjnych Pionu Produkcji w zakresie przemysłowej infrastruktury informatycznej, systemów sterowania opartych na PLC oraz komputerowych systemów...	\N	\N	\N	f	f	2025-11-09 15:51:00+01	2025-11-16 22:59:59+01	t	f	1	229	226	\N	\N	18
68	Młodszy specjalista ds. wsparcia IT	Twój zakres obowiązków, Konfiguracja nowego sprzętu dla pracowników, Modernizacja stanowisko komputerowych, Diagnostyka oraz usuwanie usterek w sprzęcie oraz oprogramowaniu IT, Konfiguracja i wymiana telefonów, Zamawianie nowego sprzętu,...	\N	\N	\N	f	f	2025-11-09 13:33:00+01	2025-12-04 22:59:59+01	t	f	1	230	227	\N	\N	18
69	Administrator Systemów Informatycznych Backup	Twój zakres obowiązków, Administracja, konfiguracja, utrzymanie, monitoring Systemów Kopii Zapasowych (Backup)., Udział w projektach infrastrukturalnych i samodzielne wykonywanie prac rekonfiguracyjnych., Diagnozowanie i rozwiązywanie...	\N	\N	\N	f	f	2025-11-08 23:00:00+01	2025-12-08 22:59:59+01	t	f	1	7	228	\N	\N	18
70	Informatyk	Twój zakres obowiązków, wprowadzenie polityk zapewniających utrzymanie wysokich standardów dostępności, autentyczności, integralności i poufności danych,, opracowywanie strategii operacyjnej odporności cyfrowej,, wsparcie techniczne użytkowników...	5000.00	9000.00	f	f	f	2025-11-08 18:11:12+01	2025-11-23 22:59:59+01	f	f	1	232	229	1	9	18
85	Specjalista IT - administrator IT	Twój zakres obowiązków, Wsparcie użytkowników (helpdesk): rozwiązywanie problemów technicznych oraz bieżące wsparcie w korzystaniu z systemów informatycznych., Usuwanie awarii systemów IT: diagnoza i naprawa usterek w systemach oraz sprzęcie...	\N	\N	\N	t	\N	2025-11-07 08:21:52+01	2025-12-07 22:59:59+01	t	f	1	247	244	\N	\N	18
86	Młodszy specjalista ds. wsparcia IT	Twój zakres obowiązków, Wsparcie informatyczne użytkowników w rozwiązywaniu problemów technicznych związanych z aplikacjami, systemami, sprzętem oraz siecią komputerową, Konfiguracja, nadzór i utrzymywanie infrastruktury serwerowej, sieciowej oraz...	\N	\N	\N	f	f	2025-11-07 08:12:00+01	2025-11-23 22:59:59+01	t	f	1	248	245	\N	\N	18
87	IT help desk - informatyk	Twój zakres obowiązków, Diagnozowanie i rozwiązywanie problemów technicznych użytkowników  , Instalowanie i konfigurowanie oprogramowania oraz sprzętu komputerowego  , Wsparcie w zakresie systemów operacyjnych i aplikacji firmowych  ,...	6000.00	10000.00	f	f	f	2025-11-06 14:45:19+01	2025-12-06 22:59:59+01	t	f	1	249	246	1	9	18
88	Specjalista ds. bezpieczeństwa IT	Twój zakres obowiązków, monitorowanie bezpieczeństwa infrastruktury IT i reagowanie na incydenty,, zarządzanie systemami ochrony punktów końcowych (EDR, AV, ASR),, administracja usługami Microsoft 365: Intune, Entra ID, Defender, Sentinel, DLP,,...	\N	\N	\N	f	f	2025-11-06 14:16:00+01	2025-11-21 22:59:59+01	f	f	1	250	247	\N	\N	7
89	Informatyk	Twój zakres obowiązków, Praca według wymaganych obowiązków...	\N	\N	\N	f	f	2025-11-06 13:19:39+01	2025-11-29 22:59:59+01	t	f	1	251	248	\N	\N	18
90	Administrator IT	Twój zakres obowiązków, Zakres obowiązków (skrótowo), Utrzymanie i rozwój infrastruktury IT (serwery, sieć, stacje robocze)., Administracja środowiskiem Windows Server i Active Directory, Microsoft365., Utrzymanie kopii zapasowych, aktualizacji,...	\N	\N	\N	f	f	2025-11-06 13:08:00+01	2025-11-21 22:59:59+01	f	f	1	252	249	\N	\N	18
91	Specjalista IT / Administrator IT	Twój zakres obowiązków, wsparcie pracowników i obsługa systemów IT (finanse, sprzedaż, logistyka, infrastruktura, sieci),, diagnozowanie i rozwiązywanie problemów sprzętowych oraz aplikacyjnych,, tworzenie raportów i analiz (Excel, SQL),,...	\N	\N	\N	f	f	2025-11-06 12:30:22+01	2025-12-06 22:59:59+01	f	t	1	253	250	\N	\N	18
92	Administrator ds. bezpieczeństwa IT (K/M)	Twój zakres obowiązków, monitorowanie i rozwój systemów bezpieczeństwa (SIEM/SOAR, EDR/XDR),, zarządzanie podatnościami środowiska IT za pomocą narzędzi VMS,, reagowanie i analiza incydentów w ramach drugiej linii wsparcia,, udział w testowaniu i...	\N	\N	\N	f	f	2025-11-06 12:03:00+01	2025-11-22 22:59:59+01	f	f	1	254	251	\N	\N	7
93	Analityk ds. bezpieczeństwa IT (K/M)	Twój zakres obowiązków, monitorowanie i analiza zdarzeń bezpieczeństwa (SIEM, EDR/XDR),, reagowanie i wstępna analiza incydentów oraz ich i eskalacja do starszych specjalistów,, udział w testowaniu i rozwoju rozwiązań bezpieczeństwa,, współpraca z...	\N	\N	\N	f	f	2025-11-06 11:57:00+01	2025-11-22 22:59:59+01	f	f	1	254	252	\N	\N	7
94	Administrator systemu informatycznego	Twój zakres obowiązków, Zadaniem ASI będzie operacyjne zarządzanie systemami informatycznymi, kontrola przepływu informacji, weryfikowanie bezpieczeństwa przepływu informacji i dostępu do nich, stosowanie odpowiednich środków technicznych...	\N	\N	\N	t	\N	2025-11-06 10:41:23+01	2025-12-06 22:59:59+01	f	f	1	256	253	\N	\N	18
95	Specjalista ds. wsparcia IT	Twój zakres obowiązków, Bezpośrednie oraz zdalne wsparcie użytkowników w zakresie stacji roboczych, urządzeń peryferyjnych, urządzeń mobilnych, Instalacja, konfiguracja i aktualizacja stacji roboczych, Zarządzanie zasobami IT w zakresie stacji...	\N	\N	\N	f	f	2025-11-06 10:34:00+01	2025-11-29 22:59:59+01	f	f	1	257	254	\N	\N	18
96	Informatyk - Specjalista ds. wsparcia IT	Twój zakres obowiązków, W związku z rozwojem przedsiębiorstwa poszukujemy kolejnej osoby na nowe stanowisko pracy. Nasze przedsiębiorstwo - Sunseco świadczy szeroko rozumiane usługi informatyczne. Głównym nurtem działania przedsiębiorstwa jest...	\N	\N	\N	f	f	2025-11-06 09:49:34+01	2025-12-06 22:59:59+01	t	f	1	258	255	\N	\N	18
97	Specjalista Wsparcia IT	Twój zakres obowiązków, stałe rozwijanie kompetencji technicznych i miękkich,, diagnozowanie oraz rozwiązywanie bieżących i systemowych problemów technicznych,, zapewnienie kompleksowej opieki informatycznej dla klientów firmy,, tworzenie i...	7000.00	13000.00	f	f	f	2025-11-06 08:56:00+01	2025-11-29 22:59:59+01	t	f	1	259	256	1	9	18
98	Informatyk	Twój zakres obowiązków, administracja serwerami pracującymi pod kontrolą systemów z rodziny Linux (Oracle Linux, Debian, Ubuntu, Proxmox), administracja usługami/oprogramowaniem (uruchomionymi na powyższych serwerach):, OpenLDAP, PostgreSQL,...	6400.00	6900.00	f	f	f	2025-11-05 18:10:51+01	2025-12-05 22:59:59+01	f	f	1	260	257	1	9	18
99	Administrator / Administratorka systemów Informatycznych i aplikacji biznesowych	Twój zakres obowiązków, Pełnienie nadzoru i opieki technicznej nad systemami informatycznymi /aplikacjami biznesowymi;, Administrowanie certyfikatami niezbędnymi do wykonywania zadań przez pracowników banku;, Udział w projektach dotyczących...	\N	\N	\N	f	f	2025-11-05 18:10:49+01	2025-12-05 22:59:59+01	f	f	1	261	258	\N	\N	18
100	Informatyk	Twój zakres obowiązków, Stały nadzór nad funkcjonowaniem systemów wspomagających zarządzanie firmą, Planowanie, realizacja i ocena dokonywanych wdrożeń i rozwoju systemu, Instalacja oraz konfiguracja serwerów i uaktualnień, stacji roboczych,...	6500.00	6880.00	f	f	f	2025-11-05 16:10:53+01	2025-12-05 22:59:59+01	t	f	1	262	259	1	9	18
101	Starszy informatyk - helpdesk	Twój zakres obowiązków, Pierwsza linia wsparcia technicznego – przyjmowanie, rejestrowanie i obsługa zgłoszeń,, Bezpośrednie, telefoniczne oraz zdalne wsparcie techniczne użytkowników w zakresie wybranych aplikacji, oprogramowania, urządzeń...	7000.00	8000.00	f	f	f	2025-11-05 15:19:02+01	2025-12-05 22:59:59+01	f	f	1	263	260	1	9	18
102	Asystent / Asystentka ds. wsparcia IT	Twój zakres obowiązków, Wsparcie IT (lokalne i zdalne) – pierwsza linia pomocy, Rozwiązywanie problemów ze sprzętem, oprogramowaniem i siecią, Instalacja i konfiguracja urządzeń IT (PC, laptopy, peryferia, sieć), Wsparcie administracyjne –...	\N	\N	\N	f	f	2025-11-05 14:28:34+01	2025-11-15 22:59:59+01	f	f	1	264	261	\N	\N	18
103	Specjalista / Specjalistka ds. wsparcia IT	Twój zakres obowiązków, Opis stanowiska:, , Stanowisko Specjalisty ds. wsparcia IT polega na bieżącej obsłudze naszych klientów. Pomoc świadczona jest zarówno zdalnie, jak i lokalnie w siedzibach naszych klientów.&nbsp;, , Młodszy Specjalista odpowiada...	4666.00	6666.00	\N	f	f	2025-11-05 14:01:54+01	2025-11-15 22:59:59+01	t	t	1	265	262	1	9	18
104	Specjalista ds. Bezpieczeństwa Sieci LTE	Twój zakres obowiązków, Współpraca z architektami bezpieczeństwa ICT w projektowaniu rozwiązań dla systemów telekomunikacyjnych,, Aktywny udział we wdrażaniu i obsłudze rozwiązań bezpieczeństwa ICT dla sieci telekomunikacyjnej LTE450,, Przeglądy...	\N	\N	\N	f	f	2025-11-05 10:06:00+01	2025-11-20 22:59:59+01	f	f	1	266	263	\N	\N	39
105	Stanowisko ds. informatyki w zakresie statystyki i prognoz aktuarialnych	Twój zakres obowiązków, generuje raporty z systemów informatycznych (SQL), uczestniczy w projektowaniu, modyfikacji, administrowaniu oraz eksploatacji systemów informatycznych wspomagających prace departamentu, tworzy oprogramowanie w zakresie...	\N	\N	\N	f	f	2025-11-05 08:07:16+01	2025-11-18 22:59:59+01	f	f	1	267	264	\N	\N	18
106	Specjalista ds. informatyki (k/m)	Twój zakres obowiązków, przygotowanie i konfiguracja nowych stanowisk komputerowych, modernizacja i aktualizacja stanowisk komputerowych (sprzęt i oprogramowanie), administrowanie systemami biurowymi (domena, SAP, usługa katalogowa Active...	\N	\N	\N	f	f	2025-11-05 07:48:00+01	2025-11-20 22:59:59+01	f	f	1	12	265	\N	\N	18
107	Specjalista ds. Bezpieczeństwa IT	Twój zakres obowiązków, Planowanie, projektowanie oraz wsparcie wdrożeń w obszarze architektury bezpieczeństwa systemów i usług, Definiowanie i utrzymywanie konfiguracji bezpieczeństwa dla kluczowych usług, Współpraca z zespołami technicznymi w...	\N	\N	\N	f	f	2025-11-04 15:51:00+01	2025-11-20 22:59:59+01	f	f	1	269	266	\N	\N	7
108	Młodszy Administrator IT	Twój zakres obowiązków, Konfiguracja i administracja środowiska Proxmox VE (wirtualne maszyny, kontenery, backupy, sieć)., Składanie, instalacja i konfiguracja komputerów dla pracowników (Windows / Linux)., Konfiguracja i utrzymanie routerów,...	\N	\N	\N	f	f	2025-11-04 13:06:33+01	2025-11-27 22:59:59+01	t	f	1	270	267	\N	\N	18
109	Specjalista ds. Wsparcia IT	Twój zakres obowiązków, Diagnozowanie i naprawa problemów zgłaszanych przez klientów w zakresie sprzętu i oprogramowania, Instalacja i konfiguracja systemów klienckich z rodziny Microsoft Windows, Podstawowa konfiguracja urządzeń sieciowych,...	\N	\N	\N	f	f	2025-11-04 11:22:00+01	2025-11-19 22:59:59+01	t	f	1	271	268	\N	\N	18
110	Specjalista / Specjalistka ds. informatyki	Twój zakres obowiązków, wsparcie techniczne dla pracowników Instytutu - zdalnie oraz lokalnie,, rejestrowanie zgłoszonych problemów oraz incydentów,, obsługa informatyczna placówek Instytutu,, konfiguracja sprzętu komputerowego oraz drukarek,,...	\N	\N	\N	f	f	2025-11-04 10:56:46+01	2025-12-04 22:59:59+01	f	f	1	272	269	\N	\N	18
111	Specjalista ds. bezpieczeństwa IT (m/k)	Twój zakres obowiązków, identyfikacja zagrożeń bezpieczeństwa w systemach informatycznych Grupy Kapitałowej, konsultowanie realizowanych projektów wdrożeniowych i programistycznych w zakresie bezpieczeństwa informatycznego, przygotowywanie analiz...	\N	\N	\N	f	f	2025-11-04 10:31:00+01	2025-11-19 22:59:59+01	t	f	1	273	270	\N	\N	7
112	Młodszy Specjalista ds. Wsparcia IT / Junior IT Support Specialist	Twój zakres obowiązków, przyjmowanie i obsługa zgłoszeń w systemie Ready_™ Support,, odpowiedzialność za obsługiwane zgłoszenia od momentu przyjęcia aż do ich rozwiązania,, rozpisywanie zadań w ramach zgłoszeń i dbanie o ich prawidłowy workflow,,...	\N	\N	\N	f	f	2025-11-04 07:53:00+01	2025-11-19 22:59:59+01	t	f	1	274	271	\N	\N	18
113	Starszy Programista Oracle	Twój zakres obowiązków, rozwijanie i utrzymywanie systemów opartych o bazy danych Oracle, rozwijanie aplikacji klienckich opartych na technologiach Oracle, Java, J2EE, czynne wspieranie modeli wydań dla utrzymywanych systemów, obsługiwanie procesu...	\N	\N	\N	f	f	2025-11-12 23:00:00+01	2025-12-12 22:59:59+01	f	f	1	275	272	\N	\N	18
114	Specjalista / Specjalistka ds. wsparcia IT	Twój zakres obowiązków, Bezpośrednie wsparcie użytkowników w zakresie pomocy z komputerami i aplikacjami., Proste prace administracyjne w zakresie rekonfiguracji połączeń sieciowych., Nadzór nad prawidłowym funkcjonowaniem usług IT w lokalnym...	\N	\N	\N	f	f	2025-11-03 12:24:19+01	2025-12-03 22:59:59+01	f	f	1	276	273	\N	\N	18
115	Informatyk - Administrator sieci	Twój zakres obowiązków, Administracja i utrzymanie sieci firmowej opartej na urządzeniach MikroTik (RouterOS, CCR, CAPsMAN) oraz Ubiquiti (UniFi, UISP, Access Pointy, Switch’e)., Monitorowanie i optymalizacja działania sieci (VLAN, QoS, VPN,...	5500.00	6500.00	f	f	f	2025-11-03 11:57:04+01	2025-12-03 22:59:59+01	f	f	1	277	274	1	9	18
116	Specjalista ds. wsparcia IT (helpdesk)	Twój zakres obowiązków, Obsługa zgłoszeń dotyczących sprzętu i oprogramowania, w tym diagnozowanie i rozwiązywanie problemów użytkowników przy użyciu dostępnych środków., Analiza zgłoszeń i przekierowywanie ich do odpowiednich linii wsparcia,...	\N	\N	\N	f	f	2025-11-03 09:52:00+01	2025-11-19 22:59:59+01	f	f	1	278	275	\N	\N	18
117	Administrator / Administratorka systemów informatycznych	Twój zakres obowiązków, praca z systemami konteneryzacji, takimi jak Docker i Kubernetes,, administracja systemami opartymi na systemie Linux,, konfiguracja i utrzymywanie serwerów www oraz reverse proxy (HAproxy, Nginx, Apache),, zarządzenie...	\N	\N	\N	f	f	2025-11-02 16:00:00+01	2025-11-25 22:59:59+01	f	f	1	279	276	\N	\N	18
118	IT Security Manager	Tworzenie i wdrażanie polityki bezpieczeństwa IT – opracowywanie zasad, procedur i standardów ochrony informacji w organizacji., Nadzór nad bezpieczeństwem infrastruktury IT – monitorowanie i zarządzanie systemami...	\N	\N	\N	f	f	2025-11-01 12:52:00+01	2025-11-16 22:59:59+01	f	f	1	280	277	\N	\N	7
155	Pracownik Działu Utrzymania Klienta	Twój zakres obowiązków, przyjmowanie, rozwiązywanie i odpowiadanie na zgłoszenia klientów;, tworzenie procedur, narzędzi usprawniających proces diagnozowania problemów;, testowanie sprzętu i oprogramowania....	\N	\N	\N	f	f	2025-11-12 12:05:00+01	2025-11-28 22:59:59+01	f	f	1	317	314	\N	\N	18
156	Administrator aplikacji	Twój zakres obowiązków, Administracja środowiskami aplikacyjnymi opartymi o: Weblogic, JBoss, Tomcat, IIS, Apache, Utrzymanie i rozwój środowisk kontenerowych (OpenShift, Podman), Prowadzenie analiz bezpieczeństwa i wydajności dla zarządzanych...	\N	\N	\N	f	f	2025-11-12 11:53:00+01	2025-12-07 22:59:59+01	f	f	1	318	315	\N	\N	18
157	Analityk danych	Twój zakres obowiązków, Rozwój i utrzymanie hurtowni danych, Modelowanie i integracja danych z różnych baz i systemów, Budowa rozwiązań analitycznych w celu automatyzacji procesów raportowych i operacyjnych, Analiza, interpretacja danych i...	\N	\N	\N	f	f	2025-11-12 11:42:00+01	2025-12-05 22:59:59+01	t	f	1	319	316	\N	\N	18
158	Młodszy Specjalista ds. Raportowania	Tworzenie i dostarczanie cyklicznych i ad hoc'owych raportów wspierających decyzje biznesowe Działu Zakupów, Współodpowiedzialność za kontrolę procesu ISO / QMS w ramach wprowadzania do sprzedaży produktów marki własnej,...	\N	\N	\N	f	f	2025-11-12 11:33:50+01	2025-12-12 22:59:59+01	t	f	1	320	317	\N	\N	18
159	Stanowisko ds. nowych technologii IT	Twój zakres obowiązków, analizowanie potrzeb biznesowych pod kątem możliwości wykorzystania w nich nowych technologii (w tym rozwiązań z zakresu sztucznej inteligencji),, udział w zespołach projektowych i proponowanie rozwiązań z zastosowaniem...	\N	\N	\N	f	f	2025-11-12 11:08:22+01	2025-11-26 22:59:59+01	f	f	1	267	318	\N	\N	18
160	Architekt Rozwiązań IT (Solution Architect) – obszar testów automatycznych	Twój zakres obowiązków, Analiza i optymalizacja obecnych procesów deweloperskich i testowych w celu zwiększenia efektywności i jakości produktu., Opracowanie i uzgodnienie strategii transformacji testów automatycznych na poziomie zarządczym.,...	\N	\N	\N	f	f	2025-11-12 11:03:00+01	2025-12-05 22:59:59+01	t	f	1	227	319	\N	\N	1
161	Kierownik Działu Innowacji ( lotnictwo &amp; IT &amp; AI )	Twój zakres obowiązków, Zarządzanie zespołem R&amp;D w obszarze technologii ATM (specjaliści, analitycy, inżynierowie AI/ML, kierownicy projektów)., Identyfikacja potrzeb rozwojowych i wyznaczanie kierunków badań technologicznych w obszarze ATM i IT.,...	19000.00	23000.00	f	f	f	2025-11-12 10:27:00+01	2025-11-28 22:59:59+01	f	f	1	323	320	1	9	18
162	Starszy Administrator Sieci	Twój zakres obowiązków, Administracja siecią Data Center Wirtualnej Polski, Zarządzanie siecią WAN, Współudział w projektowaniu usług WP, Współpraca z zespołami DevOps, Administratorami i produktowymi, Automatyzacja i optymalizacja procesów...	\N	\N	\N	f	f	2025-11-12 10:24:34+01	2025-12-12 22:59:59+01	f	f	1	324	321	\N	\N	18
164	Architekt Systemów IT	Twój zakres obowiązków, Pełnienie roli architekta rozwiązań w wybranych projektach – odpowiedzialność za spójność techniczną, koncepcję architektoniczną oraz podejmowanie i egzekucję decyzji architektonicznych, Wsparcie przy definiowaniu celów...	\N	\N	\N	f	f	2025-11-12 10:00:00+01	2025-11-23 22:59:59+01	t	f	1	326	323	\N	\N	1
165	Analityk Systemowo-Biznesowy	Twój zakres obowiązków, Prowadzenie analiz systemowych i biznesowych dla zmian w produktach, procesach i systemach, Zbieranie, uzgadnianie i dokumentowanie wymagań biznesowych oraz systemowych (funkcjonalnych i niefunkcjonalnych), Przekładanie...	\N	\N	\N	f	f	2025-11-12 10:00:00+01	2025-11-23 22:59:59+01	t	f	1	326	324	\N	\N	18
166	Analityk / Analityczka Jakości Danych w Biurze Zarządzania Danymi	Twój zakres obowiązków, Zbieranie wymagań jakościowych od Stewardów Danych,, Przygotowywanie i aktualizację metryk jakości danych oraz ich implementację w ramach procesu monitorowania jakości danych;, Przygotowywanie i prezentowanie raportów...	\N	\N	\N	f	f	2025-11-12 09:58:47+01	2025-12-12 22:59:59+01	f	f	1	328	325	\N	\N	8
167	Starszy Specjalista Techniczny Wsparcia Operacyjnego	Twój zakres obowiązków, Współpraca z koncernem oraz lokalnymi dostawcami usług mobilnych w zakresie rozwiązywania problemów technicznych (dźwigi, łączność, awarie)., Udział w lokalnych i międzynarodowych projektach pilotażowych., Prowadzenie...	\N	\N	\N	f	f	2025-11-12 09:47:17+01	2025-12-12 22:59:59+01	t	f	1	329	326	\N	\N	12
168	Młodszy Wdrożeniowiec	Twój zakres obowiązków, wdrażanie systemów informatycznych wspierających pracę instytucji finansowych,, przygotowywanie konfiguracji biznesowej i technicznej,, prowadzenie warsztatów z klientami,, przygotowywanie dokumentacji wdrożeniowej,, praca...	\N	\N	\N	f	f	2025-11-12 09:45:00+01	2025-12-07 22:59:59+01	f	f	1	144	327	\N	\N	18
169	Fullstack Java Developer	Twój zakres obowiązków, Wsparcie techniczne mniej doświadczonych programistów, Tworzenie wysokiej jakości kodu, Projektowanie dokumentowanie rozwiązań technicznych, Przeprowadzanie testów jednostkowych, Współpraca z Analitykami biznesowymi,...	\N	\N	\N	f	f	2025-11-12 09:35:10+01	2025-12-12 22:59:59+01	t	f	1	75	328	\N	\N	18
170	Technical Delivery Manager	Twój zakres obowiązków, Prowadzenie i koordynacja end-to-end procesu dostarczania rozwiązań technologicznych w środowiskach zwinnych i DevOps., Koordynacja pracy zespołów technicznych: frontend, backend, DevOps, QA, architektura, zapewniając...	120.00	140.00	f	t	\N	2025-11-12 09:16:28+01	2025-12-12 22:59:59+01	t	f	1	332	329	1	1	24
171	Administrator/ka Systemów IT	Twój zakres obowiązków, Nadzór nad poprawnym działaniem systemów, ich konfiguracją, dostępnością i wydajnością, w tym okresowe przeglądy wydajności i pojemności, Zapewnienie bezpieczeństwa danych i systemów, wdrażanie polityk bezpieczeństwa,...	\N	\N	\N	f	f	2025-11-12 08:59:00+01	2025-12-05 22:59:59+01	f	f	1	292	330	\N	\N	18
172	Programista Oprogramowania Wbudowanego (cyberbezpieczeństwo)	Twój zakres obowiązków, Analiza wymagań klienta w projekcie, m.in. pod kątem cybersecurity, Przygotowanie specyfikacji i przeglądu wymagań oprogramowania, Rewizja architektury oprogramowania i interfejsów, Wdrażanie oprogramowania, Obsługa i...	9000.00	13000.00	f	f	f	2025-11-12 08:48:00+01	2025-11-27 22:59:59+01	f	f	1	334	331	1	9	7
173	Analityk Systemowy	Twój zakres obowiązków, współpraca z zespołem handlowym przy inicjowaniu projektów oraz z zespołami programistycznymi, wdrożeniowymi i testerskimi na etapie ich realizacji,, udział w projektach realizowanych zgodnie z metodykami zwinnymi (Agile),,...	\N	\N	\N	f	f	2025-11-12 08:34:37+01	2025-12-12 22:59:59+01	f	f	1	144	332	\N	\N	18
174	Architekt ds. Sieci Transmisyjnej	Tworzenie skalowalnej, niezawodnej i bezpiecznej architektury sieciowej zgodnej z wymaganiami biznesowymi i technicznymi,, Dobór technologii, protokołów (np. światłowody, SDH, DWDM, MPLS, SD-WAN) oraz urządzeń sieciowych...	11000.00	14000.00	f	f	f	2025-11-12 08:24:45+01	2025-12-12 22:59:59+01	f	f	1	336	333	1	9	1
175	Specjalistka / Specjalista ds. Informacji Zarządczej	Tworzenie raportów oraz analiza wyników zarządczych, które trafiają bezpośrednio do osób decyzyjnych;, Zapewnienie jakości i spójności danych w kluczowych bazach – Twoja praca będzie gwarancją wiarygodnych informacji;,...	\N	\N	\N	f	f	2025-11-12 08:20:00+01	2025-12-07 22:59:59+01	t	f	1	337	334	\N	\N	18
176	Kierownik projektów telekomunikacyjnych	Twój zakres obowiązków, Planowanie i koordynacja działań projektowych i budowlanych., Sporządzanie wycen na podstawie udostępnionych cenników., Nadzór postępu prac., Tworzenie harmonogramów i kontrola ich realizacji., Monitorowanie budżetu i ryzyk...	\N	\N	\N	t	\N	2025-11-12 07:51:59+01	2025-11-27 22:59:59+01	t	f	1	338	335	\N	\N	39
177	Specjalista ds. Administracji i Wsparcia Systemów	Twój zakres obowiązków, Administrowanie systemami WMS, MES i ERP (SAP), Pełnienie roli pierwszej linii wsparcia dla użytkowników systemów, obsługa zgłoszeń serwisowych oraz analiza i rozwiązywanie bieżących problemów, Konfiguracja i modyfikacje...	\N	\N	\N	f	f	2025-11-12 07:34:45+01	2025-12-12 22:59:59+01	t	f	1	339	336	\N	\N	18
178	Tester manualny / Testerka manualna	Twój zakres obowiązków, Wykonywanie różnego rodzaju testów oprogramowania (funkcjonalnych, regresyjnych, retestów)., Analiza i identyfikacja przyczyn błędów oraz ich rejestrowanie wraz z krokami do replikacji., Raportowanie wyników testów w...	\N	\N	\N	f	f	2025-11-11 16:11:47+01	2025-11-19 22:59:59+01	t	f	1	68	337	\N	\N	18
179	Inżynier Systemowy	Twój zakres obowiązków, Cześć, zanim zaczniesz czytać to ogłoszenie, dwa słowa od nas., , Wiemy, jak ważna w rozwoju zawodowym jest jasna ścieżka kariery. Właśnie dlatego nasi najlepsi architekci systemów IT to osoby, które przeszły pełną drogę —...	\N	\N	\N	f	f	2025-11-11 16:00:00+01	2025-12-05 22:59:59+01	f	f	1	341	338	\N	\N	18
180	Ekspert ds. Analiz i Modelowania Ryzyka Kredytowego	Twój zakres obowiązków, budowa, rozwój, utrzymanie i monitoring modeli analitycznych i scoringowych (predykcyjnych, segmentacyjnych) obejmujących: ryzyko kredytowe i fraudowe, procesy monitoringu i windykacji, procesy sprzedażowe, inne procesy...	\N	\N	\N	f	f	2025-11-11 13:31:00+01	2025-12-06 22:59:59+01	t	f	1	342	339	\N	\N	8
181	Full-stack .NET Web Developer	Your responsibilities, Projektowanie, rozwój i utrzymanie aplikacji .NET (C#) w środowisku chmurowym., Praca z REST API, integracjami systemowymi i Azure DevOps., Wsparcie architekta oprogramowania w projektowaniu rozwiązań technicznych., Udział w...	150.00	160.00	f	t	\N	2025-11-11 11:24:00+01	2025-11-29 22:59:59+01	t	f	1	343	340	1	1	18
182	Specjalista Wsparcia Technicznego L1	Twój zakres obowiązków, Obsługa zgłoszeń klientów – odbieranie i rozwiązywanie zgłoszeń pierwszej linii poprzez system ticketowy, email i (sporadycznie) telefon, Diagnozowanie problemów – identyfikacja i podstawowa diagnostyka problemów...	\N	\N	\N	f	f	2025-11-11 16:54:16+01	2025-11-26 22:59:59+01	t	f	1	344	341	\N	\N	18
183	Starszy Konsultant - Microsoft Dynamics 365 F&amp;O	Twój zakres obowiązków, Projektowanie i wdrażanie rozwiązań Microsoft Dynamics 365 F&amp;O oraz zarządzanie jakością projektów wdrożeniowych, Analiza potrzeb biznesowych i wymagań klientów, Współpraca z zespołami programistycznymi w ramach tworzenia...	\N	\N	\N	t	\N	2025-11-11 16:00:00+01	2025-11-22 22:59:59+01	f	f	1	345	342	\N	\N	18
184	Starszy Inżynier Oprogramowania	Twój zakres obowiązków, Implementacja i rozwój potoków wnioskowania modeli głębokiego uczenia (PyTorch / TensorFlow)., Przetwarzanie wstępne danych z czujników i obrazu, synchronizacja i fuzja danych z inferencji,, Integracja systemu z...	\N	\N	\N	t	\N	2025-11-11 16:00:00+01	2025-12-01 22:59:59+01	t	f	1	346	343	\N	\N	18
185	Analityk Biznesowo–Systemowy / Analityczka Biznesowo-Systemowa	Twój zakres obowiązków, Prowadzenie analizy biznesowo-systemowej zgłoszonych inicjatyw,, Opracowywanie koncepcji rozwiązania dla zagadnień/potrzeb biznesowych,, Opracowanie możliwych scenariuszy działania, wskazanie konsekwencji ich realizacji,,...	\N	\N	\N	f	f	2025-11-11 15:33:00+01	2025-11-26 22:59:59+01	f	f	1	347	344	\N	\N	18
186	Analityk SOC L2 - Tribe Security	Twój zakres obowiązków, Analiza przyczyn oraz wyjaśnienie incydentów bezpieczeństwa IT, Obsługa, utrzymanie, wdrażanie i rozwój systemów wspomagających monitorowanie bezpieczeństwa IT, Utrzymanie i rozwój produktów bezpieczeństwa dla Klientów...	\N	\N	\N	f	f	2025-11-11 14:33:00+01	2025-11-26 22:59:59+01	f	f	1	348	345	\N	\N	7
187	Ekspert/ka ds. Systemu Zarządzania Bezpieczeństwem Informacji	Twój zakres obowiązków, Utrzymanie i rozwój Systemu Zarządzania Bezpieczeństwem Informacji (SZBI) zgodnie z normą ISO/IEC 27001., Planowanie, koordynowanie i realizacja audytów wewnętrznych oraz przeglądów SZBI., Opracowywanie, aktualizacja i...	\N	\N	\N	f	f	2025-11-11 14:26:00+01	2025-12-04 22:59:59+01	f	f	1	349	346	\N	\N	7
188	Młodszy Programista T-SQL C#	Twój zakres obowiązków, Utrzymanie baz danych oraz procesów przetwarzania danych w systemach klasy enterprise, Tworzenie zapytań, funkcji i procedur w języku T-SQL, Analiza danych, Optymalizacja istniejących rozwiązań...	\N	\N	\N	t	\N	2025-11-11 14:25:00+01	2025-12-04 22:59:59+01	t	f	1	350	347	\N	\N	18
189	Ekspert Database Developer (Oracle)	Twój zakres obowiązków, projektowanie i tworzenie struktur bazodanowych,, projektowanie i tworzenie algorytmów przetwarzania danych,, implementacja funkcjonalności z użyciem PLSQL i SQL,, optymalizacja zapytań,, nadzór technologiczny nad...	\N	\N	\N	f	f	2025-11-11 14:21:00+01	2025-11-30 22:59:59+01	f	f	1	288	348	\N	\N	18
190	Administrator Systemów Bankowych	Your responsibilities, Utrzymanie i rozwój systemów oraz platform bankowych,, Udział w projektach wdrażających nowe technologie (Docker, Kubernetes, WebMethods, WebLogic),, Monitorowanie dostępności i wydajności aplikacji oraz reagowanie na...	\N	\N	\N	f	f	2025-11-11 06:13:00+01	2025-11-16 22:59:59+01	t	f	1	312	349	\N	\N	18
191	Tester manualny | Branża Finansowa	Twój zakres obowiązków, Testowanie rozwiązań IT, Zapewnienie funkcjonalnej zgodności oprogramowania z wymaganiami technicznymi i biznesowymi, Realizacja testów funkcjonalnych systemów zgodnie z dokumentacją i założeniami biznesowymi, Tworzenie...	\N	\N	\N	t	\N	2025-11-11 14:14:00+01	2025-11-14 22:59:59+01	t	f	1	75	350	\N	\N	18
192	Specjalista/Specjalistka ds. wdrożeń i utrzymania systemów IT dla uczelni	Twój zakres obowiązków, Koordynowanie i realizacja procesu wdrożenia produktów/usług u klientów, Zbieranie i analiza wymagań biznesowych oraz technicznych klientów, aby dostarczyć rozwiązania dopasowane do ich specyficznych potrzeb, Konfigurowanie...	8000.00	10000.00	\N	t	\N	2025-11-11 14:14:00+01	2025-12-05 22:59:59+01	t	f	1	354	351	1	9	18
193	Data Scientist w CRM (Modelowanie &amp; Analizy)	Twój zakres obowiązków, Budowa i rozwój nowego środowiska CRM w drugim największym Banku w Polsce, Tworzenie modeli Deep Learning i Machine Learning w oparciu o wymagania biznesowe oraz dostępne dane (m.in. PtB, segmentacyjne, klasyfikacyjne,...	\N	\N	\N	f	f	2025-11-11 14:05:00+01	2025-11-26 22:59:59+01	f	f	1	328	352	\N	\N	8
194	Programista Java	Twój zakres obowiązków, Projektowanie, implementacja i utrzymanie usług backendowych w oparciu o Java, Spring Boot i Groovy., Tworzenie i rozwój procesów biznesowych (BPMN) oraz integracji systemowych w architekturze mikroserwisowej.,...	\N	\N	\N	t	\N	2025-11-11 13:27:00+01	2025-11-14 22:59:59+01	t	f	1	227	353	\N	\N	18
195	T Business - Architekt Rozwiązań ICT (Presales)	Twój zakres obowiązków, Projektowanie rozwiązań ICT dla Klientów rynku biznesowego., Specjalizacja w obszarze rozwiązań cyberbezpieczeństwa lub  specjalizacja w projektowaniu rozwiązań sieciowych takich jak SDWAN-y, IPVPN-y, WiFi, LAN, Wsparcie...	\N	\N	\N	t	\N	2025-11-11 13:26:00+01	2025-11-26 22:59:59+01	f	f	1	348	354	\N	\N	1
196	Ekspert / Ekspertka ds. Procesów IT	Twój zakres obowiązków, Właścicielstwo trzech procesów IT: Zarządzanie ciągłością Usług IT, Zarządzanie dostępnością Usług IT oraz Zarządzanie katalogiem usług IT (jako właściciel/ka będziesz odpowiedzialny za poprawne zdefiniowanie procesu, za...	\N	\N	\N	f	f	2025-11-11 12:55:00+01	2025-11-26 22:59:59+01	f	f	1	358	355	\N	\N	18
197	Starszy Administrator Infrastruktury Sieciowej (F5 WAF) (k/m)	Twój zakres obowiązków, Administracja load balancerami F5 w zakresie LTM i WAF, Administracja urządzeniami sieciowymi, Rozwiązywanie bieżących problemów sieciowych, Dbanie o pojemność i niezawodność sieci, Udział w projektach rozbudowy sieci,...	\N	\N	\N	f	f	2025-11-11 12:49:00+01	2025-11-16 22:59:59+01	f	f	1	6	356	\N	\N	18
198	Kierownik projektu IT (e-Invoicing)	Twój zakres obowiązków, Nadzór i koordynacja projektów wdrożeniowych systemów elektronicznej wymiany dokumentów (EDI), Komunikacja z klientem na różnych etapach realizacji projektu w celu zapewnienia zgodności realizowanych prac z przyjętymi...	\N	\N	\N	f	f	2025-11-11 12:29:00+01	2025-11-22 22:59:59+01	f	f	1	360	357	\N	\N	18
199	Analityk biznesowy ds. wdrożeń ERP	Twój zakres obowiązków, Analiza, modelowanie i dokumentowanie procesów biznesowych z wykorzystaniem notacji BPMN, Wsparcie przy wdrożeniu modułów Kadry-Płace oraz Finanse-Księgowość systemu ERP, Tworzenie specyfikacji funkcjonalnych i technicznych...	\N	\N	\N	f	f	2025-11-11 12:02:00+01	2025-11-21 22:59:59+01	f	f	1	361	358	\N	\N	18
200	Analityk Systemowy (MS SQL + T-SQL)	Twój zakres obowiązków, projekt dotyczący automatyzacji wysyłki korespondencji masowej,, prowadzenie analiz biznesowych i systemowych dla banku,, udział w analizie, tworzeniu i rozwijaniu procesów związanych z wydrukami masowymi,, opracowywanie i...	\N	\N	\N	t	\N	2025-11-11 11:53:00+01	2025-11-19 22:59:59+01	t	f	1	38	359	\N	\N	18
201	Programista .NET	Twój zakres obowiązków, uczestnictwo w projekcie dot. branży medycznej,, uczestnictwo we wszystkich fazach tworzenia oprogramowania (opracowanie koncepcji i założeń, eksploracja i badania, projektowanie, implementacja),, implementacja nowych...	90.00	110.00	f	f	f	2025-11-11 11:22:00+01	2025-11-16 22:59:59+01	t	f	1	38	360	1	1	18
202	Analityk systemowy – obszar bankowości	Twój zakres obowiązków, Współpraca z biznesem przy określaniu wymagań, Modelowanie procesów systemowych oraz opisywanie funkcjonalnych procesów biznesowych, Aktualizacja zmian w dokumentacji projektowej, Współpraca i konsultacja rozwiązań...	\N	\N	\N	t	\N	2025-11-11 10:31:00+01	2025-11-26 22:59:59+01	f	f	1	361	361	\N	\N	18
203	Architekt IT	Twój zakres obowiązków, Projektowanie rozwiązań IT w zakresie architektury...	\N	\N	\N	t	\N	2025-11-11 10:12:00+01	2025-11-30 22:59:59+01	f	f	1	295	362	\N	\N	1
204	Kierownik Projektu – Usługi Chmurowe i Cyberbezpieczeństwo	Twój zakres obowiązków, Koordynacja projektów wdrożeniowych (kolokacja, chmura, backup, disaster recovery, SOC, administracja systemami, bezpieczeństwo IT)., Zarządzanie zespołem projektowym (specjaliści IT, administratorzy, inżynierowie...	\N	\N	\N	f	f	2025-11-11 09:56:00+01	2025-12-06 22:59:59+01	f	f	1	366	363	\N	\N	18
205	Młodszy Specjalista / Młodsza Specjalistka ds. IT - Help desk	Twój zakres obowiązków, zapewnienie wsparcia technicznego użytkownikom w zakresie sprzętu, oprogramowania, sieci i systemów informatycznych, rozwiązywanie incydentów oraz realizacja zgłoszeń serwisowych w systemie ticketowym, obsługa...	\N	\N	\N	f	f	2025-11-11 09:14:00+01	2025-11-21 22:59:59+01	f	f	1	367	364	\N	\N	18
206	Specjalista ds. sieci IP	Twój zakres obowiązków, Zarządzanie infrastrukturą sieciową LAN/WAN - w tym - projektowanie, rozbudowa i optymalizacja rozwiązań sieciowych., Monitoring, analiza i optymalizacja wydajności sieci (LAN/WAN) oraz łączy transmisyjnych – także na...	16000.00	18000.00	f	t	\N	2025-11-11 09:03:00+01	2025-12-07 22:59:59+01	f	f	1	343	365	1	9	18
207	Kierownik Produktu ICT	Twój zakres obowiązków, Zarządzanie usługami Chmurowymi., Prowadzenie wdrożeń produktów., Analizy rynku i tworzenie rekomendacji., Wsparcie i współpraca z siłami sprzedaży., Prowadzenie spotkań i negocjacji biznesowych., Prowadzenie konferencji,...	\N	\N	\N	f	f	2025-11-11 08:47:00+01	2025-11-26 22:59:59+01	f	f	1	369	366	\N	\N	18
208	Inżynier IT	Twój zakres obowiązków, Projektowanie i wdrażanie systemów traceability obejmujących wszystkie etapy procesu produkcyjnego – od zakupu surowców po wysyłkę wyrobów., Tworzenie dokumentacji wymagań i współpraca z dostawcami systemów oraz...	\N	\N	\N	f	f	2025-11-11 08:46:48+01	2025-12-04 22:59:59+01	f	f	1	370	367	\N	\N	18
209	Ekspert Data Scientist	Twoje obowiązki, Umiejętność dobierania odpowiedniego algorytmu do problemu oraz posiadanych danych (względem ich wielkości, struktury)., Projektowanie i przeprowadzanie eksperymentów modelarskich, budowa prototypów oraz industrializacja...	\N	\N	\N	t	\N	2025-11-11 08:32:00+01	2025-11-16 22:59:59+01	t	f	1	312	368	\N	\N	8
210	Analityk/czka systemowy/a	Twój zakres obowiązków, identyfikacja potrzeb biznesowych;, pozyskiwanie, opracowanie, walidacja i zarządzanie wymaganiami;, rekomendowanie rozwiązań;, modelowanie procesów biznesowych oraz systemowych;, dokumentowanie funkcjonalności działających...	8000.00	14000.00	f	f	f	2025-11-11 08:32:00+01	2025-11-26 22:59:59+01	t	f	1	372	369	1	9	18
211	Specjalista ds. Bezpieczeństwa	Twój zakres obowiązków, Przygotowywanie analiz, raportów z audytów oraz dokumentacji związanej z bezpieczeństwem informacji;, Prowadzenie projektów dla klientów z obszaru bezpieczeństwa IT, w tym nadzór nad wdrożeniem norm ISO27001 oraz ISO22301;,...	\N	\N	\N	t	\N	2025-11-11 08:23:00+01	2025-11-30 22:59:59+01	t	f	1	227	370	\N	\N	7
212	Analityk Danych IT	Twój zakres obowiązków, Projektowanie, tworzenie i utrzymywanie raportów oraz dashboardów w Power BI na potrzeby operacyjne i strategiczne, Analiza danych z różnych źródeł (bazy danych, systemy IT, logi) w celu identyfikacji trendów, anomalii i...	\N	\N	\N	f	f	2025-11-11 08:15:00+01	2025-11-26 22:59:59+01	t	f	1	374	371	\N	\N	18
213	Analityk kampanii CRM	Twój zakres obowiązków, konfigurowanie i realizowanie kampanii dla wszystkich kanałów kontaktu w dedykowanych systemach kampanijnych,, utrzymywanie portfela istniejących kampanii (opieka nad cyklicznie realizowanymi kampaniami w różnych kanałach...	\N	\N	\N	f	f	2025-11-11 08:03:00+01	2025-12-06 22:59:59+01	f	f	1	375	372	\N	\N	18
214	Młodszy Programista ML / AI	Twój zakres obowiązków, Współpraca przy przygotowaniu i przetwarzaniu danych do projektów ML/AI, Implementacja i testowanie prostych modeli uczenia maszynowego, Tworzenie i rozwój backendu w Pythonie (API, logika aplikacji, integracje z Java lub...	\N	\N	\N	f	f	2025-11-10 15:02:00+01	2025-11-30 22:59:59+01	f	f	1	376	373	\N	\N	18
215	Projektant (analityk systemowy) - systemy wsparcia dowodzenia i kierowania	Twój zakres obowiązków, analiza i dekompozycja wymagań na system i oprogramowanie,, analiza wojskowych i komercyjnych dokumentów normatywnych,, projektowanie systemu oraz specyfikacja wymagań na system i jego komponenty,, prowadzenie dokumentacji...	\N	\N	\N	f	f	2025-11-10 07:13:00+01	2025-11-20 22:59:59+01	f	f	1	377	374	\N	\N	18
216	Inżynier DevOps (Dane / Infrastruktura On-Premise)	Twój zakres obowiązków, Utrzymanie i rozwój infrastruktury DevOps w środowiskach on-premise, Wsparcie zespołów developerskich w procesach CI/CD, Współpraca przy projektach z zakresu Data Hub - integracja, przechowywanie, przetwarzanie danych,...	16000.00	21000.00	\N	t	\N	2025-11-10 15:31:00+01	2025-12-03 22:59:59+01	f	t	1	378	375	1	9	18
217	Młodszy Specjalista Power Platform	Twój zakres obowiązków, Tworzenie i rozwijanie raportów oraz dashboardów w Microsoft Power BI,, Budowa i utrzymanie przepływów automatyzujących procesy w Power Automate,, Tworzenie prostych aplikacji użytkowych w Power Apps,, Współpraca z działami...	7000.00	11000.00	f	t	\N	2025-11-10 14:31:00+01	2025-12-05 22:59:59+01	t	f	1	379	376	1	9	18
218	Inżynier Danych (AI/NLP) (średniozaawansowany/regularny lub starszy)	Twój zakres obowiązków, Pozyskiwanie, analiza i przetwarzanie różnego typu danych z procesów wdrożeniowych i wsparcia klienta., Budowanie baz wiedzy i modelowanie danych., Przygotowanie zbiorów uczących i testowych oraz współpraca z zespołem ML/AI...	\N	\N	\N	t	\N	2025-11-10 14:15:21+01	2025-11-20 22:59:59+01	t	f	1	380	377	\N	\N	18
219	Inżynier sieciowy	Twój zakres obowiązków, JEŚLI SZUKASZ:, •\tFascynujących zadań w obszarze administracji, obejmujących sieci, systemy, środowiska wirtualne i chmurowe, •\tAktywnego udziału w projektach IT, •\tStałej pracy od poniedziałku do piątku z opcją dwóch...	\N	\N	\N	f	f	2025-11-10 14:06:00+01	2025-11-30 22:59:59+01	t	f	1	381	378	\N	\N	18
220	Analityk / Analityczka Danych	Twój zakres obowiązków, analizowanie danych biznesowych z różnych obszarów w celu identyfikacji luk, trendów, wzorców i anomalii, tworzenie i utrzymywanie raportów i pulpitów nawigacyjnych przy użyciu narzędzi BI w celu prezentowania wyników...	\N	\N	\N	f	f	2025-11-10 13:56:00+01	2025-12-05 22:59:59+01	t	f	1	382	379	\N	\N	8
221	Data Engineer	Twój zakres obowiązków, Tworzenie i utrzymanie wydajnych pipeline’ów danych – procesów ETL/ELT do ekstrakcji, przekształcania i ładowania dużych wolumenów danych z różnych źródeł., Projektowanie i implementacja hurtowni danych / data lake – budowa...	100.00	130.00	f	f	f	2025-11-10 13:34:00+01	2025-11-21 22:59:59+01	f	f	1	384	381	1	1	18
222	Starszy Programista Angular | Branża Bankowa	Twój zakres obowiązków, Udzielanie wsparcia technicznego mniej doświadczonych programistów, Tworzenie wysokiej jakości kodu, Przeprowadzanie testów jednostkowych, Współpraca z Analitykami biznesowymi, Architektami IT itd. oraz klientem biznesowym,...	\N	\N	\N	t	\N	2025-11-10 12:21:00+01	2025-11-29 22:59:59+01	t	f	1	75	382	\N	\N	18
223	Kierownik rozwoju systemu CRM	Twój zakres obowiązków, kierowanie zespołem odpowiedzialnym za rozwój i utrzymanie systemu CRM oraz współtworzenie roadmapy rozwoju narzędzi wspierających działania marketingowe, sprzedażowe i operacyjne,, koordynacja wdrożeń nowych...	\N	\N	\N	f	f	2025-11-10 12:07:00+01	2025-11-30 22:59:59+01	t	f	1	386	383	\N	\N	18
224	Analityk/czka Systemowy/a (ekspert)	Twój zakres obowiązków, Udział w przekrojowych projektach realizowanych przy udziale Pionu IT, Projektowanie kompleksowych systemów informatycznych, Weryfikacja potrzeb biznesowych pod kątem możliwości implementacji w systemie, Wypracowywanie...	\N	\N	\N	f	f	2025-11-10 11:48:00+01	2025-12-03 22:59:59+01	f	f	1	387	384	\N	\N	18
225	Młodszy Programista Backend	Twój zakres obowiązków, Tworzenie i rozwijanie aplikacji oraz mikroserwisów - głównieGolang., Implementacja oraz poznawanie nowoczesnych rozwiązań architektonicznych., Nauka analizy wymagań biznesowych i ich przekładania na konkretne rozwiązania...	\N	\N	\N	f	f	2025-11-10 11:47:00+01	2025-11-30 22:59:59+01	t	f	1	388	385	\N	\N	18
226	Programista .NET (backend)	Twój zakres obowiązków, Pracowanie nad kodami generycznej platformy do modelowania i przetwarzania procesów biznesowych;, Programowanie głównych modułów systemu;, Zapewnienie technicznej spójności w obrębie mechanizmów systemu współdzielonych...	\N	\N	\N	f	f	2025-11-10 11:13:00+01	2025-11-20 22:59:59+01	t	f	1	389	386	\N	\N	18
227	Starszy Specjalista ds. Analizy Biznesowej i Systemowej	Twój zakres obowiązków, Zbieranie, analizowanie i dokumentowanie wymagań biznesowych i tłumaczenie ich na wymagania funkcjonalne i niefunkcjonalne., Modelowanie i dokumentowanie procesów biznesowych w notacji BPMN., Współpraca ze zróżncowaną grupą...	\N	\N	\N	f	f	2025-11-10 10:37:00+01	2025-11-21 22:59:59+01	t	f	1	320	387	\N	\N	24
228	Inżynier Testów QA	Twój zakres obowiązków, Zapewnienie, że następujące działania są prowadzone wydajnie i zgodnie z dokumentacją, Analizowanie i projektowanie nowych testów (Test Cases / Scenarios), Planowanie i wykonywanie testów, Raportowanie defektów (w tym ich...	\N	\N	\N	f	f	2025-11-10 10:28:00+01	2025-11-21 22:59:59+01	t	f	1	391	388	\N	\N	18
229	Starszy Programista Python	Twój zakres obowiązków, Implementacja i rozwój potoków wnioskowania modeli głębokiego uczenia (PyTorch / TensorFlow), Przetwarzanie wstępne danych z czujników i obrazu, synchronizacja i fuzja danych z inferencji, Integracja systemu z architekturą...	\N	\N	\N	f	f	2025-11-10 09:21:00+01	2025-11-21 22:59:59+01	t	f	1	392	389	\N	\N	18
230	Starszy Inżynier DevOps	Twój zakres obowiązków, Projektowanie, rozwój i utrzymanie środowiska CI/CD z wykorzystaniem narzędzi takich jak GitLab, Jenkins, GitHub Actions, bądź pokrewne, Automatyzacja procesów budowania, testowania i wdrażania modułów w językach Python i...	\N	\N	\N	f	f	2025-11-10 09:21:00+01	2025-11-21 22:59:59+01	t	f	1	392	390	\N	\N	18
231	Ekspert ds. Systemów Transakcyjnych (k/m)	Twój zakres obowiązków, Analiza zgłoszeń oraz wykonywanie testów systemowych, Parametryzacja i konfiguracja wybranych elementów biznesowych i technicznych systemów transakcyjnych, Dbałość o spójność wprowadzanych zmian w systemach transakcyjnych...	\N	\N	\N	f	f	2025-11-10 08:33:00+01	2025-11-30 22:59:59+01	t	f	1	6	391	\N	\N	18
232	Java Starter Kit	Twój zakres obowiązków, przez 3 miesiące będziesz mieć możliwość nauczyć się, jak kompleksowo tworzyć nowoczesne systemy informatyczne – od projektowania i optymalizacji baz danych, przez implementację rozwiązań serwerowych bazujących na...	\N	\N	\N	f	f	2025-11-10 08:06:00+01	2025-12-05 22:59:59+01	f	f	1	395	392	\N	\N	18
233	Programista / Programistka Hurtowni Danych	Twój zakres obowiązków, Rozwijanie Hurtowni Danych w zakresie: przekładania wymagań biznesowych na projekt techniczny, projektowania i tworzenia procesów integracji danych (pakiety ETL), tworzenia kodów źródłowych dla procesów zasilających...	\N	\N	\N	f	f	2025-11-10 07:32:00+01	2025-11-15 22:59:59+01	f	f	1	396	393	\N	\N	18
234	Architekt Cyberbezpieczeństwa	Twój zakres obowiązków, wykonywanie analiz wymagań funkcjonalnych i niefunkcjonalnych tworzonego rozwiązania (aplikacji, baz danych i mechanizmów integracyjnych i komunikacyjnych),, wsparcie naszych klientów w utrzymaniu systemów bezpieczeństwa,...	16000.00	21000.00	f	f	f	2025-11-10 07:28:00+01	2025-11-30 22:59:59+01	f	f	1	372	394	1	9	7
235	Magazynier - Kierowca, dział WYROBY HUTNICZE	Zatrudnimy osobę na stanowisko: Magazynier -kierowca dział WYROBY HUTNICZE. Kompletacja zamówień dla naszych klientów, przyjmowanie dostaw, kontrola dostaw pod kątem ilościowym i jakościowym, przeprowadzanie okresowych inwentaryzacji. Kierowanie pojazdem kategorii C lub C+E. Dostarczanie towaru do punktów sprzedaży. Realizacja tras lokalnych do 200 km od siedzib firmy. Troska o powierzony sprzęt. Przestrzeganie przepisów dotyczących czasu pracy. Wykonywanie zadań transportowych. Nadzór nad ilością i jakością towaru.	7200.00	7200.00	f	\N	\N	2025-06-17 07:32:21+02	2025-11-21 07:11:10+01	f	t	398	398	395	1	9	26
236	Magazynier - Magazyn Wysyłkowy Mebli Tapicerowanych	Opis stanowiska pracy: Załadunek mebli tapicerowanych, Kontrola zgodności załadunków i zabezpieczenia towaru, Organizacja i utrzymanie porządku w przestrzeni magazynowej, Układanie i składowanie mebli zgodnie z przyjętymi standardami, Współpraca z działem logistyki w zakresie przygotowania wysyłek i dostaw. Wymagania: dobra organizacja pracy, dokładność, komunikatywność i umiejętność pracy w zespole, odpowiedzialność, sumienność, zaangażowanie, mile widziane doświadczenie w pracy. Zapewniamy: stabilne zatrudnienie w oparciu o umowę o pracę, Przyjazną atmosferę pracy, Możliwość zakwaterowania, Bardzo dobre warunki pracy i płacy.	\N	\N	\N	f	f	2025-03-17 08:22:32+01	2025-12-12 10:41:15+01	f	t	398	399	396	\N	\N	26
237	Magazynier w sklepie internetowym - dla studentów zaocznych	Sklep internetowy i-want.pl poszukuje magazyniera do kompletacji towaru. Adres magazynu to Luboń, ul. Magazynowa 10. Poszukujemy młodych osób do 26 roku życia do naszego zespołu. Zakres obowiązków: kompletacja towaru na magazynie, przygotowanie zamówień hurtowych, przyjmowanie dostaw, prowadzenie prac porządkowych na magazynie. Wymagamy: status studenta/ucznia, pracowitość, rzetelność, uczciwość, punktualność. Oferujemy: wynagrodzenie 30,5zł na rękę, premia od obrotu firmy (400-600zł/mc), premia za najlepsze wyniki w kompletacji towaru, pracę w młodym zespole osób i dobrą atmosferę. Zainteresowane osoby prosimy wysłanie CV za pomocą OLX. Zapraszamy!	30.50	600.00	f	f	f	2025-11-13 17:03:36+01	2025-12-13 17:05:22+01	f	t	398	400	397	1	9	26
240	Pakowanie wyrobów czekoladowych - Praca sezonowa	Firma Karmello Chocolatier, producent wysokiej jakości wyrobów czekoladowych z siedzibą w Bielsku-Białej, poszukuje kandydatów na stanowisko: PRACOWNIK DZIAŁU PAKOWANIA (praca sezonowa). Zakres obowiązków: konfekcjonowanie wyrobów czekoladowych, etykietowanie produktów, dbanie o estetykę miejsca pracy.	\N	\N	\N	f	f	2025-10-31 12:20:45+01	2025-11-30 12:20:46+01	f	t	398	403	400	\N	1	26
335	Magazynier	Praca w trybie zmianowym (dwie zmiany 6:00–14:00 / 14:00–22:00). Przyjmowanie i wydawanie towaru / przyjmowanie i realizacja zamówień, załadunek i rozładunek dostaw, kontrola dostaw pod względem ilościowym i jakościowym, układanie i przewożenie towarów na terenie magazynu / nadawanie i umiejscowienie lokalizacji towarów na magazynie, pakowanie paczek/ komisjonowanie.	\N	\N	\N	f	f	2025-09-30 09:29:32+02	2025-12-13 14:56:05+01	f	t	398	534	531	\N	\N	26
346	Magazynier - ul. Inwestycyjna - 3 zmiany	Nasz klient to nowoczesne przedsiębiorstwo z branży stalowej zlokalizowane w Sosnowcu. Specjalizuje się w magazynowaniu, cięciu oraz dystrybucji wyrobów hutniczych, takich jak blachy, rury i profile stalowe. Magazynier - ul. Inwestycyjna - 3 zmiany Opis stanowiska: • Załadunek, rozładunek, składowanie i transport wyrobów stalowych. • Obsługa suwnicy do transportu wyrobów stalowych. • Praca z dokumentacją (etykiety, listy przewozowe) i ewidencją ruchów magazynowych. • Utrzymanie porządku w miejscu pracy i dbałość o sprzęt magazynowy. • Współpraca z zespołem i kierownikiem magazynu, udział w bieżących procesach logistycznych. Wymagania: • Uprawnienia UDT na suwnice - mile widziane • Doświadczenie w pracy na magazynie, mile widziane w środowisku stalowym lub produkcyjnym • Gotowość do pracy fizycznej w trybie zmianowym. • Umiejętność obsługi komputera oraz łatwość w uczeniu się nowych systemów. • Praca zespołowa, sumienność, zaangażowanie i dyspozycyjność Oferujemy: • Zatrudnienie na Umowę o Pracę Tymczasową z możliwością przejścia bezpośrednio pod firmę • Atrakcyjne wynagrodzenie adekwatne od doświadczenia • Pracę od poniedziałku do piątku w systemie 3 zmianowym. • Pracę w nowoczesnym magazynie z dobrą organizacją i zasadami BHP. • Możliwość rozwoju zawodowego i udziału w szkoleniach wewnętrznych. &nbsp; Oferta dotyczy pracy tymczasowej. APT 364 'Informujemy, iż w spółkach z Grupy Adecco działających w Polsce wdrożyliśmy procedurę dokonywania zgłoszeń wymaganą Ustawą o Ochronie Sygnalistów. Szczegółowe informacje są dostępne w biurach Adecco.' Adecco Poland Sp. z o.o. należy do międzynarodowej korporacji The Adecco Group - światowego lidera wśród firm doradztwa personalnego, który posiada 5100 placówek w ponad 60 krajach. W Polsce działamy od 1994 roku. Swoją wiedzą i doświadczeniem służymy w ponad 50 lokalizacjach na terenie kraju. W 2020 roku pracę dzięki Adecco Poland znalazło blisko 60000 osób.	\N	\N	\N	f	f	2025-10-24 14:45:53+02	2025-11-23 13:45:54+01	f	t	398	423	532	\N	\N	26
243	Pracownik sortowni - praca dodatkowa w sortowni paczek	Sortowanie przesyłek ręcznie przy użyciu stołów sortowniczych, praca ze skanerem ręcznym, drobne czynności porządkowe.	30.50	\N	f	f	f	2025-10-31 15:58:08+01	2025-11-30 15:58:09+01	f	t	398	406	403	1	1	26
248	Magazynier na porannej zmianie	Jesień to czas zmian – zacznij nową pracę i zdobądź dodatkowy zastrzyk gotówki! Październik to idealny moment, aby zadbać o stabilny dochód przed zimą. Dołącz do naszego zespołu jako magazynier na porannej zmianie i wykorzystaj jesienną energię na zarobki, które pozwolą Ci spokojnie planować kolejne miesiące.	30.50	31.50	f	f	f	2025-10-02 13:05:45+02	2025-12-12 14:18:04+01	f	t	398	411	408	1	1	26
249	Sezonowa praca dla studenta - pakowanie zamówień internetowych	W MyGift wierzymy, że praca może być przyjemna, rozwijająca i pełna dobrej energii. Tworzymy wyjątkowe prezenty, które wywołują uśmiech na twarzach naszych klientów i ich bliskich — a wszystko to dzięki zgranemu zespołowi, który wspiera się nawzajem i działa z pasją. Teraz szukamy kogoś, kto pomoże nam w tym magicznym procesie! Jeśli jesteś osobą dokładną, sumienną i lubisz, gdy wszystko jest dopięte na ostatni guzik — pasujesz do nas idealnie! Szukamy wsparcia przy realizacji zamówień w naszym sklepie internetowym. Praca sezonowa w terminie od listopada do 22 grudnia 2025 r. Twoje zadania: Kompletowanie zamówień — zbieranie produktów i sprawdzanie ich zgodności w systemie. Kontrola jakości — sprawdzanie, czy produkty są w odpowiednim do wysyłki stanie, a paczka wygląda schludnie i zgodnie ze standardem firmy Estetyczne przygotowanie paczek — układanie produktów w opakowaniach prezentowych, dodawanie ozdobnych elementów, tak aby paczka wyglądała schludnie — tworzymy i wysyłamy prezenty tak, jak sami chcielibyśmy je otrzymać lub podarować bliskim Pakowanie zamówień — czyli umieszczanie produktów w odpowiednich opakowaniach, tak aby były dobrze zabezpieczone na czas transportu (folia bąbelkowa, wypełniacze, pudełka) Etykietowanie przesyłek — naklejanie etykiet adresowych i informacyjnych zgodnie z systemem wysyłkowym Utrzymanie porządku na stanowisku pracy — regularne sprzątanie, odkładanie materiałów na miejsce, uzupełnianie zapasów (np. taśmy, pudełka, wypełniacze) Czego oczekujemy: Dyspozycyjności od poniedziałku do piątku w godzinach 9:30 - 19:30 Podstawowej znajomości obsługi komputera Zaangażowania oraz dbania o jakość Nie musisz mieć doświadczenia, wszystkiego Cię nauczymy Fajnie jeśli jesteś studentem, uczniem lub słuchaczem - ale nie jest to warunek konieczny Co oferujemy: Pracę sezonową w terminie od listopada do 22 grudnia 2025 Wynagrodzenie w wysokości 30,50 zł/h brutto Zero dress code’u - ubierasz się tak, jak Ci wygodnie Wsparcie i wdrożenie od pierwszego dnia Zgrany zespół, na który możesz zawsze liczyć Zniżki na nasze produkty Aby wziąć udział w procesie rekrutacji prześlij nam proszę następujące informacje: Swoje aktualne CV; Informację o godzinowej dostępności w tygodniu;	30.50	30.50	f	f	f	2025-11-12 09:29:14+01	2025-12-12 09:29:26+01	f	t	398	412	409	1	1	26
250	Magazynier	Tomadex S.C. – wiodący europejski producent gadżetów dla kibiców, skarpet sportowych i odzieży, z dynamicznie rozwijającym się zespołem handlowym. Poszukiwany zorganizowany, sumienny i doświadczony pracownik na magazynie. Zakres obowiązków: Przyjmowanie, wydawanie i weryfikacja towarów, Kompletowanie zamówień, Rozładunek i załadunek dostaw, Dbałość o porządek i czystość w magazynie, Obsługa wózka widłowego, Praca z systemem magazynowym.	\N	\N	\N	f	f	2025-09-12 12:03:47+02	2025-12-12 15:18:32+01	f	t	398	413	410	\N	\N	26
251	Pracownik działu obsługi i sprzedaży odzieży sportowej	Dystrybutor odzieży sportowej poszukuje pracownika do działu obsługi, sprzedaży internetowej, znakowania odzieży, obsługi klubów sportowych. Mile widziana znajomość obsługi plotera tnąco-drukującego.	4000.00	6000.00	f	f	f	2025-11-07 14:59:26+01	2025-12-07 15:10:50+01	f	t	398	414	411	1	9	26
252	Pracownik fizyczny w magazynie/sortowni odzieży Ostrów Wielkopolski	Jesteśmy firmą świadczącą usługi w zakresie outsourcingu i leasingu pracowniczego, rekrutacji, doradztwa personalnego oraz pracy tymczasowej. Obecnie dla jednego z Naszych Klientów poszukujemy osób zainteresowanych pracą na stanowisku: Pracownik fizyczny w magazynie/sortowni odzieży Ostrów Wielkopolski Opis stanowiska: załadunek i rozładunek dostaw dostarczanie i odbieranie odzieży z hali sortowniczej proste prace fizyczne Wymagania: dyspozycyjność sumienność zaangażowanie Oferujemy: pracę od poniedziałku do piątku w systemie 2-zmianowym (6.00-14.00, 14.00-22.00) możliwość stałego, stabilnego zatrudnienia atrakcyjne wynagrodzenie Osoby zainteresowane prosimy o przesyłanie aplikacji klikając w przycisk aplikowania. Klikając w przycisk "Aplikuj" lub "Wyślij" wyraża Pani/Pan zgodę na przetwarzanie przez OUTWORKING SA danych osobowych zawartych w Pani/Pana zgłoszeniu rekrutacyjnym do realizacji procesu rekrutacji na stanowisko, na które Pani/Pan niniejszym aplikuje. Agencja zatrudnienia, nr certyfikatu 8102	\N	\N	\N	f	f	2025-10-24 11:44:44+02	2025-11-23 10:44:45+01	f	t	398	415	412	\N	\N	26
247	Magazynier marki Crocs	Proste prace magazynowe związane z przyjmowaniem dostaw oraz zwrotów ze sklepu internetowego.	\N	\N	\N	f	f	2025-11-07 15:30:46+01	2025-12-07 15:35:24+01	f	t	398	410	445	\N	\N	26
245	Kierowca/Magazynier - filia Inter Cars	Czy wiedziałeś, że Inter Cars S.A. jest liderem w dystrybucji części zamiennych oraz materiałów eksploatacyjnych w Europie Środkowo Wschodniej?\n&nbsp;\nChcesz dowiedzieć się więcej na nasz temat? Wejdź na&nbsp;www.intercars.eu. Do Filii Inter Cars w Lublinie, poszukujemy kandydatów na stanowisko:\n\nKierowca/Magazynier - filia Inter Cars\n\nOpis stanowiska:\n• Dostawa towaru do klientów\n• Prawidłowe rozliczanie i obieg dokumentacji\n• Przyjmowanie dostaw oraz rozlokowanie towarów w magazynie\n• Kompletowanie towarów według zamówienia oraz przygotowywanie wysyłek\n\nOd kandydatów oczekujemy:\n• Podstawowej umiejętności obsługi komputera\n• Umiejętności pracy w zespole\n• Punktualności, skrupulatności, umiejętności organizacji pracy\n• Czynnego prawa jazdy kat. B\n\nWybranej osobie oferujemy:\n• Stabilne warunki zatrudnienia w ramach umowy o pracę\n• Możliwości rozwoju w polskiej Firmie o europejskim zasięgu, w strukturach lidera branży\n• Możliwość zdobywania doświadczenia zawodowego\n&nbsp;\n&nbsp;\n&nbsp;\nObowiązek informacyjny | Informacja dot. zgłaszania nadużyć na podstawie ustawy o sygnalistach. Administratorem danych osobowych jest Inter Cars S.A., ul. Powsińska 64, 02-903 Warszawa, tel. 801 80 20 20, sekretariat[]intercars.eu. Administrator wyznaczył Inspektora Ochrony Danych, z którym&nbsp;\nmożna kontaktować się pod adresem iod[]intercars.eu&nbsp;\nWięcej...&nbsp;\nPodanie danych jest dobrowolne i jest warunkiem wzięcia udziału w procesie rekrutacyjnym, a ich niepodanie uniemożliwi wzięcie w nim udziału.&nbsp;\nDane będą przetwarzane w celu:&nbsp;\n- przeprowadzenia procesu rekrutacyjnego na stanowisko wskazane w ogłoszeniu (podstawa prawna: przetwarzanie niezbędne do podjęcia działań na żądanie osoby, której dane dotyczą, przed zawarciem umowy, niezbędność do wypełnienia obowiązków prawnych ciążących na administratorze, a w przypadku podania danych osobowych wykraczających poza niezbędne do celów prowadzenia rekrutacji - Pani/Pana dobrowolna zgoda),&nbsp;\n- prowadzenia przyszłych rekrutacji - w przypadku udzielenia odrębnej zgody (podstawa prawna: Pani/Pana dobrowolna zgoda),&nbsp;\n- realizacji wewnętrznych celów administracyjnych Grupy Kapitałowej Inter Cars, ustalenia, dochodzenia lub obrony przed roszczeniami (podstawa prawna: uzasadniony interes administratora lub strony trzeciej).&nbsp;\nOdbiorcami Pani/Pana danych osobowych mogą być podmioty świadczące na rzecz administratora usługi niezbędne do realizacji celów przetwarzania, w tym np. dostawcy systemów informatycznych i usług IT, podmioty świadczące usługi prawne, doradcze, audytowe, bezpieczeństwa, przeciwdziałania nadużyciom, wspierające administratora w prowadzeniu rekrutacji, banki, firmy świadczące usługi kurierskie i przewozowe, Poczta Polska, podmioty z Grupy Kapitałowej Inter Cars, a ponadto organy państwa i inne podmioty uprawnione do przetwarzania danych na podstawie przepisów prawa powszechnie obowiązującego.&nbsp;\nPani/Pana dane osobowe będą co do zasady przetwarzane na terenie Europejskiego Obszaru Gospodarczego (EOG). Administrator może jednak wyjątkowo przekazywać dane partnerom przetwarzającym je poza EOG, ale tylko w zakresie niezbędnym, związanym z świadczeniem usług na rzecz administratora przez tych partnerów. Administrator zapewnia wówczas odpowiednie zabezpieczenia danych, w szczególności przez transfer do krajów, co do których Komisja Europejska (KE) wydała decyzję stwierdzającą adekwatny stopień ochrony lub stosowanie standardowych klauzul ochrony danych przyjętych na mocy decyzji KE. Może Pani/Pan uzyskać kopię stosowanych zabezpieczeń kontaktując się z administratorem po adresem: iod()intercars.eu.&nbsp;\nDane będą przechowywane do czasu zakończenia procesu rekrutacyjnego, przez okres niezbędny do realizacji pozostałych, wyżej wskazanych celów, do czasu wycofania udzielonej zgody - w przypadku przetwarza danych na tej podstawie lub wniesienia sprzeciwu - w przypadku przetwarzania danych na podstawie uzasadnionego interesu administratora. W przypadku wyrażenia zgody na Pani/Pana udział w przyszłych rekrutacjach dane osobowe będą przechowywane przez okres nie dłuższy niż 2 lata.&nbsp;\nKażdej osobie przysługuje prawo do żądania dostępu do swoich danych osobowych, w tym uzyskania ich kopii, sprostowania, usunięcia, ograniczenia przetwarzania oraz przenoszenia danych, prawo do wniesienia sprzeciwu wobec przetwarzania danych, wniesienia skargi do organu nadzorczego oraz cofnięcia zgody w dowolnym momencie bez wpływu na zgodność z prawem przetwarzania, którego dokonano na podstawie zgody przed jej cofnięciem - na zasadach i w przypadkach przewidzianych przez przepisy prawa.&nbsp;\nPani/Pana dane osobowe nie podlegają zautomatyzowanemu podejmowaniu decyzji, w tym profilowaniu. Informacja dot. zgłaszania nadużyć na podstawie ustawy o sygnalistach. &nbsp;\nOsoba aplikująca na dane stanowisko ma możliwość zapoznania się z Procedurą zgłoszeń wewnętrznych Grupy Kapitałowej Inter Cars pod linkiem. Aplikując na stanowisko potwierdzam, że zostałem/zostałam poinformowany/a o takiej możliwości.&nbsp;	\N	\N	\N	f	f	2025-10-31 12:45:21+01	2025-11-30 12:45:21+01	f	t	398	408	535	\N	\N	26
376	Technik jakości (laboratorium)	<p>Prowadzimy rekrutację do firmy produkcyjnej z branży samochodowej.</p>	\N	\N	\N	f	f	2025-11-03 00:00:00+01	2025-12-03 00:00:00+01	f	f	590	590	594	\N	\N	19
377	Team Leader (K/M)	Zakład produkcyjny Dijo to prężnie rozwijająca się dolnośląska firma branży spożywczej o międzynarodowym zasięgu, lider w produkcji tortilli w Europie Centralnej i Wschodniej oraz laureat nagrody Forbes.	\N	\N	\N	f	f	2025-11-05 00:00:00+01	2025-12-05 00:00:00+01	f	f	590	591	595	\N	\N	328
401	Asystent	\N	7500.00	\N	f	f	f	2025-11-13 00:00:00+01	2025-12-14 00:00:00+01	f	f	590	642	596	1	9	4
378	Clinical Account Manager	MichalePage feed import	\N	\N	\N	f	f	2025-10-29 00:00:00+01	2025-11-27 00:00:00+01	f	f	590	593	597	\N	\N	328
402	Technolog/specjalista w laboratorium w zespole biologii gamet	Wymagane doświadczenie w pracy laboratoryjnej, praktyczna znajomość technik laboratoryjnych. Umiejętność planowania i organizacji pracy laboratoryjnej, rzetelność i odpowiedzialność w realizacji powierzonych zadań, umiejętność pracy w zespole badawczym, doświadczenie w izolacji mikropęcherzyków i znajomość podstaw cytometrii przepływowej, znajomość zasad dobrej praktyki laboratoryjnej.	5300.00	5600.00	f	f	f	2025-10-27 00:00:00+01	2025-11-20 00:00:00+01	f	f	590	644	598	1	9	20
403	Elektryk	\N	4666.00	\N	f	f	f	2025-10-27 00:00:00+01	2025-11-30 00:00:00+01	f	f	590	645	599	1	9	13
379	Pracownik ds. pisarstwa medycznego	Zatrudnienie w projekcie Utworzenie innowacyjnego Centrum Wsparcia Badań Klinicznych w NIO-PIB Kraków (inno-CWBK NIO-PIB KRK) w ramach Działania 4.2. Rozwój sieci Centrów Wsparcia Badań Klinicznych z Rządowego Planu Rozwoju Sektora Biomedycznego na lata 2022-2031, finansowanego ze środków Krajowego Planu Odbudowy i Zwiększania Odporności.	1875.00	\N	f	f	f	2025-10-24 00:00:00+02	2025-11-19 00:00:00+01	f	f	590	646	600	1	9	20
404	Laborant kosmetyczny	\N	5000.00	6800.00	f	f	f	2025-09-17 00:00:00+02	2025-12-31 00:00:00+01	f	f	590	647	601	1	9	21
380	Manager Sprzedaży	BIOCEUM P.S.A. działa w obszarze nowej "zielonej" technologii. Oferujemy naturalne innowacyjne rozwiązania uzyskane w wyniku badań B+R w zakresie biotechnologii do zastosowań w hodowli i rolnictwie jak również wspierania zdrowia ludzi. Rozwijamy swoją działalność i dlatego poszukujemy zaangażowanej i aktywnej osoby. Samodzielne stanowisko; Manger Sprzedaży. Twój zakres aktywności; aktywne pozyskiwanie nowych klientów, ustalanie szczegółów warunków współpracy i nadzorowanie prawidłowego jej przebiegu; budowanie długofalowych relacji biznesowych z Klientami, aktywne budowanie portfolio produktów do sprzedaży; dbałość o realizację założonych planów sprzedażowych; monitorowanie działań.	\N	\N	\N	f	f	2024-04-23 00:00:00+02	2025-11-21 00:00:00+01	f	f	590	648	602	\N	\N	328
381	Manager Sprzedaży	BIOCEUM P.S.A. działa w obszarze nowej "zielonej" technologii. Oferujemy naturalne innowacyjne rozwiązania uzyskane w wyniku badań B+R w zakresie biotechnologii do zastosowań w hodowli i rolnictwie jak również wspierania zdrowia ludzi. Rozwijamy swoją działalność i dlatego poszukujemy zaangażowanej i aktywnej osoby. Samodzielne stanowisko; Manger Sprzedaży. Twój zakres aktywności; aktywne pozyskiwanie nowych klientów, ustalanie szczegółów warunków współpracy i nadzorowanie prawidłowego jej przebiegu; budowanie długofalowych relacji biznesowych z Klientami, aktywne budowanie portfolio produktów do sprzedaży; dbałość o realizację założonych planów sprzedażowych; monitorowanie działań.	\N	\N	\N	f	f	2024-04-23 00:00:00+02	2025-11-19 00:00:00+01	f	f	590	648	603	\N	\N	328
432	Praktyki studenckie Python / Django / Flask / Javascript	Wymagania: Podstawowa znajomość języka Python lub Javascript. Chęć doskonalenia umiejętności. Podstawowa znajomość SQL / HTTP. Znajomość systemów kontroli wersji GIT. Umiejętność tworzenie struktur danych takich jak np. drzewo binarne. Oferujemy: Code Review, Sprawdzoną ścieżkę kariery w IT, Projekty open source, Praca w dynamicznie rozwijającym się zespole, Ciekawe projekty, Kawa, Napoje, Dartsy, Biuro dobrze skomunikowane SKM / ZKM w centrum Gdyni. Informacje o firmie: We are The Software House where business questions meet software answers. Kontakt:@limebrains.com	\N	\N	\N	f	f	2022-02-11 00:00:00+01	2025-11-19 00:00:00+01	f	f	590	693	650	\N	\N	18
255	Prosta praca w magazynie bez pracy w nocy - 2 zmiany | Premia za wydajność!	Szukasz prostej pracy na terenie Legnicy? Zatrudnimy osoby do pracy magazynowej przy prostych zadaniach: kontrola jakości i pakowanie butów. Praca od poniedziałku do piątku, bez zmian nocnych i bez weekendów. Świetna oferta także dla Pań i studentów.	30.50	\N	f	f	f	2025-10-02 09:40:22+02	2025-12-07 09:24:11+01	f	t	398	418	415	1	1	26
256	Pracownik serwisu sitodruku oraz magazynu produkcyjnego	K+L Biuro Handlowe Polska Spółka z o.o. z siedzibą w Łodzi to firma o międzynarodowym zasięgu z historią i wieloletnim doświadczeniem w branży. Oferujemy kompleksowe wsparcie dla drukarni sitodrukowych i tampondrukowych, dostarczając maszyny i urządzenia drukarskie, materiały eksploatacyjne, części zamienne, a także prowadząc szkolenia i doradztwo. K+L to nie tylko dystrybutor, lecz przede wszystkim partner dla drukarni, oferujący wsparcie techniczne i technologiczne oraz pomagający w rozwiązywaniu problemów. Firma stawia na innowacyjność, dlatego poszerza swoją ofertę o cyfrowe, nowoczesne technologie, takie jak DTF, DTF-UV. Misją firmy K+L jest dostarczanie klientom najwyższej jakości produktów i usług, wspieranie ich rozwoju w dynamicznie zmieniającej się branży poligraficznej, oraz stawianie na innowacyjność i partnerstwo. Poszukujemy na stanowisko: Pracownik serwisu sitodruku oraz magazynu produkcyjnego Zakres obowiązków: - wykonywanie i regeneracja szablonów sitodrukowych; - ogólne prace magazynowe. Oferujemy: - stabilną pracę w systemie jednozmianowym od pn. do pt. w godzinach 8-16; - umowę o pracę z pominięciem agencji pracy; - możliwość podjęcia pracy od zaraz; - stabilne warunki zatrudnienia; - atrakcyjne wynagrodzenie; - doświadczenie nie jest konieczne, szkolenia w firmie; - opieka medyczna; - grupowe ubezpieczenie zdrowotne. Uprzejmie informujemy, że skontaktujemy się tylko z wybranymi osobami.	\N	\N	\N	f	f	2025-10-31 12:50:31+01	2025-11-30 12:50:32+01	f	t	398	419	416	\N	\N	26
262	Magazynier / MANGO / Pełny etat / Westfield Mokotów	Aktualnie rekrutujemy na stanowisko: Magazynier / cały etat. W lokalizacji: MANGO Westfield Mokotów. Koordynacja towaru w strefie magazynu. Utrzymywanie nienagannego porządku względem zasad MANGO. Realizacja zamówień i odsyłki towaru. Przeprowadzanie cotygodniowych inwentaryzacji. Udział we wszystkich dostawach i koordynacja zadań. Powitanie Klientów i pomoc w zakupach. Dobra orientacja w naszej ofercie, umożliwiająca sprawne doradztwo. Utrzymanie atrakcyjnego wizerunku sklepu. Szukamy osób, które są odważne, otwarte i komunikatywne! :)	\N	\N	\N	f	f	2025-02-28 00:11:58+01	2025-12-12 20:49:23+01	f	t	398	425	422	\N	\N	26
263	Magazynier BHP	SUPON S.A. do Hurtowni BHP w Okunicy k/Pyrzyc zatrudni osobę na stanowisko: MAGAZYNIER Wymagania: - doświadczenie zawodowe na pokrewnym stanowisku, - wykształcenie minimum średnie, -znajomość obsługi komputera, - niekaralność. Oferujemy: - stałe zatrudnienie na umowę o pracę, - szkolenia i możliwość rozwoju zawodowego, - świadczenia socjalne i ubezpieczenie grupowe, - praca w przyjaznym środowisku od poniedziałku do piątku w godz. 7,00 - 15,00.	\N	\N	\N	f	f	2025-10-29 10:46:02+01	2025-11-28 10:46:33+01	f	t	398	426	423	\N	\N	26
254	Magazynier - Stalowa Wola	Agata S.A. oferuje szeroki wybór mebli i artykułów wyposażenia wnętrz. Do głównych obowiązków na tym stanowisku będzie należało: Rozładunek dostaw i przyjęcie towaru; Przygotowanie oraz wydanie towarów; Dbałość o powierzony towar i mienie firmy. Od kandydatów oczekujemy: Elastyczności czasu pracy; Gotowości do ciężkiej pracy fizycznej; Doświadczenia w pracy na magazynie; Umiejętności pracy w zespole.	\N	\N	\N	f	f	2025-11-07 12:56:19+01	2025-12-07 12:56:19+01	f	t	398	417	450	\N	\N	26
259	Pracownik Magazynu	Sortowanie i układanie przesyłek na taśmie oraz na wózkach, skanowanie i przygotowywanie paczek do wysyłki, pomoc przy załadunku i rozładunku samochodów dostawczych, dbanie o porządek na stanowisku pracy.	35.50	\N	f	f	f	2025-10-24 16:11:42+02	2025-11-23 15:11:43+01	f	t	398	422	489	1	1	26
260	Pracownik Magazynowy Amazon Sady	Pracownik Magazynowy Amazon Sady. Ostatnia szansa na zatrudnienie w tym roku! Atrakcyjna podstawa wynagrodzenia, comiesięczna premia, posiłki i dojazd do pracy, a także liczne benefity. Pracuj wedle zagranicznych standardów! Zakres obowiązków, to proste prace magazynowe, m.in. kompletowanie zamówień, pobieranie towarów, wykładanie produktów na półki, pakowanie.	\N	36.22	f	f	f	2025-11-07 15:45:38+01	2025-12-07 15:45:38+01	f	t	398	423	493	1	1	26
257	Prace magazynowe, bez doświadczenia, elastyczne godz., DHL Legnica	Osoba do prac magazynowych. Oferujemy: Współpracę na podstawie umowy zlecenia, stawka za godziny nocne: 37,70 PLN brutto/h. Elastyczne godziny pracy - praca w godzinach porannych i popołudniowych, możliwa praca w soboty. Możliwość rozwoju poprzez udział w rekrutacjach wewnętrznych. Twoje zadania: Układanie przesyłek (taśmociąg - transport liniowy), Procesowanie przesyłek w wyznaczonej strefie, Dbanie i zapobieganie uszkodzeniu przesyłek w trakcie ich procesowania. Nasze oczekiwania: Umiejętność planowania i organizacji pracy, Umiejętność współpracy w zespole.	\N	37.70	f	f	f	2025-10-03 17:28:18+02	2025-12-02 16:28:19+01	f	t	398	420	512	1	1	26
390	Specjalista ds. IT [3/4 etatu]	\N	3499.50	4500.00	f	f	f	2025-11-07 00:00:00+01	2025-11-17 00:00:00+01	f	f	590	616	620	1	9	18
391	Technik/czka	\N	5005.00	\N	f	f	f	2025-11-06 00:00:00+01	2025-11-21 00:00:00+01	f	f	590	617	621	1	9	18
392	Informatyk	\N	7300.00	\N	f	f	f	2025-11-06 00:00:00+01	2025-11-24 00:00:00+01	f	f	590	618	622	1	9	18
413	Podinspektor ds. komputeryzacji	\N	6500.00	\N	f	f	f	2025-11-06 00:00:00+01	2025-11-18 00:00:00+01	f	f	590	669	623	1	9	18
393	Technik informatyk	\N	30.50	\N	f	f	f	2025-11-06 00:00:00+01	2025-12-07 00:00:00+01	f	f	590	620	624	1	1	18
394	Informatyk r11/2689-1322/25	\N	6000.00	\N	f	f	f	2025-11-05 00:00:00+01	2025-12-04 00:00:00+01	f	f	590	621	625	1	9	18
414	Referent informatyk	\N	4700.00	\N	f	f	f	2025-11-05 00:00:00+01	2025-12-01 00:00:00+01	f	f	590	672	626	1	9	18
415	Technik informatyk	\N	2755.10	2755.10	f	f	f	2025-11-05 00:00:00+01	2025-11-19 00:00:00+01	f	f	590	660	627	1	9	18
416	Starszy informatyk	\N	5234.00	\N	f	f	f	2025-11-04 00:00:00+01	2025-11-16 00:00:00+01	f	f	590	674	628	1	9	18
395	Informatyk	\N	6800.00	\N	f	f	f	2025-11-04 00:00:00+01	2025-11-16 00:00:00+01	f	f	590	625	629	1	9	18
396	Pomoc techniczna	\N	1550.00	\N	f	f	f	2025-11-04 00:00:00+01	2025-12-04 00:00:00+01	f	f	590	626	630	1	9	18
397	Informatyk	\N	6400.00	\N	f	f	f	2025-11-04 00:00:00+01	2025-11-16 00:00:00+01	f	f	590	625	631	1	9	18
417	Informatyk-informatyczka	\N	5060.00	\N	f	f	f	2025-11-04 00:00:00+01	2025-11-20 00:00:00+01	f	f	590	660	632	1	9	18
418	Informatyk	\N	6880.00	\N	f	f	f	2025-11-04 00:00:00+01	2025-11-30 00:00:00+01	f	f	590	660	633	1	9	18
398	Asystent w laboratorium kryminalistycznym	\N	6005.00	\N	f	f	f	2025-11-04 00:00:00+01	2025-12-05 00:00:00+01	f	f	590	630	634	1	9	20
419	Młodszy specjalista ds. informatyki	\N	4666.00	\N	f	f	f	2025-10-30 00:00:00+01	2025-12-01 00:00:00+01	f	f	590	645	635	1	9	18
399	Specjalista ds. informatyki	\N	7500.00	\N	f	f	f	2025-10-29 00:00:00+01	2025-11-16 00:00:00+01	f	f	590	632	636	1	9	18
420	Programista/grafik	\N	7000.00	\N	f	f	f	2025-10-29 00:00:00+01	2025-11-28 00:00:00+01	f	f	590	683	637	1	9	18
421	130 - Teleinformatyk	\N	5200.00	\N	f	f	f	2025-10-27 00:00:00+01	2025-11-26 00:00:00+01	f	f	590	684	638	1	9	18
422	Informatyk	\N	3200.00	\N	f	f	f	2025-10-27 00:00:00+01	2025-11-28 00:00:00+01	f	f	590	685	639	1	9	18
423	Informatyk/młodszy specjalista ds. marketingu	\N	4700.00	5500.00	f	f	f	2025-10-24 00:00:00+02	2025-11-23 00:00:00+01	f	f	590	686	640	1	9	18
400	Informatyk	\N	7000.00	\N	f	f	f	2025-10-23 00:00:00+02	2025-11-30 00:00:00+01	f	f	590	637	641	1	9	18
265	Picking zakupów - kompletowanie zamówień	Holding1 to grupa kapitałowa, w którą wchodzą takie spółki jak PGD, Traficar, Express Car Rental, Workeo, Megapolis i wiele innych. Każda spółka ma za zadanie ułatwiać życie i zapewniać usługi bez stresu, bez nadmiernych emocji – prosto do celu. Dołącz do grona pracowników Holding1 i rozwijaj z nami nowe linie biznesowe pracując elastycznie w jednej z największych organizacji w Polsce. W związku z rozwojem naszego nowego projektu, poszukujemy Kandydatek i Kandydatów na stanowisko: Picking zakupów - kompletacja zamówień (branża spożywcza). Zakupy kompletowane są w sklepie przy ul. Zakopiańskiej, ul. Witosa 7, ul. Podgórska 34, ul. Pawia 5, Medweckiego 2, ul. Fabryczna 11 lub ul. Lea 53 Dostaniesz zlecenie na skanerze, skompletujesz zakupy spożywcze i przekażesz je kurierowi w celu dostarczenia klientowi.	30.50	\N	f	f	f	2025-10-31 11:57:19+01	2025-11-30 11:57:19+01	f	t	398	406	425	1	1	26
267	Wsparcie produkcji - NATYCHMIASTOWY START!	Obsługa prostych maszyn i wsparcie produkcji w renomowanej firmie w Będzinie. Idealna oferta dla pełnoletnich uczniów i studentów. Możliwość wyboru godzin wykonywania zlecenia, darmowy, monitorowany parking, zaplecze socjalne [szatnia, stołówka], dostęp do rekrutacji wewnętrznych z furtką do specjalistycznych ofert z prestiżowymi szkoleniami, możliwość rozwoju ścieżki zawodowej, umowa zlecenie z opcją przystąpienia do dobrowolnego ubezpieczenia chorobowego, elastyczne zarządzanie wynagrodzeniem, nowoczesna aplikacja do załatwiania formalności w telefonie, program stypendialny wspierający rozwój Młodych Talentów, dostęp do obiektów sportowych dzięki możliwości zakupu karty Multisport.	\N	\N	\N	f	f	2025-10-24 14:58:13+02	2025-11-23 13:58:13+01	f	t	398	431	427	\N	\N	26
270	Praca w pralni - Siemianowice Śląskie	Sortowanie, przygotowywanie do prania i suszenia odzieży, czytanie uwag przy skanowaniu odzieży, praca przy komputerze.	30.50	30.50	f	f	f	2025-10-24 16:31:23+02	2025-11-23 15:31:23+01	f	t	398	434	430	1	1	26
273	Pracownik Magazynu w Internetowym Sklepie Wędkarskim	Lider e-commerce branży wędkarskiej w Polsce zatrudni osobę odpowiedzialną za pakowanie zamówień w internetowym sklepie wędkarskim. Zatrudniona osoba będzie odpowiedzialna za pakowanie paczek, wystawianie dokumentów sprzedażowych, przygotowywanie przesunięć międzymagazynowych, realizację zamówień klientów, przyjmowanie dostaw.	\N	\N	\N	f	f	2018-10-30 18:51:41+01	2025-12-12 12:59:40+01	f	t	398	437	433	\N	\N	26
274	Operator Wózka Widłowego (umowa z DHL)	Jako DHL jesteśmy światowym liderem w zarządzaniu łańcuchem dostaw. Twoją główną rolą na stanowisku Operator Wózka Widłowego będzie obsługa procesów magazynowych takich jak rozładunek, załadunek towaru w wyznaczone miejsce, sortowanie, kompletacja towaru wg. zamówień.	\N	\N	\N	f	f	2025-10-22 13:24:02+02	2025-11-21 12:24:03+01	f	t	398	416	434	\N	\N	26
268	Sortowanie paczek	Jobman Sp. z o.o. poszukuje osób chętnych do podjęcia pracy tymczasowej na stanowisku: Sortowanie paczek. Do Twoich zadań należeć będzie sortowanie przesyłek, rozładunek i załadunek, drobne czynności porządkowe oraz inne prace magazynowe zlecone przez przełożonego. Nie wymagamy doświadczenia.	33.00	\N	f	f	f	2025-10-24 14:45:25+02	2025-11-23 13:45:25+01	f	t	398	432	490	1	1	26
271	Praca w NOWYM magazynie MediaExpert - dołącz do EXTRA EKIPY!	Jesteśmy Expertem, Media Expertem! Rządzimy na rynku elektro w Polsce! Ale to nie koniec naszych planów, dlatego szukamy:\nMagazynierów\nLokalizacja: Łódź, ul. Józefów 3c\nSprawdź jedną z najlepszych ofert w Łodzi! Chodź i zobacz swoje miejsce pracy, a potem podpisz umowę o pracę! Twoi koledzy już tu są! A jeśli nie, to poznasz nowych!\nZapewnimy Ci:\nzatrudnienie na podstawie umowy o pracę\njasny system wynagradzania – do Twojego wynagrodzenia zasadniczego dodajemy premię frekwencyjną i konkursową/wydajnościową, dodatek nocny podwyższony do 40% (jeśli występują godziny nocne)\ndodatkowy dodatek za dyspozycyjność w IVQ - XI 300 zł brutto, XII 1500 zł brutto\ndodatek za doświadczenie od 100 do 300 zł brutto\nbezpłatny transport pracowniczy na terenie Łodzi\nkartę MultiSport z dofinansowaniem\nprywatną opiekę medyczną Medicover z dofinansowaniem\nopiekę Trenerów, którzy wdrożą Cię do pracy\nstołówkę pracowniczą i obiady z dofinansowaniem (dwie wersje: mięsna i wegetariańska)\nprogram poleceń pracowniczych\nbezpłatną kawę i herbatę z automatu\nBaby Expert, czyli wyprawkę dla Twojego nowonarodzonego dziecka\nubezpieczenie grupowe dla Ciebie i Twojej rodziny&nbsp;na preferencyjnych warunkach\nzniżki na zakup sprzętu w naszych elektromarketach\nmożliwość awansu\npomoc i opiekę fundacji Mediaexpert „Włącz się”\nstrefę relaksu\nTwoje zadania w zależności od obszaru, na którym zostaniesz zatrudniony/a:\nprzyjęcie oraz relokacja towaru w magazynie\nkompletacja i załadunek towaru\npakowanie zamówień\npraca ze skanerem\nprowadzenie dokumentacji magazynowej\nwsparcie pozostałych procesów magazynowych\nudział w inwentaryzacji towaru w magazynie\nOd Ciebie oczekujemy:\ndyspozycyjności do pracy w systemie zmianowym\ndokładności i sumienności w wykonywaniu zadań\nzaangażowania w powierzoną pracę\nprzestrzegania zasad i procedur obowiązujących w firmie\nTo co, jesteśmy umówieni na wspólną przyszłość?	\N	\N	\N	f	f	2025-11-07 14:36:18+01	2025-12-07 14:36:18+01	f	t	398	326	499	\N	\N	26
424	Administrator/instalator sieci	\N	4666.00	\N	f	f	f	2025-10-07 00:00:00+02	2025-11-30 00:00:00+01	f	f	590	688	642	1	9	18
425	Młodszy konsultant / wdrożeniowiec	\N	5200.00	5800.00	f	f	f	2025-09-22 00:00:00+02	2025-11-30 00:00:00+01	f	f	590	689	643	1	9	18
275	Magazynier/ka	Firma A.B.Z. Sp. z o.o. z siedzibą w Wargowie (pod Poznaniem) zatrudni magazyniera. Zajmujemy się produkcją i montażem stoisk targowych. Zadania: - rozładunek i załadunek towaru dbanie o powierzone mienie oraz towar - gotowość do pracy fizycznej - kontrolowanie zgodności dokumentacji magazynowej - przygotowywanie gotowych mebli do załadunku - prace porządkowe w magazynie Oferujemy: - pracę w zgranym zespole, - stabilne zatrudnienie, - atrakcyjne wynagrodzenie, - komfortowe warunki pracy, - umowa o pracę lub inna forma współpracy. Osoby zainteresowane uprzejmie prosimy o przesłanie CV .	\N	\N	\N	f	f	2024-12-06 08:58:06+01	2025-12-05 09:27:32+01	f	t	398	439	435	\N	\N	26
281	Uzupełnianie automatów	Uzupełnianie automatów wydających w produkty BHP - rękawice robocze. Wymagane posiadanie własnego samochodu, znajomość obsługi komputera, dyspozycyjność, sumienność i zaangażowanie. Konieczne posiadanie miejsca do składowania towaru ( powierzchnia około 4m-6m² ).	50.00	60.00	f	f	f	2025-11-07 15:25:49+01	2025-12-07 15:26:55+01	f	t	398	445	441	1	8	26
284	Operator wózka widłowego/ Magazynier	Poszukujemy kandydatów na stanowisko Magazynier / Operator wózka widłowego do mroźni składowej w Niepruszewie. Zakres obowiązków: Obsługa procesu przyjęcia towarów, rozładunek i załadunek samochodów ciężarowych, obsługa wózka widłowego, kontrola stanów magazynowych, praca ze skanerem, uczestnictwo w procesie inwentaryzacji towaru.	\N	\N	\N	f	f	2022-11-28 14:55:46+01	2025-11-19 13:37:09+01	f	t	398	448	444	\N	\N	26
244	Magazynier (umowa zlecenie) Media Expert - Zabrze	Kompletacja towaru zgodnie z zamówieniami, przygotowanie towaru do transportu z zachowaniem najwyższych standardów, kontrola jakości i ilości towaru przyjmowanego oraz wydawanego, zapewnienie prawidłowego lokowania towaru w magazynie, obsługa skanera, wykonywanie innych podstawowych czynności magazynowych.	\N	\N	\N	f	f	2025-10-24 14:58:12+02	2025-11-23 13:58:12+01	f	t	398	326	446	\N	\N	26
246	Pracownik Magazynowy	Jesteśmy noo.ma, Naszym celem jest oferowanie przystępnych cenowo produktów o wysokiej jakości, wyprodukowanych w całości w Europie na bazie materiałów pozyskiwanych w zrównoważony sposób ze sprawdzonych źródeł. Dostarczyliśmy Nasze meble do ponad 150000 zadowolonych klientów. Aktualnie do Naszego zespołu poszukujemy: pracownika magazynowego. Zakres obowiązków: przyjmowanie i rozładunek dostaw, kompletacja zamówień i przygotowywanie towaru do wysyłki, dbanie o porządek w magazynie, podstawowa obsługa dokumentacji magazynowej. Wymagania: mile widziane min. 1 rok doświadczenia w pracy magazynowej, dodatkowym atutem będą uprawnienia na wózek widłowy, sumienność, zaangażowanie i chęć do pracy. Oferujemy: wynagrodzenie po roku pracy: 4100 zł netto + premie miesięczne, praca na jedną zmianę: 8:00–16:00 (pon.–pt.), transport pracowniczy z Poznania, ciepły obiad pracowniczy, zapewniamy odzież roboczą i jej pranie, przyjazna atmosfera i mały, zgrany zespół. Jeśli szukasz stabilnego zatrudnienia w dobrej atmosferze – zapraszamy! Kontakt poprzez portal OLX.	4100.00	4100.00	f	f	f	2025-11-10 19:12:00+01	2025-12-10 19:30:23+01	f	t	398	409	447	1	9	26
253	Pracownik Magazynowy (umowa o pracę bezpośrednio z DHL)	Jako DHL jesteśmy światowym liderem w zarządzaniu łańcuchem dostaw. Obsługujemy największe magazyny w Polsce i na świecie dla różnych branż. Pracujemy w oparciu o wzajemny szacunek, a od Ciebie oczekujemy otwartości na nowe wyzwania, których u nas nie brakuje! Jako Magazynier DHL Supply Chain będziesz mieć możliwość dostarczania naszym klientom usług na najwyższym poziomie, a my zapewnimy Ci możliwości rozwoju oraz codzienne wsparcie. Będziesz zajmować się zbieraniem produktów na magazynie i skanowaniem ich kodów przy pomocy ręcznego skanera, przenoszeniem ręcznym towarów lub za pomocą wózka paletowego, selekcją jakościową i ilościową towarów, oceną zgodności produktów z opisem, przygotowaniem towarów do dalszego przetwarzania.	\N	\N	\N	f	f	2025-10-21 10:48:34+02	2025-11-20 09:48:34+01	f	t	398	416	448	\N	\N	26
276	Pracownik Magazynowy	Praca w magazynie mebli i artykułów wyposażenia wnętrz w Robakowie koło Poznania. Przyjmowanie, rozładunek i załadunek dostaw, przygotowywanie towaru do wysyłki (pakowanie, zabezpieczanie, etykietowanie), kompletowanie zamówień. Oferta pracy tymczasowej z darmowym transportem i dofinansowaniem do obiadów.	4746.00	5246.00	f	f	f	2025-10-24 11:00:27+02	2025-11-23 10:00:28+01	f	t	398	440	476	1	9	26
278	Pracownik Magazynowy	Jako Magazynier DHL Supply Chain będziesz mieć możliwość dostarczania naszym klientom usług na najwyższym poziomie oraz zapewnimy Ci wiele możliwości rozwoju oraz codzienne wsparcie.	\N	\N	\N	f	f	2025-06-12 15:30:26+02	2025-12-14 09:57:57+01	f	t	398	416	487	\N	\N	26
280	Pracownik ds. rozładunku towaru	Manpower (Agencja zatrudnienia nr 412) to globalna firma o ponad 70-letnim doświadczeniu, działająca w 82 krajach. Na polskim rynku jesteśmy od 2001 roku i obecnie posiadamy prawie 35 oddziałów w całym kraju. Naszym celem jest otwieranie przed kandydatami nowych możliwości, pomoc w znalezieniu pracy odpowiadającej ich kwalifikacjom i doświadczeniu. Skontaktuj się z nami - to nic nie kosztuje, możesz za to zyskać profesjonalne doradztwo i wymarzoną pracę!\n\nDla naszego Klienta poszukujemy osób do pracy przy rozładunku i rozmieszczaniu towaru zgodnie ze standardami bezpieczeństwa żywności.	30.50	50.00	\N	f	f	2025-11-07 15:25:11+01	2025-12-07 15:25:12+01	f	t	398	444	544	1	1	26
426	Specjalista/ka działu helpdesk	\N	4797.00	6850.00	f	f	f	2025-08-21 00:00:00+02	2025-11-17 00:00:00+01	f	f	590	690	644	1	9	18
427	Projektant w zakresie sieci teletechnicznych	\N	4666.00	\N	f	f	f	2025-03-25 00:00:00+01	2025-12-31 00:00:00+01	f	f	590	660	645	1	9	18
428	Monter światłowodów - pracownik fizyczny	Opis stanowiska:– wykonywanie prac ziemnych– montaż instalacji teletechnicznych– spawanie światłowodów– montaż korytek / listew wewnątrz budynków– podwieszanie światłowodów pod słupy telekomunikacyjne– układanie rozbieranie kostki brukowej– wykonywanie przecisków	\N	\N	\N	f	f	2024-05-07 00:00:00+02	2025-11-19 00:00:00+01	f	f	590	692	646	\N	\N	18
429	Programista Python/Flask/pandas	Wymagania: Znakomita znajomość języka Python. Dobra znajomość Frameworków Pythonowych (Flask, FastAPI, DJango). Dobra znajomość Pandas, NumPy. Znajomość systemów kontroli wersji (GIT). Oferujemy: Code Review, Projekty open source, Praca w dynamicznie rozwijającym się zespole, Multisport, Catering, Biuro dobrze skomunikowane SKM / ZKM w centrum Gdyni.	\N	\N	\N	f	f	2022-07-27 00:00:00+02	2025-11-19 00:00:00+01	f	f	590	693	647	\N	\N	18
430	Staż programista	Wymagania: Podstawowa znajomość języka Python lub Javascript, chęć doskonalenia umiejętności, podstawowa znajomość SQL / HTTP, znajomość systemów kontroli wersji GIT, umiejętność tworzenie struktur danych takich jak np. drzewo binarne. Oferujemy: Code Review, sprawdzoną ścieżkę kariery w IT, projekty open source, praca w dynamicznie rozwijającym się zespole, ciekawe projekty, kawa, napoje, darty. Biuro dobrze skomunikowane SKM / ZKM w centrum Gdyni.	\N	\N	\N	f	f	2022-05-02 00:00:00+02	2025-11-19 00:00:00+01	f	f	590	693	648	\N	\N	18
431	Staż programistyczny	Wymagania: Podstawowa znajomość języka Python lub Javascript, chęć doskonalenia umiejętności, podstawowa znajomość SQL / HTTP, znajomość systemów kontroli wersji GIT, umiejętność tworzenie struktur danych takich jak np. drzewo binarne. Oferujemy: Code Review, sprawdzoną ścieżkę kariery w IT, projekty open source, praca w dynamicznie rozwijającym się zespole, ciekawe projekty, kawa, napoje, darty, biuro dobrze skomunikowane SKM / ZKM w centrum Gdyni.	\N	\N	\N	f	f	2022-02-11 00:00:00+01	2025-11-19 00:00:00+01	f	f	590	693	649	\N	\N	18
264	Magazynier/Magazynierka	Przyjmowanie dostaw towaru, Rozkładanie towaru w magazynie – przenoszenie i/lub rozwożenie produktów, Przygotowywanie zamówień dla klientów detalicznych i/lub hurtowych, Sprawdzanie zgodności przygotowanych do wysyłki towarów, Czynności związane z wysyłką towaru – pakowanie produktów według ustalonych procedur, Załadunek zamówień do aut dostawczych, Kontrola stanów magazynowych, Zgłaszanie uszkodzonych produktów do kierownika oraz ich usuwanie z magazynu, Dbanie o porządek w miejscu pracy, Przestrzeganie zasad bezpieczeństwa, Współpraca z innymi działami.	\N	\N	\N	f	f	2025-10-24 08:55:47+02	2025-11-23 07:55:48+01	f	t	398	254	449	\N	\N	26
285	Pracownik Magazynowy w Drogerii Warszawa Szpotańskiego 4	przyjmowanie dostaw towar, układanie towaru na półkach i w magazynie drogerii, prace związane z uzupełnianiem towaru na sali sprzedaży w ciągu dnia.	\N	\N	\N	f	f	2025-11-07 14:19:44+01	2025-12-07 14:19:44+01	f	t	398	458	451	\N	\N	26
261	Magazynier w Firmie Produkcyjnej	Firma FOLIMPEX specjalizująca się w produkcji i sprzedaży materiałów termoizolacyjnych zatrudni pracownika na stanowisko Magazyniera. Kluczowe zadania na tym stanowisku to: przewożenie towaru z hal produkcyjnych do magazynów, wypełnianie dokumentacji przyjmowania towaru z produkcji, dbałość o należyte składowanie i przechowywanie towarów, przestrzeganie zasad BHP oraz utrzymywanie należytego porządku na hali magazynowej.	4000.00	\N	f	f	f	2025-09-15 15:07:11+02	2025-12-13 10:48:02+01	f	t	398	424	453	1	9	26
242	Magazynier/Magazynierka	Firma buduje swoją pozycję na rynku w oparciu o wiedzę i kwalifikacje pracowników. Zakres obowiązków: przyjmowanie dostaw towaru, rozkładanie towaru w magazynie – przenoszenie i/lub rozwożenie produktów, przygotowywanie zamówień dla klientów detalicznych i/lub hurtowych, sprawdzanie zgodności przygotowanych do wysyłki towarów, czynności związane z wysyłką towaru – pakowanie produktów według ustalonych procedur, załadunek zamówień do aut dostawczych, kontrola stanów magazynowych, zgłaszanie uszkodzonych produktów do kierownika oraz ich usuwanie z magazynu, dbanie o porządek w miejscu pracy, przestrzeganie zasad bezpieczeństwa, współpraca z innymi działami.	\N	\N	\N	f	f	2025-11-07 10:32:11+01	2025-12-07 10:32:11+01	f	t	398	254	454	\N	\N	26
287	Pracownik Magazynowy w Drogerii	Twój zakres obowiązków, przyjmowanie dostaw towar, układanie towaru na półkach i w magazynie drogerii, prace związane z uzupełnianiem towaru na sali sprzedaży w ciągu dnia. Nasze wymagania, gotowość do pracy fizycznej, umiejętność pracy w zespole, uczciwość, sumienność.	\N	\N	\N	f	f	2025-10-24 10:19:16+02	2025-11-23 09:19:16+01	f	t	398	458	455	\N	\N	26
258	Magazynier	Kompletacja zamówień za pomocą skanera, przygotowanie towaru do wysyłki, rozmieszczanie asortymentu w lokacjach magazynowych, przyjmowanie towaru na stan magazynowy.	37.00	39.00	f	f	f	2025-10-24 10:41:53+02	2025-11-23 09:41:53+01	f	t	398	421	456	1	1	26
288	Magazynier / Elastyczny grafik /Natychmiastowy dostęp do wynagrodzenia	Agencja Pracy Tymczasowej BeFlexi Sp. z o.o., wpisana do rejestru podmiotów prowadzących agencje zatrudnienia pod numerem 28050, poszukuje pracowników na stanowisko: Pracownik magazynu – kompletacja zamówień (artykuły papiernicze, zabawki itp.) Miejsce: Modlniczka (k. Krakowa) Start: od 12 listopada Stawka: 30,50 zł brutto/h Wybierz, jak chcesz pracować! poranna zmiana: 5:00–13:00 popołudniowa zmiana: 14:00–22:00  od końca listopada także nocne (22:00–6:00)  dla chętnych — niedziele (7:00–17:00) Możesz samodzielnie wybierać dni pracy! Możesz dostosować godziny pracy na danej zmianie do swojej dyspozycyjności! Czym się zajmiesz: Kompletacja zamówień w magazynie sklepu z książkami, artykułami papierniczymi, zabawkami itp.. Dodatkowy plus:  Możliwość wypłaty nawet 50% wynagrodzenia na bieżąco! Dostęp do karty sportowej MEDICOVER Sport. Aplikuj już teraz!	30.50	30.50	f	f	f	2025-11-07 09:08:52+01	2025-12-07 09:12:45+01	f	t	398	466	457	1	1	26
266	Magazynier - Katowice	Agata S.A. - Naszą pasją są wnętrza. Oferując szeroki wybór mebli i artykułów wyposażenia wnętrz, pragniemy stać się częścią przestrzeni, która wyraża charakter, styl i wartości naszych Klientów. W ten sposób pomagamy tworzyć niepowtarzalną atmosferę domu.\n\nDo głównych obowiązków na tym stanowisku będzie należało:\nRozładunek dostaw i przyjęcie towaru;\nPrzygotowanie oraz wydanie towarów;\nDbałość o powierzony towar i mienie firmy.\n\nOd kandydatów oczekujemy:\nElastyczności czasu pracy;\nGotowości do ciężkiej pracy fizycznej;\nDoświadczenia w pracy na magazynie;\nUmiejętności pracy w zespole.\n\nZatrudnionym oferujemy:\nMożliwość zrobienia kursu na wózki widłowe;\nStabilne warunki zatrudnienia;\nAtrakcyjne wynagrodzenie;\nPremię retencyjną;\nMożliwość rozwoju;\nSystem wdrożenia pracownika;\nPrywatna opieka medyczna Medicover;\nPakiet sportowy Medicover Sport;\nGrupowe ubezpieczenie zdrowotne;\nWczasy pod gruszą;\nBonus Bożonarodzeniowy;\nZniżki pracownicze;\nPrezent urodzinowy.	\N	\N	\N	f	f	2025-11-07 10:52:57+01	2025-12-07 10:52:58+01	f	t	398	417	458	\N	\N	26
289	Sprzedawca - magazynier	Obsługa klientów w kontakcie bezpośrednim oraz telefonicznym, Doradzanie klientom w kwestiach dopasowania oferty produktowej do indywidualnych potrzeb, Rozliczenie bieżących zamówień, Dbałość o najwyższą jakość obsługi transakcji sprzedażowych, Udzielanie informacji o oferowanych produktach oraz ich dostępności, Przyjmowanie towaru oraz kompletacja zamówień na magazynie.	\N	\N	\N	f	f	2025-10-31 15:36:32+01	2025-11-30 15:36:32+01	f	t	398	468	459	\N	\N	26
290	Magazynier - Kierowca	Firma MARTOM M. RYCHLIŃSKA I WSPÓLNICY działająca w branży tworzyw sztucznych i opakowań foliowych poszukuję pracownika na stanowisko magazynier – kierowca. Główne obowiązki: Wykonywanie prac rozładunkowo – załadunkowych, sprawdzanie zgodności dostarczonego towaru z dokumentacją, przygotowywanie surowców do produkcji, kompletowanie towaru oraz jego wysyłka, bieżące monitorowanie zapasów magazynowych, konserwacja urządzeń magazynowych, obsługa wózków widłowych, dbanie o porządek w magazynie, dostawa towaru do klientów.	\N	\N	\N	\N	\N	2025-11-07 13:13:07+01	2025-12-07 13:15:13+01	f	t	398	469	460	\N	\N	26
241	Amazon | Ostatnia szansa! Do 36,22zł/h brutto*+2000 zł** brutto ekstra	Poszukujemy pracowników na stanowisko pracownika magazynu w Gliwicach. Ostatnie dni rekrutacji w Centrum Logistyki Amazon! Pospiesz się! To ostatnia szansa na to by otrzymać +2000zł brutto** ekstra. Nie czekaj! Liczba miejsc ograniczona! Ostatnie dni rekrutacji w Centrum Logistyki Amazon! Pracuj dla firmy, która zdobyła tytuł Top Employer, i zyskaj więcej, bo to Twoja zmiana i Twoja nowa, wyższa stawka godzinowa do 36,22 zł/godz. brutto* Pospiesz się! To ostatnia szansa na to by otrzymać +2000zł brutto** ekstra. Poleć znajomych do pracy, Ty i osoba polecona zyskacie wspólnie do 5000 zł brutto***! Nie wymagamy od Ciebie doświadczenia. Możesz liczyć na wsparcie wyspecjalizowanych instruktorów.	\N	36.22	f	f	f	2025-10-24 19:58:17+02	2025-11-23 18:58:17+01	f	t	398	404	480	1	1	26
279	Pracownik Magazynowy	Praca w magazynie mebli i artykułów wyposażenia wnętrz. Przyjmowanie, rozładunek i załadunek dostaw, przygotowywanie towaru do wysyłki (pakowanie, zabezpieczanie, etykietowanie), kompletowanie zamówień według specyfikacji klientów.	4746.00	5246.00	f	f	f	2025-10-24 11:03:26+02	2025-11-23 10:03:27+01	f	t	398	440	481	1	9	26
239	Magazynier - Kielce INTER-TEAM	TWÓJ ZAKRES OBOWIĄZKÓW: Przyjmowanie dostaw towaru (części, narzędzia oraz akcesoria samochodowe), Rozkładanie towarów zgodnie z przyjętymi zasadami, Przygotowywanie i weryfikacja zamówień dla klientów, Wykonywanie czynności związane z wydaniem towaru, Kontrolowanie stanów magazynowych, Dbanie o porządek i bezpieczeństwo w miejscu pracy. NASZE WYMAGANIA: Mile widziana umiejętność obsługi komputera, Zaangażowanie, dokładność i sumienność w wykonywaniu obowiązków, Komunikatywność i umiejętność pracy w zespole, Wcześniejsze doświadczenie w pracy magazynowej będzie dodatkowym atutem. TO OFERUJEMY: Stałe zatrudnienie w oparciu o umowę o pracę lub umowę zlecenie, Szkolenie wstępne umożliwiające sprawne wykonywanie powierzonych obowiązków, System nagradzania oparty na wynikach, Pakiet świadczeń pozapłacowych/socjalnych (m.in. dofinansowanie do prywatnej opieki zdrowotnej i pakietu sportowego, możliwość przystąpienia do ubezpieczenia grupowego na preferencyjnych warunkach, dodatkowe zniżki w systemie kafeteryjnym, dofinansowanie do żłobka/przedszkola, świadczenia okolicznościowe), Inicjatywy pracownicze (konkursy z nagrodami, akcje prozdrowotne i charytatywne), Przyjazną atmosferę w miejscu pracy, Zniżki pracownicze na części i akcesoria samochodowe.	\N	\N	\N	f	f	2025-10-31 15:04:50+01	2025-11-30 15:04:51+01	f	t	398	402	461	\N	\N	26
291	Magazynier, Kierowca, Pakowacz, kompletacja zamówień w branży mięsnej	Producent wędlin firma Kiszeczka z Karczewa poszukuje pracownika - mężczyzny, do obsługi magazynu, pakowania wędlin, kompletowanie zamówień, dostawa do sklepów firmowych oraz sklepów partnerskich. Szukamy energicznej, zaangażowanej, dobrze zorganizowanej oraz samodzielnej osoby. Praca w siedzibie firmy w Janowie przy ul. Janów 60. Oferujemy pracę w magazynie od poniedziałku do piątku w godzinach od 00.00 do 10.00-14.00 oraz soboty od 00.00 do 10.00	6000.00	9000.00	f	f	f	2021-01-08 08:48:09+01	2025-12-10 10:32:43+01	f	t	398	471	462	1	9	26
292	Magazynier w sklepie spożywczym	Sklepy Market Punkt są częścią Polskiej Sieci Handlowej Lewiatan od ponad 20 lat działające w obszarze handlu detalicznego. Zatrudniamy pond 300 osób w 13 lokalizacjach w Krakowie, Węgrzcach oraz Słomnikach. Poszukujemy mężczyzny do pracy na stanowisku magazyniera do naszego sklepu, który znajduje się w Krakowie na ul. Bobrzyńskiego 33. Oferujemy: -umowę o pracę od pierwszego jej dnia -stałe, atrakcyjne wynagrodzenie (5360-5760 brutto) -możliwość przystąpienia do grupowego ubezpieczenia -bony przedświąteczne Obowiązki : -przyjmowanie dostaw -sprawdzanie zgodności dostaw z fakturą -wprowadzanie faktur do systemu -dbanie o porządek w magazynie Wymagamy: -doświadczenia na stanowisku magazyniera -badań do celów sanitarno-epidemiologicznych -wysokiej kultury osobistej -dobrej organizacji pracy -chęci do pracy -uczciwości i rzetelności w wykonywaniu obowiązków Pracujemy w systemie dwu-zmianowym (brak zmian nocnych!) ﻿ Zainteresowanych proszę o przesłanie swojego CV lub kontakt od pn do pt w godzinach 7-15. Do usłyszenia! :)	5360.00	5760.00	f	f	f	2025-07-14 10:46:44+02	2025-12-14 08:49:04+01	f	t	398	473	463	1	9	26
293	Magazynier - Bricomarche Sępólno Krajeńskie	Przyjmowanie i sprawdzanie dostaw, kontrola jakości i ilości przyjmowanego i wydawanego towaru, dbałość o porządek w magazynie, obrót opakowaniami zwrotnymi.	\N	\N	\N	f	f	2025-11-07 14:26:15+01	2025-12-07 14:28:12+01	f	t	398	474	464	\N	\N	26
269	Magazynier - dział zwrotów	Naszym Klientem jest znana firma logistyczna, magazyn z częściami m.in do samochodów ciężarowych znajdującego się w Będzinie (Łagisza).\nObecnie szukamy magazynierów na dział zwrotów.\n\nMagazynier - dział zwrotów\n\nOpis stanowiska:\n• Przyjmowanie i weryfikacja zwracanych towarów od klientów.\n• Sprawdzanie kompletności i stanu technicznego zwróconych produktów.\n• Wprowadzanie danych o zwrotach do systemu\n• Oznaczanie, etykietowanie i odpowiednie rozmieszczanie zwrotów w magazynie.\n• Przygotowywanie dokumentacji związanej z procesem zwrotów (protokoły, noty, korekty).\n\nWymagania:\n• Doświadczenie w pracy magazynowej lub w dziale zwrotów / reklamacji będzie mile widziane.\n• Umiejętność obsługi komputera i podstawowych programów biurowych (np. Excel, systemy magazynowe).\n• Dokładność, spostrzegawczość i dobra organizacja pracy.\n• Odpowiedzialność oraz umiejętność pracy w zespole.\n• Gotowość do pracy w systemie zmianowym.\n• Mile widziane uprawnienia UDT na wózki widłowe\n\nOferujemy:\n• Umowę o Pracę tymczasową z możliwością przejęcia przez firmę\n• Atrakcyjne wynagrodzenie miesięczne od 5000 zł brutto (bez UDT), 5400 zł brutto (z UDT)\n• Dodatkowe premie za wyniki do 700 zł\n• Praca od poniedziałku do piątku\n• System pracy 2 zmiany 06:00-14:00 oraz 14:00-22:00\n&nbsp;\nOferta dotyczy pracy tymczasowej.\nAPT 364\n&nbsp;\n'Informujemy, iż w spółkach z Grupy Adecco działających w Polsce wdrożyliśmy procedurę dokonywania zgłoszeń wymaganą Ustawą o Ochronie Sygnalistów. Szczegółowe informacje są dostępne w biurach Adecco.'	5000.00	5400.00	f	f	f	2025-10-24 11:45:42+02	2025-11-23 10:45:42+01	f	t	398	423	465	1	9	26
294	Magazynier - Biała Podlaska	Agata S.A. - Naszą pasją są wnętrza. Oferując szeroki wybór mebli i artykułów wyposażenia wnętrz, pragniemy stać się częścią przestrzeni, która wyraża charakter, styl i wartości naszych Klientów. W ten sposób pomagamy tworzyć niepowtarzalną atmosferę domu. Do głównych obowiązków na tym stanowisku będzie należało: Rozładunek dostaw i przyjęcie towaru; Przygotowanie oraz wydanie towarów; Dbałość o powierzony towar i mienie firmy. Od kandydatów oczekujemy: Elastyczności czasu pracy; Gotowości do ciężkiej pracy fizycznej; Doświadczenia w pracy na magazynie; Umiejętności pracy w zespole. Zatrudnionym oferujemy: Możliwość zrobienia kursu na wózki widłowe; Stabilne warunki zatrudnienia; Atrakcyjne wynagrodzenie; Premię retencyjną; Możliwość rozwoju; System wdrożenia pracownika; Prywatna opieka medyczna Medicover; Pakiet sportowy Medicover Sport; Grupowe ubezpieczenie zdrowotne; Wczasy pod gruszą; Bonus Bożonarodzeniowy; Zniżki pracownicze; Prezent urodzinowy.	\N	\N	\N	f	f	2025-11-07 10:33:06+01	2025-12-07 10:33:06+01	f	t	398	417	466	\N	\N	26
295	Magazynier/Magazynierka	Firma Auto Partner S.A. poszukuje zaangażowanych i kompetentnych osób na stanowisko Magazyniera/Magazynierki w Łomiankach. Do obowiązków należy przyjmowanie dostaw towaru, rozkładanie towaru w magazynie, przygotowywanie zamówień, sprawdzanie zgodności towarów, pakowanie produktów, załadunek zamówień do aut dostawczych, kontrola stanów magazynowych, zgłaszanie uszkodzonych produktów oraz dbanie o porządek w miejscu pracy. Mile widziana umiejętność obsługi komputera i skanerów, umiejętność pracy w grupie i dobra organizacja pracy, motywacja do pracy, rzetelność, dokładność, sumienność i zaangażowanie. Oferujemy stabilne warunki zatrudnienia na umowę o pracę, profesjonalne narzędzia pracy, możliwość zdobycia cennego doświadczenia, wsparcie na każdym etapie wdrożenia do pracy oraz możliwość dołączenia do pakietu sportowego Multisport lub Medicover Sport.	\N	\N	\N	f	f	2025-10-31 14:41:54+01	2025-11-30 14:41:54+01	f	t	398	254	467	\N	\N	26
296	Magazynier - Łódź	Agata S.A. oferuje szeroki wybór mebli i artykułów wyposażenia wnętrz. Do głównych obowiązków na tym stanowisku będzie należało: Rozładunek dostaw i przyjęcie towaru, przygotowanie oraz wydanie towarów, dbałość o powierzony towar i mienie firmy.	\N	\N	\N	f	f	2025-10-31 11:34:28+01	2025-11-30 11:34:28+01	f	t	398	417	468	\N	\N	26
297	Magazynier - Operator Wózka	HAMELIN Polska Sp. z o.o. jest częścią europejskiego koncernu, będącego jednym z największych w Europie producentów i dystrybutorów artykułów papierniczych dla biura i szkoły. Aktualnie poszukuje do Magazynu Surowców kandydatów na stanowisko: MAGAZYNIER - OPERATOR WÓZKA. Do obowiązków osoby zatrudnionej na w/w stanowisku należeć będzie:  - przyjmowanie dostaw surowców, - przygotowanie surowców do wydania, - sprawdzanie zgodności dostarczonych/ wydawanych surowców z zamówieniami, - rozładunek i załadunek surowców, - organizowanie dokumentacji dotyczących zamówień surowców, - dbałość o porządek w magazynie. Wymagania:  - wykształcenie minimum zawodowe, - uprawnienia do obsługi wózków widłowych UDT, - min. rok doświadczenia w pracy w magazynie oraz przy administracji dokumentacji magazynowej, - znajomość tematyki gospodarki magazynowej, - umiejętność obsługi komputera. Wybranemu kandydatowi oferujemy:  - zatrudnienie na podstawie umowy o pracę na pełny etat; - atrakcyjne wynagrodzenie; - możliwość otrzymania premii dwumiesięcznych, zadaniowych; - przedświąteczne świadczenie pieniężne; - dofinansowanie do karty Multisport; - zniżki na firmowe produkty; - możliwość przystąpienia do ubezpieczenia grupowego na życie na atrakcyjnych warunkach; - wsparcie przy podnoszeniu kwalifikacji zawodowych.	\N	\N	\N	f	f	2025-11-07 13:40:49+01	2025-12-07 13:40:49+01	f	t	398	479	469	\N	\N	26
298	Magazynier - dz. części zamiennych - GSC Swadzim	GSC Sp. Z O.O. jest autoryzowanym Dealerem i Partnerem Serwisowym marki FORD TRUCK i IVECO. Przedsiębiorstwo prowadzi rekrutację do nowo otwartego oddziału serwisu pojazdów ciężarowych i dostawczych marki Iveco na stanowisko: Magazynier. Obsługa magazynu w autoryzowanym serwisie pojazdów ciężarowych i dostawczych IVECO. Identyfikacja oraz dobór części zamiennych z wykorzystaniem programów katalogowych. Przygotowywanie ofert i kompletowanie towaru zgodnie z zamówieniami działu serwisu. Kontrola jakościowa i ilościowa towarów przyjmowanych i wydawanych z magazynu. Dbanie o prawidłową organizację i porządek w magazynie. Realizacja standardów i procedur obowiązujących w firmie.	\N	\N	\N	f	f	2025-10-31 14:32:33+01	2025-11-30 14:32:51+01	f	t	398	480	470	\N	\N	26
299	Magazynier - Sas oddział w Głogoczowie k. Krakowa	Spółka SAS zajmuje się dystrybucją najwyższej jakości produktów do przemysłu meblowego, działamy na polskim rynku nieprzerwanie od 1994 roku, aktualnie poszukujemy kandydatów do pracy w Głogoczowie na stanowisko magazyniera na dział z akcesoriami. Zadaniem zatrudnionej osoby będzie głównie przyjmowanie i wydawanie towaru. Zależy nam na osobach szukających stabilności i przewidywalności w pracy. Szukamy pracowników poważnie podchodzących do swoich obowiązków.	\N	\N	\N	f	f	2024-10-23 12:32:22+02	2025-12-13 12:42:00+01	f	t	398	481	471	\N	\N	26
300	Magazynier / kierowca w branży rolniczej	Firma Przedsiębiorstwo Handlowe „AGA” Anna Gałka o ugruntowanej pozycji na rynku działająca w branży rolniczej od 2000 roku poszukuje osoby na stanowisko magazynier/kierowca. Miejsce pracy: Raczyny. Opis stanowiska: przyjmowanie i wydawanie towaru, kompletacja zamówień i przygotowywanie towaru, rozładunek dostaw, okazjonalne dostarczanie towarów do klienta, dbałość o porządek i bezpieczeństwo w magazynie.	\N	\N	\N	f	f	2025-08-31 19:49:13+02	2025-12-13 11:24:23+01	f	t	398	482	472	\N	\N	26
301	Magazynier + bezpłatny transport	PPF Logo Express to dynamicznie rozwijająca się firma produkcyjna zajmująca się dekorowaniem szerokiej gamy produktów promocyjnych. Nasza siedziba znajduje się w nowoczesnym parku logistyczno-produkcyjnym w Robakowie k. Gądek. Wyjątkowa atmosfera pracy oparta na wzajemnym szacunku i wsparciu, a także nastawienie na ciągły rozwój i doskonalenie to wartości, na które stawiamy w naszym zespole. Jeśli chcesz do niego dołączyć, szukasz pewnej i stabilnej pracy oraz nowych wyzwań - APLIKUJ. Obecnie poszukujemy kandydatów na stanowisko MAGAZYNIER Miejsce pracy: Robakowo k. Gądek pod Poznaniem (bezpłatny dojazd ze Śremu, Gniezna, Środy Wielkopolskiej, Poznania, Wrześni, Kostrzyna, Kościana) Chcemy Ci zaoferować: • stałe, stabilne zatrudnienie w oparciu o umowę o pracę • wynagrodzenie 32 zł/h brutto plus nagroda uznaniowa • dla osób chętnych możliwość uzyskania dodatkowego wynagrodzenia dzięki pracy w nadgodzinach • dodatkową płatną przerwę w ciągu dnia pracy • atmosferę pracy zespołowej opartej na wzajemnym szacunku i wsparciu • możliwość rozwoju zawodowego i awansu wewnętrznego na inne stanowiska • dofinansowanie dojazdów do pracy lub bezpłatny transport pracowniczy • kartę MultiSport oraz opiekę medyczną Medicover • możliwość bezpłatnego szkolenia z obsługi wózków widłowych W zależności od działu, na który zostaniesz zatrudniony do Twoich obowiązków będzie należeć między innymi: • przygotowywanie i kompletacja zleceń (zobacz jak to wygląda: www.bitly.com/o-pfle-5) • załadunek i rozładunek towarów • praca ze skanerem • rozkładanie towaru na lokalizacje magazynowe • skanowanie paczek • przygotowywanie zleceń do wysyłki Od Ciebie oczekujemy: • prawo jazdy kategorii B lub uprawnienia UDT - warunek konieczny • uczciwości, sumienności i punktualności oraz zaangażowania w wykonywanie powierzonych zadań • umiejętności pracy w zespole • gotowości do pracy w systemie dwuzmianowym: 6:00-14:00, 14:00-22:00 • dyspozycyjności do pracy od poniedziałku do piątku i sporadycznie w soboty Dodatkowym atutem będzie: • doświadczenie w pracy na magazynie	\N	32.00	f	f	f	2021-08-25 12:23:52+02	2025-11-28 08:47:59+01	f	t	398	483	473	1	1	26
302	Magazynier - Operator wózka widłowego	Jesteśmy polską firmą z blisko 40-letnim doświadczeniem produkcyjnym, wytwarzającą oporęczowanie ze stali czarnej i nierdzewnej. Współpracujemy z klientami z sektora automotive, ale także z wieloma innymi. Obecnie do naszego zespołu poszukujemy kandydata na stanowisko: Magazynier - Operator wózka widłowego. Miejsce pracy: Poznań, ul. Wrzesińska. Osoba zatrudniona na tym stanowisku będzie odpowiedzialna min. za: obsługę wózka widłowego, załadunek i rozładunek detali, transportowanie półfabrykatów pomiędzy poszczególnymi działami produkcyjnymi w firmie, inne prace pomocnicze związane z prawidłowym funkcjonowaniem magazynu.	\N	\N	\N	f	f	2025-10-24 13:20:01+02	2025-11-23 12:20:02+01	f	t	398	484	474	\N	\N	26
303	Magazynier-Handlowiec	Praca na 1 zmianę w godzinach 8-16. Stabilne zatrudnienie w zgranym zespole. Umowa o pracę. Stabilne zatrudnienie w zgranym zespole. Przyjazna i miła atmosfera pracy. Możliwość zdobycia doświadczenia w branży pokryć dachowych i płyt warstwowych. Pracę w stałych godzinach – od 8:00 do 16:00. Uczciwe warunki zatrudnienia i jasne zasady współpracy. Obsługa magazynu, Przyjmowanie i wydawanie towaru, Formatowanie płyt i obróbek (cięcie płyt pod wymiar) Dbanie o porządek w miejscu pracy. Chęć do pracy i zaangażowanie, Uczciwość i odpowiedzialność w wykonywaniu powierzonych zadań, Wysoka kultura osobista i umiejętność współpracy w zespole, Uprawnienia na wózek widłowy, Gotowość do nauki i zdobywania nowych umiejętności. Nie wymagamy doświadczenia – wszystkiego Cię nauczymy! Jeśli szukasz pracy w stabilnej firmie z przyjaznym zespołem i zależy Ci na jasnych zasadach oraz dobrej atmosferze, wyślij swoje CV. Dołącz do nas i rozwijaj się razem z nami! Skontaktujemy się z wybranymi kandydatami. Firma zajmuję się sprzedażą płyty warstwowych, płyty izolacyjnych PIR,XPS oraz pokryć dachowych.	\N	\N	\N	f	f	2025-11-07 12:38:36+01	2025-12-07 12:48:46+01	f	t	398	485	475	\N	\N	26
304	Praca w MediaExpert - kompletacja / pakowanie towaru	Osoby do kompletacji/pakowania towaru. Zatrudnienie na podstawie umowy zlecenia. Miejsce: Łódź, ul. Zakładowa 90/92. Zapewnimy obiad z dofinansowaniem. Twoje zadania: kompletacja/pakowanie towaru, obsługa skanera, proste zadania magazynowe. Nasze oczekiwania: sumienność i dyspozycyjność min. 170 h miesięcznie. Nie oczekujemy doświadczenia! Mamy na pokładzie Media Expertów od tej pracy, którzy wszystkiego Cię nauczą.	34.00	40.00	f	f	f	2025-11-07 14:14:18+01	2025-12-07 14:14:18+01	f	t	398	326	477	1	1	26
305	Magazynier/Sprzedawca	Praca w drogerii Hebe na stanowisku Magazynier-Sprzedawca. Praca polega na przyjmowaniu i rozpakowywaniu dostaw (2 razy w tygodniu, 3-6 palet, pojemniki do 12kg), kompletowaniu zamówień ze sklepu internetowego, utrzymywaniu porządku w magazynie, zmianach w planogramach oraz obsłudze kasy fiskalnej w razie potrzeby. Oferta zawiera umowę o pracę bez okresu próbnego, kartę Multisport PLUS lub Medicover Sport, kafeterię benefitów, wynagrodzenie podstawowe + premię oraz możliwość szybkiego awansu.	\N	\N	\N	f	f	2025-10-31 10:00:04+01	2025-11-30 10:00:04+01	f	t	398	488	478	\N	9	26
272	Pracownik Magazynowy	Poszukujemy pracownika magazynowego do przyjmowania i rozładunku dostaw, kompletacji zamówień, przygotowywania towaru do wysyłki, dbania o porządek w magazynie oraz podstawowej obsługi dokumentacji magazynowej. Oferujemy transport pracowniczy z Poznania, ciepły obiad pracowniczy, odzież roboczą i jej pranie oraz przyjazną atmosferę w małym, zgranym zespole.	4100.00	4100.00	f	f	f	2025-11-10 19:13:15+01	2025-12-10 19:29:32+01	f	t	398	489	479	1	9	26
315	Magazynier - E-commerce - Kierowca	Firma zajmująca się sprzedażą pasz, dodatków paszowych, surowców do produkcji pasz do żywienia zwierząt , obrotem materiałem siewnym oraz skupem zbóż poszukuje osoby na stanowisko: Magazynier - E-commerce - obsługa sprzedaży internetowej. Dostarczanie, wydawanie i przyjęcie towarów handlowych tj pasze, dodatki, surowce do produkcji pasz, obsługa klientów, E-commerce: obsługa sprzedaży Internetowej. Oferujemy przygotowanie do pracy, przeszkolenie do stanowiska, szkolenia podnoszące kwalifikacje, rozwój zawodowy, stabilność zatrudnienia.	4000.00	5400.00	f	f	f	2025-11-12 10:54:24+01	2025-12-12 11:03:00+01	f	t	398	507	496	1	9	26
306	Magazynier - Koszalin	Agata S.A. - Naszą pasją są wnętrza. Oferując szeroki wybór mebli i artykułów wyposażenia wnętrz, pragniemy stać się częścią przestrzeni, która wyraża charakter, styl i wartości naszych Klientów. W ten sposób pomagamy tworzyć niepowtarzalną atmosferę domu.\n\nDo głównych obowiązków na tym stanowisku będzie należało:\nRozładunek dostaw i przyjęcie towaru;\nPrzygotowanie oraz wydanie towarów;\nDbałość o powierzony towar i mienie firmy.\n\nOd kandydatów oczekujemy:\nElastyczności czasu pracy;\nGotowości do ciężkiej pracy fizycznej;\nDoświadczenia w pracy na magazynie;\nUmiejętności pracy w zespole.\n\nZatrudnionym oferujemy:\nMożliwość zrobienia kursu na wózki widłowe;\nStabilne warunki zatrudnienia;\nAtrakcyjne wynagrodzenie;\nPremię retencyjną;\nMożliwość rozwoju;\nSystem wdrożenia pracownika;\nPrywatna opieka medyczna Medicover;\nPakiet sportowy Medicover Sport;\nGrupowe ubezpieczenie zdrowotne;\nWczasy pod gruszą;\nBonus Bożonarodzeniowy;\nZniżki pracownicze;\nPrezent urodzinowy.	\N	\N	\N	f	f	2025-10-31 12:56:10+01	2025-11-30 12:56:10+01	f	t	398	417	482	\N	\N	26
307	Praca od zaraz dla Magazynierów. Dużo wakatów!	Jeśli poszukujesz pracy od zaraz, posiadasz już pierwsze doświadczenie w pracach magazynowych i jesteś zdeterminowany do pracy - mamy dla Ciebie ofertę. Aktualnie poszukujemy osób do pracy na stałe lub sezonowo. Decyzja należy do Ciebie. Miejsce pracy: Łódź ul. Jędrzejowska Zakres obowiązków: kompletowanie zamówień zgodnie z listą (pickowanie towaru); przyjmowanie i wydawanie towaru; rozmieszczanie produktów w magazynie; obsługa skanera i komputera; współpraca z zespołem magazynowym. Oczekujemy: doświadczenia w pracy na podobnym stanowisku (mile widziane); gotowości do pracy w systemie 3-zmianowym (godz. 6-14, 14-22 i 22-6) od poniedziałku do piątku; chęci, odpowiedzialności oraz zaangażowania w obowiązki; możliwości rozpoczęcia od zaraz! Oferujemy: zatrudnienie w ramach umowy o pracę lub zlecenie (decyzja należy do Ciebie); wynagrodzenie: 4920zł brutto miesięcznie (umowa o pracę) lub 34 zł/h brutto (umowa zlecenie); system premiowy (do 1200 zł miesięcznie); prywatną opieka medyczna; dofinansowanie do karty sportowej oraz posiłków pracowniczych możliwość przystąpienia do ubezpieczenia grupowego; darmowe przejazdy pracownicze Łodzi oraz (m.in. z Andrespola, Justynowa, Gałkowa, Brzezin i Koluszek, Pabianice, Zgierz, Ozorków). Jeśli jesteś zainteresowany konieczniej wyślij do nas swoje CV lub zadzwoń/wyślij SMS o treści praca magazynier – 735&nbsp;924&nbsp;008.	34.00	4920.00	f	f	f	2025-10-24 15:13:47+02	2025-11-23 14:13:47+01	f	t	398	493	483	1	9	26
308	Magazynier - operator wózka jezdniowego ABLER Toruń	Ogólnopolski lider i dostawca materiałów dla meblarstwa oraz wykończenia wnętrz w związku z dynamicznym rozwojem poszukuje osoby na stanowisko Magazynier - operator wózka jezdniowego (miejsce pracy: Toruń). Opis stanowiska: załadunki i rozładunki towarów dostarczanych i wysyłanych z firmy, obsługa klientów, wydawanie towaru z magazynu, przygotowywanie towarów do wysyłki, sprawdzanie zgodności dostaw z dokumentami, obsługa sprzętu magazynowego, uczestniczenie i rozliczanie wyników inwentaryzacji magazynowych, współpraca z działem sprzedaży, dbanie i utrzymywanie porządku w magazynie, realizacja dostaw do klientów. Oczekiwania: doświadczenie w pracy na podobnym stanowisku, uprawnienia na wózki jezdniowe (UDT) oraz doświadczenie w ich obsłudze - warunek konieczny, dokładność, umiejętność pracy w zespole oraz zaangażowanie, uczciwość, odpowiedzialność, wysoka kultura osobista. Oferujemy: zatrudnienie na podstawie umowy o pracę, na pełen etat, stabilną pracę w firmie o ugruntowanej pozycji rynkowej, wynagrodzenie odpowiadające efektom pracy i poziomowi umiejętności, prywatną opiekę medyczną, dofinansowanie do kart sportowych.	\N	\N	\N	f	f	2025-03-31 17:40:23+02	2025-12-13 14:49:08+01	f	t	398	494	484	\N	\N	26
309	Niemcy - praca na stanowisku magazyniera - bez doświadczenia!	Szukamy komisjonerów do pracy w nowoczesnym magazynie w Niemczech! Pracę możesz rozpocząć od zaraz! Praca komisjonera polega na kompletowaniu paczek na podstawie zleceń. Czasami zdarzają się cięższe paczki (do 30 kg), dlatego szukamy osób, które cieszą się dobrą kondycją fizyczną. Mile widziana znajomość j. angielskiego na poziomie komunikatywnym.	14.53	14.96	f	f	f	2025-10-20 08:21:37+02	2025-11-19 07:21:37+01	f	t	398	496	485	2	1	26
310	Magazynier z UDT | Bez doświadczenia | Od zaraz	Zostań częścią świata automotive! Dołącz do naszego zespołu i zdobądź cenne doświadczenie u liderów branży razem z AQS Poland!\n\nLokalizacja: Wrocław, Stargardzka\n\nOferujemy:\n\nUmowę zlecenie\n\nAtrakcyjną stawkę od 33 zł/h brutto\n\nDodatek za pracę w godzinach nocnych\n\nDla chętnych - możliwość nadgodzin i pracy w soboty (dodatkowo płatne)\n\nPracę w systemie trzyzmianowym - od PN do PT\n\nBonus za polecenie nowych pracowników\n\nPremię kwartalną do 600zł\n\nPrywatną opiekę medyczną\n\nKartę Multisport\n\nWymagania:\n\nUprawnienia na wózki widłowe\n\nDyspozycyjność do pracy w systemie trzyzmianowym\n\nOdpowiedzialność za wykonywaną pracę\n\nMile widziane:\n\nStatus ucznia/studenta\n\nDoświadczenie w pracy na wózkach czołowych i bocznych\n\nObowiązki:\n\nZaopatrzenie stanowisk produkcyjnych lub montażowych w niezbędne materiały\n\nRealizowanie prac magazynowych zgodnie z obowiązującymi procedurami i standardami\n\nPrzestrzeganie zasad BHP\n\nDlaczego warto wybrać naszą firmę?\n\nDlatego, że jesteśmy dynamicznym zespołem pełnym energii, otwartym na nowe wyzwania i gotowym do rozwoju razem z Tobą!\n\nJesteś zainteresowany? Nie zwlekaj! Wyślij swoją aplikację już teraz, a my odezwiemy się do Ciebie tak szybko, jak to możliwe!\n\nInformujemy, że będziemy kontaktować się tylko z wybranymi kandydatami!\n\nProsimy o zawarcie w CV klauzuli:\n\nWyrażam zgodę na przetwarzanie moich danych osobowych dla potrzeb niezbędnych do realizacji procesu rekrutacji (zgodnie z ustawą z dnia 10 maja 2018 roku o ochronie danych osobowych (Dz. Ustaw z 2018, poz. 1000) oraz zgodnie z Rozporządzeniem Parlamentu Europejskiego i Rady (UE) 2016/679 z dnia 27 kwietnia 2016 r. w sprawie ochrony osób fizycznych w związku z przetwarzaniem danych osobowych i w sprawie swobodnego przepływu takich danych oraz uchylenia dyrektywy 95/46/WE (RODO).\n\nJeśli chcesz abyśmy zatrzymali Twoje CV, dopisz:\n\nWyrażam zgodę na przetwarzanie moich danych osobowych w zakresie przyszłych procesów rekrutacyjnych.	33.00	\N	f	f	f	2025-11-07 20:44:40+01	2025-12-07 20:44:41+01	f	t	398	497	486	1	1	26
311	Pracownik magazynu | Wysoka stawka i grafik bez nocnych zmian	Dołącz do zespołu w nowoczesnym centrum logistycznym w Teresinie, gdzie kompletujemy delikatną bieliznę znanej i lubianej marki. Nie musisz dźwigać ciężkich paczek ani mieć doświadczenia – wszystkiego nauczymy Cię na miejscu!	38.00	\N	f	f	f	2025-10-24 11:11:04+02	2025-11-23 10:11:05+01	f	t	398	499	488	1	1	26
313	Magazynier - obsługa wózka jezdniowego	Firma produkcyjna z Wyszkowa (07-200), dostawca części dla przemysłu motoryzacyjnego, budowlanego i rolniczego. Zakres obowiązków: przyjmowanie, rozładunek i wydawanie towaru z magazynu, kompletowanie zamówień zgodnie z dokumentacją magazynową, prowadzenie ewidencji stanów magazynowych w systemie, kontrola ilościowa i jakościowa dostaw, utrzymywanie porządku i bezpieczeństwa na stanowisku pracy, obsługa wózków jezdniowych (widłowych).	\N	\N	\N	f	f	2025-11-07 11:55:14+01	2025-12-07 12:05:08+01	f	t	398	503	492	\N	\N	26
282	Kontroler Stanów Magazynowych / Specjalista ds. Inwentaryzacji	Schronisko Bukowina to sklep e-commerce, który stale się rozwija. Sprzedajemy produkty spożywcze i chemię gospodarczą, a nasze zaplecze magazynowe w Nowym Targu to serce całej firmy. Szukamy dokładnej i odpowiedzialnej osoby, która będzie dbać o zgodność stanów magazynowych, kontrolę pracy magazynierów i inwentaryzacje towaru.	\N	\N	\N	f	f	2025-11-07 11:34:54+01	2025-12-07 11:37:00+01	f	t	398	446	494	\N	\N	26
314	Magazynier - Kierowca	Zapewnienie sprawnego przepływu towarów w procesie cross-dockingowym oraz bezpiecznej realizacji dostaw do klientów od rozładunku, przez sortowanie, po transport i obsługę zwrotów. Do zadań należy: Rozładunek dostaw z centrali (meble tapicerowane, luzem), układanie mebli na paletach, foliowanie, etykietowanie, sortowanie towarów według tras i przygotowanie do załadunku, załadunek busów dostawczych (15 m³) oraz kontrola kompletności przesyłek, realizacja dostaw do klientów (kierowca kurier) w razie potrzeby lub zastępstwa, obsługa zwrotów odbiór, weryfikacja i przekazanie do strefy kontrolnej, praca z terminalem mobilnym (Zebra) skanowanie, potwierdzanie przyjęć i wydań, wprowadzanie danych w aplikacji magazynowej, utrzymanie porządku, bezpieczeństwa i zgodności z BHP, codzienne raportowanie wykonanych zadań do lidera magazynu.	\N	\N	\N	f	f	2025-11-07 13:02:22+01	2025-12-07 13:03:44+01	f	t	398	506	495	\N	\N	26
316	Magazynier	Firma produkcyjna poszukuje kandydatów na stanowisko Magazyniera. Praca w Robakowie k. Gądek pod Poznaniem. Do obowiązków należy przygotowywanie i kompletacja zleceń, załadunek i rozładunek towarów, praca ze skanerem, rozkładanie towaru na lokalizacje magazynowe, skanowanie paczek, przygotowywanie zleceń do wysyłki.	\N	\N	\N	f	f	2021-05-07 13:29:12+02	2025-11-28 08:48:09+01	f	t	398	483	497	\N	\N	26
317	Magazynier / Serwisant	Axxo Fitness, importer sprzętu sportowego i fitness, założony w 2011r., dystrybutor marek Axxo, Spirit Fitness, Xterra Fitness, poszukuje pracownika Magazynu i Serwisu. Zakres obowiązków: przygotowywanie paczek i palet z towarem do wysyłki, obsługa wózka widłowego, dokumentacja wysyłkowa, diagnostyka sprzętu mechanicznego, możliwy szybki awans na montera i serwisanta. Wymagania: Doświadczenie w pracy na magazynie, Prawo jazdy kat. B, Doświadczenie i uprawnienia obsługi wózka widłowego, Komunikatywność i umiejętność pracy w zespole, Sumienność, odpowiedzialność i samodzielność w działaniu, Podstawowe umiejętności mechaniczne i techniczne będą dodatkowym atutem. Oferujemy: szkolenie z zakresu wymaganej pracy, stabilne zatrudnienie na podstawie umowy o pracę, atrakcyjne wynagrodzenie adekwatne do doświadczenia, z możliwością podwyżki i awansu, możliwość podnoszenia kwalifikacji w rozwijającej się branży, przyjazną atmosferę pracy w rozwijającej się firmie. Typowy dzień w Axxo zaczyna się o 6 rano. Przygotowujemy dokumentację i towar do wysyłki. Stawiamy na jakość a nie tempo pracy w myśl zasady "powoli ale dokładnie". Jesteśmy otwarci na ludzi młodych i ambitnych, bez doświadczenia. Aplikacje tylko z załączonym CV.	\N	\N	\N	f	f	2025-10-31 12:09:16+01	2025-11-30 12:11:52+01	f	t	398	509	498	\N	\N	26
318	Prace Magazynowe - 1/2 etatu	Wsparcie procesu załadunku i rozładunku, zapewnienie prawidłowego składowania towaru w magazynie oraz odpowiedniego rozmieszczenia towarów, poprawne wydanie towarów z magazynu, utrzymanie porządku w miejscu pracy.	\N	\N	\N	f	f	2025-10-24 13:50:33+02	2025-11-23 12:50:33+01	f	t	398	347	500	\N	\N	26
319	Magazynier - kompletacja elementów aluminiowych i drewnianych	W związku z ciągłym rozwojem poszukujemy osoby do pracy w naszym magazynie. Osoba na tym stanowisku będzie odpowiedzialna za: Kompletację zadaszeń tarasów i ogrodów zimowych oraz podobnych produktów, przygotowań do wysyłki, kontrolę stanu produktów na składzie, kontrola i dbanie o właściwy stan magazynowy. Praca w pełnym wymiarze godzin. Praca stała przez cały rok. Propozycję proszę wysyłać poprzez napisz wiadomość. Koniecznie proszę również napisać kilka słów o swoim doświadczeniu. Miejsce pracy Radzymin, Weteranów 247	\N	\N	\N	f	f	2025-10-31 08:47:33+01	2025-12-04 12:53:10+01	f	t	398	512	501	\N	\N	26
320	Pracownik magazynowy	Aktualnie dla Naszego Klienta- Lidera w branży logistycznej poszukujemy Pracownika magazynu! Nie wymagamy doświadczenia ani żadnych certyfikatów– liczy się Twoja chęć do pracy! Zakres obowiązków: kompletowanie zamówień, rozładunek towarów, prace porządkowe na magazynie. Wymagania: gotowość do pracy w godzinach nocnych, rzetelność i zaangażowanie, umiejętność pracy w zespole, komunikatywny język polski bądź angielski. Oferujemy: stabilne zatrudnienie, szkolenie wprowadzające, możliwość rozwoju i podnoszenia kwalifikacji, bezpłatny dojazd do firmy, pomoc w zakwaterowaniu, przyjazną atmosferę w pracy. Zainteresowane osoby prosimy o kontakt telefoniczny pod nr 79*****47 lub przesłanie CV poprzez portal OLX. Aplikuj jeszcze dziś! Oferta dotyczy pracy tymczasowej (z możliwością długotrwałej współpracy).	\N	\N	\N	f	f	2025-11-07 14:32:00+01	2025-12-07 14:32:50+01	f	t	398	513	502	\N	\N	26
321	Magazynier w MediaExpert - umowa o pracę	Jesteśmy Expertem, Media Expertem! Rządzimy na rynku elektro w Polsce! Ale to nie koniec naszych planów, dlatego szukamy: Magazynierów Lokalizacja: Łódź, ul. Jędrzejowska 43a Sprawdź jedną z najlepszych ofert w Łodzi! Chodź i zobacz swoje miejsce pracy, a potem podpisz umowę o pracę! Twoi koledzy już tu są! A jeśli nie, to poznasz nowych! Zapewnimy Ci: zatrudnienie na podstawie umowy o pracę jasny system wynagradzania – do Twojego wynagrodzenia zasadniczego dodajemy premię frekwencyjną i konkursową/wydajnościową, dodatek nocny podwyższony do 40% (jeśli występują godziny nocne) dodatkowy dodatek za dyspozycyjność w IVQ - XI 300 zł brutto, XII 1500 zł brutto dodatek za doświadczenie od 100 do 300 zł brutto bezpłatny transport pracowniczy na terenie Łodzi kartę MultiSport z dofinansowaniem prywatną opiekę medyczną Medicover z dofinansowaniem opiekę Trenerów, którzy wdrożą Cię do pracy stołówkę pracowniczą i obiady z dofinansowaniem (dwie wersje: mięsna i wegetariańska) program poleceń pracowniczych bezpłatną kawę i herbatę z automatu Baby Expert, czyli wyprawkę dla Twojego nowonarodzonego dziecka ubezpieczenie grupowe dla Ciebie i Twojej rodziny  na preferencyjnych warunkach zniżki na zakup sprzętu w naszych elektromarketach możliwość awansu pomoc i opiekę fundacji Mediaexpert „Włącz się” strefę relaksu Twoje zadania w zależności od obszaru, na którym zostaniesz zatrudniony/a: przyjęcie oraz relokacja towaru w magazynie kompletacja i załadunek towaru pakowanie zamówień praca ze skanerem prowadzenie dokumentacji magazynowej wsparcie pozostałych procesów magazynowych udział w inwentaryzacji towaru w magazynie Od Ciebie oczekujemy: dyspozycyjności do pracy w systemie zmianowym dokładności i sumienności w wykonywaniu zadań zaangażowania w powierzoną pracę przestrzegania zasad i procedur obowiązujących w firmie To co, jesteśmy umówieni na wspólną przyszłość?	\N	\N	\N	f	f	2025-11-07 14:31:01+01	2025-12-07 14:31:01+01	f	t	398	326	503	\N	\N	26
322	Operator Wózka Widłowego	Szukasz stabilnej pracy i cenisz sobie wolny czas? Dołącz do naszego zespołu w Legnicy! Poszukujemy zarówno doświadczonych operatorów, jak i osób po egzaminie UDT, gotowych do podjęcia pracy i nauki.	36.00	38.00	f	f	f	2025-10-31 09:18:08+01	2025-11-30 09:18:08+01	f	t	398	418	504	1	1	26
323	Magazynier - Kierowca kat. C+E Media Expert - praca w niedzielę	Media Expert to: Lider branży elektromarketów w Polsce (mamy ich ponad 600), Najlepszy omnichannel w Polsce, 3 sklepy internetowe – ponad 20 milionów Klientów rocznie, Zautomatyzowana i nowoczesna logistyka (6 Centrów Dystrybucji w Łodzi i ok. 30 centrów przeładunkowych). Dołącz do nas jako: Magazynier - Kierowca kat. C+E Twoje zadania: Podstawianie naczep na placu i dbanie o ich prawidłowe rozmieszczenie. Przestrzeganie przepisów BHP oraz troska o powierzony sprzęt i pojazd. Szukamy właśnie Ciebie, jeśli: Posiadasz czynne prawo jazdy kat. C+E. Cechuje Cię odpowiedzialność, sumienność i zaangażowanie. Jesteś dyspozycyjny do pracy w niedzielę w godzinach 8:00-14:00 Zapewnimy Ci: Prywatną opiekę medyczną z dofinansowaniem, Kartę sportową z dofinansowaniem, Ubezpieczenie na życie dla Ciebie i Twojej rodziny, Zniżki na zakupy w Media Expert, Program Poleceń Pracowniczych, Bony świąteczne, Wsparcie Fundacji Mediaexpert „Włącz się” dla Ciebie i Twoich bliskich, Baby Expert – wyprawkę dla Twojego nowo narodzonego dziecka To co, jesteśmy umówieni na wspólną przyszłość?	\N	\N	\N	f	f	2025-10-31 14:16:55+01	2025-11-30 14:16:56+01	f	t	398	326	505	\N	\N	40
324	Pracownik magazynu	Dla naszego Klienta, producenta profili PVC, poszukujemy pracowników magazynu. Darmowy transport do/z pracy, premia za frekwencję oraz premia za wydajność. Zatrudnienie w oparciu o umowę o pracę tymczasową. Możliwość pracy w nadgodzinach. Praca w nowej hali oraz przyjazne zaplecze socjalne. Kompletowanie towaru na podstawie listy wydań, weryfikacja zgodności przyjmowanych i wydawanych towarów z zamówieniami, porządkowanie przestrzeni magazynowej, weryfikacja stanów magazynowych, wykonywanie czynności związanych z codziennym funkcjonowaniem magazynu. Mile widziane doświadczenie w pracy na magazynie. Oferta dotyczy pracy tymczasowej (z możliwością długotrwałej współpracy).	5000.00	5000.00	f	\N	\N	2025-10-31 11:49:48+01	2025-11-30 11:49:48+01	f	t	398	518	506	1	9	26
325	Niemcy - praca na stanowisku magazyniera - bez doświadczenia!	Szukamy komisjonerów do pracy w nowoczesnym magazynie w Niemczech! Pracę możesz rozpocząć od zaraz! Stawka: 14,53 EUR brutto/h, nie wymagamy znajomości języka i uprawnień! Praca komisjonera polega na kompletowaniu paczek na podstawie zleceń. Czasami zdarzają się cięższe paczki (do 30 kg), dlatego szukamy osób, które cieszą się dobrą kondycją fizyczną. Do Twoich zadań należeć będzie: kompletowanie zamówień przy użyciu prostego i wygodnego w obsłudze skanera ręcznego, pomoc przy pakowaniu przesyłek, z czasem również obsługa wózka EPT – przeszkolenie oraz uprawnienia zrobisz na miejscu. Praca odbywa się w systemie 2-zmianowym. Wymagania: doświadczenie na podobnym stanowisku, czynne prawo jazdy kat. B Mile widziana znajomość j. angielskiego na poziomie komunikatywnym. Atuty: stawka 14,53 euro brutto/h, od stycznia 14,96 euro brutto/h! możesz zacząć pracę bez znajomości języka! bonus za dowożenie kolegów i koleżanek do pracy 150€ brutto/msc możesz skorzystać z oferowanego przez nas zakwaterowania zapewniamy legalne zatrudnienie na podstawie umowy o pracę tymczasową oraz polskie ubezpieczenie na terenie Niemiec pomożemy Ci zorganizować transport do Niemiec oraz zapewniamy go na miejscu będziesz pod stałą opieką polskojęzycznego opiekuna projektu darmowy dostęp do platformy z kursami językowymi po 30 dniach zatrudnienia Dzięki polskiej umowie: odkładasz środki na emeryturę w Polsce zachowujesz ciągłość ubezpieczenia nie tracisz prawa do dodatków socjalnych i zasiłku dla bezrobotnych Jak wygląda rekrutacja? możesz aplikować o pracę tak, jak Ci wygodnie: przez formularz na stronie, telefonicznie, mailem (rekrutacja(a)contrain.pl) lub przez Messenger oddzwonimy do Ciebie, nasza rekruterka/rekruter pomoże Ci wybrać ofertę i opowie o jej szczegółach umówimy Cię na badania lekarskie i podpiszemy umowę i już – możesz zarabiać! W Contrain mamy 27 lat doświadczenia w zatrudnianiu pracowników tymczasowych, z sukcesami potwierdzonymi nie tylko wyróżnieniami jak „Agencja zatrudnienia przyjazna pracownikom” czy Diamenty Forbesa, ale przede wszystkim opiniami zatrudnianych przez nas osób. Chcesz wiedzieć, co mówią o nas pracownicy?	14.53	14.96	f	f	f	2025-10-16 09:04:52+02	2025-11-15 08:04:52+01	f	t	398	496	507	2	1	26
326	Magazynier Intar Sp. z o.o. w Sycowie	INTAR Sp. z o.o. jest firmą handlowo – usługową oferującą materiały płytowe, akcesoria do produkcji mebli oraz materiały wykończeniowe, a także usługi cięcia i okleinowania płyt. W związku z rozwojem poszukuje kandydatów na stanowisko Magazyniera w Sycowie. Do obowiązków należy przyjmowanie i rozładunek towarów na magazyn, kompletowanie i przygotowywanie zamówień do wysyłki, obsługa wózków widłowych oraz innych urządzeń magazynowych, dbanie o porządek i organizację przestrzeni magazynowej, prowadzenie dokumentacji magazynowej oraz raportowanie stanów magazynowych, współpraca z działem logistyki oraz produkcji.	\N	\N	\N	\N	\N	2025-11-12 14:50:27+01	2025-12-12 14:50:59+01	f	f	398	521	508	\N	\N	26
334	Magazynier	Proste prace fizyczne na terenie magazynu, Układanie, pakowanie i konfekcjonowanie towaru, Kontrola zamówień, Inne prace pomocnicze.	\N	\N	\N	\N	\N	2025-10-31 13:14:22+01	2025-11-30 13:14:23+01	f	t	398	533	530	\N	\N	26
327	Młodszy Magazynier	Do naszego zespołu magazynowego poszukujemy zaangażowanej i odpowiedzialnej osoby na stanowisko Młodszego Magazyniera. Jeśli lubisz pracę fizyczną, cenisz sobie dobrą organizację, a przy tym dobrze odnajdujesz się w zespole – zapraszamy do aplikowania!\n\nZakres obowiązków:\n\nzaładunek i rozładunek samochodów dostawczych oraz ciężarowych\nutrzymanie porządku na stanowisku pracy\ndostarczanie towaru do klienta samochodem typu bus (do 3,5 t)\nobsługa wózków widłowych\nprzyjmowanie, kompletowanie i wydawanie towarów zgodnie z obowiązującymi zasadami\nkontrola ilościowa i jakościowa towarów przy przyjęciu i wydaniu\ndbałość o powierzony sprzęt (wózek widłowy, skaner, samochód dostawczy)\nprzestrzeganie zasad BHP\n\nWymagania:\n\nuprawnienia do obsługi wózków widłowych – UDT (wymóg konieczny)\nprawo jazdy kat. B (warunek konieczny)\ndoświadczenie w pracy na magazynie – mile widziane\ndobra organizacja pracy i odpowiedzialność\numiejętność pracy w zespole\npodstawowa znajomość obsługi komputera\nwykształcenie średnie – mile widziane	\N	\N	\N	f	f	2025-10-24 10:03:58+02	2025-11-23 09:03:59+01	f	t	398	523	509	\N	\N	26
328	Magazynier	Zasil nasz zespół Magazynierów w Pabianicach! Możliwość wyboru godzin wykonywania zlecenia, w ramach świadczenia usług przez Steam Workforce. Dostęp do rekrutacji wewnętrznych z furtką do specjalistycznych ofert z prestiżowymi szkoleniami. Możliwość rozwoju ścieżki zawodowej. Nowoczesna aplikacja, dzięki której szybko i wygodnie załatwisz formalności w telefonie. Wspieramy rozwój Młodych Talentów w ramach programu stypendialnego. Obsługa wózka widłowego.	\N	\N	\N	f	f	2025-11-07 10:44:49+01	2025-12-07 10:44:50+01	f	t	398	431	510	\N	\N	26
277	Pracownik Magazynowy Amazon Sady	Pracownik Magazynowy Amazon Sady. Ostatnia szansa na zatrudnienie w tym roku! Atrakcyjna podstawa wynagrodzenia, comiesięczna premia, posiłki i dojazd do pracy, a także liczne benefity. Pracuj wedle zagranicznych standardów! Zakres obowiązków, to proste prace magazynowe, m.in. kompletowanie zamówień, pobieranie towarów, wykładanie produktów na półki, pakowanie.	\N	36.22	f	f	f	2025-11-07 15:45:40+01	2025-12-07 15:45:41+01	f	t	398	423	511	1	1	26
329	Magazynier FedEx	Magazynier. Miejsce pracy: Czechowice-Dziedzice, ul. Cicha 14. Wymiar etatu: 1/2 etatu. Godziny pracy: 15:00-19:00 i/lub 15:30-19:30 i/ lub 6:00-10:00 i/lub 7:00-11:00. Zakres obowiązków: rozładunek i załadunek przesyłek, weryfikacja parametrów przesyłek, praca ze skanerem, sortowanie na kierunki doręczenia, dbanie o powierzony sprzęt firmowy: urządzenia, maszyny, sporządzanie raportów. Profil kandydata: wykształcenie zawodowe/średnie, dokładność, sumienność i rzetelność w wykonywaniu obowiązków, odpowiedzialność i zaangażowanie, mile widziane doświadczenie w pracy na magazynie, mile widziane uprawnienia na wózki widłowe UDT.	\N	\N	\N	f	f	2025-10-24 09:53:39+02	2025-11-23 08:53:39+01	f	t	398	527	513	\N	\N	26
330	Magazynier	Jesteśmy jedną z dziesięciu największych agencji pracy i doradztwa personalnego w Polsce, należącą do międzynarodowej Grupy Synergie – globalnego lidera w obszarze HR. Od 2006 roku wspieramy kandydatów w rozwoju kariery, oferując sprawdzone oferty pracy w kraju i za granicą. Zatrudniamy kilka tysięcy osób rocznie, a dzięki naszej wiedzy, doświadczeniu i indywidualnemu podejściu pomagamy znaleźć zatrudnienie dopasowane do kwalifikacji i oczekiwań. Z naszych usług korzystają zarówno duże międzynarodowe koncerny, jak i mniejsze przedsiębiorstwa, a my zawsze koncentrujemy się na ludziach, którzy za nimi stoją oferując m.in. transparentne warunki i opiekę naszego konsultanta na każdym etapie współpracy. Interkadra by Synergie – Zaufaj specjalistom od rekrutacji! Twoja kariera zaczyna się tutaj. Do Twoich obowiązków będzie należało: obsługa skanera ręcznego, kompletowanie zamówień (produkty kosmetyczne i odżywki proteinowe), dbanie o dokładność i czystość przy kompletowaniu zamówień.	4970.00	5715.50	f	f	f	2025-11-07 09:59:18+01	2025-12-07 09:59:19+01	f	t	398	528	514	1	9	26
331	Praca w magazynie | Kompletacja zamówień | 2000zł premii | 30,50zł/h	Do 2000zł premii za polecenie pracownika! Mamy Państwu do zaproponowania pracę na magazynie przy kompletowaniu zamówień. Zapewniamy darmowy transport z wielu przystanków we Wrocławiu. Kompletujemy zamówienia za pomocą skanera, najpierw zbierając produkty z półek do wózka a później pakując je do kartonów na wózku. Oferujemy darmowy transport z różnych lokalizacji we Wrocławiu, automaty z darmową kawą, herbatą, barszczem i czekoladą, elastyczny grafik, możliwość pracy od poniedziałku do niedzieli w systemie zmianowym: 6 - 14, 14 - 22. Po wdrożeniu możliwe również zmiany nocne, premię frekwencyjną w wysokości do 2000zł, bonus do 2000zł za polecenie pracownika, 30,50zł brutto/h, wynagrodzenie na czas, pełne szkolenie i wdrożenie do pracy. Nie wymagamy doświadczenia. Liczy się dla nas elastyczność i chęć do pracy. Oferujemy możliwość pracy stałej lub dorywczej. Mile widziana dyspozycyjność powyżej 4 dni w tygodniu. Rozpoczęcie pracy możliwe od zaraz.	\N	\N	f	f	f	2025-09-01 07:13:43+02	2025-12-07 11:29:29+01	f	t	398	529	515	\N	1	26
332	Pracownik produkcji - jedna zmiana: obróbka aluminium, montaż, magazyn	Jesteśmy częścią międzynarodowej grupy "Alutec KK" z główną siedzibą w Czechach, jedynym i oficjalnym dostawcą konstrukcyjnych systemów aluminiowych w Polsce. Jako polska firma z polskim kapitałem, z siedzibą w Jasinie koło Swarzędza, działamy na polskim i częściowo europejskim rynku od 2012 roku. Jako przedstawiciel grupy ALUTEC KK dostarczamy modułowy system profili aluminiowych do budowy nowoczesnych konstrukcji przemysłowych. Współpracujemy z największymi w Polsce producentami maszyn, ale i również z Klientami końcowymi z wielu gałęzi przemysłu. Poszukujemy pracownika produkcji do naszego zespołu! Jeśli masz doświadczenie w pracy na produkcji ta oferta jest dla Ciebie! Zakres obowiązków: Prace magazynowe – kompletacja zamówień Obsługa prostych urządzeń do obróbki metalu (wiertarka stołowa, piła tarczowa, elektronarzędzia) Montaż konstrukcji z profili aluminiowych Pakowanie zamówień do wysyłek Kontrola jakości produktu Prace porządkowe Dbanie o porządek na stanowisku pracy Przestrzeganie zasad BHP Wymagania: Umiejętność obsługi elektronarzędzi Dokładność i zaangażowanie w wykonywane obowiązki Mile widziane doświadczenie na podobnym stanowisku Umiejętność pracy w zespole Sumienność i dokładność - praca z dużą precyzją oraz dbałość o aspekty wizualne wytwarzanych produktów Zaangażowanie i chęć do pracy Podstawowa znajomość rysunku technicznego. Oferujemy: Stabilne zatrudnienie w firmie o ugruntowanej pozycji Możliwość rozwoju i zdobycia nowych umiejętności Przyjazną atmosferę w zgranym zespole Niezbędne narzędzia do pracy Dołącz do nas już dziś!	\N	\N	\N	f	f	2025-11-07 07:45:23+01	2025-12-07 07:49:17+01	f	t	398	530	516	\N	\N	26
333	Magazynier Gdańsk Kowale	Firma Trident BMC świadczy usługi inżynieryjne, produkcyjne i konserwacyjne dla armatorów, stoczni i klientów offshore na całym świecie. Specjalizujemy się w projektach dla przemysłu morskiego związanych z budową i modernizacją statków. Aktualnie poszukujemy pracownika do naszego magazynu, zlokalizowanego w Gdańsku (Kowale, ul. Magnacka). Obowiązki: przyjmowanie dostaw i ich kontrola z uwzględnieniem jakościowym i ilościowym, kompletacja zamówień oraz przygotowywanie wysyłek według standardu firmy, właściwe przechowywanie i składowanie towarów, prowadzenie dokumentacji magazynowej, obsługiwanie urządzeń magazynowych, dbałość o porządek na magazynie i na terenie przyległym.	\N	\N	\N	f	f	2025-10-31 12:38:35+01	2025-11-30 12:44:18+01	f	t	398	531	517	\N	\N	26
336	Praca sezonowa dla studentów - Pracownik/ Pracownica magazynu	Glosel jest właścicielem takich marek jak TaniaKsiazka.pl i Bee.pl. To duże drzewo e-commerce, które wyrosło z małej, rodzinnej firmy. Pamiętamy o tych korzeniach i je pielęgnujemy. Dlatego wspólnie tworzymy zespół, w którym każdy jest ważny. Zbudowaliśmy przestrzeń możliwości, inspiracji i wyzwań. W niej wspólnie rozwijamy nasze marki i siebie nawzajem. Wraz z każdym sukcesem przychodzi wiedza, doświadczenie i apetyt na więcej, dlatego ciągle sięgamy wyżej. Działamy dynamicznie i wspólnie mamy wpływ na rozwój firmy. Glosel znaczy WSPÓLNIE. Glosel znaczy ZESPÓŁ. Lubisz być w ruchu i nie przeszkadza Ci fizyczna praca? Szukamy ​pracowników do naszego zespołu w Centrum Logistycznym w Białymstoku (ul. Welurowa 4) Idealna praca dla studentów – nie wymagamy doświadczenia	30.50	\N	f	f	f	2025-10-24 15:07:32+02	2025-11-23 14:07:32+01	f	t	398	535	520	1	1	26
337	Magazynier, dowóz z wielu lokalizacji	Dla naszego Klienta z branży logistycznej poszukujemy kandydatów do pracy na stanowisko: MAGAZYNIER. Miejsce pracy: Ameryka ( okolice Olsztynka). DARMOWY DOWÓZ Z WIELU LOKALIZACJI. Zadania: Kompletowanie, pakowanie towaru, Przygotowywanie przesyłek do wysyłki, Przyjmowanie i rozlokowywanie towarów na magazynie. Wymagania: Dyspozycyjność -praca od poniedziałku do soboty - 5 dni w tygodniu, Chęci do pracy, Umiejętność pracy w grupie, Znajomość obsługi komputera (min. Podstawy). Oferujemy: Zatrudnienie w renomowanej firmie, Umowę o pracę tymczasową, Atrakcyjne zarobki, Dodatek za pracę w soboty, Benefity (m.in. zniżki na posiłki, karta podarunkowa, opieka medyczna Medicover), Darmowe dojazdy do i z pracy. Masz pytania? Zadzwoń !	\N	\N	\N	\N	\N	2025-10-24 15:17:05+02	2025-11-23 14:17:06+01	f	t	398	536	521	\N	\N	26
338	Pracownik Magazynowy Amazon Sady	Pracownik Magazynowy Amazon Sady. Ostatnia szansa na zatrudnienie w tym roku! Atrakcyjna podstawa wynagrodzenia, comiesięczna premia, posiłki i dojazd do pracy, a także liczne benefity. Pracuj wedle zagranicznych standardów! Zakres obowiązków, to proste prace magazynowe, m.in. kompletowanie zamówień, pobieranie towarów, wykładanie produktów na półki, pakowanie.	\N	36.22	f	f	f	2025-11-07 15:45:39+01	2025-12-07 15:45:39+01	f	t	398	423	522	1	1	26
340	Magazynier z uprawnieniami UDT	Poszukiwany magazynier z uprawnieniami UDT do prostych prac magazynowych i obsługi wózka widłowego. Praca na jedną zmianę od poniedziałku do piątku w Gądkach koło Poznania.	4200.00	4600.00	f	f	f	2025-11-07 14:29:01+01	2025-12-07 14:29:02+01	f	t	398	499	524	1	9	26
341	Praca przy kompletacji zamówień	Manpower (Agencja zatrudnienia nr 412) to globalna firma o ponad 70-letnim doświadczeniu, działająca w 82 krajach. Na polskim rynku jesteśmy od 2001 roku i obecnie posiadamy prawie 35 oddziałów w całym kraju. Naszym celem jest otwieranie przed kandydatami nowych możliwości, pomoc w znalezieniu pracy odpowiadającej ich kwalifikacjom i doświadczeniu. Skontaktuj się z nami - to nic nie kosztuje, możesz za to zyskać profesjonalne doradztwo i wymarzoną pracę!\nFirma oferuje atrakcyjne benefity pracownicze, w tym kartę sportową, kartę lunchową, ubezpieczenie na życie oraz miejsce parkingowe, co czyni ją atrakcyjnym pracodawcą na rynku pracy w Łodzi. Organizacja ceni sobie komfort i satysfakcję swoich pracowników, zapewniając im korzystne warunki zatrudnienia oraz wsparcie w codziennych potrzebach.\nZadania:\nKompletacja zamówień małych gabarytów zgodnie z wytycznymi\nRozmieszczenie towaru na magazynie w sposób uporządkowany i efektywny\nObsługa systemów komputerowych i skanerów do identyfikacji i ewidencji towarów\nWymagane umiejętności i kwalifikacje:\nDoświadczenie na stanowisku magazynowym lub w pracy związanej z obsługą magazynu - mile widziane\nMile widziane prawo jazdy kat. B&nbsp;\nPodstawowa znajomość obsługi komputera i skanera\nUmiejętność pracy w zespole&nbsp;\nDobra organizacja pracy&nbsp;\nOferta:\nAtrakcyjne wynagrodzenie wzrastające miesięcznie od 37zł/h brutto (październik), 38,5zł/h brutto (listopad), 40zł/h brutto (grudzień)\nPraca na 2 lub 3 zmiany, dostosowana do preferencji kandydatów\nDarmowy transport pracowniczy zapewniający wygodny dojazd do miejsca pracy&nbsp;\nPakiet benefitów ManpowerGroup Premium, obejmujący m.in. kartę sportową, kartę lunchową, ubezpieczenie na życie oraz miejsce parkingowe\nMożliwość rozpoczęcia pracy od zaraz\nNie zwlekaj! Dołącz do zespołu naszego klienta i skorzystaj z atrakcyjnych warunków zatrudnienia oraz szerokiego pakietu benefitów. Aplikuj już dziś i rozpocznij nową, satysfakcjonującą karierę w dynamicznym środowisku magazynowym w Łodzi!\n&nbsp;	37.00	40.00	f	f	f	2025-11-07 15:03:09+01	2025-12-07 15:03:09+01	f	t	398	444	525	1	1	26
342	Pracownik magazynu | Praca z częściami samochodowymi	Szukasz pracy, w której nie potrzebujesz doświadczenia? Dołącz do zespołu jednego z największych dystrybutorów części samochodowych w Polsce – miejsca, gdzie nauczysz się obsługi skanera, poznasz branżę motoryzacyjną od środka i zyskasz stabilne zatrudnienie w przyjaznej atmosferze!&nbsp;Miejsce pracy: ŚwiebodzinWynagrodzenie: 30,50 zł brutto/h + do 300 zł brutto premii za wydajnośćTyp umowy: Umowa zlecenieGrafik: Od poniedziałku do piątku, dwie zmiany (06:00–14:00, 14:00–22:00). W okresie zwiększonego zapotrzebowania może uruchomić się 3 zmiana: 22:00-6:00.&nbsp;Twoje zadania:Odbiór towaru i jego liczenie,Kompletowanie zamówień,Prace transportowe,Praca ze skanerem.&nbsp;Co zyskujesz:Zatrudnienie na podstawie umowy zlecenia z możliwością przejścia bezpośrednio do klientaPrzyjazny zespół i nowoczesne warunki pracy,Jasne zasady wynagradzania i premiowania,Pełne wsparcie i opiekę koordynatorów OTTO Work Force,Możliwość korzystania z cotygodniowych zaliczek na poczet wynagrodzenia,Możliwość wyrobienia karty Multisport,Dostęp do prywatnej opieki medycznej Luxmed.&nbsp;Wymagania:Znajomość języka polskiego w stopniu komunikatywnym w mowie i piśmieChęć do pracy i nauki nowych zadań.&nbsp;Aplikuj już dziś – to praca, w której naprawdę się odnajdziesz! Z nami zdobędziesz nowe umiejętności i rozpoczniesz stabilną karierę w motoryzacji.	30.50	\N	f	f	f	2025-10-24 16:21:09+02	2025-11-23 15:21:09+01	f	t	398	499	526	1	1	26
343	Pracownik Magazynu Wysokiego Składowania	Wiodąca firma produkcyjno-handlowa o międzynarodowym zasięgu z branży techniki zamocowań i narzędzi ręcznych zlokalizowana w Krakowie Nowa Huta poszukuje: PRACOWNIKA MAGAZYNU WYSOKIEGO SKŁADOWANIA Obowiązki: przyjmowanie i sprawdzanie zgodności dostarczanego towaru z dowodem dostawy, przygotowywanie i wydawanie z magazynu ekspediowanego towaru, układanie, segregacja i porządkowanie towaru na półkach (paletach), pilnowanie poprawności i zgodności faktycznego stanu magazynu z komputerowym. kontrola ilościowo-jakościowa towarów, dbanie o czystość na stanowisku pracy. Wymagania: odpowiedzialność i dokładność, umiejętność obsługi komputera, komunikatywność, umiejętność pracy z zespole, uczciwość, rzetelność i punktualność, nie wymagamy doświadczenia, uprawnienia na wózek widłowy mile widziane Oferujemy: umowę o pracę na pełny etat, praca w trybie zmianowym (7-15, 10-18), możliwość pracy dodatkowo płatnej w formie godzin nadliczbowych lub pracującej soboty, pracę w firmie o pozycji lidera na rynku, możliwość rozwoju zawodowego w strukturach organizacji, pracę w młodym zgranym zespole oraz otwartą i przyjazną atmosferę, możliwość realizowania własnych umiejętności i ciągłego rozwoju, pakiet świadczeń dodatkowych: ubezpieczenie na życie, dofinansowanie wypoczynku; Informujemy, że skontaktujemy się tylko z wybranymi kandydatami.	\N	\N	\N	f	f	2016-04-08 09:13:45+02	2025-12-10 08:26:08+01	f	t	398	542	527	\N	\N	26
344	Magazynier	Kompletowanie zamówień, praca ze skanerem. Możliwość zatrudnienia bezpośrednio w strukturach firmy.	\N	30.73	f	f	f	2025-10-24 13:10:47+02	2025-11-23 12:10:47+01	f	f	398	543	528	1	1	26
345	Amazon | Ostatnia szansa! Do 36,22zł/h brutto*+2000 zł** brutto ekstra	Poszukujemy pracowników na stanowisko pracownika magazynu w Gliwicach. Ostatnie dni rekrutacji w Centrum Logistyki Amazon! Pospiesz się! To ostatnia szansa na to by otrzymać +2000zł brutto** ekstra. Nie czekaj! Liczba miejsc ograniczona! Ostatnie dni rekrutacji w Centrum Logistyki Amazon! Pracuj dla firmy, która zdobyła tytuł Top Employer, i zyskaj więcej, bo to Twoja zmiana i Twoja nowa, wyższa stawka godzinowa do 36,22 zł/godz. brutto* Pospiesz się! To ostatnia szansa na to by otrzymać +2000zł brutto** ekstra. Poleć znajomych do pracy, Ty i osoba polecona zyskacie wspólnie do 5000 zł brutto***! Nie wymagamy od Ciebie doświadczenia. Możesz liczyć na wsparcie wyspecjalizowanych instruktorów.	\N	36.22	f	f	f	2025-10-24 19:58:20+02	2025-11-23 18:58:21+01	f	t	398	404	529	1	1	26
347	Amazon | Ostatnia szansa! Do 36,22zł/h brutto*+2000 zł** brutto ekstra	Poszukujemy pracowników na stanowisko pracownika magazynu w Gliwicach. Ostatnie dni rekrutacji w Centrum Logistyki Amazon! Pospiesz się! To ostatnia szansa na to by otrzymać +2000zł brutto** ekstra. Nie czekaj! Liczba miejsc ograniczona! Ostatnie dni rekrutacji w Centrum Logistyki Amazon! Pracuj dla firmy, która zdobyła tytuł Top Employer, i zyskaj więcej, bo to Twoja zmiana i Twoja nowa, wyższa stawka godzinowa do 36,22 zł/godz. brutto* Pospiesz się! To ostatnia szansa na to by otrzymać +2000zł brutto** ekstra. Poleć znajomych do pracy, Ty i osoba polecona zyskacie wspólnie do 5000 zł brutto***! Nie wymagamy od Ciebie doświadczenia. Możesz liczyć na wsparcie wyspecjalizowanych instruktorów.	\N	36.22	f	\N	\N	2025-10-24 19:58:48+02	2025-11-23 18:58:48+01	f	t	398	404	533	1	1	26
348	Praca w hurtowni spożywczej, Eurocash C&amp;C	Eurocash Cash &amp; Carry, to sieć 180 hurtowni w całej Polsce, koncentrujących się na obsłudze małych i średnich sklepów spożywczych. Jesteśmy częścią Grupy Eurocash, największej polskiej firmy zajmującej się hurtową dystrybucją produktów FMCG. Twoje obowiązki: Uzupełnianie towaru na półkach, Utrzymywanie magazynu w czystości, Rozładunek dostaw, Kontrola pod względem asortymentowym, ilościowym i jakościowym. Współpraca z zespołem. Zgłoś się jeżeli: Posiadasz książeczkę do celów sanitarno-epidemiologicznych, Posiadasz uprawnienia UDT na obsługę wózków widłowych - mile widziane :) Jesteś osobą rzetelną i punktualną, Jesteś osobą zaangażowaną w realizację zadań, Posiadasz doświadczenie na podobnym stanowisku - mile widziane ;) Oferujemy: Umowę o pracę i wynagrodzenie zawsze na czas, Pracę w systemie 2-zmianowym w zespole z miłą atmosferą i świetnym przełożonym, Dzień wolny za pracę w sobotę, Bogaty pakiet benefitów np. karty sportowe, ubezpieczenie medyczne, dofinansowanie do kolonii, karty podarunkowe, rabat na zakupy w hurtowni.	\N	\N	\N	f	f	2025-11-07 16:16:17+01	2025-12-07 16:16:17+01	f	t	398	550	534	\N	\N	26
349	Magazynier - z orzeczeniem	Poszukujemy osób na stanowisko Magazyniera z orzeczeniem. Codzienne obowiązki obejmują przyjmowanie i wydawanie towarów, kontrolę stanów magazynowych oraz dbanie o porządek w miejscu pracy. Praca wymaga dokładności, odpowiedzialności oraz umiejętności współpracy w zespole. Osoba na tym stanowisku będzie wspierać efektywne funkcjonowanie magazynu, co ma kluczowe znaczenie dla realizacji zamówień i satysfakcji klientów.	\N	\N	\N	f	f	2025-10-24 15:54:09+02	2025-11-23 14:54:10+01	f	t	398	553	536	\N	\N	26
350	Magazynier	Praca na zleceniu. Kompletacja/pakowanie towaru, obsługa skanera, proste zadania magazynowe. Wymagana sumienność i dyspozycyjność min. 170 h miesięcznie. Nie wymagane doświadczenie.	34.00	40.00	f	f	f	2025-11-07 14:10:57+01	2025-12-07 14:10:58+01	f	t	398	326	537	1	1	26
238	Pracownik magazynu WZZ "Herbapol" SA - Wrocław	Wrocławskie Zakłady Zielarskie „Herbapol” S.A. specjalizują się w wytwarzaniu produktów leczniczych na bazie surowców zielarskich. Jesteśmy największym producentem leków ziołowych w Polsce. Od ponad 60 lat wprowadzamy na rynek nowoczesne preparaty ziołowe, w których łączymy osiągnięcia medycyny naturalnej oraz najnowocześniejsze technologie stosowane w produkcji leków. Obecnie poszukujemy kandydata na stanowisko: Pracownik Magazynu w Magazynie Surowców i Opakowań miejsce pracy: Wrocław, ul. Księcia Witolda 56 Zatrudniona osoba będzie odpowiedzialna m.in. za: rozładunek produktów dostarczanych do magazynu, weryfikacja przyjmowanych do magazynu produktów pod względem zgodności z dokumentem przyjęcia oraz stanu opakowań zbiorczych, prawidłowe rozmieszczenie i składowanie produktów w magazynie zgodnie z przepisami GMP oraz instrukcjami magazynowymi, przygotowanie wydawanych produktów do wydania na wydziały produkcyjne, zabezpieczenie wydawanych produktów przed uszkodzeniem podczas transportu, załadunek i transport wydanych z magazynu produktów, utrzymywanie w czystości urządzeń magazynowych oraz użytkowanych pomieszczeń.	\N	\N	\N	\N	\N	2025-10-31 15:31:38+01	2025-11-30 15:31:38+01	f	t	398	401	538	\N	\N	26
351	Operator wózka widłowego	Praca jako operator wózka widłowego w Niemczech (Halberstadt). Oferujemy tanie zakwaterowanie i stabilną umowę. Szukamy zmotywowanych kandydatów do pracy w dynamicznym środowisku magazynowym.	\N	\N	\N	\N	\N	2025-11-07 14:32:05+01	2025-12-07 14:33:17+01	f	t	398	556	539	\N	\N	26
352	Prace magazynowe pakowanie układanie sprzątanie	Praca w magazynie sklepu internetowego Hopki.pl - Dorotowo (teren magazynu Lech Centrum). Szukamy osób młodych (również bez doświadczenia), które wspomogą pracowników magazynu w sezonie jesienno- zimowym (między grudniem a marcem), z możliwością pozostania na dłużej, wszystkiego nauczymy. Praca odbywałaby się wg ustalonego wcześniej z osobą przełożoną harmonogramu - pracujemy od pn do pt w systemie zmianowym w godzinach 7-20 oraz w ścisłym sezonie w soboty 6-14. Stawka za godzinę na początek to 31 zł (w przypadku osób uczących się do 26 r.ż. całość wypłacanej kwoty nie jest opodatkowana). Rozpoczęcie pracy - od zaraz lub do uzgodnienia. Posiadasz status osoby uczącej się - daj nam znać w zgłoszeniu, jest wymogiem jego pozytywnego rozpatrzenia. Główne zadania: a) uczestniczenie w dostawie towaru, b) przyjmowanie towaru, liczenie i segregowanie towaru, mini inwentaryzacje, c) rozkładania towaru na półkach, d) pomoc w pakowaniu paczek - donoszenie towaru osobom pakującym e) porządkowanie magazynu f) oklejanie towaru kodami kreskowymi, g) samodzielne pakowanie paczek, h) współpraca z kurierami. Co oferujemy: pracę w nowoczesnej firmie, w młodym i dynamicznym zespole, stałe godziny pracy (wcześniej uzgodniony grafik), pracę na podstawie umowy cywilnoprawnej, niezbędne narzędzia do pracy, szkolenie wewnętrzne i wdrożenie do pracy, finansowe nagrody frekwencyjno-jakościowe. Czego oczekujemy: mobilność (samodzielny dojazd do pracy), dyspozycyjność w ustalonych wcześniej grafikiem godzinach, samodzielność i odpowiedzialność, punktualność, terminowość wykonywania zadań, zaangażowanie w pracy, mile widziane doświadczenie w pracy na podobnym stanowisku (aczkolwiek nie jest wymagane, wszystkiego uczymy). Jesteś zainteresowany? Przyślij do nas swoje CV (mile widziane ze zdjęciem) oraz wynikającą z przepisów prawa zgodą na przetwarzanie danych osobowych w procesie rekrutacji. Na zgłoszenia czekamy do początku grudnia br. Informujemy, iż skontaktujemy się tylko z wybranymi osobami. Proszę dzwonić tylko w godzinach pracy 7-15. W pozostałych godzinach kontakt przez OLX.	31.00	\N	f	f	f	2025-11-10 11:46:44+01	2025-12-10 11:49:47+01	f	t	398	557	540	1	1	26
339	Magazynier - Komisjoner / Order Picker	MGcentral dla MGsolutions MGJJ Sp. z o.o. Sp. k. - Agencji Pracy Tymczasowej oraz Doradztwa Personalnego z siedzibą w Krakowie poszukuje osób zainteresowanych pracą jako: Magazynier - Komisjoner - Order Picker, Miejsce pracy: Berlin (Niemcy) - PRACA OD ZARAZ! Realizacja/kompletowanie zamówień przy pomocy prostego systemu (możliwość pracy na systemie w języku polskim!) Podstawowa obsługa pojazdu elektronicznego do przewożenia palet / koszyków w obrębie magazynu znanej niemieckiej sieci marketów spożywczych Dodatkowe proste prace w obrębie nowoczesnego magazynu logistycznego	15.00	20.00	\N	f	f	2025-11-10 10:47:50+01	2025-12-10 10:47:50+01	f	t	398	533	541	2	1	26
353	Magazynier z obsługą wózka widłowego	Zatrudnimy osoby do organizacji pracy na magazynie w hurtowni wykładzin INTERVIOL. Wymagania: Obsługa wózka widłowego i prawo jazdy kat. B, Doświadczenie na podobnym stanowisku min. 2 lata, Umiejętność planowania przestrzeni magazynowej i bieżąca kontrola stanów, Szykowanie towaru (palety/rolki) do wysyłek, Wiedza z zakresu czytania dokumentów magazynowych (WZ, PZ, listy przewozowe), Nadzór nad dokumentacją i terminowością zadań - stały kontakt z biurem itp., Doskonała organizacja własnej pracy, umiejętność wykonywania czynności pod presją czasu. Oferujemy: umowa o pracę (po okresie próbnym), solidne wynagrodzenie wraz z zestawem premii za wyrobione osiągnięcia, zniżki na firmowe produkty i usługi, spotkania integracyjne, premie świąteczne, rozwój pod okiem doświadczonych mentorów, niekorporacyjną atmosferę pracy w dynamicznie rozwijającej się firmie i zgranym zespole pracującym ze sobą od wielu lat. Firma INTERVIOL działa na rynku od 30 lat i należy do liderów branży wykładzin hotelowych.	\N	\N	\N	f	f	2025-07-30 13:23:25+02	2025-11-18 21:18:17+01	f	t	398	560	542	\N	\N	26
354	Operator Wózka Widłowego	Adecco jest światowym liderem branży doradztwa personalnego, usług i rozwiązań HR. Jest częścią The Adecco Group, obecnego w 60 krajach na całym świecie. Ma ponad 50 lat doświadczenia w branży rekrutacyjnej. W Polsce działa od 1994 roku. Od tego czasu zatrudniło setki tysięcy pracowników i otworzyło w naszym kraju ponad 45 biur. W bazie Adecco znajduje się ponad 350 tysięcy kandydatów. Adecco Poland Sp. z o.o. jest Agencją Zatrudnienia nr 364.\nPoszukujemy kandydatów na stanowisko: Pracownik Magazynu - Operator Wózka Widłowego.\n\nOperator Wózka Widłowego\n\nZakres odpowiedzialności:\n• Obsługa wózka widłowego, rozładunek i załadunek towaru,\n• Przygotowywanie dokumentacji przewozowej,\n• Składowanie przyjętych towarów na regałach i miejscach odstawczych,\n• Obsługa systemów magazynowych, obsługa terminali,\n• Wykonywanie cyklicznej kontroli stanów magazynowych,\n• Przestrzeganie BHP.\n\nProfil kandydata:\n• Uprawnienia na wózki widłowe UDT,\n• Jeśli nie posiadasz aktualnych UDT - pełne wsparcie w procesie uzyskania uprawnień: zapewniamy szkolenia, materiały edukacyjne oraz wsparcie merytoryczne,\n• Obsługa wózka widłowego,\n• Mile widziane doświadczenie w pracy na podobnym stanowisku,\n• Dyspozycyjność do pracy w systemie 4-brygadowym,\n• Odpowiedzialność, dokładność oraz rzetelność.\n\nOferujemy:\n• Stabilne zatrudnienie na pełen etat na podstawie umowy o pracę tymczasową.\n• Możliwości rozwoju w strukturach firmy dzięki szkoleniom podnoszącym kwalifikacje,\n• Karta lunch pass,\n• Benefity poza płacowe: dostęp do karty MultiSport, prywatnej opieki medycznej, ubezpieczenia grupowego,\n• Stabilne i przejrzyste warunki zatrudnienia,\n• Miła atmosfera w pracy,\n• Komfortowe warunki pracy,\n• Kompleksowy program wdrożenia.\n'Informujemy, iż w spółkach z Grupy Adecco działających w Polsce wdrożyliśmy procedurę dokonywania zgłoszeń wymaganą Ustawą o Ochronie Sygnalistów. Szczegółowe informacje są dostępne w biurach Adecco.'	\N	\N	\N	f	f	2025-11-07 06:45:26+01	2025-12-07 06:45:26+01	f	t	398	423	543	\N	\N	26
355	Pracownik ds. rozładunku towarów (branża spożywcza)	Manpower (Agencja zatrudnienia nr 412) to globalna firma o ponad 70-letnim doświadczeniu, działająca w 82 krajach. Na polskim rynku jesteśmy od 2001 roku i obecnie posiadamy prawie 35 oddziałów w całym kraju. Naszym celem jest otwieranie przed kandydatami nowych możliwości, pomoc w znalezieniu pracy odpowiadającej ich kwalifikacjom i doświadczeniu. Skontaktuj się z nami - to nic nie kosztuje, możesz za to zyskać profesjonalne doradztwo i wymarzoną pracę!\nMiejsce pracy: Skierniewice\nRodzaj pracy: praca dodatkowa\nWarunki wynagrodzenia: od 30,50 zł brutto/h do 50,00 zł brutto/h\nWynagrodzenie wypłacane 2 razy w miesiącu\n\nZakres obowiązków:\nRozładunek i rozkładanie towaru zgodnie ze standardami bezpieczeństwa żywności w restauracjach partnerskich\nKontrola poprawności dostaw\nWymagania:\nKomunikatywna znajomość języka polskiego\nDoświadczenie w obsłudze wózka paletowego – mile widziane\nAktualne badania sanitarno-epidemiologiczne\nOferujemy:\nUmowę-zlecenie za pośrednictwem agencji pracy Manpower\nWynagrodzenie 2 razy w miesiącu\nStałe wsparcie koordynatora\nSzkolenia wdrożeniowe\n\nOferta dotyczy pracy tymczasowej.	30.50	50.00	f	f	f	2025-10-31 15:16:40+01	2025-11-30 15:16:40+01	f	t	398	444	545	1	1	26
356	Praca dodatkowa przy rozładunku (sieć restauracji) - Gdańsk	Oferta pracy dodatkowej przy rozładunku towarów w sieci restauracji w Gdańsku. Obowiązki obejmują rozładunek i rozkładanie towaru zgodnie ze standardami bezpieczeństwa żywności, kontrolę poprawności dostaw, dbałość o porządek w magazynie restauracji i przygotowywanie opakowań zwrotnych. Mile widziane doświadczenie w obsłudze wózka paletowego oraz aktualne badania sanitarno-epidemiologiczne. Oferujemy wypłatę co dwa tygodnie, szkolenie w restauracji partnerskiej, stałe wsparcie koordynatora oraz atrakcyjną ofertę rabatową na posiłki w restauracji partnerskiej.	\N	\N	\N	f	f	2025-11-07 09:45:51+01	2025-12-07 09:45:52+01	f	t	398	564	546	\N	\N	26
357	Magazynier - Pipelife Warszawa Modlińska	Firma Pipelife, zajmująca się produkcją systemów rurowych z tworzyw sztucznych jest jednym z 3 największych europejskich producentów w swojej branży. Należy do międzynarodowego holdingu utworzonego przez austriacki koncern wienerberger. W związku z ciągłym rozwojem firmy zapraszamy do udziału w rekrutacji na stanowisko: Magazynier Lokalizacja: Warszawa, Modlińska 205E Twój zakres obowiązków: przyjęcia zewnętrzne i wewnętrzne na magazyn, utrzymanie porządku na placu składowym i w magazynie, dbałość o powierzone mienie, prawidłowe rozlokowanie towaru, załadunek samochodu zgodnie z kartą załadunkową, rozładunki towarów przychodzących z fabryki. Nasze wymagania: znajomość zasad gospodarki magazynowej, uprawnienia UDT na wózek widłowy. To oferujemy: ciekawą, stałą pracę w międzynarodowej firmie, stabilne zatrudnienie w oparciu o umowę o pracę, godziny pracy 8-16, pakiet socjalny (w tym opiekę medyczną i ubezpieczenie na życie), kartę przedpłaconą Lunchpass, zasilaną 200zł miesięcznie, szkolenie produktowe. Informujemy, że skontaktujemy się tylko z wybranymi kandydatami.	\N	\N	\N	f	f	2025-11-07 16:27:57+01	2025-12-07 16:27:58+01	f	t	398	565	547	\N	\N	26
283	Kierownik Magazynu	Firma produkcyjna zatrudni na stanowisko: Kierownik Magazynu. Do głównych obowiązków magazyniera należy: organizowanie pracy zgodnie z przepisami bhp i przeciwpożarowymi, planowanie i monitorowanie zapasów magazynowych, wprowadzanie ulepszeń w systemie magazynowania, przyjmowanie towarów na magazyn oraz uzupełnianie odpowiedniej dokumentacji z tym związanej, przechowywanie i składowanie towaru, kompletowanie towaru oraz jego wysyłka, posługiwanie się obowiązującą dokumentacją magazynową, instrukcjami i normami, obsługa systemów informatycznych – wprowadzanie danych do systemu Comarch, przyjmowanie i rozpatrywanie reklamacji, współpraca z kierującym zmianą, śledzenie procesów produkcyjnych – wykonywanie zleceń materiałów przeznaczonych do produkcji, współpraca z klientami i dostawcami.	\N	\N	\N	f	f	2025-11-07 15:00:11+01	2025-12-07 15:01:24+01	f	f	398	447	548	\N	\N	26
358	Praca bez doświadczenia – magazyn blisko Gdańska | Niepełny etat	Dołącz do zespołu w nowoczesnym terminalu logistycznym w Kowalach (okolice Gdańska) – Tu nauczysz się wszystkiego od podstaw, a grafik dopasujesz do swojego życia! Miejsce pracy: Kowale (okolice Gdańska). Stanowisko: Pracownik magazynu. Twoje zadania: rozładunek i załadunek przesyłek, sortowanie paczek w terminalu, przygotowanie towarów do wysyłki, utrzymanie porządku w miejscu pracy. Bez doświadczenia – wszystkiego nauczymy Cię na miejscu! Niepełny etat – idealna praca dla studentów, rodziców lub osób szukających dodatkowych godzin. Przyjazna atmosfera – pracuj w zespole, który wspiera od pierwszego dnia. Szybka rekrutacja – bez CV, bez zbędnych formalności. Stałe wsparcie koordynatora i pełna obsługa. Ochrona socjalna i bezpieczne warunki pracy. Wymagania: chęć do pracy, dobra kondycja fizyczna, pozytywne nastawienie i punktualność. To praca, która pozwoli Ci szybko zarabiać bez dodatkowych etapów i stresu. Aplikuj już teraz — prześlemy Ci szczegóły i rozpoczniesz pracę w kilka dni!	\N	4500.00	f	f	f	2025-10-24 15:27:28+02	2025-11-23 14:27:29+01	f	t	398	499	549	1	9	26
359	Pracownik ds. rozładunku towarów (branża spożywcza)	Manpower (Agencja zatrudnienia nr 412) to globalna firma o ponad 70-letnim doświadczeniu, działająca w 82 krajach. Na polskim rynku jesteśmy od 2001 roku i obecnie posiadamy prawie 35 oddziałów w całym kraju. Naszym celem jest otwieranie przed kandydatami nowych możliwości, pomoc w znalezieniu pracy odpowiadającej ich kwalifikacjom i doświadczeniu. Skontaktuj się z nami - to nic nie kosztuje, możesz za to zyskać profesjonalne doradztwo i wymarzoną pracę!\n\nMiejsce pracy: Kutno\nRodzaj pracy: praca dodatkowa\nWarunki wynagrodzenia: od 30,50 zł brutto/h do 50,00 zł brutto/h\nWynagrodzenie wypłacane 2 razy w miesiącu\n\nZakres obowiązków:\nRozładunek i rozkładanie towaru zgodnie ze standardami bezpieczeństwa żywności w restauracjach partnerskich\nKontrola poprawności dostaw\nWymagania:\nKomunikatywna znajomość języka polskiego\nDoświadczenie w obsłudze wózka paletowego – mile widziane\nAktualne badania sanitarno-epidemiologiczne\nOferujemy:\nUmowę-zlecenie za pośrednictwem agencji pracy Manpower\nWynagrodzenie 2 razy w miesiącu\nStałe wsparcie koordynatora\nSzkolenia wdrożeniowe\n\nOferta dotyczy pracy tymczasowej.	30.50	50.00	f	f	f	2025-10-31 15:10:51+01	2025-11-30 15:10:51+01	f	t	398	444	550	1	1	26
375	Pracownik magazynu	Zapraszamy chętnych do pracy na stanowisko pracownik magazynu w Sosnowcu. Oferujemy stabilne zatrudnienie, przyjazną atmosferę i atrakcyjne wynagrodzenie. Jedyne czego potrzebujesz to doświadczenie w pracach magazynowych oraz w obsłudze wózków paletowych typu piesek. Praca na 3 zmiany od poniedziałku do piątku 6:00-14:00, 14:00-22:00, 22:00-6:00. Stabilne zatrudnienie w znanej, międzynarodowej firmie logistycznej. Atrakcyjne wynagrodzenie z premiami za wyniki do 25%. Darmowe owoce. Latem codziennie darmowe lody dla wszystkich pracowników. Imprezy integracyjne dla pracowników. Dostęp do opieki medycznej. Możliwość dojazdu do pracy komunikacją miejską. Możliwość korzystania z cotygodniowych zaliczek na poczet wynagrodzenia. Ubezpieczenie od następstw nieszczęśliwych wypadków. 5-dniowe szkolenie dla osób bez doświadczenia. Karta MultiSport. Rekrutacja online. Wsparcie konsultantów. Przyjemna atmosfera w miejscu pracy. Twoje główne obowiązki będą obejmować: Zbieranie zamówień za pomocą skanera na wózku widłowym. Utrzymywanie czystości w miejscu pracy. Aplikuj już teraz i zacznij pracę, która przyniesie Ci satysfakcję i atrakcyjne wynagrodzenie!	\N	\N	\N	f	f	2025-10-24 16:55:10+02	2025-11-23 15:55:11+01	f	t	398	499	568	\N	\N	26
360	Magazynier	Sieć polskich sklepów spożywczych TOPAZ działająca od ponad 30 lat, wypracowała wysoki standard placówek handlowych, atrakcyjną ofertę i silną pozycję na regionalnym rynku. TOPAZ to obecnie ponad sto trzydzieści placówek (sklepów własnych i franczyzowych) w czterech województwach wschodniej, a także centralnej Polski. Nasza marka stale się rozwija i chociaż najważniejszym trzonem działalności TOPAZ są sklepy spożywcze, rozwijamy również własne Centra Logistyczne, stacje benzynowe, punkty gastronomiczne TOP DRIVE, browar restauracyjny BROFAKTURA, galerie handlowe, inwestycje deweloperskie. Dysponujemy również Zakładem rozbioru drobiu, Piekarnią Sokołowską oraz ciastkarnią NATALIA I SZYMON. Misją naszej firmy jest zapewnienie wygody codziennych zakupów z myślą o naszych Klientach i Ich potrzebach. Aktualnie do sklepu TOPAZ w Cegłowie, plac Anny Jagiellonki 4A (województwo mazowieckie, powiat miński) - poszukujemy pracownika na stanowisko - Magazynier Twoje główne zadania: • prawidłowe przyjmowanie dostaw asortymentu • sprawdzanie jakości towaru • odpowiednie rozlokowanie towarów na terenie magazynu • utrzymanie porządku w miejscu pracy • współpraca w zadaniach z pozostałym personelem sklepu Oczekujemy od Ciebie: • dobrej organizacji pracy i rzetelności w realizacji zadań • zaangażowania i dyspozycyjności (praca na 2 zmiany) • umiejętności skutecznej współpracy w zespole • aktualnych badań do celów sanitarno-epidemiologicznych (lub gotowości do ich uzupełnienia) • mile widziane doświadczenie w pracy na podobnym stanowisku Oferujemy Ci: • umowę o pracę i wynagrodzenie zawsze na czas • stabilne zatrudnienie w dynamicznie rozwijającej się firmie • talony na święta • możliwość rozwoju zawodowego i awansu • prywatną opiekę medyczną dla Pracowników i ich rodzin • ubezpieczenie na życie	\N	\N	\N	\N	\N	2025-11-13 12:45:30+01	2025-12-13 12:45:30+01	f	t	398	571	551	\N	\N	26
361	Niemcy - praca na stanowisku magazyniera - bez doświadczenia!	Szukamy komisjonerów do pracy w nowoczesnym magazynie w Niemczech! Pracę możesz rozpocząć od zaraz!\n\nStawka: 14,53 EUR brutto/h, nie wymagamy znajomości języka i uprawnień!\n\nSzczegóły:\n\nPraca komisjonera polega na kompletowaniu paczek na podstawie zleceń. Czasami zdarzają się cięższe paczki (do 30 kg), dlatego szukamy osób, które cieszą się dobrą kondycją fizyczną.\n\nDo Twoich zadań należeć będzie:\n\nkompletowanie zamówień przy użyciu prostego i wygodnego w obsłudze skanera ręcznego\npomoc przy pakowaniu przesyłek\nz czasem również obsługa wózka EPT – przeszkolenie oraz uprawnienia zrobisz na miejscu\nPraca odbywa się w systemie 2-zmianowym.\n\nWymagania:\n\ndoświadczenie na podobnym stanowisku\nczynne prawo jazdy kat. B\nMile widziana znajomość j. angielskiego na poziomie komunikatywnym.\n\nAtuty:\n\nstawka 14,53 euro brutto/h, od stycznia 14,96 euro brutto/h!\nmożesz zacząć pracę bez znajomości języka!\nbonus za dowożenie kolegów i koleżanek do pracy 150€ brutto/msc\nmożesz skorzystać z oferowanego przez nas zakwaterowania\nzapewniamy legalne zatrudnienie na podstawie umowy o pracę tymczasową oraz polskie ubezpieczenie na terenie Niemiec\npomożemy Ci zorganizować transport do Niemiec oraz zapewniamy go na miejscu\nbędziesz pod stałą opieką polskojęzycznego opiekuna projektu\ndarmowy dostęp do platformy z kursami językowymi po 30 dniach zatrudnienia\n\nDzięki polskiej umowie:\n\nodkładasz środki na emeryturę w Polsce\nzachowujesz ciągłość ubezpieczenia\nnie tracisz prawa do dodatków socjalnych i zasiłku dla bezrobotnych\n\nJak wygląda rekrutacja?\n\nmożesz aplikować o pracę tak, jak Ci wygodnie: przez formularz na stronie, telefonicznie, mailem (rekrutacja(a)contrain.pl) lub przez Messenger\noddzwonimy do Ciebie, nasza rekruterka/rekruter pomoże Ci wybrać ofertę i opowie o jej szczegółach\numówimy Cię na badania lekarskie i podpiszemy umowę\n\ni już – możesz zarabiać!\n\nW Contrain mamy 27 lat doświadczenia w zatrudnianiu pracowników tymczasowych, z sukcesami potwierdzonymi nie tylko wyróżnieniami jak „Agencja zatrudnienia przyjazna pracownikom” czy Diamenty Forbesa, ale przede wszystkim opiniami zatrudnianych przez nas osób. Chcesz wiedzieć, co mówią o nas pracownicy?	14.53	14.96	f	\N	\N	2025-10-16 08:59:19+02	2025-11-15 07:59:19+01	f	t	398	496	552	2	1	26
362	Pracownik magazynowy	Firma Wutech Sp. z o.o. poszukuje osoby odpowiedzialnej, dobrze zorganizowanej i samodzielnej do pracy na magazynie w Gorzowie Wielkopolskim. Do głównych zadań należy pakowanie paczek, wydawanie paczek kurierom, dostawa paczek do punktów odbioru, utrzymanie porządku na magazynie, uzupełnianie towaru, kontrola stanów materiałów do pakowania oraz współpraca z zespołem.	\N	\N	\N	f	f	2025-10-31 13:39:53+01	2025-11-30 13:45:24+01	f	t	398	574	553	\N	\N	26
363	Pracownik magazynu z narzędziami (k/m) - praca od zaraz	Do naszego zespołu w EC Siekierki poszukujemy narzędziowca (k/m).\nPraca stacjonarna - 07:00-15:00	\N	\N	\N	f	f	2025-11-07 14:54:35+01	2025-12-07 14:54:35+01	f	t	398	575	554	\N	\N	26
364	Doświadczony Magazynier z UDT	Poszukujemy doświadczonych magazynierów z uprawnieniami UDT, którzy chcą rozwijać się w dynamicznym i nowoczesnym środowisku pracy. Jeśli masz doświadczenie w obsłudze wózków widłowych, dobrze odnajdujesz się w pracy magazynowej – ta oferta jest właśnie dla Ciebie.	\N	\N	\N	f	f	2025-11-07 11:48:05+01	2025-12-07 11:48:06+01	f	f	398	440	555	\N	\N	26
365	Kontroler Stanów Magazynowych Media Expert Łódź	Media Expert to: Lider branży elektromarketów w Polsce (mamy ich ponad 600), Najlepszy omnichannel w Polsce, 3 sklepy internetowe - ponad 20 milionów Klientów rocznie, Zautomatyzowana i nowoczesna logistyka (6 Centrów Dystrybucji w Łodzi i ok. 30 centrów przeładunkowych), Ponad 13 000 pracowników. Dołącz do nas jako: Kontroler Stanów Magazynowych. Łódź, Centrum Dystrybucji/ Umowa o pracę. Do Twoich zadań będzie należeć: Przeprowadzanie codziennej inwentaryzacji towarów w magazynie – weryfikacja stanów we wszystkich lokalizacjach. Współpraca z innymi działami w zakresie bieżącej weryfikacji rozbieżności stanów magazynowych, w tym z zespołem gospodarki magazynowej i dokumentacji. Przygotowanie i aktywny udział w inwentaryzacjach całościowych. Szukamy właśnie Ciebie, jeśli: Jesteś osobą dokładną, sumienną i skrupulatną w wykonywaniu zadań. Angażujesz się w powierzone obowiązki i potrafisz pracować w zespole. Przestrzegasz zasad i procedur obowiązujących w organizacji. Jesteś dyspozycyjny do pracy w systemie zmianowym.	\N	\N	\N	f	f	2025-11-07 15:48:08+01	2025-12-07 15:48:11+01	f	t	398	326	556	\N	\N	26
366	Kierowca / Magazynier w Firmie Best Partner Kielce	Firma Best Partner, hurtownia artykułów biurowych, środków czystości i higieny, zatrudni: Kierowca / Magazynier. Miejsce pracy: Kielce i okolice. Główne obowiązki: Realizowanie dostaw towarów do klientów i odbiorców, Przyjmowanie, rozładunek oraz magazynowanie towarów, Kompletowanie zamówień i przygotowywanie przesyłek do wysyłki, Prowadzenie dokumentacji magazynowej oraz transportowej, Utrzymywanie porządku i prawidłowej organizacji przestrzeni magazynowej. Kandydatem jest osoba: Prawo jazdy kategorii B, Doświadczenie w prowadzeniu pojazdów dostawczych, Umiejętność pracy w zespole, Dobra organizacja pracy własnej, Mile widziane doświadczenie w pracy na magazynie. Kandydatom oferujemy: Stabilne zatrudnienie w oparciu o umowę o pracę, Terminowe wypłaty wynagrodzenia, Możliwość rozwoju i podnoszenia kwalifikacji zawodowych, Dodatkowe benefity pracownicze, takie jak możliwość ubezpieczenia grupowego. Jednocześnie informujemy, że skontaktujemy się z wybranymi kandydatami.	\N	\N	\N	f	f	2022-05-31 17:31:34+02	2025-12-07 22:12:09+01	f	t	398	578	557	\N	\N	40
367	Praca dodatkowa przy rozładunku - Kutno	Rozładunek i rozkładanie towaru zgodnie ze standardami bezpieczeństwa żywności w restauracjach partnerskich, kontrola poprawności dostaw, rozkładanie towaru, dbałość o porządek w magazynie restauracji i przygotowywanie opakowań zwrotnych.	\N	\N	\N	f	f	2025-11-07 16:45:57+01	2025-12-07 16:45:57+01	f	t	398	564	558	\N	\N	26
368	Magazynier - Kierowca	Zapewnienie sprawnego przepływu towarów w procesie cross-dockingowym oraz bezpiecznej realizacji dostaw do klientów od rozładunku, przez sortowanie, po transport i obsługę zwrotów. Do Twoich zadań należeć będzie: Rozładunek dostaw z centrali (meble tapicerowane, luzem). Układanie mebli na paletach, foliowanie, etykietowanie. Sortowanie towarów według tras i przygotowanie do załadunku. Załadunek busów dostawczych (15 m³) oraz kontrola kompletności przesyłek. Realizacja dostaw do klientów (kierowca kurier) w razie potrzeby lub zastępstwa: prowadzenie pojazdu kat. B (busy firmowe), kontakt z klientami przy odbiorze, dbanie o dokumentację i potwierdzenia dostaw. Obsługa zwrotów odbiór, weryfikacja i przekazanie do strefy kontrolnej. Praca z terminalem mobilnym (Zebra) skanowanie, potwierdzanie przyjęć i wydań, wprowadzanie danych w aplikacji magazynowej. Utrzymanie porządku, bezpieczeństwa i zgodności z BHP. Codzienne raportowanie wykonanych zadań do lidera magazynu.	\N	\N	\N	f	f	2025-11-07 12:59:49+01	2025-12-07 13:03:32+01	f	t	398	506	559	\N	\N	26
369	Pracownik ds. rozładunku towaru	Manpower (Agencja zatrudnienia nr 412) to globalna firma o ponad 70-letnim doświadczeniu, działająca w 82 krajach. Na polskim rynku jesteśmy od 2001 roku i obecnie posiadamy prawie 35 oddziałów w całym kraju. Naszym celem jest otwieranie przed kandydatami nowych możliwości, pomoc w znalezieniu pracy odpowiadającej ich kwalifikacjom i doświadczeniu. Skontaktuj się z nami - to nic nie kosztuje, możesz za to zyskać profesjonalne doradztwo i wymarzoną pracę!\nPracownik ds. rozładunku towaru\n\nPoszukujemy osób do pracy przy rozładunku i rozmieszczaniu towaru zgodnie ze standardami bezpieczeństwa żywności.\n\nMiejsce pracy: Płońsk\n\nWarunki zatrudnienia:Praca we wtorki, piątki i soboty w godzinach nocno-porannychStały grafik ustalany i przekazywany z wyprzedzeniemStała i niezmienna lokalizacja pracyWynagrodzenie: stawka minimalna 30,50 zł/h z możliwością uzyskania dużo wyższej stawki godzinowej przy sprawnym rozładunku (nawet ok. 50 zł/h)Wypłaty realizowane co dwa tygodnieWymagania:Komunikatywna znajomość języka polskiegoKsiążeczka sanitarno-epidemiologicznaOferujemy:Szkolenie przygotowujące do pracyStałe wsparcie koordynatoraDodatkowe benefityZatrudnienie na podstawie umowy-zlecenia\nOferta dotyczy pracy tymczasowej.	30.50	50.00	\N	f	f	2025-11-07 14:27:07+01	2025-12-07 14:27:07+01	f	t	398	444	560	1	1	26
370	Pracownik Magazynu	Praca w Emilianowie niedaleko Warszawy. Proste prace magazynowe niewymagające doświadczenia. Praca od poniedziałku do piątku. Prace porządkowe na magazynie, załadunek/rozładunek paczek.	4800.00	\N	f	\N	\N	2025-11-07 16:12:55+01	2025-12-07 16:12:55+01	f	t	398	499	561	1	9	26
371	Magazynier / Zaopatrzeniowiec	Firma SANTEX Sp. z o.o. z Sędziszowa Małopolskiego zatrudni na stanowisko: Magazynier/Zaopatrzeniowiec Zakres obowiązków: planowanie i koordynacja pracy magazynów; obsługa programu sprzedażowo- magazynowego Insert; nadzór nad stanami magazynowymi – kontrola, inwentaryzacja, raportowanie; dbałość o prawidłowy obieg dokumentów i informacji; prowadzenie dokumentacji magazynowej, wprowadzenie i wystawianie dokumentów; przygotowanie i nadzór nad inwentaryzacją magazynową; planowanie i organizacja transportów (przyjęcia, wysyłki, dostawy); odpowiedzialność za procesy wewnętrznej dystrybucji materiałów, narzędzi i sprzętu; Nasze wymagania: doświadczenie w pracy na podobnym stanowisku ; znajomość procesów magazynowych i obiegu dokumentów; biegła znajomość komputera oraz oprogramowania MS Office; dobra organizacja pracy oraz terminowe wykonywanie zadań; uczciwość; komunikatywność; prawo jazdy kat. B; uprawnienia UDT na wózki widłowe; samodzielność i odpowiedzialność w realizacji powierzonych zadań; dodatkowym atutem będzie znajomość materiałów, sprzętu i narzędzi używanych w branży sanitarnej i budowlanej. Oferujemy: praca w firmie o ugruntowanej pozycji na rynku z 40 letnią tradycją; stabilne warunki zatrudnienia; atrakcyjne wynagrodzenie ; ubezpieczenie na zdrowie i życie; Karta Multisport; MultiLife – benefit pozwalający zadbać o rozwój, zdrowie, kondycję psychiczną i zbilansowane odżywianie; dodatkowe benefity w ramach ZFŚS; Osoby chętne do podjęcia pracy zapraszamy do załączania CV, kontakt telefoniczny lub osobisty w siedzibie naszej firmy (ul. Wspólna 13B , 39-120 Sędziszów Małopolski) Kontakt: (17) 749 21 70 / 60*****41 SANTEX jest firmą rodzinną działającą na rynku lokalnym i krajowym nieprzerwanie od 1984 roku. Działalność firmy, oparta o wieloletnie doświadczenie w branży inżynieryjno-budowlanej i produkcyjnej, pozwoliła na wypracowanie solidnej marki i silnej pozycji na rynku.	\N	\N	\N	f	f	2025-10-31 15:24:04+01	2025-11-30 15:27:40+01	f	t	398	583	562	\N	\N	26
312	Magazynier	Przyjmowanie towaru w magazynie, przygotowywanie i wydawanie zamówień dla klientów, dokonywanie załadunku i rozładunku towarów, utrzymanie magazynu i jego wyposażenia w dobrym stanie technicznym, przestrzeganie obowiązujących przepisów BHP i PPOŻ, utrzymywanie dobrych relacji interpersonalnych ze współpracownikami oraz klientami.	\N	\N	\N	f	f	2025-11-07 16:05:49+01	2025-12-07 16:05:50+01	f	t	398	502	563	\N	\N	26
372	Magazynier – Tool &amp; Equipment Assistant Lubczyńska 6	FairWind – międzynarodowa firma działająca w branży energetyki wiatrowej – poszukuje osoby na stanowisko Magazyniera (Tool &amp; Equipment Assistant). Jeśli jesteś dobrze zorganizowany, potrafisz planować swoją pracę i chcesz rozwijać się w dynamicznej branży OZE – dołącz do naszego zespołu. Miejsce pracy: ul. Lubczyńska 6, Szczecin Zakres obowiązków: Przygotowywanie i pakowanie zamówień zgodnie z listą – przed rozpoczęciem projektów, z placów budowy lub indywidualnych zamówień od klientów, Przyjmowanie i wydawanie zamówień, Konserwacja i przeglądy narzędzi oraz sprzętu – sprawdzanie ich stanu przed wydaniem, Obsługa wózka widłowego, Codzienny kontakt z innymi działami firmy FairWind oraz z technikami. Wymagania: Dobra organizacja pracy i umiejętność planowania, Komunikatywność i umiejętność pracy w zespole, Nastawienie na jakość i realizację celów, Podstawowa znajomość programu MS Excel, Znajomość języka angielskiego na poziomie dobrym/bardzo dobrym, Uprawnienia na wózki widłowe UDT – kategoria II WJO, Prawo jazdy kat. B. Oferujemy: Umowę o pracę w stabilnej, międzynarodowej firmie, Godziny pracy: 8:00–16:00, Możliwość rozpoczęcia kariery w branży energetyki wiatrowej, Pracę w międzynarodowym środowisku, Benefity pozapłacowe: Luxmed, Multisport, grupowe ubezpieczenie, Dostęp do świeżych owoców, kawy i herbaty w pracy. Zainteresowany? Prześlij swoje CV i dołącz do zespołu, który wspiera rozwój zielonej energii na całym świecie.	\N	\N	\N	f	f	2025-10-06 13:51:52+02	2025-12-05 14:14:02+01	f	t	398	585	564	\N	\N	26
286	Magazynier	Rozładunek i załadunek pojazdów, przyjmowanie i wydawanie towarów, kontrola stanów magazynowych, dbałość o porządek w magazynie, kompletowanie i składanie zamówień, prowadzenie dokumentacji magazynu.	27.00	27.00	f	f	f	2025-07-22 22:06:11+02	2025-12-03 10:35:23+01	f	t	398	459	565	1	1	26
373	Pracownik ds. rozładunku towarów (branża spożywcza)	Manpower (Agencja zatrudnienia nr 412) to globalna firma o ponad 70-letnim doświadczeniu, działająca w 82 krajach. Na polskim rynku jesteśmy od 2001 roku i obecnie posiadamy prawie 35 oddziałów w całym kraju. Naszym celem jest otwieranie przed kandydatami nowych możliwości, pomoc w znalezieniu pracy odpowiadającej ich kwalifikacjom i doświadczeniu. Skontaktuj się z nami - to nic nie kosztuje, możesz za to zyskać profesjonalne doradztwo i wymarzoną pracę!\nMiejsce pracy: Pabianice\nRodzaj pracy: praca dodatkowa\nWarunki wynagrodzenia: od 30,50 zł brutto/h do 50,00 zł brutto/h\nWynagrodzenie wypłacane 2 razy w miesiącu\n\nZakres obowiązków: Rozładunek i rozkładanie towaru zgodnie ze standardami bezpieczeństwa żywności w restauracjach partnerskich, Kontrola poprawności dostaw\nWymagania: Komunikatywna znajomość języka polskiego, Doświadczenie w obsłudze wózka paletowego – mile widziane, Aktualne badania sanitarno-epidemiologiczne\nOferujemy: Umowę-zlecenie za pośrednictwem agencji pracy Manpower, Wynagrodzenie 2 razy w miesiącu, Stałe wsparcie koordynatora, Szkolenia wdrożeniowe\n\nOferta dotyczy pracy tymczasowej.	30.50	50.00	f	f	f	2025-10-31 15:10:52+01	2025-11-30 15:10:52+01	f	t	398	444	566	1	1	26
374	Praca dodatkowa przy rozładunku (sieć restauracji) - Człuchów	Rozładunek i rozkładanie towaru zgodnie ze standardami bezpieczeństwa żywności w restauracjach partnerskich, kontrola poprawności dostaw, rozkładanie towaru, dbałość o porządek w magazynie restauracji i przygotowywanie opakowań zwrotnych.	\N	\N	\N	f	f	2025-10-31 14:45:52+01	2025-11-30 14:45:53+01	f	t	398	564	567	\N	\N	26
433	Staż programista frontend developer	Wymagania: Podstawowa znajomość języka Python lub Javascript, chęć doskonalenia umiejętności, podstawowa znajomość SQL / HTTP, znajomość systemów kontroli wersji GIT, umiejętność tworzenie struktur danych takich jak np. drzewo binarne. Oferujemy: Code Review, sprawdzoną ścieżkę kariery w IT, projekty open source, praca w dynamicznie rozwijającym się zespole, ciekawe projekty, kawa, napoje, darty. Biuro dobrze skomunikowane SKM / ZKM w centrum Gdyni.	\N	\N	\N	f	f	2022-02-11 00:00:00+01	2025-11-19 00:00:00+01	f	f	590	693	651	\N	\N	18
382	Specjalista/ka ds. Serwisu IT	GRUPA AZOTY – TU PRACUJĄ POKOLENIA. Grupa Azoty S.A. – jednostka dominująca w Grupie Kapitałowej Grupa Azoty - to firma z niemal 100-letnią tradycją, w której kolejne pokolenia pracowników tworzą nowoczesną i bezpieczną chemię. Posiadamy silne zaplecze techniczne i stawiamy na bezpieczeństwo, stabilność i rozwój. Jesteśmy jednym z kluczowych producentów branży nawozowo-chemicznej w Europie. Naszą domeną jest działalność produkcyjna, usługowa i handlowa w zakresie nawozów mineralnych, tworzyw konstrukcyjnych oraz specjalistycznych chemikaliów. Produkujemy m.in. nawozy azotowe i wieloskładnikowe, w tym Saletrosan® 26 i siarczan amonu AS 21 – których jesteśmy największym producentem w kraju. Jesteśmy również wiodącym producentem tworzyw konstrukcyjnych Tarnamid® (PA6) oraz chemikaliów, takich jak cykloheksanon czy katalizatory. Dołącz do zespołu Grupy Azoty i rozwijaj swoje umiejętności w jednej z największych firm chemicznych w Europie.	\N	\N	\N	f	f	2025-11-13 00:00:00+01	2025-12-28 00:00:00+01	f	f	590	603	607	\N	\N	18
388	Specjalista	\N	5900.00	5900.00	f	f	f	2025-11-13 00:00:00+01	2025-12-12 00:00:00+01	f	f	590	609	613	1	9	18
389	Informatyk R11/2689-1322/25	\N	\N	\N	\N	f	f	2025-11-13 00:00:00+01	2025-12-13 00:00:00+01	f	f	590	611	615	\N	\N	18
405	Handlowiec	W trosce o zdrowie ludzi współczesna produkcja będzie ograniczać zakresu oddziaływania na środowiska oraz wykluczać w zastosowaniach praktycznych substancji uznanych za szkodliwe lub mogące mieć negatywny wpływ na środowisko i zdrowie ludzi. Nasze rozwiązania produktowe i doradcze będą zapobiegać zagrożeniom dla środowiska jak i dla zdrowia zwierząt i ludzi. Jesteśmy przedsiębiorstwem specjalizującym się w oferowaniu produktów pochodzących z natury. Oferujemy naturalne innowacyjne rozwiązania oparte na fitobiotykach i biotechnologii do zastosowań w hodowli i rolnictwie jak również wspierania zdrowia ludzi . Rozwijamy swoją działalność i dlatego poszukujemy zaangażowanych i aktywnych osób;Przedstawiciela Handlowego (specjalista ds. sprzedaży)Twój zakres obowiązków•\taktywne pozyskiwanie nowych klientów, sprzedaż asortymentu spółki na powierzonym terenie (teren całej Polski)•\tustalanie szczegółów warunków współpracy i nadzorowanie prawidłowego jej przebiegu;•\tbudowanie długofalowych relacji biznesowych z Klientami na powierzonym terenie;•\tdoradztwo merytoryczne i techniczne w zakresie produktów z portfolio firmy;•\tdbałość o realizację założonych planów sprzedażowych;•\tmonitorowanie działań konkurencji;•\tsporządzanie cyklicznych raportów z realizowanych zadań.Nasze wymagania•\tpożądane wykształcenie (biologia, biotechnologia, rolnictwo, weterynaria lub pokrewne),•\tczynne prawo jazdy kat. B i gotowość do pracy w terenie,•\tumiejętność samodzielnego planowania czasu pracy;•\tUmiejętności sprzedażoweMile widziane•\tminimum 2 letnie doświadczenie w pracy na podobnym stanowiskuTo oferujemy•\tzatrudnienie w oparciu o umowę o pracę;•\tniezbędne narzędzia do pracy•\tmotywacyjny system premiowy;	\N	\N	\N	f	f	2022-02-11 00:00:00+01	2025-11-20 00:00:00+01	f	f	590	650	604	\N	\N	328
406	Technik Laboratoryjny	Poszukujemy absolwentek/ów kierunków biologicznych, mikrobiologicznych, biotechnologicznych oraz chemicznych, chętnych do pracy na stanowisku Technika Laboratoryjnego Biobanku, którego głównym zakresem obowiązków będą:•\tobróbka materiału biologicznego zgodnie z przyjętymi standardowymi procedurami operacyjnymi,•\tbiobankowanie pobranego materiału biologicznego,•\tarchiwizacja danych z obróbki materiału biologicznego,•\tudział w tworzeniu/przygotowanie dokumentacji procesowej,•\tudział w przygotowaniu dokumentacji technicznej,•\tudział w doskonaleniu Systemu Zarządzania Jakością i Środowiskiem•\tgospodarka magazynowa,•\twspółpraca z pracownikami naukowymi,•\tzabezpieczenie i konserwacja aparatury.	\N	\N	\N	f	f	2022-03-22 00:00:00+01	2025-11-21 00:00:00+01	f	f	590	651	605	\N	\N	18
407	Handlowiec	Aktywne pozyskiwanie nowych klientów, sprzedaż asortymentu spółki na terenie całej Polski, budowanie długofalowych relacji biznesowych z Klientami, doradztwo merytoryczne i techniczne w zakresie produktów z portfolio firmy, dbałość o realizację założonych planów sprzedażowych, monitorowanie działań konkurencji, sporządzanie cyklicznych raportów z realizowanych zadań.	\N	\N	\N	\N	\N	2022-02-11 00:00:00+01	2025-11-20 00:00:00+01	f	f	590	652	606	\N	\N	328
383	Specjalista ds. Wsparcia Technicznego z Językiem Niemieckim	<p>Manpower (Agencja zatrudnienia nr 412) to globalna firma o ponad 70-letnim doświadczeniu, działająca w 82 krajach. Na polskim rynku jesteśmy od 2001 roku i obecnie posiadamy prawie 35 oddziałów w całym kraju. Naszym celem jest otwieranie przed kandydatami nowych możliwości, pomoc w znalezieniu pracy odpowiadającej ich kwalifikacjom i doświadczeniu.&nbsp;Więcej informacji na temat Manpower znajduje się na www.manpower.pl</p>\n<br>\n<p>Skontaktuj się z nami - to nic nie kosztuje, możesz za to zyskać profesjonalne doradztwo i wymarzoną pracę!</p>	7500.00	7500.00	f	f	f	2025-11-04 00:00:00+01	2025-12-04 00:00:00+01	f	f	590	654	608	1	9	18
384	Specjalista Informatyk	Spółka CELMA INDUKTA S.A. poszukuje pracownika na stanowisko Specjalista informatyk. Praca wynagradzana w systemie miesięcznym, od poniedziałku do piątku w godz. 700 – 1500, miejsce pracy BIELSKO-BIAŁA.	\N	\N	\N	f	f	2025-10-29 00:00:00+01	2025-11-28 00:00:00+01	f	f	590	605	609	\N	9	18
385	Pracownik I Linii Wsparcia Klienta / Tester Oprogramowania	<p style="text-align: justify"><a href="https://www.docusoft.pl/" target="_blank" rel="nofollow"><strong>Docusoft Sp. z o.o.</strong></a> to firma od ponad 20 lat działająca na rynku, specjalizująca się w tworzeniu i wdrażaniu inteligentnego oprogramowania klasy BPM służącego wspieraniu procesów biznesowych przedsiębiorstw i instytucji oraz zaawansowanych systemów rozpoznawania danych (Optical Character Recognition)</p>\n            \n            <p>Jesteśmy producentem oprogramowania do zarządzania dokumentacją i procesami biznesowymi. Wspieramy firmy w całej Polsce – teraz szukamy osoby, która pomoże nam obsługiwać zgłoszenia serwisowe od klientów i dbać o komunikację z zespołem technicznym.</p>	\N	\N	\N	f	f	2025-10-27 00:00:00+01	2025-11-26 00:00:00+01	f	f	590	606	610	\N	\N	18
386	Technik ds. Badań i Rozwoju	Synthos jest koncernem chemicznym, którego działalność polega na produkcji oraz dostarczaniu rozwiązań na potrzeby różnych gałęzi przemysłu. Firma produkuje i dostarcza kauczuki syntetyczne, tworzywa styrenowe, środki ochrony roślin oraz dyspersje i lateksy, których odbiorcami są firmy z całego świata. Synthos posiada zakłady produkcyjne zlokalizowane w Polsce, Czechach, Holandii, Niemczech i we Francji. To tam produkuje, a następnie dostarcza swoje innowacyjne rozwiązania – nawet do najdalszych zakątków świata. Firma zatrudnia ponad 3 600 pełnych pasji ludzi, którzy na co dzień pracują nad rozwiązaniami pomagającymi zmieniać świat. Technik ds. Badań i Rozwoju Dział R&amp;D Kauczuki Miejsce pracy: Oświęcim	\N	\N	\N	f	f	2025-10-23 00:00:00+02	2025-12-07 00:00:00+01	f	f	590	607	611	\N	\N	21
387	Specjalista ds. SAP	Od blisko 130 lat firma Klingspor wyznacza światowe standardy w zakresie techniki szlifowania. W naszych zakładach produkcyjnych wytwarzanych jest ponad 50 000 artykułów, między innymi z grupy wyrobów ściernych na podłożu, tarcz do cięcia i szlifowania, ściernic listkowych talerzowych i ściernic listkowych nasadzanych, tarcz diamentowych - przeznaczonych do najróżniejszych obszarów zastosowań. Nasze zakłady produkcyjne i handlowe, zlokalizowane w 36 miejscach na świecie, zatrudniają łącznie blisko 3000 pracowników, pozwalając na elastyczne dostosowanie profilu produkcji do potrzeb rynków regionalnych. Nasz zespół to między innymi: ponad 300 doradców terenowych, wysoko wykwalifikowanych techników i inżynierów, którzy pomagają naszym klientom na miejscu w prawidłowym doborze wyrobów. Wykształcone standardy, jakość, niezawodność i perfekcyjny serwis klienta, połączone z najnowszymi technologiami produkcji stanowią podstawę trwałego sukcesu wyrobów Klingspor i powodują, iż nasze przedsiębiorstwo należy do wiodących w branży wyrobów ściernych. Specjalista ds. SAP w firmie zajmującej się produkcją i sprzedażą artykułów ściernych. Międzynarodowe środowisko pracy	\N	\N	\N	f	f	2025-10-23 00:00:00+02	2025-11-22 00:00:00+01	f	f	590	608	612	\N	\N	18
408	Informatyk	\N	4666.00	\N	f	f	f	2025-11-13 00:00:00+01	2025-12-10 00:00:00+01	f	f	590	660	614	1	9	18
409	Informatyk w security operation center	\N	9000.00	\N	f	f	f	2025-11-12 00:00:00+01	2025-11-30 00:00:00+01	f	f	590	660	616	1	9	18
410	Specjalista pierwszej linii wsparcia IT	\N	7000.00	\N	f	f	f	2025-11-10 00:00:00+01	2025-12-31 00:00:00+01	f	f	590	663	617	1	9	18
411	Starszy teleinformatyk	\N	5700.00	\N	f	f	f	2025-11-10 00:00:00+01	2025-12-01 00:00:00+01	f	f	590	664	618	1	9	18
412	Technik informatyk	\N	4666.00	\N	f	f	f	2025-11-07 00:00:00+01	2025-12-05 00:00:00+01	f	f	590	660	619	1	9	18
434	Osoba ds. tworzenia i pozycjonowania stron	Szukam osoby do współpracy przy tworzeniu nowoczesnych stron internetowych oraz ich pozycjonowaniu (SEO). Praca zdalna lub stacjonarna (do ustalenia). Zakres obowiązków: Tworzenie stron internetowych (WordPress, HTML, CSS, itp.) Optymalizacja stron pod kątem SEO Pozycjonowanie i dbanie o widoczność w wyszukiwarkach Aktualizacja i utrzymanie istniejących stron. Wymagania: Doświadczenie w tworzeniu stron www Znajomość SEO i narzędzi analitycznych (Google Analytics, Search Console itp.) Kreatywność i dbałość o szczegóły Samodzielność i terminowość Oferuję: Elastyczne godziny pracy Umowa o pracę Rozliczenie za projekt lub stała współpraca Możliwość rozwoju i realizacji ciekawych projektów	\N	\N	\N	t	\N	2025-08-12 00:00:00+02	2025-11-18 00:00:00+01	f	f	590	698	652	\N	8	18
435	Programista Kohana 7 do CRMa	Nawiążemy współpracę z doświadczonym Programistą/ Informatykiem ze znajomością KOHANA 7 do dokończenia programu CRMoraz na zasadzie dodatkowych zleceń do wykonania wg. aktualnych potrzeb firmy. Zlecenia mogą dotyczyć: wykonania drobnych poprawek na istniejącej stronie www, umieszczenie kodu, utworzenie i podpięcie formularza kontaktowego, umieszczenie na serwerze dedykowanego formularza lub ankiety do wyplenienia, podpięcie możliwości płatności online za kurs na www, programowania np. stworzenia formularza online z pytaniami, PROGRAMOWANIA CRMa, stworzenia grafiki lub banera z umieszczeniem go na www, utworzenia nowej responsywnej strony internetowej dla szkoleń.	\N	\N	\N	\N	\N	2022-11-16 00:00:00+01	2025-11-21 00:00:00+01	f	f	590	699	653	\N	\N	18
436	Programista Unity	Rozpoczynamy produkcję trzech ambitnych gier VR o międzynarodowym potencjale. Poszukujemy Programisty (silnik Unity) który wesprze nasze zespoły produkcyjne. Czego oczekujemy? 1) Doświadczenia w zakresie programowania na silniku Unity. 2) Chęci do pracy w dogodnym dla Ciebie trybie i wymiarze (preferujemy współpracę stałą). 3) Doświadczenie w projektach VR/AR nie jest wymagane, ale będzie dodatkowym atutem. Co oferujemy? • atrakcyjne wynagrodzenie, • możliwość pracy zdalnej (lub w naszym krakowskim biurze), • udział w szybko rozwijającej się firmie z apetytem na globalny sukces:) • dodatkowe kursy i szkolenia Jeśli znudziła Cię praca w aktualnym miejscu - szukasz wyzwań i chcesz realizować projekty, z których będziesz dumny – aplikuj do nas. Poznaj nieograniczony potencjał gier VR! Aplikuj na: @	\N	\N	\N	t	\N	2022-11-16 00:00:00+01	2025-11-21 00:00:00+01	f	f	590	700	654	\N	\N	18
437	Zatrudnię	Zatrudnimy absolwenta szkół elektronicznych/elektrycznych do nauki spawania światłowodów lub osobę posiadającą takie umiejętności. Zatrudnimy osoby do pracy przy budowie, przebudowie sieci teletechnicznych, osoby nie potrzebują posiadać doświadczenia wystarczą chęci do pracy, a doświadczenie przyjdzie w trakcie zatrudnienia.	\N	\N	\N	f	f	2023-05-24 00:00:00+02	2025-11-21 00:00:00+01	f	f	590	701	655	\N	\N	18
438	Przygotuję ekspertyzy techniczne	Ubezpieczyciel wymaga przestawienia ekspertyzy uszkodzonego sprzętu❓ Nic strasznego. Zapraszamy do Syntcom Polska, nasz serwisant profesjonalnie przygotuje wynaganą dokumentację	\N	\N	\N	f	f	2022-11-16 00:00:00+01	2025-11-19 00:00:00+01	f	f	590	702	656	\N	\N	18
439	Starszy Programista Embedded C++	Jeśli masz pasję do osiągania sukcesów, możemy zapewnić Ci rozwój. Dołącz do dynamicznego ekosystemu, w którym ludzie są centralnym elementem działań, osiągnięć i wartości, jakie dostarczają naszym partnerom. Nasz partner - szybko rozwijająca się międzynarodowa firma - z siedzibą w Krakowie poszukuje doświadczonej osoby na stanowisko: Starszy Programista Embedded C++ (Kraków lub zdalnie) Będziesz odpowiedzialny za: - Udział w najnowocześniejszych projektach motoryzacyjnych z technologiami związanymi z rynkiem, - Analizę wymagań rozwiązania, - Projektowanie i rozwój oprogramowania w obszarze motoryzacji, - Kreatywne podejście do pracy w celu wymyślania nowych rozwiązań, - Bliską współpracę z PM, projektantami i analitykami, - Zapewnienie jakości kodu. Oferujemy: - Atrakcyjne wynagrodzenie adekwatne do kompetencji - szukamy członków zespołu i lidera zespołu, więc zakres wynagrodzeń jest bardzo szeroki, - Członkowie zespołu mogą pracować stacjonarnie, hybrydowo lub zdalnie, - Ambitne wyzwania wynikające z szybkiego rozwoju międzynarodowej firmy, - Możliwość pracy dla czołowej firmy motoryzacyjnej i współpracy z jej kierownictwem, - Wymagające projekty dla największych marek motoryzacyjnych, - Możliwość tworzenia i wdrażania własnych rozwiązań biznesowych w zakresie zarządzania zespołem i rozwoju biznesu, - Ścieżkę kariery opartą na kulturze wzrostu wraz z organizacją, - Uczciwą i otwartą komunikację, - Niekorporacyjną atmosferę i obowiązki, które zapobiegają popadnięciu w rutynę.	\N	\N	\N	\N	\N	2022-11-16 00:00:00+01	2025-11-21 00:00:00+01	f	f	590	703	657	\N	\N	18
440	Programista Java w branży motoryzacyjnej	Współpraca ze znanym niemieckim producentem samochodów. Poszukujemy doświadczonego kandydata, który będzie pracował z inżynierami oprogramowania z centrum doskonałości w zespole pracującym nad oprogramowaniem pośredniczącym Java w pojeździe (bezpieczne routowanie wiadomości między zapleczem motoryzacyjnym a usługami działającymi wewnątrz podłączonego samochodu) na stanowisku: Programista Java w branży motoryzacyjnej (praca zdalna lub biuro w Łodzi). Będziesz odpowiedzialny za: - Rozumienie wymagań klienta i przedstawionego HLD (High-Level Design), - Tworzenie LLD (Low Level Design) odpowiadającego przedstawionemu HLD, - Projektowanie komponentów oprogramowania, - Implementowanie wysokiej jakości obiektowo zorientowanego kodu w Javie osadzonego w wewnętrznej infrastrukturze obliczeniowej pojazdu, - Zapewnienie jakości tworzonych komponentów oprogramowania za pomocą pokrycia testami jednostkowymi i rozwiązywania problemów SCA. Wymagamy: - Min. 3 lata doświadczenia w tworzeniu oprogramowania, w tym min. 1 rok doświadczenia w programowaniu w Javie, - Umiejętność samodzielnej pracy, - Umiejętność rozumienia HLD i LLD oraz samodzielnego wdrażania komponentów z wysokiej jakości modularnym kodem testowalnym, - Dobra znajomość architektury klient-serwer i protokołów sieciowych, takich jak HTTP, REST, - Umiejętność podsumowywania i debugowania typowych tematów technicznych, - Praca w środowisku SCRUM, - Silne umiejętności analityczne i chęć rozwoju w Javie, - Znajomość standardów motoryzacyjnych, takich jak CAN i AutoSAR jest dodatkowym atutem, - Znajomość standardów, takich jak TCP/IP, Ethernet, SOCKS i MQTT jest dodatkowym atutem, - Biegła komunikacja w języku angielskim jest konieczna, - Komunikacja w języku niemieckim jest dodatkowym atutem. Oferujemy: - Atrakcyjne wynagrodzenie adekwatne do kompetencji – przedział wynagrodzeń zależy od Twoich umiejętności i jest zawsze powyżej regularnych ofert rynkowych – spróbuj nas! - Członkowie zespołu mogą pracować w modelu stacjonarnym, hybrydowym lub zdalnym, - Ambitne wyzwania wynikające z szybko rozwijającej się międzynarodowej firmy, - Możliwość pracy dla czołowej światowej firmy motoryzacyjnej i współpracy z jej kierownictwem, - Wymagające projekty dla największych marek motoryzacyjnych, - Możliwość tworzenia i wdrażania własnych rozwiązań biznesowych w zakresie zarządzania zespołem i rozwoju biznesu, - Ścieżka kariery oparta na kulturze wzrostu wraz z organizacją, - Uczciwa i otwarta komunikacja, - Niekorporacyjna atmosfera i obowiązki, które zapobiegają wpadnięciu w rutynę.	\N	\N	\N	\N	\N	2022-04-20 00:00:00+02	2025-11-21 00:00:00+01	f	f	590	703	658	\N	\N	18
441	Programista C++	Jako członek zespołu będziesz rozwijać komponenty oprogramowania pośredniczącego, umożliwiające łączenie modułów systemu samochodowego i platformy z zapleczami OEM. Będziesz pracować w zwinnym zespole programistycznym, projektować, wdrażać, integrować i testować swój kod. Będziesz częścią większej organizacji programistycznej z setkami innych inżynierów oprogramowania.	\N	\N	\N	\N	\N	2022-04-20 00:00:00+02	2025-11-21 00:00:00+01	f	f	590	703	659	\N	\N	18
475	Monter instalacji światłowodowych	Instalacja i konfiguracja sieci światłowodowych u klientów. Diagnozowanie i usuwanie awarii sieci światłowodowych. Wykonywanie pomiarów i testów sieci światłowodowych. Praca w terenie. Dbanie o wysoką jakość wykonywanych prac.	\N	\N	\N	f	f	2025-10-11 00:00:00+02	2025-11-17 00:00:00+01	f	f	590	739	693	\N	\N	18
442	Przedstawiciel Handlowy Netia	Netia S.A. zatrudni 2 przedstawicieli na terenie Poznania i okolic. Oferuje atrakcyjny system premiowy, pracę w młodym, dynamicznym zespole, nielimitowane połączenia oraz smsy na telefonie służbowym, pełne szkolenie wdrożeniowo-produktowe, zatrudnienie bezpośrednio w Netii oraz materiały BTL. Do obowiązków należy pozyskiwanie nowych klientów, aktywna sprzedaż produktów Netii na wyznaczonym terenie (klient indywidualny oraz SOHO), kolportaż materiałów BTL i dbanie o dobry wizerunek firmy. Od kandydata oczekuje się pozytywnego nastawienia, bardzo dobrej komunikatywności, pełnego zaangażowania, mile widziane doświadczenie w sprzedaży bezpośredniej oraz organizacji czasu pracy od poniedziałku do piątku.	\N	\N	\N	f	f	2023-02-02 00:00:00+01	2025-11-19 00:00:00+01	f	f	590	706	660	\N	\N	18
443	Serwisant sprzętu Apple Serwis MacHelp	MacHelp - profesjonalny serwis urządzeń elektronicznych. Specjalizujemy się w naprawach wszystkich urządzeń Apple: iPhone, iPad, Apple Watch, MacBook, iMac, Mac mini etc. Do naszego zespołu poszukujemy Serwisanta sprzętu Apple tj. osoby odpowiedzialnej za diagnostykę i naprawę urządzeń Apple (iPhone, iPad, MacBook, Apple Watch, Mac mini, iMac) oraz obsługę klienta (kontakt telefoniczny oraz face to face ). Wymagania: doświadczenie w naprawie elektroniki użytkowej, zdolności manualne, dobra organizacja stanowiska pracy, kultura osobista na wysokim poziomie, punktualność, sumienność, dokładność, uczciwość, komunikatywność, umiejętność szybkiego podejmowania decyzji oraz rozwiązywania problemów, odpornośc na stres oraz gotowość do pracy pod presją czasu, znajomość na wysokim poziomie oprogramowaniem iOS, macOS, watchOS. Mile widziane są: doświadczenie w zakresie lutowania elementów SMD oraz BGA; umiejętności czytania schematów elektronicznych; doświadczenie na w/w albo podobnym stanowisku, doświadczenie w użytkowaniu sprzętu Apple, znajomość języków obcych. Obowiązki: diagnozowanie i naprawa urządzeń Apple, obsługa klienta online oraz face to face, rejestracja i przygotowanie dokumentacji dla Klienta w zakresie przeprowadzonej naprawy, diagnostyka usterek, naprawa urządzeń iPhone, iPad, Apple Watch, MacBook, iMac etc., dbanie o wizerunek firmy. Praca min. 5 dni w tygodniu 8h dziennie. Oferujemy: kontakt z najnowszymi produktami marki Apple, możliwość dalszego rozwoju, miłą atmosferę w młodym i dynamicznie rozwijającym się Zespole, atrakcyjne wynagrodzenie oraz system motywacyjny, uzależnione od doświadczenia i zaangażowania.	\N	\N	\N	f	f	2022-11-16 00:00:00+01	2025-11-20 00:00:00+01	f	f	590	707	661	\N	\N	18
444	Pracownik budowy sieci teletechnicznych / Spawacz światłowodów / Operator koparki	Zatrudnimy osoby do budowy sieci teletechnicznych (mile widziane doświadczenie w łączeniu kabli, ale również bez doświadczenia do prac bardzo prostych jak układanie rurek, list PCV, wciąganie kabli w kanalizacji teletech. itd.), absolwentów szkół elektronicznych, elektrycznych do spawania światłowodów (może być bez doświadczenia, tylko chęci) oraz osobę z prawem jazdy B+E lub C+E do przyuczenia, a następnie wysłania na koszt firmy na uprawnienie do pracy na mini koparce lub operatora koparki z B+E. Studentów do prac pomocniczych w okresie wakacyjnym.	\N	\N	\N	f	f	2022-02-11 00:00:00+01	2025-11-19 00:00:00+01	f	f	590	701	662	\N	\N	18
445	Specjalista ds. wdrożeń systemów WMS	Jeśli masz doświadczenie w kierowaniu projektami wdrożeniowymi systemów ERP, WMS lub MES i rozważasz zmianę pracy – ta rekrutacja może być dla Ciebie!Jeśli poszukujesz nowych projektów, ciekawych wyzwań i pełnego zaplecza (merytoryczno-szkoleniowego) ta rekrutacja jest dla Ciebie. Jeśli czujesz, że to może być kolejny krok w Twojej karierze – aplikuj!WYMAGANIA względem Kandydata:•\tKilkuletnie doświadczenie w samodzielnym prowadzeniu projektów wdrożeniowych, wdrażaniu i konfigurowaniu oprogramowania klasy ERP, WMS lub MES w bezpośredniej współpracy z Klientami•\tWiedza techniczna na temat funkcjonowania przedsiębiorstw w obszarze logistyki i zarządzania magazynem lub produkcji•\tDużym plusem będzie znajomość procesów biznesowych związanych z funkcjonowaniem przedsiębiorstw w obszarze workflow – obieg dokumentów•\tMile widziana znajomość konfiguracji relacyjnych baz danych MS SQL Server lub znajomość języka SQL•\tWysokie umiejętności organizacyjne, odpowiedzialność i nastawienie na osiąganie rezultatów•\tPrawo jazdy kat. B•\tDyspozycyjność i gotowość do odbywania podróży służbowych – przeprowadzanie wdrożeń na miejscu u KlientaZADANIA na stanowisku:•\tProwadzenie projektów wdrożeniowych systemów ERP/WMS, w tym koordynacja prac zespołów projektowych•\tBliska współpraca z Klientami: analiza potrzeb, planowanie, realizacja wdrożenia oraz rozwój funkcjonalności systemu•\tProwadzenie projektów, monitorowanie postępów, zarządzanie budżetami i harmonogramami•\tInicjowanie nowych usprawnień w istniejących rozwiązaniach•\tPrzeprowadzanie integracji między systemami•\tTestowanie wdrażanych rozwiązań•\tTworzenie dokumentacji technicznej•\tPrzeprowadzanie migracji danych, konfigurowanie baz danych•\tRaportowanie postępów projektów i dbanie o terminową realizację zadań•\tŚcisła współpraca z zespołem IT, manageramiOFERTA zatrudnienia:•\tStabilne wynagrodzenie podstawowe na start od 10 000 zł do 15 000 zł netto + VAT (B2B) (uzależnione od posiadanego doświadczenia i umiejętności) lub ekwiwalent na UoP•\tSystem premiowy•\tRealny wpływ na kształt i przebieg prowadzonych projektów•\tSamochód służbowy z możliwością prywatnego użytku po wypracowaniu odpowiedniego stażu w firmie i posiadaniu odpowiednich wyników•\tMożliwość rozwoju zawodowego•\tMożliwość pracy w trybie zdalnym 1-2 dni w tygodniu (po okresie wdrażania na nowe stanowisko, podczas okresu próbnego preferowana praca na miejscu w biurze)•\tPełen dostęp do nowoczesnej technologii, a także możliwość podnoszenia swoich kwalifikacji•\tBenefity pozapłacowe	10000.00	15000.00	f	\N	\N	2025-09-16 00:00:00+02	2025-11-16 00:00:00+01	f	f	590	709	663	1	9	18
446	Informatyk	Zakres obowiązków:1.\tZarządzanie Microsoft 365 – zapewnienie sprawnego działania podstawowych usług chmurowych (Exchange, Teams, OneDrive), co obejmuje m.in.:-\tzarządzanie użytkownikami i grupami,-\tzarządzanie serwerem pocztowym Exchange Online,-\tzarządzanie Microsoft Teams,-\tzapewnienie odpowiedniego poziomu bezpieczeństwa zasobów w chmurze.2.\tZarządzanie pakietami Microsoft Office,3.\tObsługa urządzeń drukujących:- zamawianie tonerów,- tworzenie kont użytkowników,- usuwanie usterek / nadzorowanie postępu prac serwisowych.4.\tAktualizacja systemów operacyjnych5.\tTechniczna obsługa sprzętu komputerowego6.\tProwadzenie dokumentacji / inwentaryzacja oprogramowania używanego w IKiFP PAN7.\tWsparcie dla użytkowników końcowych, co obejmuje m.in.:-\tprzyjmowanie zgłoszeń od użytkowników za pomocą poczty elektronicznej lub telefonicznie (w godzinach pracy),-\tdiagnozę usterek sprzętu komputerowego,-\tpilotowanie napraw gwarancyjnych i pogwarancyjnych,-\twsparcie przetargów na sprzęt komputerowy – przygotowanie specyfikacji, weryfikacja ofert,-\tzapewnienie bezpieczeństwa danych.Nasze wymagania•\tco najmniej rok doświadczenia na podobnym stanowisku•\twykształcenie wyższe informatyczne lub techniczne (również w trakcie studiów)•\tznajomość systemów operacyjnych Microsoft•\twiedza dot. sprzętu komputerowego oraz umiejętność jego konfiguracji i diagnostyki•\tznajomość budowy i działania elementów infrastruktury informatycznej•\tznajomość podstaw budowy sieci komputerowych•\tMS Office / MS Office 365•\tzaangażowanie, sumienność oraz dokładność w wykonywaniu zadańMile widziane•\tZainteresowanie technologiami IT.•\tZnajomość sprzętu komputerowego w jego budowie i instalowaniu oprogramowania.•\tUmiejętność pisania instrukcji technicznych i dokumentacji opisowej.To oferujemy•\tstabilna praca•\tdodatki stażowe i nagrody jubileuszowe•\tbycie częścią instytucji, która realizuje swoje projekty na ponad 50 rynkach świata i cały czas się rozwija•\tpracę pełną wyzwań, inspirujące środowisko pracy oraz możliwość wdrażania swoich pomysłów•\trealny wpływ na rozwój firmy, możliwość rozwoju zawodowego i zdobycia nowych doświadczeń•\tdofinansowanie zajęć sportowych dla Ciebie i Twojej rodziny•\tkarnety teatralne	\N	\N	\N	f	f	2025-02-12 00:00:00+01	2025-11-21 00:00:00+01	f	f	590	710	664	\N	\N	18
447	Monter / Telemonter (Bydgoszcz)	Prace przy budowie kanalizacji teletechnicznych, układanie telekomunikacyjnych linii kablowych, montaż szafek telekomunikacyjnych, montaż tras kablowych w obiektach (koryta, rury PCV), układanie wewnątrz budynkowej instalacji kablowej, praca lokalnie lub w delegacji, system pracy od poniedziałku do piątku jednozmianowy.	\N	\N	\N	f	f	2024-10-21 00:00:00+02	2025-11-20 00:00:00+01	f	f	590	711	665	\N	\N	39
448	Zatrudnimy elektryka	Firma Chimide Polska Sp. z o.o., poszukuje elektryka z doświadczeniem, najchętniej z okolic Łodzi. Mile widziana dyspozycyjność (praca w delegacjach), prawo jazdy kat. B. Warunki płacowe do uzgodnienia. Możliwość stałego zatrudnienia na umowę o pracę. Prosimy o nie wysyłanie CV, tylko bezpośredni kontakt telefoniczny: Piotr	\N	\N	\N	f	f	2022-02-11 00:00:00+01	2025-11-21 00:00:00+01	f	f	590	712	666	\N	\N	13
449	Partner / Doradca Biznesowy PLAY	Dołącz do zespołu Play Dreams. Rozwijamy specjalistów w obszarze profesjonalnego doradztwa w zakresie usług telekomunikacyjnych. W związku z dynamicznym rozwojem podjęliśmy decyzję o powiększeniu obecnie działającej sieci sprzedaży B2B. Zakres obowiązków: Aktywna sprzedaż produktów i usług sieci Play, pozyskiwanie nowych oraz obsługa obecnych klientów biznesowych, negocjowanie warunków sprzedaży i przygotowywanie umów, doradztwo w zakresie wyboru usług.	\N	\N	\N	\N	\N	2024-01-03 00:00:00+01	2025-11-21 00:00:00+01	f	f	590	713	667	\N	\N	18
450	Handlowiec - Dział oprogramowania ERP	Do działu handlowego poszukujemy osoby o szerokim spojrzeniu na użyteczność technologii w handlu i biznesie – począwszy od oprogramowania, a kończąc na sprzęcie elektronicznym. Do Twoich zadań należeć będzie: Aktywne poszukiwanie klientów potrzebujących wsparcia Współpraca z działam Wdrożeniowym w celu efektywnego tworzenia i realizacji strategii przed- i po sprzedażowej. Zarażanie zespołu swoją pasją i ciekawymi pomysłami.	\N	\N	\N	f	f	2022-11-16 00:00:00+01	2025-11-19 00:00:00+01	f	f	590	714	668	\N	\N	18
451	Praca dla elektromontera	Studio Arcan Vision poszukuje pracowników na stanowisko: Elektromonter instalacji niskoprądowych Zakres prac elektromontera to:Montaż i konfiguracja instalacji:- RTV-SAT- instalacji wzmocnienia GSM, sieci LAN, WLAN, LTE- instalacji alarmowych- monter systemów monitoringu CCTVZakres obowiązków:- dostawa oraz montaż u klienta ww. systemów- serwis ww. systemów	\N	\N	\N	f	f	2022-11-16 00:00:00+01	2025-11-19 00:00:00+01	f	f	590	715	669	\N	\N	13
452	Alpinista / Monter	Instalacje na wysokości związane z budową stacji UMTS/GSM, instalacja radiolinii, torów antenowych, uruchamianie stacji, prace budowlane.	\N	\N	\N	f	f	2022-02-11 00:00:00+01	2025-11-21 00:00:00+01	f	f	590	716	670	\N	\N	18
453	Specjalista ds. bezpieczeństwa IT	Do zadań zatrudnionej osoby będzie należało: - obsługa spraw związanych z systemem zarządzania bezpieczeństwem informacji (incydenty, zmiany, zapewnienie ciągłości, podnoszenie świadomości), - nadzór nad przestrzeganiem systemu zarządzania bezpieczeństwem informacji, - aktualizacja dokumentacji SZBI Kandydat powinien posiadać wiedzę: - znajomość dobrych praktyk w dziedzinie bezpieczeństwa informacji, - znajomość architektury systemów informatycznych, - znajomość urządzeń i rozwiązań wykorzystywanych do zabezpieczania systemów informatycznych Pierwsza umowa na okres próbny 6 mc-y. Szczegóły ogłoszenia pod poniższym linkiem: lub na stronie bip.bialystok.pl w zakładce "postępowania-&gt;nabór na stanowiska urzędnicze"	\N	\N	\N	f	f	2022-02-11 00:00:00+01	2025-11-19 00:00:00+01	f	f	590	717	671	\N	\N	7
454	Projektant sieci telekomunikacyjnych	Firma telekomunikacyjna ELKATEL SYSTEM zatrudni osobę kreatywną, z inicjatywą na stanowisko projektanta sieci telekomunikacyjnych. Zakres obowiązków: Opracowywanie dokumentacji projektowej - budowlanej, wykonawczej i powykonawczej - sieci telekomunikacyjnych, światłowodowych i miedzianych, zewnętrzynych i budynkowych, w różnych technologiach i architekturach, Opracowywanie koncepcji technicznych, schematów instalacyjnych, przedmiarów robót, zestawień materiałowych, Pozyskiwanie uzgodnień urzędowych i prywatnych, opinii, decyzji i pozwoleń, niezbędnych do uzyskania prawa budowy zaprojektowanej sieci, zarówno w ramach procedur administracyjnych, jak i prawa drogi od właścicieli, Współpraca z przedstawicielami inwestora w zakresie uzgodnień technicznych lub zarządców nieruchomości projektu, Wsparcie techniczne i merytoryczne wykonawcy na etapie realizacji prac budowlanych i instalacyjnych w terenie.	\N	\N	\N	f	f	2022-02-11 00:00:00+01	2025-11-20 00:00:00+01	f	f	590	718	672	\N	\N	18
455	Webmaster w sklepie internetowym + EBAY.DE + AMAZON	Dynamicznie rozwijająca się firma na rynku E-COMMERCE specjalizująca się w branży urządzeń dla biznesu i przemysłu poszukuje zdolnej i ambitnej osoby na stanowisko WEBMASTER do obsługi i tworzenia sklepu internetowego HurtowniaPrzemyslowa.pl oraz działań związanych ze sprzedażą w internecie. W ramach tworzonego stanowiska oferujemy ciekawą pracę w młodym i dynamicznym zespole, brak monotonii oraz możliwość rozwoju zawodowego. Do atutów tworzonego stanowiska należeć będą elastyczne godziny pracy, oficjalna forma zatrudnienia oraz możliwość szybkiego awansu zawodowego. Do obowiązków osoby zatrudnionej na stanowisku webmaster-a będzie należało: - tworzenie i uzupełnianie treści stron internetowych - optymalizacja stron internetowych pod kątem wyszukiwarek - zamieszczanie aukcji internetowych w serwisach EBAY / AMAZON - tworzenie i zamieszczanie treści w portalach społecznościowych np. FACEBOOK - tworzenie i zamieszczanie treści na forach internetowych i w blogu Idealny kandydat powinien mieć biegłe umiejętności i wiedzę w zakresie: - posługiwanie się językiem niemieckim w mowie i piśmie - znajomość i doświadczenie w obsłudze eBay.de oraz Amazon - podstawowa znajomość HTML, CSS - umiejętność poprawnego tworzenia treści w języku niemieckim - znajomość programów graficznych i umiejętność tworzenia i dostosowywania grafik na potrzeby stron www - znajomość programu EXCEL Dodatkowymi atutami będą: - dobra znajomość języka angielskiego	\N	\N	\N	f	f	2022-02-11 00:00:00+01	2025-11-19 00:00:00+01	f	f	590	719	673	\N	\N	18
456	Business Development Manager	Spółka OutsourcingIT LTD specjalizuje się w outsourcingu IT, kompleksowej obsłudze projektów IT (w tym Body Leasing) oraz działalności inwestycyjnej. W zakresie outsourcingu IT dostarczamy zespoły IT dla spółek w Polsce i za granicą, w tym między innymi dla takich klientów jak: PKN Orlen, Raiffeisen Polbank czy PZU. Nasze usługi obejmują również kompleksowe projektowanie i wdrożenie dedykowanych rozwiązań software &amp; eBusiness. Tworzymy zespół ludzi o wysokich kwalifikacjach zawodowych z rożnych komplementarnych dziedzin, powiązanych z biznesem i IT, co pozwala nam na realizację nawet bardzo wymagających projektów. W związku z dynamicznym rozwojem, poszukujemy osoby na stanowisko: Business Development Manager Miejsce pracy: Poznań oraz częściowo zdalnie. Osoba odpowiedzialna za aktywne rozwijanie kanałów sprzedażowych w zakresie outsourcingu zespołów IT, w tym szczególnie pozyskiwanie nowych klientów w Polsce i za granicą oraz utrzymywanie relacji z obecnymi klientami. Zakres odpowiedzialności: - Działania handlowe – aktywna sprzedaż usług outsourcingu zespołów IT – Polska i zagranica. - Aktywny rozwój kanałów przychodowych spółki w Polsce i za granicą. - Pozyskiwanie aktywnych Klientów dla Portalu: OutsourcingIT.co.uk - Planowanie i przeprowadzanie działań Business Development oraz dokładna analiza ich efektów. - Przygotowywanie: ofert, kosztorysów, umów, przy współpracy z zespołem spółki. - Negocjowanie i zawieranie kontraktów handlowych z klientami. - Inicjowanie i podejmowanie działań zmierzających do maksymalizacji wyników spółki.	\N	\N	\N	\N	\N	2022-02-11 00:00:00+01	2025-11-21 00:00:00+01	f	f	590	720	674	\N	\N	18
457	Pracownik do budowy sieci telekomunikacyjnej	Poszukujemy osoby z doświadczeniem w budowie i rozbudowie sieci telekomunikacyjnej na podbudowie słupowej oraz doziemnej. Praca w dynamicznym i zgranym zespole. Umowa o pracę po okresie próbnym. Dodatkowe walory to możliwość skorzystania z pakietów takich jak MultiSport, Opieka medyczna, ubezpieczenie na życie. Praca na terenie Krakowa i okolic. Mile widziane prawo jazdy kat.B.	\N	\N	\N	f	f	2025-05-29 00:00:00+02	2025-11-17 00:00:00+01	f	f	590	721	675	\N	\N	39
458	Inżynier Sprzedaży i Serwisu IT	Zakres obowiązków:•sprzedaż towarów i usług, doradztwo Klientom indywidualnym i firmom z zakresu IT,•prowadzenie działalności marketingowej firmy, przygotowywanie akcji promocyjnych, aktualizacja strony internetowej,•prowadzenie prac serwisowych w siedzibie firmy oraz u Klienta,•troska o ład i porządek w siedzibie firmy,Oferujemy•ciekawą pracę i atrakcyjne wynagrodzenie,•szkolenia i rozwój zawodowy,•miłą atmosferę i bardzo dobre warunki pracy,Wymagania•wykształcenia wyższego lub średniego•znajomości technik sprzedaży i podstawowych umiejętności handlowych,•dobrej znajomości zagadnień i rynku IT,•wiedzy pozwalającej na właściwy dobór i konfigurację sprzętu IT,•umiejętności instalacji i konfiguracji sprzętu, oprogramowania OS oraz aplikacji,•umiejętności podstawowych napraw elektronicznych,•umiejętności aktualizowania strony internetowej,•znajomości oprogramowania zarządzania treścią,•znajomość obsługi oprogramowania pakietów biurowych, programów graficznych•podstawowej znajomości j. angielskiego•prawo jazdy kat. B•wysokiej kultury osobistej, •Poszukujemy osób ambitnych, pracowitych i uczciwych, gotowych na nowe wyzwania,•Osoby zainteresowane prosimy przesyłać zgłoszenia w postaci CV ze zdjęciem	\N	\N	\N	f	f	2022-02-11 00:00:00+01	2025-11-19 00:00:00+01	f	f	590	722	676	\N	\N	18
459	Praktyki Informatyk	Praktyki w dziale bieżącego wsparcia technicznego klientów; Rozwiązywanie problemów technicznych na podstawie powierzonych zadań; Monitorowanie logów i alarmów; Obsługa systemu zgłoszeń	\N	\N	\N	f	f	2022-02-11 00:00:00+01	2025-11-20 00:00:00+01	f	f	590	723	677	\N	\N	18
460	Serwisant/Monter Infrastruktury Telekomunikacyjnej i Elektrycznej	Solutions 30 Wschód jest częścią międzynarodowej grupy Solutions 30. Świadczymy usługi telekomunikacyjne w zakresie utrzymania i budowy sieci miedzianych oraz światłowodowych. Obecnie zatrudniamy ponad 600 osób we wschodniej Polsce – od Suwałk po Zamość. Polegając na naszej rozległej sieci profesjonalnych inżynierów serwisowych, jesteśmy w stanie dotrzeć do klientów w krótkim czasie i zadbać o to, aby technologia działała dla nich tak, jak powinna.Solutions 30 jest liderem swojej branży na rynku europejskim. Dzięki rozległej sieci wsparcia oraz optymalnemu systemowi i procesom zarządzania tą siecią, dostarczamy odpowiednie rozwiązania dla problemów technologicznym klientów, czyniąc to zarówno efektownie, jak i efektywnie. W związku z dynamicznym rozwojem obecnie poszukujemy kandydatów na stanowisko: Serwisant /Monter Infrastruktury Telekomunikacyjnej i Elektrycznej Miejsce pracy: Garwolin, Wyszków, Sokołów Podlaski ,Mińsk Mazowiecki ,Siedlce Twoje zadania: -wykonywanie prac instalacyjnych nowo budowanych kabli telekomunikacyjnych i elektrycznych -wykonywanie obiektów infrastruktury telekomunikacyjnej -wykonywanie instalacji w technologii FTTH -wykonywanie pomiarów budowlanych instalacji telekomunikacyjnych -wykonywanie prac konserwacyjnych na elementach sieci -techniczna obsługa klientów w zakresie usuwania uszkodzeń, wykonywania instalacji i aktywizacji usług telekomunikacyjnych (sieć miedziana i światłowodowa) Oczekujemy: - znajomości sieci dostępowej (budowa i eksploatacja); atutem będzie umiejętność wykonywania pomiarów elektrycznych i szerokopasmowych,- zdolności do pracy na wysokości pow. 3m,- prawa jazdy kat. B,(warunek konieczny )- dodatkowym atutem będzie prawo jazdy kat E+B oraz uprawnienia innych maszyn (np. HDS lub koparki) czy uprawnienia SEP.	\N	\N	\N	f	f	2022-02-11 00:00:00+01	2025-11-17 00:00:00+01	f	f	590	724	678	\N	\N	18
461	Logistyk/Magazynier	Telekom Usługi Sp z o.o. z siedzibą w Gdańsku, jest częścią międzynarodowej grupy Solutions 30. Świadczymy usługi telekomunikacyjne w zakresie utrzymania i budowy sieci miedzianych oraz światłowodowych. Spółka powstała w 2011, obecnie zatrudniamy ponad 300 osób. Naszym kluczowym partnerem jest Orange Polska. Tworzymy zespół specjalistów wysokiej klasy, wyróżnia nas wysoki standard usług i profesjonalizm w podejściu do Klienta. Solutions 30 jest liderem swojej branży na rynku europejskim. Dzięki rozległej sieci wsparcia oraz optymalnemu systemowi i procesom zarządzania tą siecią, dostarczamy odpowiednie rozwiązania problemów technologicznych klientów. Posiadamy certyfikat: Firma Godna Zaufania 2021W związku z dynamicznym rozwojem obecnie poszukujemy kandydatów na stanowisko: LOGISTYK/MAGAZYNIER Miejsce pracy: GDAŃSK Jesteś kandydatem, którego poszukujemy, jeśli posiadasz:-wykształcenie średnie lub wyższe (mile widziane podstawy : elektryczne lub  telekomunikacyjne),-posiadasz umiejętność pracy w grupie,-sumiennie wykonujesz powierzone zadania -znajomość obsługi elektronarzędzi (mile widziana )-prawo jazdy kat. B (warunek konieczny)-posiadasz min.1 rok doświadczenia na wymaganym stanowiskui dodatkowo jesteś osobą:-pracowitą,-samodzielną a dokładność jest Twoją mocną stroną. Będziesz odpowiedzialny/a za:-przyjmowanie dostaw -wraz z kontrolą ilościową i jakościową,-kompleksową obsługę klientów w obszarze realizacji zamówień ,-prowadzenie bieżącej kontroli nad stanem magazynu oraz utrzymanie porządku w miejscu pracy ,-sporządzanie listy zakupów oraz zamawiania dla potrzeb firmy(telekomunikacja)	\N	\N	\N	f	f	2022-02-11 00:00:00+01	2025-11-21 00:00:00+01	f	f	590	725	679	\N	\N	26
462	Rekruter IT/Finanse	Poszukujemy zmotywowanego rekrutera IT/Finanse do naszego zespołu. Osoba na tym stanowisku będzie odpowiedzialna za zarządzanie pełnym cyklem procesów rekrutacyjnych i pozyskiwanie nowych klientów, pełniąc rolę zaufanego doradcy HR. Głównym obszarem zainteresowania będą stanowiska umysłowe, szczególnie w branży farmaceutycznej i sprzętu medycznego.	\N	\N	\N	\N	t	2025-10-22 00:00:00+02	2025-11-18 00:00:00+01	f	f	590	726	680	\N	\N	18
463	Poszukujemy podwykonawców - infrastruktura telekomunikacyjna	Firma PRT BAZA Sp. z o.o. Sp. k. w Toruniu poszukuje podwykonawców w zakresie budowy infrastruktury telekomunikacyjnej. Nawiążemy współpracę z firmami podwykonawczymi na terenie całej Polski z doświadczeniem w zakresie prac budowlanych, elektrycznych i instalacyjnych. Wykonujemy następujące prace: Prace ziemne/fundamentowe Prace elektryczne niskiego i wysokiego napięcia Prace budowlane montażowe konstrukcji stalowych Prace instalacyjne sieci telekomunikacyjnych i sprzętu telekomunikacyjnego niskiego napięcia Oferujemy długotrwałą współpracę. Kontakt przez naszą stronę internetową: , lub telefoniczny	\N	\N	\N	f	f	2022-02-11 00:00:00+01	2025-11-21 00:00:00+01	f	f	590	716	681	\N	\N	18
464	Trener do prowadzenia kursu komputerowego dla seniorów	Szukamy osoby do prowadzenia kursów komputerowych dla seniorów w okolicach Rzeszowa. Miejscowość: HyżneLiczba osób:15Liczba godzin do zrealizowania 30 h czyli 8 spotkań. Zajęcia w piątki od 8-12.	\N	\N	\N	f	f	2025-09-17 00:00:00+02	2025-11-18 00:00:00+01	f	f	590	728	682	\N	\N	9
465	Doradca Klienta ds. Kredytów Frankowych	We współpracy z Votum Robin Lawyers S.A. pomagamy osobom z kredytem frankowym w walce z bankami. Zatrudniamy doradców klienta i menadżerów w całej Polsce. Oferujemy wynagrodzenie prowizyjne bez górnej granicy, umowę o współpracę (tzw. umowa B2B lub umowa zlecenie), rozbudowany pakiet szkoleń, pełne wsparcie merytoryczne, jasne zasady współpracy i przejrzystą ścieżkę kariery, udział w konkursach sprzedażowych z atrakcyjnymi nagrodami, wsparcie w dotarciu do Klienta, możliwość przystąpienia do programu Multisport.	\N	\N	\N	t	\N	2022-12-24 00:00:00+01	2025-11-20 00:00:00+01	f	f	590	729	683	\N	\N	725
466	Programista PHP7 / Informatyk Kohana - Dodatkowe Zlecenia	Nawiążemy współpracę z doświadczonym Programistą/ Informatykiem znającym Kohana php7 na zasadzie dodatkowego zlecenia (CRM dla firmy). Wymagania: doświadczenie i znajomość języków programowania Kohana php7, doświadczenie w programowaniu CRM, wiedza i doświadczenie w programowaniu CMSów. Dodatkowe Inne Zlecenia mogą dotyczyć także: wykonania drobnych poprawek na istniejącej stronie www, umieszczenie kodu, utworzenie i podpięcie formularza kontaktowego, umieszczenie na serwerze dedykowanego formularza lub ankiety do wyplenienia, podpięcie możliwości płatności online za kurs na www, programowania np. stworzenia formularza online z pytaniami, PROGRAMOWANIA CMSa, stworzenia grafiki lub banera z umieszczeniem go na www, utworzenia nowej responsywnej strony internetowej dla szkoleń. Oferujemy: stałą współpracę i stałe dodatkowe zlecenia, miłą atmosferę współpracy, terminowe wynagrodzenia. Prosimy o zgłoszenia mailem na adres: biuro(at)lingua-house.pl. Prosimy o dołączenie klauzuli o przetwarzaniu danych osobowych. Odpowiemy na wybrane oferty.	\N	\N	\N	f	f	2022-11-16 00:00:00+01	2025-11-17 00:00:00+01	f	f	590	730	684	\N	\N	18
467	Programista	Projektowanie, rozwój i utrzymanie oprogramowania. Pisanie czystego, wydajnego i dobrze udokumentowanego kodu. Współpraca z zespołem projektowym w celu określenia wymagań i specyfikacji. Testowanie i wdrażanie oprogramowania. Monitorowanie wydajności aplikacji i wprowadzanie optymalizacji. Analiza danych i generowanie raportów. Rozwiązywanie problemów i usuwanie błędów w oprogramowaniu.	8000.00	16000.00	\N	f	f	2025-04-12 00:00:00+02	2025-11-18 00:00:00+01	f	f	590	731	685	1	9	18
468	Inżynier Sprzedaży	Ze względu na rozwój, Deeplai poszukuje pracowników na stanowisko Inżyniera Sprzedaży. Zakres obowiązków: • bezpośrednie działania sprzedażowe na określonym terytorium, międzynarodowe • Zarządzanie Partnerami Biznesowymi, w tym wdrażanie procesu autoryzacji • zaangażowanie marketingowe aktywne zaangażowanie skoncentrowane na potrzebach kanału sprzedaży • współpraca z innymi działami DEEPLAI w procesie wdrażania produktu • budowanie i promowanie silnych, długotrwałych relacji z klientami poprzez współpracę z nimi i zrozumienie ich potrzeb • osiąganie wzrostu i realizacja celów sprzedażowych poprzez skuteczne zarządzanie kanałami sprzedaży Nasze wymagania: • licencjat lub magister • bardzo dobra znajomość języka angielskiego • bardzo dobra i sprawdzona wiedza i doświadczenie w zakresie umiejętności sprzedażowych • analityczne myślenie w zrozumieniu wymagań i problemów klienta • umiejętność współpracy z innymi i dobre umiejętności komunikacyjne Czego możesz od nas oczekiwać: • praca w zespole inspirowanym i zafascynowanym innowacjami • przyjazna atmosfera, codzienna współpraca z ludźmi o otwartych umysłach • różnorodne, ciekawe projekty w najnowocześniejszych technologiach • realny osobisty wpływ na realizowane projekty • pakiet wynagrodzeń adekwatny do Twojego doświadczenia • elastyczne godziny pracy Aby aplikować, prześlij nam swoje CV. Proszę dodać klauzulę: „Wyrażam zgodę na przetwarzanie danych osobowych zawartych w tym dokumencie w celu realizacji procesu rekrutacji przez Deeplai P.S.A., Al. Kraśnicka 31, 20-718 Lublin, zgodnie z ustawą o ochronie danych osobowych z dnia 10 maja 2018 r. (Dz.U. 2018, poz. 1000) oraz zgodnie z rozporządzeniem Parlamentu Europejskiego i Rady (UE) 2016/679 z dnia 27 kwietnia 2016 r. w sprawie ochrony osób fizycznych w związku z przetwarzaniem danych osobowych i w sprawie swobodnego przepływu takich danych oraz uchylenia dyrektywy 95/46/WE (ogólne rozporządzenie o ochronie danych) Twoje dane osobowe będą przetwarzane przez Deeplai P.S.A. z siedzibą w Lublinie, Al. Kraśnicka 31, 20-718 Lublin, wyłącznie w celu przeprowadzenia procesu rekrutacji. W każdej chwili możesz zażądać usunięcia lub zmiany danych, wysyłając wiadomość e-mail, którą znajdziesz na naszej stronie internetowej.	\N	\N	\N	f	f	2023-07-06 00:00:00+02	2025-11-17 00:00:00+01	f	f	590	732	686	\N	\N	18
469	Specjalista ds. inwestycji telekomunikacyjnych (przyuczymy studenta)	Firma Telekomunikacyjna z kilkudziesięcioletnim doświadczeniem w branży zatrudni kandydata z doświadczeniem na stanowisko:Sp. ds. INWESTYCJI TELEKOMUNIKACYJNYCH/ ŚWIATŁOWODOWYCH Obowiązki:•\tPrzygotowanie, nadzorowanie i koordynowanie prac inwestycyjnych•\tProwadzenie dokumentacji wykonawczej, przygotowanie dokumentacji podwykonawczej•\tPrzygotowanie kosztorysów, analiza dokumentów •\tKontakt z zarządami dróg w zakresie pozwoleń na zajęcia pasa drogowego•\tKontakt i budowanie relacji z innymi jednostkami organizacyjnymi zaangażowanymi w proces inwestycji światłowodowych•\tRaportowanieOczekiwania:•\tDoświadczenie na podobnym stanowisku mile widziane, ale nieobowiązkowe (możemy przyuczyć)•\tZnajomość programów CAD 2D, MS Office i QGIS•\tPrawo jazdy kat. B•\tSamodzielność, skrupulatność•\t Dobra organizacja pracy Oferujemy •\tStałą, długoterminową pracę (umowa o pracę lub B2B w zależności od Twoich preferencji)•\tAtrakcyjne wynagrodzenie •\tBogaty pakiet świadczeń•\tMożliwości rozwoju i awansu w strukturach firmy•\tMożliwość podnoszenia kwalifikacji, możliwość zdobycia dodatkowych uprawnień•\tMożliwość udziału w szkoleniach i kursach•\tWszelkie narzędzia niezbędne do wykonywania pracy na tym stanowisku•\tSamodzielne stanowisko pracyJeżeli jesteś zainteresowany rozwojem zawodowym w branży telekomunikacyjnej i jesteś otwarty na naukę, zdobywanie wiedzy to przyślij swoje CV a my zadzwonimy do Ciebie ze szczegółami. Uprzejmie prosimy o zamieszczenie klauzuli:Oświadczam, że zgodnie z przepisami rozporządzenia Parlamentu Europejskiego i Rady (UE) 2016/679 z dnia 27 kwietnia 2016 r. w sprawie ochrony osób fizycznych w związku z przetwarzaniem danych osobowych i w sprawie swobodnego przepływu takich danych oraz uchylenia dyrektywy 95/46/WE (RODO) wyrażam zgodę na przetwarzanie w procesie rekrutacji podanych przeze mnie dobrowolnie dodatkowych danych osobowych niebędących danymi, których pracodawca może żądać na podstawie przepisów prawa.	\N	\N	\N	f	f	2025-09-11 00:00:00+02	2025-11-18 00:00:00+01	f	f	590	733	687	\N	\N	18
470	Specjalista IT / INFORMATYK /	Firma e-integra sp. z o. o. poszukuje do pracy w Płocku kandydatów na stanowisko: Specjalista IT. Wymagania stawiane pracownikowi: • Wykształcenie średnie informatyczne lub pokrewne • Bardzo dobra znajomość systemu Windows (pozwalająca na diagnozowanie i rozwiązywanie problemów)• Bardzo dobra znajomość MS Office (pozwalająca na diagnozowanie i rozwiązywanie problemów)• Podstawowa znajomość zagadnień sieci komputerowych• Znajomość budowy komputera• Zdolność analitycznego myślenia i rozwiązywania problemów• Dobra organizacja czasu pracy, umiejętność pracy pod presją czasu• Umiejętność pracy w zespole. Do zadań należeć będzie:• Obsługa serwisowa klientów spółki• Dostarczanie i wymiana materiałów eksploatacyjnych• Serwisowanie urządzeń drukujących• Serwis gwarancyjny sprzętu komputerowego dla największych światowych producentów• Montaż dostawa sprzętu komputerowego• Realizacja bieżących zgłoszeń, pomoc zdalna• Monitorowanie środowiska serwerowego. Osoby zainteresowane propozycją pracy prosimy o przesyłanie aplikacji zawierających CV	\N	\N	\N	t	\N	2022-02-11 00:00:00+01	2025-11-20 00:00:00+01	f	f	590	734	688	\N	\N	18
471	Młodszy Koordynator ds. Marketingu i Sprzedaży	Szukamy energicznej i zmotywowanej osoby na stanowisko Młodszego Koordynatora ds. Marketingu i Sprzedaży, która będzie wspierać nasz dział sprzedaży i marketingu. Jeśli chcesz zdobywać doświadczenie w kontaktach z klientami, prospectingu oraz działaniach marketingowych, a przy tym interesujesz się technologiami webowymi i aplikacjami – to stanowisko jest dla Ciebie! Twoje zadania: Nawiązywanie kontaktu z potencjalnymi klientami (leadami) – telefonicznie i mailowo, Wsparcie działań sprzedażowych i marketingowych, Prospecting i aktywne poszukiwanie nowych możliwości biznesowych, Obsługa kanałów social media i wspieranie działań promocyjnych online, Współpraca z zespołem w zakresie przygotowywania ofert i materiałów marketingowych, Monitorowanie rynku i raportowanie wyników.	\N	\N	\N	\N	t	2025-10-14 00:00:00+02	2025-11-20 00:00:00+01	f	f	590	735	689	\N	\N	18
472	Instalator sieci	W związku z dynamicznym rozwojem firmy i powiększeniem Działu Eksploatacji Sieci, poszukujemy Instalatora Sieci. Zainteresowane osoby prosimy o wysyłanie swoich CV. Zakres obowiązków: wykonywanie instalacji oraz okablowania miedzianego i światłowodowego, budowa przyłączy, przewiertów, wykonywanie tras kablowych, montaż skrzynek, realizacja prac związanych z budową kanalizacji teletechnicznej oraz linii napowietrznych, konfiguracja urządzeń abonenckich (np. routery) Wymagania: doświadczenie w pracach monterskich, umiejętność szybkiego przyswajania wiedzy, zaangażowanie, dyspozycyjność i samodzielność, umiejętność pracy w zespole, prawo jazdy kat. B., doświadczenie w pracach przy sieciach światłowodowych będzie dodatkowym atutem, uprawnienia SEP będą dodatkowym atutem Oferujemy: zatrudnienie w oparciu o umowę o pracę, wynagrodzenie odpowiednie do kwalifikacji i osiąganych wyników, możliwość doskonalenia zawodowego, niezbędne narzędzia pracy, szkolenia wymagane do wykonywanych prac	\N	\N	\N	f	f	2022-11-16 00:00:00+01	2025-11-21 00:00:00+01	f	f	590	721	690	\N	\N	18
473	Informatyk do audytów w zakresie ochrony danych osobowych	Informatyk do audytów z zakresu ochrony danych osobowych wg. RODO w Katowicach i do prowadzenia ewentualnych szkoleń z zakresu przygotowania pracowników do prawidłowego zabezpieczania danych informatycznych wg. RODO i OCHRONY DANYCH OSOBOWYCH w firmach. Poszukiwana jest osoba z doświadczeniem i wiedzą w zakresie zabezpieczeń informatycznych i OCHRONY DANYCH OSOBOWYCH WG. RODO oraz Ustaw i bieżących zmian legislacyjnych dotyczących tego zakresu.	\N	\N	\N	f	f	2022-11-16 00:00:00+01	2025-11-21 00:00:00+01	f	f	590	737	691	\N	\N	7
474	Instalator / monter światłowodów	Instalacja i konfiguracja sieci światłowodowych u klientów. Diagnozowanie i usuwanie awarii sieci światłowodowych. Wykonywanie pomiarów i testów sieci światłowodowych. Praca w terenie. Dbanie o wysoką jakość wykonywanych prac.	\N	\N	\N	f	f	2025-10-11 00:00:00+02	2025-11-17 00:00:00+01	f	f	590	738	692	\N	\N	18
\.

COPY public.benefits_junction (offer_id, benefit_id) FROM stdin;
1	1
1	2
1	3
1	4
1	5
1	302
1	7
1	8
1	9
1	10
1	11
1	12
1	13
3	1
3	2
4	1
4	2
4	4
4	5
4	20
4	7
4	8
4	10
4	24
5	25
5	26
5	27
5	28
5	29
5	30
5	31
5	32
5	33
5	34
5	35
5	36
5	37
5	38
6	25
6	26
6	27
6	28
6	29
6	44
6	30
6	31
6	47
6	48
6	49
6	50
6	51
6	52
6	53
6	54
6	55
6	36
6	37
6	58
6	59
6	60
6	61
6	62
7	25
7	26
7	27
7	28
7	29
7	68
7	35
7	36
7	71
7	72
8	26
8	27
8	28
8	29
8	31
8	35
8	36
8	37
9	1
9	2
9	5
9	84
9	85
9	86
9	87
9	10
9	11
9	90
46	1
46	105
46	90
46	110
46	405
46	275
46	407
46	408
41	1
41	105
41	90
41	110
51	25
51	26
51	29
51	30
51	31
51	48
51	32
51	33
51	35
51	51
51	1653
51	1654
41	405
41	275
41	407
41	408
11	25
11	26
11	28
11	29
11	31
11	32
11	53
11	98
11	55
11	37
11	101
11	102
13	25
13	26
13	28
13	29
13	68
13	44
13	31
13	48
13	50
13	155
13	32
13	33
13	35
13	51
13	55
13	37
14	25
14	26
14	27
14	28
14	29
14	53
15	25
15	26
15	29
15	140
15	31
15	32
15	35
15	55
15	36
15	37
15	147
15	148
16	1
16	2
16	3
16	84
16	108
16	86
16	124
16	110
16	275
16	1421
16	129
16	765
16	130
42	1
42	2
42	176
42	105
42	3
42	84
42	85
42	10
17	25
17	26
17	29
17	32
18	26
18	28
18	29
18	30
18	31
18	35
18	53
18	173
19	25
19	26
19	27
19	29
19	30
19	140
19	31
19	155
19	32
19	33
19	34
19	35
45	25
45	26
45	29
45	30
45	31
45	48
45	49
45	32
45	33
45	52
45	53
23	25
23	27
23	28
23	29
23	140
54	1
54	2
23	31
23	192
23	32
23	52
23	195
23	53
23	197
47	28
47	44
47	140
47	31
24	25
24	26
24	31
24	36
48	1
48	298
48	299
48	302
48	7
48	108
48	305
48	8
48	10
48	11
48	12
48	13
48	1500
48	405
48	24
48	1503
48	249
26	25
26	26
26	27
26	28
26	29
26	68
26	30
26	31
26	33
26	35
27	25
27	26
37	4
37	5
37	110
37	275
37	129
27	29
27	44
27	30
27	155
27	32
27	33
54	298
54	299
54	4
54	5
54	7
54	1662
54	8
54	9
54	10
54	11
54	12
55	25
55	26
27	52
27	195
27	55
27	37
27	101
27	225
43	25
43	26
43	28
43	44
43	31
43	32
43	33
28	31
28	35
55	27
55	28
55	29
55	68
55	44
55	30
55	33
55	35
55	1678
55	195
55	36
55	37
55	101
56	25
56	26
56	29
56	30
56	140
56	31
57	86
57	124
57	90
57	13
57	1693
57	275
57	1421
57	130
58	25
58	26
58	27
58	28
58	36
58	1702
59	25
59	26
59	28
59	29
59	44
59	31
59	33
59	35
59	195
59	1712
59	1713
59	1714
59	1715
61	25
61	68
61	44
61	140
61	33
61	35
61	51
61	195
61	1724
62	25
62	26
62	27
62	28
62	140
62	31
62	48
62	32
63	25
63	28
63	155
63	35
63	52
63	53
63	98
63	1740
64	1
64	2
64	3
64	85
64	87
64	10
65	25
65	26
66	1749
66	1750
66	1751
66	1752
66	1753
66	1754
66	1755
66	1756
66	1757
66	1758
66	1759
66	1760
66	1761
66	1762
66	1763
68	25
68	26
12	25
12	26
12	28
12	29
12	68
12	48
12	49
12	52
12	1047
12	1048
12	1049
12	1050
68	28
68	29
68	44
68	31
68	50
68	33
68	35
68	52
68	53
68	36
69	25
69	26
69	27
69	28
69	29
69	68
69	35
69	36
69	71
69	72
70	26
70	28
70	29
70	35
71	1
71	2
71	3
71	20
71	84
71	108
71	87
71	10
71	1798
71	130
71	1800
72	25
72	26
72	27
72	28
72	29
72	44
72	32
72	35
72	36
72	1653
72	1811
73	1
73	2
73	298
73	299
73	3
73	5
73	20
73	7
73	1820
73	1821
73	8
73	9
73	10
73	13
73	110
73	1827
73	1828
73	24
73	1503
73	249
73	1832
75	25
75	26
75	28
75	29
77	26
77	29
77	31
77	33
80	25
80	26
80	27
80	28
80	29
80	44
80	140
80	36
81	26
81	29
81	31
81	32
81	33
99	26
99	27
99	28
99	29
99	31
99	1939
99	33
99	53
99	55
99	197
101	25
101	26
101	27
101	53
102	26
102	28
102	29
102	140
102	31
102	32
102	33
102	55
103	25
103	27
103	28
103	140
103	31
103	32
103	33
103	35
104	1
104	2
104	105
104	3
104	5
104	1820
104	109
104	124
104	405
104	275
104	1974
105	25
105	28
105	53
106	25
106	26
106	28
106	29
106	68
82	25
82	26
82	28
82	29
82	68
82	44
82	31
83	28
86	29
86	140
86	31
86	35
88	25
88	26
88	28
88	29
88	31
88	49
88	53
88	98
88	55
90	31
90	35
90	1877
91	1
91	2
91	3
92	25
92	26
92	27
92	29
92	68
92	30
92	140
92	50
92	32
92	33
92	35
92	53
92	54
92	1894
92	37
92	1896
93	25
93	26
93	27
93	29
93	68
93	30
93	140
93	50
93	32
93	33
93	35
93	53
93	54
93	1894
29	25
29	26
29	27
29	28
29	29
29	31
29	50
29	33
29	35
29	237
29	98
29	54
29	55
33	25
33	26
33	29
33	30
33	31
33	33
33	34
33	35
33	101
33	250
33	251
49	25
49	28
49	29
49	140
49	50
49	52
49	53
49	197
49	1570
49	1571
49	1572
35	25
35	26
35	28
35	29
35	140
35	31
35	155
93	37
93	1896
95	25
95	26
95	29
95	31
97	25
97	28
97	44
97	31
97	1921
97	155
97	33
97	34
97	1894
35	32
35	33
35	51
35	55
35	37
35	264
44	1
44	2
44	3
44	5
44	85
36	26
36	33
36	52
36	53
36	269
36	270
36	271
50	25
50	26
50	27
50	28
50	29
50	68
50	32
50	35
50	36
50	71
50	72
38	25
38	26
38	27
38	28
38	29
38	68
38	44
38	30
38	31
38	32
38	287
38	33
38	34
38	35
38	51
38	55
38	36
38	197
38	37
39	1
39	2
39	298
39	105
39	3
39	20
39	1634
39	7
39	1636
39	305
39	8
39	10
39	1257
39	309
39	24
97	36
97	1927
97	1928
97	1929
98	25
98	32
98	53
99	25
106	48
106	49
106	52
106	1047
106	1048
106	1049
106	1050
107	26
107	27
107	29
107	30
107	31
107	155
107	32
107	33
107	1894
107	36
108	44
108	155
108	32
108	33
108	34
108	51
108	1894
108	58
109	27
109	28
109	31
111	25
111	26
111	27
111	29
111	31
111	52
111	53
111	2018
111	2019
111	2020
112	25
112	26
112	30
112	31
112	33
112	34
113	1
113	2
113	176
113	3
113	20
113	108
113	87
113	2034
116	25
116	26
116	27
116	28
116	29
116	68
116	31
116	48
116	1939
116	2044
116	32
116	33
116	34
116	35
116	1678
116	54
116	55
116	36
116	197
116	101
116	2055
117	27
117	28
117	29
117	32
117	35
117	53
118	1
118	2
118	3
118	90
118	110
118	275
118	129
118	765
119	26
119	29
119	44
119	31
119	155
119	32
119	33
120	1
120	2
120	3
124	25
124	26
124	27
124	28
124	31
124	35
126	25
126	26
126	27
126	28
126	29
126	68
126	44
126	31
126	155
126	52
127	25
127	26
127	28
127	44
127	140
127	31
127	48
127	155
127	32
127	36
127	37
128	25
128	26
128	29
128	44
128	31
129	25
129	26
129	27
129	28
129	44
129	31
129	32
129	35
130	1
130	2
130	3
130	4
130	5
130	20
130	84
130	85
130	87
130	10
130	110
130	405
130	2132
130	2133
131	25
131	26
131	27
131	28
131	29
131	68
131	140
131	31
131	48
131	49
131	50
131	155
131	52
131	2147
131	53
131	36
131	37
132	25
132	26
132	27
132	28
132	29
132	44
132	30
132	31
132	47
132	48
132	49
132	50
132	51
132	52
132	53
132	54
132	55
132	36
132	37
132	58
132	59
132	60
132	61
132	62
134	25
134	26
134	31
136	1
136	2
136	3
136	302
136	7
136	108
136	8
136	10
136	12
136	309
136	24
136	1503
137	25
137	26
137	27
137	28
137	29
137	31
137	32
137	33
137	34
138	1
138	2
138	3
138	4
138	5
138	85
138	765
138	1832
139	25
139	140
139	31
139	155
139	32
139	33
139	35
139	58
139	2215
139	2216
139	2217
140	26
140	27
140	28
140	29
140	68
140	31
140	48
140	155
140	32
140	287
140	33
140	34
140	35
140	51
140	53
140	1894
140	36
140	37
140	2236
140	2237
141	25
141	26
141	29
141	44
141	31
142	68
143	25
143	26
143	27
143	28
143	29
143	30
143	31
143	155
144	25
144	26
144	27
144	28
144	29
144	68
144	140
144	31
144	48
144	49
144	50
144	155
144	52
144	2147
144	53
144	36
144	37
145	25
145	26
145	27
145	44
145	31
145	48
145	155
145	32
145	33
145	35
145	53
145	101
146	1
146	105
146	275
148	68
149	68
151	68
152	68
153	44
153	140
153	31
153	33
153	35
153	51
153	36
153	37
153	101
154	25
154	26
154	29
154	50
154	53
154	2302
154	2303
156	27
156	29
156	68
156	140
156	31
156	48
156	33
156	35
156	36
156	37
157	26
157	140
157	32
157	33
158	25
158	26
158	49
158	52
158	98
158	54
158	2324
158	55
158	2326
158	2327
159	1
159	105
159	275
161	25
161	26
161	27
161	28
161	29
161	140
161	31
161	47
161	2339
161	48
161	2341
161	49
161	192
161	50
161	35
161	51
161	52
161	2147
161	237
161	2350
161	53
161	98
162	25
162	26
162	29
162	140
162	32
162	37
163	25
163	26
164	25
164	26
164	29
164	36
164	37
164	2366
164	2367
164	2368
164	2369
164	2370
165	25
165	26
165	29
165	36
165	37
165	2366
165	2367
165	2368
165	2369
165	2370
166	25
166	26
166	28
166	29
166	68
166	52
166	53
166	36
167	25
167	26
167	27
167	28
167	29
167	68
167	30
167	31
167	48
167	155
167	35
167	52
168	25
168	26
168	29
168	30
168	31
168	48
168	49
168	32
168	33
168	52
168	53
169	25
169	26
169	29
169	44
169	31
170	25
170	26
170	68
171	25
171	26
171	29
171	68
171	44
171	30
171	140
171	31
171	32
171	33
171	52
171	237
171	53
171	2133
171	2132
172	25
172	26
172	27
172	29
172	68
172	31
172	53
172	2442
173	25
173	26
173	29
173	30
173	31
173	48
173	49
173	32
173	33
173	52
173	53
174	25
174	26
174	28
174	29
174	50
174	155
174	52
174	2147
174	237
174	53
174	98
174	2465
174	2466
174	2467
175	25
175	26
175	27
175	28
175	29
175	68
175	155
175	32
177	25
177	26
177	27
177	28
177	29
177	31
177	33
177	34
177	35
177	55
177	1894
177	36
178	25
178	26
178	28
178	31
178	32
178	33
179	26
179	28
179	44
179	31
179	33
179	51
179	197
179	2501
180	25
180	26
180	27
180	28
180	31
184	25
184	26
184	27
184	28
184	29
184	44
184	30
184	31
184	47
184	33
184	34
184	52
184	53
184	36
185	25
185	26
185	29
185	48
185	33
185	35
185	36
185	37
186	25
186	26
186	28
186	29
186	68
186	44
186	140
186	1939
186	32
186	35
186	36
186	37
187	25
187	26
187	27
187	28
187	48
187	53
187	2547
187	2548
189	25
189	26
189	27
189	28
189	29
189	68
189	44
189	31
189	155
189	52
191	25
191	26
191	29
191	44
191	31
192	26
192	29
192	30
192	31
192	33
192	36
192	2570
192	2571
192	2572
192	2573
192	2574
193	25
193	26
193	28
193	29
193	68
193	52
193	53
193	36
194	1
194	2
196	25
196	26
196	29
196	68
196	44
196	140
196	31
196	48
196	33
196	35
196	51
196	36
196	37
196	2598
196	2599
197	25
197	26
197	27
197	28
197	29
197	44
197	30
197	31
197	47
197	48
197	49
197	50
197	51
197	52
197	53
197	54
197	55
197	36
197	37
197	58
197	59
197	60
197	61
197	62
198	44
198	31
198	32
198	33
198	35
198	36
198	37
200	25
200	26
200	28
200	29
200	31
200	55
200	36
200	37
201	25
201	26
201	29
201	31
201	36
201	37
204	25
204	26
204	28
204	29
204	68
204	44
204	30
204	31
204	32
204	33
204	35
204	1894
205	1
205	2
205	3
205	84
205	108
205	87
205	10
205	90
206	25
206	26
206	28
206	29
206	68
206	44
206	47
206	2339
206	2341
206	49
206	52
206	237
206	2350
206	53
207	25
207	26
207	27
207	28
207	29
207	140
207	31
207	37
208	26
208	29
208	44
208	30
208	31
208	155
208	32
208	33
208	34
208	35
208	51
208	52
208	55
208	36
210	25
210	26
210	27
210	28
210	29
210	44
210	155
210	32
210	33
210	237
210	53
212	25
212	26
212	27
212	28
212	29
212	68
212	44
212	30
212	140
212	31
212	47
212	2339
212	48
212	49
212	32
212	237
213	25
213	26
213	27
213	28
213	29
213	68
213	140
213	50
213	52
213	237
213	2324
213	36
214	25
214	26
214	27
214	29
214	30
214	31
214	32
214	33
214	55
215	25
215	26
215	27
215	28
215	29
215	68
215	44
215	31
215	48
215	155
215	32
215	35
215	52
215	53
215	98
215	55
215	36
215	101
215	2767
217	25
217	26
217	27
217	28
217	29
217	68
217	44
217	31
217	58
218	1
218	2
218	3
219	25
219	28
219	29
220	26
220	27
220	28
220	44
220	140
220	31
220	32
220	33
220	35
220	55
222	25
222	26
222	29
222	44
222	31
223	25
223	26
223	28
223	29
223	31
223	49
223	50
223	37
223	2806
224	25
224	26
224	27
224	29
224	68
224	44
224	30
224	140
224	31
224	32
224	33
224	52
224	237
224	2133
224	2132
225	25
225	26
225	140
225	31
225	48
225	33
225	36
225	101
226	26
226	155
226	36
226	37
226	2834
226	2835
226	20
226	2837
227	25
227	26
227	49
227	52
227	98
227	54
227	2324
227	55
227	2326
227	2327
228	25
228	26
228	29
228	47
228	49
228	54
228	36
228	2855
229	25
229	26
229	68
229	44
229	31
230	1
230	2
230	4
230	5
230	85
231	25
231	26
231	27
231	28
231	29
231	44
231	30
231	31
231	47
231	48
231	49
231	50
231	51
231	52
231	53
231	54
231	55
231	36
231	37
231	58
231	59
231	60
231	61
231	62
232	25
232	32
232	35
232	52
232	2894
232	36
232	37
232	2897
232	2898
232	2899
233	26
233	29
233	44
233	1939
233	287
233	33
233	35
233	52
233	53
233	37
233	58
234	25
234	26
234	27
234	28
234	29
234	44
234	30
234	31
234	50
234	155
234	32
234	33
234	52
234	237
234	53
237	2926
243	2937
243	2949
243	2950
249	2955
249	2956
249	5
250	2958
250	2946
250	407
250	2961
256	2962
256	2979
262	2962
262	2988
263	2989
263	2946
265	2937
265	2949
273	3027
273	3028
274	3029
274	3030
274	3031
274	2976
274	3033
274	3034
274	2951
281	2969
284	2953
284	3057
284	3058
284	3059
284	3060
284	3061
284	3062
246	2951
246	2952
246	2953
246	2954
253	3029
253	3068
253	2834
253	3070
253	2976
253	113
253	2951
253	3074
253	3075
254	2993
254	2994
254	2995
254	2974
254	2969
254	2998
254	2999
254	3000
254	2979
254	2976
254	3003
254	2947
254	2977
285	2
285	3
285	251
285	3092
285	3093
285	3094
285	3095
285	3096
285	3097
242	2
242	2944
242	2945
242	2946
242	3102
242	2947
287	2
287	3
287	3106
287	3107
287	3092
287	3093
287	3094
287	3095
287	3096
287	3097
258	2980
258	2981
258	2982
258	2983
258	2984
288	3119
288	3120
266	2999
266	3000
266	2979
266	2976
266	3003
266	2947
266	2977
266	2969
289	2
289	3
289	3131
289	3132
239	3133
239	2937
239	2946
239	2947
239	3137
239	3138
239	2941
291	3106
291	3141
291	3142
291	3143
292	2946
292	3145
294	2993
294	2994
294	2995
294	2974
294	2969
294	2998
294	2999
294	3000
294	2979
294	2976
294	3003
294	2947
294	2977
295	2
295	2834
296	2993
296	2994
296	2995
296	2974
296	2969
296	2998
296	2999
296	3000
296	2979
296	2976
296	3003
296	2947
296	2977
297	3174
297	3175
297	3176
297	3177
297	3178
297	3179
299	2
301	3181
301	3029
301	3058
301	3022
301	407
301	3186
301	3187
302	3
302	2
302	3190
302	3191
302	20
302	2927
302	85
276	3036
304	3036
304	3197
305	2834
305	3107
305	3200
305	3201
305	3202
305	3203
305	3204
272	2951
272	2952
272	2953
279	3036
306	2993
306	2994
306	2995
306	2974
306	2969
306	2998
306	2999
306	3000
306	2979
306	2976
306	3003
306	2947
306	2977
307	2
307	3223
307	3224
307	2946
307	3226
308	2
308	3228
310	2
310	2834
278	3029
278	2976
278	3033
278	3042
278	2938
278	113
311	3237
311	2834
311	3239
311	3240
311	3241
311	3242
268	3243
268	3008
268	3009
268	251
268	3247
313	3252
313	2989
313	2969
260	2
260	2963
282	3050
282	3051
282	3052
282	3053
282	2955
314	3050
314	3263
314	3264
314	3265
314	3266
314	3267
314	2946
315	3106
316	3181
316	3029
316	3058
316	3022
316	407
316	3186
316	3276
317	3106
271	2951
271	3181
271	2
271	3281
271	24
271	3283
271	3284
271	3285
271	3286
271	2946
271	3288
271	3020
271	13
318	3
319	2953
321	3012
321	3013
321	3014
321	24
321	3297
321	3017
321	2946
321	3019
321	3020
321	13
321	3022
322	2834
322	3305
323	2
323	2963
323	3
323	3309
323	3310
323	3033
323	3312
323	3286
324	3314
325	3315
325	3316
325	3317
326	2962
326	2937
326	2946
327	3050
327	3322
327	3323
327	3324
327	3325
327	3326
327	114
327	3328
277	2
277	2963
277	3314
329	2962
329	3
329	3334
329	3335
329	1571
329	3337
329	3338
329	3339
329	110
330	3314
330	3342
330	3343
330	3344
330	2
330	3346
330	3347
336	2963
336	2946
336	3355
337	3356
337	3357
337	3029
337	3359
338	2
338	2963
340	3370
340	3371
340	2834
341	2963
341	2034
341	3
341	3376
342	2834
342	3378
343	3
343	275
345	3119
345	3382
345	3383
345	3384
345	3385
345	3386
345	3387
335	2
335	129
335	3350
335	3
335	7
347	3119
347	3382
347	3395
347	3396
347	3385
347	3398
347	3399
348	3400
348	3401
348	3402
348	3403
348	3404
350	3405
350	3406
350	3407
238	2927
238	2928
238	2929
238	2
238	86
238	275
238	2933
238	1832
238	407
339	3362
339	3315
339	3364
339	3365
339	3366
339	3367
339	3368
339	3369
353	3177
353	85
353	3427
354	3428
354	3181
354	2
354	2946
354	3432
354	3433
280	3434
280	3047
280	3048
355	3437
355	3047
356	3439
356	3440
356	3047
356	3442
357	2962
357	3
357	1724
357	3446
359	3437
359	3047
360	3449
360	407
360	2
360	3
362	407
362	3454
362	3455
362	3050
363	3457
363	3458
363	3459
363	2
363	2946
363	3223
363	2976
363	3464
363	3465
363	3466
363	3467
363	3468
365	2
365	2963
365	3
365	3309
365	3310
365	3033
365	3475
365	3476
365	3477
366	2946
368	3050
368	3263
368	3264
368	3265
368	3266
368	3267
368	3485
369	3434
369	3047
369	3048
370	3370
370	3490
370	2834
370	3492
371	3493
371	3
371	2834
371	3496
371	3497
312	2034
312	3
312	2
312	2834
372	3502
372	3503
372	3504
372	3505
372	3506
372	3507
375	2962
375	3181
375	3370
375	3511
375	3512
375	7
376	3514
376	3515
376	3516
377	3517
377	3518
377	3310
377	3520
377	3521
377	3034
377	3432
377	3524
377	3525
377	7
377	3527
378	3263
378	3529
378	3530
378	3531
382	3532
382	3533
382	3534
382	3535
382	3536
382	3537
382	3538
382	3539
383	3617
383	3618
383	3619
383	3620
383	3621
384	3542
384	3543
384	3544
384	3545
384	3546
384	3547
384	2961
384	3549
385	3550
385	3551
385	3552
385	3553
385	3554
386	3555
386	3556
386	3637
386	3638
386	3639
386	3640
386	3561
386	3562
386	3563
386	3644
387	3565
387	3566
387	3567
387	3568
387	3569
387	3570
387	3571
399	3572
399	3573
399	3400
399	3575
399	3576
399	3577
399	3578
399	3579
399	3580
399	3581
399	3582
399	3583
399	3584
399	3585
399	3586
399	3587
399	3588
399	3589
399	3590
426	3671
426	3672
426	3673
428	3
428	3675
431	3676
431	3677
431	3678
431	3679
431	3680
431	3681
431	11
431	3683
447	3684
447	3181
447	3
451	3106
451	3688
457	3689
457	2962
457	3
462	2
462	3693
462	5
462	3106
465	3106
465	3697
465	2969
465	3503
\.


COPY public.education_levels (id, level) FROM stdin;
1	Wykształcenie średnie
2	Wykształcenie wyższe informatyczne
3	Certyfikaty potwierdzające wiedzę z zakresu bezpieczeństwa IT oraz produktów bezpieczeństwa
4	Studia informatyczne
5	Studia elektroniczne
6	Wykształcenie wyższe
7	Wykształcenie techniczne
8	Wykształcenie informatyczne
9	Wykształcenie wyższe techniczne
12	Wykształcenie wyższe techniczne/informatyczne
15	Informatyka
16	Teleinformatyka
18	Wykształcenie wyższe z zakresu informatyki
19	Wykształcenie wyższe z zakresu elektroniki
20	Wykształcenie wyższe w obszarze informatyki
24	Certyfikat ekspercki z zakresu bezpieczeństwa IT
28	Wykształcenie pokrewne
29	Wykształcenie średnie informatyczne
30	Wykształcenie średnie pokrewne
33	Wykształcenie wyższe techniczne, preferowany kierunek IT
37	Wykształcenie wyższe w zakresie informatyki, teleinformatyki lub pokrewnych kierunków.
39	Wykształcenie średnie techniczne w zakresie IT
60	Wykształcenie wyższe w zakresie informatyki, teleinformatyki lub pokrewnych kierunków
64	Wykształcenie wyższe teleinformatyczne
74	Wykształcenie średnie o profilu informatycznym
83	Wykształcenie wyższe: Energetyka
84	Wykształcenie wyższe: Matematyka
85	Wykształcenie wyższe: Informatyka
86	Wykształcenie wyższe: Inżynieria Środowiska
97	Certyfikaty z zakresu bezpieczeństwa IT
98	Certyfikaty produktów bezpieczeństwa
113	Energetyka
114	Matematyka
116	Inżynieria Środowiska
119	Technologie informacyjne
127	Wykształcenie kierunkowe (IT)
129	Wykształcenie wyższe przyrodnicze
131	Wykształcenie wyższe na kierunku informatyka
133	Technikum informatyczne
139	Wykształcenie średnie elektroniczne
140	Wykształcenie o profilu informatycznym
143	Wykształcenie średnie techniczne o profilu informatycznym
144	Wykształcenie informatyczne średnie
146	Wykształcenie średnie techniczne
147	Wyższe (preferowane: informatyka, teleinformatyka, itp.)
152	Wykształcenie średnie kierunkowe
156	Telekomunikacja
162	Wyższe wykształcenie w dziedzinie informatyki lub pokrewne
166	Wykształcenie wyższe – informatyczne
167	Wyższe informatyczne
170	Inżynieria Oprogramowania
171	Wykształcenie wyższe techniczne lub pokrewne
174	Ekonomia
175	Wykształcenie wyższe w obszarze IT
176	Wykształcenie wyższe w obszarze telekomunikacji
177	Wykształcenie wyższe w obszarze cyberbezpieczeństwa
178	Wykształcenie wyższe w zakresie informatyki
179	Wyższe wykształcenie techniczne
181	Lingwistyka
185	Inżynieria
192	Licencjat
193	Magister
199	Kierunki pokrewne
201	Wykształcenie wyższe techniczne (informatyka, telekomunikacja lub pokrewne)
204	Wykształcenie wyższe w zakresie telekomunikacji
210	System Engineering
211	Project Management
220	Studia biznesowe
221	Certyfikat Microsoft Dynamics 365 F&amp;O Core
222	Certyfikat techniczny
224	Elektronika
225	Automatyka
229	Wykształcenie wyższe kierunkowe
230	Certyfikat ISEB/ISTQB
232	Wykształcenie wyższe (matematyka, metody ilościowe, ekonometria, informatyka, fizyka)
236	Wykształcenie wyższe o profilu technicznym, informatycznym lub ekonomicznym
237	Szkolenia i/lub certyfikaty w obszarze project managementu (PRINCE2, PMP, PMI, ITIL, AgilePM, SCRUM)
240	Wykształcenie wyższe ekonomiczne
245	Wykształcenie wyższe biznesowe
253	Wykształcenie wyższe techniczne z dziedziny data science
254	Wykształcenie wyższe techniczne z dziedziny informatyki
255	Wykształcenie wyższe techniczne z dziedziny statystyki
256	Wykształcenie wyższe techniczne z dziedziny matematyki
259	Statystyka
261	Ekonometria
265	Wykształcenie wyższe techniczne (informatyka, elektronika, automatyka lub pokrewne)
267	W trakcie studiów
272	Inżynieria baz danych
275	Inżynieria danych
281	Wykształcenie wyższe techniczne (informatyka, matematyka lub pokrewne)
282	Wykształcenie wyższe w zakresie informatyki lub kierunków pokrewnych
283	Wykształcenie wyższe licencjackie
284	Wykształcenie wyższe magisterskie
285	Certyfikat ISTQB
290	Inżynier
296	Fizyka
300	Kurs na przewóz towarów
301	Status studenta/ucznia
302	Wykształcenie zawodowe
305	Orzeczenie lekarskie do celów sanitarno-epidemiologicznych
307	Uprawnienia UDT
308	Aktualne badania sanitarno-epidemiologiczne
311	Wykształcenie podstawowe
322	Książeczka sanitarno-epidemiologiczna
323	Uprawnienia UDT na obsługę wózków widłowych
324	Wykształcenie zasadnicze zawodowe
326	Uprawnienia UDT na wózki widłowe
341	Wykształcenie związane z biotechnologią
342	Wyższe (w tym licencjat)
343	Wykształcenie biologiczne
349	Wyższe techniczne
350	Wykształcenie średnie techniczne o profilu chemicznym
356	Wykształcenie średnie zawodowe
360	Certyfikaty poświadczające umiejętności zawodowe
361	Wykształcenie średnie zawodowe, informatyczne
362	Średnie zawodowe, informatyczne
366	Średnie zawodowe
367	Wykształcenie średnie ogólnokształcące
368	Wykształcenie wyższe biologiczne
369	Wykształcenie wyższe medyczne
372	Kurs inspektora / administratora bezpieczeństwa teleinformatycznego
376	Technik informatyk
382	Magister biotechnologii
383	Magister technologii żywności
384	Magister mikrobiologii
385	Magister biologii
387	Biologia
388	Biotechnologia
391	Świadectwo kwalifikacyjne na stanowisku eksploatacji dla urządzeń, instalacji i sieci grupy 1
393	Wykształcenie medyczne
395	Bioinżynieria
397	Chemia
398	Zdrowie publiczne
399	Fizjoterapia
400	Dietetyka
401	Analityka medyczna
402	Ratownictwo medyczne
403	Psychologia kliniczna
404	Genetyka
411	Rolnictwo
412	Weterynaria
414	Wykształcenie wyższe analityczne
415	Wykształcenie wyższe biotechnologiczne
416	Wykształcenie wyższe chemiczne
417	Wykształcenie wyższe mikrobiologiczne
418	Wykształcenie pozwalające rozumieć mechanizmy biologii
429	Ukończenie szkoły wyższej z zakresu informatyki
434	Wykształcenie średnie branżowe, informatyczne
442	Wyższe studia informatyczne
443	Studia podyplomowe
459	Wykształcenie wyższe pokrewne
463	Wyższe
464	Kurs inspektora bezpieczeństwa teleinformatycznego
465	Kurs administratora bezpieczeństwa teleinformatycznego
468	Wykształcenie średnie branżowe
469	Wykształcenie średnie zawodowe, kierunek: informatyk
476	Wykształcenie średnie ekonomiczne
479	Wyższe (w tym licencjat) techniczne
481	Elektrotechnika
482	Szkoła elektroniczna
483	Szkoła elektryczna
486	Technologie komunikacyjne
492	Wykształcenie wyższe telekomunikacyjne
\.

COPY public.education_levels_junction (offer_id, education_level_id) FROM stdin;
7	1
7	2
8	97
8	98
9	4
9	5
46	2
46	29
41	2
41	28
41	29
41	30
14	7
14	8
16	9
18	1
19	12
45	6
45	113
45	114
45	15
45	116
21	37
48	15
48	119
26	1
43	39
29	2
30	1
34	20
39	15
51	2
53	127
57	9
57	129
59	1
64	131
66	15
68	133
69	1
69	2
70	2
71	2
76	29
76	139
77	140
80	15
80	114
81	143
82	144
83	1
86	146
86	147
91	1
91	6
95	6
100	2
101	152
102	1
104	7
104	15
104	156
105	6
106	29
110	1
12	6
113	15
113	114
116	162
117	6
118	15
122	29
123	166
125	167
127	9
128	15
128	170
130	171
131	15
131	114
131	174
132	175
132	176
132	177
137	178
138	179
144	15
144	181
146	6
147	6
148	15
148	185
149	15
149	185
149	174
149	114
150	6
151	9
152	192
152	193
156	9
157	6
158	6
159	6
160	15
160	199
161	6
162	201
164	6
165	178
165	204
166	6
167	146
168	6
169	2
170	15
170	210
170	211
172	179
173	9
174	9
175	174
175	114
175	15
179	192
183	15
183	220
183	221
183	222
184	15
184	224
184	225
185	15
186	2
187	6
189	229
191	230
192	9
193	232
194	9
196	9
197	9
198	236
198	237
200	9
200	2
200	240
201	6
202	2
204	2
204	9
204	245
205	1
205	9
206	15
206	156
206	16
207	6
208	9
209	253
209	254
209	255
209	256
210	6
213	114
213	259
213	15
213	261
215	15
215	224
215	156
216	265
217	6
217	267
218	6
220	15
220	114
220	259
220	272
221	15
221	114
221	275
222	15
222	114
223	6
224	15
224	114
225	281
226	282
227	283
227	284
228	285
229	15
229	224
229	225
230	179
231	290
231	193
232	15
232	225
232	224
232	114
232	296
233	15
233	114
233	296
235	300
237	301
263	1
265	305
284	307
284	308
293	1
297	302
299	311
302	307
322	307
324	302
324	1
327	1
329	302
329	1
340	307
348	322
348	323
238	324
238	1
354	326
280	322
355	308
356	308
283	146
359	308
363	7
369	322
312	302
312	1
373	308
376	146
376	6
377	1
377	6
401	192
401	382
401	383
401	384
401	385
378	341
402	387
402	388
403	146
403	302
403	391
379	342
379	393
379	388
379	395
379	387
379	397
379	398
379	399
379	400
379	401
379	402
379	403
379	404
404	1
404	6
380	343
381	343
405	387
405	388
405	411
405	412
406	368
406	414
406	415
406	416
406	417
407	418
382	146
382	192
382	290
384	74
384	349
386	350
388	342
388	8
388	1
408	361
408	429
409	366
410	356
411	29
411	6
412	434
390	1
390	2
391	356
392	284
392	283
392	15
392	360
413	442
413	443
393	361
394	362
414	342
415	367
416	367
416	2
416	9
395	342
395	15
395	16
396	366
397	367
417	1
398	368
398	369
398	459
419	2
419	192
399	167
399	463
399	464
399	465
420	342
421	367
422	468
423	469
400	146
400	229
400	356
400	376
424	366
425	6
425	476
426	356
426	7
427	479
427	156
427	481
437	482
437	483
441	15
441	119
441	486
444	482
444	483
446	2
446	9
449	1
454	492
454	64
458	6
458	1
461	1
461	6
468	192
468	193
470	29
\.

COPY public.employment_schedules (id, schedule) FROM stdin;
103	Pełny etat
162	Część etatu
\.

COPY public.employment_schedules_junction (offer_id, employment_schedule_id) FROM stdin;
51	103
52	103
53	103
54	103
55	103
56	103
57	103
58	103
59	162
60	103
61	162
62	103
63	103
12	103
64	103
65	162
66	103
67	103
68	103
69	103
70	103
71	103
72	103
73	103
74	103
75	103
76	103
77	103
78	103
79	103
80	103
81	103
82	103
83	103
84	103
85	103
85	162
86	103
37	1
37	5
87	103
87	162
88	103
89	103
90	103
91	103
92	103
93	103
95	103
1	103
2	103
3	103
4	103
5	103
6	103
7	103
8	103
9	103
40	103
10	103
46	103
41	103
11	103
13	103
14	103
15	103
16	103
42	103
17	103
18	103
19	103
20	103
45	103
21	103
22	103
23	103
47	103
24	103
24	162
25	103
48	103
26	103
96	103
97	103
98	103
99	103
100	103
101	103
102	103
103	103
104	103
105	103
106	103
107	103
108	103
109	103
110	103
111	103
112	103
113	103
114	103
114	162
115	103
116	103
117	103
118	103
119	103
120	103
121	103
122	103
123	103
124	103
125	162
126	103
127	103
128	103
129	103
130	103
131	103
132	103
134	103
135	103
136	103
137	103
27	103
43	103
28	103
29	103
30	103
31	103
32	103
33	103
49	103
34	103
34	162
35	103
44	103
36	103
50	103
38	103
39	103
138	103
139	103
140	103
141	103
142	103
143	103
144	103
145	103
146	103
147	103
148	103
149	103
150	103
151	103
152	103
154	103
155	103
156	103
157	103
158	103
159	103
160	103
161	103
162	103
163	9
164	103
165	103
166	103
167	103
168	103
169	103
170	103
171	103
172	103
173	103
174	103
175	103
176	103
177	103
178	103
179	103
180	103
181	103
182	103
183	103
184	103
185	103
186	103
187	103
188	103
188	162
189	103
190	103
191	103
192	103
193	103
194	103
195	103
196	103
197	103
198	103
199	103
200	103
201	103
202	103
203	103
204	103
205	103
206	103
207	103
208	103
209	103
210	103
211	103
212	103
213	103
214	103
215	103
216	103
217	103
218	103
219	103
220	103
221	103
222	103
223	103
224	103
225	103
226	103
227	103
228	103
229	103
230	103
231	103
232	103
232	162
233	103
234	103
235	1
236	103
237	103
243	162
248	103
249	103
250	103
251	103
252	103
255	103
256	1
262	103
263	103
265	103
267	103
270	103
273	1
274	103
275	103
281	162
284	103
247	103
244	6
246	103
253	2
264	103
254	103
285	162
261	103
242	103
287	162
258	103
288	9
266	103
289	103
290	103
239	103
291	1
292	2
293	103
269	2
294	103
295	103
296	103
297	103
298	103
299	2
300	103
301	2
302	2
303	1
276	1
305	103
272	103
241	2
279	1
306	103
307	2
308	103
309	2
310	2
278	2
311	103
259	162
268	7
268	8
313	103
260	2
282	103
314	103
315	1
316	2
317	103
271	2
318	162
319	103
320	103
321	2
322	1
323	8
324	2
325	2
326	2
327	1
328	103
277	2
257	9
329	162
330	2
331	2
331	6
331	9
332	103
333	2
336	2
337	103
338	2
340	1
341	103
342	103
343	2
344	2
345	103
334	103
335	2
346	2
347	2
348	2
245	103
349	103
350	162
238	1
351	2
352	103
339	2
353	103
354	103
280	7
355	162
356	5
357	1
283	103
358	5
359	162
360	2
361	2
362	103
363	1
364	1
365	2
366	103
367	7
368	103
369	3
370	103
371	103
312	1
372	1
286	103
373	103
374	7
375	2
376	103
401	103
378	103
402	103
403	103
379	162
404	103
380	103
381	103
405	103
406	103
407	103
382	103
383	103
384	1
385	103
386	103
387	103
388	103
408	103
409	103
410	103
411	103
412	103
390	162
391	103
392	103
413	103
393	103
394	103
414	103
415	103
416	103
395	103
396	162
397	103
417	103
418	103
398	103
419	103
399	103
420	103
421	103
422	162
423	103
400	103
424	103
425	103
426	103
427	103
428	103
429	103
430	103
431	103
432	103
433	103
434	9
435	9
437	103
439	103
440	103
441	9
442	103
443	1
444	103
445	103
446	1
447	1
449	9
450	103
451	9
452	103
453	1
454	103
455	9
456	103
457	103
458	103
459	103
460	103
461	1
462	103
463	103
464	1
465	9
467	162
468	103
469	103
470	9
471	1
472	1
473	103
474	103
475	103
\.

COPY public.employment_types (id, type) FROM stdin;
538	Część etatu
593	Umowa o pracę tymczasową
\.

COPY public.employment_types_junction (offer_id, employment_type_id) FROM stdin;
51	1
51	4
52	1
52	2
53	1
53	2
53	4
54	1
54	2
54	4
55	1
56	1
57	1
58	1
59	1
60	1
60	4
12	1
61	2
62	1
63	1
64	1
65	4
66	1
67	1
68	1
69	1
70	1
71	1
72	1
72	4
73	1
74	1
74	4
75	1
75	4
76	1
77	1
78	2
79	1
37	1
37	2
80	1
81	1
82	1
82	4
83	1
84	1
85	3
85	2
85	4
86	1
87	1
87	2
87	4
88	1
89	1
1	4
2	4
3	4
4	1
5	1
6	1
7	1
8	1
8	4
9	1
40	1
40	2
40	4
10	1
10	4
46	1
41	1
11	1
13	1
13	4
14	1
15	1
16	1
42	1
17	1
18	1
19	1
20	1
45	1
45	4
21	1
22	1
23	1
47	1
47	4
90	1
91	1
92	1
92	4
93	1
93	4
94	2
94	4
95	1
96	1
96	4
97	1
97	4
98	1
99	1
100	1
101	1
102	1
102	2
102	4
103	1
103	2
104	1
105	1
106	1
107	1
108	1
108	2
108	4
109	1
110	1
111	1
112	1
113	1
114	1
114	2
115	1
116	1
117	1
118	1
118	4
119	1
120	1
121	1
122	1
123	1
124	1
124	4
125	1
24	1
24	4
25	1
48	4
26	1
27	1
43	1
43	2
43	4
28	1
29	1
30	1
31	1
32	1
33	1
49	1
34	1
35	1
44	4
36	1
50	1
50	4
38	1
38	4
39	1
126	1
127	4
128	4
129	4
130	1
131	1
132	1
133	4
134	4
135	4
136	1
137	4
138	4
139	1
140	1
141	1
141	4
142	4
143	1
144	1
145	1
145	4
146	1
147	4
148	4
149	4
150	4
151	4
152	4
153	2
154	1
155	1
156	1
157	1
158	1
159	1
160	4
161	1
162	1
163	4
164	1
165	1
166	1
167	1
168	1
169	4
170	4
171	1
172	1
173	1
173	4
174	1
175	1
176	1
176	4
177	1
178	1
178	2
178	4
179	1
180	1
180	4
181	4
182	1
182	4
183	1
184	4
185	1
185	4
186	4
187	1
188	1
188	3
188	2
188	4
188	5
189	1
190	4
191	4
192	1
192	4
193	1
194	4
195	4
196	1
197	1
198	1
198	4
199	1
199	4
200	4
201	4
202	1
202	4
203	4
204	1
205	1
206	1
207	1
208	1
209	4
210	1
211	4
212	1
213	1
214	1
215	1
215	4
216	1
216	4
217	1
217	2
217	4
218	1
218	2
218	4
219	1
219	4
220	1
221	4
222	4
223	1
224	1
225	1
226	1
227	1
228	1
228	4
229	4
230	4
231	1
232	1
233	1
234	1
235	1
236	1
237	2
240	2
243	2
243	4
248	2
249	2
250	1
251	1
252	2
255	2
256	1
262	1
263	1
265	2
265	4
267	2
270	2
273	2
274	1
275	1
275	2
275	3
275	4
281	2
284	538
284	2
284	1
247	2
244	2
246	538
246	2
253	1
264	538
254	538
285	538
261	1
261	2
242	1
287	1
287	538
258	2
288	2
266	538
289	2
289	1
290	1
239	1
239	2
291	1
292	1
293	1
269	1
294	538
295	1
296	538
297	1
298	1
300	1
301	1
302	1
303	1
276	593
304	2
305	1
272	2
241	1
279	593
306	538
307	1
307	2
308	1
309	593
310	2
278	1
311	2
259	2
268	2
313	1
313	538
260	1
282	538
282	2
314	538
315	1
316	1
317	538
317	4
317	2
271	1
318	2
319	1
320	2
321	1
322	2
323	538
324	1
325	593
326	1
328	2
277	1
257	2
329	538
330	593
331	2
332	538
333	2
336	2
337	1
338	1
340	593
341	2
342	2
343	1
344	2
345	593
334	538
335	1
346	1
347	593
348	1
245	1
349	1
349	538
350	2
238	1
351	1
352	2
339	1
353	1
354	1
280	2
355	2
356	2
357	1
283	1
358	2
358	538
359	2
360	1
361	593
362	538
362	2
363	1
364	1
365	1
366	1
367	2
368	538
368	2
369	2
370	2
371	538
312	1
372	1
286	538
286	2
373	2
374	2
375	1
376	1
377	1
401	1
378	1
402	1
403	1
379	1
404	1
380	1
381	1
405	1
406	1
407	1
382	1
383	1
384	1
385	1
386	1
387	1
388	1
408	1
389	1
409	1
410	1
411	1
412	1
390	1
391	1
392	1
413	1
393	1
394	1
414	1
415	5
416	1
395	1
396	1
397	1
417	1
418	1
398	1
419	1
399	1
420	1
421	1
422	1
423	1
400	1
424	1
425	1
426	1
427	1
428	1
429	1
430	1
431	1
432	7
433	1
434	1
435	1
435	2
435	4
436	1
437	1
438	1
439	1
440	1
441	1
442	2
443	1
444	1
445	1
445	4
446	1
447	1
448	1
449	1
450	1
451	1
452	1
453	1
454	1
455	1
456	1
457	1
458	1
459	5
460	1
461	1
462	1
463	1
464	2
465	2
465	4
466	2
467	1
468	1
469	1
469	4
470	1
471	2
472	1
473	1
474	1
474	2
475	1
475	2
\.

COPY public.experience_levels (id, level) FROM stdin;
160	Średniozaawansowany
171	Zaawansowany
283	Biegły
284	Dobry
\.

COPY public.external_offers (offer_id, query_string, offer_lifespan_expiration) FROM stdin;
1	python-developer-poznan-mostowa-38,oferta,1004458917	2025-11-15 18:31:32.644489+01
2	senior-ai-python-developer-gdynia-aleja-zwyciestwa-96-98,oferta,1004461413	2025-11-15 18:31:32.640758+01
3	python-developer-engineering-automation-aec-wroclaw-legnicka-59,oferta,1004470041	2025-11-15 18:31:32.641224+01
4	python-developer-krakow-floriana-straszewskiego-10,oferta,1004454908	2025-11-15 18:31:32.642294+01
5	programista-python-opole-technologiczna-2a,oferta,1004458080	2025-11-15 18:31:32.645458+01
6	starszy-specjalista-ds-administrowania-systemami-bezpieczenstwa-it-k-m-warszawa-chmielna-73,oferta,1004471199	2025-11-15 18:31:32.647569+01
7	administrator-systemow-informatycznych-windows-warszawa-szturmowa-2,oferta,1004471187	2025-11-15 18:31:32.644782+01
8	starszy-inzynier-systemowy-bezpieczenstwo-it-warszawa-pulawska-474,oferta,1004447762	2025-11-15 18:31:32.644734+01
9	mlodszy-administrator-it-warszawa-bukowinska-22,oferta,1004470810	2025-11-15 18:31:32.644046+01
40	informatyk-specjalista-helpdesk-gdansk-narwicka-2g,oferta,1004464472	2025-11-15 18:31:32.641254+01
37	informatyk-administrator-sieci-ostrowiec-swietokrzyski-kolejowa-20,oferta,1004465475	2025-11-14 20:23:24.422394+01
10	specjalista-specjalistka-ds-wsparcia-it-wroclaw-buforowa-125,oferta,1004447318	2025-11-15 18:31:32.64046+01
46	informatyk-w-dziale-rozwoju-aplikacji-i-zarzadzania-bazami-danych-bialystok-warszawska-13,oferta,1004464071	2025-11-15 18:31:32.644273+01
12	starszy-specjalista-ds-informatyki-k-m-konstancin-jeziorna-warszawska-165,oferta,1004474387	2025-11-14 22:28:26.42503+01
41	informatyk-w-dziale-administracji-uslugami-publicznymi-bialystok-warszawska-13,oferta,1004464065	2025-11-15 18:31:32.644029+01
11	specjalista-specjalistka-ds-wsparcia-it-lublin-konrada-wallenroda-4c,oferta,1004463968	2025-11-15 18:31:32.644268+01
13	network-security-engineer-azure-wroclaw-jerzmanowska-2,oferta,1004474148	2025-11-15 18:31:32.645274+01
14	specjalista-ds-wsparcia-it-k-m-poznan-skladowa-4,oferta,1004463575	2025-11-15 18:31:32.642006+01
15	it-support-technik-wsparcia-it-kutno,oferta,1004449417	2025-11-15 18:31:32.655225+01
16	technik-informatyk-warszawa-posag-7-panien-1,oferta,1004469309	2025-11-15 18:31:32.652746+01
42	mlodszy-administrator-systemow-informatycznych-wroclaw-jana-dlugosza-42-46,oferta,1004446239	2025-11-15 18:31:32.651149+01
17	mlodszy-administrator-it-gdansk-juliusza-slowackiego-199,oferta,1004473056	2025-11-15 18:31:32.649744+01
18	technik-informatyk-sroda-slaska-polna-17b,oferta,1004462851	2025-11-15 18:31:32.652711+01
19	starszy-specjalista-ds-wsparcia-it-lodz-kinga-c-gillette-11,oferta,1004470931	2025-11-15 18:31:32.652822+01
20	specjalista-specjalistka-ds-systemow-informatycznych-sekocin-stary-pow-pruszkowski-lesnikow-21c,oferta,1004472829	2025-11-15 18:31:32.647589+01
21	specjalista-specjalistka-ds-informatyki-lublin-obroncow-pokoju-2,oferta,1004472688	2025-11-15 18:31:32.653192+01
22	programista-systemow-informatycznych-sekocin-stary-pow-pruszkowski-lesnikow-21c,oferta,1004469958	2025-11-15 18:31:32.648105+01
23	mlodszy-specjalista-ds-wsparcia-it-f-m-d-warszawa-inflancka-4,oferta,1004468707	2025-11-15 18:31:32.655239+01
47	specjalista-specjalistka-ds-administracji-i-rozwoju-systemow-informatycznych-eno-skawina-torowa-45,oferta,1004472489	2025-11-15 18:31:32.652532+01
24	informatyk-administrator-systemow-i-sprzetu-komputerowego-warszawa,oferta,1004462361	2025-11-15 18:31:32.65277+01
48	it-administrator-katowice-wroclawska-54,oferta,1004439478	2025-11-15 18:31:32.659498+01
28	mlodszy-specjalista-ds-wsparcia-it-z-j-niemieckim-krakow-generala-leopolda-okulickiego-66,oferta,1004461578	2025-11-15 18:31:32.656753+01
49	specjalista-ds-wdrozenia-clinical-information-system-i-innych-systemow-informaty-poznan,oferta,1004446369	2025-11-15 18:31:32.662951+01
35	specjalista-specjalistka-ds-wsparcia-it-warszawa-domaniewska-48,oferta,1004466024	2025-11-15 18:31:32.665128+01
45	konsultant-systemow-informatycznych-warszawa-adama-branickiego-13,oferta,1004472695	2025-11-15 18:31:32.656404+01
25	mlodszy-specjalista-mlodsza-specjalistka-ds-wsparcia-it-rusocin-pow-gdanski-gm-pruszcz-gdanski,oferta,1004462154	2025-11-15 18:31:32.653084+01
26	specjalista-ds-wdrozen-systemow-informatycznych-pass-pow-warszawski-zachodni-gm-blonie-stefana-batorego-1,oferta,1004456347	2025-11-15 18:31:32.657943+01
27	administrator-it-systemy-windows-komorniki-gm-komorniki-wisniowa-11,oferta,1004462224	2025-11-15 18:31:32.663373+01
77	informatyk-torun,oferta,1004469754	2025-11-15 18:33:56.052017+01
43	mlodszy-specjalista-mlodsza-specjalistka-ds-wsparcia-it-helpdesk-warszawa-brukselska-14,oferta,1004431136	2025-11-15 18:31:32.65756+01
29	analityk-ds-wsparcia-i-operacji-systemow-informatycznych-k-m-myszkow-kazimierza-pulaskiego-6,oferta,1004444551	2025-11-15 18:31:32.663032+01
30	mlodszy-informatyk-kowale-pow-gdanski,oferta,1004466901	2025-11-15 18:31:32.662004+01
31	administrator-it-poznan-marszalkowska-3a,oferta,1004435802	2025-11-15 18:31:32.6619+01
32	informatyk-gorzow-wielkopolski-franciszka-walczaka-42,oferta,1004444400	2025-11-15 18:31:32.659187+01
33	administrator-baz-danych-i-systemow-it-starszy-informatyk-krzycko-wielkie-pow-leszczynski-morkowska-3,oferta,1004443766	2025-11-15 18:31:32.664625+01
34	informatyk-dabrowka-pow-makowski-gm-czerwonka,oferta,1004443702	2025-11-15 18:31:32.659484+01
44	konsultant-ds-wdrozen-it-analityk-it-warszawa,oferta,1004460291	2025-11-15 18:31:32.663744+01
36	mlodszy-specjalista-ds-wsparcia-it-wroclaw-borowska-99,oferta,1004460257	2025-11-15 18:31:32.663888+01
78	mlodszy-specjalista-mlodsza-specjalistka-ds-wsparcia-it-helpdesk-blonie,oferta,1004437959	2025-11-15 18:33:56.051304+01
79	mlodszy-specjalista-ds-wsparcia-it-z-jezykiem-niemieckim-katowice,oferta,1004440418	2025-11-15 18:33:56.050945+01
80	specjalista-ds-bezpieczenstwa-it-warszawa-aleja-jana-pawla-ii-22,oferta,1004437898	2025-11-15 18:33:56.052828+01
81	informatyk-specjalista-ds-wsparcia-it-kaszewiec-pow-makowski-5,oferta,1004437823	2025-11-15 18:33:56.052167+01
82	koordynator-ds-it-administrator-systemow-nowa-wies-rzeczna-pow-starogardzki-rzeczna-18,oferta,1004469105	2025-11-15 18:34:54.462998+01
83	wsparcie-it-helpdesk-automatyk-technik-utrzymania-ruchu-rawicz,oferta,1004440141	2025-11-15 18:34:54.459549+01
84	specjalista-specjalistka-do-spraw-wsparcia-it-rzeszow,oferta,1004453607	2025-11-15 18:34:54.459224+01
85	specjalista-it-administrator-it-warszawa,oferta,1004468422	2025-11-15 18:34:54.459003+01
86	mlodszy-specjalista-ds-wsparcia-it-gnojnik-pow-brzeski,oferta,1004439668	2025-11-15 18:34:54.463825+01
87	it-help-desk-informatyk-warszawa,oferta,1004467447	2025-11-15 18:34:54.46725+01
88	specjalista-ds-bezpieczenstwa-it-poznan-28-czerwca-1956-r-223-229,oferta,1004436331	2025-11-15 18:34:54.463986+01
89	informatyk-warszawa-minska-65,oferta,1004452519	2025-11-15 18:34:54.463517+01
90	administrator-it-krakow,oferta,1004436024	2025-11-15 18:34:54.459364+01
50	inzynier-ds-jakosci-systemow-informatycznych-warszawa-szturmowa-2,oferta,1004425575	2025-11-15 18:31:32.664702+01
38	it-security-specialist-wroclaw-powstancow-slaskich-9,oferta,1004417057	2025-11-15 18:31:32.667701+01
39	it-security-specialist-nowy-tomysl,oferta,1004459744	2025-11-15 18:31:32.66728+01
51	specjalista-ds-bezpieczenstwa-it-zespol-monitorowania-bezpieczenstwa-warszawa-szturmowa-2,oferta,1004465182	2025-11-15 18:33:56.045762+01
52	informatyk-mlodszy-specjalista-helpdesk-warszawa-wiertnicza-161,oferta,1004472071	2025-11-15 18:33:56.035291+01
53	administrator-administratorka-systemow-informatycznych-katowice-generala-zygmunta-waltera-jankego-211,oferta,1004472038	2025-11-15 18:33:56.0436+01
54	it-administrator-warszawa-strazacka-53,oferta,1004459342	2025-11-15 18:33:56.045953+01
55	koordynator-ds-wsparcia-it-gdansk-kontenerowa-7,oferta,1004419437	2025-11-15 18:33:56.045116+01
56	senior-it-administrator-warszawa-ostrobramska-75c,oferta,1004455535	2025-11-15 18:33:56.036164+01
57	specjalista-administrator-systemow-informatycznych-centrum-informatyczne-task-nr-gdansk-gabriela-narutowicza-11-12,oferta,1004444607	2025-11-15 18:33:56.040627+01
58	it-servicedesk-specialist-specjalista-wsparcia-it-krakow-bociana-22,oferta,1004444230	2025-11-15 18:33:56.036464+01
59	mlodszy-administrator-it-srem-wiosenna-12,oferta,1004432671	2025-11-15 18:33:56.040959+01
60	administrator-it-kajetany-pow-pruszkowski,oferta,1004469366	2025-11-15 18:33:56.042534+01
61	platny-staz-administrator-it-k-m-warszawa-jutrzenki-105,oferta,1004463345	2025-11-15 18:33:56.046245+01
62	specjalista-ds-wsparcia-it-slupsk-europejska-10,oferta,1004457582	2025-11-15 18:33:56.040869+01
63	specjalista-inzynieryjno-techniczny-w-instytucie-fizyki-wydzialu-fizyki-astronom-torun-jurija-gagarina-11,oferta,1004457521	2025-11-15 18:33:56.045481+01
64	administrator-it-lublin-ludwika-spiessa-5,oferta,1004443430	2025-11-15 18:33:56.046188+01
65	administrator-it-warszawa,oferta,1004417060	2025-11-15 18:33:56.048233+01
66	governance-risk-and-compliance-specialist-it-security-nowy-tomysl,oferta,1004456956	2025-11-15 18:33:56.051408+01
67	specjalista-tka-ds-informatyki-przemyslowej-poznan,oferta,1004427659	2025-11-15 18:33:56.04515+01
68	mlodszy-specjalista-ds-wsparcia-it-nysa-nowowiejska-20,oferta,1004461416	2025-11-15 18:33:56.049991+01
69	administrator-systemow-informatycznych-backup-warszawa-szturmowa-2,oferta,1004471410	2025-11-15 18:33:56.050585+01
70	informatyk-pleszew-kraszewskiego-11,oferta,1004442089	2025-11-15 18:33:56.05004+01
71	mlodszy-specjalista-mlodsza-specjalistka-ds-wsparcia-it-warszawa-postepu-18a,oferta,1004441321	2025-11-15 18:33:56.050708+01
72	starszy-inzynier-ds-jakosci-systemow-informatycznych-warszawa-szturmowa-2,oferta,1004417953	2025-11-15 18:33:56.051196+01
73	it-security-continuous-improvement-manager-fixed-term-contract-20-months-wroclaw-kazimierza-wielkiego-3,oferta,1004449146	2025-11-15 18:33:56.052639+01
74	informatyk-wsparcie-techniczne-l1-warszawa-patriotow-303,oferta,1004440095	2025-11-15 18:33:56.050253+01
75	specjalista-ds-sieci-i-bezpieczenstwa-it-warszawa,oferta,1004442069	2025-11-15 18:33:56.052504+01
76	informatyk-do-spraw-wsparcia-teleinformatycznego-pracownikow-uke-w-wydziale-wspa-warszawa-gieldowa-7,oferta,1004456205	2025-11-15 18:33:56.051579+01
91	specjalista-it-administrator-it-kozmin-wielkopolski,oferta,1004466730	2025-11-15 18:34:54.462675+01
92	administrator-ds-bezpieczenstwa-it-k-m-bierun,oferta,1004438282	2025-11-15 18:34:54.465208+01
93	analityk-ds-bezpieczenstwa-it-k-m-bierun,oferta,1004438247	2025-11-15 18:34:54.465154+01
94	administrator-systemu-informatycznego-gliwice,oferta,1004466177	2025-11-15 18:34:54.457959+01
95	specjalista-ds-wsparcia-it-elizowka-pow-lubelski,oferta,1004451675	2025-11-15 18:34:54.459499+01
96	informatyk-specjalista-ds-wsparcia-it-krakow-ksiedza-kazimierza-siemaszki-25,oferta,1004465915	2025-11-15 18:34:54.463702+01
97	specjalista-wsparcia-it-warszawa-jutrzenki-139,oferta,1004451270	2025-11-15 18:34:54.467+01
98	informatyk-warszawa-rynek-starego-miasta-31,oferta,1004464756	2025-11-15 18:34:54.463986+01
99	administrator-administratorka-systemow-informatycznych-i-aplikacji-biznesowych-lubaczow,oferta,1004464751	2025-11-15 18:34:54.467742+01
100	informatyk-lodz-pomorska-41,oferta,1004464684	2025-11-15 18:34:54.469405+01
101	starszy-informatyk-helpdesk-warszawa-ludwika-pasteura-3,oferta,1004464574	2025-11-15 18:34:54.468619+01
102	asystent-asystentka-ds-wsparcia-it-plock,oferta,1004424317	2025-11-15 18:34:54.468076+01
103	specjalista-specjalistka-ds-wsparcia-it-warszawa-zeganska-1,oferta,1004424141	2025-11-15 18:34:54.469217+01
104	specjalistka-specjalista-ds-bezpieczenstwa-sieci-lte-warszawa-sienna-39,oferta,1004432140	2025-11-15 18:34:54.46966+01
105	stanowisko-ds-informatyki-w-zakresie-statystyki-i-prognoz-aktuarialnych-warszawa-szamocka-3,oferta,1004448342	2025-11-15 18:34:54.471593+01
106	specjalista-ds-informatyki-k-m-warszawa-aleje-jerozolimskie-132,oferta,1004431493	2025-11-15 18:34:54.469559+01
107	it-security-specialist-krakow-jozefa-marcika-14a,oferta,1004434323	2025-11-15 18:34:54.472001+01
108	junior-it-administrator-warszawa-aleja-zjednoczenia-36,oferta,1004447101	2025-11-15 18:34:54.469842+01
109	specjalista-ds-wsparcia-it-lublin,oferta,1004429658	2025-11-15 18:34:54.469834+01
110	specjalista-specjalistka-ds-informatyki-olsztyn,oferta,1004460640	2025-11-15 18:34:54.468983+01
111	specjalista-ds-bezpieczenstwa-it-m-k-katowice-kolejowa-17,oferta,1004429375	2025-11-15 18:34:54.473939+01
112	mlodszy-a-specjalista-ka-ds-wsparcia-it-junior-it-support-specialist-warszawa-tytusa-chalubinskiego-8,oferta,1004428402	2025-11-15 18:34:54.471991+01
113	senior-oracle-developer-warszawa-jana-olbrachta-94,oferta,1004475491	2025-11-15 18:34:54.473497+01
114	specjalista-specjalistka-ds-wsparcia-it-wroclaw,oferta,1004458280	2025-11-15 18:34:54.471653+01
115	informatyk-administrator-sieci-rzeszow-biznesowa-6,oferta,1004458159	2025-11-15 18:34:54.472523+01
116	specjalista-ds-wsparcia-it-helpdesk-pruszkow-promyka-153,oferta,1004429160	2025-11-15 18:34:54.474748+01
117	administrator-administratorka-systemow-informatycznych-wroclaw-wybrzeze-stanislawa-wyspianskiego-27,oferta,1004442490	2025-11-15 18:34:54.473124+01
118	it-security-manager-katowice,oferta,1004426664	2025-11-15 18:34:54.475495+01
119	specjalista-specjalistka-ds-wsparcia-i-wdrozen-it-lodz-piekna-1,oferta,1004426984	2025-11-15 18:35:24.705682+01
120	informatyk-serwisant-warszawa-szlachecka-45,oferta,1004425396	2025-11-15 18:35:24.705172+01
121	informatyk-warszawa-karmelicka-9,oferta,1004450536	2025-11-15 18:35:24.704204+01
122	urzednik-stazysta-docelowo-technik-informatyk-zielona-gora,oferta,1004420620	2025-11-15 18:35:24.705231+01
123	starszy-informatyk-olsztyn-aleja-wojska-polskiego-37,oferta,1004417357	2025-11-15 18:35:24.707202+01
124	specjalista-ds-sieci-i-bezpieczenstwa-network-security-specialist-warszawa-aleje-jerozolimskie-142b,oferta,1004417238	2025-11-15 18:35:24.708736+01
125	informatyk-medyczny-wroclaw-weigla-5,oferta,1004420822	2025-11-15 18:35:24.704883+01
126	senior-it-analyst-tester-analityk-danych-wroclaw-skarbowcow-23b,oferta,1004464943	2025-11-15 18:35:24.707715+01
127	mobile-platform-engineering-lead-poznan-piatkowska-161,oferta,1004424762	2025-11-15 18:35:24.707827+01
128	tech-leader-senior-fullstack-developer-python-angular-branza-edukacyjna-warszawa,oferta,1004424793	2025-11-15 18:35:24.707221+01
129	kierownik-ds-rozwoju-i-utrzymania-systemow-it-warszawa-pulawska-472,oferta,1004475228	2025-11-15 18:35:24.709052+01
130	specjalista-ka-ds-utrzymania-i-rozwoju-aplikacji-biznesowych-warszawa-rondo-ignacego-daszynskiego-1,oferta,1004450410	2025-11-15 18:35:24.708145+01
131	specjalistka-specjalista-ds-modelowania-danych-platform-efm-warszawa-swietokrzyska-36,oferta,1004475149	2025-11-15 18:35:24.708145+01
132	starszy-specjalista-ds-monitorowania-i-obslugi-incydentow-ict-k-m-warszawa-chmielna-73,oferta,1004438855	2025-11-15 18:35:24.711397+01
133	architekt-big-data-warszawa-grzybowska-49,oferta,1004475089	2025-11-15 18:35:24.705584+01
134	analityk-biznesowo-systemowy-ubezpieczenia-warszawa-aleje-jerozolimskie-94,oferta,1004447598	2025-11-15 18:35:24.707375+01
135	big-data-developer-warszawa-grzybowska-49,oferta,1004475054	2025-11-15 18:35:24.711692+01
136	service-desk-agent-with-spanish-and-english-wroclaw-wyscigowa-58,oferta,1004458577	2025-11-15 18:35:24.711684+01
137	ms-fabric-engineer-with-ai-gliwice-przewozowa-32,oferta,1004441716	2025-11-15 18:35:24.711902+01
138	fullstack-developer-poznan,oferta,1004441650	2025-11-15 18:35:24.713404+01
139	koordynator-koordynatorka-projektow-wdrozeniowych-it-wroclaw,oferta,1004464022	2025-11-15 18:35:24.713603+01
140	engineering-team-leader-onboarding-warszawa-prosta-67,oferta,1004421618	2025-11-15 18:35:24.713798+01
141	soc-analyst-tier-1-warszawa-grochowska-278,oferta,1004474606	2025-11-15 18:35:24.712761+01
142	f-m-data-quality-analyst-warszawa,oferta,1004466956	2025-11-15 18:35:24.712451+01
143	administrator-aplikacji-i-systemow-it-warszawa-czerniakowska-87a,oferta,1004474439	2025-11-15 18:35:24.712349+01
144	projektantka-projektant-botow-konwersacyjnych-ai-warszawa-chmielna-89,oferta,1004474562	2025-11-15 18:35:24.712569+01
145	robotics-software-engineer-poznan-gajowa-6,oferta,1004473710	2025-11-15 18:36:01.955121+01
146	stanowisko-ds-wdrazania-oprogramowania-ue-warszawa-szamocka-3,oferta,1004473700	2025-11-15 18:36:01.95412+01
147	analityk-systemowy-warszawa,oferta,1004430311	2025-11-15 18:36:01.955083+01
148	f-m-data-tester-warszawa,oferta,1004466962	2025-11-15 18:36:01.954665+01
149	f-m-data-governance-specialist-warszawa,oferta,1004466942	2025-11-15 18:36:01.952731+01
150	analityk-systemowy-wroclaw-legnicka-70,oferta,1004433002	2025-11-15 18:36:01.954342+01
151	f-m-data-automation-tester-warszawa,oferta,1004466841	2025-11-15 18:36:01.954709+01
152	f-m-data-engineer-warszawa,oferta,1004466836	2025-11-15 18:36:01.954231+01
153	platny-staz-w-dziale-cyfryzacji-produkcji-mirkow-pow-wroclawski-wroclawska-43,oferta,1004469704	2025-11-15 18:36:01.95408+01
154	programista-symfony-react-w-referacie-ds-rozwiazan-wlasnych-w-centrum-obslugi-in-krakow-basztowa-20,oferta,1004463749	2025-11-15 18:36:01.951699+01
155	pracownik-dzialu-utrzymania-klienta-bydgoszcz-pod-skarpa-51a,oferta,1004449602	2025-11-15 18:36:01.951938+01
156	administrator-aplikacji-warszawa-postepu-18,oferta,1004469501	2025-11-15 18:36:01.954765+01
157	analityk-danych-warszawa-gwiazdzista-19,oferta,1004463535	2025-11-15 18:36:01.952647+01
158	mlodszy-specjalista-ds-raportowania-warszawa-dolna-3,oferta,1004473827	2025-11-15 18:36:01.952876+01
159	stanowisko-ds-nowych-technologii-it-warszawa-szamocka-3,oferta,1004473701	2025-11-15 18:36:01.952505+01
160	architekt-rozwiazan-it-solution-architect-obszar-testow-automatycznych-warszawa,oferta,1004463304	2025-11-15 18:36:01.955422+01
161	kierownik-dzialu-innowacji-lotnictwo-it-ai-warszawa-wiezowa-8,oferta,1004449058	2025-11-15 18:36:01.958746+01
162	senior-network-administrator-warszawa-zwirki-i-wigury-16,oferta,1004473462	2025-11-15 18:36:01.960039+01
163	architekt-soc-warszawa,oferta,1004423083	2025-11-15 18:36:01.955999+01
164	architekt-systemow-it-warszawa-aleje-jerozolimskie-160,oferta,1004440180	2025-11-15 18:36:01.961158+01
165	analityk-systemowo-biznesowy-warszawa-aleje-jerozolimskie-160,oferta,1004440183	2025-11-15 18:36:01.961129+01
166	analityk-analityczka-jakosci-danych-w-biurze-zarzadzania-danymi-warszawa-zubra-1,oferta,1004473328	2025-11-15 18:36:01.957824+01
167	starszy-specjalista-techniczny-wsparcia-operacyjnego-warszawa-postepu-12a,oferta,1004473284	2025-11-15 18:36:01.958376+01
168	mlodszy-wdrozeniowiec-warszawa-adama-branickiego-13,oferta,1004468844	2025-11-15 18:36:01.958789+01
169	fullstack-java-developer-warszawa,oferta,1004473166	2025-11-15 18:36:01.958067+01
170	technical-delivery-manager-warszawa,oferta,1004473024	2025-11-15 18:36:35.42489+01
171	administrator-ka-systemow-it-warszawa-rondo-ignacego-daszynskiego-1,oferta,1004462756	2025-11-15 18:36:35.429235+01
172	embedded-software-developer-cybersecurity-wroclaw,oferta,1004445837	2025-11-15 18:36:35.428197+01
173	analityk-systemowy-warszawa-adama-branickiego-13,oferta,1004472700	2025-11-15 18:36:35.429421+01
174	architekt-ds-sieci-transmisyjnej-warszawa,oferta,1004472644	2025-11-15 18:36:35.429564+01
175	specjalistka-specjalista-ds-informacji-zarzadczej-warszawa-marcina-kasprzaka-2,oferta,1004468418	2025-11-15 18:36:35.424541+01
176	kierownik-projektow-telekomunikacyjnych-warszawa-szczesna-26,oferta,1004445577	2025-11-15 18:36:35.423232+01
177	specjalista-ds-administracji-i-wsparcia-systemow-lublin-melgiewska-104,oferta,1004472376	2025-11-15 18:36:35.429245+01
178	tester-manualny-testerka-manualna-warszawa-brukselska-14,oferta,1004431192	2025-11-15 18:36:35.423901+01
179	inzynier-systemowy-krakow-galicyjska-1,oferta,1004464984	2025-11-15 18:36:35.433333+01
180	ekspert-ds-analiz-i-modelowania-ryzyka-kredytowego-wroclaw,oferta,1004467074	2025-11-15 18:36:35.429231+01
181	full-stack-net-web-developer-warszawa,oferta,1004453868	2025-11-15 18:36:35.423677+01
182	specjalista-wsparcia-technicznego-l1-warszawa-bernardynska-16a,oferta,1004445274	2025-11-15 18:36:35.42324+01
183	principal-consultant-microsoft-dynamics-365-f-o-gdansk-marynarki-polskiej-197,oferta,1004439391	2025-11-15 18:36:35.423694+01
184	starszy-inzynier-oprogramowania-ozarow-mazowiecki,oferta,1004456334	2025-11-15 18:36:35.434439+01
185	analityk-biznesowo-systemowy-analityczka-biznesowo-systemowa-warszawa-krakowiakow-42,oferta,1004445170	2025-11-15 18:36:35.433777+01
186	analityk-czka-soc-l2-tribe-security-warszawa-marynarska-12,oferta,1004444905	2025-11-15 18:36:35.433961+01
187	ekspert-ka-ds-systemu-zarzadzania-bezpieczenstwem-informacji-warszawa,oferta,1004461791	2025-11-15 18:36:35.434296+01
188	junior-t-sql-c%23-developer-warszawa-romana-popiolka-20,oferta,1004461789	2025-11-15 18:36:35.431965+01
189	ekspert-database-developer-oracle-wroclaw-skarbowcow-23b,oferta,1004455521	2025-11-15 18:36:35.436374+01
190	administrator-systemow-bankowych-wroclaw-legnicka-70,oferta,1004425012	2025-11-15 18:36:35.434784+01
191	tester-manualny-branza-finansowa-warszawa,oferta,1004421615	2025-11-15 18:36:35.436805+01
192	specjalista-specjalistka-ds-wdrozen-i-utrzymania-systemow-it-dla-uczelni-bialystok-lipowa-32,oferta,1004464276	2025-11-15 18:36:35.436561+01
193	data-scientist-w-crm-modelowanie-analizy-warszawa-zubra-1,oferta,1004444763	2025-11-15 18:36:35.435989+01
194	programista-java-warszawa,oferta,1004421395	2025-11-15 18:37:23.440776+01
195	t-business-architekt-rozwiazan-ict-presales-warszawa-marynarska-12,oferta,1004444600	2025-11-15 18:37:23.441246+01
196	ekspert-ekspertka-ds-procesow-it-warszawa,oferta,1004444471	2025-11-15 18:37:23.450957+01
197	starszy-administrator-infrastruktury-sieciowej-f5-waf-k-m-warszawa-chmielna-73,oferta,1004426578	2025-11-15 18:37:23.451833+01
198	kierownik-projektu-it-e-invoicing-krakow-aleja-jana-pawla-ii-39a,oferta,1004438415	2025-11-15 18:37:23.437193+01
199	analityk-biznesowy-ds-wdrozen-erp-warszawa-aleja-niepodleglosci-69,oferta,1004435595	2025-11-15 18:37:23.4393+01
200	system-analyst-ms-sql-%2b-t-sql-wroclaw,oferta,1004429768	2025-11-15 18:37:23.437325+01
201	net-developer-gliwice,oferta,1004426179	2025-11-15 18:37:23.437431+01
202	analityk-systemowy-obszar-bankowosci-bialystok-jozefa-marjanskiego-3,oferta,1004443648	2025-11-15 18:37:23.430459+01
203	architekt-it-warszawa-grzybowska-49,oferta,1004454048	2025-11-15 18:37:23.450738+01
204	kierownik-projektu-uslugi-chmurowe-i-cyberbezpieczenstwo-katowice-mikolowska-100,oferta,1004465941	2025-11-15 18:37:23.451456+01
205	mlodszy-specjalista-mlodsza-specjalistka-ds-it-help-desk-warszawa-zawodzie-22,oferta,1004434687	2025-11-15 18:37:23.44419+01
206	specjalista-ds-sieci-ip-warszawa,oferta,1004468639	2025-11-15 18:37:23.451456+01
207	kierownik-produktu-ict-warszawa-pulawska-464,oferta,1004443051	2025-11-15 18:37:23.449593+01
208	it-engineer-skarbimierz-osiedle-technologiczna-1,oferta,1004460034	2025-11-15 18:37:23.452599+01
209	data-scientist-expert-wroclaw-legnicka-70,oferta,1004425352	2025-11-15 18:37:23.452567+01
210	analityk-czka-systemowy-a-warszawa-kolska-12,oferta,1004442966	2025-11-15 18:37:23.452194+01
211	specjalista-ds-bezpieczenstwa-warszawa,oferta,1004453532	2025-11-15 18:37:23.452331+01
212	it-data-analyst-warszawa-marynarska-15,oferta,1004442890	2025-11-15 18:37:23.452927+01
213	analityk-czka-kampanii-crm-poznan-kolorowa-10,oferta,1004465291	2025-11-15 18:37:23.454044+01
214	junior-ml-ai-developer-poznan-jana-henryka-dabrowskiego-29,oferta,1004455766	2025-11-15 18:37:23.455235+01
215	projektant-analityk-systemowy-systemy-wsparcia-dowodzenia-i-kierowania-warszawa-poligonowa-30,oferta,1004431443	2025-11-15 18:37:23.454086+01
216	devops-engineer-data-on-premise-infrastructure-warszawa,oferta,1004459345	2025-11-15 18:37:23.452786+01
217	junior-power-platform-specialist-bialystok,oferta,1004464342	2025-11-15 18:37:23.455212+01
218	data-engineer-ai-nlp-mid-regular-lub-senior-warszawa-kolejowa-15-17,oferta,1004433470	2025-11-15 18:37:52.923968+01
219	inzynier-sieciowy-poznan,oferta,1004455443	2025-11-15 18:37:52.921581+01
220	analityk-analityczka-danych-poznan,oferta,1004464147	2025-11-15 18:37:52.923667+01
221	data-engineer-warszawa,oferta,1004436121	2025-11-15 18:37:52.923498+01
222	senior-angular-developer-branza-bankowa-warszawa,oferta,1004452173	2025-11-15 18:37:52.923455+01
223	kierownik-rozwoju-systemu-crm-warszawa-targowa-25,oferta,1004454748	2025-11-15 18:37:52.922057+01
224	analityk-czka-systemowy-a-ekspert-warszawa-rondo-ignacego-daszynskiego-1,oferta,1004458120	2025-11-15 18:37:52.922146+01
225	junior-backend-developer-lodz-piotrkowska-270,oferta,1004454658	2025-11-15 18:37:52.921492+01
226	net-developer-backend-poznan-aleje-solidarnosci-46,oferta,1004432495	2025-11-15 18:37:52.921752+01
227	starszy-specjalista-ds-analizy-biznesowej-i-systemowej-warszawa-dolna-3,oferta,1004435147	2025-11-15 18:37:52.923455+01
228	qa-test-engineer-gdynia,oferta,1004435094	2025-11-15 18:37:52.922109+01
229	senior-python-developer-warszawa,oferta,1004434729	2025-11-15 18:37:52.92211+01
230	starszy-inzynier-devops-warszawa,oferta,1004434730	2025-11-15 18:37:52.921943+01
231	ekspert-ds-systemow-transakcyjnych-k-m-warszawa-chmielna-73,oferta,1004453579	2025-11-15 18:37:52.925158+01
232	java-starter-kit-gdansk-aleja-grunwaldzka-472d,oferta,1004462564	2025-11-15 18:37:52.923691+01
233	programista-programistka-hurtowni-danych-warszawa-inflancka-4a,oferta,1004422392	2025-11-15 18:37:52.925367+01
234	architekt-cyberbezpieczenstwa-warszawa-kolska-12,oferta,1004453392	2025-11-15 18:37:52.927324+01
235	magazynier-kierowca-dzial-wyroby-hutnicze-CID4-ID16grdM.html	2025-11-15 21:48:46.116692+01
236	zatrudnie-magazyniera-magazyn-wysylkowy-mebli-tapicerowanych-CID4-ID14XgBB.html	2025-11-15 21:48:46.116482+01
237	magazynier-w-sklepie-internetowym-dla-studentow-zaocznych-CID4-ID18fA6A.html	2025-11-15 21:48:46.116247+01
240	pakowanie-wyrobow-czekoladowych-praca-sezonowa-CID4-ID185sSX.html	2025-11-15 21:48:46.116255+01
243	pracownik-sortowni-praca-dodatkowa-w-sortowni-paczek-CID4-ID185CNB.html	2025-11-15 21:48:46.11728+01
248	magazynier-na-porannej-zmianie-zyskaj-dodatkowy-zastrzyk-gotowki-CID4-ID17GQfK.html	2025-11-15 21:48:46.118142+01
249	sezonowa-praca-dla-studenta-pakowanie-zamowien-internetowych-CID4-ID18ehmP.html	2025-11-15 21:48:46.149969+01
250	magazynier-poszukiwany-CID4-ID17qiwI.html	2025-11-15 21:48:46.149927+01
251	dystrybutor-odziezy-sportowej-zatrudni-pracownika-CID4-ID18aImA.html	2025-11-15 21:48:46.149922+01
252	pracownik-fizyczny-w-magazynie-sortowni-odziezy-ostrow-wielkopolski-CID4-ID17ZeUv.html	2025-11-15 21:48:46.149683+01
255	prosta-praca-na-magazynie-bez-nocek-2-zmiany-premia-za-wydajnosc-CID4-ID17GFf5.html	2025-11-15 21:48:46.150611+01
256	pracownik-serwisu-sitodruku-oraz-magazynu-produkcyjnego-CID4-ID185uaF.html	2025-11-15 21:48:46.150655+01
262	magazynier-mango-caly-etat-westfield-mokotow-CID4-ID14G3BP.html	2025-11-15 21:48:46.152839+01
263	magazynier-branza-bhp-CID4-ID183Kpi.html	2025-11-15 21:48:46.152797+01
265	picking-zakupow-kompletowanie-zamowien-CID4-ID185rR4.html	2025-11-15 21:48:46.155225+01
267	wsparcie-produkcji-natychmiastowy-start-CID4-ID17ZsoB.html	2025-11-15 21:48:46.153229+01
270	praca-w-pralni-siemianowice-slaskie-CID4-ID17ZywD.html	2025-11-15 21:48:46.156624+01
273	praca-marzen-magazyn-w-internetowym-sklepie-wedkarskim-pleciona-pl-CID4-IDxhbOy.html	2025-11-15 21:48:46.157063+01
274	operator-wozka-widlowego-umowa-z-dhl-CID4-ID17XAeN.html	2025-11-15 21:48:46.158239+01
275	zatrudnimy-od-zaraz-magazyniera-ke-CID4-ID13lGX5.html	2025-11-15 21:48:46.156952+01
281	uzupelnianie-automatow-malopole-CID4-ID18aJT3.html	2025-11-15 21:48:46.157999+01
284	praca-dla-operator-wozka-widlowego-magazynier-CID4-IDS4NdT.html	2025-11-15 23:48:25.63479+01
247	magazyniermarka-crocsul-poleczki-23-CID4-ID18aK8G.html	2025-11-15 23:48:25.626713+01
244	magazynier-umowa-zlecenie-media-expert-zabrze-CID4-ID17Zsov.html	2025-11-15 23:48:25.626579+01
246	pracownik-magazynowy-jedna-zmiana-4100zl-netto-premia-CID4-ID18dapO.html	2025-11-15 23:48:25.629968+01
253	pracownik-magazynowy-umowa-o-prace-bezposrednio-z-dhl-CID4-ID17Vmp2.html	2025-11-15 23:48:25.630205+01
264	praca-magazynier-magazynierka-zgorzelec-CID4-ID17Z7hu.html	2025-11-15 23:48:25.628984+01
254	magazynier-stalowa-wola-CID4-ID18aCab.html	2025-11-15 23:48:25.637001+01
285	pracownik-magazynowy-w-drogerii-warszawa-szpotanskiego-4-CID4-ID18aGoO.html	2025-11-15 23:48:25.630205+01
261	magazynier-do-firmy-produkcyjnej-CID4-ID17tfQm.html	2025-11-15 23:48:25.628596+01
242	praca-magazynier-magazynierka-k-m-szczecin-CID4-ID18auCi.html	2025-11-15 23:48:25.630169+01
287	pracownik-magazynowy-w-drogerii-warszawa-aleje-jerozolimskie-179-CID4-ID17Zayz.html	2025-11-15 23:48:25.635884+01
258	magazynier-od-zaraz-37zl-39zl-h-brutto-CID4-ID17ZbzJ.html	2025-11-15 23:48:25.63843+01
288	magazynier-elastyczny-grafik-natychmiastowy-dostep-do-wynagrodzenia-CID4-ID18arHj.html	2025-11-15 23:48:25.633989+01
266	magazynier-katowice-CID4-ID18awxh.html	2025-11-15 23:48:25.636647+01
289	sprzedawca-magazynier-katowice-mekonomen-poland-CID4-ID185Byr.html	2025-11-15 23:48:25.639994+01
290	magazynier-kierowca-CID4-ID18aDnC.html	2025-11-15 23:48:25.634706+01
239	magazynier-kielce-inter-team-CID4-ID185zWR.html	2025-11-15 23:48:25.636798+01
291	magazynier-kierowca-pakowacz-kompletacja-zamowien-w-branzy-miesnej-CID4-IDI1Mbr.html	2025-11-15 23:48:25.637856+01
292	magazynier-w-sklepie-spozywczym-ul-bobrzynskiego-33-lewiatan-CID4-ID16BGUd.html	2025-11-15 23:48:25.637896+01
293	magazynier-bricomarche-sepolno-krajenskie-CID4-ID18aGH9.html	2025-11-15 23:48:25.636297+01
269	magazynier-dzial-zwrotow-CID4-ID17ZeYe.html	2025-11-15 23:48:25.637811+01
278	pracownik-magazynowy-umowa-dhl-gorzow-wielkopolski-CID4-ID16d7Go.html	2025-11-15 23:48:52.695438+01
259	pracownik-magazynu-czestochowa-ul-legionow-CID4-ID17Zxlp.html	2025-11-15 23:48:52.693618+01
268	sortowanie-paczek-CID4-ID17ZrA3.html	2025-11-15 23:48:52.696606+01
260	pracownik-magazynowy-amazon-sady-CID4-ID18aKMN.html	2025-11-15 23:48:52.691765+01
282	kontroler-stanow-magazynowych-specjalista-ds-inwentaryzacji-CID4-ID18axQI.html	2025-11-15 23:48:52.699787+01
271	praca-w-nowym-magazynie-mediaexpert-dolacz-do-extra-ekipy-CID4-ID18aHav.html	2025-11-15 23:48:52.718474+01
277	pracownik-magazynowy-amazon-sady-CID4-ID18aKMZ.html	2025-11-15 23:48:52.724827+01
257	prace-magazynowe-bez-doswiadczenia-elastyczne-godz-dhl-legnica-CID4-ID17HO9S.html	2025-11-15 23:48:52.72544+01
245	kierowca-magazynier-filia-inter-cars-CID4-ID185tVg.html	2025-11-15 23:49:48.351578+01
238	pracownik-magazynu-wzz-herbapol-sa-wroclaw-CID4-ID185BiO.html	2025-11-15 23:49:48.359662+01
280	pracownik-ds-rozladunku-towaru-CID4-ID18aJQl.html	2025-11-15 23:49:48.35923+01
283	kierownik-magazynu-CID4-ID18aIri.html	2025-11-15 23:49:48.360261+01
286	zatrudnie-magazyniera-praca-w-zakladzie-produkcyjnym-domow-mobilnych-CID4-ID16JayB.html	2025-11-15 23:49:48.36989+01
294	magazynier-biala-podlaska-CID4-ID18auEu.html	2025-11-15 23:48:25.641813+01
295	praca-magazynier-magazynierka-k-m-lomianki-CID4-ID185yYf.html	2025-11-15 23:48:25.640091+01
296	magazynier-lodz-CID4-ID185qRs.html	2025-11-15 23:48:25.643167+01
297	magazynier-operator-wozka-CID4-ID18aEyo.html	2025-11-15 23:48:25.642265+01
298	magazynier-dz-czesci-zamiennych-gsc-swadzim-CID4-ID185yyl.html	2025-11-15 23:48:25.64143+01
299	magazynier-sas-oddzial-w-glogoczowie-k-krakowa-CID4-ID12A5ct.html	2025-11-15 23:48:25.641283+01
300	magazynier-kierowca-w-branzy-rolniczej-CID4-ID17gpdx.html	2025-11-15 23:48:25.641898+01
301	magazynier-bezplatny-transport-CID4-IDL8xW6.html	2025-11-15 23:48:25.644789+01
302	magazynier-operator-wozka-widlowego-CID4-ID17Zlnu.html	2025-11-15 23:48:25.642867+01
303	praca-w-pyzdrach-jako-magazynier-handlowiec-1-zmiana-w-godz-8-16-CID4-ID18aBcr.html	2025-11-15 23:48:25.642681+01
276	pracownik-magazynowy-1-zmiana-bezplatny-dojazd-do-pracy-CID4-ID17Zcsy.html	2025-11-15 23:48:25.642635+01
304	praca-dla-pana-pani-w-mediaexpert-zlecenie-oraz-extra-stawka-40-zl-h-CID4-ID18aG9H.html	2025-11-15 23:48:25.642484+01
305	hebe-magazynier-sprzedawca-zielona-gora-focus-mall-CID4-ID185nic.html	2025-11-15 23:48:25.643178+01
272	pracownik-magazynowy-jedna-zmiana-4100zl-netto-premia-CID4-ID18datS.html	2025-11-15 23:48:25.642836+01
241	amazon-ostatnia-szansa-do-36-22zl-h-brutto-2000-zl-brutto-ekstra-CID4-ID17ZKyP.html	2025-11-15 23:48:25.642424+01
279	pracownik-magazynowy-1-zmiana-bezplatny-dojazd-do-pracy-CID4-ID17ZcAs.html	2025-11-15 23:48:25.644442+01
306	magazynier-koszalin-CID4-ID185uoo.html	2025-11-15 23:48:52.70989+01
307	praca-od-zaraz-dla-magazynierow-umowa-o-prace-zlecenie-duzo-wakatow-CID4-ID17ZtpL.html	2025-11-15 23:48:52.697428+01
308	magazynier-operator-wozka-jezdniowego-abler-torun-CID4-ID15cPbx.html	2025-11-15 23:48:52.694266+01
309	niemcy-praca-na-stanowisku-magazyniera-bez-doswiadczenia-CID4-ID17WuQF.html	2025-11-15 23:48:52.692849+01
310	magazynier-z-udt-bez-doswiadczenia-od-zaraz-CID4-ID18aY71.html	2025-11-15 23:48:52.693268+01
311	pracownik-magazynu-wysoka-stawka-i-grafik-bez-nocnych-zmian-CID4-ID17Zd1Y.html	2025-11-15 23:48:52.695041+01
313	magazynier-obsluga-wozka-jezdniowego-CID4-ID18ayP3.html	2025-11-15 23:48:52.693514+01
314	magazynier-kierowca-CID4-ID18aCIx.html	2025-11-15 23:48:52.710817+01
315	magazynier-e-commerce-kierowca-CID4-ID18em1z.html	2025-11-15 23:48:52.710064+01
316	magazynier-bezplatny-transport-CID4-IDJM0lY.html	2025-11-15 23:48:52.712133+01
317	magazynier-serwisant-CID4-ID185so3.html	2025-11-15 23:48:52.710328+01
318	rtv-euro-agd-prace-magazynowe-1-2-etatu-1500-1900-lodz-CID4-ID17ZnOa.html	2025-11-15 23:48:52.708656+01
319	magazynier-kompletacja-elementow-aluminiowych-i-drewnianych-CID4-ID185kRH.html	2025-11-15 23:48:52.712627+01
320	pracownik-magazynowy-poszukiwany-CID4-ID18aGYS.html	2025-11-15 23:48:52.717315+01
321	magazynier-poszukiwany-do-mediaexpert-umowa-o-prace-CID4-ID18aGVt.html	2025-11-15 23:48:52.718916+01
322	do-38-zl-h-operator-wozka-widlowego-bez-nocek-legnica-CID4-ID185lSN.html	2025-11-15 23:48:52.714439+01
323	magazynier-kierowca-kat-c-e-media-expert-praca-w-niedziele-CID4-ID185xRq.html	2025-11-15 23:48:52.715352+01
324	pracownik-magazynu-5-000-premie-i-darmowy-transport-do-z-pracy-CID4-ID185rwX.html	2025-11-15 23:48:52.715214+01
325	niemcy-praca-na-stanowisku-magazyniera-bez-doswiadczenia-CID4-ID17S9AI.html	2025-11-15 23:48:52.718527+01
326	magazynier-intar-sp-z-o-o-w-sycowie-CID4-ID18eDjl.html	2025-11-15 23:48:52.724234+01
327	mlodszy-magazynier-pszczyna-jedna-zmiana-pon-pt-CID4-ID17Z9Rd.html	2025-11-15 23:48:52.731318+01
328	magazyn-prawo-jazdy-lub-uprawnienia-udt-dla-kazego-extra-stawka-CID4-ID18awaR.html	2025-11-15 23:48:52.721627+01
329	magazynier-fedex-CID4-ID17Z9qh.html	2025-11-15 23:48:52.733529+01
330	zgarnij-800-zl-bonusu-jako-magazynier-ostatnie-dni-rekrutacji-CID4-ID18atmv.html	2025-11-15 23:48:52.731659+01
331	praca-na-magazynie-kompletacja-zamowien-2000zl-premii-30-50zl-h-CID4-ID17gBkS.html	2025-11-15 23:48:52.728211+01
332	pracownik-produkcji-jedna-zmiana-obrobka-aluminium-montaz-magazyn-CID4-ID18apfl.html	2025-11-15 23:48:52.731595+01
333	magazynier-gdansk-kowale-CID4-ID185tCL.html	2025-11-15 23:48:52.730208+01
336	praca-sezonowa-dla-studentow-pracownik-pracownica-magazynu-CID4-ID17ZsYr.html	2025-11-15 23:48:52.734509+01
337	magazynier-dowoz-z-wielu-lokalizacji-CID4-ID17ZtHG.html	2025-11-15 23:48:52.734723+01
338	pracownik-magazynowy-amazon-sady-CID4-ID18aKMU.html	2025-11-15 23:48:52.735424+01
340	poszukiwany-magazynier-z-uprawnieniami-udt-jedna-zmiana-CID4-ID18aGPm.html	2025-11-15 23:48:52.73832+01
341	zgarnij-do-40-zl-h-praca-przy-kompletacji-zamowien-CID4-ID18aIGA.html	2025-11-15 23:48:52.738891+01
342	pracownik-magazynu-praca-z-czesciami-samochodowymi-CID4-ID17ZxVA.html	2025-11-15 23:49:48.351559+01
343	pracownik-magazyny-wysokiego-skladowania-CID4-IDeVHZ1.html	2025-11-15 23:49:48.347503+01
344	magazynier-bez-doswiadczenia-tygodniowki-CID4-ID17ZkDV.html	2025-11-15 23:49:48.349488+01
345	amazon-ostatnia-szansa-do-36-22zl-h-brutto-2000-zl-brutto-ekstra-CID4-ID17ZKz5.html	2025-11-15 23:49:48.347431+01
334	praca-w-niemczech-jako-magazynier-CID4-ID185wc9.html	2025-11-15 23:49:48.343013+01
335	magazynier-praca-na-magazynie-w-wieluniu-CID4-ID17F7Ms.html	2025-11-15 23:49:48.348085+01
346	magazynier-ul-inwestycyjna-3-zmiany-CID4-ID17ZrBs.html	2025-11-15 23:49:48.342986+01
347	amazon-ostatnia-szansa-do-36-22zl-h-brutto-2000-zl-brutto-ekstra-CID4-ID17ZKAV.html	2025-11-15 23:49:48.347246+01
348	praca-w-hurtowni-spozywczej-eurocash-c-c-CID4-ID18aMh6.html	2025-11-15 23:49:48.352461+01
349	magazynier-z-orzeczeniem-CID4-ID17ZvfT.html	2025-11-15 23:49:48.351575+01
350	praca-na-zleceniu-mediaexpert-szuka-magazynierow-stawka-do-40-zl-h-CID4-ID18aG0m.html	2025-11-15 23:49:48.352283+01
351	wozki-widlowe-kwatera-270-halberstadt-CID4-ID18aGZ6.html	2025-11-15 23:49:48.352507+01
352	prace-magazynowe-pakowanie-ukladanie-sprzatanie-dorotowo-k-olsztyna-CID4-ID18cLmj.html	2025-11-15 23:49:48.35899+01
339	magazynier-komisjoner-order-picker-berlin-15-20eur-godz-CID4-ID18cIyh.html	2025-11-15 23:49:48.359487+01
353	magazynier-z-obsluga-wozka-widlowego-CID4-ID16PmxY.html	2025-11-15 23:49:48.35894+01
354	operator-wozka-widlowego-CID4-ID18ao4H.html	2025-11-15 23:49:48.359603+01
355	pracownik-ds-rozladunku-towarow-branza-spozywcza-CID4-ID185AwM.html	2025-11-15 23:49:48.359813+01
356	dodatkowa-praca-przy-rozladunkach-siec-restauracji-gdansk-CID4-ID18asRZ.html	2025-11-15 23:49:48.359856+01
357	magazynier-pipelife-warszawa-modlinska-CID4-ID18aMQo.html	2025-11-15 23:49:48.359955+01
358	praca-bez-doswiadczenia-magazyn-blisko-gdanska-niepelny-etat-CID4-ID17Zuwi.html	2025-11-15 23:49:48.360298+01
359	pracownik-ds-rozladunku-towarow-branza-spozywcza-CID4-ID185AdG.html	2025-11-15 23:49:48.360943+01
360	magazynier-topaz-CID4-ID18fm4I.html	2025-11-15 23:49:48.364073+01
361	niemcy-praca-na-stanowisku-magazyniera-bez-doswiadczenia-CID4-ID17S9pO.html	2025-11-15 23:49:48.364281+01
362	pracownik-magazynowy-CID4-ID185vlI.html	2025-11-15 23:49:48.364744+01
363	pracownik-magazynu-z-narzedziami-k-m-praca-od-zaraz-CID4-ID18aI0s.html	2025-11-15 23:49:48.369644+01
364	doswiadczony-magazynier-z-udt-poszukiwany-CID4-ID18ayuS.html	2025-11-15 23:49:48.364237+01
365	kontroler-stanow-magazynowych-media-expert-lodz-CID4-ID18aKSv.html	2025-11-15 23:49:48.369171+01
366	kierowca-magazynier-do-firmy-best-partner-kielce-CID4-IDPqyLu.html	2025-11-15 23:49:48.364237+01
367	dodatkowa-praca-przy-rozladunkach-siec-restauracji-kutno-CID4-ID18aNDU.html	2025-11-15 23:49:48.364376+01
368	magazynier-kierowca-CID4-ID18aCwu.html	2025-11-15 23:49:48.369916+01
369	pracownik-ds-rozladunku-towaru-CID4-ID18aGJP.html	2025-11-15 23:49:48.365744+01
370	poszukiwany-pracownik-magazynu-w-emilianowie-bez-doswiadczenia-CID4-ID18aM6v.html	2025-11-15 23:49:48.367928+01
371	magazynier-zaopatrzeniowec-CID4-ID185ATG.html	2025-11-15 23:49:48.369419+01
312	praca-magazynier-saint-gobain-1-zm-CID4-ID18aLLg.html	2025-11-15 23:49:48.36931+01
372	magazynier-tool-equipment-assistant-lubczynska-6-CID4-ID17KcnP.html	2025-11-15 23:49:48.370908+01
373	pracownik-ds-rozladunku-towarow-branza-spozywcza-CID4-ID185AdM.html	2025-11-15 23:49:48.369982+01
374	dodatkowa-praca-przy-rozladunkach-siec-restauracji-czluchow-CID4-ID185za7.html	2025-11-15 23:49:48.369432+01
375	pracownik-magazynu-stabilna-praca-i-przyjazna-atmosfera-czekaja-CID4-ID17ZzQO.html	2025-11-15 23:49:48.370818+01
376	3011453/technik-jakosci-laboratorium-umowa-o-prace-gi-group-s-a-	2025-11-16 00:55:56.922381+01
377	3014148/team-leader-k-m-umowa-o-prace-dijo-baking-sp-z-o-o-	2025-11-16 00:55:56.916992+01
401	3020174/asystent-umowa-o-prace-instytut-biotechnologii-przemyslu-rolno-spozywczego-im-prof-waclawa-dabrowskiego-panstwowy-instytut-badawczy	2025-11-16 00:55:56.916284+01
378	3008510/clinical-account-manager-umowa-o-prace-michael-page-international-poland-sp-z-o-o-	2025-11-16 00:55:56.913472+01
402	3006605/technolog-specjalista-w-laboratorium-w-zespole-biologii-gamet-umowa-o-prace-instytut-rozrodu-zwierzat-i-badan-zywnosci-polskiej-akademii-nauk	2025-11-16 00:55:56.912663+01
403	3006474/elektryk-umowa-o-prace-wodociagi-leszczynskie-sp-z-o-o-	2025-11-16 00:55:56.913111+01
379	3004793/pracownik_czka-ds-pisarstwa-medycznego-w-cwbk-umowa-o-prace-narodowy-instytut-onkologii-im-marii-sklodowskiej-curie-panstwowy-instytut-badawczy-oddzial-w-krakowie	2025-11-16 00:55:56.916211+01
404	2971823/laborant-kosmetyczny-umowa-o-prace-adex-beauty-care-sp-z-o-o-	2025-11-16 00:55:56.912829+01
380	2491864/manager-sprzedazy-umowa-o-prace-lento	2025-11-16 00:55:56.912698+01
381	2491862/manager-sprzedazy-umowa-o-prace-lento	2025-11-16 00:55:56.912511+01
405	1498534/handlowiec-umowa-o-prace-lento	2025-11-16 00:55:56.913008+01
406	782478/technik-laboratoryjny-umowa-o-prace-lento	2025-11-16 00:55:56.913077+01
407	1440462/handlowiec-umowa-o-prace-lento	2025-11-16 00:55:56.91256+01
382	3019841/specjalista-ka-ds-serwisu-it-umowa-o-prace-grupa-azoty-s-a-	2025-11-16 00:55:56.9177+01
383	3013036/technical-support-specialist-with-german-umowa-o-prace-manpowergroup-sp-z-o-o-	2025-11-16 00:55:56.923173+01
384	3008959/specjalista-informatyk-umowa-o-prace-celma-indukta-spolka-akcyjna	2025-11-16 00:55:56.923827+01
385	3006311/pracownik-i-ej-linii-wsparcia-klienta-tester-oprogramowania-umowa-o-prace-docusoft	2025-11-16 00:55:56.945139+01
386	3003201/technik-ds-badan-i-rozwoju-umowa-o-prace-synthos-s-a-	2025-11-16 00:55:56.922606+01
387	3003418/specjalista-ds-sap-umowa-o-prace-klingspor-sp-z-o-o-	2025-11-16 00:55:56.922969+01
388	3020560/specjalista-umowa-o-prace-kujawsko-pomorska-wojewodzka-komenda-ohp	2025-11-16 00:55:56.917434+01
408	3020403/informatyk-umowa-o-prace	2025-11-16 00:55:56.917396+01
389	3019771/informatyk-r11-2689-1322-25-umowa-o-prace-wrc-polska-jacek-konieczny	2025-11-16 00:55:56.916449+01
409	3019272/informatyk-w-security-operation-center-umowa-o-prace	2025-11-16 00:55:56.923224+01
410	3017294/specjalista-pierwszej-linii-wsparcia-it-umowa-o-prace-polska-grupa-gornicza-s-a-	2025-11-16 00:55:56.91783+01
411	3017264/starszy-teleinformatyk-umowa-o-prace-regionalne-centrum-informatyki-krakow	2025-11-16 00:55:56.918769+01
412	3016448/technik-informatyk-umowa-o-prace	2025-11-16 00:55:56.921926+01
390	3016256/specjalista-ds-it-3-4-etatu-umowa-o-prace-slaska-wojewodzka-komenda-ochotniczych-hufcow-pracy-w-katowicach	2025-11-16 00:55:56.922194+01
391	3015623/technik-czka-umowa-o-prace-regionalne-centrum-informatyki-bydgoszcz-11-wojskowy-oddzial-gospodarczy	2025-11-16 00:55:56.92422+01
392	3015619/informatyk-umowa-o-prace-slaski-uniwersytet-medyczny-w-katowicach	2025-11-16 00:55:56.924805+01
413	3015591/podinspektor-ds-komputeryzacji-umowa-o-prace-urzad-miasta-oswiecim	2025-11-16 00:55:56.927319+01
393	3015558/technik-informatyk-umowa-o-prace-wyzsza-szkola-ekonomiczno-humanistyczna	2025-11-16 00:55:56.923137+01
394	3014621/informatyk-r11-2689-1322-25-umowa-o-prace-wrc-polska-jacek-konieczny	2025-11-16 00:55:56.923218+01
414	3014433/referent-informatyk-umowa-o-prace-eko-region-sp-z-o-o-	2025-11-16 00:55:56.924364+01
415	3014410/technik-informatyk-staz-praktyka	2025-11-16 00:55:56.923581+01
416	3013906/starszy-informatyk-umowa-o-prace-baza-lotnictwa-taktycznego	2025-11-16 00:55:56.927788+01
395	3013818/informatyk-umowa-o-prace-szpital-wojewodzki-im-jana-pawla-ii-w-belchatowie	2025-11-16 00:55:56.927911+01
396	3013782/pomoc-techniczna-umowa-o-prace-zarzad-zieleni-miejskiej	2025-11-16 00:55:56.93011+01
397	3013584/informatyk-umowa-o-prace-szpital-wojewodzki-im-jana-pawla-ii-w-belchatowie	2025-11-16 00:55:56.927908+01
417	3013210/informatyk-informatyczka%0Anr-2437-umowa-o-prace	2025-11-16 00:55:56.928857+01
418	3012389/informatyk-umowa-o-prace	2025-11-16 00:55:56.928326+01
398	3011704/asystent-w-laboratorium-kryminalistycznym-umowa-o-prace-komenda-wojewodzka-policji-w-rzeszowie	2025-11-16 00:55:56.929052+01
419	3010429/mlodszy-specjalista-ds-informatyki-umowa-o-prace-wodociagi-leszczynskie-sp-z-o-o-	2025-11-16 00:55:56.928554+01
399	3009236/specjalista-ds-informatyki-umowa-o-prace-generalna-dyrekcja-drog-krajowych-i-autostrad	2025-11-16 00:55:56.93359+01
420	3009218/programista-grafik-umowa-o-prace-juzjade-pl-sp-z-o-o-	2025-11-16 00:55:56.928756+01
421	3006868/130-teleinformatyk-umowa-o-prace-regionalne-centrum-informatyki	2025-11-16 00:55:56.927986+01
422	3006714/informatyk-umowa-o-prace-wojskowe-biuro-emerytalne-w-lublinie	2025-11-16 00:55:56.928514+01
423	3004715/informatyk-mlodszy-specjalista-ds-marketingu-umowa-o-prace-mewa-sp-z-o-o-spolka-komandytowa	2025-11-16 00:55:56.930109+01
400	3004189/informatyk-umowa-o-prace-muzeum-narodowe-we-wroclawiu	2025-11-16 00:55:56.932782+01
424	2989179/administrator-instalator-sieci-umowa-o-prace-datech-systemy-i-sieci-komputerowe-damian-pozniak	2025-11-16 00:55:56.930091+01
425	2975335/mlodszy-konsultant-wdrozeniowiec-umowa-o-prace-everest-przedsiebiorstwo-informatyczne-s-c-szreiter-marek-szreiter-halina	2025-11-16 00:55:56.930426+01
426	2944047/specjalista-ka-dzialu-helpdesk-umowa-o-prace-globtrak-polska-spolka-z-ograniczona-odpowiedzialnoscia	2025-11-16 00:57:19.192231+01
427	2804062/projektant-w-zakresie-sieci-teletechnicznych-umowa-o-prace-centralna-baza-ofert-pracy	2025-11-16 00:57:19.187014+01
428	2503800/monter-swiatlowodow-pracownik-fizyczny-umowa-o-prace-lento	2025-11-16 00:57:19.186258+01
429	1777656/programista-python-flask-pandas-umowa-o-prace-lento	2025-11-16 00:57:19.231484+01
430	1671445/staz-programista-umowa-o-prace-lento	2025-11-16 00:57:19.192575+01
431	1398294/staz-programistyczny-umowa-o-prace-lento	2025-11-16 00:57:19.234477+01
432	1398293/praktyki-studenckie-python-django-flask-javascript-umowa-o-prace-lento	2025-11-16 00:57:19.232722+01
433	1398291/staz-programista-frontend-developer-umowa-o-prace-lento	2025-11-16 00:57:19.237045+01
434	2934789/poszukiwana-osoba-ds-tworzenia-i-pozycjonowania-stron-umowa-o-prace-lento	2025-11-16 00:57:19.237254+01
435	739110/programista-znajomosc-kohana-7-do-crma_dodatkowa-na-zlecenia-umowa-o-prace-lento	2025-11-16 00:57:19.234689+01
436	1903339/programista-unity-umowa-o-prace-lento	2025-11-16 00:57:19.244306+01
437	2088448/zatrudnie-umowa-o-prace-lento	2025-11-16 00:57:19.237243+01
438	1907639/przygotuje-ekspertyzy-techniczne-umowa-o-prace-lento	2025-11-16 00:57:19.23289+01
439	1900524/senior-embedded-c-developer-umowa-o-prace-lento	2025-11-16 00:57:19.260144+01
440	1615642/java-developer-in-automotive-umowa-o-prace-lento	2025-11-16 00:57:19.250799+01
441	1615641/c-developer-umowa-o-prace-lento	2025-11-16 00:57:19.255429+01
442	1964760/przedstawiciel-handlowy-netia-umowa-zlecenie-lento	2025-11-16 00:57:19.24505+01
443	1902434/serwisant-sprzetu-apple-serwis-machelp-umowa-o-prace-lento	2025-11-16 00:57:19.252322+01
444	741337/zatrudnimy-umowa-o-prace-lento	2025-11-16 00:57:19.24728+01
445	2970448/specjalista-ds-wdrozen-systemow-wms-umowa-o-prace-lento	2025-11-16 00:57:19.253102+01
446	2761918/infromatyk-umowa-o-prace-lento	2025-11-16 00:57:19.255435+01
447	2676863/monter-telemonter-bydgoszcz-umowa-o-prace-lento	2025-11-16 00:57:19.251035+01
448	772711/zatrudnimy-elektryka-umowa-o-prace-lento	2025-11-16 00:57:19.247496+01
449	2363709/partner-doradca-biznesowy-play-umowa-o-prace-lento	2025-11-16 00:57:19.25348+01
450	934546/handlowiec-dzial-oprogramowania-erp-umowa-o-prace-lento	2025-11-16 00:57:19.255627+01
451	739209/praca-dla-elektromontera-umowa-o-prace-lento	2025-11-16 00:57:19.253319+01
452	1035289/praca-alpinista-monter-umowa-o-prace-lento	2025-11-16 00:57:19.255414+01
453	732400/specjalista-ds-bezpieczenstwa-it-umowa-o-prace-lento	2025-11-16 00:57:19.259575+01
454	738597/projektant-sieci-telekomunikacyjnych-umowa-o-prace-lento	2025-11-16 00:57:19.255617+01
455	735155/webmaster-w-sklepie-internetowym-ebay-de-amazon-umowa-o-prace-lento	2025-11-16 00:57:19.261364+01
456	738174/business-development-manager-umowa-o-prace-lento	2025-11-16 00:57:19.262224+01
457	2867811/pracownik-do-budowy-sieci-telekomunikacyjnej-umowa-o-prace-lento	2025-11-16 00:57:19.260324+01
458	1454681/inzynier-sprzedazy-i-serwisu-it-umowa-o-prace-lento	2025-11-16 00:57:19.261569+01
459	1312964/praktyki-informatyk-umowa-o-prace-lento	2025-11-16 00:57:19.262405+01
460	1212281/serwisant-monter-infrastruktury-telekomunikacyjnej-i-elektry-umowa-o-prace-lento	2025-11-16 00:57:19.260406+01
461	1212273/logistyk-magazynier-umowa-o-prace-lento	2025-11-16 00:57:19.261127+01
462	3002386/it-finance-recruiter-umowa-o-prace-lento	2025-11-16 00:57:19.267097+01
463	741195/poszukujemy-podwykonawcow-infrastruktura-telekomunikacyjna-umowa-o-prace-lento	2025-11-16 00:57:19.261169+01
464	2971595/trener-do-prowadzenia-kursu-komputerowego-dla-seniorow-umowa-zlecenie-lento	2025-11-16 00:57:19.260905+01
465	1936299/to-wlasnie-ciebie-szukam-do-swojego-zespolu-umowa-zlecenie-lento	2025-11-16 00:57:19.262368+01
466	739257/programista-php7_informatyk-framwor-kohana_dodatkowe-zlecen-umowa-o-prace-lento	2025-11-16 00:57:19.262462+01
467	2823518/programista-umowa-o-prace-lento	2025-11-16 00:57:19.268443+01
468	2138228/sales-engineer-umowa-o-prace-lento	2025-11-16 00:57:19.268535+01
469	2965045/sp-ds-inwestycji-telekomunikacyjnych-przyuczymy-studenta-umowa-o-prace-lento	2025-11-16 00:57:19.265743+01
470	1436531/specjalista-it-informatyk-umowa-o-prace-lento	2025-11-16 00:57:19.268424+01
471	2995518/junior-marketing-amp-sales-coordinator-umowa-zlecenie-lento	2025-11-16 00:57:19.268545+01
472	1891072/instalator-sieci-umowa-o-prace-lento	2025-11-16 00:57:19.267137+01
473	738279/informatyka-do-audytow-w-zakresie-ochrony-danych-osobowych-umowa-o-prace-lento	2025-11-16 00:57:19.268464+01
474	2993233/instalator-monter-swiatlowodow-umowa-o-prace-lento	2025-11-16 00:57:19.26877+01
475	2993232/praca-monter-instalacji-swiatlowodowych-umowa-o-prace-lento	2025-11-16 00:57:19.268398+01
\.

COPY public.internal_offers (offer_id) FROM stdin;
\.

COPY public.languages_junction (offer_id, language_id, language_level_id) FROM stdin;
1	2	4
3	2	4
3	34	4
4	2	5
5	2	4
6	2	4
7	2	4
8	2	4
9	2	3
41	2	4
15	34	6
15	2	3
16	2	4
18	2	4
19	2	4
21	2	4
25	2	3
48	2	5
26	2	3
27	2	4
43	2	3
28	31	4
29	2	4
30	2	4
31	2	3
32	2	4
37	2	3
33	2	5
35	2	4
36	2	3
38	2	4
39	2	3
51	2	4
54	34	6
54	2	6
55	2	4
56	2	4
57	2	4
58	2	4
59	2	4
60	2	4
61	2	4
66	2	4
69	2	4
74	2	3
75	2	4
76	2	2
79	2	4
79	31	4
80	2	4
80	34	6
83	2	4
84	2	4
86	2	3
87	2	3
92	2	4
93	2	4
95	2	4
100	2	3
101	2	3
103	2	1
104	2	4
107	2	4
109	2	4
113	2	4
114	2	3
116	2	3
122	2	3
124	2	3
128	2	4
129	2	4
130	2	4
132	2	3
134	2	4
136	18	6
136	2	6
137	2	4
139	2	3
141	34	5
142	2	6
143	2	3
144	34	6
145	2	4
146	2	4
149	2	4
150	2	4
153	2	4
155	34	6
158	2	3
159	2	4
161	2	5
162	2	4
163	2	5
168	2	4
170	2	5
172	2	4
173	2	4
175	2	3
177	2	4
179	2	4
180	2	4
181	2	5
182	34	6
183	2	4
186	2	4
187	2	6
192	2	3
195	2	4
196	2	4
197	2	4
198	2	5
199	34	6
204	2	4
206	2	3
207	2	4
208	2	4
212	2	5
212	34	6
213	2	4
215	2	4
217	2	3
220	2	4
221	2	4
222	2	4
227	2	4
232	31	3
233	2	4
262	2	3
309	2	3
320	34	3
320	2	3
325	2	3
340	34	4
342	34	3
351	31	\N
351	2	\N
339	34	\N
280	34	3
355	34	3
356	34	3
359	34	3
361	2	3
367	34	3
369	34	3
370	34	1
372	2	4
373	34	3
374	34	3
401	2	4
401	34	6
378	2	4
379	2	4
406	2	3
382	2	\N
383	31	5
384	2	3
387	2	4
387	31	4
409	2	3
411	2	\N
413	2	3
413	34	6
393	2	3
394	2	\N
396	2	2
417	2	4
419	2	4
399	2	3
420	2	3
400	2	3
424	2	3
426	2	3
427	2	5
427	18	5
440	2	5
440	31	4
441	2	4
455	31	6
455	2	4
456	2	4
456	31	3
456	36	3
456	15	3
458	2	2
462	34	6
462	2	6
468	2	4
\.

COPY public.skills (id, skill) FROM stdin;
1	Python
2	FastAPI
3	Django
4	Flask
5	Git
6	Backend, Python
7	Celery
8	Mikroserwisy, PydanticAI
9	IaC, CI/CD
10	Prometheus, Grafana, Langfuse, Arize Phoenix
15	PostgreSQL
16	Bazy danych relacyjne
17	JWT
18	OAuth
19	REST API
23	SQL
24	ETL
25	Analiza danych
27	GIT
28	OpenCV
29	TensorFlow
32	Wizja komputerowa
33	Przetwarzanie obrazu
34	Uczenie maszynowe
35	Sztuczna inteligencja
36	Cyberbezpieczeństwo
37	SIEM, DAM, AV, EDR/XDR, DLP, IPS/IDS, NDR CASB, Skaner podatności, HSM
38	Windows, Linux, EntraID, GCP
39	TCP/IP
40	Python, PowerShell, Bash
41	CIS, NIST, DORA
42	Administracja Microsoft
43	Wirtualizacja
44	Chmura
45	Bezpieczeństwo IT
46	NGFW/IPS
47	NDR
48	LB/WAF
49	DNS Security
50	XDR
51	DB Security
53	Windows
54	Linux
56	Sieci Ethernet
58	Windows 10/11
59	Sieci komputerowe, TCP/IP
60	Google Workspace
61	Microsoft 365
63	Automatyzacja procesów
64	Analiza problemów
65	Windows Server
68	VLAN
69	DNS
70	DHCP
71	PowerShell
72	Zabbix
73	Nagios
74	ISO 27001
75	ITIL
78	MS Office
79	Microsoft Configuration Manager
80	Sprzęt komputerowy
81	Zdalny pulpit
82	Azure
83	Administrowanie systemami
84	Microsoft Windows
86	Sharepoint
87	LAN
88	Wsparcie techniczne
89	Wsparcie IT
90	Systemy operacyjne
91	Aplikacje
93	Windows 11
94	Pakiet Office
95	Konfiguracja sprzętu
96	Diagnostyka
97	Drukarki
98	Obsługa klienta
99	Zarządzanie czasem
100	Prawo jazdy
101	Obsługa sprzętu komputerowego
103	Microsoft Windows Serwer
104	Active Directory
105	Hyper-V
106	VMware
108	Sieci komputerowe
110	iOS
111	Microsoft Intune
113	Jira
114	Informatyka
115	Teleinformatyka
120	MS SQL Server
121	MySQL
123	Sieci SAN
124	Kopie zapasowe
129	Cisco
130	UTM
131	Zabezpieczenia sieciowe
132	Infrastruktura IT
134	Komunikacja
135	ITSM
136	Rozwiązywanie problemów
137	Innowacyjność
138	Excel
139	Power BI
142	LAN/WAN
143	Naprawa sprzętu komputerowego
144	Program Insert: Gestor, Subiekt
145	Wsparcie użytkownika
146	Serwis sprzętu komputerowego
147	Konfiguracja Windows
148	Microsoft Office
149	HTML
150	CSS
151	Praca zespołowa
152	Analityczne myślenie
153	Komunikatywność
154	Procesy magazynowe
155	Administracja
157	Administracja systemami Microsoft Server
158	Administracja Microsoft 365
160	Entra ID
161	Exchange
165	Obsługa komputera
167	Cisco, VLAN
168	Praca w zespole
169	Organizacja pracy
172	Mac OS
175	WLAN
176	CCTV
177	VoIP
178	Windows, Active Directory, Hyper-V
179	LAN, WLAN
180	Urządzenia sieciowe
181	Licencjonowanie oprogramowania Microsoft, Eset
184	SIEM
189	Zarządzanie zadaniami
190	Administracja SQL Server, Windows Server
191	SQL Server Profiler
192	SQL Server Integration Services
193	SQL Server Analysis Services
194	DBPLUS Performance Monitor
197	Rozwiązywanie problemów technicznych
198	Rozwiązywanie problemów IT
199	MS Windows
204	SSL/TLS
205	MDM
207	VPN
209	Konfiguracja urządzeń
210	MS Windows 11
211	MS Windows Server
212	Microsoft Azure
213	Autodesk Inventor
214	Autodesk Vault
219	Bezpieczeństwo sieci
222	EDR
223	Analiza ryzyka
224	Reagowanie na incydenty
225	Compliance
226	Ochrona danych
227	RODO/GDPR
228	Zarządzanie bezpieczeństwem informacji
229	Konfiguracja systemów i sieci
230	Firewall
231	IDS/IPS
232	SIEM Wazuh
233	SOAR
234	WAF
235	ATP
237	BURP
238	Techniki ataków
239	Podatności systemów i sieci
241	Praca projektowa
249	Architektura mikroserwisów
250	PydanticAI
251	IaC
252	CI/CD
253	Prometheus
254	Grafana
255	Langfuse
256	Arize Phoenix
267	Postgres
282	SIEM, DAM, AV, EDR/XDR, DLP, IPS/IDS, NDR CASB, HSM
303	IT
304	MS 365
306	NAS
310	NAT
311	Oprogramowanie
312	Budowa komputera
316	Google Workspace, Microsoft 365
319	Administracja systemami Linux
320	Administracja systemami Windows
331	iLO
336	Konfiguracja aplikacji klient-serwer
341	Diagnostyka usterek
343	Umiejętności interpersonalne
344	Branża IT
348	System rejestracji zgłoszeń
351	Konfiguracja komputerów
352	Diagnostyka komputerowa
353	Naprawa drukarek
354	Obsługa sal konferencyjnych
357	Microsoft Windows Serwer, Active Directory
358	TCP/IP, Ethernet
359	Router, Przełącznik, Mikrotik
361	IPMI/iDRAC
362	Linux, Windows Server
368	Microsoft Active Directory
369	SAP
418	Praca w środowisku przemysłowym
419	Planowanie
422	Praca pod presją czasu
432	Windows Server, Active Directory, Hyper-V
442	Analiza
445	SQL Server
454	Integracja Systemów Informatycznych, Web Services
455	XML, XSD
491	Python, Backend
495	Infrastructure as Code (IaC)
497	Prometheus, Grafana
498	Langfuse, Arize Phoenix
545	Prawo jazdy kat. B
549	Serwery plików NAS
550	Sieci VLAN/VPN/DHCP/NAT
553	Organizacja czasu pracy
555	Rozwój
560	Skrypty, Automatyzacja
576	Administrowanie Microsoft
595	Call logging
603	Ustalanie priorytetów
692	Licencjonowanie oprogramowania Microsoft
693	Eset
694	Prawo jazdy kategorii B
700	Administracja SQL Server
775	DAM
776	AV
777	EDR/XDR
778	DLP
779	IPS/IDS
780	NDR CASB
781	Skaner podatności
782	HSM
785	EntraID
786	GCP
790	Bash
791	Systemy operacyjne Microsoft
807	Łączność bezprzewodowa
817	Sieci komputerowe TCP/IP
841	Microsoft Configuration Manager (MCM/SCCM)
845	Konfiguracja
851	Praca Zespołowa
886	Wdrażanie systemów IT
891	Relacje biznesowe
907	Administracja IT
915	Windows, Linux
917	Insert Gestor, Insert Subiekt
921	Konfiguracja systemu Windows
954	Dyspozycyjność
955	Zaangażowanie
962	Licencjonowanie oprogramowania
963	Microsoft
991	Web Services, Integracja Systemów Informatycznych
1021	Doskonalenie standardów i procedur bezpieczeństwa IT
1061	Windows, Linux, EntraID, GCP, Bazy danych
1066	Technologie wirtualizacji
1067	Rozwiązania chmurowe
1098	Administracja bazami danych
1099	Microsoft SQL Server
1100	Oracle Database
1118	Instalacja i konfiguracja aplikacji
1132	Konfiguracja komputera
1141	Instalacja oprogramowania
1142	Konfiguracja oprogramowania
1146	Umiejętność ustalania priorytetów
1147	Zdolności interpersonalne
1169	Umiejętności analityczne
1180	SAN
1196	ERP
1198	API
1203	Sieci LAN/WAN
1204	Wsparcie użytkowników
1205	Serwis komputerowy
1206	Konfiguracja systemów Windows
1215	Intune
1216	Entra
1217	Azure AD
1218	Defender
1219	Windows 10
1221	ServiceNow
1222	Zendesk
1223	TeamViewer
1224	BeyondTrust
1225	Procesy Magazynowe
1226	Organizacja Pracy
1229	Microsoft Server
1242	Administracja sieciami Cisco
1256	Administracja Windows Server
1259	Administracja LAN
1260	Administracja WLAN
1278	Wdrażanie systemów informatycznych
1279	HL7, FHIR, RODO, Cyberbezpieczeństwo
1280	Systemy operacyjne, Infrastruktura sieciowa, Infrastruktura serwerowa
1281	Analiza danych, SQL
1282	MS Office, MS Teams
1303	Testowanie systemów informatycznych
1306	TestLink
1308	Myślenie analityczne
1333	Antywirus
1337	Routery
1338	Przełączniki
1339	Sandbox
1340	Systemy informatyczne
1342	Tworzenie obrazów dysków
1343	Office
1346	Administracja systemami
1348	WAN
1349	WIFI
1350	Konfiguracja urządzeń sieciowych
1353	VMware vSphere
1355	Dokumentacja techniczna
1356	Priorytetyzacja zadań
1358	Konfiguracja sieci
1359	Synology
1360	Dell
1361	FreeNAS
1364	Docker
1366	Usługi chmurowe
1367	Hardware
1368	Software
1369	Infrastruktura sieciowa
1376	Zarządzanie zespołem
1377	Jira Service Management
1379	Administracja systemami IT (Windows/Linux)
1380	Wielowątkowość
1385	Perl
1387	SSH
1388	IP
1391	M365
1394	Android
1395	Sieci LAN
1396	Sieci WLAN
1398	Podejmowanie decyzji
1401	Obsługa sprzętu biurowego
1402	Diagnozowanie problemów
1407	Administracja systemem Windows
1413	AD
1417	CAD
1419	MS SQL
1423	.NET
1424	C#
1431	HPC
1432	Monitorowanie sprzętu HPC
1433	Oprogramowanie naukowe
1434	Frontend
1435	Backend
1436	SLURM
1437	Chemia kwantowa
1440	Administracja Sieci LAN/WAN/WLAN
1442	Systemy ERP
1443	Routing Statyczny
1446	VPN - IPsec i SSL
1450	Go
1452	Konteneryzacja LXC/LXD
1453	Technologie sieciowe VPN
1454	Monitorowanie
1456	Bazy danych
1457	Usługi webowe
1460	Podatność systemów
1461	Podatność sieci
1463	PLC
1464	Microsoft Windows Server
1467	Sprzęt informatyczny
1473	Bezpieczeństwo systemów informatycznych
1479	Wiedza serwisowa
1480	Microsoft Office 365
1486	Selenium
1490	Analiza biznesowa
1492	Wiedza techniczna
1494	Zarządzanie dokumentacją
1496	Zarządzanie projektami
1498	Zarządzanie ryzykiem
1499	Microsoft Windows, M365
1502	Administracja sieciami komputerowymi
1503	Palo Alto Networks
1504	Ruckus
1505	Arista
1507	Zapory ogniowe Palo Alto Networks
1508	Monitoring sieci
1509	Diagnostyka sieci
1510	Zabezpieczenia sieci
1514	Oprogramowanie systemowe
1515	Aplikacje biurowe
1516	Office 365
1517	Podpis elektroniczny
1523	Szyfrowanie
1524	Monitoring CCTV
1538	Microsoft Purview
1540	Microsoft Entra ID
1541	Oprogramowanie antywirusowe
1542	Oprogramowanie antyspamowe
1543	Password management
1546	Privileged Access Management (PAM)
1549	Administracja sieci
1553	Skrypty
1554	Aplikacje WEB
1556	Administracja serwerami Windows
1557	Administracja serwerami Linux
1558	Proxmox
1566	Mechanika
1569	Sprzęt sieciowy
1570	Komputery PC
1578	Windows 10/11 PRO
1580	MS Office/M365
1581	Okablowanie sieciowe
1582	Microsoft Teams
1583	Team Viewer
1584	RPD
1585	Vmware
1588	Wi-Fi
1597	Microsoft 365 Security
1601	Sentinel
1602	ISO 27000
1603	NIST SP 800-60
1604	NIS 2
1606	Analiza logów
1608	Microsoft Exchange
1609	Eset Consola
1610	Axence nVision
1611	Raks SQL
1612	Linux Debian
1613	Samba
1617	Cisco Switch
1618	DrayTek Router
1620	Konica Minolta Bizhub
1621	Kontrola dostępu
1622	Q-Nap
1624	Microsoft SQL
1625	Telefonia IP (VolP)
1628	Microsoft365
1633	MS Excel
1635	WMS
1636	POS
1638	Administracja serwerami
1639	Bezpieczeństwo systemów operacyjnych Windows/Linux
1640	SIEM/SOAR
1645	HTTP
1647	Phishing
1648	Malware
1649	Ransomware
1652	Systemy operacyjne Windows/Linux
1653	Systemy mobilne
1664	Doświadczenie
1665	Obsługa urządzeń sieciowych
1666	Bezpieczeństwo systemów operacyjnych
1673	RDP
1674	Windows Firewall
1676	Druk sieciowy
1681	Bezpieczeństwo sieci komputerowych
1685	Szybkie uczenie się
1686	Kreatywność
1699	Administracja Windows
1700	Administracja Linux
1701	HYPER-V
1703	Veeam
1704	ESET
1705	FortiGate
1706	EZD
1707	Mikrotik
1709	Diagnostyka systemów Windows
1710	Testowanie urządzeń
1711	Diagnozowanie urządzeń
1712	Naprawa urządzeń
1723	Sieci LAN, WLAN, VLAN, VPN
1724	Protokół TCP/IP
1728	Nawiązywanie kontaktów
1730	Bezpieczeństwo sieci komórkowych 2G/3G/4G
1731	Protokoły sieciowe
1732	Szyfrowanie transmisji
1733	Routing
1738	Administracja siecią
1740	Bezpieczeństwo systemów
1742	Bezpieczeństwo chmurowe
1745	Linux Ubuntu Server
1746	Proxmox VE
1747	DHCP, DNS, NAT, VPN
1748	Hardware, BIOS
1759	Bezpieczeństwo teleinformatyczne
1760	Zarządzanie bezpieczeństwem IT
1761	Analiza incydentów bezpieczeństwa
1762	Zabezpieczenia systemów informatycznych
1763	Ochrona informacji
1764	Przeciwdziałanie atakom
1768	Pakiet biurowy
1769	Analiza zgłoszeń
1771	Oracle
1773	PL/SQL
1774	UML
1775	Java
1776	J2EE
1783	MikroTik RouterOS
1784	Ubiquiti UniFi
1790	QoS
1793	Outlook
1796	Telekomunikacja
1811	Automatyka
1813	Audyt
1826	Konfiguracja sprzętu komputerowego
1834	Odporność na stres
1835	Zdolności analityczne
1842	VMware Server
1845	Analiza zagrożeń
1847	Fortinet
1849	SMTP
1850	sFTP
1855	SD-WAN
1856	OSPF
1857	BGP
1858	EIGRP
1859	TCP
1860	UDP
1861	IPSEC VPN
1862	SSL VPN
1863	PKI
1865	Systemy IT
1867	Programowanie
1870	Nowe technologie
1874	Testowanie funkcjonalne
1875	Testowanie procesowe
1876	Testowanie
1879	Inżynieria oprogramowania
1880	Architektura systemów
1882	Angular
1883	Prompt engineering
1884	Architektura API
1886	UX design
1887	Zarządzanie IT
1888	Utrzymanie systemów informatycznych
1889	Rozwój systemów informatycznych
1890	Zdolności organizacyjne
1891	Samodyscyplina
1892	Systematyczność
1893	Proaktywność
1896	IT Service Management
1897	Zarządzanie portfelem projektowym
1898	PMO
1899	Negocjacje
1900	Prezentacja wyników analiz
1901	Rekomendacje
1902	Trendy IT
1903	Metodyki realizacji projektów IT
1911	Kubernetes
1914	Monitorowanie systemów ICT, Obsługa incydentów
1915	Zabbix, Prometheus, Elastic Stack, Grafana, Selenium, Alerta, Splunk, Dynatrace, Instana
1916	ITIL (Event, Incident, Problem Management)
1917	Jira, Confluence
1922	Big Data
1923	Architektura IT
1924	Dokumentacja projektowa
1925	Hadoop
1926	JIRA
1927	Confluence
1928	Enterprise Architect
1929	Bazy relacyjne
1930	NoSQL
1931	DevOps
1932	Ubezpieczenia
1938	Visual Paradigm
1940	Scala
1941	SOLID
1942	DRY
1943	TDD
1945	Spark
1947	SOAP
1948	REST
1949	MongoDB
1950	Cassandra
1951	HBase
1952	Apache Spark
1953	Hive
1954	Oozie
1955	Pig
1956	Sqoop
1957	Impala
1958	Kafka
1960	Obsługa zgłoszeń
1962	Microsoft Fabric
1966	TypeScript
1967	JavaScript
1968	HTML5
1969	CSS3
1970	RxJS
1971	Karma
1972	Jest
1974	Spring
1975	Spring Boot
1976	Hibernate
1977	JUnit
1978	Spock
1980	RabbitMQ
1983	OracleDB
1984	Zarządzanie projektami IT
1985	Agile
1986	Scrum
1987	Prince2
1988	PMI
1989	BPMN
1991	Pozyskiwanie wymagań
1993	Prezentacje
1997	Programowanie mobilne
1999	Skalowalność
2000	Wydajność
2001	Niezawodność
2003	Bezpieczeństwo informacji
2006	Social engineering
2008	Splunk
2010	QRadar
2012	Microsoft Defender
2013	CrowdStrike
2016	Data Quality
2017	Data Quality - zasady i procesy
2018	Narzędzia DQ
2021	DataBricks
2022	Snowflake
2023	Procesy bankowe
2024	Produkty finansowe
2027	Metodyki Agile
2030	Konfiguracja serwerów
2038	Chatboty, Voiceboty
2041	C++
2043	ROS
2044	ROS2
2047	Zarządzanie
2050	Analiza IT
2052	Wdrażanie IT
2055	Robotyka
2056	Matematyka
2057	Fizyka
2058	Analiza systemowa
2063	Kafka Confluent
2065	Integracja systemów
2067	Bazy danych nierelacyjne
2068	Wydajność systemów
2070	Analiza DWH
2071	Usługi finansowe
2072	Data Management
2076	Databricks
2078	Frameworki testowe
2079	Projektowanie testów
2081	Data Governance Analyst
2092	T-SQL
2096	draw.io
2097	Automatyzacja Testów
2098	Procesy Biznesowe
2099	Testowanie Oparte na Metadanych
2106	Frameworki Testowe
2107	Scenariusze Testowe
2111	Data Engineer
2115	ELT
2116	ODI
2117	dbt Cloud
2118	GitHub
2119	Airflow
2123	Data Science
2130	Symfony
2131	React
2133	Elektronika
2139	OpenShift
2142	Weblogic
2143	JBoss
2144	Tomcat
2145	IIS
2146	Apache
2152	Microsoft Excel
2154	Integracja danych
2157	Współpraca
2159	AI
2161	LLM/ML
2163	Testy automatyczne
2164	QA
2166	Jenkins
2167	GitLab CI
2168	Bamboo
2170	SAFe
2171	Raportowanie metryk jakości
2172	Prowadzenie warsztatów
2174	Mentoring
2176	Prince
2179	Lotnictwo
2180	Kreatywne myślenie
2186	Router
2187	Przełącznik
2189	Load Balancer
2197	IP Fabric
2198	EVPN
2199	Ansible
2200	Terraform
2205	Incident Response
2206	Detection Engineering
2209	Architektura rozwiązań
2210	eCommerce
2213	Archimate
2216	Backoffice
2221	Modelowanie procesów biznesowych
2222	Modelowanie struktur danych
2224	ADONIS – Business Process Modelling Suite
2225	Bankowość
2229	Zarządzanie danymi
2230	Jakość danych
2233	IBM Infosphere
2234	Informatica
2235	Ab Initio
2236	Colibra
2237	Elektrotechnika
2240	Elektryka
2242	Inżynieria komputerowa
2254	Finanse
2255	Rachunkowość
2260	Oracle PL/SQL
2264	Argo CD
2265	IT, DevOps, Inżynieria Systemowa, Zarządzanie Projektami
2266	ITIL v4, DevOps, Agile, Kanban, CI/CD, Monitoring, Zarządzanie Incydentami
2267	Azure, GCP, Mikroserwisy, Automatyzacja
2268	Azure DevOps, JIRA, Confluence
2275	Diagnostyka błędów
2276	Zarządzanie cyklem życia aplikacji
2280	C
2285	CANoe
2287	DOORS
2289	Metodyki zwinne
2290	Narzędzia analityczne
2291	Bazy danych SQL
2293	Modelowanie systemów informatycznych
2297	XML
2298	JSON
2301	Projektowanie sieci
2302	SDH
2303	DWDM
2304	MPLS
2309	Informacja zarządcza, Analiza danych
2310	MS Office, MS Excel
2321	MES
2323	SAP MM
2325	Service desk
2330	Komunikacja interpersonalna
2332	Wirtualizacja serwerów
2333	Macierze dyskowe
2334	Sieć LAN/WLAN
2339	Active Directory/LDAP
2340	HTTPS
2343	NTP
2345	SNMP
2349	Syslog
2352	Modele statystyczne
2353	Analiza statystyczna
2358	Pandas
2359	Statsmodels
2360	Scikit-learn
2365	DDD
2367	Diagramy techniczne
2374	Microsoft Dynamics 365 F&amp;O
2376	Dokumentacja
2378	Procesy biznesowe
2380	PyTorch
2382	MQTT
2383	ZeroMQ
2388	Specyfikacja wymagań
2391	Metodologie Agile
2397	Anty DDoS
2399	Technologie sieciowe
2401	Korelacja logów
2402	Zarządzanie podatnościami
2403	Mapowanie zagrożeń
2406	Audyt IT
2407	Zarządzanie SZBI
2409	Konfiguracja systemów Linux
2410	Sieci komputerowe (IP, VLAN, firewalle, VPN, DNS)
2411	RODO
2412	Ustawa o Krajowym Systemie Cyberbezpieczeństwa
2413	NIS2
2420	Projektowanie Oprogramowania
2423	PLSQL
2425	MariaDB
2429	Nexus
2430	Debugowanie
2434	WebMethods
2435	IBM MQ
2436	WebLogic
2440	CentOS
2441	Ubuntu
2442	RHEL
2443	GitLab
2444	ArgoCD
2449	ELK Stack
2451	OpenSearch
2453	Testowanie IT
2454	HP QC
2455	UTP
2457	Katalon
2458	ALM
2460	Aplikacje bankowe
2461	Aplikacje maklerskie
2465	Systemy uczelniane
2466	Budowa modeli ML
2468	R
2469	SAS
2470	Praca z dużymi wolumenami danych
2474	Groovy
2476	Maven
2482	Mockito
2484	Elastic
2491	Juniper
2492	HPe/Aruba
2493	Huawei
2495	Palo Alto
2498	Zarządzanie procesami IT
2502	Administracja Sieci
2503	F5 LTM
2504	F5 WAF
2505	iRules
2506	Firewalling
2507	Switching
2510	Telefonia IP
2511	Cisco ASA
2512	Palo Alto NGFW
2513	CheckPoint FW
2514	CUCM
2517	Zarządzanie projektem IT
2523	ERP, Kadry-Płace, Finanse-Księgowość
2524	Analiza biznesowa, BPMN
2525	Bizagi Modeler, Enterprise Architect
2526	Procesy finansowo-księgowe, Kadrowo-płacowe
2527	Dokumentacja migracyjna
2528	Zarządzanie zadaniami, Zarządzanie ryzykiem
2529	Analiza systemów informatycznych
2532	Modelowanie UML/BPMN
2535	Analiza AS IS
2538	.NET Framework
2542	MVVM
2543	Aplikacje 3-warstwowe
2544	Aplikacje desktopowe
2547	Testowanie sterowane rozwojem
2548	Architektura CQRS
2556	WSO2
2559	IaaS
2560	PaaS
2561	SaaS
2564	Wzorce projektowe
2565	Architektura chmurowa
2569	Modelowanie danych
2570	Modelowanie procesów integracyjnych
2571	Mikroserwisy
2572	Konteneryzacja
2573	RESTfull
2574	EDA
2575	SOA
2576	MVC
2579	EA Sparx
2580	AWS
2582	Skalowanie horyzontalne
2583	Skalowanie wertykalne
2584	Bezpieczeństwo rozwiązań
2585	IAM
2586	SSO
2587	MFA
2588	OAUTH
2589	SAML
2590	OIDC
2591	WK/PZ
2593	KRI
2594	eIDAS
2595	NIS
2597	Data Center
2600	Infrastruktura Data Center
2601	Kolokacja
2602	Backup
2603	Chmura (IaaS, PaaS, SaaS)
2605	SOC
2607	Audyty bezpieczeństwa
2608	MS Project
2611	Systemy operacyjne Windows, macOS, Linux
2612	MS Office 365
2613	Sieci komputerowe, Active Directory, VPN, Outlook
2614	Systemy ticketowe
2617	Cisco Firepower
2619	VNet
2620	NSG
2622	Azure Firewall
2623	Application Gateway
2626	Zarządzanie produktem
2627	Analiza rynku
2633	Mechatronika
2634	Inżynieria Produkcji
2635	Traceability
2640	SCADA
2645	MLflow
2646	Systemy rekomendacyjne
2647	Machine learning
2648	Deep learning
2649	Statystyka
2650	Metody klasyfikacyjne
2651	Metody regresyjne
2652	Uczenie nienadzorowane
2654	PySpark
2657	Sci-kit Learn
2658	MLOps
2665	Specyfikowanie przypadków użycia
2671	CISSP
2672	ISO27001
2673	ISO22301
2674	BCM
2675	Analiza procesów
2677	Bezpieczeństwo w chmurze
2678	Analiza bezpieczeństwa
2687	Direct marketing
2688	Personalizacja komunikacji
2689	Testy A/B
2690	CRM
2695	NumPy
2700	SQLAlchemy
2702	SQLite
2705	Pytest
2706	Unittest
2717	Power Automate
2718	Power Apps
2721	Dataverse
2728	Inżynieria danych
2734	Hugging Face
2735	NLP
2736	LLaMA
2737	Mistral
2738	GPT
2739	Prompt Engineering
2740	Data Governance
2741	Data Compliance
2742	Administracja sieciami
2743	Systemy
2744	Środowiska wirtualne
2748	Wizualizacja danych
2749	SAP Analytics Cloud
2750	Looker Studio
2753	SAP Datasphere
2754	Machine Learning
2761	Apache Airflow
2772	LESS/SASS
2773	RWD
2775	Node.js
2780	Salesforce
2782	Tableau
2785	Marketing
2786	Sprzedaż
2790	Golang
2791	MSSQL
2794	.NET8
2795	Visual Studio
2798	SOAP/REST
2809	BI
2810	CMS
2811	PIM
2814	Testowanie Web/Mobile
2815	Testowanie API
2816	Postman
2817	Swagger
2818	qTest
2835	GitHub Actions
2840	AI/ML
2841	Administracja systemu Linux
2844	Eksploatacja systemów
2845	Parametryzacja systemów
2849	SQL / PLSQL
2851	SQL Management Studio
2852	SQL Developer
2853	Core Banking
2854	Treasury
2855	BackOffice
2857	Hurtownia danych
2859	SQL Server Integration Services (SSIS)
2866	Load balancing
2870	Check Point
2882	NIST
2883	CIS
2884	SOC/NOC
2885	Prawo jazdy kat. C+E
2886	Prawo jazdy kat. C
2891	Obsługa wózków widłowych
2892	Obsługa urządzeń magazynowych
2895	Zdolności manualne
2896	Etykietowanie
2898	Skaner
2899	Praca w grupie
2901	Sprawność manualna
2907	Obsługa dokumentacji magazynowej
2908	Wózek widłowy
2909	Praca fizyczna
2912	Obsługa wózka widłowego
2914	Systemy Magazynowe
2916	Sprzedaż internetowa
2917	Obsługa plotera
2918	Obsługa skanera
2919	Praca na magazynie
2920	Praca w firmie produkcyjnej
2923	Obsługa magazynu
2928	Sortowanie
2929	Skanowanie
2931	BHP
2942	Wózek paletowy
2945	Systemy magazynowe
2947	Dokumentacja magazynowa
2948	Praca Magazynowa
2949	Obsługa Wózka Widłowego
2951	Sprzęt wędkarski
2955	Skaner ręczny
2958	Rękawice robocze
2961	Google Sheets
2964	Gospodarka magazynowa
2967	Comarch
2970	Obsługa reachtruck
2993	Kompletacja zamówień
2997	Motoryzacja
3001	Pakowanie
3003	Dostawa
3004	Magazynowanie
3005	Obsługa faktur
3018	Administracja dokumentacji magazynowej
3026	Prawo jazdy kategorii C
3029	Magazyn
3036	Obsługa kasy fiskalnej
3040	Obsługa Magazynu
3051	Wózki widłowe
3060	Praca w magazynie
3067	Logistyka
3070	Obsługa wózków paletowych
3071	Obsługa terminali mobilnych
3072	Aplikacje WMS
3073	Skanowanie kodów
3075	E-commerce
3080	Umiejętności mechaniczne
3081	Umiejętności techniczne
3085	Kondycja fizyczna
3086	Pracowitość
3087	Sumienność
3088	Dokładność
3089	Czytanie dokumentacji WZ
3093	Prowadzenie dokumentacji magazynowej
3100	Obsługa skanera ręcznego
3114	Obsługa elektronarzędzi
3115	Rysunek techniczny
3116	Obróbka metalu
3117	Prace magazynowe
3118	Montaż
3119	Kontrola jakości
3130	Kompletowanie zamówień
3131	Obsługa wózka jezdniowego
3146	Obsługa suwnicy
3162	Układanie towaru
3163	Sprzątanie
3167	Planowanie przestrzeni magazynowej
3170	Obsługa wózka paletowego
3189	Narzędzia techniczne
3198	Wózki paletowe
3199	Terminal mobilny
3219	Praca w laboratorium
3220	Microsoft Word
3222	Microsoft Outlook
3224	Zarządzanie Produkcją
3225	Technologia Żywności
3227	Towaroznawstwo
3228	Chemia Spożywcza
3229	Biotechnologia
3233	CDN
3235	RCP
3236	Lean management
3237	5S
3238	Kaizen
3239	Kanban
3240	SMED
3241	TPM
3242	VSA
3243	Six Sigma
3244	Poka-yoke
3245	Praca w aptece
3246	Prezentacja
3250	Bioinżynieria
3251	Biologia
3252	Chemia
3253	Zdrowie publiczne
3254	Fizjoterapia
3255	Dietetyka
3256	Analityka medyczna
3257	Ratownictwo medyczne
3258	Psychologia kliniczna
3259	Genetyka
3260	Badania naukowe
3266	BIOS/UEFI
3271	Windows PE
3273	CMD
3275	Urządzenia peryferyjne
3284	GPO
3285	WSUS
3288	MS HyperV
3295	Komunikacja z klientem
3298	Systemy DMS/BPM
3300	Helpdesk
3304	SAP ERP
3305	ABAP
3310	Diagnostyka sprzętu komputerowego
3319	Elektromechanika
3322	Komutacja
3323	Teletransmisja
3329	MacOS
3332	Technik informatyk
3333	Konfiguracja systemów operacyjnych
3334	AMMS
3335	Pixel
3336	ATD
3339	Systemy Windows
3342	GNU/Linux-Debian
3344	Red Hat
3345	KVM
3346	VirtualBox
3348	FirebirdSQL
3349	Postgresql
3350	Mysql
3356	Monitoring wizyjny
3357	Alarmy
3358	Systemy pobierania płatności
3364	Biologia molekularna
3365	Obsługa sprzętu laboratoryjnego
3366	Oprogramowanie biurowe
3368	Samodzielność
3375	FTP
3378	Naprawa sprzętu teleinformatycznego
3384	ESXI
3385	Eset Remote Administrator
3386	Fobian
3388	SAP Business
3389	R2 Płatnik
3390	Mona
3391	Bazy danych MSQL
3392	Firebird
3422	Mikrobiologia żywności
3423	Analiza mikrobiologiczna
3424	Realizacja projektów badawczych
3425	Procedury laboratoryjne
3430	Praca laboratoryjna
3431	Techniki laboratoryjne
3432	Izolacja mikropęcherzyków
3433	Cytometria przepływowa
3435	Automatyka przemysłowa
3436	Sterowanie napędami
3437	Czytanie rysunków technicznych
3441	Bazy danych medycznych
3442	Przegląd literatury medycznej
3443	Prezentacja danych
3446	Technologia kosmetyków
3447	Praca w laboratorium chemicznym
3507	OpenVPN
3508	IPsec
3515	iptables
3516	LVM
3520	NewRelic
3553	PHP
3558	HyperV
3560	Fortigate
3566	VMware ESX
3567	vCenter
3572	Obsługa pakietu Office
3609	MS Windows 10/11
3617	ADDS
3631	WMware
3643	Grafika komputerowa
3644	Tworzenie stron internetowych
3648	Fotografia
3649	Film video
3650	Allegro
3651	Amazon
3652	Sztuczna Inteligencja
3653	Social Media
3656	Sprzęt biurowy
3657	Narzędzia biurowe
3658	Internet
3668	SaP Biznes
3671	Bazy Danych MSQL
3674	Systemy komputerowe
3677	Systemy finansowo-księgowe
3679	Spawanie światłowodów
3680	Montaż instalacji teletechnicznych
3693	Struktury danych
3711	WordPress
3714	SEO
3715	Google Analytics
3716	Search Console
3717	Kohana 7
3720	Unity
3721	VR
3722	AR
3728	QNX
3735	SCRUM
3736	CAN
3737	AutoSAR
3739	Ethernet
3740	SOCKS
3744	Yocto
3749	D-Bus
3753	Qemu
3758	Naprawa elektroniki użytkowej
3761	WatchOS
3762	Lutowanie SMD
3763	Lutowanie BGA
3765	Budowa sieci teletechnicznych
3772	Zarządzanie magazynem
3773	Workflow
3781	Elektryk
3782	Sprzedaż B2B
3785	Doradztwo
3790	Prezentacje handlowe
3791	Montaż systemów niskoprądowych
3792	Bezpieczeństwo Informacji
3793	Architektura Systemów Informatycznych
3794	Zabezpieczanie Systemów Informatycznych
3795	ACAD
3799	Grafika
3800	eBay
3804	Budowanie relacji biznesowych
3805	Outsourcing IT
3807	E-marketing
3808	Budowa sieci telekomunikacyjnej
3812	Naprawy elektroniczne
3813	Aktualizacja stron internetowych
3815	Programy graficzne
3816	Linux CentOS
3819	Sieci komputerowe LAN
3820	Sieci komputerowe WAN
3823	Tworzenie dokumentacji
3825	Sieci dostępowe
3826	FTTH
3830	Rekrutacja
3833	Pozyskiwanie klientów
3835	Prace budowlane
3837	Instalacje
3839	Sprzedaż bezpośrednia
3840	Budowanie relacji z klientem
3843	Kohana
3856	CAD 2D
3858	QGIS
3863	Social media
3866	Okablowanie światłowodowe
3867	Konfiguracja routerów
3868	Prace monterskie
3870	Ochrona danych osobowych
3871	Zabezpieczenia informatyczne
3872	Instalacja światłowodów
\.

COPY public.skills_junction (offer_id, skill_id, experience_level_id, experience_months) FROM stdin;
1	1	\N	\N
1	2	\N	\N
1	3	\N	\N
1	4	\N	\N
1	5	\N	\N
2	491	4	60
2	7	\N	\N
2	250	\N	\N
2	9	\N	\N
2	10	\N	\N
3	1	3	48
3	3	\N	\N
3	2	\N	\N
3	4	\N	\N
3	15	\N	\N
3	16	\N	\N
3	19	\N	\N
3	5	\N	\N
3	17	\N	\N
3	18	\N	\N
4	1	\N	\N
4	267	\N	\N
4	23	\N	\N
4	24	\N	\N
4	25	\N	\N
5	1	3	36
5	27	\N	\N
5	28	\N	\N
5	29	\N	\N
5	2	\N	\N
5	3	\N	\N
5	32	\N	\N
5	33	\N	\N
5	34	\N	\N
5	35	\N	\N
6	36	4	36
6	37	\N	\N
6	1061	\N	\N
6	39	\N	\N
6	40	\N	\N
6	41	\N	\N
7	320	3	24
7	1066	1	\N
7	1067	1	\N
8	45	4	54
8	46	\N	\N
8	47	\N	\N
8	48	\N	\N
8	49	\N	\N
8	50	\N	\N
8	51	\N	\N
8	39	\N	\N
8	53	\N	\N
8	54	\N	\N
9	45	1	\N
9	56	1	\N
9	53	1	\N
40	303	3	36
40	304	\N	\N
40	104	\N	\N
40	306	\N	\N
40	68	\N	\N
40	207	\N	\N
40	70	\N	\N
40	310	\N	\N
40	311	\N	\N
40	312	\N	\N
40	136	\N	\N
10	58	\N	\N
10	817	\N	\N
10	60	\N	\N
10	61	\N	\N
10	36	\N	\N
10	63	\N	\N
46	1098	\N	12
51	303	3	60
51	1333	\N	\N
51	222	\N	\N
51	230	\N	\N
51	231	\N	\N
51	1337	\N	\N
46	1099	\N	\N
46	1100	\N	\N
46	15	\N	\N
41	319	\N	\N
41	320	\N	\N
11	53	\N	\N
11	65	\N	\N
11	54	\N	\N
11	39	\N	\N
11	68	\N	\N
11	69	\N	\N
11	70	\N	\N
11	331	\N	\N
11	72	\N	\N
11	73	\N	\N
11	71	\N	\N
11	74	\N	\N
11	75	\N	\N
13	82	\N	\N
14	1118	\N	\N
14	84	\N	\N
14	61	\N	\N
14	86	\N	\N
14	87	\N	\N
14	88	\N	\N
14	168	\N	\N
15	89	\N	\N
15	90	\N	\N
15	91	\N	\N
15	80	\N	\N
15	595	\N	\N
15	93	\N	\N
15	94	\N	\N
15	1132	\N	\N
15	96	\N	\N
15	353	\N	\N
15	354	\N	\N
15	99	\N	\N
15	603	\N	\N
15	419	\N	\N
15	100	\N	\N
16	101	\N	\N
16	1141	\N	\N
16	1142	\N	\N
16	54	\N	\N
16	103	\N	\N
16	104	\N	\N
16	1146	\N	\N
16	1147	\N	\N
42	358	\N	\N
42	359	\N	\N
42	230	\N	\N
42	361	\N	\N
42	362	2	24
17	105	\N	\N
17	106	\N	\N
17	65	\N	\N
17	108	\N	\N
18	84	\N	\N
18	368	\N	\N
18	369	\N	\N
18	230	\N	\N
18	207	\N	\N
19	53	\N	\N
19	110	\N	\N
19	111	\N	\N
19	94	\N	\N
45	886	\N	\N
45	98	\N	\N
45	78	\N	\N
45	1169	\N	\N
45	153	\N	\N
45	891	\N	\N
45	151	\N	\N
21	53	\N	\N
21	54	\N	\N
21	104	\N	\N
21	61	\N	\N
21	120	\N	\N
21	121	\N	\N
21	106	\N	\N
21	1180	\N	\N
21	39	\N	\N
51	1338	\N	\N
51	1339	\N	\N
21	70	\N	\N
21	69	\N	\N
21	68	\N	\N
21	129	\N	\N
21	130	\N	\N
21	131	\N	\N
21	132	\N	\N
23	25	\N	\N
23	134	\N	\N
23	135	\N	\N
23	136	\N	\N
23	137	\N	\N
23	138	\N	\N
23	139	\N	\N
47	1196	2	24
47	23	\N	\N
47	1198	\N	\N
47	45	\N	\N
24	320	3	18
24	54	1	\N
24	143	\N	\N
24	1203	1	\N
25	1204	\N	\N
25	1205	\N	\N
25	1206	\N	\N
25	148	\N	\N
25	149	\N	\N
25	150	\N	\N
25	151	\N	\N
25	152	\N	\N
25	153	\N	\N
48	368	\N	12
48	61	\N	\N
48	1215	\N	\N
48	1216	\N	\N
48	1217	\N	\N
48	1218	\N	\N
48	1219	\N	\N
48	93	\N	\N
48	1221	\N	\N
48	1222	\N	\N
51	1340	\N	\N
52	53	\N	\N
37	210	\N	\N
37	211	\N	\N
37	212	\N	\N
37	213	\N	\N
37	214	\N	\N
37	23	\N	\N
52	1342	\N	\N
52	1343	\N	\N
52	168	\N	\N
52	152	\N	\N
53	1346	3	24
53	87	\N	\N
48	1223	\N	\N
48	1224	\N	\N
26	1225	\N	\N
26	1226	\N	\N
26	851	\N	\N
26	148	\N	\N
27	1229	3	36
27	61	3	36
27	104	3	\N
27	160	3	\N
27	161	3	\N
27	65	3	\N
27	71	3	\N
27	108	1	\N
43	108	\N	\N
43	23	\N	\N
43	151	\N	\N
28	165	\N	\N
28	151	\N	\N
29	1242	\N	24
53	1348	\N	\N
53	1349	\N	\N
53	1350	\N	\N
53	65	\N	\N
53	54	\N	\N
53	1353	\N	\N
53	105	\N	\N
53	1355	\N	\N
53	1356	\N	\N
54	1346	\N	\N
54	1358	\N	\N
54	1359	\N	\N
54	1360	\N	\N
54	1361	\N	\N
54	54	\N	\N
54	65	\N	\N
54	1364	\N	\N
54	43	\N	\N
54	1366	\N	\N
54	1367	\N	\N
54	1368	\N	\N
54	1369	\N	\N
55	89	3	36
55	53	\N	\N
55	61	\N	\N
55	1215	\N	\N
55	108	1	\N
55	45	1	\N
55	1376	\N	\N
55	1377	\N	\N
55	75	\N	\N
56	1379	4	72
56	1380	\N	\N
56	152	\N	\N
57	54	\N	\N
57	790	\N	\N
57	1	\N	\N
57	1385	\N	\N
57	5	\N	\N
57	1387	\N	\N
57	1388	\N	\N
58	89	\N	24
58	165	\N	\N
58	1391	\N	\N
58	136	\N	\N
58	93	\N	\N
58	1394	\N	\N
58	1395	\N	\N
58	1396	\N	\N
58	343	\N	\N
58	1398	\N	\N
58	1356	\N	\N
58	545	\N	\N
59	1401	\N	\N
59	1402	\N	\N
59	70	\N	\N
59	69	\N	\N
59	61	\N	\N
59	148	\N	\N
59	1407	\N	\N
59	151	\N	\N
60	199	\N	\N
60	65	\N	\N
60	69	\N	\N
60	70	\N	\N
60	1413	\N	\N
60	54	\N	\N
60	87	\N	\N
60	43	1	\N
61	1417	\N	\N
61	1229	\N	\N
61	1419	\N	\N
61	71	\N	\N
61	790	\N	\N
61	43	\N	\N
61	1423	\N	\N
61	1424	\N	\N
62	53	\N	\N
62	65	\N	\N
62	1343	\N	\N
62	95	\N	\N
62	169	\N	\N
63	319	\N	\N
63	1431	\N	\N
63	1432	\N	\N
63	1433	\N	\N
63	1434	\N	\N
63	1435	\N	\N
63	1436	\N	\N
12	42	4	36
12	58	\N	\N
12	78	\N	\N
12	841	\N	\N
12	80	\N	\N
12	81	\N	\N
63	1437	\N	\N
63	34	\N	\N
63	1	\N	\N
64	1440	3	36
64	199	\N	\N
64	1442	\N	\N
64	1443	\N	\N
64	310	\N	\N
64	68	\N	\N
64	1446	\N	\N
65	319	3	24
65	790	\N	\N
65	1	\N	\N
65	1450	\N	\N
65	1385	\N	\N
65	1452	\N	\N
65	1453	\N	\N
65	1454	\N	\N
65	96	\N	\N
65	1456	\N	\N
65	1457	\N	\N
66	228	3	24
66	238	\N	\N
66	1460	\N	\N
66	1461	\N	\N
66	241	\N	\N
67	1463	\N	\N
68	1464	\N	\N
68	104	\N	\N
68	108	1	\N
68	1467	\N	\N
69	54	\N	\N
69	43	1	\N
69	44	1	\N
70	114	\N	\N
70	115	\N	\N
70	1473	\N	\N
70	53	\N	\N
70	54	\N	\N
70	136	\N	\N
70	219	\N	\N
70	226	\N	\N
71	1479	\N	\N
71	1480	\N	\N
71	104	\N	\N
71	1358	\N	\N
71	70	\N	\N
71	68	\N	\N
72	1303	4	24
72	1486	1	\N
72	1424	1	\N
72	78	\N	\N
72	1306	\N	\N
73	134	\N	\N
73	1492	\N	\N
73	169	\N	\N
73	1494	\N	\N
73	1490	\N	\N
73	1496	\N	\N
73	136	\N	\N
73	1498	\N	\N
74	1499	\N	\N
74	197	\N	\N
74	694	\N	\N
75	1502	\N	24
75	1503	\N	\N
75	1504	\N	\N
75	1505	\N	\N
75	129	\N	\N
75	1507	\N	24
75	1508	\N	\N
75	1509	\N	\N
75	1510	\N	\N
75	132	\N	\N
75	197	\N	\N
76	80	\N	\N
76	1514	\N	\N
76	1515	\N	\N
76	1516	\N	\N
76	1517	\N	\N
76	108	\N	\N
77	1502	\N	\N
77	230	\N	\N
77	231	\N	\N
77	207	\N	\N
77	1523	\N	\N
77	1524	\N	\N
77	963	\N	\N
77	1355	\N	\N
78	89	2	12
78	136	\N	\N
78	58	\N	\N
78	78	\N	\N
78	61	\N	\N
78	1395	\N	\N
79	88	\N	\N
79	1221	\N	\N
79	135	\N	\N
79	1169	\N	\N
79	134	\N	\N
80	1538	\N	\N
80	212	\N	\N
80	1540	\N	\N
80	1541	\N	\N
80	1542	\N	\N
80	1543	\N	\N
80	207	\N	\N
80	778	\N	\N
80	1546	\N	\N
81	53	\N	\N
81	95	\N	\N
81	1549	\N	\N
81	108	\N	\N
81	180	\N	\N
81	90	\N	\N
81	1553	\N	\N
81	1554	\N	\N
81	1456	\N	\N
100	207	\N	\N
100	69	\N	\N
100	70	\N	\N
100	104	\N	\N
100	1699	\N	\N
100	1700	\N	\N
100	1701	\N	\N
100	161	\N	\N
100	1703	\N	\N
100	1704	\N	\N
100	1705	\N	\N
100	1706	\N	\N
100	1707	\N	\N
101	1206	3	24
101	1709	3	24
101	1710	\N	\N
101	1711	\N	\N
101	1712	\N	\N
101	87	\N	\N
101	1348	\N	\N
101	1588	\N	\N
101	78	\N	\N
101	211	\N	\N
101	54	\N	\N
102	89	\N	12
102	39	\N	\N
102	78	\N	\N
102	53	\N	\N
103	1723	1	\N
103	1724	1	\N
29	418	\N	\N
29	419	\N	\N
29	169	\N	\N
29	134	\N	\N
29	151	\N	\N
30	53	\N	\N
30	54	\N	\N
30	172	\N	\N
30	104	\N	\N
30	87	\N	\N
30	175	\N	\N
30	176	\N	\N
30	177	\N	\N
31	1256	\N	\N
31	104	\N	\N
31	105	\N	\N
31	1259	\N	\N
31	1260	\N	\N
31	180	\N	\N
31	692	\N	\N
31	693	\N	\N
31	545	\N	\N
32	184	\N	\N
32	54	\N	\N
32	65	\N	\N
32	136	\N	\N
32	134	\N	\N
32	169	\N	\N
33	445	4	24
33	65	4	24
33	191	\N	\N
33	192	\N	\N
33	193	\N	\N
33	194	\N	\N
49	114	\N	\N
49	1278	\N	36
49	1279	\N	\N
49	1280	\N	\N
49	1281	\N	\N
49	1282	\N	\N
34	90	1	\N
34	108	1	\N
34	197	\N	\N
103	211	1	\N
103	199	1	\N
103	61	1	\N
103	1728	\N	\N
103	169	\N	\N
104	1730	\N	\N
104	1731	\N	\N
104	1732	\N	\N
104	1733	\N	\N
105	23	\N	\N
106	88	\N	\N
106	80	\N	\N
106	90	\N	\N
106	1738	\N	\N
107	45	3	24
107	1740	\N	\N
107	238	\N	\N
107	1742	\N	\N
107	63	\N	\N
107	1553	\N	\N
108	1745	1	\N
108	1746	1	\N
108	1747	\N	\N
108	1748	\N	\N
108	136	\N	\N
109	89	3	24
109	84	\N	\N
235	2885	\N	\N
82	1556	\N	\N
82	1557	\N	\N
82	1558	\N	\N
82	104	\N	\N
35	198	\N	\N
35	199	\N	\N
35	78	\N	\N
35	87	\N	\N
35	175	\N	\N
35	104	\N	\N
35	204	\N	\N
35	205	\N	\N
35	71	\N	\N
35	207	\N	\N
44	454	3	24
44	455	\N	\N
44	23	\N	\N
44	64	\N	\N
44	151	\N	\N
36	136	\N	\N
36	209	\N	\N
50	1303	\N	12
50	78	\N	\N
50	53	\N	\N
50	1306	\N	\N
50	151	\N	\N
50	1308	\N	\N
38	45	3	30
38	132	3	30
38	74	\N	\N
38	219	\N	\N
38	90	\N	\N
38	184	\N	\N
38	222	\N	\N
38	223	\N	\N
38	224	\N	\N
38	225	\N	\N
38	226	\N	\N
38	227	\N	\N
39	228	3	36
39	229	\N	\N
39	230	\N	\N
39	231	\N	\N
39	232	\N	\N
39	233	\N	\N
39	234	\N	\N
39	235	\N	\N
39	222	\N	\N
39	237	\N	\N
39	151	\N	\N
82	1196	\N	\N
82	45	\N	\N
83	84	\N	\N
83	148	\N	\N
83	87	\N	\N
83	1348	\N	\N
83	1566	\N	\N
84	87	\N	\N
84	175	\N	\N
84	1569	\N	\N
84	1570	\N	\N
85	303	\N	\N
85	84	\N	\N
85	180	\N	\N
85	136	\N	\N
85	124	\N	\N
85	132	\N	\N
86	65	\N	\N
86	1578	\N	\N
86	54	\N	\N
86	1580	\N	\N
86	1581	\N	\N
86	1582	\N	\N
86	1583	\N	\N
86	1584	\N	\N
86	1585	\N	\N
86	142	\N	\N
86	39	\N	\N
86	1588	\N	\N
87	114	\N	\N
87	115	\N	\N
87	53	\N	\N
87	148	\N	\N
87	197	\N	\N
87	153	\N	\N
88	1346	\N	24
88	45	\N	24
88	1597	\N	\N
88	160	\N	\N
88	1215	\N	\N
88	1218	\N	\N
88	1601	\N	\N
88	1602	\N	\N
88	1603	\N	\N
88	1604	\N	\N
88	104	\N	\N
88	1606	\N	\N
89	104	\N	\N
89	1608	\N	\N
89	1609	\N	\N
89	1610	\N	\N
89	1611	\N	\N
89	1612	\N	\N
89	1613	\N	\N
89	73	\N	\N
89	1353	\N	\N
89	1456	\N	\N
89	1617	\N	\N
89	1618	\N	\N
89	53	\N	\N
89	1620	\N	\N
89	1621	\N	\N
89	1622	\N	\N
89	1524	\N	\N
89	1624	\N	\N
89	1625	\N	\N
90	65	\N	\N
90	104	\N	\N
90	1628	\N	\N
90	132	\N	\N
91	53	\N	\N
91	54	\N	\N
91	23	\N	\N
91	1633	\N	\N
91	1196	\N	\N
91	1635	\N	\N
91	1636	\N	\N
91	1549	\N	\N
91	1638	\N	\N
92	1639	\N	\N
92	1640	\N	\N
92	777	\N	\N
92	108	\N	\N
92	39	\N	\N
92	69	\N	\N
92	1645	\N	\N
92	45	\N	\N
92	1647	\N	\N
92	1648	\N	\N
92	1649	\N	\N
92	151	\N	\N
92	152	\N	\N
93	1652	\N	\N
93	1653	\N	\N
93	108	\N	\N
93	39	\N	\N
93	69	\N	\N
93	1645	\N	\N
93	45	\N	\N
93	1647	\N	\N
93	1648	\N	\N
93	1649	\N	\N
93	151	\N	\N
93	152	\N	\N
94	1664	\N	\N
95	1665	\N	\N
95	1666	\N	\N
95	78	\N	\N
95	60	\N	\N
97	108	\N	\N
97	70	\N	\N
97	69	\N	\N
97	39	\N	\N
97	1673	\N	\N
97	1674	\N	\N
97	84	\N	\N
97	1676	\N	\N
98	1346	\N	\N
98	54	\N	\N
98	1	\N	\N
98	142	\N	\N
99	1681	\N	\N
99	169	\N	\N
99	153	\N	\N
99	136	\N	\N
99	1685	\N	\N
99	1686	\N	\N
100	198	\N	\N
100	53	\N	\N
100	39	\N	\N
100	71	\N	\N
100	65	\N	\N
100	87	\N	\N
100	68	\N	\N
100	175	\N	\N
109	108	1	\N
109	168	\N	\N
110	53	\N	12
110	148	\N	12
110	108	\N	12
110	1355	\N	\N
110	151	\N	\N
111	1759	3	36
111	1760	\N	\N
111	1761	\N	\N
111	1762	\N	\N
111	1763	\N	\N
111	1764	\N	\N
112	98	1	6
112	169	\N	\N
112	53	\N	\N
112	1768	\N	\N
112	1769	\N	\N
112	136	\N	\N
113	1771	4	48
113	23	\N	\N
113	1773	\N	\N
113	1774	\N	\N
113	1775	\N	\N
113	1776	\N	\N
113	120	\N	\N
114	58	\N	12
114	53	\N	\N
114	65	\N	\N
114	78	\N	\N
114	1350	\N	\N
115	1783	\N	\N
115	1784	\N	\N
115	39	\N	\N
115	70	\N	\N
115	69	\N	\N
115	310	\N	\N
115	207	\N	\N
115	1790	\N	\N
115	53	\N	\N
115	1343	\N	\N
115	1793	\N	\N
115	45	\N	\N
116	80	\N	\N
116	1796	\N	\N
116	104	\N	\N
116	71	\N	\N
116	75	\N	\N
116	207	\N	\N
116	108	\N	\N
116	84	\N	\N
117	54	\N	\N
117	1	\N	\N
117	790	\N	\N
117	1364	\N	\N
117	5	\N	\N
117	1456	\N	\N
118	36	3	60
118	1796	\N	\N
118	1811	\N	\N
118	223	\N	\N
118	1813	\N	\N
118	1376	\N	\N
118	1496	\N	\N
118	226	\N	\N
119	88	3	24
119	134	\N	\N
119	54	\N	\N
119	114	\N	\N
119	53	\N	\N
119	136	\N	\N
120	545	\N	\N
120	53	\N	\N
120	54	\N	\N
120	1826	\N	\N
120	108	\N	\N
120	98	\N	\N
122	53	\N	\N
122	78	\N	\N
122	151	\N	\N
122	419	\N	\N
122	153	\N	\N
122	1834	\N	\N
122	1835	\N	\N
123	104	\N	\N
123	53	\N	\N
123	87	\N	\N
123	1348	\N	\N
123	175	\N	\N
123	207	\N	\N
123	1842	\N	\N
123	1703	\N	\N
123	1705	\N	\N
123	1845	\N	\N
123	45	\N	\N
124	1847	\N	\N
124	69	\N	\N
124	1849	\N	\N
124	1850	\N	\N
124	70	\N	\N
124	87	\N	\N
124	175	\N	\N
124	1348	\N	\N
124	1855	\N	\N
124	1856	\N	\N
124	1857	\N	\N
124	1858	\N	\N
124	1859	\N	\N
124	1860	\N	\N
124	1861	\N	\N
124	1862	\N	\N
124	1863	\N	\N
124	36	\N	\N
125	1865	\N	36
125	1456	\N	\N
125	1867	\N	\N
125	1308	\N	\N
125	134	\N	\N
125	1870	\N	\N
126	25	4	48
126	138	5	\N
126	23	3	\N
126	1874	\N	\N
126	1875	\N	\N
126	1876	\N	\N
127	1394	5	120
127	110	5	120
127	1879	\N	\N
127	1880	\N	\N
128	1	\N	\N
128	1882	\N	\N
128	1883	\N	\N
128	1884	\N	\N
128	252	\N	\N
128	1886	\N	\N
129	1887	\N	60
129	1888	\N	\N
129	1889	\N	\N
129	1890	\N	\N
129	1891	\N	\N
129	1892	\N	\N
129	1893	\N	\N
129	955	\N	\N
129	136	\N	\N
129	1896	\N	\N
129	1897	\N	\N
129	1898	\N	\N
129	1899	\N	\N
129	1900	\N	\N
129	1901	\N	\N
129	1902	\N	\N
129	1903	\N	\N
129	153	\N	\N
130	23	\N	\N
130	1771	\N	\N
130	15	\N	\N
130	54	\N	\N
130	53	\N	\N
130	1364	\N	\N
130	1911	\N	\N
131	1	\N	\N
131	23	\N	\N
132	1914	4	36
132	1915	\N	\N
132	1916	\N	\N
132	1917	\N	\N
132	136	\N	\N
132	151	\N	\N
132	422	\N	\N
132	1169	\N	\N
133	1922	3	24
133	1923	\N	\N
133	1924	\N	\N
133	1925	\N	\N
133	1926	\N	\N
133	1927	\N	\N
133	1928	\N	\N
133	1929	\N	\N
133	1930	\N	\N
133	1931	\N	\N
134	1932	3	24
134	1490	3	36
134	1774	\N	\N
134	1926	\N	\N
134	1927	\N	\N
134	1928	\N	\N
134	1938	\N	\N
135	1775	3	36
135	1940	3	36
135	1941	\N	\N
135	1942	\N	\N
135	1943	\N	\N
135	1925	\N	\N
135	1945	\N	\N
135	23	\N	\N
135	1947	\N	\N
135	1948	\N	\N
135	1949	\N	\N
135	1950	\N	\N
135	1951	\N	\N
135	1952	\N	\N
135	1953	\N	\N
135	1954	\N	\N
135	1955	\N	\N
135	1956	\N	\N
135	1957	\N	\N
135	1958	\N	\N
136	88	\N	\N
136	1960	\N	\N
136	151	\N	\N
137	1962	\N	\N
137	82	\N	36
137	139	\N	36
138	1882	3	60
138	1966	3	60
138	1967	3	60
138	1968	3	60
138	1969	3	60
138	1970	\N	\N
138	1971	\N	\N
138	1972	\N	\N
138	1775	3	60
138	1974	3	60
138	1975	3	60
138	1976	3	60
138	1977	\N	\N
138	1978	\N	\N
138	1948	\N	\N
138	1980	\N	\N
138	1958	\N	\N
138	15	\N	\N
138	1983	\N	\N
139	1984	4	60
139	1985	\N	\N
139	1986	\N	\N
139	1987	\N	\N
139	1988	\N	\N
139	1989	\N	\N
139	1774	\N	\N
139	1991	\N	\N
139	134	\N	\N
139	1993	\N	\N
139	169	\N	\N
139	1308	\N	\N
140	1775	\N	\N
140	1997	\N	\N
140	1376	\N	\N
140	1999	\N	\N
140	2000	\N	\N
140	2001	\N	\N
140	151	\N	\N
141	2003	\N	\N
141	1647	\N	\N
141	1648	\N	\N
141	2006	\N	\N
141	184	\N	\N
141	2008	\N	\N
141	1601	\N	\N
141	2010	\N	\N
141	777	\N	\N
141	2012	\N	\N
141	2013	\N	\N
141	1606	\N	\N
141	25	\N	\N
142	2016	3	30
142	2017	\N	\N
142	2018	\N	\N
142	1771	\N	\N
142	445	\N	\N
142	2021	\N	\N
142	2022	\N	\N
142	2023	\N	\N
142	2024	\N	\N
142	1169	\N	\N
142	136	\N	\N
142	2027	\N	\N
143	54	\N	\N
143	790	\N	\N
143	2030	\N	\N
143	23	\N	\N
143	1771	\N	\N
143	15	\N	\N
143	1606	\N	\N
143	1364	\N	\N
143	1911	\N	\N
143	1	\N	\N
144	2038	\N	\N
144	1967	160	\N
144	1	160	\N
145	2041	3	36
145	1	3	36
145	2043	\N	\N
145	2044	\N	\N
145	54	\N	\N
145	5	\N	\N
146	2047	\N	12
146	114	\N	12
146	155	\N	12
146	2050	\N	\N
146	1923	\N	\N
146	2052	\N	\N
146	1496	\N	\N
147	114	\N	\N
147	2055	\N	\N
147	2056	\N	\N
147	2057	\N	\N
147	2058	3	60
147	1490	3	60
147	2050	3	60
147	23	3	\N
147	24	\N	\N
147	2063	\N	\N
147	1880	\N	\N
147	2065	\N	\N
147	1740	\N	\N
147	2067	\N	\N
147	2068	\N	\N
148	1876	3	30
148	2070	3	30
148	2071	1	12
148	2072	\N	\N
148	23	171	\N
148	1771	\N	\N
148	445	\N	\N
148	2076	\N	\N
148	2022	\N	\N
148	2078	\N	\N
148	2079	\N	\N
148	1985	\N	\N
149	2081	3	36
149	2072	\N	\N
149	1771	\N	\N
149	445	\N	\N
149	2022	\N	\N
149	2076	\N	\N
149	442	\N	\N
150	169	\N	\N
150	2058	3	48
150	1490	\N	\N
150	1419	\N	\N
150	2092	\N	\N
150	1774	\N	\N
150	1989	\N	\N
150	1928	\N	\N
150	2096	\N	\N
151	2097	3	48
151	2098	1	12
151	2099	\N	\N
151	2072	\N	\N
151	23	171	\N
151	1771	\N	\N
151	445	\N	\N
151	2076	\N	\N
151	2022	\N	\N
151	2106	\N	\N
151	2107	\N	\N
151	851	\N	\N
151	134	\N	\N
151	1985	\N	\N
152	2111	4	60
152	2022	\N	\N
152	2076	\N	\N
152	24	\N	\N
152	2115	\N	\N
152	2116	\N	\N
152	2117	\N	\N
152	2118	\N	\N
152	2119	\N	\N
152	1922	\N	\N
153	114	\N	\N
153	115	\N	\N
153	2123	\N	\N
153	78	\N	\N
153	1867	\N	\N
153	65	\N	\N
153	71	\N	\N
153	54	\N	\N
153	39	\N	\N
154	2130	\N	\N
154	2131	\N	\N
155	114	\N	\N
155	1796	\N	\N
155	2133	\N	\N
155	53	\N	\N
155	54	\N	\N
156	54	\N	\N
156	2139	\N	\N
156	1911	\N	\N
156	1364	\N	\N
156	2142	\N	\N
156	2143	\N	\N
156	2144	\N	\N
156	2145	\N	\N
156	2146	\N	\N
156	1771	\N	\N
156	1419	\N	\N
156	45	\N	\N
157	1	\N	\N
157	23	\N	\N
157	2152	\N	\N
157	139	\N	\N
157	2154	\N	\N
157	25	\N	\N
157	134	\N	\N
157	2157	\N	\N
158	1633	3	\N
159	2159	\N	12
159	1922	\N	\N
159	2161	\N	\N
159	114	\N	12
160	2163	4	60
160	2164	3	24
160	252	\N	\N
160	2166	\N	\N
160	2167	\N	\N
160	2168	\N	\N
160	1986	\N	\N
160	2170	\N	\N
160	2171	\N	\N
160	2172	\N	\N
161	1496	\N	\N
161	2174	\N	\N
161	1985	\N	\N
161	2176	\N	\N
161	2159	\N	\N
161	303	\N	\N
161	2179	\N	\N
161	2180	\N	\N
162	87	4	60
162	1348	4	60
162	175	4	60
162	1857	3	\N
162	1856	3	\N
162	2186	3	\N
162	2187	3	\N
162	230	3	\N
162	2189	3	\N
162	207	3	\N
162	231	3	\N
162	54	3	\N
162	65	3	\N
162	1	3	\N
162	790	3	\N
162	71	3	\N
162	2197	3	\N
162	2198	3	\N
162	2199	3	\N
162	2200	3	\N
163	45	5	120
163	1640	\N	\N
163	777	\N	\N
163	47	\N	\N
163	2205	\N	\N
163	2206	\N	\N
164	114	\N	\N
164	1796	\N	\N
164	2209	3	60
164	2210	\N	\N
164	1196	\N	\N
164	1774	\N	\N
164	2213	\N	\N
164	1434	\N	\N
164	2065	\N	\N
164	2216	\N	\N
165	1490	3	60
165	2058	3	60
165	1989	\N	\N
165	1774	\N	\N
165	2221	\N	\N
165	2222	\N	\N
165	2065	\N	\N
165	2224	\N	\N
166	2225	\N	\N
166	1796	\N	\N
166	114	\N	\N
166	2056	\N	\N
166	2229	3	36
166	2230	2	12
166	23	\N	\N
166	25	\N	\N
166	2233	\N	\N
166	2234	\N	\N
166	2235	\N	\N
166	2236	\N	\N
167	2237	\N	\N
167	1796	\N	\N
167	2133	\N	\N
167	2240	\N	\N
167	114	\N	\N
167	2242	\N	\N
167	88	3	60
167	369	\N	\N
167	1811	\N	\N
168	23	3	\N
168	54	1	\N
168	108	1	\N
168	442	\N	12
168	1876	\N	12
168	155	\N	12
168	1931	\N	12
168	1867	\N	12
168	2254	1	\N
168	2255	1	\N
169	1775	4	48
169	1975	4	48
169	1976	4	48
169	1882	\N	\N
169	2260	\N	\N
169	1947	\N	\N
169	1948	\N	\N
169	254	\N	\N
169	2264	\N	\N
170	2265	\N	96
170	2266	\N	\N
170	2267	\N	\N
170	2268	\N	\N
171	1346	\N	\N
171	1340	\N	\N
171	23	\N	\N
171	1930	\N	\N
171	1198	\N	\N
171	1606	\N	\N
171	2275	\N	\N
171	2276	\N	\N
171	45	\N	\N
171	1621	\N	\N
171	1523	\N	\N
172	2280	\N	\N
172	2041	\N	\N
172	1774	\N	\N
172	113	\N	\N
172	1927	\N	\N
172	2285	\N	\N
172	1928	\N	\N
172	2287	\N	\N
173	2058	3	36
173	2289	\N	\N
173	2290	\N	\N
173	2291	\N	\N
173	19	\N	\N
173	2293	\N	\N
173	1774	\N	\N
173	1989	\N	\N
173	113	\N	\N
173	2297	\N	\N
173	2298	\N	\N
174	1796	\N	\N
174	114	\N	\N
174	2301	3	60
174	2302	\N	\N
174	2303	\N	\N
174	2304	\N	\N
174	1855	\N	\N
174	1857	\N	\N
235	2886	\N	\N
174	1856	\N	\N
174	219	\N	\N
175	2309	\N	24
175	2310	\N	\N
175	23	\N	\N
175	63	\N	\N
175	442	\N	\N
176	1496	3	24
176	1340	\N	\N
176	99	\N	\N
176	1633	5	\N
176	168	\N	\N
176	545	\N	\N
177	1635	3	60
177	2321	3	60
177	1196	3	60
177	2323	\N	\N
177	136	\N	\N
177	2325	\N	\N
177	442	\N	\N
178	136	\N	\N
178	442	\N	\N
178	151	\N	\N
178	2330	\N	\N
178	303	\N	\N
179	2332	3	60
179	2333	3	60
179	2334	3	60
179	36	3	60
179	54	\N	\N
179	53	\N	\N
179	1350	\N	\N
179	2339	\N	\N
179	2340	\N	\N
179	70	\N	\N
179	69	\N	\N
179	2343	\N	\N
179	1387	\N	\N
179	2345	\N	\N
179	1849	\N	\N
179	207	\N	\N
179	230	\N	\N
179	2349	\N	\N
179	39	\N	\N
179	78	\N	\N
180	2352	5	60
180	2353	\N	\N
180	23	\N	\N
180	1	\N	\N
180	138	\N	\N
180	1456	\N	\N
180	2358	\N	\N
180	2359	\N	\N
180	2360	\N	\N
180	78	\N	\N
181	1424	4	60
181	212	\N	\N
181	19	\N	\N
181	2365	\N	\N
181	1943	\N	\N
181	2367	\N	\N
182	98	\N	12
182	88	\N	12
182	134	\N	\N
182	1340	\N	\N
182	64	\N	\N
182	169	\N	\N
183	2374	4	48
183	442	\N	\N
183	2376	\N	\N
183	1876	\N	\N
183	2378	\N	\N
184	1	\N	\N
184	2380	\N	\N
184	29	\N	\N
184	2382	\N	\N
184	2383	\N	\N
184	1958	\N	\N
184	1364	\N	\N
184	1911	\N	\N
185	1490	\N	24
185	2388	\N	\N
185	19	\N	\N
185	1947	\N	\N
185	2391	\N	\N
185	134	\N	\N
185	152	\N	\N
186	184	\N	\N
186	230	\N	\N
186	234	\N	\N
186	2397	\N	\N
186	222	\N	\N
186	2399	\N	\N
186	25	\N	\N
186	2401	\N	\N
186	2402	\N	\N
186	2403	\N	\N
187	114	\N	\N
187	2003	3	36
187	2406	3	36
187	2407	3	36
187	1206	\N	\N
187	2409	\N	\N
187	2410	\N	\N
187	2411	\N	\N
187	2412	\N	\N
187	2413	\N	\N
188	2092	1	\N
188	1424	1	\N
189	114	\N	\N
189	2133	\N	\N
189	2056	\N	\N
189	2057	\N	\N
189	2420	5	96
189	1867	5	96
189	23	5	\N
189	2423	5	\N
189	1771	\N	\N
189	2425	\N	\N
189	15	\N	\N
189	5	\N	\N
189	2166	\N	\N
189	2429	\N	\N
189	2430	\N	\N
189	442	\N	\N
190	790	\N	\N
190	1	\N	\N
190	2434	\N	\N
190	2435	\N	\N
190	2436	\N	\N
190	1364	\N	\N
190	1911	\N	\N
190	54	\N	\N
190	2440	\N	\N
190	2441	\N	\N
190	2442	\N	\N
190	2443	\N	\N
190	2444	\N	\N
190	2166	\N	\N
190	2199	\N	\N
190	253	\N	\N
190	254	\N	\N
190	2449	\N	\N
190	2008	\N	\N
190	2451	\N	\N
190	2200	\N	\N
191	2453	\N	\N
191	2454	\N	\N
191	2455	\N	\N
191	1926	\N	\N
191	2457	\N	\N
191	2458	\N	\N
191	1876	\N	\N
191	2460	\N	\N
191	2461	\N	\N
191	151	\N	\N
191	153	\N	\N
191	1927	\N	\N
192	2465	3	60
193	2466	\N	\N
193	1	\N	\N
193	2468	\N	\N
193	2469	\N	\N
193	2470	\N	\N
194	114	\N	\N
194	1775	3	48
194	1974	3	36
194	2474	3	36
194	27	\N	\N
194	2476	\N	\N
194	2166	\N	\N
194	2443	\N	\N
194	113	\N	\N
194	1927	\N	\N
194	1977	\N	\N
194	2482	\N	\N
194	19	\N	\N
194	2484	\N	\N
194	254	\N	\N
194	1986	\N	\N
195	108	\N	\N
195	114	\N	\N
195	1796	\N	\N
195	129	\N	36
195	2491	\N	36
195	2492	\N	36
195	2493	\N	36
195	1847	\N	36
195	2495	\N	36
195	87	\N	\N
195	175	\N	\N
196	2498	3	36
196	78	\N	\N
196	419	\N	\N
196	169	\N	\N
197	2502	4	36
197	2503	\N	\N
197	2504	\N	\N
197	2505	\N	\N
197	2506	\N	\N
197	2507	\N	\N
197	1733	\N	\N
197	1855	\N	\N
197	2510	\N	\N
197	2511	\N	\N
197	2512	\N	\N
197	2513	\N	\N
197	2514	\N	\N
197	1856	\N	\N
197	1857	\N	\N
198	2517	3	36
198	1926	\N	\N
198	1927	\N	\N
198	78	\N	\N
198	1490	\N	\N
198	99	\N	\N
199	2523	\N	\N
199	2524	3	48
199	2525	\N	\N
199	2526	3	12
199	2527	\N	\N
199	2528	\N	\N
200	2529	3	48
200	1419	\N	\N
200	2092	\N	\N
200	2532	\N	\N
200	1928	\N	\N
200	2096	\N	\N
200	2535	\N	\N
200	1924	\N	\N
201	1423	3	42
201	2538	\N	\N
201	1771	\N	\N
201	15	\N	\N
201	27	\N	\N
201	2542	\N	\N
201	2543	\N	\N
201	2544	\N	\N
201	1198	\N	\N
201	252	\N	\N
201	2547	\N	\N
201	2548	\N	\N
201	2365	\N	\N
201	1442	\N	\N
202	1774	\N	\N
202	1989	\N	\N
202	1928	\N	\N
202	1948	\N	\N
202	1958	\N	\N
202	2556	\N	\N
202	16	\N	\N
203	1923	4	60
203	2559	\N	\N
203	2560	\N	\N
203	2561	\N	\N
203	43	\N	\N
203	1865	5	\N
203	2564	5	\N
203	2565	\N	\N
203	82	\N	\N
203	1456	\N	\N
203	23	\N	\N
203	2569	\N	\N
203	2570	\N	\N
203	2571	\N	\N
203	2572	\N	\N
203	2573	\N	\N
203	2574	\N	\N
203	2575	\N	\N
203	2576	\N	\N
203	1774	\N	\N
203	1989	\N	\N
203	2579	\N	\N
203	2580	\N	\N
203	786	\N	\N
203	2582	\N	\N
203	2583	\N	\N
203	2584	\N	\N
203	2585	\N	\N
203	2586	\N	\N
203	2587	\N	\N
203	2588	\N	\N
203	2589	\N	\N
203	2590	\N	\N
203	2591	\N	\N
203	74	\N	\N
203	2593	\N	\N
203	2594	\N	\N
203	2595	\N	\N
204	1984	3	24
204	2597	\N	\N
204	1366	\N	\N
204	36	\N	\N
204	2600	\N	\N
204	2601	\N	\N
204	2602	\N	\N
204	2603	\N	\N
204	45	\N	\N
204	2605	\N	\N
204	1640	\N	\N
204	2607	\N	\N
204	2608	\N	\N
204	113	\N	\N
204	2411	\N	\N
205	2611	2	12
205	2612	\N	\N
205	2613	\N	\N
205	2614	\N	\N
206	142	4	60
206	2495	\N	\N
206	2617	\N	\N
206	234	\N	\N
206	2619	\N	\N
206	2620	\N	\N
206	207	\N	\N
206	2622	\N	\N
206	2623	\N	\N
206	2199	\N	\N
206	1	\N	\N
207	2626	\N	\N
207	2627	\N	\N
207	78	\N	\N
207	1993	\N	\N
207	151	\N	\N
208	114	\N	\N
208	1811	\N	\N
208	2633	\N	\N
208	2634	\N	\N
208	2635	\N	\N
208	2321	\N	\N
208	1196	\N	\N
208	25	\N	\N
208	1463	\N	\N
208	2640	\N	\N
208	139	\N	\N
208	23	\N	\N
208	1	\N	\N
209	2076	\N	\N
209	2645	\N	\N
209	2646	\N	\N
209	2647	5	\N
209	2648	5	\N
209	2649	5	\N
209	2650	\N	\N
209	2651	\N	\N
209	2652	\N	\N
209	1	\N	\N
209	2654	\N	\N
209	29	\N	\N
209	2380	\N	\N
209	2657	\N	\N
209	2658	\N	\N
209	252	\N	\N
209	27	\N	\N
209	442	\N	\N
209	99	\N	\N
209	151	\N	\N
210	1490	\N	\N
210	2665	\N	\N
210	2293	\N	\N
210	1774	\N	\N
210	1989	\N	\N
210	23	1	\N
210	134	\N	\N
211	2671	\N	\N
211	2672	\N	36
211	2673	\N	\N
211	2674	\N	\N
211	2675	\N	\N
211	1845	\N	12
211	2677	\N	\N
211	2678	\N	\N
211	219	\N	\N
211	90	\N	\N
212	23	5	60
212	139	3	\N
212	71	\N	\N
212	1	\N	\N
212	113	\N	\N
212	1927	\N	\N
213	2687	\N	\N
213	2688	\N	\N
213	2689	\N	\N
213	2690	\N	\N
213	23	\N	\N
213	25	\N	\N
214	1	1	\N
214	2358	1	\N
214	2695	1	\N
214	2360	1	\N
214	34	1	\N
214	2	1	\N
214	19	1	\N
214	2700	1	\N
214	1456	1	\N
214	2702	1	\N
214	15	1	\N
214	5	1	\N
214	2705	1	\N
214	2706	1	\N
215	1774	\N	\N
215	1759	\N	\N
215	1340	\N	\N
215	90	\N	\N
215	1731	\N	\N
216	1931	\N	36
216	1346	\N	36
216	252	\N	\N
216	63	\N	\N
217	139	1	\N
217	2717	1	\N
217	2718	1	\N
217	23	1	\N
217	138	\N	\N
217	2721	1	\N
217	1962	1	\N
218	2123	\N	\N
218	114	\N	\N
218	2056	\N	\N
218	23	\N	\N
218	1930	\N	\N
218	2728	\N	\N
218	1	\N	\N
218	2358	\N	\N
218	2360	\N	\N
218	2380	\N	\N
218	29	\N	\N
218	2734	\N	\N
218	2735	\N	\N
218	2736	\N	\N
218	2737	\N	\N
218	2738	\N	\N
218	2739	\N	\N
218	2740	\N	\N
218	2741	\N	\N
219	2742	\N	\N
219	2743	\N	\N
219	2744	\N	\N
219	44	\N	\N
220	25	\N	\N
220	2123	\N	\N
220	2748	\N	\N
220	2749	\N	\N
220	2750	\N	\N
220	23	\N	\N
220	786	\N	\N
220	2753	\N	\N
220	2754	\N	\N
220	35	\N	\N
221	1922	3	36
221	1	283	\N
221	23	284	\N
221	1925	\N	\N
221	1945	\N	\N
221	2761	\N	\N
221	2580	\N	\N
221	82	\N	\N
221	1364	\N	\N
221	5	\N	\N
221	1911	\N	\N
222	1882	4	\N
222	1967	4	\N
222	2298	\N	\N
222	1968	\N	\N
222	1969	\N	\N
222	2772	\N	\N
222	2773	\N	\N
222	1970	\N	\N
222	2775	\N	\N
222	1966	\N	\N
222	1926	\N	\N
222	1927	\N	\N
223	2690	4	36
223	2780	\N	\N
223	2022	\N	\N
223	2782	\N	\N
223	1985	\N	\N
223	1986	\N	\N
223	2785	\N	\N
223	2786	\N	\N
224	23	\N	\N
224	1771	\N	\N
224	1928	\N	\N
225	2790	1	\N
225	2791	\N	\N
225	1930	\N	\N
226	1424	\N	\N
226	2794	\N	\N
226	2795	\N	\N
226	1419	\N	\N
226	15	\N	\N
226	2798	\N	\N
226	2297	\N	\N
227	1989	\N	\N
227	113	\N	\N
227	1927	\N	\N
227	1985	\N	\N
227	2170	\N	\N
227	75	\N	\N
227	23	\N	\N
227	1196	\N	\N
227	2690	\N	\N
227	2809	\N	\N
227	2810	\N	\N
227	2811	\N	\N
228	2164	4	48
228	1303	\N	\N
228	2814	\N	\N
228	2815	\N	\N
228	2816	\N	\N
228	2817	\N	\N
228	2818	\N	\N
228	113	\N	\N
228	1927	\N	\N
229	1	4	72
229	2380	\N	\N
229	29	\N	\N
229	2382	\N	\N
229	2383	\N	\N
229	1958	\N	\N
229	54	\N	\N
229	1364	\N	\N
229	1911	\N	\N
230	114	\N	\N
230	1931	4	72
230	252	\N	\N
230	2167	\N	\N
230	2166	\N	\N
230	2835	\N	\N
230	44	\N	\N
230	2382	\N	\N
230	1958	\N	\N
230	2383	\N	\N
230	2840	\N	\N
230	2841	5	\N
230	790	\N	\N
230	1	\N	\N
231	2844	\N	60
231	2845	\N	60
231	2058	\N	60
231	1490	\N	60
231	1876	\N	60
231	2849	\N	\N
231	1926	\N	\N
231	2851	\N	\N
231	2852	\N	\N
231	2853	\N	\N
231	2854	\N	\N
231	2855	\N	\N
232	1775	1	\N
233	2857	\N	12
233	2092	3	\N
233	2859	\N	\N
234	1369	4	54
234	45	4	54
234	39	3	\N
234	1733	3	\N
234	2507	3	\N
234	207	3	\N
234	2866	3	\N
234	230	\N	\N
234	2495	\N	\N
234	1847	\N	\N
234	2870	\N	\N
234	231	\N	\N
234	184	\N	\N
234	2008	\N	\N
234	2010	\N	\N
234	2484	\N	\N
234	233	\N	\N
234	777	\N	\N
234	1606	\N	\N
234	54	3	\N
234	65	3	\N
234	74	\N	\N
234	2882	\N	\N
234	2883	\N	\N
234	2884	\N	\N
236	169	\N	\N
236	168	\N	\N
240	2895	\N	\N
240	2896	\N	\N
243	2901	\N	\N
243	151	\N	\N
243	2898	\N	\N
248	2909	\N	\N
248	2898	\N	\N
249	165	1	\N
250	2912	\N	\N
250	168	\N	\N
250	2914	\N	\N
251	98	\N	\N
251	2916	\N	\N
251	2917	\N	\N
263	165	\N	\N
273	165	1	\N
273	2951	1	\N
274	2912	\N	\N
274	2918	\N	\N
274	165	\N	\N
281	165	\N	\N
281	2958	\N	\N
284	2912	\N	\N
284	2970	\N	\N
284	2919	\N	\N
246	2948	\N	12
246	2949	\N	\N
253	2919	\N	\N
253	2898	\N	\N
253	2942	\N	\N
264	2912	\N	\N
264	165	\N	\N
264	2929	\N	\N
264	2899	\N	\N
264	169	\N	\N
254	168	\N	\N
254	2909	\N	\N
254	2923	\N	\N
285	168	\N	\N
261	2912	\N	\N
261	2931	\N	\N
242	165	\N	\N
287	168	\N	\N
288	2993	\N	\N
266	168	\N	\N
266	2909	\N	\N
289	98	\N	\N
289	2997	\N	\N
290	2891	\N	\N
239	165	\N	\N
291	2923	\N	\N
291	3001	\N	\N
291	2993	\N	\N
291	3003	\N	\N
292	3004	\N	\N
292	3005	\N	\N
269	165	\N	\N
269	138	\N	\N
269	2945	\N	\N
294	168	\N	\N
294	2909	\N	\N
294	2923	\N	\N
295	165	\N	\N
295	2929	\N	\N
295	168	\N	\N
295	169	\N	\N
296	168	\N	\N
297	2891	\N	\N
297	3018	\N	12
297	2964	\N	\N
297	165	\N	\N
298	165	\N	\N
298	2964	\N	\N
299	2891	\N	\N
300	2909	\N	\N
300	2891	\N	\N
300	3026	\N	\N
300	168	\N	\N
300	169	\N	\N
300	3029	\N	\N
301	168	\N	\N
301	2918	\N	\N
302	2912	\N	\N
303	2923	\N	\N
303	2908	\N	\N
304	2918	\N	\N
305	3036	\N	\N
305	151	\N	\N
272	2912	\N	\N
272	2907	\N	\N
279	3040	\N	\N
279	3001	\N	\N
279	2896	\N	\N
306	168	\N	\N
306	2909	\N	\N
307	2918	\N	\N
307	165	\N	\N
308	2912	\N	\N
308	2923	\N	\N
309	2955	\N	\N
309	545	\N	\N
310	3051	\N	\N
278	2955	\N	\N
278	2942	\N	\N
268	2898	\N	\N
268	2942	\N	\N
313	2891	\N	\N
313	3060	\N	\N
282	165	1	\N
282	1635	\N	\N
282	1196	\N	\N
282	138	1	\N
282	2961	1	\N
282	2964	\N	\N
314	3067	\N	12
314	3029	\N	12
314	545	\N	\N
314	3070	\N	\N
314	3071	\N	\N
314	3072	\N	\N
314	3073	\N	\N
315	98	\N	\N
315	3075	\N	\N
316	2918	\N	\N
316	168	\N	\N
317	2912	\N	\N
317	168	\N	\N
317	3080	\N	\N
317	3081	\N	\N
271	2918	\N	\N
271	2947	\N	\N
318	168	\N	\N
318	3085	\N	\N
319	3086	\N	\N
319	3087	\N	\N
319	3088	\N	\N
319	3089	\N	\N
319	169	\N	\N
320	168	\N	\N
321	2918	\N	\N
321	3093	\N	\N
322	2912	\N	\N
322	369	\N	\N
322	165	\N	\N
323	2885	\N	\N
324	168	\N	\N
324	2895	\N	\N
325	3100	\N	\N
326	2891	\N	\N
326	165	\N	\N
326	168	\N	\N
327	2891	\N	\N
327	545	\N	\N
327	165	1	\N
328	2912	\N	\N
257	151	\N	\N
257	169	\N	\N
257	419	\N	\N
329	3060	\N	\N
329	2918	\N	\N
330	2918	\N	\N
332	3114	\N	\N
332	3115	1	\N
332	3116	\N	\N
332	3117	\N	\N
332	3118	\N	\N
332	3119	\N	\N
333	2912	\N	\N
333	1196	\N	\N
333	2918	\N	\N
333	78	\N	\N
337	168	\N	\N
337	165	1	\N
340	2912	\N	\N
340	3117	\N	\N
341	2923	\N	\N
341	165	1	\N
341	2918	1	\N
341	168	\N	\N
341	169	\N	\N
342	2918	\N	\N
343	165	\N	\N
343	2908	\N	\N
335	2891	\N	\N
335	168	\N	\N
335	169	\N	\N
335	3001	\N	\N
346	3146	\N	\N
346	2919	\N	\N
346	165	\N	\N
245	165	1	\N
245	168	\N	\N
245	545	\N	\N
349	165	\N	\N
350	2918	\N	\N
238	3070	\N	\N
238	2891	\N	\N
238	165	\N	\N
238	78	\N	\N
238	168	\N	\N
351	2912	\N	\N
352	3060	\N	\N
352	3001	\N	\N
352	3162	\N	\N
352	3163	\N	\N
339	3130	\N	\N
339	3131	\N	\N
353	2912	\N	\N
353	3167	\N	\N
353	169	\N	\N
354	2912	\N	\N
355	3170	\N	\N
356	3170	\N	\N
357	2964	\N	\N
357	2912	\N	\N
283	2923	\N	\N
283	2967	\N	\N
283	78	\N	\N
283	2908	\N	\N
358	3085	\N	\N
358	151	\N	\N
359	2942	\N	\N
360	169	\N	\N
360	151	\N	\N
361	2955	\N	\N
362	169	\N	\N
362	151	\N	\N
363	165	\N	\N
363	138	\N	\N
363	369	\N	\N
363	3189	\N	\N
364	2912	\N	\N
364	369	\N	\N
364	2955	\N	\N
366	168	\N	\N
366	169	\N	\N
367	3170	\N	\N
368	3067	\N	12
368	3029	\N	12
368	3198	\N	\N
368	3199	\N	\N
368	3073	\N	\N
368	98	\N	\N
370	2909	\N	\N
371	2923	\N	\N
371	78	283	\N
312	2891	\N	12
312	1635	\N	\N
312	78	\N	\N
372	1633	1	\N
372	2912	\N	\N
372	545	\N	\N
286	168	\N	\N
286	2912	\N	\N
286	165	1	\N
373	2942	\N	\N
374	2942	\N	\N
375	3117	\N	\N
375	3170	\N	\N
376	3119	\N	\N
376	3219	\N	\N
376	3220	\N	\N
376	2152	\N	\N
376	3222	\N	\N
376	2895	\N	\N
377	3224	\N	\N
377	3225	\N	\N
377	2634	\N	\N
377	3227	\N	\N
377	3228	\N	\N
377	3229	\N	\N
377	2047	\N	24
377	78	\N	\N
377	2321	\N	\N
377	3233	\N	\N
377	1635	\N	\N
377	3235	\N	\N
377	3236	\N	\N
377	3237	\N	\N
377	3238	\N	\N
377	3239	\N	\N
377	3240	\N	\N
377	3241	\N	\N
377	3242	\N	\N
377	3243	\N	\N
377	3244	\N	\N
401	165	\N	\N
401	151	\N	\N
401	3422	\N	48
401	3423	\N	\N
401	3424	\N	\N
401	3425	\N	\N
378	3245	\N	\N
378	3246	\N	\N
378	134	\N	\N
378	545	\N	\N
402	3430	\N	\N
402	3431	\N	\N
402	3432	\N	\N
402	3433	\N	\N
403	2237	\N	\N
403	3435	\N	\N
403	3436	\N	\N
403	3437	\N	\N
403	151	\N	\N
403	153	\N	\N
403	1686	\N	\N
379	3441	\N	\N
379	3442	\N	\N
379	3443	\N	\N
404	3252	\N	\N
404	3229	\N	\N
404	3446	\N	\N
404	3447	\N	\N
380	2786	\N	24
380	419	\N	\N
381	2786	\N	\N
405	2786	\N	\N
405	419	\N	\N
406	78	\N	\N
407	2786	\N	\N
382	311	\N	\N
382	80	\N	\N
382	90	\N	\N
382	3266	\N	\N
382	108	\N	\N
382	84	\N	\N
382	148	\N	\N
382	143	\N	\N
382	3271	\N	\N
382	104	\N	\N
382	3273	\N	\N
382	71	\N	\N
382	3275	\N	\N
382	97	\N	\N
382	151	\N	\N
383	88	\N	\N
383	1367	\N	\N
383	90	\N	\N
384	1556	\N	\N
384	104	\N	\N
384	3284	\N	\N
384	3285	\N	\N
384	70	\N	\N
384	69	\N	\N
384	3288	\N	\N
384	54	\N	\N
384	1419	\N	\N
384	1395	\N	\N
384	2795	1	\N
384	1424	1	\N
385	169	\N	\N
385	3295	\N	\N
385	138	\N	\N
385	1793	\N	\N
385	3298	\N	\N
385	98	\N	\N
385	3300	\N	\N
386	78	\N	\N
386	151	\N	\N
386	545	\N	\N
387	369	3	24
387	3304	\N	\N
387	3305	284	\N
387	1967	\N	\N
387	2131	\N	\N
388	78	\N	24
388	165	\N	\N
388	3310	\N	\N
388	1826	\N	\N
409	65	\N	\N
409	54	\N	\N
409	207	\N	\N
409	3507	\N	\N
409	3508	\N	\N
409	129	\N	\N
409	184	\N	\N
409	233	\N	\N
409	790	\N	\N
409	1385	\N	\N
409	1	\N	\N
409	3515	\N	\N
409	3516	\N	\N
409	1387	\N	\N
409	69	\N	\N
409	2345	\N	\N
409	3520	\N	\N
410	53	1	\N
410	197	\N	\N
411	53	\N	\N
411	78	\N	\N
411	87	\N	\N
411	129	\N	\N
411	54	\N	\N
412	148	\N	\N
412	165	\N	\N
412	1141	\N	\N
412	153	\N	\N
390	53	\N	\N
390	54	\N	\N
390	45	\N	\N
391	1796	\N	36
391	115	\N	36
391	114	\N	36
391	2133	\N	36
391	3319	\N	36
391	199	160	\N
391	78	160	\N
392	80	284	\N
392	104	284	\N
392	108	284	\N
392	3300	\N	\N
392	84	\N	\N
392	3329	\N	\N
392	54	\N	\N
413	320	\N	\N
413	2841	\N	\N
413	39	\N	\N
413	1613	\N	\N
413	3553	\N	\N
413	1967	\N	\N
413	121	\N	\N
413	1419	\N	\N
413	106	\N	\N
413	3558	\N	\N
413	1558	\N	\N
413	3560	\N	\N
393	108	\N	\N
394	3332	\N	\N
414	1464	\N	\N
414	53	\N	\N
414	78	\N	\N
414	3566	\N	\N
414	3567	\N	\N
414	142	\N	\N
414	207	\N	\N
414	68	\N	\N
414	39	\N	\N
416	3572	\N	\N
416	312	\N	\N
416	320	\N	\N
416	87	\N	\N
416	1348	\N	\N
416	39	\N	\N
395	3333	\N	\N
395	3334	\N	\N
395	3335	\N	\N
395	3336	\N	\N
395	1169	\N	\N
396	114	\N	\N
396	3339	\N	\N
396	65	\N	\N
396	104	\N	\N
396	3342	\N	\N
396	2440	\N	\N
396	3344	\N	\N
396	3345	\N	\N
396	3346	\N	\N
396	23	\N	\N
396	3348	\N	\N
396	3349	\N	\N
396	3350	\N	\N
396	1196	\N	\N
396	1	\N	\N
396	790	\N	\N
396	3273	\N	\N
396	71	\N	\N
396	3356	\N	\N
396	3357	\N	\N
396	3358	\N	\N
397	3333	\N	\N
397	3334	\N	\N
397	3335	\N	\N
397	3336	\N	\N
397	1169	\N	\N
417	3609	\N	\N
417	211	\N	\N
417	104	\N	\N
417	115	\N	\N
417	2133	\N	\N
417	36	\N	\N
417	114	\N	\N
418	65	\N	\N
418	3617	\N	\N
418	69	\N	\N
418	70	\N	\N
418	105	\N	\N
418	54	\N	\N
398	3364	\N	12
398	3365	\N	\N
398	3366	\N	\N
398	151	\N	\N
398	3368	\N	\N
398	1834	\N	\N
419	78	\N	\N
419	65	\N	\N
419	129	\N	\N
419	3631	\N	\N
399	104	\N	12
399	199	\N	\N
399	87	\N	\N
399	70	\N	\N
399	69	\N	\N
399	3375	\N	\N
399	1387	\N	\N
399	78	\N	\N
399	165	\N	\N
399	3378	\N	\N
420	1867	\N	\N
420	3643	\N	\N
420	3644	\N	\N
421	1502	\N	\N
422	1340	\N	\N
422	152	\N	\N
423	3648	\N	\N
423	3649	\N	\N
423	3650	\N	\N
423	3651	\N	\N
423	3652	\N	\N
423	3653	\N	\N
423	108	\N	\N
423	311	\N	\N
423	3656	\N	\N
423	3657	\N	\N
423	3658	\N	\N
400	53	\N	\N
400	54	\N	\N
400	2440	\N	\N
400	2441	\N	\N
400	106	\N	\N
400	3384	\N	\N
400	3385	\N	\N
400	3386	\N	\N
400	2602	\N	\N
400	3668	\N	\N
400	3389	\N	\N
400	3390	\N	\N
400	3671	\N	\N
400	3392	\N	\N
424	108	\N	\N
424	3674	\N	\N
425	165	\N	\N
425	78	\N	\N
425	3677	\N	\N
426	303	\N	12
428	3679	\N	\N
428	3680	\N	\N
429	1	283	\N
429	4	284	\N
429	2	284	\N
429	3	284	\N
429	2358	284	\N
429	2695	284	\N
429	27	\N	\N
430	1	1	\N
430	1967	1	\N
430	23	1	\N
430	1645	1	\N
430	27	\N	\N
430	3693	\N	\N
431	1	1	\N
431	1967	1	\N
431	23	1	\N
431	1645	1	\N
431	27	1	\N
432	1	1	\N
432	1967	1	\N
432	23	1	\N
432	27	\N	\N
432	1645	1	\N
432	3	\N	\N
432	4	\N	\N
433	1	1	\N
433	1967	1	\N
433	23	1	\N
433	1645	1	\N
433	27	\N	\N
434	3711	\N	\N
434	149	\N	\N
434	150	\N	\N
434	3714	\N	\N
434	3715	\N	\N
434	3716	\N	\N
435	3717	3	\N
435	2810	\N	\N
435	2690	\N	\N
436	3720	\N	\N
436	3721	\N	\N
436	3722	\N	\N
437	3679	\N	\N
439	2041	4	60
439	3693	4	60
439	1	\N	\N
439	54	\N	\N
439	3728	\N	\N
439	1985	\N	\N
439	1986	\N	\N
439	2170	\N	\N
440	1775	\N	12
440	1645	\N	\N
440	1948	\N	\N
440	3735	\N	\N
440	3736	\N	\N
440	3737	\N	\N
440	39	\N	\N
440	3739	\N	\N
440	3740	\N	\N
440	2382	\N	\N
441	2041	\N	\N
441	54	\N	\N
441	3744	\N	\N
441	27	\N	\N
441	252	\N	\N
441	1	1	\N
441	1876	\N	\N
441	3749	\N	\N
441	1948	\N	\N
441	2146	\N	\N
441	106	\N	\N
441	3753	\N	\N
441	113	\N	\N
441	1927	\N	\N
442	2786	\N	\N
442	134	\N	\N
443	3758	\N	\N
443	110	283	\N
443	3329	283	\N
443	3761	283	\N
443	3762	\N	\N
443	3763	\N	\N
444	3679	\N	\N
444	3765	\N	\N
445	1196	\N	\N
445	1635	\N	\N
445	2321	\N	\N
445	23	\N	\N
445	120	\N	\N
445	3067	\N	\N
445	3772	\N	\N
445	3773	\N	\N
446	61	\N	12
446	148	\N	\N
446	791	\N	\N
446	80	\N	\N
446	132	\N	\N
446	108	\N	\N
447	3114	\N	\N
448	3781	\N	\N
449	3782	\N	\N
449	98	\N	\N
449	1899	\N	\N
449	3785	\N	\N
449	100	\N	\N
449	165	\N	\N
450	2786	\N	12
450	1196	\N	\N
450	3790	\N	\N
451	3791	\N	\N
453	3792	\N	\N
453	3793	\N	\N
453	3794	\N	\N
454	3795	\N	\N
455	149	1	\N
455	150	1	\N
455	138	\N	\N
455	3799	\N	\N
455	3800	\N	\N
455	3651	\N	\N
456	2786	\N	36
456	1899	\N	\N
456	3804	\N	\N
456	3805	\N	\N
456	1993	\N	\N
456	3807	\N	\N
457	3808	\N	\N
458	2786	\N	\N
458	303	\N	\N
458	95	\N	\N
458	3812	\N	\N
458	3813	\N	\N
458	3366	\N	\N
458	3815	\N	\N
459	3816	1	\N
459	790	1	\N
459	230	1	\N
459	3819	\N	\N
459	3820	\N	\N
459	1733	\N	\N
459	43	\N	\N
459	3823	\N	\N
459	169	\N	\N
460	3825	\N	\N
460	3826	\N	\N
461	98	\N	\N
461	3114	\N	\N
461	2899	\N	\N
462	3830	3	24
462	303	\N	\N
462	2254	\N	\N
462	3833	\N	\N
462	2786	\N	\N
463	3835	\N	\N
463	2240	\N	\N
463	3837	\N	\N
464	165	\N	\N
465	3839	\N	\N
465	3840	\N	\N
465	99	\N	\N
466	3553	3	\N
466	3843	3	\N
466	2690	3	\N
466	2810	3	\N
467	1775	\N	\N
467	1	\N	\N
467	2041	\N	\N
467	1424	\N	\N
467	121	\N	\N
467	15	\N	\N
467	1949	\N	\N
467	5	\N	\N
468	2786	\N	\N
468	134	\N	\N
469	3856	\N	\N
469	78	\N	\N
469	3858	\N	\N
470	53	283	\N
470	78	283	\N
470	108	1	\N
470	312	\N	\N
471	3863	\N	\N
471	2786	\N	\N
471	2785	\N	\N
472	3866	\N	\N
472	3867	\N	\N
472	3868	\N	\N
473	2411	\N	\N
473	3870	\N	\N
473	3871	\N	\N
474	3872	\N	\N
474	1358	\N	\N
474	1509	\N	\N
475	3872	\N	\N
\.

COPY public.sub_categories (id, sub_category) FROM stdin;
1	Administracja systemami
2	Analiza biznesowa
3	Programowanie
4	Administracja baz danych i przechowywania
5	Administracja systemów
8	Architektura
14	Administrowanie sieciami
15	Administrowanie systemami
16	Bezpieczeństwo / Audyt
24	Wsparcie techniczne / Helpdesk
34	Administrowanie bazami danych i storage
40	Zarządzanie projektem/produktem
45	Zarządzanie usługami
46	Wprowadzanie / Przetwarzanie danych
54	IT / Telekomunikacja
72	Administracja sieci
78	Administracja bazami danych i przechowywaniem
138	Wdrożenia ERP
149	Administracja sieciami
151	Security / Audit
342	Cyberbezpieczeństwo
438	Wsparcie / Helpdesk
444	Specjaliści / Urzędnicy
454	Automatyka
455	Monterzy / Serwisanci
491	Telekomunikacja
495	Bezpieczeństwo / Porządek Publiczny
507	Testowanie
509	Analiza biznesowa i systemowa
535	Projektowanie UX/UI
539	Analiza / Ryzyko
588	Business Intelligence / Data Warehouse
589	FMCG
612	Elektronika / Elektryka
641	IT/Telekomunikacja
647	Zarządzanie projektami
770	Logistyka
772	Team leader
773	Manager
774	Informatyk
784	Informatyka
792	Biolog, Botanik
845	Finanse
846	Rekrutacja
\.

COPY public.sub_categories_junction (offer_id, sub_category_id) FROM stdin;
51	14
51	15
51	16
53	14
53	15
53	24
54	4
54	72
54	5
56	34
56	14
56	15
57	34
57	15
57	8
59	14
59	15
59	24
60	14
60	15
60	24
61	14
61	15
62	14
62	15
62	24
63	34
63	14
12	14
12	15
12	8
63	15
65	34
65	14
65	15
66	149
66	1
66	151
68	14
68	15
1	1
1	2
1	3
68	24
73	5
73	16
73	438
75	14
75	15
75	16
76	14
76	24
76	444
77	14
77	15
77	24
80	14
80	15
80	16
82	14
82	15
82	24
2	4
2	5
2	3
83	454
83	455
83	24
84	14
84	15
84	24
86	14
86	15
86	24
87	14
87	15
87	24
88	14
88	15
88	16
89	14
89	15
89	24
90	14
90	15
90	24
91	34
91	14
91	24
92	14
92	15
92	16
93	14
3	2
3	8
3	3
4	1
4	2
4	3
5	3
6	14
6	15
6	16
7	14
7	15
8	14
8	15
8	16
41	14
41	15
41	45
11	14
11	15
11	24
13	14
13	15
13	342
14	14
14	15
14	24
16	14
16	15
16	24
17	34
17	14
17	15
18	14
18	15
18	24
45	15
45	138
45	24
22	3
23	15
23	24
23	40
47	14
47	15
26	46
26	34
26	15
27	34
27	14
27	15
28	14
28	24
28	54
29	14
29	15
29	24
30	14
30	15
30	24
33	34
33	15
33	138
49	15
49	24
49	40
34	15
35	14
35	15
35	24
44	15
44	8
44	3
36	14
36	15
36	24
38	14
38	15
38	16
93	16
93	24
97	14
97	15
97	24
98	15
100	14
100	15
100	24
104	491
104	14
104	16
105	15
105	495
107	14
107	15
107	16
109	14
109	15
109	24
111	14
111	15
111	16
112	15
112	24
112	507
113	15
113	509
113	3
117	34
117	14
117	15
118	15
118	16
118	40
120	14
120	24
123	14
123	15
123	24
124	14
124	16
124	24
126	509
126	507
127	8
127	507
127	40
128	8
128	3
128	40
129	509
129	40
129	535
130	15
130	45
130	509
131	539
131	34
133	15
133	509
133	8
135	34
135	15
135	3
137	34
137	15
137	3
138	15
138	8
138	3
139	15
139	509
139	40
140	15
140	45
140	40
141	14
141	15
141	16
142	34
142	15
142	509
144	3
144	507
144	40
145	3
146	15
146	45
146	495
148	507
150	15
150	138
150	509
151	34
151	509
151	507
152	34
152	15
152	8
155	15
155	24
155	54
156	34
156	14
156	15
157	588
157	589
157	34
158	34
158	509
159	509
159	495
160	15
160	8
160	507
161	588
161	40
162	34
162	14
162	15
163	14
163	15
163	16
164	509
164	8
164	40
165	15
165	509
165	40
167	612
167	455
167	24
168	15
168	138
168	509
169	509
169	8
169	3
170	15
170	24
170	40
171	34
171	15
171	40
172	612
172	8
172	3
173	34
173	15
173	509
174	491
174	8
174	3
177	34
177	15
177	138
179	14
179	15
179	641
180	539
180	509
180	3
181	3
181	507
181	647
183	15
183	138
183	509
184	15
184	8
184	3
185	34
185	15
185	509
186	16
186	509
187	14
187	15
187	16
189	15
189	509
189	3
191	507
192	15
192	24
192	509
193	34
193	509
193	3
194	3
194	507
195	14
195	15
195	8
197	34
197	14
197	15
198	15
198	45
198	40
200	15
200	509
200	8
201	15
201	509
201	3
203	8
204	16
204	45
204	40
205	14
205	15
205	24
206	34
206	509
206	3
207	15
207	40
208	15
208	138
208	3
210	34
210	15
210	509
211	14
211	16
212	34
212	15
212	509
213	34
213	15
213	509
214	15
214	8
214	3
215	15
215	509
215	3
216	34
216	15
216	3
218	15
218	509
218	3
219	14
219	15
219	24
220	588
220	34
220	15
221	34
221	15
221	8
222	15
222	509
222	3
223	34
223	15
223	509
224	15
224	509
224	40
225	15
225	8
225	3
226	34
226	15
226	3
227	45
228	15
228	509
228	507
229	34
229	15
229	3
231	34
231	15
231	509
232	15
232	509
232	3
233	34
233	15
233	3
234	14
234	15
234	16
330	770
344	770
377	772
401	792
378	773
382	774
383	774
384	774
385	774
387	774
388	774
408	774
389	774
409	774
410	774
411	784
390	774
391	774
392	774
413	774
393	784
394	774
416	774
395	774
396	774
397	774
417	774
418	774
419	774
399	774
420	774
422	774
423	774
400	774
425	774
426	774
427	774
428	774
429	774
430	784
431	774
432	774
433	774
434	774
436	784
437	774
440	774
441	774
442	774
443	774
444	774
445	774
449	774
452	774
455	774
459	774
462	845
462	846
466	774
468	774
469	774
471	784
472	774
474	774
475	784
\.


SELECT pg_catalog.setval('public.benefits_id_seq', 3699, true);
SELECT pg_catalog.setval('public.cities_id_seq', 693, true);
SELECT pg_catalog.setval('public.companies_id_seq', 739, true);
SELECT pg_catalog.setval('public.currencies_id_seq', 232, true);
SELECT pg_catalog.setval('public.education_levels_id_seq', 500, true);
SELECT pg_catalog.setval('public.employment_schedules_id_seq', 706, true);
SELECT pg_catalog.setval('public.employment_types_id_seq', 826, true);
SELECT pg_catalog.setval('public.experience_levels_id_seq', 384, true);
SELECT pg_catalog.setval('public.language_levels_id_seq', 312, true);
SELECT pg_catalog.setval('public.languages_id_seq', 363, true);
SELECT pg_catalog.setval('public.leading_categories_id_seq', 735, true);
SELECT pg_catalog.setval('public.location_details_id_seq', 693, true);
SELECT pg_catalog.setval('public.offers_id_seq', 475, true);
SELECT pg_catalog.setval('public.postal_codes_id_seq', 405, true);
SELECT pg_catalog.setval('public.salary_periods_id_seq', 217, true);
SELECT pg_catalog.setval('public.skills_id_seq', 3875, true);
SELECT pg_catalog.setval('public.sources_id_seq', 739, true);
SELECT pg_catalog.setval('public.streets_id_seq', 441, true);
SELECT pg_catalog.setval('public.sub_categories_id_seq', 853, true);