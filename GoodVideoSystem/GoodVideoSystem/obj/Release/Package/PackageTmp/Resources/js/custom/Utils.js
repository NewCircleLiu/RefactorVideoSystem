/*
    检查邮箱格式
*/
function checkEmail() {
    var email = jQuery("#email");
    var re = /^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/;
    if (!re.test(email.val()))
    {
        jQuery("#info").text("请输入有效的邮箱！");
        email.val("");
        email.focus();
        return;
    }
}

/*
    检查手机号格式
*/
function checkPhone() {
    var phone = jQuery("#phone");
    if (!(/^1(3|4|5|7|8)\d{9}$/.test(phone.val()))) {
        jQuery("#info").text("请输入正确的手机号！");
        phone.val("");
        phone.focus();
        return;
    }
}


/*
    检查验证码是否正确
*/
function verifyCode() {
    var code = jQuery("#code");
    if (code.val().length <= 0) {
        jQuery("#info").text("验证码不能为空");
        return;
    }

    jQuery.post(
        "/VerifyCode/CheckVerifyCode",
        { "verifycode": code.val() },
        function (data, statusText, xhr) {
            if (data) {
                //$.jBox.info(data, '提示');
                jQuery("#info").text(data);
                code.val("");
                return;
            }
        },
        "text"
     );
}

/*
    刷新验证码
*/
function refreshCode() {
    var codeImg = jQuery("#codeImg");
    var time = new Date().getSeconds();
    var url = "/VerifyCode?time=" + time;
    codeImg.attr("src", url);
}
