function createNewPurchaseInvoice() {
    $("#tblShowProducts").addClass("hidden");
    var t = document.getElementById('purchaseTbl');
    var i = (t.rows.length) - 1
    for ( i ; i > 1; i--) {
        t.deleteRow(i);
    }
    emptyFirstRow();
    $("#btnExcutePurchase").removeClass("disabledBtn");
    $("#btnExcutePurchase").prop("disabled", false)
}
function emptyFirstRow() {
    $('#txtName1').val("");
    $('#txtPrice1').val("");
    $('#txtQty1').val("");
    $('#txtTotalForOneProduct1').val("");
    $("#btnTotalInvoice").text(0);
    $("#Be4DiscountNumberPur").val(0);
    GetInvPurchaseCode();
}