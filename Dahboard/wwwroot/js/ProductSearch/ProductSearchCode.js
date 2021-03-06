function SearchByCode(value) {
    $('#product_barcode_box').val('');
    var subcat_val = document.getElementById("SubCatFilterPro").value;
    if (subcat_val == "") subcat_val = 0
    int_sub_val = parseInt(subcat_val);

    if (value != "") {
        $('#product_data').empty();
        $('#filter_product_data_purchase').empty();
        $.ajax({
            url: "https://localhost:44315/api/ProductSearch/SearchByCode",
            type: 'GET',
            data: {
                character: value,
                sub_cat_id: int_sub_val
            },
            beforeSend: function () {
                //$('#').css("visibility", "visible");
            },
            dataType: 'json',
            success: function (result) {
                for (var i = 0; i < result.length; i++) {
                    var row = result[i];
                    $('#product_data').append("<tr><td>" + row.product_name + "</td><td>" + row.sub_cat_name + "</td><td>" + row.code + "</td><td>" + row.barcode + "</td><td>" + row.purchase_price + "</td><td>" + row.sale_price + "</td><td><i class='fa fa-edit'></i></td><td><i class='fa fa-remove' id=" + row.id + "  data-toggle='modal' data-target='#DeleteModal" + row.id + "'></i></td></tr>");
                    $('#ContainProduct').append("<div class='modal fade' id='DeleteModal" + row.id + "' tabindex='-1' role='dialog' aria-labelledby='DeleteModalLabel' aria-hidden='true'><div class='modal-dialog' role='document' style='width:300px;'><div class='modal-content'><div class='modal-header'><h5 class='modal-title'>حذف الصنف</h5><button type='button' class='close' data-dismiss='modal' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div><div class='modal-body'><h6> هل تريد حذف صنف <span style='color:red'>" + row.product_name + " </span></h6></div><div class='modal-footer'><button type='button' class='btn btn-danger' onclick='DeleteProduct(" + row.id + ");'>تأكيد الحذف</button><button type='button' class='btn btn-secondary' data-dismiss='modal'>الغاء</button></div></div></div></div>");
                    $('#filter_product_data_purchase').append("<tr data-toggle='modal' data-target='#ForQtyModal" + row.id + "'><td>" + row.product_name + "</td><td>" + row.code + "</td><td>" + row.barcode + "</td><td>" + row.purchase_price + "</td><td>" + row.sale_price + "</td></tr>");
                    $('#filter_product_data_purchase').append("<div class='modal fade qtyModal' id='ForQtyModal" + row.id + "' tabindex='-1' role='dialog' aria-labelledby='ForQtyModalLabel' aria-hidden='true'><div class='modal-dialog' role='document' style='width:200px;'><div class='modal-content'><div class='modal-header'><h5 class='modal-title'>الكميه</h5><button type='button' class='close' data-dismiss='modal' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div><div class='modal-body'><input type='number' class='form-control' id='qty'></div><div class='modal-footer'><button type='button' class='btn btn-success' onClick='GetData(" + row.id + ",\" " + row.product_name + "\"," + row.purchase_price + ");'> confrom</button><button type='button' class='btn btn-secondary' data-dismiss='modal'>الغاء</button></div></div></div></div>");

                }
            }, complete: function () {
                //$("#").css("visibility", "hidden");
            }
        });
    } else {
        LoadProducts();
    }
}
function GetData(id, name, price) {
    $('.qtyModal').modal('hide')
    var qty = document.getElementById("qty").value;
    $('#purchaseTbl').append("<tr><td>" + id + "</td><td>" + name + "</td><td>" + qty + "</td><td>" + price + "</td><td>" + (qty * price) + "</td><td style='width:25%'><i class='fa fa-remove' id=" + row.id + "  data-toggle='modal' data-target='#DeleteModalForPurchase'></i></td></tr>");
    is_existPurchase();
    getTotal();
}
function is_existPurchase() {
    var tbody = $("#purchaseTbl");
    if (tbody.children().length > 0) {
        $('#btnGoToStore').prop("disabled", false);
        $('#message_confirm_purchase').empty();
        $('#purchaseTotalRow').removeClass("hidden");
    }
}
function getTotal() {
    var i = 0;
    var sum = 0;
    var t = document.getElementById('purchaseTbl');
    $("#purchaseTbl tr").each(function () {

        sum += parseInt($(t.rows[i].cells[4]).text());
        i++;
    });
    $('tfoot th#purchaseTotalNum').text(sum);

}