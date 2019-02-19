using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Gerenciador_de_Tarefas.Classes
{
    public static class Tarefa
    {
        #region Variaveis
        private static bool novaTarefa = true, primeiraPagina = true, travar = true, 
            tarefaApagada = false;

        private static int id = 0, prioridade = 1, status = 0;
        private static string assunto = "", atribuicao = "", dataFinal = "", dataCadastro = "",
            dataInicial = "", empresa = "", texto = "", textoImpressao = "", titulo = "";
        private static DataGridView dgvTarefasAtualizada = new DataGridView();

        private static List<string> anexos = new List<string>();

        #region Backup
        private static int _prioridade = 1, _status = 0;
        private static string _assunto = "", _atribuicao = "", _dataFinal = "",
            _dataInicial = "", _empresa = "", _texto = "";
        private static DataGridView _dgvTarefasAtual = new DataGridView();

        private static List<string> _anexos = new List<string>();
        #endregion
        #endregion

        #region Propriedades
        #region bool
        public static bool NovaTarefa
        {
            get
            {
                return novaTarefa;
            }
        }
        public static bool PrimeiraPagina
        {
            get
            {
                return primeiraPagina;
            }
            set
            {
                primeiraPagina = value;
            }
        }
        
        public static bool TarefaApagada
        {
            get
            {
                return tarefaApagada;
            }
            set
            {
                tarefaApagada = value;
            }
        }
        public static bool Travar
        {
            set
            {
                travar = value;
            }
        }
        #endregion

        #region DataGridView
        public static DataGridView DGVAtualizada
        {
            get
            {
                return dgvTarefasAtualizada;
            }
        }
        #endregion

        #region int
        public static int ID
        {
            get
            {
                return id;
            }
        }
        public static int Prioridade
        {
            get
            {
                return prioridade;
            }
            set
            {
                prioridade = value;
            }
        }
        public static int Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }
        #endregion
        
        #region string
        public static string Assunto
        {
            get
            {
                return assunto;
            }
            set
            {
                assunto = value;
            }
        }
        public static string Atribuicao
        {
            get
            {
                return atribuicao;
            }
            set
            {
                atribuicao = value;
            }
        }
        public static string DataInicial
        {
            get
            {
                return dataInicial;
            }
            set
            {
                dataInicial = value;
            }
        }
        public static string DataFinal
        { 
            get
            {
                return dataFinal;
            }
            set
            {
                dataFinal = value;
            }
        }
        public static string DataCadastro
        {
            get
            {
                return dataCadastro;
            }
        }
        public static string Empresa
        {
            get
            {
                return empresa;
            }
            set
            {
                empresa = value;
            }
        }
        public static string Texto
        {
            get
            {
                return texto;
            }
            set
            {
                texto = value;
            }
        }
        public static string TextoImpressao
        {
            get
            {
                return textoImpressao;
            }
            set
            {
                textoImpressao = value;
            }
        }
        public static string Titulo
        {
            get
            {
                return titulo;
            }
        }
        #endregion

        #region List<string>
        public static List<string> Anexos
        {
            get
            {
                return anexos;
            }
            set
            {
                anexos = value;
            }
        }
        public static List<string> ListaClientes
        {
            get
            {
                return Sistema.PreencheCMB("Select tbl_contato.nome from tbl_contato;");
            }
        }
        public static List<string> ListaFuncionarios
        {
            get
            {
                return Sistema.PreencheCMB("Select nome from tbl_funcionarios;");
            }
        }
        #endregion

        #region StringReader
        public static StringReader Leitor
        {
            get; set;
        }
        #endregion
        #endregion

        #region Funcoes
        public static bool AnexarArquivo(string caminhoArquivo)
        {
            bool resultado = false;

            try
            {
                string formato = Path.GetExtension(caminhoArquivo).ToUpper();
                string nomeArquivo = Path.GetFileNameWithoutExtension(caminhoArquivo);
                string nomeArquivoCompleto = nomeArquivo + formato;
                string enderecoServidor = Encoding.UTF8.GetString(Convert.FromBase64String(Sistema.EnderecoServidor));
                string caminhoPasta = "\\\\" + enderecoServidor + "\\GerenciadorTarefas\\Anexos\\" + id.ToString();
                string destino = caminhoPasta + "\\" + nomeArquivoCompleto;

                if (!Directory.Exists(caminhoPasta)) 
                {
                    Directory.CreateDirectory(caminhoPasta);
                }

                FileInfo arquivo = new FileInfo(destino); 

                if (!arquivo.Exists)
                {
                    try
                    {
                        File.Copy(caminhoArquivo, destino);
                        resultado = true;
                    }
                    catch (Exception)
                    {
                        ListaErro.RetornaErro(65);
                        return resultado;
                    }
                    finally
                    {
                        if(resultado)
                        {
                            string comando = "Select id from tbl_tarefa_anexos " +
                                "where tarefa = '" + id.ToString() + "' and nome = '" + nomeArquivoCompleto + "';";

                            if (string.IsNullOrEmpty(Sistema.ConsultaSimples(comando)))
                            {
                                Sistema.ExecutaComando("insert into tbl_tarefa_anexos values(0," + id.ToString() + "," +
                                "'" + nomeArquivoCompleto + "');");
                            }
                            else
                            {
                                ListaMensagens.RetornaMensagem(35, MessageBoxIcon.Information);
                                resultado = false;
                            }
                        }
                    }
                }
                else
                {
                    ListaMensagens.RetornaMensagem(34, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
            }

            return resultado;
        }

        /// <summary>
        /// Apaga a tarefa
        /// </summary>
        /// <param name="tarefa">ID da tarefa que deseja apagar</param>
        public static bool ApagarTarefa()
        {
            try
            {
                string enderecoServidor = Encoding.UTF8.GetString(Convert.FromBase64String(Sistema.EnderecoServidor));

                Sistema.ExecutaComando("delete from tbl_tarefa_anexos where tarefa = " + ID + ";");
                Directory.Delete(@"\\"+ enderecoServidor + @"\GerenciadorTarefas\Anexos\" + ID);

                Sistema.ExecutaComando("delete from tbl_tarefas where id = " + ID + ";");
            }
            catch (Exception)
            {
                tarefaApagada = false;
                return false;
            }

            tarefaApagada = true;
            return true;
        }

        public static bool ApagarAnexos()
        {
            try
            {
                Sistema.ExecutaComando("delete from tbl_tarefa_anexos where tarefa = " + ID + ";");

                Directory.Delete(@"\\192.168.254.253\GerenciadorTarefas\Anexos\" + ID);
            }
            catch (Exception)
            {
                return false;
            }

            tarefaApagada = true;
            return true;
        }

        /// <summary>
        ///Atualiza a tabela de tarefas
        /// </summary>
        public static bool AtualizaDGVTarefas(int posicaoCmbFiltroTarefas)
        {
            if (Sistema.TestaConexao())
            {
                string comando = "";

                switch (posicaoCmbFiltroTarefas)
                {
                    case 0:
                        comando = "select tbl_contato.ID, tbl_contato.Nome AS `Empresa`, tbl_funcionarios.Nome AS 'Atribuido a', tbl_tarefas.Assunto, tbl_status.`Status`, tbl_tarefas.DataFinal AS 'Data Conclusão', tbl_tarefas.prioridade " +
                        "from tbl_tarefas " +
                        "Join tbl_contato on tbl_contato.ID = tbl_tarefas.empresa " +
                        "Join tbl_funcionarios on tbl_funcionarios.id = tbl_tarefas.Funcionario " +
                        "Join tbl_status on tbl_status.id = tbl_tarefas.`Status` " +
                        "Where tbl_tarefas.`Status` = 1 OR tbl_tarefas.`Status` = 2 OR tbl_tarefas.`Status` = 3 OR tbl_tarefas.`Status` = 4 " +
                        "order by tbl_tarefas.id desc;";
                        break;
                    case 1:
                        comando = "select tbl_contato.ID, tbl_contato.Nome AS `Empresa`, tbl_funcionarios.Nome AS 'Atribuido a', tbl_tarefas.Assunto, tbl_status.`Status`, tbl_tarefas.DataFinal AS 'Data Conclusão', tbl_tarefas.prioridade " +
                        "from tbl_tarefas " +
                        "Join tbl_contato on tbl_contato.ID = tbl_tarefas.empresa " +
                        "Join tbl_funcionarios on tbl_funcionarios.id = tbl_tarefas.Funcionario " +
                        "Join tbl_status on tbl_status.id = tbl_tarefas.`Status` " +
                        "Where tbl_tarefas.`Status` = 5 " +
                        "order by tbl_tarefas.id desc;";
                        break;
                    case 2:
                        comando = "select tbl_contato.ID, tbl_contato.Nome AS `Empresa`, tbl_funcionarios.Nome AS 'Atribuido a', tbl_tarefas.Assunto, tbl_status.`Status`, tbl_tarefas.DataFinal AS 'Data Conclusão', tbl_tarefas.prioridade " +
                        "from tbl_tarefas " +
                        "Join tbl_contato on tbl_contato.ID = tbl_tarefas.empresa " +
                        "Join tbl_funcionarios on tbl_funcionarios.id = tbl_tarefas.Funcionario " +
                        "Join tbl_status on tbl_status.id = tbl_tarefas.`Status` " +
                        "order by tbl_tarefas.id desc;";
                        break;
                }


                if (Sistema.IniciaTelaTarefas)
                {
                    // Atualiza a tabela tarefas pendentes
                    _dgvTarefasAtual.DataSource = Sistema.PreencheDGV(comando);
                    dgvTarefasAtualizada.DataSource = _dgvTarefasAtual.DataSource;

                    return true;
                }
                else
                {
                    DataGridView _dgvTemp = new DataGridView()
                    {
                        //Atualiza a tabela atual temporária
                        DataSource = Sistema.PreencheDGV(comando)
                    };

                    //Se a tabela atualizada for diferente da tabela anterior
                    if (_dgvTarefasAtual != _dgvTemp)
                    {
                        dgvTarefasAtualizada.DataSource = _dgvTemp.DataSource;
                        _dgvTarefasAtual.DataSource = _dgvTemp.DataSource;
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
        /// Atualiza a tarefa
        /// </summary>
        /// <returns>Se verdadeiro, a operação foi um sucesso</returns>
        public static bool AtualizarTarefa()
        {
            string comando = null;
            int idEmpresa = 0, idFuncionario = 0;

            try
            {
                comando = "Select ID from tbl_contato where nome = '" + empresa + "';";
                idEmpresa = int.Parse(Sistema.ConsultaSimples(comando));

                comando = "Select ID from tbl_funcionarios where nome = '" + atribuicao + "';";
                idFuncionario = int.Parse(Sistema.ConsultaSimples(comando));
            }
            catch (Exception)
            {
                ListaErro.RetornaErro(17);
                return false;
            }

            try
            {
                comando = "update tbl_tarefas " +
                        "SET empresa = " + idEmpresa + ", funcionario = " + idFuncionario + ", " +
                        "status = " + status + ", assunto = '" + assunto + "', " +
                        "datainicial = '" + dataInicial + "', datafinal = '" + dataFinal + "', " +
                        "prioridade = " + prioridade + ", texto = '" + texto + "' " +
                        "Where id = " + ID + ";";

                Sistema.ExecutaComando(comando);
            }
            catch (Exception)
            {
                ListaErro.RetornaErro(17);
                return false;
            }

            //Atualiza variáveis
            _assunto = Assunto;
            _atribuicao = Atribuicao;
            _dataFinal = DataFinal;
            _dataInicial = DataInicial;
            _empresa = Empresa;
            _prioridade = Prioridade;
            _status = Status;
            _texto = Texto;

            return true;
        }

        /// <summary>
        /// Verifica se há mudanças na tarefa
        /// </summary>
        public static bool AvaliaMudancas()
        {
            try
            {
                if (NovaTarefa)
                {
                    if (_assunto != Assunto || _texto != Texto)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (_dataInicial.Length > 10)
                    {
                        _dataInicial = _dataInicial.Substring(0, 10);
                    }
                    if (_dataFinal.Length > 10)
                    {
                        _dataFinal = _dataFinal.Substring(0, 10);
                    }
                    if (DataInicial.Length > 10)
                    {
                        DataInicial = DataInicial.Substring(0, 10);
                    }
                    if (DataFinal.Length > 10)
                    {
                        DataFinal = DataFinal.Substring(0, 10);
                    }
                    if (_assunto != Assunto || _atribuicao != Atribuicao || _empresa != Empresa || 
                        _dataInicial != DataInicial || _dataFinal != DataFinal ||
                        _prioridade != Prioridade || (_status - 1) != (Status - 1) || 
                        _texto != Texto || _anexos != Anexos)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Cadastra a tarefa
        /// </summary>
        public static void CadastrarTarefa()
        {
            string comando = null;
            int idEmpresa = 0, idFuncionario = 0;

            try
            {
                comando = "Select ID from tbl_contato where nome = '" + empresa + "';";
                idEmpresa = int.Parse(Sistema.ConsultaSimples(comando));

                comando = "Select ID from tbl_funcionarios where nome = '" + atribuicao + "';";
                idFuncionario = int.Parse(Sistema.ConsultaSimples(comando));
            }
            catch (Exception)
            {
                ListaErro.RetornaErro(16);
                return;
            }
            finally
            {
                //Atualiza variáveis
                string _dataCadastro = Sistema.Hoje.Substring(6, 4) + "-" + Sistema.Hoje.Substring(3, 2) + "-" + Sistema.Hoje.Substring(0, 2);

                _assunto = Assunto;
                _atribuicao = Atribuicao;
                _dataFinal = DataFinal.Substring(6, 4) + "-" + DataFinal.Substring(3, 2) + "-" + DataFinal.Substring(0, 2);
                _dataInicial = DataInicial.Substring(6, 4) + "-" + DataInicial.Substring(3, 2) + "-" + DataInicial.Substring(0, 2);
                _empresa = Empresa;
                _prioridade = Prioridade;
                _status = Status;
                _texto = Texto;

                if (travar)
                {
                    //Cadastra a tarefa
                    comando = "insert into tbl_tarefas values (0," + idEmpresa + "," + idFuncionario
                    + "," + status + ",'" + assunto + "','" + _dataCadastro + "','" + _dataInicial + "', '" + _dataFinal
                    + "'," + prioridade + ",'" + texto + "','S');";
                    Sistema.ExecutaComando(comando);

                    _dataFinal = DataFinal;
                    _dataInicial = DataInicial;

                    //ID da tarefa
                    comando = "Select ID from tbl_tarefas where empresa = '" + idEmpresa
                   + "' AND funcionario = '" + idFuncionario
                   + "' AND assunto = '" + assunto + "';";

                    id = int.Parse(Sistema.ConsultaSimples(comando));

                    //Data de Cadastro da tarefa
                    comando = "Select DataCadastro from tbl_tarefas"
                   + " where id = '" + id.ToString() + "';";

                    string resultado = Sistema.ConsultaSimples(comando);

                    dataCadastro = resultado.Substring(6, 4) + "-" + resultado.Substring(3, 2) + "-" + resultado.Substring(0, 2);

                    novaTarefa = false;
                }
                else
                {
                    comando = "insert into tbl_tarefas values (0," + idEmpresa + "," + idFuncionario
                    + "," + status + ",'" + assunto + "','" + dataCadastro + "','" + dataInicial + "', '" + dataFinal
                    + "'," + prioridade + ",'" + texto + "','N');";
                    Sistema.ExecutaComando(comando);
                }
            }
        }

        /// <summary>
        /// Carrega os dados da tarefa escolhida
        /// </summary>
        public static void CarregarTarefa()
        {
            try
            {
                int idEmpresa = 0, idFuncionario = 0;

                string comando = "Select ID from tbl_contato"
                    + " where nome = '" + empresa + "';";
                idEmpresa = int.Parse(Sistema.ConsultaSimples(comando));

                comando = "Select ID from tbl_funcionarios"
                    + " where nome = '" + atribuicao + "';";
                idFuncionario = int.Parse(Sistema.ConsultaSimples(comando));

                comando = "Select ID from tbl_tarefas"
                   + " where empresa = '" + idEmpresa + "' AND funcionario = '" + idFuncionario +
                   "' AND assunto = '" + assunto + "';";

                id = int.Parse(Sistema.ConsultaSimples(comando));
            }
            catch (Exception)
            {
                ListaErro.RetornaErro(15);
                return;
            }
            finally
            {
                novaTarefa = false;

                List<string> lista = Sistema.ConsultaTarefas("select tbl_contato.nome AS 'empresa', " +
                    "tbl_funcionarios.nome as 'funcionario', tbl_tarefas.`status`, tbl_tarefas.assunto, " +
                    "tbl_tarefas.dataCadastro, tbl_tarefas.datainicial, tbl_tarefas.datafinal, " +
                    "tbl_tarefas.prioridade, tbl_tarefas.texto from tbl_tarefas " +
                    "Join tbl_contato on tbl_contato.ID = tbl_tarefas.Empresa " +
                    "Join tbl_funcionarios on tbl_funcionarios.id = tbl_tarefas.Funcionario " +
                    "Where tbl_tarefas.id = " + ID + ";");


                Empresa = lista[0];
                Atribuicao = lista[1];
                Status = int.Parse(lista[2]) - 1;
                Assunto = lista[3];
                dataCadastro = lista[4].Substring(0,10);
                DataInicial = lista[5].Substring(0, 10);
                if (lista[6] == "" || lista[6] == null)
                {
                    DataFinal = lista[5].Substring(0, 10);
                }
                else
                {
                    DataFinal = lista[6].Substring(0, 10);
                }

                Prioridade = int.Parse(lista[7]);
                Texto = lista[8];
                titulo = lista[0] + " - " + lista[3];
                Anexos = Sistema.ConsultaAnexosTarefa(ID, "select nome from tbl_tarefa_anexos " +
                    "Where id = " + ID + ";");

                _empresa = lista[0];
                _atribuicao = lista[1];
                _status = int.Parse(lista[2]) - 1;
                _assunto = lista[3];
                _dataInicial = lista[5].Substring(0, 10);
                if (lista[6] == "" || lista[6] == null)
                {
                    _dataFinal = lista[5].Substring(0, 10);
                }
                else
                {
                    _dataFinal = lista[6].Substring(0, 10);
                }
                _prioridade = int.Parse(lista[7]);
                _texto = lista[8];
                _anexos = Anexos;
            }
            
        }

        /// <summary>
        /// Retorna a prioridade da tarefa solicitada
        /// </summary>
        public static string ConsultaPrioridade()
        {
            int idEmpresa = 0, idFuncionario = 0, idTarefa = 0;
            string comando = null;

            comando = "Select ID from tbl_contato where nome = '" + Empresa + "';";
            idEmpresa = int.Parse(Sistema.ConsultaSimples(comando));

            comando = "Select ID from tbl_funcionarios where nome = '" + Atribuicao + "';";
            idFuncionario = int.Parse(Sistema.ConsultaSimples(comando));

            comando = "Select ID from tbl_tarefas where empresa = '" + idEmpresa + "'"
                + " AND funcionario = '" + idFuncionario + "' AND assunto = '" + Assunto + "';";
            idTarefa = int.Parse(Sistema.ConsultaSimples(comando));

            comando = "Select prioridade from tbl_tarefas where id = " + idTarefa + ";";
            return Sistema.ConsultaSimples(comando);
        }

        /// <summary>
        /// Destrava a tarefa selecionada
        /// </summary>
        public static void DestravaTarefa()
        {
            if (TarefaBloqueada())
            {
                Sistema.ExecutaComando("Update tbl_tarefas set travar = 'N' where id = " + ID + ";");
            }
        }

        /// <summary>
        /// Destrava todas as tarefas do banco de dados
        /// </summary>
        public static bool DestravaTodasTarefas()
        {
            try
            {
                Sistema.ExecutaComando("Update tbl_tarefas set travar = 'N';");
            }
            catch (Exception)
            {
                ListaErro.RetornaErro(19);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Limpa as variáveis
        /// </summary>
        public static void LimparVariaveis()
        {
            //Originais
            novaTarefa = true;
            primeiraPagina = true;

            id = 0;
            prioridade = 1;
            status = 0;
            assunto = "";
            atribuicao = "";
            dataCadastro = "";
            dataFinal = "";
            dataInicial = "";
            empresa = "";
            texto = "";
            textoImpressao = "";
            titulo = "";
            anexos = new List<string>();

            //Backup
            _prioridade = 1;
            _status = 0;
            _assunto = "";
            _atribuicao = "";
            _dataFinal = "";
            _dataInicial = "";
            _empresa = "";
            _texto = "";
            _anexos = new List<string>();
        }

        /// <summary>
        /// Verifica se a tarefa esta bloqueada
        /// </summary>
        public static bool TarefaBloqueada()
        {
            try
            {
                if (ID == 0)
                {
                    return false;
                }
                else
                {
                    if (Sistema.ConsultaSimples("Select travar from tbl_tarefas where id = " + ID + ";") == "S")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            catch (NullReferenceException)
            {
                return false;
            }
        }

        /// <summary>
        /// Trava a tarefa
        /// </summary>
        public static bool TravaTarefa()
        {
            bool resultado = false;

            if (!TarefaBloqueada())
            {
                Sistema.ExecutaComando("Update tbl_tarefas set travar = 'S' where id = " + ID + ";");
                resultado = true;
            }

            return resultado;
        }

        /// <summary>
        /// Verifica como está os combobox na tela de tarefas e realiza os filtros
        /// </summary>
        /// <param name="posicaoCmbFiltros">Posição da comboBox filtro na tela de tarefas</param>
        /// <param name="posicaoCmbTipoTarefas">Posição da comboBox tipo tarefas na tela de tarefas</param>
        public static string VerificaComboBoxTarefas(int posicaoCmbFiltros, int posicaoCmbTipoTarefas)
        {
            string comando = "", ordenadoPor = "";

            if (posicaoCmbTipoTarefas != -1)
            {
                switch (posicaoCmbFiltros)
                {
                    case 0:
                        ordenadoPor = "tbl_funcionarios.Nome ASC";
                        break;
                    case 1:
                        ordenadoPor = "tbl_funcionarios.Nome DESC";
                        break;
                    case 2:
                        ordenadoPor = "tbl_contato.Nome ASC";
                        break;
                    case 3:
                        ordenadoPor = "tbl_contato.Nome DESC";
                        break;
                    case 4:
                        ordenadoPor = "tbl_tarefas.id ASC";
                        break;
                    case 5:
                        ordenadoPor = "tbl_tarefas.id DESC";
                        break;
                    case 6:
                        ordenadoPor = "tbl_tarefas.Prioridade ASC";
                        break;
                    case 7:
                        ordenadoPor = "tbl_tarefas.Prioridade DESC";
                        break;
                }

                switch (posicaoCmbTipoTarefas)
                {
                    case 0:
                        comando = "select tbl_contato.Nome AS `Empresa`, tbl_funcionarios.Nome AS 'Atribuido a', tbl_tarefas.Assunto, tbl_status.`Status`, tbl_tarefas.DataFinal AS 'Data Conclusão' " +
                        "from tbl_tarefas " +
                        "Join tbl_contato on tbl_contato.ID = tbl_tarefas.empresa " +
                        "Join tbl_funcionarios on tbl_funcionarios.id = tbl_tarefas.Funcionario " +
                        "Join tbl_status on tbl_status.id = tbl_tarefas.`Status` " +
                        "Where tbl_tarefas.`Status` = 1 OR tbl_tarefas.`Status` = 2 OR tbl_tarefas.`Status` = 3 OR tbl_tarefas.`Status` = 4 " +
                        "order by " + ordenadoPor + ";";
                        break;
                    case 1:
                        comando = "select tbl_contato.Nome AS `Empresa`, tbl_funcionarios.Nome AS 'Atribuido a', tbl_tarefas.Assunto, tbl_status.`Status`, tbl_tarefas.DataFinal AS 'Data Conclusão' " +
                        "from tbl_tarefas " +
                        "Join tbl_contato on tbl_contato.ID = tbl_tarefas.empresa " +
                        "Join tbl_funcionarios on tbl_funcionarios.id = tbl_tarefas.Funcionario " +
                        "Join tbl_status on tbl_status.id = tbl_tarefas.`Status` " +
                        "Where tbl_tarefas.`Status` = 5 " +
                        "order by " + ordenadoPor + ";";
                        break;
                    case 2:
                        comando = "select tbl_contato.Nome AS `Empresa`, tbl_funcionarios.Nome AS 'Atribuido a', tbl_tarefas.Assunto, tbl_status.`Status`, tbl_tarefas.DataFinal AS 'Data Conclusão' " +
                        "from tbl_tarefas " +
                        "Join tbl_contato on tbl_contato.ID = tbl_tarefas.empresa " +
                        "Join tbl_funcionarios on tbl_funcionarios.id = tbl_tarefas.Funcionario " +
                        "Join tbl_status on tbl_status.id = tbl_tarefas.`Status` " +
                        "order by " + ordenadoPor + ";";
                        break;
                }
            }
            else
            {
                switch (posicaoCmbTipoTarefas)
                {
                    case 0:
                        comando = "select tbl_contato.Nome AS `Empresa`, tbl_funcionarios.Nome AS 'Atribuido a', tbl_tarefas.Assunto, tbl_status.`Status`, tbl_tarefas.DataFinal AS 'Data Conclusão' " +
                        "from tbl_tarefas " +
                        "Join tbl_contato on tbl_contato.ID = tbl_tarefas.empresa " +
                        "Join tbl_funcionarios on tbl_funcionarios.id = tbl_tarefas.Funcionario " +
                        "Join tbl_status on tbl_status.id = tbl_tarefas.`Status` " +
                        "Where tbl_tarefas.`Status` = 1 OR tbl_tarefas.`Status` = 2 OR tbl_tarefas.`Status` = 3 OR tbl_tarefas.`Status` = 4 " +
                        "order by tbl_tarefas.id desc;";
                        break;
                    case 1:
                        comando = "select tbl_contato.Nome AS `Empresa`, tbl_funcionarios.Nome AS 'Atribuido a', tbl_tarefas.Assunto, tbl_status.`Status`, tbl_tarefas.DataFinal AS 'Data Conclusão' " +
                        "from tbl_tarefas " +
                        "Join tbl_contato on tbl_contato.ID = tbl_tarefas.empresa " +
                        "Join tbl_funcionarios on tbl_funcionarios.id = tbl_tarefas.Funcionario " +
                        "Join tbl_status on tbl_status.id = tbl_tarefas.`Status` " +
                        "Where tbl_tarefas.`Status` = 5 " +
                        "order by tbl_tarefas.id desc;";
                        break;
                    case 2:
                        comando = "select tbl_contato.Nome AS `Empresa`, tbl_funcionarios.Nome AS 'Atribuido a', tbl_tarefas.Assunto, tbl_status.`Status`, tbl_tarefas.DataFinal AS 'Data Conclusão' " +
                        "from tbl_tarefas " +
                        "Join tbl_contato on tbl_contato.ID = tbl_tarefas.empresa " +
                        "Join tbl_funcionarios on tbl_funcionarios.id = tbl_tarefas.Funcionario " +
                        "Join tbl_status on tbl_status.id = tbl_tarefas.`Status` " +
                        "order by tbl_tarefas.id desc;";
                        break;
                }
            }

            return comando;
        }

        #endregion
    }
}
