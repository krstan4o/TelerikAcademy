// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232509
(function () {
    "use strict";

    WinJS.Binding.optimizeBindingReferences = true;

    var app = WinJS.Application;
    var activation = Windows.ApplicationModel.Activation;

    var applicationData = Windows.Storage.ApplicationData.current;
    var localFolder = applicationData.localFolder;
    var localSettings = applicationData.localSettings;

    var currentUsernameSetting = "current-username";
    var currentUsername = localSettings[currentUsernameSetting];

    app.onactivated = function (args) {
        if (args.detail.kind === activation.ActivationKind.launch) {
            
            args.setPromise(WinJS.UI.processAll());

            var currentUsernameInput = document.getElementById("current-username-input");
            var setUsernameButton = document.getElementById("set-username");
            var messageInput = document.getElementById("current-message");
            var addMessageButton = document.getElementById("add-message");
            var deletePrevMessages = document.getElementById("delete-previous-checkbox");
            var currentUsernameDisplay = document.getElementById("current-username-display");

            //currentUsernameDisplay.innerText = Windows.Storage.ApplicationData.current.localFolder.path;

            setUsernameButton.onclick = function () {
                currentUsername = currentUsernameInput.value;
                currentUsernameInput.value = "";
                currentUsernameDisplay.innerText = currentUsername;

                loadCurrentMessagesFile();
            }

            addMessageButton.onclick = function () {
                if (currentUsername) {
                    addMessage(currentUsername, messageInput.value, deletePrevMessages.checked);
                }

                messageInput.value = "";
            }
        }
    };

    var loadCurrentMessagesFile = function () {
        if (currentUsername) {
            var currentMessagesFilename = currentUsername + "-messages.txt";
            localFolder.getFileAsync(currentMessagesFilename).then(
                function (file) {
                    displayCurrentMessages(file);
                },
                function () {
                    var messagesList = document.getElementById("messages-list");
                    messagesList.innerHTML = "";
                });
        }
    }

    var displayCurrentMessages = function (messagesFile) {
        Windows.Storage.FileIO.readTextAsync(messagesFile).done(function (contents) {
            var messages = contents.split("\r\n");

            var messagesList = document.getElementById("messages-list");
            messagesList.innerHTML = "";

            var user = currentUsername;
            for (var i = 0; i < messages.length - 1; i++) { //reading all split lines, except the last (it's empty)
                var message = messages[i];
                var userMessage = "<li>" + user + ": " + message + "</li>";
                messagesList.innerHTML = userMessage + messagesList.innerHTML;
            }
        });
    }

    var addMessage = function (user, message, deletePrevious) {
        var messagesList = document.getElementById("messages-list");

        var userMessage = "<li>" + user + ": " + message + "</li>";
        if (deletePrevious) {
            messagesList.innerHTML = userMessage;

            localFolder.createFileAsync(user + "-messages.txt", Windows.Storage.CreationCollisionOption.replaceExisting).done(
                function (file) {
                    Windows.Storage.FileIO.writeTextAsync(file, message + "\r\n");
                }
                );
        }
        else {
            messagesList.innerHTML = userMessage + messagesList.innerHTML;
            
            localFolder.createFileAsync(user + "-messages.txt", Windows.Storage.CreationCollisionOption.openIfExists).done(
                function (file) {
                    Windows.Storage.FileIO.appendTextAsync(file, message + "\r\n");
                }
                );
        }

        
    }

    app.start();
})();
