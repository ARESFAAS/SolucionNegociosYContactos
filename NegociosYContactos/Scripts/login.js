$(function () {
    loginUser();
});

function loginUser() {
    $('#btnLogin').click(function () {
        $.ajax({
            url: 'LoginUser',
            //dataType: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({
                Email: '', IdentificationType: '',
                IdentificationNumber: '', Password: $('#txtPassword').val(),
                AccessFailedCount: '0', UserName: $('#txtUserName').val(), Phone: ''
            }),
            async: true,
            processData: false,
            cache: false,
            success: function (data) {
                $('#dialog-message').html(data.Message).dialog('open');
            },
            error: function (xhr, errorText) {
                alert('En este momento no podemos procesar tu solicitud, por favor intenta más tarde.');
            }
        });
    });
    
}