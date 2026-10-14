"use client";

import { createContext, useContext, useEffect, useState } from "react";
type PersonalInformation = {
  ssn: string;
  policyNumber: string;
  firstName: string;
  lastName: string;
  dateOfBirth: string;
  zipCode: string;
};

type AccountInformation = {
  username: string;
  password: string;
  confirmPassword: string;
};

type SecurityInformation = {
  nickname: string;
  childhoodHero: string;
};

type ContactInformation = {
  email: string;
  phone: string;
  streetAddress: string;
  streetAddressLine2: string;
  city: string;
  state: string;
  zipCode: string;
  country: string;
};

type RegistrationContextType = {
  personalInformation: PersonalInformation;
  setPersonalInformation: React.Dispatch<
    React.SetStateAction<PersonalInformation>
  >;
  accountInformation: AccountInformation;
  setAccountInformation: React.Dispatch<
    React.SetStateAction<AccountInformation>
  >;
  securityInformation: SecurityInformation;
  setSecurityInformation: React.Dispatch<
    React.SetStateAction<SecurityInformation>
  >;
  contactInformation: ContactInformation;
  setContactInformation: React.Dispatch<
    React.SetStateAction<ContactInformation>
  >;
  completedStep: number;
  setCompletedStep: React.Dispatch<
    React.SetStateAction<number>
  >;
  draftEnabled: boolean;
  setDraftEnabled: React.Dispatch<React.SetStateAction<boolean>>;
  resetRegistration: () => void;
  
};

const RegistrationContext =
  createContext<RegistrationContextType | undefined>(undefined);

export function RegistrationProvider({
  children,
}: {
  children: React.ReactNode;
}) {
  const [personalInformation, setPersonalInformation] =
    useState<PersonalInformation>({
      ssn: "",
      policyNumber: "",
      firstName: "",
      lastName: "",
      dateOfBirth: "",
      zipCode: "",
    });

  const [accountInformation, setAccountInformation] =
    useState<AccountInformation>({
      username: "",
      password: "",
      confirmPassword: "",
    });

  const [securityInformation, setSecurityInformation] =
    useState<SecurityInformation>({
      nickname: "",
      childhoodHero: "",
    });

  const [contactInformation, setContactInformation] =
    useState<ContactInformation>({
      email: "",
      phone: "",
      streetAddress: "",
      streetAddressLine2: "",
      city: "",
      state: "",
      zipCode: "",
      country: "USA",
    });

  const [completedStep, setCompletedStep] = useState(0);
  
  const resetRegistration = () => {
    setPersonalInformation({
      ssn: "",
      policyNumber: "",
      firstName: "",
      lastName: "",
      dateOfBirth: "",
      zipCode: "",
    });

    setAccountInformation({
      username: "",
      password: "",
      confirmPassword: "",
    });

    setSecurityInformation({
      nickname: "",
      childhoodHero: "",
    });

    setContactInformation({
      email: "",
      phone: "",
      streetAddress: "",
      streetAddressLine2: "",
      city: "",
      state: "",
      zipCode: "",
      country: "USA",
    });

    setCompletedStep(0);
    sessionStorage.removeItem("registrationDraft");
  };

  const [draftLoaded, setDraftLoaded] = useState(false);
  const [draftEnabled, setDraftEnabled] = useState(true);

  
  useEffect(() => {
    const savedDraft = sessionStorage.getItem("registrationDraft");

    if (savedDraft) {
      try {
        const draft = JSON.parse(savedDraft);

        if (draft.personalInformation) {
          setPersonalInformation((current) => ({
            ...current,
            ...draft.personalInformation,
            ssn: "",
            policyNumber: "",
          }));
        }

        if (draft.contactInformation) {
          setContactInformation((current) => ({
            ...current,
            ...draft.contactInformation,
          }));
        }

        if (typeof draft.completedStep === "number") {
          setCompletedStep(
            Math.min(4, Math.max(0, draft.completedStep))
          );
        }
      } catch {
        sessionStorage.removeItem("registrationDraft");
      }
    }

    setDraftLoaded(true);
  }, []);

  
 
  useEffect(() => {
    if (!draftLoaded || !draftEnabled) return;

    const draft = {
      personalInformation: {
        firstName: personalInformation.firstName,
        lastName: personalInformation.lastName,
        dateOfBirth: personalInformation.dateOfBirth,
        zipCode: personalInformation.zipCode,
      },
      contactInformation,
      completedStep,
    };

    sessionStorage.setItem(
      "registrationDraft",
      JSON.stringify(draft)
    );
  }, [
    personalInformation,
    contactInformation,
    completedStep,
    draftLoaded,
    draftEnabled,
  ]);




  return (
    <RegistrationContext.Provider
      value={{
        personalInformation,
        setPersonalInformation,
        accountInformation,
        setAccountInformation,
        securityInformation,
        setSecurityInformation,
        contactInformation,
        setContactInformation,
        completedStep,
        setCompletedStep,
        draftEnabled,
        setDraftEnabled,
        resetRegistration,
      }}
    >
      {children}
    </RegistrationContext.Provider>
  );
}

export function useRegistration() {
  const context = useContext(RegistrationContext);

  if (!context) {
    throw new Error(
      "useRegistration must be used inside RegistrationProvider"
    );
  }

  return context;
}