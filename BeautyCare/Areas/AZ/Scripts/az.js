$.ajaxSetup({ cache: false });

(function (window) {
    var _intravision = function () {
        this.defaultAjaxFormOptions = {
            success: this.handleAjaxFormResult
        };
    };

    _intravision.prototype.showGlobalErrors = function (jsonCommandResult) {
        if (jsonCommandResult.Errors['Global']) {
            var n = noty({ text: jsonCommandResult.Errors['Global'], type: 'error', timeout: 5000 });
        }
    };

    _intravision.prototype.showFormErrors = function (form, jsonCommandResult) {
        intravision.showGlobalErrors(jsonCommandResult);
        //Calculate how many errors in Errors object we have
        var properties = 0;
        for (p in jsonCommandResult.Errors) { if (jsonCommandResult.Errors.hasOwnProperty(p) && p != 'Global') properties++; }
        if (properties > 0) {
            var validator = form.validate();
            if (validator && jsonCommandResult.Errors) { validator.showErrors(jsonCommandResult.Errors); } //WTF??? 
        }
    };

    _intravision.prototype.handleAjaxFormResult = function (r, s, x, form, target) {
        var self = this;
        if (!target) target = form.attr('targetupdate') ? $(form.attr('targetupdate')) : form.parents('.updatepanel:first');
        try {
            if (!r.hasOwnProperty('Successful')) r = $.parseJSON(r);  // json
            if (!r.Successful) { intravision.showFormErrors(form, r); return false; }
            else intravision.reloadPanel(target);
        }
        catch (e) {
            target.html(r);
        }
        form.trigger('ajaxSubmitComplete');
        return true;
    };

    // Убрать хвосты fancybox
    _intravision.prototype.fancyboxFormOptions = function (link, target) {
        return {
            type: 'POST',
            success: function (r, s, x, form) { if (!intravision.handleAjaxFormResult(r, s, x, form, target)) return false; if (link.attr('targetupdate') != '#fancybox-content') { $.fancybox.close(); } }
        };
    };
    
    _intravision.prototype.reloadPanel = function (panel, callback) { panel = $(panel); if (panel.is('img')) { panel.attr('src', function (i, attr) { if (attr.indexOf('?') > 0) attr = attr.substr(0, attr.indexOf('?')); return attr + '?_=' + Math.random(); }); } else { panel.load(panel.attr('data-action'), function () { panel.data('loaded', true); if (callback) callback(); panel.trigger('reload'); }); } };
    
    _intravision.prototype.zebraRows = function (element) {
        $('ul.items li:odd', element).addClass('odd');
    };
    
    window.intravision = new _intravision();
    
})(window);

$.validator.methods.number = function (value, element) {
    return !isNaN(parseFloat(value));
};

