function LoadProducts() {
    $('#product_name_box').val('');
    $('#product_code_box').val('');
    $('#product_barcode_box').val('');
    $('#product_data').empty();
    $.ajax({
        url: "https://localhost:44315/api/Product/GetAllProduct",
        type: 'GET',
        beforeSend: function () {
            //$('#').css("visibility", "visible");
        },
        dataType: 'json',
        success: function (result) {
            for (var i = 0; i < result.length; i++) {
                var row = result[i];
                $('#product_data').append("<tr><td>" + row.product_name + "</td><td>" + row.sub_cat_name + "</td><td>" + row.code + "</td><td>" + row.barcode + "</td><td>" + row.purchase_price + "</td><td>" + row.sale_price + "</td><td><i class='fa fa-edit' onclick='GetAllSabCats(" + row.sub_cat_id + ");' id=" + row.id + " data-toggle='modal' data-target='#EditModal" + row.id + "' onclick='getCatsForEdit(" + row.sub_cat_id + ");'></i></td><td><i class='fa fa-remove' id=" + row.id + "  data-toggle='modal' data-target='#DeleteModal" + row.id + "'></i></td></tr>");
                $('#ContainProduct').append("<div class='modal fade showModal' id='EditModal" + row.id + "' tabindex='-1' role='dialog' aria-labelledby='exampleModalLabel' aria-hidden='true'><div class='modal-dialog' role='document'><div class='modal-content'><div class='modal-header'><h5 class='modal-title' id='exampleModalLabel'>تعديل الصنف</h5><button type='button' class='close' data-dismiss='modal' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div><div class='modal-body'><div class='row'><div class='col-md-6'><label for='SubCatsForComboBOx'>التصنيف</label><select onchange='GetValueForEditSubCat(this);' name='subcat'  class='form-control no-padding selectSubCatForEdit' value='" + row.cat_id + "' autofocus></select></div><div class='col-md-6'><label for='product_name'>اسم الصنف</label><input onkeydown='clearWarningMsg('#validProductName'), clearWarningMsg('#validProductCode')' type='text' class='form-control' id='product_name" + row.id + "' placeholder='اسم الصنف' autocomplete='off' value=" + row.product_name + "><p class='text-danger' id='validProductName'></p></div></div><br /><div class='row'><div class='col-md-6'><label for='product_code'>الكود</label><input onkeydown='clearWarningMsg('#validProductName'), clearWarningMsg('#validProductCode')' type='text' class='form-control' id='product_code" + row.id + "' placeholder='الكود' autocomplete='off' value=" + row.code + "><p class='text-danger' id='validProductCode'></p></div><div class='col-md-6'><label for='product_barcode'>الباركود </label><input type='text' class='form-control' id='product_barcode" + row.id + "' placeholder='الباركود' autocomplete='off' value=" + row.barcode + "></div></div><br/><div class=row'><div class='col-md-6'><label for='purchase_price'>التكلفه السوقيه </label><input type='number' placeholder='التكلفه السوقيه' min='0' class='form-control' id='purchase_price" + row.id + "' value=" + row.purchase_price + "></div><div class='col-md-6'><label for='sale_price'>سعر البيع </label><input type='number' placeholder='سعر البيع' min='0' class='form-control' id='sale_price" + row.id + "' value=" + row.sale_price + "></div></div><input type='hidden' class='form-control' id='idPro" + row.id + "' value='" + row.id + "'><br></div><div class='modal-footer'><button type='button' class='btn btn-primary' onclick=EditProduct(" + row.id + ")>حفظ</button><button type='button' class='btn btn-secondary' data-dismiss='modal'>الغاء</button></div></div></div></div>");
                $('#ContainProduct').append("<div class='modal fade' id='DeleteModal" + row.id + "' tabindex='-1' role='dialog' aria-labelledby='DeleteModalLabel' aria-hidden='true'><div class='modal-dialog' role='document' style='width:300px;'><div class='modal-content'><div class='modal-header'><h5 class='modal-title'>حذف الصنف</h5><button type='button' class='close' data-dismiss='modal' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div><div class='modal-body'><h6> هل تريد حذف صنف <span style='color:red'>" + row.product_name + " </span></h6></div><div class='modal-footer'><button type='button' class='btn btn-danger' onclick='DeleteProduct(" + row.id + ");' autofocus>تأكيد الحذف</button><button type='button' class='btn btn-secondary' data-dismiss='modal'>الغاء</button></div></div></div></div>");
            }
        }, complete: function () {
            //$("#").css("visibility", "hidden");
        }
    });
}

var new_sub_cat_id_ForEdit;
function GetAllSabCats(SubCatValue) {
    $('.selectSubCatForEdit').empty();
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
                if (row.id == SubCatValue) {
                    $('.selectSubCatForEdit').append("<option value=" + row.id + " Selected>" + row.subcat_name + "</option>");
                } else {
                    $('.selectSubCatForEdit').append("<option value=" + row.id + ">" + row.subcat_name + "</option>");
                }
            }
        }, complete: function () {
            //$("#").css("visibility", "hidden");
        }
    });

}
function GetValueForEditSubCat(select) {
    new_sub_cat_id_ForEdit = select.value;
}

function EditProduct(id) {
    var new_pro_name = document.getElementById('product_name' + id).value;
    var new_pro_code = document.getElementById('product_code' + id).value;
    var new_pro_barcode = document.getElementById('product_barcode' + id).value;
    var new_pro_purchase = document.getElementById('purchase_price' + id).value;
    var new_pro_sales = document.getElementById('sale_price' + id).value;
    var formData = {
        id: id,
        code: new_pro_code,
        barcode: new_pro_barcode,
        product_name: new_pro_name,
        purchase_price: new_pro_purchase,
        sale_price: new_pro_sales,
        sub_cat_id: new_sub_cat_id_ForEdit
    };
    $.ajax({
        type: 'POST',
        url: "https://localhost:44315/api/Product/EditProduct",
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
  function DeleteProduct(id) {
        $.ajax({
            url: "https://localhost:44315/api/Product/DeleteProduct",
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
