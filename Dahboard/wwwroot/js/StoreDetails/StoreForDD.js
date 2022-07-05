
    $.ajax({
        url: "https://localhost:44315/api/Store/GetAllStore",
        type: 'GET',
        beforeSend: function () {
            //$('#').css("visibility", "visible");
        },
        dataType: 'json',
        success: function (result) {
            for (var i = 0; i < result.payload.length; i++) {
                var row = result.payload[i];
                $('#StoreDD').append("<option value=" + row.id + ">" + row.store_name + "</option>");
            }
            GetCatsDependsOnStore();
        }, complete: function () {
            //$("#").css("visibility", "hidden");
        }
    });
