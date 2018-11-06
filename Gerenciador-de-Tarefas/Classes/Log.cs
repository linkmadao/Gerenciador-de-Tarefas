using System;

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
        /// <param name="usuario">id do usuário</param>
        public static void Login(int usuario)
        {
            string comando = "Insert into tbl_log values (0," + usuario + ", 'Login efetuado - " +
                DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "');";
            conexao.ExecutaComando(comando);
        }

        /// <summary>
        /// Cria um log do logoff do usuário
        /// </summary>
        /// <param name="usuario">id do usuário</param>
        public static void Logoff(int usuario)
        {
            string comando = "Insert into tbl_log values (0," + usuario + ", 'Logoff efetuado - " +
                DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "');";
            conexao.ExecutaComando(comando);
        }
        #endregion

        public static void AbrirFornecedor(int usuario, int fornecedor)
        {
            string comando = "Insert into tbl_log values (0," + usuario + ", 'Abriu o fornecedor: " + fornecedor + " - " +
                DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "');";
            conexao.ExecutaComando(comando);
        }

        

        public static void AbrirCliente(int usuario, int cliente)
        {
            string comando = "Insert into tbl_log values (0," + usuario + ", 'Abriu cliente ID: " + cliente + " - " +
                DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "');";
            conexao.ExecutaComando(comando);
        }

        
        #endregion
    }
}
