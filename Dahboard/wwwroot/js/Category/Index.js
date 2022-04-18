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
            $('#cat_data').append("<tr><td>" + row.cat_name + "</td><td><i class='fa fa-edit hand org' data-toggle='modal' data-target='#EditModal" + row.id + "'></i></td><td><i class='fa fa-remove hand red' id=" + row.id + "  data-toggle='modal' data-target='#DeleteModal" + row.id + "'></i></td></tr>");
            $('#container').append("<div class='modal fade' id='EditModal" + row.id + "' tabindex='-1' role='dialog' aria-labelledby='exampleModalLabel' aria-hidden='true'><div class='modal-dialog' role='document'><div class='modal-content'><div class='modal-header'><h4 class='modal-title text-center org' id='exampleModalLabel'>تعديل القسم</h4><button type='button' class='close' data-dismiss='modal' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div><div class='modal-body'><label for='cat_name_edit" + row.id + "' class='control-label fayroz'>اسم القسم</label><input dir='rtl' type='text' class='form-control border-radius-5 border-fayroz' id='cat_name_edit" + row.id + "' value='" + row.cat_name + "' autofocus onkeypress='PressEnterEditCat(event,\"btnSaveEdit\"," + row.id + ")'/><input type='hidden' class='form-control' id='idCat" + row.id + "' value='" + row.id + "'/></div><div class='modal-footer'><button id='btnSaveEdit' type='button' class='btn-org' onclick='EditCat(" + row.id +");'>حفظ</button><button type='button' class='btn-grey' data-dismiss='modal'>الغاء</button></div></div></div></div>");
            $('#container').append("<div class='modal fade' id='DeleteModal" + row.id + "' tabindex='-1' role='dialog' aria-labelledby='DeleteModalLabel' aria-hidden='true'><div class='modal-dialog' role='document' style='width:300px;'><div class='modal-content'><div class='modal-header'><h5 class='modal-title text-center org'>حذف القسم</h5><button type='button' class='close' data-dismiss='modal' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div><div class='modal-body'><h6> هل تريد حذف قسم <span class='red'>" + row.cat_name + " </span></h6></div><div class='modal-footer'><button type='button' class='btn-org' onclick='DeleteCat(" + row.id + ");' autofocus>تأكيد الحذف</button><button type='button' class='btn-grey' data-dismiss='modal'>الغاء</button></div></div></div></div>");
        }
    }, complete: function () {
       
    }
});
function EditCat(id) {
    var new_cat_name = document.getElementById('cat_name_edit' + id).value;
    var formData = { id: id, cat_name: new_cat_name };
    $.ajax({
        type: 'POST',
        url: "https://localhost:44315/api/Category/EditCategory",
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(formData),
        success: function () {
            $('#EditModal').modal('hide');
            location.reload();
        },
        error: function () {
            console.log('Fail!!!');
        }
    });

}
function DeleteCat(id) {
    $.ajax({
        url: "https://localhost:44315/api/Category/DeleteCategory",
        type: 'GET',
        data: {
            id:id
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