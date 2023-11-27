# Week Planner

### Description

The default view for a teacher upon logging in to the app. This feature will show the teacher a list of all the classes they are teaching for the current week. The teacher will be able to click on a class to view the class details.

The week planner will display lessons and breaks based on a template that is defined by the teacher upon account setup. A planner is created upon account creation and is updated by the teacher as needed.

### API Endpoints

- GET /week-planner
- PATCH /week-planner

### Endpoint specs

- GET /week-planner

  - Returns the week planner for the current week

  - If no week planner is found for the current week, a new week planner is created and returned. The new planner will use the same template as the associated YearData object.

- PATCH /week-planner
