var controls = (function () {  

    function Gallery(selector) {
        var items = [];
        var galleryHolder = document.querySelector(selector);
        var itemsList = document.createElement("ul");

        function showOrHideAlbums(albumElement) {
            var pics = albumElement.nextSibling;
            var nestedAlbums = pics.nextSibling;
            if (pics.childNodes.length == 0 && nestedAlbums.childNodes.length == 0) {
                return;
            }
            if (nestedAlbums.className == "" && pics.className == "") {
                pics.className = "hidden";
                nestedAlbums.className = "hidden";
            } else {
                pics.className = "";
                nestedAlbums.className = "";
            }

        }
        function zoomImage(image) {
            var holder = document.getElementById("zoom-img-holder");
            if (!holder) {
                holder = document.createElement('span');
                holder.id = "zoom-img-holder";
                galleryHolder.appendChild(holder);
            }

            var zoomedImage = document.createElement('img');
            zoomedImage.width = image.width + image.width * 2;
            zoomedImage.height = image.height + image.width * 2;
            zoomedImage.src = image.src;
            holder.innerHTML = '';
            holder.appendChild(zoomedImage);

        }
        function sortByTitle(btnClicked) {
                    
        }

        galleryHolder.addEventListener('click', function (ev) {
            if (!ev) {
                ev = window.event;
            }
            ev.stopPropagation();
            ev.preventDefault();
            var clickedItem = ev.target;
            if (clickedItem instanceof HTMLAnchorElement) {
                var div = clickedItem.parentNode;
                showOrHideAlbums(div.parentNode);
            }
            else if (clickedItem instanceof HTMLImageElement) {
                zoomImage(clickedItem);
            } else if (clickedItem instanceof HTMLButtonElement) {
                sortByTitle(clickedItem);
            }
        }, false);
       
        this.addAlbum = function addAlbum(title) {
            var album = new Album(title);
            items.push(album);
            return album;
        }
        this.addImage = function addImage(title, src) {
            var image = new Image(title, src);
            items.push(image);
        }
        this.render = function () {
            itemsList.innerHTML = '';
            galleryHolder.childNodes = [];
            var liElementForAlbums = document.createElement('li');
            liElementForAlbums.innerHTML = '<button class="asc">Sort</button>';
            var liElementForImages = document.createElement('li');
            for (var i = 0, len = items.length; i < len; i += 1) {
                var domItem = items[i].render();
                if (domItem.nodeName == "UL") {
                    liElementForAlbums.appendChild(domItem);
                }
                else {
                    liElementForImages.appendChild(domItem);
                }
            }
            itemsList.appendChild(liElementForImages);
            itemsList.appendChild(liElementForAlbums);
            galleryHolder.appendChild(itemsList);
            return this;
        }
        this.getImageGalleryData = function () {
            var serializedItems = [];
            for (var i = 0; i < items.length; i += 1) {
                serializedItems.push(items[i].serialize());
            }
            return serializedItems;
        }
    
    }
    function safe_tags(str) {
        return str.replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;');
    }

    var Image = function (title, src) {
        this.title = title;
        this.src = src;
        this.render = function () {
            var div = document.createElement('div');
            var pElement = document.createElement('p');
            var imageElement = document.createElement("img");
            imageElement.src = this.src;
            imageElement.width = 150;
            imageElement.height = 150;
            div.appendChild(imageElement);
            pElement.textContent = this.title;
            div.appendChild(pElement);

            return div;
        }
        this.serialize = function () {
            var object = { title: title, src: src }
            return object;
        }
        
    };

    var Album = function (title) {
        this.title = title;
        var elements = [];

        this.addAlbum = function addAlbum(title) {
            var album = new Album(title);
            elements.push(album);
            return album;
        }
        this.addImage = function addImage(title, src) {
            var image = new Image(title, src);
            elements.push(image);
        }
        this.render = function () {
            var itemNode = document.createElement("ul");
            var liElementForTitle = document.createElement('li');
            var liElementForAlbums = document.createElement('li');
            var liElementForImages = document.createElement('li');
          
            liElementForTitle.innerHTML = "<div><a href='javascript:void(0)' >" + safe_tags(title) + "</a><button class=\"asc\">Sort</button></div>";
            for (var i = 0, len = elements.length; i < len; i += 1) {
                var domItem = elements[i].render();
                if (domItem.nodeName == "UL") {
                    liElementForAlbums.appendChild(domItem);
                }
                else {
                    liElementForImages.appendChild(domItem);
                }
                
            }
            itemNode.appendChild(liElementForTitle);
            itemNode.appendChild(liElementForImages);
            itemNode.appendChild(liElementForAlbums);
            return itemNode;
        }
        this.serialize = function () {
            var thisItem = {
                title: title
            };
            if (elements.length > 0) {
                var serializedItems = [];
                for (var i = 0; i < elements.length; i += 1) {
                    var serItem = elements[i].serialize();
                    serializedItems.push(serItem);
                }
                thisItem.items = serializedItems;
            }
            return thisItem;
        }
    };

    function deserialize(data, album) {

        for (var i = 0; i < data.length; i++) {

            if (data[i].src) {
                album.addImage(data[i].title, data[i].src);
            } else if (data[i].title && data[i].items) {
                var newAlbum = album.addAlbum(data[i].title);
                deserialize(data[i].items, newAlbum);
            } else if (data[i].title) {
                album.addAlbum(data[i].title);
            }
        }
        return album;
    }
    return {
        getImageGallery: function (selector) {
            return new Gallery(selector);
        },
        buildImageGallery: function (selector, data) {
            var gallery = new Gallery(selector);
          
            if (data) {
              
                deserialize(data, gallery);
            }
            return gallery;
        }
    }
}());

var imageGalleryRepository = (function () {
     function save(name, data) {
         localStorage.setObject(name, data);
     }
     function load(name) {
         var imageGalleryData = localStorage.getObject(name);
         return imageGalleryData;
     }
     return {
         save: save,
         load:load
     }
}());