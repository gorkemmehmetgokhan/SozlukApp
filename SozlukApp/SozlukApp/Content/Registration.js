$(document).ready(function () {
    $("#view_button3").bind("mousedown touchstart", function () {
        $("#password").attr("type", "text");
    }), $("#view_button3").bind("mouseup touchend", function () {
        $("#password").attr("type", "password");
    }), $("#view_button4").bind("mousedown touchstart", function () {
        $("#verifypassword").attr("type", "text");
    }), $("#view_button4").bind("mouseup touchend", function () {
        $("#verifypassword").attr("type", "password")
    })
});


function submitForm() {

    if (document.getElementById("username").value.trim() === "" && document.getElementById("username").value !== null) {
        $('#message1').css('color', 'red');
        $('#message1').html('Kullanıcı adınızı giriniz.');
    }
    else if (document.getElementById("yourEmail").value.trim() === "" && document.getElementById("yourEmail").value !== null) {
        $('#message1').css('color', 'red');
        $('#message1').html('Email adresinizi giriniz.');
    }   
    else if (checkEmail() === false) {
        $('#message1').css('color', 'red');
        $('#message1').html('Geçerli bir Email adresi giriniz.');

    }
    else if (document.getElementById("password").value.trim() === "" && document.getElementById("password").value !== null) {
        $('#message1').css('color', 'red');
        $('#message1').html('Şifrenizi giriniz');
    }
    else if (document.getElementById("verifypassword").value.trim() === "" && document.getElementById("verifypassword").value !== null) {
        $('#message1').css('color', 'red');
        $('#message1').html('Şifrenizi doğrulamak için tekrar giriniz.');
    }
  
    else {
        var password = $('#password').val();
        var confirm = $('#verifypassword').val();
        if (password == confirm) {
            $('#message1').css('color', 'green');
            document.forms["myform"].submit();

        }
        else {
            $('#message1').css('color', 'red');
            $('#message1').html('Şifre ve şifre tekrarı aynı olmak zorundadır.');
            return false;

        }
       
        return true;
    }
}

function checkEmail() {
    var email = $('#yourEmail').val();
    if ((email.indexOf(".") > 2) && (email.indexOf("@") > 0)) {
        return true;
    }
    else {
        return false;
    }

}