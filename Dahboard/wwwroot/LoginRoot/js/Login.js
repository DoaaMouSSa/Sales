function Login() {
    var userName = $("#user_name").val().trim();
    var Password = $("#password").val().trim();
    var formData = {
        username: userName,
        password: Password
    };
    if (userName != "" && Password !="") {
        $.ajax({
            type: 'POST',
            url: "https://localhost:44315/api/Authentication/Login",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(formData),
            success: function (data) {
                let token = data.token;
                if (token == "0") {
                    AddAlertForWrongData();
                } else {
                    AddAlertForLoginSuccess();
                    localStorage.setItem("key", token);
                    window.location.replace("/Dashboard");
                }
            },
            error: function () {
                console.log('Fail!!!');
            }
        });
    } else if (userName == "") {
        ClearWarningMsgs();
        $('#RequiredUserName').append('برجاء ادخال اسم المستحدم');
    } else if (Password == "") {
        ClearWarningMsgs();
        $('#RequiredPassword').append('برجاء ادخال  كلمه السر');
    }
    else if (userName == "" && Password == "") {
        ClearWarningMsgs();
        $('#RequiredUserName').append('برجاء ادخال اسم المستحدم');
        $('#RequiredPassword').append('برجاء ادخال  كلمه السر');

    } else {
        console.log("error!!");
    }
}
function ClearWarningMsgs() {
    $('#RequiredUserName').empty();
    $('#RequiredPassword').empty();
}
function AddAlertForWrongData() {
    $("#alertLoginFail").css("display", "block");
}
function RemoveAlertForWrongData() {
    $("#alertLoginFail").css("display", "none");
}
function AddAlertForLoginSuccess() {
    $("#alertLoginSuccess").css("display", "block");
}
function RemoveAlertForLoginSuccess() {
    $("#alertLoginSuccess").css("display", "none");
}

