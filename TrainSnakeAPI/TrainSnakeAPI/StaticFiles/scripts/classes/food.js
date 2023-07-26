export class Food{

  constructor(heightWidth) {
    this.heightWidth = heightWidth;
  }

  updateFoodPosition(){
    // Passing a random 1 - 30 value as food position
    this.foodX = Math.floor(Math.random() * this.heightWidth) + 1;
    this.foodY = Math.floor(Math.random() * this.heightWidth) + 1;
  }

  getFoodPosition(){
    return [this.foodX,this.foodY];
  }
}
