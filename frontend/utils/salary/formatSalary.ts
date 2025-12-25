export function formatSalaryValue(value: number | string | null): string {
  if (value == null) return '';
  return Number(value).toLocaleString('pl-PL', { useGrouping: true }); // zawsze spacja jako separator tysięcy
}
