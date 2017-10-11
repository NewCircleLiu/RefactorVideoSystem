// 图片上传
jQuery(function() {
    var $ = jQuery,
        $list = $('#thelist'),
        // 缩略图大小
        thumbnailWidth = 500,
        thumbnailHeight = 300,
        // Web Uploader实例
        uploader;

    // 初始化Web Uploader
    uploader = WebUploader.create({
        // 自动上传。
        auto: true,
        // swf文件路径
        swf: "/plugin/WebUploader/Uploader.swf",
        // 文件接收服务端。
        server: "/Upload/UploadImage",
        pick: {
            innerHTML:"选择图片",
            id: "#picker",
            multiple:false
        },
        method:"POST",
        fileNumLimit: 1,
        fileSizeLimit:1024*1024*3,
        // 只允许选择文件，可选。
        accept: {
            title: 'Images',
            extensions: 'gif,jpg,jpeg,bmp,png',
            mimeTypes: 'image/*'
        },
        compress: {
            width: 500,
            height: 300,
            // 图片质量，只有type为`image/jpeg`的时候才有效。
            quality: 90,
            // 是否允许放大，如果想要生成小图的时候不失真，此选项应该设置为false.
            allowMagnify: false,
            // 是否允许裁剪。
            crop: false,
            // 是否保留头部meta信息。
            preserveHeaders: true,
            // 如果发现压缩后文件大小比原来还大，则使用原来图片
            // 此属性可能会影响图片自动纠正功能
            noCompressIfLarger: false,
            // 单位字节，如果图片大小小于此值，不会采用压缩。
            compressSize: 1024*1024*3
        }
        
    });

    // 当有文件添加进来的时候
    uploader.on( 'fileQueued', function( file ) {
        var $li = jQuery('<div id="' + file.id + '" class="file-item thumbnail">' +
                    '<img>' +
                    '<div class="info">' + file.name + '</div>' +
                '</div>'),
            $img = $li.find('img');

        $list.append( $li );

        // 创建缩略图
        uploader.makeThumb( file, function( error, src ) {
            if ( error ) {
                $img.replaceWith('<span>不能预览</span>');
                return;
            }

            $img.attr( 'src', src );
        }, thumbnailWidth, thumbnailHeight );
    });

     //文件上传过程中创建进度条实时显示。
    uploader.on('uploadProgress', function (file, percentage) {
        var $li = $('#' + file.id),
            $percent = $li.find('.progress .progress-bar');

        // 避免重复创建
        if (!$percent.length) {
            $percent = $('<div class="progress progress-striped active">' +
              '<div class="progress-bar" role="progressbar" style="width: 0%">' +
              '</div>' +
            '</div>').appendTo($li).find('.progress-bar');
        }

        $li.find('p.state').text('上传中:' + percentage * 100 + '%');

        $percent.css('width', percentage * 100 + '%');
    });

    // 文件上传成功，给item添加成功class, 用样式标记上传成功。
    uploader.on('uploadSuccess', function (file, response) {
        var imageUrl = jQuery("#ImageUrl");
        imageUrl.val(response.info);

        var $li = jQuery('#' + file.id);
        var success = $li.find('div.success');
        if (!success.length) {
            success = jQuery('<div class="success"></div>').appendTo($li);
        }
        success.text('图片上传成功');
    });

    // 文件上传失败
    uploader.on('uploadError', function (file, reason) {
        var li = jQuery('#' + file.id);
        var error = li.find('div.error');
        // 避免重复创建
        if (!error.length) {
            error = jQuery('<div class="error"></div>').appendTo(li);
        }
        error.text("图片上传失败");
    });

    // 完成上传完了，成功或者失败，先删除进度条。
    uploader.on('uploadComplete', function (file) {
        $('#' + file.id).find('.progress').fadeOut();
    });
})


//生成GUID
function newGuid() {
    var guid = "";
    for (var i = 1; i <= 32; i++) {
        var n = Math.floor(Math.random() * 16.0).toString(16);
        guid += n;
        if ((i == 8) || (i == 12) || (i == 16) || (i == 20))
            guid += "-";
    }
    return guid;
}

