var Utils = {
    toString: function () {
        var result = [];
        for (var prop in this) {
            if (this.hasOwnProperty(prop) && this[prop] !== "function" && this[prop] !== undefined) {
                result.push(prop + ": " + this[prop]);
            }
        }

        return result.join('\n');
    },

    //"2013-07-13T21:00:00.000Z"
    parseDay: function (date) {
        var result = new Date("2013-07-13T21:00:00.000Z");
        return result;
    },

    inherit: function (Child, Parent) {
        Child.prototype = Object.create(Parent.prototype);
        Child.constructor = Child;

        return Child;
    },

    merge: function (source, target) {
        for (var prop in target) {
            if (target.hasOwnProperty(prop)) {
                source[prop]= target[prop];
            }
        }

        return source;
    }
};
