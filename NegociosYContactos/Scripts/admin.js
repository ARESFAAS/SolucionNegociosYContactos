﻿$(function () {
    fileUploadConfig();
    dialogConfig();
    uploadLogo();
    colorPickerConfig();
    enableStep();
    enableContextMenu();
    disableRefresh();
    selectableConfig();
});

function fileUploadConfig() {
    // Initialize the jQuery File Upload widget:
    $('#fileupload').fileupload({
        url: getHost() + 'Admin/UploadFiles',
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
            if (data.result.files[0].name == 'limitSize') {
                $("table tbody.files").empty();
                $("#dialog-message").html('¡¡Has excedido el numero de imágenes permitidas en tu cuenta, para poder agregar otra, debes primero borrar una de las que ya tienes')
                    .dialog('open');
            }
            else {
                var template = '<div style="float:left;margin: 4em 4em 4em 4em;text-align:center;"><img id={0} class="context-menu-one" src="{1}" /></div>';
                var html = '';
                html = template.format(data.result.files[0].id, data.result.files[0].url);
                $('#divActualPhotos').append(html);
                $("table tbody.files").empty();
                $('#divCargaFotos').parent().position({
                    my: 'center',
                    at: 'center',
                    of: window,
                    collision: 'fit'
                });
                $('#divPaso2').css('background-color', 'yellowgreen');
            }
        }
    });
}