$(function () {
    // Gif лоадер для jquery ui dialog
    $(document).ajaxStart(function () { $("body").addClass("loading"); });
    $(document).ajaxStop(function () { $("body").removeClass("loading"); });
    $(document).ajaxError(function () { $("body").removeClass("loading"); });
    $(document).ajaxComplete(function () { $("body").removeClass("loading"); });
    $('body').append('<div id="ajax-loader"></div>');
    $('form').attr('autocomplete', 'off');
    $('body').append('<div id="jqt-tooltip"></div>');
    // Окошко ошибок
    $('body').ajaxError(function (e, xhr, options) { var result = $.parseJSON(xhr.responseText); if (result && result.Errors && result.Errors['Global']) var n = noty({ text: result.Errors['Global'], type: 'error', timeout: 5000 }); });
    // Модальная форма в jquery ui dialog
    $('body').on('click', 'a.dialog-form', function () {
        var link = $(this);

        $.ajax({
            url: $(this).attr('href'),
            success: function (data) {
                $(data).dialog({
                    autoOpen: true,
                    closeOnEscape: false,
                    draggable: false,
                    resizable: false,
                    modal: true,
                    width: 750,
                    height: "auto",
                    show: "fade",
                    hide: "fade",
                    buttons: {
                        "ОК": function () {
                            $('form', $('div.ui-dialog-content')).submit();
                        },
                        "Отмена": function () {
                            $(this).dialog("close");
                        }
                    },
                    create: function (event, ui) {
                        var form = $('form', $('div.ui-dialog-content'));
                        form.attr('autocomplete', 'off');

                        if ($('input[type=submit]', $('div.ui-dialog-content')).length == 0) {
                            $(this).dialog({ buttons: [{ text: "Закрыть", click: function () { $(this).dialog("close"); } }] });
                        }

                        adaptJQueryUIDialogContent();

                        if (!form.hasClass('skip-ajax'))
                            dialogajaxForm(link, form);

                        form.find(':input:visible:first').focus();
                    },
                    close: function () {
                        $(this).dialog("destroy");
                        $(this).remove();
                    }
                });
            }
        });

        return false;
    });

    $('body').on('focus mouseenter', 'form.ajax-form', function () {
        var form = $(this);
        form.attr('autocomplete', 'off');
        if (form.data('initialized')) return;
        var target = form.attr('targetupdate') ? $(form.attr('targetupdate')) : form.parents('.updatepanel');
        form.ajaxForm({
            beforeSubmit: function (data, form) { if (!form.valid()) { $.fancybox.resize(); return false; } },
            error: function (xhr, s) { alert('error'); },
            success: function (r, s, x, form) {
                form.trigger('ajaxFormSuccess');
                var result = null;
                try { result = $.parseJSON(r); } catch (e) { target.html(r); return false; };
                if (result && result.Error) {
                    form.find('.validationsummary').html(result.Error);
                    form.find('.validationsummary').show();
                }
                else {
                    reloadPanel(target);
                    return false;
                };
            }
        });
        form.data('initialized', true);
    });

    createTableDnD();

    $('body').on('click', 'a.subcontent-loader', function () {
        var a = $(this);
        var target = a.attr('targetupdate')
            ? $(a.attr('targetupdatepanel'))
            : a.parents('.updatepanel:first');

        if (!target.length)
            return false;

        target.load(a.attr('href'), function () {
            target.trigger('reload');
        });

        return false;
    });
    $('body').on('focus', 'form', function () { $(this).validate(); });
    $('body').on('click', '.stringlist li > img', function () { $(this).parents('ul:first').append('<li>' + $(this).parents('li:first').html() + '</li>'); $(this).parents('li:first').find('img').remove(); });
    $('body').on('mouseenter focus', '.mvcgrid th > a', function () { $(this).addClass('subcontent-loader'); });
    $('body').on('mouseenter focus', '.pagination a', function () { $(this).addClass('subcontent-loader'); });
    $('body').on('click', '.showFilterCondition', function () { var visibleFilterCondition = $('.selectFilterCondition:visible'); var currentfilterCondition = $($(this).attr('filterIndex')); visibleFilterCondition.hide(); if (visibleFilterCondition.attr('id') != currentfilterCondition.attr('id')) { currentfilterCondition.show(); } return false; });
    $('body').on('click', '.closeFilterCondition', function () { $($(this).attr('filterIndex')).hide(); return false; });
    $('body').on('change', '.selectFilterCondition input[type=checkbox]', function () {
        var checkbox = $(this);
        var selectFilterCondition = checkbox.parents('div.selectFilterCondition:first');
        var applyItems = selectFilterCondition.next('div.applyItems');
        var classApplyItem = 'newApplyItem';
        $(selectFilterCondition.find('input#applyFilterCondition').val().split('|')).each(function () { if (this == checkbox.val()) { classApplyItem = 'applyItem'; } });
        if (checkbox.is(':checked')) {
            var adeleteApplyItem = $('<a/>').attr({
                hfer: '#',
                'class': 'deleteApplyItem',
                itemValue: checkbox.val()
            }).text('x');
            var pApplyItem = $('<p/>').attr('class', classApplyItem).text(checkbox.get(0).nextSibling.nodeValue).append(adeleteApplyItem);

            applyItems.append(pApplyItem);
        }
        else {
            applyItems.find('p:has(a[itemvalue=' + checkbox.val() + '])').remove();
        }
    });
    $('body').on('click', '.button.resetFilter', function () {
        var href = $(this).attr('href');
        if (href.indexOf("eventId") == -1) {
            $(this).attr('href', href + "?eventId=" + $('div.updatepanel.filterpanel').attr('data-action').split('eventId=')[1]);
        }
    });
    $('body').on('focus', 'input.dateRU', function() {
        $(this).datepicker();
    });
    $('body').on('focus', 'input.timeRU', function () {
        var timeField = $(this);
        
        timeField.timepicker({
            ampm: false,
            stepMinute: 10,
            minuteGrid: 10
        });
    });
    $('body').on('focus', 'input.datetimeRU', function () {
        var dateTimeField = $(this);
        dateTimeField.datetimepicker({
            language: 'ru',
            minuteStepping: 15
            /*onClose: function (dateText, inst) {
                dateTimeField.trigger('dateTimePickerOnClose');
            },
            onSelect: function (selectedDateTime) {
                dateTimeField.trigger('dateTimePickerOnSelect');
            }*/
        });
    });
    $('body').on('click', 'a.deleteApplyItem', function () {
        var selectFilterCondition = $(this).parents('div.applyItems:first').prev('div.selectFilterCondition');
        selectFilterCondition.find('input[value=' + $(this).attr('itemvalue') + ']').attr('checked', false);
        $(this).parents('p:first').remove();
        return false;
    });
    $('body').on('ajaxFormSuccess', 'div.filter form', function () { var filterUpdatePanel = $(this).parents('.updatepanel:first'); reloadPanel(filterUpdatePanel); });
    $('body').on('focus', 'input.dateRU', function () {
        var dateTimeField = $(this);
        dateTimeField.datepicker({
            onClose: function (dateText, inst) {
                dateTimeField.trigger('datePickerOnClose');
            },
            onSelect: function (selectedDateTime) {
                dateTimeField.trigger('datePickerOnSelect');
            }
        });
    });
    // Замена шрифтов заголовков
    //Cufon.replace('#header > h1');
    //Cufon.replace('.contenthead > h2');
    // Скрипты проекта
    $('img.show-image').error(function () {
        var imageBlock = $('.show-image').parents('dd').prev().andSelf();
        imageBlock.hide();
    });
    $('body').on('click', 'a.delete-image-link', function () {
        var imageBlock = $('.show-image').parents('dd').prev().andSelf(),
            link = $(this).attr('href');

        $.ajax({
            type: 'DELETE',
            url: link,
            dataType: 'json',
            data: { __RequestVerificationToken: $(this).parents('form').children('input[name=__RequestVerificationToken]').val() },
            success: function (data, textStatus, jqXHR) {
                if (data.Successful == true) {
                    imageBlock.fadeOut('slow', function () {
                        $(this).remove();
                    });
                } else {
                    intravision.showGlobalErrors(data);
                }
            }
        });

        return false;
    });
    $('body').on('change', 'select#MBClassId', function () {
        var mbClassSelect = $(this),
            mbModelSelect = $('select#MBModelId');
        
        $.ajax({
            url: '/Car/GetMBModelByMBClassId',
            data: { mbClassId: mbClassSelect.val() },
            success: function (data) {
                mbModelSelect.empty();
                
                $(data).each(function () {
                    mbModelSelect.append($('<option></option>').val(this.Value).html(this.Text).prop('selected', this.Selected));
                });
            }
        });
    });
    $('body').on('click', 'a.delete-mbclass-image', function (event) {
        var div = $(this).parent('div');

        $.ajax({
            url: $(this).attr('href'),
            data: { __RequestVerificationToken: $(this).parents('form').children("input[name=__RequestVerificationToken]").attr("value") },
            type: 'DELETE',
            success: function (data, textStatus, jqXHR) {
                if (data['result'] == true) {
                    div.fadeOut('slow');
                } else if (data['result'] == false && data['error'] != null) {
                    window.alert("Не удалось удалить изображение.");
                }
            }
        });

        return false;
    });
    $('body').on('click', 'input#IsNotWorkday', function () {
        if ($(this).prop('checked') == true) {
            if ($('input#StartTime').val() == "" || $('input#EndTime').val() == "") {
                $('input#StartTime').val("0:00");
                $('input#EndTime').val("0:00");
            }
        } else if ($(this).prop('checked') == false) {
            if ($('input#StartTime').val() == "0:00" || $('input#EndTime').val() == "0:00") {
                $('input#StartTime').val("");
                $('input#EndTime').val("");
            }
        }
    });
    $('body').on('click', 'a#sendCarReport', function (event) {
        $.ajax({
            url: $('#Action-SendCarReport').val(),
            dataType: 'json',
            success: function(json) {
                if (json.Successful) {
                    var n = noty({ text: 'Отчет отправлен.', type: 'success', timeout: 5000 });
                } else {
                    intravision.showGlobalErrors(json);
                }
            }
        });

        event.preventDefault();
    });
    $('body').on('click', 'a#sendWeeklyReport', function (event) {
        $.ajax({
            url: $('#Action-SendWeeklyReport').val(),
            dataType: 'json',
            success: function (json) {
                if (json.Successful) {
                    var n = noty({ text: 'Отчет отправлен.', type: 'success', timeout: 5000 });
                } else {
                    intravision.showGlobalErrors(json);
                }
            }
        });

        event.preventDefault();
    });
	

    /*var filterTop = $('#header').height() - $('.second-lvl-menu').height();
	$('div#right-sidebar-filter').css('top', filterTop);

	
	$(window).on('resize', function(){
		filterTop = $('#header').height() - $('.second-lvl-menu').height();
		$('div#right-sidebar-filter').css('top', filterTop);
	});
	
	 $(document).on('scroll', function(){
	     if ($(document).scrollTop() > 0)
			{
				$('div#right-sidebar-filter').addClass('fixed');
			}
		else
			{
				$('div#right-sidebar-filter').removeClass('fixed');
			}
	 });*/



    /***********   Filter hiding / showing   **********/

    $('body').on('click', '.showHideFilter', function () {
        $('#right-sidebar-filter').slideToggle(function () {
            if ($('#right-sidebar-filter').css('display') == 'none')
                {
                $('.showHideFilter').text('Показать фильтр');
                }

            else {
                $('.showHideFilter').text('Скрыть фильтр');
                 }
        });

    });

   /* var rightSideFilter = $('#right-sidebar-filter'),
        isFilterCollapsed = false,
        filterHeight;

    $('body').on('click', '.showHideFilter', function () {
        if (isFilterCollapsed == false) {
            filterHeight = rightSideFilter.outerHeight(false);

            $(this).parents('#right-sidebar-filter').css({
                'min-height': '0',
                'padding-bottom': '0'
            }).animate({
                height: '65'
            });

            $(this).text('Показать фильтр');

            isFilterCollapsed = true;

            //console.log('filterHeight = ' + filterHeight);
        }

        else {
            $(this).parents('#right-sidebar-filter').css({
                'min-height': '400px',
                'padding-bottom': '50px'
            }).animate({
                height: filterHeight
            });

            $(this).text('Скрыть фильтр');
            isFilterCollapsed = false;
            //console.log('filterHeight = ' + filterHeight);
            
        }
        
    });

    $(window).on('resize', function () {

        if (isFilterCollapsed == false)
            {
                rightSideFilter.css({
                    'height': 'auto',
                    'min-height': '400px',
                    'padding-bottom': '50px'
                });
                
                filterHeight = rightSideFilter.outerHeight(false);
            }

        else
            {
                rightSideFilter.css({
                    'opacity': '0',
                    'height': 'auto',
                    'min-height': '400px',
                    'padding-bottom': '50px'
                });

                filterHeight = rightSideFilter.outerHeight(false);

                rightSideFilter.css({
                    'opacity': '1',
                    'height': '65px',
                    'min-height': '0',
                    'padding-bottom': '0'
                });
            }
    });*/

    /*************************************************/



});

