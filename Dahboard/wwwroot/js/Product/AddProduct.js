function AddProduct() {
    var product_name = $("#product_name").val().trim();
    var product_code = $("#product_code").val().trim();
    var product_barcode = $("#product_barcode").val().trim();
    var purchase_price = $("#purchase_price").val().trim();
    if (purchase_price == "") {
        purchase_price = "0";
    }
    var sale_price = $("#sale_price").val().trim();
    if (sale_price == "") {
        sale_price = "0";
    }
    var e = document.getElementById("SubCatsForComboBOx");
    var sub_cat_id = e.value;
    var formData = {
        id: 0, product_name: product_name, sub_cat_id: sub_cat_id,
        code: product_code, barcode: product_barcode,
        purchase_price: purchase_price, sale_price: sale_price
    };
    if (product_name != "") {
        $.ajax({
            type: 'POST',
            url: "https://localhost:44315/api/Product/AddProduct",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(formData),
            success: function (data) {
                if (data.code == "21") {
                    clearWarningMsg('#validProductCode');
                    $('#validProductCode').append(data.message);
                } else if (data.code == "25"){
                    clearWarningMsg('#validProductBarcode');
                    $('#validProductBarcode').append(data.message);
                }
                else {
                    $('#exampleModal').modal('hide');
                    location.reload();
                }
            },
            error: function () {
                console.log('Fail!!!');
            }
        });
    }
    else {
        clearWarningMsg('#validProductName');
        clearWarningMsg('#validProductCode');
        $('#validProductName').append('برجاء ادخال اسم الصنف')
    }
}
