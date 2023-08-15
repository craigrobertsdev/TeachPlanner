function getDayName(date: Date): string {
  const days = [
    "Sunday",
    "Monday",
    "Tuesday",
    "Wednesday",
    "Thursday",
    "Friday",
    "Saturday",
  ];
  return days[date.getDay()];
}

function getCalendarTime(date: Date): string {
  const hours = date.getHours();
  const minutes = date.getMinutes();
  const hoursString = hours > 12 ? `${hours - 12}` : `${hours}`;
  const minutesString = minutes < 10 ? `0${minutes}` : `${minutes}`;
  const ampm = hours > 12 ? "pm" : "am";
  return `${hoursString}:${minutesString}${ampm}`;
}

export { getDayName, getCalendarTime };
