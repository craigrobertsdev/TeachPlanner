# Teach Planner (name WIP) - Overview

## Introduction

This application is built for classroom teachers to enable them to efficiently manage their lesson planning, assessment tracking and resource management. It is designed to make the process of planning and tracking lessons as simple as possible, while also providing a powerful tool for teachers to use to improve their teaching.

Classroom teachers currently manage their planning and resources in a variety of ways. This application aims to provide a single tool that can be used to manage all of these tasks. It is designed to take the stress out of report writing time by having an easy to use assessment tracker that can be used to generate reports. It also aims to make lesson planning as simple as possible by providing a simple interface for teachers to use to plan their lessons.

Furthermore, when a teacher needs to take a sick day, they are expected to provide the relief teacher with a lesson plan to enable them to teach the class. This application aims to make this process as simple as possible by allowing teachers to export their lesson plans for sending via email.

## Features

- Lesson Planner
- Calendar
- Assessment Tracker
- Resource Store
- Admin Panel
- Curriculum Browser

## Planner

This feature is designed to make lesson planning as simple as possible. It provides a simple interface for teachers to use to plan their lessons. It also provides a way for teachers to export their lesson plans for sending via email.

### Week Planner

This feature allows teachers to plan their lessons for the week. They can add lessons to the planner either as individual periods or as a block of time, such as a double period. They can also add notes to the planner to remind them of things they need to do during the week.

Each lesson slot can have a single subject associated with it, including NIT lessons. The subject can be selected from a list of subjects that the teacher teaches, which is added in the admin panel. The available subjects are based on the subjects parsed from the Australian Curriculum. Subjects are colour coded to make it easier to see what subjects are being taught during the week.

### Term Planner

The term planner is a calendar which shows each week of the term with a high level overview of what is happening on a day by day basis. This view is useful for visualing the term as a whole and for planning around events that take students out of their normal classroom routine, such as excursions and camps.

### Year Planner

I'm not actually sure what Rhi wants with this so I'm just going to leave it for now.

### Lesson Planner

This feature allows teachers to plan their lessons in detail. They can select a topic from the Curriculum and further refine it by selecting Content Descriptors associated with that subject.

They can add notes, resources and links to assessments to the lesson, with the resources being links to their personal (or potentially pooled) resource store.

Notes can be added against each student which will be associated with them for that subject and available for use at report writing time.

This information can be used to generate a report for the relief teacher.

## Resource store

This will be a cloud based store for teaching resources that can be accessed by the teacher who uploaded it.

Resources will be divided by subject and topic, and will be able to be searched by name, subject and topic.

## Assessment Tracker

This feature allows teachers to create assessments and track progress. Grades can be entered against each student and those marks can be used at report writing time to generate a grade.

## Admin

This section is for adding subjects, students, generating week plan templates, setting up lessons times and lengths, and other administrative tasks.

## Curriculum

The curriculum is preloaded into the application and is used to generate the list of subjects and content descriptors that are available for use in the lesson planner.

Teachers can view the curriculum to see what content descriptors are available for each subject.

## Reports

Reports are typically done using another application that differs from site to site. This section of the app will be responsible for generating reports based on the data entered into the application and assisting in comment generation.

Comments will be generated from a store of generic comments that are associated with a grade (from A-F) and subject. These can then be modified and personalised by the teacher.
