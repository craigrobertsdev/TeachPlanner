// use this to decode a token and get the user's information out of it
import jwtDecode, { JwtPayload } from "jwt-decode";

// create a new class to instantiate for a user
class AuthService {
  // check if user's logged in
  loggedIn() {
    // Checks if there is a saved token and it's still valid
    const token = this.getToken();
    return !!token && !this.isTokenExpired(token); // handwaiving here
  }

  // check if token is expired
  isTokenExpired(token: string) {
    try {
      const decoded = jwtDecode(token) as JwtPayload;
      if (decoded.exp! < Date.now() / 1000) {
        return true;
      } else return false;
    } catch (err) {
      return false;
    }
  }

  getToken(): string | null {
    // Retrieves the user token from localStorage
    const user = localStorage.getItem("user");
    if (user) {
      return JSON.parse(user).token;
    }

    return null;
  }

  logout() {
    // Clear user token and profile data from localStorage
    localStorage.removeItem("user");
  }
}

export default new AuthService();
