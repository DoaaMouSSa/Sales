function EditSupplier(id) {
    var new_supplier_name = document.getElementById('supplier_name_edit' + id).value;
    var new_supplier_code = document.getElementById('supplier_code_edit' + id).value;
    var new_supplier_phone = document.getElementById('supplier_phone_edit' + id).value;
    var new_supplier_address = document.getElementById('supplier_address_edit' + id).value;

    var formData = {
        id: id, supplier_name: new_supplier_name,
        supplier_code: new_supplier_code,
        supplier_phone: new_supplier_phone,
        supplier_address: new_supplier_address,
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
