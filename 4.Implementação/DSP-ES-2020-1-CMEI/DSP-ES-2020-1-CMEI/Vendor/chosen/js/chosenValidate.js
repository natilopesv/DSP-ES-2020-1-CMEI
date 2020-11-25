////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Validação do chosen personalizada ------------------------------------------------------------------------//

//Função chamada nos submits dos forms que possuem chosen
function validateChosen() {
    //Percorre todos os campos chosen que estão sujeitos a validação para aplicar a classe de validação
    $(".chosen-select.input-validation-error").each(function () {
        var id = $(this).attr('id');
        $("#" + id + "_chosen").addClass("chosen-validation-error");

        //Precisa de um tempo até que a tela seja carregada completamente
        setTimeout(function () {
            //Seta o focus no campo chosen de empresas
            $('.chosen-validation-error > .chosen-drop .chosen-search input[type="text"]').focus();
            $('.chosen-validation-error').addClass('chosen-container-active');
        }, 50);
    });
}

//Onchange disparado toda vez que um campo chosen tem seu valor alterado: usado para fins de validação
$('.chosen-select').on('change', function (evt, params) {
    //Se o campo em questão tiver essa classe significa que as alterações de validação devem ser verificadas
    if (this.className.indexOf("input-validation-error") > -1) {
        //Se for escolhido um valor para o chosen que está requerid no formulário, suas classes de validação
        //devem ser limpas, retornando ao seu estado natural
        if (this.value !== "") {
            var id = $(this).attr('id');

            $("#" + id + "_chosen").removeClass("chosen-validation-error");
            $('span[data-valmsg-for="' + this.name + '"]').text("");
        }
        //Se não for escolhido um valor ele deve permanecer validation-error
        else {
            var id = $(this).attr('id');

            $("#" + id + "_chosen").addClass("chosen-validation-error");
            $('span[data-valmsg-for="' + this.name + '"]').text($(this).attr("data-val-required"));
        }
    }
    //Se o campo em questão tiver essa classe significa que o formulário já está em processo de validação
    //Logo se o valor desse campo for alterado para vazio então precisamos torná-lo validation-error novamente sem que haja necessidade de dar post no form
    else if (this.className.indexOf("valid") > -1 && $(this).attr("data-val-required") !== undefined) {
        //Se não houver valor no campo deve ser disparada a validação
        if (this.value === "") {
            var id = $(this).attr('id');

            $("#" + id + "_chosen").addClass("chosen-validation-error");
            $('span[data-valmsg-for="' + this.name + '"]').text("Campo obrigatório");
        }
        //Se houver valor no campo não deve ser disparada a validação
        else {
            var id = $(this).attr('id');

            $("#" + id + "_chosen").removeClass("chosen-validation-error");
            $('span[data-valmsg-for="' + this.name + '"]').text("");
        }
    }
});