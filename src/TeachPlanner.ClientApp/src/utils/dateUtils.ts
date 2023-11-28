function getCalendarTime(time: string) {
  // hh:mm:ss
  const hours = +time.split(":")[0];
  const minutes = +time.split(":")[1];
  const hoursString = hours > 12 ? `${hours - 12}` : `${hours}`;
  const minutesString = minutes < 10 ? `0${minutes}` : `${minutes}`;
  const ampm = hours >= 12 ? "pm" : "am";
  return `${hoursString}:${minutesString}${ampm}`;
}

function getCalendarDate(date: Date | string, offset?: number): string {
  if (typeof date === "string") {
    const newDate = new Date(date);
    newDate.setDate(newDate.getDate() + (offset || 0));
    const day = newDate.getDate();
    const month = getMonthName(newDate);
    const suffix = getOrdinalSuffix(day);
    return `${day}${suffix} ${month}`;
  } else {
    const day = date.getDate();
    const month = getMonthName(date);
    const suffix = getOrdinalSuffix(day);
    return `${day}${suffix} ${month}`;
  }
}

function getDayName(date: number | Date): string {
  const days = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
  if (typeof date === "number") {
    return days[date];
  }

  return days[(date as Date).getDay()];
}

function getMonthName(date: Date | number): string {
  const months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "Decemeber"];
  return typeof date === "number" ? months[date] : months[date.getMonth()];
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
