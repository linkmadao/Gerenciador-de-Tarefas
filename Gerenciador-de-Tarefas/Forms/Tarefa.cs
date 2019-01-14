using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Gerenciador_de_Tarefas.Classes;
using Microsoft.VisualBasic;


namespace Gerenciador_de_Tarefas
{
    public partial class Tarefa : Form
    {

        #region Variáveis
        private const int MaxCaracteres = 110;
        private StringReader leitor;
        #endregion

        public Tarefa()
        {
            InitializeComponent();
            
            CarregaFuncionarios();
            CarregaEmpresas();

            if (Classes.Tarefa.NovaTarefa)
            {
                btnSair.Text = "Cancelar";
                btnSalvar.Text = "Cadastrar";
                btnSalvarFechar.Text = "Cadastrar \ne Fechar";
                btnExcluir.Enabled = false;
                btnImprimir.Enabled = false;
            }
            else
            {
                cmbEmpresa.SelectedIndex = cmbEmpresa.FindStringExact(Classes.Tarefa.Empresa);
                cmbFuncionario.SelectedIndex = cmbFuncionario.FindStringExact(Classes.Tarefa.Atribuicao);
                cmbStatus.SelectedIndex = Classes.Tarefa.Status;
                txtAssunto.Text = Classes.Tarefa.Assunto;
                dtpInicio.Text = Classes.Tarefa.DataInicial;
                dtpFinal.Text = Classes.Tarefa.DataFinal;
                cmbPrioridade.SelectedIndex = Classes.Tarefa.Prioridade;
                rtbTexto.Text = Classes.Tarefa.Texto;
                this.Text = Classes.Tarefa.Titulo;
            }
        }

        private void Tarefa_FormClosing(object sender, FormClosingEventArgs e)
        {
            Classes.Tarefa.Assunto = txtAssunto.Text;
            Classes.Tarefa.Atribuicao = cmbFuncionario.SelectedItem.ToString();
            Classes.Tarefa.DataInicial = dtpInicio.Text;
            Classes.Tarefa.DataFinal = dtpFinal.Text;
            Classes.Tarefa.Prioridade = cmbPrioridade.SelectedIndex;
            Classes.Tarefa.Status = cmbStatus.SelectedIndex;
            Classes.Tarefa.Texto = rtbTexto.Text;

            if (Classes.Tarefa.AvaliaMudancas())
            {
                if (Classes.Tarefa.NovaTarefa)
                {
                    if (ListaMensagens.RetornaDialogo(02) == DialogResult.No)
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    if (ListaMensagens.RetornaDialogo(03) == DialogResult.No)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        Classes.Tarefa.DestravaTarefa();
                    }
                }
            }
        }