function dialogConfig() {
    $('#divCargaNombre').dialog({
        width: 'auto', // overcomes width:'auto' and maxWidth bug
        maxWidth: 800,
        height: 'auto',
        modal: true,
        fluid: true, //new option
        resizable: false,
        autoOpen: false,
        position: {
            my: 'center',
            at: 'center',
            of: window,
            collision: 'fit'
        },
        buttons: {
            "Aceptar": function () {
                $.ajax({
                    url: getHost() + 'Admin/UpdateNameTemp',
                    type: "POST",
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify({
                        newName: $('#txtNombre').val()
                    }),
                    async: true,
                    processData: false,
                    cache: false,
                    success: function (data) {
                    },
                    error: function (xhr, errorText) {
                        $("#dialog-message").html('En este momento no podemos procesar tu solicitud, por favor intenta más tarde.')
                            .dialog('open');
                    }
                });
                $('#divPaso0').css('background-color', 'yellowgreen');
                $(this).dialog("close");
            }
        }
    });

    $("#divCargaLogo").dialog({
        width: 'auto', // overcomes width:'auto' and maxWidth bug
        maxWidth: 1000,
        height: 'auto',
        maxHeight: 600,
        modal: true,
        fluid: true, //new option
        resizable: false,
        autoOpen: false,
        position: {
            my: 'center',
            at: 'center',
            of: window,
            collision: 'fit'
        },
        buttons: {
            "Aceptar": function () {
                $('#divPaso1').css('background-color', 'yellowgreen');
                $(this).dialog("close");
            }
        }
    });

    $("#divCargaFotos").dialog({
        width: 'auto', // overcomes width:'auto' and maxWidth bug
        maxWidth: 900,
        height: 'auto',
        maxHeight: 600,
        modal: true,
        fluid: true, //new option
        resizable: false,
        autoOpen: false,
        position: {
            my: 'center',
            at: 'center',
            of: window,
            collision: 'fit'
        },
        buttons: {
            "Aceptar": function () {
                $('#divPaso2').css('background-color', 'yellowgreen');
                $(this).dialog("close");
            }
        }
    });

    $("#divCambiaColor").dialog({
        width: 'auto', // overcomes width:'auto' and maxWidth bug
        maxWidth: 800,
        height: 'auto',
        modal: true,
        fluid: true, //new option
        resizable: false,
        autoOpen: false,
        position: {
            my: 'center',
            at: 'center',
            of: window,
            collision: 'fit'
        },
        buttons: {
            "Aceptar": function () {
                $.ajax({
                    url: getHost() + 'Admin/UpdateStyleTemp',
                    type: "POST",
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify({
                        newStyle: $('#divCambiaColor').attr('style')
                    }),
                    async: true,
                    processData: false,
                    cache: false,
                    success: function (data) { },
                    error: function (xhr, errorText) {
                        $("#dialog-message").html('En este momento no podemos procesar tu solicitud, por favor intenta más tarde.')
                            .dialog('open');
                    }
                });
                $('#divPaso3').css('background-color', 'yellowgreen');
                $(this).dialog("close");
            }
        }
    });

    $('#divAgregaInformacion').dialog({
        width: 'auto', // overcomes width:'auto' and maxWidth bug
        maxWidth: 900,
        height: 'auto',
        maxHeight: 900,
        modal: true,
        fluid: true, //new option
        resizable: false,
        autoOpen: false,
        position: {
            my: 'center',
            at: 'center',
            of: window,
            collision: 'fit'
        },
        buttons: {
            "Aceptar": function () {
                var id = 0;
                var name = '';
                var description = '';
                var value = 0;
                var urlimage = '';
                var list = new Array();
                $.each($('#divImgContent').children('div'), function (key, data) {
                    list.push({
                        "Id": data.id,
                        "Name": $(data).children('#imgName').val(),
                        "Description": $(data).children('#imgDescription').val(),
                        "Value": $(data).children('#imgValue').val(),
                        "UrlImage": $(data).children('img').attr('src'),
                        "IdBusiness": data.idBusiness
                    });
                });
                $.ajax({
                    url: getHost() + 'Admin/UpdateProductInformation',
                    type: "POST",
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify(list),
                    async: true,
                    processData: false,
                    cache: false,
                    success: function (json) { },
                    error: function (xhr, errorText) {
                        $("#dialog-message").html('En este momento no podemos procesar tu solicitud, por favor intenta más tarde.')
                            .dialog('open');
                    }
                });
                $('#divPaso4').css('background-color', 'yellowgreen');
                $(this).dialog("close");
            }
        }
    });

    $('#divDireccion').dialog({
        width: 'auto', // overcomes width:'auto' and maxWidth bug
        maxWidth: 800,
        height: 'auto',
        modal: true,
        fluid: true, //new option
        resizable: false,
        autoOpen: false,
        position: {
            my: 'center',
            at: 'center',
            of: window,
            collision: 'fit'
        },
        buttons: {
            "Aceptar": function () {
                $.ajax({
                    url: getHost() + 'Admin/UpdateAddressTemp',
                    type: "POST",
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify({ newAddress: $('#txtDireccion').val() }),
                    async: true,
                    processData: false,
                    cache: false,
                    success: function (json) { },
                    error: function (xhr, errorText) {
                        $("#dialog-message").html('En este momento no podemos procesar tu solicitud, por favor intenta más tarde.')
                            .dialog('open');
                    }
                });
                $('#divPaso5').css('background-color', 'yellowgreen');
                $(this).dialog("close");
            }
        }
    });

    $('#divCategoria').dialog({
        width: 'auto', // overcomes width:'auto' and maxWidth bug
        maxWidth: 800,
        height: 'auto',
        modal: true,
        fluid: true, //new option
        resizable: false,
        autoOpen: false,
        position: {
            my: 'center',
            at: 'center',
            of: window,
            collision: 'fit'
        },
        buttons: {
            "Aceptar": function () {
                $.ajax({
                    url: getHost() + 'Admin/UpdateCategoryTemp',
                    type: "POST",
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify({
                        newCategory: $('#txtCategoria').attr('idCategory'),
                        newCategoryName: $('#txtCategoria').val()
                    }),
                    async: true,
                    processData: false,
                    cache: false,
                    success: function (json) { },
                    error: function (xhr, errorText) {
                        $("#dialog-message").html('En este momento no podemos procesar tu solicitud, por favor intenta más tarde.')
                            .dialog('open');
                    }
                });
                $('#divPaso6').css('background-color', 'yellowgreen');
                $(this).dialog("close");
            }
        }
    });

    $('#divPaso0').click(function () {
        $(this).css('background-color', 'gainsboro');
        $('#divCargaNombre').dialog('open');
    });

    $('#divPaso1').click(function () {
        $(this).css('background-color', 'gainsboro');
        $('#divCargaLogo').dialog('open');
    });

    $('#divPaso2').click(function () {
        $(this).css('background-color', 'gainsboro');
        $('#divCargaFotos').dialog('open');
    });

    $('#divPaso3').click(function () {
        $(this).css('background-color', 'gainsboro');
        $('#divCambiaColor').dialog('open');
    });

    $('#divPaso4').click(function () {
        $(this).css('background-color', 'gainsboro');
        $.ajax({
            url: getHost() + 'Admin/GetProductTemp',
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            async: true,
            processData: false,
            cache: false,
            success: function (json) {
                var template = '<div id="{0}" idBusiness="{5}" style=\'float:left;margin: 4em 4em 4em 4em\'><img src="{1}" /><br />' +
                    '<input id="imgName" class="txtTooltip" type="text" data-toggle="tooltip" value="{2}" title="Aqui escribe el nombre de tu producto" /><br />' +
                    '<input id="imgDescription" class="txtTooltip" type="text" data-toggle="tooltip" value="{3}" title="Aqui escribe algun dato o referencia de tu producto" /><br />' +
                    '<input id="imgValue" class="txtTooltip" type="text" data-toggle="tooltip" value="{4}" title="Aqui puedes colocar el valor de tu producto" /><br /><br /></div>';
                var html = '';
                if (json != null) {
                    $('#divImgContent').html('');
                    $.each(json, function (key, data) {
                        html = template.format(data.Id, data.UrlImage, data.Name, data.Description, data.Value, data.IdBusiness);
                        $('#divImgContent').append(html);
                    });
                    $('#divAgregaInformacion').parent().position({
                        my: 'center',
                        at: 'center',
                        of: window,
                        collision: 'fit'
                    });
                }
            },
            error: function (xhr, errorText) {
                $("#dialog-message").html('En este momento no podemos procesar tu solicitud, por favor intenta más tarde.')
                    .dialog('open');
            }
        });
        $('#divAgregaInformacion').dialog('open');
    });

    $('#divPaso5').click(function () {
        $(this).css('background-color', 'gainsboro');
        $('#divDireccion').dialog('open');
    });

    $('#divPaso6').click(function () {
        $(this).css('background-color', 'gainsboro');
        if ($('#lstCategorySelect').html() == '') {
            $.ajax({
                url: getHost() + 'Admin/GetCategories',
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                async: true,
                processData: false,
                cache: false,
                success: function (json) {
                    var template2 = '<li style="cursor:pointer" class="ui-widget-content" id="{0}">{1}</li>';
                    var html2 = '';
                    if (json != null) {
                        $('#txtCategoria').catcomplete({
                            delay: 0,
                            source: json.Categories,
                            select: function (event, ui) {
                                $('#txtCategoria').attr('idCategory', ui.item.id);
                                return false;
                            }
                        });                        
                        $('#lstCategoriaSelect').html('');
                        $.each(json.Categories, function (key, data) {
                            html2 = template2.format(data.id, data.label);
                            $('#lstCategorySelect').append(html2);
                        });
                        $('#txtCategoria').blur(function () {
                            var validateCategory = false;
                            $.each($('#lstCategorySelect li'), function (key, data) {
                                if ($(data).html() == $('#txtCategoria').val())
                                {
                                    $('#txtCategoria').attr('idCategory', $(data).attr('id'));
                                    validateCategory = true;
                                }
                            });
                            if (!validateCategory)
                            {
                                $('#txtCategoria').val('');
                                $('#txtCategoria').attr('idCategory', 0);
                            }
                        });
                        $('#divCategoria').parent().position({
                            my: 'center',
                            at: 'center',
                            of: window,
                            collision: 'fit'
                        });
                    }
                },
                error: function (xhr, errorText) {
                    $("#dialog-message").html('En este momento no podemos procesar tu solicitud, por favor intenta más tarde.')
                        .dialog('open');
                }
            });
        }
        $('#divCategoria').dialog('open');
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
        url: getHost() + 'Admin/UploadLogo',
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
            //$('.file_name').html(data.result.name);
            //$('.file_type').html(data.result.type);
            //$('.file_size').html(data.result.size);
            $('#divNewPhoto').html('');
            $('#divNewPhoto').append('<label>Nueva foto o nuevo logo: </label><br/><img src="' + data.result.savedFileImage + '">');
            $('#divPaso1').css('background-color', 'yellowgreen');
        }
    }).on('fileuploadprogressall', function (e, data) {
        var progress = parseInt(data.loaded / data.total * 100, 10);
        $('.progress .progress-bar').css('width', progress + '%');
    });
}

