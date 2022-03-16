function FilterSubCat(id) {
    $('#SubCatFilterPro').empty();
    $('#filter_product_data').empty();
    $('#product_name_box').val('');
    $('#product_code_box').val('');
    $('#product_barcode_box').val('');
    $.ajax({
        url: "https://localhost:44315/api/SubCategory/FilterSubCatOnCat",
        type: 'GET',
        data: {
            id:id
        },
        beforeSend: function () {
            //$('#').css("visibility", "visible");
        },
        dataType: 'json',
        success: function (result) {
            for (var i = 0; i < result.length; i++) {
                var row = result[i];
                $('#SubCatFilterPro').append("<option value=" + row.id + ">" + row.subcat_name + "</option>");
            }
        }, complete: function () {
            //$("#").css("visibility", "hidden");
        }
    });
}