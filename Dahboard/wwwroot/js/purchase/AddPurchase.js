﻿var idOfNewExcutedInv;
function AddPurchase() {
    if ($('#txtName1').val() != "") {
        var t = document.getElementById('purchaseTbl');
        var totalPurchase = parseFloat($('#Be4DiscountNumberPur').val());
        var purchaseFinalTotal = parseFloat($('#FinalTotalInvoice').text());            
        var supplierId = parseInt(document.getElementById("SupplierDD").value);
        var storeId = parseInt(document.getElementById("StoreDD").value);
        var purchaseInvCode = document.getElementById("invCode").value;
        var purchaseDiscount = parseFloat($("#discountAmount").text());
        var purchaseTax = parseFloat($('#taxAmount').text());
        var purchaseDiscountTax = parseFloat($('#discountTaxAmount').text());

        if (purchaseInvCode == "")
            purchaseInvCode = 0;
        else
            purchaseInvCode = parseInt(purchaseInvCode);
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
                purchase_price_one_product: productPrice,
                total_purchase_price_one_product: productTotal,
                purchase_inv_id: 0,
                notes: "note"
            };
            products.push(product);
        }
        var formData = {
            id: 0,
            pur_inv_code: purchaseInvCode,
            invoice_total: totalPurchase,
            discount: purchaseDiscount,
            final_total:purchaseFinalTotal,
            tax: purchaseTax,
            tax_discount: purchaseDiscountTax,
            store_id: storeId,
            supplier_id: supplierId,
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
            success: function (result) {
                if (result.code == "51") {
                    swal({
                        title: "فاتوره الشراء",
                        text: "تم تنفيذ عمليه الشراء",
                        icon: "success",
                        buttons: {
                            confirm: { text: 'تم', className: 'sweet-warning btn-org' },
                            //cancel: 'Batalkan'
                        },
                    });
                    idOfNewExcutedInv = result.payload.id;
                } else if (result.code == "52") {
                    swal({
                        title: "فاتوره الشراء",
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
        disabledBtnPurchase();
    } else {
        alert('فاتوره الشراء فارغه');
    }
}



