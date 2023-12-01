/** @type {import('tailwindcss').Config} */
export default {
  content: ["./index.html", "./src/**/*.{js,jsx,ts,tsx}"],
  plugins: ["prettier-plugin-tailwindcss"],
  theme: {
    extend: {
      colors: {
        // core colors
        primary: "#f5f5f7",
        main: "#EEE6DE",
        sage: "#90AEB2",
        darkGreen: "#37514D",
        peach: "#DD8E75",
        ceramic: "#B6594C",

        // light
        lightPeach: "#DD8E7550",
        lightSage: "#90AEB250",

        // hover
        primaryHover: "#9c938a",
        sageHover: "#A3C1C5",
        darkGreenHover: "#2E3F3C",
        peachHover: "#C97C5D",
        ceramicHover: "#A84F42",
        // focus
        primaryFocus: "#BAB1A8",
        sageFocus: "#6D9EA2",

        // border
        primaryFocusBorder: "#999188",

        // disabled
        primaryDisabled: "#F2EDE9",
        sageDisabled: "#D1E0E2",
        darkGreenDisabled: "#5C6D6A",
        peachDisabled: "#E3B8A9",
        ceramicDisabled: "#D08F7F",

        // curriculum
        maths: "#FCCF03",
        english: "#85D42A",
        hass: "2AD48D",
        health: "AD3BFF",
      },
      height: {
        screen: "100dvh",
      },
      maxWidth: {
        "80ch": "80ch",
      },
      minHeight: {
        screen: "100dvh",
      },
      gridRowStart: {
        8: "8",
        9: "9",
        10: "10",
        11: "11",
        12: "12",
      },
    },
    fontFamily: {
      theme: ["'Nunito Sans'", "sans-serif"],
    },
  },
  safelist: [
    "max-w-80ch",
    {
      pattern: /row-start-(1|2|3|4|5|6|7|8|9|10|11|12)/,
    },
    {
      pattern: /col-start-(1|2|3|4|5|6|7|8|9|10)/,
    },
    {
      pattern: /row-span-(1|2)/,
    },
    {
      pattern: /(bg|text|border)-(sage|maths|english|hass|health)/,
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
    {
      pattern: /text-(right|center)/,
    },
  ],
};
