function createNewPurchaseInvoice() {
    $("#tblShowProducts").addClass("hidden");
    var t = document.getElementById('return_purchaseTbl');
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
    $("#Be4DiscountNumberReturnPur").val("");
    $("#DiscountPerNumberReturnPur").val("");
    $("#DiscountNumberCalculationReturnPur").val("");
    $("#TaxNumberReturnPur").val("");
    $("#DiscountNumberReturnPur").val("");
    $("#TaxDiscountNumberReturnPur").val("");
    $("#ServiceNumberReturnPur").val("");
    $("#discountAmount").text(0);
    $("#taxAmount").text(0);
    $("#discountTaxAmount").text(0);
    $("#FinalTotalInvoice").text(0);
    GetInvPurchaseCode();
}