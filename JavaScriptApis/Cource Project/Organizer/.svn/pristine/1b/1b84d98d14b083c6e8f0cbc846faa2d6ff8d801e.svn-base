/// <reference path="external/jquery-2.0.2.js" />
/// <reference path="object_utils.js" />
/// <reference path="external/_vs2012.intellisense.js" />
//var $j = jQuery.noConflict();
var contactController = (function () {
    //---------------CONTACT------------------------------------
    var Contact = function (firstName, mobileNumber,
        phoneNumber, email, address, website, image) {
        this.firstName = firstName;
        this.mobileNumber = mobileNumber;
        this.phoneNumber = phoneNumber;
        this.email = email;
        this.address = address;
        this.website = website;
        this.image = image;
    };
    Contact.prototype = {
        editName: function (firstName) {
            this.firstName = firstName;
        },
        editMobile: function (mobileNumber) {
            this.mobileNumber = mobileNumber;
        },
        editPhone: function (phoneNumber) {
            this.phoneNumber = phoneNumber;
        },
        editEmail: function (email) {
            this.email = email;
        },
        editAddress: function (address) {
            this.address = address;
        },
        editWebsite: function (website) {
            this.website = website;
        },
        editImage: function (image) {
            this.image = image;
        },
    };

    //------------PERSON------------------------------------
    Utils.inherit(Person, Contact);
    function Person(firstName, mobileNumber,
                        phoneNumber, email, address, website, image,
                        lastName, middleName, birthDate, occupation) {
        Contact.apply(this, arguments);
        this.lastName = lastName;
        this.middleName = middleName;
        this.birthDate = birthDate;
        this.occupation = occupation;
    };
    Utils.merge(Person.prototype, {
        editLName: function (lastName) {
            this.lastName = lastName;
        },
        editMName: function (middleName) {
            this.middleName = middleName;
        },
        editBirthDate: function (birthDate) {
            this.birthDate = birthDate;
        },
        editOccupation: function (occupation) {
            this.occupation = occupation;
        },
        toString: Utils.toString
    });

    //----------------CORPORATIVE---------------------------
    Utils.inherit(Corporative, Contact);
    function Corporative(firstName, mobileNumber,
                        phoneNumber, email, address, website, image,
                        bulStat, workSector) {
        Contact.apply(this, arguments);
        this.bulStat = bulStat;
        this.workSector = workSector;
    };

    Utils.merge(Corporative.prototype, {
        editBulStat: function (bulStat) {
            this.bulStat = bulStat;
        },
        editWorkSector: function (workSector) {
            this.workSector = workSector;
        },
        toString: Utils.toString
    });

    // person inheritors

    //-----------------BUSINESS----------------------------
    Utils.inherit(Business, Person);
    function Business(firstName, mobileNumber,
                        phoneNumber, email, address, website, image,
                        lastName, middleName, birthDate, occupation,
                        organization) {
        Person.apply(this, arguments);
        this.organization = organization;
    };

    Utils.merge(Business.prototype, {
        editOrganization: function (organization) {
            this.organization = organization;
        },
        toString: Utils.toString
    });

    //-------------FAMILY MEMBER------------------------
    Utils.inherit(FamilyMember, Person);
    function FamilyMember(firstName, mobileNumber,
        phoneNumber, email, address, website, image,
        lastName, middleName, birthDate, occupation,
        bloodGroup, egn, familyMemberType) {
        Person.apply(this, arguments);
        this.bloodGroup = bloodGroup;
        this.egn = egn;
        this.familyMemberType = familyMemberType;
    };
    Utils.merge(FamilyMember, {
        editBloodGroup: function (bloodGroup) {
            this.bloodGroup = bloodGroup;
        },
        editEgn: function (egn) {
            this.egn = egn;
        },
        editFamilyMemberType: function (familyMemberType) {
            this.familyMemberType = familyMemberType;
        },
        toString: Utils.toString
    });

    Utils.inherit(Friend, Person);
    function Friend(firstName, mobileNumber,
                        phoneNumber, email, address, website, image,
                        lastName, middleName, birthDate, occupation,
                        nickName) {
        Person.apply(this, arguments);
        this.nickName = nickName;
    };
    Utils.merge(Friend.prototype, {
        editnickName: function (nickName) {
            this.nickName = nickName;
        },
        toString: Utils.toString
    });

    return {
        Business: Business,
        FamilyMember: FamilyMember,
        Friend: Friend,
        Corporative: Corporative
    };
})();