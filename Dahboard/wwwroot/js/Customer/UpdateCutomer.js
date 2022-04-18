function EditCustomer(id) {
    var new_customer_name = document.getElementById('customer_name_edit' + id).value;
    var new_customer_code = document.getElementById('customer_code_edit' + id).value;
    var new_customer_phone = document.getElementById('customer_phone_edit' + id).value;
    var new_customer_address = document.getElementById('customer_address_edit' + id).value;

    var formData = {
        id: id, customer_name: new_customer_name,
        customer_code: new_customer_code,
        customer_phone: new_customer_phone,
        customer_address: new_customer_address,
    };
    $.ajax({
        type: 'POST',
        url: "https://localhost:44315/api/Customer/EditCustomer",
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(formData),
        success: function () {
            $('#EditModal').modal('hide');
            location.reload();
        },
        error: function () {
            console.log('Fail!!!');
        }
    });

}
