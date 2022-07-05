$(document).ready(function () {
    $('select').selectize({
        sortField: 'text'
    });
    // to clear the selected value.
    $('form').find('.selectized').each(function (index, element) { element.selectize && element.selectize.clear() })
});