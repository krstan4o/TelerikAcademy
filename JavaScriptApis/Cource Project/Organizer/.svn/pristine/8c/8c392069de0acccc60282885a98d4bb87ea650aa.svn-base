if (!Object.prototype.extend) {
    Object.defineProperty(Object.prototype, "extend", {
        enumerable: false,
        //configurable: false,
        //writable: false,
        value: function (properties) {
            function f() { };
            f.prototype = Object.create(this);
            for (var prop in properties) {
                f.prototype[prop] = properties[prop];
            }
            f.prototype._super = this;
            return new f();
        }
    });
}
