import { ReactNode, MouseEvent } from "react";

type ButtonProps = {
  variant?: ButtonTypes;
  children: ReactNode;
  onClick: React.MouseEventHandler<HTMLButtonElement>;
};

type ButtonTypeVariants = {
  submit: "submit";
  cancel: "cancel";
};

type ButtonTypes = ButtonTypeVariants[keyof ButtonTypeVariants];

function Button({ variant = "submit", children, onClick }: ButtonProps) {
  const buttonStyles = {
    submit: "bg-sage text-primary px-2 py-1 rounded-md text-lg",
    cancel: "bg-ceramic text-primary px-2 py-1 rounded-md text-lg",
  };

  return (
    <button className={`${buttonStyles[variant]}`} onClick={onClick}>
      {children}
    </button>
  );
}

export default Button;
