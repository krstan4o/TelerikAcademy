// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232509
(function () {
    "use strict";

    WinJS.Binding.optimizeBindingReferences = true;

    var app = WinJS.Application;
    var activation = Windows.ApplicationModel.Activation;

    app.onactivated = function (args) {
        if (args.detail.kind === activation.ActivationKind.launch) {

            args.setPromise(WinJS.UI.processAll());

            var roamingSettings = Windows.Storage.ApplicationData.current.roamingSettings;
            var roamingStorageQuota = Windows.Storage.ApplicationData.current.roamingStorageQuota;

            Windows.Storage.ApplicationData.current.addEventListener("datachanged", populateSettingsList);

            roamingSettings.values['simpleNumber'] = 42;
            roamingSettings.values['simpleString'] = "The answer to life, the Universe and everything";

            var accountDetails = new Windows.Storage.ApplicationDataCompositeValue();
            accountDetails["holder"] = "Smith";
            accountDetails["currency"] = "$";
            accountDetails["amount"] = 4000;

            roamingSettings.values["account"] = accountDetails;

            var roamingQuotaDisplay = document.getElementById("roaming-storage-quota-display");
            roamingQuotaDisplay.innerText = roamingStorageQuota;

            document.getElementById("simulate-datachanged-button").addEventListener("click",
                function () {
                    Windows.Storage.ApplicationData.current.signalDataChanged();
                });

            document.getElementById("change-setting-button").addEventListener("click",
                function () {
                    var simpleNumberSettingValue = roamingSettings.values['simpleNumber'];
                    roamingSettings.values['simpleNumber'] = simpleNumberSettingValue - 1;
                    var simpleNumberSettingValue = roamingSettings.values['simpleNumber'];
                });
        }
    };

    var populateSettingsList = function () {
        var settingsList = document.getElementById("settings-list");

        settingsList.innerHTML = "";

        var roamingSettings = Windows.Storage.ApplicationData.current.roamingSettings;

        settingsList.innerHTML += "<li> roamingSettings.values['simpleNumber'] is " + roamingSettings.values['simpleNumber'] + "</li>";
        settingsList.innerHTML += "<li> roamingSettings.values['simpleString'] is " + roamingSettings.values['simpleString'] + "</li>";
        settingsList.innerHTML += "<li> roamingSettings.values['account'] is " +
            roamingSettings.values['account'] + " with values " + JSON.stringify(roamingSettings.values['account']) +
            "<ul>" + "<li>" + "Note: account is a 'composite' setting and will hold either all its values, or none of them if the write operation fails (i.e. composite values are atomic)" + "</li>" + "</ul>";

        settingsList.innerHTML += "<li>roamingSettings.values can be JSON.stringify-ed: <br>" + JSON.stringify(roamingSettings.values, null, " ") + "</li>";
    }

    app.start();
})();
