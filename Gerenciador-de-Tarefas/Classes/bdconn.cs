using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System;

namespace Gerenciador_de_Tarefas.Classes
{
    /// <summary>
    /// Classe responsável por realizar todas as ações referente ao banco de dados.
    /// </summary>
    public static class BDCONN
    {
        private static MySqlConnection conexao;
        //Nome do arquivo XML que guarda as informações de conexão com o banco de dados
        private static string nomeXML = "bdconfig.xml";

        //Propriedade responsável por armazenar e ler o erro de conexão gerado
        public static string ErroConexao
        {
            get; set;
        }
        //Propriedade responsável por armazenar e ler os dados do servidor
        public static string Servidor
        {
            get; set;
        }
        //Propriedade responsável por armazenar e ler os dados do banco
        public static string Banco
        {
            get; set;
        }
        //Propriedade responsável por armazenar e ler os dados do usuário
        public static string Uid
        {
            get; set;
        }
        //Propriedade responsável por armazenar e ler os dados da senha
        public static string Pwd
        {
            get; set;
        }



        /// <summary>
        /// Método responsável pela abertura da conexão com o Banco de dados.
        /// </summary>
        private static void AbreConexao()
        {
            if (Servidor == null)
            {
                XElement xml = XElement.Load(nomeXML);
                foreach (XElement x in xml.Elements())
                {
                    Servidor = x.Attribute("servidor").Value;
                    Banco = x.Attribute("banco").Value;
                    Uid = x.Attribute("uid").Value;
                    Pwd = x.Attribute("pwd").Value;
                }
            }

            string connString = "Server=" + Servidor + ";" +
                " Database=" + Banco + ";" +
                " Userid=" + Uid + ";" +
                " Password=" + Pwd + ";";

            conexao = new MySqlConnection(connString);

            if (conexao.State == ConnectionState.Closed)
            {
                conexao.Open();
            }
        }

        /// <summary>
        /// Método responsável pelo fechamento da conexão com o Banco de dados.
        /// </summary>
        private static void FechaConexao()
        {
            if (conexao.State == ConnectionState.Open)
            {
                conexao.Close();
            }
        }
    }
        
}