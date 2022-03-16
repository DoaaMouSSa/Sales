function AddSubCat() {
    var sub_cat_name = $("#sub_cat_name").val().trim();
    var e = document.getElementById("departments");
    var cat_id = e.value;
    var formData = { id: 0, subcat_name: sub_cat_name, cat_id: cat_id };
    $.ajax({
        type: 'POST',
        url: "https://localhost:44315/api/SubCategory/AddSubCategory",
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
