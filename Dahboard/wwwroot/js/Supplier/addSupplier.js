function Addsupplier() {
    var supplier_name = $("#supplier_name").val().trim();
    var supplier_code = $("#supplier_code").val().trim();
    var supplier_phone = $("#supplier_phone").val().trim();
    var supplier_address = $("#supplier_address").val().trim();
    var supplier_email = $("#supplier_email").val();
    var supplier_commercial_record = $("#supplier_commercial_record").val();
    var supplier_card = $("#supplier_card").val();
    var supplier_type = $("#supplier_type").val();
    if (supplier_commercial_record == "") supplier_commercial_record = 0
    if (supplier_card == "") supplier_card = 0
    if (supplier_code == "") supplier_code = 0
    var formData = {
        id: 0,
        supplier_name: supplier_name,
        supplier_phone: supplier_phone,
        supplier_code: supplier_code,
        supplier_address: supplier_address,
        supplier_email: supplier_email,
        supplier_commercial_record: supplier_commercial_record,
        supplier_card: supplier_card,
        supplier_type: supplier_type,
    };
    if (supplier_name != "") {
        $.ajax({
            type: 'POST',
            url: "https://localhost:44315/api/Supplier/AddSupplier",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(formData),
            success: function (data) {
                if (data.code == "24") {
                    clearWarningMsg('#validsupplierName');
                    $('#validsupplierName').append(data.message);
                } else if (data.code == "21") {
                    clearWarningMsg('#validsupplierCode');
                    $('#validsupplierCode').append(data.message);
                } 
                    else if (data.code == "22") {
                    clearWarningMsg('#validsupplierComm');
                    $('#validsupplierComm').append(data.message);
                } else if (data.code == "23") {
                    clearWarningMsg('#validsupplierCard');
                    $('#validsupplierCard').append(data.message);
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
        clearWarningMsg('#validsupplierName');
        $('#validsupplierName').append('برجاء ادخال اسم المورد')
    }
}
