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

            var localSettings = Windows.Storage.ApplicationData.current.localSettings;

            localSettings.values['simpleNumber'] = 42;
            localSettings.values['simpleString'] = "The answer to life, the Universe and everything";
            
            var accountDetails = new Windows.Storage.ApplicationDataCompositeValue();
            accountDetails["holder"] = "Smith";
            accountDetails["currency"] = "$";
            accountDetails["amount"] = 4000;

            localSettings.values["account"] = accountDetails;

            var settingsList = document.getElementById("settings-list");

            settingsList.innerHTML += "<li> localSettings.values['simpleNumber'] is " + localSettings.values['simpleNumber'] + "</li>";
            settingsList.innerHTML += "<li> localSettings.values['simpleString'] is " + localSettings.values['simpleString'] + "</li>";
            settingsList.innerHTML += "<li> localSettings.values['account'] is " +
                localSettings.values['account'] + " with values " + JSON.stringify(localSettings.values['account']) +
                "<ul>" + "<li>" + "Note: account is a 'composite' setting and will hold either all its values, or none of them if the write operation fails (i.e. composite values are atomic)" + "</li>" + "</ul>";
            
                settingsList.innerHTML += "<li>localSettings.values can be JSON.stringify-ed: <br>" + JSON.stringify(localSettings.values, null, " ") + "</li>";

        }
    };

    app.start();
})();
