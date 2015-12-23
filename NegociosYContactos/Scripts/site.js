window.onload = function () {
    animateTopBar();
    modalMessage();

    gapi.load('auth2', function () {
        auth2 = gapi.auth2.init({
            client_id: '575694803506-gku0hudru360d2s5p1rdlesp72pg2msm.apps.googleusercontent.com',
            cookiepolicy: 'single_host_origin'
        });
    });

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

function signOutGoogle() {
    
    var auth2 = gapi.auth2.getAuthInstance();
    auth2.signOut().then(function () {
        console.log('User Google signed out.');
        $.ajax({
            url: 'http://localhost:59927/Account/Logout',
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            async: true,
            processData: false,
            cache: false,
            success: function (data) {
                document.location.href = 'http://localhost:59927/Home/Index';
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
            url: 'http://localhost:59927/Account/Logout',
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            async: true,
            processData: false,
            cache: false,
            success: function (data) {
                document.location.href = 'http://localhost:59927/Home/Index';
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
        url: 'http://localhost:59927/Account/Logout',
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        async: true,
        processData: false,
        cache: false,
        success: function (data) {
            document.location.href = 'http://localhost:59927/Home/Index';
        },
        error: function (xhr, errorText) {
            location.reload();
            alert('En este momento no podemos procesar tu solicitud, por favor intenta más tarde.');
        }
    });
}