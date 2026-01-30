export type ApiResponse<T> = {
  data: T | null;
  error: string | null;
};

export class ApiClient {
  constructor(private baseUrl: string = "/api") {}

  private async request<T>(
    path: string,
    options: RequestInit = {},
    timeout = 1000000
  ): Promise<ApiResponse<T>> {
    const controller = new AbortController();
    const id = setTimeout(() => controller.abort(), timeout);

    try {
      const normalizedPath = path.startsWith('/') && this.baseUrl.endsWith('/') ? path.substring(1) : path;
      const res = await fetch(`${this.baseUrl}${normalizedPath}`, {
        ...options,
        headers: { "Content-Type": "application/json", ...options.headers },
        signal: controller.signal,
      });
      clearTimeout(id);

      if (!res.ok) {
        const text = await res.text();
        return { data: null, error: `API error: ${res.status} ${text}` };
      }

      const data: T = await res.json();
      return { data, error: null };
    } catch (err: unknown) {
      clearTimeout(id);
      const errName = (err as any)?.name;
      if (errName === "AbortError") return { data: null, error: "Request timeout" };
      if (err instanceof Error) return { data: null, error: err.message };
      return { data: null, error: "Unknown error" };
    }
  }

  get<T>(path: string, options?: RequestInit) {
    return this.request<T>(path, { ...options, method: "GET" });
  }

  post<T, B = Record<string, unknown>>(path: string, body: B, options?: RequestInit) {
    return this.request<T>(path, { ...options, method: "POST", body: JSON.stringify(body) });
  }

  put<T, B = Record<string, unknown>>(path: string, body: B, options?: RequestInit) {
    return this.request<T>(path, { ...options, method: "PUT", body: JSON.stringify(body) });
  }

  delete<T>(path: string, options?: RequestInit) {
    return this.request<T>(path, { ...options, method: "DELETE" });
  }
}

// eksport singletona
export const apiClient = new ApiClient(process.env.NEXT_PUBLIC_API_URL);
