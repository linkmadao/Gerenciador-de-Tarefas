using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System;

namespace Gerenciador_de_Tarefas
{
    /// <summary>
    /// Classe responsável por realizar todas as ações referente ao banco de dados.
    /// </summary>
    public class BDCONN
    {
        private MySqlConnection conexao;
        //Nome do arquivo XML que guarda as informações de conexão com o banco de dados
        private string nomeXML = "bdconfig.xml";

        //Propriedade responsável por armazenar e ler o erro de conexão gerado
        public string ErroConexao
        {
            get; set;
        }
        //Propriedade responsável por armazenar e ler os dados do servidor
        public string Servidor
        {
            get; set;
        }
        //Propriedade responsável por armazenar e ler os dados do banco
        public string Banco
        {
            get; set;
        }
        //Propriedade responsável por armazenar e ler os dados do usuário
        public string Uid
        {
            get; set;
        }
        //Propriedade responsável por armazenar e ler os dados da senha
        public string Pwd
        {
            get; set;
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
            catch (MySqlException)
            {
                string erro = ListaErro.RetornaErro(01);
                int separador = erro.LastIndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Configuracoes_Banco config = new Configuracoes_Banco(true);
                config.ShowDialog();
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
            catch (MySqlException)
            {
                string erro = ListaErro.RetornaErro(01);
                int separador = erro.LastIndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Configuracoes_Banco config = new Configuracoes_Banco(true);
                config.ShowDialog();
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
            catch (MySqlException)
            {
                string erro = ListaErro.RetornaErro(01);
                int separador = erro.LastIndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Configuracoes_Banco config = new Configuracoes_Banco(true);
                config.ShowDialog();
            }
            finally
            {
                resultado = true;
                FechaConexao();
            }

            return resultado;
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
            if(conexao.State == ConnectionState.Open)
            {
                conexao.Close();
            }
        }

        /// <summary>
        /// Método responsável por preencher a tabela na Tela inicial.
        /// </summary>
        /// <param name="comando">Comando que será consultado no banco de dados.</param>
        public DataSet PreencheDGV(string comando)
        {
            DataSet resultado = new DataSet();

            try
            {
                AbreConexao();

                MySqlDataAdapter dA = new MySqlDataAdapter(comando, conexao);
                dA.Fill(resultado);
            }
            catch (MySqlException)
            {
                string erro = ListaErro.RetornaErro(02);
                int separador = erro.LastIndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                FechaConexao();
            }
            finally
            {
                FechaConexao();
            }

            return resultado;
        }

        /// <summary>
        /// Método responsável por preencher as combobox nas telas.
        /// </summary>
        /// <param name="comando">Comando que será consultado no banco de dados.</param>
        public List<string> PreencheCMB(string comando)
        {
            List<string> resultado = new List<string>();

            try
            {
                AbreConexao();

                MySqlCommand cmd = new MySqlCommand(comando, conexao);
                MySqlDataReader dR;

                dR = cmd.ExecuteReader();

                while (dR.Read())
                {
                    resultado.Add(dR.GetString("Nome"));
                }
            }
            catch (MySqlException)
            {
                string erro = ListaErro.RetornaErro(03);
                int separador = erro.LastIndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                FechaConexao();
            }
            finally
            {
                FechaConexao();
            }

            return resultado;
        }

        /// <summary>
        /// Método responsável por retornar o ID do item especificado.
        /// </summary>
        /// <param name="comando">Comando que será consultado no banco de dados.</param>
        public string ConsultaSimples(string comando)
        {
            string resultado = null;

            try
            {
                AbreConexao();

                MySqlCommand cmd = new MySqlCommand(comando, conexao);
                resultado = cmd.ExecuteScalar().ToString();
            }
            catch (NullReferenceException)
            {
                string erro = ListaErro.RetornaErro(04);
                int separador = erro.LastIndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                FechaConexao();
            }
            catch (MySqlException)
            {
                string erro = ListaErro.RetornaErro(04);
                int separador = erro.LastIndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                FechaConexao();
            }
            finally
            {
                FechaConexao();
            }

            return resultado;
        }

        /// <summary>
        /// Método responsável por retornar a tarefa solicitada.
        /// </summary>
        /// <param name="comando">Comando que será consultado no banco de dados.</param>
        public List<string> ConsultaTarefas(string comando)
        {
            List<string> resultado = new List<string>();

            try
            {
                AbreConexao();

                MySqlCommand cmd = new MySqlCommand(comando, conexao);
                MySqlDataReader dR;

                dR = cmd.ExecuteReader();
                
                while (dR.Read())
                {
                    for(int i = 0; i < 8; i++)
                    {
                        if(dR.GetString(i) == null)
                        {
                            resultado.Add("");
                        }
                        else
                        {
                            resultado.Add(dR.GetString(i));
                        }
                    }
                }
            }
            catch (MySqlException)
            {
                string erro = ListaErro.RetornaErro(05);
                int separador = erro.LastIndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                FechaConexao();
            }
            finally
            {
                FechaConexao();
            }

            return resultado;
        }

        /// <summary>
        /// Método responsável por retornar a tarefa solicitada.
        /// </summary>
        /// <param name="comando">Comando que será consultado no banco de dados.</param>
        public List<string> ConsultaFornecedor(string comando)
        {
            List<string> resultado = new List<string>();

            try
            {
                AbreConexao();

                MySqlCommand cmd = new MySqlCommand(comando, conexao);
                MySqlDataReader dR;

                dR = cmd.ExecuteReader();

                while (dR.Read())
                {
<<<<<<< HEAD
                    for (int i = 0; i < 31; i++)
=======
                    for (int i = 0; i < 25; i++)
>>>>>>> 52d342db164d391dbc84c5835e0cf98955f0a9b5
                    {
                        if(dR.IsDBNull(i))
                        {
                            resultado.Add("");
                        }
                        else
                        {
                            if(dR.GetString(i) == null)
                            {
                                resultado.Add("");
                            }
                            else
                            {

                            }
                            resultado.Add(dR.GetString(i).ToString());
                        }
                    }
                }

                dR.Close();
            }


            catch (MySqlException)
            {
                string erro = ListaErro.RetornaErro(55);
                int separador = erro.LastIndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                FechaConexao();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("AQUI");
            }
            finally
            {
                FechaConexao();
            }

            return resultado;
        }

        /// <summary>
        /// Método responsável por retornar o contato solicitado.
        /// </summary>
        /// <param name="comando">Comando que será consultado no banco de dados.</param>
        public List<string> ConsultaContato(string comando)
        {
            List<string> resultado = new List<string>();

            try
            {
                AbreConexao();

                MySqlCommand cmd = new MySqlCommand(comando, conexao);
                MySqlDataReader dR;

                dR = cmd.ExecuteReader();

                while (dR.Read())
                {
                    for (int i = 0; i < 21; i++)
                    {
                        if (dR.IsDBNull(i))
                        {
                            resultado.Add("");
                        }
                        else
                        {
                            resultado.Add(dR.GetString(i));
                        }
                    }
                }
            }
            catch (MySqlException)
            {
                string erro = ListaErro.RetornaErro(06);
                int separador = erro.LastIndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                FechaConexao();
            }
            finally
            {
                FechaConexao();
            }

            return resultado;
        }

        /// <summary>
        /// Método responsável por retornar o id AutoIncrement do MYSQL da tabela selecionada
        /// </summary>
        /// <param name="tipo">1:Clientes/Fornecedores, 2:Ordem de serviço</param>
        /// <returns></returns>
        public string ConsultaAutoIncrement(int tipo)
        {
            string resultado = null;

            string comando = "SELECT `AUTO_INCREMENT` " +
                    "FROM INFORMATION_SCHEMA.TABLES " +
                    "WHERE TABLE_SCHEMA = 'gerenciatarefa' ";

            switch (tipo)
            {
                case 1:
                    comando += "AND TABLE_NAME = 'tbl_contato';";
                    break;
                case 2:
                    break;
            }

            try
            {
                AbreConexao();
            }
            catch (MySqlException)
            {
                string erro = ListaErro.RetornaErro(07);
                int separador = erro.LastIndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                FechaConexao();
            }
            finally
            {
                MySqlCommand cmd = new MySqlCommand(comando, conexao);

                resultado = cmd.ExecuteScalar().ToString();

                FechaConexao();
            }

            return resultado;
        }

        /// <summary>
        /// Método responsável por retornar se a tarefa esta bloqueada.
        /// </summary>
        /// <param name="idTarefa">Qual a tarefa que deseja conferir.</param>
        public bool TarefaBloqueada(int idTarefa)
        {
            bool resultado = false;

            string comando = "Select travar from tbl_tarefas where id = " + idTarefa.ToString() + ";";

            try
            {
                AbreConexao();

                MySqlCommand cmd = new MySqlCommand(comando, conexao);

                if(!string.IsNullOrEmpty(cmd.ExecuteScalar().ToString()))
                {
                    if (cmd.ExecuteScalar().ToString() == "S")
                    {
                        resultado = true;
                    }
                }
            }
            catch (NullReferenceException)
            {
                resultado = false;
                return resultado;
            }
            catch (MySqlException)
            {
                string erro = ListaErro.RetornaErro(08);
                int separador = erro.LastIndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                FechaConexao();
            }
            finally
            {
                FechaConexao();
            }

            return resultado;
        }

        /// <summary>
        /// Método responsável por retornar a prioridade da tarefa solicitada.
        /// </summary>
        /// <param name="empresa">Nome da empresa</param>
        /// <param name="funcionario">Nome do funcionário responsável</param>
        /// <param name="assunto">Assunto da tarefa</param>
        public string ConsultaPrioridade(string empresa, string funcionario, string assunto)
        {
            int idEmpresa = 0, idFuncionario = 0, idTarefa = 0;
            string comando = null, resultado = null;

            comando = "Select ID from tbl_contato where nome = '" + empresa + "';";
            idEmpresa = Int32.Parse(ConsultaSimples(comando));

            comando = "Select ID from tbl_funcionarios where nome = '" + funcionario + "';";
            idFuncionario = Int32.Parse(ConsultaSimples(comando));

            comando = "Select ID from tbl_tarefas where empresa = '" + idEmpresa + "'" 
                + " AND funcionario = '" + idFuncionario + "' AND assunto = '" + assunto + "';";
            idTarefa = Int32.Parse(ConsultaSimples(comando));
            
            comando = "Select prioridade from tbl_tarefas where id = " + idTarefa + ";";

            try
            {
                AbreConexao();
                MySqlCommand cmd = new MySqlCommand(comando, conexao);

                resultado = cmd.ExecuteScalar().ToString();
            }
            catch (MySqlException)
            {
                string erro = ListaErro.RetornaErro(09);
                int separador = erro.LastIndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                FechaConexao();
            }
            finally
            {
                FechaConexao();
            }

            return resultado;
        }

        /// <summary>
        /// Método responsável por retornar o tipo de contrato do contato solicitado.
        /// </summary>
        /// <param name="nomeEmpresa">Nome da empresa</param>
        public string ConsultaTipoContrato(string nomeEmpresa)
        {
            int idCliente = 0;
            string comando = null, resultado = null;

            comando = "Select ID from tbl_contato where nome = '" + nomeEmpresa + "';";
            idCliente = Int32.Parse(ConsultaSimples(comando));

            comando = "Select contrato from tbl_contato_contrato where contato = " + idCliente + ";";

            try
            {
                AbreConexao();
                MySqlCommand cmd = new MySqlCommand(comando, conexao);

                resultado = cmd.ExecuteScalar().ToString();
            }
            catch (MySqlException)
            {
                string erro = ListaErro.RetornaErro(10);
                int separador = erro.LastIndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                FechaConexao();
            }
            finally
            {
                FechaConexao();
            }

            return resultado;
        }

        /// <summary>
        /// Método que executa um comando no MYSQL
        /// </summary>
        /// <param name="comando">Comando que será executado no banco de dados.</param>
        public void ExecutaComando(string comando)
        {        
            try
            {
                AbreConexao();
                MySqlCommand cmd = new MySqlCommand(comando, conexao);
                cmd.ExecuteNonQuery();

            }
            catch (MySqlException)
            {
                string erro = ListaErro.RetornaErro(11);
                int separador = erro.LastIndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                FechaConexao();
            }
            finally
            {
                FechaConexao();
            }
        }

        /// <summary>
        /// Método responsável por retornar se a tarefa esta bloqueada.
        /// </summary>
        /// <param name="idFornecedor">Qual a tarefa que deseja conferir.</param>
        public bool FornecedorBloqueado(int idFornecedor)
        {
            bool resultado = false;

            string comando = "Select travar from tbl_fornecedor where id = '" + idFornecedor.ToString() + "';";

            try
            {
                AbreConexao();

                MySqlCommand cmd = new MySqlCommand(comando, conexao);

                if (!string.IsNullOrEmpty(cmd.ExecuteScalar().ToString()))
                {
                    if (cmd.ExecuteScalar().ToString() == "S")
                    {
                        resultado = true;
                    }
                }
            }
            catch (NullReferenceException)
            {
                resultado = false;
                return resultado;
            }
            catch (MySqlException)
            {
                string erro = ListaErro.RetornaErro(52);
                int separador = erro.LastIndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                FechaConexao();
            }
            finally
            {
                FechaConexao();
            }

            return resultado;
        }

        #region Login
        /// <summary>
        /// Verifica o Login    
        /// </summary>
        /// <param name="user">Usuário</param>
        /// <param name="pw">Senha</param>
        /// <returns></returns>
        public bool VerificaLogin(string user, string pw)
        {
            bool resultado = false;
            string comando = null;

            try
            {
                AbreConexao();

                comando = "select id from tbl_usuarios where user = '" + user + "' and pw = '" + pw + "';";
                MySqlCommand cmd = new MySqlCommand(comando, conexao);
                if (!string.IsNullOrEmpty(cmd.ExecuteScalar().ToString()))
                {
                    resultado = true;
                }
            }
            catch (NullReferenceException)
            {
                string erro = ListaErro.RetornaErro(47);
                int separador = erro.IndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                FechaConexao();
                return resultado;
            }
            catch (MySqlException)
            {
                string erro = ListaErro.RetornaErro(04);
                int separador = erro.IndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                FechaConexao();
                return resultado;
            }
            finally
            {
                FechaConexao();
            }

            return resultado;
        }
        #endregion

        /// <summary>
        /// Método que realiza o backup do banco de dados
        /// </summary>
        /// <param name="local">Local onde o arquivo será salvo</param>
        public bool Backup(string local)
        {
            bool resultado = false;
            /*
            try
            {
                using (conexao)
                {
                    AbreConexao();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        using (MySqlBackup mb = new MySqlBackup(cmd))
                        {
                            cmd.Connection = conexao;

                            mb.ExportToFile(local);

                            FechaConexao();

                            resultado = true;
                        }
                    }
                }
            }
            catch (MySqlException)
            {
                string erro = ListaErro.RetornaErro(12);
                int separador = erro.LastIndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                FechaConexao();
            }
           
            */
            return resultado;
        }

        /// <summary>
        /// Método que realiza a restauração do banco de dados
        /// </summary>
        /// <param name="local">Local onde backup está salvo</param>
        public bool Restauracao(string local)
        {
            bool resultado = false;
            /*
            try
            {
                using (conexao)
                {
                    FechaConexao();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        using (MySqlBackup mb = new MySqlBackup(cmd))
                        {
                            cmd.Connection = conexao;
                            AbreConexao();

                            mb.ImportFromFile(local);

                            FechaConexao();

                            resultado = true;
                        }
                    }
                }
            }
            catch (MySqlException)
            {
                string erro = ListaErro.RetornaErro(13);
                int separador = erro.LastIndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                FechaConexao();
            }*/

            return resultado;
        }
    }
}