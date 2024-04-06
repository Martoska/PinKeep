var connection = new signalR.HubConnectionBuilder().withUrl("/Items/Details").build();

//Disable send button until connection is established
$("#sendMessage").prop('disabled', true);

connection.on("ReceiveMessage", function (user, message, itemId) {
    var thisItemId = $("#itemId").val();;
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var empt = document.createElement("div");
    var li = document.createElement("div");
    li.innerHTML = "<h5>" + user + "</h5>" + msg + "\n";
    if (thisItemId == itemId) {
        $("#messagesList").append(li);
        $("#messagesList").append(empt);
    }
});

connection.start().then(function () {
    $("#sendMessage").prop('disabled', false);
}).catch(function (err) {
    return console.error(err.toString());
});


$("#sendMessage").click(function () {
    var receiver = '';
    var userId = $("#userId").val();
    var sender = $("#sender").val();
    var itemId = $("#itemId").val() - 0;
    var message = $("#message").val();
    var toController = message + "||" + userId + "||" + sender + "||" + itemId;
    $.ajax({
        url: "/CommentsLikes/PostComment",
        data: { msg: toController },
        dataType: "json",  
        type: 'POST',
        success: function (data) {
            console.log("Success!" + data);
        },
        error: function () { console.log('error!!'); }
    });

    if (receiver != "") {
        //send to a user
        connection.invoke("SendMessageToGroup", sender, receiver, message).catch(function (err) {
            return console.error(err.toString());
        });
    }
    else {
        //send to all
        connection.invoke("SendMessage", sender, message, itemId).catch(function (err) {
            return console.error(err.toString());
        });
    }

    event.preventDefault();

});