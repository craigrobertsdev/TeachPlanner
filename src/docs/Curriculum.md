# Curriculum Browser and Parser

## Features

- Parse the Australian Curriculum
- View the Australian Curriculum

### Parse the Australian Curriculum

A parser has been written to parse the Australian Curriculum into object representations of its constituent parts. These are then used to populate the database with the curriculum.

There will need to be a number of different parsers written to parse the different subjects. For example, Mathematics is structured as:

```
Subject
  Strand
    Content Descriptor
      Elaboration
```

Whereas HASS is structured as:

```
Subject
  Strand
    Sub-strand
      Content Descriptor
        Elaboration
```

The subjects will be individual classes that inherit from a base class and will contain all of the methods that need to act on those classes. They will be abstract and the implementation will be in the individual subject classes.

### View the Australian Curriculum

The curriculum can be viewed in a tree structure. The tree structure is generated from the database.
