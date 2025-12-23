"use client";
import { Progress } from '@/components/ui/progress';
import { useRouter } from 'next/navigation';
export function ListElement({key}: {key: number}) {
    const router = useRouter();
    function goToDetails(key: number): void {
        router.push('/details/' + key);
    }

    return (
            <div className="offer-list-header cursor-pointer scale-hover" onClick={() => goToDetails(key)}>
                <img className="company" src="/icons/company0.svg" />
                <div className="offer-header">
                    <div className="offer-header-info">
                    <div className="offer-header-content">
                        <div className="offer-header2">
                            <div className="job-title">JobTitle</div>
                            <div className="company-name">Company name</div>
                        </div>
                        <div className="offer-header-desc">
                            <div className="job-header-sub-item">
                                <img className="vector5" src="/icons/location.svg" />
                                <div className="address">Address</div>
                            </div>
                            <div className="job-header-sub-item">
                                <img className="vector5" src="/icons/house.svg" />
                                <div className="employment-type">Employment type</div>
                            </div>
                            <div className="job-header-sub-item">
                                <img className="vector5" src="/icons/document.svg" />
                                <div className="employment-schedules">Employment schedules</div>
                            </div>
                        </div>
                    </div>
                    <div className="offer-header-salary-date">
                        <div className="job-salary">Job salary</div>
                        <div className="added-10-12-2025 flex gap-5"><div>10.12.2025</div><Progress className='w-[100px]' value={10} max={100} /><div>12.12.2025</div></div>
                    </div>
                    </div>
                </div>
            </div>
    );
}