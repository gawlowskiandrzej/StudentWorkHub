import { OptionsNavigation } from '@/components/feature/options/OptionsNavigation';
import { UserInfoCard } from '@/components/feature/options/UserInfoCard';
import OptionsStyles from '@/styles/OptionStyle.module.css'

export default function Options() {
    return (
        <div className={OptionsStyles["frame-174"]}>
            <OptionsNavigation/>
            <div className={OptionsStyles["card"]}>
                <div className={OptionsStyles["card-name"]}>Card name</div>
                <UserInfoCard/>
            </div>
        </div>
    );
}