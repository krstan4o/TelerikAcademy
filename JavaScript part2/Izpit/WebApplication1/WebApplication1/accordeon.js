var controls = (function myfunction() {
    function Accordion(selector) {
        var item =document.createElement('ul');
        this.add = function (title) {
            item.appendChild(new Item(title));
        }
        this.render = function () {
            for (var i = 0; i < item.length; i++) {
                item[i].render();
            }
            var accHolder = document.querySelector(selector);
            accHolder.appendChild(item);
        }
      
    }
    function Item(title) {
        var subItems = document.createElement('li');
        subItems.innerHTML = title;
        this.render = function () {
            
            return subItems;
        }
        this.add = function (title) {
            var newSubItem = document.createElement('ul');
            newSubItem.innerHTML = title;
            subItems.appendChild(newSubItem);
        }
        return subItems;
        
    }

    return {
        getAccordion: function (selector) {
            return new Accordion(selector);
        }
        };
}());