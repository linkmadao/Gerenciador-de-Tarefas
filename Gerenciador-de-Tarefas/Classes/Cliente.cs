using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using Gerenciador_de_Tarefas.Classes;

namespace Gerenciador_de_Tarefas.Classes
{
    public static class Cliente
    {
        #region Variaveis
        //Originais
        private static int id = 0, contrato = 0;
        private static string dataCadastro, nome, razaoSocial, telefoneComercial, contato, setor, cpf, rg, cnpj, inscricaoEstadual, inscricaoMunicipal,
            site, email, endereco, numero, bairro, cidade, estado, cep, complemento, pontoReferencia, obs, numeroTarefas;
        private static bool novoCadastro = false;

        //Backup
        private static int _contrato = 0;
        private static string _dataCadastro, _nome, _razaoSocial, _telefoneComercial, _contato, _setor, _cpf, _rg, _cnpj, _inscricaoEstadual, _inscricaoMunicipal,
            _site, _email, _endereco, _numero, _bairro, _cidade, _estado, _cep, _complemento, _pontoReferencia, _obs;
        #endregion

        #region Propriedades
        public static int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public static int Contrato
        {
            get
            {
                return contrato;
            }
            set
            {
                contrato = value;
            }
        }
        public static bool NovoCadastro
        {
            get
            {
                return novoCadastro;
            }
            set
            {
                novoCadastro = value;
            }
        }
        public static string DataCadastro
        {
            get
            {
                return dataCadastro;
            }
            set
            {
                dataCadastro = value;
            }
        }
        public static string Nome
        {
            get
            {
                return nome;
            }
            set
            {
                nome = value;
            }
        }
        public static string RazaoSocial
        {
            get
            {
                return razaoSocial;
            }
            set
            {
                razaoSocial = value;
            }
        }
        public static string Telefone
        {
            get
            {
                return telefoneComercial;
            }
            set
            {
                telefoneComercial = value;
            }
        }
        public static string Contato
        {
            get
            {
                return contato;
            }
            set
            {
                contato = value;
            }
        }
        public static string Setor
        {
            get
            {
                return setor;
            }
            set
            {
                setor = value;

            }
        }
        public static string CPF
        {
            get
            {
                return cpf;
            }
            set
            {
                cpf = value;
            }
        }
        public static string RG
        {
            get
            {
                return rg;
            }
            set
            {
                rg = value;
            }
        }
        public static string CNPJ
        {
            get
            {
                return cnpj;
            }
            set
            {
                cnpj = value;
            }
        }
        public static string InscricaoEstadual
        {
            get
            {
                return inscricaoEstadual;
            }
            set
            {
                inscricaoEstadual = value;
            }
        }
        public static string InscricaoMunicipal
        {
            get
            {
                return inscricaoMunicipal;
            }
            set
            {
                inscricaoMunicipal = value;
            }
        }
        public static string Site
        {
            get
            {
                return site;
            }
            set
            {
                site = value;
            }
        }
        public static string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }
        public static string Endereco
        {
            get
            {
                return endereco;
            }
            set
            {
                endereco = value;
            }
        }
        public static string Numero
        {
            get
            {
                return numero;
            }
            set
            {
                numero = value;
            }
        }
        public static string Bairro
        {
            get
            {
                return bairro;
            }
            set
            {
                bairro = value;
            }
        }
        public static string Cidade
        {
            get
            {
                return cidade;
            }
            set
            {
                cidade = value;
            }
        }
        public static string Estado
        {
            get
            {
                return estado;
            }
            set
            {
                estado = value;
            }
        }
        public static string CEP
        {
            get
            {
                return cep;
            }
            set
            {
                cep = value;
            }
        }
        public static string Complemento
        {
            get
            {
                return complemento;
            }
            set
            {
                complemento = value;
            }
        }
        public static string PontoReferencia
        {
            get
            {
                return pontoReferencia;
            }
            set
            {
                pontoReferencia = value;
            }
        }
        public static string Obs
        {
            get
            {
                return obs;
            }
            set
            {
                obs = value;
            }
        }
        public static string NumeroTarefas
        {
            get
            {
                return numeroTarefas;
            }
        }
        public static int ClientePesquisado
        {
            get; set;
        }
        #endregion

