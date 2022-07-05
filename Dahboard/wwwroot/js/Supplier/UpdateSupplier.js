function EditSupplier(id) {
    var new_supplier_name = document.getElementById('supplier_name_edit' + id).value;
    var new_supplier_code = document.getElementById('supplier_code_edit' + id).value;
    var new_supplier_phone = document.getElementById('supplier_phone_edit' + id).value;
    var new_supplier_address = document.getElementById('supplier_address_edit' + id).value;
    var new_supplier_email = document.getElementById('supplier_email_edit' + id).value;
    var new_supplier_card = document.getElementById('supplier_card_edit' + id).value;
    var new_supplier_comm = document.getElementById('supplier_comm_edit' + id).value;
    var new_supplier_type = document.getElementById('supplier_type_edit' + id).value;
    
    var formData = {
        id: id, supplier_name: new_supplier_name,
        supplier_code: new_supplier_code,
        supplier_phone: new_supplier_phone,
        supplier_address: new_supplier_address,
        supplier_email: new_supplier_email,
        supplier_card: new_supplier_card,
        supplier_commercial_record: new_supplier_comm,
        supplier_type: new_supplier_type,
    };
    $.ajax({
        type: 'POST',
        url: "https://localhost:44315/api/Supplier/EditSupplier",
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
