﻿var clientProvider = 'Local';
var logout = '';
var googleUser = {};
$(function () {
    loginUser();
    loginFacebook();
    loginGoogle();
    loginMessage();
    forgotPassword();
    disableRefresh();
});

function loginUser() {
    $('#btnLogin').click(function () {
        $.ajax({
            url: getHost() + 'Account/LoginUser',
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({
                Email: '', IdentificationType: '',
                IdentificationNumber: '', Password: $('#txtPassword').val(),
                AccessFailedCount: '0', UserName: $('#txtUserName').val(), Phone: '', LoginProvider: 'Local'
            }),
            async: true,
            processData: false,
            cache: false,
            success: function (data) {
                if (!data.Authenticated) {
                    logout = 'Local';
                    clientProvider = 'Local';
                    $('#login-message-logout').html(data.Message).dialog('open');
                }
                else {
                    urlToRedirect = getHost() + 'Admin/Index';
                    $("#dialog-message-redirect").html(data.Message).dialog('open');
                }
            },
            error: function (xhr, errorText) {
                $("#dialog-message").html('En este momento no podemos procesar tu solicitud, por favor intenta más tarde.')
                    .dialog('open');
            }
        });
    });
}

// This is called with the results from from FB.getLoginStatus().
function statusChangeCallback(response) {
    console.log('statusChangeCallback');
    console.log(response);
    // The response object is returned with a status field that lets the
    // app know the current login status of the person.
    // Full docs on the response object can be found in the documentation
    // for FB.getLoginStatus().
    if (response.status === 'connected') {
        // Logged into your app and Facebook.
        getFacebookData();
    } else if (response.status === 'not_authorized') {
        // The person is logged into Facebook, but not your app.
    } else {
        // The person is not logged into Facebook, so we're not sure if
        // they are logged into this app or not.
    }
}

// This function is called when someone finishes with the Login
// Button.  See the onlogin handler attached to it in the sample
// code below.
function checkLoginState() {
    FB.getLoginStatus(function (response) {
        statusChangeCallback(response);
    });
}

window.fbAsyncInit = function () {
    FB.init({
        appId: '878926252226269',
        cookie: true,  // enable cookies to allow the server to access
        // the session
        xfbml: true,  // parse social plugins on this page
        version: 'v2.5' // use version 2.2
    });

    // Now that we've initialized the JavaScript SDK, we call
    // FB.getLoginStatus().  This function gets the state of the
    // person visiting this page and can return one of three states to
    // the callback you provide.  They can be:
    //
    // 1. Logged into your app ('connected')
    // 2. Logged into Facebook, but not your app ('not_authorized')
    // 3. Not logged into Facebook and can't tell if they are logged into
    //    your app or not.
    //
    // These three cases are handled in the callback function.

    //FB.getLoginStatus(function (response) {
    //    statusChangeCallback(response);
    //});
};

//// Load the SDK asynchronously
//(function (d, s, id) {
//    var js, fjs = d.getElementsByTagName(s)[0];
//    if (d.getElementById(id)) return;
//    js = d.createElement(s); js.id = id;
//    js.src = "//connect.facebook.net/es_ES/sdk.js";
//    fjs.parentNode.insertBefore(js, fjs);
//}(document, 'script', 'facebook-jssdk'));

// Here we run a very simple test of the Graph API after login is
// successful.  See statusChangeCallback() for when this call is made.
function getFacebookData() {
    console.log('Welcome!  Fetching your information.... ');
    FB.api('/me', {fields: 'name,email,picture'}, function (response) {
        console.log(JSON.stringify(response));

        $.ajax({
            url: getHost() + 'Account/LoginUserExternalProvider',
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({
                Email: response.email, IdentificationType: '',
                IdentificationNumber: '', Password: '',
                AccessFailedCount: '0', UserName: response.name, Phone: '', LoginProvider: 'Facebook',
                UserImage: response.picture.data.url
            }),
            async: true,
            processData: false,
            cache: false,
            success: function (data) {
                if (data.CompleteUserData && data.Authenticated) {
                    window.location.href = getHost() + 'Account/Edit';                   
                } else {
                    if (!data.Authenticated) {
                        logout = 'Facebook';
                        clientProvider = 'Facebook';
                        $('#login-message-logout').html(data.Message).dialog('open');
                    }
                    else {
                        urlToRedirect = getHost() + 'Admin/Index';
                        $("#dialog-message-redirect").html(data.Message).dialog('open');
                    }
                }
            },
            error: function (xhr, errorText) {
                signOutFacebookWithOutStatus();
            }
        });
    });
}

