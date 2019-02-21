
(function (w, $, undefined) {
    try {

        // override these in your code to change the default behavior and style 
        $.blockUI.defaults.message = '<h3>Procesando...</h3>';

        // z-index for the blocking overlay. Modal Bootstrap
        $.blockUI.defaults.baseZ = 2000; 

        $.blockUI.defaults.css = {
            width: '30%',
            top: '40%',
            left: '35%',
            textAlign: 'center',
            color: '#000',
            border: '3px solid #aaa',
            backgroundColor: '#fff',
            cursor: 'wait'
        };

        $.fn.datepicker.defaults.format = "dd/mm/yyyy";
        $.fn.datepicker.defaults.language = "es";

        setAjaxEvents();

        if (toastr) {
            //Default
            toastr.options.positionClass = "toast-top-right";
        }
        

    } catch (ex) {
        alert('app.js: error =>' + ex);
    }

    //serializeFormToObject plugin for jQuery
    $.fn.serializeFormToObject = function () {
        //serialize to array
        var data = $(this).serializeArray();

        //add also disabled items
        $(':disabled[name]', this).each(function () {
            data.push({ name: this.name, value: $(this).val() });
        });

        //map to object
        var obj = {};
        data.map(function (x) { obj[x.name] = x.value; });

        return obj;
    };

    function setAjaxEvents() {
        // Ajax events fire in following order
        $(document).ajaxStart(function () {

            $.blockUI({ css: { top: '40%', left: '35%' } });

            /*
            }).ajaxSend(function (e, xhr, opts) {
            */

        }).ajaxError(function (e, xhr, opts, thrownError) {
            //Function( Event event, jqXHR jqXHR, PlainObject ajaxSettings, String thrownError )
            //TODO: Mejorar los mensajes de error

            $.unblockUI();

            if (xhr.status == 0) {
                //alert('offline!\n Please check your network.');
            } else if (xhr.status == 400) {

                var jsonResult = '';
                // responseText.  statusText. status

                try {
                    jsonResult = JSON.parse(xhr.responseText);
                }
                catch (ex) {
                    jsonResult = xhr.responseText;
                }

                //TODO: En donde se define errors ???
                if (jsonResult != undefined && jsonResult.errors) {
                    message = '';
                    $.each(jsonResult.errors, function (i, fieldItem) {
                        //i => nombre del campo
                        if (fieldItem.length > 0) {
                            $.each(fieldItem, function (j, errItem) {
                                message += errItem + ' ';
                            });
                            message += "\n";
                        }
                    });

                    var dlg_error = $('#nuc_dlg_error');

                    if (dlg_error) {
                        dlg_error.modal('show');
                        dlg_error.find('p').html(message);
                    } else {

                        alert('Error: ' + message);

                    }

                } else {
                    alert('Errores  : ' + thrownError);
                    //alert('status: ' + xhr.status + ', errors: ' + jsonResult);
                }

            } else if (xhr.status == 403) {
                alert('status: ' + xhr.status + ', errors: ' + 'Sorry, your session has expired. Please login again to continue');
                // window.location.href = "/Cuenta/Ingreso";
            } else if (xhr.status == 302) {


            } else if (xhr.status == 500) {

                var jsonResult = '';
                // responseText.  statusText. status

                try {
                    jsonResult = JSON.parse(xhr.responseText);
                }
                catch (ex) {
                    jsonResult = xhr.responseText;
                }

                if (jsonResult != undefined && jsonResult.error) {

                    var dlg_error = $('#nuc_dlg_error');

                    if (dlg_error) {
                        dlg_error.modal('show');
                        dlg_error.find('p').html(jsonResult.error);
                    } else {

                        alert('Error: ' + jsonResult.error);

                    }
                } else {
                    alert('Errores  : ' + thrownError);
                    //alert('status: ' + xhr.status + ', errors: ' + jsonResult);
                }
            }



        }).ajaxSuccess(function (e, xhr, opts) {

            if (xhr.status == 302 && xhr.getResponseHeader('Location')) {
                //alert('Location: ' + xhr.getResponseHeader('Location'));
                window.location = xhr.getResponseHeader('Location');
            }

        }).ajaxComplete(function (e, xhr, opts) {

            if (xhr.status == 302) {
                var jsonResult = '';
                // responseText.  statusText. status

                try {
                    jsonResult = JSON.parse(xhr.responseText);
                }
                catch (ex) {
                    jsonResult = xhr.responseText;
                }

                if (jsonResult != undefined && jsonResult.success && jsonResult.redirect) {
                    //alert('status: ' + xhr.status + ', redirect: ' + jsonResult.redirect);
                    window.location = jsonResult.redirect;
                } else {
                    alert('status: ' + xhr.status + ', result: ' + jsonResult);
                }
            }

        }).ajaxStop(function () {

            $.unblockUI();
        });

    }

}) (window, jQuery);

