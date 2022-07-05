  
$.ajax({
    url: "https://localhost:44315/api/SubCategory/GetAllSubCategory",
    type: 'GET',
    beforeSend: function () {
        //$('#').css("visibility", "visible");
    },
    dataType: 'json',
    success: function (result) {
       // $("body").append($("<script></script>").attr("src", "https://localhost:44306/js/SubCat/LoadCategoriesToComboBox.js"));
        for (var i = 0; i < result.length; i++) {
            var row = result[i];
            $('#sub_cat_data').append("<tr><td>" + row.subcat_name + "</td><td>" + row.cat_name + "</td><td><i class='fa fa-edit hand org' id=" + row.id + " data-toggle='modal' data-target='#EditModal" + row.id + "' onclick='getCatsForEdit(" + row.cat_id + ");'></i></td><td><i class='fa fa-remove hand red' id=" + row.id + "  data-toggle='modal' data-target='#DeleteModal" + row.id + "'></i></td></tr>");
            $('#ContainSubCat').append("<div class='modal fade showModal' id='EditModal" + row.id + "' tabindex='-1' role='dialog' aria-labelledby='exampleModalLabel' aria-hidden='true'><div class='modal-dialog' role='document'><div class='modal-content'><div class='modal-header'><h4 class='modal-title text-center org' id='exampleModalLabel'>تعديل التصنيف</h4><button type='button' class='close' data-dismiss='modal' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div><div class='modal-body'><label for='sub_cat_name_edit" + row.id + "' class='control-label fayroz'>اسم التصنيف</label><input type='text' class='form-control border-radius-5 border-fayroz' id='sub_cat_name_edit" + row.id + "' value='" + row.subcat_name + "' onkeypress='PressEnterEditSubCat(event,\"btnEditSubCat\"," + row.id + ")' autofocus/><br><input type='hidden' class='form-control' id='idCat" + row.id + "' value='" + row.id + "'/><label for='selectCatForEdit' class='control-label fayroz'>القسم </label><select onchange='GetValueForEdit(this);' name='department' id='selectCatForEdit'  class='form-control no-padding border-radius-5 border-fayroz departments' value='" + row.cat_id + "'></select></div><div class='modal-footer'><button type='button' class='btn-org' id='btnEditSubCat' onclick=EditSubCat(" + row.id + ")>حفظ</button><button type='button' class='btn-grey' data-dismiss='modal'>الغاء</button></div></div></div></div>");
            $('#ContainSubCat').append("<div class='modal fade' id='DeleteModal" + row.id + "' tabindex='-1' role='dialog' aria-labelledby='DeleteModalLabel' aria-hidden='true'><div class='modal-dialog' role='document' style='width:300px;'><div class='modal-content'><div class='modal-header'><h4 class='modal-title text-center org'>حذف التصنيف</h4><button type='button' class='close' data-dismiss='modal' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div><div class='modal-body'><h6> هل تريد حذف تصنيف <span class='red'>" + row.subcat_name + " </span></h6></div><div class='modal-footer'><button type='button' class='btn-org' onclick='DeleteSubCat(" + row.id + ");' autofocus>تأكيد الحذف</button><button type='button' class='btn-grey' data-dismiss='modal'>الغاء</button></div></div></div></div>");
        }  
    }, complete: function () {
 
    }
});
var new_cat_id_ForEdit;
function GetValueForEdit(select) {
    new_cat_id_ForEdit = select.value;
}
function EditSubCat(id) {
    var new_sub_cat_name = document.getElementById('sub_cat_name_edit' + id).value;
    var formData = { id: id, subcat_name: new_sub_cat_name, cat_id: new_cat_id_ForEdit};
    $.ajax({
        type: 'POST',
        url: "https://localhost:44315/api/SubCategory/EditSubCategory",
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

function getCatsForEdit(catValue) {
    $('.departments').empty();
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
                if (row.id == catValue) {
                    $('.departments').append("<option value=" + row.id + " Selected>" + row.cat_name + "</option>");
                } else {
                    $('.departments').append("<option value=" + row.id + ">" + row.cat_name + "</option>");
                }
            }
        }, complete: function () {
            //$("#").css("visibility", "hidden");
        }
    });
}