        #region Funcoes
        public static void PesquisaCliente(DataGridView dgvClientes, List<string> listaPesquisa)
        {
            int linha = 0;
            bool resultado = false;

            for (int i = 0; i < dgvClientes.Rows.Count; i++)
            {
                for (int x = 0; x < listaPesquisa.Count; x++)
                {
                    if (dgvClientes[0, i].Value.ToString().ToUpper().Contains(listaPesquisa[x].ToUpper()))
                    {
                        linha = dgvClientes[0, i].RowIndex;
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
                ClientePesquisado = linha;
            }
            else
            {
                ClientePesquisado = -1;
            }
        }

        /// <summary>
        /// Método responsável por verificar como está os combobox na tela inicial e realizar os filtros. 
        /// </summary>
        /// <param name="posicaoCmbFiltroClientes">Posição da comboBox tipo tarefas na tela de clientes</param>
        public static string FiltroClientes(int posicaoCmbFiltroClientes)
        {
            string comando = "";

            switch (posicaoCmbFiltroClientes)
            {
                case 0:
                    comando = "select tbl_contato.Nome, tbl_contato.Contato, tbl_contato.Telefone, tbl_contato.email as 'E-mail', tbl_contato_contrato.contrato " +
                        "from tbl_contato " +
                        "join tbl_contato_contrato on tbl_contato_contrato.Contato = tbl_contato.id " +
                        "where tbl_contato_contrato.Contrato = 3;";
                    break;
                case 1:
                    comando = "select tbl_contato.Nome, tbl_contato.Contato, tbl_contato.Telefone, tbl_contato.email as 'E-mail', tbl_contato_contrato.contrato " +
                        "from tbl_contato " +
                        "join tbl_contato_contrato on tbl_contato_contrato.Contato = tbl_contato.id " +
                        "where tbl_contato_contrato.Contrato = 2 OR tbl_contato_contrato.Contrato = 3 OR tbl_contato_contrato.Contrato = 4;";
                    break;
                case 2:
                    comando = "select tbl_contato.Nome, tbl_contato.Contato, tbl_contato.Telefone, tbl_contato.email as 'E-mail', tbl_contato_contrato.contrato " +
                        "from tbl_contato " +
                        "join tbl_contato_contrato on tbl_contato_contrato.Contato = tbl_contato.id " +
                        "where tbl_contato_contrato.Contrato = 2;";
                    break;

                case 3:
                    comando = "select tbl_contato.Nome, tbl_contato.Contato, tbl_contato.Telefone, tbl_contato.email as 'E-mail', tbl_contato_contrato.contrato " +
                       "from tbl_contato " +
                       "join tbl_contato_contrato on tbl_contato_contrato.Contato = tbl_contato.id " +
                       "where tbl_contato_contrato.Contrato = 1;";
                    break;
                default:
                    comando = "select tbl_contato.Nome, tbl_contato.Contato, tbl_contato.Telefone, tbl_contato.email as 'E-mail', tbl_contato_contrato.contrato " +
                        "from tbl_contato " +
                        "join tbl_contato_contrato on tbl_contato_contrato.Contato = tbl_contato.id " +
                        "order by tbl_contato.Nome ASC;";
                    break;
            }

            return comando;
        }


        public static void PreCarregaCliente(string nome)
        {
            try
            {
                LimparVariaveis();

                string comando = "Select ID from tbl_contato" + " where nome = '" + nome + "';";
                ID = int.Parse(Sistema.ConsultaSimples(comando));
            }
            catch (Exception)
            {
                ListaErro.RetornaErro(20);
                return;
            }
        }

        public static void AbrirCliente()
        {
            List<string> lista = new List<string>();
            try
            {
                //Carrega os dados
                lista = Sistema.ConsultaContato("select nome, razaosocial, telefone, contato, setor, datacadastro, email, site, obs, cpf, "
                    + "rg, cnpj, inscricaomunicipal, inscricaoestadual, cep, endereco, bairro, cidade, "
                    + "estado, complemento, pontoreferencia "
                    + "from tbl_contato where id = " + ID + ";");

                //Originais
                Nome = lista[0];
                RazaoSocial = lista[1];
                Telefone = lista[2];
                Contato = lista[3];
                Setor = lista[4];
                DataCadastro = lista[5];
                Email = lista[6];
                Site = lista[7];
                Obs = lista[8];
                CPF = lista[9];
                RG = lista[10];
                CNPJ = lista[11];
                InscricaoMunicipal = lista[12];
                InscricaoEstadual = lista[13];
                CEP = lista[14];
                if (lista[15].Contains(","))
                {
                    Endereco = lista[15].Substring(0, lista[15].LastIndexOf(','));
                    Numero = lista[15].Substring(lista[15].LastIndexOf(',') + 2, (lista[15].Length - (lista[15].LastIndexOf(',') + 2)));
                }
                else
                {
                    Endereco = lista[15];
                }
                Bairro = lista[16];
                Cidade = lista[17];
                Estado = lista[18];
                Complemento = lista[19];
                PontoReferencia = lista[20];

                Contrato = Convert.ToInt32(Sistema.ConsultaSimples("select contrato from tbl_contato_contrato where contato = " + ID + ";")) - 1;
                numeroTarefas = Sistema.ConsultaSimples("select count(id) from tbl_tarefas where empresa = " + ID + ";");

                //Backup
                _nome = lista[0];
                _razaoSocial = lista[1];
                _telefoneComercial = lista[2];
                _contato = lista[3];
                _setor = lista[4];
                _dataCadastro = lista[5];
                _email = lista[6];
                _site = lista[7];
                _obs = lista[8];
                _cpf = lista[9];
                _rg = lista[10];
                _cnpj = lista[11];
                _inscricaoMunicipal = lista[12];
                _inscricaoEstadual = lista[13];
                _cep = lista[14];
                if (lista[15].Contains(","))
                {
                    _endereco = lista[15].Substring(0, lista[15].LastIndexOf(','));
                    _numero = lista[15].Substring(lista[15].LastIndexOf(',') + 2, (lista[15].Length - (lista[15].LastIndexOf(',') + 2)));
                }
                else
                {
                    _endereco = lista[15];
                }
                _bairro = lista[16];
                _cidade = lista[17];
                _estado = lista[18];
                _complemento = lista[19];
                _pontoReferencia = lista[20];

                _contrato = Convert.ToInt32(Sistema.ConsultaSimples("select contrato from tbl_contato_contrato where contato = " + ID + ";")) - 1;

                Log.AbrirCliente();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static bool CadastrarCliente()
        {
            bool resultado = false;

            try
            {
                string comando = string.Format("insert into tbl_contato values " +
                "(0,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}');",
                DataCadastro, Nome, RazaoSocial, Telefone, Contato, Setor, CPF, RG, CNPJ, InscricaoEstadual, InscricaoMunicipal, Site, Email, Endereco + ", " + Numero, Bairro,
                Cidade, Estado, CEP, Complemento, PontoReferencia, Obs);
                Sistema.ExecutaComando(comando);

                comando = string.Format("Select id from tbl_contato where nome = '{0}';", Nome);
                ID = Int32.Parse(Sistema.ConsultaSimples(comando));

                comando = string.Format("insert into tbl_contato_contrato values ({0},{1});", ID, Contrato);
                Sistema.ExecutaComando(comando);
            }
            catch (Exception)
            {
                ListaErro.RetornaErro(39);
                resultado = false;
            }
            finally
            {
                _nome = Nome;
                _razaoSocial = RazaoSocial;
                _telefoneComercial = Telefone;
                _contato = Contato;
                _setor = Setor;
                _dataCadastro = DataCadastro;
                _email = Email;
                _site = Site;
                _obs = Obs;
                _cpf = CPF;
                _rg = RG;
                _cnpj = CNPJ;
                _inscricaoMunicipal = InscricaoMunicipal;
                _inscricaoEstadual = InscricaoEstadual;
                _cep = CEP;
                if (Endereco.Contains(","))
                {
                    _endereco = Endereco.Substring(0, Endereco.LastIndexOf(','));
                    _numero = Endereco.Substring(Endereco.LastIndexOf(',') + 2, (Endereco.Length - (Endereco.LastIndexOf(',') + 2)));
                }
                else
                {
                    _endereco = Endereco;
                }
                _bairro = Bairro;
                _cidade = Cidade;
                _estado = Estado;
                _complemento = Complemento;
                _pontoReferencia = PontoReferencia;

                _contrato = Convert.ToInt32(Sistema.ConsultaSimples("select contrato from tbl_contato_contrato where contato = " + ID + ";")) - 1;

                Log.CadastrarCliente();
                resultado = true;
            }

            return resultado;
        }

        public static void AtualizarCliente()
        {
            try
            {

                string comando = "Update tbl_contato set nome = '" + Nome + "', " +
                    "razaosocial = '" + RazaoSocial + "', telefone = '" + Telefone + "', " +
                    "contato = '" + Contato + "', setor = '" + Setor + "', " +
                    "cpf = '" + CPF + "', rg = '" + RG + "', cnpj = '" + CNPJ + "', " +
                    "inscricaoestadual = '" + InscricaoEstadual + "', inscricaomunicipal = '" + InscricaoMunicipal + "', " +
                    "site = '" + Site + "', email = '" + Email + "', endereco = '" + Endereco + "', " +
                    "bairro = '" + Bairro + "', cidade = '" + Cidade + "', estado = '" + Estado + "', " +
                    "cep = '" + CEP + "', complemento = '" + Complemento + "', " +
                    "pontoreferencia = '" + PontoReferencia + "', obs = '" + Obs + "'" +
                    "where id = '" + ID + "';";
                Sistema.ExecutaComando(comando);

                comando = "Update tbl_contato_contrato set contrato = " + Contrato + " where contato = " + ID + ";";
                Sistema.ExecutaComando(comando);
            }
            catch (Exception)
            {
                ListaErro.RetornaErro(38);
                return;
            }
            finally
            {
                Log.AlterarCliente();

                ListaMensagens.RetornaMensagem(11);
            }
        }

        public static void ApagarCliente()
        {
            Sistema.ExecutaComando("delete from tbl_tarefas where empresa = " + ID + ";");
            Sistema.ExecutaComando("delete from tbl_contato_subsubgrupo where contato = " + ID + ";");
            Sistema.ExecutaComando("delete from tbl_contato_subgrupo where contato = " + ID + ";");
            Sistema.ExecutaComando("delete from tbl_contato_telefone where contato = " + ID + ";");
            Sistema.ExecutaComando("delete from tbl_contato_contrato where contato = " + ID + ";");
            Sistema.ExecutaComando("delete from tbl_contato where id = " + ID + ";");

            Log.ApagarCliente();

            LimparVariaveis();
        }

        public static bool AvaliarMudancas()
        {
            bool resultado = false;

            if (DataCadastro == "  /  /")
            {
                DataCadastro = "";
            }

            if (_contrato != Contrato || _nome != Nome || _razaoSocial != RazaoSocial || _telefoneComercial != Telefone ||
                    _contato != Contato || _setor != Setor || _email != Email || _site != Site || _obs != Obs ||
                    _cpf != CPF || _rg != RG || _cnpj != CNPJ || _inscricaoMunicipal != InscricaoMunicipal ||
                    _inscricaoEstadual != InscricaoEstadual || _cep != CEP || _endereco != Endereco ||
                    _numero != Numero || _bairro != Bairro || _cidade != Cidade || _estado != Estado ||
                    _complemento != Complemento || _pontoReferencia != PontoReferencia)
            {
                resultado = true;
            }

                return resultado;
        }

        /// <summary>
        /// Limpa as variáveis na classe Cliente
        /// </summary>
        public static void LimparVariaveis()
        {
            //Originais
            id = 0; contrato = 0;
            numeroTarefas = ""; dataCadastro = "";
            nome = ""; razaoSocial = ""; telefoneComercial = ""; contato = ""; setor = "";
            cpf = ""; rg = ""; cnpj = ""; inscricaoEstadual = ""; inscricaoMunicipal = "";
            site = ""; email = ""; endereco = ""; numero = ""; bairro = ""; cidade = "";
            cep = ""; complemento = ""; pontoReferencia = ""; obs = "";

            novoCadastro = false;

            //Backup
            _contrato = 0;
            _dataCadastro = "";
            _nome = ""; _razaoSocial = ""; _telefoneComercial = ""; _contato = ""; _setor = "";
            _cpf = ""; _rg = ""; _cnpj = ""; _inscricaoEstadual = ""; _inscricaoMunicipal = "";
            _site = ""; _email = ""; _endereco = ""; _numero = ""; _bairro = ""; _cidade = "";
            _cep = ""; _complemento = ""; _pontoReferencia = ""; _obs = "";
        }

        /// <summary>
        /// Método responsável por retornar o tipo de contrato do contato solicitado.
        /// </summary>
        /// <param name="nomeEmpresa">Nome da empresa</param>
        public static string ConsultaTipoContrato(string nomeEmpresa)
        {
            int idCliente = 0;
            string comando = null;

            comando = "Select ID from tbl_contato where nome = '" + nomeEmpresa + "';";
            idCliente = int.Parse(Sistema.ConsultaSimples(comando));

            comando = "Select contrato from tbl_contato_contrato where contato = " + idCliente + ";";
            return Sistema.ConsultaSimples(comando);
        }
        #endregion
    }
}
