
function AddSales() {
    if ($('#txtName1').val() != "") {
        var t = document.getElementById('return_salesTbl');
        var totalReturnSales = parseFloat($('#Be4DiscountNumberReturnSale').val());
        var ReturnsalesFinalTotal = parseFloat($('#FinalTotalInvoice').text());
        var clientId = parseInt(document.getElementById("CustomerDD").value);
        var storeId = parseInt(document.getElementById("StoreDD").value);
        var ReturnsaleInvCode = document.getElementById("invCode").value;
        var ReturnsalesDiscount = parseFloat($("#discountAmount").text());
        var ReturnsalesTax = parseFloat($('#taxAmount').text());
        var ReturnsalesDiscountTax = parseFloat($('#discountTaxAmount').text());
        if (ReturnsaleInvCode == "")
            ReturnsaleInvCode = 0;
        else
            ReturnsaleInvCode = parseInt(ReturnsaleInvCode);
        var products = [];
        for (var i = 1; i < t.rows.length; i++) {
            var productId = parseInt(t.rows[i].cells[0].getElementsByTagName('input')[0].value);
            var productPrice = parseInt(t.rows[i].cells[2].getElementsByTagName('input')[0].value);
            var productQty = parseFloat(t.rows[i].cells[3].getElementsByTagName('input')[0].value);
            var productTotal = parseFloat(t.rows[i].cells[4].getElementsByTagName('input')[0].value);
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
        }
        var formData = {
            id: 0,
            sale_return_inv_code: ReturnsaleInvCode,
            invoice_return_total: totalReturnSales,
            discount: ReturnsalesDiscount,
            final_total: ReturnsalesFinalTotal,
            tax: ReturnsalesTax,
            tax_discount: ReturnsalesDiscountTax,
            store_id: storeId,
            client_id: clientId,
            sales_Added_Time: Date.now,
            sales_return_invoice_details:
                products,
        }
        $.ajax({
            type: 'POST',
            url: "https://localhost:44315/api/SalesReturn/AddSalesReturnInvoice",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(formData),         
            success: function (result) {
                if (result.code == "51") {
                    swal({
                        title: "فاتوره مرتجع المبيعات",
                        text: "تم تنفيذ عمليه مرتجع المبيعات",
                        icon: "success",
                        buttons: {
                            confirm: { text: 'تم', className: 'sweet-warning btn-org' },
                            //cancel: 'Batalkan'
                        },
                    });
                } else if (result.code == "52") {
                    swal({
                        title: "فاتوره مرتجع المبيعات",
                        text: result.message,
                        icon: "warning",
                        buttons: {
                            confirm: { text: 'الغاء', className: 'sweet-warning btn-org' },
                            //cancel: 'Batalkan'
                        },
                    });
                }
               
            }, complete: function () {
                
               
            }

        });
        disabledBtnSales();
    } else {
        alert('فاتوره البيع فارغه');
    }
}

   
