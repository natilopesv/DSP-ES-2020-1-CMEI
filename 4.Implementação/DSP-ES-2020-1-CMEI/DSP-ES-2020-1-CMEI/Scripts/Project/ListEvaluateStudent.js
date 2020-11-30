
function generateEvaluation() {
    var idClassroom = $("#idClassroom").val();

    $.get("../EvaluateStudent/GenerateReportEvaluate", { idClassroom: idClassroom })
        .done(function (data) {
            if (data.type === 0) {
                //Gera um link com download do arquivo xml
                var arquivo = 'data:application/octet-stream;base64,' + data.reportBase64;
                var linkDownload = document.getElementById('relatorioAvaliacao');
                linkDownload.href = arquivo;
                linkDownload.click();
            }
            else if (data.type === 1) {
                confirmMsgWarning(data.msg);
            }
            else {
                confirmMsgError(data.msg);
            }
        })
        .fail(function () {
            confirmMsgError("Erro desconhecido");
        })
}