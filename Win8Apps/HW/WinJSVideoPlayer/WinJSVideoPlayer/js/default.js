// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232509
(function () {
    "use strict";

    WinJS.Binding.optimizeBindingReferences = true;

    var app = WinJS.Application;
    var activation = Windows.ApplicationModel.Activation;

    app.onactivated = function (args) {
        var loadedSongsList = document.getElementById("loaded-songs");
        var player = document.getElementById("player");
        var playlist = new Windows.Media.Playlists.Playlist();
        var selectedVideoIndex = -1;
        var storagePermissions = Windows.Storage.AccessCache.StorageApplicationPermissions;

        loadedSongsList.addEventListener("click", function (event) {
            var videoEntry = event.target;

            if (videoEntry.tagName.toLowerCase() == "strong") {
                videoEntry = videoEntry.parentElement;
            }

            var videoIndex = videoEntry.getAttribute("data-video-index");
            selectedVideoIndex = videoIndex;
            var videoUrl = URL.createObjectURL(playlist.files[videoIndex]);

            player.src = videoUrl;
            player.play();
        });


        player.addEventListener("ended", function (event) {
            if (selectedVideoIndex < 0 || selectedVideoIndex >= playlist.files.length - 1) {
                return;
            }
            selectedVideoIndex++;
            var videoUrl = URL.createObjectURL(playlist.files[selectedVideoIndex]);

            player.src = videoUrl;
            player.play();
        });

        var RedrawPlaylist = function () {
            loadedSongsList.innerHTML = "";
            for (var i = 0; i < playlist.files.length; i++) {
                var storageFile = playlist.files[i];
                var videoName = storageFile.displayName;

                var videoEntry = document.createElement("li");
                videoEntry.setAttribute("data-video-index", i);
                videoEntry.innerHTML = "<strong>" + videoName + "</strong>";
                loadedSongsList.appendChild(videoEntry);
            }

        }

        var addSong = function (storageFile) {

            storageFile.properties.getVideoPropertiesAsync().then(function (properties) {
                playlist.files.append(storageFile);
                storagePermissions.futureAccessList.add(storageFile);
                RedrawPlaylist();
            });

        }

        WinJS.Utilities.id("appbar-add-button").listen("click", function () {
            var openPicker = Windows.Storage.Pickers.FileOpenPicker();

            openPicker.fileTypeFilter.append("*");
            openPicker.pickMultipleFilesAsync().then(function (files) {
                files.forEach(addSong);
            });
        })

        WinJS.Utilities.id("appbar-remove-button").listen("click", function () {
            if (selectedVideoIndex < 0 || selectedVideoIndex >= playlist.files.length) {
                return;
            }

            playlist.files.removeAt(selectedVideoIndex);

            var videoUrl;// = URL.createObjectURL(playlist.files[selectedVideoIndex]);

            if (selectedVideoIndex < 0 || selectedVideoIndex >= playlist.files.length) {
                player.src = "#";
            }
            else {
                videoUrl = URL.createObjectURL(playlist.files[selectedVideoIndex]);
            }

            player.src = videoUrl;

            RedrawPlaylist();
        })

        WinJS.Utilities.id("appbar-save-button").listen("click", function () {
            var savePicker = new Windows.Storage.Pickers.FolderPicker();
            savePicker.fileTypeFilter.append("*");

            savePicker.pickSingleFolderAsync().then(function (folder) {
                playlist.saveAsAsync(folder, "My Playlist", Windows.Storage.NameCollisionOption.replaceExisting, Windows.Media.Playlists.PlaylistFormat.windowsMedia);
            });

        })

        WinJS.Utilities.id("appbar-open-button").listen("click", function () {
            var openPicker = Windows.Storage.Pickers.FileOpenPicker();

            openPicker.fileTypeFilter.append(".wpl");
            openPicker.pickSingleFileAsync().then(function (file) {
                Windows.Media.Playlists.Playlist.loadAsync(file).then(function (loadedPlaylist) {
                    playlist = loadedPlaylist;
                    RedrawPlaylist();
                });
            });

        })

        args.setPromise(WinJS.UI.processAll());
    };

    app.start();
})();

