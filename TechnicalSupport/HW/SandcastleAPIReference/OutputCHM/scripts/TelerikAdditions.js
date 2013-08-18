/* Begin: codeBlock */
$(document).ready(function ()
{
    $(".codeBlockStyle").each(function (index, wrapperDiv)
    {// For every codeBlockStyle

        // Restore selected tab 
        var selectedTagId = getSelLangCoockie(); // get the coockieValue
        if (selectedTagId == null)
        {
            selectedTagId = $('li:first', wrapperDiv).attr("id");
        }

        var tabToHighlightOnLoad = $('li[id^="' + selectedTagId + '"]', wrapperDiv);
        tabToHighlightOnLoad.addClass("current");

        var contentShowOnLoad = $('div[id^="' + selectedTagId + '"]', wrapperDiv);
        contentShowOnLoad.show();

        // attach handlers
        $("li", wrapperDiv).each(function (index, liElement)
        {
            var currentLi = $(liElement);
            currentLi.bind("click", function (element, args)
            {
                var liEelement = $(this);
                var liIdNoCurrent = liEelement.attr("id"); // the class name, without 'current' class
                liEelement.addClass("current").siblings().removeClass("current"); // Add 'current'
                var contentToShow = $('div[id^="' + liIdNoCurrent + '"]', wrapperDiv);
                contentToShow.siblings().hide();
                contentToShow.show();
                createCookie("tcbCookieKey", liIdNoCurrent, 100);
            });
        });
    });
});

function getSelLangCoockie()
{
    return readCookie("tcbCookieKey");
}

function createCookie(name, value, days)
{
    var expires;
    if (days)
    {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    else
    {
        expires = "";
    }
    var coockieValue = name + "=" + value + expires + "; path=/";
    document.cookie = coockieValue;
}

function readCookie(name)
{
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++)
    {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}
/* End: codeBlock */