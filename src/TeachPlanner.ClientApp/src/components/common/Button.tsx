import { ReactNode } from "react";

type ButtonProps = {
  variant?: ButtonTypes;
  children: ReactNode;
  onClick: React.MouseEventHandler<HTMLButtonElement>;
  classList?: string;
  disabled?: boolean;
};

type ButtonTypeVariants = {
  submit: "submit";
  cancel: "cancel";
  add: "add";
  close: "close";
};

type ButtonTypes = ButtonTypeVariants[keyof ButtonTypeVariants];

function Button({ variant = "submit", classList = "", children, onClick, disabled = false }: ButtonProps) {
  const buttonStyles = {
    submit: "bg-sage text-primary px-2 py-1 rounded-md text-lg hover:bg-sageHover",
    cancel: "bg-ceramic text-primary px-2 py-1 rounded-md text-lg hover:bg-ceramicHover",
    add: "bg-peach text-primary px-2 py-1 rounded-md text-lg hover:bg-peachHover ",
    close: "bg-ceramic text-primary px-2 py-1 rounded-md hover:bg-ceramicHover",
  };

  return (
    <button className={`${buttonStyles[variant]} ${classList}`} onClick={onClick} disabled={disabled}>
      {children}
    </button>
  );
}

export default Button;
