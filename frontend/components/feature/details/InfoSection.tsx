import { offer } from "@/types/list/Offer/offer";

type infoSection = {
    offer: offer
}
export function InfoSection({ offer }: infoSection) {
    return (
        <div className="skill-section-content">
            <div className="skill-section-row">
                <div className="skill-section-item1">
                    <img className="file-text" src="/icons/light/file-textLight.svg" />
                    <div className="city-name">{offer.category.leadingCategory}</div>
                </div>
                <div className="skill-section-item2">
                    <img className="map-pin" src="/icons/light/map-pinLight.svg" />
                    <div className="city-name">
                        {offer.location.street
                            ? `${offer.location.street}, ${offer.location.postalCode ? offer.location.postalCode : "Nie wykryto kodu pocztowego"}`
                            : "Nie zdefiniowano adresu"}
                    </div>
                </div>
                <div className="skill-section-item3">
                    <img className="map-pin2" src="/icons/flag0.svg" />
                    <div className="city-name">{offer.isForUkrainians == true ? "Również dla ukaińców" : "Praca przeznaczona dla Polaków"}</div>
                </div>

            </div>
            <div className="skill-section-row">
                <div className="skill-section-item1">
                    <img className="briefcase" src="/icons/light/briefcaseLight.svg" />
                    <div className="city-name">{offer.employment.types?.join(' / ')}</div>
                </div>

                <div className="skill-section-item2">
                    <img className="home" src="/icons/light/homeLight.svg" />
                    <div className="city-name">{offer.location.city ? offer.location.city : "Nie zdefiniowano miasta"}</div>
                </div>
                <div className="skill-section-item3">
                    <img className="map-pin3" src="/icons/cast0.svg" />
                    <div className="city-name">
                        {offer.location.isRemote
                            ? "Możliwa praca zdalna"
                            : offer.location.isHybrid
                                ? "Możliwa praca hybrydowa"
                                : "Praca tylko stacjonarnie"}
                    </div>

                </div>
            </div>
            <div className="skill-section-row">
                <div className="skill-section-item1">
                    <img className="pie-chart" src="/icons/light/pie-chartLight.svg" />
                    <div className="city-name">{offer.employment.schedules ? offer.employment.schedules?.join(' / ') : "Nie zdefiniowano godzin pracy"}</div>
                </div>
                <div className="skill-section-item2">
                    <img className="dollar-sign" src="/icons/light/dollar-signLight.svg" />
                    <div className="city-name">{offer.salary.period ? offer.salary.period : "Nie zdefiniowno czasu wynagrodzenia"}</div>
                </div>
                <div className="skill-section-item3">
                    <img className="map-pin4" src="/icons/linkedin0.svg" />
                    <div className="city-name">{offer.source}</div>
                </div>
            </div>
        </div>
    );
}