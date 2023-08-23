import { ReactNode, createContext, useContext, useEffect, useMemo, useState } from "react";

type ThemeContextType = {
  cancelModalOpen: boolean;
  setCancelModalOpen: React.Dispatch<React.SetStateAction<boolean>>;
  dialogOpenStyle: string[];
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
  const [dialogOpenStyle, setDialogOpenStyle] = useState<string[]>(["filter", "blur-xs", "opacity-50", "pointer-events-none"]);

  return <ThemeContext.Provider value={{ cancelModalOpen, setCancelModalOpen, dialogOpenStyle }}>{children}</ThemeContext.Provider>;
}

export default ThemeProvider;

export function useThemeContext() {
  return useContext(ThemeContext);
}
