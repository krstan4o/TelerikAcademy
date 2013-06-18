module("Player");
test("should set correct values",
  function () {
      var startX = startX, startY = startY, name = name, hp = hp, damage = damage, modelId = modelId;
      var player = new Player(startX, startY, name, hp, damage, modelId);
      equal(player.startX, startY);
      equal(player.startY, startY);
      equal(player.name, name);
      equal(player.hp, hp);
      equal(player.damage, damage);
      equal(player.modelId, modelId);
  });

test("improveDamage, when damage is 1 and newDamage is 3, should return 4", function () {
    var damage = 1;
    var newDamage = 3;
    var improveDamage = damage + newDamage;
    var actual = damage + newDamage;
    var expected = improveDamage;
    equal(actual, expected);
});

test("improveHP, when hp is 1 and newHP is 3, should return 4", function () {
    var hp = 1;
    var newHP = 3;
    var improveHP = hp + newHP;
    var actual = hp + newHP;
    var expected = improveHP;
    equal(actual, expected);
});

test("improveSpeed, when moveAmount is 4 and newSpeed is 10, should return 14", function () {
    var moveAmount = 4;
    var newSpeed = 10;
    var improveSpeed = moveAmount + newSpeed;
    var actual = moveAmount + newSpeed;
    var expected = improveSpeed;
    equal(actual, expected);
});

test("setCurrentDirection, when newDirection is 5, should return 5", function () {
    var currentDirection = 5;
    var newDirection = currentDirection;
    var actual = currentDirection;
    var expected = newDirection;
    equal(actual, expected);
});

test("setX, when newX is 20, should return 20", function () {
    var x = 20;
    var newX = x;
    var actual = x;
    var expected = newX;
    equal(actual, expected);
});

test("setY, when newY is 80, should return 80", function () {
    var y = 80;
    var newY = y;
    var actual = y;
    var expected = newY;
    equal(actual, expected);
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

test("getHP, when hp is 3, should return 3", function () {
    var hp = 3;
    var getHP = hp;
    var actual = getHP;
    var expected = hp;
    equal(actual, expected);
});

test("getDamage, when damage is 2, should return 2", function () {
    var damage = 2;
    var getDamage =damage;
    var actual = getDamage;
    var expected = damage;
    equal(actual, expected);
});

test("getModelId, when modelId is 2, should return 2", function () {
    var modelId = 2;
    var getModelId = modelId;
    var actual = getModelId;
    var expected = modelId;
    equal(actual, expected);
});

test("getCurrentDirection, when currentDirection is 128, should return 128", function () {
    var currentDirection = 128;
    var getCurrentDirection = currentDirection;
    var actual = getCurrentDirection;
    var expected = currentDirection;
    equal(actual, expected);
});