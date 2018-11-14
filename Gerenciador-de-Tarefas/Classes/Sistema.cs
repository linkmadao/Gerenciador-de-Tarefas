using IWshRuntimeLibrary;
using Shell32;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using File = System.IO.File;
using Folder = Shell32.Folder;

namespace Gerenciador_de_Tarefas.Classes
{
    public static class Sistema
    {
        #region Variaveis
        private static string versaoLocal = "", versaoAtalho = "";
        private static readonly string versaoServidor = FileVersionInfo.GetVersionInfo(@"\\192.168.254.253\GerenciadorTarefas\Projeto\Gerenciador-de-Tarefas\Gerenciador-de-Tarefas\bin\Release\Gerenciador-de-Tarefas.exe").FileVersion;
        #pragma warning disable 414
        private static readonly string diretorioServidor = @"\\192.168.254.253\GerenciadorTarefas\Projeto\Gerenciador-de-Tarefas\Gerenciador-de-Tarefas\bin\Release\";
        private static readonly string diretorioPadrao = @"\\192.168.254.253\GerenciadorTarefas\";
        #pragma warning restore 414
        private static readonly string diretorioAtalho = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        private static readonly string executavel = "gerenciador-de-tarefas.exe";
        private static readonly string nomeAtalho = @"\Gerenciador de Tarefas.lnk";
        private static readonly string descricaoSoftware = "Gerenciador de Tarefas - ";
        private static int usuarioLogado = 0;

        // Conexão Banco de Dados
        private static bool servidorLocal = false;
        private static MySqlConnection conexao;
        private static readonly string arquivoXML = "bdconfig.xml";
        private static string enderecoServidor = "";
        private static string nomeBanco = "";
        private static string nomeUsuario = "";
        private static string senhaUsuario = "";

        // Variáveis de Backup Conexao Banco de Dados
        #pragma warning disable 414, IDE0044
        private static string _enderecoServidor = "";
        private static string _nomeBanco = "";
        private static string _nomeUsuario = "";
        private static string _senhaUsuario = "";
        #pragma warning restore 414, IDE0044
        #endregion

