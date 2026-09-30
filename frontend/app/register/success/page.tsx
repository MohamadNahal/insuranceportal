import Link from "next/link";

export default function RegistrationSuccessPage() {
  return (
    <main className="flex min-h-screen items-center justify-center bg-gray-100 p-8">
      <div className="w-full max-w-2xl rounded-xl bg-white p-10 text-center shadow-lg">

        <h1 className="mb-4 text-3xl font-bold text-gray-800">
          Registration Successful
        </h1>

        <p className="mb-2 text-lg text-gray-600">
          Your account has been registered successfully.
        </p>

        <p className="mb-8 text-gray-500">
          Please proceed to login to access your Insurance Portal.
        </p>

        <Link
          href="/"
          className="inline-block rounded-md bg-blue-600 px-6 py-3 font-semibold text-white transition hover:bg-blue-700"
        >
          Proceed to Login
        </Link>

      </div>
    </main>
  );
}