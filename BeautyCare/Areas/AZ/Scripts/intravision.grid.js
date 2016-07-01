$(function () {
    $('body').on('mouseenter', 'table.grid tr', function () { $(this).find('a.delete').show(); $(this).find('td').css({ background: '#e2f0fa !important' }); });
    $('body').on('mouseleave', 'table.grid tr', function () { $(this).find('a.delete').hide(); $(this).find('td').css({ background: '' }); });
    $('body').on('click', 'table.mvcgrid a.delete', function () { if (!confirm(_GridResources.ConfirmDeletion)) return false; var panel = $(this).parents('.updatepanel:first'); $.ajax({ type: 'DELETE', url: $(this).attr('href'), data: { __RequestVerificationToken: $(this).prev("input[name=__RequestVerificationToken]").attr("value") }, dataType: 'json', success: function (json) { if (json.Successful) intravision.reloadPanel(panel); else intravision.showGlobalErrors(json); } }); return false; });
    $('body').on('mouseenter focus', 'table.mvcgrid th > a', function () { $(this).addClass('subcontent-loader'); });
    $('body').on('mouseenter focus', 'div.pagination a', function () { $(this).addClass('subcontent-loader'); });
    $('body').on('keypress', 'div.search input[name="SearchString"]', function (e) { var code = e.keyCode || e.which; if (code == 13) { var input = $(this); var panel = input.parents('.updatepanel:first'); panel.load(panel.attr('data-action'), { SearchString: input.val() }, function () { panel.trigger('reload'); }); return false; } });
    $('body').on('click', 'div.search button', function () { var input = $('.search input'); var panel = input.parents('.updatepanel:first'); panel.load(panel.attr('data-action'), { SearchString: input.val() }, function () { panel.trigger('reload'); }); });
    $('body').on('click', 'div.grid-options-button button', function () { $(this).parents('.updatepanel:first').find('.grid-options').toggle(100); });
    $('body').on('click', 'div.grid-options button', function () { var panel = $(this).parents('.updatepanel:first'); var visible = new Array(); $('input[name="VisibleColumns"]:checked', panel).each(function (i, el) { visible.push(el.value); }); panel.load(panel.attr('data-action'), $('form', panel).serialize(), function () { panel.trigger('reload'); }); });
    $('body').on('click', 'a.grid-options-hide', function () { $(this).parents('div.grid-options').fadeOut(100); });
    $('body').on('change keypress', 'div.filter .filter-condition select', function () { var select = $(this); var fv = select.parent().next('.filter-value'); if (select.val() == 'False' || select.val() == 'True' || select.val() == 'Defined' || select.val() == 'Undefined') fv.hide(); else fv.show(); if (select.val() == 'Between') fv.find('input:nth-child(2)').show(); else fv.find('input:nth-child(2)').hide(); });
    $('body').on('click', 'div.filter .save-filter-button', function () { $('div.save-filter', $(this).parents('.filter:first')).toggle(100); });
    $('body').on('click', 'div.filter .save-filter-hide', function () { $('div.save-filter', $(this).parents('.filter:first')).fadeOut(100); });
    $('body').on('ajaxSubmitComplete', 'div.save-filter form', function () { $('.save-filter').hide(); });
    $('body').on('reload', 'div.filterpanel', function () {
        if (jQuery.multipleSelect != undefined) {
            jQuery.multipleSelect.initByClassName('.multipleselect', { init: true });
        }
    });
    $('.mvcgrid').parent('.updatepanel:first').bind('reload', initSort); initSort(); function initSort() { /* $('.grid-options ul').sortable({ handle: 'a.move', axis: 'y' }).disableSelection(); */ }
});

var _GridResources = {
    ConfirmDeletion: 'Вы уверены, что хотите удалить эту запись?'
};