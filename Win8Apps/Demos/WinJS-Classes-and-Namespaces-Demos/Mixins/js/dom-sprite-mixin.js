var DomSpriteMixin = {
    imgSrc: "",
    position: {
        leftPx: 0,
        topPx: 0
    },

    spriteContainer: null,

    imageElement: null,

    updateImgSrc: function(imgSrc){
        this.imgSrc = imgSrc || this.imgSrc;
        if(this.imageElement){
            this.imageElement.setAttribute("src", this.imgSrc);
        }
    },

    updatePosition: function(positionTopLeftPx){
        this.position = positionTopLeftPx || this.position;
        if(this.spriteContainer){
            //this.spriteContainer.setAttribute("style", "position:absolute; top:" + this.position.topPx + "px; left:" + this.position.leftPx + "px;");
            this.spriteContainer.style.position = "absolute";
            this.spriteContainer.style.top = this.position.topPx + "px";
            this.spriteContainer.style.left = this.position.leftPx + "px";
        }
    },

    updateToDom: function () {
        if (!this.spriteContainer) {
            this.spriteContainer = document.createElement("div");
            document.body.appendChild(this.spriteContainer);
            
            this.imageElement = document.createElement("img");
            this.imageElement.src = "#";
            this.spriteContainer.appendChild(this.imageElement);
        }

        this.updatePosition();
        this.updateImgSrc();
    }
}