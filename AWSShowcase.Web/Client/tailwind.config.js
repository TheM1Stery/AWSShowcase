/** @type {import('tailwindcss').Config} */
export default {
  content: [
      "./../Pages/**/*.cshtml",
      "./src/**/*.{html,js}",
      "./node_modules/flowbite/**/*.js",
      "./../**/*.cshtml"
  ],
  theme: {
    extend: {},
  },
  plugins: [
      require("flowbite/plugin")
  ], 
  darkMode: "class" 
}

