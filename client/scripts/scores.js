// Fetch data from the backend API
fetch("https://api.example.com/scores")
  .then((response) => response.json())
  .then((data) => {
    // Iterate over the scores and add rows to the scoreboard
    const scoreboardBody = document.getElementById("scoreboard-body");
    data.forEach((score) => {
      const row = document.createElement("tr");
      const usernameCell = document.createElement("td");
      const scoreCell = document.createElement("td");

      usernameCell.textContent = score.username;
      scoreCell.textContent = score.score;

      row.appendChild(usernameCell);
      row.appendChild(scoreCell);
      scoreboardBody.appendChild(row);
    });
  })
  .catch((error) => {
    console.error("Error fetching scores:", error);
  });