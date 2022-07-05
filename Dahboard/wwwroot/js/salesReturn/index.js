function getAllSales() {
    $('#CustomerDD').val("0");
    $('#StoreDD').val("0");
    $('#sales_data').empty();
    if ($("#chboxAllSales").prop('checked', true)) {
        $.ajax({
            url: "https://localhost:44315/api/Sales/GetAllSales",
            type: 'GET',
            beforeSend: function () {
                //$('#').css("visibility", "visible");
            },
            dataType: 'json',
            success: function (result) {
                removeChecks();

                for (var i = 0; i < result.payload.length; i++) {

                    var row = result.payload[i];
                    if (row.isDeleted == 0) {
                        $('#sales_data').append("<tr><td onclick=window.location='Sales/SalesInvoiceDetails/" + row.id + "'>" + row.id + "</td><td>" + row.client_name + "</td><td>" + row.store_name + "</td><td>" + row.invoice_total + "</td><td><span class='label label-success'>معتمد</span></td><td>" + row.sales_Added_Time_ar + "</td><td><i class='fa fa-remove hand red' id=" + row.id + " data-toggle='modal' data-target='#DeleteModal" + row.id + "'></i></td></tr>");
                        $('#sales_data').append("<div class='modal fade' id='DeleteModal" + row.id + "' tabindex='-1' role='dialog' aria-labelledby='DeleteModalLabel' aria-hidden='true'><div class='modal-dialog' role='document' style='width:300px;'><div class='modal-content'><div class='modal-header'><h5 class='modal-title text-center org'>حذف فاتوره المبيعات  </h5><button type='button' class='close' data-dismiss='modal' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div><div class='modal-body'><h6> هل تريد حذف فاتوره المبيعات برقم  <span class='red'>" + row.id + " </span></h6></div><div class='modal-footer'><button type='button' class='btn-org' onclick='DeleteSalesInv(" + row.id + ");' autofocus>تأكيد الحذف</button><button type='button' class='btn-grey' data-dismiss='modal'>الغاء</button></div></div></div></div>");
                    }
                    else
                        $('#sales_data').append("<tr onclick=window.location='Sales/SalesInvoiceDetails/" + row.id + "'><td>" + row.id + "</td><td>" + row.client_name + "</td><td>" + row.store_name + "</td><td>" + row.invoice_total + "</td><td><span class='label label-danger'>غير معتمد</span></td><td>" + row.sales_Added_Time_ar + "</td></tr>");
                }
            }, complete: function () {

            }
        });
    }
}
getAllSales();
function removeChecks() {
    if ($("#chboxNotDeletdSales").prop('checked', true)) {
        $("#chboxNotDeletdSales").prop('checked', false);
    }
    if ($("#chboxDeletdSales").prop('checked', true)) {
        $("#chboxDeletdSales").prop('checked', false);
    }
}