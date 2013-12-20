var controls = (function () {
    function GridView(selector) {

        var rows = [];
        var header = [];
        var holder = document.querySelector(selector);
        var table = document.createElement('table');
        table.border = 1;

        table.addEventListener('click', function (ev) {
            ev.preventDefault();
            ev.stopPropagation();
            
            var clickedItem = ev.target;
            if (clickedItem instanceof HTMLTableCellElement) {
                showOrHideNestedGrid(clickedItem);
            }

        }, false);
        function showOrHideNestedGrid(item) {
            var tableClickedRow = item.parentElement;
            var nestedGridRow = tableClickedRow.nextElementSibling;
            if (nestedGridRow.classList.contains('nested-grid-view')) {
                if (nestedGridRow.classList.contains('hidden')) {
                    nestedGridRow.classList.remove('hidden');
                } else {
                    nestedGridRow.classList.add('hidden');
                }
            }
      
        }
        this.addRow = function (rowItems) {
            if (arguments[0] == {}) {
                var gridRow = new GridViewRow(arguments[0]);
            } else {
                var gridRow = new GridViewRow(arguments);
            }
            
            rows.push(gridRow);
            return gridRow;
        }
        this.addHeader = function (wtf) {
        
            if (header.length == 0) {
                for (var i = 0; i < arguments.length; i++) {
                    header.push(arguments[i]);
                }
            }
        }
        this.render = function () {
            table.innerHTML = '';
            if (header.length > 0) {
                var headerRow = document.createElement('tr');
                for (var i = 0; i < header.length; i++) {
                    var headerCol = document.createElement('th');
                    headerCol.textContent = header[i];
                    headerRow.appendChild(headerCol);
                }
                table.appendChild(headerRow);
            }
            for (var i = 0; i < rows.length; i++) {
                var element = rows[i].render();
                table.appendChild(element);

                if (rows[i].nestedGrid) {
                    var nestedGridRow = document.createElement('tr');
                    nestedGridRow.classList.add('nested-grid-view');
                    var nestedGridCol = document.createElement('td');
                    nestedGridCol.appendChild(rows[i].nestedGrid.render());
                    nestedGridRow.appendChild(nestedGridCol);
                    table.appendChild(nestedGridRow);
                }
            }
            if (holder) {
               
                holder.childNodes = [];
                holder.appendChild(table);
            }
            else
                return table;
        }
        this.getGridViewData = function () {
            var data = {};
            data.schools = [];
            if (header.length > 0) {
                data.schools.header = [];
                for (var i = 0; i < header.length; i++) {
                    data.schools.header.push(header[i]);
                }
            }
           
            for (var i = 0; i < rows.length; i++) {
                data.schools.push(rows[i].serialize());
            }

            return data;
        }
        this.serialize = function () {
            var obj = [];
            if (header.length > 0) {
                obj.header = [];
                for (var i = 0; i < header.length; i++) {
                    obj.header.push(header[i]);
                }
            }
            if (rows.length > 0) {
                if (rows.length == 3) {
                    
                    for (var i = 0; i < rows.length; i++) {
                        obj.push(rows[i].serialize());
                    }
                }
                if (rows.length == 4) {
                    
                    for (var i = 0; i < rows.length; i++) {
                        obj.push(rows[i].serialize());
                    }
                }
            }
            
            return obj;

        }
    }
    function GridViewRow(rowItems) {
        if (rowItems[0] instanceof Array) {
            rowItems = rowItems[0];
        }
        var items = [];
        this.nestedGrid = null;
        for (var i = 0; i < rowItems.length; i++) {
            items.push(rowItems[i]);
        }

        this.getNestedGridView = function () {
            if (this.nestedGrid == null) {
                this.nestedGrid = new GridView();
                return this.nestedGrid;
            }
        }
        this.render = function () {
            var rowElement = document.createElement('tr');
            if (items.length > 0) {
                for (var i = 0; i < items.length; i++) {
                    var colElement = document.createElement('td');
                    colElement.textContent = items[i];
                    rowElement.appendChild(colElement);
                }
            }

            return rowElement;
        }
        this.serialize = function () {
            var school = {};
      
                school.data = [];
                school.data = items;
            
            if (this.nestedGrid) {
                if (items.length == 3) {
                    school.students = this.nestedGrid.serialize();

                } else if (items.length == 4) {
                    school.courses = this.nestedGrid.serialize();
                }
            }
            return school;
        }
    }

    return {
        getGridView: function (selector) {
            return new GridView(selector);
        },
        buildGridView: function (selector, data) {
            var copiedGridView = controls.getGridView(selector);
            copiedGridView.addHeader("Name", 'Location', 'Total Students', 'Speciality');
            var schools = data.schools;
 
            for (var i = 0; i < schools.length; i++) {
                if (schools[i].courses) {               
                    var row = copiedGridView.addRow(schools[i].data);
                    var coursesGrid = row.getNestedGridView();
                    coursesGrid.addHeader('Title', 'Start Date', 'Total Students');
                    var courses = schools[i].courses;
                    for (var j = 0; j < courses.length; j++) {
                        if (courses[j].students) {
                            var studentRow = coursesGrid.addRow(courses[j].data);
                            var studentsGrid = studentRow.getNestedGridView();
                            studentsGrid.addHeader('FirstName', 'LastName', 'Grade', 'Mark');
                            var students = courses[j].students;
                            for (var k = 0; k < students.length; k++) {
                                studentsGrid.addRow(students[k].data);
                            }
                        } else {
                            
                            coursesGrid.addRow(courses[j].data);
                        }
                    }
                } else {
                    copiedGridView.addRow(schools[i].data);
                }
            }
            copiedGridView.render();
            return copiedGridView;
        }
        
    }
}());

var schoolRepository = (function () {

    return {
        save: function (name, data) {
            localStorage.setObject(name, data);
        },
        load:function (name) {
            return localStorage.getObject(name);
        }
    }
}());