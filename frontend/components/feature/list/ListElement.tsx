"use client";
import { Progress } from '@/components/ui/progress';
import { useRouter } from 'next/navigation';
import { offer } from '../../../types/list/Offer/offer';
import { calculateProgress } from '@/utils/date/dateProgress';
import { formatDateTime } from '@/utils/date/dateFormatter';
import { formatSalaryValue } from '@/utils/salary/formatSalary';

export function ListElement({ offer }: { offer: offer }) {
    const router = useRouter();
    function goToDetails(id: number): void {
        router.push('/details/' + id);
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
        <div className="offer-list-header cursor-pointer scale-hover" onClick={() => goToDetails(offer.id)}>
            <div className='companyWrapper'>
                <img className="company" src={`${logoUrl}`} />
            </div>
            <div className="offer-header">
                <div className="offer-header-info">
                    <div className="offer-header-content">
                        <div className="offer-header2">
                            <div className="job-title">{offer.jobTitle}</div>
                            <div className="company-name">{offer.company.name}</div>
                        </div>
                        <div className="offer-header-desc">
                            <div className="job-header-sub-item">
                                <img className="vector5" src="/icons/location.svg" />
                                <div className="address">{offer.location.city}</div>
                            </div>
                            <div className="job-header-sub-item">
                                <img className="vector5" src="/icons/house.svg" />
                                <div className="employment-type">{offer.employment.types?.join(' / ')}</div>
                            </div>
                            <div className="job-header-sub-item">
                                <img className="vector5" src="/icons/document.svg" />
                                <div className="employment-schedules">{offer.employment.schedules?.join(' / ')}</div>
                            </div>
                        </div>
                    </div>
                    <div className="offer-header-salary-date">
                        <div className="job-salary">
                            {offer.salary.from != null && offer.salary.to != null
                                ? `${formatSalaryValue(offer.salary.from)} ${offer.salary.currency} - ${formatSalaryValue(offer.salary.to)} ${offer.salary.currency}`
                                : 'Nie zdefiniowano'}
                        </div>
                        <div className="added-10-12-2025 flex gap-5"><div>{formatDateTime(offer.dates.published)}</div><Progress className='w-[100px]' value={progress} max={100} /><div>{formatDateTime(offer.dates.expires)}</div></div>
                    </div>
                </div>
            </div>
        </div>
    );
}