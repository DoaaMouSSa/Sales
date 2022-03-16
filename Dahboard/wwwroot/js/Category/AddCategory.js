function AddCat() {
    var cat_name = $("#cat_name").val().trim();
    var formData = { id: 0, cat_name: cat_name };
    $.ajax({
        type: 'POST',
        url: "https://localhost:44315/api/Category/AddCategory",
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(formData),
        success: function () {
            $('#exampleModal').modal('hide');
            location.reload();
        },
        error: function () {
            console.log('Fail!!!');
        }
    });

}
