
function AddPurchase() {
    var totalPurchase = parseFloat($('tfoot th#purchaseTotalNum').text());
    
    var storeId = parseInt(document.getElementById("stores").value);
    var products = [];  
    var i = 0;
    var t = document.getElementById('purchaseTbl');

    $("#purchaseTbl tr").each(function () {

        var productId = parseInt($(t.rows[i].cells[0]).text());
        var productQty = parseInt($(t.rows[i].cells[2]).text());
        var productPrice = parseFloat($(t.rows[i].cells[3]).text());
        var productTotal = parseFloat($(t.rows[i].cells[4]).text());
            var product = {
                id: 0,
                product_id: productId,
                qty: productQty,
                purchase_price_one_product: productPrice,
                total_purchase_price_one_product: productTotal,
                purchase_inv_id: 0,
                notes: "note"
        };
        
        products.push(product);
     i++;
    });
   
   
    var formData = {
        id: 0,
        invoice_total: totalPurchase,
        store_id: storeId,
        purchase_Added_Time: Date.now,
        purchase_invoice_details:
            products,
    }
    $.ajax({
        type: 'POST',
        url: "https://localhost:44315/api/Purchase/AddPurchase",
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(formData),
        success: function () {
            alert('test');
           
        },
        error: function () {
            $('#btnGoToStore').prop("disabled", true);
            $("#message_confirm_purchase").append("تم تنفيذ عمليه الشراء بنجاح");
            $("#purchaseTbl").empty();
            $('tfoot th#purchaseTotalNum').text(0);

        }
    });

}
