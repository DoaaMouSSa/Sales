$.ajax({
    url: "https://localhost:44315/api/Store/GetAllStore",
    type: 'GET',
    beforeSend: function () {
        //$('#').css("visibility", "visible");
    },
    dataType: 'json',
    success: function (result) {
        for (var i = 0; i < result.length; i++) {
            var row = result[i];
            $('#store_data').append("<tr><td>" + row.store_name + "</td><td><i class='fa fa-edit' data-toggle='modal' data-target='#EditModal" + row.id + "'></i></td><td><i class='fa fa-remove' id=" + row.id + "  data-toggle='modal' data-target='#DeleteModal" + row.id + "'></i></td></tr>");
            $('#containerStore').append("<div class='modal fade' id='EditModal" + row.id + "' tabindex='-1' role='dialog' aria-labelledby='exampleModalLabel' aria-hidden='true'><div class='modal-dialog' role='document'><div class='modal-content'><div class='modal-header'><h5 class='modal-title' id='exampleModalLabel'>تعديل المخزن</h5><button type='button' class='close' data-dismiss='modal' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div><div class='modal-body'><input type='text' class='form-control' id='store_name_edit" + row.id + "' value='" + row.store_name + "' autofocus autocomplete='off'><input type='hidden' class='form-control' id='idStore" + row.id + "' value='" + row.id + "'></div><div class='modal-footer'><button type='button' class='btn btn-primary' onclick='EditStore(" + row.id +");'>حفظ</button><button type='button' class='btn btn-secondary' data-dismiss='modal'>الغاء</button></div></div></div></div>");
            $('#containerStore').append("<div class='modal fade' id='DeleteModal" + row.id + "' tabindex='-1' role='dialog' aria-labelledby='DeleteModalLabel' aria-hidden='true'><div class='modal-dialog' role='document' style='width:300px;'><div class='modal-content'><div class='modal-header'><h5 class='modal-title'>حذف المخزن</h5><button type='button' class='close' data-dismiss='modal' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div><div class='modal-body'><h6> هل تريد حذف مخزن <span style='color:red'>" + row.store_name + " </span></h6></div><div class='modal-footer'><button type='button' class='btn btn-danger' onclick='DeleteStore(" + row.id + ");' autofocus>تأكيد الحذف</button><button type='button' class='btn btn-secondary' data-dismiss='modal'>الغاء</button></div></div></div></div>");
        }
    }, complete: function () {
        //$("#").css("visibility", "hidden");
    }
});
function EditStore(id) {
    var storeName = document.getElementById("store_name_edit" + id).value;
    var formData = { id: id, store_name: storeName };
    $.ajax({
        type: 'POST',
        url: "https://localhost:44315/api/Store/EditStore",
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
function DeleteStore(id) {
    $.ajax({
        url: "https://localhost:44315/api/Store/DeleteStore",
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