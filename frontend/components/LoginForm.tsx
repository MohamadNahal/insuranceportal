"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import Link from "next/link";
import { loginUser } from "@/services/authService";

type FormErrors = {
  username: string;
  password: string;
};

export default function LoginForm() {
  const router = useRouter();

  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const [errors, setErrors] = useState<FormErrors>({
    username: "",
    password: "",
  });

  const [message, setMessage] = useState("");
  const [loading, setLoading] = useState(false);

  const handleUsernameChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setUsername(event.target.value);

    setErrors({
      ...errors,
      username: "",
    });

    setMessage("");
  };

  const handlePasswordChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setPassword(event.target.value);

    setErrors({
      ...errors,
      password: "",
    });

    setMessage("");
  };

  const validateForm = () => {
    const newErrors: FormErrors = {
      username: "",
      password: "",
    };

    if (!username.trim()) {
      newErrors.username = "Please enter your username";
    }

    if (!password) {
      newErrors.password = "Please enter your password";
    }

    setErrors(newErrors);

    return Object.values(newErrors).every(
      (error) => error === ""
    );
  };

  const handleLogin = async (
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
      const { response, data } = await loginUser({
        username,
        password,
      });

      if (response.ok) {
        router.push("/welcome");
      } else {
        setMessage(
          data.message ||
            "Invalid username or password."
        );
      }
    } catch {
      setMessage(
        "Unable to connect to the backend."
      );
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
          Customer Login
        </p>

        <form
          onSubmit={handleLogin}
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
              value={username}
              onChange={handleUsernameChange}
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
              value={password}
              onChange={handlePasswordChange}
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

          <button
            type="submit"
            disabled={loading}
            className="w-full rounded-md bg-blue-600 px-4 py-3 font-semibold text-white transition hover:bg-blue-700 disabled:cursor-not-allowed disabled:bg-gray-400"
          >
            {loading ? "Logging in..." : "Login"}
          </button>

          <p className="text-center text-gray-700">
            New User?{" "}
            <Link
              href="/register/personal"
              className="font-semibold text-blue-600 hover:text-blue-700"
            >
              Register here
            </Link>
          </p>

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