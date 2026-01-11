"use client";
import { ArrowLeft, ChevronRight } from "lucide-react"
import ProgressBar from "@/components/feature/details/Progress";
import { useParams, useRouter } from 'next/navigation';
import offersJson from '@/store/data/DummyOffers.json';
import { formatSalaryValue } from "@/utils/salary/formatSalary";
import { calculateProgress } from "@/utils/date/dateProgress";
import { SkillsSection } from "@/components/feature/details/SkillSection";
import { LanguageSection } from "@/components/feature/details/LanguagesSection";
import { EducationSection } from "@/components/feature/details/EducationSection";
import { BenefitsSection } from "@/components/feature/details/BenefitsSection";
import { InfoSection } from "@/components/feature/details/InfoSection";
import detailsStyles from '../../../../styles/OfferDetails.module.css';
import buttonStyles from '../../../../styles/ButtonStyle.module.css';
import { useTranslation } from "react-i18next";
import { useOffers } from "@/store/OffersContext";

export default function DetailsPage() {
    const params = useParams();
    const id = params.id as string;
    const router = useRouter();
    const { offersResponse, loading, error } = useOffers();
    const { t } = useTranslation("details");
    function goBackToListView() {
        router.back();
    };
    const offer = offersResponse?.pagination.items.find(
        (item) => item.id === Number(id)
    );
    if (!offer) {
        return <div>Offer not found</div>;
    }
    const progress = calculateProgress(
        offer.dates.published,
        offer.dates.expires
    );
    const logoUrl = offer.company.logoUrl
        ? offer.source === 'Aplikujpl'
            ? `https://aplikuj.pl${offer.company.logoUrl}`
            : offer.company.logoUrl
        : '/icons/company0.svg';
    return (
        <div className={detailsStyles["offer-details-view"]}>
            <div className={detailsStyles["offer-details-view-navigation"]}>
                <div className={`${detailsStyles["go-back-button"]} ${detailsStyles["scale-hover"]}`}>
                    <div
                        className={`flex flex-row items-center self-stretch text-left justify-start cursor-pointer gap-3`}
                        onClick={() => goBackToListView()}
                    >
                        <ArrowLeft className={`secondary ${detailsStyles["chevron-left"]}`} /> {t("goBackButton")}
                    </div>
                </div>
                <div className={detailsStyles["categories-header"]}>
                    <div className={detailsStyles["leading-category"]}>{offer.category.leadingCategory}</div>
                    <ChevronRight />
                    {offer.category.subCategories && offer.category.subCategories?.length > 0 ? (
                        <div className={detailsStyles["subcategory-subcategory"]}>
                            {offer.category.subCategories?.join(" / ")}
                        </div>
                    ) : (
                        <div className={detailsStyles["subcategory-subcategory"]}>{t("noSubCategories")}</div>
                    )}
                </div>
            </div>

            <div className={detailsStyles["offer-details-view-content"]}>
                <div className={detailsStyles["general-details"]}>
                    <div className={detailsStyles["general-header"]}>
                        <div className={detailsStyles["companyWrapper"]}>
                            <img className={detailsStyles["company"]} src={logoUrl} />
                        </div>
                        <div className={detailsStyles["header-of-details"]}>
                            <div className={detailsStyles["job-main-info"]}>
                                <div className={detailsStyles["frame-53"]}>
                                    <div className={detailsStyles["job-title"]}>{offer.jobTitle}</div>
                                    <div className={detailsStyles["company-name"]}>{offer.company.name}</div>
                                </div>
                                <div className={detailsStyles["frame-73"]}>
                                    <div className={detailsStyles["_100-200-z"]}>
                                        {offer.salary.from != null && offer.salary.to != null && offer.salary.to != 0 && offer.salary.from != 0 && offer.salary.period != null 
                                            ? `${formatSalaryValue(offer.salary.from)} ${offer.salary.currency} - ${formatSalaryValue(offer.salary.to)} ${offer.salary.currency} / ${offer.salary.period}`
                                            : t("noSalary")}
                                    </div>
                                </div>
                            </div>
                            <ProgressBar
                                progress={progress}
                                expiresAt={new Date(offer.dates.expires)}
                            />
                        </div>
                    </div>

                    <div className={detailsStyles["line-6"]}></div>

                    <InfoSection offer={offer} />
                    <SkillsSection offerSkills={offer.requirements.skills ?? []} />
                    <LanguageSection languages={offer.requirements.languages ?? []} />
                    <EducationSection educations={offer.requirements.education} />
                    <BenefitsSection benefits={offer.benefits} />

                    <div
                        className={buttonStyles["main-button"]}
                        onClick={() => window.open(offer.url, "_blank", "noopener,noreferrer")}
                    >
                        <div className={buttonStyles["find-mathing-job"]}>{t("goToOfferPage")}</div>
                    </div>
                </div>

                <div className={detailsStyles["right-column-details"]}>
                    <div
                        className={buttonStyles["main-button"]}
                        onClick={() => window.open(offer.url, "_blank", "noopener,noreferrer")}
                    >
                        <div className={buttonStyles["find-mathing-job"]}>{t("goToOfferPage")}</div>
                    </div>
                </div>
            </div>
        </div>
    );
}