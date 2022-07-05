
function AddSales() {
    if ($('#txtName1').val() != "") {
        var t = document.getElementById('salesTbl');
        var totalSales = parseFloat($('#Be4DiscountNumberSale').val());
        var salesFinalTotal = parseFloat($('#FinalTotalInvoice').text());
        var clientId = parseInt(document.getElementById("CustomerDD").value);
        var storeId = parseInt(document.getElementById("StoreDD").value);
        var saleInvCode = document.getElementById("invCode").value;
        var salesDiscount = parseFloat($("#discountAmount").text());
        var salesTax = parseFloat($('#taxAmount').text());
        var salesDiscountTax = parseFloat($('#discountTaxAmount').text());
        if (saleInvCode == "")
            saleInvCode = 0;
        else
            saleInvCode = parseInt(saleInvCode);
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
            sale_inv_code: saleInvCode,
            invoice_total: totalSales,
            discount: salesDiscount,
            final_total: salesFinalTotal,
            tax: salesTax,
            tax_discount: salesDiscountTax,
            store_id: storeId,
            client_id: clientId,
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
            success: function (result) {
                if (result.code == "51") {
                    swal({
                        title: "فاتوره البيع",
                        text: "تم تنفيذ عمليه البيع",
                        icon: "success",
                        buttons: {
                            confirm: { text: 'تم', className: 'sweet-warning btn-org' },
                            //cancel: 'Batalkan'
                        },
                    });
                } else if (result.code == "52") {
                    swal({
                        title: "فاتوره البيع",
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

   
