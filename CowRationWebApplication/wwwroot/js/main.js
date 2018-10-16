$(".list-group a").on("click", function () {
    alert("Hello");
    $(".list-group a").removeClass("active");
    $(this).addClass("active");
});
$(function () {
    $("#slider").slider({
        min: 10,
        max: 40,
        step: 2,
        value: 0,
        slide: function (event, ui) {
            $("#value-range").text(ui.value);
        }
    });
    $("#value-range").text($("#slider").slider("value"));
});