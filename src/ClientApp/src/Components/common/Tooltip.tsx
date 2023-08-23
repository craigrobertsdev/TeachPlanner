import { useEffect, useState } from "react";

type TooltipProps = {
  content: string;
  position: "top" | "bottom" | "left" | "right";
  children: React.ReactNode;
};

function Tooltip({ content, position, children }: TooltipProps) {
  const [hover, setHover] = useState(false);

  const positionMap = {
    top: {
      bottom: "50%",
    } as React.CSSProperties,
    bottom: {
      top: "50%",
    } as React.CSSProperties,
    left: {
      right: "50%",
    } as React.CSSProperties,
    right: {
      left: "50%",
    } as React.CSSProperties,
  };

  return (
    <div className="relative" onMouseEnter={() => setHover(true)} onMouseLeave={() => setHover(false)}>
      {children}
      <span
        style={positionMap[position]}
        className={`absolute z-10 w-full text-sm bg-sage border border-darkGreen px-3 py-2 rounded-md ${hover ? "visible" : "hidden"}`}>
        {content}
      </span>
    </div>
  );
}

export default Tooltip;
