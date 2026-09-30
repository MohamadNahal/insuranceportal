"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import { useRegistration } from "@/context/RegistrationContext";

type FormErrors = {
  ssn: string;
  policyNumber: string;
  firstName: string;
  lastName: string;
  dateOfBirth: string;
  zipCode: string;
};

export default function PersonalInformationForm() {
  const router = useRouter();

  const {
    personalInformation,
    setPersonalInformation,
  } = useRegistration();

  const [message, setMessage] = useState("");
  const [loading, setLoading] = useState(false);

  const [errors, setErrors] = useState<FormErrors>({
    ssn: "",
    policyNumber: "",
    firstName: "",
    lastName: "",
    dateOfBirth: "",
    zipCode: "",
  });

  const handleChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    const { name, value } = event.target;

    if (name === "firstName" || name === "lastName") {
      if (!/^[A-Za-z\s]*$/.test(value)) {
        return;
      }
    }

    setPersonalInformation({
      ...personalInformation,
      [name]: value,
    });

    setErrors({
      ...errors,
      [name]: "",
    });

    setMessage("");
  };

  const validateForm = () => {
    const newErrors: FormErrors = {
      ssn: "",
      policyNumber: "",
      firstName: "",
      lastName: "",
      dateOfBirth: "",
      zipCode: "",
    };

    if (!personalInformation.ssn) {
      newErrors.ssn =
        "Please enter last 4 digits of SSN";
    } else if (!/^\d{4}$/.test(personalInformation.ssn)) {
      newErrors.ssn =
        "Please enter last 4 digits of SSN";
    }

    if (!personalInformation.policyNumber) {
      newErrors.policyNumber =
        "Please enter a valid Policy Number";
    } else if (
      !/^[A-Za-z]{2}\d{7}$/.test(
        personalInformation.policyNumber
      )
    ) {
      newErrors.policyNumber =
        "Please enter a valid Policy Number";
    }

    if (!personalInformation.firstName.trim()) {
      newErrors.firstName =
        "Please enter valid First Name";
    }

    if (!personalInformation.lastName.trim()) {
      newErrors.lastName =
        "Please enter valid Last Name";
    }

    if (!personalInformation.dateOfBirth) {
      newErrors.dateOfBirth =
        "Please enter valid Date of Birth";
    } else if (
      !/^\d{2}\/\d{2}\/\d{4}$/.test(
        personalInformation.dateOfBirth
      )
    ) {
      newErrors.dateOfBirth =
        "Please enter valid Date of Birth";
    } else {
      const [month, day, year] =
        personalInformation.dateOfBirth
          .split("/")
          .map(Number);

      const date = new Date(year, month - 1, day);
      const today = new Date();

      const isValidDate =
        date.getFullYear() === year &&
        date.getMonth() === month - 1 &&
        date.getDate() === day;

      if (!isValidDate || date > today) {
        newErrors.dateOfBirth =
          "Please enter valid Date of Birth";
      }
    }

    if (!personalInformation.zipCode) {
      newErrors.zipCode =
        "Please enter valid 5 digit Zip Code";
    } else if (
      !/^\d{5}$/.test(personalInformation.zipCode)
    ) {
      newErrors.zipCode =
        "Please enter valid 5 digit Zip Code";
    }

    setErrors(newErrors);

    return Object.values(newErrors).every(
      (error) => error === ""
    );
  };

  const validatePolicyholder = async (
    event: React.FormEvent<HTMLFormElement>
  ) => {
    event.preventDefault();

    const isValid = validateForm();

    if (!isValid) {
      return;
    }

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
              className={`w-full rounded-md border bg-white px-3 py-2 text-gray-800 outline-none focus:ring-2 ${
                errors.ssn
                  ? "border-red-500 focus:border-red-500 focus:ring-red-200"
                  : "border-gray-300 focus:border-blue-500 focus:ring-blue-200"
              }`}
            />

            {errors.ssn && (
              <p className="mt-1 text-sm font-medium text-red-500">
                {errors.ssn} ❗
              </p>
            )}
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
              className={`w-full rounded-md border bg-white px-3 py-2 text-gray-800 outline-none focus:ring-2 ${
                errors.policyNumber
                  ? "border-red-500 focus:border-red-500 focus:ring-red-200"
                  : "border-gray-300 focus:border-blue-500 focus:ring-blue-200"
              }`}
            />

            {errors.policyNumber && (
              <p className="mt-1 text-sm font-medium text-red-500">
                {errors.policyNumber} ❗
              </p>
            )}

            <p className="mt-2 text-sm text-blue-700">
              ℹ Your policy number can be found on your original policy document.
            </p>
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
              className={`w-full rounded-md border bg-white px-3 py-2 text-gray-800 outline-none focus:ring-2 ${
                errors.firstName
                  ? "border-red-500 focus:border-red-500 focus:ring-red-200"
                  : "border-gray-300 focus:border-blue-500 focus:ring-blue-200"
              }`}
            />

            {errors.firstName && (
              <p className="mt-1 text-sm font-medium text-red-500">
                {errors.firstName} ❗
              </p>
            )}
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
              className={`w-full rounded-md border bg-white px-3 py-2 text-gray-800 outline-none focus:ring-2 ${
                errors.lastName
                  ? "border-red-500 focus:border-red-500 focus:ring-red-200"
                  : "border-gray-300 focus:border-blue-500 focus:ring-blue-200"
              }`}
            />

            {errors.lastName && (
              <p className="mt-1 text-sm font-medium text-red-500">
                {errors.lastName} ❗
              </p>
            )}
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
              maxLength={10}
              className={`w-full rounded-md border bg-white px-3 py-2 text-gray-800 outline-none focus:ring-2 ${
                errors.dateOfBirth
                  ? "border-red-500 focus:border-red-500 focus:ring-red-200"
                  : "border-gray-300 focus:border-blue-500 focus:ring-blue-200"
              }`}
            />

            {errors.dateOfBirth && (
              <p className="mt-1 text-sm font-medium text-red-500">
                {errors.dateOfBirth} ❗
              </p>
            )}
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
              className={`w-full rounded-md border bg-white px-3 py-2 text-gray-800 outline-none focus:ring-2 ${
                errors.zipCode
                  ? "border-red-500 focus:border-red-500 focus:ring-red-200"
                  : "border-gray-300 focus:border-blue-500 focus:ring-blue-200"
              }`}
            />

            {errors.zipCode && (
              <p className="mt-1 text-sm font-medium text-red-500">
                {errors.zipCode} ❗
              </p>
            )}
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