/// <reference path="../jQuery/jquery-1.10.1.min.js" />

var slider = (function () {
    var items = [];
    var itemNumber = 0;
    var slideNumber;
    var AddNewSlide = function (item) {
        itemNumber++;
        items.push("<div class='hidden' id=slide" + itemNumber + ">" + item + "</div>");
    }
    var render = function (selector) {
        var holder = $(selector);
        holder.html(items);
       
        slideNumber = items.length;
    }

    $("#left-arrow").on("click", function myfunction() {
        if (slideNumber == 1) {
            slideNumber = items.length;
            var previewsSelected = $("#slide" + 1);
            previewsSelected.removeClass("selected");
            previewsSelected.addClass("hidden");
            
            var selected = $("#slide" + slideNumber);
            selected.removeClass("hidden");
            selected.addClass("selected");
        }
        else {
            slideNumber--;
            var previewsSelected = $("#slide" + (slideNumber + 1));
            previewsSelected.removeClass();
            previewsSelected.addClass("hidden");

            var selected = $("#slide" + slideNumber);
            selected.removeClass();
            selected.addClass("selected");
        }
    })
        $("#right-arrow").on("click", function myfunction() {
            if (slideNumber == items.length) {
                slideNumber = 1;
                var previewsSelected = $("#slide" + items.length);
                previewsSelected.removeClass("selected");
                previewsSelected.addClass("hidden");
            
                var selected = $("#slide" + slideNumber);
                selected.removeClass("hidden");
                selected.addClass("selected");
            }
            else {
                slideNumber++;
                var previewsSelected = $("#slide" + (slideNumber - 1));
                previewsSelected.removeClass();
                previewsSelected.addClass("hidden");

                var selected = $("#slide" + slideNumber);
                selected.removeClass();
                selected.addClass("selected");
            }
        });

        return {
        
            AddNewSlide: AddNewSlide,
            Render:render
        }
    
}());