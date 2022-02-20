$(document).ready(() => {


    $(".myButton").on("click", () => {
        $("#dishIngredients option").prop('selected', true);
    });

    $("#add").on("click", () => {
        $("#allIngredients :selected").remove().appendTo("#dishIngredients");
    });
    $("#remove").on("click", () => {
        $("#dishIngredients :selected").remove().appendTo("#allIngredients");
    });

})