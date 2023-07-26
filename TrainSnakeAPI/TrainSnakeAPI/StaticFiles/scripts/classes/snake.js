export class Snake{

  constructor(snakeX, snakeY, heightWidth) {
    this.heightWidth = heightWidth;
    this.snakeX = snakeX;
    this.snakeY = snakeY;
    this.velocityX = 0;
    this.velocityY = 0;
    this.snakeBody = [];
  }

  changeDirection(e) {
    // Changing velocity value based on key press
    if(e.key === "ArrowUp" && this.velocityY != 1) {
        this.velocityX = 0;
        this.velocityY = -1;
    } else if(e.key === "ArrowDown" && this.velocityY != -1) {
        this.velocityX = 0;
        this.velocityY = 1;
    } else if(e.key === "ArrowLeft" && this.velocityX != 1) {
        this.velocityX = -1;
        this.velocityY = 0;
    } else if(e.key === "ArrowRight" && this.velocityX != -1) {
        this.velocityX = 1;
        this.velocityY = 0;
    }
  }

  updateSnake(){
    this.snakeX += this.velocityX;
    this.snakeY += this.velocityY;

      // Shifting forward the values of the elements in the snake body by one
      for (let i = this.snakeBody.length - 1; i > 0; i--) {
        this.snakeBody[i] = this.snakeBody[i - 1];
      }
      this.snakeBody[0] = [this.snakeX, this.snakeY]; // Setting first element of snake body to current snake position
  }

  getSnakePosition(){
    return [this.snakeX,this.snakeY];
  }

  getSnakeBody(){
    return this.snakeBody;
  }

  addFood(foodY, foodX){
    this.snakeBody.push([foodY, foodX]);
  }

  checkCollsion(){
    if(this.snakeX <= 0 || this.snakeX > this.heightWidth || this.snakeY <= 0 || this.snakeY > this.heightWidth ) {
      return true; 
    }

    for (let i = 0; i < this.snakeBody.length; i++) {
      if (i !== 0 && this.snakeBody[0][1] === this.snakeBody[i][1] && this.snakeBody[0][0] === this.snakeBody[i][0]) {
          return true;
      }
    }
  }

}