# TermPlanner

This aggregate relates to the individual subjects and content descriptions that the teacher has scheduled for each term.

This provides a high level overview for what a teacher is planning for each term.

This will be used by the teacher to plan out the content descriptions that they will be teaching for each term and to make sure they aren't missing any.

````json
{
  "id": { "value": "00000000-0000-0000-0000-000000000000" },
  "yearLevelsTaught": ["Foundation", "Year 1"],
  "termPlans": [
    {
      "subjects": [
        {
          "name": "Mathematics",
          "contentDescriptions": [
            {
              "curriculumCode": "ACMNA001",
              "termsTaughtIn": [1, 2, 3, 4],
              "scheduled": true,
            }
          ]
        }
      ]
    }
  ],
  "createdDateTime": "2020-01-01T00:00:00.000Z",
  "updatedDateTime": "2020-01-01T00:00:00.000Z"
} ```
````
