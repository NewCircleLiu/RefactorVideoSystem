
//��ȡ�����ָ��
function getBrowserInfo() {
    //Ĭ������
    var fp1 = new Fingerprint();
    //canvas����
    var fp2 = new Fingerprint({ canvas: true });
    //�������
    var fp3 = new Fingerprint({ ie_activex: true });
    //��Ļ����
    var fp4 = new Fingerprint({ screen_resolution: true });

    var str = "" + fp1.get() + fp2.get() + fp3.get() + fp4.get();

    return $.md5(str);
}

//����ֻ��Ÿ�ʽ
function checkPhone(phone) {
    if (!(/^1(3|4|5|7|8)\d{9}$/.test(phone))) {
        return false;
    }
    return true;
}