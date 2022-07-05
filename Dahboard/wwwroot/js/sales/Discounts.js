
function CalculateDiscounts() {
    let Be4DiscountNumberSale = $("#Be4DiscountNumberSale").val();
    let DiscountNumberSale = $("#DiscountNumberSale").val();
    let DiscountPerNumberSale = $("#DiscountPerNumberSale").val();
    let TaxNumberSale = $("#TaxNumberSale").val();
    let TaxDiscountNumberSale = $("#TaxDiscountNumberSale").val();
    if (DiscountNumberSale == "") DiscountNumberSale = 0;
    if (DiscountPerNumberSale == "") DiscountPerNumberSale = 0;
    if (TaxNumberSale == "") TaxNumberSale = 0;
    if (TaxDiscountNumberSale == "") TaxDiscountNumberSale = 0;
    if ($('#txtName1').val() != "") {
        $.ajax({
            url: "https://localhost:44315/api/Sales/GetTotalAfterDiscounts",
            type: 'GET',
            beforeSend: function () {
                //$('#').css("visibility", "visible");
            },
            dataType: 'json',
            data: {
                total: Be4DiscountNumberSale,
                discountNum: DiscountNumberSale,
                discountPer: DiscountPerNumberSale,
                tax: TaxNumberSale,
                taxM: TaxDiscountNumberSale
            },
            success: function (result) {
                console.log(result)
                $("#FinalTotalInvoice").text(result.payload.final_total);
                $("#discountAmount").text(result.payload.discount);
                $("#DiscountNumberCalculationSale").val(result.payload.discount);
                $("#taxAmount").text(result.payload.tax);
                $("#discountTaxAmount").text(result.payload.tax_discount);
            }, complete: function () {

            }
        });
    } else {
        alert('عفوا الفاتوره فارغه');
      
    }
}
