using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gerenciador_de_Tarefas.Classes
{
    public static class Fornecedor
    {
        #region Variaveis
        private static int id, tipo, categoria1, categoria2, categoria3, subCategoria1, subCategoria2, subCategoria3;
        private static string documento, nome, apelido, dataCadastro, dataNascimento, cep, endereco, numero, complemento, bairro, cidade, estado, pais,
            telefone, contato, telefoneComercial, contatoComercial, celular, contatoCelular, email, site, inscricaoEstadual, inscricaoMunicipal, obs;
        //private static BDCONN conexao = new BDCONN();
        #endregion

        #region Propriedades
        public static int ID
        {
            get { return id; }
            set { id = value; }
        }
        public static int Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        public static int Categoria1
        {
            get { return categoria1; }
            set { categoria1 = value; }
        }
        public static int Categoria2
        {
            get { return categoria2; }
            set { categoria2 = value; }
        }
        public static int Categoria3
        {
            get { return categoria3; }
            set { categoria3 = value; }
        }
        public static int SubCategoria1
        {
            get { return subCategoria1; }
            set { subCategoria1 = value; }
        }
        public static int SubCategoria2
        {
            get { return subCategoria2; }
            set { subCategoria2 = value; }
        }
        public static int SubCategoria3
        {
            get { return subCategoria3; }
            set { subCategoria3 = value; }
        }

        public static string Documento
        {
            get { return documento; }
            set { documento = value; }
        } 
        public static string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        public static string Apelido
        {
            get { return apelido; }
            set { apelido = value; }
        }
        public static string DataCadastro
        {
            get { return dataCadastro; }
            set { dataCadastro = value; }
        }
        public static string DataNascimento
        {
            get { return dataNascimento; }
            set { dataNascimento = value; }
        }
        public static string CEP
        {
            get { return cep; }
            set { cep = value; }
        }
        public static string Endereco
        {
            get { return endereco; }
            set { endereco = value; }
        }
        public static string Numero
        {
            get { return numero; }
            set { numero = value; }
        }
        public static string Complemento
        {
            get { return complemento; }
            set { complemento = value; }
        }
        public static string Bairro
        {
            get { return bairro; }
            set { bairro = value; }
        }
        public static string Cidade
        {
            get { return cidade; }
            set { cidade = value; }
        }
        public static string Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        public static string Pais
        {
            get { return pais; }
            set { pais = value; }
        }
        public static string Telefone
        {
            get { return telefone; }
            set { telefone = value; }
        }
        public static string Contato
        {
            get { return contato; }
            set { contato = value; }
        }
        public static string TelefoneComercial
        {
            get { return telefoneComercial; }
            set { telefoneComercial = value; }
        }
        public static string ContatoComercial
        {
            get { return contatoComercial; }
            set { contatoComercial = value; }
        }
        public static string Celular
        {
            get { return celular; }
            set { celular = value; }
        }
        public static string ContatoCelular
        {
            get { return contatoCelular; }
            set { contatoCelular = value; }
        }
        public static string Email
        {
            get { return email; }
            set { email = value; }
        }
        public static string Site
        {
            get { return site; }
            set { site = value; }
        }
        public static string InscricaoEstadual
        {
            get { return inscricaoEstadual; }
            set { inscricaoEstadual = value; }
        }
        public static string InscricaoMunicipal
        {
            get { return inscricaoMunicipal; }
            set { inscricaoMunicipal = value; }
        }
        public static string Obs
        {
            get { return obs; }
            set { obs = value; }
        }
        #endregion

        #region Funcoes
        public static int CadastraFornecedor(string tipo, string dataCadastro, string dataNascimento, string documento, string nome,
           string apelido, string cep, string endereco, string numero, string complemento, string bairro, string cidade, string estado,
           string pais, string telefone, string contato, string telefoneComercial, string contatoComercial, string celular, string contatoCelular,
           string email, string site, string inscEstadual, string inscMunicipal, string obs, string categoria1, string categoria2, string categoria3,
           string subcategoria1, string subcategoria2, string subcategoria3, bool travar)
        {
            string comando = null, textoTravar = "N", _dataCadastro = "", _dataNascimento = null, erro = "";
            int resultado = 0, separador = 0;

            try
            {
                if (travar)
                {
                    textoTravar = "S";
                }

                _dataCadastro = dataCadastro.Substring(6, 4) + "-" + dataCadastro.Substring(3, 2) + "-" + dataCadastro.Substring(0, 2);

                if (!string.IsNullOrEmpty(dataNascimento))
                {
                    _dataNascimento = dataNascimento.Substring(6, 4) + "-" + dataNascimento.Substring(3, 2) + "-" + dataNascimento.Substring(0, 2);
                }

                comando = "insert into tbl_fornecedor values (0," + tipo + ", '" + _dataCadastro + "',if('" + _dataNascimento + "' = '',NULL,'" + _dataNascimento + "'),'" + documento + "','" + nome + "','" + apelido + "','" + cep + "','"
                    + endereco + "','" + numero + "','" + complemento + "','" + bairro + "','" + cidade + "','" + estado + "','" + pais + "','" + telefone + "','" + contato + "','"
                    + telefoneComercial + "','" + contatoComercial + "','" + celular + "','" + contatoCelular + "','" + email + "','" + site + "','" + inscEstadual + "','" + inscMunicipal + "','"
                    + obs + "','" + categoria1 + "','" + categoria2 + "','" + categoria3 + "','" + subcategoria1 + "','" + subcategoria2 + "','" + subcategoria3 + "','" + textoTravar + "'); ";
                conexao.ExecutaComando(comando);
            }
            catch (Exception)
            {
                erro = ListaErro.RetornaErro(51);
                separador = erro.IndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }

            try
            {
                comando = "Select ID from tbl_fornecedor where tipo = '" + tipo + "'" +
                " AND nome = '" + nome + "'" +
                " AND datacadastro = '" + _dataCadastro + "';";

                resultado = Int32.Parse(conexao.ConsultaSimples(comando));
            }
            catch (Exception)
            {
                erro = ListaErro.RetornaErro(51);
                separador = erro.IndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }

            return resultado;
        }

        public static void CarregaFornecedor(int _idFornecedor)
        {
            List<string> listaFornecedor = conexao.ConsultaFornecedor("select * from tbl_fornecedor where tbl_fornecedor.id = '" + _idFornecedor + "';");

            ID = _idFornecedor;
            Tipo = int.Parse(listaFornecedor[1]);
            DataCadastro = listaFornecedor[2];
            DataNascimento = listaFornecedor[3];
            Documento = listaFornecedor[4];
            Nome = listaFornecedor[5];
            Apelido = listaFornecedor[6];
            CEP = listaFornecedor[7];
            Endereco = listaFornecedor[8];
            Numero = listaFornecedor[9];
            Complemento = listaFornecedor[10];
            Bairro = listaFornecedor[11];
            Cidade = listaFornecedor[12];
            Estado = listaFornecedor[13];
            Pais = listaFornecedor[14];
            Telefone = listaFornecedor[15];
            Contato = listaFornecedor[16];
            TelefoneComercial = listaFornecedor[17];
            ContatoComercial = listaFornecedor[18];
            Celular = listaFornecedor[19];
            ContatoCelular = listaFornecedor[20];
            Email = listaFornecedor[21];
            Site = listaFornecedor[22];
            InscricaoEstadual = listaFornecedor[23];
            InscricaoMunicipal = listaFornecedor[24];
            Obs = listaFornecedor[25];
            Categoria1 = int.Parse(listaFornecedor[26]);
            Categoria2 = int.Parse(listaFornecedor[27]);
            Categoria3 = int.Parse(listaFornecedor[28]);
            SubCategoria1 = int.Parse(listaFornecedor[29]);
            SubCategoria2 = int.Parse(listaFornecedor[30]);
            SubCategoria3 = int.Parse(listaFornecedor[31]);
        }
        #endregion
    }
}
