"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import { useRegistration } from "@/context/RegistrationContext";
import { registerUser } from "@/services/authService";

type FormErrors = {
  email: string;
  phone: string;
  streetAddress: string;
  streetAddressLine2: string;
  city: string;
  state: string;
  zipCode: string;
  country: string;
};

export default function ContactInformationForm() {
  const router = useRouter();

  const {
    personalInformation,
    accountInformation,
    securityInformation,
    contactInformation,
    setContactInformation,
  } = useRegistration();

  const [errors, setErrors] = useState<FormErrors>({
    email: "",
    phone: "",
    streetAddress: "",
    streetAddressLine2: "",
    city: "",
    state: "",
    zipCode: "",
    country: "",
  });

  const [message, setMessage] = useState("");
  const [loading, setLoading] = useState(false);

  const handleChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    const { name, value } = event.target;

    if (
      name === "city" ||
      name === "state" ||
      name === "country"
    ) {
      if (!/^[A-Za-z\s]*$/.test(value)) {
        return;
      }
    }

    if (name === "phone" || name === "zipCode") {
      if (!/^\d*$/.test(value)) {
        return;
      }
    }

    setContactInformation({
      ...contactInformation,
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
      email: "",
      phone: "",
      streetAddress: "",
      streetAddressLine2: "",
      city: "",
      state: "",
      zipCode: "",
      country: "",
    };

    if (!contactInformation.email.trim()) {
      newErrors.email = "Please enter your email";
    } else if (
      !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(
        contactInformation.email
      )
    ) {
      newErrors.email = "Please enter a valid email";
    }

    if (!contactInformation.phone) {
      newErrors.phone = "Please enter your phone number";
    } else if (!/^\d{10}$/.test(contactInformation.phone)) {
      newErrors.phone =
        "Please enter a valid 10 digit phone number";
    }

    if (!contactInformation.streetAddress.trim()) {
      newErrors.streetAddress =
        "Please enter your street address";
    }

    if (!contactInformation.city.trim()) {
      newErrors.city = "Please enter a valid city";
    }

    if (!contactInformation.state.trim()) {
      newErrors.state = "Please enter a valid state";
    }

    if (!contactInformation.zipCode) {
      newErrors.zipCode =
        "Please enter a valid 5 digit ZIP Code";
    } else if (!/^\d{5}$/.test(contactInformation.zipCode)) {
      newErrors.zipCode =
        "Please enter a valid 5 digit ZIP Code";
    }

    if (!contactInformation.country.trim()) {
      newErrors.country = "Please enter a valid country";
    }

    setErrors(newErrors);

    return Object.values(newErrors).every(
      (error) => error === ""
    );
  };

  const handleContinue = async (
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
      const { response, data } = await registerUser({
        personalInformation,
        accountInformation,
        securityInformation,
        contactInformation,
      });

      if (response.ok) {
        router.push("/register/success");
      } else {
        setMessage(
          data.message || "Registration failed."
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
          Step 4: Contact Information
        </p>

        <div className="mb-8 flex justify-between rounded-lg bg-gray-100 p-3 text-sm">
          <span className="text-gray-500">
            1. Personal Info
          </span>

          <span className="text-gray-500">
            2. Account
          </span>

          <span className="text-gray-500">
            3. Security
          </span>

          <span className="font-bold text-blue-600">
            4. Contact
          </span>
        </div>

        <form
          onSubmit={handleContinue}
          className="space-y-5"
        >
          <div>
            <label
              htmlFor="email"
              className="mb-2 block text-sm font-semibold text-gray-800"
            >
              Email
            </label>

            <input
              id="email"
              name="email"
              type="email"
              value={contactInformation.email}
              onChange={handleChange}
              className={`w-full rounded-md border bg-white px-3 py-2 text-gray-800 outline-none focus:ring-2 ${
                errors.email
                  ? "border-red-500 focus:border-red-500 focus:ring-red-200"
                  : "border-gray-300 focus:border-blue-500 focus:ring-blue-200"
              }`}
            />

            {errors.email && (
              <p className="mt-1 text-sm font-medium text-red-500">
                {errors.email} ❗
              </p>
            )}
          </div>

          <div>
            <label
              htmlFor="phone"
              className="mb-2 block text-sm font-semibold text-gray-800"
            >
              Phone
            </label>

            <input
              id="phone"
              name="phone"
              type="tel"
              value={contactInformation.phone}
              onChange={handleChange}
              maxLength={10}
              className={`w-full rounded-md border bg-white px-3 py-2 text-gray-800 outline-none focus:ring-2 ${
                errors.phone
                  ? "border-red-500 focus:border-red-500 focus:ring-red-200"
                  : "border-gray-300 focus:border-blue-500 focus:ring-blue-200"
              }`}
            />

            {errors.phone && (
              <p className="mt-1 text-sm font-medium text-red-500">
                {errors.phone} ❗
              </p>
            )}
          </div>

          <div>
            <label
              htmlFor="streetAddress"
              className="mb-2 block text-sm font-semibold text-gray-800"
            >
              Street Address
            </label>

            <input
              id="streetAddress"
              name="streetAddress"
              value={contactInformation.streetAddress}
              onChange={handleChange}
              className={`w-full rounded-md border bg-white px-3 py-2 text-gray-800 outline-none focus:ring-2 ${
                errors.streetAddress
                  ? "border-red-500 focus:border-red-500 focus:ring-red-200"
                  : "border-gray-300 focus:border-blue-500 focus:ring-blue-200"
              }`}
            />

            {errors.streetAddress && (
              <p className="mt-1 text-sm font-medium text-red-500">
                {errors.streetAddress} ❗
              </p>
            )}
          </div>

          <div>
            <label
              htmlFor="streetAddressLine2"
              className="mb-2 block text-sm font-semibold text-gray-800"
            >
              Street Address Line 2
            </label>

            <input
              id="streetAddressLine2"
              name="streetAddressLine2"
              value={contactInformation.streetAddressLine2}
              onChange={handleChange}
              className={`w-full rounded-md border bg-white px-3 py-2 text-gray-800 outline-none focus:ring-2 ${
                errors.streetAddressLine2
                  ? "border-red-500 focus:border-red-500 focus:ring-red-200"
                  : "border-gray-300 focus:border-blue-500 focus:ring-blue-200"
              }`}
            />

            {errors.streetAddressLine2 && (
              <p className="mt-1 text-sm font-medium text-red-500">
                {errors.streetAddressLine2} ❗
              </p>
            )}
          </div>

          <div>
            <label
              htmlFor="city"
              className="mb-2 block text-sm font-semibold text-gray-800"
            >
              City
            </label>

            <input
              id="city"
              name="city"
              value={contactInformation.city}
              onChange={handleChange}
              className={`w-full rounded-md border bg-white px-3 py-2 text-gray-800 outline-none focus:ring-2 ${
                errors.city
                  ? "border-red-500 focus:border-red-500 focus:ring-red-200"
                  : "border-gray-300 focus:border-blue-500 focus:ring-blue-200"
              }`}
            />

            {errors.city && (
              <p className="mt-1 text-sm font-medium text-red-500">
                {errors.city} ❗
              </p>
            )}
          </div>

          <div>
            <label
              htmlFor="state"
              className="mb-2 block text-sm font-semibold text-gray-800"
            >
              State
            </label>

            <input
              id="state"
              name="state"
              value={contactInformation.state}
              onChange={handleChange}
              className={`w-full rounded-md border bg-white px-3 py-2 text-gray-800 outline-none focus:ring-2 ${
                errors.state
                  ? "border-red-500 focus:border-red-500 focus:ring-red-200"
                  : "border-gray-300 focus:border-blue-500 focus:ring-blue-200"
              }`}
            />

            {errors.state && (
              <p className="mt-1 text-sm font-medium text-red-500">
                {errors.state} ❗
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
              value={contactInformation.zipCode}
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

          <div>
            <label
              htmlFor="country"
              className="mb-2 block text-sm font-semibold text-gray-800"
            >
              Country
            </label>

            <input
              id="country"
              name="country"
              value={contactInformation.country}
              onChange={handleChange}
              className={`w-full rounded-md border bg-white px-3 py-2 text-gray-800 outline-none focus:ring-2 ${
                errors.country
                  ? "border-red-500 focus:border-red-500 focus:ring-red-200"
                  : "border-gray-300 focus:border-blue-500 focus:ring-blue-200"
              }`}
            />

            {errors.country && (
              <p className="mt-1 text-sm font-medium text-red-500">
                {errors.country} ❗
              </p>
            )}
          </div>

          <button
            type="submit"
            disabled={loading}
            className="w-full rounded-md bg-blue-600 px-4 py-3 font-semibold text-white transition hover:bg-blue-700 disabled:cursor-not-allowed disabled:bg-gray-400"
          >
            {loading ? "Registering..." : "Register"}
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