// 文件上传
jQuery(function () {

    WebUploader.Uploader.register(
        {
            "before-send-file": "beforeSendFile",  // 整个文件上传前
            "before-send": "beforeSend",           // 每个分片上传前
            "after-send-file": "afterSendFile"     // 分片上传完毕
        },
        {
            beforeSendFile: function (file) {
                var task = new $.Deferred();
                //获取文件校验值
                //var filemd5 = (new WebUploader.Uploader()).md5File(file);
                
                jQuery.ajax({
                    url: "/Upload/UploadVideo",
                    data: {
                        type: "init",
                        guid: newGuid()
                    },
                    type: "POST",
                    dataType: "json",
                    cache: false,
                    async: false,  // 同步
                    timeout: 1000, //超时的话，认为该文件不曾上传过
                }).then(function (data, textStatus, jqXHR) {
                    if (data.info == "true") { //若存在，这返回失败给WebUploader，表明该文件不需要上传                
                        task.reject();
                    } else {
                        task.resolve();
                    }
                }, function (jqXHR, textStatus, errorThrown) { //任何形式的验证失败，都触发重新上传
                    task.resolve();
                });
                return $.when(task);
            },
            beforeSend: function (block) {
                //分片验证是否已传过，用于断点续传
                var task = new $.Deferred();
                $.ajax({
                    type: "POST",
                    url: "/Upload/UploadVideo",
                    data: {
                        type: "block",
                        chunk: block.chunk,
                        currentBlockSize: block.end - block.start
                    },
                    cache: false,
                    async: false,  // 同步
                    timeout: 1000, //超时的话，认为该分片未上传过
                    dataType: "json"
                }).then(function (data, textStatus, jqXHR) {
                    if (data.info == "true") { //若存在，返回失败给WebUploader，表明该分块不需要上传
                        task.reject();
                    } else {
                        task.resolve();
                    }
                }, function (jqXHR, textStatus, errorThrown) { //任何形式的验证失败，都触发重新上传
                    task.resolve();
                });
                return $.when(task);
            },
            afterSendFile: function (file) {
                var chunksTotal = Math.ceil(file.size / (1024 * 1024 * 3));
                if (chunksTotal > 1) {
                    //合并请求
                    var task = new $.Deferred();
                    $.ajax({
                        type: "POST",
                        url: "/Upload/UploadVideo",
                        data: {
                            type: "merge",
                            chunks: chunksTotal,
                            size: file.size
                        },
                        cache: false,
                        async: false,  // 同步
                        dataType: "json"
                    }).then(function (data, textStatus, jqXHR) {
                        jQuery("#VideoUrl").val(data.info);
                        //alert(data.info);
                        task.resolve();
                    }, function (jqXHR, textStatus, errorThrown) {
                        current_uploader.uploader.trigger('uploadError');
                        task.reject();
                    });
                    return $.when(task);
                }
            }
        }
    );

    var $ = jQuery,
        $list = $('#videoiList'),
        $btn = $('#ctlBtn'),
        state = 'pending';

    var uploader = WebUploader.create({
        auto: true,
        resize: false,
        swf: "/plugin/WebUploader/Uploader.swf",
        server: "/Upload/UploadVideo",
        method: "POST",
        pick: '#videoPicker',
        //开启分块上传
        chunked: true,
        //每块大小
        chunkSize: 1024 * 1024 * 3,
        //分块上传失败后重传的次数
        chunkRetry: 5,
        threads: 1,
        //队列大小限制
        fileNumLimit: 1,
        //单个文件大小限制
        fileSingleSizeLimit: 1024 * 1024 * 1024 * 5,
        accept: {
            title: 'Videoes',
            extensions: 'mp4,flv',
            mimeTypes: 'video/*'
        },
        formData: {
            guid: newGuid()
        }
    });

    // 当有文件添加进来的时候
    uploader.on('fileQueued', function (file) {
        $list.append('<div id="' + file.id + '" class="item">' +
            '<h4 class="info">' + file.name + '</h4>' +
            '<p class="state">等待上传...</p>' +
        '</div>');
    });

    // 文件上传过程中创建进度条实时显示。
    uploader.on('uploadProgress', function (file, percentage) {
        var $li = $('#' + file.id),
            $percent = $li.find('.progress .progress-bar');

        // 避免重复创建
        if (!$percent.length) {
            $percent = $('<div class="progress progress-striped active">' +
              '<div class="progress-bar" role="progressbar" style="width: 0%">' +
              '</div>' +
            '</div>').appendTo($li).find('.progress-bar');
        }

        $li.find('p.state').text('上传中:' + percentage * 100 + '%');

        $percent.css('width', percentage * 100 + '%');
    });

    uploader.on('uploadSuccess', function (file,response) {
        $('#' + file.id).find('p.state').text('上传成功');
    });

    uploader.on('uploadError', function (file, reason) {
        $('#' + file.id).find('p.state').text('上传出错');
    });

    uploader.on('uploadComplete', function (file) {
        $('#' + file.id).find('.progress').fadeOut();
    });

    uploader.on('all', function (type) {
        if (type === 'startUpload') {
            state = 'uploading';
        } else if (type === 'stopUpload') {
            state = 'paused';
        } else if (type === 'uploadFinished') {
            state = 'done';
        }

        if (state === 'uploading') {
            $btn.text('暂停上传');
        } else {
            $btn.text('开始上传');
        }
    });

    $btn.on('click', function () {
        if (state === 'uploading') {
            uploader.stop(true);
        } else {
            uploader.upload();
        }
    });
    
});