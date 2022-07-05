$(document).ready(function () {
    $('.modal').on('shown.bs.modal', function () {
        $(this).find('[autofocus]').focus();
    });
    $('.modal').on('show.bs.modal', function (e) {
        var activeElement = document.activeElement;
        $(this).on('hidden.bs.modal', function () {
            activeElement.focus();
            $(this).off('hidden.bs.modal');
        });
    });
    
});


