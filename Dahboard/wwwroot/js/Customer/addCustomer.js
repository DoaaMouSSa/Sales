function AddCustomer() {
    var customer_name = $("#customer_name").val().trim();
    var customer_code = $("#customer_code").val().trim();
    var customer_phone = $("#customer_phone").val().trim();
    var customer_address = $("#customer_address").val().trim();
    var customer_type = $("#customer_type").val().trim();
    var customer_card = $("#customer_card").val().trim();
    var customer_email = $("#customer_email").val().trim();
    var customer_commercial_record = $("#customer_commercial_record").val().trim();
    if (customer_commercial_record == "") customer_commercial_record = 0
    if (customer_card == "") customer_card = 0
    if (customer_code == "") customer_code = 0
    var formData = {
        id: 0, customer_name: customer_name, customer_phone: customer_phone,
        customer_code: customer_code,
        customer_address: customer_address,
        customer_type: customer_type,
        customer_email: customer_email,
        customer_card: customer_card,
        customer_commercial_record: customer_commercial_record
    };
    if (customer_name != "") {
        $.ajax({
            type: 'POST',
            url: "https://localhost:44315/api/Customer/AddCustomer",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(formData),
            success: function (data) {
                if (data.code == "24") {
                    clearWarningMsg('#validcustomerName');
                    $('#validcustomerName').append(data.message);
                } else if (data.code == "21") {
                     clearWarningMsg('#validcustomerCode');
                    $('#validcustomerCode').append(data.message);
                }
                else if (data.code == "22") {
                    clearWarningMsg('#validcustomerComm');
                    $('#validcustomerComm').append(data.message);
                } else if (data.code == "23") {
                    clearWarningMsg('#validcustomerCard');
                    $('#validcustomerCard').append(data.message);
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
        clearWarningMsg('#validcustomerName');
        $('#validcustomerName').append('برجاء ادخال اسم العميل')
    }
}
