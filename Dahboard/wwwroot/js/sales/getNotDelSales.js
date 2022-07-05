function getNotDelSales() {
    if ($("#chboxNotDeletdSales").prop('checked', true)) {
        if ($("#chboxAllSales").prop('checked', true)) {
            $("#chboxAllSales").prop('checked', false);
        }
        if ($("#chboxDeletdSales").prop('checked', true)) {
            $("#chboxDeletdSales").prop('checked', false);
        }
        $.ajax({
            type: 'GET',
            url: "https://localhost:44315/api/Sales/GetNotDeletedSales",
            dataType: "json",
            contentType: "application/json",
            success: function (result) {
                $('#sales_data').empty();
                for (var i = 0; i < result.payload.length; i++) {
                    var row = result.payload[i];
                    $('#sales_data').append("<tr><td onclick=window.location='Sales/SalesInvoiceDetails/" + row.id + "'>" + row.id + "</td><td>" + row.client_name + "</td><td>" + row.store_name + "</td><td>" + row.invoice_total + "</td><td><span class='label label-success'>معتمد</span></td><td>" + row.sales_Added_Time_ar + "</td><td><i class='fa fa-remove hand red' id=" + row.id + " data-toggle='modal' data-target='#DeleteModal" + row.id + "'></i></td></tr>");
                }
            },
            error: function () {
                console.log('Login Fail!!!');
            }
        });
    }
}
