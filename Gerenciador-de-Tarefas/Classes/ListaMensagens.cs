using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gerenciador_de_Tarefas.Classes
{
    public static class ListaMensagens
    {
        private static string Lista(int mensagem)
        {
            string resultado = "";

            switch (mensagem)
            {
                case 01:
                    resultado = "Tarefa bloqueada : " +
                        "A tarefa foi aberta por outro usuário.\n\n" +
                        "Caso não tenha nenhum usuário utilizando o software e esta mensagem aparecer, vá no menu \"Ferramentas\" e clique em \"Destravar Tarefas\"." +
                        "\n\nOBS: Será solicitado uma senha, porém só os administradores possuem ela!";
                    break;
                case 02:
                    resultado = "Cancelar tarefa? : " +
                        "Você tem certeza de que deseja cancelar esta tarefa?\nTodas as informações inseridas aqui serão perdidas!";
                    break;
                case 03:
                    resultado = "Sair da tarefa? : " +
                        "Você tem certeza de que deseja sair desta tarefa?\nTodas as alterações serão perdidas!";
                    break;
                case 04:
                    resultado = "Excluir a tarefa? : " +
                        "Você tem certeza de que deseja excluir esta tarefa?\nTodas as informações dessa tarefa serão perdidas!";
                    break;
                case 05:
                    resultado = "Excluir a tarefa? : " +
                        "Tem certeza absoluta disso?\nEssa operação não poderá ser desfeita!" +
                        "\n\nPara continuar digite a senha de administrador.";
                    break;
                case 06:
                    resultado = "Cancelar cadastro? : " +
                        "Você tem certeza de que deseja cancelar este cadastro?\nTodas as informações inseridas aqui serão perdidas!";
                    break;
                case 07:
                    resultado = "Sair do contato? : " +
                        "Você tem certeza de que deseja sair deste contato?\nTodas as alterações serão perdidas!";
                    break;
                case 08:
                    resultado = "Excluir o contato? : " +
                        "Você tem certeza de que deseja excluir este contato?\nTodas as informações desse contato serão perdidas!" +
                        "\n\nOBS: Ao apagar o contato todas as tarefas relacionadas a ele também serão apagadas!";
                    break;
                case 09:
                    resultado = "Excluir o contato? : " +
                        "Tem certeza absoluta disso?\nEssa operação não poderá ser desfeita!" +
                        "\n\nPara continuar digite a senha de administrador.";
                    break;
                case 10:
                    resultado = "Excluir o contato? : " +
                        "Contato apagado com sucesso!";
                    break;
                case 11:
                    resultado = "Contato alterado : " +
                        "Contato alterado com sucesso!";
                    break;
                case 12:
                    resultado = "Novo contato : " +
                        "Contato cadastrado com sucesso!\n\nGostaria de cadastrar um novo contato?";
                    break;
                case 13:
                    resultado = "Atualização do sistema disponível : " +
                        "Há uma nova versão do sistema, gostaria de atualizá-lo agora?";
                    break;
                case 14:
                    resultado = "Excluir a tarefa? : " +
                        "A tarefa foi apagada com sucesso!";
                    break;
                case 15:
                    resultado = "Excluir a tarefa? : " +
                        "Tem certeza absoluta disso?\nEssa operação não poderá ser desfeita!";
                    break;
                case 16:
                    resultado = "Novo fornecedor : " +
                        "Fornecedor cadastrado com sucesso!\n\nGostaria de cadastrar um novo fornecedor?";
                    break;
                case 17:
                    resultado = "Fornecedor alterado : " +
                        "O fornecedor foi editado com sucesso!";
                    break;
                case 18:
                    resultado = "Sair do fornecedor : " +
                        "Você tem certeza de que deseja sair deste fornecedor?\nTodas as alterações serão perdidas!";
                    break;
                case 19:
                    resultado = "Excluir o fornecedor? : " +
                        "Você tem certeza de que deseja excluir este fornecedor?\nTodas as informações desse fornecedor serão perdidas!";
                    break;
                case 20:
                    resultado = "Excluir o fornecedor? : " +
                        "Tem certeza absoluta disso?\nEssa operação não poderá ser desfeita!" +
                        "\n\nPara continuar digite a senha de administrador.";
                    break;
                case 21:
                    resultado = "Excluir o fornecedor? : " +
                        "O fornecedor foi excluído com sucesso!";
                    break;
                case 22:
                    resultado = "Excluir o fornecedor? : " +
                        "Tem certeza absoluta disso?\nEssa operação não poderá ser desfeita!";
                    break;
                case 23:
                    resultado = "Atualizar fornecedor : " +
                        "Não há alterações no fornecedor!";
                    break;
                case 24:
                    resultado = "Alterar dados de conexão SQL : " +
                        "Os dados foram alterados com sucesso!";
                    break;
                case 25:
                    resultado = "Corrigir CEP? : " +
                        "O CEP informado não foi encontrado.\nDeseja corrigir o número do CEP?";
                    break;
                case 26:
                    resultado = "Excluir o contato? : " +
                        "Tem certeza absoluta disso?\nEssa operação não poderá ser desfeita!";
                    break;
                case 27:
                    resultado = "Texto está correto? : " +
                        "O texto que deseja inserir está correto?\nApós ele ser inserido não terá como ser editado!";
                    break;
                case 28:
                    resultado = "Cadastrar nova tarefa? : " +
                        "Deseja cadastrar uma nova tarefa?";
                    break;
                case 29:
                    resultado = "Tarefa alterada : " +
                        "Tarefa alterada com sucesso!";
                    break;
                case 30:
                    resultado = "Cadastrar Tarefa : " +
                        "Para cadastrar a tarefa, tanto o assunto quanto o texto não podem estar em branco!";
                    break;
                case 31:
                    resultado = "Destravar Tarefas? : " +
                        "Você tem certeza de que deseja destravar todas as tarefas?\nTodas as outras estações devem estar fechadas!";
                    break;
                case 32:
                    resultado = "Destravar Tarefas? : " +
                        "Para continuar digite a senha de administrador.";
                    break;
                case 33:
                    resultado = "Destravar Tarefas? : " +
                        "As tarefas foram desbloqueadas com sucesso!";
                    break;

            }

            return resultado;
        }

        /// <summary>
        /// Retorna um MessageBox com o a mensagem solicitada
        /// </summary>
        /// <param name="mensagem"></param>
        public static void RetornaMensagem(int mensagem, MessageBoxIcon icone)
        {
            string texto = Lista(mensagem);
            int separador = 0;
            separador = texto.IndexOf(":");
            MessageBox.Show(texto.Substring((separador + 2)), texto.Substring(0, (separador - 1)), MessageBoxButtons.OK, icone);
        }

        /// <summary>
        /// Retorna um DialogResult com o a mensagem solicitada
        /// </summary>
        /// <param name="mensagem"></param>
        public static DialogResult RetornaDialogo(int mensagem)
        {
            string texto = Lista(mensagem);
            int separador = 0;
            separador = texto.IndexOf(":");
            return MessageBox.Show(texto.Substring((separador + 2)), texto.Substring(0, (separador - 1)), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        /// <summary>
        /// Retorna uma matris de string com o a mensagem solicitada
        /// </summary>
        /// <param name="mensagem"></param>
        public static string[] RetornaInputBox(int mensagem)
        {
            string[] resultado = new string[2];
            string texto = Lista(mensagem);
            int separador = 0;
            separador = texto.IndexOf(":");

            resultado[0] = texto.Substring(separador + 2);
            resultado[1] = texto.Substring(0, (separador - 1));

            return resultado;
        }
    }
}
