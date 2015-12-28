var validPass = false;
$(function () {
    $('#divValidarPassword').hide();
    configFormValidate();
    configFormUpdateUserValidate();
    validatePassword();
    getPartialTerms();
});

function configFormValidate() {
    $('#frmRegister').validate({ // initialize plugin
        rules: {
            identificationNumber: "required",
            password: {
                required: true,
                minlength: 4,
                maxlength: 6
            },
            userName: {
                required: true,
                minlength: 4,
                maxlength: 20
            },
            phone: "required",
            email: {
                required: true,
                email: true
            }
        },
        messages: {
            identificationNumber: "Necesitamos tu número de identificación",
            userName: {
                required: "Especifica tu nombre",
                minlength: jQuery.validator.format("Al menos {0} caracteres son requeridos para el nombre!"),
                maxlength: jQuery.validator.format("Hasta {0} caracteres son permitidos para el nombre!")
            },
            password: {
                required: "Asigna una contraseña que recuerdes facilmente",
                minlength: jQuery.validator.format("Al menos {0} caracteres son requeridos para la contraseña!"),
                maxlength: jQuery.validator.format("Hasta {0} caracteres son permitidos en la contraseña!")
            },
            phone: "Necesitamos un número de teléfono para contactarnos contigo",
            email: {
                required: "Necesitamos tu correo electrónico para contactarnos contigo",
                email: "Tu correo electrónico debe tener el formato nombre@dominio.com"
            }
        }
        ,
        // your rules & options,
        submitHandler: function (form) {
            // your ajax would go here
            if (validPass) {
                saveUser();
            }
            return false;  // blocks regular submit since you have ajax
        },
        showErrors: function (errorMap, errorList) {
            this.defaultShowErrors();
        },
        errorContainer: "#divSummaryErrors",
        errorLabelContainer: "#divSummaryErrors ul",
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
}

function saveUser() {
   $.ajax({
            url: 'http://localhost:59927/Account/SaveUser',
            dataType: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({
                Email: $('#email').val(), IdentificationType: $('#cmbIdentificationType').val(),
                IdentificationNumber: $('#identificationNumber').val(), Password: $('#txtPassword1').val(),
                AccessFailedCount: '0', UserName: $('#userName').val(), Phone: $('#phone').val(), LoginProvider: 'Local'
            }),
            async: true,
            processData: false,
            cache: false,
            success: function (data) {
                $("#dialog-message").html(data.Message).dialog('open');
            },
            error: function (xhr, errorText) {
                alert('En este momento no podemos procesar tu solicitud, por favor intenta más tarde.');
            }
        });
}

function validatePassword() {

    if ($('#txtPassword2').val() != '' && $('#txtPassword1').val() != '') {
        if ($('#txtPassword2').val() == $('#txtPassword1').val()) {
            validPass = true;
            $('#divValidarPassword').hide('fade');
        }
    }

    $('#txtPassword1').change(function () {
        if ($('#txtPassword1').val() == '') {
            $('#txtPassword2').val('');
        }
        if ($('#txtPassword2').val() != $('#txtPassword1').val()) {
            validPass = false;
            $('#divValidarPassword').show('fade');
        }
        else {
            validPass = true;
            $('#divValidarPassword').hide('fade');
        }
    });
    $('#txtPassword2').change(function () {
        if ($('#txtPassword1').val() == '') {
            $('#txtPassword2').val('');
        }
        if ($('#txtPassword2').val() != $('#txtPassword1').val()) {
            validPass = false;
            $('#divValidarPassword').show('fade');
        }
        else {
            validPass = true;
            $('#divValidarPassword').hide('fade');
        }
    });
}

function configFormUpdateUserValidate() {
    $('#frmEditUser').validate({ // initialize plugin
        rules: {
            identificationNumber: "required",
            password: {
                required: true,
                minlength: 4,
                maxlength: 6
            },
            userName: {
                required: true,
                minlength: 4,
                maxlength: 20
            },
            phone: "required",
            email: {
                required: true,
                email: true
            }
        },
        messages: {
            identificationNumber: "Necesitamos tu número de identificación",
            userName: {
                required: "Especifica tu nombre",
                minlength: jQuery.validator.format("Al menos {0} caracteres son requeridos para el nombre!"),
                maxlength: jQuery.validator.format("Hasta {0} caracteres son permitidos para el nombre!")
            },
            password: {
                required: "Asigna una contraseña que recuerdes facilmente",
                minlength: jQuery.validator.format("Al menos {0} caracteres son requeridos para la contraseña!"),
                maxlength: jQuery.validator.format("Hasta {0} caracteres son permitidos en la contraseña!")
            },
            phone: "Necesitamos un número de teléfono para contactarnos contigo",
            email: {
                required: "Necesitamos tu correo electrónico para contactarnos contigo",
                email: "Tu correo electrónico debe tener el formato nombre@dominio.com"
            }
        }
        ,
        // your rules & options,
        submitHandler: function (form) {
            // your ajax would go here
            if (validPass) {
                EditUser();
            }
            return false;  // blocks regular submit since you have ajax
        },
        showErrors: function (errorMap, errorList) {
            this.defaultShowErrors();
        },
        errorContainer: "#divSummaryErrors",
        errorLabelContainer: "#divSummaryErrors ul",
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
}

function EditUser() {
    $.ajax({
        url: 'http://localhost:59927/Account/EditUser',
        dataType: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({
            Email: $('#email').val(), IdentificationType: $('#cmbIdentificationType').val(),
            IdentificationNumber: $('#identificationNumber').val(), Password: $('#txtPassword1').val(),
            AccessFailedCount: '0', UserName: $('#userName').val(), Phone: $('#phone').val(), LoginProvider: 'Local'
        }),
        async: true,
        processData: false,
        cache: false,
        success: function (data) {
            $("#dialog-message").html(data.Message).dialog('open');
        },
        error: function (xhr, errorText) {
            alert('En este momento no podemos procesar tu solicitud, por favor intenta más tarde.');
        }
    });
}

function getPartialTerms()
{
    var divWidth = 0;
    var divHeight = 0;

    if ($(window).width() <= 760)
    {
        divWidth = 400;
        divHeight = 400;
    }
    else {
        divWidth = 600;
        divHeight = 600;
    }
    $('#divTerms').dialog({
        autoOpen: false,
        height: divHeight,
        width: divWidth,        
        modal: true,
        buttons: {
            Ok: function() {
                $('#divTerms').dialog('close');
                $('#chkTerms').attr('checked', true);
            }
        },
        close: function() {
            $('#divTerms').dialog('close');
        }
    });
    $('#lnkTerm').on('click', function (evt) {
        evt.preventDefault();
        evt.stopPropagation();
        var url = 'http://localhost:59927/Home/PartialTerms';
        $.get(url, function (data) {
            $('#divTerms').html(data);
            $('#divTerms').dialog('open');
            $('#lnkPrivacy').attr('href', '#');
            getPartialPrivacyPolicy();
        });
    });
}

function getPartialPrivacyPolicy() {
    var divWidth = 0;
    var divHeight = 0;

    if ($(window).width() <= 760) {
        divWidth = 400;
        divHeight = 400;
    }
    else {
        divWidth = 600;
        divHeight = 600;
    }
    $('#divPrivacy').dialog({
        autoOpen: false,
        height: divHeight,
        width: divWidth,
        modal: true,
        buttons: {
            Ok: function () {
                $('#divPrivacy').dialog('close');
            }
        },
        close: function () {
            $('#divPrivacy').dialog('close');
        }
    });
    $('#lnkPrivacy').on('click', function (evt) {
        evt.preventDefault();
        evt.stopPropagation();
        var url = 'http://localhost:59927/Home/PartialPrivacyPolicy';
        $.get(url, function (data) {
            $('#divPrivacy').html(data);
            $('#divPrivacy').dialog('open');
        });
    });
}