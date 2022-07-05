function DeleteSalesInv(id) {
    $.ajax({
        type: 'GET',
        url: "https://localhost:44315/api/Sales/DeleteSales",
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
            console.log('Fail!!!');
        }
    });

}
