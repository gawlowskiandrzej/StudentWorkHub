"use client";
import { useState, useEffect } from "react";
import profileCreationStyles from '@/styles/ProfileCreationStyle.module.css';
import { Progress } from "@/components/ui/progress";
import { Button } from "@/components/ui/button";
import { ArrowLeft, ArrowRight, Settings } from "lucide-react";
import { STEPS } from "@/constants/profileCreation/steps";
import { CheckCircle2 } from "lucide-react";
import { useTranslation } from "react-i18next";
import { UserApi } from "@/lib/api/controllers/user";
import { useRouter } from "next/navigation";
import { useUser } from "@/store/userContext";
import { useProfileCreationDictionaries } from "@/hooks/useDictionaries";


export default function CreateProfile() {

    const { t } = useTranslation("profileCreation");
    const router = useRouter();
    const { jwt, isAuthenticated, loading } = useUser();
    const { fullDictionaries } = useProfileCreationDictionaries();

    const [index, setIndex] = useState(0);
    const [isSubmitting, setIsSubmitting] = useState(false);
    const [error, setError] = useState<string | null>(null);

    // Validate authentication on mount and redirect if not logged in
    useEffect(() => {
        if (!loading && !isAuthenticated) {
            router.push("/login");
        }
    }, [isAuthenticated, loading, router]);

    const [formData, setFormData] = useState({
        major: null as number | null,
        skills: [] as Array<{ id: string; name: string; months: string }>,
        earnings: { isMonthly: true, min: 2000, max: 5000 },
        contracts: [] as number[],
        languages: { 
            native: null as number | null, 
            others: [] as Array<{ id: string; languageId: number | null; levelId: number | null }>
        },
        location: { city: "", workplaceTypes: [] as string[] },
        jobStatus: "actively_looking" as string
    });

    const updateData = (key: string, value: any) => {
        setFormData((prev: any) => {
            if (typeof value === 'object' && value !== null && !Array.isArray(value) && typeof prev[key] === 'object') {
                return {
                    ...prev,
                    [key]: { ...prev[key], ...value }
                };
            }
            return { ...prev, [key]: value };
        });
    };

    const handleNext = () => {
        if (index < STEPS.length - 1) {
            setIndex(index + 1);
        } else {
            handleFinish();
        }
    }

    const handlePrev = () => {
        if (index > 0) setIndex(index - 1);
    }

    const handleFinish = async () => {
        if (!jwt) {
            setError("You must be logged in to save preferences");
            return;
        }

        setIsSubmitting(true);
        setError(null);

        try {
            // highest language level ID for native 
            const highestLevelId = fullDictionaries?.language_levels 
                ? Math.max(...fullDictionaries.language_levels.map(l => l.id))
                : undefined;

            // first main preferences with native language
            const response = await UserApi.updatePreferences({
                jwt,
                leadingCategoryId: formData.major || undefined,
                salaryFrom: formData.earnings.min || undefined,
                salaryTo: formData.earnings.max || undefined,
                employmentTypeIds: formData.contracts.length > 0 ? formData.contracts : undefined,
                jobStatusName: formData.jobStatus,
                cityName: formData.location.city || undefined,
                workTypeNames: formData.location.workplaceTypes.length > 0 ? formData.location.workplaceTypes : undefined,
                languageId: formData.languages.native || undefined,
                languageLevelId: formData.languages.native && highestLevelId ? highestLevelId : undefined,
                skillNames: formData.skills.map(s => s.name),
                skillMonths: formData.skills.map(s => parseInt(s.months) || 0)
            });

            if (response.error) {
                setError(response.error);
                setIsSubmitting(false);
                return;
            }

            // second add other languages if any
            const otherLanguages = formData.languages.others.filter(
                lang => lang.languageId && lang.levelId
            );

            if (otherLanguages.length > 0) {
                for (const language of otherLanguages) {
                    const langResponse = await UserApi.updatePreferences({
                        jwt,
                        languageId: language.languageId || undefined,
                        languageLevelId: language.levelId || undefined,
                    });

                    if (langResponse.error) {
                        setError(`Failed to add language: ${langResponse.error}`);
                        setIsSubmitting(false);
                        return;
                    }
                }
            }

            // success - redirect to options page
            router.push("/options");
        } catch (err) {
            setError(err instanceof Error ? err.message : "Failed to save preferences");
        } finally {
            setIsSubmitting(false);
        }
    };

    const CurrentStepComponent = STEPS[index].component;
    const progressValue = ((index + 1) / STEPS.length) * 100;
    const isLastStep = index === STEPS.length - 1;

    // Show loading state while checking authentication
    if (loading) {
        return (
            <div className={profileCreationStyles.container}>
                <header className={profileCreationStyles.header}>
                    <div className="bg-blue-500/10 w-16 h-16 rounded-full flex items-center justify-center mx-auto mb-1 animate-in zoom-in duration-500">
                        <Settings className="w-6 h-6 text-blue-500 animate-spin" />
                    </div>
                </header>
                <main className={profileCreationStyles.mainContent}>
                    <div className="text-center text-gray-400">
                        {t("loading") || "Loading..."}
                    </div>
                </main>
            </div>
        );
    }

    return (
        <div className={profileCreationStyles.container}>
            <header className={profileCreationStyles.header}>
                {index === STEPS.length - 1 ? (
                    <div className="bg-blue-500/10 w-16 h-16 rounded-full flex items-center justify-center mx-auto mb-1 animate-in zoom-in duration-500">
                        <CheckCircle2 className="w-16 h-16 text-blue-500" />
                    </div>
                ) : (
                    <div className="w-[80%] max-w-md mx-auto relative pt-10 pb-4">
                        <div
                            className="absolute top-0 transition-all duration-500 ease-out"
                            style={{
                                left: `${progressValue}%`,
                                transform: 'translateX(-50%)'
                            }}
                        >
                            <Settings className="w-6 h-6 text-blue-500 animate-spin" />
                        </div>
                        <Progress value={progressValue} className={profileCreationStyles.progressBar + " ease-in-out"} />
                    </div>
                )}
            </header>

            <main className={profileCreationStyles.mainContent}>
                {error && (
                    <div className="mb-6 p-4 bg-red-500/10 border border-red-500/50 rounded-lg text-red-400 text-center">
                        {error}
                    </div>
                )}
                <CurrentStepComponent
                    data={formData}
                    updateData={updateData}
                />
            </main>

            <footer className={profileCreationStyles.footer}>
                {index > 0 && (
                    <Button
                        onClick={handlePrev}
                        disabled={isSubmitting}
                        className={profileCreationStyles.prevButton}
                    >
                        <ArrowLeft className="mr-2" size={18} />
                        {t("previous")}
                    </Button>
                )}
                <Button
                    onClick={handleNext}
                    disabled={isSubmitting}
                    className={profileCreationStyles.nextButton}
                >
                    {isSubmitting ? t("sending") || "Wysyłanie..." : isLastStep ? t("finish") : t("next")}
                    {!isLastStep && !isSubmitting && <ArrowRight className="ml-2" size={18} />}
                </Button>
            </footer>
        </div>
    );
}