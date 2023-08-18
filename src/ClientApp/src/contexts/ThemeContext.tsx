import { ReactNode, createContext, useContext, useEffect, useState } from "react";

type ThemeContextType = {
  subjectColors: SubjectColors;
};

type ThemeProviderProps = {
  children: ReactNode;
};

type SubjectColors = {
  [key: string]: string;
};

const ThemeContext = createContext<ThemeContextType>({} as ThemeContextType);

export function ThemeProvider({ children }: ThemeProviderProps) {
  const [subjectColors, setSubjectColors] = useState<SubjectColors>({});
  useEffect(() => {
    const colors = {
      maths: "#FCCF03",
      english: "#85D42A",
      hass: "2AD48D",
      health: "AD3BFF",
    } as SubjectColors;

    setSubjectColors(colors);
  }, []);

  return <ThemeContext.Provider value={{ subjectColors: subjectColors }}>{children}</ThemeContext.Provider>;
}

export default ThemeProvider;

export function useThemeContext() {
  return useContext(ThemeContext);
}
