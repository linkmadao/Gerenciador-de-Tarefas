using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;
using IWshRuntimeLibrary;
using System.IO;


namespace Gerenciador_de_Tarefas
{
    public partial class Login : Form
    {
        private BDCONN conexao = new BDCONN();
        private FuncoesVariaveis funcoes = new FuncoesVariaveis();
        private bool atualizaSistema = false;
        private string diretorioNovo = FuncoesEstaticas.DiretorioNovo();
        private string diretorioPadrao = FuncoesEstaticas.DiretorioPadrao();

        public Login()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            if(cmbUser.SelectedItem.ToString() != "")
            {
                if (!string.IsNullOrEmpty(txtPwd.Text.Replace(" ", "")))
                {
                    if(txtPwd.Text.Length > 3)
                    {
                        if(conexao.VerificaLogin(cmbUser.SelectedItem.ToString(), txtPwd.Text))
                        {
                            string comando = "select id from tbl_usuarios where user = '" + cmbUser.SelectedItem.ToString() + "';";
                            int id = int.Parse(conexao.ConsultaSimples(comando));

                            comando = "Insert into tbl_log values (0," + id + ", 'Login efetuado - " +
                                DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "');";
                            conexao.ExecutaComando(comando);

                            this.Hide();
                            this.ShowInTaskbar = false;

                            tInicial telaInicial = new tInicial(id);
                            telaInicial.ShowDialog();

                        }
                        else
                        {
                            txtPwd.Clear();
                            cmbUser.Focus();
                        }
                    }
                    else
                    {
                        string erro = ListaErro.RetornaErro(27);
                        int separador = erro.LastIndexOf(":");
                        MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtPwd.Clear();
                        txtPwd.Focus();
                    }
                }
                else
                {
                    string erro = ListaErro.RetornaErro(26);
                    int separador = erro.LastIndexOf(":");
                    MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtPwd.Focus();
                }
            }
            else
            {
                string erro = ListaErro.RetornaErro(25);
                int separador = erro.LastIndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cmbUser.Focus();
            }
        }

        #region Funções
        /// <summary>
        /// Método que cria o atalho do software na área de trabalho.
        /// </summary>
        /// <param name="shortcutFullPath">Local onde o atalho será criado</param>
        /// <param name="target">Local que o atalho irá obedecer</param>
        /// <param name="fileversion">Versão atual do software</param>
        private void CriaAtalho(string shortcutFullPath, string target, string fileVersion)
        {
            WshShell wshShell = new WshShell();
            IWshShortcut newShortcut = (IWshShortcut)wshShell.CreateShortcut(shortcutFullPath);
            newShortcut.TargetPath = target + "Gerenciador-de-Tarefas.exe";
            newShortcut.WorkingDirectory = target;
            newShortcut.Description = "Gerenciador de Tarefas - " + fileVersion;
            newShortcut.IconLocation = target + @"\Resources\favicon.ico";
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
        /// Método que atualiza o software.
        /// </summary>
        /// <param name="versaoAtual">Versão em que o software se encontra</param>
        private bool AtualizaAPP(string versaoAtual)
        {
            bool resultado = false;

            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            FileVersionInfo nFvi = FileVersionInfo.GetVersionInfo(diretorioPadrao + "gerenciador-de-tarefas.exe");


            if (nFvi.FileVersion != fvi.FileVersion)
            {
                string mensagem = ListaMensagens.RetornaMensagem(13);
                int separador = mensagem.LastIndexOf(":");
                if (MessageBox.Show(mensagem.Substring((separador + 2)), mensagem.Substring(0, (separador - 1)), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //Diretório onde será salvado os arquivos
                    diretorioNovo = @"" + diretorioNovo + versaoAtual + @"\";

                    //Se o diretório novo não existir
                    if (!Directory.Exists(diretorioNovo))
                    {
                        //Cria a pasta
                        Directory.CreateDirectory(diretorioNovo);
                        //Copia todos os arquivos
                        CopiaDiretorio(diretorioPadrao, diretorioNovo, true);

                        //Atualiza/Cria o Atalho
                        AtualizaAtalho(diretorioNovo, versaoAtual);

                        //Diz que será atualizado o sistema
                        atualizaSistema = true;
                    }
                    else
                    {
                        //Abre o executável existente
                        Process process = Process.Start(diretorioNovo + @"\gerenciador-de-tarefas.exe");
                    }
                    resultado = true;
                }
                else
                {
                    resultado = false;
                }
            }

            return resultado;
        }

        /// <summary>
        /// Método que atualiza o Atalho.
        /// </summary>
        /// <param name="diretorioAlvo">Qual o diretório que se encontra o software no momento atual</param>
        /// <param name="versaoAtual">Versão em que o software se encontra</param>
        private void AtualizaAtalho(string diretorioAlvo, string versaoAtual)
        {
            string atalho = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\Gerenciador de Tarefas.lnk";

            MessageBox.Show(atalho);

            //Verifica se existe o atalho
            if (System.IO.File.Exists(atalho))
            {   
                //Deleta o atalho
                System.IO.File.Delete(atalho);

                if (!System.IO.File.Exists(atalho))
                {
                    //Cria o atalho
                    CriaAtalho(atalho, diretorioNovo, versaoAtual);
                }
            }
            else
            {
                //Cria o atalho
                CriaAtalho(atalho, diretorioNovo, versaoAtual);
            }
        }

        #endregion

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lnkTiago_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Navigate to a URL.
            Process.Start("http://www.facebook.com/tiagosmiguel");
        }

        private void Login_Load(object sender, EventArgs e)
        {
            if (conexao.TestaConexao())
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                FileVersionInfo nFvi = FileVersionInfo.GetVersionInfo(diretorioPadrao + "gerenciador-de-tarefas.exe");

                //Verifica se existe o atalho
                if (AtualizaAPP(nFvi.FileVersion))
                {
                    Application.Exit();
                }
                else if (!System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\Gerenciador de Tarefas.lnk"))
                {
                    //Diretório onde será salvado os arquivos
                    diretorioNovo = @"" + diretorioNovo + nFvi.FileVersion + @"\";

                    AtualizaAtalho(diretorioNovo, nFvi.FileVersion);

                    //Abre o executável existente
                    Process process = Process.Start(diretorioNovo + @"\gerenciador-de-tarefas.exe");

                    Application.Exit();
                }
            }
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (atualizaSistema)
            {
                Process process = Process.Start(diretorioNovo + @"\gerenciador-de-tarefas.exe");
            }
        }
    }
}
