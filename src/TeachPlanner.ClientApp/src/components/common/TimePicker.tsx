import React, { useState } from "react";

type TimePickerProps = {
  onChange: (hours: string, minutes: string, period: string) => void;
};

const TimePicker = ({ onChange }: TimePickerProps) => {
  const [hours, setHours] = useState("");
  const [minutes, setMinutes] = useState("");
  const [period, setPeriod] = useState("AM");

  const handleHoursChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    setHours(event.target.value);
    onChange(event.target.value, minutes, period);
  };

  const handleMinutesChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    setMinutes(event.target.value);
    onChange(hours, event.target.value, period);
  };

  const handlePeriodChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    setPeriod(event.target.value);
    onChange(hours, minutes, event.target.value);
  };

  return (
    <div>
      <select value={hours} onChange={handleHoursChange}>
        {Array.from({ length: 12 }, (_, i) => i + 1).map((hour) => (
          <option key={hour} value={hour}>
            {hour}
          </option>
        ))}
      </select>
      <select value={minutes} onChange={handleMinutesChange}>
        {Array.from({ length: 12 }, (_, i) => i * 5).map((minute) => (
          <option key={minute} value={minute}>
            {minute.toString().padStart(2, "0")}
          </option>
        ))}
      </select>
      <select value={period} onChange={handlePeriodChange}>
        <option value="AM">AM</option>
        <option value="PM">PM</option>
      </select>
    </div>
  );
};

export default TimePicker;