function createTableDnD() {
    $('table[tableSortable=true]').tableDnD({
        onDrop: function (table, row) {
            var rows = table.tBodies[0].rows;
            var w = "";
            for (var i = 0; i < rows.length; i++) {
                w += rows[i].id + "|";
            }
            $.get($(table).attr('urlsortable'), { rowIds: w }, function (data) {

            });
        },
        dragHandle: 'dragHandle'
    });
};

function createQtipsForThumbnails() {
    $('img.gallery-thumbnail').each(function () {
          var content = $('<img />', {
            src: $(this).parent().attr('href'),
            alt: 'Загрузка изображения...',
            width: 715
        });

        $(this).qtip({
            content: {
                text: content,
                title: {
                    text: $(this).attr('alt')
                }
            },
            position: {
                my: 'left center',
                at: 'right center',
                viewport: $(window)
            },
            hide: {
                fixed: true
            },
            style: {
                classes: 'ui-tooltip-light ui-tooltip-shadow'
            }
        });
    });
}

function populateDropdown(select, data) { select.html(''); $.each(data, function (id, option) { select.append($('<option></option>').val(option.value).html(option.name)); }); }

function reloadPanel(panel, callback) { panel = $(panel); panel.load(panel.attr('data-action'), function () { panel.data('loaded', true); if (callback) callback(); panel.trigger('reload'); }); }

