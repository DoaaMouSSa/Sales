var lastSegmentOfUrl = window.location.href.substring(window.location.href.lastIndexOf('/') + 1);
    $.ajax({
        url: "https://localhost:44315/api/Purchase/GetCustomPurchaseInvoice",
        type: 'GET',
        data: {
            pur_inv_id: lastSegmentOfUrl
        },
        beforeSend: function () {
            //$('#').css("visibility", "visible");
        },
        dataType: 'json',
        success: function (result) {
            masterRow = result.payload[0];
            console.log(masterRow)
            if (masterRow.lstProducts != null) {
                for (var i = 0; i < masterRow.lstProducts.length; i++) {
                    var row = masterRow.lstProducts[i];
                    $('#purchase_details_data').append("<tr><td>" + row.product_name + "</td><td>" + row.qty + "</td><td>" + row.purchase_price_one_product + "</td><td>" + row.total_purchase_price_one_product + "</td></tr>");
                }
            }
        }, complete: function () {

        }
    });
