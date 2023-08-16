function getCalendarTime(date: Date): string {
  const hours = date.getHours();
  const minutes = date.getMinutes();
  const hoursString = hours > 12 ? `${hours - 12}` : `${hours}`;
  const minutesString = minutes < 10 ? `0${minutes}` : `${minutes}`;
  const ampm = hours >= 12 ? "pm" : "am";
  return `${hoursString}:${minutesString}${ampm}`;
}

function getCalendarDate(date: Date): string {
  const day = date.getDate();
  const month = getMonthName(date);
  const suffix = getOrdinalSuffix(day);
  return `${day}${suffix} ${month}`;
}

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

function getMonthName(date: Date): string {
  const months = [
    "January",
    "February",
    "March",
    "April",
    "May",
    "June",
    "July",
    "August",
    "September",
    "October",
    "November",
    "Decemeber",
  ];
  return months[date.getMonth()];
}

function getOrdinalSuffix(day: number): string {
  const suffixes = ["th", "st", "nd", "rd"];
  const remainder = day % 10;

  if (day === 11 || day === 12 || day === 13) {
    return suffixes[0];
  } else if (remainder === 1) {
    return suffixes[1];
  } else if (remainder === 2) {
    return suffixes[2];
  } else if (remainder === 3) {
    return suffixes[3];
  } else {
    return suffixes[0];
  }
}

export { getDayName, getCalendarTime, getCalendarDate };
