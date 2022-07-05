function Print() {
    if ($("#txtName1").val() != "" && $("#btnExcutePurchase").is(":disabled")) {

        window.open("https://localhost:44306/Report/PurchaseInvoice?inv_number=" + idOfNewExcutedInv, "_blank");
    }
    else if ($("#txtName1").val() == "") {
        alert('لا يوجد بيانات للطباعه');
    } else {
        alert('يوجد خطأ');
    }
}
function PrintForShowPage() { 
    if ($("#purchase_details_data tr").length >= 1) {

        window.open("https://localhost:44306/Report/PurchaseInvoice?inv_number=" + lastSegmentOfUrl, "_blank");
    }
    else {
        alert('لا يوجد منتجات بالفاتوره');
    }
}