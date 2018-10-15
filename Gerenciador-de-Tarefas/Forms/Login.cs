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
        private TextBox txtUser;
        private MaskedTextBox txtPwd;
        private LinkLabel lnkTiago;
        private Button btnSair;
        private Button btnEntrar;
        private string diretorioPadrao = FuncoesEstaticas.DiretorioPadrao();

        public Login()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            if (txtUser.Text != "")
            {
                if (!string.IsNullOrEmpty(txtPwd.Text.Replace(" ", "")))
                {
                    if (txtPwd.Text.Length > 3)
                    {
                        if (conexao.VerificaLogin(txtUser.Text, txtPwd.Text))
                        {
                            string comando = "select id from tbl_usuarios where user = '" + txtUser.Text + "';";
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
                            txtUser.Focus();
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
                txtUser.Focus();
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

                // Verifica se tem atualização
                if (AtualizaAPP(nFvi.FileVersion))
                {
                    Application.Exit();
                }
                // Verifica se existe o atalho
                else if (!System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\Gerenciador de Tarefas.lnk"))
                {
                    // Diretório onde será salvado os arquivos
                    diretorioNovo = @"" + diretorioNovo + nFvi.FileVersion + @"\";

                    AtualizaAtalho(diretorioNovo, nFvi.FileVersion);

                    // Abre o executável existente
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

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPwd = new System.Windows.Forms.MaskedTextBox();
            this.lnkTiago = new System.Windows.Forms.LinkLabel();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnEntrar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtUser
            // 
            this.txtUser.AutoCompleteCustomSource.AddRange(new string[] {
            "Eduardo",
            "Tiago",
            "Manutencao",
            "Mario",
            "Lizane"});
            this.txtUser.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtUser.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(186)))), ((int)(((byte)(242)))));
            this.txtUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUser.CausesValidation = false;
            this.txtUser.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser.ForeColor = System.Drawing.Color.Black;
            this.txtUser.Location = new System.Drawing.Point(234, 131);
            this.txtUser.MaxLength = 15;
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(221, 19);
            this.txtUser.TabIndex = 0;
            this.txtUser.Text = "Manutencao";
            this.txtUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPwd
            // 
            this.txtPwd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(186)))), ((int)(((byte)(242)))));
            this.txtPwd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPwd.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPwd.Location = new System.Drawing.Point(234, 166);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(221, 19);
            this.txtPwd.TabIndex = 1;
            this.txtPwd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lnkTiago
            // 
            this.lnkTiago.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(112)))), ((int)(((byte)(141)))));
            this.lnkTiago.AutoSize = true;
            this.lnkTiago.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(209)))), ((int)(((byte)(245)))));
            this.lnkTiago.Font = new System.Drawing.Font("Trebuchet MS", 9.5F, System.Drawing.FontStyle.Bold);
            this.lnkTiago.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lnkTiago.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(112)))), ((int)(((byte)(141)))));
            this.lnkTiago.Location = new System.Drawing.Point(325, 253);
            this.lnkTiago.Name = "lnkTiago";
            this.lnkTiago.Size = new System.Drawing.Size(118, 18);
            this.lnkTiago.TabIndex = 4;
            this.lnkTiago.TabStop = true;
            this.lnkTiago.Text = "Tiago Silva Miguel";
            this.lnkTiago.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(112)))), ((int)(((byte)(141)))));
            this.lnkTiago.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkTiago_LinkClicked);
            // 
            // btnSair
            // 
            this.btnSair.BackColor = System.Drawing.Color.IndianRed;
            this.btnSair.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.ForeColor = System.Drawing.Color.Black;
            this.btnSair.Location = new System.Drawing.Point(193, 198);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(120, 36);
            this.btnSair.TabIndex = 3;
            this.btnSair.Text = "Fechar";
            this.btnSair.UseVisualStyleBackColor = false;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnEntrar
            // 
            this.btnEntrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnEntrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnEntrar.FlatAppearance.BorderSize = 0;
            this.btnEntrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEntrar.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEntrar.Location = new System.Drawing.Point(336, 198);
            this.btnEntrar.Name = "btnEntrar";
            this.btnEntrar.Size = new System.Drawing.Size(120, 36);
            this.btnEntrar.TabIndex = 2;
            this.btnEntrar.Text = "Entrar";
            this.btnEntrar.UseVisualStyleBackColor = false;
            this.btnEntrar.Click += new System.EventHandler(this.btnEntrar_Click);
            // 
            // Login
            // 
            this.AcceptButton = this.btnEntrar;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackgroundImage = global::Gerenciador_de_Tarefas.Properties.Resources.TelaLogin;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CancelButton = this.btnSair;
            this.ClientSize = new System.Drawing.Size(637, 401);
            this.Controls.Add(this.btnEntrar);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.lnkTiago);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.txtUser);
            this.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gerenciador de Tarefas";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
