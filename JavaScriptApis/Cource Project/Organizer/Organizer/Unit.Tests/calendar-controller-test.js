/// <reference path="../scripts/calendar-controller.js" />
/// <reference path="../scripts/contact-controller.js" />
/// <reference path="../scripts/enums.js" />

module("CalendarController.init");

test("When CalendarController.Task is initialized, should set the correct values", function () {
    var title = "Test Task";
    var color = "#000000";
    var priority = Priority.high;
    var comment = "Test comment";
    var date = "1.1.2011";
    var category = CategoryType.work;
    var duration = 6;
    var location = "Sofia";

    var task = new calendarController.Task(title, color, priority, comment,
        date, category, duration, location);

    ok(true, task.isPrototypeOf(calendarController.Task),"task.isPrototypeOf(calendarController.Task)");
    equal(title, task.title, "Title is set");
    equal(color, task.color, "Color is set");
    equal(priority, task.priority, "Priority is set");
    equal(comment, task.comment, "Comment is set");
    equal(date, task.date, "Date is set");
    equal(category, task.categoryType, "Category is set");
    equal(duration, task.duration, "Duration is set");
    equal(location, task.location, "Location is set");
});

test("When CalendarController.Project is initialized, should set the correct values", function () {
    var title = "Test Project";
    var color = "#000000";
    var priority = Priority.high;
    var comment = "Test comment";
    var tasks = null;
    var project = new calendarController.Project(title, color, priority, comment, tasks)

    equal(title, project.title, "Title is set");
    equal(color, project.color, "Color is set");
    equal(priority, project.priority, "Priority is set");
    equal(comment, project.comment, "Comment is set");
    equal(tasks, project.tasks, "Tasks is set");
});

test("When CalendarController.Note is initialized, should set the correct values", function () {
    var title = "Test Note";
    var color = "#000000";
    var priority = Priority.high;
    var comment = "Test comment";
    var content = "Test content"

    var note = new calendarController.Note(title, color, priority, comment, content);

    equal(title, note.title, "Title is set");
    equal(color, note.color, "Color is set");
    equal(priority, note.priority, "Priority is set");
    equal(comment, note.comment, "Comment is set");
    equal(content, note.content, "Content is set");
});

module("CalendarController.Change");

test("When CalendarController.Task is changed, should set the correct values", function () {
    var task = new calendarController.Task("Task1", "#000000",
        Priority.critical, "Comment1", "1.1.2011", CategoryType.TelerikAcademy, 1, "");

    task.changeCategoryType(CategoryType.social);
    task.changePriority(Priority.medium);
    task.changeDuration(8);
    task.changeLocation("Sofia");

    equal(CategoryType.social, task.categoryType, "Category is changed");
    equal(Priority.medium, task.priority, "Priority is changed");
    equal(8, task.duration, "Duration is changed");
    equal("Sofia", task.location, "Location is changed");
});

test("When CalendarController.Note is changed, should set the correct values", function () {
    var note = new calendarController.Note("note1", "#000000",
        Priority.critical, "Comment1", "");

    note.changeContent("New Content");
    note.changeColor("#111111");
    note.changePriority(Priority.low);

    equal("New Content", note.content, "Content is changed");
    equal("#111111", note.color, "Color is changed");
    equal(Priority.low, note.priority, "Priority is changed");
});


