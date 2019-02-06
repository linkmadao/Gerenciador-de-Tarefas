using System.Windows.Forms;

namespace Gerenciador_de_Tarefas.Classes
{
    public static class ListaErro
    {
        #region Funcoes
        private static string Lista(int erro)
        {
            string resultado = "";

            switch (erro)
            {
                case 01:
                    resultado = "Erro na Conexão pelo Banco de Dados : " +
                        "Não foi possível conectar ao banco de dados!\r\n\nConfigure o acesso ao banco na tela a seguir...";
                    break;
                case 02:
                    resultado = "Erro ao Preencher DGV : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 03:
                    resultado = "Erro ao Preencher CMB : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 04:
                    resultado = "Erro ao Executar Consulta Simples : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 05:
                    resultado = "Erro ao Consultar Tarefa : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 06:
                    resultado = "Erro ao Consultar Contato : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 07:
                    resultado = "Erro ao Consultar AI : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 08:
                    resultado = "Erro ao Travar a Tarefa : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 09:
                    resultado = "Erro ao Consultar Prioridade : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 10:
                    resultado = "Erro ao Consultar o Tipo de Contrato : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 11:
                    resultado = "Erro ao Executar Comando SQL : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 12:
                    resultado = "Erro ao Realizar Backup : " +
                        "Favor entrar em contato pelo suporte@cftva.com.br";
                    break;
                case 13:
                    resultado = "Erro ao Realizar Restauração : " +
                        "Favor entrar em contato pelo suporte@cftva.com.br";
                    break;
                case 14:
                    resultado = "Configuração não Executada : " +
                        "O programa não foi configurado corretamente e por isso ele será fechado!\r\n\nPara mais dúvidas, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 15:
                    resultado = "Erro ao Abrir a Tarefa : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 16:
                    resultado = "Erro ao Cadastrar a Tarefa : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 17:
                    resultado = "Erro ao Atualizar a Tarefa : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 18:
                    resultado = "Erro ao Apagar a Tarefa : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 19:
                    resultado = "Erro ao Destravar Todas as Tarefa : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 20:
                    resultado = "Erro ao Abrir o Contato : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 21:
                    resultado = "Erro ao Otimizar as Tabelas do Banco de Dados : " +
                        "Favor entrar em contato pelo suporte@cftva.com.br";
                    break;
                case 22:
                    resultado = "Erro ao Acessar o Arquivo Informado : " +
                        "Verifique se você tem mesmo a permissão para acessar o/a arquivo/pasta!\n\nFavor entrar em contato pelo suporte@cftva.com.br";
                    break;
                case 23:
                    resultado = "Erro ao Preparar o Documento para Impressão : " +
                        "Há algum erro no texto ou na configuração da página!\n\nFavor entrar em contato pelo suporte@cftva.com.br";
                    break;
                case 24:
                    resultado = "Erro ao Imprimir o Documento : " +
                        "Há algum erro no texto ou na configuração da página!\n\nFavor entrar em contato pelo suporte@cftva.com.br";
                    break;
                case 25:
                    resultado = "Usuário Inválido : " +
                        "O usuário não pode estar vazio.\nPor favor, selecione um usuário!";
                    break;
                case 26:
                    resultado = "Senha Inválida : " +
                        "A senha não pode estar vazia.\nPor favor, insira uma senha!";
                    break;
                case 27:
                    resultado = "Senha Incompleta : " +
                        "A senha deve conter 4 caracteres.\nPor favor, corrija a senha!";
                    break;
                case 28:
                    resultado = "Quantidade de Caracteres Insuficiente : " +
                        "O assunto possui menos do que 5 caracteres!\nPor favor, insira um assunto maior do que 5 caracteres.";
                    break;
                case 29:
                    resultado = "Texto Inválido : " +
                        "O texto não pode estar vazio!\nPor favor, coloque ao menos um ponto.";
                    break;
                case 30:
                    resultado = "O diretório a seguir não existe ou não pode ser acessado : ";
                    break;
                case 31:
                    resultado = "Fornecedor não Encontrado : " +
                        "O fornecedor que você buscou não foi encontrado.\nPor favor, verifique o texto informado e tente novamente.";
                    break;
                case 32:
                    resultado = "Erro ao colorir a tabela de tarefas : " +
                        "Por favor reportar ao suporte técnico.";
                    break;
                case 33:
                    resultado = "Razão Social do Fornecedor Inválido : " +
                        "Por favor, insira a razão social do fornecedor corretamente.";
                    break;
                case 34:
                    resultado = "Quantidade de Caracteres Insuficiente : " +
                        "O nome do contato possui menos do que 3 caracteres!\nPor favor, insira um nome maior do que 3 caracteres.";
                    break;
                case 35:
                    resultado = "Nome do Fornecedor Inválido : " +
                        "Por favor, insira o nome do fornecedor corretamente.";
                    break;
                case 36:
                    resultado = "Quantidade de Caracteres Insuficiente : " +
                        "A razão social do fornecedor possui menos do que 3 caracteres!\nPor favor, insira uma razão social maior do que 3 caracteres.";
                    break;
                case 37:
                    resultado = "Erro ao Apagar o Contato : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 38:
                    resultado = "Erro ao Atualizar o Contato : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 39:
                    resultado = "Erro ao Cadastrar o Contato : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 40:
                    resultado = "CNPJ Incompleto : " +
                        "O CNPJ informado esta incompleto!\nPor favor, digite-o novamente.";
                    break;
                case 41:
                    resultado = "CNPJ Inválido : " +
                        "O CNPJ informado é inválido!\nPor favor, digite-o novamente.";
                    break;
                case 42:
                    resultado = "CPF Incompleto : " +
                        "O CPF informado esta incompleto!\nPor favor, digite-o novamente.";
                    break;
                case 43:
                    resultado = "CPF Inválido : " +
                        "O CPF informado é inválido!\nPor favor, digite-o novamente.";
                    break;
                case 44:
                    resultado = "CEP Inválido : " +
                        "O CEP informado é inválido!\nPor favor, digite-o novamente.";
                    break;
                case 46:
                    resultado = "CEP Inexistente : " +
                        "O CEP informado existe.\nPor favor, digite um CEP válido.";
                    break;
                case 47:
                    resultado = "Usuário/Senha Inválido(a) : " +
                        "O usuário/senha informado(a) está incorreto(a).\nPor favor, tente novamente.";
                    break;
                case 48:
                    resultado = "Cliente não Encontrado : " +
                        "O cliente que você buscou não foi encontrado.\nPor favor, verifique o texto informado e tente novamente.";
                    break;
                case 49:
                    resultado = "Erro ao filtrar a tabela de tarefas : " +
                        "Por favor reportar ao suporte técnico qual filtro foi utilizado antes de gerar esse erro.";
                    break;
                case 50:
                    resultado = "Quantidade de Caracteres Insuficiente : " +
                        "O nome do fornecedor possui menos do que 3 caracteres!\nPor favor, insira um nome maior do que 3 caracteres.";
                    break;
                case 51:
                    resultado = "Erro ao Cadastrar o Fornecedor : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 52:
                    resultado = "Erro ao Travar o Fornecedor : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 53:
                    resultado = "Erro ao pesquisar o CNPJ : " +
                        "O sistema da receita federal pode estar fora do ar ou o CNPJ informado não tem cadastro na receita federal;" +
                        "Caso o CNPJ seja verdadeiro e o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 54:
                    resultado = "Erro ao Carregar o Fornecedor : " +
                        "O fornecedor foi aberto por outro usuário.\n\n" +
                        "Caso não tenha nenhum usuário utilizando o software e esta mensagem aparecer, vá no menu \"Ferramentas\" e clique em \"Destravar Fornecedores\"." +
                        "\n\nOBS: Será solicitado uma senha, porém só os administradores possuem ela!";
                    break;
                case 55:
                    resultado = "Erro ao Consultar Fornecedor : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 56:
                    resultado = "Erro ao Atualizar Fornecedor : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 57:
                    resultado = "Erro ao Apagar o Fornecedor : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 58:
                    resultado = "Erro ao Imprimir o Documento : " +
                        "Erro de formato em algum texto da página!\n\nFavor entrar em contato pelo suporte@cftva.com.br";
                    break;
                case 59:
                    resultado = "Erro na Conexão com o Banco de Dados : " +
                        "Não foi possível conectar ao banco de dados!\r\n\nVerifique se os dados digitados estão corretos e tente novamente.\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 60:
                    resultado = "Erro ao Ler as Informações do Arquivo de Conexão com o Banco de Dados : " +
                        "Não foi possível ler os dados de conexão com o banco de dados!\r\n\nFavor reportar esse erro pelo suporte@cftva.com.br";
                    break;
                case 61:
                    resultado = "Erro ao preencher a tabela de tarefas : " +
                        "Não foi possível preencher a tabela de tarefas devido a erro de pesquisa SQL ou algo relacionado!\r\n\nFavor reportar esse erro pelo suporte@cftva.com.br";
                    break;
                case 62:
                    resultado = "Erro ao cadastrar/salvar a tarefa : " +
                        "Não foi possível cadastrar/salvar a tarefa devido a um erro de SQL ou algo do tipo!\r\n\nFavor reportar esse erro pelo suporte@cftva.com.br";
                    break;
                case 63:
                    resultado = "Quantidade de Caracteres Insuficiente : " +
                        "O texto a ser inserido possui menos do que 3 caracteres!\nPor favor, insira um texto que possua mais do que 3 caracteres.";
                    break;
                case 64:
                    resultado = "Erro ao alterar a tarefa : " +
                        "Não foi possível alterar a tarefa!\nFavor reportar esse erro pelo suporte@cftva.com.br.";
                    break;
            }
            
            return resultado;
        }

        /// <summary>
        /// Retorna um MessageBox com o erro solicitado
        /// </summary>
        /// <param name="erro"></param>
        public static void RetornaErro(int erro)
        {
            string texto = Lista(erro);
            int separador = 0;
            separador = texto.IndexOf(":");
            MessageBox.Show(texto.Substring((separador + 2)), texto.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion
    }
}
