// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $(".card").click(function () {
            var block = $(this).attr('id');
            $("#tx-block-no").text(block);
            $('#transactions-grid').load('/index?handler=TransactionsPartial&id=' + block);
        });

});
