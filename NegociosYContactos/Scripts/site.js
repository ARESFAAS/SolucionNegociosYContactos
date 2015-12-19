window.onload = function () {
    animateTopBar();
}
function animateTopBar()
{    
    $('#divNavBarTop').click(function (e) {
        if ($('#divNavBar').is(":visible")) {
            $('#divNavBar').hide("fade");
        }
        else {
            $('#divNavBar').show("fade");
        }
    });    
}
