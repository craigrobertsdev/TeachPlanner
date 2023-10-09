# YearData

## Description

This aggregate is used to store the data for a year. It is used to store references to the students, subjects, reports. It is also used to store the data for the week planner. One is created when a new teacher is created (when a user signs up), as well as for each year.

```json
{
  "id": { "value": "00000000-0000-0000-0000-000000000000" },
  "calendarYear": 2023,
  "teacherId": { "value": "00000000-0000-0000-0000-000000000000" },
  "termPlanner": { "value": "00000000-0000-0000-0000-000000000000" },
  "students": [{ "value": "00000000-0000-0000-0000-000000000000" }],
  "subjects": [{ "value": "00000000-0000-0000-0000-000000000000" }],
  "weekPlannerIds": [{ "value": "00000000-0000-0000-0000-000000000000" }],
  "yearLevelsTaught": ["Foundation", "Year 1"],
  "CreatedDateTime": "2020-01-01T00:00:00.000Z",
  "ModifiedDateTime": "2020-01-01T00:00:00.000Z"
}
```
