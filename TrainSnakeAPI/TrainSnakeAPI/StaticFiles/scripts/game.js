import { Snake } from "./classes/snake.js";
import { Food } from "./classes/food.js";

const playBoard = document.querySelector(".play-board");
const scoreElement = document.querySelector(".score");
const highScoreElement = document.querySelector(".high-score");


const trainImage = '../resources/train.png';
const trainCartImage = '../resources/car.png';
const beerImage = "../resources/beer.png";
const heightWidth = 20;
const food = new Food(heightWidth,beerImage);
const snake = new Snake(5,5,heightWidth,trainImage,trainCartImage);
let setIntervalId;
let score = 0;

const urlParams = new URLSearchParams(window.location.search);
const accessTokenUrl = urlParams.get('access_token');
const playerNameUrl = urlParams.get('playerName');

if (localStorage.getItem('access_token') === null || localStorage.getItem('playerName') === null) {
	localStorage.setItem('access_token', accessTokenUrl);
	localStorage.setItem('playerName', playerNameUrl);
}

if ((accessTokenUrl === null || playerNameUrl === null) && (localStorage.getItem('access_token') === null || localStorage.getItem('playerName') === null)) {
	console.log("Redirecting");
	window.location.href = '/';
}

// Getting high score from API
let highScore = 0;
highScoreElement.innerText = `High Score: ${highScore}`;

const playerName = localStorage.getItem('playerName');

fetch(`../../api/player/score?playerName=${encodeURIComponent(playerName)}`, {
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
		highScore = data.score;	
		highScoreElement.innerText = `High Score: ${highScore}`;

		food.updateFoodPosition();
		setIntervalId = setInterval(initGame, 150);
		document.addEventListener("keydown", changeDirection);
	})
	.catch((error) => {
		console.error("Error fetching player score:", error);
	});

const sendScoreToAPI = () => {
	console.log("Running post request");
	let currentPlayerScore = score;
	let playerName = localStorage.getItem("playerName");

	const ScoreResult = {
		playerName: playerName,
		playerScore: parseInt(currentPlayerScore),
	};

	console.log(ScoreResult);

	return fetch('../../api/player/score', {
		method: 'POST',
		headers: {
			'Content-Type': 'application/json',
			'Authorization': `Bearer ${localStorage.getItem('access_token')}`,
			// Add any required authentication headers if needed
		},
		body: JSON.stringify(ScoreResult),
	})
		.then(function (response) {
			if (response.status === 401) {
				window.location.href = '/';
			}

			return response.json();
		})
		.catch((error) => {
			console.error("Error fetching player score:", error);
		});
}

// Gameplay
const handleGameOver = () => {
		clearInterval(setIntervalId);
	sendScoreToAPI()
		.then(() => {
			alert("Game Over! Press OK to replay...");
			location.reload();
		})
		.catch((error) => {
			console.error("Error sending player score:", error);
			alert("Game Over! Press OK to replay...");
			location.reload();
		});
}

const changeDirection = e => {
    snake.changeDirection(e);
}

const initGame = () => {
    
    let html = food.getHtml();

    if(snake.getSnakePosition()[0] == food.getFoodPosition()[0] && snake.getSnakePosition()[1] == food.getFoodPosition()[1]) {
        console.log("hit")
        food.updateFoodPosition();
        snake.addFood(food.getFoodPosition()[1],food.getFoodPosition()[0]);
        score++; 
        highScore = score >= highScore ? score : highScore;
        scoreElement.innerText = `Score: ${score}`;
        highScoreElement.innerText = `High Score: ${highScore}`;
    }

    snake.updateSnake();

    if (snake.checkCollsion()){
      return handleGameOver();
    }

    html += snake.getHtml();

    playBoard.innerHTML = html;
}


