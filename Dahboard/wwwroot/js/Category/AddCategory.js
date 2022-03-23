function AddCat() {
    var cat_name = $("#cat_name").val().trim();
    var formData = { id: 0, cat_name: cat_name };
    
    if (cat_name != "") {
        $.ajax({
            type: 'POST',
            url: "https://localhost:44315/api/Category/AddCategory",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(formData),
            success: function (data) {
               // $('#exampleModal').modal('hide');
               // location.reload();
                if (data.payload == null) {
                    clearWarningMsg('#validCatName');
                    $('#validCatName').append(data.message);
                } else {
                    location.reload();
                }
            },
            error: function () {
                console.log('Fail!!!');
            }
        });
    }
    else {
        clearWarningMsg('#validCatName');
        $('#validCatName').append('برجاء ادخال اسم القسم')
    }
    }

function clearWarningMsg(id) {
    $(id).text('');   
}