function loginMessage() {
    $("#login-message-logout").dialog({
        modal: true,
        autoOpen: false,
        buttons: {
            Ok: function () {
                if (clientProvider == 'Local' && logout == 'Local') {
                    signOutLocal();
                }
                if (clientProvider == 'Google' && logout == 'Google') {
                    signOutGoogle();
                }
                if (clientProvider == 'Facebook' && logout == 'Facebook') {
                    signOutFacebookWithOutStatus();
                }
                $(this).dialog("close");
                location.reload();
            }
        },
        close: function () {
            if (clientProvider == 'Local' && logout == 'Local') {
                signOutLocal();
            }
            if (clientProvider == 'Google' && logout == 'Google') {
                signOutGoogle();
            }
            if (clientProvider == 'Facebook' && logout == 'Facebook') {
                signOutFacebookWithOutStatus();
            }
            $(this).dialog("close");
            location.reload();
        }
    });
}

function loginFacebook() {
    $('#btnLoginFacebook').click(function () {
        FB.login(function (response) {
            // Handle the response object, like in statusChangeCallback() in our demo
            // code.
            statusChangeCallback(response);
        });
    });
}

var loginGoogle = function () {
    gapi.load('auth2', function () {
        // Retrieve the singleton for the GoogleAuth library and set up the client.
        // auth2 = gapi.auth2.init({
            auth2 = gapi.auth2.getAuthInstance({
            client_id: '575694803506-gku0hudru360d2s5p1rdlesp72pg2msm.apps.googleusercontent.com',
            cookiepolicy: 'single_host_origin',
            // Request scopes in addition to 'profile' and 'email'
            //scope: 'additional_scope'
        });
        attachSignin(document.getElementById('btnLoginGoogle'));
    });
};

function attachSignin(element) {
    auth2.attachClickHandler(element, {},
        function (googleUser) {
            var profile = googleUser.getBasicProfile();
            console.log('ID: ' + profile.getId()); // Do not send to your backend! Use an ID token instead.
            
            $('#divUserName').html(profile.getName());
            $('#imgUser').attr('src', profile.getImageUrl());

            $.ajax({
                url: getHost() + 'Account/LoginUserExternalProvider',
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({
                    Email: profile.getEmail(), IdentificationType: '',
                    IdentificationNumber: '', Password: '',
                    AccessFailedCount: '0', UserName: profile.getName(), Phone: '', LoginProvider: 'Google',
                    UserImage: profile.getImageUrl()
                }),
                async: true,
                processData: false,
                cache: false,
                success: function (data) {
                    if (data.CompleteUserData && data.Authenticated) {
                        window.location.href = getHost() + 'Account/Edit';
                    } else {
                        if (!data.Authenticated) {
                            logout = 'Google';
                            clientProvider = 'Google';
                            $('#login-message-logout').html(data.Message).dialog('open');
                        }
                        else {
                            urlToRedirect = getHost() + 'Admin/Index';
                            $("#dialog-message-redirect").html(data.Message).dialog('open');
                        }
                    }
                },
                error: function (xhr, errorText) {
                    signOutGoogle();
                }
            });
        },
        function (error) {
            $("#dialog-message").html(JSON.stringify(error, undefined, 2))
                .dialog('open');
        });
}

function forgotPassword()
{
    $('#frmForgotPassword').validate({ // initialize plugin
        rules: {
            txtForgotEmail: {
                required: true,
                email: true
            }
        },
        messages: {
            txtForgotEmail: {
                required: "Necesitamos tu correo <br/> electrónico para contactarnos <br/> contigo",
                email: "Tu correo electrónico <br/> debe tener el formato <br> nombre@dominio.com"
            }
        },
        // your rules & options,
        submitHandler: function (form) {
            // your ajax would go here
            sendPassword();
            return false;  // blocks regular submit since you have ajax
        },
        showErrors: function (errorMap, errorList) {
            this.defaultShowErrors();
        },
        errorContainer: "#divSummaryForgotPasswordErrors",
        errorLabelContainer: "#divSummaryForgotPasswordErrors ul",
        wrapper: "li",
        errorClass: "authError",
        highlight: function (element, required) {
            $(element).fadeOut(function () {
                $(element).fadeIn();
                $(element).css('border', '2px solid #FDADAF');
            });
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).css('border', '1px solid #CCC');
        }
    });
    $('#divForgotPassword').dialog({
        width: 'auto', // overcomes width:'auto' and maxWidth bug
        maxWidth: 800,
        height: 'auto',
        modal: true,
        fluid: true, //new option
        resizable: false,
        autoOpen: false,
    });
    $('#lnkForgotPassword').click(function () {
        $('#divForgotPassword').dialog('open');
    });
}

function sendPassword() {
    $.ajax({
        url: getHost() + 'Account/SendPassword',
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({
            email: $('#txtForgotPassword').val()
        }),
        async: true,
        processData: false,
        cache: false,
        success: function (data) {
            $('#login-message-logout').html(data.Message).dialog('open');
        },
        error: function (xhr, errorText) {
            $("#dialog-message").html('En este momento no podemos procesar tu solicitud, por favor intenta más tarde.')
                .dialog('open');
        }
    });
}