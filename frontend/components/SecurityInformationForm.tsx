"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import { useRegistration } from "@/context/RegistrationContext";

type FormErrors = {
  nickname: string;
  childhoodHero: string;
};

export default function SecurityInformationForm() {
  const router = useRouter();

  const {
    securityInformation,
    setSecurityInformation,
  } = useRegistration();

  const [errors, setErrors] = useState<FormErrors>({
    nickname: "",
    childhoodHero: "",
  });

  const handleChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    const { name, value } = event.target;

    if (name === "nickname" || name === "childhoodHero") {
      if (!/^[A-Za-z\s]*$/.test(value)) {
        return;
      }
    }

    setSecurityInformation({
      ...securityInformation,
      [name]: value,
    });

    setErrors({
      ...errors,
      [name]: "",
    });
  };

  const validateForm = () => {
    const newErrors: FormErrors = {
      nickname: "",
      childhoodHero: "",
    };

    if (!securityInformation.nickname.trim()) {
      newErrors.nickname =
        "Please enter a valid nickname";
    }

    if (!securityInformation.childhoodHero.trim()) {
      newErrors.childhoodHero =
        "Please enter a valid childhood hero";
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

    router.push("/register/contact");
  };

  return (
    <main className="flex min-h-screen items-center justify-center bg-gray-100 p-8">
      <div className="w-full max-w-2xl rounded-xl bg-white p-8 shadow-lg">
        <h1 className="mb-2 text-center text-3xl font-bold text-gray-800">
          Insurance Portal
        </h1>

        <p className="mb-8 text-center text-gray-500">
          Step 3: Security Information
        </p>

        <div className="mb-8 flex justify-between rounded-lg bg-gray-100 p-3 text-sm">
          <span className="text-gray-500">
            1. Personal Info
          </span>

          <span className="text-gray-500">
            2. Account
          </span>

          <span className="font-bold text-blue-600">
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
              htmlFor="nickname"
              className="mb-2 block text-sm font-semibold text-gray-800"
            >
              Nickname
            </label>

            <input
              id="nickname"
              name="nickname"
              value={securityInformation.nickname}
              onChange={handleChange}
              className={`w-full rounded-md border bg-white px-3 py-2 text-gray-800 outline-none focus:ring-2 ${
                errors.nickname
                  ? "border-red-500 focus:border-red-500 focus:ring-red-200"
                  : "border-gray-300 focus:border-blue-500 focus:ring-blue-200"
              }`}
            />

            {errors.nickname && (
              <p className="mt-1 text-sm font-medium text-red-500">
                {errors.nickname} ❗
              </p>
            )}
          </div>

          <div>
            <label
              htmlFor="childhoodHero"
              className="mb-2 block text-sm font-semibold text-gray-800"
            >
              Childhood Hero
            </label>

            <input
              id="childhoodHero"
              name="childhoodHero"
              value={securityInformation.childhoodHero}
              onChange={handleChange}
              className={`w-full rounded-md border bg-white px-3 py-2 text-gray-800 outline-none focus:ring-2 ${
                errors.childhoodHero
                  ? "border-red-500 focus:border-red-500 focus:ring-red-200"
                  : "border-gray-300 focus:border-blue-500 focus:ring-blue-200"
              }`}
            />

            {errors.childhoodHero && (
              <p className="mt-1 text-sm font-medium text-red-500">
                {errors.childhoodHero} ❗
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