export const KEY = "ui-theme";

export const getTheme = () => {
  return localStorage.getItem(KEY) || "system";
}

export const setTheme = (theme) => {
  localStorage.setItem(KEY, theme);

  // clear
  const root = window.document.documentElement;
  root.classList.remove("light", "dark");

  // if not passed down from the top, then set as the current one
  theme = theme || getTheme();

  if (theme === "system") {
    const systemTheme = window.matchMedia("(prefers-color-scheme: dark)").matches
      ? "dark"
      : "light"

    root.classList.add(systemTheme);
    return;
  }

  root.classList.add(theme);
}