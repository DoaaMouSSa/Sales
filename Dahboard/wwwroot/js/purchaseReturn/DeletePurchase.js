function DeletePurchaseInv(id) {
    $.ajax({
        type: 'GET',
        url: "https://localhost:44315/api/PurchaseReturn/DeleteReturnPurchase",
        dataType: "json",
        contentType: "application/json",
        data: {
            id: id
        },
        success: function () {
            $('#deleteModal').modal('hide');
            location.reload();
        },
        error: function () {
            console.log('Login Fail!!!');
        }
    });

}
