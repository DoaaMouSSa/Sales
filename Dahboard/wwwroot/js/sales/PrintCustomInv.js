
    function Print() {
        if ($("#txtName1").val() != "" && $("#btnExcuteSales").is(":disabled")) {
            var id = $("#invCode").val();
            window.open("https://localhost:44306/SalesReport/SaleInvoice?inv_number=" + id, "_blank");
        }
        else if ($("#txtName1").val() == "") {
            alert('لا يوجد بيانات للطباعه');
        } else {
            alert('يوجد خطأ');
        }
    }

function PrintForShowPage() { 
    if ($("#sales_details_data tr").length >= 1) {
        var id = $("#invCode").text();
        window.open("https://localhost:44306/SalesReport/SaleInvoice?inv_number=" + id, "_blank");
    }
    else {
        alert('لا يوجد منتجات بالفاتوره');
    }
}