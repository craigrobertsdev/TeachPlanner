# Assessment

## Formative Assessment

```json
{
  "id": { "value": "00000000-0000-0000-0000-000000000000" },
  "teacherId": { "teacherId": { "value": "00000000-0000-0000-0000-000000000000" } },
  "studentId": { "studentId": { "value": "00000000-0000-0000-0000-000000000000" } },
  "comments": "string",
  "dateConducted": "2020-01-01T00:00:00.000Z"
}
```

## Summative Assessment

```json
{
  "id": { "value": "00000000-0000-0000-0000-000000000000" },
  "teacherId": { "teacherId": { "value": "00000000-0000-0000-0000-000000000000" } },
  "subjectId": { "subjectId": { "value": "00000000-0000-0000-0000-000000000000" } },
  "lessonId": { "lessonId": { "value": "00000000-0000-0000-0000-000000000000" } },
  "yearLevel": "string",
  "dateScheduled": "2020-01-01T00:00:00.000Z",
  "summativeAssessmentResults": [
    {
      "id": { "value": "00000000-0000-0000-0000-000000000000" },
      "summativeAssessmentId": { "summativeAssessmentId": { "value": "00000000-0000-0000-0000-000000000000" } },
      "studentId": { "studentId": { "value": "00000000-0000-0000-0000-000000000000" } },
      "subjectId": { "subjectId": { "value": "00000000-0000-0000-0000-000000000000" } },
      "comments": "string",
      "grade": {
        "id": { "value": "00000000-0000-0000-0000-000000000000" },
        "grade": "string",
        "gradePercentage": 0
      },
      "dateConducted": "2020-01-01T00:00:00.000Z",
      "dateMarked": "2020-01-01T00:00:00.000Z"
    }
  ]
}
```
