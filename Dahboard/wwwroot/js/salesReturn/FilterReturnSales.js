function FilterSales() {
        $('#sales_data').empty();
        $.ajax({
            url: "https://localhost:44315/api/Sales/FilterSalesInvoices",
            type: 'GET',
            beforeSend: function () {
                //$('#').css("visibility", "visible");
            },
            dataType: 'json',
            data:{
                customer_id: $('#CustomerDD').val(),
                store_id: $('#StoreDD').val(),
                from_date: $('#fromDate').val(),
                to_date: $('#toDate').val()
        }, 
            success: function (result) {
                console.log(result);
                for (var i = 0; i < result.payload.length; i++) {
                    var row = result.payload[i];
                    if (row.isDeleted == 0)
                        $('#sales_data').append("<tr onclick=window.location='Sales/SalesInvoiceDetails/" + row.id + "'><td>" + row.id + "</td><td>" + row.client_name + "</td><td>" + row.store_name + "</td><td>" + row.invoice_total + "</td><td><span class='label label-success'>معتمد</span></td><td>" + row.sales_Added_Time_ar + "</td></tr>");

                    else
                        $('#sales_data').append("<tr onclick=window.location='Sales/SalesInvoiceDetails/" + row.id + "'><td>" + row.id + "</td><td>" + row.client_name + "</td><td>" + row.store_name + "</td><td>" + row.invoice_total + "</td><td><span class='label label-danger'>غير معتمد</span></td><td>" + row.sales_Added_Time_ar + "</td></tr>");

                }
            }, complete: function () {

            }
        });
}