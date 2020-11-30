function removeEvaluateStudentGrade(handler) {
    var tr = $(handler).closest('tr');

    tr.fadeOut(300, function () {
        tr.remove();
    });
};

$('#formEvaluation').unbind('submit').bind('submit', function (e) {
    e.preventDefault();

    if (!$(this).valid()) {
        return false;
    }
    else {
        var form = new Object();
        form.listEvaluateStudentGrade = new Array();

        form.idLoginAccess = $("#idLoginAccess").val();
        form.idClassroom = $("#idClassroom").val();
        form.idStudent = $("#idStudent").val();
        form.idEvaluateStudent = $("#idEvaluateStudent").val();

        $('#tblEvaluateStudentGrade > tbody > tr').each(function (i, linha) {
            var obj = new Object();

            obj.idTeachingPlan = $(linha).find('td:eq(1)').text();
            obj.grade = $("#grade-" + obj.idTeachingPlan).val();

            form.listEvaluateStudentGrade.push(obj);
        });

        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: '../EvaluateStudent/ApplyEvaluation',
            data: JSON.stringify({ form: form }),
            beforeSend: function () {

            },
            success: function (data) {
                if (data.type === 0) {
                    confirmMsgSuccess(data.msg, '../EvaluateStudent/ListEvaluateStudent');
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
