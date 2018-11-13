namespace Gerenciador_de_Tarefas
{
    partial class Tarefa
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tarefa));
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmbFuncionario = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbEmpresa = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbPrioridade = new System.Windows.Forms.ComboBox();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFinal = new System.Windows.Forms.DateTimePicker();
            this.dtpInicio = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAssunto = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rtbTexto = new System.Windows.Forms.RichTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.btnBold = new System.Windows.Forms.ToolStripMenuItem();
            this.btnItalico = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSublinhado = new System.Windows.Forms.ToolStripMenuItem();
            this.btnInserirImagem = new System.Windows.Forms.ToolStripMenuItem();
            this.pdPrevisualizarImpressao = new System.Windows.Forms.PrintPreviewDialog();
            this.pdDocumento = new System.Drawing.Printing.PrintDocument();
            this.pdConfiguraImpressao = new System.Windows.Forms.PrintDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnSalvarFechar = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cmbFuncionario);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.cmbEmpresa);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.cmbPrioridade);
            this.panel2.Controls.Add(this.cmbStatus);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.dtpFinal);
            this.panel2.Controls.Add(this.dtpInicio);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtAssunto);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(2, 76);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(738, 120);
            this.panel2.TabIndex = 3;
            // 
            // cmbFuncionario
            // 
            this.cmbFuncionario.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cmbFuncionario.FormattingEnabled = true;
            this.cmbFuncionario.Location = new System.Drawing.Point(488, 7);
            this.cmbFuncionario.Name = "cmbFuncionario";
            this.cmbFuncionario.Size = new System.Drawing.Size(218, 24);
            this.cmbFuncionario.Sorted = true;
            this.cmbFuncionario.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label7.Location = new System.Drawing.Point(402, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 17);
            this.label7.TabIndex = 0;
            this.label7.Text = "Atribuido a:";
            // 
            // cmbEmpresa
            // 
            this.cmbEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cmbEmpresa.FormattingEnabled = true;
            this.cmbEmpresa.Location = new System.Drawing.Point(117, 7);
            this.cmbEmpresa.Name = "cmbEmpresa";
            this.cmbEmpresa.Size = new System.Drawing.Size(279, 24);
            this.cmbEmpresa.Sorted = true;
            this.cmbEmpresa.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label6.Location = new System.Drawing.Point(10, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "Empresa:";
            // 
            // cmbPrioridade
            // 
            this.cmbPrioridade.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cmbPrioridade.FormattingEnabled = true;
            this.cmbPrioridade.Items.AddRange(new object[] {
            "Baixa",
            "Normal",
            "Alta"});
            this.cmbPrioridade.Location = new System.Drawing.Point(370, 96);
            this.cmbPrioridade.Name = "cmbPrioridade";
            this.cmbPrioridade.Size = new System.Drawing.Size(144, 24);
            this.cmbPrioridade.TabIndex = 7;
            this.cmbPrioridade.Text = "Normal";
            // 
            // cmbStatus
            // 
            this.cmbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "Não iniciada",
            "Em andamento",
            "Aguardando outra pessoa",
            "Adiada",
            "Concluída"});
            this.cmbStatus.Location = new System.Drawing.Point(370, 68);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(336, 24);
            this.cmbStatus.TabIndex = 6;
            this.cmbStatus.Text = "Não iniciada";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(307, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Prioridade:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(307, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Status:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.Location = new System.Drawing.Point(10, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Data de Conclusão:";
            // 
            // dtpFinal
            // 
            this.dtpFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFinal.Location = new System.Drawing.Point(148, 96);
            this.dtpFinal.Name = "dtpFinal";
            this.dtpFinal.Size = new System.Drawing.Size(141, 23);
            this.dtpFinal.TabIndex = 5;
            this.dtpFinal.ValueChanged += new System.EventHandler(this.dtpFinal_ValueChanged);
            // 
            // dtpInicio
            // 
            this.dtpInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dtpInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpInicio.Location = new System.Drawing.Point(148, 69);
            this.dtpInicio.Name = "dtpInicio";
            this.dtpInicio.Size = new System.Drawing.Size(141, 23);
            this.dtpInicio.TabIndex = 4;
            this.dtpInicio.ValueChanged += new System.EventHandler(this.dtpInicio_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(10, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Data de Início:";
            // 
            // txtAssunto
            // 
            this.txtAssunto.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtAssunto.Location = new System.Drawing.Point(117, 34);
            this.txtAssunto.Name = "txtAssunto";
            this.txtAssunto.Size = new System.Drawing.Size(589, 23);
            this.txtAssunto.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(10, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Assunto:";
            // 
            // rtbTexto
            // 
            this.rtbTexto.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.rtbTexto.Location = new System.Drawing.Point(6, 27);
            this.rtbTexto.Name = "rtbTexto";
            this.rtbTexto.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.rtbTexto.Size = new System.Drawing.Size(738, 379);
            this.rtbTexto.TabIndex = 8;
            this.rtbTexto.Text = "";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rtbTexto);
            this.panel3.Controls.Add(this.menuStrip1);
            this.panel3.Location = new System.Drawing.Point(-7, 198);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(747, 409);
            this.panel3.TabIndex = 5;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnBold,
            this.btnItalico,
            this.btnSublinhado,
            this.btnInserirImagem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(747, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // btnBold
            // 
            this.btnBold.Name = "btnBold";
            this.btnBold.Size = new System.Drawing.Size(12, 20);
            this.btnBold.Click += new System.EventHandler(this.btnBold_Click);
            // 
            // btnItalico
            // 
            this.btnItalico.Name = "btnItalico";
            this.btnItalico.Size = new System.Drawing.Size(12, 20);
            this.btnItalico.Click += new System.EventHandler(this.btnItalico_Click);
            // 
            // btnSublinhado
            // 
            this.btnSublinhado.Name = "btnSublinhado";
            this.btnSublinhado.Size = new System.Drawing.Size(12, 20);
            this.btnSublinhado.Click += new System.EventHandler(this.btnSublinhado_Click);
            // 
            // btnInserirImagem
            // 
            this.btnInserirImagem.Name = "btnInserirImagem";
            this.btnInserirImagem.Size = new System.Drawing.Size(12, 20);
            this.btnInserirImagem.Click += new System.EventHandler(this.btnInserirImagem_Click);
            // 
            // pdPrevisualizarImpressao
            // 
            this.pdPrevisualizarImpressao.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.pdPrevisualizarImpressao.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.pdPrevisualizarImpressao.ClientSize = new System.Drawing.Size(400, 300);
            this.pdPrevisualizarImpressao.Document = this.pdDocumento;
            this.pdPrevisualizarImpressao.Enabled = true;
            this.pdPrevisualizarImpressao.Icon = ((System.Drawing.Icon)(resources.GetObject("pdPrevisualizarImpressao.Icon")));
            this.pdPrevisualizarImpressao.Name = "pdPrevisualizarImpressao";
            this.pdPrevisualizarImpressao.ShowIcon = false;
            this.pdPrevisualizarImpressao.UseAntiAlias = true;
            this.pdPrevisualizarImpressao.Visible = false;
            // 
            // pdDocumento
            // 
            this.pdDocumento.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.pdDocumento_BeginPrint);
            this.pdDocumento.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.pdDocumento_PrintPage);
            // 
            // pdConfiguraImpressao
            // 
            this.pdConfiguraImpressao.Document = this.pdDocumento;
            this.pdConfiguraImpressao.UseEXDialog = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSair);
            this.panel1.Controls.Add(this.btnImprimir);
            this.panel1.Controls.Add(this.btnExcluir);
            this.panel1.Controls.Add(this.btnSalvar);
            this.panel1.Controls.Add(this.btnSalvarFechar);
            this.panel1.Location = new System.Drawing.Point(-1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(741, 69);
            this.panel1.TabIndex = 2;
            // 
            // btnSair
            // 
            this.btnSair.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSair.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSair.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSair.Location = new System.Drawing.Point(393, 3);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 62);
            this.btnSair.TabIndex = 13;
            this.btnSair.Text = "Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnImprimir.Location = new System.Drawing.Point(231, 3);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(75, 62);
            this.btnImprimir.TabIndex = 11;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnExcluir
            // 
            this.btnExcluir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExcluir.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnExcluir.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExcluir.Location = new System.Drawing.Point(312, 3);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(75, 62);
            this.btnExcluir.TabIndex = 12;
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExcluir.UseVisualStyleBackColor = true;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSalvar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSalvar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSalvar.Location = new System.Drawing.Point(117, 3);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(108, 62);
            this.btnSalvar.TabIndex = 10;
            this.btnSalvar.Text = "Salvar/Editar";
            this.btnSalvar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnSalvarFechar
            // 
            this.btnSalvarFechar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSalvarFechar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSalvarFechar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSalvarFechar.Location = new System.Drawing.Point(3, 3);
            this.btnSalvarFechar.Name = "btnSalvarFechar";
            this.btnSalvarFechar.Size = new System.Drawing.Size(108, 62);
            this.btnSalvarFechar.TabIndex = 9;
            this.btnSalvarFechar.Text = "Salvar/Fechar";
            this.btnSalvarFechar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSalvarFechar.UseVisualStyleBackColor = true;
            this.btnSalvarFechar.Click += new System.EventHandler(this.btnSalvarFechar_Click);
            // 
            // TelaTarefa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnSair;
            this.ClientSize = new System.Drawing.Size(738, 603);
            this.ControlBox = false;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Tarefa";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nova Tarefa";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Tarefa_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Tarefa_KeyDown);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSalvarFechar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cmbPrioridade;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpFinal;
        private System.Windows.Forms.DateTimePicker dtpInicio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAssunto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtbTexto;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox cmbFuncionario;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbEmpresa;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.PrintPreviewDialog pdPrevisualizarImpressao;
        private System.Drawing.Printing.PrintDocument pdDocumento;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.PrintDialog pdConfiguraImpressao;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btnInserirImagem;
        private System.Windows.Forms.ToolStripMenuItem btnBold;
        private System.Windows.Forms.ToolStripMenuItem btnItalico;
        private System.Windows.Forms.ToolStripMenuItem btnSublinhado;
    }
}