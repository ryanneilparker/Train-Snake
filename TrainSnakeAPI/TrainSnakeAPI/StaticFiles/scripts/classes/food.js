export class Food{

  constructor(heightWidth, beerImage) {
    this.heightWidth = heightWidth;
    this.beerImage = beerImage;
  }

  updateFoodPosition(){
    // Passing a random 1 - 30 value as food position
    this.foodX = Math.floor(Math.random() * this.heightWidth) + 1;
    this.foodY = Math.floor(Math.random() * this.heightWidth) + 1;
  }

  getFoodPosition(){
    return [this.foodX,this.foodY];
  }

  getHtml(){
    let html = `<img class="food" 
    src=${this.beerImage}
    style="grid-area: ${this.foodY} / ${this.foodX}"
    ></img>`;
    return html;
  }
}
