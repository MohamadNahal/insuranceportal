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

export async function registerUser(
  request: RegisterRequest
) {
  const response = await fetch(
    "http://localhost:5200/api/Auth/register",
    {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(request),
    }
  );

  const data = await response.json();

  return {
    response,
    data,
  };
}

type LoginRequest = {
  username: string;
  password: string;
};

export async function loginUser(
  request: LoginRequest
) {
  const response = await fetch(
    "http://localhost:5200/api/Auth/login",
    {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(request),
    }
  );

  const data = await response.json();

  return {
    response,
    data,
  };
}