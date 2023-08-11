import {
  ReactNode,
  createContext,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import { useLocation, useNavigate } from "react-router-dom";
import * as userService from "../services/UserService";
import { useLocalStorage } from "../hooks/useLocalStorage";

type AuthContextType = {
  user: User | null;
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

type AuthProviderProps = {
  children: ReactNode;
  userData: User;
};

const AuthContext = createContext<AuthContextType>({} as AuthContextType);

export function AuthProvider({ children, userData }: AuthProviderProps) {
  const [user, setUser] = useLocalStorage<User | null>("user", userData);
  const [error, setError] = useState<any>();
  const [loading, setLoading] = useState<boolean>(false);
  const [loadingInitial, setLoadingInitial] = useState<boolean>(true);
  const routerLocation = useLocation();
  const navigate = useNavigate();

  useEffect(() => {
    if (error) {
      setError(undefined);
    }
  }, [routerLocation.pathname]);

  async function login(email: string, password: string) {
    try {
      setLoading(true);

      const response = await userService.login(email, password);

      setUser(response);

      setLoading(false);
      navigate("/");
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

      setUser(response);

      setLoading(false);
      navigate("/", { replace: true });
    } catch (error) {
      setError(error);
      setLoading(false);
    }
  }

  function logout() {
    setUser(null);
    navigate("/", { replace: true });
  }

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
    <AuthContext.Provider value={memoedValue}>{children}</AuthContext.Provider>
  );
}

export default function useAuth() {
  return useContext(AuthContext);
}
