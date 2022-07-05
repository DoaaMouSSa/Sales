function DeleteSupplier(id) {
    $.ajax({
        url: "https://localhost:44315/api/Supplier/DeleteSupplier",
        type: 'GET',
        data: {
            id: id
        },
        beforeSend: function () {
            //$('#').css("visibility", "visible");
        },
        dataType: 'json',
        success: function (result) {
            location.reload();
        }, complete: function () {
            //$("#").css("visibility", "hidden");
        }
    });
}