$.ajax({
    url: "https://localhost:44315/api/Statistics/GetCountOfSupplier",
    type: 'GET',
    beforeSend: function () {
        //$('#').css("visibility", "visible");
    },
    dataType: 'json',
    success: function (result) {
        $('#countSup').append(result.payload);
    }, complete: function () {

    }
});
$.ajax({
    url: "https://localhost:44315/api/Statistics/GetCountOfProduct",
    type: 'GET',
    beforeSend: function () {
        //$('#').css("visibility", "visible");
    },
    dataType: 'json',
    success: function (result) {
        $('#countPro').append(result.payload);
    }, complete: function () {

    }
});
$.ajax({
    url: "https://localhost:44315/api/Statistics/GetCountOfSubCategory",
    type: 'GET',
    beforeSend: function () {
        //$('#').css("visibility", "visible");
    },
    dataType: 'json',
    success: function (result) {
        $('#countSubCat').append(result.payload);
    }, complete: function () {

    }
});
$.ajax({
    url: "https://localhost:44315/api/Statistics/GetCountOfCategory",
    type: 'GET',
    beforeSend: function () {
        //$('#').css("visibility", "visible");
    },
    dataType: 'json',
    success: function (result) {
        $('#countCat').append(result.payload);
    }, complete: function () {

    }
});