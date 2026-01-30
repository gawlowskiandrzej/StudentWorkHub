import { describe, expect, it, vi, beforeEach } from "vitest";
import { DictionaryApi } from "@/lib/api/controllers/dictionary";
import { apiClient } from "@/lib/api/apiClient";

vi.mock("@/lib/api/apiClient", () => {
  const get = vi.fn();
  return { apiClient: { get } };
});

type MockGet = ReturnType<typeof vi.fn>;

describe("DictionaryApi", () => {
  let get: MockGet;

  beforeEach(() => {
    get = (apiClient.get as unknown) as MockGet;
    get.mockReset();
    get.mockResolvedValue({ data: null, error: null });
  });

  it("fetches static dictionaries with expected path", async () => {
    await DictionaryApi.getStaticDictionaries();
    expect(get).toHaveBeenCalledWith(`/dictionary/searchview-dictionaries`);
  });

  it("fetches full dictionaries with expected path", async () => {
    await DictionaryApi.getAllDictionaries();
    expect(get).toHaveBeenCalledWith(`/dictionary/all-dictionaries`);
  });
});
