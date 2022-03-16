function SearchByBarCode(value) {
    $('#product_name_box').val('');
    $('#product_code_box').val('');
    var subcat_val = document.getElementById("SubCatFilterPro").value;
    if (subcat_val == "") subcat_val = 0
    int_sub_val = parseInt(subcat_val);

    if (value != null) {
        $('#filter_product_data').empty();
        $.ajax({
            url: "https://localhost:44315/api/ProductSearch/SearchByBarCode",
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
                    $('#filter_product_data').append("<tr><td>" + row.product_name + "</td><td>" + row.code + "</td><td>" + row.barcode + "</td><td>" + row.purchase_price + "</td><td>" + row.sale_price + "</td></tr>");
                }
            }, complete: function () {
                //$("#").css("visibility", "hidden");
            }
        });
    }
}