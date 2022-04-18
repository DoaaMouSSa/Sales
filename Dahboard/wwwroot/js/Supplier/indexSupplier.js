$.ajax({
    url: "https://localhost:44315/api/Supplier/GetAllSupplier",
    type: 'GET',
    beforeSend: function () {
        //$('#').css("visibility", "visible");
    },
    dataType: 'json',
    success: function (result) {
        for (var i = 0; i < result.length; i++) {
            var row = result[i];
            $('#supplier_data').append("<tr><td>" + row.supplier_name + "</td><td>" + row.supplier_code + "</td><td>" + row.supplier_phone + "</td><td>" + row.supplier_address + "</td><td><i class='fa fa-edit hand org' data-toggle='modal' data-target='#EditModal" + row.id + "'></i></td><td><i class='fa fa-remove hand red' id=" + row.id + "  data-toggle='modal' data-target='#DeleteModal" + row.id + "'></i></td></tr>");
            $('#container').append("<div class='modal fade' id='EditModal" + row.id + "' tabindex='-1' role='dialog' aria-labelledby='exampleModalLabel' aria-hidden='true'><div class='modal-dialog' role='document'><div class='modal-content'><div class='modal-header'><h4 class='modal-title text-center org' id='exampleModalLabel'>تعديل </h4><button type='button' class='close' data-dismiss='modal' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div><div class='modal-body'><label for='supplier_name_edit" + row.id + "' class='control-label fayroz'>اسم العميل</label><input dir='rtl' type='text' class='form-control border-radius-5 border-fayroz' id='supplier_name_edit" + row.id + "' value='" + row.supplier_name + "' autofocus><label for='supplier_code_edit" + row.id + "' class='control-label fayroz'>كود العميل</label><input dir='rtl' type='text' class='form-control border-radius-5 border-fayroz' id='supplier_code_edit" + row.id + "' value='" + row.supplier_code + "'/><label for='supplier_phone_edit" + row.id + "' class='control-label fayroz'>تليفون العميل</label><input dir='rtl' type='text' class='form-control border-radius-5 border-fayroz' id='supplier_phone_edit" + row.id + "' value='" + row.supplier_phone + "'><label for='supplier_address" + row.id + "' class='control-label fayroz'>عنوان العميل</label><input dir='rtl' type='text' class='form-control border-radius-5 border-fayroz' id='supplier_address_edit" + row.id + "' value='" + row.supplier_address + "'><input type='hidden' class='form-control' id='idCat" + row.id + "' value='" + row.id + "'/></div><div class='modal-footer'><button id='btnSaveEdit' type='button' class='btn-org' onclick='EditSupplier(" + row.id + ");'>حفظ</button><button type='button' class='btn-grey' data-dismiss='modal'>الغاء</button></div></div></div></div>");
            $('#container').append("<div class='modal fade' id='DeleteModal" + row.id + "' tabindex='-1' role='dialog' aria-labelledby='DeleteModalLabel' aria-hidden='true'><div class='modal-dialog' role='document' style='width:300px;'><div class='modal-content'><div class='modal-header'><h5 class='modal-title text-center org'>حذف العميل</h5><button type='button' class='close' data-dismiss='modal' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div><div class='modal-body'><h6> هل تريد حذف عميل <span class='red'>" + row.supplier_name + " </span></h6></div><div class='modal-footer'><button type='button' class='btn-org' onclick='DeleteSupplier(" + row.id + ");' autofocus>تأكيد الحذف</button><button type='button' class='btn-grey' data-dismiss='modal'>الغاء</button></div></div></div></div>");
        }
    }, complete: function () {

    }
});
