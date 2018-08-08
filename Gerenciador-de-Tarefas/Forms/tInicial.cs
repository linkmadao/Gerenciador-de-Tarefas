﻿using System;
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
using System.IO;
using System.Collections;
using System.Drawing.Printing;
using System.Xml;
using System.Xml.Linq;
using System.Net;
using System.Collections.Specialized;
using Newtonsoft.Json;
using System.Web;

namespace Gerenciador_de_Tarefas
{
    public partial class tInicial : Form
    {
        #region Variáveis
        private BDCONN conexao = new BDCONN();
        private FuncoesVariaveis funcoes = new FuncoesVariaveis();
        private int idUsuario, segundos = 0, _idFornecedor;
        private string tituloSoftware = "Gerenciador de Tarefas - CFTVA " + DateTime.Now.Year;
        private string nomeXML = "bdconfig.xml";
        private bool programaDesativado = false;
        private bool iniciaTelaClientes = true, iniciaTelaFornecedores = true, iniciaTelaTarefas = true, iniciaTelaNovoFornecedor = true;
        public static Panel pOpcoes;
        #endregion

        #region telaInicial
        public tInicial(int user)
        {
            InitializeComponent();
            idUsuario = user;

            string comando = "select user from tbl_usuarios where id = " + user + ";";
            string nomeUsuario = conexao.ConsultaSimples(comando);

            lblUsuario.Text = "Usuário: " + nomeUsuario.ToUpper();

            pOpcoes = panelOpcoes;
        }

        private void tInicial_Load(object sender, EventArgs e)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);

            //Titulo Software
            lblVersao.Text = "Versão: " + fvi.FileVersion;

            //Coloca a hora
            lblHorario.Text = "Hora: " + DateTime.Now.ToShortTimeString();

            EscondePaineis();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tInicial_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (programaDesativado)
            {
                string erro = ListaErro.RetornaErro(14);
                int separador = erro.IndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);

