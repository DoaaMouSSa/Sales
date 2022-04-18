function AddCustomer() {
    var customer_name = $("#customer_name").val().trim();
    var customer_code = $("#customer_code").val().trim();
    var customer_phone = $("#customer_phone").val().trim();
    var customer_address = $("#customer_address").val().trim();
    
    var formData = {
        id: 0, customer_name: customer_name, customer_phone: customer_phone,
        code: customer_code,
        customer_address: customer_address
    };
    if (customer_name != "") {
        $.ajax({
            type: 'POST',
            url: "https://localhost:44315/api/customer/Addcustomer",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(formData),
            success: function (data) {
                if (data.payload == null) {
                    clearWarningMsg('#validcustomerName');
                    $('#validcustomerName').append(data.message);
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
        clearWarningMsg('#validcustomerName');
        $('#validcustomerName').append('برجاء ادخال اسم العميل')
    }
}
