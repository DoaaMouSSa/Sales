$.ajax({
    url: "https://localhost:44315/api/Customer/GetAllCustomerForDD",
    type: 'GET',
    beforeSend: function () {
        //$('#').css("visibility", "visible");
    },
    dataType: 'json',
    success: function (result) {
        for (var i = 0; i < result.payload.length; i++) {
            var row = result.payload[i];
            $('#CustomerDD').append("<option value="+row.id+"> "+ row.customer_name+"</option>");
        }
    }, complete: function () { }
});