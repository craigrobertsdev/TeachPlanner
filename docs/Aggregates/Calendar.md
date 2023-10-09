# Calendar

## Description

This aggregate acts stores data used to create a calendar that acts as an overview of all the school events and other things the teacher needs to make a note of for a term.

In addition, the calendar will need to display the subjects and content descriptions that the teacher has scheduled for each term.

```json
{
  "id": { "value": "00000000-0000-0000-0000-000000000000" },
  "yearDataId": { "value": "00000000-0000-0000-0000-000000000000" },
  "calendarYear": 2023,
  "termData": [
    {
      "termNumber": 1,
      "schoolEvents": [
        {
          "location": {
            "streetNumber": "123",
            "streetName": "Fake Street",
            "suburb": "Fake Suburb"
          },
          "name": "Aquatics",
          "fullDay": true,
          "eventStart": "2023-01-01T00:00:00.000Z",
          "eventEnd": "2023-01-01T00:00:00.000Z",
          "pesmissionSlips": [
            {
              "studentId": { "value": "00000000-0000-0000-0000-000000000000" },
              "returned": false
            }
          ],
          "permissionSlipsDue": "2023-01-01T00:00:00.000Z",
          "notes": "This is a note about the event"
        }
      ]
    }
  ]
}
```
