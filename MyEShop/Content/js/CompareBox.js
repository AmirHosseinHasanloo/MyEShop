function AddToCompare(id) {
    $.get("/Compare/AddToCompare/" + id, function (result) {
        $("#ShowCart").html(result);
    });
}

function RemoveFromCompare(id) {
    $.ajax({
        url: "/Compare/DeleteFromCompare/" + id,
        type: "Get",
    }).done(function (result) {
        $("#ShowCart").html(result);
    });
}