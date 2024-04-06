var _userId = $('userId').val();
var _itemId = $("#itemId").val() - 0;
var _return;
var buttons = document.querySelectorAll('.heart, .heart-button');
function getLikesCount() {
    $.ajax({
        url: "/CommentsLikes/GetLikesCount",
        data: { id: _itemId },
        dataType: "json",
        type: 'GET',
        success: function (data) {
            console.log("Success!" + data);
            $("#likes").text(data);
        },
        error: function () {
            console.log('error-GetLikesCount1');
        }
    });
}

$(document).ready(function () {
    getLikesCount();
});

// Обновляем количество лайков при нажатии на кнопку
buttons.forEach(function (button) {
    button.addEventListener('click', function () {
        button.classList.toggle('active');

        // Получаем текущее состояние кнопки
        var isChecked = button.classList.contains('active');

        // Выполняем AJAX-запрос для обновления количества лайков на сервере
        $.ajax({
            url: "/CommentsLikes/OnLikeClick",
            data: { id: _itemId },
            dataType: "json",
            type: 'POST',
            success: function (data) {
                console.log("Success! Likes count updated.");
                // Обновляем отображение количества лайков
                $("#likes").text(data);
            },
            error: function () {
                console.log('Error updating likes count.');
            }
        });
    });
});

// Проверяем сохраненное состояние кнопок при загрузке страницы
//buttons.forEach(function (button) {
//    var buttonId = button.getAttribute('id');
//    var savedButtonState = localStorage.getItem(buttonId);
//    if (savedButtonState === 'active') {
//        button.classList.add('active');
//    }
//});