function SearchCustomer(value) {
    if (value != "") {
        $('#customer_data').empty();
        $.ajax({
            url: "https://localhost:44315/api/Customer/SearchCustomer",
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
                if (row.customer_email == null || row.customer_email == "") row.customer_email = 'غير متوفر بريد الكترونى';
                if (row.customer_phone == null || row.customer_phone == "") row.customer_phone = 'غير متوفر تليفون';
                if (row.customer_address == null || row.customer_address == "") row.customer_address = 'غير متوفر عنوان';
                if (row.customer_type == null || row.customer_type == "") row.customer_type = 'غير متوفر نوع';
                $('#customer_data').append("<tr><td>" + row.customer_name + "</td><td>" + row.customer_code + "</td><td>" + row.customer_phone + "</td><td>" + row.customer_address + "</td><td>" + row.customer_email + "</td><td>" + row.customer_commercial_record + "</td><td>" + row.customer_card + "</td><td>" + row.customer_type + "</td><td><i class='fa fa-edit hand org' data-toggle='modal' data-target='#EditModal" + row.id + "'></i></td><td><i class='fa fa-remove hand red' id=" + row.id + "  data-toggle='modal' data-target='#DeleteModal" + row.id + "'></i></td></tr>");
                }
            }, complete: function () {
                //$("#").css("visibility", "hidden");
            }
        });
    }else {
        getAllCustomer();
    }
}