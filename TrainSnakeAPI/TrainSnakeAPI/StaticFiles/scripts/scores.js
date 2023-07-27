// Fetch data from the backend API
fetch('../../api/scoreboard', {
	method: 'GET',
	headers: {
		'Content-Type': 'application/json',
		'Authorization': `Bearer ${localStorage.getItem('access_token')}`,
		// Add any required authentication headers if needed
	}
})
	.then(function (response) {
		if (response.status === 401) {
			window.location.href = '/';
		}

		return response.json();
	})
	.then((data) => {
    // Iterate over the scores and add rows to the scoreboard
    const scoreboardBody = document.getElementById("table-body-scores");
		data.forEach((highestScores) => {
      const row = document.createElement("tr");
      const usernameCell = document.createElement("td");
      const scoreCell = document.createElement("td");

			usernameCell.textContent = highestScores.playerName;
			scoreCell.textContent = highestScores.playerScore;

      row.appendChild(usernameCell);
      row.appendChild(scoreCell);
      scoreboardBody.appendChild(row);
    });
  })
  .catch((error) => {
    console.error("Error fetching scores:", error);
	});


