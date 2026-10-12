import { render, screen, fireEvent, waitFor } from "@testing-library/react";
import { beforeEach, describe, expect, it, vi } from "vitest";
import WelcomePage from "@/app/welcome/page";
import { getCurrentUser, logoutUser } from "@/services/authService";

const { mockReplace } = vi.hoisted(() => ({
  mockReplace: vi.fn(),
}));

vi.mock("next/navigation", () => ({
  useRouter: () => ({
    replace: mockReplace,
  }),
}));

vi.mock("@/services/authService", () => ({
  getCurrentUser: vi.fn(),
  logoutUser: vi.fn(),
}));

describe("WelcomePage", () => {
  beforeEach(() => {
    vi.clearAllMocks();
  });

  it("redirects to login when the session is unauthorized", async () => {
    vi.mocked(getCurrentUser).mockResolvedValue({
      response: new Response(null, { status: 401 }),
      data: { message: "Unauthorized" },
    });

    render(<WelcomePage />);

    await waitFor(() => {
      expect(mockReplace).toHaveBeenCalledWith("/");
    });

    expect(
      screen.queryByText("Welcome to Insurance Portal")
    ).not.toBeInTheDocument();
  });

  it("shows the welcome page for an authenticated user", async () => {
    vi.mocked(getCurrentUser).mockResolvedValue({
      response: new Response(
        JSON.stringify({
          username: "testuser",
          policyholderId: "123",
        }),
        {
          status: 200,
          headers: {
            "Content-Type": "application/json",
          },
        }
      ),
      data: {
        username: "testuser",
        policyholderId: "123",
      },
    });

    render(<WelcomePage />);

    expect(
      await screen.findByText("Welcome to Insurance Portal")
    ).toBeInTheDocument();

    expect(screen.getByText("Logged in as testuser")).toBeInTheDocument();
  });

  it("logs out and redirects to login", async () => {
    vi.mocked(getCurrentUser).mockResolvedValue({
      response: new Response(
        JSON.stringify({
          username: "testuser",
          policyholderId: "123",
        }),
        {
          status: 200,
          headers: {
            "Content-Type": "application/json",
          },
        }
      ),
      data: {
        username: "testuser",
        policyholderId: "123",
      },
    });

    vi.mocked(logoutUser).mockResolvedValue({
      response: new Response(
        JSON.stringify({ message: "Logged out successfully." }),
        {
          status: 200,
          headers: {
            "Content-Type": "application/json",
          },
        }
      ),
      data: { message: "Logged out successfully." },
    });

    render(<WelcomePage />);

    fireEvent.click(
      await screen.findByRole("button", { name: "Logout" })
    );

    await waitFor(() => {
      expect(logoutUser).toHaveBeenCalledOnce();
      expect(mockReplace).toHaveBeenCalledWith("/");
    });
  });
});