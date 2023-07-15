# Lesson Planner

## Features

- Topic selector
- Content descriptor selector
- Notes
- Resource links
- Assessment links
- Student notes
- Email exporter

### Topic selector

A dropdown list of topics for the selected subject. The list is populated from the curriculum.

### Content descriptor selector

A dropdown list of content descriptors for the selected topic. The list is populated from the curriculum.

### Lesson Plan

A text area for entering notes about the lesson.

The notes are saved against the lesson and can be viewed in the lesson planner.

The text entry area will have a toolbar for formatting the text.

### Resource links

A list of links to resources that are relevant to the lesson.

These will be visible from a panel on the right hand side of the screen and will be able to be added to the lesson by dragging and dropping them into the lesson.

They can each be clicked on for viewing, and will open in a new tab. They can also be deleted from the lesson.

Additional resources can be added by clicking on the `Add Resource` button.

### Assessment links

A list of links to assessments that are relevant to the lesson.

These will be visible from a panel on the right hand side of the screen and will be able to be added to the lesson by dragging and dropping them into the lesson.

They can each be clicked on for viewing, and will open in a new tab. They can also be deleted from the lesson.

Additional assessments can be added by clicking on the `Add Assessment` button.

### Student notes

The teacher can add notes against each student for the lesson. The student names are in a list that can be accessed by clicking on the `Student Notes` button.

From this view, a list of all students is displayed and clicking on their name reveals a text area in which to add notes. The notes are saved against the student and can be viewed in the lesson planner as well as against that student in the admin panel.

### Email exporter

The lesson can be exported as an email by clicking on the `Export` button. This will open a new Microsoft Outlook email with the lesson details in the body of the email.

The lesson details will include:

- Subject
- Topic
- Content descriptor
- Lesson plan
- Resources
- Assessments

The resources and assessments will be attached to the email as PDFs.

The email will be addressed to the teacher and the teacher's line manager.

## Models

### Lesson

```json
{
  "id": { "value": "00000000-0000-0000-0000-000000000000" },
  "subject": "Mathematics",
  "strand": "Algebra",
  "contentDescriptors": [
    {
      "id": { "value": "00000000-0000-0000-0000-000000000000" },
      "description": "string",
      "elaborations": [
        {
          "id": { "value": "00000000-0000-0000-0000-000000000000" },
          "description": "string"
        }
      ]
    }
  ],
  "lessonPlanNotes": "string",
  "resourceIds": [
    {
      "id": { "value": "00000000-0000-0000-0000-000000000000" }
    }
  ],
  "assessmentIds": [
    {
      "id": { "value": "00000000-0000-0000-0000-000000000000" }
    }
  ],
  "studentNotesIds": [
    {
      "studentNotesId": { "value": "00000000-0000-0000-0000-000000000000" }
    }
  ],
  "startDateTime": "2020-01-01T00:00:00.000Z",
  "endDateTime": "2020-01-01T00:00:00.000Z",
  "createdDateTime": "2020-01-01T00:00:00.000Z",
  "updatedDateTime": "2020-01-01T00:00:00.000Z"
}
```

### Resource

```json
{
  "id": { "value": "00000000-0000-0000-0000-000000000000" },
  "name": "string",
  "url": "string",
  "associatedSubjects": [
    {
      "subjectId": { "value": "00000000-0000-0000-0000-000000000000" }
    }
  ],
  "dateTimeCreated": "2020-01-01T00:00:00.000Z",
  "dateTimeUpdated": "2020-01-01T00:00:00.000Z"
}
```

### Assessment

```json
{
  "id": { "value": "00000000-0000-0000-0000-000000000000" },
  "name": "string",
  "url": "string",
  "associatedSubjects": [
    {
      "subjectId": { "value": "00000000-0000-0000-0000-000000000000" }
    }
  ],
  "dateTimeCreated": "2020-01-01T00:00:00.000Z",
  "dateTimeUpdated": "2020-01-01T00:00:00.000Z"
}
```

### Student Notes

```json
{
  "id": { "value": "00000000-0000-0000-0000-000000000000" },
  "studentId": { "value": "00000000-0000-0000-0000-000000000000" },
  "lessonId": { "value": "00000000-0000-0000-0000-000000000000" },
  "notes": "string",
  "dateTimeCreated": "2020-01-01T00:00:00.000Z",
  "dateTimeUpdated": "2020-01-01T00:00:00.000Z"
}
```
