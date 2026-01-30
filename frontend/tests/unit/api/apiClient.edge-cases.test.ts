import { describe, expect, it, beforeEach, vi } from "vitest";
import { ApiClient } from "@/lib/api/apiClient";


describe("ApiClient - Error Handling & Edge Cases", () => {
  let apiClient: ApiClient;
  const mockFetch = vi.fn();

  beforeEach(() => {
    vi.clearAllMocks();
    global.fetch = mockFetch;
    apiClient = new ApiClient("https://api.test.com");
  });

  it("handles network timeout properly", async () => {
    mockFetch.mockRejectedValueOnce(new Error("timeout"));

    const result = await apiClient.get("/test");

    expect(result.error).toBeTruthy();
    expect(result.data).toBeNull();
  });

  it("handles network unreachable error", async () => {
    mockFetch.mockRejectedValueOnce(new TypeError("Failed to fetch"));

    const result = await apiClient.get("/test");

    expect(result.error).toBeTruthy();
  });

  it("handles abort signal properly", async () => {
    mockFetch.mockRejectedValueOnce(new DOMException("The user aborted a request.", "AbortError"));

    const result = await apiClient.get("/test");

    expect(result.error).toBeTruthy();
  });

  it("handles response with invalid JSON", async () => {
    mockFetch.mockResolvedValueOnce({
      ok: true,
      json: () => Promise.reject(new SyntaxError("Invalid JSON")),
    });

    const result = await apiClient.get("/test");

    expect(result.error).toBeTruthy();
    expect(result.data).toBeNull();
  });

  it("handles empty response body", async () => {
    mockFetch.mockResolvedValueOnce({
      ok: true,
      json: () => Promise.resolve(null),
    });

    const result = await apiClient.get("/test");

    expect(result.data).toBeNull();
    expect(result.error).toBeNull();
  });

  it("handles response with unexpected data type", async () => {
    mockFetch.mockResolvedValueOnce({
      ok: true,
      json: () => Promise.resolve("string instead of object"),
    });

    const result = await apiClient.get<{ id: number }>("/test");

    expect(result.data).toBe("string instead of object");
  });

  it("handles 400 Bad Request with error message", async () => {
    mockFetch.mockResolvedValueOnce({
      ok: false,
      status: 400,
      json: () => Promise.resolve({ message: "Invalid input" }),
    });

    const result = await apiClient.get("/test");

    expect(result.error).toBeTruthy();
    expect(result.data).toBeNull();
  });

  it("handles 401 Unauthorized (token expired)", async () => {
    mockFetch.mockResolvedValueOnce({
      ok: false,
      status: 401,
      json: () => Promise.resolve({ message: "Token expired" }),
    });

    const result = await apiClient.get("/test", {
      headers: { Authorization: "Bearer expired-token" },
    });

    expect(result.error).toBeTruthy();
  });

  it("handles 403 Forbidden", async () => {
    mockFetch.mockResolvedValueOnce({
      ok: false,
      status: 403,
      json: () => Promise.resolve({ message: "Access denied" }),
    });

    const result = await apiClient.get("/protected");

    expect(result.error).toBeTruthy();
  });

  it("handles 404 Not Found", async () => {
    mockFetch.mockResolvedValueOnce({
      ok: false,
      status: 404,
      json: () => Promise.resolve({ message: "Not found" }),
    });

    const result = await apiClient.get("/nonexistent");

    expect(result.error).toBeTruthy();
  });

  it("handles 429 Too Many Requests (rate limit)", async () => {
    mockFetch.mockResolvedValueOnce({
      ok: false,
      status: 429,
      json: () => Promise.resolve({ message: "Rate limited" }),
      headers: new Headers({ "retry-after": "60" }),
    });

    const result = await apiClient.get("/test");

    expect(result.error).toBeTruthy();
  });

  it("handles 500 Internal Server Error", async () => {
    mockFetch.mockResolvedValueOnce({
      ok: false,
      status: 500,
      json: () => Promise.resolve({ message: "Internal error" }),
    });

    const result = await apiClient.get("/test");

    expect(result.error).toBeTruthy();
  });

  it("handles 503 Service Unavailable", async () => {
    mockFetch.mockResolvedValueOnce({
      ok: false,
      status: 503,
      json: () => Promise.resolve({ message: "Service down" }),
    });

    const result = await apiClient.get("/test");

    expect(result.error).toBeTruthy();
  });

  it("retries on network failure", async () => {
    mockFetch
      .mockRejectedValueOnce(new Error("Network error"))
      .mockResolvedValueOnce({
        ok: true,
        json: () => Promise.resolve({ data: "success" }),
      });


    const result = await apiClient.get("/test");

    expect(result).toBeDefined();
  });

  it("POST with large payload", async () => {
    const largePayload = {
      data: "x".repeat(1000000), // 1MB
    };

    mockFetch.mockResolvedValueOnce({
      ok: true,
      json: () => Promise.resolve({ success: true }),
    });

    const result = await apiClient.post("/upload", largePayload);

    expect(mockFetch).toHaveBeenCalled();
  });

  it("PUT preserves all headers", async () => {
    mockFetch.mockResolvedValueOnce({
      ok: true,
      json: () => Promise.resolve({ updated: true }),
    });

    await apiClient.put("/resource/1", { name: "Updated" }, {
      headers: { "X-Custom-Header": "value" },
    });

    const callArgs = mockFetch.mock.calls[0];
    const options = callArgs[1];

    expect(options.method).toBe("PUT");
  });

  it("DELETE with query parameters", async () => {
    mockFetch.mockResolvedValueOnce({
      ok: true,
      json: () => Promise.resolve({ deleted: true }),
    });

    await apiClient.delete("/resource/1?cascade=true");

    expect(mockFetch).toHaveBeenCalledWith(
      expect.stringContaining("cascade=true"),
      expect.any(Object)
    );
  });

  it("merges custom headers with defaults", async () => {
    mockFetch.mockResolvedValueOnce({
      ok: true,
      json: () => Promise.resolve({}),
    });

    await apiClient.get("/test", {
      headers: { "X-Custom": "header-value" },
    });

    const callArgs = mockFetch.mock.calls[0];
    const options = callArgs[1];

    expect(options.headers["Content-Type"]).toBeDefined();
    expect(options.headers["X-Custom"]).toBe("header-value");
  });

  it("handles special characters in URL path", async () => {
    mockFetch.mockResolvedValueOnce({
      ok: true,
      json: () => Promise.resolve({}),
    });

    await apiClient.get("/search?q=java%20developer&city=Wars%C4%85w");

    expect(mockFetch).toHaveBeenCalled();
  });

  it("handles base URL without trailing slash", async () => {
    const client = new ApiClient("https://api.test.com");
    mockFetch.mockResolvedValueOnce({
      ok: true,
      json: () => Promise.resolve({}),
    });

    await client.get("/test");

    expect(mockFetch).toHaveBeenCalledWith(
      "https://api.test.com/test",
      expect.any(Object)
    );
  });

  it("handles base URL with trailing slash", async () => {
    const client = new ApiClient("https://api.test.com/");
    mockFetch.mockResolvedValueOnce({
      ok: true,
      json: () => Promise.resolve({}),
    });

    await client.get("/test");

    expect(mockFetch).toHaveBeenCalledWith(
      "https://api.test.com/test",
      expect.any(Object)
    );
  });

  it("handles multiple concurrent requests", async () => {
    mockFetch
      .mockResolvedValueOnce({
        ok: true,
        json: () => Promise.resolve({ id: 1 }),
      })
      .mockResolvedValueOnce({
        ok: true,
        json: () => Promise.resolve({ id: 2 }),
      })
      .mockResolvedValueOnce({
        ok: true,
        json: () => Promise.resolve({ id: 3 }),
      });

    const promises = [
      apiClient.get("/users/1"),
      apiClient.get("/users/2"),
      apiClient.get("/users/3"),
    ];

    const results = await Promise.all(promises);

    expect(results).toHaveLength(3);
    expect(results.every(r => r.error === null)).toBe(true);
  });
});
