using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gerenciador_de_Tarefas.Classes;
using Microsoft.VisualBasic;

namespace Gerenciador_de_Tarefas
{
    public partial class CadastraCliente : Form
    {
        /// <summary>
        /// Controle de clientes
        /// </summary>
        /// <param name="fazerNovoCadastro">Fazer novo cadastro?</param>
        /// <param name="idCliente">ID do Cliente</param>
        public CadastraCliente()
        {
            InitializeComponent();

            if(Cliente.NovoCadastro)
            {
                Cliente.NovoCadastro = true;
                btnSair.Text = "Cancelar";
                btnImprimir.Enabled = false;
                btnApagar.Enabled = false;
                btnAlterar.Enabled = false;
            }
            else
            {
                Cliente.AbrirCliente();

                lblNumeroTarefas.Text = Cliente.NumeroTarefas;
                Text = "Cliente - " + Cliente.Nome;

                txtCodigo.Text = Cliente.ID.ToString();
                txtNome.Text = Cliente.Nome;
                txtRazaoSocial.Text = Cliente.RazaoSocial;
                txtTelefoneComercial.Text = Cliente.Telefone;
                txtContato.Text = Cliente.Contato;
                txtSetor.Text = Cliente.Setor;
                dtpDataCadastro.Text = Cliente.DataCadastro;
                txtEmail.Text = Cliente.Email;
                txtSite.Text = Cliente.Site;
                txtOBS.Text = Cliente.Obs;
                txtCPF.Text = Cliente.CPF;
                txtRG.Text = Cliente.RG;
                txtCNPJ.Text = Cliente.CNPJ;
                txtInscMunicipal.Text = Cliente.InscricaoMunicipal;
                txtInscEstadual.Text = Cliente.InscricaoEstadual;
                txtCep.Text = Cliente.CEP;
                txtEndereco.Text = Cliente.Endereco;
                txtNumero.Text = Cliente.Numero;
                txtBairro.Text = Cliente.Bairro;
                txtCidade.Text = Cliente.Cidade;
                txtEstado.Text = Cliente.Estado;
                txtComplemento.Text = Cliente.Complemento;
                txtPontoReferencia.Text = Cliente.PontoReferencia;
                cmbContrato.SelectedIndex = Cliente.Contrato;

                btnCadastrar.Enabled = false;
                btnImprimir.Enabled = true;
                btnAlterar.Enabled = true;
                btnApagar.Enabled = true;
            }            
        }

