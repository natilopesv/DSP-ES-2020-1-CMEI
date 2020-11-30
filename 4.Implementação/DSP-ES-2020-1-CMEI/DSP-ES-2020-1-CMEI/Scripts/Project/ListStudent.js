
$('#formStudent').unbind('submit').bind('submit', function (e) {
    e.preventDefault();

    if (!$(this).valid()) {
        return false;
    }
    else {
        var form = new FormData($('#formStudent')[0]);

        $.ajax({
            type: 'POST',
            url: '../Student/ImportStudent',
            data: form,
            processData: false,
            contentType: false,
            beforeSend: function () {

            },
            success: function (data) {
                if (data.type === 0) {
                    confirmMsgSuccess(data.msg, '../Student/ListStudent');
                }
                else if (data.type === 1) {
                    confirmMsgWarning(data.msg);
                }
                else {
                    confirmMsgError(data.msg);
                }
            },
            error: function (request, status, error) {
                confirmMsgError("Erro desconhecido");
            }
        });
    }
});

function exportStudent() {
    var form = new Object();
    form.listStudentModel = new Array();

    form.idLoginAccess = $("#idLoginAccess").val();

    $('#tblStudent > tbody > tr').each(function (i, linha) {
        var obj = new Object();

        obj.registrationNumber = $(linha).find('td:eq(0)').text();
        obj.nameStudent = $(linha).find('td:eq(1)').text();
        obj.birthDate = $(linha).find('td:eq(2)').text();
        obj.nameClassroomModel = $(linha).find('td:eq(3)').text();

        form.listStudentModel.push(obj);
    });

    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '../Student/ExportStudent',
        data: JSON.stringify({ form: form }),
        beforeSend: function () {

        },
        success: function (data) {
            if (data.type === 0) {
                //Gera um link com download do arquivo xml
                var arquivo = 'data:application/octet-stream;base64,' + data.excelBase64;
                var linkDownload = document.getElementById('downloadExcel');
                linkDownload.href = arquivo;
                linkDownload.click();
            }
            else if (data.type === 1) {
                confirmMsgWarning(data.msg);
            }
            else {
                confirmMsgError(data.msg);
            }
        },
        error: function (request, status, error) {
            confirmMsgError("Erro desconhecido");
        }
    });
}
