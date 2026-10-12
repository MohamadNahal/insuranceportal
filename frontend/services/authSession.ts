
const SESSION_KEY = "insurancePortalLoggedIn";

export function setLoggedIn() {
  if (typeof window !== "undefined") {
    window.sessionStorage.setItem(SESSION_KEY, "true");
  }
}

export function isLoggedIn() {
  if (typeof window === "undefined") {
    return false;
  }

  return window.sessionStorage.getItem(SESSION_KEY) === "true";
}

export function clearLoggedIn() {
  if (typeof window !== "undefined") {
    window.sessionStorage.removeItem(SESSION_KEY);
  }
}
