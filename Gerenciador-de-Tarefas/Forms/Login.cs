using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;
using Gerenciador_de_Tarefas.Classes;

namespace Gerenciador_de_Tarefas
{
    public partial class Login : Form
    {
        private TextBox txtUser;
        private MaskedTextBox txtPwd;
        private LinkLabel lnkTiago;
        private Button btnSair;
        private Button btnEntrar;
        private Label lblVersao;

        public Login()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Login));
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPwd = new System.Windows.Forms.MaskedTextBox();
            this.lnkTiago = new System.Windows.Forms.LinkLabel();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnEntrar = new System.Windows.Forms.Button();
            this.lblVersao = new System.Windows.Forms.Label();
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
            this.txtPwd.Mask = "0000";
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.PromptChar = ' ';
            this.txtPwd.Size = new System.Drawing.Size(221, 19);
            this.txtPwd.TabIndex = 1;
            this.txtPwd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPwd.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.txtPwd.Enter += new System.EventHandler(this.txtPwd_Enter);
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
            // lblVersao
            // 
            this.lblVersao.AutoSize = true;
            this.lblVersao.BackColor = System.Drawing.Color.White;
            this.lblVersao.Font = new System.Drawing.Font("Trebuchet MS", 9.25F, System.Drawing.FontStyle.Bold);
            this.lblVersao.Location = new System.Drawing.Point(498, 9);
            this.lblVersao.Name = "lblVersao";
            this.lblVersao.Size = new System.Drawing.Size(58, 18);
            this.lblVersao.TabIndex = 5;
            this.lblVersao.Text = "Versão: ";
            this.lblVersao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Login
            // 
            this.AcceptButton = this.btnEntrar;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackgroundImage = global::Gerenciador_de_Tarefas.Properties.Resources.TelaLogin;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CancelButton = this.btnSair;
            this.ClientSize = new System.Drawing.Size(637, 401);
            this.Controls.Add(this.lblVersao);
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
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Login_Load(object sender, EventArgs e)
        {
            Sistema.ChecaAtualizacao();

            lblVersao.Text = "Versão: " + Sistema.VersaoSoftware;

            #if DEBUG
            txtUser.Text = "Tiago";
            txtPwd.Text = "2222";

            //Simula o click
            btnEntrar_Click(btnEntrar, e);
            #endif
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            if (txtUser.Text != "")
            {
                if (!string.IsNullOrEmpty(txtPwd.Text.Replace(" ", "")))
                {
                    if (txtPwd.Text.Length > 3)
                    {
                        if (Sistema.VerificaLogin(txtUser.Text, txtPwd.Text))
                        {
                            string comando = "select id from tbl_usuarios where user = '" + txtUser.Text + "';";
                            Sistema.IDUsuarioLogado = int.Parse(Sistema.ConsultaSimples(comando));

                            Log.Login();

                            Hide();
                            ShowInTaskbar = false;

                            tInicial telaInicial = new tInicial();
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
                        ListaErro.RetornaErro(27);
                        txtPwd.Clear();
                        txtPwd.Focus();
                    }
                }
                else
                {
                    ListaErro.RetornaErro(26);
                    txtPwd.Focus();
                }
            }
            else
            {
                ListaErro.RetornaErro(25);
                txtUser.Focus();
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lnkTiago_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Navega para a URL
            Process.Start("http://www.facebook.com/tiagosmiguel");
        }

#region Posição cursor MaskedTextBox
        private delegate void PosicionaCursorDelegate(int posicao);

        private void PosicionaCursorSenha(int posicao)
        {
            txtPwd.SelectionStart = posicao;
        }

        private void txtPwd_Enter(object sender, EventArgs e)
        {
            BeginInvoke(new PosicionaCursorDelegate(PosicionaCursorSenha), new object[] { 0 });
        }
#endregion
    }
}
