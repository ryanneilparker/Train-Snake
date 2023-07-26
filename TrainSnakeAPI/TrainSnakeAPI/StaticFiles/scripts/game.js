import { Snake } from "./classes/snake.js";
import { Food } from "./classes/food.js";

const playBoard = document.querySelector(".play-board");
const scoreElement = document.querySelector(".score");
const highScoreElement = document.querySelector(".high-score");

const heightWidth = 20;
const food = new Food(heightWidth);
const snake = new Snake(5,5,heightWidth);
let setIntervalId;
let score = 0;
let trainImage = '../resources/train.png';
let trainCartImage = '../resources/car.png';


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
    
    let html = `<img class="food" 
    src="../resources/beer.png"
    style="grid-area: ${food.getFoodPosition()[1]} / ${food.getFoodPosition()[0]}"
    ></img>`;

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

    let snakeBody = snake.getSnakeBody();

    for (let i = 0; i < snakeBody.length; i++) {
        let image = trainImage;
        if(i > 0){
          image = trainCartImage;
        }
        html += `<img class="head" 
        src="${image}" 
        style="grid-area: ${snakeBody[i][1]} / ${snakeBody[i][0]}"
        ></img>`;
    }
    playBoard.innerHTML = html;
}

food.updateFoodPosition();
setIntervalId = setInterval(initGame, 150);
document.addEventListener("keydown", changeDirection);