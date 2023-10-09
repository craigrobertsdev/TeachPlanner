# WeekPlanner

Lesson plans store a refernce to a particular subject that allows the teacher to track the content descriptions that have been planned
for the lesson. These content descriptions need to store information about the term they are being taught in and the date of the lesson they are scheduled
for.

```json
{
  "id": { "value": "00000000-0000-0000-0000-000000000000" },
  "yearDataId": { "value": "00000000-0000-0000-0000-000000000000" },
  "lessonPlans": [
    {
      "subjectId": { "value": "00000000-0000-0000-0000-000000000000" },
      "planningNotes": "string",
      "resources": [
        {
          "id": { "value": "00000000-0000-0000-0000-000000000000" },
          "name": "string",
          "url": "string"
        }
      ],
      "assessments": [
        {
          "id": { "value": "00000000-0000-0000-0000-000000000000" },
          "name": "string",
          "url": "string"
        }
      ],
      "comments": [
        {
          "id": { "value": "00000000-0000-0000-0000-000000000000" },
          "text": "string",
          "createdDateTime": "2020-01-01T00:00:00.000Z",
          "modifiedDateTime": "2020-01-01T00:00:00.000Z",
          "completed": true,
          "completedDateTime": "2020-01-01T00:00:00.000Z",
          "struckThrough": false
        }
      ],
      "periodStart": 1,
      "numberOfPeriods": 1
    }
  ],
  "weekNumber": 0,
  "weekStart": "2020-01-01T00:00:00.000Z",
  "events": [
    {
      "id": { "value": "00000000-0000-0000-0000-000000000000" },
      "name": "string",
      "notes": "string",
      "startTime": "2020-01-01T00:00:00.000Z",
      "endTime": "2020-01-01T00:00:00.000Z",
      "fullDay": true,
      "permissionSlipRequired": true,
      "permissionSlips": [
        {
          "id": { "value": "00000000-0000-0000-0000-000000000000" },
          "studentId": { "value": "00000000-0000-0000-0000-000000000000" },
          "permissionSlipId": { "value": "00000000-0000-0000-0000-000000000000" },
          "permissionSlipSigned": true
        }
      ]
    }
  ],
  "createdDateTime": "2020-01-01T00:00:00.000Z",
  "updatedDateTime": "2020-01-01T00:00:00.000Z"
}
```
