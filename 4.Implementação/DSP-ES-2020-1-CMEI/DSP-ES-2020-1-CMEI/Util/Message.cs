namespace DSP_ES_2020_1_CMEI.Util
{
    public class Message
    {
        public const string ErrorInvalidLogin = "E-mail ou senha inválidos...";

        public const string ErrorSpreadsheetStudent = "A planilha está vazia ou é incompatível para essa operação..";

        public const string ErrorDuplicateStudent = "Existem estudantes nessa planilha que já foram importados...";

        public const string ErrorDuplicateClassroom = "Essa turma já existe...";

        public const string ErrorUnknown = "Erro desconhecido...";


        public const string WarningEvaluateIncompleteReport = "Só é possível gerar a avaliação se todos os alunos tiverem sido avaliados em 100% das atividade...";

        public const string WarningSelectClassroom = "Selecione uma turma para gerar o relatório;";

        public const string SuccessImportStudent = "Seus alunos foram importados...";

        public const string SuccessInsertClassroom = "Sua Turma foi inserida...";

        public const string SuccessUpdateClassroom = "Sua Turma foi alterada...";

        public const string SuccessDeleteClassroom = "Sua Turma foi excluída...";

        public const string SuccessEvaluate = "Sua avaliação foi realizada...";
    }
}