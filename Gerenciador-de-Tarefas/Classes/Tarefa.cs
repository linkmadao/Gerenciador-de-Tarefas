using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Gerenciador_de_Tarefas.Classes
{
    public static class Tarefa
    {
        #region Variaveis
        private static bool novaTarefa = true, primeiraPagina = true, travar = true, tarefaApagada = false;

        //Originais
        private static int id = 0, prioridade = 1, status = 0;
        private static string assunto = "", atribuicao = "", dataFinal = "", dataCadastro = "",
            dataInicial = "", empresa = "", texto = "", textoImpressao = "", titulo = "";
        private static DataGridView dgvTarefasAtualizada = new DataGridView();

        //Backup
        private static int _prioridade = 1, _status = 0;
        private static string _assunto = "", _atribuicao = "", _dataFinal = "",
            _dataInicial = "", _empresa = "", _texto = "";
        private static DataGridView _dgvTarefasAtual = new DataGridView();


        #endregion

        #region Propriedades
        public static DataGridView DGVAtualizada
        {
            get
            {
                return dgvTarefasAtualizada;
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
        public static bool Travar
        {
            set
            {
                travar = value;
            }
        }

        public static List<string>ListaClientes
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

        #region Funcoes

        public static bool AvaliaMudancas()
        {
            try
            {
                if(NovaTarefa)
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
                    /*
                    if (DataInicial.Contains("/"))
                    {
                        DataInicial = DataInicial.Substring(6, 4) + "-" + DataInicial.Substring(3, 2) + "-" + DataInicial.Substring(0, 2);
                    }
                    if (DataFinal.Contains("/"))
                    {
                        DataFinal = DataFinal.Substring(6, 4) + "-" + DataFinal.Substring(3, 2) + "-" + DataFinal.Substring(0, 2);
                    }
                    */
                    if (_assunto != Assunto || _atribuicao != Atribuicao || _empresa != Empresa || _dataInicial != DataInicial
                                || _dataFinal != DataFinal || _prioridade != Prioridade || (_status - 1) != (Status - 1) || _texto != Texto)
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
        /// Método responsável por atualizar uma tarefa
        /// </summary>
        /// <param name="tarefa">ID da tarefa</param>
        /// <param name="empresa">Nome da empresa</param>
        /// <param name="funcionario">Nome do funcionário</param>
        /// <param name="status">Status da tarefa</param>
        /// <param name="assunto">Assunto da tarefa</param>
        /// <param name="dataInicio">Data quando a tarefa iniciou</param>
        /// <param name="dataFinal">Data quando a tarefa irá terminar/terminou</param>
        /// <param name="prioridade">Prioridade da tarefa</param>
        /// <param name="texto">Texto relatado na tarefa</param>
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
        /// Método responsável por atualizar a tabela da tela inicial
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
        /// Método responsável por apagar uma tarefa
        /// </summary>
        /// <param name="tarefa">ID da tarefa que deseja apagar</param>
        public static bool ApagarTarefa()
        {
            try
            {
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

        /// <summary>
        /// Método responsável por cadastrar uma tarefa
        /// </summary>
        /// <param name="empresa">Nome da empresa</param>
        /// <param name="funcionario">Nome do funcionário</param>
        /// <param name="status">Status da tarefa</param>
        /// <param name="assunto">Assunto da tarefa</param>
        /// <param name="dataInicio">Data quando a tarefa iniciou</param>
        /// <param name="dataFinal">Data quando a tarefa irá terminar/terminou</param>
        /// <param name="prioridade">Prioridade da tarefa</param>
        /// <param name="texto">Texto relatado na tarefa</param>
        /// <param name="travar">Travar a tarefa?</param>
        /// <returns></returns>
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

                List<string> lista = Sistema.ConsultaTarefas("select tbl_contato.nome AS 'empresa', tbl_funcionarios.nome as 'funcionario', " +
                        "tbl_tarefas.`status`, tbl_tarefas.assunto, tbl_tarefas.dataCadastro, tbl_tarefas.datainicial, tbl_tarefas.datafinal, tbl_tarefas.prioridade, tbl_tarefas.texto from tbl_tarefas " +
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
            }
            
        }

        /// <summary>
        /// Método responsável por destravar a tarefa
        /// </summary>
        /// <param name="idTarefa">ID da tarefa que deseja destravar</param>
        /// <returns></returns>
        public static void DestravaTarefa()
        {
            if (TarefaBloqueada())
            {
                Sistema.ExecutaComando("Update tbl_tarefas set travar = 'N' where id = " + ID + ";");
            }
        }

        /// <summary>
        /// Método responsável por destravar todas as tarefas do banco de dados.
        /// Utilizar apenas se ocorrer algum desligamento inesperado de algum usuário.
        /// </summary>
        /// <returns>Se verdadeiro, a operação foi um sucesso.</returns>
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

            //Backup
            _prioridade = 1;
            _status = 0;
            _assunto = "";
            _atribuicao = "";
            _dataFinal = "";
            _dataInicial = "";
            _empresa = "";
            _texto = "";
    }

        public static void Salvar()
        {
            if(novaTarefa)
            {
                CadastrarTarefa();
            }
            else
            {
                if(!AtualizarTarefa())
                {
                    ListaErro.RetornaErro(17);
                }
            }
        }

        /// <summary>
        /// Método responsável por travar a tarefa
        /// </summary>
        /// <param name="idTarefa">ID da tarefa que deseja travar</param>
        /// <returns></returns>
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
        /// Método responsável por retornar se a tarefa esta bloqueada.
        /// </summary>
        public static bool TarefaBloqueada()
        {
            try
            {
                if(ID == 0)
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
        /// Verifica como está os combobox na tela inicial e realiza os filtros. 
        /// </summary>
        /// <param name="posicaoCmbFiltros">Posição da comboBox filtro na tela de tarefas</param>
        /// <param name="posicaoCmbTipoTarefas">Posição da comboBox tipo tarefas na tela de tarefas</param>
        /// <returns></returns>
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


        /// <summary>
        /// Método responsável por retornar a prioridade da tarefa solicitada.
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

        #endregion
        /*
        /// <summary>
        /// Método responsável por pegar o texto desalinhado da tela Tarefas e realinhar para impressão.
        /// </summary>
        /// <param name="textoOriginal">Texto da tarefa.</param>
        /// <param name="maxCaracteres">Quantidada máxima de caracteres por linha.</param>
        /// <example>PreparaTexto(rtbTexto.text,110);</example>
        /// <returns>Retorna texto alinhado pelo a quantidade de caracteres máxima.</returns>
        public string PreparaTexto(string textoOriginal, int maxCaracteres)
        {
            string textoResultado = "";
            string _textoOriginal = textoOriginal;


            while (_textoOriginal.Length > maxCaracteres)
            {
                int posicaoAtual = 0, posicaoUltimoEspaco = 0;

                if (_textoOriginal.Substring(posicaoAtual, 1) == " ")
                {
                    posicaoAtual += 1;
                }
                if (_textoOriginal.Substring(posicaoAtual, 1) == "\n\n")
                {
                    posicaoAtual += 1;
                }
                if (_textoOriginal.Substring(posicaoAtual, 2) == ".\n\n")
                {
                    posicaoAtual += 2;
                }

                if (_textoOriginal.Substring(posicaoAtual, posicaoAtual + maxCaracteres).Contains("\n\n"))
                {
                    if (_textoOriginal.Substring(posicaoAtual, posicaoAtual + maxCaracteres).IndexOf("\n\n") > 0)
                    {
                        posicaoUltimoEspaco = _textoOriginal.Substring(posicaoAtual, posicaoAtual + maxCaracteres).IndexOf("\n\n");
                    }
                    else if (_textoOriginal.Substring(posicaoAtual, posicaoAtual + maxCaracteres).IndexOf(".\n\n") > 0)
                    {
                        posicaoUltimoEspaco = _textoOriginal.Substring(posicaoAtual, posicaoAtual + maxCaracteres).IndexOf(".\n\n") + 1;
                    }
                    else if (_textoOriginal.Substring(posicaoAtual, posicaoAtual + maxCaracteres).IndexOf("\n\n\n\n") > 0)
                    {
                        posicaoUltimoEspaco = _textoOriginal.Substring(posicaoAtual, posicaoAtual + maxCaracteres).IndexOf("\n\n\n\n");
                    }
                }
                else
                {
                    if (_textoOriginal.Substring(posicaoAtual, posicaoAtual + maxCaracteres).LastIndexOf(" ") > 0)
                    {
                        //Define a posição do ultimo espaço em branco no texto selecionado
                        posicaoUltimoEspaco = _textoOriginal.Substring(posicaoAtual, posicaoAtual + maxCaracteres).LastIndexOf(" ");
                    }
                    else
                    {
                        posicaoUltimoEspaco = posicaoAtual + maxCaracteres;
                    }
                }

                if (posicaoUltimoEspaco <= 0)
                {
                    posicaoUltimoEspaco = posicaoAtual;
                }

                //Escreve o texto
                textoResultado += _textoOriginal.Substring(posicaoAtual, posicaoUltimoEspaco);
                textoResultado += "\n\n";

                _textoOriginal = _textoOriginal.Substring(posicaoUltimoEspaco + posicaoAtual);

            }
            if (_textoOriginal.Length > 0)
            {
                if (_textoOriginal.Substring(0, 1) == " ")
                {
                    textoResultado += _textoOriginal.Substring(1);
                }
                else
                {
                    textoResultado += _textoOriginal;
                }
            }

            return textoResultado;
        }*/
    }
}
