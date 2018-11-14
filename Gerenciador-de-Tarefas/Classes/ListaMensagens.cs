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
                    resultado = "Tarefa Bloqueada : " +
                        "A tarefa foi aberta por outro usuário.\n\n" +
                        "Caso não tenha nenhum usuário utilizando o software e esta mensagem aparecer, vá no menu \"Ferramentas\" e clique em \"Destravar Tarefas\"." +
                        "\n\nOBS: Será solicitado uma senha, porém só os administradores possuem ela!";
                    break;
                case 02:
                    resultado = "Cancelar Tarefa? : " +
                        "Você tem certeza de que deseja cancelar esta tarefa?\nTodas as informações inseridas aqui serão perdidas!";
                    break;
                case 03:
                    resultado = "Sair da Tarefa? : " +
                        "Você tem certeza de que deseja sair desta tarefa?\nTodas as alterações serão perdidas!";
                    break;
                case 04:
                    resultado = "Excluir a Tarefa? : " +
                        "Você tem certeza de que deseja excluir esta tarefa?\nTodas as informações dessa tarefa serão perdidas!";
                    break;
                case 05:
                    resultado = "Excluir a Tarefa? : " +
                        "Tem certeza absoluta disso?\nEssa operação não poderá ser desfeita!" +
                        "\n\nPara continuar digite a senha de administrador.";
                    break;
                case 06:
                    resultado = "Cancelar Cadastro? : " +
                        "Você tem certeza de que deseja cancelar este cadastro?\nTodas as informações inseridas aqui serão perdidas!";
                    break;
                case 07:
                    resultado = "Sair do Contato? : " +
                        "Você tem certeza de que deseja sair deste contato?\nTodas as alterações serão perdidas!";
                    break;
                case 08:
                    resultado = "Excluir o Contato? : " +
                        "Você tem certeza de que deseja excluir este contato?\nTodas as informações desse contato serão perdidas!" +
                        "\n\nOBS: Ao apagar o contato todas as tarefas relacionadas a ele também serão apagadas!";
                    break;
                case 09:
                    resultado = "Excluir o Contato? : " +
                        "Tem certeza absoluta disso?\nEssa operação não poderá ser desfeita!" +
                        "\n\nPara continuar digite a senha de administrador.";
                    break;
                case 10:
                    resultado = "Excluir o Contato? : " +
                        "Contato apagado com sucesso!";
                    break;
                case 11:
                    resultado = "Contato Alterado : " +
                        "Contato alterado com sucesso!";
                    break;
                case 12:
                    resultado = "Novo Contato : " +
                        "Contato cadastrado com sucesso!\n\nGostaria de cadastrar um novo contato?";
                    break;
                case 13:
                    resultado = "Atualização do sistema disponível : " +
                        "Há uma nova versão do sistema, gostaria de atualizá-lo agora?";
                    break;
                case 14:
                    resultado = "Excluir a Tarefa? : " +
                        "A tarefa foi apagada com sucesso!";
                    break;
                case 15:
                    resultado = "Excluir a Tarefa? : " +
                        "Tem certeza absoluta disso?\nEssa operação não poderá ser desfeita!";
                    break;
                case 16:
                    resultado = "Novo Fornecedor : " +
                        "Fornecedor cadastrado com sucesso!\n\nGostaria de cadastrar um novo fornecedor?";
                    break;
                case 17:
                    resultado = "Fornecedor Alterado : " +
                        "O fornecedor foi editado com sucesso!";
                    break;
                case 18:
                    resultado = "Sair do Fornecedor : " +
                        "Você tem certeza de que deseja sair deste fornecedor?\nTodas as alterações serão perdidas!";
                    break;
                case 19:
                    resultado = "Excluir o Fornecedor? : " +
                        "Você tem certeza de que deseja excluir este fornecedor?\nTodas as informações desse fornecedor serão perdidas!";
                    break;
                case 20:
                    resultado = "Excluir o Fornecedor? : " +
                        "Tem certeza absoluta disso?\nEssa operação não poderá ser desfeita!" +
                        "\n\nPara continuar digite a senha de administrador.";
                    break;
                case 21:
                    resultado = "Excluir o Fornecedor? : " +
                        "O fornecedor foi excluído com sucesso!";
                    break;
                case 22:
                    resultado = "Excluir o Fornecedor? : " +
                        "Tem certeza absoluta disso?\nEssa operação não poderá ser desfeita!";
                    break;
                case 23:
                    resultado = "Atualizar Fornecedor : " +
                        "Não há alterações no fornecedor!";
                    break;
                case 24:
                    resultado = "Alterar Dados de Conexão SQL : " +
                        "Os dados foram alterados com sucesso!";
                    break;
                case 25:
                    resultado = "Corrigir CEP? : " +
                        "O CEP informado não foi encontrado.\nDeseja corrigir o número do CEP?";
                    break;
            }

            return resultado;
        }

        /// <summary>
        /// Retorna um MessageBox com o a mensagem solicitada
        /// </summary>
        /// <param name="mensagem"></param>
        public static void RetornaMensagem(int mensagem)
        {
            string texto = Lista(mensagem);
            int separador = 0;
            separador = texto.IndexOf(":");
            MessageBox.Show(texto.Substring((separador + 2)), texto.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
