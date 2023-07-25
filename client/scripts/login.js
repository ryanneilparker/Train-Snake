const loginButton = document.getElementById("loginButton");
const apiUrl = configURL.apiUrl + "/player/login";
loginButton.addEventListener("click", () => {
  // Redirect to your API's login endpoint
  window.location.href = apiUrl;
});
