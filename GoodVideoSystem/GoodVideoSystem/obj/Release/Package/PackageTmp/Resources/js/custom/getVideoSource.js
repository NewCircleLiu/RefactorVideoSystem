jQuery(document).ready(function () {
    var info = jQuery("#info").val();
    jQuery("#video_container").load("/Home/GetVideo?info=" + info, function () {
        $('#video').bind('contextmenu', function () { return false; });

        var myPlayer = videojs('video');
        videojs("video").ready(function () {
            var myPlayer = this;
            myPlayer.play();
        });

        var screen_width = screen.availWidth;
        if (screen_width >= 1024) {
            myPlayer.height(500);
        }
        else if (screen_width >= 600) {
            myPlayer.height(400);
        }
        else {
            myPlayer.height(250);
        }
    }); 
})


