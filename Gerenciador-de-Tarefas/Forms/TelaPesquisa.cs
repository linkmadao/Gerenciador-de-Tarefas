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
    public partial class TelaPesquisa : Form
    {
        private DataGridView dgvTemp;
        private bool fornecedor = false;

        public TelaPesquisa(DataGridView dgvTemporario,bool cliente)
        {
            InitializeComponent();

            dgvTemp = dgvTemporario;

            if(!cliente)
            {
                fornecedor = true;

                //Seta o título da janela
                lblTítulo.Text = "Pesquisar Fornecedor";
                
                //Seta o novo texto do TextBox
                lblTexto.Text = "Fornecedor: ";
                //Seta o novo local onde TextBox ficará
                txtPesquisa.Location = new Point(121,66);
                //Seta o novo tamanho do TextBox
                txtPesquisa.Size = new Size(232, 22);
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            FuncoesEstaticas.FornecedorPesquisado = 0;
            FuncoesEstaticas.ClientePesquisado = 0;

            Close();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            List<string> listaPesquisa = new List<string>();
            string textoTemporario = txtPesquisa.Text;
            int espacoentrepalavras = 0;
            
            while (textoTemporario.Length > 0)
            {
                if (textoTemporario.Substring(espacoentrepalavras, 1) == " ")
                {
                    listaPesquisa.Add(textoTemporario.Substring(0, espacoentrepalavras));
                    textoTemporario = textoTemporario.Substring(espacoentrepalavras + 1, textoTemporario.Length - espacoentrepalavras - 1);
                    espacoentrepalavras = 0;
                }
                else
                {
                    espacoentrepalavras++;
                }
                if(textoTemporario.Length == espacoentrepalavras)
                {
                    listaPesquisa.Add(textoTemporario.Substring(0, espacoentrepalavras));
                    break;
                }
            }
            if(!fornecedor)
            {
                FuncoesEstaticas.PesquisaCliente(dgvTemp, listaPesquisa);
                
            }
            else
            {
                FuncoesEstaticas.PesquisaFornecedor(dgvTemp, listaPesquisa);
            }
            
            Close();
        }
    }
}
