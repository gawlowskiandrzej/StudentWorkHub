import { describe, expect, it, vi, beforeEach, afterEach } from "vitest";
import { ApiClient } from "@/lib/api/apiClient";

describe("ApiClient", () => {
  const fetchSpy = vi.spyOn(globalThis, "fetch");
  const client = new ApiClient("https://example.com");

  beforeEach(() => {
    fetchSpy.mockReset();
  });

  afterEach(() => {
    fetchSpy.mockReset();
  });

  it("returns data on 200 OK", async () => {
    fetchSpy.mockResolvedValueOnce({
      ok: true,
      status: 200,
      json: async () => ({ ok: true }),
      text: async () => "",
      headers: new Headers(),
    } as any);

    const res = await client.get<{ ok: boolean }>("/ping");

    expect(fetchSpy).toHaveBeenCalledWith("https://example.com/ping", expect.any(Object));
    expect(res.data).toEqual({ ok: true });
    expect(res.error).toBeNull();
  });

  it("returns error string on non-OK status", async () => {
    fetchSpy.mockResolvedValueOnce({
      ok: false,
      status: 500,
      json: async () => ({ message: "boom" }),
      text: async () => "boom",
      headers: new Headers(),
    } as any);

    const res = await client.get("/fail");

    expect(res.data).toBeNull();
    expect(res.error).toContain("500");
  });

  it("maps AbortError to Request timeout", async () => {
    const abortErr = new DOMException("Aborted", "AbortError");
    fetchSpy.mockRejectedValueOnce(abortErr as any);

    const res = await client.get("/slow");

    expect(res.error).toContain("timeout");
    expect(res.data).toBeNull();
  });
});
