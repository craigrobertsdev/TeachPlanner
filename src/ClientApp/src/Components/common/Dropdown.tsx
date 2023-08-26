import { CSSProperties, useEffect, useRef, useState } from "react";
import { isStringArray } from "../../utils/typeGuards";

type DropdownProps<T extends string | string[] | undefined> = {
  options: string[];
  placeholder: string;
  onChange: (value: T) => void;
  defaultValue?: string;
  multiSelect?: boolean;
  styles?: CSSProperties;
  isSearchable?: boolean;
  disabled?: boolean;
};

type AcceptsStringArray = (arg: string[]) => void;
type AcceptsString = (arg: string) => void;

function Dropdown<T extends string | string[] | undefined>({
  multiSelect = false,
  isSearchable = false,
  placeholder,
  styles,
  options,
  defaultValue,
  onChange,
  disabled,
}: DropdownProps<T>) {
  const [showMenu, setShowMenu] = useState<boolean>(false);
  const [selectedValue, setSelectedValue] = useState<string | string[]>(multiSelect ? [] : "");
  const [searchValue, setSearchValue] = useState<string>("");
  const searchRef = useRef<HTMLInputElement>(null);
  const inputRef = useRef<HTMLDivElement>(null);
  const menuRef = useRef<HTMLDivElement>(null);
  const disabledStyle = "bg-gray-300 cursor-not-allowed";

  useEffect(() => {
    setSearchValue("");
    if (showMenu && searchRef.current) {
      searchRef.current.focus();
    }
  }, [showMenu]);

  useEffect(() => {
    const handler = (event: MouseEvent) => {
      if (showMenu && !inputRef.current?.contains(event.target as Node)) {
        if (!multiSelect) {
          setShowMenu(false);
        } else if (multiSelect && !menuRef.current?.contains(event.target as Node)) {
          setShowMenu(false);
        }
      }
    };

    window.addEventListener("click", handler);
    return () => window.removeEventListener("click", handler);
  });

  function handleInputSelect(): void {
    setShowMenu(!showMenu);
  }

  function getDisplay(): string | JSX.Element {
    if (!selectedValue || selectedValue.length === 0) {
      return defaultValue ? defaultValue : placeholder;
    }

    if (isStringArray(selectedValue)) {
      return (
        <div className="flex flex-wrap gap-1">
          {selectedValue.map((value) => (
            <div key={value} className="flex items-center bg-gray-300 px-1 py-0.5 rounded-md">
              {value}
              <span onClick={(e) => onTagRemove(e, value)} className="flex items-center">
                <CloseIcon />
              </span>
            </div>
          ))}
        </div>
      );
    }

    return selectedValue;
  }

  function removeOption(option: string): string[] | undefined {
    if (isStringArray(selectedValue)) {
      return selectedValue.filter((value) => value !== option);
    }
    return;
  }

  function onTagRemove(event: React.MouseEvent, option: string): void {
    event.stopPropagation();
    const newValue = removeOption(option);
    if (isStringArray(selectedValue) && newValue) {
      setSelectedValue(newValue);
      (onChange as AcceptsStringArray)(newValue);
    } else if (isStringArray(selectedValue)) {
      setSelectedValue([]);
      (onChange as AcceptsStringArray)([]);
    }
  }

  function onItemClick(option: string): void {
    let newValue: string | string[];
    if (isStringArray(selectedValue)) {
      if (selectedValue.findIndex((o) => o === option) >= 0) {
        newValue = removeOption(option) as string[];
      } else {
        newValue = [...selectedValue, option];
      }
      (onChange as AcceptsStringArray)(newValue);
    } else {
      newValue = option;
      (onChange as AcceptsString)(option);
    }

    setSelectedValue(newValue);
  }

  function isSelected(option: string): boolean {
    if (isStringArray(selectedValue)) {
      return selectedValue.filter((o) => o === option).length > 0;
    }

    if (!selectedValue) {
      return false;
    }

    return selectedValue === option;
  }

  function onSearch(event: React.ChangeEvent<HTMLInputElement>): void {
    setSearchValue(event.target.value);
  }

  function getOptions(): string[] {
    if (!searchValue) {
      return options;
    }

    return options.filter((option) => option.toLowerCase().includes(searchValue.toLowerCase()));
  }

  return (
    <div
      className={`border border-darkGreen relative text-left rounded-md min-w-[9rem] bg-primaryHover ${styles ? styles : ""} ${
        disabled ? disabledStyle : ""
      }`}>
      <div ref={inputRef} onClick={handleInputSelect} className="flex justify-between items-center select-none w-full px-2 py-1">
        <div className="flex flex-wrap gap-1 max-w-full">{getDisplay()}</div>
        <div>
          <Icon />
        </div>
      </div>
      {showMenu && (
        <div ref={menuRef} className="absolute z-50 border border-darkGreen translate-y-1 overflow-auto bg-primaryHover w-max max-h-48 rounded-md">
          {isSearchable && (
            <div>
              <input onChange={onSearch} value={searchValue} ref={searchRef} className="w-full box-border p-2 border border-darkGreen rounded-t-md" />
            </div>
          )}
          {getOptions().map((option) => (
            <div
              key={option}
              onClick={() => onItemClick(option)}
              className={`p-1 cursor-pointer hover:bg-sageHover ${isSelected(option) && "bg-sage text-primary"}`}>
              {option}
            </div>
          ))}
        </div>
      )}
    </div>
  );
}

export default Dropdown;

const Icon = () => {
  return (
    <svg height="20" width="20" viewBox="0 0 20 20">
      <path d="M4.516 7.548c0.436-0.446 1.043-0.481 1.576 0l3.908 3.747 3.908-3.747c0.533-0.481 1.141-0.446 1.574 0 0.436 0.445 0.408 1.197 0 1.615-0.406 0.418-4.695 4.502-4.695 4.502-0.217 0.223-0.502 0.335-0.787 0.335s-0.57-0.112-0.789-0.335c0 0-4.287-4.084-4.695-4.502s-0.436-1.17 0-1.615z"></path>
    </svg>
  );
};

const CloseIcon = () => {
  return (
    <svg height="20" width="20" viewBox="0 0 20 20">
      <path d="M14.348 14.849c-0.469 0.469-1.229 0.469-1.697 0l-2.651-3.030-2.651 3.029c-0.469 0.469-1.229 0.469-1.697 0-0.469-0.469-0.469-1.229 0-1.697l2.758-3.15-2.759-3.152c-0.469-0.469-0.469-1.228 0-1.697s1.228-0.469 1.697 0l2.652 3.031 2.651-3.031c0.469-0.469 1.228-0.469 1.697 0s0.469 1.229 0 1.697l-2.758 3.152 2.758 3.15c0.469 0.469 0.469 1.229 0 1.698z"></path>
    </svg>
  );
};
