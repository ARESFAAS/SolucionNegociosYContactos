var validPass = false;
$(function () {
    configFormValidate();
    validatePassword();
});

function configFormValidate() {
    $('#frmRegister').validate({ // initialize plugin
        rules: {
            userName: "required",
            identificationNumber: "required",
            password: {
                required: true,
                minlength: 4,
                maxlength: 6
            },
            phone: "required",
            email: {
                required: true,
                email: true
            }
        },
        messages: {
            userName: "Especifica tu nombre",
            identificationNumber: "Necesitamos tu número de identificación",
            password: {
                required: "Asigna un password que recuerdes facilmente",
                minlength: jQuery.validator.format("Al menos {0} caracteres son requeridos!"),
                maxlength: jQuery.validator.format("Hasta {0} caracteres son permitidos!")
            },
            phone: "Necesitamos un número de teléfono para contactarnos contigo",
            email: {
                required: "Necesitamos tu email para contactarnos contigo",
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
        errorClass: "authError"
    });
}

function saveUser()
{
    $.ajax({
        url: 'Account/SaveUser',
        dataType: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({
            Email: $('#email').val(), IdentificationType: $('#cmbIdentificationType').val(),
            IdentificationNumber: $('#identificationNumber').val(), Password: $('#txtPassword1').val(),
            AccessFailedCount: '0', UserName: $('#userName').val(), Phone: $('#phone').val()
        }),
        async: true,
        processData: false,
        cache: false,
        success: function (data) {
            alert(data);
        },
        error: function (xhr) {
            alert('error');
        }
    })
}

function validatePassword()
{
    $('#txtPassword1').change(function () {
        if ($('#txtPassword1').val() == '') {
            $('#txtPassword2').val('');
        }
    });
    $('#txtPassword2').change(function () {
        if ($('#txtPassword1').val() == '')
        {
            $('#txtPassword2').val('');
        }
        {
            if ($('#txtPassword2').val() != $('#txtPassword1').val()) {
                validPass = false;
                $('#divValidarPassword').show('fade');
            }
            else {
                validPass = true;
                $('#divValidarPassword').hide('fade');
            }
        }
    });
}