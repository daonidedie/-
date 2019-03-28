var jq = jQuery.noConflict(); 

jq(document).ready(function () {
    jq("#menu2 li").prepend("<span></span>");

    jq("#menu2 li").each(function () {
        var linkText = jq(this).find("a").html();
        jq(this).find("span").show().html(linkText);
    });

    jq("#menu2 li").hover(function () {
        jq(this).find("span").stop().animate({
            marginTop: "-40"
        }, 250);
    }, function () { //On hover out...
        jq(this).find("span").stop().animate({
            marginTop: "0"
        }, 250);
    });
});


