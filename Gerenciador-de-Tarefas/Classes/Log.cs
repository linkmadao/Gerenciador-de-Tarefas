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

        public static void AbrirFornecedor(int usuario, int fornecedor)
        {
            string comando = "Insert into tbl_log values (0," + usuario + ", 'Abriu o fornecedor: " + fornecedor + " - " +
                DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "');";
            conexao.ExecutaComando(comando);
        }

        public static void Logoff(int usuario)
        {
            string comando = "Insert into tbl_log values (0," + usuario + ", 'Logoff efetuado - " +
                DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "');";
            conexao.ExecutaComando(comando);
        }

        public static void AbrirCliente()
        {
            string comando = "Insert into tbl_log values (0," + idUsuario + ", 'Abriu cliente ID: " + idCliente + " - " +
                DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "');";
            conexao.ExecutaComando(comando);
        }
        #endregion
    }
}
