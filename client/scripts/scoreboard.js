const urlParams = new URLSearchParams(window.location.search);
const accessToken = urlParams.get("access_token");

localStorage.setItem("access_token", accessToken);

const scoreBoardRoute = () => {
  const apiUrl = config.apiUrl + "/scoreboard";

  fetch(apiUrl, {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
      Authorization: "Bearer ${accessToken}",
      // Add any required authentication headers if needed
    },
  })
    .then((response) => response.json())
    .then((data) => {
      // Check the response from the API and redirect accordingly
      if (data.success) {
        // Redirect to the success screen
        window.location.href = "./scores.html";
      } else {
        // Redirect to the error screen
        window.location.href = "./login.html";
      }
    })
    .catch((error) => {
      // Handle any errors that may occur during the API call
      console.error("Error:", error);
    });
};

document.getElementById("scoreBoardLink").addEventListener("click", (event) => {
  event.preventDefault(); // Prevent the default link behavior
  scoreBoardRoute(); // Call the gameRoute function
});
