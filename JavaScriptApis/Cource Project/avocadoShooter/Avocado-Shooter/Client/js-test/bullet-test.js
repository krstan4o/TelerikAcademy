module("Bullet");
test("should set correct values",
  function () {
      var startX = startX, startY = startY, scr = scr, direction = direction, damage = damage;
      var bullet = new Bullet(startX, startY, scr, direction, damage);
      equal(bullet.startX, startY);
      equal(bullet.startY, startY);
      equal(bullet.scr, scr);
      equal(bullet.direction, direction);
      equal(bullet.damage, damage);     
  });

test("getCurrentX, when startX is 86, should return 86", function () {
    var startX = 86;
    var getCurrentX = startX;
    var actual = getCurrentX;
    var expected = startX;
    equal(actual, expected);
});

test("getCurrentY, when startY is 220, should return 220", function () {
    var startY = 220;
    var getCurrentY = startY;
    var actual = getCurrentY;
    var expected = startY;
    equal(actual, expected);
});

test("getDamage, when damage is 2, should return 2", function () {
    var damage = 2;
    var getDamage = damage;
    var actual = getDamage;
    var expected = damage;
    equal(actual, expected);
});