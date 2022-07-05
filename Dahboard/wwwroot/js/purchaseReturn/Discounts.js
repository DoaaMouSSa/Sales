
function CalculateDiscounts() {
    let Be4DiscountNumberReturnPur = $("#Be4DiscountNumberReturnPur").val();
    let DiscountNumberReturnPur = $("#DiscountNumberReturnPur").val();
    let DiscountPerNumberReturnPur = $("#DiscountPerNumberReturnPur").val();
    let TaxNumberReturnPur = $("#TaxNumberReturnPur").val();
    let TaxDiscountNumberReturnPur = $("#TaxDiscountNumberReturnPur").val();
    if (DiscountNumberReturnPur == "") DiscountNumberReturnPur = 0;
    if (DiscountPerNumberReturnPur == "") DiscountPerNumberReturnPur = 0;
    if (TaxNumberReturnPur == "") TaxNumberReturnPur = 0;
    if (TaxDiscountNumberReturnPur == "") TaxDiscountNumberReturnPur = 0;
    if ($('#txtName1').val() != "") {
        $.ajax({
            url: "https://localhost:44315/api/PurchaseReturn/GetTotalAfterDiscounts",
            type: 'GET',
            beforeSend: function () {
                //$('#').css("visibility", "visible");
            },
            dataType: 'json',
            data: {
                total: Be4DiscountNumberReturnPur,
                discountNum: DiscountNumberReturnPur,
                discountPer: DiscountPerNumberReturnPur,
                tax: TaxNumberReturnPur,
                taxM: TaxDiscountNumberReturnPur
            },
            success: function (result) {
                $("#FinalTotalInvoice").text(result.payload.final_total);
                $("#discountAmount").text(result.payload.discount);
                $("#DiscountNumberCalculationReturnPur").val(result.payload.discount);
                $("#taxAmount").text(result.payload.tax);
                $("#discountTaxAmount").text(result.payload.tax_discount);
            }, complete: function () {

            }
        });
    } else {
        alert('عفوا الفاتوره فارغه');
      
    }
}
