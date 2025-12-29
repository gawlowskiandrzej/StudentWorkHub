import { offer } from "@/types/list/Offer/offer";
import detailsStyles from "../../../styles/OfferDetails.module.css"

type infoSection = {
    offer: offer
}
export function InfoSection({ offer }: infoSection) {
    return (
        <div className={detailsStyles["skill-section-content"]}>
            <div className={detailsStyles["skill-section-row"]}>
                <div className={detailsStyles["skill-section-item1"]}>
                    <img className={detailsStyles["file-text"]} src="/icons/light/file-textLight.svg" />
                    <div className={detailsStyles["city-name"]}>{offer.category.leadingCategory}</div>
                </div>
                <div className={detailsStyles["skill-section-item2"]}>
                    <img className={detailsStyles["map-pin"]} src="/icons/light/map-pinLight.svg" />
                    <div className={detailsStyles["city-name"]}>
                        {offer.location.street
                            ? `${offer.location.street}, ${offer.location.postalCode ? offer.location.postalCode : "Nie wykryto kodu pocztowego"}`
                            : "Nie zdefiniowano adresu"}
                    </div>
                </div>
                <div className={detailsStyles["skill-section-item3"]}>
                    <img className={detailsStyles["map-pin2"]} src="/icons/flag0.svg" />
                    <div className={detailsStyles["city-name"]}>
                        {offer.isForUkrainians == true ? "Również dla ukaińców" : "Praca przeznaczona dla Polaków"}
                    </div>
                </div>
            </div>

            <div className={detailsStyles["skill-section-row"]}>
                <div className={detailsStyles["skill-section-item1"]}>
                    <img className={detailsStyles["briefcase"]} src="/icons/light/briefcaseLight.svg" />
                    <div className={detailsStyles["city-name"]}>{offer.employment.types?.join(" / ")}</div>
                </div>

                <div className={detailsStyles["skill-section-item2"]}>
                    <img className={detailsStyles["home"]} src="/icons/light/homeLight.svg" />
                    <div className={detailsStyles["city-name"]}>
                        {offer.location.city ? offer.location.city : "Nie zdefiniowano miasta"}
                    </div>
                </div>
                <div className={detailsStyles["skill-section-item3"]}>
                    <img className={detailsStyles["map-pin3"]} src="/icons/cast0.svg" />
                    <div className={detailsStyles["city-name"]}>
                        {offer.location.isRemote
                            ? "Możliwa praca zdalna"
                            : offer.location.isHybrid
                                ? "Możliwa praca hybrydowa"
                                : "Praca tylko stacjonarnie"}
                    </div>
                </div>
            </div>

            <div className={detailsStyles["skill-section-row"]}>
                <div className={detailsStyles["skill-section-item1"]}>
                    <img className={detailsStyles["pie-chart"]} src="/icons/light/pie-chartLight.svg" />
                    <div className={detailsStyles["city-name"]}>
                        {offer.employment.schedules
                            ? offer.employment.schedules?.join(" / ")
                            : "Nie zdefiniowano godzin pracy"}
                    </div>
                </div>
                <div className={detailsStyles["skill-section-item2"]}>
                    <img className={detailsStyles["dollar-sign"]} src="/icons/light/dollar-signLight.svg" />
                    <div className={detailsStyles["city-name"]}>
                        {offer.salary.period ? offer.salary.period : "Nie zdefiniowno czasu wynagrodzenia"}
                    </div>
                </div>
                <div className={detailsStyles["skill-section-item3"]}>
                    <img className={detailsStyles["map-pin4"]} src="/icons/linkedin0.svg" />
                    <div className={detailsStyles["city-name"]}>{offer.source}</div>
                </div>
            </div>
        </div>

    );
}