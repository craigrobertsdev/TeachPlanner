import { ReactNode, createContext, useContext, useEffect, useMemo, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import teacherService from "../services/TeacherService";
import { useLocalStorage } from "../hooks/useLocalStorage";

type AuthContextType = {
  teacher: Teacher | null;
  token: string | null;
  loading: boolean;
  error?: any;
  login: (email: string, password: string) => void;
  register: (email: string, firstName: string, lastName: string, password: string, confirmedPassword: string) => void;
  logout: () => void;
};

type AuthProviderProps = {
  children: ReactNode;
  teacherData: Teacher;
};

const AuthContext = createContext<AuthContextType>({} as AuthContextType);

export function AuthProvider({ children, teacherData }: AuthProviderProps) {
  const [teacher, setTeacher] = useLocalStorage<Teacher | null>("teacher", teacherData);
  const [token, setToken] = useLocalStorage<string>("token", "");
  const [error, setError] = useState<any>();
  const [loading, setLoading] = useState<boolean>(false);
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

      const response = await teacherService.login(email, password);

      setTeacher(response.teacher);
      setToken(response.token);

      setLoading(false);
      navigate("/");
    } catch (error) {
      setError(error);
      setLoading(false);
    }
  }

  async function register(email: string, firstName: string, lastName: string, password: string, confirmedPassword: string) {
    try {
      setLoading(true);

      const response = await teacherService.register(email, firstName, lastName, password, confirmedPassword);

      setTeacher(response.teacher);
      setToken(response.token);

      setLoading(false);
      navigate("/", { replace: true });
    } catch (error) {
      setError(error);
      setLoading(false);
    }
  }

  function logout() {
    setTeacher(null);
    setToken("");
    navigate("/", { replace: true });
  }

  const memoedValue = useMemo(
    () => ({
      teacher,
      token,
      loading,
      error,
      login,
      register,
      logout,
    }),
    [teacher, token, loading, error]
  );

  return <AuthContext.Provider value={memoedValue}>{children}</AuthContext.Provider>;
}

export default function useAuth() {
  return useContext(AuthContext);
}