function colorPickerConfig() {
    $('#btnChangeColor').colorpicker({
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
    $('#btnChangeColorText').colorpicker({
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
        $('#divCambiaColor').css('color', event.color.toHex());
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

function enableContextMenu() {
    $.contextMenu({
        selector: '.context-menu-one',
        callback: function (key, options) {
            var idTemp = $('.context-menu-active').attr('id');
            $.ajax({
                url: getHost() + 'Admin/DeleteImageTemp',
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({
                    id: idTemp
                }),
                async: true,
                processData: false,
                cache: false,
                success: function (data) {
                    $('#' + idTemp).parent().remove();
                },
                error: function (xhr, errorText) {
                    $("#dialog-message").html('En este momento no podemos procesar tu solicitud, por favor intenta más tarde.')
                        .dialog('open');
                }
            });
        },
        items: {
            "delete": { name: "Borrar", icon: "delete" }
        }
    });
}

function selectableConfig() {
    $('#lstCategorySelect').selectable({
        stop: function () {
            $(".ui-selected", this).each(function () {
                $('#txtCategoria').val($(this).html());
                $('#txtCategoria').attr('idCategory', $(this).attr('id'));
            });
        }
    });
}

function viewMessage(result, message)
{
    if (result == 'true') {
        alert(message);
    }
}

String.prototype.format = function () {
    var args = arguments;
    return this.replace(/\{\{|\}\}|\{(\d+)\}/g, function (m, n) {
        if (m == "{{") { return "{"; }
        if (m == "}}") { return "}"; }
        return args[n];
    });
};