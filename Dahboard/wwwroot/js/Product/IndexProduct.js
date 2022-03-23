function LoadProducts() {
    $.ajax({
        url: "https://localhost:44315/api/Product/GetAllProduct",
        type: 'GET',
        beforeSend: function () {
            //$('#').css("visibility", "visible");
        },
        dataType: 'json',
        success: function (result) {
            for (var i = 0; i < result.length; i++) {
                var row = result[i];
                $('#product_data').append("<tr><td>" + row.product_name + "</td><td>" + row.sub_cat_name + "</td><td>" + row.code + "</td><td>" + row.barcode + "</td><td>" + row.purchase_price + "</td><td>" + row.sale_price + "</td><td><i class='fa fa-edit'></i></td><td><i class='fa fa-remove' id=" + row.id + "  data-toggle='modal' data-target='#DeleteModal" + row.id + "'></i></td></tr>");
                $('#ContainProduct').append("<div class='modal fade' id='DeleteModal" + row.id + "' tabindex='-1' role='dialog' aria-labelledby='DeleteModalLabel' aria-hidden='true'><div class='modal-dialog' role='document' style='width:300px;'><div class='modal-content'><div class='modal-header'><h5 class='modal-title'>حذف الصنف</h5><button type='button' class='close' data-dismiss='modal' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div><div class='modal-body'><h6> هل تريد حذف صنف <span style='color:red'>" + row.product_name + " </span></h6></div><div class='modal-footer'><button type='button' class='btn btn-danger' onclick='DeleteProduct(" + row.id + ");'>تأكيد الحذف</button><button type='button' class='btn btn-secondary' data-dismiss='modal'>الغاء</button></div></div></div></div>");
            }
        }, complete: function () {
            //$("#").css("visibility", "hidden");
        }
    });

    function DeleteProduct(id) {
        $.ajax({
            url: "https://localhost:44315/api/Product/DeleteProduct",
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
}