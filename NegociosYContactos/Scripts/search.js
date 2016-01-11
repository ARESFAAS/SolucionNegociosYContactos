$(function () {
    economicBannerLoad();
    newsBannerLoad();
    itemSearchAnimate();
    loadAutoComplete();
});

function economicBannerLoad() {
    $('#divEconomic').cycle({
        fx: 'scrollLeft' // choose your transition type, ex: fade, scrollUp, shuffle, etc...
    });
}

function newsBannerLoad() {
    $('#divNews').cycle({
        fx: 'scrollLeft' // choose your transition type, ex: fade, scrollUp, shuffle, etc...
    });
}

function itemSearchAnimate()
{
    $('.itemTopSearch').mouseover(function () {
        $(this).css('position', 'relative')
            .animate({                
                top: '-=70'
            },
            {
                duration: 2000,
                specialEasing: {
                    width: "linear",
                    height: "easeOutBounce"
                },
                complete: function () { $('#txtSearch').val($(this).html()); $(this).animate({width: 0, height: 0}).remove(); }
            });
    });
}

function goRoom(url) {
    var param = $('#txtSearch').val().trim().replace(/\s/g, "+");
    window.location.href = url.replace('[*]', param);
}

function loadAutoComplete() {
    $.ajax({
        url: getHost() + 'Search/GetAutoComplete',
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        async: true,
        processData: false,
        cache: false,
        success: function (data) {
            $("#txtSearch").catcomplete({
                delay: 0,
                source: data.DataAutoComplete
            });
        },
        error: function (xhr) {
            
        }
    });
}