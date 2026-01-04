export function parseDateTimeSpecial(dateTime: string): Date {
  const [date, time] = dateTime.split(' ');
  const [year, month, day] = date.split('-').map(Number);
  const [hour, minute, second] = time.split(':').map(Number);

  return new Date(year, month - 1, day, hour, minute, second);
}
export function parseDateTime(dateTime: string): Date{
  return new Date(dateTime)
}
