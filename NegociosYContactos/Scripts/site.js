window.onload = function () {
    animateTopBar();
    modalMessage();
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

function modalMessage()
{
    $("#dialog-message").dialog({
        modal: true,
        autoOpen:false,
        buttons: {
            Ok: function () {
                $(this).dialog("close");
                location.reload();
            }
        }
    });
}
