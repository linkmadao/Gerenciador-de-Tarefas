using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gerenciador_de_Tarefas
{
    public partial class CadastraCliente : Form
    {
        private BDCONN conexao = new BDCONN();
        private bool novoCadastro = false;
        private int _idCliente = 0, _contrato = 0;

        private string _nome = null, _razaoSocial = null, _telefone = null,
            _contato = null, _setor = null, _email = null, _site = null, _obs = null, _cpf = null,
            _rg = null, _cnpj = null, _inscMunicipal = null, _inscEstadual = null, _cep = null, 
            _endereco = null, _numero = null, _bairro = null, _cidade = null, _estado = null,
            _complemento = null, _pontoReferencia = null;


        /// <summary>
        /// Controle de clientes
        /// </summary>
        /// <param name="fazerNovoCadastro">Fazer novo cadastro?</param>
        /// <param name="idCliente">ID do Cliente</param>
        public CadastraCliente(bool fazerNovoCadastro, int idCliente)
        {
            InitializeComponent();

            if(fazerNovoCadastro)
            {
                novoCadastro = true;
                btnSair.Text = "Cancelar";
                btnImprimir.Enabled = false;
                btnApagar.Enabled = false;
                btnAlterar.Enabled = false;

            }
            else
            {
                _idCliente = idCliente;
                CarregarCliente(_idCliente);
            }            
        }

        private void CadastraCliente_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (novoCadastro)
            {
                string erro = ListaMensagens.RetornaMensagem(06);
                int separador = erro.IndexOf(":");
                DialogResult resultadoDialogo = MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultadoDialogo == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
            else
            {
                if (_contrato != (cmbContrato.SelectedIndex + 1) || _nome != txtNome.Text ||
                    _razaoSocial != txtRazaoSocial.Text || _telefone != txtTelefoneComercial.Text ||
                    _contato != txtContato.Text || _setor != txtSetor.Text || _email != txtEmail.Text ||
                    _site != txtSite.Text || _obs != txtOBS.Text || _cpf != txtCPF.Text ||
                    _rg != txtRG.Text || _cnpj != txtCNPJ.Text || _inscMunicipal != txtInscMunicipal.Text ||
                    _inscEstadual != txtInscEstadual.Text || _cep != txtCep.Text || _endereco != txtEndereco.Text ||
                    _numero != txtNumero.Text || _bairro != txtBairro.Text || _cidade != txtCidade.Text ||
                    _estado != txtEstado.Text || _complemento != txtComplemento.Text ||
                    _pontoReferencia != txtPontoReferencia.Text)
                {
                    string erro = ListaMensagens.RetornaMensagem(07);
                    int separador = erro.IndexOf(":");
                    DialogResult resultadoDialogo = MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resultadoDialogo == DialogResult.No)
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
            string erro = ListaMensagens.RetornaMensagem(08);
            int separador = erro.IndexOf(":");
            DialogResult resultadoDialogo = MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultadoDialogo == DialogResult.Yes)
            {
                Console.Beep(1000, 2000);
                erro = ListaMensagens.RetornaMensagem(09);
                separador = erro.IndexOf(":");
                string resposta = Microsoft.VisualBasic.Interaction.InputBox(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), "");

                if (resposta == "MB8719")
                {
                    try
                    {
                        conexao.ExecutaComando("delete from tbl_tarefas where empresa = " + _idCliente + ";");
                        conexao.ExecutaComando("delete from tbl_contato_subsubgrupo where contato = " + _idCliente + ";");
                        conexao.ExecutaComando("delete from tbl_contato_subgrupo where contato = " + _idCliente + ";");
                        conexao.ExecutaComando("delete from tbl_contato_telefone where contato = " + _idCliente + ";");
                        conexao.ExecutaComando("delete from tbl_contato_contrato where contato = " + _idCliente + ";");
                        conexao.ExecutaComando("delete from tbl_contato where id = " + _idCliente + ";");
                    }
                    catch (Exception)
                    {
                        erro = ListaErro.RetornaErro(37);
                        separador = erro.IndexOf(":");
                        MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    finally
                    {
                        erro = ListaMensagens.RetornaMensagem(10);
                        separador = erro.IndexOf(":");
                        MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            LimpaCampos();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if(txtNome.Text.Length < 3)
            {
                string erro = ListaErro.RetornaErro(34);
                int separador = erro.IndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabPageDadosCadastrais.Show();
                txtNome.Focus();
            }
            else
            {
                try
                {
                    string comando = null;

                    comando = "Update tbl_contato set nome = '" + txtNome.Text + "', " +
                        "razaosocial = '" + txtRazaoSocial.Text + "', telefone = '" + txtTelefoneComercial.Text +"', " +
                        "contato = '" + txtContato.Text + "', setor = '" + txtSetor.Text + "', " +
                        "cpf = '" + txtCPF.Text + "', rg = '" + txtRG.Text + "', cnpj = '" + txtCNPJ.Text +"', " +
                        "inscricaoestadual = '" + txtInscEstadual.Text + "', inscricaomunicipal = '" + txtInscMunicipal.Text +"', " +
                        "site = '" + txtSite.Text + "', email = '" + txtEmail.Text + "', endereco = '" + txtEndereco.Text +"', " +
                        "bairro = '" + txtBairro.Text + "', cidade = '" + txtCidade.Text + "', estado = '" + txtEstado.Text +"', " +
                        "cep = '" + txtCep.Text + "', complemento = '" + txtComplemento.Text + "', " +
                        "pontoreferencia = '" + txtPontoReferencia.Text + "', obs = '" + txtOBS.Text +"'" +
                        "where id = " + _idCliente + ";";
                    conexao.ExecutaComando(comando);

                    comando = "Update tbl_contato_contrato set contrato = " + (cmbContrato.SelectedIndex + 1).ToString()
                        + " where contato = " + _idCliente.ToString() + ";";
                    conexao.ExecutaComando(comando);
                }
                catch (Exception)
                {
                    string erro = ListaErro.RetornaErro(38);
                    int separador = erro.IndexOf(":");
                    MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    string erro = ListaMensagens.RetornaMensagem(11);
                    int separador = erro.IndexOf(":");
                    MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SalvaCampos();
                }
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text.Length < 3)
            {
                string erro = ListaErro.RetornaErro(34);
                int separador = erro.IndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabPageDadosCadastrais.Show();
                txtNome.Focus();
            }
            else
            {
                try
                {
                    string comando = null;
                    comando = "insert into tbl_contato values " +
                    "(0, 1 , '" + dtpDataCadastro.Value.ToString("yyyy-MM-dd") + "', " +
                    "'" + txtNome.Text + "', '" + txtRazaoSocial.Text + "', " +
                    "'" + txtTelefoneComercial.Text + "', '" + txtContato.Text + "', " +
                    "'" + txtSetor.Text + "', '" + txtCPF.Text + "', '" + txtRG.Text + "', " +
                    "'" + txtCNPJ.Text + "', '" + txtInscEstadual.Text + "', '" + txtInscMunicipal.Text + "', " +
                    "'" + txtSite.Text + "', '" + txtEmail.Text + "', " +
                    "'" + txtEndereco.Text + ", " + txtNumero.Text + "', " +
                    "'" + txtBairro.Text + "', '" + txtCidade.Text + "', '" + txtEstado.Text + "', " +
                    "'" + txtCep.Text + "', '" + txtComplemento.Text + "', '" + txtPontoReferencia.Text + "', " +
                    "'" + txtOBS.Text + "');";
                    conexao.ExecutaComando(comando);

                    comando = "Select id from tbl_contato where nome = '" + txtNome.Text + "';";
                    _idCliente = Int32.Parse(conexao.ConsultaSimples(comando));

                    comando = "insert into tbl_contato_contrato values " +
                    "("+_idCliente.ToString() + ", " + (cmbContrato.SelectedIndex + 1) +");";
                    conexao.ExecutaComando(comando);
                }
                catch (Exception)
                {
                    string erro = ListaErro.RetornaErro(39);
                    int separador = erro.IndexOf(":");
                    MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    string erro = ListaMensagens.RetornaMensagem(12);
                    int separador = erro.IndexOf(":");
                    DialogResult resultadoDialogo = MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resultadoDialogo == DialogResult.Yes)
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

                        txtCodigo.Text = _idCliente.ToString();

                        btnSair.Text = "Sair";
                        novoCadastro = false;

                        SalvaCampos();
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
                    string erro = ListaErro.RetornaErro(40);
                    int separador = erro.IndexOf(":");
                    MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCNPJ.Clear();
                    txtCNPJ.Focus();
                }
                else if (txtCNPJ.Text.Length == 14 && !FuncoesEstaticas.ValidaCNPJ(txtCNPJ.Text))
                {
                    string erro = ListaErro.RetornaErro(41);
                    int separador = erro.IndexOf(":");
                    MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCNPJ.Clear();
                    txtCNPJ.Focus();
                }
            }
            catch (Exception)
            {
                string erro = ListaErro.RetornaErro(41);
                int separador = erro.IndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    string erro = ListaErro.RetornaErro(42);
                    int separador = erro.IndexOf(":");
                    MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCPF.Clear();
                    txtCPF.Focus();
                }
                else if (txtCPF.Text.Length == 11 && !FuncoesEstaticas.ValidaCPF(txtCPF.Text))
                {
                    string erro = ListaErro.RetornaErro(43);
                    int separador = erro.IndexOf(":");
                    MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCPF.Clear();
                    txtCPF.Focus();
                }
            }
            catch (Exception)
            {
                string erro = ListaErro.RetornaErro(43);
                int separador = erro.IndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            _idCliente = 0;
            _nome = null;
            _razaoSocial = null;
            _contrato = 0;
            _telefone = null;
            _contato = null;
            _setor = null;
            _email = null;
            _site = null;
            _obs = null;
            _cpf = null;
            _rg = null;
            _cnpj = null;
            _inscMunicipal = null;
            _inscEstadual = null;
            _cep = null;
            _endereco = null;
            _numero = null;
            _bairro = null;
            _cidade = null;
            _estado = null;
            _complemento = null;
            _pontoReferencia = null;

            btnAlterar.Enabled = false;
            btnApagar.Enabled = false;
            btnImprimir.Enabled = false;
            btnCadastrar.Enabled = true;

            tabPageDadosCadastrais.Select();
            tabPageDadosCadastrais.Show();
            txtNome.Focus();

            btnSair.Text = "Cancelar";

            novoCadastro = true;
        }

        private void LocalizarCEP()
        {
            txtCep.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

            if (txtCep.Text.Length > 0 && txtCep.Text.Length < 8)
            {
                string erro = ListaErro.RetornaErro(44);
                int separador = erro.IndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                    }
                    else
                    {
                        string erro = ListaErro.RetornaErro(45);
                        int separador = erro.IndexOf(":");
                        DialogResult resultadoDialogo = MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (resultadoDialogo == DialogResult.Yes)
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
                    string erro = ListaErro.RetornaErro(46);
                    int separador = erro.IndexOf(":");
                    MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCep.Clear();
                    txtCep.Focus();
                }
            }
        }

        private void CarregarCliente(int idCliente)
        {
            if (!novoCadastro)
            {
                string comando = null;

                comando = "select nome, razaosocial, telefone, contato, setor, datacadastro, email, site, obs, cpf, " 
                    + "rg, cnpj, inscricaomunicipal, inscricaoestadual, cep, endereco, bairro, cidade, "
                    + "estado, complemento, pontoreferencia " 
                    + "from tbl_contato where id = " + idCliente + ";";

                List<string> lista = conexao.ConsultaContato(comando);

                comando = "select count(id) from tbl_tarefas where empresa = " + idCliente + ";";
                lblNumeroTarefas.Text = conexao.ConsultaSimples(comando);

                Text = "Cliente - " + lista[0];

                txtCodigo.Text = idCliente.ToString();
                txtNome.Text = lista[0];
                txtRazaoSocial.Text = lista[1];
                txtTelefoneComercial.Text = lista[2];
                txtContato.Text = lista[3];
                txtSetor.Text = lista[4];
                dtpDataCadastro.Text = lista[5];
                txtEmail.Text = lista[6];
                txtSite.Text = lista[7];
                txtOBS.Text = lista[8];
                txtCPF.Text = lista[9];
                txtRG.Text = lista[10];
                txtCNPJ.Text = lista[11];
                txtInscMunicipal.Text = lista[12];
                txtInscEstadual.Text = lista[13];
                txtCep.Text = lista[14];
                if(txtEndereco.Text.Contains(','))
                {
                    txtEndereco.Text = lista[15].Substring(0, lista[15].LastIndexOf(','));
                    txtNumero.Text = lista[15].Substring(lista[15].LastIndexOf(',') + 2, (lista[15].Length - (lista[15].LastIndexOf(',') + 2)));
                }
                else
                {
                    txtEndereco.Text = lista[15];
                }
                txtBairro.Text = lista[16];
                txtCidade.Text = lista[17];
                txtEstado.Text = lista[18];
                txtComplemento.Text = lista[19];
                txtPontoReferencia.Text = lista[20];

                comando = "select contrato from tbl_contato_contrato where contato = " + idCliente + ";";
                cmbContrato.SelectedIndex = Convert.ToInt32(conexao.ConsultaSimples(comando)) - 1;

                btnCadastrar.Enabled = false;
                btnImprimir.Enabled = true;
                btnAlterar.Enabled = true;
                btnApagar.Enabled = true;

                SalvaCampos();
            }
        }

        private void SalvaCampos()
        {
            _nome = txtNome.Text;
            _razaoSocial = txtRazaoSocial.Text;
            _contrato = cmbContrato.SelectedIndex + 1;
            _telefone = txtTelefoneComercial.Text;
            _contato = txtContato.Text;
            _setor = txtSetor.Text;
            _email = txtEmail.Text;
            _site = txtSite.Text;
            _obs = txtOBS.Text;
            _cpf = txtCPF.Text;
            _rg = txtRG.Text;
            _cnpj = txtCNPJ.Text;
            _inscMunicipal = txtInscMunicipal.Text;
            _inscEstadual = txtInscEstadual.Text;
            _cep = txtCep.Text;
            _endereco = txtEndereco.Text;
            _numero = txtNumero.Text;
            _bairro = txtBairro.Text;
            _cidade = txtCidade.Text;
            _estado = txtEstado.Text;
            _complemento = txtComplemento.Text;
            _pontoReferencia = txtPontoReferencia.Text;
        }
    }
}
