
const API_BASE_URL =
  process.env.NEXT_PUBLIC_API_BASE_URL ?? "http://localhost:5200/api";

type RegisterRequest = {
  personalInformation: {
    ssn: string;
    policyNumber: string;
    firstName: string;
    lastName: string;
    dateOfBirth: string;
    zipCode: string;
  };
  accountInformation: {
    username: string;
    password: string;
    confirmPassword: string;
  };
  securityInformation: {
    nickname: string;
    childhoodHero: string;
  };
  contactInformation: {
    email: string;
    phone: string;
    streetAddress: string;
    streetAddressLine2: string;
    city: string;
    state: string;
    zipCode: string;
    country: string;
  };
};

type PolicyholderValidationRequest = {
  ssn: string;
  policyNumber: string;
  firstName: string;
  lastName: string;
  dateOfBirth: string;
  zipCode: string;
};

type LoginRequest = {
  username: string;
  password: string;
};

async function readResponse(response: Response) {
  try {
    return await response.json();
  } catch {
    return {
      message: response.ok
        ? "Request completed."
        : "The request could not be completed. Please try again.",
    };
  }
}

export async function validatePolicyholder(
  request: PolicyholderValidationRequest
) {
  const response = await fetch(
    `${API_BASE_URL}/Auth/validate-policyholder`,
    {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(request),
    }
  );

  const data = await readResponse(response);

  return {
    response,
    data,
  };
}

export async function registerUser(request: RegisterRequest) {
  const response = await fetch(`${API_BASE_URL}/Auth/register`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(request),
  });

  const data = await readResponse(response);

  return {
    response,
    data,
  };
}

export async function loginUser(request: LoginRequest) {
  const response = await fetch(`${API_BASE_URL}/Auth/login`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(request),
  });

  const data = await readResponse(response);

  return {
    response,
    data,
  };
}
