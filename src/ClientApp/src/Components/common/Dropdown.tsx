import { useEffect, useRef, useState } from "react";
import { isStringArray } from "../../utils/typeGuards";

type DropdownProps = {
  multiSelect?: boolean;
  isSearchable?: boolean;
  options: string[];
  placeholder: string;
  styles?: string;
  onChange: (value: string | string[] | undefined) => void;
};

function Dropdown({ multiSelect = false, isSearchable = false, placeholder, styles, options, onChange }: DropdownProps) {
  const [showMenu, setShowMenu] = useState<boolean>(false);
  const [selectedValue, setSelectedValue] = useState<string | string[]>(multiSelect ? [] : "");
  const [searchValue, setSearchValue] = useState<string>("");
  const searchRef = useRef<HTMLInputElement>(null);
  const inputRef = useRef<HTMLDivElement>(null);
  const menuRef = useRef<HTMLDivElement>(null);

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

  function handleInputSelect() {
    setShowMenu(!showMenu);
  }

  function getDisplay() {
    if (!selectedValue || selectedValue.length === 0) {
      return placeholder;
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

  function removeOption(option: string) {
    if (isStringArray(selectedValue)) {
      return selectedValue.filter((value) => value !== option);
    }
  }

  function onTagRemove(event: React.MouseEvent, option: string) {
    event.stopPropagation();
    const newValue = removeOption(option);
    if (isStringArray(selectedValue) && newValue) {
      setSelectedValue(newValue);
    } else if (isStringArray(selectedValue)) {
      setSelectedValue([]);
    }

    onChange(newValue);
  }

  function onItemClick(option: string) {
    let newValue: string | string[];
    if (isStringArray(selectedValue)) {
      if (selectedValue.findIndex((o) => o === option) >= 0) {
        newValue = removeOption(option) as string[];
      } else {
        newValue = [...selectedValue, option];
      }
    } else {
      newValue = option;
    }

    setSelectedValue(newValue);
    onChange(newValue);
  }

  function isSelected(option: string) {
    if (isStringArray(selectedValue)) {
      return selectedValue.filter((o) => o === option).length > 0;
    }

    if (!selectedValue) {
      return false;
    }

    return selectedValue === option;
  }

  function onSearch(event: React.ChangeEvent<HTMLInputElement>) {
    setSearchValue(event.target.value);
  }

  function getOptions() {
    if (!searchValue) {
      return options;
    }

    return options.filter((option) => option.toLowerCase().includes(searchValue.toLowerCase()));
  }

  return (
    <div className={`border border-darkGreen relative text-left rounded-md ${styles}`}>
      <div ref={inputRef} onClick={handleInputSelect} className="flex justify-between items-center select-none px-2 py-1">
        <div className="flex flex-wrap gap-1 max-w-full">{getDisplay()}</div>
        <div>
          <Icon />
        </div>
      </div>
      {showMenu && (
        <div ref={menuRef} className="absolute w-full z-50 border border-darkGreen translate-y-1 overflow-auto max-h-36 rounded-md">
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
