function GetProductsBe4AddToInvoice(val, txtId) {
    var txt_id = txtId.substring(7);
    if (val != "") {
        var storeId=$("#StoreDD").val();
        $("#selectedProducts").empty();
        $.ajax({
            url: "https://localhost:44315/api/Product/GetAllProductBeforeAddToInvoice",
            type: 'GET',
            beforeSend: function () {
                //$('#').css("visibility", "visible");
            },
            dataType: 'json',
            data: {
                character: val,
                store_id: storeId
            },
            success: function (result) {
                for (var i = 0; i < result.payload.length; i++) {
                    var row = result.payload[i];
                    $("#tblShowProducts").removeClass("hidden");
                    if (row.qty <= 0) {
                        $("#selectedProducts").append("<tr onclick='getProductInfo(" + txt_id + "," + row.id + ",\" " + row.product_name + "\"," + row.sale_price + ")' id=" + row.id + "><td class='tdBlack'>" + row.product_name + "</td><td class='tdBlack'>" + row.sale_price + "</td><td class='tdBlack bgBlack'><span class='text-danger'>" + row.qty + "</span></td></tr>");

                    } else{
                        $("#selectedProducts").append("<tr onclick='getProductInfo(" + txt_id + "," + row.id + ",\" " + row.product_name + "\"," + row.sale_price + ")' id=" + row.id + "><td class='tdBlack'>" + row.product_name + "</td><td class='tdBlack'>" + row.sale_price + "</td><td class='tdBlack'>" + row.qty + "</td></tr>");
                    }
                }
            }, complete: function () {

            }
        });
    } else {
        $(".txtProductPrice").val('');
        $(".txtProductQty").val('');
        $(".txtOneProductTotalPrice").val('');
    }
}

function getProductInfo(txtId, id, name, price) {
    var numOftr = $("#salesTbl tr").length;
    if (numOftr == 2) {
        $("#" + txtId + "").val(id);
        $("#txtName" + txtId + "").val(name);
        $("#txtPrice" + txtId + "").val(price);
        $("#txtQty" + txtId + "").val(1);
        getTotalForOneProduct(txtId);
        $("#selectedProducts").empty();
        $("#tblShowProducts").addClass("hidden");
        $("#txtQty" + txtId + "").focus();
        getTotal();

    } else {
        var t = document.getElementById('salesTbl');
        var i = 1;
        for (i; i < numOftr; i++) {
            var productId = parseInt(t.rows[i].cells[0].getElementsByTagName('input')[0].value);
            if (productId == id) {
                var oldQty = parseInt(t.rows[i].cells[3].getElementsByTagName('input')[0].value);
                var NewQtyAfterIncrease = oldQty + 1;
                t.rows[i].cells[3].getElementsByTagName('input')[0].value = NewQtyAfterIncrease;
                getTotal();
                return true;
                break;
            }
        }
                $("#" + txtId + "").val(id);
                $("#txtName" + txtId + "").val(name);
                $("#txtPrice" + txtId + "").val(price);
                $("#txtQty" + txtId + "").val(1);
                getTotalForOneProduct(txtId);
                $("#selectedProducts").empty();
                $("#tblShowProducts").addClass("hidden");
                $("#txtQty" + txtId + "").focus();
            getTotal();
     
        }
    }

function CalculateSalesinvoiceForOneProduct(idRow) {
    var index = $(idRow).closest('tr').index();
    var t = document.getElementById('salesTblBody');
    var price = t.rows[index].cells[2].getElementsByTagName('input')[0].value;
    var qty = t.rows[index].cells[3].getElementsByTagName('input')[0].value;
    var totalofrow = (qty * price);
    t.rows[index].cells[4].getElementsByTagName('input')[0].value = totalofrow;
    getTotal();
}
function getTotalForOneProduct(txtId) {
    var price = $("#txtPrice" + txtId + "").val();
    var qty = $("#txtQty" + txtId + "").val();
    $("#txtTotalForOneProduct" + txtId + "").val(price * qty);
}
$('.btn-add-row').on('click', () => {
    //item-tr
    var $lastRow = $('.item:last');
    if ($lastRow.find('input:first').val() != "") {
        var lastId = parseInt($lastRow.find('input:first').attr('id'));
        var newId = lastId+1;
        var $newRow = $lastRow.clone().find("input").removeAttr("id")
            .val("").end();
        $newRow.insertAfter($lastRow);
        $newRow.find('input:first').focus();
        //pure id number
        $newRow.find('input:first').attr("id", newId)
        $newRow.eq(0).find('input.txtProductName').attr("id", "txtName" + newId)
        $newRow.eq(0).find('input.txtProductPrice').attr("id", "txtPrice" + newId)
        $newRow.eq(0).find('input.txtProductQty').attr("id", "txtQty" + newId)
        $newRow.eq(0).find('input.txtOneProductTotalPrice').attr("id", "txtTotalForOneProduct" + newId)

    } else {
        alert("please fill the first row");
    }
    
});
function DeleteRow(idRow) {
    var rowsCount = $('#salesTbl tbody').find('tr').length;
    if (rowsCount > 1) {
        var index = $(idRow).closest('tr').index();
        index = index + 1;
        document.getElementById("salesTbl").deleteRow(index);
        getTotal();
    }
}
function getTotal() {
    var i = 0;
    var sum = 0;
    var t = document.getElementById('salesTbl');
    for (var i = 1; i < t.rows.length; i++) {
        sum += parseInt(t.rows[i].cells[4].getElementsByTagName('input')[0].value);
    }
    $("#btnTotalInvoice").text(sum);
    $("#Be4DiscountNumberSale").val(sum)
}