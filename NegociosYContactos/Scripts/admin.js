$(function () {
    $('#divNavBar').hide();
    fileUploadConfig();
    dialogConfig();
    uploadLogo();
    colorPickerConfig();
    enableStep();
});

function fileUploadConfig()
{
    // Initialize the jQuery File Upload widget:
    $('#fileupload').fileupload({
        url: '/Admin/UploadFiles',
        // Enable image resizing, except for Android and Opera,
        // which actually support image resizing, but fail to
        // send Blob objects via XHR requests:
        disableImageResize: /Android(?!.*Chrome)|Opera/
            .test(window.navigator.userAgent),
        maxFileSize: 999000,
        acceptFileTypes: /(\.|\/)(gif|jpe?g|png)$/i,
        imageMaxWidth: 200,
        imageMaxHeight: 200,
        imageCrop: false, // Force cropped images
        maxNumberOfFiles: 9,
        done: function (e, data) {
            $('#divPaso2').css('background-color', 'yellowgreen');
        }
    });
}

function dialogConfig()
{
    $('#divCargaNombre').dialog({
        width: 'auto', // overcomes width:'auto' and maxWidth bug
        maxWidth: 800,
        height: 'auto',
        modal: true,
        fluid: true, //new option
        resizable: false,
        autoOpen: false,
        position: { my: "center bottom", at: "center bottom", of: '#divNavBar' }
    });

    $("#divCargaLogo").dialog({
        width: 'auto', // overcomes width:'auto' and maxWidth bug
        maxWidth: 800,
        height: 'auto',
        modal: true,
        fluid: true, //new option
        resizable: false,
        autoOpen: false,
        position: { my: "center bottom", at: "center bottom", of: '#divNavBar' }
    });

    $("#divCargaFotos").dialog({
        width: 'auto', // overcomes width:'auto' and maxWidth bug
        maxWidth: 800,
        height: 'auto',
        modal: true,
        fluid: true, //new option
        resizable: false,
        autoOpen: false,
        position: { my: "center bottom", at: "center bottom", of: '#divNavBar' }
    });

    $("#divCambiaColor").dialog({
        width: 'auto', // overcomes width:'auto' and maxWidth bug
        maxWidth: 800,
        height: 'auto',
        modal: true,
        fluid: true, //new option
        resizable: false,
        autoOpen: false,
        position: { my: "center bottom", at: "center bottom", of: '#divNavBar' }
    });

    $('#divAgregaInformacion').dialog({
        width: 'auto', // overcomes width:'auto' and maxWidth bug
        maxWidth: 800,
        height: 'auto',
        modal: true,
        fluid: true, //new option
        resizable: false,
        autoOpen: false,
        position: { my: "center bottom", at: "center bottom", of: '#divNavBar' }
    });

    $('#divPaso0').click(function () {
        $('#divCargaNombre').dialog('open');
    });

    $('#divPaso1').click(function () {
        $('#divCargaLogo').dialog('open');
    });

    $('#divPaso2').click(function () {
        $('#divCargaFotos').dialog('open');
    });

    $('#divPaso3').click(function () {
        $('#divCambiaColor').dialog('open');
    });

    $('#divPaso4').click(function () {
        $('#divAgregaInformacion').dialog('open');
    });

    // on window resize run function
    $(window).resize(function () {
        fluidDialog();
    });

    // catch dialog if opened within a viewport smaller than the dialog width
    $(document).on("dialogopen", ".ui-dialog", function (event, ui) {
        fluidDialog();
    }); 
}

function fluidDialog() {
    var $visible = $(".ui-dialog:visible");
    // each open dialog
    $visible.each(function () {
        var $this = $(this);
        var dialog = $this.find(".ui-dialog-content").data("ui-dialog");
        // if fluid option == true
        if (dialog.options.fluid) {
            var wWidth = $(window).width();
            // check window width against dialog width
            if (wWidth < (parseInt(dialog.options.maxWidth) + 50)) {
                // keep dialog from filling entire screen
                $this.css("max-width", "90%");
            } else {
                // fix maxWidth bug
                $this.css("max-width", dialog.options.maxWidth + "px");
            }
            //reposition dialog
            dialog.option("position", dialog.options.position);
        }
    });

}

function uploadLogo() {
    $('#divCargaLogo').fileupload({
        dataType: 'json',
        url: '/Admin/UploadLogo',
        autoUpload: true,
        // Enable image resizing, except for Android and Opera,
        // which actually support image resizing, but fail to
        // send Blob objects via XHR requests:
        disableImageResize: /Android(?!.*Chrome)|Opera/
            .test(window.navigator.userAgent),
        maxFileSize: 999000,
        acceptFileTypes: /(\.|\/)(gif|jpe?g|png)$/i,
        imageMaxWidth: 400,
        imageMaxHeight: 400,
        imageCrop: false, // Force cropped images
        maxNumberOfFiles: 1,
        done: function (e, data) {
            $('.file_name').html(data.result.name);
            $('.file_type').html(data.result.type);
            $('.file_size').html(data.result.size);
            $('#divPaso1').css('background-color', 'yellowgreen');
        }
    }).on('fileuploadprogressall', function (e, data) {
        var progress = parseInt(data.loaded / data.total * 100, 10);
        $('.progress .progress-bar').css('width', progress + '%');
    });
}

function colorPickerConfig() {
    $('#divCambiaColor').colorpicker({
        customClass: 'colorpicker-2x',
        sliders: {
            saturation:
                {
                maxLeft: 200,
                maxTop: 200
                },
            hue:
            {
                maxTop: 200
            },
            alpha:
            {
                maxTop: 200
            }
        }
    }).on('changeColor.colorpicker', function (event) {
        $('#divCambiaColor').css('backgroundColor', event.color.toHex());
        $('#divPaso3').css('background-color', 'yellowgreen');
    });
}

function enableStep() {
    $('#txtNombre').change(function () {
        if ($('#txtNombre').val() != '') {
            $('#divPaso0').css('background-color', 'yellowgreen');
        }
        else {
            $('#divPaso0').css('background-color', 'gainsboro');
        }
    });
}