
function CalculateDiscounts() {
    let Be4DiscountNumberReturnSale = $("#Be4DiscountNumberReturnSale").val();
    let DiscountNumberReturnSale = $("#DiscountNumberReturnSale").val();
    let DiscountPerNumberReturnSale = $("#DiscountPerNumberReturnSale").val();
    let TaxNumberReturnSale = $("#TaxNumberReturnSale").val();
    let TaxDiscountNumberReturnSale = $("#TaxDiscountNumberReturnSale").val();
    if (DiscountNumberReturnSale == "") DiscountNumberReturnSale = 0;
    if (DiscountPerNumberReturnSale == "") DiscountPerNumberReturnSale = 0;
    if (TaxNumberReturnSale == "") TaxNumberReturnSale = 0;
    if (TaxDiscountNumberReturnSale == "") TaxDiscountNumberReturnSale = 0;
    if ($('#txtName1').val() != "") {
        $.ajax({
            url: "https://localhost:44315/api/SalesReturn/GetTotalAfterDiscounts",
            type: 'GET',
            beforeSend: function () {
                //$('#').css("visibility", "visible");
            },
            dataType: 'json',
            data: {
                total: Be4DiscountNumberReturnSale,
                discountNum: DiscountNumberReturnSale,
                discountPer: DiscountPerNumberReturnSale,
                tax: TaxNumberReturnSale,
                taxM: TaxDiscountNumberReturnSale
            },
            success: function (result) {
                $("#FinalTotalInvoice").text(result.payload.final_total);
                $("#discountAmount").text(result.payload.discount);
                $("#DiscountNumberCalculationReturnSale").val(result.payload.discount);
                $("#taxAmount").text(result.payload.tax);
                $("#discountTaxAmount").text(result.payload.tax_discount);
            }, complete: function () {

            }
        });
    } else {
        alert('عفوا الفاتوره فارغه');
      
    }
}
