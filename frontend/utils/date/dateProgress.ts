export function calculateProgress(
  published: string,
  expires: string
): number {
  const publishedDate = new Date(published).getTime();
  const expiresDate = new Date(expires).getTime();
  const now = Date.now();

  if (now <= publishedDate) return 0;
  if (now >= expiresDate) return 100;

  const total = expiresDate - publishedDate;
  const elapsed = now - publishedDate;

  return Math.round((elapsed / total) * 100);
}

export function daysLeft(expires: string): number {
  const expiresDate = new Date(expires).getTime();
  const now = Date.now();

  return Math.max(
    Math.ceil((expiresDate - now) / (1000 * 60 * 60 * 24)),
    0
  );
}