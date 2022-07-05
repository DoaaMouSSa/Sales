function SearchSupplier(value) {
    if (value != "") {
        $('#supplier_data').empty();
        $.ajax({
            url: "https://localhost:44315/api/Supplier/SearchSupplier",
            type: 'GET',
            data: {
                val: value,
            },
            beforeSend: function () {
                //$('#').css("visibility", "visible");
            },
            dataType: 'json',
            success: function (result) {
                for (var i = 0; i < result.payload.length; i++) {
                    var row = result.payload[i];
                    if (row.supplier_email == null || row.supplier_email == "") row.supplier_email = 'غير متوفر بريد الكترونى';
                    if (row.supplier_phone == "" || row.supplier_phone == null) row.supplier_phone = 'غير متوفر تليفون';
                    if (row.supplier_address == null || row.supplier_address == "") row.supplier_address = 'غير متوفر عنوان';
                    if (row.supplier_type == null || row.supplier_type == "") row.supplier_type = 'غير متوفر نوع';
                    $('#supplier_data').append("<tr><td>" + row.supplier_name + "</td><td>" + row.supplier_code + "</td><td>" + row.supplier_phone + "</td><td>" + row.supplier_address + "</td><td>" + row.supplier_email + "</td><td>" + row.supplier_commercial_record + "</td><td>" + row.supplier_card + "</td><td>" + row.supplier_type + "</td><td><i class='fa fa-edit hand org' data-toggle='modal' data-target='#EditModal" + row.id + "'></i></td><td><i class='fa fa-remove hand red' id=" + row.id + "  data-toggle='modal' data-target='#DeleteModal" + row.id + "'></i></td></tr>");
                }
            }, complete: function () {
                //$("#").css("visibility", "hidden");
            }
        });
    } else {
        getAllSuppliers();
    }
}