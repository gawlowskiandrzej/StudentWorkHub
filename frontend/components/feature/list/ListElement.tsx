"use client";
import { Progress } from '@/components/ui/progress';
import { useRouter } from 'next/navigation';
import { offer } from '../../../types/list/Offer/offer';
import { calculateProgress } from '@/utils/date/dateProgress';
import { formatDateTime } from '@/utils/date/dateFormatter';
import { formatSalaryValue } from '@/utils/salary/formatSalary';
import listStyles from "../../../styles/OfferList.module.css";
import buttonStyles from "../../../styles/ButtonStyle.module.css";
import Link from 'next/link';

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
        <Link
         href={`/details/${offer.id}`}
         className={`${listStyles["offer-list-header"]} ${buttonStyles["scale-hover"]}`} onClick={() => goToDetails(offer.id)}>
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
                            {offer.salary.from != null && offer.salary.to != null
                                ? `${formatSalaryValue(offer.salary.from)} ${offer.salary.currency} - ${formatSalaryValue(offer.salary.to)} ${offer.salary.currency} / ${offer.salary.period}`
                                : 'Nie zdefiniowano'}
                        </div>
                        <div className={`${listStyles["added-10-12-2025"]} flex gap-5`}><div>{formatDateTime(offer.dates.published)}</div><Progress className='w-[100px]' value={progress} max={100} /><div>{formatDateTime(offer.dates.expires)}</div></div>
                    </div>
                </div>
            </div>
        </Link>
    );
}