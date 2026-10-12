import {
  afterEach,
  beforeEach,
  describe,
  expect,
  it,
  vi,
} from "vitest";

import {
  getCurrentUser,
  loginUser,
  logoutUser,
} from "@/services/authService";

describe("Authentication service", () => {
  const fetchMock = vi.fn();

  beforeEach(() => {
    vi.stubGlobal("fetch", fetchMock);
    fetchMock.mockReset();

    fetchMock.mockResolvedValue(
      new Response(
        JSON.stringify({ username: "testuser" }),
        {
          status: 200,
          headers: {
            "Content-Type": "application/json",
          },
        }
      )
    );
  });

  afterEach(() => {
    vi.unstubAllGlobals();
  });

  it("sends credentials when logging in", async () => {
    await loginUser({
      username: "testuser",
      password: "Password123!",
    });

    expect(fetchMock).toHaveBeenCalledWith(
      expect.stringContaining("/Auth/login"),
      expect.objectContaining({
        method: "POST",
        credentials: "include",
      })
    );
  });

  it("checks the current authenticated user", async () => {
    const result = await getCurrentUser();

    expect(fetchMock).toHaveBeenCalledWith(
      expect.stringContaining("/Auth/me"),
      expect.objectContaining({
        method: "GET",
        credentials: "include",
      })
    );

    expect(result.response.status).toBe(200);
    expect(result.data.username).toBe("testuser");
  });

  it("sends credentials when logging out", async () => {
    await logoutUser();

    expect(fetchMock).toHaveBeenCalledWith(
      expect.stringContaining("/Auth/logout"),
      expect.objectContaining({
        method: "POST",
        credentials: "include",
      })
    );
  });

  it("handles an unauthorized session response", async () => {
    fetchMock.mockResolvedValueOnce(
      new Response(
        JSON.stringify({ message: "Unauthorized" }),
        {
          status: 401,
          headers: {
            "Content-Type": "application/json",
          },
        }
      )
    );

    const result = await getCurrentUser();

    expect(result.response.status).toBe(401);
    expect(result.data.message).toBe("Unauthorized");
  });

  it("handles a failed login", async () => {
    fetchMock.mockResolvedValueOnce(
      new Response(
        JSON.stringify({
          message: "Invalid username or password.",
        }),
        {
          status: 401,
          headers: {
            "Content-Type": "application/json",
          },
        }
      )
    );

    const result = await loginUser({
      username: "testuser",
      password: "WrongPassword!",
    });

    expect(result.response.status).toBe(401);
    expect(result.data.message).toBe(
      "Invalid username or password."
    );
  });
});