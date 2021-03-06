﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;
using Microsoft.VisualBasic;
using Gerenciador_de_Tarefas.Classes;
using System.Diagnostics;

namespace Gerenciador_de_Tarefas
{
    public partial class TInicial : Form
    {
        #region telaInicial
        public TInicial() 
        {
            InitializeComponent();
        }

        private void TInicial_Load(object sender, EventArgs e)
        {
            //Título do Software 
            Text = "Gerenciador de Tarefas - CFTVA " + Sistema.Ano;
            //Versão do Software
            lblVersao.Text = "Versão: " + Sistema.VersaoSoftware;
            //Coloca a hora
            lblHorario.Text = "Hora: " + Sistema.Hora;
            //Seta o nome do usuário logado
            lblUsuario.Text = "Usuário: " + Sistema.NomeUsuarioLogado;

            EscondePaineis();
        }

        private void TInicial_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Sistema.ProgramaDesativado)
            {
                ListaErro.RetornaErro(14);
                Sistema.ProgramaDesativado = false;
            }
            else
            {
                Log.Logoff();
            }
        }

        private void BtnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnClientes_Click(object sender, EventArgs e)
        {
            if (!panelClientes.Visible)
            {
                EscondePaineis();
                panelClientes.Visible = true;
                panelClientes.Enabled = true;
                panelClientes.Show();

                if (!Sistema.IniciaTelaClientes)
                {
                    AtualizaDGVClientes(dgvClientes.CurrentCell.RowIndex);

                    for (int i = 0; i < dgvClientes.Columns.Count; i++)
                    {
                        dgvClientes.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
                else
                {
                    AtualizaDGVClientes(0);
                }

                dgvClientes.Focus();
            }
        }

        private void BtnFornecedores_Click(object sender, EventArgs e)
        {
            if (!panelFornecedores.Visible)
            {
                EscondePaineis();
                panelFornecedores.Visible = true;
                panelFornecedores.Enabled = true;
                panelFornecedores.Show();

                if (!Sistema.IniciaTelaFornecedores)
                {
                    AtualizaDGVFornecedores(dgvFornecedores.CurrentCell.RowIndex);

                    for (int i = 0; i < dgvFornecedores.Columns.Count; i++)
                    {
                        dgvFornecedores.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
                else
                {
                    AtualizaDGVFornecedores(0);
                }

                dgvFornecedores.Focus();
            }
        }

        private void BtnTarefas_Click(object sender, EventArgs e)
        {
            if (!panelTarefas.Visible)
            {
                EscondePaineis();
                panelTarefas.Visible = true;
                panelTarefas.Enabled = true;
                panelTarefas.Show();

                if (!Sistema.IniciaTelaTarefas)
                {
                    AtualizaDGVTarefas(dgvTarefas.CurrentCell.RowIndex);

                    for (int i = 0; i < dgvTarefas.Columns.Count; i++)
                    {
                        dgvTarefas.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
                else
                {
                    AtualizaDGVTarefas(0);
                }

                dgvTarefas.Focus();
            }
        }

        private void BtnOpcoes_Click(object sender, EventArgs e)
        {
            if (!panelOpcoes.Visible)
            {
                EscondePaineis();

                if (Sistema.ServidorLocal)
                {
                    rdbtnRemoto.Checked = true;
                    rdbtnServidorLocal.Checked = false;
                    txtServidor.Text = Encoding.UTF8.GetString(Convert.FromBase64String(Sistema.EnderecoServidor));
                    txtServidor.Enabled = true;
                }

                txtBanco.Text = Encoding.UTF8.GetString(Convert.FromBase64String(Sistema.NomeBanco));
                txtUid.Text = Encoding.UTF8.GetString(Convert.FromBase64String(Sistema.NomeUsuario));
                txtPwd.Text = Encoding.UTF8.GetString(Convert.FromBase64String(Sistema.SenhaUsuario));

                panelOpcoes.Show();
                panelOpcoes.Enabled = true;
                panelOpcoes.Visible = true;
            }
        }

        private void TimerHora_Tick(object sender, EventArgs e)
        {
            lblHorario.Text = "Hora: " + Sistema.Hora;

            if (Sistema.ContadorSegundos == 30)
            {
                if (panelClientes.Visible)
                {
                    AtualizaDGVClientes(dgvClientes.CurrentCell.RowIndex);
                }
                else if (panelFornecedores.Visible)
                {
                    AtualizaDGVFornecedores(dgvFornecedores.CurrentCell.RowIndex);
                }
                else if (panelTarefas.Visible)
                {
                    AtualizaDGVTarefas(dgvTarefas.CurrentCell.RowIndex);
                }
            }
        }

        private void EscondePaineis()
        {
            panelClientes.Visible = false;
            panelClientes.Enabled = false;
            panelClientes.Hide();

            panelFornecedores.Visible = false;
            panelFornecedores.Enabled = false;
            panelFornecedores.Hide();

            panelNF.Visible = false;
            panelNF.Enabled = false;
            panelNF.Hide();

            panelTarefas.Visible = false;
            panelTarefas.Enabled = false;
            panelTarefas.Hide();

            panelNT.Visible = false;
            panelNT.Enabled = false;
            panelNT.Hide();

            panelOpcoes.Visible = false;
            panelOpcoes.Enabled = false;
            panelOpcoes.Hide();
        }

        private void TInicial_KeyDown(object sender, KeyEventArgs e)
        {
            //O painel de clientes está visível?
            if (panelClientes.Visible)
            {
                //Se apertar CTRL + N
                if (e.Control && e.KeyCode == Keys.N)
                {
                    // Simula o click
                    BtnAddCliente_Click(btnAddCliente, e);
                    e.SuppressKeyPress = true;
                }
                //Se apertar CTRL + P
                else if (e.Control && e.KeyCode == Keys.P)
                {
                    // Simula o click
                    BtnPrintListaClientes_Click(btnPrintListaClientes, e);
                    e.SuppressKeyPress = true;
                }
                //Se apertar CTRL + F
                else if (e.Control && e.KeyCode == Keys.F)
                {
                    // Simula o click
                    BtnPesqCliente_Click(btnPesqCliente, e);
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
                        DgvClientes_CellDoubleClick(dgvClientes, new DataGridViewCellEventArgs(dgvClientes.CurrentCell.ColumnIndex, dgvClientes.CurrentRow.Index));
                    }
                }
            }
            //O painel de fornecedores está visível?
            else if (panelFornecedores.Visible)
            {
                // Se apertar CTRL +N
                if (e.Control && e.KeyCode == Keys.N)
                {
                    // Simula o click
                    BtnNovoFornecedor_Click(btnNovoFornecedor, e);
                    e.SuppressKeyPress = true;
                }
                //Se apertar CTRL + P
                else if (e.Control && e.KeyCode == Keys.P)
                {
                    // Simula o click
                    BtnImprimeFornecedores_Click(btnImprimeFornecedores, e);
                    e.SuppressKeyPress = true;
                }
                //Se apertar ESC
                else if (e.KeyCode == Keys.Escape)
                {
                    panelFornecedores.Visible = false;
                    e.SuppressKeyPress = true;
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    if (dgvFornecedores.Focused)
                    {
                        //Executa a mesma função de dar 2 cliques com o mouse no cliente
                        DgvFornecedores_CellDoubleClick(dgvFornecedores, new DataGridViewCellEventArgs(dgvFornecedores.CurrentCell.ColumnIndex, dgvFornecedores.CurrentRow.Index));
                    }
                    e.SuppressKeyPress = true;
                }
            }
            //O painel do Fornecedor/Novo Fornecedor está visível?
            else if (panelNF.Visible)
            {
                // Se apertar CTRL +N
                if (e.Control && e.KeyCode == Keys.N)
                {
                    BtnNFNovoCadastro_Click(btnNFNovoCadastro, e);
                    e.SuppressKeyPress = true;
                }
                //Se apertar CTRL + P
                else if (e.Control && e.KeyCode == Keys.P)
                {
                    BtnNFImprimir_Click(btnNFImprimir, e);
                    e.SuppressKeyPress = true;
                }
                //Se apertar ESC
                else if (e.KeyCode == Keys.Escape)
                {
                    BtnNFFechar_Click(btnNFFechar, e);
                    e.SuppressKeyPress = true;
                }
            }
            //O painel de tarefas está visível?
            else if (panelTarefas.Visible)
            {
                // Se apertar CTRL +N
                if (e.Control && e.KeyCode == Keys.N)
                {
                    BtnNovaTarefa_Click(btnNovaTarefa, e);
                    e.SuppressKeyPress = true;
                }
                //Se apertar CTRL + P
                else if (e.Control && e.KeyCode == Keys.P)
                {
                    BtnImprimirListaTarefas_Click(btnImprimirListaTarefas, e);
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
                        DgvTarefas_CellDoubleClick(dgvTarefas, new DataGridViewCellEventArgs(dgvTarefas.CurrentCell.ColumnIndex, dgvTarefas.CurrentRow.Index));
                    }
                }
            }
            //O painel de NT está visível?
            else if (panelNT.Visible)
            {
                // Se apertar CTRL +N
                if (e.Control && e.KeyCode == Keys.N)
                {
                    if(btnNTCadastrar.Text == "Nova Tarefa")
                    {
                        BtnNTCadastrar_Click(btnNTCadastrar, e);
                    }
                    e.SuppressKeyPress = true;
                }
                //Se apertar CTRL + P
                else if (e.Control && e.KeyCode == Keys.P)
                {
                    if(btnNTImprimir.Visible)
                    {
                        BtnNTImprimir_Click(btnNTImprimir, e);
                    }
                    e.SuppressKeyPress = true;
                }
                //Se apertar ESC
                else if (e.KeyCode == Keys.Escape)
                {
                    BtnNTSair_Click(btnNTSair, e);
                    e.SuppressKeyPress = true;
                }
                else if (e.Control && e.KeyCode == Keys.S)
                {
                    if (btnNTCadastrar.Text == "Cadastrar Tarefa")
                    {
                        BtnNTCadastrar_Click(btnNTCadastrar, e);
                    }
                    else
                    {
                        BtnNTEditar_Click(btnNTEditar, e);
                    }
                    e.SuppressKeyPress = true;
                }
            }
            //O painel de fornecedores está visível?
            else if (panelOpcoes.Visible)
            {
                //Se apertar CTRL + S
                if (e.Control && e.KeyCode == Keys.S)
                {
                    //Sistema.SalvarDadosXML(rdbtnRemoto.Checked, txtServidor.Text, txtBanco.Text, txtUid.Text, txtPwd.Text);
                    e.SuppressKeyPress = true;
                }
                //Se apertar CTRL + D
                else if (e.Control && e.KeyCode == Keys.D)
                {
                    if (Sistema.IDUsuarioLogado != 1 && Sistema.IDUsuarioLogado != 2)
                    {
                        if (ListaMensagens.RetornaDialogo(31) == DialogResult.Yes)
                        {
                            string resposta = Interaction.InputBox(ListaMensagens.RetornaInputBox(32)[1], ListaMensagens.RetornaInputBox(32)[0], "");

                            if (resposta == Sistema.SenhaADM)
                            {
                                if (Tarefa.DestravaTodasTarefas())
                                {
                                    ListaMensagens.RetornaMensagem(33, MessageBoxIcon.Information);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (ListaMensagens.RetornaDialogo(31) == DialogResult.Yes)
                        {
                            if (Tarefa.DestravaTodasTarefas())
                            {
                                ListaMensagens.RetornaMensagem(33, MessageBoxIcon.Information);
                            }
                        }
                    }
                    e.SuppressKeyPress = true;
                }
                //Se apertar CTRL + B
                else if (e.Control && e.KeyCode == Keys.B)
                {
                    //Backup();
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
                        //Simula o click
                        BtnClientes_Click(btnClientes, e);
                    }
                }
                //Se apertar o F2
                else if (e.KeyCode == Keys.F2)
                {
                    //O painel de Fornecedores não está visível?
                    if (!panelFornecedores.Visible && !panelNF.Visible)
                    {
                        //Simula o click
                        BtnFornecedores_Click(btnFornecedores, e);
                    }
                }
                //Se apertar o F3
                else if (e.KeyCode == Keys.F3)
                {
                    //O painel de Tarefas não está visível?
                    if (!panelTarefas.Visible)
                    {
                        //Simula o click
                        BtnTarefas_Click(btnTarefas, e);
                    }
                }
                //Se apertar o F5
                else if (e.KeyCode == Keys.F5)
                {
                    //O painel de Tarefas não está visível?
                    if (!panelOpcoes.Visible)
                    {
                        BtnOpcoes_Click(btnOpcoes, e);
                    }
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
        /// Método responsável por atualizar a tabela da tela de clientes
        /// </summary>
        /// 
        private void AtualizaDGVClientes(int linha)
        {
            if (Cliente.AtualizaDGVClientes(cmbFiltroClientes.SelectedIndex))
            {
                dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dgvClientes.DataSource = Cliente.DGVAtualizada.DataSource;

                Funcoes.FastAutoSizeColumns(dgvClientes);

                //Desmarca a primeira linha
                dgvClientes.Rows[0].Selected = false;

                //Tenta selecionar a linha
                try
                {
                    dgvClientes[0, linha].Selected = true;
                    dgvClientes.FirstDisplayedScrollingRowIndex = 0;
                }
                catch (ArgumentOutOfRangeException)
                {
                    dgvClientes[0, 0].Selected = true;
                    dgvClientes.FirstDisplayedScrollingRowIndex = 0;
                }

                try
                {
                    foreach (DataGridViewRow dgvr in dgvClientes.Rows)
                    {
                        if(dgvr.Cells["Telefone"].Value.ToString() != "")
                        {
                            if(dgvr.Cells["Telefone"].Value.ToString().Length > 7 && dgvr.Cells["Telefone"].Value.ToString().Length < 11)
                            {
                                dgvr.Cells["Telefone"].Value = "(" + dgvr.Cells["Telefone"].Value.ToString().Substring(0, 2) + ") " +
                                    dgvr.Cells["Telefone"].Value.ToString().Substring(2, 4) + "-" +
                                    dgvr.Cells["Telefone"].Value.ToString().Substring(6, 4);
                            }
                            else if(dgvr.Cells["Telefone"].Value.ToString().Length > 8 && dgvr.Cells["Telefone"].Value.ToString().Length < 12)
                            {
                                dgvr.Cells["Telefone"].Value = "(" + dgvr.Cells["Telefone"].Value.ToString().Substring(0, 2) + ") " +
                                    dgvr.Cells["Telefone"].Value.ToString().Substring(2, 5) + "-" +
                                    dgvr.Cells["Telefone"].Value.ToString().Substring(7, 4);
                            }
                        }

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

        private void DgvClientes_DataSourceChanged(object sender, EventArgs e)
        {
            dgvClientes.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvClientes.Columns[2].MinimumWidth = 150;
        }

        private void DgvClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                int posicaoAtualScroll = dgvClientes.FirstDisplayedScrollingRowIndex;

                DataGridViewRow linha = dgvClientes.Rows[e.RowIndex];
                Cliente.PreCarregaCliente(linha.Cells["Nome"].Value.ToString());

                CadastraCliente cadastraCliente = new CadastraCliente();
                cadastraCliente.ShowDialog();

                AtualizaDGVClientes(linha.Index);
            }
        }

        private void BtnAddCliente_Click(object sender, EventArgs e)
        {
            int posicaoAtualScroll = dgvClientes.FirstDisplayedScrollingRowIndex;

            //Limpa todas as variáveis
            Cliente.LimparVariaveis();
            Cliente.NovoCadastro = true;
            CadastraCliente cadastraCliente = new CadastraCliente();
            cadastraCliente.ShowDialog();

            AtualizaDGVClientes(dgvClientes.CurrentCell.RowIndex);
            dgvClientes.FirstDisplayedScrollingRowIndex = posicaoAtualScroll;
        }

        private void BtnPesqCliente_Click(object sender, EventArgs e)
        {
            TelaPesquisa telaPesquisaCliente = new TelaPesquisa(dgvClientes, true);
            telaPesquisaCliente.ShowDialog();

            if (Cliente.ClientePesquisado != -1)
            {
                dgvClientes[0, Cliente.ClientePesquisado].Selected = true;
            }
            else
            {
                ListaErro.RetornaErro(48);
            }

            dgvClientes.Focus();
        }

        private void CmbFiltroClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizaDGVClientes(dgvClientes.CurrentCell.RowIndex);
            dgvClientes.Focus();
        }

        #region Impressão
        private void BtnPrintListaClientes_Click(object sender, EventArgs e)
        {
            if (pdImprimirClientes.ShowDialog() == DialogResult.OK)
            {
                pdClientes.DefaultPageSettings.Landscape = true;
                pdClientes.DocumentName = "Lista de Clientes - CFTVA " + DateTime.Now.Date.ToShortDateString();

                pPreviewClientes.ShowDialog();
                AtualizaDGVClientes(dgvClientes.CurrentCell.RowIndex);
            }
        }

        private void PdClientes_BeginPrint(object sender, PrintEventArgs e)
        {
            try
            {
                Impressao.FormatoString = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Center,
                    Trimming = StringTrimming.EllipsisCharacter
                };

                Impressao.ArrColunasRestantes.Clear();
                Impressao.ArrLarguraColunas.Clear();
                Impressao.AlturaCelula = 0;
                Impressao.LinhaAtual = 0;
                Impressao.PrimeiraPagina = true;
                Impressao.NovaPagina = true;

                // Calcula a Largura Total
                Impressao.LarguraTotal = 0;
                foreach (DataGridViewColumn dgvGridCol in dgvClientes.Columns)
                {
                    Impressao.LarguraTotal += dgvGridCol.Width;
                }
            }
            catch (Exception)
            {
                ListaErro.RetornaErro(23);
            }
        }

        private void PdClientes_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                // Seta a margem esquerda da folha
                Impressao.MargemEsquerda = e.MarginBounds.Left;
                // Seta a margem superior da folha
                Impressao.MargemSuperior = e.MarginBounds.Top;
                //Verifica se há mais páginas para imprimir
                Impressao.TemMaisPaginasParaImprimir = false;
                Impressao.LarguraTemporaria = 0;

                //Seta a largura da célula e a altura do cabeçalho caso seja a primeira folha
                if (Impressao.PrimeiraPagina)
                {
                    foreach (DataGridViewColumn GridCol in dgvClientes.Columns)
                    {
                        if (GridCol.Name != "contrato")
                        {
                            Impressao.LarguraTemporaria = (int)(Math.Floor((double)((double)GridCol.Width /
                            (double)Impressao.LarguraTotal * (double)Impressao.LarguraTotal *
                            ((double)e.MarginBounds.Width / (double)Impressao.LarguraTotal))));

                            Impressao.AlturaCabecalho = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
                                GridCol.InheritedStyle.Font, Impressao.LarguraTemporaria).Height) + 11;

                            // Save width and height of headers
                            Impressao.ArrColunasRestantes.Add(Impressao.MargemEsquerda);
                            Impressao.ArrLarguraColunas.Add(Impressao.LarguraTemporaria);
                            Impressao.MargemEsquerda += Impressao.LarguraTemporaria;
                        }
                    }
                }
                //Loop till all the grid rows not get printed
                while (Impressao.LinhaAtual <= dgvClientes.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = dgvClientes.Rows[Impressao.LinhaAtual];
                    //Set the cell height
                    Impressao.AlturaCelula = GridRow.Height + 12;
                    int iCount = 0;
                    //Check whether the current page settings allows more rows to print
                    if (Impressao.MargemSuperior + Impressao.AlturaCelula >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        Impressao.NovaPagina = true;
                        Impressao.PrimeiraPagina = false;
                        Impressao.TemMaisPaginasParaImprimir = true;
                        break;
                    }
                    else
                    {
                        if (Impressao.NovaPagina)
                        {
                            //Draw Header
                            e.Graphics.DrawString("Lista de Clientes",
                                new Font(dgvClientes.Font, FontStyle.Bold),
                                Impressao.FonteCorPreta, e.MarginBounds.Left,
                                e.MarginBounds.Top - e.Graphics.MeasureString("Lista de Clientes",
                                new Font(dgvClientes.Font, FontStyle.Bold),
                                e.MarginBounds.Width).Height - 13);

                            //Draw Date
                            e.Graphics.DrawString(Sistema.DataHoraImpressao,
                                new Font(dgvClientes.Font, FontStyle.Bold), Impressao.FonteCorPreta,
                                e.MarginBounds.Left +
                                (e.MarginBounds.Width - e.Graphics.MeasureString(Sistema.DataHoraImpressao,
                                new Font(dgvClientes.Font, FontStyle.Bold),
                                e.MarginBounds.Width).Width),
                                e.MarginBounds.Top - e.Graphics.MeasureString("Lista de Clientes",
                                new Font(new Font(dgvClientes.Font, FontStyle.Bold),
                                FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            //Draw Columns                 
                            Impressao.MargemSuperior = e.MarginBounds.Top;
                            foreach (DataGridViewColumn GridCol in dgvClientes.Columns)
                            {
                                if (GridCol.Name != "contrato")
                                {
                                    e.Graphics.FillRectangle(Impressao.FonteCorCinzaClaro,
                                    new Rectangle((int)Impressao.ArrColunasRestantes[iCount], Impressao.MargemSuperior,
                                    (int)Impressao.ArrLarguraColunas[iCount], Impressao.AlturaCabecalho));

                                    e.Graphics.DrawRectangle(Pens.Black,
                                        new Rectangle((int)Impressao.ArrColunasRestantes[iCount], Impressao.MargemSuperior,
                                        (int)Impressao.ArrLarguraColunas[iCount], Impressao.AlturaCabecalho));

                                    e.Graphics.DrawString(GridCol.HeaderText,
                                        GridCol.InheritedStyle.Font,
                                        new SolidBrush (GridCol.InheritedStyle.ForeColor),
                                        new RectangleF((int)Impressao.ArrColunasRestantes[iCount], Impressao.MargemSuperior,
                                        (int)Impressao.ArrLarguraColunas[iCount], Impressao.AlturaCabecalho), Impressao.FormatoString);
                                    iCount++;
                                }
                            }
                            Impressao.NovaPagina = false;
                            Impressao.MargemSuperior += Impressao.AlturaCabecalho;
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
                                    new SolidBrush (Cel.InheritedStyle.ForeColor),
                                    new RectangleF((int)Impressao.ArrColunasRestantes[iCount],
                                    (float)Impressao.MargemSuperior,
                                    (int)Impressao.ArrLarguraColunas[iCount], (float)Impressao.AlturaCelula),
                                    Impressao.FormatoString);
                                }

                                //Drawing Cells Borders 
                                e.Graphics.DrawRectangle(Pens.Black,
                                    new Rectangle((int)Impressao.ArrColunasRestantes[iCount], Impressao.MargemSuperior,
                                    (int)Impressao.ArrLarguraColunas[iCount], Impressao.AlturaCelula));
                                iCount++;
                            }
                        }
                    }
                    Impressao.LinhaAtual++;
                    Impressao.MargemSuperior += Impressao.AlturaCelula;
                }
                //If more Impressao.LinhaTemporarias exist, print another page.
                if (Impressao.TemMaisPaginasParaImprimir)
                    e.HasMorePages = true;
                else
                    e.HasMorePages = false;
            }
            catch (Exception)
            {
                ListaErro.RetornaErro(24);
            }
        }
        #endregion

        #endregion

        #region Tarefas
        #region Painel Tarefas
        /// <summary>
        /// Método responsável por atualizar a tabela da tela inicial
        /// </summary>
        /// 
        private void AtualizaDGVTarefas(int linha)
        {
            if (Tarefa.AtualizaDGVTarefas(cmbTipoTarefas.SelectedIndex))
            {
                try
                {
                    dgvTarefas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    dgvTarefas.DataSource = Tarefa.DGVAtualizada.DataSource;

                    Funcoes.FastAutoSizeColumns(dgvTarefas);
                }
                catch (ArgumentOutOfRangeException)
                {
                    //ListaErro.RetornaErro(61);
                    return;
                }

                //desmarca a primeira linha
                dgvTarefas.Rows[0].Selected = false;


                if (Sistema.IniciaTelaTarefas)
                {
                    try
                    {
                        //Esconde a coluna ID
                        dgvTarefas.Columns["ID"].Visible = false;
                        //Esconde a coluna Prioridade
                        dgvTarefas.Columns["Prioridade"].Visible = false;
                        //Esconde a coluna Data Conclusão
                        dgvTarefas.Columns["Data Conclusão"].Visible = false;
                        //Exibe a coluna de Status
                        dgvTarefas.Columns["Status"].Visible = true;
                    }
                    catch (Exception e)
                    {
                        ListaErro.RetornaErro(61);
                        return;
                    }
                    
                    try
                    {
                        //Filtra os resultados da DGV
                        dgvTarefas.Sort(dgvTarefas.Columns[0], ListSortDirection.Descending);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        ListaErro.RetornaErro(49);
                        return;
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
                        ListaErro.RetornaErro(32);
                        return;
                    }

                    Sistema.IniciaTelaTarefas = false;
                }
                else
                {
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
                        ListaErro.RetornaErro(49);
                        return;
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

                    if(dgvTarefas.Columns[0].Visible)
                    {
                        //Esconde a coluna ID
                        dgvTarefas.Columns[0].Visible = false;
                    }
                    if(dgvTarefas.Columns["Prioridade"].Visible)
                    {
                        //Esconde a coluna prioridade
                        dgvTarefas.Columns["Prioridade"].Visible = false;
                    }
                    //Esconde a coluna DataFinal
                    if (cmbTipoTarefas.SelectedIndex == 0)
                    {
                        if (dgvTarefas.Columns["Data Conclusão"].Visible)
                        {
                            dgvTarefas.Columns["Data Conclusão"].Visible = false;
                        }
                        if (!dgvTarefas.Columns["Status"].Visible)
                        {
                            dgvTarefas.Columns["Status"].Visible = true;
                        }
                    }
                    //Esconde a coluna Status
                    else if (cmbTipoTarefas.SelectedIndex == 1)
                    {
                        if (!dgvTarefas.Columns["Data Conclusão"].Visible)
                        {
                            dgvTarefas.Columns["Data Conclusão"].Visible = true;
                        }
                        if (dgvTarefas.Columns["Status"].Visible)
                        {
                            dgvTarefas.Columns["Status"].Visible = false;
                        }
                    }
                    try
                    {
                        //Tenta selecionar a linha
                        dgvFornecedores[0, linha].Selected = true;
                        dgvFornecedores.FirstDisplayedScrollingRowIndex = 0;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        return;
                    }
                }
            }
        }

        private void DgvTarefas_DataSourceChanged(object sender, EventArgs e)
        {
            dgvTarefas.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void CmbTipoTarefas_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizaDGVTarefas(dgvTarefas.CurrentCell.RowIndex);
            dgvTarefas.Focus();
        }

        private void CmbFiltros_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizaDGVTarefas(dgvTarefas.CurrentCell.RowIndex);
            dgvTarefas.Focus();
        }

        private void DgvTarefas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow linha = dgvTarefas.Rows[e.RowIndex];

                Tarefa.Empresa = linha.Cells["Empresa"].Value.ToString();
                Tarefa.Atribuicao = linha.Cells["Atribuido a"].Value.ToString();
                Tarefa.Assunto = linha.Cells["Assunto"].Value.ToString();
                Tarefa.CarregarTarefa();

                if (Tarefa.TravaTarefa())
                {
                    Log.AbrirTarefa(Tarefa.ID);
                    NTCarregaTarefa();

                    panelNT.Visible = true;
                    panelNT.Enabled = true;

                    panelTarefas.Visible = false;
                    panelTarefas.Enabled = false;
                }
                else
                {
                    ListaMensagens.RetornaMensagem(01, MessageBoxIcon.Warning);

                    AtualizaDGVTarefas(dgvTarefas.CurrentCell.RowIndex);
                }
            }
        }

        #region Impressão

        private void BtnImprimirListaTarefas_Click(object sender, EventArgs e)
        {
            ImprimirTarefas();
        }

        private void PdTarefas_BeginPrint(object sender, PrintEventArgs e)
        {
            try
            {
                Impressao.FormatoString = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Center,
                    Trimming = StringTrimming.EllipsisCharacter
                };

                Impressao.ArrColunasRestantes.Clear();
                Impressao.ArrLarguraColunas.Clear();
                Impressao.AlturaCelula = 0;
                Impressao.LinhaAtual = 0;
                Impressao.PrimeiraPagina = true;
                Impressao.NovaPagina = true;

                // Calculating Total Widths
                Impressao.LarguraTotal = 0;
                foreach (DataGridViewColumn dgvGridCol in dgvTarefas.Columns)
                {
                    Impressao.LarguraTotal += dgvGridCol.Width;
                }
            }
            catch (Exception)
            {
                ListaErro.RetornaErro(23);
            }
        }

        private void ImprimirTarefas()
        {
            if (pdImprimirTarefas.ShowDialog() == DialogResult.OK)
            {
                pdTarefas.DefaultPageSettings.Landscape = true;
                pdTarefas.DocumentName = "Tabela de Tarefas - CFTVA " + DateTime.Now.Date.ToShortDateString();

                pPreviewTarefas.ShowDialog();

                AtualizaDGVTarefas(dgvTarefas.CurrentCell.RowIndex);
            }

        }

        private void PdTarefas_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                //Seta a margem esquerda da folha
                Impressao.MargemEsquerda = e.MarginBounds.Left;
                //Seta a margem superior da folha
                Impressao.MargemSuperior = e.MarginBounds.Top;
                Impressao.LarguraTemporaria = 0;

                //Seta a largura da célula e a altura do cabeçalho caso seja a primeira folha
                if (Impressao.PrimeiraPagina)
                {
                    foreach (DataGridViewColumn GridCol in dgvTarefas.Columns)
                    {
                        if (cmbTipoTarefas.SelectedIndex == 0)
                        {
                            if (GridCol.Name != "ID" && GridCol.Name != "Data Conclusão" && GridCol.Name != "prioridade")
                            {
                                Impressao.LarguraTemporaria = (int)(Math.Floor((double)((double)GridCol.Width /
                                (double)Impressao.LarguraTotal * (double)Impressao.LarguraTotal *
                                ((double)e.MarginBounds.Width / (double)Impressao.LarguraTotal))));

                                Impressao.AlturaCabecalho = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
                                    GridCol.InheritedStyle.Font, Impressao.LarguraTemporaria).Height) + 11;

                                // Save width and height of headers
                                Impressao.ArrColunasRestantes.Add(Impressao.MargemEsquerda);
                                Impressao.ArrLarguraColunas.Add(Impressao.LarguraTemporaria);
                                Impressao.MargemEsquerda += Impressao.LarguraTemporaria;
                            }
                        }
                        else if (cmbTipoTarefas.SelectedIndex == 1)
                        {
                            if (GridCol.Name != "ID" && GridCol.Name != "Status" && GridCol.Name != "prioridade")
                            {
                                Impressao.LarguraTemporaria = (int)(Math.Floor((double)((double)GridCol.Width /
                                (double)Impressao.LarguraTotal * (double)Impressao.LarguraTotal *
                                ((double)e.MarginBounds.Width / (double)Impressao.LarguraTotal))));

                                Impressao.AlturaCabecalho = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
                                    GridCol.InheritedStyle.Font, Impressao.LarguraTemporaria).Height) + 11;

                                // Save width and height of headers
                                Impressao.ArrColunasRestantes.Add(Impressao.MargemEsquerda);
                                Impressao.ArrLarguraColunas.Add(Impressao.LarguraTemporaria);
                                Impressao.MargemEsquerda += Impressao.LarguraTemporaria;
                            }
                        }
                        else if (cmbTipoTarefas.SelectedIndex == 2)
                        {
                            if (GridCol.Name != "ID" && GridCol.Name != "prioridade")
                            {
                                Impressao.LarguraTemporaria = (int)(Math.Floor((double)((double)GridCol.Width /
                                    (double)Impressao.LarguraTotal * (double)Impressao.LarguraTotal *
                                    ((double)e.MarginBounds.Width / (double)Impressao.LarguraTotal))));

                                Impressao.AlturaCabecalho = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
                                    GridCol.InheritedStyle.Font, Impressao.LarguraTemporaria).Height) + 11;

                                // Save width and height of headers
                                Impressao.ArrColunasRestantes.Add(Impressao.MargemEsquerda);
                                Impressao.ArrLarguraColunas.Add(Impressao.LarguraTemporaria);
                                Impressao.MargemEsquerda += Impressao.LarguraTemporaria;
                            }
                        }
                    }
                }
                //Loop till all the grid rows not get printed
                while (Impressao.LinhaAtual <= dgvTarefas.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = dgvTarefas.Rows[Impressao.LinhaAtual];
                    //Seta a altura da linha
                    Impressao.AlturaCelula = GridRow.Height + 12;
                    int iCount = 0;
                    //Check whether the current page settings allows more rows to print
                    if (Impressao.MargemSuperior + Impressao.AlturaCelula >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        Impressao.NovaPagina = true;
                        Impressao.PrimeiraPagina = false;
                        Impressao.TemMaisPaginasParaImprimir = true;
                        break;
                    }
                    else
                    {
                        if (Impressao.NovaPagina)
                        {
                            //Draw Header
                            e.Graphics.DrawString("Lista de Tarefas",
                                new Font(dgvTarefas.Font, FontStyle.Bold),
                                Impressao.FonteCorPreta, e.MarginBounds.Left,
                                e.MarginBounds.Top - e.Graphics.MeasureString("Lista de Tarefas",
                                new Font(dgvTarefas.Font, FontStyle.Bold),
                                e.MarginBounds.Width).Height - 13);

                            //Draw Date
                            e.Graphics.DrawString(Sistema.DataHoraImpressao,
                                new Font(dgvTarefas.Font, FontStyle.Bold), Impressao.FonteCorPreta,
                                e.MarginBounds.Left +
                                (e.MarginBounds.Width - e.Graphics.MeasureString(Sistema.DataHoraImpressao,
                                new Font(dgvTarefas.Font, FontStyle.Bold),
                                e.MarginBounds.Width).Width),
                                e.MarginBounds.Top - e.Graphics.MeasureString("Lista de Tarefas",
                                new Font(new Font(dgvTarefas.Font, FontStyle.Bold),
                                FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            //Draw Columns                 
                            Impressao.MargemSuperior = e.MarginBounds.Top;
                            foreach (DataGridViewColumn GridCol in dgvTarefas.Columns)
                            {
                                if (cmbTipoTarefas.SelectedIndex == 0)
                                {
                                    if (GridCol.Name != "ID" && GridCol.Name != "Data Conclusão" && GridCol.Name != "prioridade")
                                    {
                                        e.Graphics.FillRectangle(Impressao.FonteCorCinzaClaro,
                                            new Rectangle((int)Impressao.ArrColunasRestantes[iCount], Impressao.MargemSuperior,
                                            (int)Impressao.ArrLarguraColunas[iCount], Impressao.AlturaCabecalho));

                                        e.Graphics.DrawRectangle(Pens.Black,
                                            new Rectangle((int)Impressao.ArrColunasRestantes[iCount], Impressao.MargemSuperior,
                                            (int)Impressao.ArrLarguraColunas[iCount], Impressao.AlturaCabecalho));

                                        e.Graphics.DrawString(GridCol.HeaderText,
                                            GridCol.InheritedStyle.Font,
                                            new SolidBrush (GridCol.InheritedStyle.ForeColor),
                                            new RectangleF((int)Impressao.ArrColunasRestantes[iCount], Impressao.MargemSuperior,
                                            (int)Impressao.ArrLarguraColunas[iCount], Impressao.AlturaCabecalho), Impressao.FormatoString);
                                        iCount++;
                                    }
                                }
                                else if (cmbTipoTarefas.SelectedIndex == 1)
                                {
                                    if (GridCol.Name != "ID" && GridCol.Name != "Status" && GridCol.Name != "prioridade")
                                    {
                                        e.Graphics.FillRectangle(Impressao.FonteCorCinzaClaro,
                                            new Rectangle((int)Impressao.ArrColunasRestantes[iCount], Impressao.MargemSuperior,
                                            (int)Impressao.ArrLarguraColunas[iCount], Impressao.AlturaCabecalho));

                                        e.Graphics.DrawRectangle(Pens.Black,
                                            new Rectangle((int)Impressao.ArrColunasRestantes[iCount], Impressao.MargemSuperior,
                                            (int)Impressao.ArrLarguraColunas[iCount], Impressao.AlturaCabecalho));

                                        e.Graphics.DrawString(GridCol.HeaderText,
                                            GridCol.InheritedStyle.Font,
                                            new SolidBrush (GridCol.InheritedStyle.ForeColor),
                                            new RectangleF((int)Impressao.ArrColunasRestantes[iCount], Impressao.MargemSuperior,
                                            (int)Impressao.ArrLarguraColunas[iCount], Impressao.AlturaCabecalho), Impressao.FormatoString);
                                        iCount++;
                                    }
                                }
                                else if (cmbTipoTarefas.SelectedIndex == 2)
                                {
                                    if (GridCol.Name != "ID" && GridCol.Name != "prioridade")
                                    {
                                        e.Graphics.FillRectangle(Impressao.FonteCorCinzaClaro,
                                            new Rectangle((int)Impressao.ArrColunasRestantes[iCount], Impressao.MargemSuperior,
                                            (int)Impressao.ArrLarguraColunas[iCount], Impressao.AlturaCabecalho));

                                        e.Graphics.DrawRectangle(Pens.Black,
                                            new Rectangle((int)Impressao.ArrColunasRestantes[iCount], Impressao.MargemSuperior,
                                            (int)Impressao.ArrLarguraColunas[iCount], Impressao.AlturaCabecalho));

                                        e.Graphics.DrawString(GridCol.HeaderText,
                                            GridCol.InheritedStyle.Font,
                                            new SolidBrush (GridCol.InheritedStyle.ForeColor),
                                            new RectangleF((int)Impressao.ArrColunasRestantes[iCount], Impressao.MargemSuperior,
                                            (int)Impressao.ArrLarguraColunas[iCount], Impressao.AlturaCabecalho), Impressao.FormatoString);
                                        iCount++;
                                    }
                                }
                            }
                            Impressao.NovaPagina = false;
                            Impressao.MargemSuperior += Impressao.AlturaCabecalho;
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
                                                new SolidBrush (Cel.InheritedStyle.ForeColor),
                                                new RectangleF((int)Impressao.ArrColunasRestantes[iCount],
                                                (float)Impressao.MargemSuperior,
                                                (int)Impressao.ArrLarguraColunas[iCount], (float)Impressao.AlturaCelula),
                                                Impressao.FormatoString);

                                            //Drawing Cells Borders 
                                            e.Graphics.DrawRectangle(Pens.Black,
                                                new Rectangle((int)Impressao.ArrColunasRestantes[iCount], Impressao.MargemSuperior,
                                                (int)Impressao.ArrLarguraColunas[iCount], Impressao.AlturaCelula));
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
                                            new SolidBrush (Cel.InheritedStyle.ForeColor),
                                            new RectangleF((int)Impressao.ArrColunasRestantes[iCount],
                                            (float)Impressao.MargemSuperior,
                                            (int)Impressao.ArrLarguraColunas[iCount], (float)Impressao.AlturaCelula),
                                            Impressao.FormatoString);

                                            //Drawing Cells Borders 
                                            e.Graphics.DrawRectangle(Pens.Black,
                                                new Rectangle((int)Impressao.ArrColunasRestantes[iCount], Impressao.MargemSuperior,
                                                (int)Impressao.ArrLarguraColunas[iCount], Impressao.AlturaCelula));
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
                                            new SolidBrush (Cel.InheritedStyle.ForeColor),
                                            new RectangleF((int)Impressao.ArrColunasRestantes[iCount],
                                            (float)Impressao.MargemSuperior,
                                            (int)Impressao.ArrLarguraColunas[iCount], (float)Impressao.AlturaCelula),
                                            Impressao.FormatoString);

                                            //Drawing Cells Borders 
                                            e.Graphics.DrawRectangle(Pens.Black,
                                                new Rectangle((int)Impressao.ArrColunasRestantes[iCount], Impressao.MargemSuperior,
                                                (int)Impressao.ArrLarguraColunas[iCount], Impressao.AlturaCelula));
                                            iCount++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    Impressao.LinhaAtual++;
                    Impressao.MargemSuperior += Impressao.AlturaCelula;
                }
                //If more Impressao.LinhaTemporarias exist, print another page.
                if (Impressao.TemMaisPaginasParaImprimir)
                    e.HasMorePages = true;
                else
                    e.HasMorePages = false;
            }
            catch (Exception)
            {
                ListaErro.RetornaErro(24);
            }
        }
        #endregion
        #endregion

        #region Nova Tarefa
        private void BtnNovaTarefa_Click(object sender, EventArgs e)
        {
            Tarefa.LimparVariaveis();
            NTLimpaCampos();

            panelNT.Visible = true;
            panelNT.Enabled = true;

            panelTarefas.Visible = false;
            panelTarefas.Enabled = false;
        }

        private void NTLimpaCampos()
        {
            btnNTAnexar.Enabled = false;
            btnNTApagar.Visible = false;
            btnNTApagar.Enabled = false;
            btnNTCadastrar.Text = "Cadastrar Tarefa";
            btnNTEditar.Visible = false;
            btnNTEditar.Enabled = false;
            btnNTGerarOS.Visible = false;
            btnNTGerarOS.Enabled = false;
            btnNTImprimir.Visible = false;
            btnNTImprimir.Enabled = false;
            btnNTSair.Text = "Cancelar";

            if (cmbNTCliente.DataSource == null)
            {
                cmbNTCliente.DataSource = Tarefa.ListaClientes;
            }
            cmbNTCliente.Text = "";

            if (cmbNTResponsavel.DataSource == null)
            {
                cmbNTResponsavel.DataSource = Tarefa.ListaFuncionarios;
            }
            cmbNTResponsavel.Text = "";

            cmbNTPrioridade.SelectedIndex = 1;
            cmbNTStatus.SelectedIndex = 0;


            dtpNTDataInicial.Text = Sistema.Hoje;
            dtpNTDataFinal.Text = Sistema.Hoje;
            
            lblNTTitulo.Text = "Nova Tarefa";
            lstNTAnexos.Clear();

            mskNTDataCadastro.Text = "";

            rtbNTHistorico.Text = "";
            rtbNTTexto.Text = "";

            txtNTAssunto.Focus();
            txtNTAssunto.Text = "";
            txtNTID.Text = "";
            txtNTNomeArquivo.Text = "";
        }

        private void NTCarregaTarefa()
        {
            btnNTAnexar.Enabled = true;
            btnNTApagar.Visible = true;
            btnNTApagar.Enabled = true;
            btnNTCadastrar.Text = "Nova Tarefa";
            btnNTEditar.Visible = true;
            btnNTEditar.Enabled = true;
            btnNTGerarOS.Visible = true;
            btnNTGerarOS.Enabled = true;
            btnNTImprimir.Visible = true;
            btnNTImprimir.Enabled = true;
            btnNTSair.Text = "Sair";

            if (cmbNTCliente.DataSource == null)
            {
                cmbNTCliente.DataSource = Tarefa.ListaClientes;
            }
            cmbNTCliente.Text = Tarefa.Empresa;

            if (cmbNTResponsavel.DataSource == null)
            {
                cmbNTResponsavel.DataSource = Tarefa.ListaFuncionarios;
            }
            cmbNTResponsavel.Text = Tarefa.Atribuicao;

            cmbNTPrioridade.SelectedIndex = Tarefa.Prioridade;
            cmbNTStatus.SelectedIndex = Tarefa.Status;
            
            dtpNTDataInicial.Text = Tarefa.DataInicial;
            if(Tarefa.Status == 5)
            {
                dtpNTDataFinal.Text = Tarefa.DataFinal;
            }
            else
            {
                dtpNTDataFinal.Text = Tarefa.DataInicial;
            }

            lblNTTitulo.Text = Tarefa.Titulo;

            mskNTDataCadastro.Text = Tarefa.DataCadastro;

            rtbNTHistorico.Text = Tarefa.Texto;
            rtbNTTexto.Focus();

            txtNTID.Text = Tarefa.ID.ToString();
            txtNTAssunto.Text = Tarefa.Assunto;
        }

        #region Botões
        private void btnNTAnexar_Click(object sender, EventArgs e)
        {
            if (ofdAbrirArquivo.ShowDialog() == DialogResult.OK)
            {
                string nomeArquivo = ofdAbrirArquivo.FileName;
                txtNTNomeArquivo.Text = nomeArquivo;

                try
                {   
                    if (Tarefa.AnexarArquivo(nomeArquivo))
                    {
                        ListViewItem item = new ListViewItem();

                        item.SubItems.Add(Path.GetExtension(nomeArquivo).ToUpper());
                        item.SubItems.Add(Path.GetFileNameWithoutExtension(nomeArquivo));

                        lstNTAnexos.Items.Add(item);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void BtnNTApagar_Click(object sender, EventArgs e)
        {
            if (ListaMensagens.RetornaDialogo(04) == DialogResult.Yes)
            {
                if (Sistema.IDUsuarioLogado != 1 && Sistema.IDUsuarioLogado != 2)
                {
                    string resposta = Interaction.InputBox(ListaMensagens.RetornaInputBox(05)[0], ListaMensagens.RetornaInputBox(05)[1], "");

                    if (resposta == Sistema.SenhaADM)
                    {
                        if (Tarefa.ApagarTarefa())
                        {
                            ListaMensagens.RetornaMensagem(14, MessageBoxIcon.Information);
                        }
                        else
                        {
                            ListaErro.RetornaErro(18);
                        }

                        BtnNTSair_Click(btnNTSair, e);
                    }
                }
                else
                {
                    if (ListaMensagens.RetornaDialogo(15) == DialogResult.Yes)
                    {
                        if (Tarefa.ApagarTarefa())
                        {
                            ListaMensagens.RetornaMensagem(14, MessageBoxIcon.Information);
                        }
                        else
                        {
                            ListaErro.RetornaErro(18);
                        }

                        BtnNTSair_Click(btnNTSair, e);
                    }
                }
            }
        }

        private void BtnNTCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                Tarefa.Assunto = txtNTAssunto.Text;
                Tarefa.Texto = rtbNTHistorico.Text;

                if (Tarefa.AvaliaMudancas())
                {
                    if (Tarefa.NovaTarefa)
                    {
                        if (txtNTAssunto.TextLength < 5)
                        {
                            ListaErro.RetornaErro(29);
                            txtNTAssunto.Focus();
                        }
                        else
                        {
                            if (rtbNTHistorico.TextLength < 1)
                            {
                                ListaErro.RetornaErro(28);
                                rtbNTTexto.Focus();
                            }
                            else
                            {
                                Tarefa.Atribuicao = cmbNTResponsavel.SelectedItem.ToString();
                                Tarefa.DataInicial = dtpNTDataInicial.Text;
                                Tarefa.DataFinal = dtpNTDataFinal.Text;
                                Tarefa.Prioridade = cmbNTPrioridade.SelectedIndex;
                                Tarefa.Status = cmbNTStatus.SelectedIndex + 1;
                                Tarefa.Empresa = cmbNTCliente.SelectedItem.ToString();

                                Tarefa.CadastrarTarefa();

                                if (ListaMensagens.RetornaDialogo(28) == DialogResult.Yes)
                                {
                                    Tarefa.DestravaTarefa();
                                    Tarefa.LimparVariaveis();
                                    NTLimpaCampos();
                                }
                                else
                                {
                                    btnNTAnexar.Enabled = true;
                                    btnNTApagar.Visible = true;
                                    btnNTApagar.Enabled = true;
                                    btnNTCadastrar.Text = "Nova Tarefa";
                                    btnNTEditar.Visible = true;
                                    btnNTEditar.Enabled = true;
                                    btnNTGerarOS.Visible = true;
                                    btnNTGerarOS.Enabled = true;
                                    btnNTImprimir.Visible = true;
                                    btnNTImprimir.Enabled = true;
                                    btnNTSair.Text = "Sair";
                                    lblNTTitulo.Text = Tarefa.Titulo;
                                    mskNTDataCadastro.Text = Tarefa.DataCadastro;
                                    txtNTID.Text = Tarefa.ID.ToString();
                                    
                                }
                            }
                        }
                    }
                    else
                    {
                        if (ListaMensagens.RetornaDialogo(03) == DialogResult.Yes)
                        {
                            Tarefa.DestravaTarefa();
                            Tarefa.LimparVariaveis();
                            NTLimpaCampos();
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else
                {
                    if (Tarefa.NovaTarefa)
                    {
                        ListaMensagens.RetornaMensagem(30, MessageBoxIcon.Information);
                        txtNTAssunto.Focus();
                    }
                    else
                    {
                        Tarefa.DestravaTarefa();
                        Tarefa.LimparVariaveis();
                        NTLimpaCampos();
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
        }

        private void BtnNTEditar_Click(object sender, EventArgs e)
        {
            if (txtNTAssunto.TextLength < 1)
            {
                ListaErro.RetornaErro(29);
                txtNTAssunto.Focus();
            }
            else
            {
                if (rtbNTHistorico.TextLength < 5)
                {
                    ListaErro.RetornaErro(28);
                    rtbNTHistorico.Focus();
                }
                else
                {
                    try
                    {
                        Tarefa.Assunto = txtNTAssunto.Text;
                        Tarefa.Atribuicao = cmbNTResponsavel.Text;
                        Tarefa.DataFinal = dtpNTDataFinal.Value.ToString("yyyy-MM-dd");
                        Tarefa.DataInicial = dtpNTDataInicial.Value.ToString("yyyy-MM-dd");
                        Tarefa.Empresa = cmbNTCliente.Text;
                        Tarefa.Prioridade = cmbNTPrioridade.SelectedIndex;
                        Tarefa.Status = cmbNTStatus.SelectedIndex + 1;
                        Tarefa.Texto = rtbNTHistorico.Text;

                        List<string> anexos = new List<string>();
                        foreach(ListViewItem l in lstNTAnexos.Items)
                        {
                            anexos.Add(l.SubItems[2].Text + l.SubItems[1].Text);
                        }

                        if (Tarefa.Anexos != anexos)
                        {
                            Tarefa.Anexos = anexos;
                        }

                        if (Tarefa.AvaliaMudancas())
                        {
                            lblNTTitulo.Text = cmbNTCliente.Text + " - " + txtNTAssunto.Text;

                            Tarefa.Travar = true;

                            Tarefa.AtualizarTarefa();

                            ListaMensagens.RetornaMensagem(29, MessageBoxIcon.Information);
                        }
                    }
                    catch (NullReferenceException)
                    {
                        ListaErro.RetornaErro(64);
                    }
                }
            }
        }

        private void BtnNTInsereTexto_Click(object sender, EventArgs e)
        {
            if (rtbNTTexto.TextLength > 3)
            {
                if(ListaMensagens.RetornaDialogo(27) == DialogResult.Yes)
                {
                    if(rtbNTHistorico.TextLength <= 0)
                    {
                        rtbNTHistorico.Text = Sistema.Hoje + " - " + Sistema.Hora + " - " + Sistema.NomeUsuarioLogado + "\n\n" + rtbNTTexto.Text;
                    }
                    else
                    {
                        rtbNTHistorico.Text = Sistema.Hoje + " - " + Sistema.Hora + " - " + Sistema.NomeUsuarioLogado + "\n\n" + rtbNTTexto.Text +
                            Sistema.Divisoria + rtbNTHistorico.Text;
                    }

                    rtbNTTexto.Clear();
                    rtbNTTexto.Focus();
                }
                else
                {
                    rtbNTTexto.Focus();
                }
            }
            else
            {
                ListaErro.RetornaErro(63);
                rtbNTTexto.Focus();
            }
        }

        private void BtnNTSair_Click(object sender, EventArgs e)
        {
            if (!Tarefa.TarefaApagada)
            {
                Tarefa.Assunto = txtNTAssunto.Text;
                Tarefa.Atribuicao = cmbNTResponsavel.SelectedItem.ToString();
                Tarefa.DataInicial = dtpNTDataInicial.Text;
                Tarefa.DataFinal = dtpNTDataFinal.Text;
                Tarefa.Prioridade = cmbNTPrioridade.SelectedIndex;
                Tarefa.Status = cmbNTStatus.SelectedIndex;
                Tarefa.Texto = rtbNTHistorico.Text;

                if (Tarefa.AvaliaMudancas())
                {
                    if (!Tarefa.NovaTarefa)
                    {
                        if (ListaMensagens.RetornaDialogo(03) == DialogResult.Yes)
                        {


                            Tarefa.DestravaTarefa();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (ListaMensagens.RetornaDialogo(02) == DialogResult.No)
                        {
                            return;
                        }
                    }
                }
                else
                {
                    if (Tarefa.TarefaBloqueada())
                    {
                        Tarefa.DestravaTarefa();
                    }
                }
            }

            panelTarefas.Visible = true;
            panelTarefas.Enabled = true;

            panelNT.Visible = false;
            panelNT.Enabled = false;

            AtualizaDGVTarefas(dgvTarefas.CurrentCell.RowIndex);
        }
        #endregion

        #region ListView - Anexo
        private void lstNTAnexos_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = lstNTAnexos.Columns[e.ColumnIndex].Width;
        }

        private void lstNTAnexos_MouseDown(object sender, MouseEventArgs e)
        {
            var info = lstNTAnexos.HitTest(e.X, e.Y);
            //var row = info.Item.Index;
            //var col = info.Item.SubItems.IndexOf(info.SubItem);

            var value = info.Item.SubItems[2].Text + info.Item.SubItems[1].Text;

            Process.Start(@"\\192.168.254.253\GerenciadorTarefas\Anexos\" + Tarefa.ID + @"\" + value);
        }

        #region Checkbox - coluna 0
        /*
         * OBS: é necessário ativar a opção "Mostrar conteúdo da janela ao arrastar" no windows para funcionar
         * COMO ATIVAR:
         *  1º - Aperte a combinação "Tecla do Windows + R" e digite "sysdm.cpl" (Sem as aspas duplas), depois aperte OK;
         *  2º - Na janela de Propriedades do Sistema, vá para a aba "Avançado" e depois clique no botão "Configurações" do grupo "Desempenho";
         *  3º - Na janela de Opções de Desempenho, ative a opção "Mostrar conteúdo da janela ao arrastar" e clique em OK;
         *  4º - Depois disso só clicar em OK e pronto, irá funcionar :3
        */

        private void lstNTAnexos_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            // Se for a primeira coluna
            if(e.ColumnIndex == 0)
            {
                // Desenha o fundo
                e.DrawBackground();
                //Cria uma variável booleana
                bool value = false;

                try
                {
                    value = Convert.ToBoolean(e.Header.Tag);
                }
                catch (Exception)
                {
                }

                CheckBoxRenderer.DrawCheckBox(e.Graphics, new Point(e.Bounds.Left + 4, e.Bounds.Top + 4),
                    value ? System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal :
                    System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);
            }
            else
            {
                e.DrawDefault = true;
            }
        }

        private void lstNTAnexos_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void lstNTAnexos_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void lstNTAnexos_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Se for a coluna 0
            if (e.Column == 0)
            {
                bool value = false;

                try
                {
                    value = Convert.ToBoolean(this.lstNTAnexos.Columns[e.Column].Tag);
                }
                catch (Exception)
                {
                }

                this.lstNTAnexos.Columns[e.Column].Tag = !value;

                foreach (ListViewItem item in this.lstNTAnexos.Items)
                {
                    item.Checked = !value;
                }

                this.lstNTAnexos.Invalidate();
            }
        }
        #endregion
        #endregion

        #region NT-Impressão
        private void BtnNTImprimir_Click(object sender, EventArgs e)
        {
            if (pdNTConfigImpressao.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pPreviewNT.ShowDialog();
                }
                catch (ArgumentOutOfRangeException)
                {
                    ListaErro.RetornaErro(24);
                }
            }
        }

        private void PdNTDocumento_BeginPrint(object sender, PrintEventArgs e)
        {
            Tarefa.PrimeiraPagina = true;
            Tarefa.TextoImpressao = Funcoes.PreparaTexto(rtbNTHistorico.Text, Impressao.MaximoCaracteres);
            pdNTDocumento.DocumentName = cmbNTCliente.Text + " - " + txtNTAssunto.Text;

        }

        private void PdNTDocumento_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            //Seta a margem esquerda da folha
            Impressao.MargemEsquerda = e.MarginBounds.X;
            //Seta a margem superior da folha
            Impressao.MargemSuperior = e.MarginBounds.Y;

            Impressao.MaximoCaracteres = 0;
            //Calcula a quantidade máxima de caracteres que a linha suporta
            Impressao.MaximoCaracteres = e.MarginBounds.Width / (int)Impressao.FonteTexto.Size;

            
            Impressao.PosicaoTexto = 0;
           Impressao.PosicaoUltimoEspaco = 0;

            //Nome da empresa que será impresso
            string titulo = "Empresa: " + cmbNTCliente.SelectedItem.ToString();
            //Assunto que será usado
            string assunto = "Assunto: " + txtNTAssunto.Text;

            Impressao.Graficos.DrawString(titulo, Impressao.FonteCabecalho, Impressao.FonteCorPreta, Impressao.MargemEsquerda, Impressao.MargemSuperior);
            Impressao.MargemSuperior += Impressao.FonteCabecalho.Height + 20;

            while (assunto.Length - Impressao.PosicaoTexto > Impressao.MaximoCaracteres)
            {
                if (assunto.Substring(Impressao.PosicaoTexto, 1) == " ")
                {
                    Impressao.PosicaoTexto += 1;
                }
                else
                {
                    //Define a posição do ultimo espaço em branco no texto selecionado
                    Impressao.PosicaoUltimoEspaco = assunto.Substring(Impressao.PosicaoTexto, Impressao.PosicaoTexto + Impressao.MaximoCaracteres).LastIndexOf(" ");

                    //Escreve o assunto
                    Impressao.Graficos.DrawString(assunto.Substring(Impressao.PosicaoTexto, Impressao.PosicaoUltimoEspaco), Impressao.FonteCabecalho, Impressao.FonteCorPreta, Impressao.MargemEsquerda, Impressao.MargemSuperior);
                    //Soma na altura atual do texto
                    Impressao.MargemSuperior += Impressao.FonteCabecalho.Height;
                    //Define a nova posição do texto
                    Impressao.PosicaoTexto += Impressao.PosicaoUltimoEspaco;
                }
            }
            if (assunto.Length - Impressao.PosicaoTexto > 0)
            {
                if (assunto.Substring(Impressao.PosicaoTexto, 1) == " ")
                {
                    Impressao.PosicaoTexto += 1;
                }
                Impressao.Graficos.DrawString(assunto.Substring(Impressao.PosicaoTexto), Impressao.FonteCabecalho, Impressao.FonteCorPreta, Impressao.MargemEsquerda, Impressao.MargemSuperior);
                Impressao.MargemSuperior += Impressao.FonteCabecalho.Height;
            }

            //Cria um espaço de 10pixels 
            Impressao.MargemSuperior += 10;
            //Cria a linha
            Impressao.Graficos.DrawLine(Impressao.LinhaPreta, new Point(Impressao.MargemEsquerda, Impressao.MargemSuperior), new Point(e.MarginBounds.Width + 100, Impressao.MargemSuperior));
            //Cria um espaço de 10pixels
            Impressao.MargemSuperior += 10;

            //A quem a tarefa foi atribuida
            string atribuicao = "Atribuido a: " + cmbNTResponsavel.SelectedItem.ToString();
            //Qual o período da tarefa
            string periodo = "Data de Início: " + dtpNTDataInicial.Value.ToShortDateString() + " - Data de Conclusão: " + dtpNTDataFinal.Value.ToShortDateString();
            //Qual o status da tarefa
            string status = "Status: " + cmbNTStatus.SelectedItem.ToString();
            //Qual a prioridade da tarefa
            string prioridade = "Prioridade: " + cmbNTPrioridade.SelectedItem.ToString();

            //Imprime a atribuição com a prioridade
            Impressao.Graficos.DrawString(atribuicao + " | " + prioridade, Impressao.FonteTexto, Impressao.FonteCorPreta, Impressao.MargemEsquerda, Impressao.MargemSuperior);
            Impressao.MargemSuperior += Impressao.FonteTexto.Height;
            //Imprime o período
            Impressao.Graficos.DrawString(periodo, Impressao.FonteTexto, Impressao.FonteCorPreta, Impressao.MargemEsquerda, Impressao.MargemSuperior);
            Impressao.MargemSuperior += Impressao.FonteTexto.Height;
            //Imprime o status da tarefa
            Impressao.Graficos.DrawString(status, Impressao.FonteTexto, Impressao.FonteCorPreta, Impressao.MargemEsquerda, Impressao.MargemSuperior);
            Impressao.MargemSuperior += Impressao.FonteTexto.Height;

            //Cria um espaço de 10pixels 
            Impressao.MargemSuperior += 10;
            //Cria a linha
            Impressao.Graficos.DrawLine(Impressao.LinhaPreta, new Point(Impressao.MargemEsquerda, Impressao.MargemSuperior), new Point(e.MarginBounds.Width + 100, Impressao.MargemSuperior));
            //Cria um espaço de 10pixels
            Impressao.MargemSuperior += 10;


            //Imprime o título "Texto: "
            Impressao.Graficos.DrawString("Texto:", Impressao.FonteCabecalho, Impressao.FonteCorPreta, Impressao.MargemEsquerda, Impressao.MargemSuperior);
            Impressao.MargemSuperior += Impressao.FonteCabecalho.Height * 2;

            Impressao.ContadorLinhas = 0;

            if (Tarefa.PrimeiraPagina)
            {
                Tarefa.PrimeiraPagina = false;
                Tarefa.Leitor = new StringReader(Tarefa.TextoImpressao);
            }

            Impressao.LinhaTemporaria = null;

            while (Impressao.ContadorLinhas < Impressao.MaximoLinhas && ((Impressao.LinhaTemporaria = Tarefa.Leitor.ReadLine()) != null))
            {
                Impressao.MargemSuperior += Impressao.FonteTextoAlternativo.Height;
                e.Graphics.DrawString(Impressao.LinhaTemporaria, Impressao.FonteTextoAlternativo, Impressao.FonteCorPreta, Impressao.MargemEsquerda, Impressao.MargemSuperior, new StringFormat());
                Impressao.ContadorLinhas++;
            }

            if (Impressao.LinhaTemporaria != null)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }

            Impressao.FonteCorPreta.Dispose();
        }
        #endregion
        #endregion

        #endregion

        #region Fornecedores

        /// <summary>
        /// Método responsável por atualizar a tela de Fornecedores
        /// </summary>
        /// 
        private void AtualizaDGVFornecedores(int linha)
        {
            if (Fornecedor.AtualizaDGVFornecedores(cmbGrupoFornecedor.SelectedIndex))
            {
                dgvFornecedores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dgvFornecedores.DataSource = Fornecedor.DGVAtualizada.DataSource;

                Funcoes.FastAutoSizeColumns(dgvFornecedores);

                //Desmarca a primeira linha
                dgvFornecedores.Rows[0].Selected = false;

                //Tenta selecionar a linha
                try
                {
                    dgvFornecedores[0, linha].Selected = true;
                    dgvFornecedores.FirstDisplayedScrollingRowIndex = 0;
                }
                catch (ArgumentOutOfRangeException)
                {
                    dgvFornecedores[0, 0].Selected = true;
                    dgvFornecedores.FirstDisplayedScrollingRowIndex = 0;
                }

                try
                {
                    foreach (DataGridViewRow dgvr in dgvFornecedores.Rows)
                    {
                        if (dgvr.Cells["Telefone"].Value.ToString() != "")
                        {
                            if (dgvr.Cells["Telefone"].Value.ToString().Length > 7 && dgvr.Cells["Telefone"].Value.ToString().Length < 11)
                            {
                                dgvr.Cells["Telefone"].Value = "(" + dgvr.Cells["Telefone"].Value.ToString().Substring(0, 2) + ") " +
                                    dgvr.Cells["Telefone"].Value.ToString().Substring(2, 4) + "-" +
                                    dgvr.Cells["Telefone"].Value.ToString().Substring(6, 4);
                            }
                            else if (dgvr.Cells["Telefone"].Value.ToString().Length > 8 && dgvr.Cells["Telefone"].Value.ToString().Length < 12)
                            {
                                dgvr.Cells["Telefone"].Value = "(" + dgvr.Cells["Telefone"].Value.ToString().Substring(0, 2) + ") " +
                                    dgvr.Cells["Telefone"].Value.ToString().Substring(2, 5) + "-" +
                                    dgvr.Cells["Telefone"].Value.ToString().Substring(7, 4);
                            }
                        }
                    }
                }
                catch
                {

                }
            }
        }

        private void DgvFornecedores_DataSourceChanged(object sender, EventArgs e)
        {
            dgvFornecedores.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvFornecedores.Columns[2].MinimumWidth = 150;
        }

        private void DgvFornecedores_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                NfLimparCampos();

                NfCarregaFornecedor(int.Parse(dgvFornecedores.Rows[e.RowIndex].Cells["id"].Value.ToString()));
            }
        }

        private void BtnPesquisaFornecedor_Click(object sender, EventArgs e)
        {
            TelaPesquisa telaPesquisaFornecedor = new TelaPesquisa(dgvFornecedores, false);
            telaPesquisaFornecedor.ShowDialog();

            if (Fornecedor.FornecedorPesquisado != -1)
            {
                dgvFornecedores[1, Fornecedor.FornecedorPesquisado].Selected = true;
            }
            else
            {
                ListaErro.RetornaErro(31);
            }

            dgvFornecedores.Focus();
        }

        #region Impressão

        private void BtnImprimeFornecedores_Click(object sender, EventArgs e)
        {
            if (pdImprimirClientes.ShowDialog() == DialogResult.OK)
            {
                pdClientes.DefaultPageSettings.Landscape = true;
                pdClientes.DocumentName = "Lista de Fornecedores - CFTVA " + DateTime.Now.Date.ToShortDateString();

                pPreviewClientes.ShowDialog();
                AtualizaDGVClientes(dgvClientes.CurrentCell.RowIndex);
            }
        }

        private void PdFornecedores_BeginPrint(object sender, PrintEventArgs e)
        {
            try
            {
                Impressao.FormatoString = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Center,
                    Trimming = StringTrimming.EllipsisCharacter
                };

                Impressao.ArrColunasRestantes.Clear();
                Impressao.ArrLarguraColunas.Clear();
                Impressao.AlturaCelula = 0;
                Impressao.LinhaAtual = 0;
                Impressao.PrimeiraPagina = true;
                Impressao.NovaPagina = true;

                // Calculating Total Widths
                Impressao.LarguraTotal = 0;
                foreach (DataGridViewColumn dgvGridCol in dgvFornecedores.Columns)
                {
                    Impressao.LarguraTotal += dgvGridCol.Width;
                }
            }
            catch (Exception)
            {
                ListaErro.RetornaErro(23);
            }
        }

        private void PdFornecedores_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                //Seta a margem esquerda da folha
                Impressao.MargemEsquerda = e.MarginBounds.Left;
                //Seta a margem superior da folha
                Impressao.MargemSuperior = e.MarginBounds.Top;
                //Verifica se há mais páginas para imprimir
                Impressao.TemMaisPaginasParaImprimir = false;
                Impressao.LarguraTemporaria = 0;

                //Seta a largura da célula e a altura do cabeçalho caso seja a primeira folha
                if (Impressao.PrimeiraPagina)
                {
                    foreach (DataGridViewColumn GridCol in dgvFornecedores.Columns)
                    {
                        Impressao.LarguraTemporaria = (int)(Math.Floor((double)((double)GridCol.Width /
                            (double)Impressao.LarguraTotal * (double)Impressao.LarguraTotal *
                            ((double)e.MarginBounds.Width / (double)Impressao.LarguraTotal))));

                        Impressao.AlturaCabecalho = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
                            GridCol.InheritedStyle.Font, Impressao.LarguraTemporaria).Height) + 11;

                        // Save width and height of headers
                        Impressao.ArrColunasRestantes.Add(Impressao.MargemEsquerda);
                        Impressao.ArrLarguraColunas.Add(Impressao.LarguraTemporaria);
                        Impressao.MargemEsquerda += Impressao.LarguraTemporaria;
                    }
                }
                //Loop till all the grid rows not get printed
                while (Impressao.LinhaAtual <= dgvFornecedores.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = dgvFornecedores.Rows[Impressao.LinhaAtual];
                    //Set the cell height
                    Impressao.AlturaCelula = GridRow.Height + 12;
                    int iCount = 0;
                    //Check whether the current page settings allows more rows to print
                    if (Impressao.MargemSuperior + Impressao.AlturaCelula >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        Impressao.NovaPagina = true;
                        Impressao.PrimeiraPagina = false;
                        Impressao.TemMaisPaginasParaImprimir = true;
                        break;
                    }
                    else
                    {
                        if (Impressao.NovaPagina)
                        {
                            //Draw Header
                            e.Graphics.DrawString("Lista de Fornecedores",
                                new Font(dgvClientes.Font, FontStyle.Bold),
                                Impressao.FonteCorPreta, e.MarginBounds.Left,
                                e.MarginBounds.Top - e.Graphics.MeasureString("Lista de Fornecedores",
                                new Font(dgvClientes.Font, FontStyle.Bold),
                                e.MarginBounds.Width).Height - 13);
 
                            //Draw Date
                            e.Graphics.DrawString(Sistema.DataHoraImpressao,
                                new Font(dgvClientes.Font, FontStyle.Bold), Impressao.FonteCorPreta,
                                e.MarginBounds.Left +
                                (e.MarginBounds.Width - e.Graphics.MeasureString(Sistema.DataHoraImpressao,
                                new Font(dgvClientes.Font, FontStyle.Bold),
                                e.MarginBounds.Width).Width),
                                e.MarginBounds.Top - e.Graphics.MeasureString("Lista de Fornecedores",
                                new Font(new Font(dgvClientes.Font, FontStyle.Bold),
                                FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            //Draw Columns                 
                            Impressao.MargemSuperior = e.MarginBounds.Top;
                            foreach (DataGridViewColumn GridCol in dgvClientes.Columns)
                            {
                                e.Graphics.FillRectangle(Impressao.FonteCorCinzaClaro,
                                    new Rectangle((int)Impressao.ArrColunasRestantes[iCount], Impressao.MargemSuperior,
                                    (int)Impressao.ArrLarguraColunas[iCount], Impressao.AlturaCabecalho));

                                e.Graphics.DrawRectangle(Pens.Black,
                                    new Rectangle((int)Impressao.ArrColunasRestantes[iCount], Impressao.MargemSuperior,
                                    (int)Impressao.ArrLarguraColunas[iCount], Impressao.AlturaCabecalho));

                                e.Graphics.DrawString(GridCol.HeaderText,
                                    GridCol.InheritedStyle.Font,
                                    new SolidBrush (GridCol.InheritedStyle.ForeColor),
                                    new RectangleF((int)Impressao.ArrColunasRestantes[iCount], Impressao.MargemSuperior,
                                    (int)Impressao.ArrLarguraColunas[iCount], Impressao.AlturaCabecalho), Impressao.FormatoString);
                                iCount++;
                            }
                            Impressao.NovaPagina = false;
                            Impressao.MargemSuperior += Impressao.AlturaCabecalho;
                        }
                        iCount = 0;
                        //Draw Columns Contents                
                        foreach (DataGridViewCell Cel in GridRow.Cells)
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
                                new SolidBrush (Cel.InheritedStyle.ForeColor),
                                new RectangleF((int)Impressao.ArrColunasRestantes[iCount],
                                (float)Impressao.MargemSuperior,
                                (int)Impressao.ArrLarguraColunas[iCount], (float)Impressao.AlturaCelula),
                                Impressao.FormatoString);

                                //Drawing Cells Borders 
                                e.Graphics.DrawRectangle(Pens.Black,
                                    new Rectangle((int)Impressao.ArrColunasRestantes[iCount], Impressao.MargemSuperior,
                                    (int)Impressao.ArrLarguraColunas[iCount], Impressao.AlturaCelula));
                                iCount++;
                            }
                        }
                    }
                    Impressao.LinhaAtual++;
                    Impressao.MargemSuperior += Impressao.AlturaCelula;
                }
                //If more Impressao.LinhaTemporarias exist, print another page.
                if (Impressao.TemMaisPaginasParaImprimir)
                    e.HasMorePages = true;
                else
                    e.HasMorePages = false;
            }
            catch (Exception)
            {
                ListaErro.RetornaErro(24);
            }
        }
        #endregion
        #region Novo Fornecedor
        private void BtnNovoFornecedor_Click(object sender, EventArgs e)
        {
            Fornecedor.LimparVariaveis();

            txtNFDataCadastro.Text = DateTime.Today.ToShortDateString();

            panelNF.Visible = true;
            panelNF.Enabled = true;

            panelFornecedores.Visible = false;
            panelFornecedores.Enabled = false;
        }

        private void NfCarregaFornecedor(int idFornecedor)
        {
            Fornecedor.ID = idFornecedor;

            if (Fornecedor.FornecedorBloqueado())
            {
                ListaErro.RetornaErro(54);
            }
            else
            {
                Fornecedor.AbrirFornecedor();

                txtNFIDFornecedor.Text = Fornecedor.ID.ToString();
                cmbNFTipoFornecedor.SelectedIndex = Fornecedor.Tipo;
                txtNFDataCadastro.Text = Fornecedor.DataCadastro;
                txtNFDataNascimento.Text = Fornecedor.DataNascimento;
                txtNFDocumento.Text = Fornecedor.Documento;
                lblNFTitulo.Text = Fornecedor.Nome;
                txtNFNome.Text = Fornecedor.Nome;
                txtNFApelido.Text = Fornecedor.Apelido;
                txtNFCEP.Text = Fornecedor.CEP;
                txtNFEndereco.Text = Fornecedor.Endereco;
                txtNFNumero.Text = Fornecedor.Numero;
                txtNFComplemento.Text = Fornecedor.Complemento;
                txtNFBairro.Text = Fornecedor.Bairro;
                txtNFCidade.Text = Fornecedor.Cidade;
                txtNFEstado.Text = Fornecedor.Estado;
                txtNFPais.Text = Fornecedor.Pais;
                txtNFTelefone.Text = Fornecedor.Telefone;
                txtNFContato.Text = Fornecedor.Contato;
                txtNFTelefoneComercial.Text = Fornecedor.TelefoneComercial;
                txtNFContatoComercial.Text = Fornecedor.ContatoComercial;
                txtNFCelular.Text = Fornecedor.Celular;
                txtNFContatoCelular.Text = Fornecedor.ContatoCelular;
                txtNFEmail.Text = Fornecedor.Email;
                txtNFSite.Text = Fornecedor.Site;
                txtNFInscricaoEstadual.Text = Fornecedor.InscricaoEstadual;
                txtNFInscricaoMunicipal.Text = Fornecedor.InscricaoMunicipal;
                txtNFObservacoes.Text = Fornecedor.Obs;

                btnNFApagar.Enabled = true;
                btnNFApagar.Visible = true;
                btnNFEditar.Enabled = true;
                btnNFEditar.Visible = true;
                btnNFImprimir.Enabled = true;
                btnNFImprimir.Visible = true;
                btnNFNovoCadastro.Text = "Novo Fornecedor";
                btnNFFechar.Text = "Fechar";

                panelNF.Visible = true;
                panelNF.Enabled = true;

                panelFornecedores.Visible = false;
                panelFornecedores.Enabled = false;

                Fornecedor.TravaFornecedor();
            }
        }

        private void NfLocalizarCEP()
        {
            try
            {
                if (txtNFCEP.Text.Length > 0 && txtNFCEP.Text.Length < 8)
                {
                    ListaErro.RetornaErro(44);

                    txtNFCEP.Clear();
                    txtNFCEP.Focus();
                }
                else
                {
                    if (Fornecedor.LocalizarCEP(txtNFCEP.Text))
                    {
                        txtNFEndereco.Text = Fornecedor.Endereco;
                        txtNFNumero.Text = Fornecedor.Numero;
                        txtNFComplemento.Text = Fornecedor.Complemento;
                        txtNFBairro.Text = Fornecedor.Bairro;
                        txtNFCidade.Text = Fornecedor.Cidade;
                        txtNFEstado.Text = Fornecedor.Estado;
                        txtNFPais.Text = Fornecedor.Pais;
                    }
                    else
                    {
                        if (ListaMensagens.RetornaDialogo(25) == DialogResult.Yes)
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
            }
            catch (KeyNotFoundException)
            {
                ListaErro.RetornaErro(46);

                txtNFCEP.Clear();
                txtNFCEP.Focus();
            }
            finally
            {
                txtNFNumero.Focus();
            }
        }

        private void NfPesquisaCNPJ()
        {
            try
            {
                if (txtNFDocumento.Text.Length >= 0 && txtNFDocumento.Text.Length < 14)
                {
                    ListaErro.RetornaErro(40);
                    txtNFDocumento.Clear();
                    txtNFDocumento.Focus();
                }
                else if (txtNFDocumento.Text.Length == 14 && !Funcoes.ValidaCNPJ(txtNFDocumento.Text))
                {
                    ListaErro.RetornaErro(41);
                    txtNFDocumento.Clear();
                    txtNFDocumento.Focus();
                }
                else
                {
                    if (Fornecedor.PesquisarCNPJ(txtNFDocumento.Text))
                    {
                        txtNFNome.Text = Fornecedor.Nome;
                        txtNFApelido.Text = Fornecedor.Apelido;
                        txtNFDataNascimento.Text = Fornecedor.DataNascimento;
                        txtNFCEP.Text = Fornecedor.CEP;
                        txtNFEndereco.Text = Fornecedor.Endereco;
                        txtNFNumero.Text = Fornecedor.Numero;
                        txtNFComplemento.Text = Fornecedor.Endereco;
                        txtNFBairro.Text = Fornecedor.Bairro;
                        txtNFCidade.Text = Fornecedor.Cidade;
                        txtNFEstado.Text = Fornecedor.Estado;
                        txtNFPais.Text = "Brasil";
                        txtNFTelefoneComercial.Text = Fornecedor.TelefoneComercial;
                        txtNFEmail.Text = Fornecedor.Email;

                    }
                    else
                    {
                        ListaErro.RetornaErro(41);
                        txtNFDocumento.Clear();
                        txtNFDocumento.Focus();
                    }
                }
            }
            catch (Exception)
            {
                ListaErro.RetornaErro(41);
                txtNFDocumento.Clear();
                txtNFDocumento.Focus();
            }
        }

        private void NfLimparCampos()
        {
            Fornecedor.LimparVariaveis();

            cmbNFTipoFornecedor.SelectedIndex = 0;
            cmbNFTipoFornecedor.Text = "Pessoa Juridica";
            txtNFDocumento.Clear();
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
            btnNFApagar.Visible = false;
            btnNFEditar.Enabled = false;
            btnNFEditar.Visible = false;
            btnNFImprimir.Enabled = false;
            btnNFImprimir.Visible = false;
            btnNFNovoCadastro.Text = "Cadastrar Fornecedor";
            btnNFFechar.Text = "Cancelar";

            lblNFTitulo.Text = "Novo Fornecedor";
        }
        
        private void NfEnviaDados()
        {
            Fornecedor.Tipo = cmbNFTipoFornecedor.SelectedIndex;
            Fornecedor.DataCadastro = txtNFDataCadastro.Text;
            Fornecedor.DataNascimento = txtNFDataNascimento.Text;
            Fornecedor.Documento = txtNFDocumento.Text;
            Fornecedor.Nome = txtNFNome.Text;
            Fornecedor.Apelido = txtNFApelido.Text;
            Fornecedor.CEP = txtNFCEP.Text;
            Fornecedor.Endereco = txtNFEndereco.Text;
            Fornecedor.Numero = txtNFNumero.Text;
            Fornecedor.Complemento = txtNFComplemento.Text;
            Fornecedor.Bairro = txtNFBairro.Text;
            Fornecedor.Cidade = txtNFCidade.Text;
            Fornecedor.Estado = txtNFEstado.Text;
            Fornecedor.Pais = txtNFPais.Text;
            Fornecedor.Telefone = txtNFTelefone.Text;
            Fornecedor.Contato = txtNFContato.Text;
            Fornecedor.TelefoneComercial = txtNFTelefoneComercial.Text;
            Fornecedor.ContatoComercial = txtNFContatoComercial.Text;
            Fornecedor.Celular = txtNFCelular.Text;
            Fornecedor.ContatoCelular = txtNFContatoCelular.Text;
            Fornecedor.Email = txtNFEmail.Text;
            Fornecedor.Site = txtNFSite.Text;
            Fornecedor.InscricaoEstadual = txtNFInscricaoEstadual.Text;
            Fornecedor.InscricaoMunicipal = txtNFInscricaoMunicipal.Text;
            Fornecedor.Obs = txtNFObservacoes.Text;
        }

        private void BtnNFFechar_Click(object sender, EventArgs e)
        {
            //Envia os dados dos campos da tela para a classe Fornecedor
            NfEnviaDados();

            //Se não tiver mudanças
            if (Fornecedor.AvaliarMudancas()) 
            {
                if (btnNFFechar.Text == "Cancelar")
                {
                    if (ListaMensagens.RetornaDialogo(06) == DialogResult.Yes)
                    {
                        NfLimparCampos();

                        panelNF.Visible = false;
                        panelNF.Enabled = false;

                        AtualizaDGVClientes(dgvClientes.CurrentCell.RowIndex);

                        panelFornecedores.Enabled = true;
                        panelFornecedores.Visible = true;
                    }
                }
                else
                {
                    if (ListaMensagens.RetornaDialogo(18) == DialogResult.Yes)
                    {
                        if (Fornecedor.ID != 0)
                        {
                            Fornecedor.DestravaFornecedor();
                        }

                        NfLimparCampos();

                        panelNF.Visible = false;
                        panelNF.Enabled = false;

                        AtualizaDGVClientes(dgvClientes.CurrentCell.RowIndex);

                        panelFornecedores.Enabled = true;
                        panelFornecedores.Visible = true;
                    }
                }
            }
            else
            {
                if (btnNFFechar.Text != "Cancelar")
                {
                    if (Fornecedor.ID != 0)
                    {
                        Fornecedor.DestravaFornecedor();
                    }
                }

                NfLimparCampos();

                panelNF.Visible = false;
                panelNF.Enabled = false;
                panelFornecedores.Enabled = true;
                panelFornecedores.Visible = true;

                AtualizaDGVFornecedores(dgvFornecedores.CurrentCell.RowIndex);
            }   
        }

        private void BtnNFBuscarEndereco_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNFCEP.Text))
            {
                NfLocalizarCEP();
            }
        }

        private void BtnNFBuscarDados_Click(object sender, EventArgs e)
        {
            NfPesquisaCNPJ();
        }

        private void BtnNFApagar_Click(object sender, EventArgs e)
        {
            if (ListaMensagens.RetornaDialogo(19) == DialogResult.Yes)
            {
                if (Sistema.IDUsuarioLogado != 1 && Sistema.IDUsuarioLogado != 2)
                {
                    string resposta = Interaction.InputBox(ListaMensagens.RetornaInputBox(20)[0], ListaMensagens.RetornaInputBox(20)[1], "");

                    if (resposta == Sistema.SenhaADM)
                    {
                        try
                        {
                            Fornecedor.ApagarFornecedor(int.Parse(txtNFIDFornecedor.Text));
                        }
                        catch (Exception)
                        {
                            ListaErro.RetornaErro(57);
                            return;
                        }
                        finally
                        {
                            ListaMensagens.RetornaMensagem(21, MessageBoxIcon.Information);

                            NfLimparCampos();

                            panelNF.Visible = false;
                            panelNF.Enabled = false;

                            AtualizaDGVClientes(dgvClientes.CurrentCell.RowIndex);

                            panelFornecedores.Enabled = true;
                        }
                    }
                }
                else
                {
                    if (ListaMensagens.RetornaDialogo(22) == DialogResult.Yes)
                    {
                        try
                        {
                            Fornecedor.ApagarFornecedor(int.Parse(txtNFIDFornecedor.Text));
                        }
                        catch (Exception)
                        {
                            ListaErro.RetornaErro(57);
                            return;
                        }
                        finally
                        {
                            ListaMensagens.RetornaMensagem(21, MessageBoxIcon.Information);

                            NfLimparCampos();

                            panelNF.Visible = false;
                            panelNF.Enabled = false;

                            AtualizaDGVClientes(dgvClientes.CurrentCell.RowIndex);

                            panelFornecedores.Enabled = true;
                        }
                    }
                }
            }
        }

        private void BtnNFEditar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNFNome.Text))
            {
                if (txtNFNome.Text.Length > 3)
                {
                    txtNFDataNascimento.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

                    //Envia os dados dos campos da tela para a classe Fornecedor
                    NfEnviaDados();

                    if (Fornecedor.AvaliarMudancas())
                    {
                        try
                        {
                            Fornecedor.AtualizarFornecedor();
                        }
                        catch (Exception)
                        {
                            ListaErro.RetornaErro(56);
                            return;
                        }
                        finally
                        {
                            ListaMensagens.RetornaMensagem(17, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        ListaMensagens.RetornaMensagem(23, MessageBoxIcon.Information);
                    }

                    txtNFDataNascimento.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                }
                else
                {
                    if (cmbNFTipoFornecedor.SelectedIndex == 0)
                    {
                        ListaErro.RetornaErro(36);
                    }
                    else
                    {
                        ListaErro.RetornaErro(50);
                    }
                }
            }
            else
            {
                ListaErro.RetornaErro(33);
            }
        }

        private void BtnNFNovoCadastro_Click(object sender, EventArgs e)
        {
            if (btnNFNovoCadastro.Text == "Cadastrar Fornecedor")
            {
                if (!string.IsNullOrEmpty(txtNFNome.Text))
                {
                    if (txtNFNome.Text.Length > 3)
                    {
                        txtNFDataNascimento.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

                        //Envia os dados dos campos da tela para a classe Fornecedor
                        NfEnviaDados();

                        txtNFDataNascimento.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;

                        Fornecedor.CadastrarFornecedor();

                        if (Fornecedor.ID != 0)
                        {
                            if (ListaMensagens.RetornaDialogo(16) == DialogResult.Yes)
                            {
                                //Destrava o fornecedor cadastrado
                                Fornecedor.DestravaFornecedor();

                                NfLimparCampos();
                            }
                            else
                            {
                                txtNFIDFornecedor.Text = Fornecedor.ID.ToString();

                                btnNFNovoCadastro.Text = "Novo Cadastro";
                                btnNFImprimir.Enabled = true;
                                btnNFImprimir.Visible = true;
                                btnNFEditar.Enabled = true;
                                btnNFEditar.Visible = true;
                                btnNFApagar.Enabled = true;
                                btnNFApagar.Visible = true;
                                btnNFFechar.Text = "Fechar";
                            }
                        }
                    }
                    else
                    {
                        if (cmbNFTipoFornecedor.SelectedIndex == 0)
                        {
                            ListaErro.RetornaErro(36);
                        }
                        else
                        {
                            ListaErro.RetornaErro(50);
                        }
                    }
                }
                else
                {
                    if (cmbNFTipoFornecedor.SelectedIndex == 0)
                    {
                        ListaErro.RetornaErro(33);
                    }
                    else
                    {
                        ListaErro.RetornaErro(35);
                    }
                }
            }
            else
            {
                //Envia os dados dos campos da tela para a classe Fornecedor
                NfEnviaDados();

                if (Fornecedor.AvaliarMudancas())
                {
                    if (ListaMensagens.RetornaDialogo(18) == DialogResult.Yes)
                    {
                        NfLimparCampos();

                        btnNFImprimir.Enabled = false;
                        btnNFImprimir.Visible = false;
                        btnNFEditar.Enabled = false;
                        btnNFEditar.Visible = false;
                        btnNFApagar.Enabled = false;
                        btnNFApagar.Visible = false;
                    }
                }
                else
                {
                    NfLimparCampos();

                    btnNFImprimir.Enabled = false;
                    btnNFImprimir.Visible = false;
                    btnNFEditar.Enabled = false;
                    btnNFEditar.Visible = false;
                    btnNFApagar.Enabled = false;
                    btnNFApagar.Visible = false;
                }
            }
        }

        private void CmbTipoFornecedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Se escolher pessoa física
            if (cmbNFTipoFornecedor.SelectedIndex == 1)
            {
                grpNFCadastroJuridico.Visible = false;

                btnNFBuscarDados.Enabled = false;
                btnNFBuscarDados.Visible = false;

                lblNomeFornecedor.Text = "Nome:";
                lblNFApelido.Text = "Apelido:";
                lblNFDataNascimento.Text = "Data de Nascimento:";
                lblNFCPFCNPJ.Text = "CPF:";

                //Muda a mascara para o padrão CPF
                txtNFDocumento.Mask = "000,000,000-00";
            }
            else
            {
                grpNFCadastroJuridico.Visible = true;

                btnNFBuscarDados.Enabled = true;
                btnNFBuscarDados.Visible = true;

                lblNomeFornecedor.Text = "Razão Social:";
                lblNFApelido.Text = "Nome Fantasia:";
                lblNFDataNascimento.Text = "Fundação:";

                lblNFCPFCNPJ.Text = "CNPJ:";
                //Muda a mascara para o padrão CPF
                txtNFDocumento.Mask = "00,000,000/0000-00";
            }
        }

        /*
        private void cmbNFCateg1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!iniciaTelaNovoFornecedor)
            {
                cmbNFSubCateg1.Enabled = true;
                //cmbNFSubCateg1.DataSource = Fornecedor.CarregarSubCategoria(cmbNFCateg1.SelectedIndex + 1);
            }

        }

        private void cmbNFSubCateg1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!iniciaTelaNovoFornecedor)
            {
                cmbNFCateg2.Enabled = true;
            }
        }

        private void cmbNFCateg2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!iniciaTelaNovoFornecedor)
            {
                cmbNFSubCateg2.Enabled = true;
            }
        }

        private void cmbNFSubCateg2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!iniciaTelaNovoFornecedor)
            {
                cmbNFCateg3.Enabled = true;
            }
        }

        private void cmbNFCateg3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!iniciaTelaNovoFornecedor)
            {
                cmbNFSubCateg3.Enabled = true;
            }
        }*/

        private void BtnNFImprimir_Click(object sender, EventArgs e)
        {
            if (pdNFConfigImpressao.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pPreviewNF.ShowDialog();
                }
                catch (ArgumentOutOfRangeException)
                {
                    ListaErro.RetornaErro(24);
                }
                catch (FormatException)
                {
                    ListaErro.RetornaErro(58);
                }
            }
        }

        private void PdNFDocumento_BeginPrint(object sender, PrintEventArgs e)
        {
            Fornecedor.PrimeiraPagina = true;
            Fornecedor.TextoImpressao = "";
            Fornecedor.TextoImpressao = Funcoes.PreparaTexto(txtNFObservacoes.Text, Impressao.MaximoCaracteres);
            pdNFDocumento.DocumentName = txtNFNome.Text;
        }

        private void PdNFDocumento_PrintPage(object sender, PrintPageEventArgs e)
        {
            Impressao.Graficos = e.Graphics;

            //Seta a margem esquerda da folha
            Impressao.MargemEsquerda = e.MarginBounds.X;
            //Seta a margem superior da folha
            Impressao.MargemSuperior = e.MarginBounds.Y;

            //Zera a quantidade máxima de caracteres
            Impressao.MaximoCaracteres = 0;
            //Calcula a quantidade máxima de caracteres que a linha suporta
            Impressao.MaximoCaracteres = e.MarginBounds.Width / (int)Impressao.FonteTexto.Size;

            
            Impressao.PosicaoTexto = 0;
            
            Impressao.PosicaoUltimoEspaco = 0;

            List<string> dados = Fornecedor.DadosImpressao();
                        
            //Escreve o ID do fornecedor
            Impressao.Graficos.DrawString(dados[0], Impressao.FonteCabecalho, Impressao.FonteCorPreta, Impressao.MargemEsquerda, Impressao.MargemSuperior);
            //Soma na altura atual do texto
            Impressao.MargemSuperior += Impressao.FonteCabecalho.Height;

            //Nome
            while (dados[1].Length - Impressao.PosicaoTexto > Impressao.MaximoCaracteres)
            {
                if (dados[1].Substring(Impressao.PosicaoTexto, 1) == " ")
                {
                    Impressao.PosicaoTexto += 1;
                }
                else
                {
                    //Define a posição do ultimo espaço em branco no texto selecionado
                    Impressao.PosicaoUltimoEspaco = dados[1].Substring(Impressao.PosicaoTexto, Impressao.PosicaoTexto + Impressao.MaximoCaracteres).LastIndexOf(" ");

                    //Escreve o nome
                    Impressao.Graficos.DrawString(dados[1].Substring(Impressao.PosicaoTexto, Impressao.PosicaoUltimoEspaco), Impressao.FonteCabecalho, Impressao.FonteCorPreta, Impressao.MargemEsquerda, Impressao.MargemSuperior);
                    //Soma na altura atual do texto
                    Impressao.MargemSuperior += Impressao.FonteCabecalho.Height;
                    //Define a nova posição do texto
                    Impressao.PosicaoTexto += Impressao.PosicaoUltimoEspaco;
                }
            }
            if (dados[1].Length - Impressao.PosicaoTexto > 0)
            {
                if (dados[1].Substring(Impressao.PosicaoTexto, 1) == " ")
                {
                    Impressao.PosicaoTexto += 1;
                }
                Impressao.Graficos.DrawString(dados[1].Substring(Impressao.PosicaoTexto), Impressao.FonteCabecalho, Impressao.FonteCorPreta, Impressao.MargemEsquerda, Impressao.MargemSuperior);
                Impressao.MargemSuperior += Impressao.FonteCabecalho.Height;
                Impressao.PosicaoTexto = 0;
            }

            //Apelido
            while (dados[2].Length - Impressao.PosicaoTexto > Impressao.MaximoCaracteres)
            {
                if (dados[2].Substring(Impressao.PosicaoTexto, 1) == " ")
                {
                    Impressao.PosicaoTexto += 1;
                }
                else
                {
                    //Define a posição do ultimo espaço em branco no texto selecionado
                    Impressao.PosicaoUltimoEspaco = dados[2].Substring(Impressao.PosicaoTexto, Impressao.PosicaoTexto + Impressao.MaximoCaracteres).LastIndexOf(" ");

                    //Escreve o apelido
                    Impressao.Graficos.DrawString(dados[2].Substring(Impressao.PosicaoTexto, Impressao.PosicaoUltimoEspaco), Impressao.FonteCabecalho, Impressao.FonteCorPreta, Impressao.MargemEsquerda, Impressao.MargemSuperior);
                    //Soma na altura atual do texto
                    Impressao.MargemSuperior += Impressao.FonteCabecalho.Height;
                    //Define a nova posição do texto
                    Impressao.PosicaoTexto += Impressao.PosicaoUltimoEspaco;
                }
            }
            if (dados[2].Length - Impressao.PosicaoTexto > 0)
            {
                if (dados[2].Substring(Impressao.PosicaoTexto, 1) == " ")
                {
                    Impressao.PosicaoTexto += 1;
                }
                Impressao.Graficos.DrawString(dados[2].Substring(Impressao.PosicaoTexto), Impressao.FonteCabecalho, Impressao.FonteCorPreta, Impressao.MargemEsquerda, Impressao.MargemSuperior);
                Impressao.MargemSuperior += Impressao.FonteCabecalho.Height;
                Impressao.PosicaoTexto = 0;
            }

            //Escreve o documento
            Impressao.Graficos.DrawString(dados[3], Impressao.FonteCabecalho, Impressao.FonteCorPreta, Impressao.MargemEsquerda, Impressao.MargemSuperior);
            Impressao.MargemSuperior += Impressao.FonteCabecalho.Height;

            //Escreve a inscrição estadual
            Impressao.Graficos.DrawString(dados[12], Impressao.FonteCabecalho, Impressao.FonteCorPreta, Impressao.MargemEsquerda, Impressao.MargemSuperior);
            Impressao.MargemSuperior += Impressao.FonteCabecalho.Height;

            //Escreve a data de aniversario
            Impressao.Graficos.DrawString(dados[4], Impressao.FonteCabecalho, Impressao.FonteCorPreta, Impressao.MargemEsquerda, Impressao.MargemSuperior);
            Impressao.MargemSuperior += Impressao.FonteCabecalho.Height;

            //Escreve a data de cadastro
            Impressao.Graficos.DrawString(dados[5], Impressao.FonteCabecalho, Impressao.FonteCorPreta, Impressao.MargemEsquerda, Impressao.MargemSuperior);
            Impressao.MargemSuperior += Impressao.FonteCabecalho.Height;

            //Escreve o site
            Impressao.Graficos.DrawString(dados[11], Impressao.FonteCabecalho, Impressao.FonteCorPreta, Impressao.MargemEsquerda, Impressao.MargemSuperior);
            Impressao.MargemSuperior += Impressao.FonteCabecalho.Height;

            //Cria um espaço de 10pixels 
            Impressao.MargemSuperior += 10;
            //Cria a linha
            Impressao.Graficos.DrawLine(Impressao.LinhaPreta, new Point(Impressao.MargemEsquerda, Impressao.MargemSuperior), new Point(e.MarginBounds.Width + 100, Impressao.MargemSuperior));
            //Cria um espaço de 10pixels
            Impressao.MargemSuperior += 10;

            //Endereço
            while (dados[6].Length - Impressao.PosicaoTexto > Impressao.MaximoCaracteres)
            {
                if (dados[6].Substring(Impressao.PosicaoTexto, 1) == " ")
                {
                    Impressao.PosicaoTexto += 1;
                }
                else
                {
                    //Define a posição do ultimo espaço em branco no texto selecionado
                    Impressao.PosicaoUltimoEspaco = dados[6].Substring(Impressao.PosicaoTexto, Impressao.PosicaoTexto + Impressao.MaximoCaracteres).LastIndexOf(" ");

                    //Escreve o endereço
                    Impressao.Graficos.DrawString(dados[6].Substring(Impressao.PosicaoTexto, Impressao.PosicaoUltimoEspaco), Impressao.FonteCabecalho, Impressao.FonteCorPreta, Impressao.MargemEsquerda, Impressao.MargemSuperior);
                    //Soma na altura atual do texto
                    Impressao.MargemSuperior += Impressao.FonteCabecalho.Height;
                    //Define a nova posição do texto
                    Impressao.PosicaoTexto += Impressao.PosicaoUltimoEspaco;
                }
            }
            if (dados[6].Length - Impressao.PosicaoTexto > 0)
            {
                if (dados[6].Substring(Impressao.PosicaoTexto, 1) == " ")
                {
                    Impressao.PosicaoTexto += 1;
                }
                Impressao.Graficos.DrawString(dados[6].Substring(Impressao.PosicaoTexto), Impressao.FonteCabecalho, Impressao.FonteCorPreta, Impressao.MargemEsquerda, Impressao.MargemSuperior);
                Impressao.MargemSuperior += Impressao.FonteCabecalho.Height;
                Impressao.PosicaoTexto = 0;
            }

            //Cria um espaço de 10pixels 
            Impressao.MargemSuperior += 10;
            //Cria a linha
            Impressao.Graficos.DrawLine(Impressao.LinhaPreta, new Point(Impressao.MargemEsquerda, Impressao.MargemSuperior), new Point(e.MarginBounds.Width + 100, Impressao.MargemSuperior));
            //Cria um espaço de 10pixels
            Impressao.MargemSuperior += 10;

            //Escreve os telefones
            Impressao.Graficos.DrawString("Contatos: ", Impressao.FonteCabecalho, Impressao.FonteCorPreta, Impressao.MargemEsquerda, Impressao.MargemSuperior);
            Impressao.MargemSuperior += Impressao.FonteCabecalho.Height;

            //Escreve o contato 1
            if(!string.IsNullOrEmpty(dados[7]))
            {
                Impressao.Graficos.DrawString(dados[7], Impressao.FonteCabecalho, Impressao.FonteCorPreta, Impressao.MargemEsquerda, Impressao.MargemSuperior);
                Impressao.MargemSuperior += Impressao.FonteCabecalho.Height;
            }
            //Escreve o contato 2
            if (!string.IsNullOrEmpty(dados[8]))
            {
                Impressao.Graficos.DrawString(dados[8], Impressao.FonteCabecalho, Impressao.FonteCorPreta, Impressao.MargemEsquerda, Impressao.MargemSuperior);
                Impressao.MargemSuperior += Impressao.FonteCabecalho.Height;
            }
            //Escreve o celular
            if (!string.IsNullOrEmpty(dados[9]))
            {
                Impressao.Graficos.DrawString(dados[9], Impressao.FonteCabecalho, Impressao.FonteCorPreta, Impressao.MargemEsquerda, Impressao.MargemSuperior);
                Impressao.MargemSuperior += Impressao.FonteCabecalho.Height;
            }
            //Escreve o e-mail
            Impressao.Graficos.DrawString(dados[10], Impressao.FonteCabecalho, Impressao.FonteCorPreta, Impressao.MargemEsquerda, Impressao.MargemSuperior);
            Impressao.MargemSuperior += Impressao.FonteCabecalho.Height;

            //Cria um espaço de 10pixels 
            Impressao.MargemSuperior += 10;
            //Cria a linha
            Impressao.Graficos.DrawLine(Impressao.LinhaPreta, new Point(Impressao.MargemEsquerda, Impressao.MargemSuperior), new Point(e.MarginBounds.Width + 100, Impressao.MargemSuperior));
            //Cria um espaço de 10pixels
            Impressao.MargemSuperior += 10;

            Impressao.Graficos.DrawString("Observações:", Impressao.FonteCabecalho, Impressao.FonteCorPreta, Impressao.MargemEsquerda, Impressao.MargemSuperior);
            Impressao.MargemSuperior += Impressao.FonteCabecalho.Height * 2;

            Impressao.ContadorLinhas = 0;

            if (Fornecedor.PrimeiraPagina)
            {
                Fornecedor.PrimeiraPagina = false;
                Fornecedor.Leitor = new StringReader(Fornecedor.TextoImpressao);
            }

            Impressao.LinhaTemporaria = null;

            while (Impressao.ContadorLinhas < Impressao.MaximoLinhas && ((Impressao.LinhaTemporaria = Fornecedor.Leitor.ReadLine()) != null))
            {
                Impressao.MargemSuperior += Impressao.FonteTextoAlternativo.Height;
                e.Graphics.DrawString(Impressao.LinhaTemporaria, Impressao.FonteTextoAlternativo, Impressao.FonteCorPreta, Impressao.MargemEsquerda, Impressao.MargemSuperior, new StringFormat());
                Impressao.ContadorLinhas++;
            }

            if (Impressao.LinhaTemporaria != null)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }

            Impressao.FonteCorPreta.Dispose();
        }
        #endregion

        #endregion
        /*
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
                    if (Sistema.Backup(local))
                    {
                        if (System.IO.File.Exists(local))
                        {
                            string comando = "Insert into tbl_log values (0," + Sistema.IDUsuarioLogado + ", 'Backup realizado - " +
                            DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "');";
                            Sistema.ExecutaComando(comando);

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
    }

    private void btnTestarAplicar_Click(object sender, EventArgs e)
    {
        bool abreprograma = false;

        if (rdbtnServidorLocal.Checked)
        {
            if (Sistema.TestaConexao(txtBanco.Text, txtUid.Text, txtPwd.Text))
            {
                abreprograma = true;
            }
        }
        else
        {
            if (Sistema.TestaConexao(txtServidor.Text, txtBanco.Text, txtUid.Text, txtPwd.Text))
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

            string comando = "Insert into tbl_log values (0," + Sistema.IDUsuarioLogado + ", 'Informações de conexão SQL alteradas - " +
            DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "');";
            Sistema.ExecutaComando(comando);

            MessageBox.Show("Dados atualizados com sucesso!");
        }
    }

    private void rdbtnRemoto_Click(object sender, EventArgs e)
    {
        //rdbtnRemoto.Checked = true;
        //rdbtnServidorLocal.Checked = false;
        //txtServidor.Enabled = true;
    }

    private void rdbtnServidorLocal_Click(object sender, EventArgs e)
    {
      //  rdbtnRemoto.Checked = false;
       // rdbtnServidorLocal.Checked = true;
        //txtServidor.Enabled = false;
    }

    private void btnDestravaTarefas_Click(object sender, EventArgs e)
    {
        DialogResult resultadoDialogo = MessageBox.Show("Você tem certeza de que deseja destravar todas as tarefas?\nTodas as outras estações devem estar fechadas!", "Destravar Tarefas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (resultadoDialogo == DialogResult.Yes)
        {
            /*string resposta = Interaction.InputBox("Digite a senha para prosseguir", "Destravar Tarefas", "");

            if (resposta == Sistema.SenhaADM)
            {
            if (funcoes.DestravaTodasTarefas())
            {
                string comando = "Insert into tbl_log values (0," + Sistema.IDUsuarioLogado + ", 'As tarefas foram destravadas - " +
                    DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "');";
                Sistema.ExecutaComando(comando);

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
                if (Sistema.Backup(local))
                {
                    if (System.IO.File.Exists(local))
                    {
                        string comando = "Insert into tbl_log values (0," + Sistema.IDUsuarioLogado + ", 'Restauração realizada - " +
                        DateTime.Now.ToShortDateString() + " às " + DateTime.Now.ToShortTimeString() + "');";
                        Sistema.ExecutaComando(comando);
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

*/
    }
}