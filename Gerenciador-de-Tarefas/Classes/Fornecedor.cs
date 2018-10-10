using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace Gerenciador_de_Tarefas.Classes
{
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

    public static class Fornecedor
    {
        #region Variaveis
        private static int id, tipo, categoria1, categoria2, categoria3, subCategoria1, subCategoria2, subCategoria3;
        private static int _id, _tipo, _categoria1, _categoria2, _categoria3, _subCategoria1, _subCategoria2, _subCategoria3;
        private static string documento, nome, apelido, dataCadastro, dataNascimento, cep, endereco, numero, complemento, bairro, cidade, estado, pais,
            telefone, contato, telefoneComercial, contatoComercial, celular, contatoCelular, email, site, inscricaoEstadual, inscricaoMunicipal, obs;
        private static string _documento, _nome, _apelido, _dataCadastro, _dataNascimento, _cep, _endereco, _numero, _complemento, _bairro, _cidade, _estado, _pais,
           _telefone, _contato, _telefoneComercial, _contatoComercial, _celular, _contatoCelular, _email, _site, _inscricaoEstadual, _inscricaoMunicipal, _obs;
        private static BDCONN conexao = new BDCONN();
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
        public static int FornecedorPesquisado
        {
            get; set;
        }
        #endregion

        #region Funcoes
        public static string FiltroFornecedores(int posicaoCmbFiltroFornecedores)
        {
            string comando = "";

            switch (posicaoCmbFiltroFornecedores)
            {
                default:
                    comando = "Select tbl_fornecedor.ID, tbl_fornecedor.nome as 'Razão Social / Nome', " +
                        "IFNULL(tbl_subgrupos.nome, '') as 'Fornece', " +
                        "IFNULL(tbl_fornecedor.Telefone, IFNULL(tbl_fornecedor.TelefoneComercial, IFNULL(tbl_fornecedor.Celular, ''))) as 'Telefone', " +
                        "IFNULL(tbl_fornecedor.Email, '') as 'E-mail', " +
                        "IFNULL(tbl_fornecedor.Contato, IFNULL(tbl_fornecedor.ContatoComercial, IFNULL(tbl_fornecedor.ContatoCelular, ''))) as 'Contato' " +
                        "from tbl_fornecedor " +
                        "join tbl_subgrupos on tbl_subgrupos.id = tbl_fornecedor.Categoria1 " +
                        "ORDER BY tbl_fornecedor.nome ASC;";
                    break;
            }

            return comando;
        }

        public static void PesquisaFornecedor(DataGridView dgvFornecedores, List<string> listaPesquisa)
        {
            int linha = 0;
            bool resultado = false;

            for (int i = 0; i < dgvFornecedores.Rows.Count; i++)
            {
                for (int x = 0; x < listaPesquisa.Count; x++)
                {
                    if (dgvFornecedores[1, i].Value.ToString().ToUpper().Contains(listaPesquisa[x].ToUpper()))
                    {
                        linha = dgvFornecedores[1, i].RowIndex;
                        resultado = true;
                    }
                }
                if (resultado)
                {
                    break;
                }
            }

            if (resultado)
            {
                FornecedorPesquisado = linha;
            }
            else
            {
                FornecedorPesquisado = -1;
            }
        }

        public static void AbrirFornecedor(int _idFornecedor)
        {
            LimparVariaveis();
            List<string> listaFornecedor = conexao.ConsultaFornecedor("select tipo, datacadastro, datanascimento, documento, nome, " +
                "apelido, cep, endereco, numero, complemento, bairro, cidade, estado, pais, telefone, contato, telefonecomercial, " +
                "contatocomercial, celular, contatocelular, email, site, inscricaoestadual, inscricaomunicipal, observacoes, categoria1, " +
                "categoria2, categoria3, subcategoria1, subcategoria2, subcategoria3 " +
                "from tbl_fornecedor where id = '" + _idFornecedor + "';");
            
            ID = _idFornecedor;
            Tipo = int.Parse(listaFornecedor[0]);
            DataCadastro = listaFornecedor[1];
            DataNascimento = listaFornecedor[2];
            Documento = listaFornecedor[3];
            Nome = listaFornecedor[4];
            Apelido = listaFornecedor[5];
            CEP = listaFornecedor[6];
            Endereco = listaFornecedor[7];
            Numero = listaFornecedor[8];
            Complemento = listaFornecedor[9];
            Bairro = listaFornecedor[10];
            Cidade = listaFornecedor[11];
            Estado = listaFornecedor[12];
            Pais = listaFornecedor[13];
            Telefone = listaFornecedor[14];
            Contato = listaFornecedor[15];
            TelefoneComercial = listaFornecedor[16];
            ContatoComercial = listaFornecedor[17];
            Celular = listaFornecedor[18];
            ContatoCelular = listaFornecedor[19];
            Email = listaFornecedor[20];
            Site = listaFornecedor[21];
            InscricaoEstadual = listaFornecedor[22];
            InscricaoMunicipal = listaFornecedor[23];
            Obs = listaFornecedor[24];
            Categoria1 = int.Parse(listaFornecedor[25]);
            Categoria2 = int.Parse(listaFornecedor[26]);
            Categoria3 = int.Parse(listaFornecedor[27]);
            SubCategoria1 = int.Parse(listaFornecedor[28]);
            SubCategoria2 = int.Parse(listaFornecedor[29]);
            SubCategoria3 = int.Parse(listaFornecedor[30]);
            
            //Backup
            _id = ID;
            _tipo = Tipo;
            _dataCadastro = DataCadastro.Substring(0,10);
            if(!string.IsNullOrEmpty(DataNascimento) && DataNascimento != "")
            {
                DataNascimento.Substring(0, 10);
            }
            else
            {
                _dataNascimento = DataNascimento;
            }
            _documento = Documento;
            _nome = Nome;
            _apelido = Apelido;
            _cep = CEP;
            _endereco = Endereco;
            _numero = Numero;
            _complemento = Complemento;
            _bairro = Bairro;
            _cidade = Cidade;
            _estado = Estado;
            _pais = Pais;
            _telefone = Telefone;
            _contato = Contato;
            _telefoneComercial = TelefoneComercial;
            _contatoComercial = ContatoComercial;
            _celular = Celular;
            _contatoCelular = ContatoCelular;
            _email = Email;
            _site = Site;
            _inscricaoEstadual = InscricaoEstadual;
            _inscricaoMunicipal = InscricaoMunicipal;
            _obs = Obs;
            _categoria1 = Categoria1;
            _categoria2 = Categoria2;
            _categoria3 = Categoria3;
            _subCategoria1 = SubCategoria1;
            _subCategoria2 = SubCategoria2;
            _subCategoria3 = SubCategoria3;
        }

        public static List<string> CarregarSubCategoria(int _idCategoria)
        {
            string comando = "Select nome from tbl_subgrupo_categoria where subgrupo = '" + _idCategoria + "';";

            return conexao.PreencheCMB(comando);
        }

        public static List<string> CarregarCategoria()
        {
            string comando = "Select nome from tbl_subgrupos;";

            return conexao.PreencheCMB(comando);
        }

        /// <summary>
        /// Método responsável por descobrir o endereço baseado no CEP informado
        /// </summary>
        /// <param name="_CEP"></param>
        /// <returns>CEP</returns>
        public static bool LocalizarCEP(string _CEP)
        {
            bool resultado = false;

            if (_CEP.Length == 8)
            {
                CEP dados = new CEP(_CEP);

                if (dados.cep != null)
                {
                    Estado = dados.uf;
                    Cidade = dados.localidade;
                    Bairro = dados.bairro;
                    Endereco = dados.logradouro;
                    Complemento = dados.complemento;

                    resultado = true;
                }
            }

            return resultado;
        }

        /// <summary>
        /// Método responsável por verificar o CNPJ na receita federal e importar os dados do CNPJ informado
        /// </summary>
        /// <param name="_CNPJ"></param>
        /// <returns>CNPJ da empresa/MEI</returns>
        public static bool PesquisarCNPJ(string _CNPJ)
        {
            bool resultado = false;
            string erro = null;
            int separador = int.MinValue;


            if (FuncoesEstaticas.ValidaCNPJ(_CNPJ))
            {
                string url = "https://www.receitaws.com.br/v1/cnpj/" + _CNPJ;

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
                        Nome = rootobject.nome;
                        Apelido = rootobject.fantasia;
                        DataNascimento = rootobject.abertura;
                        CEP = rootobject.cep.Replace(".", "").Replace("-", "");
                        Endereco = rootobject.logradouro;
                        Numero = rootobject.numero.Replace("(", "").Replace(")", "").Replace(" ", "");
                        Complemento = rootobject.complemento;
                        Bairro = rootobject.bairro;
                        Cidade = rootobject.municipio;
                        Estado = rootobject.uf;
                        TelefoneComercial = rootobject.telefone;
                        Email = rootobject.email;
                    }
                }
                catch (Exception)
                {
                    erro = ListaErro.RetornaErro(53);
                    separador = erro.IndexOf(":");
                    MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    resultado = true;
                }
            }

            return resultado;
        }

        /// <summary>
        /// Método responsável por cadastrar o fornecedor
        /// </summary>
        public static void CadastrarFornecedor()
        {
            string comando = null, _dataCadastro = "", _dataNascimento = null, erro = "";
            int separador = 0;

            try
            {
                _dataCadastro = DataCadastro.Substring(6, 4) + "-" + DataCadastro.Substring(3, 2) + "-" + DataCadastro.Substring(0, 2);

                if (!string.IsNullOrEmpty(DataNascimento))
                {
                    _dataNascimento = DataNascimento.Substring(6, 4) + "-" + DataNascimento.Substring(3, 2) + "-" + DataNascimento.Substring(0, 2);
                }

                if (Categoria1 == -1)
                {
                    Categoria1 = 0;
                }
                if (Categoria2 == -1)
                {
                    Categoria2 = 0;
                }
                if (Categoria3 == -1)
                {
                    Categoria3 = 0;
                }
                if (SubCategoria1 == -1)
                {
                    SubCategoria1 = 0;
                }
                if (SubCategoria2 == -1)
                {
                    SubCategoria2 = 0;
                }
                if (SubCategoria3 == -1)
                {
                    SubCategoria3 = 0;
                }

                comando = "insert into tbl_fornecedor values (0," + Tipo + ", '" + _dataCadastro + "',if('" + _dataNascimento + "' = '',NULL,'" + _dataNascimento + "'),'" + Documento + "','" + Nome + "','" + apelido + "','" + cep + "','"
                    + Endereco + "','" + Numero + "','" + Complemento + "','" + Bairro + "','" + Cidade + "','" + Estado + "','" + Pais + "','" + Telefone + "','" + Contato + "','"
                    + TelefoneComercial + "','" + ContatoComercial + "','" + Celular + "','" + ContatoCelular + "','" + Email + "','" + Site + "','" + InscricaoEstadual + "','" + InscricaoMunicipal + "','"
                    + Obs + "','" + Categoria1 + "','" + Categoria2 + "','" + Categoria3 + "','" + SubCategoria1 + "','" + SubCategoria2 + "','" + SubCategoria3 + "','S'); ";
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
                comando = "Select ID from tbl_fornecedor where tipo = '" + Tipo + "'" +
                " AND nome = '" + Nome + "'" +
                " AND datacadastro = '" + _dataCadastro + "';";

                ID = Int32.Parse(conexao.ConsultaSimples(comando));
            }
            catch (Exception)
            {
                erro = ListaErro.RetornaErro(51);
                separador = erro.IndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }

            if (ID != 0)
            {
                //Backup
                _id = ID;
                _tipo = Tipo;
                _dataCadastro = DataCadastro;
                _dataNascimento = DataNascimento;
                _documento = Documento;
                _nome = Nome;
                _apelido = Apelido;
                _cep = CEP;
                _endereco = Endereco;
                _numero = Numero;
                _complemento = Complemento;
                _bairro = Bairro;
                _cidade = Cidade;
                _estado = Estado;
                _pais = Pais;
                _telefone = Telefone;
                _contato = Contato;
                _telefoneComercial = TelefoneComercial;
                _contatoComercial = ContatoComercial;
                _celular = Celular;
                _contatoCelular = ContatoCelular;
                _email = Email;
                _site = Site;
                _inscricaoEstadual = InscricaoEstadual;
                _inscricaoMunicipal = InscricaoMunicipal;
                _obs = Obs;
                _categoria1 = Categoria1;
                _categoria2 = Categoria2;
                _categoria3 = Categoria3;
                _subCategoria1 = SubCategoria1;
                _subCategoria2 = SubCategoria2;
                _subCategoria3 = SubCategoria3;
            }
        }

        public static void ApagarFornecedor(int _id)
        {
            string comando = null, erro = "";
            int separador = 0;

            try
            {
                comando = "delete from tbl_fornecedor where ID = '" + _id + "';";
                conexao.ExecutaComando(comando);
            }
            catch (Exception)
            {
                erro = ListaErro.RetornaErro(57);
                separador = erro.IndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        /// <summary>
        /// Método responsável por verificar se há alterações entre os dados originais e os dados atuais
        /// </summary>
        /// <returns>Se há mudança ou não</returns>
        public static bool AvaliarMudancas()
        {
            bool resultado = false;

            if(Categoria1 == -1)
            {
                Categoria1 = 0;
            }
            else if (Categoria1 > 0)
            {
                Categoria1++;
            }
            if (Categoria2 == -1)
            {
                Categoria2 = 0;
            }
            else if (Categoria2 > 0)
            {
                Categoria2++;
            }
            if (Categoria3 == -1)
            {
                Categoria3 = 0;
            }
            else if (Categoria3 > 0)
            {
                Categoria3++;
            }
            if (SubCategoria1 == -1)
            {
                SubCategoria1 = 0;
            }
            else if (SubCategoria1 > 0)
            {
                SubCategoria1++;
            }
            if (SubCategoria2 == -1)
            {
                SubCategoria2 = 0;
            }
            else if(SubCategoria2 > 0)
            {
                SubCategoria2++;
            }
            if (SubCategoria3 == -1)
            {
                SubCategoria3 = 0;
            }
            else if (SubCategoria3 > 0)
            {
                SubCategoria3++;
            }

            if (Tipo != _tipo || Categoria1 != _categoria1 || Categoria2 != _categoria2 || Categoria3 != _categoria3 || SubCategoria1 != _subCategoria1 ||
                 SubCategoria2 != _subCategoria2 || SubCategoria3 != _subCategoria3 || Documento != _documento || Nome != _nome || Apelido != _apelido ||
                 DataNascimento != _dataNascimento ||CEP != _cep || Endereco != _endereco || Numero != _numero || Complemento != _complemento || 
                 Bairro != _bairro || Cidade != _cidade || Estado != _estado || Pais != _pais || Telefone != _telefone || Contato != _contato || 
                 TelefoneComercial != _telefoneComercial || ContatoComercial != _contatoComercial || Celular != _celular || ContatoCelular != _contatoCelular || 
                 Email != _email || Site != _site ||InscricaoEstadual != _inscricaoEstadual || InscricaoMunicipal != _inscricaoMunicipal || Obs != _obs)
            {
                resultado = true;
            }
            
            return resultado;
        }

        /// <summary>
        /// Método responsável por limpar todas as variáveis relacionadas aos Fornecedores
        /// </summary>
        public static void LimparVariaveis()
        {
            id = 0;
            tipo = 0;
            categoria1 = 0;
            categoria2 = 0;
            categoria3 = 0;
            subCategoria1 = 0;
            subCategoria2 = 0;
            subCategoria3 = 0;
            _id = 0;
            _tipo = 0;
            _categoria1 = 0;
            _categoria2 = 0;
            _categoria3 = 0;
            _subCategoria1 = 0;
            _subCategoria2 = 0;
            _subCategoria3 = 0;

            documento = null;
            nome = null;
            apelido = null;
            dataCadastro = null;
            dataNascimento = null;
            cep = null;
            endereco = null;
            numero = null;
            complemento = null;
            bairro = null;
            cidade = null;
            estado = null;
            pais = null;
            telefone = null;
            contato = null;
            telefoneComercial = null;
            contatoComercial = null;
            celular = null;
            contatoCelular = null;
            email = null;
            site = null;
            inscricaoEstadual = null;
            inscricaoMunicipal = null;
            obs = null;
            _documento = null;
            _nome = null;
            _apelido = null;
            _dataCadastro = null;
            _dataNascimento = null;
            _cep = null;
            _endereco = null;
            _numero = null;
            _complemento = null;
            _bairro = null;
            _cidade = null;
            _estado = null;
            _pais = null;
            _telefone = null;
            _contato = null;
            _telefoneComercial = null;
            _contatoComercial = null;
            _celular = null;
            _contatoCelular = null;
            _email = null;
            _site = null;
            _inscricaoEstadual = null;
            _inscricaoMunicipal = null;
            _obs = null;
    }

        /// <summary>
        /// Método responsável por atualizar o fornecedor
        /// </summary>
        public static void AtualizarFornecedor()
        {
            string comando = null, _dataNascimento = null, erro = "";
            int separador = 0;

            try
            {
                if (!string.IsNullOrEmpty(DataNascimento))
                {
                    _dataNascimento = DataNascimento.Substring(6, 4) + "-" + DataNascimento.Substring(3, 2) + "-" + DataNascimento.Substring(0, 2);
                }

                comando = "update tbl_fornecedor set tipo = '" + Tipo + "', dataNascimento = '" + _dataNascimento + "', documento = '" + Documento + "'," +
                    " nome = '" + Nome + "', apelido = '" + Apelido + "', cep = '" + CEP + "', endereco = '" + Endereco + "', numero = '" + Numero + "', complemento = '" + Complemento + "'," +
                    " bairro = '" + Bairro + "', cidade = '" + Cidade + "', estado = '" + Estado + "', pais = '" + Pais + "', telefone = '" + Telefone + "', contato = '" + Contato + "'," +
                    " telefoneComercial = '" + TelefoneComercial + "', contatoComercial = '" + ContatoComercial + "', celular = '" + Celular + "', contatoCelular = '" + ContatoCelular + "'," +
                    " email = '" + Email + "', site = '" + Site + "', inscricaoEstadual = '" + InscricaoEstadual + "', inscricaoMunicipal = '" + InscricaoMunicipal + "', observacoes = '" + Obs + "'," +
                    " categoria1 = '" + Categoria1 + "', categoria2 = '" + Categoria2 + "', categoria3 = '" + Categoria3 + "', subcategoria1 = '" + SubCategoria1 + "'," +
                    " subcategoria2 = '" + SubCategoria2 + "', subcategoria3 = '" + SubCategoria3 + "' where id = '" + ID + "';";
                conexao.ExecutaComando(comando);
            }
            catch (Exception)
            {
                erro = ListaErro.RetornaErro(51);
                separador = erro.IndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        /// <summary>
        /// Método responsável por travar o fornecedor
        /// </summary>
        /// <param name="_idFornecedor">ID da tarefa que deseja travar</param>
        /// <returns></returns>
        public static bool TravaFornecedor()
        {
            bool resultado = false;

            if (!conexao.FornecedorBloqueado(ID))
            {
                conexao.ExecutaComando("Update tbl_fornecedor set travar = 'S' where id = '" + ID.ToString() + "';");
                resultado = true;
            }

            return resultado;
        }

        /// <summary>
        /// Método responsável por destravar o fornecedor
        /// </summary>
        /// <returns></returns>
        public static void DestravaFornecedor()
        { 
            if (conexao.FornecedorBloqueado(ID))
            {
                conexao.ExecutaComando("Update tbl_fornecedor set travar = 'N' where id = '" + ID.ToString() + "';");
            }
        }

        /// <summary>
        /// Método responsável por destravar todos os Fornecedores do banco de dados.
        /// Utilizar apenas se ocorrer algum desligamento inesperado de algum usuário.
        /// </summary>
        /// <returns>Se verdadeiro, a operação foi um sucesso.</returns>
        public static bool DestravaTodosFornecedores()
        {
            bool resultado = false;
            string comando = null;

            try
            {
                comando = "Update tbl_fornecedor set travar = 'N';";
                conexao.ExecutaComando(comando);
            }
            catch (Exception)
            {
                string erro = ListaErro.RetornaErro(19);
                int separador = erro.IndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                resultado = true;
            }

            return resultado;
        }


        #endregion
    }
}
