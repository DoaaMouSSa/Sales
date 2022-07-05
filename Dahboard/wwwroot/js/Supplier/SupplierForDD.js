$.ajax({
    url: "https://localhost:44315/api/Supplier/GetAllSupplierForDD",
    type: 'GET',
    beforeSend: function () {
        //$('#').css("visibility", "visible");
    },
    dataType: 'json',
    success: function (result) {
        for (var i = 0; i < result.payload.length; i++) {
            var row = result.payload[i];
            $('#SupplierDD').append("<option value="+row.id+"> "+ row.supplier_name+"</option>");
        }
    }, complete: function () { }
});