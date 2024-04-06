var checkbox = document.getElementById('check-style');


$.ajax({
    url: "/CommentsLikes/GetStyle",
    type: 'GET',
    success: function (data) {
        console.log("Success!" + data);
        var old = document.getElementsByClassName("change-class").item(0);
        var current = old.getAttribute("href").toString().slice(0, 24);
        cssFile = (data == "dark") ? "/css/StyleSheetDarkk.css" : "/css/StyleSheetLight.css";
        cssFile = cssFile + '?v=' + Math.random();
        if (checkbox) {
            checkbox.checked = (data == "dark") ? true : false;
        }
        var newlink = document.createElement("link");
        newlink.setAttribute("rel", "stylesheet");
        newlink.setAttribute("type", "text/css");
        newlink.setAttribute("class", "change-class");
        newlink.setAttribute("href", cssFile);
        document.getElementsByTagName("head").item(0).replaceChild(newlink, old);

    },
    error: function () { console.log('error-GetStyle'); }
});


function changeCSS(cssFile, cssLinkIndex) {
    $.ajax({
        url: "/CommentsLikes/PostStyle",
        type: 'POST',
        success: function (data) {
            console.log("Success!" + data);
        },
        error: function () { console.log('error-click-on-style'); }
    });

    var old = document.getElementsByClassName("change-class").item(cssLinkIndex);
    var current = old.getAttribute("href").toString().slice(0, 24);
    cssFile = (current == "/css/StyleSheetLight.css") ? "/css/StyleSheetDarkk.css" : "/css/StyleSheetLight.css";
    cssFile = cssFile + '?v=' + Math.random();
    var newlink = document.createElement("link");
    newlink.setAttribute("rel", "stylesheet");
    newlink.setAttribute("type", "text/css");
    newlink.setAttribute("class", "change-class");
    newlink.setAttribute("href", cssFile);

    document.getElementsByTagName("head").item(cssLinkIndex).replaceChild(newlink, old);
}
