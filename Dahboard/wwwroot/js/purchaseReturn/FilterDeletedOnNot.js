function GetDeletedPurchaseInv() {
    $("#chboxAllPurchase").prop('checked', false);
    $("#chboxNotDeletdPurchase").prop('checked', false);

    if ($('#chboxDeletdPurchase').is(":checked")) {
        var t = document.getElementById('purchaseTbl');
        var numOftr = $("#purchaseTbl tr").length;

        var i = 0;
        for (i; i < numOftr; i++) {
            var purchase_type = $(t.rows[i].cells[4]).text();
            if (purchase_type == 'معتمد') {
                $(t.rows[i].cells[4]).closest('tr').hide();
            }
            else if (purchase_type == 'غير معتمد') {
                $(t.rows[i].cells[3]).closest('tr').show();

            }
        }
    } else {
        getAllPurchase();
    }

    //} else if (!$("#chHasQty").is(":checked")) {
    //    LoadProductsFromCustomStore();
    //}
}
function GetNotDeletedPurchaseInv() {
    $("#chboxAllPurchase").prop('checked', false);
    $("#chboxDeletdPurchase").prop('checked', false);

    if ($('#chboxNotDeletdPurchase').is(":checked")) {
        var t = document.getElementById('purchaseTbl');
        var numOftr = $("#purchaseTbl tr").length;
        var i = 0;
        for (i; i < numOftr; i++) {
            var purchase_type = $(t.rows[i].cells[4]).text();
            if (purchase_type == 'غير معتمد') {
                $(t.rows[i].cells[4]).closest('tr').hide();
            }
            else if (purchase_type == 'معتمد') {
                $(t.rows[i].cells[4]).closest('tr').show();
            }
        }
    } else {
        getAllPurchase();
    }
}
