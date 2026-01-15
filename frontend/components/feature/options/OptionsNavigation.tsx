import { useUser } from '@/store/userContext';
import sideMenuStyles from '@/styles/SideMenuStyle.module.css'
import { CARD_TYPES, CardType } from '@/types/options/cardTypes';
import { useRouter } from 'next/navigation';
import { useTranslation } from 'react-i18next';

export type sideMenuProps = {
    activeCard: CardType
}
type SideMenuItem = {
  id: CardType;
  label: string;
  icon: string;
  onClick: (card: CardType) => void
};

export function OptionsNavigation({ activeCard }: sideMenuProps) {
    const {isAuthenticated, logout, loading} = useUser();
    const {t} = useTranslation("navigation");
    const openOptions = (card: CardType) => {
    if (!isAuthenticated) {
    router.push("/login");
  } else {
    router.push(`/options?card=${card}`);
  }
  };
  const handleLogout = async () => {
    await logout();
    router.push("/");
  }
    const SIDE_MENU_ITEMS: SideMenuItem[] = [
  {
    id: CARD_TYPES[0],
    label: t("userInfo"),
    icon: "/icons/user1.svg",
    onClick: () => {openOptions('UserInfo')}
  },
  {
    id: CARD_TYPES[1],
    label: t("matchedForYou"),
    icon: "/icons/check-circle0.svg",
    onClick: () => {openOptions('MatchedForYou')}
  },
  {
    id: CARD_TYPES[2],
    label: t("applicationHistory"),
    icon: "/icons/briefcase0.svg",
    onClick: () => {openOptions('ApplicationHistory')}
  },
  {
    id: CARD_TYPES[3],
    label: t("settings"),
    icon: "/icons/sliders0.svg",
    onClick: () => {openOptions('Settings')}
  }
];
    const router = useRouter();
  return (
    
    <div className={sideMenuStyles["navigation"]}>
      {SIDE_MENU_ITEMS.map((item) => {
        const isSelected = item.id === activeCard;

        return (
          <div
            onClick={() => item.onClick(item.id)}
            key={item.id}
            className={`${sideMenuStyles["pofile-nav"]} ${
              isSelected ? sideMenuStyles["selected"] : ""
            }`}
          >
            <img
              className={sideMenuStyles["user2"]}
              src={item.icon}
            />
            <div className={sideMenuStyles["side-menu-item"]}>
              {item.label}
            </div>
          </div>
        );
      })}
      <div
            className={sideMenuStyles["pofile-nav"]}
            onClick={loading ? undefined : handleLogout}
          >
            <img className={sideMenuStyles["user2"]} src="/icons/log-in0.svg" />
            <div className={sideMenuStyles["side-menu-item"]}>
              {t("logout")}
            </div>
          </div>
    </div>
  );
}