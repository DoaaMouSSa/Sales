function disabledBtnSales() {
    if ($("#btnExcuteSales").prop("disabled", true)) {
        $("#btnExcuteSales").addClass("disabledBtn");
    }
}
function disabledBtnPurchase() {
    if ($("#btnExcutePurchase").prop("disabled", true)) {
        $("#btnExcutePurchase").addClass("disabledBtn");
    }
}