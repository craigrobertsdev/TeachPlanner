/** @type {import('tailwindcss').Config} */
export default {
  content: ["./index.html", "./src/**/*.{js,jsx,ts,tsx}"],
  plugins: ["prettier-plugin-tailwindcss"],
  theme: {
    extend: {
      colors: {
        primary: "#EEE6DE",
        sage: "#90AEB2",
        darkGreen: "#37514D",
        peach: "#DD8E75",
        ceramic: "#B6594C",
        // hover
        primaryHover: "#E0D8D0",

        // focus
        primaryFocus: "#9E9790",
      },
      height: {
        screen: "100dvh",
      },
      minHeight: {
        screen: "100dvh",
      },
    },
    fontFamily: {
      theme: ["'Nunito Sans'", "sans-serif"],
    },
  },
  safelist: [
    "col-start-2",
    "col-start-3",
    "col-start-4",
    "col-start-5",
    "col-start-6",
    "col-start-7",
    "col-start-8",
    "row-start-1",
    "row-start-2",
    "row-start-3",
    "row-start-4",
    "row-start-5",
    "row-start-6",
    "row-start-7",
    "row-start-8",
    "row-start-9",
    "row-start-10",
    "row-end-1",
    "row-end-2",
    "row-end-3",
    "row-end-4",
    "row-end-5",
    "row-end-6",
    "row-end-7",
    "row-end-8",
    "row-end-9",
    "row-end-10",
    "row-end-11",
    "row-end-12",
    "row-span-1",
    "row-span-2",
    {
      pattern: /(bg|text|border)-(sage)/,
    },
    {
      pattern: /grid-rows-[^\s]+/,
    },
    {
      pattern: /(mt|mb|mr|ml|my|mx|px|py|pt|pb|pl|pr)-[0-9]+/,
    },
    {
      pattern: /flex-.*/,
    },
    {
      pattern: /(bottom|right|top|left)-[0-9]+/,
    },
    {
      pattern: /(w|h)-[0-9]+/,
    },
  ],
  plugins: ["prettier-plugin-tailwindcss"],
};
