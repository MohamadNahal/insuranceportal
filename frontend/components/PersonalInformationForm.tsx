"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import { useRegistration } from "@/context/RegistrationContext";

export default function PersonalInformationForm() {
  const router = useRouter();

  const {
    personalInformation,
    setPersonalInformation,
  } = useRegistration();

  const [message, setMessage] = useState("");
  const [loading, setLoading] = useState(false);

  const handleChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setPersonalInformation({
      ...personalInformation,
      [event.target.name]: event.target.value,
    });
  };

  const validatePolicyholder = async (
    event: React.FormEvent<HTMLFormElement>
  ) => {
    event.preventDefault();

    setLoading(true);
    setMessage("");

    try {
      const response = await fetch(
        "http://localhost:5200/api/Auth/validate-policyholder",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(personalInformation),
        }
      );

      const data = await response.json();

      if (response.ok) {
        setMessage("");
        router.push("/register/account");
      } else {
        setMessage(
          data.message ||
            "Policyholder information could not be validated."
        );
      }
    } catch {
      setMessage("Unable to connect to the backend.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <main className="flex min-h-screen items-center justify-center bg-gray-100 p-8">
      <div className="w-full max-w-2xl rounded-xl bg-white p-8 shadow-lg">

        <h1 className="mb-2 text-center text-3xl font-bold text-gray-800">
          Insurance Portal
        </h1>

        <p className="mb-8 text-center text-gray-500">
          Step 1: Personal Information
        </p>

        <div className="mb-8 flex justify-between rounded-lg bg-gray-100 p-3 text-sm">
          <span className="font-bold text-blue-600">
            1. Personal Info
          </span>

          <span className="text-gray-500">
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
          onSubmit={validatePolicyholder}
          className="space-y-5"
        >
          <div>
            <label
              htmlFor="ssn"
              className="mb-2 block text-sm font-semibold text-gray-800"
            >
              SSN Last 4
            </label>

            <input
              id="ssn"
              name="ssn"
              value={personalInformation.ssn}
              onChange={handleChange}
              maxLength={4}
              required
              className="w-full rounded-md border border-gray-300 bg-white px-3 py-2 text-gray-800 outline-none focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
            />
          </div>

          <div>
            <label
              htmlFor="policyNumber"
              className="mb-2 block text-sm font-semibold text-gray-800"
            >
              Policy Number
            </label>

            <input
              id="policyNumber"
              name="policyNumber"
              value={personalInformation.policyNumber}
              onChange={handleChange}
              required
              className="w-full rounded-md border border-gray-300 bg-white px-3 py-2 text-gray-800 outline-none focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
            />
          </div>

          <div>
            <label
              htmlFor="firstName"
              className="mb-2 block text-sm font-semibold text-gray-800"
            >
              First Name
            </label>

            <input
              id="firstName"
              name="firstName"
              value={personalInformation.firstName}
              onChange={handleChange}
              required
              className="w-full rounded-md border border-gray-300 bg-white px-3 py-2 text-gray-800 outline-none focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
            />
          </div>

          <div>
            <label
              htmlFor="lastName"
              className="mb-2 block text-sm font-semibold text-gray-800"
            >
              Last Name
            </label>

            <input
              id="lastName"
              name="lastName"
              value={personalInformation.lastName}
              onChange={handleChange}
              required
              className="w-full rounded-md border border-gray-300 bg-white px-3 py-2 text-gray-800 outline-none focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
            />
          </div>

          <div>
            <label
              htmlFor="dateOfBirth"
              className="mb-2 block text-sm font-semibold text-gray-800"
            >
              Date of Birth
            </label>

            <input
              id="dateOfBirth"
              name="dateOfBirth"
              value={personalInformation.dateOfBirth}
              onChange={handleChange}
              placeholder="MM/DD/YYYY"
              required
              className="w-full rounded-md border border-gray-300 bg-white px-3 py-2 text-gray-800 outline-none focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
            />
          </div>

          <div>
            <label
              htmlFor="zipCode"
              className="mb-2 block text-sm font-semibold text-gray-800"
            >
              ZIP Code
            </label>

            <input
              id="zipCode"
              name="zipCode"
              value={personalInformation.zipCode}
              onChange={handleChange}
              maxLength={5}
              required
              className="w-full rounded-md border border-gray-300 bg-white px-3 py-2 text-gray-800 outline-none focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
            />
          </div>

          <button
            type="submit"
            disabled={loading}
            className="w-full rounded-md bg-blue-600 px-4 py-3 font-semibold text-white transition hover:bg-blue-700 disabled:cursor-not-allowed disabled:bg-gray-400"
          >
            {loading ? "Validating..." : "Continue"}
          </button>

          {message && (
            <p className="text-center text-sm font-medium text-red-600">
              {message}
            </p>
          )}
        </form>
      </div>
    </main>
  );
}