function AddProduct() {
    var product_name = $("#product_name").val().trim();
    var product_code = $("#product_code").val().trim();
    var product_barcode = $("#product_barcode").val().trim();
    var purchase_price = $("#purchase_price").val().trim();
    var sale_price = $("#sale_price").val().trim();
    var e = document.getElementById("SubCatsForComboBOx");
    var sub_cat_id = e.value;
    var formData = {
        id: 0, product_name: product_name, sub_cat_id: sub_cat_id,
        code: product_code, barcode: product_barcode,
        purchase_price: purchase_price, sale_price: sale_price
    };
    $.ajax({
        type: 'POST',
        url: "https://localhost:44315/api/Product/AddProduct",
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(formData),
        success: function () {
            $('#exampleModal').modal('hide');
            location.reload();
        },
        error: function () {
            console.log('Fail!!!');
        }
    });

}
