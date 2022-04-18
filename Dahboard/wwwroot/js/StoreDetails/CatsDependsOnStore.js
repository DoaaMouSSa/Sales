function GetCatsDependsOnStore() {
    $('#CatDD').empty();
    RemoveChecked();
    var store_id = document.getElementById("StoreDD").value;
    $.ajax({
        url: "https://localhost:44315/api/StoreDetails/GetAllCatsByStore",
        type: 'GET',
        data: {
            store_id: store_id
        },
        beforeSend: function () {
            //$('#').css("visibility", "visible");
        },
        dataType: 'json',
        success: function (result) {
            $('#CatDD').append("<option value ='0'>اختر قسم</option>")
            for (var i = 0; i < result.payload.length; i++) {
                var row = result.payload[i];
                $('#CatDD').append("<option value=" + row.id + ">" + row.cat_name + "</option>");
            }
            GetSubCats(0);
        }, complete: function () {

        }
    });
}