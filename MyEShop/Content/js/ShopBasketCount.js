$(function () {
    CountShopCart();
});
function CountShopCart() {
    $.get("/Api/BasketApi", function (result) {
        $("#countShopCart").html(result);
    })
}

function AddToCart(id) {
    $.get("/Api/basketApi/" + id, function (result) {
        $("#countShopCart").html(result);
        UpdateShowCart();
    })
}

function UpdateShowCart() {
    $("#ShowCart").load("/ShopCart/ShowCart").fadeOut(800).fadeIn(800);
}