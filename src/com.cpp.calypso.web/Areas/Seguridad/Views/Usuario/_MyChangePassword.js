(function ($) {
    
    var _$modal = $('#nuc_dlg_contenedor');
    var _$form = $('form[id=frmMyChangePassword]');


    function save() {

        if (!_$form.valid()) {
            return;
        }

        var user = _$form.serializeFormToObject();
       
        $.ajax({
            url: abp.appPath + 'Seguridad/Usuario/MyChangePasswordSave',
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
    _$form.closest('div.modal-content').find("#usuario_my_change_password_save").click(function (e) {
        e.preventDefault();
        save();
    });

    //Handle enter key
    _$form.find('input').on('keypress', function (e) {
        if (e.which === 13) {
            e.preventDefault();
            save();
        }
    });

    _$modal.on('shown.bs.modal', function () {
        _$form.find('input[type=text]:first').focus();
    });
})(jQuery);