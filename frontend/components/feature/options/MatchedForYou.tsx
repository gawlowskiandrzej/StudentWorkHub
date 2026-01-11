"use client";

import { useEffect, useState, useCallback, useRef } from 'react';
import { useUser } from '@/store/userContext';
import { useRanking } from '@/store/RankingContext';
import { ListElement } from '@/components/feature/list/ListElement';
import { ListElementSkeleton } from '@/components/feature/list/ListElementSkeleton';
import { Button } from '@/components/ui/button';
import { useTranslation } from 'react-i18next';
import { RefreshCw } from 'lucide-react';
import { OfferApi } from '@/lib/api/controllers/offer';
import { useProfileCreationDictionaries } from '@/hooks/useDictionaries';
import listStyles from '@/styles/OfferList.module.css';
import buttonStyle from '@/styles/ButtonStyle.module.css';
import { offer } from '@/types/list/Offer/offer';

export function MatchedForYou(){
    const { t } = useTranslation("common");
    const { preferences, isAuthenticated, fetchPreferences } = useUser();
    const { rankOffersList, getOfferScore } = useRanking();
    const { fullDictionaries, loading: dictionariesLoading } = useProfileCreationDictionaries();
    
    const [matchedOffers, setMatchedOffers] = useState<offer[]>([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const hasInitializedFetchRef = useRef(false);

    const fetchMatchedOffers = useCallback(async () => {
        if (!isAuthenticated || !preferences) {
            setLoading(false);
            return;
        }

        setLoading(true);
        setError(null);
        
        console.log('[MatchedForYou] Starting fetch with preferences:', preferences);

        try {
            const categoryName = preferences.leading_category_id && fullDictionaries
                ? fullDictionaries.leading_categories.find(c => c.id === preferences.leading_category_id)?.name || ""
                : "";

            const skillsFilter = preferences.skills && preferences.skills.length > 0
                ? preferences.skills.map((s: any) => s.skill_name).join("|")
                : "";

            const searchConfigs = [
                // 1. Pełne filtry - zakres pensji + lokalizacja + kategoria + umiejętności
                {
                    searchQuery: {
                        keyword: "",
                        category: categoryName,
                        localization: preferences.city_name || "",
                    },
                    searchFilterKeywords: {
                        skillName: skillsFilter,
                        educationName: "",
                        benefitName: "",
                    },
                    filters: {
                        salaryFrom: preferences.salary_from ? preferences.salary_from.toString() : undefined,
                        salaryTo: preferences.salary_to ? preferences.salary_to.toString() : undefined,
                        employmentType: undefined,
                        employmentSchedules: undefined,
                        salaryPeriods: undefined,
                    },
                },
                // 2. Bez zakresu pensji - lokalizacja + kategoria + umiejętności
                {
                    searchQuery: {
                        keyword: "",
                        category: categoryName,
                        localization: preferences.city_name || "",
                    },
                    searchFilterKeywords: {
                        skillName: skillsFilter,
                        educationName: "",
                        benefitName: "",
                    },
                    filters: {
                        salaryFrom: undefined,
                        salaryTo: undefined,
                        employmentType: undefined,
                        employmentSchedules: undefined,
                        salaryPeriods: undefined,
                    },
                },
                // 3. Bez umiejętności - lokalizacja + zakres pensji + kategoria
                {
                    searchQuery: {
                        keyword: "",
                        category: categoryName,
                        localization: preferences.city_name || "",
                    },
                    searchFilterKeywords: {
                        skillName: "",
                        educationName: "",
                        benefitName: "",
                    },
                    filters: {
                        salaryFrom: preferences.salary_from ? preferences.salary_from.toString() : undefined,
                        salaryTo: preferences.salary_to ? preferences.salary_to.toString() : undefined,
                        employmentType: undefined,
                        employmentSchedules: undefined,
                        salaryPeriods: undefined,
                    },
                },
                // 4. Bez umiejętności - kategoria + zakres pensji
                {
                    searchQuery: {
                        keyword: "",
                        category: categoryName,
                        localization: "",
                    },
                    searchFilterKeywords: {
                        skillName: "",
                        educationName: "",
                        benefitName: "",
                    },
                    filters: {
                        salaryFrom: preferences.salary_from ? preferences.salary_from.toString() : undefined,
                        salaryTo: preferences.salary_to ? preferences.salary_to.toString() : undefined,
                        employmentType: undefined,
                        employmentSchedules: undefined,
                        salaryPeriods: undefined,
                    },
                },
                // 5. Tylko kategoria
                {
                    searchQuery: {
                        keyword: "",
                        category: categoryName,
                        localization: "",
                    },
                    searchFilterKeywords: {
                        skillName: "",
                        educationName: "",
                        benefitName: "",
                    },
                    filters: {
                        salaryFrom: undefined,
                        salaryTo: undefined,
                        employmentType: undefined,
                        employmentSchedules: undefined,
                        salaryPeriods: undefined,
                    },
                },
                // 6. Brak filtrów - wyszukiwanie bez jakichkolwiek ograniczeń
                {
                    searchQuery: {
                        keyword: "",
                        category: "",
                        localization: "",
                    },
                    searchFilterKeywords: {
                        skillName: "",
                        educationName: "",
                        benefitName: "",
                    },
                    filters: {
                        salaryFrom: undefined,
                        salaryTo: undefined,
                        employmentType: undefined,
                        employmentSchedules: undefined,
                        salaryPeriods: undefined,
                    },
                },
            ];

            let offers: offer[] = [];

            for (let i = 0; i < searchConfigs.length; i++) {
                const config = searchConfigs[i];
                
                const mappedConfig = {
                    searchQuery: {
                        keyword: config.searchQuery.keyword || "(none)",
                        category: config.searchQuery.category || "(none)",
                        localization: config.searchQuery.localization || "(none)",
                    },
                    filters: {
                        salaryFrom: config.filters.salaryFrom || "(none)",
                        salaryTo: config.filters.salaryTo || "(none)",
                    },
                    skills: config.searchFilterKeywords.skillName || "(none)"
                };
                
                console.log(`[MatchedForYou] Config ${i + 1}:`, mappedConfig);
                
                const response = await OfferApi.getOffersDatabase(config.searchQuery, {
                    ...config.filters,
                    ...config.searchFilterKeywords
                });
                
                if (response.error) {
                    console.warn(`[MatchedForYou] Config ${i + 1} failed:`, response.error);
                    continue;
                }

                offers = response.data?.pagination?.items ?? [];
                console.log(`[MatchedForYou] Config ${i + 1} found ${offers.length} offers`);
                
                if (offers.length > 0) {
                    break;
                }
            }
            
            console.log('[MatchedForYou] Total offers to rank:', offers.length);
            
            if (offers.length > 0) {
                const rankedOffers = rankOffersList(offers);
                const sortedOffers = rankedOffers
                    .map(scored => offers.find((o: offer) => o.id === scored.offerId)!)
                    .filter(Boolean)
                    .slice(0, 10);
                
                console.log('[MatchedForYou] Matched offers after ranking:', sortedOffers.length);
                setMatchedOffers(sortedOffers);
            } else {
                console.warn('[MatchedForYou] No offers found');
                setMatchedOffers([]);
            }
        } catch (err) {
            console.error('[MatchedForYou] Error fetching offers:', err);
            setError(err instanceof Error ? err.message : 'Failed to load matched offers');
            setMatchedOffers([]);
        } finally {
            setLoading(false);
        }
    }, [isAuthenticated, preferences, fullDictionaries, rankOffersList]);

    useEffect(() => {
        if (isAuthenticated && !hasInitializedFetchRef.current) {
            hasInitializedFetchRef.current = true;
            fetchPreferences();
        }
    }, [isAuthenticated, fetchPreferences]);

    useEffect(() => {
        if (isAuthenticated && preferences && fullDictionaries && !dictionariesLoading) {
            fetchMatchedOffers();
        }
    }, [isAuthenticated, preferences, fullDictionaries, dictionariesLoading, fetchMatchedOffers]);

    if (!isAuthenticated) {
        return (
            <div className={listStyles["offer-list-view"]}>
                <div className="p-6 text-center text-gray-500">
                    {t("pleaseLoginToSeeMatched") || "Zaloguj się aby zobaczyć dopasowane oferty"}
                </div>
            </div>
        );
    }

    if (!preferences) {
        return (
            <div className={listStyles["offer-list-view"]}>
                <div className="p-6 text-center text-gray-500">
                    {t("completeProfileToSeeMatched") || "Uzupełnij swój profil aby zobaczyć dopasowane oferty"}
                </div>
            </div>
        );
    }

    return (
        <div className={listStyles["offer-list-view"]}>
            <div className="p-6">
                <div className="flex justify-between items-center mb-6">
                    <h2 className="text-2xl font-bold">
                        {t("matchedForYou") || "Dopasowane dla Ciebie"}
                    </h2>
                    <Button
                        onClick={fetchMatchedOffers}
                        disabled={loading}
                        className={`${buttonStyle["main-button"]} flex items-center gap-2`}
                    >
                        <RefreshCw size={18} className={loading ? 'animate-spin' : ''} />
                        {loading ? t("loading") || "Ładowanie..." : t("refresh") || "Odśwież"}
                    </Button>
                </div>

                {error && (
                    <div className="mb-6 p-4 bg-red-500/10 border border-red-500/50 rounded-lg text-red-400 text-center">
                        {error}
                    </div>
                )}

                {loading ? (
                    <div className={listStyles["list-with-filter"]}>
                        {Array.from({ length: 5 }).map((_, i) => (
                            <ListElementSkeleton key={i} />
                        ))}
                    </div>
                ) : matchedOffers.length > 0 ? (
                    <div className={listStyles["list-with-filter"]}>
                        {matchedOffers.map((offer) => (
                            <ListElement 
                                key={offer.id} 
                                offer={offer} 
                                score={getOfferScore(offer)}
                            />
                        ))}
                    </div>
                ) : (
                    <div className="p-6 text-center text-gray-500">
                        {t("noOffersMatched") || "Brak ofert pasujących do Twoich preferencji"}
                    </div>
                )}
            </div>
        </div>
    );
}