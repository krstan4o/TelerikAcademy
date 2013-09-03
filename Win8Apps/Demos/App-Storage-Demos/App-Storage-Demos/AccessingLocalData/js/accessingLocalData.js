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

            var localFolder = Windows.Storage.ApplicationData.current.localFolder;

            document.getElementById("local-data-path-display").innerText = localFolder.path;

            localFolder.
                createFileAsync("fileOverwrittenEveryTime.txt",
                Windows.Storage.CreationCollisionOption.replaceExisting).done(
                function (file) {
                    Windows.Storage.FileIO.writeTextAsync(file, "Some text on a line\r\n");
                    Windows.Storage.FileIO.appendLinesAsync(file, ["And some appended", "lines"]);
                });

            localFolder.
                createFileAsync("fileCreatedOnceAndOpenedEveryOtherTime.txt",
                Windows.Storage.CreationCollisionOption.openIfExists).done(
                function (file) {
                    Windows.Storage.FileIO.appendTextAsync(file, "Appended text");
                    //Windows.Storage.FileIO.writeTextAsync("Single line overwriting text");//if you uncomment this line, the file contents will be overwritten every time (even though the file itself isn't overwritten)
                });

            var subFolder = localFolder.
                createFolderAsync("aSubFolder",
                Windows.Storage.CreationCollisionOption.openIfExists).done(
                function (folder) {
                    folder.createFileAsync("fileCreatedOnlyOnceInSubFolder.txt",
                        Windows.Storage.CreationCollisionOption.failIfExists).done(
                        function (file) {
                            Windows.Storage.FileIO.writeTextAsync(file, "This is the only thing that will be in the file, because the 'failIfExists' option was chosen for creation");
                        },
                        function (errorMessage) {
                            //handle "file already exists" error here
                            console.log("Creaton of 'fileCreatedOnlyOnceInSubFolder' failed with the message: " + errorMessage);
                        });
                });

            localFolder.getFolderAsync("aSubFolder").done(
                function (folder) {
                    console.log("Found folder with path: " + folder.path);
                });

            localFolder.getFolderAsync("aNonExistingFolder").done(
                function (folder) {
                    //this will not be executed, because the folder doesn't exist
                },
                function (errorMessage) {
                    console.log("Cannot find 'aNonExistingFolder' - original error message: " + errorMessage);
                });
        }
    };

    app.start();
})();
