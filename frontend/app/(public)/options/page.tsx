"use client";
import { ApplicationHistory } from '@/components/feature/options/ApplicationHistory';
import { MatchedForYou } from '@/components/feature/options/MatchedForYou';
import { OptionsNavigation } from '@/components/feature/options/OptionsNavigation';
import { Settings } from '@/components/feature/options/Settings';
import { UserInfoCard } from '@/components/feature/options/UserInfoCard';
import OptionsStyles from '@/styles/OptionStyle.module.css'
import { CARD_TYPES, CardType, isCardType } from '@/types/options/cardTypes';
import { useSearchParams } from 'next/navigation';
import { useMemo, Suspense } from 'react';
import { useTranslation } from 'react-i18next';

type NavigationKeys = "register" | "login" | "userInfo" | "matchedForYou" | "applicationHistory" | "settings";
export const cardTranslationKey: Record<CardType, NavigationKeys> = {
  UserInfo: "userInfo",
  MatchedForYou: "matchedForYou",
  ApplicationHistory: "applicationHistory",
  Settings: "settings",
};

function OptionsContent() {
  const { t } = useTranslation('navigation');
  const searchParams = useSearchParams();
  const param = searchParams.get('card') as string | null;

  const activeCard: CardType = useMemo(() => 
    isCardType(param) ? param : CARD_TYPES[0], 
    [param]
  );

  return (
    <div className={OptionsStyles["frame-174"]}>
      <OptionsNavigation activeCard={activeCard}/>
      <div className={OptionsStyles["card"]}>
        <div className={OptionsStyles["card-name"]}>
          {t(cardTranslationKey[activeCard])}
        </div>
        {activeCard === 'UserInfo' && <UserInfoCard />}
        {activeCard === 'MatchedForYou' && <MatchedForYou />}
        {activeCard === 'ApplicationHistory' && <ApplicationHistory />}
        {activeCard === 'Settings' && <Settings />}
      </div>
    </div>
  );
}

export default function Options() {
  return (
    <Suspense fallback={<div>Loading...</div>}>
      <OptionsContent />
    </Suspense>
  );
}