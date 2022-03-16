$.ajax({
    url: "https://localhost:44315/api/Category/GetAllCategory",
    type: 'GET',
    beforeSend: function () {
        //$('#').css("visibility", "visible");
    },
    dataType: 'json',
    success: function (result) {
        for (var i = 0; i < result.length; i++) {
            var row = result[i];
            $('#departments').append("<option value=" + row.id + ">" + row.cat_name + "</option>");
        }
    }, complete: function () {
        //$("#").css("visibility", "hidden");
    }
});