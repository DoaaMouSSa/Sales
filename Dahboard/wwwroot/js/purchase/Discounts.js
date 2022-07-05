
function CalculateDiscounts() {
    let Be4DiscountNumberPur = $("#Be4DiscountNumberPur").val();
    let DiscountNumberPur = $("#DiscountNumberPur").val();
    let DiscountPerNumberPur = $("#DiscountPerNumberPur").val();
    let TaxNumberPur = $("#TaxNumberPur").val();
    let TaxDiscountNumberPur = $("#TaxDiscountNumberPur").val();
    if (DiscountNumberPur == "") DiscountNumberPur = 0;
    if (DiscountPerNumberPur == "") DiscountPerNumberPur = 0;
    if (TaxNumberPur == "") TaxNumberPur = 0;
    if (TaxDiscountNumberPur == "") TaxDiscountNumberPur = 0;
    if ($('#txtName1').val() != "") {
        $.ajax({
            url: "https://localhost:44315/api/Purchase/GetTotalAfterDiscounts",
            type: 'GET',
            beforeSend: function () {
                //$('#').css("visibility", "visible");
            },
            dataType: 'json',
            data: {
                total: Be4DiscountNumberPur,
                discountNum: DiscountNumberPur,
                discountPer: DiscountPerNumberPur,
                tax: TaxNumberPur,
                taxM: TaxDiscountNumberPur
            },
            success: function (result) {
                $("#FinalTotalInvoice").text(result.payload.final_total);
                $("#discountAmount").text(result.payload.discount);
                $("#DiscountNumberCalculationPur").val(result.payload.discount);
                $("#taxAmount").text(result.payload.tax);
                $("#discountTaxAmount").text(result.payload.tax_discount);
            }, complete: function () {

            }
        });
    } else {
        alert('عفوا الفاتوره فارغه');
      
    }
}
