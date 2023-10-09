# Assessment

An assesssment is an individual entity that represents a single test taken by one student.

```json
{
  "id": { "value": "00000000-0000-0000-0000-000000000000" },
  "teacherId": { "value": "00000000-0000-0000-0000-000000000000" },
  "studentId": { "value": "00000000-0000-0000-0000-000000000000" },
  "lessonPlanId": { "value": "00000000-0000-0000-0000-000000000000" },
  "resourceId": { "value": "00000000-0000-0000-0000-000000000000" },
  "yearLevel": "Year 1",
  "assessementType": "summative | formative",
  "assessmentResult": {
    "comments": "string",
    "grade": {
      "id": { "value": "00000000-0000-0000-0000-000000000000" },
      "grade": "string",
      "gradePercentage": 0
    }
  },
  "planningNotes": "string",
  "dateConducted": "2020-01-01T00:00:00.000Z"
}
```
