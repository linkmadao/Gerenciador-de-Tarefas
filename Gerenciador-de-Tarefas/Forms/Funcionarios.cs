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
    public partial class Funcionarios : Form
    {
        private BDCONN conexao = new BDCONN();
        private string nomeFuncionarioAtual = "";

        public Funcionarios()
        {
            InitializeComponent();
            AtualizaLista();
        }

        private void LimpaCampos()
        {
            txtFuncionario.Text = "";
            btnCadastrar.Enabled = true;
            btnAlterarN.Enabled = false;
            btnRemover.Enabled = false;
        }

        private void AtualizaLista()
        {
            List<string> lista = conexao.PreencheCMB("Select nome from tbl_funcionarios;");
            lstbFuncionarios.DataSource = lista;

            LimpaCampos();
        }

        private void Excluir()
        {
            if (txtFuncionario.Text == lstbFuncionarios.SelectedItem.ToString())
            {
                DialogResult dialogResult = MessageBox.Show("Você tem certeza de que quer apagar o funcionário " + txtFuncionario.Text + "?", "Apagar usuário", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    conexao.ExecutaComando("delete from tbl_funcionarios where nome = '" + txtFuncionario.Text + "';");
                    MessageBox.Show("O funcionário " + txtFuncionario.Text + " foi apagado com sucesso!");
                    AtualizaLista();
                    btnAlterarN.Enabled = false;
                }
            }
            else
            {
                txtFuncionario.Focus();
                MessageBox.Show("O funcionário " + txtFuncionario.Text + " não condiz com o nome selecionado '" + lstbFuncionarios.SelectedItem.ToString() + "'.", "Erro ao apagar o funcionário", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Atualizar()
        {
            if ((txtFuncionario.Text != lstbFuncionarios.SelectedItem.ToString()) && (txtFuncionario.Text != ""))
            {
                conexao.ExecutaComando("Update tbl_funcionarios set nome='" + txtFuncionario.Text +
                    "' where nome ='" + lstbFuncionarios.SelectedItem.ToString() + "';");
                MessageBox.Show("O funcionário " + lstbFuncionarios.SelectedItem.ToString() + " foi atualizado para " + txtFuncionario.Text + " com sucesso!");
                AtualizaLista();
            }
            else
            {
                txtFuncionario.Focus();
                MessageBox.Show("A alteração solicitada não pode ser executada.\nVerifique se o nome da empresa é o mesmo!", "Erro ao alterar o nome do funcionário!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAlterarN_Click(object sender, EventArgs e)
        {
            if (txtFuncionario.Text != lstbFuncionarios.SelectedIndex.ToString())
            {
                conexao.ExecutaComando("Update tbl_funcionarios set nome='" + txtFuncionario.Text +
                    "' where nome ='" + lstbFuncionarios.SelectedItem.ToString() + "';");
                MessageBox.Show("O funcionário " + lstbFuncionarios.SelectedItem.ToString() + " foi atualizado para " + txtFuncionario.Text + " com sucesso!");
                AtualizaLista();
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (txtFuncionario.Text != "")
            {
                conexao.ExecutaComando("insert into tbl_funcionarios values (null,'" + txtFuncionario.Text +"');");
                MessageBox.Show("O funcionário " + txtFuncionario.Text + " foi inserido com sucesso!");
                AtualizaLista();
                btnAlterarN.Enabled = false;
            }
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            Excluir();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lstbFuncionarios_MouseClick(object sender, MouseEventArgs e)
        {
            txtFuncionario.Text = lstbFuncionarios.SelectedItem.ToString();
            nomeFuncionarioAtual = lstbFuncionarios.SelectedItem.ToString();

            if (!btnAlterarN.Enabled)
            {
                btnAlterarN.Enabled = true;
            }
            if (!btnRemover.Enabled)
            {
                btnRemover.Enabled = true;
            }
            if (btnCadastrar.Enabled)
            {
                btnCadastrar.Enabled = false;
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimpaCampos();
        }

        private void Funcionarios_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                Atualizar();
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                Excluir();
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
                LimpaCampos();
            }
        }

        private void Funcionarios_Load(object sender, EventArgs e)
        {

        }
    }
}
