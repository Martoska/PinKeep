


$("#deleteItem").click(function () {
    var itemId = $("#itemId").val() - 0;
    var toController = message + "||" + userId + "||" + sender + "||" + itemId;
    $.ajax({
        url: "/Items/Delete",
        data: { id: itemId },
        dataType: "json",
        type: 'POST',
        success: function (data) {
            console.log("Success!" + data);
        },
        error: function () { console.log('error!!'); }
    });


});