                programaDesativado = false;
            }

            string comando = "Insert into tbl_log values (0," + idUsuario + ", 'Logoff efetuado - " +
                DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "');";
            conexao.ExecutaComando(comando);
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            if (!panelClientes.Visible)
            {
                EscondePaineis();
                panelClientes.Visible = true;
                panelClientes.Enabled = true;
                panelClientes.Show();

                if (iniciaTelaClientes)
                {
                    AtualizaDGVClientes();

                    for (int i = 0; i < dgvClientes.Columns.Count; i++)
                    {
                        dgvClientes.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }

                dgvClientes.Focus();
            }
        }

        private void btnFornecedores_Click(object sender, EventArgs e)
        {
            if (!panelFornecedores.Visible)
            {
                EscondePaineis();
                panelFornecedores.Visible = true;
                panelFornecedores.Enabled = true;
                panelFornecedores.Show();

                AtualizaDGVFornecedores();
                dgvFornecedores.Focus();
            }
        }

        private void btnTarefas_Click(object sender, EventArgs e)
        {
            if (!panelTarefas.Visible)
            {
                EscondePaineis();
                panelTarefas.Visible = true;
                panelTarefas.Enabled = true;
                panelTarefas.Show();

                AtualizaDGVTarefas();
                dgvTarefas.Focus();
            }
        }

        private void btnOpcoes_Click(object sender, EventArgs e)
        {
            if (!panelOpcoes.Visible)
            {
                EscondePaineis();

                XElement xml = XElement.Load(nomeXML);
                foreach (XElement x in xml.Elements())
                {
                    if (x.Attribute("servidor").Value != "" || x.Attribute("servidor").Value != "localhost" || x.Attribute("servidor").Value != "127.0.0.1")
                    {
                        rdbtnRemoto.Checked = true;
                        rdbtnServidorLocal.Checked = false;
                        txtServidor.Text = x.Attribute("servidor").Value;
                        txtServidor.Enabled = true;
                    }

                    txtBanco.Text = x.Attribute("banco").Value;
                    txtUid.Text = x.Attribute("uid").Value;
                    txtPwd.Text = x.Attribute("pwd").Value;
                }

                panelOpcoes.Show();
                panelOpcoes.Enabled = true;
                panelOpcoes.Visible = true;
            }
        }

        private void timerHora_Tick(object sender, EventArgs e)
        {
            lblHorario.Text = "Hora: " + DateTime.Now.ToShortTimeString();

            if (segundos < 30)
            {
                segundos++;
            }
            else
            {
                if (panelClientes.Visible)
                {
                    AtualizaDGVClientes();
                }
                else if (panelFornecedores.Visible)
                {

                }
                else if (panelTarefas.Visible)
                {
                    AtualizaDGVTarefas();
                }

                segundos = 0;
            }
        }

        private void EscondePaineis()
        {
            panelClientes.Visible = false;
            panelClientes.Enabled = false;
            panelClientes.Hide();

            panelFornecedores.Visible = false;
            panelClientes.Enabled = false;
            panelFornecedores.Hide();

            panelNF.Visible = false;
            panelNF.Enabled = false;
            panelNF.Hide();

            panelTarefas.Visible = false;
            panelTarefas.Enabled = false;
            panelTarefas.Hide();

            panelOpcoes.Visible = false;
            panelOpcoes.Enabled = false;
            panelOpcoes.Hide();
        }

        private void tInicial_KeyDown(object sender, KeyEventArgs e)
        {
            //O painel de clientes está visível?
            if (panelClientes.Visible)
            {
                //Se apertar CTRL + N
                if (e.Control && e.KeyCode == Keys.N)
                {
                    //Tarefa(é uma nova tarefa?, id da tarefa);
                    //Ex tarefa nova: Tarefa(true,0);
                    //Ex tarefa já existente: Tarefa(false,1);

                    CadastraCliente cadastraCliente = new CadastraCliente(true, 0);
                    cadastraCliente.ShowDialog();
                    AtualizaDGVClientes();
                    //tContador.Interval += intervaloTemporizador;
                    //tContador.Start();
                    e.SuppressKeyPress = true;
                }
                //Se apertar CTRL + P
                else if (e.Control && e.KeyCode == Keys.P)
                {
                    ImprimirClientes();
                    e.SuppressKeyPress = true;
                }
                //Se apertar CTRL + F
                else if (e.Control && e.KeyCode == Keys.F)
                {
                    TelaPesquisa telaPesquisaClientes = new TelaPesquisa(dgvClientes, true);
                    telaPesquisaClientes.ShowDialog();

                    if (FuncoesEstaticas.ClientePesquisado != -1)
                    {
                        if (FuncoesEstaticas.ClientePesquisado != 0)
                        {
                            dgvClientes[0, FuncoesEstaticas.ClientePesquisado].Selected = true;
                        }
                    }
                    else
                    {
                        string erro = ListaErro.RetornaErro(48);
                        int separador = erro.LastIndexOf(":");
                        MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    e.SuppressKeyPress = true;
                }
                //Se apertar ESC
                else if (e.KeyCode == Keys.Escape)
                {
                    panelClientes.Visible = false;
                    e.SuppressKeyPress = true;
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    if (dgvClientes.Focused)
                    {
                        //Executa a mesma função de dar 2 cliques com o mouse no cliente
                        dgvClientes_CellDoubleClick(dgvClientes, new DataGridViewCellEventArgs(dgvClientes.CurrentCell.ColumnIndex, dgvClientes.CurrentRow.Index));
                    }
                }
            }
            //O painel de fornecedores está visível?
            else if (panelFornecedores.Visible)
            {
                // Se apertar CTRL +N
                if (e.Control && e.KeyCode == Keys.N)
                {
                }
                //Se apertar CTRL + P
                else if (e.Control && e.KeyCode == Keys.P)
                {
                }
                //Se apertar ESC
                else if (e.KeyCode == Keys.Escape)
                {
                    panelFornecedores.Visible = false;
                    e.SuppressKeyPress = true;
                }
            }
            //O painel de tarefas está visível?
            else if (panelTarefas.Visible)
            {
                // Se apertar CTRL +N
                if (e.Control && e.KeyCode == Keys.N)
                {
                    //Tarefa(é uma nova tarefa?, id da tarefa);
                    //Ex tarefa nova: Tarefa(true,0);
                    //Ex tarefa já existente: Tarefa(false,1);

                    Tarefa ntarefa = new Tarefa(true, 0, idUsuario);
                    ntarefa.ShowDialog();
                    AtualizaDGVTarefas();

                    e.SuppressKeyPress = true;
                }
                //Se apertar CTRL + P
                else if (e.Control && e.KeyCode == Keys.P)
                {
                    ImprimirTarefas();
                    e.SuppressKeyPress = true;
                }
                //Se apertar ESC
                else if (e.KeyCode == Keys.Escape)
                {
                    panelTarefas.Visible = false;
                    e.SuppressKeyPress = true;
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    if (dgvTarefas.Focused)
                    {
                        //Executa a mesma função de dar 2 cliques com o mouse na tarefa
                        dgvTarefas_CellDoubleClick(dgvTarefas, new DataGridViewCellEventArgs(dgvTarefas.CurrentCell.ColumnIndex, dgvTarefas.CurrentRow.Index));
                    }
                }
            }
            //O painel de fornecedores está visível?
            else if (panelOpcoes.Visible)
            {
                //Se apertar CTRL + S
                if (e.Control && e.KeyCode == Keys.S)
                {
                    bool abreprograma = false;

                    if (rdbtnServidorLocal.Checked)
                    {
                        if (conexao.TestaConexao(txtBanco.Text, txtUid.Text, txtPwd.Text))
                        {
                            abreprograma = true;
                        }
                    }
                    else
                    {
                        if (conexao.TestaConexao(txtServidor.Text, txtBanco.Text, txtUid.Text, txtPwd.Text))
                        {
                            abreprograma = true;
                        }
                    }

                    if (abreprograma)
                    {
                        XElement xml = XElement.Load(nomeXML);
                        XElement x = xml.Elements().First();
                        if (x != null)
                        {
                            if (rdbtnRemoto.Checked && txtServidor.Text != "")
                            {
                                x.Attribute("servidor").SetValue(txtServidor.Text);
                            }
                            x.Attribute("banco").SetValue(txtBanco.Text);
                            x.Attribute("uid").SetValue(txtUid.Text);
                            x.Attribute("pwd").SetValue(txtPwd.Text);
                        }

                        xml.Save(nomeXML);

                        MessageBox.Show("Dados atualizados com sucesso!");
                    }
                }
                //Se apertar CTRL + D
                else if (e.Control && e.KeyCode == Keys.D)
                {
                    DialogResult resultadoDialogo = MessageBox.Show("Você tem certeza de que deseja destravar todas as tarefas?\nTodas as outras estações devem estar fechadas!", "Destravar Tarefas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resultadoDialogo == DialogResult.Yes)
                    {
                        string resposta = Microsoft.VisualBasic.Interaction.InputBox("Digite a senha para prosseguir", "Destravar Tarefas", "");

                        if (resposta == "MB8719")
                        {
                            if (funcoes.DestravaTodasTarefas())
                            {
                                MessageBox.Show("As tarefas foram destravadas com sucesso.");
                            }
                        }
                    }
                }
                //Se apertar CTRL + B
                else if (e.Control && e.KeyCode == Keys.B)
                {
                    Backup();
                }
                //Se apertar CTRL + R
                else if (e.Control && e.KeyCode == Keys.R)
                {

                }
                //Se apertar ESC
                else if (e.KeyCode == Keys.Escape)
                {
                    panelOpcoes.Visible = false;
                    e.SuppressKeyPress = true;
                }
            }
            else
            {
                //Se apertar o F1
                if (e.KeyCode == Keys.F1)
                {
                    //O painel de clientes não está visível?
                    if (!panelClientes.Visible)
                    {
                        EscondePaineis();
                        panelClientes.Visible = true;
                        panelClientes.Show();

                        AtualizaDGVClientes();

                        for (int i = 0; i < dgvClientes.Columns.Count; i++)
                        {
                            dgvClientes.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                        }
                        dgvClientes.Focus();
                    }
                }
                //Se apertar o F2
                else if (e.KeyCode == Keys.F2)
                {
                    //O painel de Fornecedores não está visível?
                    if (!panelFornecedores.Visible)
                    {
                        EscondePaineis();
                        panelFornecedores.Visible = true;
                        panelFornecedores.Show();

                        AtualizaDGVFornecedores();
                    }
                }
                //Se apertar o F3
                else if (e.KeyCode == Keys.F3)
                {
                    //O painel de Tarefas não está visível?
                    if (!panelTarefas.Visible)
                    {
                        EscondePaineis();
                        panelTarefas.Visible = true;
                        panelTarefas.Show();

                        AtualizaDGVTarefas();

                        dgvTarefas.Focus();
                    }
                }
                //Se apertar o F4
                else if (e.KeyCode == Keys.F4)
                {
                    EscondePaineis();

                    XElement xml = XElement.Load(nomeXML);
                    foreach (XElement x in xml.Elements())
                    {
                        if (x.Attribute("servidor").Value != "" || x.Attribute("servidor").Value != "localhost" || x.Attribute("servidor").Value != "127.0.0.1")
                        {
                            rdbtnRemoto.Checked = true;
                            rdbtnServidorLocal.Checked = false;
                            txtServidor.Text = x.Attribute("servidor").Value;
                            txtServidor.Enabled = true;
                        }

                        txtBanco.Text = x.Attribute("banco").Value;
                        txtUid.Text = x.Attribute("uid").Value;
                        txtPwd.Text = x.Attribute("pwd").Value;
                    }

                    panelOpcoes.Show();
                    panelOpcoes.Visible = true;
                }
                //Se apertar o ESC
                else if (e.KeyCode == Keys.Escape)
                {
                    Application.Exit();
                    e.SuppressKeyPress = true;
                }
            }
        }
        #endregion

        #region Clientes
        /// <summary>
        /// Método responsável por atualizar a tabela da tela inicial
        /// </summary>
        /// 
        private DataGridView _dgvClientesAtual = new DataGridView();

        private void AtualizaDGVClientes()
        {
            if (conexao.TestaConexao())
            {
                string comando = FuncoesEstaticas.FiltroClientes(cmbFiltroClientes.SelectedIndex);

                DataGridView _dgvTemp = new DataGridView();
                int linhaAtual = 0, colunaAtual = 0, posvertical = 0;

                if (iniciaTelaClientes)
                {
                    _dgvClientesAtual.DataSource = conexao.PreencheDGV(comando).Tables[0];
                    _dgvTemp.DataSource = conexao.PreencheDGV(comando).Tables[0];
                    dgvClientes.DataSource = _dgvClientesAtual.DataSource;

                    dgvClientes.Rows[0].Selected = false;

                    iniciaTelaClientes = false;
                }
                else
                {
                    //Tenta pegar as posições de onde estava antes de atualizar.
                    try
                    {
                        linhaAtual = dgvClientes.CurrentCell.RowIndex;
                        colunaAtual = dgvClientes.CurrentCell.ColumnIndex;
                        posvertical = dgvClientes.FirstDisplayedScrollingRowIndex;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        linhaAtual = dgvClientes.CurrentCell.RowIndex;
                        colunaAtual = dgvClientes.CurrentCell.ColumnIndex;
                    }

                    //Atualiza a tabela atual temporária
                    _dgvClientesAtual.DataSource = conexao.PreencheDGV(comando).Tables[0];

                    //Se a tabela atualizada for diferente da tabela anterior
                    if (_dgvClientesAtual != _dgvTemp)
                    {
                        dgvClientes.DataSource = _dgvClientesAtual.DataSource;
                        _dgvTemp = _dgvClientesAtual;
                    }
                    else
                    {
                        return;
                    }

                    //Desmarca a primeira linha
                    dgvClientes.Rows[0].Selected = false;

                    //Tenta selecionar a linha
                    try
                    {
                        dgvClientes[colunaAtual, linhaAtual].Selected = true;
                        dgvClientes.FirstDisplayedScrollingRowIndex = posvertical;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        try
                        {
                            dgvClientes[colunaAtual, linhaAtual].Selected = true;
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            dgvClientes[0, 0].Selected = true;
                            dgvClientes.FirstDisplayedScrollingRowIndex = 0;
                        }
                    }
                }

                try
                {
                    foreach (DataGridViewRow dgvr in dgvClientes.Rows)
                    {
                        if (dgvr.Cells["contrato"].Value.ToString() == "2")
                        {
                            dgvr.DefaultCellStyle.BackColor = Color.LightBlue;
                        }
                        else if (dgvr.Cells["contrato"].Value.ToString() == "3")
                        {
                            dgvr.DefaultCellStyle.BackColor = Color.Yellow;
                        }
                        else if (dgvr.Cells["contrato"].Value.ToString() == "4")
                        {
                            dgvr.DefaultCellStyle.BackColor = Color.YellowGreen;
                        }
                    }
                }
                catch (NullReferenceException)
                {
                    return;
                }

                //Esconde a coluna contrato
                dgvClientes.Columns["contrato"].Visible = false;
            }
        }

        private void dgvClientes_DataSourceChanged(object sender, EventArgs e)
        {
            dgvClientes.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvClientes.Columns[2].MinimumWidth = 150;
        }

        private void dgvClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                int idCliente = 0, posicaoAtualScroll = dgvClientes.FirstDisplayedScrollingRowIndex;


                DataGridViewRow linha = dgvClientes.Rows[e.RowIndex];
                idCliente = funcoes.AbreCliente(linha.Cells["Nome"].Value.ToString());

                string comando = "Insert into tbl_log values (0," + idUsuario + ", 'Abriu cliente ID: " + idCliente + " - " +
                DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "');";
                conexao.ExecutaComando(comando);

                CadastraCliente cadastraCliente = new CadastraCliente(false, idCliente);
                cadastraCliente.ShowDialog();

                AtualizaDGVClientes();
                dgvClientes.FirstDisplayedScrollingRowIndex = posicaoAtualScroll;
            }
        }

        private void btnAddCliente_Click(object sender, EventArgs e)
        {
            int posicaoAtualScroll = dgvClientes.FirstDisplayedScrollingRowIndex;

            CadastraCliente cadastraCliente = new CadastraCliente(true, 0);
            cadastraCliente.ShowDialog();

            AtualizaDGVClientes();
            dgvClientes.FirstDisplayedScrollingRowIndex = posicaoAtualScroll;
        }

        private void btnPesqCliente_Click(object sender, EventArgs e)
        {
            TelaPesquisa telaPesquisaCliente = new TelaPesquisa(dgvClientes, true);
            telaPesquisaCliente.ShowDialog();

            if (FuncoesEstaticas.ClientePesquisado != -1)
            {
                dgvClientes[0, FuncoesEstaticas.ClientePesquisado].Selected = true;
            }
            else
            {
                string erro = ListaErro.RetornaErro(48);
                int separador = erro.LastIndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            dgvClientes.Focus();
        }

        private void cmbFiltroClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizaDGVClientes();
            dgvClientes.Focus();
        }

        #region Impressão

        StringFormat strFormat; //Used to format the grid rows.
        ArrayList arrColumnLefts = new ArrayList();//Used to save left coordinates of columns
        ArrayList arrColumnWidths = new ArrayList();//Used to save column widths
        int iCellHeight = 0; //Used to get/set the datagridview cell height
        int iTotalWidth = 0; //
        int iRow = 0;//Used as counter
        bool bFirstPage = false; //Used to check whether we are printing first page
        bool bNewPage = false;// Used to check whether we are printing a new page
        int iHeaderHeight = 0; //Used for the header height

        private void btnPrintListaClientes_Click(object sender, EventArgs e)
        {
            ImprimirClientes();
        }

        private void pdClientes_BeginPrint(object sender, PrintEventArgs e)
        {
            try
            {
                strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Near;
                strFormat.LineAlignment = StringAlignment.Center;
                strFormat.Trimming = StringTrimming.EllipsisCharacter;

                arrColumnLefts.Clear();
                arrColumnWidths.Clear();
                iCellHeight = 0;
                iRow = 0;
                bFirstPage = true;
                bNewPage = true;

                // Calculating Total Widths
                iTotalWidth = 0;
                foreach (DataGridViewColumn dgvGridCol in dgvClientes.Columns)
                {
                    iTotalWidth += dgvGridCol.Width;
                }
            }
            catch (Exception)
            {
                string erro = ListaErro.RetornaErro(23);
                int separador = erro.LastIndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ImprimirClientes()
        {
            if (pdImprimirClientes.ShowDialog() == DialogResult.OK)
            {


                pdClientes.DefaultPageSettings.Landscape = true;
                pdClientes.DocumentName = "Lista de Clientes - CFTVA " + DateTime.Now.Date.ToShortDateString();

                pPreviewClientes.ShowDialog();
                AtualizaDGVClientes();
            }

        }

        private void pdClientes_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                //Set the left margin
                int iLeftMargin = e.MarginBounds.Left;
                //Set the top margin
                int iTopMargin = e.MarginBounds.Top;
                //Whether more pages have to print or not
                bool bMorePagesToPrint = false;
                int iTmpWidth = 0;

                //For the first page to print set the cell width and header height
                if (bFirstPage)
                {
                    foreach (DataGridViewColumn GridCol in dgvClientes.Columns)
                    {
                        if (GridCol.Name != "contrato")
                        {
                            iTmpWidth = (int)(Math.Floor((double)((double)GridCol.Width /
                            (double)iTotalWidth * (double)iTotalWidth *
                            ((double)e.MarginBounds.Width / (double)iTotalWidth))));

                            iHeaderHeight = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
                                GridCol.InheritedStyle.Font, iTmpWidth).Height) + 11;

                            // Save width and height of headers
                            arrColumnLefts.Add(iLeftMargin);
                            arrColumnWidths.Add(iTmpWidth);
                            iLeftMargin += iTmpWidth;
                        }
                    }
                }
                //Loop till all the grid rows not get printed
                while (iRow <= dgvClientes.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = dgvClientes.Rows[iRow];
                    //Set the cell height
                    iCellHeight = GridRow.Height + 12;
                    int iCount = 0;
                    //Check whether the current page settings allows more rows to print
                    if (iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }
                    else
                    {
                        if (bNewPage)
                        {
                            //Draw Header
                            e.Graphics.DrawString("Lista de Clientes",
                                new Font(dgvClientes.Font, FontStyle.Bold),
                                Brushes.Black, e.MarginBounds.Left,
                                e.MarginBounds.Top - e.Graphics.MeasureString("Lista de Clientes",
                                new Font(dgvClientes.Font, FontStyle.Bold),
                                e.MarginBounds.Width).Height - 13);

                            String strDate = DateTime.Now.ToLongDateString() + " " +
                                DateTime.Now.ToShortTimeString();
                            //Draw Date
                            e.Graphics.DrawString(strDate,
                                new Font(dgvClientes.Font, FontStyle.Bold), Brushes.Black,
                                e.MarginBounds.Left +
                                (e.MarginBounds.Width - e.Graphics.MeasureString(strDate,
                                new Font(dgvClientes.Font, FontStyle.Bold),
                                e.MarginBounds.Width).Width),
                                e.MarginBounds.Top - e.Graphics.MeasureString("Lista de Clientes",
                                new Font(new Font(dgvClientes.Font, FontStyle.Bold),
                                FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            //Draw Columns                 
                            iTopMargin = e.MarginBounds.Top;
                            foreach (DataGridViewColumn GridCol in dgvClientes.Columns)
                            {
                                if (GridCol.Name != "contrato")
                                {
                                    e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
                                    new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight));

                                    e.Graphics.DrawRectangle(Pens.Black,
                                        new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                        (int)arrColumnWidths[iCount], iHeaderHeight));

                                    e.Graphics.DrawString(GridCol.HeaderText,
                                        GridCol.InheritedStyle.Font,
                                        new SolidBrush(GridCol.InheritedStyle.ForeColor),
                                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                        (int)arrColumnWidths[iCount], iHeaderHeight), strFormat);
                                    iCount++;
                                }
                            }
                            bNewPage = false;
                            iTopMargin += iHeaderHeight;
                        }
                        iCount = 0;
                        //Draw Columns Contents                
                        foreach (DataGridViewCell Cel in GridRow.Cells)
                        {
                            if (Cel.ColumnIndex != 4)
                            {
                                if (Cel.Value != null)
                                {
                                    string texto = Cel.Value.ToString();

                                    if (Cel.ColumnIndex == 0)
                                    {
                                        if (texto.Length > 50)
                                        {
                                            texto = texto.Substring(0, 47) + "...";
                                        }
                                    }

                                    e.Graphics.DrawString(texto,
                                    Cel.InheritedStyle.Font,
                                    new SolidBrush(Cel.InheritedStyle.ForeColor),
                                    new RectangleF((int)arrColumnLefts[iCount],
                                    (float)iTopMargin,
                                    (int)arrColumnWidths[iCount], (float)iCellHeight),
                                    strFormat);
                                }

                                //Drawing Cells Borders 
                                e.Graphics.DrawRectangle(Pens.Black,
                                    new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iCellHeight));
                                iCount++;
                            }
                        }
                    }
                    iRow++;
                    iTopMargin += iCellHeight;
                }
                //If more lines exist, print another page.
                if (bMorePagesToPrint)
                    e.HasMorePages = true;
                else
                    e.HasMorePages = false;
            }
            catch (Exception)
            {
                string erro = ListaErro.RetornaErro(24);
                int separador = erro.LastIndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #endregion 

        #region Tarefas
        private void btnNovaTarefa_Click(object sender, EventArgs e)
        {
            //Tarefa(é uma nova tarefa?, id da tarefa);
            //Ex tarefa nova: Tarefa(true,0);
            //Ex tarefa já existente: Tarefa(false,1);

            Tarefa ntarefa = new Tarefa(true, 0, idUsuario);
            ntarefa.ShowDialog();
            //AtualizaDGV();
            //tContador.Interval += intervaloTemporizador;
            //tContador.Start();
        }

        /// <summary>
        /// Método responsável por atualizar a tabela da tela inicial
        /// </summary>

        private DataGridView _dgvTarefasAtual = new DataGridView(), _dgvTarefasConcluidas = new DataGridView(), _dgvTodasTarefas = new DataGridView();
        private DataGridView _dgvTempTarefas = new DataGridView(), _dgvTempTarefasConcluidas = new DataGridView(), _dgvTempTodasTarefas = new DataGridView();

        private void AtualizaDGVTarefas()
        {
            if (conexao.TestaConexao())
            {
                int linhaAtual = 0, colunaAtual = 0, posvertical = 0;

                if (iniciaTelaTarefas)
                {
                    //Atualiza a tabela tarefas pendentes
                    _dgvTarefasAtual.DataSource = conexao.PreencheDGV(funcoes.VerificaComboBoxTarefas(0)).Tables[0];
                    _dgvTempTarefas.DataSource = _dgvTarefasAtual.DataSource;
                    dgvTarefas.DataSource = _dgvTarefasAtual.DataSource;

                    //Atualiza a tabela tarefas concluidas
                    _dgvTarefasConcluidas.DataSource = conexao.PreencheDGV(funcoes.VerificaComboBoxTarefas(1)).Tables[0];
                    _dgvTempTarefasConcluidas.DataSource = _dgvTarefasConcluidas.DataSource;

                    //Atualiza a tabela tarefas temporária
                    _dgvTodasTarefas.DataSource = conexao.PreencheDGV(funcoes.VerificaComboBoxTarefas(2)).Tables[0];
                    _dgvTempTodasTarefas.DataSource = _dgvTodasTarefas.DataSource;

                    iniciaTelaTarefas = false;


                    //Esconde a coluna ID
                    dgvTarefas.Columns[0].Visible = false;
                    //Esconde a coluna Prioridade
                    dgvTarefas.Columns["Prioridade"].Visible = false;
                    //Esconde a coluna Data Conclusão
                    dgvTarefas.Columns["Data Conclusão"].Visible = false;
                    //Exibe a coluna de Status
                    dgvTarefas.Columns["Status"].Visible = true;

                    try
                    {
                        //Filtra os resultados da DGV
                        dgvTarefas.Sort(dgvTarefas.Columns[0], ListSortDirection.Descending);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        string erro = ListaErro.RetornaErro(49);
                        int separador = erro.IndexOf(":");
                        MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        throw;
                    }

                    try
                    {
                        foreach (DataGridViewRow dgvr in dgvTarefas.Rows)
                        {
                            if (dgvr.Cells["Prioridade"].Value.ToString() == "2")
                            {
                                dgvr.DefaultCellStyle.BackColor = Color.LightSalmon;
                            }
                            else
                            {
                                if (dgvr.Cells["Status"].Value.ToString() == "Em Andamento")
                                {
                                    dgvr.DefaultCellStyle.BackColor = Color.LightBlue;
                                }
                                else if (dgvr.Cells["Status"].Value.ToString() == "Aguardando outra pessoa")
                                {
                                    dgvr.DefaultCellStyle.BackColor = Color.Yellow;
                                }
                                else if (dgvr.Cells["Status"].Value.ToString() == "Adiada")
                                {
                                    dgvr.DefaultCellStyle.BackColor = Color.LightSeaGreen;
                                }
                            }
                        }
                    }
                    catch (NullReferenceException)
                    {
                        string erro = ListaErro.RetornaErro(32);
                        int separador = erro.IndexOf(":");
                        MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        throw;
                    }
                }
                else
                {
                    string comando = funcoes.VerificaComboBoxTarefas(cmbTipoTarefas.SelectedIndex);

                    //Tenta pegar as posições de onde estava antes de atualizar
                    try
                    {
                        linhaAtual = dgvTarefas.CurrentCell.RowIndex;
                        colunaAtual = dgvTarefas.CurrentCell.ColumnIndex;
                        posvertical = dgvClientes.FirstDisplayedScrollingRowIndex;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        linhaAtual = dgvClientes.CurrentCell.RowIndex;
                        colunaAtual = dgvClientes.CurrentCell.ColumnIndex;
                    }

                    if (cmbTipoTarefas.SelectedIndex == 0)
                    {
                        //Atualiza a tabela atual temporária
                        _dgvTarefasAtual.DataSource = conexao.PreencheDGV(comando).Tables[0];

                        //Se a tabela atualizada for diferente da tabela anterior
                        if (_dgvTarefasAtual != _dgvTempTarefas)
                        {
                            dgvTarefas.DataSource = _dgvTarefasAtual.DataSource;
                            _dgvTempTarefas = _dgvTarefasAtual;
                        }
                        else if (dgvTarefas.DataSource != _dgvTempTarefas)
                        {
                            dgvTarefas.DataSource = _dgvTarefasAtual.DataSource;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else if (cmbTipoTarefas.SelectedIndex == 1)
                    {

                        //Atualiza a tabela atual temporária
                        _dgvTarefasConcluidas.DataSource = conexao.PreencheDGV(comando).Tables[0];

                        //Se a tabela atualizada for diferente da tabela anterior
                        if (_dgvTarefasConcluidas != _dgvTempTarefasConcluidas)
                        {
                            dgvTarefas.DataSource = _dgvTarefasConcluidas.DataSource;
                            _dgvTempTarefasConcluidas = _dgvTarefasConcluidas;
                        }
                        else if (dgvTarefas.DataSource != _dgvTempTarefasConcluidas)
                        {
                            dgvTarefas.DataSource = _dgvTempTarefasConcluidas.DataSource;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else if (cmbTipoTarefas.SelectedIndex == 2)
                    {
                        //Atualiza a tabela atual temporária
                        _dgvTodasTarefas.DataSource = conexao.PreencheDGV(comando).Tables[0];

                        //Se a tabela atualizada for diferente da tabela anterior
                        if (_dgvTodasTarefas != _dgvTempTodasTarefas)
                        {
                            dgvTarefas.DataSource = _dgvTodasTarefas.DataSource;
                            _dgvTempTodasTarefas = _dgvTodasTarefas;
                        }
                        else if (dgvTarefas.DataSource != _dgvTempTodasTarefas)
                        {
                            dgvTarefas.DataSource = _dgvTempTodasTarefas.DataSource;
                        }
                        else
                        {
                            return;
                        }
                    }

                    try
                    {
                        switch (cmbFiltros.SelectedIndex)
                        {
                            case 0:
                                dgvTarefas.Sort(dgvTarefas.Columns[2], ListSortDirection.Ascending);
                                break;
                            case 1:
                                dgvTarefas.Sort(dgvTarefas.Columns[2], ListSortDirection.Descending);
                                break;
                            case 2:
                                dgvTarefas.Sort(dgvTarefas.Columns[1], ListSortDirection.Ascending);
                                break;
                            case 3:
                                dgvTarefas.Sort(dgvTarefas.Columns[1], ListSortDirection.Descending);
                                break;
                            case 4:
                                dgvTarefas.Sort(dgvTarefas.Columns[0], ListSortDirection.Ascending);
                                break;
                            case 5:
                                dgvTarefas.Sort(dgvTarefas.Columns[0], ListSortDirection.Descending);
                                break;
                            case 6:
                                dgvTarefas.Sort(dgvTarefas.Columns["Prioridade"], ListSortDirection.Ascending);
                                break;
                            case 7:
                                dgvTarefas.Sort(dgvTarefas.Columns["Prioridade"], ListSortDirection.Descending);
                                break;
                        }
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        string erro = ListaErro.RetornaErro(49);
                        int separador = erro.IndexOf(":");
                        MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        throw;
                    }

                    foreach (DataGridViewRow dgvr in dgvTarefas.Rows)
                    {
                        if (cmbTipoTarefas.SelectedIndex == 0)
                        {
                            if (dgvr.Cells["Prioridade"].Value.ToString() == "2")
                            {
                                dgvr.DefaultCellStyle.BackColor = Color.LightSalmon;
                            }
                            else
                            {
                                if (dgvr.Cells["Status"].Value.ToString() == "Em Andamento")
                                {
                                    dgvr.DefaultCellStyle.BackColor = Color.LightBlue;
                                }
                                else if (dgvr.Cells["Status"].Value.ToString() == "Aguardando outra pessoa")
                                {
                                    dgvr.DefaultCellStyle.BackColor = Color.Yellow;
                                }
                                else if (dgvr.Cells["Status"].Value.ToString() == "Adiada")
                                {
                                    dgvr.DefaultCellStyle.BackColor = Color.LightSeaGreen;
                                }
                            }
                        }
                        else if (cmbTipoTarefas.SelectedIndex == 1)
                        {
                            dgvr.DefaultCellStyle.BackColor = Color.LightGray;
                        }
                        else if (cmbTipoTarefas.SelectedIndex == 2)
                        {
                            if (dgvr.Cells["Status"].Value.ToString() == "Concluída")
                            {
                                dgvr.DefaultCellStyle.BackColor = Color.LightGray;
                            }
                            else
                            {
                                if (dgvr.Cells["Prioridade"].Value.ToString() == "2")
                                {
                                    dgvr.DefaultCellStyle.BackColor = Color.LightSalmon;
                                }
                                else
                                {
                                    if (dgvr.Cells["Status"].Value.ToString() == "Em Andamento")
                                    {
                                        dgvr.DefaultCellStyle.BackColor = Color.LightBlue;
                                    }
                                    else if (dgvr.Cells["Status"].Value.ToString() == "Aguardando outra pessoa")
                                    {
                                        dgvr.DefaultCellStyle.BackColor = Color.Yellow;
                                    }
                                    else if (dgvr.Cells["Status"].Value.ToString() == "Adiada")
                                    {
                                        dgvr.DefaultCellStyle.BackColor = Color.LightSeaGreen;
                                    }
                                }
                            }
                        }
                    }

                    //Esconde a coluna ID
                    dgvTarefas.Columns[0].Visible = false;

                    //Esconde a coluna prioridade
                    dgvTarefas.Columns["Prioridade"].Visible = false;

                    //Esconde a coluna DataFinal
                    if (cmbTipoTarefas.SelectedIndex == 0)
                    {
                        dgvTarefas.Columns["Data Conclusão"].Visible = false;
                        dgvTarefas.Columns["Status"].Visible = true;
                    }
                    //Esconde a coluna Status
                    else if (cmbTipoTarefas.SelectedIndex == 1)
                    {
                        dgvTarefas.Columns["Data Conclusão"].Visible = true;
                        dgvTarefas.Columns["Status"].Visible = false;
                    }

                    //desmarca a primeira linha
                    dgvTarefas.Rows[0].Selected = false;

                    //Tenta selecionar as linhas
                    try
                    {
                        dgvTarefas[colunaAtual, linhaAtual].Selected = true;
                        dgvTarefas.FirstDisplayedScrollingRowIndex = posvertical;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        try
                        {
                            dgvTarefas[colunaAtual, linhaAtual].Selected = true;
                            dgvTarefas.FirstDisplayedScrollingRowIndex = 0;
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            dgvTarefas[0, 0].Selected = true;
                        }
                    }
                }
            }
        }

        private void dgvTarefas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                int idTarefa = 0, posicaoAtualScroll = dgvTarefas.FirstDisplayedScrollingRowIndex;

                DataGridViewRow linha = dgvTarefas.Rows[e.RowIndex];

                idTarefa = funcoes.AbreTarefa(linha.Cells["Empresa"].Value.ToString(),
                    linha.Cells["Atribuido a"].Value.ToString(), linha.Cells["Assunto"].Value.ToString());

                string comando = "Insert into tbl_log values (0," + idUsuario + ", 'Abriu a Tarefa ID: " + idTarefa + " - " +
                DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "');";
                conexao.ExecutaComando(comando);

                if (funcoes.TravaTarefa(idTarefa))
                {
                    Tarefa telaTarefa = new Tarefa(false, idTarefa, idUsuario);
                    telaTarefa.ShowDialog();
                    funcoes.DestravaTarefa(idTarefa);
                }
                else
                {
                    string erro = ListaMensagens.RetornaMensagem(01);
                    int separador = erro.IndexOf(":");
                    MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                AtualizaDGVTarefas();
                dgvTarefas.FirstDisplayedScrollingRowIndex = posicaoAtualScroll;
            }
        }

        private void dgvTarefas_DataSourceChanged(object sender, EventArgs e)
        {
            dgvTarefas.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void cmbTipoTarefas_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizaDGVTarefas();
            dgvTarefas.Focus();
        }

        private void cmbFiltros_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizaDGVTarefas();
            dgvTarefas.Focus();
        }

        #region Impressão

        private void btnImprimirListaTarefas_Click(object sender, EventArgs e)
        {
            ImprimirTarefas();
        }

        private void pdTarefas_BeginPrint(object sender, PrintEventArgs e)
        {
            try
            {
                strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Near;
                strFormat.LineAlignment = StringAlignment.Center;
                strFormat.Trimming = StringTrimming.EllipsisCharacter;

                arrColumnLefts.Clear();
                arrColumnWidths.Clear();
                iCellHeight = 0;
                iRow = 0;
                bFirstPage = true;
                bNewPage = true;

                // Calculating Total Widths
                iTotalWidth = 0;
                foreach (DataGridViewColumn dgvGridCol in dgvTarefas.Columns)
                {
                    iTotalWidth += dgvGridCol.Width;
                }
            }
            catch (Exception)
            {
                string erro = ListaErro.RetornaErro(23);
                int separador = erro.LastIndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ImprimirTarefas()
        {
            if (pdImprimirTarefas.ShowDialog() == DialogResult.OK)
            {
                pdTarefas.DefaultPageSettings.Landscape = true;
                pdTarefas.DocumentName = "Tabela de Tarefas - CFTVA " + DateTime.Now.Date.ToShortDateString();

                pPreviewTarefas.ShowDialog();

                AtualizaDGVTarefas();
            }

        }

        private void pdTarefas_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                //Set the left margin
                int iLeftMargin = e.MarginBounds.Left;
                //Set the top margin
                int iTopMargin = e.MarginBounds.Top;
                //Whether more pages have to print or not
                bool bMorePagesToPrint = false;
                int iTmpWidth = 0;

                //For the first page to print set the cell width and header height
                if (bFirstPage)
                {
                    foreach (DataGridViewColumn GridCol in dgvTarefas.Columns)
                    {
                        if (cmbTipoTarefas.SelectedIndex == 0)
                        {
                            if (GridCol.Name != "ID" && GridCol.Name != "Data Conclusão" && GridCol.Name != "prioridade")
                            {
                                iTmpWidth = (int)(Math.Floor((double)((double)GridCol.Width /
                                (double)iTotalWidth * (double)iTotalWidth *
                                ((double)e.MarginBounds.Width / (double)iTotalWidth))));

                                iHeaderHeight = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
                                    GridCol.InheritedStyle.Font, iTmpWidth).Height) + 11;

                                // Save width and height of headers
                                arrColumnLefts.Add(iLeftMargin);
                                arrColumnWidths.Add(iTmpWidth);
                                iLeftMargin += iTmpWidth;
                            }
                        }
                        else if (cmbTipoTarefas.SelectedIndex == 1)
                        {
                            if (GridCol.Name != "ID" && GridCol.Name != "Status" && GridCol.Name != "prioridade")
                            {
                                iTmpWidth = (int)(Math.Floor((double)((double)GridCol.Width /
                                (double)iTotalWidth * (double)iTotalWidth *
                                ((double)e.MarginBounds.Width / (double)iTotalWidth))));

                                iHeaderHeight = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
                                    GridCol.InheritedStyle.Font, iTmpWidth).Height) + 11;

                                // Save width and height of headers
                                arrColumnLefts.Add(iLeftMargin);
                                arrColumnWidths.Add(iTmpWidth);
                                iLeftMargin += iTmpWidth;
                            }
                        }
                        else if (cmbTipoTarefas.SelectedIndex == 2)
                        {
                            if (GridCol.Name != "ID" && GridCol.Name != "prioridade")
                            {
                                iTmpWidth = (int)(Math.Floor((double)((double)GridCol.Width /
                                    (double)iTotalWidth * (double)iTotalWidth *
                                    ((double)e.MarginBounds.Width / (double)iTotalWidth))));

                                iHeaderHeight = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
                                    GridCol.InheritedStyle.Font, iTmpWidth).Height) + 11;

                                // Save width and height of headers
                                arrColumnLefts.Add(iLeftMargin);
                                arrColumnWidths.Add(iTmpWidth);
                                iLeftMargin += iTmpWidth;
                            }
                        }
                    }
                }
                //Loop till all the grid rows not get printed
                while (iRow <= dgvTarefas.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = dgvTarefas.Rows[iRow];
                    //Seta a altura da linha
                    iCellHeight = GridRow.Height + 12;
                    int iCount = 0;
                    //Check whether the current page settings allows more rows to print
                    if (iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }
                    else
                    {
                        if (bNewPage)
                        {
                            //Draw Header
                            e.Graphics.DrawString("Lista de Tarefas",
                                new Font(dgvTarefas.Font, FontStyle.Bold),
                                Brushes.Black, e.MarginBounds.Left,
                                e.MarginBounds.Top - e.Graphics.MeasureString("Lista de Tarefas",
                                new Font(dgvTarefas.Font, FontStyle.Bold),
                                e.MarginBounds.Width).Height - 13);

                            String strDate = DateTime.Now.ToLongDateString() + " " +
                                DateTime.Now.ToShortTimeString();
                            //Draw Date
                            e.Graphics.DrawString(strDate,
                                new Font(dgvTarefas.Font, FontStyle.Bold), Brushes.Black,
                                e.MarginBounds.Left +
                                (e.MarginBounds.Width - e.Graphics.MeasureString(strDate,
                                new Font(dgvTarefas.Font, FontStyle.Bold),
                                e.MarginBounds.Width).Width),
                                e.MarginBounds.Top - e.Graphics.MeasureString("Lista de Tarefas",
                                new Font(new Font(dgvTarefas.Font, FontStyle.Bold),
                                FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            //Draw Columns                 
                            iTopMargin = e.MarginBounds.Top;
                            foreach (DataGridViewColumn GridCol in dgvTarefas.Columns)
                            {
                                if (cmbTipoTarefas.SelectedIndex == 0)
                                {
                                    if (GridCol.Name != "ID" && GridCol.Name != "Data Conclusão" && GridCol.Name != "prioridade")
                                    {
                                        e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
                                            new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                            (int)arrColumnWidths[iCount], iHeaderHeight));

                                        e.Graphics.DrawRectangle(Pens.Black,
                                            new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                            (int)arrColumnWidths[iCount], iHeaderHeight));

                                        e.Graphics.DrawString(GridCol.HeaderText,
                                            GridCol.InheritedStyle.Font,
                                            new SolidBrush(GridCol.InheritedStyle.ForeColor),
                                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                            (int)arrColumnWidths[iCount], iHeaderHeight), strFormat);
                                        iCount++;
                                    }
                                }
                                else if (cmbTipoTarefas.SelectedIndex == 1)
                                {
                                    if (GridCol.Name != "ID" && GridCol.Name != "Status" && GridCol.Name != "prioridade")
                                    {
                                        e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
                                            new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                            (int)arrColumnWidths[iCount], iHeaderHeight));

                                        e.Graphics.DrawRectangle(Pens.Black,
                                            new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                            (int)arrColumnWidths[iCount], iHeaderHeight));

                                        e.Graphics.DrawString(GridCol.HeaderText,
                                            GridCol.InheritedStyle.Font,
                                            new SolidBrush(GridCol.InheritedStyle.ForeColor),
                                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                            (int)arrColumnWidths[iCount], iHeaderHeight), strFormat);
                                        iCount++;
                                    }
                                }
                                else if (cmbTipoTarefas.SelectedIndex == 2)
                                {
                                    if (GridCol.Name != "ID" && GridCol.Name != "prioridade")
                                    {
                                        e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
                                            new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                            (int)arrColumnWidths[iCount], iHeaderHeight));

                                        e.Graphics.DrawRectangle(Pens.Black,
                                            new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                            (int)arrColumnWidths[iCount], iHeaderHeight));

                                        e.Graphics.DrawString(GridCol.HeaderText,
                                            GridCol.InheritedStyle.Font,
                                            new SolidBrush(GridCol.InheritedStyle.ForeColor),
                                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                            (int)arrColumnWidths[iCount], iHeaderHeight), strFormat);
                                        iCount++;
                                    }
                                }
                            }
                            bNewPage = false;
                            iTopMargin += iHeaderHeight;
                        }
                        iCount = 0;
                        //Draw Columns Contents                
                        foreach (DataGridViewCell Cel in GridRow.Cells)
                        {
                            if (Cel.ColumnIndex != 0)
                            {
                                string texto = null;

                                if (cmbTipoTarefas.SelectedIndex == 0)
                                {
                                    if (Cel.ColumnIndex != 5 && Cel.ColumnIndex != 6)
                                    {
                                        if (Cel.Value != null)
                                        {
                                            texto = Cel.Value.ToString();

                                            if (Cel.ColumnIndex == 1)
                                            {
                                                if (texto.Length > 16)
                                                {
                                                    texto = texto.Substring(0, 13) + "...";
                                                }
                                            }
                                            else if (Cel.ColumnIndex == 2)
                                            {
                                                if (texto.Length > 8)
                                                {
                                                    texto = texto.Substring(0, texto.IndexOf(" "));
                                                }
                                            }
                                            else if (Cel.ColumnIndex == 3)
                                            {
                                                if (texto.Length > 60)
                                                {
                                                    texto = texto.Substring(0, 60) + "...";
                                                }
                                            }
                                            else if (Cel.ColumnIndex == 4)
                                            {
                                                if (texto.Length > 13)
                                                {
                                                    texto = texto.Substring(0, texto.IndexOf(" "));
                                                }
                                            }

                                            e.Graphics.DrawString(texto,
                                                Cel.InheritedStyle.Font,
                                                new SolidBrush(Cel.InheritedStyle.ForeColor),
                                                new RectangleF((int)arrColumnLefts[iCount],
                                                (float)iTopMargin,
                                                (int)arrColumnWidths[iCount], (float)iCellHeight),
                                                strFormat);

                                            //Drawing Cells Borders 
                                            e.Graphics.DrawRectangle(Pens.Black,
                                                new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                                (int)arrColumnWidths[iCount], iCellHeight));
                                            iCount++;
                                        }
                                    }
                                }
                                else if (cmbTipoTarefas.SelectedIndex == 1)
                                {
                                    if (Cel.ColumnIndex != 4 && Cel.ColumnIndex != 6)
                                    {
                                        if (Cel.Value != null)
                                        {
                                            texto = Cel.Value.ToString();

                                            if (Cel.ColumnIndex == 1)
                                            {
                                                if (texto.Length > 16)
                                                {
                                                    texto = texto.Substring(0, 13) + "...";
                                                }
                                            }
                                            else if (Cel.ColumnIndex == 2)
                                            {
                                                if (texto.Length > 8)
                                                {
                                                    texto = texto.Substring(0, texto.IndexOf(" "));
                                                }
                                            }
                                            else if (Cel.ColumnIndex == 3)
                                            {
                                                if (texto.Length > 60)
                                                {
                                                    texto = texto.Substring(0, 60) + "...";
                                                }
                                            }
                                            else if (Cel.ColumnIndex == 5)
                                            {
                                                if (texto.Contains("/"))
                                                {
                                                    DateTime data = DateTime.Parse(Cel.Value.ToString().Substring(0, 10));
                                                    texto = data.ToString("dd/MM/yy");
                                                }
                                            }

                                            e.Graphics.DrawString(texto,
                                            Cel.InheritedStyle.Font,
                                            new SolidBrush(Cel.InheritedStyle.ForeColor),
                                            new RectangleF((int)arrColumnLefts[iCount],
                                            (float)iTopMargin,
                                            (int)arrColumnWidths[iCount], (float)iCellHeight),
                                            strFormat);

                                            //Drawing Cells Borders 
                                            e.Graphics.DrawRectangle(Pens.Black,
                                                new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                                (int)arrColumnWidths[iCount], iCellHeight));
                                            iCount++;
                                        }
                                    }
                                }
                                else if (cmbTipoTarefas.SelectedIndex == 2)
                                {
                                    if (Cel.ColumnIndex != 6)
                                    {
                                        if (Cel.Value != null)
                                        {
                                            texto = Cel.Value.ToString();

                                            if (Cel.ColumnIndex == 1)
                                            {
                                                if (texto.Length > 16)
                                                {
                                                    texto = texto.Substring(0, 13) + "...";
                                                }
                                            }
                                            else if (Cel.ColumnIndex == 2)
                                            {
                                                if (texto.Length > 8)
                                                {
                                                    texto = texto.Substring(0, texto.IndexOf(" "));
                                                }
                                            }
                                            else if (Cel.ColumnIndex == 3)
                                            {
                                                if (texto.Length > 60)
                                                {
                                                    texto = texto.Substring(0, 60) + "...";
                                                }
                                            }
                                            else if (Cel.ColumnIndex == 4)
                                            {
                                                if (texto.Length > 13)
                                                {
                                                    texto = texto.Substring(0, texto.IndexOf(" "));
                                                }
                                            }
                                            else if (Cel.ColumnIndex == 5)
                                            {
                                                if (texto.Contains("/"))
                                                {
                                                    DateTime data = DateTime.Parse(Cel.Value.ToString().Substring(0, 10));
                                                    texto = data.ToString("dd/MM/yy");
                                                }
                                            }

                                            e.Graphics.DrawString(texto,
                                            Cel.InheritedStyle.Font,
                                            new SolidBrush(Cel.InheritedStyle.ForeColor),
                                            new RectangleF((int)arrColumnLefts[iCount],
                                            (float)iTopMargin,
                                            (int)arrColumnWidths[iCount], (float)iCellHeight),
                                            strFormat);

                                            //Drawing Cells Borders 
                                            e.Graphics.DrawRectangle(Pens.Black,
                                                new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                                (int)arrColumnWidths[iCount], iCellHeight));
                                            iCount++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    iRow++;
                    iTopMargin += iCellHeight;
                }
                //If more lines exist, print another page.
                if (bMorePagesToPrint)
                    e.HasMorePages = true;
                else
                    e.HasMorePages = false;
            }
            catch (Exception)
            {
                string erro = ListaErro.RetornaErro(24);
                int separador = erro.LastIndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #endregion

        #region Fornecedores
        #region Variáveis
        private int _nfTipo, _nfCateg1, _nfCateg2, _nfCateg3, _nfSubCateg1, _nfSubCateg2, _nfSubCateg3;
        private string _nfDocumento, _nfNome, _nfApelido, _nfAniversario, _nfCep, _nfEndereco, _nfNumero, _nfComplemento, _nfBairro, _nfCidade, _nfEstado, _nfPais,
            _nfTel, _nfContato, _nfTelComercial, _nfContatoComercial, _nfCelular, _nfContatoCelular, _nfEmail, _nfSite, _nfInscEstadual, _nfInscMunicipal, _nfOBS;
        #endregion

        /// <summary>
        /// Método responsável por atualizar a tela de Fornecedores
        /// </summary>
        /// 
        private DataGridView _dgvFornecedoresAtual = new DataGridView();

        private void AtualizaDGVFornecedores()
        {
            if (conexao.TestaConexao())
            {
                string comando = FuncoesEstaticas.FiltroFornecedores(cmbGrupoFornecedor.SelectedIndex);

                DataGridView _dgvTemp = new DataGridView();
                int linhaAtual = 0, colunaAtual = 0, posvertical = 0;

                if (iniciaTelaFornecedores)
                {
                    //Preenche o combobox de Grupos
                    List<string> lista = conexao.PreencheCMB("Select Nome from tbl_subgrupos;");
                    //Insere na lista uma opção "Todos"
                    lista.Insert(lista.Count, "Todos");
                    //Preenche o combobox
                    cmbGrupoFornecedor.DataSource = lista;
                    cmbGrupoFornecedor.DisplayMember = "Fornecedores";
                    //Seleciona a opção "Todos" no combobox
                    cmbGrupoFornecedor.Text = "Todos";

                    //Reescreve o comando, dando certeza de que selecionou a opção "Todos"
                    comando = FuncoesEstaticas.FiltroFornecedores(cmbGrupoFornecedor.SelectedIndex);

                    //Preenche o DataGridView Temporário
                    _dgvFornecedoresAtual.DataSource = conexao.PreencheDGV(comando).Tables[0];
                    //Preenche o DataGridView de Backup
                    _dgvTemp.DataSource = _dgvFornecedoresAtual.DataSource;
                    //Preenche o DataGridView Oficial
                    dgvFornecedores.DataSource = _dgvFornecedoresAtual.DataSource;

                    //Deseleciona a primeira linha
                    dgvFornecedores.Rows[0].Selected = false;

                    //Seta que a tela já foi aberta
                    iniciaTelaFornecedores = false;
                }
                else
                {
                    //Tenta pegar as posições de onde estava antes de atualizar.
                    try
                    {
                        linhaAtual = dgvFornecedores.CurrentCell.RowIndex;
                        colunaAtual = dgvFornecedores.CurrentCell.ColumnIndex;
                        posvertical = dgvFornecedores.FirstDisplayedScrollingRowIndex;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        linhaAtual = dgvFornecedores.CurrentCell.RowIndex;
                        colunaAtual = dgvFornecedores.CurrentCell.ColumnIndex;
                    }

                    //Atualiza a tabela atual temporária
                    _dgvFornecedoresAtual.DataSource = conexao.PreencheDGV(comando).Tables[0];

                    //Se a tabela atualizada for diferente da tabela anterior
                    if (_dgvFornecedoresAtual != _dgvTemp)
                    {
                        dgvFornecedores.DataSource = _dgvFornecedoresAtual.DataSource;
                        _dgvTemp = _dgvFornecedoresAtual;
                    }
                    else
                    {
                        return;
                    }

                    //Desmarca a primeira linha
                    dgvFornecedores.Rows[0].Selected = false;

                    //tenta selecionar a linha
                    try
                    {
                        dgvFornecedores[colunaAtual, linhaAtual].Selected = true;
                        dgvFornecedores.FirstDisplayedScrollingRowIndex = posvertical;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        try
                        {
                            dgvFornecedores[colunaAtual, linhaAtual].Selected = true;
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            dgvFornecedores[0, 0].Selected = true;
                            dgvFornecedores.FirstDisplayedScrollingRowIndex = 0;
                        }
                    }
                }

                //Esconde a coluna contrato
                dgvFornecedores.Columns["id"].Visible = false;
            }
        }

        private void dgvFornecedores_DataSourceChanged(object sender, EventArgs e)
        {
            dgvFornecedores.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvFornecedores.Columns[2].MinimumWidth = 150;
        }

        private void dgvFornecedores_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                nfLimparCampos();

                DataGridViewRow linha = dgvFornecedores.Rows[e.RowIndex];
                int idFornecedor = int.Parse(linha.Cells["id"].Value.ToString());
                nfCarregaFornecedor(idFornecedor);

                string comando = "Insert into tbl_log values (0," + idUsuario + ", 'Abriu fornecedor ID: " + idFornecedor + " - " +
                DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "');";
                conexao.ExecutaComando(comando);

                AtualizaDGVFornecedores();
            }
        }

        private void btnPesquisaFornecedor_Click(object sender, EventArgs e)
        {
            TelaPesquisa telaPesquisaFornecedor = new TelaPesquisa(dgvFornecedores, false);
            telaPesquisaFornecedor.ShowDialog();

            if (FuncoesEstaticas.FornecedorPesquisado != -1)
            {
                dgvFornecedores[1, FuncoesEstaticas.FornecedorPesquisado].Selected = true;
            }
            else
            {
                string erro = ListaErro.RetornaErro(31);
                int separador = erro.LastIndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            dgvFornecedores.Focus();
        }

        private void btnNovoFornecedor_Click(object sender, EventArgs e)

        {
            cmbNFCateg1.DataSource = nfCarregaCategoria();
            txtNFDataCadastro.Text = DateTime.Today.ToShortDateString();

            panelNF.Visible = true;
            panelNF.Enabled = true;

            panelFornecedores.Enabled = false;
        }

        #region Novo Fornecedor
        private void nfCarregaFornecedor(int _idFornecedor)
        {
            string erro = null;
            int separador = 0;

            if (conexao.FornecedorBloqueado(_idFornecedor))
            {
                erro = ListaErro.RetornaErro(54);
                separador = erro.LastIndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                List<string> listaFornecedor = funcoes.CarregaFornecedor(_idFornecedor);

                txtNFIDFornecedor.Text = listaFornecedor[0];
                cmbNFTipoFornecedor.SelectedIndex = int.Parse(listaFornecedor[1]);
                txtNFDataCadastro.Text = listaFornecedor[2];
                txtNFDataNascimento.Text = listaFornecedor[3];
                txtNFCPFCNPJ.Text = listaFornecedor[4];
                lblNFTitulo.Text = listaFornecedor[5];
                txtNFNome.Text = listaFornecedor[5];
                txtNFApelido.Text = listaFornecedor[6];
                txtNFCEP.Text = listaFornecedor[7];
                txtNFEndereco.Text = listaFornecedor[8];
                txtNFNumero.Text = listaFornecedor[9];
                txtNFComplemento.Text = listaFornecedor[10];
                txtNFBairro.Text = listaFornecedor[11];
                txtNFCidade.Text = listaFornecedor[12];
                txtNFEstado.Text = listaFornecedor[13];
                txtNFPais.Text = listaFornecedor[14];
                txtNFTelefone.Text = listaFornecedor[15];
                txtNFContato.Text = listaFornecedor[16];
                txtNFTelefoneComercial.Text = listaFornecedor[17];
                txtNFContatoComercial.Text = listaFornecedor[18];
                txtNFCelular.Text = listaFornecedor[19];
                txtNFContatoCelular.Text = listaFornecedor[20];
                txtNFEmail.Text = listaFornecedor[21];
                txtNFSite.Text = listaFornecedor[22];
                txtNFInscricaoEstadual.Text = listaFornecedor[23];
                txtNFInscricaoMunicipal.Text = listaFornecedor[24];
                txtNFObservacoes.Text = listaFornecedor[25];


                string comando = "Select nome from tbl_subgrupos;";
                cmbNFCateg1.DataSource = conexao.PreencheCMB(comando);
                cmbNFCateg2.DataSource = conexao.PreencheCMB(comando);
                cmbNFCateg3.DataSource = conexao.PreencheCMB(comando);

                if (!string.IsNullOrEmpty(listaFornecedor[26]))
                {    
                    cmbNFCateg1.SelectedIndex = int.Parse(listaFornecedor[26]) - 1;

                    cmbNFSubCateg1.DataSource = funcoes.CarregaSubCategoriaFornecedor(int.Parse(listaFornecedor[26]));
                    cmbNFSubCateg1.Enabled = true;
                }
                
                if (!string.IsNullOrEmpty(listaFornecedor[27]))
                {
                    cmbNFCateg2.SelectedIndex = int.Parse(listaFornecedor[27]) - 1;

                    cmbNFSubCateg2.DataSource = funcoes.CarregaSubCategoriaFornecedor(int.Parse(listaFornecedor[27]));
                    cmbNFSubCateg2.Enabled = true;
                }
                if (!string.IsNullOrEmpty(listaFornecedor[28]))
                {
                    cmbNFCateg3.SelectedIndex = int.Parse(listaFornecedor[28]) - 1;

                    cmbNFSubCateg3.DataSource = funcoes.CarregaSubCategoriaFornecedor(int.Parse(listaFornecedor[28]));
                    cmbNFSubCateg3.Enabled = true;
                }
                if (!string.IsNullOrEmpty(listaFornecedor[29]))
                {
                    cmbNFSubCateg1.SelectedIndex = int.Parse(listaFornecedor[29]) - 1;
                    cmbNFCateg2.Enabled = true;
                }
                if (!string.IsNullOrEmpty(listaFornecedor[30]))
                {
                    cmbNFSubCateg2.SelectedIndex = int.Parse(listaFornecedor[30]) - 1;

                    cmbNFCateg3.Enabled = true;
                }
                if (!string.IsNullOrEmpty(listaFornecedor[31]))
                {
                    cmbNFSubCateg3.SelectedIndex = int.Parse(listaFornecedor[31]) - 1;
                }

                btnNFApagar.Enabled = true;
                btnNFEditar.Enabled = true;
                btnNFImprimir.Enabled = true;
                btnNFNovoCadastro.Text = "Novo Fornecedor";
                btnNFFechar.Text = "Fechar";

                panelNF.Visible = true;
                panelNF.Enabled = true;

                panelFornecedores.Enabled = false;

                iniciaTelaNovoFornecedor = false;
            }
        }

        private List<string> nfCarregaCategoria()
        {
            string comando = "Select nome from tbl_subgrupos;";

            return conexao.PreencheCMB(comando);
        }

        private void nfLocalizarCEP()
        {
            if (txtNFCEP.Text.Length > 0 && txtNFCEP.Text.Length < 8)
            {
                string erro = ListaErro.RetornaErro(44);
                int separador = erro.IndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);

                txtNFCEP.Clear();
                txtNFCEP.Focus();
            }
            else if (txtNFCEP.Text.Length == 8)
            {
                try
                {
                    CEP cep = new CEP(txtNFCEP.Text);

                    if (cep.cep != null)
                    {
                        txtNFEstado.Text = cep.uf;
                        txtNFCidade.Text = cep.localidade;
                        txtNFBairro.Text = cep.bairro;
                        txtNFEndereco.Text = cep.logradouro;
                        txtNFComplemento.Text = cep.complemento;
                    }
                    else
                    {
                        string erro = ListaErro.RetornaErro(45);
                        int separador = erro.IndexOf(":");
                        DialogResult resultadoDialogo = MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (resultadoDialogo == DialogResult.Yes)
                        {
                            txtNFCEP.Clear();
                            txtNFCEP.Focus();
                        }
                        else
                        {
                            txtNFCEP.Clear();
                        }
                    }
                }
                catch (KeyNotFoundException)
                {
                    string erro = ListaErro.RetornaErro(46);
                    int separador = erro.IndexOf(":");
                    MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNFCEP.Clear();
                    txtNFCEP.Focus();
                }
                finally
                {
                    txtNFNumero.Focus();
                }
            }
        }

        private void nfPesquisaCNPJ()
        {
            string erro = null;
            int separador = int.MinValue;

            try
            {
                if (txtNFCPFCNPJ.Text.Length >= 0 && txtNFCPFCNPJ.Text.Length < 14)
                {
                    erro = ListaErro.RetornaErro(40);
                    separador = erro.IndexOf(":");
                    MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNFCPFCNPJ.Clear();
                    txtNFCPFCNPJ.Focus();
                }
                else if (txtNFCPFCNPJ.Text.Length == 14 && !FuncoesEstaticas.ValidaCNPJ(txtNFCPFCNPJ.Text))
                {
                    erro = ListaErro.RetornaErro(41);
                    separador = erro.IndexOf(":");
                    MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNFCPFCNPJ.Clear();
                    txtNFCPFCNPJ.Focus();
                }
                else if (FuncoesEstaticas.ValidaCNPJ(txtNFCPFCNPJ.Text))
                {
                    string url = "https://www.receitaws.com.br/v1/cnpj/" + txtNFCPFCNPJ.Text;

                    try
                    {
                        string fileContents;

                        WebRequest request = WebRequest.Create(url);
                        WebResponse response = request.GetResponse();
                        Stream data = response.GetResponseStream();

                        string html = String.Empty;
                        using (StreamReader sr = new StreamReader(data))
                        {
                            fileContents = sr.ReadToEnd();
                        }

                        JsonTextReader reader = new JsonTextReader(new StringReader(fileContents));
                        RootObject rootobject = JsonConvert.DeserializeObject<RootObject>(fileContents);

                        for (int i = 0; i < 1; i++)
                        {
                            txtNFNome.Text = rootobject.nome;
                            txtNFApelido.Text = rootobject.fantasia;
                            txtNFDataNascimento.Text = rootobject.abertura;
                            txtNFCEP.Text = rootobject.cep.Replace(".", "").Replace("-", "");
                            txtNFEndereco.Text = rootobject.logradouro;
                            txtNFNumero.Text = rootobject.numero.Replace("(", "").Replace(")", "").Replace(" ", "");
                            txtNFComplemento.Text = rootobject.complemento;
                            txtNFBairro.Text = rootobject.bairro;
                            txtNFCidade.Text = rootobject.municipio;
                            txtNFEstado.Text = rootobject.uf;
                            txtNFTelefone.Text = rootobject.telefone;
                            txtNFEmail.Text = rootobject.email;
                        }
                    }
                    catch (Exception)
                    {
                        erro = ListaErro.RetornaErro(53);
                        separador = erro.IndexOf(":");
                        MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception)
            {
                erro = ListaErro.RetornaErro(41);
                separador = erro.IndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNFCPFCNPJ.Clear();
                txtNFCPFCNPJ.Focus();
            }
        }

        private void nfLimparCampos()
        {
            _nfTipo = int.MinValue;
            _nfCateg1 = int.MinValue;
            _nfCateg2 = int.MinValue;
            _nfCateg3 = int.MinValue;
            _nfSubCateg1 = int.MinValue;
            _nfSubCateg2 = int.MinValue;
            _nfSubCateg3 = int.MinValue;
            _nfDocumento = null;
            _nfNome = null;
            _nfApelido = null;
            _nfAniversario = null;
            _nfCep = null;
            _nfEndereco = null;
            _nfNumero = null;
            _nfComplemento = null;
            _nfBairro = null;
            _nfCidade = null;
            _nfEstado = null;
            _nfPais = null;
            _nfTel = null;
            _nfContato = null;
            _nfTelComercial = null;
            _nfContatoComercial = null;
            _nfCelular = null;
            _nfContatoCelular = null;
            _nfEmail = null;
            _nfSite = null;
            _nfInscEstadual = null;
            _nfInscMunicipal = null;
            _nfOBS = null;

            cmbNFTipoFornecedor.SelectedIndex = 0;
            cmbNFTipoFornecedor.Text = "Pessoa Juridica";
            txtNFCPFCNPJ.Clear();
            txtNFIDFornecedor.Clear();
            txtNFNome.Clear();
            txtNFApelido.Clear();
            txtNFDataCadastro.Text = DateTime.Today.ToShortDateString();
            txtNFDataNascimento.Clear();
            cmbNFCateg1.ResetText();
            cmbNFCateg1.DataSource = null;
            cmbNFCateg2.ResetText();
            cmbNFCateg2.DataSource = null;
            cmbNFCateg2.Enabled = false;
            cmbNFCateg3.ResetText();
            cmbNFCateg3.DataSource = null;
            cmbNFCateg3.Enabled = false;
            cmbNFSubCateg1.ResetText();
            cmbNFSubCateg1.DataSource = null;
            cmbNFSubCateg1.Enabled = false;
            cmbNFSubCateg2.ResetText();
            cmbNFSubCateg2.DataSource = null;
            cmbNFSubCateg2.Enabled = false;
            cmbNFSubCateg3.ResetText();
            cmbNFSubCateg3.DataSource = null;
            cmbNFSubCateg3.Enabled = false;
            txtNFCEP.Clear();
            txtNFEndereco.Clear();
            txtNFNumero.Clear();
            txtNFComplemento.Clear();
            txtNFBairro.Clear();
            txtNFCidade.Clear();
            txtNFEstado.Clear();
            txtNFPais.Clear();
            txtNFTelefone.Clear();
            txtNFContato.Clear();
            txtNFTelefoneComercial.Clear();
            txtNFContatoComercial.Clear();
            txtNFCelular.Clear();
            txtNFContatoCelular.Clear();
            txtNFEmail.Clear();
            txtNFSite.Clear();
            txtNFInscricaoEstadual.Clear();
            txtNFInscricaoMunicipal.Clear();
            txtNFObservacoes.Clear();

            btnNFApagar.Enabled = false;
            btnNFEditar.Enabled = false;
            btnNFImprimir.Enabled = false;
            btnNFNovoCadastro.Text = "Cadastrar Fornecedor";
            btnNFFechar.Text = "Cancelar";

            lblNFTitulo.Text = "Novo Fornecedor";

            iniciaTelaNovoFornecedor = true;
        }

        public class RootObject
        {
            public string nome { get; set; }
            public string fantasia { get; set; }
            public string cnpj { get; set; }
            public string telefone { get; set; }
            public string email { get; set; }
            public string abertura { get; set; }
            public string logradouro { get; set; }
            public string numero { get; set; }
            public string complemento { get; set; }
            public string bairro { get; set; }
            public string municipio { get; set; }
            public string uf { get; set; }
            public string cep { get; set; }
        }

        private void cmbTipoFornecedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Se escolher pessoa física
            if (cmbNFTipoFornecedor.SelectedIndex == 1)
            {
                btnNFBuscarDados.Enabled = false;
                btnNFBuscarDados.Visible = false;

                lblNomeFornecedor.Text = "Nome:";
                lblNFApelido.Text = "Apelido:";
                lblNFDataNascimento.Text = "Data de Nascimento:";
                lblNFCPFCNPJ.Text = "CPF:";

                //Muda a mascara para o padrão CPF
                txtNFCPFCNPJ.Mask = "000,000,000-00";
            }
            else
            {
                btnNFBuscarDados.Enabled = true;
                btnNFBuscarDados.Visible = true;

                lblNomeFornecedor.Text = "Razão Social:";
                lblNFApelido.Text = "Nome Fantasia:";
                lblNFDataNascimento.Text = "Fundação:";

                lblNFCPFCNPJ.Text = "CNPJ:";
                //Muda a mascara para o padrão CPF
                txtNFCPFCNPJ.Mask = "00,000,000/0000-00";
            }
        }

        private void btnNFFechar_Click(object sender, EventArgs e)
        {
            string mensagem = "";
            int separador = 0;

            if (_nfTipo != cmbNFTipoFornecedor.SelectedIndex || _nfCateg1 != cmbNFCateg1.SelectedIndex || _nfCateg2 != cmbNFCateg2.SelectedIndex ||
                _nfCateg3 != cmbNFCateg3.SelectedIndex || _nfSubCateg1 != cmbNFSubCateg1.SelectedIndex || _nfSubCateg2 != cmbNFSubCateg2.SelectedIndex ||
                _nfSubCateg3 != cmbNFSubCateg3.SelectedIndex || _nfDocumento != txtNFCPFCNPJ.Text || _nfNome != txtNFNome.Text || _nfApelido != txtNFApelido.Text ||
                _nfAniversario != txtNFDataNascimento.Text || _nfCep != txtNFCEP.Text || _nfEndereco != txtNFEndereco.Text || _nfNumero != txtNFNumero.Text ||
                _nfComplemento != txtNFComplemento.Text || _nfBairro != txtNFBairro.Text || _nfCidade != txtNFCidade.Text || _nfEstado != txtNFEstado.Text ||
                _nfPais != txtNFPais.Text || _nfTel != txtNFTelefone.Text || _nfContato != txtNFContato.Text || _nfTelComercial != txtNFTelefoneComercial.Text ||
                _nfContatoComercial != txtNFContatoComercial.Text || _nfCelular != txtNFCelular.Text || _nfContatoCelular != txtNFContatoCelular.Text ||
                _nfEmail != txtNFEmail.Text || _nfSite != txtNFSite.Text || _nfInscEstadual != txtNFInscricaoEstadual.Text || _nfInscMunicipal != txtNFInscricaoMunicipal.Text ||
                _nfOBS != txtNFObservacoes.Text)
            {
                if (btnNFFechar.Text == "Cancelar")
                {
                    mensagem = ListaMensagens.RetornaMensagem(06);
                    separador = mensagem.IndexOf(":");
                    DialogResult resultadoDialogo = MessageBox.Show(mensagem.Substring((separador + 2)), mensagem.Substring(0, (separador - 1)), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resultadoDialogo == DialogResult.Yes)
                    {
                        nfLimparCampos();

                        panelNF.Visible = false;
                        panelNF.Enabled = false;

                        AtualizaDGVFornecedores();

                        panelFornecedores.Enabled = true;
                    }
                }
                else
                {
                    mensagem = ListaMensagens.RetornaMensagem(18);
                    separador = mensagem.IndexOf(":");
                    DialogResult resultadoDialogo = MessageBox.Show(mensagem.Substring((separador + 2)), mensagem.Substring(0, (separador - 1)), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resultadoDialogo == DialogResult.Yes)
                    {
                        nfLimparCampos();

                        panelNF.Visible = false;
                        panelNF.Enabled = false;

                        if (_idFornecedor != 0)
                        {
                            funcoes.DestravaFornecedor(_idFornecedor);
                        }

                        AtualizaDGVFornecedores();

                        panelFornecedores.Enabled = true;
                    }
                }
            }
            else
            {
                nfLimparCampos();

                panelNF.Visible = false;
                panelNF.Enabled = false;

                AtualizaDGVFornecedores();

                panelFornecedores.Enabled = true;
            }   
        }

        private void btnNFBuscarEndereco_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNFCEP.Text))
            {
                nfLocalizarCEP();
            }
        }

        private void btnNFBuscarDados_Click(object sender, EventArgs e)
        {
            nfPesquisaCNPJ();
        }

        private void btnNFApagar_Click(object sender, EventArgs e)
        {

        }

        private void btnNFEditar_Click(object sender, EventArgs e)
        {
            string erro = null;
            int separador = 0;
            string nfCategoria1 = "", nfCategoria2 = "", nfCategoria3 = "", nfSubCategoria1 = "", nfSubCategoria2 = "", nfSubCategoria3 = "";
            if (!string.IsNullOrEmpty(txtNFNome.Text))
            {
                if (txtNFNome.Text.Length > 3)
                {
                    if (txtNFDataNascimento.Text == "  /  /")
                    {
                        txtNFDataNascimento.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                    }

                    if (cmbNFCateg1.SelectedIndex != -1)
                    {
                        nfCategoria1 = (cmbNFCateg1.SelectedIndex + 1).ToString();
                    }
                    if (cmbNFCateg2.SelectedIndex != -1)
                    {
                        nfCategoria2 = (cmbNFCateg2.SelectedIndex + 1).ToString();
                    }
                    if (cmbNFCateg3.SelectedIndex != -1)
                    {
                        nfCategoria3 = (cmbNFCateg3.SelectedIndex + 1).ToString();
                    }
                    if (cmbNFSubCateg1.SelectedIndex != -1)
                    {
                        nfSubCategoria1 = (cmbNFSubCateg1.SelectedIndex + 1).ToString();
                    }
                    if (cmbNFSubCateg2.SelectedIndex != -1)
                    {
                        nfSubCategoria2 = (cmbNFSubCateg2.SelectedIndex + 1).ToString();
                    }
                    if (cmbNFSubCateg3.SelectedIndex != -1)
                    {
                        nfSubCategoria3 = (cmbNFSubCateg3.SelectedIndex + 1).ToString();
                    }

                    try
                    {
                        funcoes.AtualizarFornecedor(txtNFIDFornecedor.Text, cmbNFTipoFornecedor.SelectedIndex.ToString(),
                        txtNFDataNascimento.Text, txtNFCPFCNPJ.Text, txtNFNome.Text, txtNFApelido.Text, txtNFCEP.Text,
                        txtNFEndereco.Text, txtNFNumero.Text, txtNFComplemento.Text, txtNFBairro.Text, txtNFCidade.Text,
                        txtNFEstado.Text, txtNFPais.Text, txtNFTelefone.Text, txtNFContato.Text, txtNFTelefoneComercial.Text,
                        txtNFContatoComercial.Text, txtNFCelular.Text, txtNFContatoCelular.Text, txtNFEmail.Text,
                        txtNFSite.Text, txtNFInscricaoEstadual.Text, txtNFInscricaoMunicipal.Text, txtNFObservacoes.Text,
                        nfCategoria1, nfCategoria2, nfCategoria3, nfSubCategoria1, nfSubCategoria2, nfSubCategoria3);

                        
                        _nfTipo = cmbNFTipoFornecedor.SelectedIndex;

                    }
                    catch (Exception)
                    {
                        erro = ListaErro.RetornaErro(56);
                        separador = erro.IndexOf(":");
                        MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw;
                    }
                    finally
                    {
                        erro = ListaMensagens.RetornaMensagem(17);
                        separador = erro.IndexOf(":");
                        MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (cmbNFTipoFornecedor.SelectedIndex == 0)
                    {
                        erro = ListaErro.RetornaErro(36);
                        separador = erro.LastIndexOf(":");
                        MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        erro = ListaErro.RetornaErro(50);
                        separador = erro.LastIndexOf(":");
                        MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                erro = ListaErro.RetornaErro(33);
                separador = erro.LastIndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNFImprimir_Click(object sender, EventArgs e)
        {

        }

        private void cmbNFCateg1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!iniciaTelaNovoFornecedor)
            {
                cmbNFSubCateg1.Enabled = true;
                cmbNFSubCateg1.DataSource = funcoes.CarregaSubCategoriaFornecedor(cmbNFCateg1.SelectedIndex + 1);
            }

        }

        private void cmbNFSubCateg1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!iniciaTelaNovoFornecedor)
            {
                cmbNFCateg2.Enabled = true;
                cmbNFCateg2.DataSource = nfCarregaCategoria();
            }
        }

        private void cmbNFCateg2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!iniciaTelaNovoFornecedor)
            {
                cmbNFSubCateg2.Enabled = true;
                cmbNFSubCateg2.DataSource = funcoes.CarregaSubCategoriaFornecedor(cmbNFCateg2.SelectedIndex + 1);
            }
        }

        private void cmbNFSubCateg2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!iniciaTelaNovoFornecedor)
            {
                cmbNFCateg3.Enabled = true;
                cmbNFCateg3.DataSource = nfCarregaCategoria();
            }
        }

        private void cmbNFCateg3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!iniciaTelaNovoFornecedor)
            {
                cmbNFSubCateg3.Enabled = true;
                cmbNFSubCateg3.DataSource = funcoes.CarregaSubCategoriaFornecedor(cmbNFCateg3.SelectedIndex + 1);
            }
        }

        private void btnNFNovoCadastro_Click(object sender, EventArgs e)
        {
            string erro = null;
            int separador = 0;
            if (btnNFNovoCadastro.Text == "Cadastrar Fornecedor")
            {
                string nfCategoria1 = "", nfCategoria2 = "", nfCategoria3 = "", nfSubCategoria1 = "", nfSubCategoria2 = "", nfSubCategoria3 = "";
                if (!string.IsNullOrEmpty(txtNFNome.Text))
                {
                    if (txtNFNome.Text.Length > 3)
                    {
                        if (txtNFDataNascimento.Text == "  /  /")
                        {
                            txtNFDataNascimento.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                        }

                        if (cmbNFCateg1.SelectedIndex != -1)
                        {
                            nfCategoria1 = cmbNFCateg1.SelectedIndex.ToString();
                        }
                        if (cmbNFCateg2.SelectedIndex != -1)
                        {
                            nfCategoria2 = cmbNFCateg2.SelectedIndex.ToString();
                        }
                        if (cmbNFCateg3.SelectedIndex != -1)
                        {
                            nfCategoria3 = cmbNFCateg3.SelectedIndex.ToString();
                        }
                        if (cmbNFSubCateg1.SelectedIndex != -1)
                        {
                            nfSubCategoria1 = cmbNFSubCateg1.SelectedIndex.ToString();
                        }
                        if (cmbNFSubCateg2.SelectedIndex != -1)
                        {
                            nfSubCategoria2 = cmbNFSubCateg2.SelectedIndex.ToString();
                        }
                        if (cmbNFSubCateg3.SelectedIndex != -1)
                        {
                            nfSubCategoria3 = cmbNFSubCateg3.SelectedIndex.ToString();
                        }

                        _idFornecedor = funcoes.CadastraFornecedor(cmbNFTipoFornecedor.SelectedIndex.ToString(), txtNFDataCadastro.Text,
                            txtNFDataNascimento.Text, txtNFCPFCNPJ.Text, txtNFNome.Text, txtNFApelido.Text, txtNFCEP.Text,
                            txtNFEndereco.Text, txtNFNumero.Text, txtNFComplemento.Text, txtNFBairro.Text, txtNFCidade.Text,
                            txtNFEstado.Text, txtNFPais.Text, txtNFTelefone.Text, txtNFContato.Text, txtNFTelefoneComercial.Text,
                            txtNFContatoComercial.Text, txtNFCelular.Text, txtNFContatoCelular.Text, txtNFEmail.Text,
                            txtNFSite.Text, txtNFInscricaoEstadual.Text, txtNFInscricaoMunicipal.Text, txtNFObservacoes.Text,
                            nfCategoria1, nfCategoria2, nfCategoria3, nfSubCategoria1, nfSubCategoria2, nfSubCategoria3, true);

                        if (_idFornecedor != 0)
                        {
                            erro = ListaMensagens.RetornaMensagem(16);
                            separador = erro.IndexOf(":");
                            DialogResult resultadoDialogo = MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (resultadoDialogo == DialogResult.Yes)
                            {
                                nfLimparCampos();
                                funcoes.DestravaFornecedor(_idFornecedor);
                            }
                            else
                            {
                                txtNFIDFornecedor.Text = _idFornecedor.ToString();

                                btnNFNovoCadastro.Text = "Novo Cadastro";
                                btnNFImprimir.Enabled = true;
                                btnNFEditar.Enabled = true;
                                btnNFApagar.Enabled = true;
                                btnNFFechar.Text = "Fechar";
                            }
                        }

                    }
                    else
                    {
                        if (cmbNFTipoFornecedor.SelectedIndex == 0)
                        {
                            erro = ListaErro.RetornaErro(36);
                            separador = erro.LastIndexOf(":");
                            MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            erro = ListaErro.RetornaErro(50);
                            separador = erro.LastIndexOf(":");
                            MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    if (cmbNFTipoFornecedor.SelectedIndex == 0)
                    {
                        erro = ListaErro.RetornaErro(33);
                        separador = erro.LastIndexOf(":");
                        MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        erro = ListaErro.RetornaErro(35);
                        separador = erro.LastIndexOf(":");
                        MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                bool testeMudancas = false;
                if (testeMudancas)
                {
                    erro = ListaMensagens.RetornaMensagem(04);
                    separador = erro.IndexOf(":");
                    DialogResult resultadoDialogo = MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resultadoDialogo == DialogResult.Yes)
                    {
                        nfLimparCampos();
                    }
                }
                else
                {
                    nfLimparCampos();
                }
            } 
        }
        #endregion

        /*


               try
               {
                   foreach (DataGridViewRow dgvr in dgvClientes.Rows)
                   {
                       if (dgvr.Cells["contrato"].Value.ToString() == "2")
                       {
                           dgvr.DefaultCellStyle.BackColor = Color.LightBlue;
                       }
                       else if (dgvr.Cells["contrato"].Value.ToString() == "3")
                       {
                           dgvr.DefaultCellStyle.BackColor = Color.Yellow;
                       }
                       else if (dgvr.Cells["contrato"].Value.ToString() == "4")
                       {
                           dgvr.DefaultCellStyle.BackColor = Color.YellowGreen;
                       }
                   }
               }
               catch (NullReferenceException)
               {
                   return;
               }

               //Esconde a coluna contrato
               dgvClientes.Columns["contrato"].Visible = false;
           }
       }
       */
        /*
        private void dgvClientes_DataSourceChanged(object sender, EventArgs e)
        {
            dgvClientes.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvClientes.Columns[2].MinimumWidth = 150;
        }

        private void dgvClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                int idCliente = 0, posicaoAtualScroll = dgvClientes.FirstDisplayedScrollingRowIndex;


                DataGridViewRow linha = dgvClientes.Rows[e.RowIndex];
                idCliente = funcoes.AbreCliente(linha.Cells["Nome"].Value.ToString());

                string comando = "Insert into tbl_log values (0," + idUsuario + ", 'Abriu cliente ID: " + idCliente + " - " +
                DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "');";
                conexao.ExecutaComando(comando);

                CadastraCliente cadastraCliente = new CadastraCliente(false, idCliente);
                cadastraCliente.ShowDialog();

                AtualizaDGVClientes();
                dgvClientes.FirstDisplayedScrollingRowIndex = posicaoAtualScroll;
            }
        }

        private void btnAddCliente_Click(object sender, EventArgs e)
        {
            int posicaoAtualScroll = dgvClientes.FirstDisplayedScrollingRowIndex;

            CadastraCliente cadastraCliente = new CadastraCliente(true, 0);
            cadastraCliente.ShowDialog();

            AtualizaDGVFornecedores();
            dgvFornecedores.FirstDisplayedScrollingRowIndex = posicaoAtualScroll;
        }

        private void btnPesqFornecedor_Click(object sender, EventArgs e)
        {
            /*
            TelaPesquisa telaPesquisaCliente = new TelaPesquisa(dgvClientes);
            telaPesquisaCliente.ShowDialog();

            if (FuncoesEstaticas.ClientePesquisado != -1)
            {
                dgvClientes[0, FuncoesEstaticas.ClientePesquisado].Selected = true;
            }
            else
            {
                string erro = ListaErro.RetornaErro(48);
                int separador = erro.LastIndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void cmbFiltroFornecedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizaDGVFornecedores();
            dgvFornecedores.Focus();
        }

        #region Impressão

        StringFormat strFormat; //Used to format the grid rows.
        ArrayList arrColumnLefts = new ArrayList();//Used to save left coordinates of columns
        ArrayList arrColumnWidths = new ArrayList();//Used to save column widths
        int iCellHeight = 0; //Used to get/set the datagridview cell height
        int iTotalWidth = 0; //
        int iRow = 0;//Used as counter
        bool bFirstPage = false; //Used to check whether we are printing first page
        bool bNewPage = false;// Used to check whether we are printing a new page
        int iHeaderHeight = 0; //Used for the header height

        private void btnPrintListaFornecedores_Click(object sender, EventArgs e)
        {
            ImprimirFornecedores();
        }

        private void pdFornecedores_BeginPrint(object sender, PrintEventArgs e)
        {
            try
            {
                strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Near;
                strFormat.LineAlignment = StringAlignment.Center;
                strFormat.Trimming = StringTrimming.EllipsisCharacter;

                arrColumnLefts.Clear();
                arrColumnWidths.Clear();
                iCellHeight = 0;
                iRow = 0;
                bFirstPage = true;
                bNewPage = true;

                // Calculating Total Widths
                iTotalWidth = 0;
                foreach (DataGridViewColumn dgvGridCol in dgvClientes.Columns)
                {
                    iTotalWidth += dgvGridCol.Width;
                }
            }
            catch (Exception)
            {
                string erro = ListaErro.RetornaErro(23);
                int separador = erro.LastIndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ImprimirFornecedores()
        {
            if (pdImprimirFornecedores.ShowDialog() == DialogResult.OK)
            {
                pdFornecedores.DefaultPageSettings.Landscape = true;
                pdFornecedores.DocumentName = "Lista de Clientes - CFTVA " + DateTime.Now.Date.ToShortDateString();

                pPreviewFornecedores.ShowDialog();
                AtualizaDGVFornecedores();
            }

        }

        private void pdFornecedores_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                //Set the left margin
                int iLeftMargin = e.MarginBounds.Left;
                //Set the top margin
                int iTopMargin = e.MarginBounds.Top;
                //Whether more pages have to print or not
                bool bMorePagesToPrint = false;
                int iTmpWidth = 0;

                //For the first page to print set the cell width and header height
                if (bFirstPage)
                {
                    foreach (DataGridViewColumn GridCol in dgvClientes.Columns)
                    {
                        if (GridCol.Name != "contrato")
                        {
                            iTmpWidth = (int)(Math.Floor((double)((double)GridCol.Width /
                            (double)iTotalWidth * (double)iTotalWidth *
                            ((double)e.MarginBounds.Width / (double)iTotalWidth))));

                            iHeaderHeight = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
                                GridCol.InheritedStyle.Font, iTmpWidth).Height) + 11;

                            // Save width and height of headers
                            arrColumnLefts.Add(iLeftMargin);
                            arrColumnWidths.Add(iTmpWidth);
                            iLeftMargin += iTmpWidth;
                        }
                    }
                }
                //Loop till all the grid rows not get printed
                while (iRow <= dgvClientes.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = dgvClientes.Rows[iRow];
                    //Set the cell height
                    iCellHeight = GridRow.Height + 12;
                    int iCount = 0;
                    //Check whether the current page settings allows more rows to print
                    if (iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }
                    else
                    {
                        if (bNewPage)
                        {
                            //Draw Header
                            e.Graphics.DrawString("Lista de Clientes",
                                new Font(dgvClientes.Font, FontStyle.Bold),
                                Brushes.Black, e.MarginBounds.Left,
                                e.MarginBounds.Top - e.Graphics.MeasureString("Lista de Clientes",
                                new Font(dgvClientes.Font, FontStyle.Bold),
                                e.MarginBounds.Width).Height - 13);

                            String strDate = DateTime.Now.ToLongDateString() + " " +
                                DateTime.Now.ToShortTimeString();
                            //Draw Date
                            e.Graphics.DrawString(strDate,
                                new Font(dgvClientes.Font, FontStyle.Bold), Brushes.Black,
                                e.MarginBounds.Left +
                                (e.MarginBounds.Width - e.Graphics.MeasureString(strDate,
                                new Font(dgvClientes.Font, FontStyle.Bold),
                                e.MarginBounds.Width).Width),
                                e.MarginBounds.Top - e.Graphics.MeasureString("Lista de Clientes",
                                new Font(new Font(dgvClientes.Font, FontStyle.Bold),
                                FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            //Draw Columns                 
                            iTopMargin = e.MarginBounds.Top;
                            foreach (DataGridViewColumn GridCol in dgvClientes.Columns)
                            {
                                if (GridCol.Name != "contrato")
                                {
                                    e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
                                    new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight));

                                    e.Graphics.DrawRectangle(Pens.Black,
                                        new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                        (int)arrColumnWidths[iCount], iHeaderHeight));

                                    e.Graphics.DrawString(GridCol.HeaderText,
                                        GridCol.InheritedStyle.Font,
                                        new SolidBrush(GridCol.InheritedStyle.ForeColor),
                                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                        (int)arrColumnWidths[iCount], iHeaderHeight), strFormat);
                                    iCount++;
                                }
                            }
                            bNewPage = false;
                            iTopMargin += iHeaderHeight;
                        }
                        iCount = 0;
                        //Draw Columns Contents                
                        foreach (DataGridViewCell Cel in GridRow.Cells)
                        {
                            if (Cel.ColumnIndex != 4)
                            {
                                if (Cel.Value != null)
                                {
                                    string texto = Cel.Value.ToString();

                                    if (Cel.ColumnIndex == 0)
                                    {
                                        if (texto.Length > 50)
                                        {
                                            texto = texto.Substring(0, 47) + "...";
                                        }
                                    }

                                    e.Graphics.DrawString(texto,
                                    Cel.InheritedStyle.Font,
                                    new SolidBrush(Cel.InheritedStyle.ForeColor),
                                    new RectangleF((int)arrColumnLefts[iCount],
                                    (float)iTopMargin,
                                    (int)arrColumnWidths[iCount], (float)iCellHeight),
                                    strFormat);
                                }

                                //Drawing Cells Borders 
                                e.Graphics.DrawRectangle(Pens.Black,
                                    new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iCellHeight));
                                iCount++;
                            }
                        }
                    }
                    iRow++;
                    iTopMargin += iCellHeight;
                }
                //If more lines exist, print another page.
                if (bMorePagesToPrint)
                    e.HasMorePages = true;
                else
                    e.HasMorePages = false;
            }
            catch (Exception)
            {
                string erro = ListaErro.RetornaErro(24);
                int separador = erro.LastIndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    */
        #endregion

        #region Opcoes
        private void Backup()
        {
                try
                {
                    string dia = DateTime.Now.Day.ToString();
                    string mes = DateTime.Now.Month.ToString();
                    string ano = DateTime.Now.Year.ToString();
                    string hora = DateTime.Now.ToShortTimeString().Replace(":", "");
                    string nomeDoArquivo = ano + mes + dia + "-" + hora;

                    sfdSalvarArquivo.Filter = "Backup SQL|*.sql";
                    sfdSalvarArquivo.Title = "Realizar Backup";
                    sfdSalvarArquivo.FileName = nomeDoArquivo;

                    if (sfdSalvarArquivo.ShowDialog() == DialogResult.OK)
                    {
                        string local = Path.GetFullPath(sfdSalvarArquivo.FileName);
                        if (conexao.Backup(local))
                        {
                            if (System.IO.File.Exists(local))
                            {
                                string comando = "Insert into tbl_log values (0," + idUsuario + ", 'Backup realizado - " +
                                DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "');";
                                conexao.ExecutaComando(comando);

                                MessageBox.Show("Backup realizado com sucesso!", "Backup realizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                string erro = ListaErro.RetornaErro(22);
                                int separador = erro.LastIndexOf(":");
                                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                catch (Exception)
                {

                    throw;
                }

        }

        private void btnTestarAplicar_Click(object sender, EventArgs e)
        {
            bool abreprograma = false;

            if (rdbtnServidorLocal.Checked)
            {
                if (conexao.TestaConexao(txtBanco.Text, txtUid.Text, txtPwd.Text))
                {
                    abreprograma = true;
                }
            }
            else
            {
                if (conexao.TestaConexao(txtServidor.Text, txtBanco.Text, txtUid.Text, txtPwd.Text))
                {
                    abreprograma = true;
                }
            }

            if (abreprograma)
            {
                XElement xml = XElement.Load(nomeXML);
                XElement x = xml.Elements().First();
                if (x != null)
                {
                    if (rdbtnRemoto.Checked && txtServidor.Text != "")
                    {
                        x.Attribute("servidor").SetValue(txtServidor.Text);
                    }
                    x.Attribute("banco").SetValue(txtBanco.Text);
                    x.Attribute("uid").SetValue(txtUid.Text);
                    x.Attribute("pwd").SetValue(txtPwd.Text);
                }

                xml.Save(nomeXML);

                string comando = "Insert into tbl_log values (0," + idUsuario + ", 'Informações de conexão SQL alteradas - " +
                DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "');";
                conexao.ExecutaComando(comando);

                MessageBox.Show("Dados atualizados com sucesso!");
            }
        }

        private void rdbtnRemoto_Click(object sender, EventArgs e)
        {
            rdbtnRemoto.Checked = true;
            rdbtnServidorLocal.Checked = false;
            txtServidor.Enabled = true;
        }

        private void rdbtnServidorLocal_Click(object sender, EventArgs e)
        {
            rdbtnRemoto.Checked = false;
            rdbtnServidorLocal.Checked = true;
            txtServidor.Enabled = false;
        }

        private void btnDestravaTarefas_Click(object sender, EventArgs e)
        {
            DialogResult resultadoDialogo = MessageBox.Show("Você tem certeza de que deseja destravar todas as tarefas?\nTodas as outras estações devem estar fechadas!", "Destravar Tarefas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultadoDialogo == DialogResult.Yes)
            {
                /*string resposta = Microsoft.VisualBasic.Interaction.InputBox("Digite a senha para prosseguir", "Destravar Tarefas", "");

                if (resposta == "MB8719")
                {*/
                if (funcoes.DestravaTodasTarefas())
                {
                    string comando = "Insert into tbl_log values (0," + idUsuario + ", 'As tarefas foram destravadas - " +
                        DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "');";
                    conexao.ExecutaComando(comando);

                    MessageBox.Show("As tarefas foram destravadas com sucesso.");
                }
                //}
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            Backup();
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            try
            {
                string dia = DateTime.Now.Day.ToString();
                string mes = DateTime.Now.Month.ToString();
                string ano = DateTime.Now.Year.ToString();
                string hora = DateTime.Now.ToShortTimeString().Replace(":", "");
                string nomeDoArquivo = ano + mes + dia + "-" + hora;

                ofdAbrirArquivo.Filter = "Backup SQL|*.sql";
                ofdAbrirArquivo.Title = "Realizar Backup";

                if (ofdAbrirArquivo.ShowDialog() == DialogResult.OK)
                {
                    string local = Path.GetFullPath(ofdAbrirArquivo.FileName);
                    if (conexao.Backup(local))
                    {
                        if (System.IO.File.Exists(local))
                        {
                            string comando = "Insert into tbl_log values (0," + idUsuario + ", 'Restauração realizada - " +
                            DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "');";
                            conexao.ExecutaComando(comando);
                            MessageBox.Show("Restauração realizada com sucesso!", "Restauração realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            string erro = ListaErro.RetornaErro(22);
                            int separador = erro.LastIndexOf(":");
                            MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion


    }
}