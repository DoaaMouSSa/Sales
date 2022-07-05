﻿
function SearchByName(value) {
    $('#product_barcode_box').val('');
    var subcat_val = document.getElementById("SubCatFilterPro").value;
    if (subcat_val == "") subcat_val = 0
    int_sub_val = parseInt(subcat_val);
    if (value != null) {
        $('#filter_product_data_purchase').empty();
        $.ajax({
            url: "https://localhost:44315/api/ProductSearch/SearchByName",
            type: 'GET',
            data: {
                character: value,
                sub_cat_id: int_sub_val
            },
            beforeSend: function () {
                //$('#').css("visibility", "visible");
            },
            dataType: 'json',
            success: function (result) {
                for (var i = 0; i < result.length; i++) {
                    var row = result[i];
                    $('#filter_product_data_purchase').append("<tr data-toggle='modal' data-target='#ForQtyModalPurchase" + row.id + "'><td>" + row.product_name + "</td><td>" + row.code + "</td><td>" + row.barcode + "</td><td>" + row.purchase_price + "</td></tr>");
                    $('#filter_product_data_purchase').append("<div class='modal fade qtyModalPurchase' id='ForQtyModalPurchase" + row.id + "' tabindex='-1' role='dialog' aria-labelledby='ForQtyModalLabel' aria-hidden='true'><div class='modal-dialog' role='document' style='width:200px;'><div class='modal-content'><div class='modal-header'><h5 class='modal-title'>الكميه</h5></div><div class='modal-body'><input type='number' min='1' value='1' class='form-control' id='qty" + row.id +"'  onkeyup='PressEnter(event)' autofocus></div><div class='modal-footer'><button id='btnCloseQtyModal' type='button' class='btn btn-success' onClick='GetDataForPurchase(" + row.id + ",\" " + row.product_name + "\"," + row.purchase_price + ");'>تأكيد</button></div></div></div></div>");
                }
            }, complete: function () {

            }
        });
    }
}
function PressEnter(event) {
        if (event.keyCode === 13) {
            event.preventDefault();
            document.getElementById("btnCloseQtyModal").click();     
    }
}
function GetDataForPurchase(id, name, price) {
    var numOftr = $("#purchaseTbl tr").length;
    if (numOftr == 0) {
        var NewQty = document.getElementById('qty' + id).value;
        $('#purchaseTbl').append("<tr><td>" + id + "</td><td>" + name + "</td><td style='width:25%'><input type='number' value=" + NewQty + " style='border:none;' onclick='CalculatePurchaseinvoice(this);' onkeyup='CalculatePurchaseinvoice(this);'></td><td>" + price + "</td><td>" + (NewQty * price) + "</td><td><i class='fa fa-remove' style='color:red;cursor:pointer'; onclick='DeleteRow(this);'></i></td></tr>");
        $('.qtyModalPurchase').modal('hide');
        is_existPurchase();
        getTotal();
    }
    else {
        var t = document.getElementById('purchaseTbl');
        var i = 0;
        for (i; i < numOftr ; i++) {
            var productId = parseInt($(t.rows[i].cells[0]).text());
            if (productId == id) {
                var oldQty = parseInt(t.rows[i].cells[2].getElementsByTagName('input')[0].value);
                var NewQtyForSamePro = parseInt(document.getElementById('qty' + id).value);
                var NewQtyAfterIncrease = oldQty + NewQtyForSamePro;
                t.rows[i].cells[2].getElementsByTagName('input')[0].value = NewQtyAfterIncrease;
                $('.qtyModalPurchase').modal('hide');
                CalculatePurchaseinvoiceByRowNum(i);
                return;
                break;
            } else {
                
            }    
        }
        var New_Qty = document.getElementById('qty' + id).value;
        $('#purchaseTbl').append("<tr><td>" + id + "</td><td>" + name + "</td><td style='width:25%'><input type='number' value=" + New_Qty + " style='border:none;' onclick='CalculatePurchaseinvoice(this);' onkeyup='CalculatePurchaseinvoice(this);'></td><td>" + price + "</td><td>" + (New_Qty * price) + "</td><td><i class='fa fa-remove' style='color:red;cursor:pointer'; onclick='DeleteRow(this);'></i></td></tr>");
        $('.qtyModalPurchase').modal('hide');
        getTotal();
        return;
    }
}

function CalculatePurchaseinvoiceByRowNum(index) {
    var t = document.getElementById('purchaseTbl');
    var qty = t.rows[index].cells[2].getElementsByTagName('input')[0].value;
    var price = parseInt($(t.rows[index].cells[3]).text());
    var totalofrow = (qty * price);
    $(t.rows[index].cells[4]).text(totalofrow);
    getTotal();
}
function CalculatePurchaseinvoice(idRow) {
    var index = $(idRow).closest('tr').index();
    var t = document.getElementById('purchaseTbl');
    var qty = t.rows[index].cells[2].getElementsByTagName('input')[0].value;
    var price = parseInt($(t.rows[index].cells[3]).text());
    var totalofrow = (qty * price);
    $(t.rows[index].cells[4]).text(totalofrow);
    getTotal();
}
function DeleteRow(idRow) {
    var index = $(idRow).closest('tr').index();
    document.getElementById("purchaseTbl").deleteRow(index);
    getTotal();
}
function is_existPurchase() {
    var tbody = $("#purchaseTbl");
    if (tbody.children().length > 0) {
        $('#btnGoToStore').prop("disabled", false);
        $('#message_confirm_purchase').empty();
        $('#purchaseTotalRow').removeClass("hidden");
    }
}
function getTotal() {
    var i = 0;
    var sum = 0;
    var t = document.getElementById('purchaseTbl');
    $("#purchaseTbl tr").each(function () {
        sum += parseInt($(t.rows[i].cells[4]).text());
        i++;
    });
    $('tfoot th#purchaseTotalNum').text(sum);
}





