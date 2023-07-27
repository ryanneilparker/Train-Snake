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

// Getting high score from the local storage
let highScore = localStorage.getItem("high-score") || 0;
highScoreElement.innerText = `High Score: ${highScore}`;

const handleGameOver = () => {
    clearInterval(setIntervalId);
    alert("Game Over! Press OK to replay...");
    location.reload();
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
        localStorage.setItem("high-score", highScore);
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

food.updateFoodPosition();
setIntervalId = setInterval(initGame, 150);
document.addEventListener("keydown", changeDirection);