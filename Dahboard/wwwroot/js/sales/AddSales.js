
function AddSales() {
    var totalSales = parseFloat($('tfoot th#salesTotalNum').text());
    var storeId = parseInt(document.getElementById("stores").value);
    var products = [];
    var i = 0;
    var t = document.getElementById('salesTbl');

    $("#salesTbl tr").each(function () {

        var productId = parseInt($(t.rows[i].cells[0]).text());
        var productQty = t.rows[i].cells[2].getElementsByTagName('input')[0].value;
        var productPrice = parseFloat($(t.rows[i].cells[3]).text());
        var productTotal = parseFloat($(t.rows[i].cells[4]).text());
        var product = {
            id: 0,
            product_id: productId,
            qty: productQty,
            sales_price_one_product: productPrice,
            total_sales_price_one_product: productTotal,
            sales_inv_id: 0,
            notes: "note"
        };

        products.push(product);
        i++;
    });


    var formData = {
        id: 0,
        invoice_total: totalSales,
        store_id: storeId,
        sales_Added_Time: Date.now,
        sales_invoice_details:
            products,
    }
    $.ajax({
        type: 'POST',
        url: "https://localhost:44315/api/Sales/AddSales",
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(formData),
        success: function (data) {
            console.log(data);
        },
        error: function (data) {
            $('#btnGoToStore').prop("disabled", true);
            $("#message_confirm_sales").append("تم تنفيذ عمليه البيع بنجاح");
            $("#salesTbl").empty();
            $('tfoot th#salesTotalNum').text(0);

            console.log(data);

        }
    });

}