        private void Tarefa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                btnSalvar_Click(btnSalvar, e);
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.Alt && e.KeyCode == Keys.S)
            {
                btnSalvarFechar_Click(btnSalvarFechar, e);
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.F1)
            {
                Ajuda telaAjuda = new Ajuda();
                telaAjuda.ShowDialog();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.F2)
            {
                this.Text = "Nova Tarefa";
                cmbEmpresa.SelectedIndex = 0;
                cmbFuncionario.SelectedIndex = 0;
                cmbPrioridade.SelectedIndex = 1;
                cmbStatus.SelectedIndex = 0;
                txtAssunto.Text = "";
                rtbTexto.Text = "";
                dtpInicio.Text = DateTime.Today.ToShortDateString();
                dtpFinal.Text = DateTime.Today.ToShortDateString();
                btnExcluir.Enabled = false;
                e.SuppressKeyPress = true;
            }
        }

        private void CarregaEmpresas()
        {
            cmbEmpresa.DataSource = Classes.Tarefa.ListaClientes;
        }

        private void CarregaFuncionarios()
        {
            cmbFuncionario.DataSource = Classes.Tarefa.ListaFuncionarios;
        }

        private void btnInserirImagem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Filter = "Todos os arquivos |*.*",
                Multiselect = true
            };

            var orgdata = Clipboard.GetDataObject();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string fileName in fileDialog.FileNames)
                {
                    Image img = Image.FromFile(fileName);
                    Clipboard.SetImage(img);
                    rtbTexto.Paste();
                    rtbTexto.Focus();
                }
            }
        }

        private void btnBold_Click(object sender, EventArgs e)
        {
            string nome_fonte = null;
            float tamanho_fonte = 0;
            bool negrito = false;
            nome_fonte = rtbTexto.SelectionFont.Name;
            tamanho_fonte = rtbTexto.SelectionFont.Size;
            negrito = rtbTexto.SelectionFont.Bold;
            if (negrito == false)
            {
                rtbTexto.SelectionFont = new Font(nome_fonte, tamanho_fonte, FontStyle.Bold);
            }
            else
            {
                rtbTexto.SelectionFont = new Font(nome_fonte, tamanho_fonte, FontStyle.Regular);
            }
        }

        private void btnItalico_Click(object sender, EventArgs e)
        {
            string nome_fonte = null;
            float tamanho_fonte = 0;
            bool italico = false;
            nome_fonte = rtbTexto.SelectionFont.Name;
            tamanho_fonte = rtbTexto.SelectionFont.Size;
            italico = rtbTexto.SelectionFont.Italic;
            if (italico == false)
            {
                rtbTexto.SelectionFont = new Font(nome_fonte, tamanho_fonte, FontStyle.Italic);
            }
            else
            {
                rtbTexto.SelectionFont = new Font(nome_fonte, tamanho_fonte, FontStyle.Regular);
            }
        }

        private void btnSublinhado_Click(object sender, EventArgs e)
        {
            string nome_fonte = null;
            float tamanho_fonte = 0;
            bool sublinha = false;
            nome_fonte = rtbTexto.SelectionFont.Name;
            tamanho_fonte = rtbTexto.SelectionFont.Size;
            sublinha = rtbTexto.SelectionFont.Underline;
            if (sublinha == false)
            {
                rtbTexto.SelectionFont = new Font(nome_fonte, tamanho_fonte, FontStyle.Underline);
            }
            else
            {
                rtbTexto.SelectionFont = new Font(nome_fonte, tamanho_fonte, FontStyle.Regular);
            }
        }

        private void dtpInicio_ValueChanged(object sender, EventArgs e)
        {
            if(dtpFinal.Value < dtpInicio.Value)
            {
                dtpFinal.Value = dtpInicio.Value;
            }
        }

        private void dtpFinal_ValueChanged(object sender, EventArgs e)
        {
            if(dtpFinal.Value < dtpInicio.Value)
            {
                dtpInicio.Value = dtpFinal.Value;
            }
        }
        
        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSalvarFechar_Click(object sender, EventArgs e)
        {
            if (txtAssunto.TextLength < 1)
            {
                ListaErro.RetornaErro(29);
                txtAssunto.Focus();
            }
            else
            {
                if (rtbTexto.TextLength < 5)
                {
                    ListaErro.RetornaErro(28);
                    rtbTexto.Focus();
                }
                else
                {
                    try
                    {
                        Classes.Tarefa.Assunto = txtAssunto.Text;
                        Classes.Tarefa.Atribuicao = cmbFuncionario.Text;
                        Classes.Tarefa.DataFinal = dtpFinal.Value.ToString("yyyy-MM-dd");
                        Classes.Tarefa.DataInicial = dtpInicio.Value.ToString("yyyy-MM-dd");
                        Classes.Tarefa.Empresa = cmbEmpresa.Text;
                        Classes.Tarefa.Prioridade = cmbPrioridade.SelectedIndex;
                        Classes.Tarefa.Status = cmbStatus.SelectedIndex + 1;
                        Classes.Tarefa.Texto = rtbTexto.Text;
                        Classes.Tarefa.Travar = false;

                        Classes.Tarefa.Salvar();
                    }
                    catch (NullReferenceException ex)
                    {
                        MessageBox.Show(ex.ToString());
                        throw;
                    }
                    finally
                    {
                        Close();
                    }
                }
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (ListaMensagens.RetornaDialogo(04) == DialogResult.Yes)
            {
                if (Sistema.IDUsuarioLogado != 1 && Sistema.IDUsuarioLogado != 2)
                {
                    string resposta = Interaction.InputBox(ListaMensagens.RetornaInputBox(05)[0], ListaMensagens.RetornaInputBox(05)[1], "");

                    if (resposta == Sistema.SenhaADM)
                    {
                        if (Classes.Tarefa.ApagarTarefa())
                        {
                            ListaMensagens.RetornaMensagem(14, MessageBoxIcon.Information);
                        }
                        else
                        {
                            ListaErro.RetornaErro(18);
                        }
                        this.Close();
                    }
                }
                else
                {
                    if (ListaMensagens.RetornaDialogo(15) == DialogResult.Yes)
                    {
                        if (Classes.Tarefa.ApagarTarefa())
                        {
                            ListaMensagens.RetornaMensagem(14, MessageBoxIcon.Information);
                        }
                        else
                        {
                            ListaErro.RetornaErro(18);
                        }
                        this.Close();
                    }
                }
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtAssunto.TextLength < 1)
            {
                ListaErro.RetornaErro(29);
                txtAssunto.Focus();
            }
            else
            {
                if (rtbTexto.TextLength < 5)
                {
                    ListaErro.RetornaErro(28);
                    rtbTexto.Focus();
                }
                else
                {
                    try
                    {
                        if(Classes.Tarefa.NovaTarefa)
                        {
                            btnImprimir.Enabled = true;
                            btnExcluir.Enabled = true;
                            btnSair.Text = "Sair";
                            this.Text = cmbEmpresa.Text + " - " + txtAssunto.Text;
                        }
                        else
                        {
                            if (Classes.Tarefa.AvaliaMudancas())
                            {
                                this.Text = cmbEmpresa.Text + " - " + txtAssunto.Text;
                            }
                        }

                        Classes.Tarefa.Assunto = txtAssunto.Text;
                        Classes.Tarefa.Atribuicao = cmbFuncionario.Text;
                        Classes.Tarefa.DataFinal = dtpFinal.Value.ToString("yyyy-MM-dd");
                        Classes.Tarefa.DataInicial = dtpInicio.Value.ToString("yyyy-MM-dd");
                        Classes.Tarefa.Empresa = cmbEmpresa.Text;
                        Classes.Tarefa.Prioridade = cmbPrioridade.SelectedIndex;
                        Classes.Tarefa.Status = cmbStatus.SelectedIndex + 1;
                        Classes.Tarefa.Texto = rtbTexto.Text;
                        Classes.Tarefa.Travar = true;

                        Classes.Tarefa.Salvar();
                    }
                    catch (NullReferenceException ex)
                    {
                        MessageBox.Show(ex.ToString());
                        throw;
                    }
                }
            }
        }

        #region Impressão
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if(pdConfiguraImpressao.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pdPrevisualizarImpressao.ShowDialog();
                }
                catch (ArgumentOutOfRangeException)
                {
                    ListaErro.RetornaErro(24);
                }
            }
        }

        private void pdDocumento_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Classes.Tarefa.PrimeiraPagina = true;
            Classes.Tarefa.TextoImpressao = Funcoes.PreparaTexto(rtbTexto.Text, MaxCaracteres);
            pdDocumento.DocumentName = Text;
        }

        private void pdDocumento_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            //Define a fonte do Título
            Font fonteTitulo = new Font("Arial", 12, FontStyle.Bold);
            //Define a fonte do texto
            Font fontetexto = new Font("Arial", 10);

            //Cria um Brush solido com a cor preta
            SolidBrush brush = new SolidBrush(Color.Black);

            //Pega a largura inicial da folha
            int xPosition = e.MarginBounds.X;
            //Pega a altura inicial da folha
            int yPosition = e.MarginBounds.Y;

            int maxCharacters = 0;
            //Calcula a quantidade máxima de caracteres que a linha suporta
            maxCharacters = e.MarginBounds.Width / (int)fontetexto.Size;

            //Posição onde o texto parou
            int position = 0;
            // A variável "posicaoUltimoEspaco" é atribuída, mas seu valor nunca é usado
            int posicaoUltimoEspaco = 0;

            //Nome da empresa que será impresso
            string titulo = "Empresa: " + cmbEmpresa.SelectedItem.ToString();
            //Assunto que será usado
            string assunto = "Assunto: " + txtAssunto.Text;

            g.DrawString(titulo, fonteTitulo, brush, xPosition, yPosition);
            yPosition += fonteTitulo.Height + 20;

            while (assunto.Length - position > maxCharacters)
            {
                if (assunto.Substring(position, 1) == " ")
                {
                    position += 1;
                }
                else
                {
                    //Define a posição do ultimo espaço em branco no texto selecionado
                    posicaoUltimoEspaco = assunto.Substring(position, position + maxCharacters).LastIndexOf(" ");

                    //Escreve o assunto
                    g.DrawString(assunto.Substring(position, posicaoUltimoEspaco), fonteTitulo, brush, xPosition, yPosition);
                    //Soma na altura atual do texto
                    yPosition += fonteTitulo.Height;
                    //Define a nova posição do texto
                    position += posicaoUltimoEspaco;
                }
            }
            if (assunto.Length - position > 0)
            {
                if (assunto.Substring(position, 1) == " ")
                {
                    position += 1;
                }
                g.DrawString(assunto.Substring(position), fonteTitulo, brush, xPosition, yPosition);
                yPosition += fonteTitulo.Height;
            }

            //Configura a cor e o tamanho da linha
            Pen blackPen = new Pen(Color.Black, 3);

            //Cria um espaço de 10pixels 
            yPosition += 10;
            //Cria a linha
            g.DrawLine(blackPen, new Point(xPosition, yPosition), new Point(e.MarginBounds.Width + 100, yPosition));
            //Cria um espaço de 10pixels
            yPosition += 10;

            //A quem a tarefa foi atribuida
            string atribuicao = "Atribuido a: " + cmbFuncionario.SelectedItem.ToString();
            //Qual o período da tarefa
            string periodo = "Data de Início: " + dtpInicio.Value.ToShortDateString() + " - Data de Conclusão: " + dtpFinal.Value.ToShortDateString();
            //Qual o status da tarefa
            string status = "Status: " + cmbStatus.SelectedItem.ToString();
            //Qual a prioridade da tarefa
            string prioridade = "Prioridade: " + cmbPrioridade.SelectedItem.ToString();

            //Imprime a atribuição com a prioridade
            g.DrawString(atribuicao + " | " + prioridade, fontetexto, brush, xPosition, yPosition);
            yPosition += fontetexto.Height;
            //Imprime o período
            g.DrawString(periodo, fontetexto, brush, xPosition, yPosition);
            yPosition += fontetexto.Height;
            //Imprime o status da tarefa
            g.DrawString(status, fontetexto, brush, xPosition, yPosition);
            yPosition += fontetexto.Height;

            //Cria um espaço de 10pixels 
            yPosition += 10;
            //Cria a linha
            g.DrawLine(blackPen, new Point(xPosition, yPosition), new Point(e.MarginBounds.Width + 100, yPosition));
            //Cria um espaço de 10pixels
            yPosition += 10;


            //Imprime o título "Texto: "
            g.DrawString("Texto:", fonteTitulo, brush, xPosition, yPosition);
            yPosition += fonteTitulo.Height * 2;

            int linhasPorPagina = 60;
            int contador = 0;

            //Calcula a quantidade máxima de caracteres que a linha suporta
            maxCharacters = MaxCaracteres;

            //Define a fonte do texto
            Font fonteTexto2 = new Font("Arial", 8);

            if (Classes.Tarefa.PrimeiraPagina)
            {
                Classes.Tarefa.PrimeiraPagina = false;
                leitor = new StringReader(Classes.Tarefa.TextoImpressao);
            }

            //float LeftMargin = e.MarginBounds.Left;
            //float TopMargin = e.MarginBounds.Top;
            string Line = null;

            while (contador < linhasPorPagina && ((Line = leitor.ReadLine()) != null))
            {
                yPosition += fonteTexto2.Height;
                e.Graphics.DrawString(Line, fonteTexto2, brush, xPosition, yPosition, new StringFormat());
                contador++;
            }

            if (Line != null)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }

            brush.Dispose();
        }
        #endregion
    }
}
