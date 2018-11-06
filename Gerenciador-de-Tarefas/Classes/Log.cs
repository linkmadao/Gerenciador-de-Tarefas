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
            string comando = "Insert into tbl_log values (0," + Sistema.UsuarioLogado + ", 'Login efetuado - " +
                DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "');";
            conexao.ExecutaComando(comando);
        }

        /// <summary>
        /// Cria um log do logoff do usuário
        /// </summary>
        public static void Logoff()
        {
            string comando = "Insert into tbl_log values (0," + Sistema.UsuarioLogado + ", 'Logoff efetuado - " +
                DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "');";
            conexao.ExecutaComando(comando);
        }
        #endregion

        public static void AbrirFornecedor(int fornecedor)
        {
            string comando = "Insert into tbl_log values (0," + Sistema.UsuarioLogado + ", 'Abriu o fornecedor: " + fornecedor + " - " +
                DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "');";
            conexao.ExecutaComando(comando);
        }
        
        public static void AbrirCliente(int cliente)
        {
            string comando = "Insert into tbl_log values (0," + Sistema.UsuarioLogado + ", 'Abriu cliente ID: " + cliente + " - " +
                DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "');";
            conexao.ExecutaComando(comando);
        }

        public static void CadastrarCliente(int cliente)
        {
            string comando = "Insert into tbl_log values (0," + Sistema.UsuarioLogado + ", 'Cadastrou o cliente ID: " + cliente + " - " +
                DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "');";
            conexao.ExecutaComando(comando);
        }

        public static void ApagarCliente(int cliente)
        {
            string comando = "Insert into tbl_log values (0," + Sistema.UsuarioLogado + ", 'Apagou o cliente ID: " + cliente + " - " +
                DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "');";
            conexao.ExecutaComando(comando);
        }
        #endregion
    }
}
