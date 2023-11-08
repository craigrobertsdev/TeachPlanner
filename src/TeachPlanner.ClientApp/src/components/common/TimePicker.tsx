import { useState } from "react";

type TimePickerProps = {
  value: { hour: number; minute: number; period: string };
  setValue: (hour: number, minute: number, period: string) => void;
};

const TimePicker = ({ value, setValue }: TimePickerProps) => {
  console.log("value = ", value?.hour);
  const handleHoursChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    const hour = +event.target.value;
    const minute = value.minute;
    const period = value.period;
    setValue(hour, minute, period);
  };

  const handleMinutesChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    // setMinute(+event.target.value);
    const hour = value.hour;
    const minute = +event.target.value;
    const period = value.period;
    setValue(hour, minute, period);
  };

  const handlePeriodChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    const hour = value.hour;
    const minute = value.minute;
    const period = event.target.value;
    setValue(hour, minute, period);
  };

  return (
    <div>
      <select value={value.hour} onChange={handleHoursChange}>
        {Array.from({ length: 12 }, (_, i) => i++).map((hour) => (
          <option key={hour} value={hour}>
            {hour}
          </option>
        ))}
      </select>
      <select value={value.minute} onChange={handleMinutesChange}>
        {Array.from({ length: 12 }, (_, i) => i * 5).map((minute) => (
          <option key={minute} value={minute}>
            {minute.toString().padStart(2, "0")}
          </option>
        ))}
      </select>
      <select value={value.period} onChange={handlePeriodChange}>
        <option value="AM">AM</option>
        <option value="PM">PM</option>
      </select>
    </div>
  );
};

export default TimePicker;
