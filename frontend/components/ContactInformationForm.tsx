"use client";

import { useRouter } from "next/navigation";
import { useRegistration } from "@/context/RegistrationContext";
import { registerUser } from "@/services/authService";

export default function ContactInformationForm() {
  const router = useRouter();

  const {
  personalInformation,
  accountInformation,
  securityInformation,
  contactInformation,
  setContactInformation,
} = useRegistration();

  const handleChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setContactInformation({
      ...contactInformation,
      [event.target.name]: event.target.value,
    });
  };

  const handleContinue = async (
  event: React.FormEvent<HTMLFormElement>
) => {
  event.preventDefault();

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
      alert(
        data.message ||
          "Registration failed."
      );
    }
  } catch {
    alert("Unable to connect to the backend.");
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
              required
              className="w-full rounded-md border border-gray-300 bg-white px-3 py-2 text-gray-800 outline-none focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
            />
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
              required
              className="w-full rounded-md border border-gray-300 bg-white px-3 py-2 text-gray-800 outline-none focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
            />
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
              required
              className="w-full rounded-md border border-gray-300 bg-white px-3 py-2 text-gray-800 outline-none focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
            />
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
              className="w-full rounded-md border border-gray-300 bg-white px-3 py-2 text-gray-800 outline-none focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
            />
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
              required
              className="w-full rounded-md border border-gray-300 bg-white px-3 py-2 text-gray-800 outline-none focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
            />
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
              value={contactInformation.zipCode}
              onChange={handleChange}
              required
              maxLength={5}
              className="w-full rounded-md border border-gray-300 bg-white px-3 py-2 text-gray-800 outline-none focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
            />
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
              required
              className="w-full rounded-md border border-gray-300 bg-white px-3 py-2 text-gray-800 outline-none focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
            />
          </div>

          <button
            type="submit"
            className="w-full rounded-md bg-blue-600 px-4 py-3 font-semibold text-white transition hover:bg-blue-700"
          >
            Register
          </button>
        </form>
      </div>
    </main>
  );
}