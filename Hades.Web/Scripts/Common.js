
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
        Materialize.toast('' + mensagemAviso + '', 4000, 'rounded yellow');
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
