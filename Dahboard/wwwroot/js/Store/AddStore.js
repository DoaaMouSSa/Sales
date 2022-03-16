function AddStore() {
    var store_name = $("#store_name").val().trim();
    var formData = { id: 0, store_name: store_name };
    $.ajax({
        type: 'POST',
        url: "https://localhost:44315/api/Store/AddStore",
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