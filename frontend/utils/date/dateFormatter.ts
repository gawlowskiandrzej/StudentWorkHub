import { parseDateTime } from "./dateParser";

export function formatDateTime(dateTime: string): string {
  const date = parseDateTime(dateTime);

  return date.toLocaleDateString('pl-PL', {
    day: '2-digit',
    month: '2-digit',
    year: 'numeric',
  });
}