import type { Metadata } from "next";
import { Geist, Geist_Mono } from "next/font/google";
import "./globals.css";
import { RegistrationProvider } from "@/context/RegistrationContext";
import RegistrationStepGuard from "@/components/RegistrationStepGuard";

const geistSans = Geist({
  variable: "--font-geist-sans",
  subsets: ["latin"],
});

const geistMono = Geist_Mono({
  variable: "--font-geist-mono",
  subsets: ["latin"],
});

export const metadata: Metadata = {
  title: "Insurance Portal",
  description: "Insurance policyholder registration portal",
};

export default function RootLayout({ children }: LayoutProps<"/">) {
  return (
    <html
      lang="en"
      className={`${geistSans.variable} ${geistMono.variable} h-full antialiased`}
    >
      <body className="min-h-full flex flex-col">
        <RegistrationProvider>
          <RegistrationStepGuard>
            {children}
          </RegistrationStepGuard>
        </RegistrationProvider>
      </body>
    </html>
  );
}