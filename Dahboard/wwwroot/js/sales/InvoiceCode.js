function GetInvSalesCode() {
    $.ajax({
        url: "https://localhost:44315/api/Sales/GetMaxSalesCode",
        type: 'GET',
        beforeSend: function () {
            //$('#').css("visibility", "visible");
        },
        dataType: 'json',
        success: function (result) {
            var invCode = result.payload += 1;
            $("#invCode").val(invCode);
        }, complete: function () {

        }
    });
}
GetInvSalesCode();
