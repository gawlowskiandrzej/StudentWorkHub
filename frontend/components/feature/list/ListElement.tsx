"use client";
import { useRef, useCallback } from 'react';
import { Progress } from '@/components/ui/progress';
import { useRouter } from 'next/navigation';
import { offer } from '../../../types/list/Offer/offer';
import { calculateProgress } from '@/utils/date/dateProgress';
import { formatDateTime } from '@/utils/date/dateFormatter';
import { formatSalaryValue } from '@/utils/salary/formatSalary';
import listStyles from "../../../styles/OfferList.module.css";
import buttonStyles from "../../../styles/ButtonStyle.module.css";
import Link from 'next/link';
import { ShowMoreLikeButton } from './ShowMoreLikeButton';
import { useRanking } from '@/store/RankingContext';

interface ListElementProps {
    offer: offer;
    showRankingButton?: boolean;
    score?: number;
}

export function ListElement({ offer, showRankingButton = true, score }: ListElementProps) {
    const router = useRouter();
    const { recordInteraction, showMoreLikeThis } = useRanking();
    
    // Hover tracking
    const hoverStartRef = useRef<number | null>(null);
    
    const handleMouseEnter = useCallback(() => {
        hoverStartRef.current = Date.now();
    }, []);
    
    const handleMouseLeave = useCallback(() => {
        if (hoverStartRef.current) {
            const duration = Date.now() - hoverStartRef.current;
            if (duration >= 300) { // Min 300ms hover
                recordInteraction(offer, 'hover', duration);
            }
            hoverStartRef.current = null;
        }
    }, [offer, recordInteraction]);
    
    const handleClick = useCallback(() => {
        recordInteraction(offer, 'click');
        router.push('/details/' + offer.id);
    }, [offer, recordInteraction, router]);
    
    const handleShowMoreLike = useCallback(() => {
        showMoreLikeThis(offer);
    }, [offer, showMoreLikeThis]);
    
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
        <div 
            className={`${listStyles["offer-list-header"]} ${buttonStyles["scale-hover"]} relative`}
            onMouseEnter={handleMouseEnter}
            onMouseLeave={handleMouseLeave}
        >
            <Link
                href={`/details/${offer.id}`}
                className="flex flex-1"
                onClick={handleClick}
            >
                <div className={listStyles['companyWrapper']}>
                    <img className={listStyles["company"]} src={`${logoUrl}`} />
                </div>
                <div className={listStyles["offer-header"]}>
                    <div className={listStyles["offer-header-info"]}>
                        <div className={listStyles["offer-header-content"]}>
                            <div className={listStyles["offer-header2"]}>
                                <div className={listStyles["job-title"]}>{offer.jobTitle}</div>
                                <div className={listStyles["company-name"]}>{offer.company.name}</div>
                            </div>
                            <div className={listStyles["offer-header-desc"]}>
                                <div className={listStyles["job-header-sub-item"]}>
                                    <img className={listStyles["vector5"]} src="/icons/location.svg" />
                                    <div className={listStyles["address"]}>{offer.location.city}</div>
                                </div>
                                <div className={listStyles["job-header-sub-item"]}>
                                    <img className={listStyles["vector5"]} src="/icons/house.svg" />
                                    <div className={listStyles["employment-type"]}>{offer.employment.types?.join(' / ')}</div>
                                </div>
                                <div className={listStyles["job-header-sub-item"]}>
                                    <img className={listStyles["vector5"]} src="/icons/document.svg" />
                                    <div className={listStyles["employment-schedules"]}>{offer.employment.schedules?.join(' / ')}</div>
                                </div>
                                <div className={listStyles["job-header-sub-item"]}>
                                    <img className={listStyles["vector5"]} src="/icons/linkedin0.svg" />
                                    <div className={listStyles["employment-schedules"]}>{offer.source}</div>
                                </div>
                            </div>
                        </div>
                        <div className={listStyles["offer-header-salary-date"]}>
                            <div className={listStyles["job-salary"]}>
                                {offer.salary.from != null && offer.salary.to != null && offer.salary.to != 0 && offer.salary.from != 0 && offer.salary.period != null 
                                    ? `${formatSalaryValue(offer.salary.from)} ${offer.salary.currency} - ${formatSalaryValue(offer.salary.to)} ${offer.salary.currency} / ${offer.salary.period}`
                                    : 'Nie zdefiniowano'}
                            </div>
                            <div className={`${listStyles["added-10-12-2025"]} flex gap-5`}>
                                <div>{formatDateTime(offer.dates.published)}</div>
                                <Progress className='w-[100px]' value={progress} max={100} />
                                <div>{formatDateTime(offer.dates.expires)}</div>
                            </div>
                        </div>
                    </div>
                </div>
            </Link>
            
            {/* Show More Like This Button */}
            {showRankingButton && (
                <div className="absolute top-12 right-2 z-10">
                    <ShowMoreLikeButton 
                        onClick={handleShowMoreLike}
                        size="sm"
                    />
                </div>
            )}
            
            {/* Match Score Indicator - user-friendly percentage display */}
            {score !== undefined && (
                <div 
                    className="absolute top-12 right-10 z-10 flex items-center gap-1.5 px-2.5 py-1 rounded-full text-xs font-semibold shadow-sm"
                    style={{ 
                        backgroundColor: getScoreColor(score),
                        color: 'white'
                    }}
                    title={`Dopasowanie do Twojego profilu: ${formatMatchPercent(score)}`}
                >
                    <svg width="12" height="12" viewBox="0 0 24 24" fill="currentColor">
                        <path d="M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm-2 15l-5-5 1.41-1.41L10 14.17l7.59-7.59L19 8l-9 9z"/>
                    </svg>
                    <span>{formatMatchPercent(score)}</span>
                </div>
            )}
        </div>
    );
}

function formatMatchPercent(score: number): string {
    const percent = Math.round(score * 100);
    return `${percent}%`;
}

function getScoreColor(score: number): string {
    if (score >= 0.8) return '#22c55e';
    if (score >= 0.6) return '#84cc16';
    if (score >= 0.4) return '#eab308';
    if (score >= 0.2) return '#f97316';
    return '#ef4444';
}