import { useState } from "react";
import { render, screen, fireEvent } from "@testing-library/react";
import { describe, expect, it, vi } from "vitest";
import AccountInformationForm from "@/components/AccountInformationForm";

const mockPush = vi.fn();

vi.mock("next/navigation", () => ({
  useRouter: () => ({
    push: mockPush,
  }),
}));

vi.mock("@/context/RegistrationContext", () => ({
  useRegistration: () => {
    const [accountInformation, setAccountInformation] = useState({
      username: "",
      password: "",
      confirmPassword: "",
    });

    return {
      accountInformation,
      setAccountInformation,
    };
  },
}));

describe("AccountInformationForm", () => {
  it("shows required field errors when submitted empty", () => {
    render(<AccountInformationForm />);

    fireEvent.click(
      screen.getByRole("button", { name: "Continue" })
    );

    expect(
      screen.getByText(/Please enter a username/)
    ).toBeInTheDocument();

    expect(
      screen.getByText(/Please enter a password/)
    ).toBeInTheDocument();

    expect(
      screen.getByText(/Please confirm your password/)
    ).toBeInTheDocument();

    expect(mockPush).not.toHaveBeenCalled();
  });

  it("shows an error when passwords do not match", () => {
    render(<AccountInformationForm />);

    fireEvent.change(
      screen.getByLabelText("Username"),
      { target: { value: "gokulraj01" } }
    );

    fireEvent.change(
      screen.getByLabelText("Password"),
      { target: { value: "Gokul@1234" } }
    );

    fireEvent.change(
      screen.getByLabelText("Confirm Password"),
      { target: { value: "WrongPassword1!" } }
    );

    fireEvent.click(
      screen.getByRole("button", { name: "Continue" })
    );

    expect(
      screen.getByText(/Passwords do not match/)
    ).toBeInTheDocument();

    expect(mockPush).not.toHaveBeenCalled();
  });
  
  it("shows an error when username is shorter than 6 characters", () => {
    render(<AccountInformationForm />);

    fireEvent.change(
      screen.getByLabelText("Username"),
      { target: { value: "abc" } }
    );

    fireEvent.change(
      screen.getByLabelText("Password"),
      { target: { value: "Gokul@1234" } }
    );

    fireEvent.change(
      screen.getByLabelText("Confirm Password"),
      { target: { value: "Gokul@1234" } }
    );

    fireEvent.click(
      screen.getByRole("button", { name: "Continue" })
    );

    expect(
      screen.getByText(/Username must be between 6 and 18 characters/)
    ).toBeInTheDocument();

    expect(mockPush).not.toHaveBeenCalled();
  });
  
  it("shows an error when password has no uppercase letter", () => {
    render(<AccountInformationForm />);

    fireEvent.change(
      screen.getByLabelText("Username"),
      { target: { value: "gokulraj01" } }
    );

    fireEvent.change(
      screen.getByLabelText("Password"),
      { target: { value: "gokul@1234" } }
    );

    fireEvent.change(
      screen.getByLabelText("Confirm Password"),
      { target: { value: "gokul@1234" } }
    );

    fireEvent.click(
      screen.getByRole("button", { name: "Continue" })
    );

    expect(
      screen.getByText(/Password must contain an uppercase letter/)
    ).toBeInTheDocument();

    expect(mockPush).not.toHaveBeenCalled();
  });
  
  it("shows an error when password has no number", () => {
    render(<AccountInformationForm />);

    fireEvent.change(
      screen.getByLabelText("Username"),
      { target: { value: "gokulraj01" } }
    );

    fireEvent.change(
      screen.getByLabelText("Password"),
      { target: { value: "Gokul@abcd" } }
    );

    fireEvent.change(
      screen.getByLabelText("Confirm Password"),
      { target: { value: "Gokul@abcd" } }
    );

    fireEvent.click(
      screen.getByRole("button", { name: "Continue" })
    );

    expect(
      screen.getByText(/Password must contain a number/)
    ).toBeInTheDocument();

    expect(mockPush).not.toHaveBeenCalled();
  });
  
  it("shows an error when password has no special character", () => {
    render(<AccountInformationForm />);

    fireEvent.change(
      screen.getByLabelText("Username"),
      { target: { value: "gokulraj01" } }
    );

    fireEvent.change(
      screen.getByLabelText("Password"),
      { target: { value: "Gokul1234" } }
    );

    fireEvent.change(
      screen.getByLabelText("Confirm Password"),
      { target: { value: "Gokul1234" } }
    );

    fireEvent.click(
      screen.getByRole("button", { name: "Continue" })
    );

    expect(
      screen.getByText(/Password must contain a special character/)
    ).toBeInTheDocument();

    expect(mockPush).not.toHaveBeenCalled();
  });
  
  it("shows an error when password is shorter than 8 characters", () => {
    render(<AccountInformationForm />);

    fireEvent.change(
      screen.getByLabelText("Username"),
      { target: { value: "gokulraj01" } }
    );

    fireEvent.change(
      screen.getByLabelText("Password"),
      { target: { value: "Go@123" } }
    );

    fireEvent.change(
      screen.getByLabelText("Confirm Password"),
      { target: { value: "Go@123" } }
    );

    fireEvent.click(
      screen.getByRole("button", { name: "Continue" })
    );

    expect(
      screen.getByText(/Password must be between 8 and 16 characters/)
    ).toBeInTheDocument();

    expect(mockPush).not.toHaveBeenCalled();
  });
  
  it("navigates to the security page when account details are valid", () => {
    render(<AccountInformationForm />);

    fireEvent.change(
      screen.getByLabelText("Username"),
      { target: { value: "gokulraj01" } }
    );

    fireEvent.change(
      screen.getByLabelText("Password"),
      { target: { value: "Gokul@1234" } }
    );

    fireEvent.change(
      screen.getByLabelText("Confirm Password"),
      { target: { value: "Gokul@1234" } }
    );

    fireEvent.click(
      screen.getByRole("button", { name: "Continue" })
    );

    expect(mockPush).toHaveBeenCalledWith(
      "/register/security"
    );
  });
  
  it("shows an error when username exceeds 18 characters", () => {
    render(<AccountInformationForm />);

    fireEvent.change(
      screen.getByLabelText("Username"),
      { target: { value: "abcdefghijklmnopqrs" } }
    );

    fireEvent.change(
      screen.getByLabelText("Password"),
      { target: { value: "Gokul@1234" } }
    );

    fireEvent.change(
      screen.getByLabelText("Confirm Password"),
      { target: { value: "Gokul@1234" } }
    );

    fireEvent.click(
      screen.getByRole("button", { name: "Continue" })
    );

    expect(
      screen.getByText(/Username must be between 6 and 18 characters/)
    ).toBeInTheDocument();

    expect(mockPush).not.toHaveBeenCalled();
  });
  
  it("shows an error when password exceeds 16 characters", () => {
    render(<AccountInformationForm />);

    fireEvent.change(
      screen.getByLabelText("Username"),
      { target: { value: "gokulraj01" } }
    );

    fireEvent.change(
      screen.getByLabelText("Password"),
      { target: { value: "Gokul@12345678901" } }
    );

    fireEvent.change(
      screen.getByLabelText("Confirm Password"),
      { target: { value: "Gokul@12345678901" } }
    );

    fireEvent.click(
      screen.getByRole("button", { name: "Continue" })
    );

    expect(
      screen.getByText(/Password must be between 8 and 16 characters/)
    ).toBeInTheDocument();

    expect(mockPush).not.toHaveBeenCalled();
  });




}

);