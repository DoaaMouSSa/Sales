function Addsupplier() {
    var supplier_name = $("#supplier_name").val().trim();
    var supplier_code = $("#supplier_code").val().trim();
    var supplier_phone = $("#supplier_phone").val().trim();
    var supplier_address = $("#supplier_address").val().trim();
    
    var formData = {
        id: 0, supplier_name: supplier_name, supplier_phone: supplier_phone,
        code: supplier_code,
        supplier_address: supplier_address
    };
    if (supplier_name != "") {
        $.ajax({
            type: 'POST',
            url: "https://localhost:44315/api/Supplier/AddSupplier",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(formData),
            success: function (data) {
                if (data.payload == null) {
                    clearWarningMsg('#validsupplierName');
                    $('#validsupplierName').append(data.message);
                } else {
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
        clearWarningMsg('#validsupplierName');
        $('#validsupplierName').append('برجاء ادخال اسم العميل')
    }
}
