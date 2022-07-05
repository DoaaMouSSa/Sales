function createNewSalesInvoice() {
    $("#tblShowProducts").addClass("hidden");
    var t = document.getElementById('salesTbl');
    var i = (t.rows.length) - 1
    for (i; i > 1; i--) {
        t.deleteRow(i);
    }
    emptyFirstRow();
    $("#btnExcuteSales").removeClass("disabledBtn");
    $("#btnExcuteSales").prop("disabled", false)
}
function emptyFirstRow() {
    $('#txtName1').val("");
    $('#txtPrice1').val("");
    $('#txtQty1').val("");
    $('#txtTotalForOneProduct1').val("");
    $("#btnTotalInvoice").text(0);
    $("#Be4DiscountNumberSale").val(0);
    GetInvSalesCode();
}