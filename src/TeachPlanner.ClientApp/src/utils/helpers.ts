export function createCssClassString(text: string) {
	return text.split(" ").map((word, index) => {
		return index === 0 ? word.toLowerCase() : word[0].toUpperCase() + word.slice(1);
	}).join("");
}

export const dayPlanHelper = {
	dayPlanPattern: {
		pattern: [
			{
				startTime: "9:10",
				endTime: "10:00",
				periodType: "Lesson",
				name: "Lesson 1",
			},
			{
				startTime: "10:00",
				endTime: "10:50",
				periodType: "Lesson",
				name: "Lesson 2",
			},
			{
				startTime: "10:50",
				endTime: "11:20",
				periodType: "Break",
				name: "Break 1",
			},
			{
				startTime: "11:20",
				endTime: "12:10",
				periodType: "Lesson",
				name: "Lesson 3",
			},
			{
				startTime: "12:10",
				endTime: "1:00",
				periodType: "Lesson",
				name: "Lesson 4",
			},
			{
				startTime: "13:00",
				endTime: "13:30",
				periodType: "Break",
				name: "Break 2",
			},
			{
				startTime: "13:30",
				endTime: "14:20",
				periodType: "Lesson",
				name: "Lesson 5",
			},
			{
				startTime: "14:20",
				endTime: "15:10",
				periodType: "Lesson",
				name: "Lesson 6",
			},
		],
	} as DayPlanTemplate,
	dayPlans: [{
		startTime: new Date(2023, 12, 11, 9, 10, 0),
		lessonPlans: [
			{
				id: "1",
				subject: {
					id: "subject1",
					name: "Mathematics",
					contentDescriptions: [],
				},
				planningNotes: "Plan for the day",
				startTime: new Date(),
				endTime: new Date(),
				numberOfPeriods: 1,
				periodNumber: 1,
				resources: [
					{
						id: "resource1",
						name: "Math textbook",
						description: "Textbook for the lesson",
						url: "https://example.com/math-textbook",
					},
				],
				assessments: [],
				lessonComments: [
					{
						id: "comment1",
						content: "Great job!",
						date: new Date(),
					},
				],
				contentDescriptions: [],
			},
		],
		events: [
		],
		breaks: [
			{
				startTime: new Date(2023, 12, 11, 10, 50, 0),
				endTime: new Date(2023, 12, 11, 11, 20, 0),
				periodNumber: 3,
				name: "Lunch",
				duty: "Teacher supervision",
			},
		]},
		{
			startTime: new Date(2023, 13, 11, 10, 50, 0),
			lessonPlans: {},
			breaks: {},
			events: {},
		},
		{
			startTime: new Date(2023, 14, 11, 10, 50, 0),
			lessonPlans: {},
			breaks: {},
			events: {},
		},
		{
			startTime: new Date(2023, 15, 11, 10, 50, 0),
			lessonPlans: {},
			breaks: {},
			events: {},
		},
		{
			startTime: new Date(2023, 16, 11, 10, 50, 0),
			lessonPlans: {},
			breaks: {},
			events: {},
		},
	] as DayPlan[]
} 
