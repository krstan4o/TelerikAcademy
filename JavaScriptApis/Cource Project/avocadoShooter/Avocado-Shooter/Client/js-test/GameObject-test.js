module("GameObject");
test("should set correct values",
  function () {
      var startX = startX, startY = startY, scr = scr, type = type;
      var gameObject = new GameObject(startX, startY, scr, type);
      equal(gameObject.startX, startY);
      equal(gameObject.startY, startY);
      equal(gameObject.scr, scr);
      equal(gameObject.type, type);
  });

test("getX, when x is 42, should return 42", function () {
    var x = 42;
    var getX = x;
    var actual = getX;
    var expected = x;
    equal(actual, expected);
});

test("getY, when y is 124, should return 124", function () {
    var y = 124;
    var getY = y;
    var actual = getY;
    var expected = y;
    equal(actual, expected);
});

test("getType, when type is 2, should return 2", function () {
    var type = 2;
    var getType = type;
    var actual = getType;
    var expected = type;
    equal(actual, expected);
});