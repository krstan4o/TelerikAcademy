/// <reference path="object_utils.js" />
/// <reference path="external/prototype.js" />
/// <reference path="external/jquery-2.0.2.js" />

var calendarController = (function () {
    var CalendarEvent = function (title, color, priority, comment) {
        this.title = title;
        this.color = color;
        this.priority = priority;
        this.comment = comment;
    };
    CalendarEvent.prototype = {
        changeColor: function (newColor) {
            this.color = newColor;
        },
        changePriority: function (newPriority) {
            this.priority = newPriority;
        },
        toString: Utils.toString
    };

    //-----------TASK---------------------------------
    Utils.inherit(Task, CalendarEvent);
    function Task(title, color, priority, comment,
        date, categoryType, duration, location) {
        CalendarEvent.apply(this, arguments);
        this.date = date;
        this.categoryType = categoryType;
        this.duration = duration;
        this.location = location;
    };

    Utils.merge(Task.prototype, {
        changeDate: function (newDate) {
            this.date = newDate;
        },
        changeCategoryType: function (newCategoryType) {
            this.categoryType = newCategoryType;
        },
        changeDuration: function (newDuration) {
            this.duration = newDuration;
        },
        changeLocation: function (newLocation) {
            this.location = newLocation;
        },
        toString: Utils.toString
    });

    //--------------NOTE-------------------------------------
    Utils.inherit(Note, CalendarEvent);
    function Note(title, color, priority, comment, content) {
        CalendarEvent.apply(this, arguments);
        this.content = content;
    };

    Utils.merge(Note.prototype, {
        changeContent: function (newContent) {
            this.content = newContent;
        },
        toString: Utils.toString
    });

    //--------------PROJECT----------------------------------
    Utils.inherit(Project, CalendarEvent);
    function Project(title, color, priority, comment, tasks) {
        CalendarEvent.apply(this, arguments);
        this.tasks = tasks;
    };

    Utils.merge(Project.prototype, {
        addTask: function (task) {
            if (task !== undefined) {
                this.tasks.push(task);
            }
        },
        removeTask: function (task) {
            var indexOfTask = this.tasks.indexOf(task);
            this.tasks.splice(indexOfTask, 1);
        }
    });

    return {
        Task: Task,
        Note: Note,
        Project: Project
    };
})();




