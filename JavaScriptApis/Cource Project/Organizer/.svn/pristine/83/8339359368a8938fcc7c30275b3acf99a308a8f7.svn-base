/// <reference path="jQuery_plugins.js" />
/// <reference path="external/jquery-2.0.2.js" />
/// <reference path="object_utils.js" />
/// <reference path="external/MongoLab.js" />
/// <reference path="calendar-controller.js" />
/// <reference path="contact-controller.js" />
/// <reference path="enums.js" />
//$(function () {
//    var mongoDB = new MongoLab('FqC6oznteOaWZVXL9m8VJzdx5T7Op7Nd');

//    var testEvent = new calendarController.Task('Task1', Color.black, Priority.medium, 'some comment',
//        "2013-07-14T21:00:00.000Z", CategoryType.social, '2', 'Sofia');

//    //list databases
//    //mongoDB.listDatabases(function (data) {
//    //    console.log('List Databases : ', data);
//    //});

//    //list collections
//mongoDB.listCollections('team_chili', function (data) {
//    console.log('List Collections : ', data);
//});

//    ////list documents
//    mongoDB.listDocuments('team_chili', 'calendarEvents', { sk: 0, l: 20 }, function (data) {
//        console.log('List Documents : ', data);
//    });

//    ////insert documents
//    //mongoDB.insertDocuments('team_chili', 'calendarEvents', testEvent, function (data) {
//    //    console.log('Insert Documents : ', data);
//    //});

//    //delete document
//    //mongoDB.deleteDocument('team_chili', 'calendarEvents', '51b9e432e4b09971eec76f5d', function (data) {
//    //    console.log('Delete Document : ', data);
//    //});

//});
var mongo = (function () {
    var mongoDB = new MongoLab('FqC6oznteOaWZVXL9m8VJzdx5T7Op7Nd');

    function addEventToDatabase() {
        var eventForm = $('#add-event-form').serializeObject();
        mongoDB.insertDocuments('team_chili', 'calendarEvents', eventForm, function (data) {
            console.log('Inserted Documents : ', data);
        });
    }

    return {
        addEventToDB: addEventToDatabase
    };
})();