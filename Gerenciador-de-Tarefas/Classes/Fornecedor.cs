using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace Gerenciador_de_Tarefas.Classes
{
    public static class Fornecedor
    {
        #region Variaveis
        // Originais
        private static DataGridView dgvFornecedoresAtualizada = new DataGridView();

        // Backup
        private static DataGridView _dgvFornecedoresAtual = new DataGridView();
        private static int _id, _tipo;
        //private static List<int> _categorias, _subcategorias;
        private static string _documento, _nome, _apelido, _dataCadastro, _dataNascimento, _cep, 
            _endereco, _numero, _complemento, _bairro, _cidade, _estado, _pais, _telefone, _contato, 
            _telefoneComercial, _contatoComercial, _celular, _contatoCelular, _email, _site, 
            _inscricaoEstadual, _inscricaoMunicipal, _obs;

        #endregion

        #region Propriedades
        public static bool PrimeiraPagina
        {
            get; set;
        }
        public static DataGridView DGVAtualizada
        {
            get
            {
                return _dgvFornecedoresAtual;
            }
        }
        public static int ID
        {
            get; set;
        }
        public static int FornecedorPesquisado
        {
            get; set;
        }
        public static int Tipo
        {
            get; set;
        }
        public static List<int> Categorias
        {
            get; set;
        }
        public static List<int> SubCategorias
        {
            get ; set ;
        }
        public static string Apelido
        {
            get; set;
        }
        public static string Bairro
        {
            get; set;
        }
        public static string CEP
        {
            get; set;
        }
        public static string Celular
        {
            get; set;
        }
        public static string Cidade
        {
            get; set;
        }
        public static string Contato
        {
            get; set;
        }
        public static string ContatoCelular
        {
            get; set;
        }
        public static string ContatoComercial
        {
            get; set;
        }
        public static string Complemento
        {
            get; set;
        }
        public static string DataCadastro
        {
            get; set;
        }
        public static string DataNascimento
        {
            get; set;
        }
        public static string Documento
        {
            get; set;
        }
        public static string Email
        {
            get; set;
        }
        public static string Endereco
        {
            get; set;
        }
        public static string Estado
        {
            get; set;
        }
        public static string InscricaoEstadual
        {
            get; set;
        }
        public static string InscricaoMunicipal
        {
            get; set;
        }
        public static string Nome
        {
            get; set;
        }
        public static string Numero
        {
            get; set;
        }
        public static string Obs
        {
            get; set;
        }
        public static string Pais
        {
            get; set;
        }
        public static string Telefone
        {
            get; set;
        }
        public static string TelefoneComercial
        {
            get; set;
        }
        public static string TextoImpressao
        {
            get; set;
        }
        public static string Site
        {
            get; set;
        }
        public static StringReader Leitor
        {
            get; set;
        }
        #endregion

        #region Funcoes
        /// <summary>
        /// Método responsável por atualizar a tela de Fornecedores
        /// </summary>
        /// 
        public static bool AtualizaDGVFornecedores(int posicaoCmbFiltroFornecedores)
        {
            if (Sistema.TestaConexao())
            {
                string comando = "";

                switch (posicaoCmbFiltroFornecedores)
                {
                    default:
                        comando = "Select tbl_fornecedor.ID as 'ID', tbl_fornecedor.Nome as 'Razão Social / Nome', " +
                            "IFNULL(tbl_fornecedor.Contato, IFNULL(tbl_fornecedor.ContatoComercial, IFNULL(tbl_fornecedor.ContatoCelular, ''))) as 'Contato', " +
                            "IFNULL(tbl_fornecedor.Telefone, IFNULL(tbl_fornecedor.TelefoneComercial, IFNULL(tbl_fornecedor.Celular, ''))) as 'Telefone', " +
                            "IFNULL(tbl_fornecedor.Email, '') as 'E-mail' " +
                            "from tbl_fornecedor ORDER BY tbl_fornecedor.Nome ASC;";
                        break;
                }

                if (Sistema.IniciaTelaFornecedores)
                {
                    _dgvFornecedoresAtual.DataSource = Sistema.PreencheDGV(comando);
                    dgvFornecedoresAtualizada.DataSource = _dgvFornecedoresAtual.DataSource;

                    //Seta que a tela já foi aberta
                    Sistema.IniciaTelaFornecedores = false;

                    return true;
                }
                else
                {
                    DataGridView _dgvTemp = new DataGridView
                    {
                        //Atualiza a tabela atual temporária
                        DataSource = Sistema.PreencheDGV(comando)
                    };

                    //Se a tabela atualizada for diferente da tabela anterior
                    if (_dgvFornecedoresAtual != _dgvTemp)
                    {
                        dgvFornecedoresAtualizada.DataSource = _dgvTemp.DataSource;
                        _dgvFornecedoresAtual = _dgvTemp;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Método responsável por filtrar a tabela de fornecedores
        /// </summary>
        /// <param name="posicaoCmbFiltroFornecedores"></param>
        /// <returns></returns>
        public static string FiltroFornecedores(int posicaoCmbFiltroFornecedores)
        {
            string comando = "";

            

            return comando;
        }

        /// <summary>
        /// Método responsável por buscar o fornecedor no datagridview e selecioná-lo
        /// </summary>
        /// <param name="dgvFornecedores">DataGridView dos Fornecedores</param>
        /// <param name="listaPesquisa">Itens a serem pesquisados</param>
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

        /// <summary>
        /// Método responsável por carregar os dados do fornecedor selecionado
        /// </summary>
        /// <param name="_idFornecedor">ID do fornecedor</param>
        public static void AbrirFornecedor()
        {
            List<string> listaFornecedor = Sistema.ConsultaFornecedor("select Tipo, datacadastro, datanascimento, Documento, Nome, " +
                "Apelido, CEP, Endereco, Numero, Complemento, Bairro, Cidade, Estado, Pais, Telefone, Contato, Telefonecomercial, " +
                "Contatocomercial, Celular, ContatoCelular, Email, Site, inscricaoestadual, inscricaomunicipal, Observacoes " +
                "from tbl_fornecedor where ID = '" + ID + "';");

            try
            {
                Tipo = int.Parse(listaFornecedor[0]);
                DataCadastro = listaFornecedor[1].Substring(0, 10);
                if (!string.IsNullOrEmpty(listaFornecedor[2]) && listaFornecedor[2] != "")
                {
                    DataNascimento = listaFornecedor[2].Substring(0, 10);
                }
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

                //Backup
                _id = ID;
                _tipo = int.Parse(listaFornecedor[0]);
                _dataCadastro = listaFornecedor[1].Substring(0, 10);
                if (!string.IsNullOrEmpty(listaFornecedor[2]) && listaFornecedor[2] != "")
                {
                    _dataNascimento = listaFornecedor[2].Substring(0, 10);
                }

                _documento = listaFornecedor[3];
                _nome = listaFornecedor[4];
                _apelido = listaFornecedor[5];
                _cep = listaFornecedor[6];
                _endereco = listaFornecedor[7];
                _numero = listaFornecedor[8];
                _complemento = listaFornecedor[9];
                _bairro = listaFornecedor[10];
                _cidade = listaFornecedor[11];
                _estado = listaFornecedor[12];
                _pais = listaFornecedor[13];
                _telefone = listaFornecedor[14];
                _contato = listaFornecedor[15];
                _telefoneComercial = listaFornecedor[16];
                _contatoComercial = listaFornecedor[17];
                _celular = listaFornecedor[18];
                _contatoCelular = listaFornecedor[19];
                _email = listaFornecedor[20];
                _site = listaFornecedor[21];
                _inscricaoEstadual = listaFornecedor[22];
                _inscricaoMunicipal = listaFornecedor[23];
                _obs = listaFornecedor[24];

                Log.AbrirFornecedor(ID);
            }
            catch (ArgumentOutOfRangeException x)
            {
                MessageBox.Show(x.ToString());
                throw;
            }
        }
        
        //Categorias
        /*
        public static List<string> CarregarSubCategoria(int _idCategoria)
        {
            string comando = "Select Nome from tbl_subgrupo_categoria where subgrupo = '" + _idCategoria + "';";

            return Sistema.PreencheCMB(comando);
        }

        public static List<string> CarregarCategoria()
        {
            string comando = "Select Nome from tbl_subgrupos;";

            return Sistema.PreencheCMB(comando);
        }
        */

        /// <summary>
        /// Método responsável por descobrir o endereço baseado no CEP informado
        /// </summary>
        /// <param name="_cep"></param>
        /// <returns>CEP</returns>
        public static bool LocalizarCEP(string _cep)
        {
            bool resultado = false;

            if (_cep.Length == 8)
            {
                CEP dados = new CEP(_cep);

                if (dados.cep != null)
                {
                    Estado = dados.uf;
                    Cidade = dados.localidade;
                    Bairro = dados.bairro;
                    Endereco = dados.logradouro;
                    Complemento = dados.complemento;
                    Pais = "Brasil";

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

            if (Funcoes.ValidaCNPJ(_CNPJ))
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
                        Nome = rootobject.Nome;
                        Apelido = rootobject.Fantasia;
                        DataNascimento = rootobject.Abertura;
                        CEP = rootobject.CEP.Replace(".", "").Replace("-", "");
                        Endereco = rootobject.Logradouro;
                        Numero = rootobject.Numero.Replace("(", "").Replace(")", "").Replace(" ", "");
                        Complemento = rootobject.Complemento;
                        Bairro = rootobject.Bairro;
                        Cidade = rootobject.Municipio;
                        Estado = rootobject.UF;
                        TelefoneComercial = rootobject.Telefone;
                        Email = rootobject.Email;
                    }
                }
                catch (Exception)
                {
                    ListaErro.RetornaErro(53);
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
            string comando = null, _dataCadastro = "", _dataNascimento = "";

            try
            {
                _dataCadastro = DataCadastro.Substring(6, 4) + "-" + DataCadastro.Substring(3, 2) + "-" + DataCadastro.Substring(0, 2);

                if (!string.IsNullOrEmpty(DataNascimento))
                {
                    if(DataNascimento.Length > 8)
                    {
                        _dataNascimento = DataNascimento.Substring(6, 4) + "-" + DataNascimento.Substring(3, 2) + "-" + DataNascimento.Substring(0, 2);
                    }
                    else
                    {
                        _dataNascimento = DataNascimento.Substring(4, 4) + "-" + DataNascimento.Substring(2, 2) + "-" + DataNascimento.Substring(0, 2);
                    }
                }

                comando = "insert into tbl_fornecedor values (0," + Tipo + ", '" + _dataCadastro + "',if('" + _dataNascimento + "' = '',NULL,'" + _dataNascimento + "'),'" + Documento + "','" + Nome + "','" + Apelido + "','" + CEP + "','"
                    + Endereco + "','" + Numero + "','" + Complemento + "','" + Bairro + "','" + Cidade + "','" + Estado + "','" + Pais + "','" + Telefone + "','" + Contato + "','"
                    + TelefoneComercial + "','" + ContatoComercial + "','" + Celular + "','" + ContatoCelular + "','" + Email + "','" + Site + "','" + InscricaoEstadual + "','" + InscricaoEstadual + "','"
                    + Obs + "','S'); ";
                Sistema.ExecutaComando(comando);
            }
            catch (Exception)
            {
                ListaErro.RetornaErro(51);
                throw;
            }

            try
            {
                comando = "Select ID from tbl_fornecedor where Tipo = '" + Tipo + "'" +
                " AND Nome = '" + Nome + "'" +
                " AND datacadastro = '" + _dataCadastro + "';";

                ID = int.Parse(Sistema.ConsultaSimples(comando));
            }
            catch (Exception)
            {
                ListaErro.RetornaErro(51);
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
                _inscricaoEstadual = InscricaoEstadual;
                _obs = Obs;
            }
        }

        /// <summary>
        /// Método responsável por apagar o fornecedor
        /// </summary>
        public static void ApagarFornecedor(int _id)
        {
            try
            {
                string comando = "delete from tbl_fornecedor where ID = '" + _id + "';";
                Sistema.ExecutaComando(comando);
            }
            catch (Exception)
            {
                ListaErro.RetornaErro(57);
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
            if(DataNascimento == "  /  /")
            {
                DataNascimento = "";
            }

            if (Tipo != _tipo || Documento != _documento || Nome != _nome || Apelido != _apelido ||
                 DataNascimento != _dataNascimento || CEP != _cep || Endereco != _endereco || Numero != _numero || Complemento != _complemento || 
                 Bairro != _bairro || Cidade != _cidade || Estado != _estado || Pais != _pais || Telefone != _telefone || Contato != _contato || 
                 TelefoneComercial != _telefoneComercial || ContatoComercial != _contatoComercial || Celular != _celular || ContatoCelular != _contatoCelular || 
                 Email != _email || Site != _site || InscricaoEstadual != _inscricaoEstadual || InscricaoEstadual != _inscricaoEstadual || Obs != _obs)
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
            try
            {
                ID = 0;
                Tipo = 0;
                Documento = "";
                Nome = "";
                Apelido = "";
                DataCadastro = "";
                DataNascimento = "";
                CEP = "";
                Endereco = "";
                Numero = "";
                Complemento = "";
                Bairro = "";
                Cidade = "";
                Estado = "";
                Pais = "";
                Telefone = "";
                Contato = "";
                TelefoneComercial = "";
                ContatoComercial = "";
                Celular = "";
                ContatoCelular = "";
                Email = "";
                Site = "";
                InscricaoEstadual = "";
                InscricaoEstadual = "";
                Obs = "";

                _id = 0;
                _tipo = 0;
                _documento = "";
                _nome = "";
                _apelido = "";
                _dataCadastro = "";
                _dataNascimento = "";
                _cep = "";
                _endereco = "";
                _numero = "";
                _complemento = "";
                _bairro = "";
                _cidade = "";
                _estado = "";
                _pais = "";
                _telefone = "";
                _contato = "";
                _telefoneComercial = "";
                _contatoComercial = "";
                _celular = "";
                _contatoCelular = "";
                _email = "";
                _site = "";
                _inscricaoEstadual = "";
                _inscricaoEstadual = "";
                _obs = "";
            }
            catch (Exception)
            {

                throw;
            }  
        }

        /// <summary>
        /// Método responsável por atualizar o fornecedor
        /// </summary>
        public static void AtualizarFornecedor()
        {
            string comando = null, _dataNascimento = null;

            try
            {

                if (!string.IsNullOrEmpty(DataNascimento))
                {
                    if (DataNascimento.Length > 8)
                    {
                        _dataNascimento = DataNascimento.Substring(6, 4) + "-" + DataNascimento.Substring(3, 2) + "-" + DataNascimento.Substring(0, 2);
                    }
                    else
                    {
                        _dataNascimento = DataNascimento.Substring(4, 4) + "-" + DataNascimento.Substring(2, 2) + "-" + DataNascimento.Substring(0, 2);
                    }

                    comando = string.Format("update tbl_fornecedor set Tipo = '{0}', DataNascimento = '{1}', Documento = '{2}', Nome = '{3}', Apelido = '{4}', CEP = '{5}'," +
                    " Endereco = '{6}', Numero = '{7}', Complemento = '{8}', Bairro = '{9}', Cidade = '{10}', Estado = '{11}', Pais = '{12}', Telefone = '{13}', " +
                    "Contato = '{14}', TelefoneComercial = '{15}', ContatoComercial = '{16}', Celular = '{17}', ContatoCelular = '{18}', Email = '{19}', Site = '{20}'," +
                    "InscricaoEstadual = '{21}', InscricaoEstadual = '{22}', Observacoes = '{23}' where ID = '{24}';"
                    , Tipo, _dataNascimento, Documento, Nome, Apelido, CEP, Endereco, Numero, Complemento, Bairro, Cidade, Estado, Pais, Telefone, Contato, TelefoneComercial,
                    ContatoComercial, Celular, ContatoCelular, Email, Site, InscricaoEstadual, InscricaoEstadual, Obs, ID);
                }
                else
                {
                    comando = string.Format("update tbl_fornecedor set Tipo = '{0}', Documento = '{1}', Nome = '{2}', Apelido = '{3}', CEP = '{4}'," +
                    " Endereco = '{5}', Numero = '{6}', Complemento = '{7}', Bairro = '{8}', Cidade = '{9}', Estado = '{10}', Pais = '{11}', Telefone = '{12}', " +
                    "Contato = '{13}', TelefoneComercial = '{14}', ContatoComercial = '{15}', Celular = '{16}', ContatoCelular = '{17}', Email = '{18}', Site = '{19}'," +
                    "InscricaoEstadual = '{20}', InscricaoEstadual = '{21}', Observacoes = '{22}' where ID = '{23}';"
                    , Tipo, Documento, Nome, Apelido, CEP, Endereco, Numero, Complemento, Bairro, Cidade, Estado, Pais, Telefone, Contato, TelefoneComercial,
                    ContatoComercial, Celular, ContatoCelular, Email, Site, InscricaoEstadual, InscricaoEstadual, Obs, ID);
                }

                
                Sistema.ExecutaComando(comando);
            }
            catch (Exception)
            {
                ListaErro.RetornaErro(51);
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

            if (!FornecedorBloqueado())
            {
                Sistema.ExecutaComando("Update tbl_fornecedor set travar = 'S' where ID = '" + ID.ToString() + "';");
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
            if (FornecedorBloqueado())
            {
                Sistema.ExecutaComando("Update tbl_fornecedor set travar = 'N' where ID = '" + ID.ToString() + "';");
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
                Sistema.ExecutaComando(comando);
            }
            catch (Exception)
            {
                ListaErro.RetornaErro(19);
            }
            finally
            {
                resultado = true;
            }

            return resultado;
        }

        public static List<string> DadosImpressao()
        {
            List<string> lista = new List<string>
            {
                "ID: " + ID
            };

            if (Tipo == 0)
            {
                // 1
                lista.Add("Razão Social: " + Nome);
                // 2
                lista.Add("Nome Fantasia: " + Apelido);
                // 3
                if (!string.IsNullOrEmpty(Documento))
                {
                    if (Documento.Contains(".") && Documento.Contains("/") && Documento.Contains("-"))
                    {
                        lista.Add("CNPJ: " + Documento);
                    }
                    else
                    {
                        lista.Add("CNPJ: " + Documento.FormataCNPJ());
                    }
                }
                else
                {
                    lista.Add("CNPJ: ");
                }
                // 4
                if (!string.IsNullOrEmpty(DataNascimento))
                {
                    if (DataNascimento.Contains("/"))
                    {
                        lista.Add("Fundação: " + DataNascimento);
                    }
                    else
                    {
                        lista.Add("Fundação: " + DataNascimento.FormataData());
                    }
                }
                else
                {
                    lista.Add("Fundação: ");
                }
            }
            else
            {
                // 1
                lista.Add("Nome: " + Nome);
                // 2
                lista.Add("Apelido: " + Apelido);
                // 3
                if (!string.IsNullOrEmpty(Documento))
                {
                    if (Documento.Contains(".") && Documento.Contains("-"))
                    {
                        lista.Add("CPF: " + Documento);
                    }
                    else
                    {
                        lista.Add("CPF: " + Documento.FormataCPF());
                    }
                }
                else
                {
                    lista.Add("CPF: ");
                }
                if (!string.IsNullOrEmpty(DataNascimento))
                {
                    if (DataNascimento.Contains("/"))
                    {
                        lista.Add("Aniversário: " + DataNascimento);
                    }
                    else
                    {
                        lista.Add("Aniversário: " + DataNascimento.FormataData());
                    }
                }
                else
                {
                    lista.Add("Aniversário: ");
                }

            }

            // 5
            if (DataCadastro.Contains("/"))
            {
                lista.Add("Cadastrado em: " + DataCadastro);
            }
            else
            {
                lista.Add("Cadastrado em: " + DataCadastro.FormataData());
            }
            // 6
            string textoEndereco = "Endereço: ";

            if (!string.IsNullOrEmpty(Endereco))
            {
                textoEndereco += Endereco;
            }
            if (!string.IsNullOrEmpty(Numero))
            {
                textoEndereco += ", " + Numero;
            }
            if (!string.IsNullOrEmpty(Bairro))
            {
                textoEndereco += " - " + Bairro;
            }
            if (!string.IsNullOrEmpty(Cidade))
            {
                textoEndereco += " - " + Cidade;
            }
            if (!string.IsNullOrEmpty(Estado))
            {
                textoEndereco += " / " + Estado;
            }
            if (!string.IsNullOrEmpty(CEP))
            {
                if (CEP.Contains("-"))
                {
                    textoEndereco += " - " + CEP;
                }
                else
                {
                    textoEndereco += " - " + CEP.FormataCEP();
                }
            }
            lista.Add(textoEndereco);

            // 7
            if (!string.IsNullOrEmpty(Telefone) && !string.IsNullOrEmpty(Contato))
            {
                if (Telefone.Length > 11)
                {
                    lista.Add(Telefone.FormataNumeroCelular() + " - " + Contato);
                }
                else
                {
                    lista.Add(Telefone.FormataNumeroTelefone() + " - " + Contato);
                }

            }
            else if (!string.IsNullOrEmpty(Telefone) && string.IsNullOrEmpty(Contato))
            {
                if (Telefone.Length > 11)
                {
                    lista.Add(Telefone.FormataNumeroCelular());
                }
                else
                {
                    lista.Add(Telefone.FormataNumeroTelefone());
                }
            }
            else if (string.IsNullOrEmpty(Telefone) && !string.IsNullOrEmpty(Contato))
            {
                lista.Add("Contato Principal:" + Contato);
            }
            else
            {
                lista.Add("");
            }

            // 8
            if (!string.IsNullOrEmpty(TelefoneComercial) && !string.IsNullOrEmpty(ContatoComercial))
            {
                if (TelefoneComercial.Length > 11)
                {
                    lista.Add(TelefoneComercial.FormataNumeroCelular() + " - " + ContatoComercial);
                }
                else
                {
                    lista.Add(TelefoneComercial.FormataNumeroTelefone() + " - " + ContatoComercial);
                }
            }
            else if (!string.IsNullOrEmpty(TelefoneComercial) && string.IsNullOrEmpty(ContatoComercial))
            {
                if (TelefoneComercial.Length > 11)
                {
                    lista.Add(TelefoneComercial.FormataNumeroCelular());
                }
                else
                {
                    lista.Add(TelefoneComercial.FormataNumeroTelefone());
                }
            }
            else if (string.IsNullOrEmpty(TelefoneComercial) && !string.IsNullOrEmpty(ContatoComercial))
            {
                if (lista[7].Contains("Contato"))
                {
                    lista.Add("Contato 2:" + ContatoComercial);
                }
                else
                {
                    lista.Add("Contato:" + ContatoComercial);
                }
            }
            else
            {
                lista.Add("");
            }

            // 9
            if (!string.IsNullOrEmpty(Celular) && !string.IsNullOrEmpty(ContatoCelular))
            {

                if (Celular.Length > 11)
                {
                    lista.Add(Celular.FormataNumeroCelular() + " - " + ContatoCelular);
                }
                else
                {
                    lista.Add(Celular.FormataNumeroTelefone() + " - " + ContatoCelular);
                }
            }
            else if (!string.IsNullOrEmpty(Celular) && string.IsNullOrEmpty(ContatoCelular))
            {
                if (Celular.Length > 11)
                {
                    lista.Add(Celular.FormataNumeroCelular());
                }
                else
                {
                    lista.Add(Celular.FormataNumeroTelefone());
                }
            }
            else if (string.IsNullOrEmpty(Celular) && !string.IsNullOrEmpty(ContatoCelular))
            {
                if (lista[7].Contains("Contato") && lista[8].Contains("Contato 2"))
                {
                    lista.Add("Contato 3:" + ContatoCelular);
                }
                else if (lista[7].Contains("Contato") || lista[8].Contains("Contato"))
                {
                    lista.Add("Contato 2:" + ContatoCelular);
                }
                else
                {
                    lista.Add("Contato:" + ContatoCelular);
                }
            }
            else
            {
                lista.Add("");
            }

            // 10
            lista.Add("E-mail Principal: " + Email);
            // 11
            lista.Add("Site: " + Site);
            // 12
            lista.Add("Inscrição Estadual: " + InscricaoEstadual);

            /*string ID = "", Nome = "", Apelido = "", Documento = "", cadastro = "", DataNascimento = "",
               CEP = "", Endereco = "", Numero = "", Complemento = "", Bairro = "", Cidade = "", Estado = "",
               Pais = "", Telefone = "", Contato = "", TelefoneComercial = "", ContatoComercial = "", Celular = "",
               ContatoCelular = "", Email = "", Site = "", InscricaoEstadual = "", InscricaoMunicipal = "", Obs = "";*/

            return lista;
        }

        /// <summary>
        /// Método responsável por retornar se a tarefa esta bloqueada.
        /// </summary>
        /// <param name="idFornecedor">Qual a tarefa que deseja conferir.</param>
        public static bool FornecedorBloqueado()
        {
            string comando = "Select travar from tbl_fornecedor where ID = '" + ID + "';";
            if (Sistema.ConsultaSimples(comando) == "S")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
