var lastSegmentOfUrl = window.location.href.substring(window.location.href.lastIndexOf('/') + 1);
    $.ajax({
        url: "https://localhost:44315/api/Sales/GetCustomSalesInvoice",
        type: 'GET',
        data: {
            sale_inv_id: lastSegmentOfUrl
        },
        beforeSend: function () {
            //$('#').css("visibility", "visible");
        },
        dataType: 'json',
        success: function (result) {
            masterRow = result.payload[0];
            console.log(result)
            if (masterRow.lstProducts != null) {
                for (var i = 0; i < masterRow.lstProducts.length; i++) {
                    var row = masterRow.lstProducts[i];
                    $('#sales_details_data').append("<tr><td>" + row.product_name + "</td><td>" + row.qty + "</td><td>" + row.sales_price_one_product + "</td><td>" + row.total_sales_price_one_product + "</td></tr>");
                }
            }
        }, complete: function () {

        }
    });
