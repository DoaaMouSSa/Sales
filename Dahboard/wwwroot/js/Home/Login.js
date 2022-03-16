function Login() {
        alert('start');
        var user_name_val = $("#user_name").val().trim();
        var password_val = $("#password").val().trim();
    var formData = { id: 0, user_name: user_name_val, password: password_val };
    alert('login');
    $.ajax({
            type: 'POST',
            url: "https://localhost:44315/api/Authenication/Login",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(formData),
        success: function (result) {
            if (result == true) location.href = "https://localhost:44306/Category/";
            else location.href = "https://www.google.com";
            },
            error: function () {
                console.log('Login Fail!!!');
            }
        });
    
}
