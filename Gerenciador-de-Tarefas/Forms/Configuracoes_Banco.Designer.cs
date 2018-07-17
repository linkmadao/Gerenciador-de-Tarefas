namespace Gerenciador_de_Tarefas
{
    partial class Configuracoes_Banco
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Configuracoes_Banco));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbpageGeral = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUid = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBanco = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtServidor = new System.Windows.Forms.TextBox();
            this.rdbtnServidorLocal = new System.Windows.Forms.RadioButton();
            this.rdbtnRemoto = new System.Windows.Forms.RadioButton();
            this.btnFechar = new System.Windows.Forms.Button();
            this.btnTestarAplicar = new System.Windows.Forms.Button();
            this.ofdAbrirArquivo = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tbpageGeral.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Location = new System.Drawing.Point(-4, -2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(556, 93);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(134, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Parâmetros de conexão SQL";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(36, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(60, 60);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(556, 93);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Location = new System.Drawing.Point(-4, 88);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(390, 203);
            this.panel2.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbpageGeral);
            this.tabControl1.Location = new System.Drawing.Point(3, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(388, 203);
            this.tabControl1.TabIndex = 0;
            // 
            // tbpageGeral
            // 
            this.tbpageGeral.Controls.Add(this.panel3);
            this.tbpageGeral.Location = new System.Drawing.Point(4, 22);
            this.tbpageGeral.Name = "tbpageGeral";
            this.tbpageGeral.Padding = new System.Windows.Forms.Padding(3);
            this.tbpageGeral.Size = new System.Drawing.Size(380, 177);
            this.tbpageGeral.TabIndex = 0;
            this.tbpageGeral.Text = "Geral";
            this.tbpageGeral.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.txtPwd);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.txtUid);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.txtBanco);
            this.panel3.Controls.Add(this.textBox2);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.txtServidor);
            this.panel3.Controls.Add(this.rdbtnServidorLocal);
            this.panel3.Controls.Add(this.rdbtnRemoto);
            this.panel3.Location = new System.Drawing.Point(24, 10);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(331, 158);
            this.panel3.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(49, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Senha:";
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(96, 128);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(207, 20);
            this.txtPwd.TabIndex = 9;
            this.txtPwd.Text = "A1s2d3f4";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Usuário:";
            // 
            // txtUid
            // 
            this.txtUid.Location = new System.Drawing.Point(96, 102);
            this.txtUid.Name = "txtUid";
            this.txtUid.Size = new System.Drawing.Size(207, 20);
            this.txtUid.TabIndex = 7;
            this.txtUid.Text = "root";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Nome do Banco:";
            // 
            // txtBanco
            // 
            this.txtBanco.Location = new System.Drawing.Point(96, 76);
            this.txtBanco.Name = "txtBanco";
            this.txtBanco.Size = new System.Drawing.Size(207, 20);
            this.txtBanco.TabIndex = 5;
            this.txtBanco.Text = "gerenciatarefa";
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(96, 50);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(207, 20);
            this.textBox2.TabIndex = 4;
            this.textBox2.Text = "MariaDB";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Banco Usado:";
            // 
            // txtServidor
            // 
            this.txtServidor.Enabled = false;
            this.txtServidor.Location = new System.Drawing.Point(96, 24);
            this.txtServidor.Name = "txtServidor";
            this.txtServidor.Size = new System.Drawing.Size(207, 20);
            this.txtServidor.TabIndex = 2;
            // 
            // rdbtnServidorLocal
            // 
            this.rdbtnServidorLocal.AutoSize = true;
            this.rdbtnServidorLocal.Checked = true;
            this.rdbtnServidorLocal.Location = new System.Drawing.Point(25, 4);
            this.rdbtnServidorLocal.Name = "rdbtnServidorLocal";
            this.rdbtnServidorLocal.Size = new System.Drawing.Size(93, 17);
            this.rdbtnServidorLocal.TabIndex = 0;
            this.rdbtnServidorLocal.TabStop = true;
            this.rdbtnServidorLocal.Text = "Servidor Local";
            this.rdbtnServidorLocal.UseVisualStyleBackColor = true;
            this.rdbtnServidorLocal.Click += new System.EventHandler(this.rdbtnServidorLocal_Click);
            // 
            // rdbtnRemoto
            // 
            this.rdbtnRemoto.AutoSize = true;
            this.rdbtnRemoto.Location = new System.Drawing.Point(25, 27);
            this.rdbtnRemoto.Name = "rdbtnRemoto";
            this.rdbtnRemoto.Size = new System.Drawing.Size(65, 17);
            this.rdbtnRemoto.TabIndex = 1;
            this.rdbtnRemoto.Text = "Remoto:";
            this.rdbtnRemoto.UseVisualStyleBackColor = true;
            this.rdbtnRemoto.Click += new System.EventHandler(this.rdbtnRemoto_Click);
            // 
            // btnFechar
            // 
            this.btnFechar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnFechar.Location = new System.Drawing.Point(303, 297);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(75, 23);
            this.btnFechar.TabIndex = 2;
            this.btnFechar.Text = "Fechar";
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // btnTestarAplicar
            // 
            this.btnTestarAplicar.Location = new System.Drawing.Point(191, 297);
            this.btnTestarAplicar.Name = "btnTestarAplicar";
            this.btnTestarAplicar.Size = new System.Drawing.Size(106, 23);
            this.btnTestarAplicar.TabIndex = 3;
            this.btnTestarAplicar.Text = "Testar e Aplicar";
            this.btnTestarAplicar.UseVisualStyleBackColor = true;
            this.btnTestarAplicar.Click += new System.EventHandler(this.btnTestarAplicar_Click);
            // 
            // ofdAbrirArquivo
            // 
            this.ofdAbrirArquivo.DefaultExt = "sql";
            // 
            // Configuracoes_Banco
            // 
            this.AcceptButton = this.btnTestarAplicar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnFechar;
            this.ClientSize = new System.Drawing.Size(383, 324);
            this.Controls.Add(this.btnTestarAplicar);
            this.Controls.Add(this.btnFechar);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Configuracoes_Banco";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configurações de conexão SQL";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Configuracoes_Banco_FormClosing);
            this.Load += new System.EventHandler(this.Configuracoes_Banco_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tbpageGeral.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbpageGeral;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Button btnTestarAplicar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUid;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBanco;
        private System.Windows.Forms.TextBox txtServidor;
        private System.Windows.Forms.RadioButton rdbtnServidorLocal;
        private System.Windows.Forms.RadioButton rdbtnRemoto;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog ofdAbrirArquivo;
    }
}

