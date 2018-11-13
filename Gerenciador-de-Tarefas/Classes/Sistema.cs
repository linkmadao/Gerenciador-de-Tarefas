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
using Gerenciador_de_Tarefas.Classes;

namespace Gerenciador_de_Tarefas.Classes
{
    public static class Sistema
    {
        #region Variaveis
        private static BDCONN conexao = new BDCONN();

        private static string versaoLocal = "", versaoAtalho = "";
        private static readonly string versaoServidor = FileVersionInfo.GetVersionInfo(@"\\192.168.254.253\GerenciadorTarefas\Projeto\Gerenciador-de-Tarefas\Gerenciador-de-Tarefas\bin\Release\Gerenciador-de-Tarefas.exe").FileVersion;
        private static readonly string diretorioServidor = @"\\192.168.254.253\GerenciadorTarefas\Projeto\Gerenciador-de-Tarefas\Gerenciador-de-Tarefas\bin\Release\";
        private static readonly string diretorioPadrao = @"\\192.168.254.253\GerenciadorTarefas\";
        private static readonly string diretorioAtalho = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        private static readonly string executavel = "gerenciador-de-tarefas.exe";
        private static readonly string nomeAtalho = @"\Gerenciador de Tarefas.lnk";
        private static readonly string descricaoSoftware = "Gerenciador de Tarefas - ";

        //Variaveis XML
        private static readonly string arquivoXML = "bdconfig.xml";
        private static string enderecoServidor = "";
        private static string nomeBanco = "";
        private static string nomeUsuario = "";
        private static string senhaUsuario = "";
        private static bool servidorLocal = false;

        private static int usuarioLogado = 0;
        #endregion

        #region Propriedades
        /// <summary>
        /// Retorna a versão do software em questão
        /// </summary>
        public static string VersaoSoftware
        {
            get
            {
                return versaoLocal;
            }
        }
        public static string EnderecoServidor
        {
            get
            {
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(enderecoServidor));
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
        public static string SenhaUsuario
        {
            get
            {
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(senhaUsuario));
            }
        }
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
        public static string NomeUsuarioLogado
        {
            get
            {
                string comando = "select user from tbl_usuarios where id = " + usuarioLogado + ";";
                return conexao.ConsultaSimples(comando).ToUpper();
            }
        }
        public static string Hora
        {
            get
            {
                return DateTime.Now.ToShortTimeString();
            }
        }
        public static string Ano
        {
            get
            {
                return DateTime.Now.Year.ToString();
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
                string erro = ListaErro.RetornaErro(30);
                int separador = erro.LastIndexOf(":");
                throw new DirectoryNotFoundException(erro.Substring((separador + 2)) + sourceDirName);
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

        private static void LerDadosXML()
        {
            XElement xml = XElement.Load(arquivoXML);
            foreach (XElement x in xml.Elements())
            {
                if (x.Attribute("servidor").Value != "" || x.Attribute("servidor").Value != "localhost" || x.Attribute("servidor").Value != "127.0.0.1")
                {
                    servidorLocal = false;
                    enderecoServidor = x.Attribute("servidor").Value;
                }

                nomeBanco = x.Attribute("banco").Value;
                nomeUsuario = x.Attribute("uid").Value;
                senhaUsuario = x.Attribute("pwd").Value;
            }
        }

        public static void SalvarDadosXML(bool rdbtnRemoto, string servidor, string banco, string uid, string pwd)
        {
            bool resultado = false;

            if (!rdbtnRemoto)
            {
                if (conexao.TestaConexao(banco, uid, pwd))
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
            }    
        }
#endregion
    }
}
