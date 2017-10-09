
//获取浏览器指纹
function getBrowserInfo() {
    //默认设置
    var fp1 = new Fingerprint();
    //canvas设置
    var fp2 = new Fingerprint({ canvas: true });
    //插件设置
    var fp3 = new Fingerprint({ ie_activex: true });
    //屏幕设置
    var fp4 = new Fingerprint({ screen_resolution: true });

    var str = "" + fp1.get() + fp2.get() + fp3.get() + fp4.get();

    return $.md5(str);
}

//检查手机号格式
function checkPhone(phone) {
    if (!(/^1(3|4|5|7|8)\d{9}$/.test(phone))) {
        return false;
    }
    return true;
}