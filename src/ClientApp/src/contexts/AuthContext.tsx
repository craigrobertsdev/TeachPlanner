import {
  ReactNode,
  createContext,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import { useLocation } from "react-router-dom";
import * as userService from "../assets/services/UserService";
import { useLocalStorage } from "../hooks/useLocalStorage";

type AuthContextType = {
  user: User;
  loading: boolean;
  error?: any;
  login: (email: string, password: string) => void;
  register: (
    email: string,
    firstName: string,
    lastName: string,
    password: string
  ) => void;
  logout: () => void;
};

const AuthContext = createContext<AuthContextType>({} as AuthContextType);

export function AuthProvider({ children }: { children: ReactNode }) {
  const [user, setUser] = useState<User>({} as User);
  const [error, setError] = useState<any>();
  const [loading, setLoading] = useState<boolean>(false);
  const [loadingInitial, setLoadingInitial] = useState<boolean>(true);
  const { setItem, getItem } = useLocalStorage();
  const location = useLocation();

  useEffect(() => {
    if (error) {
      setError(undefined);
    }
  }, [location.pathname]);

  useEffect(() => {
    const user = getItem("user");
    if (user) {
      setUser(JSON.parse(user));
    }

    setLoadingInitial(false);
  }, []);

  async function login(email: string, password: string) {
    try {
      setLoading(true);

      const response = await userService.login(email, password);

      if (response) {
        setUser(response);
      }

      setLoading(false);
    } catch (error) {
      setError(error);
      setLoading(false);
    }
  }

  async function register(
    email: string,
    firstName: string,
    lastName: string,
    password: string
  ) {
    try {
      setLoading(true);

      const response = await userService.register(
        email,
        firstName,
        lastName,
        password
      );

      if (response) {
        setUser(response);
      }

      setLoading(false);
    } catch (error) {
      setError(error);
      setLoading(false);
    }
  }

  function logout() {}

  const memoedValue = useMemo(
    () => ({
      user,
      loading,
      error,
      login,
      register,
      logout,
    }),
    [user, loading, error]
  );

  return (
    <AuthContext.Provider value={memoedValue}>
      {!loadingInitial && children}
    </AuthContext.Provider>
  );
}

export default function useAuth() {
  return useContext(AuthContext);
}
