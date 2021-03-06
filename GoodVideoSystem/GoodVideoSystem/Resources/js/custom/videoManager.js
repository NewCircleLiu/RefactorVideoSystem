jQuery("#searchType").on("change", function () {
    var searchType = this.value;
    var searchValue = jQuery("#searchValue");
    if (searchType == "vid") {
        searchValue.html("<input type='text' class='form-control' name='searchValue' style='width: 200px;' placeholder=请输入视频编号 required/>");
    }
    if (searchType == "videoName") {
        searchValue.html("<input type='text' class='form-control' name='searchValue' style='width: 200px;' placeholder=请输入视频名称 required/>");
    }
});


//导出单个视频的邀请码
function exportExcel(vid,polyVid) {
    var html = "<div style='padding:10px;'><p style='color:red;'>请输入要导出的邀请码数量，0表示导出全部邀请码</p>数量：<input type='text' id='num' name='num' /></div>";
    var submit = function (v, h, f) {
        if (f.num == '') {
            $.jBox.tip("请输入要导出的邀请码数量。", 'error', { focusId: "num" });
            return false;
        }
        $.ajax({
            url: "/VideoCode/ExportExcel",
            data: {
                "vid": vid,
                "polyVid":polyVid,
                "num": f.num
            },
            success: function (data) {
                if (data == "erro") {
                    $.jBox.info('邀请码数量不足', '提示');
                    return;
                }
                window.location.href = "/VideoCode/ExportExcel?vid=" + vid + "&num=" + f.num;
            }
        });
        return true;
    };
    $.jBox(html, { title: "输入邀请码数量", submit: submit });
}

//导出全部邀请码
function exportAll() {
    window.location.href = "/VideoCode/ExportExcel?vid=-1";
}

//删除视频
function deleteVideo(polyVid) {
    var submit = function (v, h, f) {
        if (v == 'ok') {
            $.ajax({
                url: "/PolyProcess/DeleteVideo",
                data: {
                    "polyVid": polyVid
                },
                success: function (data) {
                    if (data = "success") {
                        alert("删除成功");
                        window.location.href = "/VideoManager";
                    }
                    else {
                        alert("删除失败");
                    }
                }
            });
        }
        else if (v == 'cancel')
            jBox.tip("取消操作", 'info');

        return true; //close
    };

    $.jBox.confirm("删除视频将导致视频所对应的全部邀请码一并删除，请再次确认操作！", "提示", submit);
}

//生成邀请码
function createCode(vid) {
    var html = "<div style='padding:10px;'>输入邀请码数量：<br /><br /><input type='text' id='codeCounts' name='codeCounts' style='width:100%'/></div>";
    var submit = function (v, h, f) {
        if (f.codeCounts == '') {
            $.jBox.tip("请输入要生成的邀请码数量。", 'error', { focusId: "codeCounts" });
            return false;
        }
        reg = /^\+?[1-9][0-9]*$/
        if (!reg.test(f.codeCounts)) {
            $.jBox.tip('请输入正确的数字!!', 'error', { focusId: "codeCounts" });
            return false;
        }
        
        $.ajax({
            url: "/VideoCode/CreateCode",
            type: "POST",
            dataType:"json",
            data: {
                "vid": vid,
                "codeCounts": f.codeCounts
            },
            success: function (data) {
                location.reload();
            }
        });
        return true;
    };
    $.jBox(html, { title: "输入邀请码数量", submit: submit, height:150, top:'30%'});
}