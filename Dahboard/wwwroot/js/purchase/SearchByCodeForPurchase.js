function SearchByCode(value) {
    $('#product_barcode_box').val('');
    var subcat_val = document.getElementById("SubCatFilterPro").value;
    if (subcat_val == "") subcat_val = 0
    int_sub_val = parseInt(subcat_val);
    if (value != null) {
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
                    $('#filter_product_data_purchase').append("<tr data-toggle='modal' data-target='#ForQtyModalPurchase" + row.id + "'><td>" + row.product_name + "</td><td>" + row.code + "</td><td>" + row.barcode + "</td><td>" + row.purchase_price + "</td></tr>");
                    $('#filter_product_data_purchase').append("<div class='modal fade qtyModalPurchase' id='ForQtyModalPurchase" + row.id + "' tabindex='-1' role='dialog' aria-labelledby='ForQtyModalLabel' aria-hidden='true'><div class='modal-dialog' role='document' style='width:200px;'><div class='modal-content'><div class='modal-header'><h5 class='modal-title'>الكميه</h5></div><div class='modal-body'><input type='number' min='1' value='1' class='form-control' id='qty" + row.id + "'  onkeyup='PressEnter(event)' autofocus></div><div class='modal-footer'><button id='btnCloseQtyModal' type='button' class='btn btn-success' onClick='GetDataForPurchase(" + row.id + ",\" " + row.product_name + "\"," + row.purchase_price + ");'>تأكيد</button></div></div></div></div>");
                }
            }, complete: function () {

            }
        });
    }
}






