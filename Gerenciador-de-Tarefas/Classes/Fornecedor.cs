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
        private static BDCONN conexao = new BDCONN();

        // Variáveis de Backup
        private static int _id, _tipo;
        private static List<int> _categorias, _subcategorias;
        private static string _documento, _nome, _apelido, _dataCadastro, _dataNascimento, _cep, _endereco, _numero, _complemento, _bairro, _cidade, _estado, _pais,
           _telefone, _contato, _telefoneComercial, _contatoComercial, _celular, _contatoCelular, _email, _site, _inscricaoEstadual, _inscricaoMunicipal, _obs;
        #endregion

        #region Propriedades
        public static int id
        {
            get; set;
        }
        public static int tipo
        {
            get; set;
        }
        public static List<int> categorias
        {
            get; set;
        }
        public static List<int> subCategorias
        {
            get ; set ;
        }
        public static string documento
        {
            get; set;
        } 
        public static string nome
        {
            get; set;
        }
        public static string apelido
        {
            get; set;
        }
        public static string dataCadastro
        {
            get; set;
        }
        public static string dataNascimento
        {
            get; set;
        }
        public static string cep
        {
            get; set;
        }
        public static string endereco
        {
            get; set;
        }
        public static string numero
        {
            get; set;
        }
        public static string complemento
        {
            get; set;
        }
        public static string bairro
        {
            get; set;
        }
        public static string cidade
        {
            get; set;
        }
        public static string estado
        {
            get; set;
        }
        public static string pais
        {
            get; set;
        }
        public static string telefone
        {
            get; set;
        }
        public static string contato
        {
            get; set;
        }
        public static string telefoneComercial
        {
            get; set;
        }

     public static string contatoComercial
        {
            get; set;
        }
        public static string celular
        {
            get; set;
        }
        public static string contatoCelular
        {
            get; set;
        }
        public static string email
        {
            get; set;
        }
        public static string site
        {
            get; set;
        }
        public static string inscricaoEstadual
        {
            get; set;
        }
        public static string inscricaoMunicipal
        {
            get; set;
        }
        public static string obs
        {
            get; set;
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
                    comando = "Select tbl_fornecedor.id as 'ID', tbl_fornecedor.nome as 'Razão Social / Nome', " +
                        "IFNULL(tbl_fornecedor.Contato, IFNULL(tbl_fornecedor.ContatoComercial, IFNULL(tbl_fornecedor.ContatoCelular, ''))) as 'Contato', " +
                        "IFNULL(tbl_fornecedor.Telefone, IFNULL(tbl_fornecedor.TelefoneComercial, IFNULL(tbl_fornecedor.Celular, ''))) as 'Telefone', " +
                        "IFNULL(tbl_fornecedor.Email, '') as 'E-mail' " +
                        "from tbl_fornecedor ORDER BY tbl_fornecedor.nome ASC;";
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
                "contatocomercial, celular, contatocelular, email, site, inscricaoestadual, inscricaomunicipal, observacoes " +
                "from tbl_fornecedor where id = '" + _idFornecedor + "';");
            
            id = _idFornecedor;
            tipo = int.Parse(listaFornecedor[0]);
            dataCadastro = listaFornecedor[1];
            dataNascimento = listaFornecedor[2];
            documento = listaFornecedor[3];
            nome = listaFornecedor[4];
            apelido = listaFornecedor[5];
            cep = listaFornecedor[6];
            endereco = listaFornecedor[7];
            numero = listaFornecedor[8];
            complemento = listaFornecedor[9];
            bairro = listaFornecedor[10];
            cidade = listaFornecedor[11];
            estado = listaFornecedor[12];
            pais = listaFornecedor[13];
            telefone = listaFornecedor[14];
            contato = listaFornecedor[15];
            telefoneComercial = listaFornecedor[16];
            contatoComercial = listaFornecedor[17];
            celular = listaFornecedor[18];
            contatoCelular = listaFornecedor[19];
            email = listaFornecedor[20];
            site = listaFornecedor[21];
            inscricaoEstadual = listaFornecedor[22];
            inscricaoMunicipal = listaFornecedor[23];
            obs = listaFornecedor[24];

            //Backup
            _id = id;
            _tipo = tipo;
            _dataCadastro = dataCadastro.Substring(0,10);
            if(!string.IsNullOrEmpty(dataNascimento) && dataNascimento != "")
            {
                _dataNascimento = dataNascimento.Substring(0, 10);
            }
            else
            {
                _dataNascimento = dataNascimento;
            }
            _documento = documento;
            _nome = nome;
            _apelido = apelido;
            _cep = cep;
            _endereco = endereco;
            _numero = numero;
            _complemento = complemento;
            _bairro = bairro;
            _cidade = cidade;
            _estado = estado;
            _pais = pais;
            _telefone = telefone;
            _contato = contato;
            _telefoneComercial = telefoneComercial;
            _contatoComercial = contatoComercial;
            _celular = celular;
            _contatoCelular = contatoCelular;
            _email = email;
            _site = site;
            _inscricaoEstadual = inscricaoEstadual;
            _inscricaoMunicipal = inscricaoMunicipal;
            _obs = obs;
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
                    estado = dados.uf;
                    cidade = dados.localidade;
                    bairro = dados.bairro;
                    endereco = dados.logradouro;
                    complemento = dados.complemento;
                    pais = "Brasil";

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
                        nome = rootobject.nome;
                        apelido = rootobject.fantasia;
                        dataNascimento = rootobject.abertura;
                        cep = rootobject.cep.Replace(".", "").Replace("-", "");
                        endereco = rootobject.logradouro;
                        numero = rootobject.numero.Replace("(", "").Replace(")", "").Replace(" ", "");
                        complemento = rootobject.complemento;
                        bairro = rootobject.bairro;
                        cidade = rootobject.municipio;
                        estado = rootobject.uf;
                        telefoneComercial = rootobject.telefone;
                        email = rootobject.email;
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
                _dataCadastro = dataCadastro.Substring(6, 4) + "-" + dataCadastro.Substring(3, 2) + "-" + dataCadastro.Substring(0, 2);

                if (!string.IsNullOrEmpty(dataNascimento))
                {
                    _dataNascimento = dataNascimento.Substring(6, 4) + "-" + dataNascimento.Substring(3, 2) + "-" + dataNascimento.Substring(0, 2);
                }

                comando = "insert into tbl_fornecedor values (0," + tipo + ", '" + _dataCadastro + "',if('" + _dataNascimento + "' = '',NULL,'" + _dataNascimento + "'),'" + documento + "','" + nome + "','" + apelido + "','" + cep + "','"
                    + endereco + "','" + numero + "','" + complemento + "','" + bairro + "','" + cidade + "','" + estado + "','" + pais + "','" + telefone + "','" + contato + "','"
                    + telefoneComercial + "','" + contatoComercial + "','" + celular + "','" + contatoCelular + "','" + email + "','" + site + "','" + inscricaoEstadual + "','" + inscricaoMunicipal + "','"
                    + obs + "','S'); ";
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
                comando = "Select id from tbl_fornecedor where tipo = '" + tipo + "'" +
                " AND nome = '" + nome + "'" +
                " AND datacadastro = '" + _dataCadastro + "';";

                id = Int32.Parse(conexao.ConsultaSimples(comando));
            }
            catch (Exception)
            {
                erro = ListaErro.RetornaErro(51);
                separador = erro.IndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }

            if (id != 0)
            {
                //Backup
                _id = id;
                _tipo = tipo;
                _dataCadastro = dataCadastro;
                _dataNascimento = dataNascimento;
                _documento = documento;
                _nome = nome;
                _apelido = apelido;
                _cep = cep;
                _endereco = endereco;
                _numero = numero;
                _complemento = complemento;
                _bairro = bairro;
                _cidade = cidade;
                _estado = estado;
                _pais = pais;
                _telefone = telefone;
                _contato = contato;
                _telefoneComercial = telefoneComercial;
                _contatoComercial = contatoComercial;
                _celular = celular;
                _contatoCelular = contatoCelular;
                _email = email;
                _site = site;
                _inscricaoEstadual = inscricaoEstadual;
                _inscricaoMunicipal = inscricaoMunicipal;
                _obs = obs;
            }
        }

        public static void ApagarFornecedor(int _id)
        {
            string comando = null, erro = "";
            int separador = 0;

            try
            {
                comando = "delete from tbl_fornecedor where id = '" + _id + "';";
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

            if (tipo != _tipo || documento != _documento || nome != _nome || apelido != _apelido ||
                 dataNascimento != _dataNascimento || cep != _cep || endereco != _endereco || numero != _numero || complemento != _complemento || 
                 bairro != _bairro || cidade != _cidade || estado != _estado || pais != _pais || telefone != _telefone || contato != _contato || 
                 telefoneComercial != _telefoneComercial || contatoComercial != _contatoComercial || celular != _celular || contatoCelular != _contatoCelular || 
                 email != _email || site != _site || inscricaoEstadual != _inscricaoEstadual || inscricaoMunicipal != _inscricaoMunicipal || obs != _obs)
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
                id = 0;
                tipo = 0;
                documento = "";
                nome = "";
                apelido = "";
                dataCadastro = "";
                dataNascimento = "";
                cep = "";
                endereco = "";
                numero = "";
                complemento = "";
                bairro = "";
                cidade = "";
                estado = "";
                pais = "";
                telefone = "";
                contato = "";
                telefoneComercial = "";
                contatoComercial = "";
                celular = "";
                contatoCelular = "";
                email = "";
                site = "";
                inscricaoEstadual = "";
                inscricaoMunicipal = "";
                obs = "";

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
                _inscricaoMunicipal = "";
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
            string comando = null, _dataNascimento = null, erro = "";
            int separador = 0;

            try
            {
                if (!string.IsNullOrEmpty(dataNascimento))
                {
                    _dataNascimento = dataNascimento.Substring(6, 4) + "-" + dataNascimento.Substring(3, 2) + "-" + dataNascimento.Substring(0, 2);
                }

                comando = "update tbl_fornecedor set tipo = '" + tipo + "', dataNascimento = '" + _dataNascimento + "', documento = '" + documento + "'," +
                    " nome = '" + nome + "', apelido = '" + apelido + "', cep = '" + cep + "', endereco = '" + endereco + "', numero = '" + numero + "', complemento = '" + complemento + "'," +
                    " bairro = '" + bairro + "', cidade = '" + cidade + "', estado = '" + estado + "', pais = '" + pais + "', telefone = '" + telefone + "', contato = '" + contato + "'," +
                    " telefoneComercial = '" + telefoneComercial + "', contatoComercial = '" + contatoComercial + "', celular = '" + celular + "', contatoCelular = '" + contatoCelular + "'," +
                    " email = '" + email + "', site = '" + site + "', inscricaoEstadual = '" + inscricaoEstadual + "', inscricaoMunicipal = '" + inscricaoMunicipal + "', observacoes = '" + obs + "'," +
                    " where id = '" + id + "';";
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
        /// <param name="_idFornecedor">id da tarefa que deseja travar</param>
        /// <returns></returns>
        public static bool TravaFornecedor()
        {
            bool resultado = false;

            if (!conexao.FornecedorBloqueado(id))
            {
                conexao.ExecutaComando("Update tbl_fornecedor set travar = 'S' where id = '" + id.ToString() + "';");
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
            if (conexao.FornecedorBloqueado(id))
            {
                conexao.ExecutaComando("Update tbl_fornecedor set travar = 'N' where id = '" + id.ToString() + "';");
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
