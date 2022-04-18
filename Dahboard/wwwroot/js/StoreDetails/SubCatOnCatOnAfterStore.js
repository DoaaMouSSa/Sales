function GetSubCats(id) {
    $('#SubCatDD').empty();
    $('#filter_product_data').empty();
    $('#product_name_box').val('');
    $('#product_code_box').val('');
    $('#product_barcode_box').val('');
    RemoveChecked();
    $.ajax({
        url: "https://localhost:44315/api/SubCategory/FilterSubCatOnCat",
        type: 'GET',
        data: {
            id: id
        },
        beforeSend: function () {
            //$('#').css("visibility", "visible");
        },
        dataType: 'json',
        success: function (result) {
            $('#SubCatDD').append("<option value='0'>اختر تصنيف</option>");
            for (var i = 0; i < result.length; i++) {
                var row = result[i];
                $('#SubCatDD').append("<option value=" + row.id + ">" + row.subcat_name + "</option>");
            }
        }, complete: function () {
            //$("#").css("visibility", "hidden");
        }
    });
}