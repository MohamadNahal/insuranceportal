"use client";

import { useState } from "react";

type PersonalInformation = {
  ssn: string;
  policyNumber: string;
  firstName: string;
  lastName: string;
  dateOfBirth: string;
  zipCode: string;
};

export default function Home() {
  const [step, setStep] = useState<
  "personal" | "account" | "security" | "contact" | "login" | "welcome" >("login") ;

  const [personalInformation, setPersonalInformation] =
    useState<PersonalInformation>({
      ssn: "",
      policyNumber: "",
      firstName: "",
      lastName: "",
      dateOfBirth: "",
      zipCode: "",
    });

  const [accountInformation, setAccountInformation] = useState({
    username: "",
    password: "",
    confirmPassword: "",
  });

  const [securityInformation, setSecurityInformation] = useState({
    nickname: "",
    childhoodHero: "",
  });

  const [contactInformation, setContactInformation] = useState({
    email: "",
    phone: "",
    streetAddress: "",
    streetAddressLine2: "",
    city: "",
    state: "",
    zipCode: "",
    country: "USA",
  });

  const [message, setMessage] = useState("");
  const [loading, setLoading] = useState(false);
  const [loginInformation, setLoginInformation] = useState({
  username: "",
  password: "",
});

  const handlePersonalChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setPersonalInformation({
      ...personalInformation,
      [event.target.name]: event.target.value,
    });
  };

  const handleAccountChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setAccountInformation({
      ...accountInformation,
      [event.target.name]: event.target.value,
    });
  };

  const handleSecurityChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setSecurityInformation({
      ...securityInformation,
      [event.target.name]: event.target.value,
    });
  };

  const handleContactChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setContactInformation({
      ...contactInformation,
      [event.target.name]: event.target.value,
    });
  };
  const handleLoginChange = (
  event: React.ChangeEvent<HTMLInputElement>
) => {
  setLoginInformation({
    ...loginInformation,
    [event.target.name]: event.target.value,
  });
};

  const validatePolicyholder = async (
    event: React.FormEvent<HTMLFormElement>
  ) => {
    event.preventDefault();

    setLoading(true);
    setMessage("");

    try {
      const response = await fetch(
        "http://localhost:5200/api/Auth/validate-policyholder",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(personalInformation),
        }
      );

      const data = await response.json();

      if (response.ok) {
        setMessage("");
        setStep("account");
      } else {
        setMessage(
          data.message ||
            "Policyholder information could not be validated."
        );
      }
    } catch {
      setMessage("Unable to connect to the backend.");
    } finally {
      setLoading(false);
    }
  };

  const registerCustomer = async (
    event: React.FormEvent<HTMLFormElement>
  ) => {
    event.preventDefault();

    if (
      accountInformation.password !==
      accountInformation.confirmPassword
    ) {
      setMessage("Passwords do not match.");
      return;
    }

    setLoading(true);
    setMessage("");

    const registerRequest = {
      personalInformation,
      accountInformation,
      securityInformation,
      contactInformation,
    };

    try {
      const response = await fetch(
        "http://localhost:5200/api/Auth/register",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(registerRequest),
        }
      );

      const data = await response.json();

      if (response.ok) {
        setMessage("");
        setStep("welcome");
      } else {
        setMessage(
        data.message || "Invalid username or password."
      );
}
    } catch {
      setMessage("Unable to connect to the backend.");
    } finally {
      setLoading(false);
    }
  };
  const loginCustomer = async (
  event: React.FormEvent<HTMLFormElement>
) => {
  event.preventDefault();

  setLoading(true);
  setMessage("");

  try {
    const response = await fetch(
      "http://localhost:5200/api/Auth/login",
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(loginInformation),
      }
    );

    const data = await response.json();

    if (response.ok) {
  setMessage("");
  setStep("welcome");
} else {
      setMessage(
        data.message || "Invalid username or password."
      );
    }
  } catch {
    setMessage("Unable to connect to the backend.");
  } finally {
    setLoading(false);
  }
};

  if (step === "account") {
  return (
    <main style={pageStyle}>
      <div style={containerStyle}>
        <h1 style={titleStyle}>Insurance Portal</h1>

        <div style={stepStyle}>
          <span>1. Personal Info</span>
          <span style={activeStepStyle}>2. Account</span>
          <span>3. Security</span>
          <span>4. Contact</span>
        </div>

        <h2 style={subtitleStyle}>
          Step 2: Account Information
        </h2>

        <form
          onSubmit={(event) => {event.preventDefault();

            if (
              accountInformation.password !==
              accountInformation.confirmPassword
            ) {
              setMessage("Passwords do not match.");
              return;
            }

            setMessage("");
            setStep("security");
          }}
        >
          <div style={fieldStyle}>
            <label>Username</label>
            <input
              name="username"
              value={accountInformation.username}
              onChange={handleAccountChange}
              required
              style={inputStyle}
            />
          </div>

          <div style={fieldStyle}>
            <label>Password</label>
            <input
              type="password"
              name="password"
              value={accountInformation.password}
              onChange={handleAccountChange}
              required
              style={inputStyle}
            />
          </div>

          <div style={fieldStyle}>
            <label>Confirm Password</label>
            <input
              type="password"
              name="confirmPassword"
              value={accountInformation.confirmPassword}
              onChange={handleAccountChange}
              required
              style={inputStyle}
            />
          </div>

          <button
            type="submit"
            style={buttonStyle}
          >
            Continue
          </button>

          {message && (
            <p style={messageStyle}>
              {message}
            </p>
          )}
        </form>
      </div>
    </main>
  );
}
if (step === "security") {
  return (
    <main style={pageStyle}>
      <div style={containerStyle}>
        <h1 style={titleStyle}>Insurance Portal</h1>

        <div style={stepStyle}>
          <span>1. Personal Info</span>
          <span>2. Account</span>
          <span style={activeStepStyle}>3. Security</span>
          <span>4. Contact</span>
        </div>

        <h2 style={subtitleStyle}>
          Step 3: Security Information
        </h2>

        <form
          onSubmit={(event) => {
            event.preventDefault();

            setMessage("");
            setStep("contact");
          }}
        >
          <div style={fieldStyle}>
            <label>Nickname</label>
            <input
              name="nickname"
              value={securityInformation.nickname}
              onChange={handleSecurityChange}
              required
              style={inputStyle}
            />
          </div>

          <div style={fieldStyle}>
            <label>Childhood Hero</label>
            <input
              name="childhoodHero"
              value={securityInformation.childhoodHero}
              onChange={handleSecurityChange}
              required
              style={inputStyle}
            />
          </div>

          <button
            type="submit"
            style={buttonStyle}
          >
            Continue
          </button>

          {message && (
            <p style={messageStyle}>
              {message}
            </p>
          )}
        </form>
      </div>
    </main>
  );
}
if (step === "contact") {
  return (
    <main style={pageStyle}>
      <div style={containerStyle}>
        <h1 style={titleStyle}>Insurance Portal</h1>

        <div style={stepStyle}>
          <span>1. Personal Info</span>
          <span>2. Account</span>
          <span>3. Security</span>
          <span style={activeStepStyle}>4. Contact</span>
        </div>

        <h2 style={subtitleStyle}>
          Step 4: Contact Information
        </h2>

        <form onSubmit={registerCustomer}>
          <div style={fieldStyle}>
            <label>Email</label>
            <input
              type="email"
              name="email"
              value={contactInformation.email}
              onChange={handleContactChange}
              required
              style={inputStyle}
            />
          </div>

          <div style={fieldStyle}>
            <label>Phone</label>
            <input
              name="phone"
              value={contactInformation.phone}
              onChange={handleContactChange}
              required
              style={inputStyle}
            />
          </div>

          <div style={fieldStyle}>
            <label>Street Address</label>
            <input
              name="streetAddress"
              value={contactInformation.streetAddress}
              onChange={handleContactChange}
              required
              style={inputStyle}
            />
          </div>

          <div style={fieldStyle}>
            <label>Street Address Line 2</label>
            <input
              name="streetAddressLine2"
              value={contactInformation.streetAddressLine2}
              onChange={handleContactChange}
              style={inputStyle}
            />
          </div>

          <div style={fieldStyle}>
            <label>City</label>
            <input
              name="city"
              value={contactInformation.city}
              onChange={handleContactChange}
              required
              style={inputStyle}
            />
          </div>

          <div style={fieldStyle}>
            <label>State</label>
            <input
              name="state"
              value={contactInformation.state}
              onChange={handleContactChange}
              required
              style={inputStyle}
            />
          </div>

          <div style={fieldStyle}>
            <label>ZIP Code</label>
            <input
              name="zipCode"
              value={contactInformation.zipCode}
              onChange={handleContactChange}
              maxLength={5}
              required
              style={inputStyle}
            />
          </div>

          <div style={fieldStyle}>
            <label>Country</label>
            <input
              name="country"
              value={contactInformation.country}
              onChange={handleContactChange}
              required
              style={inputStyle}
            />
          </div>

          <button
            type="submit"
            disabled={loading}
            style={buttonStyle}
          >
            {loading ? "Registering..." : "Register"}
          </button>

          {message && (
            <p style={messageStyle}>
              {message}
            </p>
          )}
        </form>
      </div>
    </main>
  );
}
if (step === "login") {
  return (
    <main style={pageStyle}>
      <div style={containerStyle}>
        <h1 style={titleStyle}>Insurance Portal</h1>

        <h2 style={subtitleStyle}>
          Customer Login
        </h2>

        <form onSubmit={loginCustomer}>
          <div style={fieldStyle}>
            <label>Username</label>
            <input
              name="username"
              value={loginInformation.username}
              onChange={handleLoginChange}
              required
              style={inputStyle}
            />
          </div>

          <div style={fieldStyle}>
            <label>Password</label>
            <input
              type="password"
              name="password"
              value={loginInformation.password}
              onChange={handleLoginChange}
              required
              style={inputStyle}
            />
          </div>

          <button
            type="submit"
            disabled={loading}
            style={buttonStyle}
          >
            {loading ? "Logging in..." : "Login"}
          </button>

          <div
            style={{
              textAlign: "center",
              marginTop: "20px",
            }}
          >
            <span>New User? </span>

            <button
              type="button"
              onClick={() => {
                setMessage("");
                setStep("personal");
              }}
              style={{
                background: "none",
                border: "none",
                padding: 0,
                color: "#1d4ed8",
                fontWeight: "bold",
                cursor: "pointer",
                fontSize: "15px",
              }}
            >
              Register here
            </button>
          </div>

          {message && (
            <p style={messageStyle}>
              {message}
            </p>
          )}
        </form>
      </div>
    </main>
  );
}
if (step === "welcome") {
  return (
    <main
      style={{
        minHeight: "100vh",
        background: "white",
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
      }}
    >
      <h1
        style={{
          fontSize: "36px",
          color: "#1d4ed8",
        }}
      >
        Welcome to Insurance Portal
      </h1>
    </main>
  );
}

  return (
    <main style={pageStyle}>
      <div style={containerStyle}>
        <h1 style={titleStyle}>Insurance Portal</h1>

        <div style={stepStyle}>
          <span style={activeStepStyle}>1. Personal Info</span>
          <span>2. Account</span>
          <span>3. Security</span>
          <span>4. Contact</span>
        </div>

<h2 style={subtitleStyle}>
  Step 1: Personal Information
</h2>

        <form onSubmit={validatePolicyholder}>
          <div style={fieldStyle}>
            <label>SSN Last 4</label>
            <input
              name="ssn"
              value={personalInformation.ssn}
              onChange={handlePersonalChange}
              maxLength={4}
              required
              style={inputStyle}
            />
          </div>

          <div style={fieldStyle}>
            <label>Policy Number</label>
            <input
              name="policyNumber"
              value={personalInformation.policyNumber}
              onChange={handlePersonalChange}
              required
              style={inputStyle}
            />
          </div>

          <div style={fieldStyle}>
            <label>First Name</label>
            <input
              name="firstName"
              value={personalInformation.firstName}
              onChange={handlePersonalChange}
              required
              style={inputStyle}
            />
          </div>

          <div style={fieldStyle}>
            <label>Last Name</label>
            <input
              name="lastName"
              value={personalInformation.lastName}
              onChange={handlePersonalChange}
              required
              style={inputStyle}
            />
          </div>

          <div style={fieldStyle}>
            <label>Date of Birth</label>
            <input
              name="dateOfBirth"
              value={personalInformation.dateOfBirth}
              onChange={handlePersonalChange}
              placeholder="MM/DD/YYYY"
              required
              style={inputStyle}
            />
          </div>

          <div style={fieldStyle}>
            <label>ZIP Code</label>
            <input
              name="zipCode"
              value={personalInformation.zipCode}
              onChange={handlePersonalChange}
              maxLength={5}
              required
              style={inputStyle}
            />
          </div>

          <button
  type="submit"
  disabled={loading}
  style={buttonStyle}
>
  {loading ? "Validating..." : "Continue"}
</button>
{message && (
  <p style={messageStyle}>
    {message}
  </p>
)}
        </form>
      </div>
    </main>
  );
}

