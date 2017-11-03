
$(document).ready(function () {
    $('.tooltipped').tooltip({ delay: 50 });
});

function verificaCaracteres(campo) {
    setTimeout(function () {
        var texto = $(campo).val();
        if (!new RegExp("^[\\sa-zA-Z0-9-Zàèìòùáéíóúâêîôûãõ@@ç:,!?.\b]+$").test(texto)) {
            $(campo).val(texto.substring(0, (texto.length - 1)));
        }
    }, 100);
}


function mensagem(mensagemAviso) {
    if (mensagemAviso != '') {
        Materialize.toast('' + mensagemAviso + '', 4000, 'rounded amber accent-4');
    }
};


function sucesso(message) {
    Materialize.toast('' + message + '', 4000, 'rounded green');
    return true;
}

function erro(message) {
    Materialize.toast('' + message + '', 4000, 'rounded red');
    return true;
}

function startLoading() {
    $("body").prepend(getHtmlLoading());
    $('.preloader-background, .preloader-wrapper').fadeIn();
}

function stopLoading() {
    $('.preloader-background, .preloader-wrapper').fadeOut('slow');
    $(".preloader-background").remove();
}

function getHtmlLoading() {

    return '<div class="preloader-background">'
        + '<div class="preloader-wrapper big active">'
        + '<div class="spinner-layer spinner-blue-only">'
        + '<div class="circle-clipper left">'
        + '<div class="circle"></div>'
        + '</div>'
        + '<div class="gap-patch">'
        + '<div class="circle"></div>'
        + '</div>'
        + '<div class="circle-clipper right">'
        + '<div class="circle"></div>'
        + '</div>'
        + '</div>'
        + '</div>'
        + '</div>';
}

$(document).ajaxStart(startLoading).ajaxStop(stopLoading).ajaxError(function (event, xhr) {
    //$.toast({ message: xhr.responseText || "Falha ao realizar operação" });
});
