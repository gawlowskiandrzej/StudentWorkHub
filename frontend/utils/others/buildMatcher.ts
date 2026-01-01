import { skill } from "@/types/list/Offer/skill";

export function buildMatcher(input: string): (list: string[] | skill[]) => boolean {
  if (!input) return () => true;

  // OR grupy
  const orGroups = input.split("|").map(group =>
    group
      .split("&")
      .map(phrase => phrase.trim())
      .filter(Boolean)
  );

  // type guard: czy element jest typu skill
  const isSkill = (item: string | skill): item is skill =>
    typeof item === "object" && item !== null && "skill" in item;

  return (list: string[] | skill[]) => {
    if (!list || list.length === 0) return false;

    // zamiana listy na stringi lowercase
    const lowerList: string[] = list.map(item => {
      if (typeof item === "string") return item.toLowerCase();
      if (isSkill(item)) return (item.skill ?? "").toLowerCase();
      return "";
    });

    return orGroups.some(andGroup =>
      // AND: każda fraza musi pasować do przynajmniej jednego elementu listy
      andGroup.every(phrase =>
        lowerList.some(item => item.includes(phrase.toLowerCase()))
      )
    );
  };
}
