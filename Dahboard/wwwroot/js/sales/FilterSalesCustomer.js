function FilterByCustomer(val) {
    if (val != 0) {
        $('#sales_data').empty();
        $.ajax({
            url: "https://localhost:44315/api/Sales/ReadCustomSalesInvoiceByCustomer",
            type: 'GET',
            beforeSend: function () {
                //$('#').css("visibility", "visible");
            },
            dataType: 'json',
            data:{
            customer_id: val,
        }, 
            success: function (result) {
                console.log(result);
                for (var i = 0; i < result.payload.length; i++) {
                    var row = result.payload[i];
                    if (row.isDeleted == 0)
                        $('#sales_data').append("<tr onclick=window.location='Purchase/PurchaseInvoiceDetails/" + row.id + "'><td>" + row.id + "</td><td>" + row.customer_name + "</td><td>" + row.store_name + "</td><td>" + row.invoice_total + "</td><td><span class='label label-success'>معتمد</span></td><td>" + row.purchase_Added_Time_ar + "</td></tr>");

                    else
                        $('#sales_data').append("<tr onclick=window.location='Purchase/PurchaseInvoiceDetails/" + row.id + "'><td>" + row.id + "</td><td>" + row.customer_name + "</td><td>" + row.store_name + "</td><td>" + row.invoice_total + "</td><td><span class='label label-danger'>غير معتمد</span></td><td>" + row.purchase_Added_Time_ar + "</td></tr>");

                }
            }, complete: function () {

            }
        });
    }
}
function ResetFilterByCustomer() {
    $('#SupplierDD').val(0);
}