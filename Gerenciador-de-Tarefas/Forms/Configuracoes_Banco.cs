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
using System.Xml;
using System.Xml.Linq;
using Gerenciador_de_Tarefas.Classes;

namespace Gerenciador_de_Tarefas
{
    public partial class Configuracoes_Banco: Form
    {
        private BDCONN conexao = new BDCONN();
        private string nomeXML = "bdconfig.xml";
        private bool programaDesativado = false;

        /// <summary>
        /// Método que abre o formulário de configuração da conexão com o banco de dados.
        /// </summary>
        /// <param name="desativaPrograma">Caso seja verdadeiro, o programa verificará se configuração do banco de dados é valida antes de continuar a ser executado!</param>
        public Configuracoes_Banco(bool desativaPrograma)
        {
            InitializeComponent();

            if (desativaPrograma)
            {
                programaDesativado = true;
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTestarAplicar_Click(object sender, EventArgs e)
        {
            bool abreprograma = false;

            if(rdbtnServidorLocal.Checked)
            {
                if (conexao.TestaConexao(txtBanco.Text, txtUid.Text, txtPwd.Text))
                {
                    programaDesativado = false;
                    abreprograma = true;
                }
            }
            else
            {
                if (conexao.TestaConexao(txtServidor.Text, txtBanco.Text, txtUid.Text, txtPwd.Text))
                {
                    programaDesativado = false;
                    abreprograma = true;
                }
            }

            if(abreprograma)
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
                this.Close();
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

        private void Configuracoes_Banco_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(programaDesativado)
            {
                string erro = ListaErro.RetornaErro(14);
                int separador = erro.IndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);

                programaDesativado = false;
            }
        }

        private void Configuracoes_Banco_Load(object sender, EventArgs e)
        {
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
        }
    }
}
