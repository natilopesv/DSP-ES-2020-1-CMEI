
var listTeachingPlanDelete = new Array();
var contStudent = 0;
var contTeachingPlan = 0;

function addStudentTable() {
    var idStudent = $("#student option:selected").val();
    var nameStudent = $("#student option:selected").text();

    contStudent++;

    var tr = $('<tr></tr>');

    tr.append('<td>' + contStudent + '</td>');
    tr.append('<td>' + idStudent + '</td>');
    tr.append('<td>' + nameStudent + '</td>');
    tr.append('<td><button class="btn btn-xs btn-danger" onclick="removeStudent(this)" type="button"><i class="fas fa-minus-circle"></i> Remover</button></td>');

    $("#tblStudent").append(tr);
}

function addTeachingPlanTable() {
    var activityDescription = $("#activityDescription").val();

    contTeachingPlan++;

    var tr = $('<tr></tr>');

    tr.append('<td>' + contTeachingPlan + '</td>');
    tr.append('<td>---</td>');
    tr.append('<td><input type="tel" class="form-control" id="activityDescription-' + contTeachingPlan + '" value = "' + activityDescription + '" ></td>');
    tr.append('<td><button class="btn btn-xs btn-danger" onclick="removeTeachingPlan(this)" type="button"><i class="fas fa-minus-circle"></i> Remover</button></td>');

    $("#tblTeachingPlan").append(tr);

    $("#activityDescription").val("");
    $("#activityDescription").focus();
}

function removeStudent(handler) {
    var tr = $(handler).closest('tr');

    tr.fadeOut(300, function () {
        tr.remove();
    });
};

function removeTeachingPlan(handler, idTeachingPlan) {
    var tr = $(handler).closest('tr');

    tr.fadeOut(300, function () {
        tr.remove();
    });

    if (idTeachingPlan !== undefined && idTeachingPlan !== "---") {
        var obj = new Object();

        obj.idTeachingPlan = idTeachingPlan;
        listTeachingPlanDelete.push(obj);
    }
};

$('#formCreateClassroom').unbind('submit').bind('submit', function (e) {
    e.preventDefault();

    if (!$(this).valid()) {
        return false;
    }
    else {
        var form = new Object();
        form.listClassroomStudent = new Array();
        form.listTeachingPlan = new Array();

        form.idLoginAccess = $("#idLoginAccess").val();
        form.nameClassroom = $("#nameClassroom").val();
        form.shiftClassroom = $("#shiftClassroom").val();

        $('#tblStudent > tbody > tr').each(function (i, linha) {
            var obj = new Object();
            obj.Student = new Object();

            obj.idStudent = $(linha).find('td:eq(1)').text();
            obj.Student.nameStudent = $(linha).find('td:eq(2)').text();

            form.listClassroomStudent.push(obj);
        });

        $('#tblTeachingPlan > tbody > tr').each(function (i, linha) {
            var obj = new Object();

            obj.cont = $(linha).find('td:eq(0)').text();
            obj.activityDescription = $("#activityDescription-" + obj.cont).val();

            form.listTeachingPlan.push(obj);
        });

        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: '../Classroom/CreateClassroom',
            data: JSON.stringify({ form: form }),
            beforeSend: function () {

            },
            success: function (data) {
                if (data.type === 0) {
                    confirmMsgSuccess(data.msg, '../Classroom/ListClassroom');
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

$('#formUpdateClassroom').unbind('submit').bind('submit', function (e) {
    e.preventDefault();

    if (!$(this).valid()) {
        return false;
    }
    else {
        var form = new Object();
        form.listClassroomStudent = new Array();
        form.listTeachingPlan = new Array();
        form.listTeachingPlanDelete = new Array();

        form.idLoginAccess = $("#idLoginAccess").val();
        form.idClassroom = $("#idClassroom").val();
        form.nameClassroom = $("#nameClassroom").val();
        form.shiftClassroom = $("#shiftClassroom").val();

        $('#tblStudent > tbody > tr').each(function (i, linha) {
            var obj = new Object();
            obj.Student = new Object();

            obj.idStudent = $(linha).find('td:eq(1)').text();
            obj.Student.nameStudent = $(linha).find('td:eq(2)').text();

            form.listClassroomStudent.push(obj);
        });

        $('#tblTeachingPlan > tbody > tr').each(function (i, linha) {
            var obj = new Object();

            obj.cont = $(linha).find('td:eq(0)').text();
            obj.idTeachingPlan = $(linha).find('td:eq(1)').text();
            obj.activityDescription = $("#activityDescription-" + obj.cont).val();

            form.listTeachingPlan.push(obj);
        });

        form.listTeachingPlanDelete = listTeachingPlanDelete;

        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: '../Classroom/UpdateClassroom',
            data: JSON.stringify({ form: form }),
            beforeSend: function () {

            },
            success: function (data) {
                if (data.type === 0) {
                    confirmMsgSuccess(data.msg, '../Classroom/ListClassroom');
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

$('#formDeleteClassroom').unbind('submit').bind('submit', function (e) {
    e.preventDefault();

    if (!$(this).valid()) {
        return false;
    }
    else {
        var form = new Object();

        form.idClassroom = $("#idClassroom").val();

        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: '../Classroom/DeleteClassroom',
            data: JSON.stringify({ form: form }),
            beforeSend: function () {

            },
            success: function (data) {
                if (data.type === 0) {
                    confirmMsgSuccess(data.msg, '../Classroom/ListClassroom');
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