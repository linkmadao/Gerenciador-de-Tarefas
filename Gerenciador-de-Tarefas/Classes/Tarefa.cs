using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gerenciador_de_Tarefas.Classes
{
    public static class Tarefa
    {
        #region Variaveis
        private static BDCONN conexao = new BDCONN();
        
        private static bool novaTarefa = true, primeiraPagina = true, salvar = false, travar = true;

        //Originais
        private static int id = 0, prioridade = 0, status = 0;
        private static string assunto = "", atribuicao = "", dataFinal = "",
            dataInicial = "", empresa = "", texto = "", textoImpressao = "", titulo = "";

        //Backup
        private static int _prioridade = 0, _status = 0;
        private static string _assunto = "", _atribuicao = "", _dataFinal = "",
            _dataInicial = "", _empresa = "", _texto = "";
        #endregion

        #region Propriedades
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
        public static bool SalvarTarefa
        {
            get
            {
                return salvar;
            }
            set
            {
                salvar = value;
            }
        }
        public static bool Travar
        {
            get
            {
                return travar;
            }
            set
            {
                travar = value;
            }
        }

        public static List<string>ListaClientes
        {
            get
            {
                return conexao.PreencheCMB("Select tbl_contato.nome from tbl_contato;");
            }
        }
        public static List<string> ListaFuncionarios
        {
            get
            {
                return conexao.PreencheCMB("Select nome from tbl_funcionarios;");
            }
        }
        #endregion

        #region Funcoes
        public static bool AvaliaMudancas()
        {
            try
            {
                if (_assunto != Assunto || _atribuicao != Atribuicao || _empresa != Empresa || _dataInicial != DataInicial
                            || _dataFinal != DataFinal || _prioridade != Prioridade|| _status != Status || _texto != Texto)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void CarregarTarefa()
        {
            try
            {
                int idEmpresa = 0, idFuncionario = 0;

                string comando = "Select ID from tbl_contato"
                    + " where nome = '" + empresa + "';";
                idEmpresa = Int32.Parse(conexao.ConsultaSimples(comando));

                comando = "Select ID from tbl_funcionarios"
                    + " where nome = '" + atribuicao + "';";
                idFuncionario = Int32.Parse(conexao.ConsultaSimples(comando));

                comando = "Select ID from tbl_tarefas"
                   + " where empresa = '" + idEmpresa + "' AND funcionario = '" + idFuncionario +
                   "' AND assunto = '" + assunto + "';";

                id = Int32.Parse(conexao.ConsultaSimples(comando));
            }
            catch (Exception)
            {
                string erro = ListaErro.RetornaErro(15);
                int separador = erro.IndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                novaTarefa = false;
                
                if (!NovaTarefa)
                {
                    List<string> lista = conexao.ConsultaTarefas("select tbl_contato.nome AS 'empresa', tbl_funcionarios.nome as 'funcionario', " +
                        "tbl_tarefas.`status`, tbl_tarefas.assunto, tbl_tarefas.datainicial, tbl_tarefas.datafinal, tbl_tarefas.prioridade, tbl_tarefas.texto from tbl_tarefas " +
                        "Join tbl_contato on tbl_contato.ID = tbl_tarefas.Empresa " +
                        "Join tbl_funcionarios on tbl_funcionarios.id = tbl_tarefas.Funcionario " +
                        "Where tbl_tarefas.id = " + ID + ";");


                    Empresa = lista[0];
                    Atribuicao = lista[1];
                    Status = int.Parse(lista[2]) - 1;
                    Assunto = lista[3];
                    DataInicial = lista[4];
                    if (lista[5] == "" || lista[5] == null)
                    {
                        DataFinal = lista[4];
                    }
                    else
                    {
                        DataFinal = lista[5];
                    }

                    Prioridade = int.Parse(lista[6]);
                    Texto = lista[7];
                    titulo = lista[0] + " - " + lista[3];


                    _empresa = lista[0];
                    _atribuicao = lista[1];
                    _status = int.Parse(lista[2]);
                    _assunto = lista[3];
                    _dataInicial = lista[4];
                    if (lista[5] == "" || lista[5] == null)
                    {
                        _dataFinal = lista[4];
                    }
                    else
                    {
                        _dataFinal = lista[5];
                    }
                    _prioridade = int.Parse(lista[6]);
                    _texto = lista[7];
                }
            }
            
        }

        public static void LimparVariaveis()
        {
            //Originais
            novaTarefa = true;
            salvar = false;
            primeiraPagina = true;

            id = 0;
            prioridade = 0;
            status = 0;
            assunto = "";
            atribuicao = "";
            dataFinal = "";
            dataInicial = "";
            empresa = "";
            texto = "";
            textoImpressao = "";
            titulo = "";

            //Backup
            _prioridade = 0;
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
                if(AtualizarTarefa())
                {
                    string erro = ListaErro.RetornaErro(17);
                    int separador = erro.IndexOf(":");
                    MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Verifica como está os combobox na tela inicial e realiza os filtros. 
        /// </summary>
        /// <param name="posicaoCmbTipoTarefas">Posição da comboBox tipo tarefas na tela de tarefas</param>
        public static string VerificaComboBoxTarefas(int posicaoCmbTipoTarefas)
        {
            string comando = "";

            switch (posicaoCmbTipoTarefas)
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

            return comando;
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
        /// Método responsável por travar a tarefa
        /// </summary>
        /// <param name="idTarefa">ID da tarefa que deseja travar</param>
        /// <returns></returns>
        public static bool TravaTarefa()
        {
            bool resultado = false;

            if (!conexao.TarefaBloqueada(ID))
            {
                conexao.ExecutaComando("Update tbl_tarefas set travar = 'S' where id = " + ID + ";");
                resultado = true;
            }

            return resultado;
        }

        /// <summary>
        /// Método responsável por destravar a tarefa
        /// </summary>
        /// <param name="idTarefa">ID da tarefa que deseja destravar</param>
        /// <returns></returns>
        public static void DestravaTarefa()
        {
            if (conexao.TarefaBloqueada(ID))
            {
                conexao.ExecutaComando("Update tbl_tarefas set travar = 'N' where id = " + ID + ";");
            }
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
                idEmpresa = int.Parse(conexao.ConsultaSimples(comando));

                comando = "Select ID from tbl_funcionarios where nome = '" + atribuicao + "';";
                idFuncionario = int.Parse(conexao.ConsultaSimples(comando));
            }
            catch (Exception)
            {
                string erro = ListaErro.RetornaErro(16);
                int separador = erro.IndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                if(travar)
                {
                    comando = "insert into tbl_tarefas values (0," + idEmpresa + "," + idFuncionario
                    + "," + status + ",'" + assunto + "','" + dataInicial + "', '" + dataFinal
                    + "'," + prioridade + ",'" + texto + "','S');";
                    conexao.ExecutaComando(comando);
                }
                else
                {
                    comando = "insert into tbl_tarefas values (0," + idEmpresa + "," + idFuncionario
                    + "," + status + ",'" + assunto + "','" + dataInicial + "', '" + dataFinal
                    + "'," + prioridade + ",'" + texto + "','N');";
                    conexao.ExecutaComando(comando);

                    comando = "Select ID from tbl_tarefas where empresa = '" + idEmpresa
                   + "' AND funcionario = '" + idFuncionario
                   + "' AND assunto = '" + assunto + "';";
                    id = int.Parse(conexao.ConsultaSimples(comando));
                }
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
            bool resultado = false;

            string comando = null;
            int idEmpresa = 0, idFuncionario = 0;

            try
            {
                comando = "Select ID from tbl_contato where nome = '" + empresa + "';";
                idEmpresa = Int32.Parse(conexao.ConsultaSimples(comando));

                comando = "Select ID from tbl_funcionarios where nome = '" + atribuicao + "';";
                idFuncionario = Int32.Parse(conexao.ConsultaSimples(comando));
            }
            catch (Exception)
            {
                return resultado;
            }
            finally
            {
                comando = "update tbl_tarefas " +
                            "SET empresa = " + idEmpresa + ", funcionario = " + idFuncionario + ", " +
                            "status = " + status + ", assunto = '" + assunto + "', " +
                            "datainicial = '" + dataInicial + "', datafinal = '" + dataFinal + "', " +
                            "prioridade = " + prioridade + ", texto = '" + texto + "' " +
                            "Where id = " + ID + ";";

                conexao.ExecutaComando(comando);

                resultado = true;
            }

            return resultado;
        }

        /// <summary>
        /// Método responsável por apagar uma tarefa
        /// </summary>
        /// <param name="tarefa">ID da tarefa que deseja apagar</param>
        public static bool ApagarTarefa()
        {
            try
            {
                conexao.ExecutaComando("delete from tbl_tarefas where id = " + ID + ";");
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Método responsável por destravar todas as tarefas do banco de dados.
        /// Utilizar apenas se ocorrer algum desligamento inesperado de algum usuário.
        /// </summary>
        /// <returns>Se verdadeiro, a operação foi um sucesso.</returns>
        public static bool DestravaTodasTarefas()
        {
            bool resultado = false;
            string comando = null;

            try
            {
                comando = "Update tbl_tarefas set travar = 'N';";
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
