import { ReactNode, createContext, useContext, useEffect, useMemo, useState } from "react";

type ThemeContextType = {
  cancelModalOpen: boolean;
  setCancelModalOpen: React.Dispatch<React.SetStateAction<boolean>>;
};

type ThemeProviderProps = {
  children: ReactNode;
};

type SubjectColors = {
  [key: string]: string;
};

const ThemeContext = createContext<ThemeContextType>({} as ThemeContextType);

export function ThemeProvider({ children }: ThemeProviderProps) {
  const [cancelModalOpen, setCancelModalOpen] = useState<boolean>(false);

  return <ThemeContext.Provider value={{ cancelModalOpen, setCancelModalOpen }}>{children}</ThemeContext.Provider>;
}

export default ThemeProvider;

export function useThemeContext() {
  return useContext(ThemeContext);
}
