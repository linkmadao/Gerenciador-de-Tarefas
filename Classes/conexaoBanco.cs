using System;
using System.Data;
using System.Collections.Generic;
using System.Xml.Linq;
using MySql.Data.MySqlClient;

namespace Classes
{
    public class conexaoBanco : IconexaoBanco
    {
        //Variável responsável pela conexão 
        private MySqlConnection conexao = null;
        //Nome do arquivo XML que guarda as informações de conexão com o banco de dados
        private string nomeXML = "bdconfig.xml";

        //String que guarda o erro de conexão gerado
        private string erroConexao = null;
        //String que guarda o servidor em que está sendo realizado a conexão
        private string servidor = null;
        //String que guarda o banco em que está conectado
        private string banco = null;
        //String do usuário utilizado na conexão
        private string uid = null;
        //String da senha utilizada na conexão
        private string pwd = null;

        //Propriedade responsável por armazenar e ler o erro de conexão gerado
        public string ErroConexao
        {
            get; set;
        }
        //Propriedade responsável por armazenar e ler os dados do servidor
        public string Servidor
        {
            get
            {
                return servidor;
            }
            set
            {
                servidor = value;
            }
        }
        //Propriedade responsável por armazenar e ler os dados do banco
        public string Banco
        {
            get
            {
                return banco;
            }
            set
            {
                banco = value;
            }
        }
        //Propriedade responsável por armazenar e ler os dados do usuário
        public string Uid
        {
            get
            {
                return uid;
            }
            set
            {
                uid = value;
            }
        }
        //Propriedade responsável por armazenar e ler os dados da senha
        public string Pwd
        {
            get
            {
                return pwd;
            }
            set
            {
                pwd = value;
            }
        }



        /// <summary>
        /// Método responsável pela abertura da conexão com o Banco de dados.
        /// </summary>
        private void AbreConexao()
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
        private void FechaConexao()
        {
            if (conexao.State == ConnectionState.Open)
            {
                conexao.Close();
            }
        }

        /// <summary>
        /// Método que realiza o teste de conexão com o Banco de dados.
        /// </summary>
        public bool TestaConexao()
        {
            bool resultado = false;

            try
            {
                AbreConexao();
            }
            catch (MySqlException ex)
            {
                ErroConexao = "Não foi possível conectar ao banco de dados da CFTV. Configure o acesso ao banco na tela a seguir...";
                FechaConexao();
                return resultado;
                throw;

            }
            finally
            {
                resultado = true;
                FechaConexao();
            }

            return resultado;
        }

        /// <summary>
        /// Método que realiza o teste de conexão com o Banco de dados.
        /// </summary>
        /// <param name="banco">Nome do banco de dados</param>
        /// <param name="uid">Usuário</param>
        /// <param name="pwd">Senha</param>
        public bool TestaConexao(string banco, string uid, string pwd)
        {
            bool resultado = false;

            try
            {
                AbreConexao();
            }
            catch (System.NullReferenceException e)
            {

                throw e;
            }
            catch (MySqlException ex)
            {
                ErroConexao = "Não foi possível conectar ao banco de dados da CFTV. Configure o acesso ao banco na tela a seguir...";
                FechaConexao();
                return resultado;
                throw ex;

            }
            finally
            {
                resultado = true;
                FechaConexao();
            }

            return resultado;
        }

        /// <summary>
        /// Método que realiza o teste de conexão com o Banco de dados.
        /// </summary>
        /// <param name="servidor">IP do Servidor (Pode ser usado para conexão local também, basta colocar 127.0.0.1)</param>
        /// <param name="banco">Nome do banco de dados</param>
        /// <param name="uid">Usuário</param>
        /// <param name="pwd">Senha</param>
        public bool TestaConexao(string servidor, string banco, string uid, string pwd)
        {
            bool resultado = false;

            try
            {
                AbreConexao();
            }
            catch (MySqlException ex)
            {
                ErroConexao = "Não foi possível conectar ao banco de dados da CFTV. Configure o acesso ao banco na tela a seguir...";
                FechaConexao();
                return resultado;
                throw;

            }
            finally
            {
                resultado = true;
                FechaConexao();
            }

            return resultado;
        }

    }
}
