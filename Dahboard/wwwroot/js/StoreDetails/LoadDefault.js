function LoadProductsFromMainStore() {
 
    $.ajax({
        url: "https://localhost:44315/api/StoreDetails/GetAllProductsFromMainStore",
        type: 'GET',

        beforeSend: function () {
            //$('#').css("visibility", "visible");
        },
        dataType: 'json',
        success: function (result) {
            for (var i = 0; i < result.payload.length; i++) {
                var row = result.payload[i];
                $('#store_product_tbl').append("<tr><td>" + row.store_name + "</td><td>" + row.sub_cat_name +"</td><td>" + row.product_name + "</td><td>" + row.sale_price + "</td><td>" + row.purchase_price + "</td><td>" + row.product_qty + "</td><td>" + row.code + "</td><td>" + row.barcode + "</td></tr>");

            }

        }, complete: function () {

        }
    });
}
LoadProductsFromMainStore();
function LoadProductsFromCustomStore() {
    $("#product_barcode_box").val('');
    $("#product_code_box").val('');
    $("#product_name_box").val('');

    $('#store_product_tbl').empty();
    RemoveChecked();
    var storeId = document.getElementById("StoreDD").value;
    var subCatId = document.getElementById("SubCatDD").value;
    var catId = document.getElementById("CatDD").value;   
    $.ajax({
        url: "https://localhost:44315/api/StoreDetails/GetAllProductsFromSpecificStore",
        type: 'GET',
        data:{
            store_id: storeId,
            cat_id: catId,
            subcat_id: subCatId,
    },
        beforeSend: function () {
            //$('#').css("visibility", "visible");
        },
        dataType: 'json',
        success: function (result) {
            for (var i = 0; i < result.payload.length; i++) {
                var row = result.payload[i];
                $('#store_product_tbl').append("<tr><td>" + row.store_name + "</td><td>" + row.sub_cat_name +"</td><td>" + row.product_name + "</td><td>" + row.sale_price + "</td><td>" + row.purchase_price + "</td><td>" + row.product_qty + "</td><td>" + row.code + "</td><td>" + row.barcode + "</td></tr>");

            }

        }, complete: function () {

        }
    });
}