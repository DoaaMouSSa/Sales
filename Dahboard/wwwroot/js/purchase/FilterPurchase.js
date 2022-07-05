var supId;
var storeId;
var fromDate;
var toDate;
var fromCode;
var toCode;
var isDeleted;
function check(input) {   
    var checkboxes = document.getElementsByClassName("chbox");
    for (var i = 0; i < checkboxes.length; i++) {
        //uncheck all
        if (checkboxes[i].checked == true) {
            checkboxes[i].checked = false;
        }
    }
    //set checked of clicked object
    if (input.checked == true) {
        input.checked = false;
    }
    else {
        input.checked = true;
    }
    var val = input.value;
    isDeleted = val;
}
function FilterPurchase() {
     supId = $('#SupplierDD').val();
     storeId = $('#StoreDD').val();
     fromDate = $('#fromDate').val();
    toDate = $('#toDate').val();
    fromCode = $('#codeStart').val();
    toCode = $('#codeEnd').val();
    if (fromCode == "") fromCode = 0;
    if (toCode == "") toCode = 0;
    $('#purchase_data').empty();
        $.ajax({
            url: "https://localhost:44315/api/Purchase/FilterPurchaseInvoices",
            type: 'GET',
            beforeSend: function () {
                //$('#').css("visibility", "visible");
            },
            dataType: 'json',
            data:{
                supplier_id:supId,
                store_id:storeId,
                from_date: fromDate,
                to_date: toDate,
                from_code: fromCode,
                to_code: toCode,
                is_deleted: isDeleted
        }, 
            success: function (result) {
                api = "";
                api = "/api/Purchase/FilterPurchaseInvoices";
                for (var i = 0; i < result.payload.length; i++) {
                    var row = result.payload[i];
                    if (row.isDeleted == 0) {
                        $('#purchase_data').append("<tr><td onclick=window.location='Purchase/PurchaseInvoiceDetails/" + row.id + "'>" + row.pur_inv_code + "</td><td>" + row.supplier_name + "</td><td>" + row.store_name + "</td><td>" + row.invoice_total + "</td><td><span class='label label-success'>معتمد</span></td><td>" + row.purchase_Added_Time_ar + "</td><td><i class='fa fa-remove hand red' id=" + row.id + " data-toggle='modal' data-target='#DeleteModal" + row.id + "'></i></td></tr>");
                        $('#purchase_data').append("<div class='modal fade' id='DeleteModal" + row.id + "' tabindex='-1' role='dialog' aria-labelledby='DeleteModalLabel' aria-hidden='true'><div class='modal-dialog' role='document' style='width:300px;'><div class='modal-content'><div class='modal-header'><h5 class='modal-title text-center org'>حذف فاتوره المشتريات  </h5><button type='button' class='close' data-dismiss='modal' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div><div class='modal-body'><h6> هل تريد حذف فاتوره المشتريات برقم  <span class='red'>" + row.id + " </span></h6></div><div class='modal-footer'><button type='button' class='btn-org' onclick='DeletePurchaseInv(" + row.id + ");' autofocus>تأكيد الحذف</button><button type='button' class='btn-grey' data-dismiss='modal'>الغاء</button></div></div></div></div>");
                    }
                    else
                        $('#purchase_data').append("<tr onclick=window.location='Purchase/PurchaseInvoiceDetails/" + row.id + "'><td>" + row.pur_inv_code + "</td><td>" + row.supplier_name + "</td><td>" + row.store_name + "</td><td>" + row.invoice_total + "</td><td><span class='label label-danger'>غير معتمد</span></td><td>" + row.purchase_Added_Time_ar + "</td><td><i class='fa fa-remove hidden'></i></td></tr>");
                }
            }, complete: function () {

            }
        });
}
