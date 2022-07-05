var api = "";
$("#chboxAllPurchase").prop('checked', true);
function getAllPurchase() {
    $('#SupplierDD').val("0");
    $('#StoreDD').val("0");
    $('#purchase_data').empty();
    if ($("#chboxAllPurchase").prop('checked', true)) {
        $.ajax({
            url: "https://localhost:44315/api/Purchase/GetAllPurchase",
            type: 'GET',
            beforeSend: function () {
                //$('#').css("visibility", "visible");
            },
            dataType: 'json',
            success: function (result) {
                api = "";
                api ="/api/Purchase/GetAllPurchase"
                removeChecks();
                for (var i = 0; i < result.payload.length; i++) {
                    var row = result.payload[i];
                    if (row.isDeleted == 0) {
                        $('#purchase_data').append("<tr><td onclick=window.location='Purchase/PurchaseInvoiceDetails/" + row.id + "'>" + row.pur_inv_code + "</td><td>" + row.supplier_name + "</td><td>" + row.store_name + "</td><td>" + row.invoice_total + "</td><td class='0'><span class='label label-success'>معتمد</span></td><td>" + row.purchase_Added_Time_ar + "</td><td><i class='fa fa-remove hand red' id=" + row.id + " data-toggle='modal' data-target='#DeleteModal" + row.id + "'></i></td></tr>");
                        $('#purchase_data').append("<div class='modal fade' id='DeleteModal" + row.id + "' tabindex='-1' role='dialog' aria-labelledby='DeleteModalLabel' aria-hidden='true'><div class='modal-dialog' role='document' style='width:300px;'><div class='modal-content'><div class='modal-header'><h5 class='modal-title text-center org'>حذف فاتوره المشتريات  </h5><button type='button' class='close' data-dismiss='modal' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div><div class='modal-body'><h6> هل تريد حذف فاتوره المشتريات برقم  <span class='red'>" +
                            + " </span></h6></div><div class='modal-footer'><button type='button' class='btn-org' onclick='DeletePurchaseInv(" + row.id + ");' autofocus>تأكيد الحذف</button><button type='button' class='btn-grey' data-dismiss='modal'>الغاء</button></div></div></div></div>");
                    }
                    else
                        $('#purchase_data').append("<tr onclick=window.location='Purchase/PurchaseInvoiceDetails/" + row.id + "'><td>" + row.pur_inv_code + "</td><td>" + row.supplier_name + "</td><td>" + row.store_name + "</td><td>" + row.invoice_total + "</td><td class='1'><span class='label label-danger'>غير معتمد</span></td><td>" + row.purchase_Added_Time_ar + "</td><td><i class='fa fa-remove hidden'></i></td></tr>");
                }
            }, complete: function () {

            }
        });
    }
}
    getAllPurchase();
function removeChecks() {
    if ($("#chboxNotDeletdPurchase").prop('checked', true)) {
        $("#chboxNotDeletdPurchase").prop('checked', false);
    }
    if ($("#chboxDeletdPurchase").prop('checked', true)) {
        $("#chboxDeletdPurchase").prop('checked', false);
    }
}