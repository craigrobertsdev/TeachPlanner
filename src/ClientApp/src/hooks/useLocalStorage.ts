import { useState } from "react";

export function useLocalStorage<T>(keyName: string, defaultValue: T) {
  const [storedValue, setStoredValue] = useState<T | null>(() => {
    try {
      const item = localStorage.getItem(keyName);

      return item ? (JSON.parse(item) as T) : defaultValue;
    } catch (error) {
      console.error(error);
      return defaultValue;
    }
  });

  const setValue = (value: T | null) => {
    try {
      localStorage.setItem(keyName, JSON.stringify(value));
    } catch (error) {
      console.error(error);
    }

    setStoredValue(value);
  };

  return [storedValue, setValue] as const;
}
