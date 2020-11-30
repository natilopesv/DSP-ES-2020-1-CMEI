function readClassroom(idClassroom) {
    window.location.href = '../Classroom/ReadClassroom?idClassroom=' + idClassroom;
}

function updateClassroom(idClassroom) {
    window.location.href = '../Classroom/UpdateClassroom?idClassroom=' + idClassroom;
}

function deleteClassroom(idClassroom) {
    $("#modal").load("../Classroom/DeleteClassroom?idClassroom=" + idClassroom, function (response, status, xhr) {
        if (status === "error") {
            //Se a sessão estiver expirada o erro retornado será: request.statusText = Unauthorized, dessa forma a mensagem não deve ser disparada
            if (xhr.statusText !== "Unauthorized") {
                window.confirmMsgError("Erro desconhecido");
            }
        }
        else {
            $("#modal").modal();
        }
    })
}