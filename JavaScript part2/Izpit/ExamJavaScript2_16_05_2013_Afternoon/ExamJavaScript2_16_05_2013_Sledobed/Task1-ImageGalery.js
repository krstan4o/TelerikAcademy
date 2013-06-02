
var controls = (function () {
    function hidePrev(item) {
        var prev = item.previousElementSibling;

        while (prev) {
            var sublist = prev.querySelector("ul");
            if (sublist) {
                sublist.style.display = "none";
            }
            prev = prev.previousElementSibling;
        }
    }

    function hideNext(item) {
        var next = item.nextElementSibling;
        while (next) {
            var sublist = next.querySelector("ul");
            if (sublist) {
                sublist.style.display = "none";
            }
            next = next.nextElementSibling;
        }
    }

    function ImageGallery(selector) {
        var items = [];

        var itemsList = document.createElement("ul");

        var titleOfGalery = 'Galery';
        itemsList.setAttribute('title', titleOfGalery);
       
        itemsList.addEventListener("click",
                                   function (ev) {
                                       if (!ev) {
                                           ev = window.event;
                                       }
                                       ev.stopPropagation();
                                       ev.preventDefault();

                                       var clickedItem = ev.target;
                                       if (!(clickedItem instanceof HTMLAnchorElement)) {
                                           return;
                                       }

                                       hidePrev(clickedItem.parentNode);
                                       hideNext(clickedItem.parentNode);

                                       var sublist = clickedItem.nextElementSibling;
                                       var sublistDisplay = "";
                                       if (!sublist) {
                                           return;
                                       }
                                       if (sublist.style.display === "none") {
                                           sublist.style.display = "";
                                       }
                                       else {
                                           sublist.style.display = "none";
                                       }
                                   }, false);
       
        var galeryHolder = document.querySelector(selector);

        this.addImage = function (title, src) {
            var newImage = new Image(title, src);
            items.push(newImage);
            return newImage;
        }
        this.addAlbum = function (title) {
            var newAlbum = new Album(title);
            items.push(newAlbum);
            return newAlbum;
        };
        this.render = function () {
            for (var i = 0, len = items.length; i < len; i += 1) {
                if (items[i].addAlbum) {
                    var domItem = items[i].render();
                    itemsList.appendChild(domItem);
                }
                else {
                    domItem = items[i];
                    itemsList.appendChild(domItem);
                }
            }
            galeryHolder.appendChild(itemsList);
          
            return this;
        }
    }
    function Album(title) {
        var items1 = [];
        var album = document.createElement('li');
        album.setAttribute('title', title);

        this.addAlbum = function (title) {
            var newItem = new Album(title);
            items1.push(newItem);
            return newItem;
        }

        this.addImage = function (title, src) {
            var newImage = new Image(title, src);
            items1.push(newImage);
        }

        this.render = function () {
            var itemNode = document.createElement("li");
         
            itemNode.innerHTML = "<a href='#'>" + title + "</a>";
            itemNode.setAttribute('title', title);

            if (items1.length > 0) {
                var sublist = document.createElement("ul");
               
                for (var i = 0, len = items1.length; i < len; i += 1) {
                    if (items1[i].addAlbum) {
                        var subitem = items1[i].render();
                        sublist.appendChild(subitem);
                    }
                    else {
                        domItem = items1[i];
                        sublist.appendChild(domItem);
                    }
                }
                itemNode.appendChild(sublist);
            }
         
            return itemNode;
        }
    }
    function Image(title, src) {
        var image = document.createElement('img');
        image.setAttribute('src', src);
        image.setAttribute('title', title);
        image.addEventListener('click', function (ev) {
            var container = document.getElementById('viewedImage');
            while (container.firstChild) {
                container.removeChild(container.firstChild);
            }
            var bigImage = document.createElement('img');
            bigImage.setAttribute('src', ev.target.getAttribute('src'));
            bigImage.setAttribute('title', ev.target.getAttribute('title'));
            bigImage.style.width += '500px';
            bigImage.style.height += '500px';

            container.appendChild(bigImage);
        }, false);
        return image;
    }
  
    return {
        getImageGallery: function (selector) {
            return new ImageGallery(selector);
        }
    }
}());
