# Subject Aggregate

### Base Subject

```csharp
abstract class BaseSubject
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    protected BaseSubject(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

}
```

### Mathematics

```json
{
  "id": { "value": "00000000-0000-0000-0000-000000000000" },
  "name": "Mathematics",
  "yearLevels": [
    {
      "yearLevel": "string",
      "yearLevelDescription": "string",
      "achievementStandard": "string",
      "strands": [
        {
          "id": { "value": "00000000-0000-0000-0000-000000000000" },
          "name": "string",
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
          ]
        }
      ]
    }
  ]
}
```

### English

```json
{
  "id": { "value": "00000000-0000-0000-0000-000000000000" },
  "name": "English",
  "yearLevels": [
    {
      "yearLevel": "string",
      "yearLevelDescription": "string",
      "achievementStandard": "string",
      "strands": [
        {
          "id": { "value": "00000000-0000-0000-0000-000000000000" },
          "name": "string",
          "substrands": [
            {
              "id": { "value": "00000000-0000-0000-0000-000000000000" },
              "name": "string",
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
              ]
            }
          ]
        }
      ]
    }
  ]
}
```

### Humanities and Social Sciences

```json
{
  "id": { "value": "00000000-0000-0000-0000-000000000000" },
  "name": "Humanities and Social Sciences",
  "yearLevels": [
    {
      "yearLevel": "string",
      "achievementStandard": "string",
      "strands": [
        {
          "id": { "value": "00000000-0000-0000-0000-000000000000" },
          "name": "string",
          "substrands": [
            {
              "id": { "value": "00000000-0000-0000-0000-000000000000" },
              "name": "string",
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
              ]
            }
          ]
        }
      ]
    }
  ]
}
```

## Science

```json
{
  "id": { "value": "00000000-0000-0000-0000-000000000000" },
  "name": "Science",
  "yearLevels": [
    {
      "yearLevel": "string",
      "yearLevelDescription": "string",
      "achievementStandard": "string",
      "strands": [
        {
          "id": { "value": "00000000-0000-0000-0000-000000000000" },
          "name": "string",
          "substrands": [
            {
              "id": { "value": "00000000-0000-0000-0000-000000000000" },
              "name": "string",
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
              ]
            }
          ]
        }
      ]
    }
  ]
}
```

## Health and Physical Education

```json
{
  "id": { "value": "00000000-0000-0000-0000-000000000000" },
  "name": "Health and Physical Education",
  "yearLevels": [
    {
      "yearLevel": "string",
      "yearLevelDescription": "string",
      "achievementStandard": "string",
      "strands": [
        {
          "id": { "value": "00000000-0000-0000-0000-000000000000" },
          "name": "string",
          "substrands": [
            {
              "id": { "value": "00000000-0000-0000-0000-000000000000" },
              "name": "string",
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
              ]
            }
          ]
        }
      ]
    }
  ]
}
```

```json
{
  "id": { "value": "00000000-0000-0000-0000-000000000000" },
  "name": "Child Protection Curriculum",
  "yearLevels": [
    {
      "yearLevel": "string",
      "strands": [
        {
          "name": "The right to be safe"
        },
        {
          "name": "Relationships"
        },
        {
          "name": "Recognising and reporting abuse"
        },
        {
          "name": "Protective strategies"
        }
      ]
    }
  ]
}
```
