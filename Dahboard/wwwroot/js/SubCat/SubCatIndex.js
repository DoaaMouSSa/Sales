$.ajax({
    url: "https://localhost:44315/api/SubCategory/GetAllSubCategory",
    type: 'GET',
    beforeSend: function () {
        //$('#').css("visibility", "visible");
    },
    dataType: 'json',
    success: function (result) {
        for (var i = 0; i < result.length; i++) {
            var row = result[i];
            $('#sub_cat_data').append("<tr><td>" + row.subcat_name + "</td><td>" + row.cat_name + "</td><td><i class='fa fa-edit' onclick=EditSubCat(" + row.id + ")></i></td><td><i class='fa fa-remove' id=" + row.id + "  data-toggle='modal' data-target='#DeleteModal" + row.id + "'></i></td></tr>");
            //  $('#ContainSubCat').append("<div class='modal fade' id='EditModal" + row.id + "' tabindex='-1' role='dialog' aria-labelledby='exampleModalLabel' aria-hidden='true'><div class='modal-dialog' role='document'><div class='modal-content'><div class='modal-header'><h5 class='modal-title' id='exampleModalLabel'>تعديل القسم</h5><button type='button' class='close' data-dismiss='modal' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div><div class='modal-body'><input type='text' class='form-control' id='cat_name_edit" + row.id + "' value='" + row.cat_name + "'/><input type='hidden' class='form-control' id='idCat" + row.id + "' value='" + row.id + "'/></div><div class='modal-footer'><button type='button' class='btn btn-primary' onclick=\"EditCat(" + row.id + "," + row.cat_name + ");'\">حفظ</button><button type='button' class='btn btn-secondary' data-dismiss='modal'>الغاء</button></div></div></div></div>");
            $('#ContainSubCat').append("<div class='modal fade' id='DeleteModal" + row.id + "' tabindex='-1' role='dialog' aria-labelledby='DeleteModalLabel' aria-hidden='true'><div class='modal-dialog' role='document' style='width:300px;'><div class='modal-content'><div class='modal-header'><h5 class='modal-title'>حذف التصنيف</h5><button type='button' class='close' data-dismiss='modal' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div><div class='modal-body'><h6> هل تريد حذف تصنيف <span style='color:red'>" + row.subcat_name + " </span></h6></div><div class='modal-footer'><button type='button' class='btn btn-danger' onclick='DeleteSubCat(" + row.id + ");'>تأكيد الحذف</button><button type='button' class='btn btn-secondary' data-dismiss='modal'>الغاء</button></div></div></div></div>");
        }
        
    }, complete: function () {
        //$("#").css("visibility", "hidden");
    }
});

function DeleteSubCat(id) {
    $.ajax({
        url: "https://localhost:44315/api/SubCategory/DeleteSubCategory",
        type: 'GET',
        data: {
            id: id
        },
        beforeSend: function () {
            //$('#').css("visibility", "visible");
        },
        dataType: 'json',
        success: function (result) {
            location.reload();
        }, complete: function () {
            //$("#").css("visibility", "hidden");
        }
    });
}