function showFormErrors(form, xhr) {
    var result = $.parseJSON(xhr.responseText);
    var validator = form.validate();
    if (validator) validator.showErrors(result.Errors);
    else { var n = noty({ text: result.Errors['Global'], type: 'error', timeout: 5000 }); return; }
}

function dialogajaxForm(link, form) {
    var target = link.attr('targetupdate') ? $(link.attr('targetupdate')) : link.parents('.updatepanel:first');
    var buttons = $('button', $('div.ui-dialog-buttonset'));
    
    buttons.each(function() {
        $(this).removeAttr('disabled');
    });

    form.ajaxForm({
        target: 'div.ui-dialog-content',
        type: 'POST',
        beforeSubmit: function (data, form) {
            buttons.each(function () {
                $(this).attr('disabled', 'disabled');
            });

            if (!form.valid()) {
                buttons.each(function () {
                    $(this).removeAttr('disabled');
                });

                return false;
            }
        },
        error: function (xhr, s) {
            showFormErrors($('form', $('div.ui-dialog-content')), xhr);
        },
        success: function (r, s, x, form) {
            adaptJQueryUIDialogContent();

            // Если это была форма редактирования пользователя и был изменен логин, изменяем его на странице.
            if ($(r).find('#CurrentUserName').val() == $(r).find('#Email').val()) {
                $('a#profile').text($(r).find('#Email').val());
            }

            if ($(r).find('input#ResizeImage').length > 0) {
                $('div.ui-dialog-content').trigger("dialogopen");
            }

            if (r.success == true || $(r).find('input#modelStateCount').val() == 0) {
                reloadPanel(target);

                if (link.attr('targetupdate') != 'div.ui-dialog-content') {
                    $('div.ui-dialog-content').dialog("close");
                }
            }
            else {
                form = $('form', $('div.ui-dialog-content'));
                dialogajaxForm(link, form);
            };
        }
    });
}

