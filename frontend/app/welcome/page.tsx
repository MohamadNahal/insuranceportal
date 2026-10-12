"use client";

import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import { getCurrentUser, logoutUser } from "@/services/authService";

export default function WelcomePage() {
  const router = useRouter();
  const [authorized, setAuthorized] = useState(false);
  const [checkingSession, setCheckingSession] = useState(true);
  const [loggingOut, setLoggingOut] = useState(false);
  const [username, setUsername] = useState("");

  useEffect(() => {
    let cancelled = false;

    async function checkSession() {
      try {
        const { response, data } = await getCurrentUser();

        if (cancelled) {
          return;
        }

        if (!response.ok) {
          router.replace("/");
          return;
        }

        setUsername(data.username ?? "");
        setAuthorized(true);
      } catch {
        if (!cancelled) {
          router.replace("/");
        }
      } finally {
        if (!cancelled) {
          setCheckingSession(false);
        }
      }
    }

    checkSession();

    return () => {
      cancelled = true;
    };
  }, [router]);

  const handleLogout = async () => {
    setLoggingOut(true);

    try {
      await logoutUser();
    } finally {
      router.replace("/");
    }
  };

  if (checkingSession || !authorized) {
    return null;
  }

  return (
    <main className="flex min-h-screen items-center justify-center bg-gray-100 p-8">
      <div className="w-full max-w-2xl rounded-xl bg-white p-10 text-center shadow-lg">
        <h1 className="mb-4 text-3xl font-bold text-gray-800">
          Welcome to Insurance Portal
        </h1>

        <p className="mb-3 text-gray-600">
          You have successfully logged in.
        </p>

        {username && (
          <p className="mb-8 text-gray-600">
            Logged in as {username}
          </p>
        )}

        <button
          type="button"
          onClick={handleLogout}
          disabled={loggingOut}
          className="inline-block rounded-md bg-blue-600 px-6 py-3 font-semibold text-white transition hover:bg-blue-700 disabled:cursor-not-allowed disabled:bg-gray-400"
        >
          {loggingOut ? "Logging out..." : "Logout"}
        </button>
      </div>
    </main>
  );
}