        private void CadastraCliente_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Cliente.NovoCadastro)
            {
                if (ListaMensagens.RetornaDialogo(06) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
            else
            {
                if (Cliente.AvaliarMudancas())
                {
                    if (ListaMensagens.RetornaDialogo(07) == DialogResult.No)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }
        
        #region Posição cursor MaskedTextBox
        private delegate void PosicionaCursorDelegate(int posicao);

        private void PosicionaCursorCEP(int posicao)
        {
            txtCep.SelectionStart = posicao;
        }

        private void PosicionaCursorCPF(int posicao)
        {
            txtCPF.SelectionStart = posicao;
        }
        private void PosicionaCursorCNPJ(int posicao)
        {
            txtCNPJ.SelectionStart = posicao;
        }

        private void PosicionaCursorTelefoneComercial(int posicao)
        {
            txtTelefoneComercial.SelectionStart = posicao;
        }
        private void PosicionaCursorNumero(int posicao)
        {
            txtNumero.SelectionStart = posicao;
        }

        private void txtCep_Enter(object sender, EventArgs e)
        {
            BeginInvoke(new PosicionaCursorDelegate(PosicionaCursorCEP), new object[] { 0 });
        }
        private void txtCPF_Enter(object sender, EventArgs e)
        {
            BeginInvoke(new PosicionaCursorDelegate(PosicionaCursorCPF), new object[] { 0 });
        }
        private void txtCNPJ_Enter(object sender, EventArgs e)
        {
            BeginInvoke(new PosicionaCursorDelegate(PosicionaCursorCNPJ), new object[] { 0 });
        }
        private void txtTelefoneComercial_Enter(object sender, EventArgs e)
        {
            BeginInvoke(new PosicionaCursorDelegate(PosicionaCursorTelefoneComercial), new object[] { 0 });
        }
        private void txtNumero_Enter(object sender, EventArgs e)
        {
            BeginInvoke(new PosicionaCursorDelegate(PosicionaCursorNumero), new object[] { 0 });
        }
        #endregion

        private void btnApagar_Click(object sender, EventArgs e)
        {
            if (ListaMensagens.RetornaDialogo(08) == DialogResult.Yes)
            {
                string resposta = Interaction.InputBox(ListaMensagens.RetornaInputBox(09)[0], ListaMensagens.RetornaInputBox(09)[1], "");

                if (resposta == "MB8719")
                {
                    try
                    {
                        Cliente.ApagarCliente();
                    }
                    catch (Exception)
                    {
                        ListaErro.RetornaErro(37);
                        return;
                    }
                    finally
                    {
                        ListaMensagens.RetornaMensagem(10);
                        this.Close();
                    }
                }
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNovoRegistro_Click(object sender, EventArgs e)
        {
            Cliente.LimparVariaveis();
            LimpaCampos();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if(txtNome.Text.Length < 3)
            {
                ListaErro.RetornaErro(34);
                tabPageDadosCadastrais.Show();
                txtNome.Focus();
            }
            else
            {
                Cliente.Nome = txtNome.Text;
                Cliente.RazaoSocial = txtRazaoSocial.Text;
                Cliente.Telefone = txtTelefoneComercial.Text;
                Cliente.Contato = txtContato.Text;
                Cliente.Setor = txtSetor.Text;
                Cliente.CPF = txtCPF.Text;
                Cliente.RG = txtRG.Text;
                Cliente.CNPJ = txtCNPJ.Text;
                Cliente.InscricaoEstadual = txtInscEstadual.Text;
                Cliente.InscricaoMunicipal = txtInscMunicipal.Text;
                Cliente.Site = txtSite.Text;
                Cliente.Email = txtEmail.Text;
                Cliente.Endereco = txtEndereco.Text;
                Cliente.Numero = txtNumero.Text;
                Cliente.Bairro = txtBairro.Text;
                Cliente.Cidade = txtCidade.Text;
                Cliente.Estado = txtEstado.Text;
                Cliente.CEP = txtCep.Text;
                Cliente.Complemento = txtComplemento.Text;
                Cliente.PontoReferencia = txtPontoReferencia.Text;
                Cliente.Obs = txtOBS.Text;
                Cliente.Contrato = cmbContrato.SelectedIndex + 1;

                Cliente.AtualizarCliente();
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text.Length < 3)
            {
                ListaErro.RetornaErro(34);
                tabPageDadosCadastrais.Show();
                txtNome.Focus();
            }
            else
            {
                Cliente.Contrato = cmbContrato.SelectedIndex + 1;
                Cliente.DataCadastro = dtpDataCadastro.Value.ToString("yyyy-MM-dd");
                Cliente.Nome = txtNome.Text;
                Cliente.RazaoSocial = txtRazaoSocial.Text;
                Cliente.Telefone = txtTelefoneComercial.Text;
                Cliente.Contato = txtContato.Text;
                Cliente.Setor = txtSetor.Text;
                Cliente.CPF = txtCPF.Text;
                Cliente.RG = txtRG.Text;
                Cliente.CNPJ = txtCNPJ.Text;
                Cliente.InscricaoEstadual = txtInscEstadual.Text;
                Cliente.InscricaoMunicipal = txtInscMunicipal.Text;
                Cliente.Site = txtSite.Text;
                Cliente.Email = txtEmail.Text;
                Cliente.Endereco = txtEndereco.Text;
                Cliente.Numero = txtNumero.Text;
                Cliente.Bairro = txtBairro.Text;
                Cliente.Cidade = txtCidade.Text;
                Cliente.Estado = txtEstado.Text;
                Cliente.CEP = txtCep.Text;
                Cliente.Complemento = txtComplemento.Text;
                Cliente.PontoReferencia = txtPontoReferencia.Text;
                Cliente.Obs = txtOBS.Text;
                
                if(Cliente.CadastrarCliente())
                {
                    if (ListaMensagens.RetornaDialogo(12) == DialogResult.Yes)
                    {
                        LimpaCampos();

                        tabPageDadosCadastrais.Select();
                        tabPageDadosCadastrais.Show();
                        txtNome.Focus();

                        btnSair.Text = "Cancelar";
                    }
                    else
                    {
                        btnCadastrar.Enabled = false;
                        btnImprimir.Enabled = true;
                        btnAlterar.Enabled = true;
                        btnApagar.Enabled = true;

                        txtCodigo.Text = Cliente.ID.ToString();

                        btnSair.Text = "Sair";
                        Cliente.NovoCadastro = false;
                    }
                }
            }
        }

        private void txtCep_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCep.Text.Replace("-", "")))
            {
                LocalizarCEP();
            }
        }

        private void txtCNPJ_Leave(object sender, EventArgs e)
        {
            txtCNPJ.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

            try
            {
                if (txtCNPJ.Text.Length > 0 && txtCNPJ.Text.Length < 14)
                {
                    ListaErro.RetornaErro(40);
                    txtCNPJ.Clear();
                    txtCNPJ.Focus();
                }
                else if (txtCNPJ.Text.Length == 14 && !Funcoes.ValidaCNPJ(txtCNPJ.Text))
                {
                    ListaErro.RetornaErro(41);
                    txtCNPJ.Clear();
                    txtCNPJ.Focus();
                }
            }
            catch (Exception)
            {
                ListaErro.RetornaErro(41);
                txtCNPJ.Clear();
                txtCNPJ.Focus();
            }
        }

        private void txtCPF_Leave(object sender, EventArgs e)
        {
            txtCPF.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

            try
            {
                if (txtCPF.Text.Length > 0 && txtCPF.Text.Length < 11)
                {
                    ListaErro.RetornaErro(42);
                    txtCPF.Clear();
                    txtCPF.Focus();
                }
                else if (txtCPF.Text.Length == 11 && !Funcoes.ValidaCPF(txtCPF.Text))
                {
                    ListaErro.RetornaErro(43);
                    txtCPF.Clear();
                    txtCPF.Focus();
                }
            }
            catch (Exception)
            {
                ListaErro.RetornaErro(43);
                txtCPF.Clear();
                txtCPF.Focus();
            }
            
        }

        private void LimpaCampos()
        {
            txtCodigo.Clear();
            txtNome.Clear();
            txtRazaoSocial.Clear();
            cmbContrato.SelectedIndex = 0;
            txtTelefoneComercial.Clear();
            txtContato.Clear();
            txtSetor.Clear();
            txtEmail.Clear();
            txtSite.Clear();
            dtpDataCadastro.Text = DateTime.Now.ToShortDateString();
            txtOBS.Clear();
            txtCNPJ.Clear();
            txtInscEstadual.Clear();
            txtInscMunicipal.Clear();
            txtCPF.Clear();
            txtRG.Clear();
            txtCep.Clear();
            txtEndereco.Clear();
            txtNumero.Clear();
            txtBairro.Clear();
            txtCidade.Clear();
            txtEstado.Clear();
            txtComplemento.Clear();
            txtPontoReferencia.Clear();

            btnAlterar.Enabled = false;
            btnApagar.Enabled = false;
            btnImprimir.Enabled = false;
            btnCadastrar.Enabled = true;

            tabPageDadosCadastrais.Select();
            tabPageDadosCadastrais.Show();
            txtNome.Focus();

            Cliente.LimparVariaveis();

            btnSair.Text = "Cancelar";

            Cliente.NovoCadastro = true;
        }

        private void LocalizarCEP()
        {
            txtCep.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

            if (txtCep.Text.Length > 0 && txtCep.Text.Length < 8)
            {
                ListaErro.RetornaErro(44);

                txtCep.Clear();
                txtCep.Focus();
            }
            else if (txtCep.Text.Length == 8)
            {
                try
                {
                    CEP cep = new CEP(txtCep.Text);

                    if (cep.cep != null)
                    {
                        txtEstado.Text = cep.uf;
                        txtCidade.Text = cep.localidade;
                        txtBairro.Text = cep.bairro;
                        txtEndereco.Text = cep.logradouro;
                        txtComplemento.Text = cep.complemento;
                        txtNumero.Focus();
                    }
                    else
                    {
                        if (ListaMensagens.RetornaDialogo(25) == DialogResult.Yes)
                        {
                            txtCep.Clear();
                            txtCep.Focus();
                        }
                        else
                        {
                            txtCep.Clear();
                        }
                    }
                }
                catch (KeyNotFoundException)
                {
                    ListaErro.RetornaErro(46);
                    txtCep.Clear();
                    txtCep.Focus();
                }
            }
        }

        private void CarregarCliente()
        {
            if (!Cliente.NovoCadastro)
            {
                
            }
        }
    }
}
