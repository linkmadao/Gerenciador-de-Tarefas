using System;
using Gerenciador_de_Tarefas.Classes;

namespace Gerenciador_de_Tarefas.Classes
{
    public static class Log
    {
        #region Variaveis
        private static BDCONN conexao = new BDCONN();
        #endregion

        #region Propriedades
        public static int Usuario
        {
            get; set;
        }
        #endregion

        #region Funções
        #region Tela Login
        /// <summary>
        /// Cria um log do login do usuário
        /// </summary>
        public static void Login()
        {
            string comando = "Insert into tbl_log values (0," + Sistema.IDUsuarioLogado + ", 'Login efetuado - " +
                DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "');";
            conexao.ExecutaComando(comando);
        }

        /// <summary>
        /// Cria um log do logoff do usuário
        /// </summary>
        public static void Logoff()
        {
            string comando = "Insert into tbl_log values (0," + Sistema.IDUsuarioLogado + ", 'Logoff efetuado - " +
                DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "');";
            conexao.ExecutaComando(comando);
        }
        #endregion

        public static void AbrirTarefa(int tarefa)
        {
            string comando = "Insert into tbl_log values (0," + Sistema.IDUsuarioLogado + ", 'Abriu a Tarefa ID: " + tarefa + " - " +
                DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "');";
            conexao.ExecutaComando(comando);
        }

        public static void AbrirFornecedor(int fornecedor)
        {
            string comando = "Insert into tbl_log values (0," + Sistema.IDUsuarioLogado + ", 'Abriu o fornecedor ID: " + fornecedor + " - " +
                DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "');";
            conexao.ExecutaComando(comando);
        }
        
        public static void AbrirCliente()
        {
            string comando = "Insert into tbl_log values (0," + Sistema.IDUsuarioLogado + ", 'Abriu o cliente ID: " + Cliente.ID + " - " +
                DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "');";
            conexao.ExecutaComando(comando);
        }

        public static void CadastrarCliente()
        {
            string comando = "Insert into tbl_log values (0," + Sistema.IDUsuarioLogado + ", 'Cadastrou o cliente ID: " + Cliente.ID + " - " +
                DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "');";
            conexao.ExecutaComando(comando);
        }

        public static void AlterarCliente()
        {
            string comando = "Insert into tbl_log values (0," + Sistema.IDUsuarioLogado + ", 'Alterou o cliente ID: " + Cliente.ID + " - " +
                DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "');";
            conexao.ExecutaComando(comando);
        }

        public static void ApagarCliente()
        {
            string comando = "Insert into tbl_log values (0," + Sistema.IDUsuarioLogado + ", 'Apagou o cliente ID: " + Cliente.ID + " - " +
                DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "');";
            conexao.ExecutaComando(comando);
        }

        public static void AlterarDadosConexao()
        {
            string comando = "Insert into tbl_log values (0," + Sistema.IDUsuarioLogado + ", 'Alterou as informações de conexão SQL - " +
                DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "');";
            conexao.ExecutaComando(comando);
        }
        #endregion
    }
}
