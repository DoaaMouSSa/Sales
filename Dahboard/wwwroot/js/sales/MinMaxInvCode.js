
function GetDataForIvoiceCodes() {
        $.ajax({
            url: "https://localhost:44315/api/Sales/GetMinMaxInvCode",
            type: 'GET',
            beforeSend: function () {
                //$('#').css("visibility", "visible");
            },
            dataType: 'json',
            success: function (result) {
                $("#codeStart").val(result.item1)
                $("#codeEnd").val(result.item2)
            }, complete: function () {

            }
        });
    }


