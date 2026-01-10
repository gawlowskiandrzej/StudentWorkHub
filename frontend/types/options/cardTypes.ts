export const CARD_TYPES = [
  "UserInfo",
  "MatchedForYou",
  "ApplicationHistory",
  "Settings",
] as const;

export type CardType = (typeof CARD_TYPES)[number];

export const isCardType = (value: unknown): value is CardType =>
  typeof value === "string" && CARD_TYPES.includes(value as CardType);