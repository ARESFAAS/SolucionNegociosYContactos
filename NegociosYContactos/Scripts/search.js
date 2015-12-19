window.onload = function () {
    economicBannerLoad();
    newsBannerLoad();
    itemSearchAnimate();
}

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
    var param = $('#txtSearch').val();
    window.location.href = url.replace('[*]', encodeURIComponent(param));
    //$.ajax({
    //    url: url,
    //    dataType: "json",
    //    type: "POST",
    //    contentType: 'application/json; charset=utf-8',
    //    data: JSON.stringify({ searchWord: param}),
    //    async: true,
    //    processData: false,
    //    cache: false,
    //    success: function (data) {
    //        alert(data);
    //    },
    //    error: function (xhr) {
    //        alert('error');
    //    }
    //})
}