jQuery(function () {
    jQuery.get(
    "/Login/SavedeviceUniqueCode",
    {
        "deviceUniqueCode": getBrowserInfo()
    },
    function (data) {
        showTime();
    }
);
})

//设定倒数时间（秒）
var t = 4;

//显示倒数秒数
function showTime() {
    jQuery("#info").text("欢迎您访问古驿平安串珠视频系统！正在准备，稍后将进入系统...");
    if (t == 1) {
        clearTimeout(showTime);
        location.href = '/User/Home';
    }
    t -= 1;
    setTimeout(showTime, 1000);
}