// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232509
(function () {
    "use strict";

    var app = WinJS.Application;
    var activation = Windows.ApplicationModel.Activation;
    WinJS.strictProcessing();

    app.onactivated = function (args) {
        if (args.detail.kind === activation.ActivationKind.launch) {
            if (args.detail.previousExecutionState !== activation.ApplicationExecutionState.terminated) {
                
            } else {
                
            }
            args.setPromise(WinJS.UI.processAll());

            document.getElementById("open-file-button-id").addEventListener("click", onClicked);

            setInterval(function () {
                var count = window.attachedCount;
                if (count) {
                    count++;
                    window.attachedCount = count;
                }
                else {
                    count = 1;
                    window.attachedCount = count;
                }
                document.getElementById("counter-output-id").innerText = count;
            }, 1000);
        }
    };

    app.oncheckpoint = function (args) {
    };

    var onClicked = function(){
        var picker = new Windows.Storage.Pickers.FileOpenPicker();
        picker.fileTypeFilter.replaceAll([".txt"]);
        picker.pickSingleFileAsync().then(function (pickedFile) {
            document.getElementById("picked-file-output-id").innerText = pickedFile.name;
        })
    }

    app.start();
})();
