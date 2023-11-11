type TermDatesCreatorProps = {
  termDates: TermDates[];
  setTermDates: React.Dispatch<React.SetStateAction<TermDates[]>>;
};

type TermDates = {
  startDate: Date;
  endDate: Date;
};

function TermDatesCreator({ termDates, setTermDates }: TermDatesCreatorProps) {
  function onTermStartChange(e: React.ChangeEvent<HTMLInputElement>, index: number) {
    const newStartDate = new Date(e.target.value);
    if (termDates[index].endDate < newStartDate) {
      const newEndDate = new Date(e.target.value);
      newEndDate.setDate(newStartDate.getDate() + 1);
      termDates[index].endDate = newEndDate;
    }

    setTermDates((termDates) => {
      const newTermDates = [...termDates];
      newTermDates[index].startDate = newStartDate;
      return newTermDates;
    });
  }

  function onTermEndChange(e: React.ChangeEvent<HTMLInputElement>, index: number) {
    const newEndDate = new Date(e.target.value);
    if (termDates[index].startDate > newEndDate) {
      const newStartDate = new Date(e.target.value);
      newStartDate.setDate(newStartDate.getDate() - 1);
      termDates[index].startDate = newStartDate;
    }

    setTermDates((termDates) => {
      const newTermDates = [...termDates];
      newTermDates[index].endDate = newEndDate;
      return newTermDates;
    });
  }

  return (
    <>
      <h3 className="text-xl pb-2">Please enter the term dates for this year</h3>
      <div className="text-lg grid grid-cols-2 grid-rows-2 gap-3 justify-center pb-2">
        {termDates.map((term, i) => (
          <div key={`termDates-${i}`} className="p-2 border-2 border-darkGreen rounded-md">
            <h4 className="font-semibold text-lg">Term {i + 1}</h4>
            <div className="pb-2 flex flex-col">
              <label className="pr-2" htmlFor="startDate">
                Start Date
              </label>
              <input
                className="p-1 border border-darkGreen rounded-md text-center"
                name="startDate"
                type="date"
                value={term.startDate.toISOString().slice(0, 10)}
                onChange={(e) => onTermStartChange(e, i)}
              />
            </div>
            <div className="pb-2 flex flex-col">
              <label className="pr-2" htmlFor="endDate">
                End Date
              </label>
              <input
                className="p-1 border border-darkGreen rounded-md text-center"
                name="endDate"
                type="date"
                value={term.endDate.toISOString().slice(0, 10)}
                onChange={(e) => onTermEndChange(e, i)}
              />
            </div>
          </div>
        ))}
      </div>
    </>
  );
}

export default TermDatesCreator;