const pageStyle = {
  minHeight: "100vh",
  background: "#f4f6f8",
  display: "flex",
  justifyContent: "center",
  alignItems: "center",
  padding: "30px",
};

const containerStyle = {
  width: "100%",
  maxWidth: "600px",
  background: "white",
  padding: "35px",
  borderRadius: "12px",
  boxShadow: "0 4px 20px rgba(0,0,0,0.1)",
};

const titleStyle = {
  textAlign: "center" as const,
  marginBottom: "10px",
};

const subtitleStyle = {
  textAlign: "center" as const,
  color: "#555",
  marginBottom: "30px",
};
const stepStyle = {
  display: "flex",
  justifyContent: "space-between",
  marginBottom: "30px",
  padding: "12px",
  background: "#f1f5f9",
  borderRadius: "8px",
  fontSize: "14px",
};

const activeStepStyle = {
  color: "#1d4ed8",
  fontWeight: "bold",
};

const fieldStyle = {
  marginBottom: "18px",
};

const inputStyle = {
  width: "100%",
  padding: "11px",
  marginTop: "6px",
  border: "1px solid #ccc",
  borderRadius: "6px",
  fontSize: "15px",
  boxSizing: "border-box" as const,
};

const buttonStyle = {
  width: "100%",
  padding: "13px",
  background: "#1d4ed8",
  color: "white",
  border: "none",
  borderRadius: "6px",
  fontSize: "16px",
  cursor: "pointer",
};

const messageStyle = {
  marginTop: "20px",
  textAlign: "center" as const,
  fontWeight: "bold",
};