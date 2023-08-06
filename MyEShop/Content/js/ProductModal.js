
function Create(id) {
    $.get("/Admin/ProductGroups/Create/" + id,
        function (res) {
            $("#myModal").modal();
            $("#myModalLabel").html("افزودن گروه جدید");
            $("#ModalBody").html(res);
        });
}

function Edit(id) {
    $.get("/Admin/ProductGroups/Edit/" + id, function (res) {
        $("#myModal").modal();
        $("#myModalLabel").html("ویرایش سر گروه");
        $("#ModalBody").html(res);
    });
}

function Delete(id) {
    if (confirm("آیا از حذف این آیتم مطمئن هستید؟")) {
        $.get("/Admin/ProductGroups/Delete/" + id, function () {
            $("#Group_" + id).hide("slow");
        });
    }
}

function success() {
    $("#myModal").modal('hide');
}
