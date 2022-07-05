function getNotDelPurchase() {
    if ($("#chboxNotDeletdPurchase").prop('checked', true)) {
        if ($("#chboxAllPurchase").prop('checked', true)) {
            $("#chboxAllPurchase").prop('checked', false);
        }
        if ($("#chboxDeletdPurchase").prop('checked', true)) {
            $("#chboxDeletdPurchase").prop('checked', false);
        }
        $.ajax({
            type: 'GET',
            url: "https://localhost:44315/api/PurchaseReturn/GetNotDeletedPurchaseReturnInvoices",
            dataType: "json",
            contentType: "application/json",
            success: function (result) {
                $('#purchase_data').empty();
                for (var i = 0; i < result.payload.length; i++) {
                    var row = result.payload[i];
                    $('#purchase_data').append("<tr><td onclick=window.location='Purchase/PurchaseInvoiceDetails/" + row.id + "'>" + row.id + "</td><td>" + row.supplier_name + "</td><td>" + row.store_name + "</td><td>" + row.invoice_total + "</td><td><span class='label label-success'>معتمد</span></td><td>" + row.purchase_Added_Time_ar + "</td></tr>");
                }
            },
            error: function () {
                console.log('Login Fail!!!');
            }
        });
    }
}
