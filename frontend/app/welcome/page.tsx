
"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import {
  clearLoggedIn,
  isLoggedIn,
} from "@/services/authSession";

export default function WelcomePage() {
  const router = useRouter();
  const [authorized] = useState(() => isLoggedIn());

  if (!authorized) {
    if (typeof window !== "undefined") {
      router.replace("/");
    }

    return null;
  }

  const handleLogout = () => {
    clearLoggedIn();
    router.replace("/");
  };

  return (
    <main className="flex min-h-screen items-center justify-center bg-gray-100 p-8">
      <div className="w-full max-w-2xl rounded-xl bg-white p-10 text-center shadow-lg">
        <h1 className="mb-4 text-3xl font-bold text-gray-800">
          Welcome to Insurance Portal
        </h1>

        <p className="mb-8 text-gray-600">
          You have successfully logged in.
        </p>

        <button
          type="button"
          onClick={handleLogout}
          className="inline-block rounded-md bg-blue-600 px-6 py-3 font-semibold text-white transition hover:bg-blue-700"
        >
          Logout
        </button>
      </div>
    </main>
  );
}
