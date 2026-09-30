"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import { useRegistration } from "@/context/RegistrationContext";

type FormErrors = {
  username: string;
  password: string;
  confirmPassword: string;
};

export default function AccountInformationForm() {
  const router = useRouter();

  const {
    accountInformation,
    setAccountInformation,
  } = useRegistration();

  const [errors, setErrors] = useState<FormErrors>({
    username: "",
    password: "",
    confirmPassword: "",
  });

  const handleChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    const { name, value } = event.target;

    setAccountInformation({
      ...accountInformation,
      [name]: value,
    });

    setErrors({
      ...errors,
      [name]: "",
    });
  };

  const validateForm = () => {
    const newErrors: FormErrors = {
      username: "",
      password: "",
      confirmPassword: "",
    };

    if (!accountInformation.username.trim()) {
      newErrors.username = "Please enter a username";
    }

    if (!accountInformation.password) {
      newErrors.password = "Please enter a password";
    }

    if (!accountInformation.confirmPassword) {
      newErrors.confirmPassword =
        "Please confirm your password";
    } else if (
      accountInformation.password !==
      accountInformation.confirmPassword
    ) {
      newErrors.confirmPassword =
        "Passwords do not match";
    }

    setErrors(newErrors);

    return Object.values(newErrors).every(
      (error) => error === ""
    );
  };

  const handleContinue = (
    event: React.FormEvent<HTMLFormElement>
  ) => {
    event.preventDefault();

    const isValid = validateForm();

    if (!isValid) {
      return;
    }

    router.push("/register/security");
  };

  return (
    <main className="flex min-h-screen items-center justify-center bg-gray-100 p-8">
      <div className="w-full max-w-2xl rounded-xl bg-white p-8 shadow-lg">
        <h1 className="mb-2 text-center text-3xl font-bold text-gray-800">
          Insurance Portal
        </h1>

        <p className="mb-8 text-center text-gray-500">
          Step 2: Account Information
        </p>

        <div className="mb-8 flex justify-between rounded-lg bg-gray-100 p-3 text-sm">
          <span className="text-gray-500">
            1. Personal Info
          </span>

          <span className="font-bold text-blue-600">
            2. Account
          </span>

          <span className="text-gray-500">
            3. Security
          </span>

          <span className="text-gray-500">
            4. Contact
          </span>
        </div>

        <form
          onSubmit={handleContinue}
          className="space-y-5"
        >
          <div>
            <label
              htmlFor="username"
              className="mb-2 block text-sm font-semibold text-gray-800"
            >
              Username
            </label>

            <input
              id="username"
              name="username"
              value={accountInformation.username}
              onChange={handleChange}
              className={`w-full rounded-md border bg-white px-3 py-2 text-gray-800 outline-none focus:ring-2 ${
                errors.username
                  ? "border-red-500 focus:border-red-500 focus:ring-red-200"
                  : "border-gray-300 focus:border-blue-500 focus:ring-blue-200"
              }`}
            />

            {errors.username && (
              <p className="mt-1 text-sm font-medium text-red-500">
                {errors.username} ❗
              </p>
            )}
          </div>

          <div>
            <label
              htmlFor="password"
              className="mb-2 block text-sm font-semibold text-gray-800"
            >
              Password
            </label>

            <input
              id="password"
              name="password"
              type="password"
              value={accountInformation.password}
              onChange={handleChange}
              className={`w-full rounded-md border bg-white px-3 py-2 text-gray-800 outline-none focus:ring-2 ${
                errors.password
                  ? "border-red-500 focus:border-red-500 focus:ring-red-200"
                  : "border-gray-300 focus:border-blue-500 focus:ring-blue-200"
              }`}
            />

            {errors.password && (
              <p className="mt-1 text-sm font-medium text-red-500">
                {errors.password} ❗
              </p>
            )}
          </div>

          <div>
            <label
              htmlFor="confirmPassword"
              className="mb-2 block text-sm font-semibold text-gray-800"
            >
              Confirm Password
            </label>

            <input
              id="confirmPassword"
              name="confirmPassword"
              type="password"
              value={accountInformation.confirmPassword}
              onChange={handleChange}
              className={`w-full rounded-md border bg-white px-3 py-2 text-gray-800 outline-none focus:ring-2 ${
                errors.confirmPassword
                  ? "border-red-500 focus:border-red-500 focus:ring-red-200"
                  : "border-gray-300 focus:border-blue-500 focus:ring-blue-200"
              }`}
            />

            {errors.confirmPassword && (
              <p className="mt-1 text-sm font-medium text-red-500">
                {errors.confirmPassword} ❗
              </p>
            )}
          </div>

          <button
            type="submit"
            className="w-full rounded-md bg-blue-600 px-4 py-3 font-semibold text-white transition hover:bg-blue-700"
          >
            Continue
          </button>
        </form>
      </div>
    </main>
  );
}