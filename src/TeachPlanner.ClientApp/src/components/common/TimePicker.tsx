type TimePickerProps = {
  value: { hour: number; minute: number; period: string };
  setValue: (hour: number, minute: number, period: string) => void;
};

const TimePicker = ({ value, setValue }: TimePickerProps) => {
  const handleHoursChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    setValue(+event.target.value, value.minute, value.period);
  };

  const handleMinutesChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    setValue(value.hour, +event.target.value, value.period);
  };

  const handlePeriodChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    setValue(value.hour, value.minute, event.target.value);
  };

  return (
    <div className="flex gap-2">
      <select value={value.hour} onChange={handleHoursChange} className="text-center px-1 outline outline-2 outline-darkGreen">
        {Array.from({ length: 12 }, (_, i) => ++i).map((hour) => (
          <option key={`hour-${hour}`} value={hour}>
            {hour}
          </option>
        ))}
      </select>
      <select value={value.minute} onChange={handleMinutesChange} className="text-center px-1 outline outline-2 outline-darkGreen">
        {Array.from({ length: 12 }, (_, i) => i * 5).map((minute) => (
          <option key={`minute-${minute}`} value={minute}>
            {minute.toString().padStart(2, "0")}
          </option>
        ))}
      </select>
      <select value={value.period} onChange={handlePeriodChange} className="text-center px-1 outline outline-2 outline-darkGreen">
        <option value="AM">AM</option>
        <option value="PM">PM</option>
      </select>
    </div>
  );
};

export default TimePicker;
