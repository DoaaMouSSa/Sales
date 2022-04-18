function GetProductsWithNoQty() {
    $("#chNoQty").prop('checked', false);
    $("#chAnyQty").prop('checked', false);

    if ($('#chHasQty').is(":checked")) {
        var t = document.getElementById('store_product_tbl');
        var numOftr = $("#store_product_tbl tr").length;

        var i = 0;
        for (i; i < numOftr; i++) {
            var qty = parseInt($(t.rows[i].cells[3]).text());
            if (qty == 0) {
                $(t.rows[i].cells[3]).closest('tr').hide();

            } else if (qty > 0) {
                $(t.rows[i].cells[3]).closest('tr').show();

            }
        }

    } else if (!$("#chHasQty").is(":checked")) {
        LoadProductsFromCustomStore();
    }
}
function GetProductsWithHasQty() {
    $("#chHasQty").prop('checked', false);
    $("#chAnyQty").prop('checked', false);

    if ($('#chNoQty').is(":checked")) {
        var t = document.getElementById('store_product_tbl');
        var numOftr = $("#store_product_tbl tr").length;

        var i = 0;
        for (i; i < numOftr; i++) {
            var qty = parseInt($(t.rows[i].cells[3]).text());
            if (qty > 0) {
                $(t.rows[i].cells[3]).closest('tr').hide();

            }
            else if (qty == 0) {
                $(t.rows[i].cells[3]).closest('tr').show();

            }
        }

    } else if (!$("#chNoQty").is(":checked")) {
        LoadProductsFromCustomStore();
    }
}
function GetProductsWithAnyQty() {
    $("#chHasQty").prop('checked', false);
    $("#chNoQty").prop('checked', false);

    if ($('#chAnyQty').is(":checked")) {
        var t = document.getElementById('store_product_tbl');
        var numOftr = $("#store_product_tbl tr").length;

        var i = 0;
        for (i; i < numOftr; i++) {
            var qty = parseInt($(t.rows[i].cells[3]).text());
            if (qty > 0) {
                $(t.rows[i].cells[3]).closest('tr').show();

            }
            else if (qty == 0) {
                $(t.rows[i].cells[3]).closest('tr').show();

            }
        }

    }
    else if (!$("#chAnyQty").is(":checked")) {
        LoadProductsFromCustomStore();
    }
}
function RemoveChecked() {
    $("#chAnyQty").prop('checked', false);
    $("#chHasQty").prop('checked', false);
    $("#chNoQty").prop('checked', false);
}