$(function () {
    animateTopBar();
    modalMessage();
    loadApiGoogle();
    loadApiFacebook();
    showSocial();
    //showNavBar();
});

function animateTopBar() {
    $('#divNavBarTop').click(function (e) {
        if ($('#divNavBar').is(":visible")) {
            $('#divNavBar').hide("fade");
        }
        else {
            $('#divNavBar').show("fade");
        }
    });
}

function modalMessage() {
    $("#dialog-message").dialog({
        modal: true,
        autoOpen: false,
        buttons: {
            Ok: function () {
                $(this).dialog("close");
                location.reload();
            }
        }
    });
}
    
function signOutGoogle() {

    var auth2 = gapi.auth2.getAuthInstance();
    auth2.signOut().then(function () {
        console.log('User Google signed out.');
        $.ajax({
            url: getHost() + 'Account/Logout',
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            async: true,
            processData: false,
            cache: false,
            success: function (data) {
                document.location.href = getHost() + 'Home/Index';
            },
            error: function (xhr, errorText) {
                location.reload();
                alert('En este momento no podemos procesar tu solicitud, por favor intenta más tarde.');
            }
        });
    });
}

function signOutFacebook() {
    FB.logout(function (response) {
        console.log('User Facebook signed out.');
        $.ajax({
            url: getHost() + 'Account/Logout',
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            async: true,
            processData: false,
            cache: false,
            success: function (data) {
                document.location.href = getHost() + 'Home/Index';
            },
            error: function (xhr, errorText) {
                location.reload();
                alert('En este momento no podemos procesar tu solicitud, por favor intenta más tarde.');
            }
        });
    });
}

function signOutLocal() {
    $.ajax({
        url: getHost() + 'Account/Logout',
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        async: true,
        processData: false,
        cache: false,
        success: function (data) {
            document.location.href = getHost() + 'Home/Index';
        },
        error: function (xhr, errorText) {
            location.reload();
            alert('En este momento no podemos procesar tu solicitud, por favor intenta más tarde.');
        }
    });
}

function loadApiGoogle() {
    gapi.load('auth2', function () {
        auth2 = gapi.auth2.init({
            client_id: '575694803506-gku0hudru360d2s5p1rdlesp72pg2msm.apps.googleusercontent.com',
            cookiepolicy: 'single_host_origin'
        });
    });
}

function loadApiFacebook() {
    window.fbAsyncInit = function () {
        FB.init({
            appId: '878926252226269',
            cookie: true,  // enable cookies to allow the server to access
            // the session
            xfbml: true,  // parse social plugins on this page
            version: 'v2.5' // use version 2.2
        });
    };

    // Load the SDK asynchronously
    (function (d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) return;
        js = d.createElement(s); js.id = id;
        js.src = "//connect.facebook.net/es_ES/sdk.js";
        fjs.parentNode.insertBefore(js, fjs);
    }(document, 'script', 'facebook-jssdk'));
}

function showSocial() {
    if ($(window).width() > 800) {
        $('body').mousemove(function ()
        {
            if (event.pageX <= 70) {
                $('.social').show(1000);
            }
            else
            {
                $('.social').hide(1000);
            }
        });
    }
    else
    {
        $('.social').show(1000);
    }
}

function showNavBar() {
    if ($(window).width() > 800) {
        $('body').mousemove(function () {
            if (event.pageY <= 70) {
                $('#divNavBar').show('blind',2000);
            }
            else {
                $('#divNavBar').hide('blind',2000);
            }
        });
    }
    else {
        $('#divNavBar').show('blind',2000);
    }
}

function getHost() {
    return window.location.protocol + "\\\\" + window.location.host + "\\";
}