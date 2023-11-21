type YearLevelPickerProps = {
  yearLevelsTaught: string[];
  setYearLevelsTaught: React.Dispatch<React.SetStateAction<string[]>>;
};

function YearLevelPicker({ yearLevelsTaught, setYearLevelsTaught }: YearLevelPickerProps) {
  const yearLevels = ["Foundation", "1", "2", "3", "4", "5", "6"];
  function onYearLevelSelected(yearLevel: string) {
    if (yearLevelsTaught.includes(yearLevel)) {
      setYearLevelsTaught(yearLevelsTaught.filter((yl) => yl !== yearLevel));
      return;
    }

    setYearLevelsTaught([...yearLevelsTaught, yearLevel]);
  }

  function isSelected(yearLevel: string) {
    if (yearLevelsTaught.includes(yearLevel)) {
      return true;
    }
    return false;
  }

  return (
    <>
      <h3 className="text-xl pb-2">What year levels will do you teach?</h3>
      <div className="flex flex-col bg-main rounded-md border border-darkGreen">
        {yearLevels.map((yearLevel, idx) => (
          <p
            key={yearLevel}
            className={`hover:bg-primaryHover hover:cursor-default ${
              isSelected(yearLevel) && "bg-primaryFocus outline outline-1 outline-primaryFocusBorder"
            } ${idx === 0 && "rounded-t-md"} ${idx === yearLevels.length - 1 && "rounded-b-md"}
            } text-lg`}
            onClick={() => onYearLevelSelected(yearLevel)}>
            {yearLevel}
          </p>
        ))}
      </div>
    </>
  );
}

export default YearLevelPicker;
