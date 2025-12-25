"use client";
import { ArrowLeft, ChevronRight } from "lucide-react"
import "../../../../styles/OfferDetails.css";
import "../../../../styles/ButtonStyle.css";
import ProgressBar from "@/components/feature/details/Progress";
import { useParams, useRouter } from 'next/navigation';
import offersJson from '@/store/data/DummyOffers.json';
import { formatSalaryValue } from "@/utils/salary/formatSalary";


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
    const logoUrl = offer.company.logoUrl
        ? offer.source === 'Aplikujpl'
            ? `https://aplikuj.pl${offer.company.logoUrl}`
            : offer.company.logoUrl
        : '/icons/company0.svg';
    return (
        <div className="offer-details-view">
            <div className="general-details">
                <div className="flex flex-col gap-10 items-center justify-start self-stretch">
                    <div className="flex flex-row items-center self-stretch text-left justify-start cursor-pointer scale-hover gap-5" onClick={() => goBackToListView()}><ArrowLeft className="primary chevron-left" /> Go back</div>
                    <div className="categories-header">
                        <div className="leading-category">leading category</div>
                        <ChevronRight />
                        <div className="subcategory-subcategory">subcategory / subcategory</div>
                    </div>
                </div>

                <div className="general-header">
                    <img className="company" src={logoUrl} />
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
                        <ProgressBar current={70} max={100} date={new Date()} />
                    </div>
                </div>
                <div className="line-1"></div>
                <div className="general-info-skills">
                    <div className="skill-section-content">
                        <div className="skill-section-row">
                            <div className="frame-163">
                                <img className="file-text" src="/icons/file-text0.svg" />
                                <div className="job-category">JobCategory</div>
                            </div>
                            <div className="frame-162">
                                <img className="file-text" src="/icons/map-pin0.svg" />
                                <div className="job-category">CityName</div>
                            </div>
                        </div>
                        <div className="skill-section-row">
                            <div className="frame-163">
                                <img className="file-text" src="/icons/house.svg" />
                                <div className="job-category">WorkPlace</div>
                            </div>
                            <div className="frame-162">
                                <img className="file-text" src="/icons/briefcase0.svg" />
                                <div className="job-category">WorkType</div>
                            </div>
                        </div>
                        <div className="skill-section-row">
                            <div className="frame-163">
                                <img className="file-text" src="/icons/dollar-sign0.svg" />
                                <div className="job-category">SalaryType</div>
                            </div>
                            <div className="frame-162">
                                <img className="file-text" src="/icons/pie-chart0.svg" />
                                <div className="job-category">WorkSchedule</div>
                            </div>
                        </div>
                    </div>
                </div>
                <div className="line-2"></div>
                <div className="required-skills-section">
                    <div className="required-skills">Required skills</div>
                    <div className="skill-section-content2">
                        <div className="skill-section-row">
                            <div className="skill-section-name">
                                <img className="file-text" src="/icons/check-circle0.svg" />
                                <div className="job-category">SkillName</div>
                            </div>
                            <div className="skill-section-months">
                                <img className="file-text" src="/icons/clock0.svg" />
                                <div className="job-category">SkillMonths</div>
                            </div>
                            <div className="skill-section-level">
                                <img className="file-text" src="/icons/award0.svg" />
                                <div className="job-category">SkillLevel</div>
                            </div>
                        </div>
                        <div className="skill-section-row">
                            <div className="skill-section-name">
                                <img className="file-text" src="/icons/check-circle0.svg" />
                                <div className="job-category">SkillName</div>
                            </div>
                            <div className="skill-section-months">
                                <img className="file-text" src="/icons/clock0.svg" />
                                <div className="job-category">SkillMonths</div>
                            </div>
                            <div className="skill-section-level">
                                <img className="file-text" src="/icons/award0.svg" />
                                <div className="job-category">SkillLevel</div>
                            </div>
                        </div>
                    </div>
                </div>
                <div className="line-3"></div>
                <div className="required-skills-section">
                    <div className="required-skills">Required languages</div>
                    <div className="skill-section-content2">
                        <div className="skill-section-row">
                            <div className="skill-section-name">
                                <img className="file-text" src="/icons/file-text0.svg" />
                                <div className="job-category">LanguageLevel</div>
                            </div>
                            <div className="skill-section-level">
                                <img className="file-text" src="/icons/award0.svg" />
                                <div className="job-category">LanguageLevel</div>
                            </div>
                        </div>
                        <div className="skill-language-row">
                            <div className="skill-section-name">
                                <img className="file-text" src="/icons/file-text0.svg" />
                                <div className="job-category">LanguageLevel</div>
                            </div>
                            <div className="skill-section-level">
                                <img className="file-text" src="/icons/award0.svg" />
                                <div className="job-category">LanguageLevel</div>
                            </div>
                        </div>
                    </div>
                </div>
                <div className="line-4"></div>
                <div className="required-skills-section">
                    <div className="required-skills">Required languages</div>
                    <div className="skill-section-content2">
                        <div className="skill-section-row">
                            <div className="skill-section-name">
                                <img className="file-text" src="/icons/gift0.svg" />
                                <div className="job-category">BenefitName</div>
                            </div>
                            <div className="skill-section-level">
                                <img className="file-text" src="/icons/gift0.svg" />
                                <div className="job-category">BenefitName</div>
                            </div>
                        </div>
                        <div className="skill-language-row">
                            <div className="skill-section-name">
                                <img className="file-text" src="/icons/gift0.svg" />
                                <div className="job-category">BenefitName</div>
                            </div>
                            <div className="skill-section-level">
                                <img className="file-text" src="/icons/gift0.svg" />
                                <div className="job-category">BenefitName</div>
                            </div>
                        </div>
                    </div>
                </div>
                <div className="line-5"></div>
                <div className="apply-for-offer-button">
                    <div className="main-button">
                        <div className="find-mathing-job">Go to offer page</div>
                    </div>
                </div>
            </div>
            <div className="frame-165">
                <div className="right-column-details">
                    <div className="main-button">
                        <div className="find-mathing-job">Go to offer page</div>
                    </div>
                    <div className="education-section">
                        <div className="required-education">Required education</div>
                        <div className="frame-85">
                            <div className="education-row">
                                <img className="education" src="/icons/education0.svg" />
                                <div className="job-category">Educ. level</div>
                                <img className="file-text" src="/icons/check-circle0.svg" />
                            </div>
                            <div className="education-row">
                                <img className="education" src="/icons/education0.svg" />
                                <div className="job-category">Educ. level</div>
                                <img className="file-text" src="/icons/check-circle0.svg" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    );
}