(function (w, $, undefined) {

    function resizeBroadcast() {

        var timesRun = 0;
        var interval = setInterval(function () {
            timesRun += 1;
            if (timesRun === 5) {
                clearInterval(interval);
            }
            if (navigator.userAgent.indexOf('MSIE') !== -1 || navigator.appVersion.indexOf('Trident/') > 0) {
                var evt = document.createEvent('UIEvents');
                evt.initUIEvent('resize', true, false, window, 0);
                window.dispatchEvent(evt);
            } else {
                window.dispatchEvent(new Event('resize'));
            }
        }, 62.5);
    }

    /* ---------- Main Menu Open/Close, Min/Full ---------- */
    $('.sidebar-toggler').click(function () {
        $('body').toggleClass('sidebar-hidden');
        resizeBroadcast();
    });

    $('.sidebar-minimizer').click(function () {
        $('body').toggleClass('sidebar-minimized');
        resizeBroadcast();
    });

    $('.brand-minimizer').click(function () {
        $('body').toggleClass('brand-minimized');
    });

    $('.mobile-sidebar-toggler').click(function () {
        $('body').toggleClass('sidebar-mobile-show');
        resizeBroadcast();
    });

   

    /* ---------- Opciones de Usuario ---------- */
    $('#action_info_user').click(function (e) {


        e.preventDefault();
        $.ajax({
            url: abp.appPath + 'Seguridad/Usuario/MyInfo',
            type: 'POST',
            contentType: 'application/html',
            success: function (content) {

                $('#nuc_dlg_contenedor').find("div.modal-content").html(content);
                $('#nuc_dlg_contenedor').modal('show');

            },
            error: function (e) { }
        });

    });

    $('#action_change_password_user').click(function (e) {


        e.preventDefault();
        $.ajax({
            url: abp.appPath + 'Seguridad/Usuario/MyChangePassword',
            type: 'POST',
            contentType: 'application/html',
            success: function (content) {

                $('#nuc_dlg_contenedor').find("div.modal-content").html(content);
                $('#nuc_dlg_contenedor').modal('show');

            },
            error: function (e) { }
        });

    });

}) (window, jQuery);

(function (w, $, undefined) {
    ///<param name="w" type="window">window object</param>
    ///<param name="$" type="jQuery">window object</param>
    ///<param name="undefined">undefined</param>

    var $d = $(w.document);

    var UtilsClass = function (onInit) {
        var self = this;
        self.data = {
            dialogCloseButtonClassName: undefined,
            dialogCloseButtonIcon: undefined,
            dialogCloseButtonText: undefined,
            dialogSaveButtonClassName: undefined,
            dialogSaveButtonIcon: undefined,
            dialogSaveButtonText: undefined 
        };
        $d.ready($.proxy(init, self));

        //this.configureTime = configureTime;
        this.prepareForm = prepareForm;
        //this.initModalDlgBtn = initModalDlgBtn;

        return self;

        function init() {
            if (onInit !== undefined && $.isFunction(onInit)) {
                onInit.call(this);
            }

             
            prepareForm(document);
            //initModalDlgBtn();
        }

         

        function prepareForm(container, callback) {
            var $container = $(container),
                $form = $container.is("form") ? $container : $container.find("form").first(),
                thereForm = $form.length > 0,
                $dlg = $container.closest(".modal");
            var submitBtn = $dlg.find("[data-submit='true']");
            submitBtn.toggle(thereForm);
            var closeBtn = $dlg.find("[data-dismiss='modal']");
            closeBtn.toggleClass("cancel", thereForm);

            if (thereForm) {
                parseInputs(container);

                if ($.isFunction(callback))
                    callback(container);
            }
            return $form;
        }

        function parseInputs(context) {
            context = context || document;
            parseInputDate(context);

            //initSelect2(context);
            //initDataProviders(context);
            //initDataDocente(context);
            //initDataEstudiante(context);
        }

        function parseInputDate(context) {
            if (Modernizr.inputtypes.date) return;

            $(context).find("input[type='date']").each(function () {
                var $this = $(this),
                    $inputGroup = $this.parent(".input-group"),
                    $elem = $inputGroup.length ? $inputGroup.addClass("date") : $this,
                    opt = {
                        todayBtn: "linked",
                        daysOfWeekDisabled: "",
                        calendarWeeks: true,
                        autoclose: true,
                        todayHighlight: true,
                        language: "es",
                        format: "dd/mm/yyyy"
                    };
                $elem.datepicker(opt);
            });
        }

          
    };

    UtilsClass.prototype = {
        constructor: UtilsClass,
        defaultLoading: "<p><span class='fa fa-spinner fa-spin'></span>&nbsp;Procesando &hellip;</p>",
         
    };
     
    w.UtilsClass = UtilsClass;
})(window, jQuery);