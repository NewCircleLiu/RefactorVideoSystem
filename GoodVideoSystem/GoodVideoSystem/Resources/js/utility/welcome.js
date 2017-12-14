jQuery(function () {
    jQuery.get(
    "/Login/GetUserInfoFromCookie",
    {},
    function (data) {
        showTime(data);
    }
);
})

//设定倒数时间（秒）
var t = 4;

//显示倒数秒数
function showTime(info) {
    jQuery("#info").text("欢迎您访问古驿平安串珠视频系统！正在准备，稍后将进入系统...");
    if (t == 1) {
        clearTimeout(showTime);
        if (info == "success") {
            location.href = '/User/Home';
        } else {
            location.href = '/User/Login';
        }
    }
    t -= 1;
    setTimeout(showTime(info), 1000);
}