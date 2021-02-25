document.addEventListener('DOMContentLoaded', function () {
    window.stepper = new window.Stepper(document.querySelector('.bs-stepper'));
});
var slim = new SlimSelect({
    select: '#demo',
    placeholder: "Choose product to add",
    searchText: "It is not available product",
    searchPlaceholder: "Search for available product"

});
$(function () {
    $("#btnNext").click(function () {
        var ids = $("#demo").val();
        var customerId = $("#customerId").val();
        $.ajax({
            type: "post",
            url: "/Employees/Sales/SaleProductPartial",
            data: { "ids": ids, "customerId": customerId },
            success: function (result) {
                $("#container").append(result);
            },
            error: function (ex) {
                alert("Error");
            }
        });
    });
});
$(function () {
    $("#btnPrevious").click(function () {

        $("#container").empty();

    });
});