// Удаляем лишние элементы из большой версии (Подзаголовок и стандартные кнопки формы)
function adaptJQueryUIDialogContent() {
    $('div.contenthead', $('div.ui-dialog-content')).remove();
    $('div.form-bottom.btn-right', $('div.ui-dialog-content')).remove();
}

function changeaddress(element, attribute, param, value) {
    var address = element.attr(attribute);
    var first = address.indexOf('?' + param + '=') > -1;
    var nofirst = address.indexOf('&' + param + '=') > -1;
    if (nofirst || first) {
        var params = address.split('?')[1].split('&');
        var oldvalue;
        for (var p in params) {
            if (params[p].substring(0, param.length + 1) == param + '=') {
                oldvalue = params[p];
                break;
            }
        }
        if (value == '') {
            if (first)
                if (address.indexOf('&') > -1)
                    element.attr(attribute, address.replace(oldvalue, ''));
                else
                    element.attr(attribute, address.replace('?' + oldvalue, ''));
            if (nofirst)
                element.attr(attribute, address.replace('&' + oldvalue, ''));
        }
        else
            element.attr(attribute, address.replace(oldvalue, param + '=' + value));
    }
    else if (value != '')
        if (address.indexOf('?') > -1)
            element.attr(attribute, address + '&' + param + '=' + value);
        else
            element.attr(attribute, address + '?' + param + '=' + value);
}

$(function () {
    $('div.form-jquery-tabs').tabs({
        cache: true,
        create: function (event, ui) {
            BlockUIPage('Идёт загрузка...');
        },
        load: function (event, ui) {
            var panelId = ui.panel.get(0).id;
            
            $.unblockUI();
            $('div#' + panelId).trigger('loadtab');
            tabajaxForm(panelId);
        }
    });
});

function tabajaxForm(uipanelid) {
    var form = $('div#' + uipanelid).find('form');
    form.attr('autocomplete', 'off');
    if (form.length > 0) {
        form.ajaxForm({
            target: 'div#' + uipanelid,
            type: 'POST',
            beforeSubmit: function (data, form) {
                if (!form.valid()) {
                    return false;
                }
                BlockUIPage('Идёт сохранение...');
            },
            error: function (xhr, s) { BlockUIPage('Произошла ошибка!'); },
            success: function (r, s, x, form) {
                if (r.Successful == true || $(r).find('input#modelStateCount').val() == 0) {
                    BlockUIPage('Данные успешно сохранены');
                }
                UnBlockUIPage();
                tabajaxForm(uipanelid);
            }
        });
    }
}

function BlockUIPage(text) {
    var blockUI = $('div.blockUI');
    if (blockUI.length == 0) {
        $.blockUI({
            message: text,
            css: {
                border: 'none',
                padding: '15px',
                backgroundColor: '#000',
                width: '180px',
                height: '20px',
                left: '45%',
                opacity: .5,
                color: '#fff'
            }
        });
    }
    else {
        blockUI.text(text);
    }
}

function UnBlockUIPage() {
    setTimeout($.unblockUI, 1000);
}