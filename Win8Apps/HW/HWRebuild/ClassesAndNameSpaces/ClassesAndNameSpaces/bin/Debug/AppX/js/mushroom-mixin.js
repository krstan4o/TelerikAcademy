
var MushroomMixin = {
    weight: 0,
    grow:function (watter) {
        this.weight += watter / 2;
        if (this.length) {
            this.length += watter / 2;
        }
        else if (this.radius) {
            this.radius += watter / 2;
        }
    }
}