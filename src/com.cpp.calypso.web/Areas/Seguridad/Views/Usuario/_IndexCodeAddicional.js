(function ($) {

    var _$modal = $('#nuc_dlg_contenedor');

    $('.nuc_action_Reseteo').click(function (e) {

        e.preventDefault();

        var entityId = $(this).attr("data-entity-id");

        $("#mod-confirmar-rest-entity-id").val(entityId)

        $("#mod-confirmar-rest").modal('show');
       
        $('#exampleModal').on('show.bs.modal', function (event) {
            var button = $(event.relatedTarget) // Button that triggered the modal
            var recipient = button.data('whatever') // Extract info from data-* attributes
            // If necessary, you could initiate an AJAX request here (and then do the updating in a callback).
            // Update the modal's content. We'll use jQuery here, but you could use a data binding library or other methods instead.
            var modal = $(this)
            modal.find('.modal-title').text('New message to ' + entityId)
            modal.find('.modal-body input').val(recipient)
        })
    });


    $('#btn-reset-si').click(function (e) {
        e.preventDefault();

        var entityId = $("#mod-confirmar-rest-entity-id").val();
        var data = { Id : entityId};
        


        $.ajax({
            url: abp.appPath + 'Seguridad/Usuario/Reseteo',
            data: data,
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
    });

})(jQuery);