/// <reference path="../scripts/calendar-controller.js" />
/// <reference path="../scripts/contact-controller.js" />
/// <reference path="../scripts/enums.js" />

module("contact-controller.init");

test("When ContactController.Business is initialized, should set the correct values", function () {
    var firstName = "FName";
    var mobileNumber = "0888888888";
    var phoneNumber = "2134123";
    var email = "";
    var address = "";
    var website = "";
    var image = null;
    var lastName = "";
    var middleName = null;
    var birthDate = null;
    var occupation = null;
    var organization = null;

    var contact = new contactController.Business(firstName, mobileNumber,
                        phoneNumber, email, address, website, image,
                        lastName, middleName, birthDate, occupation,
                        organization);

    ok(true, contact.isPrototypeOf(contactController.Business));
    equal(firstName, contact.firstName, "firstName is set");
    equal(mobileNumber, contact.mobileNumber, "mobileNumber is set");
    equal(phoneNumber, contact.phoneNumber, "phoneNumber is set");
    equal(email, contact.email, "email is set");
    equal(address, contact.address, "address is set");
    equal(website, contact.website, "website is set");
    equal(middleName, contact.middleName, "middleName is set");
    equal(birthDate, contact.birthDate, "birthDate is set");
    equal(occupation, contact.occupation, "occupation is set");
    equal(organization, contact.organization, "organization is set");

});

test("When ContactController.FamilyMember is initialized, should set the correct values", function () {
    var firstName = "FName";
    var mobileNumber = "0888888888";
    var phoneNumber = "2134123";
    var email = "";
    var address = "";
    var website = "";
    var image = null;
    var lastName = "";
    var middleName = null;
    var birthDate = null;
    var occupation = null;
    var organization = null;

    var contact = new contactController.FamilyMember(firstName, mobileNumber,
                        phoneNumber, email, address, website, image,
                        lastName, middleName, birthDate, occupation,
                        organization);

    ok(true, contact.isPrototypeOf(contactController.FamilyMember),"contact.isPrototypeOf(contactController.FamilyMember)");
    equal(firstName, contact.firstName, "firstName is set");
    equal(mobileNumber, contact.mobileNumber, "mobileNumber is set");
    equal(phoneNumber, contact.phoneNumber, "phoneNumber is set");
    equal(email, contact.email, "email is set");
    equal(address, contact.address, "address is set");
    equal(website, contact.website, "website is set");
    equal(middleName, contact.middleName, "middleName is set");
    equal(birthDate, contact.birthDate, "birthDate is set");
    equal(occupation, contact.occupation, "occupation is set");
    equal(organization, contact.organization, "organization is set");

});

test("When ContactController.Friend is initialized, should set the correct values", function () {
    var firstName = "FName";
    var mobileNumber = "0888888888";
    var phoneNumber = "2134123";
    var email = "";
    var address = "";
    var website = "";
    var image = null;
    var lastName = "";
    var middleName = null;
    var birthDate = null;
    var occupation = null;
    var nickname = null;

    var contact = new contactController.Friend(firstName, mobileNumber,
                        phoneNumber, email, address, website, image,
                        lastName, middleName, birthDate, occupation,
                        nickname);

    ok(true, contact.isPrototypeOf(contactController.Friend),"contact.isPrototypeOf(contactController.Friend)");
    equal(firstName, contact.firstName, "firstName is set");
    equal(mobileNumber, contact.mobileNumber, "mobileNumber is set");
    equal(phoneNumber, contact.phoneNumber, "phoneNumber is set");
    equal(email, contact.email, "email is set");
    equal(address, contact.address, "address is set");
    equal(website, contact.website, "website is set");
    equal(middleName, contact.middleName, "middleName is set");
    equal(birthDate, contact.birthDate, "birthDate is set");
    equal(occupation, contact.occupation, "occupation is set");
    equal(nickname, contact.nickName, "nickname is set");

});

test("When ContactController.Corporative is initialized, should set the correct values", function () {
    var firstName = "FName";
    var mobileNumber = "0888888888";
    var phoneNumber = "2134123";
    var email = "";
    var address = "";
    var website = "";
    var image = null;
    var bulStat = "435234523";
    var workSector = "IT";

    var contact = new contactController.Corporative(firstName, mobileNumber,
                        phoneNumber, email, address, website, image,
                        bulStat, workSector);

    ok(true, contact.isPrototypeOf(contactController.Friend),"contact.isPrototypeOf(contactController.Friend)");
    equal(firstName, contact.firstName, "firstName is set");
    equal(mobileNumber, contact.mobileNumber, "mobileNumber is set");
    equal(phoneNumber, contact.phoneNumber, "phoneNumber is set");
    equal(email, contact.email, "email is set");
    equal(address, contact.address, "address is set");
    equal(website, contact.website, "website is set");
    equal(bulStat, contact.bulStat, "bulStat is set");
    equal(workSector, contact.workSector, "workSector is set");


});


module("contact-controller.Change");

test("When ContactController.Business is changed, should set the correct values", function () {
    var firstName = "FName";
    var mobileNumber = "0888888888";
    var phoneNumber = "2134123";
    var email = "";
    var address = "";
    var website = "";
    var image = null;
    var lastName = "";
    var middleName = null;
    var birthDate = null;
    var occupation = null;
    var organization = null;

    var contact = new contactController.Business(firstName, mobileNumber,
                        phoneNumber, email, address, website, image,
                        lastName, middleName, birthDate, occupation,
                        organization);

    contact.editAddress("NEW");
    contact.editBirthDate("NEW");
    contact.editEmail("NEW");
    contact.editLName("NEW");
    contact.editMName("NEW");

    ok(true, contact.isPrototypeOf(contactController.Business),"contact.isPrototypeOf(contactController.Business)");
    equal("NEW", contact.email, "email is set");
    equal("NEW", contact.address, "address is set");
    equal("NEW", contact.middleName, "middleName is set");
    equal("NEW", contact.birthDate, "birthDate is set");

});