        #region Propriedades
        /// <summary>
        /// Retorna a versão do software em questão
        /// </summary>
        public static bool ServidorLocal
        {
            get
            {
                return servidorLocal;
            }
        }
        public static int IDUsuarioLogado
        {
            get
            {
                return usuarioLogado;
            }
            set
            {
                usuarioLogado = value;
            }
        }
        public static string Ano
        {
            get
            {
                return DateTime.Now.Year.ToString();
            }
        }
        public static string EnderecoServidor
        {
            get
            {
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(enderecoServidor));
            }
        }
        public static string Hora
        {
            get
            {
                return DateTime.Now.ToShortTimeString();
            }
        }
        public static string NomeBanco
        {
            get
            {
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(nomeBanco));
            }
        }
        public static string NomeUsuario
        {
            get
            {
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(nomeUsuario));
            }
        }
        public static string NomeUsuarioLogado
        {
            get
            {
                string comando = "select user from tbl_usuarios where id = " + usuarioLogado + ";";
                return ConsultaSimples(comando).ToUpper();
            }
        }
        public static string SenhaUsuario
        {
            get
            {
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(senhaUsuario));
            }
        }
        public static string VersaoSoftware
        {
            get
            {
                return versaoLocal;
            }
        }
        #endregion

        #region Funcoes

        /// <summary>
        /// Verifica se tem atualização do software, caso tenha ele realiza a atualização e fecha o programa atual
        /// </summary>
        public static void ChecaAtualizacao()
        {
            try
            {
                if(File.Exists(Assembly.GetExecutingAssembly().Location) && Assembly.GetExecutingAssembly().Location.Contains(".exe"))
                {
                    versaoLocal = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
                }
                else
                {
                    versaoLocal = FileVersionInfo.GetVersionInfo(GetShortcutTargetFile(diretorioAtalho + nomeAtalho)).FileVersion;
                }
                 versaoAtalho = FileVersionInfo.GetVersionInfo(GetShortcutTargetFile(diretorioAtalho + nomeAtalho)).FileVersion;
            }
            catch (ArgumentException)
            {
                versaoAtalho = "";
            }
            #if RELEASE
                if (Convert.ToInt32(versaoServidor.Replace(".", "")) > Convert.ToInt32(versaoLocal.Replace(".", "")))
                {
                    if (Directory.Exists(diretorioPadrao + versaoServidor + @"\"))
                    {
                        if (!File.Exists(diretorioAtalho + atalho))
                        {
                            CriaAtalho(diretorioAtalho + atalho, diretorioPadrao + versaoServidor, versaoServidor);
                        }
                        else
                        {
                            if(!string.IsNullOrEmpty(versaoAtalho))
                            {
                                if (Convert.ToInt32(versaoServidor.Replace(".", "")) > Convert.ToInt32(versaoAtalho.Replace(".", "")))
                                {
                                    //Atualiza/Cria o Atalho
                                    AtualizaAtalho(diretorioPadrao + versaoServidor + @"\", versaoServidor);
                                }
                            }
                        

                            //Abre o executável existente
                            Process process = Process.Start(diretorioPadrao + versaoServidor + @"\" + executavel);

                            //Fecha o aplicativo atual
                            Application.Exit();
                        }
                    }
                    else
                    {
                        //Cria a pasta
                        Directory.CreateDirectory(diretorioPadrao + versaoServidor + @"\");

                        //Copia todos os arquivos
                        CopiaDiretorio(diretorioServidor, diretorioPadrao + versaoServidor + @"\", true);

                        if (!File.Exists(diretorioAtalho + atalho))
                        {
                            CriaAtalho(diretorioAtalho + atalho, diretorioPadrao + versaoServidor, versaoServidor);
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(versaoAtalho))
                            {
                                if (Convert.ToInt32(versaoServidor.Replace(".", "")) > Convert.ToInt32(versaoAtalho.Replace(".", "")))
                                {
                                    //Atualiza/Cria o Atalho
                                    AtualizaAtalho(diretorioPadrao + versaoServidor + @"\", versaoServidor);
                                }
                            }
                        }

                        //Abre o executável existente
                        Process process = Process.Start(diretorioPadrao + versaoServidor + @"\" + executavel);

                        //Fecha o aplicativo atual
                        Application.Exit();
                    }
                }
                else
                {
                    if (Directory.Exists(diretorioPadrao + versaoLocal + @"\"))
                    {
                        if (!File.Exists(diretorioAtalho + atalho))
                        {
                            CriaAtalho(diretorioAtalho + atalho, diretorioPadrao + versaoLocal, versaoServidor);
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(versaoAtalho))
                            {
                                if (Convert.ToInt32(versaoLocal.Replace(".", "")) > Convert.ToInt32(versaoAtalho.Replace(".", "")))
                                {
                                    //Atualiza/Cria o Atalho
                                    AtualizaAtalho(diretorioPadrao + versaoLocal + @"\", versaoLocal);
                                }
                            }
                        }
                    }
                    else
                    {
                        //Cria a pasta
                        Directory.CreateDirectory(diretorioPadrao + versaoLocal + @"\");

                        //Copia todos os arquivos
                        CopiaDiretorio(diretorioServidor, diretorioPadrao + versaoLocal + @"\", true);

                        if (!File.Exists(diretorioAtalho + atalho))
                        {
                            CriaAtalho(diretorioAtalho + atalho, diretorioPadrao + versaoLocal, versaoServidor);
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(versaoAtalho))
                            {
                                if (Convert.ToInt32(versaoLocal.Replace(".", "")) > Convert.ToInt32(versaoAtalho.Replace(".", "")))
                                {
                                    //Atualiza/Cria o Atalho
                                    AtualizaAtalho(diretorioPadrao + versaoLocal + @"\", versaoLocal);
                                }
                            }
                        }
                    }
                }

                LerDadosXML();
                #endif
        }

        /// <summary>
        /// Método que atualiza o Atalho.
        /// </summary>
        /// <param name="diretorioAlvo">Qual o diretório que se encontra o software no momento atual</param>
        /// <param name="versaoAtual">Versão em que o software se encontra</param>
        private static void AtualizaAtalho(string diretorioAlvo, string versaoAtual)
        {
            string atalho = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + nomeAtalho;

            //Verifica se existe o atalho
            if (File.Exists(atalho))
            {
                //Deleta o atalho
                File.Delete(atalho);

                if (!File.Exists(atalho))
                {
                    //Cria o atalho
                    CriaAtalho(atalho, diretorioAlvo, versaoAtual);
                }
            }
            else
            {
                //Cria o atalho
                CriaAtalho(atalho, diretorioAlvo, versaoAtual);
            }
        }

        /// <summary>
        /// Método que cria o atalho do software na área de trabalho.
        /// </summary>
        /// <param name="shortcutFullPath">Local onde o atalho será criado</param>
        /// <param name="target">Local que o atalho irá obedecer</param>
        /// <param name="fileversion">Versão atual do software</param>
        private static void CriaAtalho(string caminhoCompletoAtalho, string alvo, string versao)
        {
            WshShell wshShell = new WshShell();
            IWshShortcut newShortcut = (IWshShortcut)wshShell.CreateShortcut(caminhoCompletoAtalho);
            newShortcut.TargetPath = alvo + executavel;
            newShortcut.WorkingDirectory = alvo;
            newShortcut.Description = descricaoSoftware + versao;
            newShortcut.IconLocation = alvo + @"\Resources\favicon.ico";
            newShortcut.Save();
        }

        /// <summary>
        /// Método que copia todos os arquivos do diretório.
        /// </summary>
        /// <param name="sourceDirName">De qual pasta será copiado</param>
        /// <param name="destDirName">Para qual pasta será copiado</param>
        /// <param name="copySubDirs">Copiar subpastas?</param>
        private static void CopiaDiretorio(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                ListaErro.RetornaErro(30);
                return;
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    CopiaDiretorio(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        private static string GetShortcutTargetFile(string nomeAtalho)
        {
            string caminhoPasta = Path.GetDirectoryName(nomeAtalho);
            string nomePrograma = Path.GetFileName(nomeAtalho);

            Shell shell = new Shell();
            Folder pasta = shell.NameSpace(caminhoPasta);
            FolderItem folderItem = pasta.ParseName(nomePrograma);
            if (folderItem != null)
            {
                ShellLinkObject link = (ShellLinkObject)folderItem.GetLink;
                return link.Path;
            }

            return string.Empty;
        }

        #region Banco de Dados
        private static void LerDadosXML()
        {
            XElement xml = XElement.Load(arquivoXML);
            foreach (XElement x in xml.Elements())
            {
                if (x.Attribute("servidor").Value != "localhost" || x.Attribute("servidor").Value != "127.0.0.1")
                {
                    servidorLocal = false;
                }
                else
                {
                    servidorLocal = true;
                }

                enderecoServidor = x.Attribute("servidor").Value;
                nomeBanco = x.Attribute("banco").Value;
                nomeUsuario = x.Attribute("uid").Value;
                senhaUsuario = x.Attribute("pwd").Value;
            }
        }

        public static void SalvarDadosXML()
        {
            /*
            bool resultado = false;

            if (!ServidorLocal)
            {
                if (TestaConexao)
                {
                    resultado = true;
                }
            }
            else
            {
                if (conexao.TestaConexao(servidor, banco, uid, pwd))
                {
                    resultado = true;
                }
            }

            if (resultado)
            {
                if ((rdbtnRemoto && servidor != enderecoServidor) || banco != nomeBanco || uid != nomeUsuario || pwd != senhaUsuario)
                {
                    try
                    {
                        XElement xml = XElement.Load(arquivoXML);
                        XElement x = xml.Elements().First();
                        if (x != null)
                        {
                            if (rdbtnRemoto && servidor != "")
                            {
                                x.Attribute("servidor").SetValue(servidor);
                            }
                            x.Attribute("banco").SetValue(banco);
                            x.Attribute("uid").SetValue(uid);
                            x.Attribute("pwd").SetValue(pwd);
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    finally
                    {
                        string mensagem = ListaMensagens.RetornaMensagem(24);
                        int separador = mensagem.IndexOf(":");
                        MessageBox.Show(mensagem.Substring((separador + 2)), mensagem.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    string mensagem = ListaMensagens.RetornaMensagem(24);
                    int separador = mensagem.IndexOf(":");
                    MessageBox.Show(mensagem.Substring((separador + 2)), mensagem.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                string erro = ListaErro.RetornaErro(59);
                int separador = erro.IndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

        /// <summary>
        /// Abre a conexão com o Banco de dados.
        /// </summary>
        private static void AbreConexao()
        {
            if (enderecoServidor == "")
            {
                XElement xml;

                try
                {
                    xml = XElement.Load(arquivoXML);
                }
                catch (Exception)
                {
                    Configuracoes_Banco config = new Configuracoes_Banco(true);
                    config.ShowDialog();
                    return;
                }

                try
                {
                    foreach (XElement x in xml.Elements())
                    {
                        enderecoServidor = x.Attribute("servidor").Value;
                        nomeBanco = x.Attribute("banco").Value;
                        nomeUsuario = x.Attribute("uid").Value;
                        senhaUsuario = x.Attribute("pwd").Value;
                    }
                }
                catch (Exception)
                {
                    ListaErro.RetornaErro(60);
                    return;
                }
            }

            try
            {
                string connString = string.Format("Server={0}; Database={1}; Userid={2}; Password={3};",
                    enderecoServidor, nomeBanco, nomeUsuario, senhaUsuario);

                conexao = new MySqlConnection(connString);
            }
            catch (MySqlException)
            {
                ListaErro.RetornaErro(01);
                return;
            }

            try
            {
                if (conexao.State == ConnectionState.Closed)
                {
                    conexao.Open();
                }
            }
            catch (MySqlException)
            {
                ListaErro.RetornaErro(59);
                return;
            }
        }

        /// <summary>
        /// Método responsável por retornar o id AutoIncrement do MYSQL da tabela selecionada
        /// </summary>
        /// <param name="tipo">1:Clientes/Fornecedores, 2:Ordem de serviço</param>
        /// <returns></returns>
        public static string ConsultaAutoIncrement(int tipo)
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
                ListaErro.RetornaErro(07);
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
        /// Método responsável por retornar o contato solicitado.
        /// </summary>
        /// <param name="comando">Comando que será consultado no banco de dados.</param>
        public static List<string> ConsultaContato(string comando)
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
                ListaErro.RetornaErro(06);
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
        public static List<string> ConsultaFornecedor(string comando)
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
                    for (int i = 0; i < 25; i++)
                    {
                        if (dR.IsDBNull(i))
                        {
                            resultado.Add("");
                        }
                        else
                        {
                            if (dR.GetString(i) == null)
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
                ListaErro.RetornaErro(55);
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
        /// Método responsável por retornar o ID do item especificado.
        /// </summary>
        /// <param name="comando">Comando que será consultado no banco de dados.</param>
        public static string ConsultaSimples(string comando)
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
                ListaErro.RetornaErro(04);
                FechaConexao();
            }
            catch (MySqlException)
            {
                ListaErro.RetornaErro(04);
                FechaConexao();
                return resultado;
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
        public static List<string> ConsultaTarefas(string comando)
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
                    for (int i = 0; i < 8; i++)
                    {
                        if (dR.GetString(i) == null)
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
                ListaErro.RetornaErro(05);
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
        public static void ExecutaComando(string comando)
        {
            try
            {
                AbreConexao();
                MySqlCommand cmd = new MySqlCommand(comando, conexao);
                cmd.ExecuteNonQuery();

            }
            catch (MySqlException)
            {
                ListaErro.RetornaErro(11);
                FechaConexao();
                return;
            }
            finally
            {
                FechaConexao();
            }
        }

        /// <summary>
        /// Fecha a conexão com o Banco de dados.
        /// </summary>
        private static void FechaConexao()
        {
            if (conexao.State == ConnectionState.Open)
            {
                conexao.Close();
            }
        }

        /// <summary>
        /// Método responsável por preencher as combobox nas telas.
        /// </summary>
        /// <param name="comando">Comando que será consultado no banco de dados.</param>
        public static List<string> PreencheCMB(string comando)
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
                ListaErro.RetornaErro(03);
                FechaConexao();
                return resultado;
            }
            finally
            {
                FechaConexao();
            }

            return resultado;
        }

        /// <summary>
        /// Preenche um DataSet e retorna um DataTable com o resultado.
        /// </summary>
        /// <param name="comando">Comando que será consultado no banco de dados.</param>
        public static DataTable PreencheDGV(string comando)
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
                ListaErro.RetornaErro(02);
                FechaConexao();
            }
            finally
            {
                FechaConexao();
            }

            return resultado.Tables[0];
        }

        /// <summary>
        /// Testa a conexão com o Banco de dados.
        /// </summary>
        public static bool TestaConexao()
        {
            try
            {
                AbreConexao();
            }
            catch (MySqlException)
            {

                return false;
            }
            finally
            {
                FechaConexao();
            }

            return true;
        }

        /// <summary>
        /// Verifica o Login    
        /// </summary>
        /// <param name="user">Usuário</param>
        /// <param name="pw">Senha</param>
        /// <returns></returns>
        public static bool VerificaLogin(string user, string pw)
        {
            try
            {
                AbreConexao();

                string comando = "select id from tbl_usuarios where user = '" + user + "' and pw = '" + pw + "';";
                MySqlCommand cmd = new MySqlCommand(comando, conexao);
                if (!string.IsNullOrEmpty(cmd.ExecuteScalar().ToString()))
                {
                    FechaConexao();
                    return  true;                    
                }
                else
                {
                    FechaConexao();
                    return false;
                }
            }
            catch (NullReferenceException)
            {
                ListaErro.RetornaErro(47);
                FechaConexao();
                return false;
            }
            catch (MySqlException)
            {
                ListaErro.RetornaErro(04);
                FechaConexao();
                return false;
            }
        }

        /// <summary>
        /// Método que realiza o backup do banco de dados
        /// </summary>
        /// <param name="local">Local onde o arquivo será salvo</param>
        public static bool Backup(string local)
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
        public static bool Restauracao(string local)
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
    #endregion
    #endregion
    }
}
