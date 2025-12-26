"use client";
import { ArrowLeft, ChevronRight } from "lucide-react"
import "../../../../styles/OfferDetails.css";
import "../../../../styles/ButtonStyle.css";
import ProgressBar from "@/components/feature/details/Progress";
import { useParams, useRouter } from 'next/navigation';
import offersJson from '@/store/data/DummyOffers.json';
import { formatSalaryValue } from "@/utils/salary/formatSalary";
import { calculateProgress } from "@/utils/date/dateProgress";
import { SkillsSection } from "@/components/feature/details/SkillSection";
import { LanguageSection } from "@/components/feature/details/LanguagesSection";
import { EducationSection } from "@/components/feature/details/EducationSection";
import { BenefitsSection } from "@/components/feature/details/BenefitsSection";


export default function DetailsPage() {
    const params = useParams();
    const id = params.id as string;
    const router = useRouter();
    function goBackToListView() {
        router.push('/list');
    };
    const offer = offersJson.pagination.items.find(
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
        <div className="offer-details-view">
            <div className="offer-details-view-navigation">
                <div className="go-back-button scale-hover">
                    <div className="flex flex-row items-center self-stretch text-left justify-start cursor-pointer gap-3" onClick={() => goBackToListView()}><ArrowLeft className="secondary chevron-left" /> Go to list</div>
                </div>
                <div className="categories-header">
                    <div className="leading-category">{offer.category.leadingCategory}</div>
                    <ChevronRight />
                    {offer.category.subCategories ? <div className="subcategory-subcategory">{offer.category.subCategories?.join(' / ')}</div> : <div className="subcategory-subcategory">brak podkategorii</div>}
                </div>
            </div>
            <div className="offer-details-view-content">
                <div className="general-details">
                    <div className="general-header">
                        <div className="companyWrapper">
                            <img className="company" src={logoUrl} />
                        </div>
                        <div className="header-of-details">
                            <div className="job-main-info">
                                <div className="frame-53">
                                    <div className="job-title">{offer.jobTitle}</div>
                                    <div className="company-name">{offer.company.name}</div>
                                </div>
                                <div className="frame-73">
                                    <div className="_100-200-z">{offer.salary.from != null && offer.salary.to != null
                                        ? `${formatSalaryValue(offer.salary.from)} ${offer.salary.currency} - ${formatSalaryValue(offer.salary.to)} ${offer.salary.currency}`
                                        : 'Nie zdefiniowano'}</div>
                                </div>
                            </div>
                            <ProgressBar
                                progress={progress}
                                expiresAt={new Date(offer.dates.expires)}
                            />
                        </div>
                    </div>
                    <div className="line-6"></div>
                    <div className="skill-section-content">
                        <div className="skill-section-row">
                            <div className="skill-section-item">
                                <img className="map-pin" src="/icons/light/map-pinLight.svg" />
                                <div className="city-name">
                                    {offer.location.street
                                        ? `${offer.location.street}, ${offer.location.postalCode ? offer.location.postalCode : "Nie wykryto kodu pocztowego"}`
                                        : "Nie zdefiniowano adresu"}
                                </div>
                            </div>
                            <div className="skill-section-item2">
                                <img className="map-pin2" src="/icons/flag0.svg" />
                                <div className="city-name">{offer.isForUkrainians == true ? "Również dla ukaińców" : "Praca przeznaczona dla Polaków"}</div>
                            </div>
                            <div className="skill-section-item3">
                                <img className="file-text" src="/icons/light/file-textLight.svg" />
                                <div className="city-name">{offer.category.leadingCategory}</div>
                            </div>
                        </div>
                        <div className="skill-section-row">
                            <div className="skill-section-item">
                                <img className="briefcase" src="/icons/light/briefcaseLight.svg" />
                                <div className="city-name">{offer.employment.types.join(' / ')}</div>
                            </div>
                            <div className="skill-section-item2">
                                <img className="map-pin3" src="/icons/cast0.svg" />
                                <div className="city-name">
                                    {offer.location.isRemote
                                        ? "Możliwa praca zdalna"
                                        : offer.location.isHybrid
                                            ? "Możliwa praca hybrydowa"
                                            : "Praca tylko stacjonarnie"}
                                </div>

                            </div>
                            <div className="skill-section-item3">
                                <img className="home" src="/icons/light/homeLight.svg" />
                                <div className="city-name">{offer.location.city ? offer.location.city : "Nie zdefiniowano miasta"}</div>
                            </div>
                        </div>
                        <div className="skill-section-row">
                            <div className="skill-section-item">
                                <img className="pie-chart" src="/icons/light/pie-chartLight.svg" />
                                <div className="city-name">{offer.employment.schedules ? offer.employment.schedules?.join(' / ') : "Nie zdefiniowano godzin pracy"}</div>
                            </div>
                            <div className="skill-section-item2">
                                <img className="map-pin4" src="/icons/linkedin0.svg" />
                                <div className="city-name">{offer.source}</div>
                            </div>
                            <div className="skill-section-item3">
                                <img className="dollar-sign" src="/icons/light/dollar-signLight.svg" />
                                <div className="city-name">{offer.salary.period ? offer.salary.period : "Nie zdefiniowno czasu wynagrodzenia"}</div>
                            </div>
                        </div>
                    </div>
                    <SkillsSection offerSkills={offer.requirements.skills}/>
                    <LanguageSection languages={offer.requirements.languages}/>
                    <EducationSection educations={offer.requirements.education}/>
                    <BenefitsSection benefits={offer.benefits}/>
                    <div
                        className="main-button"
                        onClick={() => window.open(offer.url, "_blank", "noopener,noreferrer")}
                    >
                        <div className="find-mathing-job">Go to offer page</div>
                    </div>
                    {/* <div className="line-5"></div> */}
                </div>
                <div className="right-column-details">
                    <div
                        className="main-button"
                        onClick={() => window.open(offer.url, "_blank", "noopener,noreferrer")}
                    >
                        <div className="find-mathing-job">Go to offer page</div>
                    </div>
                </div>
            </div>
        </div>
    );
}