using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Gerenciador_de_Tarefas
{
    public partial class Tarefa : Form
    {
        private const int MaxCaracteres = 110;
        #region Variáveis
        private BDCONN conexao = new BDCONN();
        private FuncoesVariaveis funcoesVariaveis = new FuncoesVariaveis();
        private bool novaTarefa = false, salvar = false, primeiraPagina = true;
        private int _idTarefa = 0, _idusuario = 0;
        private string _empresa = null, _atribuicao = null, _assunto = null,
            _dataInicio = null, _dataFinal = null, _status = null, _prioridade = null,
            _texto = null, _textoImprimir = null;
        private StringReader leitor;
        #endregion

        public Tarefa(bool fazerNovaTarefa, int idTarefa, int idusuario)
        {
            InitializeComponent();
            
            CarregaFuncionarios();
            CarregaEmpresas();
            if (fazerNovaTarefa)
            {
                novaTarefa = true;
                btnSair.Text = "Cancelar";
                btnExcluir.Enabled = false;
                btnImprimir.Enabled = false;
            }
            else
            {
                _idTarefa = idTarefa;
                CarregarTarefa(idTarefa);
            }

            _idusuario = idusuario;
        }

        private void Tarefa_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!salvar)
            {
                if (novaTarefa)
                {
                    string erro = ListaMensagens.RetornaMensagem(02);
                    int separador = erro.IndexOf(":");
                    DialogResult resultadoDialogo = MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    if (resultadoDialogo == DialogResult.No)
                    {
                        e.Cancel = true;
                    }
                }
                else
                {

                    if (_assunto != txtAssunto.Text || _atribuicao != cmbFuncionario.SelectedItem.ToString()
                    || _empresa != cmbEmpresa.SelectedItem.ToString() || _dataInicio.Substring(0, 10) != dtpInicio.Text
                    || _dataFinal.Substring(0, 10) != dtpFinal.Text || _prioridade != cmbPrioridade.SelectedIndex.ToString()
                    || Int32.Parse(_status) != (cmbStatus.SelectedIndex + 1) || _texto != rtbTexto.Text)
                    {
                        string erro = ListaMensagens.RetornaMensagem(03);
                        int separador = erro.IndexOf(":");
                        DialogResult resultadoDialogo = MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (resultadoDialogo == DialogResult.No)
                        {
                            e.Cancel = true;
                        }
                    }
                }
            }
            else
            {
                funcoesVariaveis.DestravaTarefa(_idTarefa);
            }
        }

        private void Tarefa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                Salvar();
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.Alt && e.KeyCode == Keys.S)
            {
                SalvarFechar();
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
                novaTarefa = true;
                cmbEmpresa.SelectedIndex = 0;
                cmbFuncionario.SelectedIndex = 0;
                cmbPrioridade.SelectedIndex = 1;
                cmbStatus.SelectedIndex = 0;
                txtAssunto.Text = "";
                rtbTexto.Text = "";
                dtpInicio.Text = DateTime.Today.ToShortDateString();
                dtpFinal.Text = DateTime.Today.ToShortDateString();
                btnExcluir.Enabled = false;
                _idTarefa = 0;
                e.SuppressKeyPress = true;
            }
        }

        private void CarregarTarefa(int idTarefa)
        {
            if (!novaTarefa)
            {
                List<string> lista = conexao.ConsultaTarefas("select tbl_contato.nome AS 'empresa', tbl_funcionarios.nome as 'funcionario', " +
                    "tbl_tarefas.`status`, tbl_tarefas.assunto, tbl_tarefas.datainicial, tbl_tarefas.datafinal, tbl_tarefas.prioridade, tbl_tarefas.texto from tbl_tarefas " +
                    "Join tbl_contato on tbl_contato.ID = tbl_tarefas.Empresa " +
                    "Join tbl_funcionarios on tbl_funcionarios.id = tbl_tarefas.Funcionario " +
                    "Where tbl_tarefas.id = " + idTarefa + ";");


                cmbEmpresa.SelectedIndex = cmbEmpresa.FindStringExact(lista[0]);
                cmbFuncionario.SelectedIndex = cmbFuncionario.FindStringExact(lista[1]);
                cmbStatus.SelectedIndex = Int32.Parse(lista[2]) - 1;
                txtAssunto.Text = lista[3];
                dtpInicio.Text = lista[4];
                if (lista[5] == "" || lista[5] == null)
                {
                    dtpFinal.Text = dtpInicio.Text;
                }
                else
                {
                    dtpFinal.Text = lista[5];
                }

                cmbPrioridade.SelectedIndex = Int32.Parse(lista[6]);
                rtbTexto.Text = lista[7];
                this.Text = lista[0] + " - " + lista[3];

                
                _empresa = lista[0];
                _atribuicao = lista[1];
                _status = lista[2];
                _assunto = lista[3];
                _dataInicio = lista[4];
                if (lista[5] == "" || lista[5] == null)
                {
                    _dataFinal = _dataInicio;
                }
                else
                {
                    _dataFinal = lista[5];
                }
                _prioridade = lista[6];
                _texto = lista[7];
            }
        }

        private void CarregaEmpresas()
        {
            List<string> lista = conexao.PreencheCMB("Select tbl_contato.nome from tbl_contato;");
            cmbEmpresa.DataSource = lista;
        }

        private void CarregaFuncionarios()
        {
            List<string> lista = conexao.PreencheCMB("Select nome from tbl_funcionarios;");
            cmbFuncionario.DataSource = lista;
        }

        private void SalvarFechar()
        {
            if (txtAssunto.TextLength < 5)
            {
                string erro = ListaErro.RetornaErro(28);
                int separador = erro.IndexOf(":");
                DialogResult resultadoDialogo = MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAssunto.Focus();
            }
            else
            {
                if (rtbTexto.TextLength < 1)
                {
                    string erro = ListaErro.RetornaErro(29);
                    int separador = erro.IndexOf(":");
                    DialogResult resultadoDialogo = MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    rtbTexto.Focus();
                }
                else
                {
                    try
                    {
                        if (novaTarefa)
                        {
                            funcoesVariaveis.CadastrarTarefa(cmbEmpresa.Text, cmbFuncionario.Text, (cmbStatus.SelectedIndex + 1), txtAssunto.Text,
                                dtpInicio.Value.ToString("yyyy-MM-dd"), dtpFinal.Value.ToString("yyyy-MM-dd"), cmbPrioridade.SelectedIndex, rtbTexto.Text, false);
                        }
                        else
                        {
                            if (!funcoesVariaveis.AtualizarTarefa(_idTarefa, cmbEmpresa.Text, cmbFuncionario.Text, (cmbStatus.SelectedIndex + 1),
                                txtAssunto.Text, dtpInicio.Value.ToString("yyyy-MM-dd"), dtpFinal.Value.ToString("yyyy-MM-dd"), cmbPrioridade.SelectedIndex,
                                rtbTexto.Text))
                            {
                                string erro = ListaErro.RetornaErro(17);
                                int separador = erro.IndexOf(":");
                                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (NullReferenceException e)
                    {
                        MessageBox.Show(e.ToString());
                        throw;
                    }
                    finally
                    {
                        this.Close();
                    }
                }
            }
        }

        private void btnInserirImagem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Todos os arquivos |*.*";
            fileDialog.Multiselect = true;

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

        private void Salvar()
        {
            if (txtAssunto.TextLength < 5)
            {
                MessageBox.Show("A quantidade de caracteres do assunto é muito baixo,\nfavor colocar mais do que 5 caracteres!", "Poucos Caracteres", MessageBoxButtons.OK);
                txtAssunto.Focus();
            }
            else
            {
                if (rtbTexto.TextLength < 1)
                {
                    MessageBox.Show("O texto não pode estar vazio, por favor coloque ao menos um ponto!", "Poucos Caracteres", MessageBoxButtons.OK);
                    rtbTexto.Focus();
                }
                else
                {
                    if (novaTarefa)
                    {
                        _idTarefa = funcoesVariaveis.CadastrarTarefa(cmbEmpresa.Text, cmbFuncionario.Text, (cmbStatus.SelectedIndex + 1), txtAssunto.Text,
                            dtpInicio.Value.ToString("yyyy-MM-dd"), dtpFinal.Value.ToString("yyyy-MM-dd"), cmbPrioridade.SelectedIndex, rtbTexto.Text, true);

                        novaTarefa = false;
                        btnImprimir.Enabled = true;
                        btnExcluir.Enabled = true;
                        btnSair.Text = "Sair";

                        _empresa = cmbEmpresa.SelectedItem.ToString();
                        _atribuicao = cmbFuncionario.SelectedItem.ToString();
                        _status = (cmbStatus.SelectedIndex + 1).ToString();
                        _assunto = txtAssunto.Text;
                        _dataInicio = dtpInicio.Text;
                        _dataFinal = dtpFinal.Text;
                        _prioridade = cmbPrioridade.SelectedIndex.ToString();
                        _texto = rtbTexto.Text;

                        this.Text = cmbEmpresa.Text + " - " + txtAssunto.Text;
                    }
                    else
                    {
                        if (!funcoesVariaveis.AtualizarTarefa(_idTarefa, cmbEmpresa.Text, cmbFuncionario.Text, (cmbStatus.SelectedIndex + 1),
                            txtAssunto.Text, dtpInicio.Value.ToString("yyyy-MM-dd"), dtpFinal.Value.ToString("yyyy-MM-dd"), cmbPrioridade.SelectedIndex,
                            rtbTexto.Text))
                        {
                            string erro = ListaErro.RetornaErro(17);
                            int separador = erro.IndexOf(":");
                            MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
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
            this.Close();
        }

        private void btnSalvarFechar_Click(object sender, EventArgs e)
        {
            salvar = true;
            SalvarFechar();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            string erro = ListaMensagens.RetornaMensagem(04);
            int separador = erro.IndexOf(":");
            DialogResult resultadoDialogo = MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (resultadoDialogo == DialogResult.Yes)
            {
                if (_idusuario != 1 && _idusuario != 2)
                {
                    erro = ListaMensagens.RetornaMensagem(05);
                    separador = erro.IndexOf(":");
                    string resposta = Microsoft.VisualBasic.Interaction.InputBox(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), "");

                    if (resposta == "MB8719")
                    {
                        if (funcoesVariaveis.ApagarTarefa(_idTarefa))
                        {
                            erro = ListaMensagens.RetornaMensagem(14);
                            separador = erro.IndexOf(":");
                            MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            erro = ListaErro.RetornaErro(18);
                            separador = erro.IndexOf(":");
                            MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        this.Close();
                    }
                }
                else
                {
                    erro = ListaMensagens.RetornaMensagem(15);
                    separador = erro.IndexOf(":");
                    resultadoDialogo = MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resultadoDialogo == DialogResult.Yes)
                    {
                        if (funcoesVariaveis.ApagarTarefa(_idTarefa))
                        {
                            erro = ListaMensagens.RetornaMensagem(14);
                            separador = erro.IndexOf(":");
                            MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            erro = ListaErro.RetornaErro(18);
                            separador = erro.IndexOf(":");
                            MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        this.Close();
                    }
                }
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Salvar();
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
                    string erro = ListaErro.RetornaErro(24);
                    int separador = erro.IndexOf(":");
                    MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pdDocumento_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            primeiraPagina = true;
            _textoImprimir = "";
            _textoImprimir = FuncoesEstaticas.PreparaTexto(rtbTexto.Text, MaxCaracteres);
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

            if (primeiraPagina)
            {
                primeiraPagina = false;
                leitor = new StringReader(_textoImprimir);
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
