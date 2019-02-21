(function ($) {
    
    var _$modal = $('#nuc_dlg_contenedor');
    var _$form = $('form[id=frmMyInfo]');

   


    function saveConfirm() {

        if (!_$form.valid()) {
            return;
        }

        //Verificar si se modifico el correo
        var correoOrigen = $("#correo_original").val();
        var correoCambiado = $("#Correo").val();


        if (correoOrigen === correoCambiado) {

            save();

        } else { 
 
            abp.message.confirm(
                "Existe un cambio de la cuenta de correo electrónico. Se enviará un correo a la nueva cuenta establecida, para que registre una nueva Contraseña.  ¿Desea continuar?",
                function (isConfirmed) {
                    if (isConfirmed) {
                        save();
                    }
                }
                );

        }
        
        
    }

    function save() {
        var user = _$form.serializeFormToObject();

        $.ajax({
            url: abp.appPath + 'Seguridad/Usuario/MyInfoSave',
            data: user,
            type: 'POST',
            success: function (response) {

                if (response.success === true) {

                    _$modal.modal('hide');

                    toastr["success"]("Proceso guardado exitosamente");

                } else if (response.success === false) {
                    var message = '';

                    if (response != undefined && response.errors) {

                        $.each(response.errors, function (i, fieldItem) {
                            //i => nombre del campo
                            if (fieldItem.length > 0) {
                                $.each(fieldItem, function (j, errItem) {
                                    message += errItem + ' ';
                                });
                                message += "<br />";
                            }
                        });
                    } else if (response != undefined && response.error) {
                        message += response.error;
                    }

                    toastr["error"](message);
                } else { //not wrapped result

                    toastr["error"](response);
                }
            }
        });
    }
 

    //Handle save button click
    _$form.closest('div.modal-content').find("#usuario_my_info_save").click(function (e) {
        e.preventDefault();
        saveConfirm();
    });

    //Handle enter key
    _$form.find('input').on('keypress', function (e) {
        if (e.which === 13) {
            e.preventDefault();
            saveConfirm();
        }
    });

    _$modal.on('shown.bs.modal', function () {
        _$form.find('input[type=text]:first').focus();
    });
})(jQuery);