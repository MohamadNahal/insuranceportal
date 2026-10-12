"use client";

import { useEffect } from "react";
import { usePathname, useRouter } from "next/navigation";
import { useRegistration } from "@/context/RegistrationContext";

const requiredSteps: Record<string, number> = {
  "/register/account": 1,
  "/register/security": 2,
  "/register/contact": 3,
  "/register/success": 4,
};

export default function RegistrationStepGuard({
  children,
}: {
  children: React.ReactNode;
}) {
  const pathname = usePathname();
  const router = useRouter();
  const { completedStep } = useRegistration();

  const requiredStep = requiredSteps[pathname];
  const isBlocked =
    requiredStep !== undefined &&
    completedStep < requiredStep;

  useEffect(() => {
    if (!isBlocked) {
      return;
    }

    if (completedStep < 1) {
      router.replace("/register/personal");
    } else if (completedStep < 2) {
      router.replace("/register/account");
    } else if (completedStep < 3) {
      router.replace("/register/security");
    } else {
      router.replace("/register/contact");
    }
  }, [completedStep, isBlocked, router]);

  if (isBlocked) {
    return null;
  }

  return children;
}