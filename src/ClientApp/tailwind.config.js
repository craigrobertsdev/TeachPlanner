/** @type {import('tailwindcss').Config} */
export default {
  content: ["./index.html", "./src/**/*.{js,jsx,ts,tsx}"],
  theme: {
    extend: {
      colors: {
        primary: "#EEE6DE",
        sage: "#90AEB2",
        darkGreen: "#37514D",
        peach: "#DD8E75",
        ceramic: "#B6594C",
      },
    },
    height: {
      screen: "100dvh",
    },
    minHeight: {
      screen: "100dvh",
    },
  },
  plugins: ["prettier-plugin-tailwindcss"],
};
