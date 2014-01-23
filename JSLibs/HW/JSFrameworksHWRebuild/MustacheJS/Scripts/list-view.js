/// <reference path="jquery-2.0.3.js" />
/// <reference path="class.js" />
var controls = controls || {};
(function (c) {
    var ListView = Class.create({
        init: function (itemsSource, rows, cols) {
            if (!(itemsSource instanceof Array)) {
                throw "The itemsSource of a ListView must be an array!";
            }
            this.itemsSource = itemsSource;
            this.rows = rows;
            this.cols = cols;
        },
        render: function (template) {
            var table = document.createElement("table"); table.border = 1;
            var indexOfItem = 0;
            debugger;
            for (var r = 0; r < this.rows; r++) {
                var rowItem = document.createElement("tr");
                for (var c = 0; c < this.cols; c++) {
                    if (indexOfItem >= this.itemsSource.length) {
                        return table.outerHTML;
                    }
                    var item = this.itemsSource[indexOfItem];
                    var col = document.createElement('td');
                    col.innerHTML = template(item); col.id = indexOfItem;
                    rowItem.appendChild(col);
                    indexOfItem++;
                }
                table.appendChild(rowItem);
            }
            
            return table.outerHTML;
        }
    });

    c.getListView = function (itemsSource, rows, cols) {
        return new ListView(itemsSource, rows, cols);
    };
}(controls || {}));