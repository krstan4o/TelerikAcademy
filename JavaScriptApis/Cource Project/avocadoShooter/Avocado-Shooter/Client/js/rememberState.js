$(function () {
    $("form").rememberState({
        objName: "personal_info",
        ignore: ["bio"]
    }).submit(false).on("click", "p.remember_state a", function () {
        $("input[type=range]").change();
    });

    $("input[type=range]").change(function () {
        $(this).closest("dd").find("span").text($(this).val());
    }).change();
});