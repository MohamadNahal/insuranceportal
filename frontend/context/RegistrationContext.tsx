"use client";

import { createContext, useContext, useState } from "react";

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
};

const RegistrationContext =
  createContext<RegistrationContextType | undefined>(
    undefined
  );

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