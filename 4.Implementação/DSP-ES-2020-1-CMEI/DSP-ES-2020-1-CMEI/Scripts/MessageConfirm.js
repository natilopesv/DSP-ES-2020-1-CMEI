
function confirmMsgSuccess(msg, url) {
    $.confirm({
        title: 'Ok!',
        content: msg,
        theme: 'material',
        closeIcon: true,
        animation: 'opacity',
        closeAnimation: 'opacity',
        animateFromElement: false,
        type: 'green',
        buttons: {
            sim: {
                text: 'OK', //Fecha o modal de alerta
                keys: ['enter'],
                action: function() {
                    window.location.href = url;
                }
            }
        }
    });
}

function confirmMsgError(msg) {
    $.confirm({
        title: 'Ops!',
        content: msg,
        theme: 'material',
        closeIcon: true,
        animation: 'opacity',
        closeAnimation: 'opacity',
        animateFromElement: false,
        type: 'red',
        buttons: {
            sim: {
                text: 'OK', //Fecha o modal de alerta
                keys: ['enter'],
            }
        }
    });
}

function confirmMsgWarning(msg) {
    $.confirm({
        title: 'Atenção!',
        content: msg,
        theme: 'material',
        closeIcon: true,
        animation: 'opacity',
        closeAnimation: 'opacity',
        animateFromElement: false,
        type: 'orange',
        buttons: {
            sim: {
                text: 'OK', //Fecha o modal de alerta
                keys: ['enter'],
            }
        }
    });
}


function confirmMsgInformation(msg) {
    $.confirm({
        title: 'Informação!',
        content: msg,
        theme: 'material',
        closeIcon: true,
        animation: 'opacity',
        closeAnimation: 'opacity',
        animateFromElement: false,
        type: 'blue',
        buttons: {
            sim: {
                text: 'OK', //Fecha o modal de alerta
                keys: ['enter'],
            }
        }
    });
}
