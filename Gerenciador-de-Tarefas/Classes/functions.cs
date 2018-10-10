using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace Gerenciador_de_Tarefas
{
    public static class ListaErro
    {
        public static string RetornaErro(int erro)
        {
            string resultado = null;

            switch(erro)
            {
                case 01:
                    resultado = "Erro na Conexão pelo Banco de Dados : " +
                        "Não foi possível conectar ao banco de dados!\r\n\nConfigure o acesso ao banco na tela a seguir...";
                    break;
                case 02:
                    resultado = "Erro ao Preencher DGV : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 03:
                    resultado = "Erro ao Preencher CMB : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 04:
                    resultado = "Erro ao Executar Consulta Simples : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 05:
                    resultado = "Erro ao Consultar Tarefa : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 06:
                    resultado = "Erro ao Consultar Contato : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 07:
                    resultado = "Erro ao Consultar AI : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 08:
                    resultado = "Erro ao Travar a Tarefa : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 09:
                    resultado = "Erro ao Consultar Prioridade : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 10:
                    resultado = "Erro ao Consultar o Tipo de Contrato : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 11:
                    resultado = "Erro ao Executar Comando SQL : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 12:
                    resultado = "Erro ao Realizar Backup : " +
                        "Favor entrar em contato pelo suporte@cftva.com.br";
                    break;
                case 13:
                    resultado = "Erro ao Realizar Restauração : " +
                        "Favor entrar em contato pelo suporte@cftva.com.br";
                    break;
                case 14:
                    resultado = "Configuração não Executada : " +
                        "O programa não foi configurado corretamente e por isso ele será fechado!\r\n\nPara mais dúvidas, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 15:
                    resultado = "Erro ao Abrir a Tarefa : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 16:
                    resultado = "Erro ao Cadastrar a Tarefa : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 17:
                    resultado = "Erro ao Atualizar a Tarefa : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 18:
                    resultado = "Erro ao Apagar a Tarefa : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 19:
                    resultado = "Erro ao Destravar Todas as Tarefa : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 20:
                    resultado = "Erro ao Abrir o Contato : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 21:
                    resultado = "Erro ao Otimizar as Tabelas do Banco de Dados : " +
                        "Favor entrar em contato pelo suporte@cftva.com.br";
                    break;
                case 22:
                    resultado = "Erro ao Acessar o Arquivo Informado : " +
                        "Verifique se você tem mesmo a permissão para acessar o/a arquivo/pasta!\n\nFavor entrar em contato pelo suporte@cftva.com.br";
                    break;
                case 23:
                    resultado = "Erro ao Preparar o Documento para Impressão : " +
                        "Há algum erro no texto ou na configuração da página!\n\nFavor entrar em contato pelo suporte@cftva.com.br";
                    break;
                case 24:
                    resultado = "Erro ao Imprimir o Documento : " +
                        "Há algum erro no texto ou na configuração da página!\n\nFavor entrar em contato pelo suporte@cftva.com.br";
                    break;
                case 25:
                    resultado = "Usuário Inválido : " +
                        "O usuário não pode estar vazio.\nPor favor, selecione um usuário!";
                    break;
                case 26:
                    resultado = "Senha Inválida : " +
                        "A senha não pode estar vazia.\nPor favor, insira uma senha!";
                    break;
                case 27:
                    resultado = "Senha Incompleta : " +
                        "A senha deve conter 4 caracteres.\nPor favor, corrija a senha!";
                    break;
                case 28:
                    resultado = "Quantidade de Caracteres Insuficiente : " +
                        "O assunto possui menos do que 5 caracteres!\nPor favor, insira um assunto maior do que 5 caracteres.";
                    break;
                case 29:
                    resultado = "Texto Inválido : " +
                        "O texto não pode estar vazio!\nPor favor, coloque ao menos um ponto.";
                    break;
                case 30:
                    resultado = "O diretório a seguir não existe ou não pode ser acessado : ";
                    break;
                case 31:
                    resultado = "Fornecedor não Encontrado : " +
                        "O fornecedor que você buscou não foi encontrado.\nPor favor, verifique o texto informado e tente novamente.";
                    break;
                case 32:
                    resultado = "Erro ao colorir a tabela de tarefas : " +
                        "Por favor reportar ao suporte técnico.";
                    break;
                case 33:
                    resultado = "Razão Social do Fornecedor Inválido : " +
                        "Por favor, insira a razão social do fornecedor corretamente.";
                    break;
                case 34:
                    resultado = "Quantidade de Caracteres Insuficiente : " +
                        "O nome do contato possui menos do que 3 caracteres!\nPor favor, insira um nome maior do que 3 caracteres.";
                    break;
                case 35:
                    resultado = "Nome do Fornecedor Inválido : " +
                        "Por favor, insira o nome do fornecedor corretamente.";
                    break;
                case 36:
                    resultado = "Quantidade de Caracteres Insuficiente : " +
                        "A razão social do fornecedor possui menos do que 3 caracteres!\nPor favor, insira uma razão social maior do que 3 caracteres.";
                    break;
                case 37:
                    resultado = "Erro ao Apagar o Contato : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 38:
                    resultado = "Erro ao Atualizar o Contato : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 39:
                    resultado = "Erro ao Cadastrar o Contato : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 40:
                    resultado = "CNPJ Incompleto : " +
                        "O CNPJ informado esta incompleto!\nPor favor, digite-o novamente.";
                    break;
                case 41:
                    resultado = "CNPJ Inválido : " +
                        "O CNPJ informado é inválido!\nPor favor, digite-o novamente.";
                    break;
                case 42:
                    resultado = "CPF Incompleto : " +
                        "O CPF informado esta incompleto!\nPor favor, digite-o novamente.";
                    break;
                case 43:
                    resultado = "CPF Inválido : " +
                        "O CPF informado é inválido!\nPor favor, digite-o novamente.";
                    break;
                case 44:
                    resultado = "CEP Inválido : " +
                        "O CEP informado é inválido!\nPor favor, digite-o novamente.";
                    break;
                case 45:
                    resultado = "Corrigir CEP? : " +
                        "O CEP informado não foi encontrado.\nDeseja corrigir o número do CEP?";
                    break;
                case 46:
                    resultado = "CEP Inexistente : " +
                        "O CEP informado existe.\nPor favor, digite um CEP válido.";
                    break;
                case 47:
                    resultado = "Usuário/Senha Inválido(a) : " +
                        "O usuário/senha informado(a) está incorreto(a).\nPor favor, tente novamente.";
                    break;
                case 48:
                    resultado = "Cliente não Encontrado : " +
                        "O cliente que você buscou não foi encontrado.\nPor favor, verifique o texto informado e tente novamente.";
                    break;
                case 49:
                    resultado = "Erro ao filtrar a tabela de tarefas : " +
                        "Por favor reportar ao suporte técnico qual filtro foi utilizado antes de gerar esse erro.";
                    break;
                case 50:
                    resultado = "Quantidade de Caracteres Insuficiente : " +
                        "O nome do fornecedor possui menos do que 3 caracteres!\nPor favor, insira um nome maior do que 3 caracteres.";
                    break;
                case 51:
                    resultado = "Erro ao Cadastrar o Fornecedor : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 52:
                    resultado = "Erro ao Travar o Fornecedor : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 53:
                    resultado = "Erro ao pesquisar o CNPJ : " +
                        "O sistema da receita federal pode estar fora do ar ou o CNPJ informado não tem cadastro na receita federal;" +
                        "Caso o CNPJ seja verdadeiro e o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 54:
                    resultado = "Erro ao Carregar o Fornecedor : " +
                        "O fornecedor foi aberto por outro usuário.\n\n" +
                        "Caso não tenha nenhum usuário utilizando o software e esta mensagem aparecer, vá no menu \"Ferramentas\" e clique em \"Destravar Fornecedores\"." +
                        "\n\nOBS: Será solicitado uma senha, porém só os administradores possuem ela!";
                    break;
                case 55:
                    resultado = "Erro ao Consultar Fornecedor : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 56:
                    resultado = "Erro ao Atualizar Fornecedor : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
                case 57:
                    resultado = "Erro ao Apagar o Fornecedor : " +
                        "Há algum erro na sintaxe do comando SQL ou algum caractere que não é válido!\r\n\nCaso o erro persista, entre em contato pelo suporte@cftva.com.br";
                    break;
            }

            return resultado;
        }
    }

    public static class ListaMensagens
    {
        public static string RetornaMensagem(int mensagem)
        {
            string resultado = null;

            switch (mensagem)
            {
                case 01:
                    resultado = "Tarefa Bloqueada : " +
                        "A tarefa foi aberta por outro usuário.\n\n" +
                        "Caso não tenha nenhum usuário utilizando o software e esta mensagem aparecer, vá no menu \"Ferramentas\" e clique em \"Destravar Tarefas\"." +
                        "\n\nOBS: Será solicitado uma senha, porém só os administradores possuem ela!";
                    break;
                case 02:
                    resultado = "Cancelar Tarefa? : " +
                        "Você tem certeza de que deseja cancelar esta tarefa?\nTodas as informações inseridas aqui serão perdidas!";
                    break;
                case 03:
                    resultado = "Sair da Tarefa? : " +
                        "Você tem certeza de que deseja sair desta tarefa?\nTodas as alterações serão perdidas!";
                    break;
                case 04:
                    resultado = "Excluir a Tarefa? : " +
                        "Você tem certeza de que deseja excluir esta tarefa?\nTodas as informações dessa tarefa serão perdidas!";
                    break;
                case 05:
                    resultado = "Excluir a Tarefa? : " +
                        "Tem certeza absoluta disso?\nEssa operação não poderá ser desfeita!" +
                        "\n\nPara continuar digite a senha de administrador.";
                    break;
                case 06:
                    resultado = "Cancelar Cadastro? : " +
                        "Você tem certeza de que deseja cancelar este cadastro?\nTodas as informações inseridas aqui serão perdidas!";
                    break;
                case 07:
                    resultado = "Sair do Contato? : " +
                        "Você tem certeza de que deseja sair deste contato?\nTodas as alterações serão perdidas!";
                    break;
                case 08:
                    resultado = "Excluir o Contato? : " +
                        "Você tem certeza de que deseja excluir este contato?\nTodas as informações desse contato serão perdidas!" +
                        "\n\nOBS: Ao apagar o contato todas as tarefas relacionadas a ele também serão apagadas!";
                    break;
                case 09:
                    resultado = "Excluir o Contato? : " +
                        "Tem certeza absoluta disso?\nEssa operação não poderá ser desfeita!" +
                        "\n\nPara continuar digite a senha de administrador.";
                    break;
                case 10:
                    resultado = "Excluir o Contato? : " +
                        "Contato apagado com sucesso!";
                    break;
                case 11:
                    resultado = "Contato Alterado : " +
                        "Contato alterado com sucesso!";
                    break;
                case 12:
                    resultado = "Novo Contato : " +
                        "Contato cadastrado com sucesso!\n\nGostaria de cadastrar um novo contato?";
                    break;
                case 13:
                    resultado = "Atualização do sistema disponível : " +
                        "Há uma nova versão do sistema, gostaria de atualizá-lo agora?";
                    break;
                case 14:
                    resultado = "Excluir a Tarefa? : " +
                        "A tarefa foi apagada com sucesso!";
                    break;
                case 15:
                    resultado = "Excluir a Tarefa? : " +
                        "Tem certeza absoluta disso?\nEssa operação não poderá ser desfeita!";
                    break;
                case 16:
                    resultado = "Novo Fornecedor : " +
                        "Fornecedor cadastrado com sucesso!\n\nGostaria de cadastrar um novo fornecedor?";
                    break;
                case 17:
                    resultado = "Fornecedor Alterado : " +
                        "O fornecedor foi editado com sucesso!";
                    break;
                case 18:
                    resultado = "Sair do Fornecedor : " +
                        "Você tem certeza de que deseja sair deste fornecedor?\nTodas as alterações serão perdidas!";
                    break;
                case 19:
                    resultado = "Excluir o Fornecedor? : " +
                        "Você tem certeza de que deseja excluir este fornecedor?\nTodas as informações desse fornecedor serão perdidas!";
                    break;
                case 20:
                    resultado = "Excluir o Fornecedor? : " +
                        "Tem certeza absoluta disso?\nEssa operação não poderá ser desfeita!" +
                        "\n\nPara continuar digite a senha de administrador.";
                    break;
                case 21:
                    resultado = "Excluir o Fornecedor? : " +
                        "O fornecedor foi excluído com sucesso!";
                    break;
                case 22:
                    resultado = "Excluir o Fornecedor? : " +
                        "Tem certeza absoluta disso?\nEssa operação não poderá ser desfeita!";
                    break;
            }

            return resultado;
        }
    }

    public static class FuncoesEstaticas
    {
        private static string diretorioNovo = @"\\192.168.254.253\GerenciadorTarefas\";
        private static string diretorioPadrao = @"\\192.168.254.253\GerenciadorTarefas\Projeto\Gerenciador-de-Tarefas\Gerenciador-de-Tarefas\bin\Release\";

        public static string DiretorioNovo()
        {
            return diretorioNovo;
        }

        public static string DiretorioPadrao()
        {
            return diretorioPadrao;
        }

        public static bool ValidaCPF(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        public static bool ValidaCNPJ(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }

        #region Fornecedores
        public static int FornecedorPesquisado
        {
            get; set;
        }

        public static void PesquisaFornecedor(DataGridView dgvFornecedores, List<string> listaPesquisa)
        {
            int linha = 0;
            bool resultado = false;

            for (int i = 0; i < dgvFornecedores.Rows.Count; i++)
            {
                for (int x = 0; x < listaPesquisa.Count; x++)
                {
                    if(dgvFornecedores[1, i].Value.ToString().ToUpper().Contains(listaPesquisa[x].ToUpper()))
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

            if(resultado)
            {
                FornecedorPesquisado = linha;
            }
            else
            {
                FornecedorPesquisado = -1;
            }
        }

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

        #endregion

        #region Clientes
        public static int ClientePesquisado
        {
            get; set;
        }

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
                        "where tbl_contato.Grupo = 1 order by tbl_contato.Nome ASC;";
                    break;
            }

            return comando;
        }
        #endregion
        
        #region Tarefas
        /// <summary>
        /// Função para preparar o texto digitado na tarefa
        /// </summary>
        /// <param name="textoOriginal">Texto que foi inserido pelo usuário!</param>
        /// <param name="maxCaracteres">Quantidade máxima de caracteres permitida por linha</param>
        /// <returns>Texto preparado para ser impresso</returns>
        public static string PreparaTexto(string textoOriginal, int maxCaracteres)
        {
            string textoResultado = "";
            string _textoOriginal = textoOriginal;

            //Enquanto o texto for maior do que a quantidade máxima de caracteres permitida por linha
            while (_textoOriginal.Length > maxCaracteres)
            {
                //Variáveis
                int posicaoAtual = 0, posicaoUltimoEspaco = 0, linhas = 0, linhasPosTexto = 0;

                #region Verifica se há sobra de linhas
                //Se houver 4 linhas antes do conteúdo e este conteúdo seguinte não for igual a 4 linhas
                if (_textoOriginal.Substring(posicaoAtual, 4) == "\n\n\n\n" && _textoOriginal.Substring(posicaoAtual + 4, 4) != "\n\n\n\n")
                {
                    posicaoAtual += 4;
                    linhas += 4;
                }
                //Se houver 1 ponto (.) e 3 linhas antes do conteúdo e este conteúdo não for igual a 1 ponto (.) e 3 linhas
                else if (_textoOriginal.Substring(posicaoAtual, 4) == ".\n\n\n" && _textoOriginal.Substring(posicaoAtual + 4, 4) != ".\n\n\n")
                {
                    posicaoAtual += 4;
                    linhas += 3;
                }
                //Se houver 3 linhas antes do conteúdo e este conteúdo seguinte não for igual a 3 linhas
                else if (_textoOriginal.Substring(posicaoAtual, 3) == "\n\n\n" && _textoOriginal.Substring(posicaoAtual + 3, 3) != "\n\n\n")
                {
                    posicaoAtual += 3;
                    linhas += 3;
                }
                //Se houver 1 ponto (.) e 2 linhas antes do conteúdo e este conteúdo não for igual a 1 ponto (.) e 2 linhas
                else if (_textoOriginal.Substring(posicaoAtual, 3) == ".\n\n" && _textoOriginal.Substring(posicaoAtual + 3, 3) != ".\n\n")
                {
                    posicaoAtual += 3;
                    linhas += 2;
                }
                //Se houver 2 linhas antes do conteúdo e este conteúdo seguinte não for igual a 2 linhas
                else if (_textoOriginal.Substring(posicaoAtual, 2) == "\n\n" && _textoOriginal.Substring(posicaoAtual + 2, 2) != "\n\n")
                {
                    posicaoAtual += 2;
                    linhas += 2;
                }
                //Se houver 1 ponto (.) e 1 linha antes do conteúdo e este conteúdo não for igual a 1 ponto (.) e 1 linha
                else if (_textoOriginal.Substring(posicaoAtual, 2) == ".\n" && _textoOriginal.Substring(posicaoAtual + 2, 2) != ".\n")
                {
                    posicaoAtual += 2;
                    linhas += 1;
                }
                //Se houver 1 linha antes do conteúdo e este conteúdo seguinte não for igual a 1 linha
                else if (_textoOriginal.Substring(posicaoAtual, 1) == "\n" && _textoOriginal.Substring(posicaoAtual + 1, 1) != "\n")
                {
                    posicaoAtual += 1;
                    linhas += 1;
                }
                //Se houver um espaço antes do conteúdo
                if (_textoOriginal.Substring(posicaoAtual, 1) == " ")
                {
                    posicaoAtual += 1;
                }
                #endregion

                //Verifica se a quantidade de caracteres é maior do que a permitida
                if (posicaoAtual + maxCaracteres < maxCaracteres)
                {
                    if (_textoOriginal.Substring(posicaoAtual, posicaoAtual + maxCaracteres).Contains("\n"))
                    {
                        if (_textoOriginal.Substring(posicaoAtual, posicaoAtual + maxCaracteres).IndexOf("\n") > 0)
                        {
                            if ((_textoOriginal.Substring(posicaoAtual, posicaoAtual + maxCaracteres).IndexOf("\n") <= maxCaracteres))
                            {
                                posicaoUltimoEspaco = _textoOriginal.Substring(posicaoAtual, posicaoAtual + maxCaracteres).IndexOf("\n");
                            }
                            else
                            {
                                posicaoUltimoEspaco = _textoOriginal.Substring(posicaoAtual, posicaoAtual + maxCaracteres).LastIndexOf(" ");
                            }

                        }
                        else if (_textoOriginal.Substring(posicaoAtual, posicaoAtual + maxCaracteres).IndexOf(".\n") > 0)
                        {
                            if ((_textoOriginal.Substring(posicaoAtual, posicaoAtual + maxCaracteres).IndexOf(".\n") < maxCaracteres))
                            {
                                posicaoUltimoEspaco = _textoOriginal.Substring(posicaoAtual, posicaoAtual + maxCaracteres).IndexOf(".\n") + 1;
                            }
                            else
                            {
                                posicaoUltimoEspaco = _textoOriginal.Substring(posicaoAtual, posicaoAtual + maxCaracteres).LastIndexOf(" ");
                            }
                        }
                    }
                    else if (_textoOriginal.Substring(posicaoAtual, posicaoAtual + maxCaracteres).Contains("\n\n"))
                    {
                        if (_textoOriginal.Substring(posicaoAtual, posicaoAtual + maxCaracteres).IndexOf("\n\n") > 0)
                        {
                            if ((_textoOriginal.Substring(posicaoAtual, posicaoAtual + maxCaracteres).IndexOf("\n\n") <= maxCaracteres))
                            {
                                posicaoUltimoEspaco = _textoOriginal.Substring(posicaoAtual, posicaoAtual + maxCaracteres).IndexOf("\n\n");
                            }
                            else
                            {
                                posicaoUltimoEspaco = _textoOriginal.Substring(posicaoAtual, posicaoAtual + maxCaracteres).LastIndexOf(" ");
                            }
                        }
                        else if (_textoOriginal.Substring(posicaoAtual, posicaoAtual + maxCaracteres).IndexOf(".\n\n") > 0)
                        {
                            if ((_textoOriginal.Substring(posicaoAtual, posicaoAtual + maxCaracteres).IndexOf(".\n\n") < maxCaracteres))
                            {
                                posicaoUltimoEspaco = _textoOriginal.Substring(posicaoAtual, posicaoAtual + maxCaracteres).IndexOf(".\n\n") + 1;
                            }
                            else
                            {
                                posicaoUltimoEspaco = _textoOriginal.Substring(posicaoAtual, posicaoAtual + maxCaracteres).LastIndexOf(" ");
                            }
                        }
                        else if (_textoOriginal.Substring(posicaoAtual, posicaoAtual + maxCaracteres).IndexOf("\n\n\n\n") > 0)
                        {
                            if ((_textoOriginal.Substring(posicaoAtual, posicaoAtual + maxCaracteres).IndexOf("\n\n\n\n") <= maxCaracteres))
                            {
                                posicaoUltimoEspaco = _textoOriginal.Substring(posicaoAtual, posicaoAtual + maxCaracteres).IndexOf("\n\n\n\n");
                            }
                            else
                            {
                                posicaoUltimoEspaco = _textoOriginal.Substring(posicaoAtual, posicaoAtual + maxCaracteres).LastIndexOf(" ");
                            }
                        }
                    }
                    else
                    {
                        if (_textoOriginal.Substring(posicaoAtual, posicaoAtual + maxCaracteres).LastIndexOf(" ") > 0)
                        {
                            //Define a posição do ultimo espaço em branco no texto selecionado
                            posicaoUltimoEspaco = _textoOriginal.Substring(posicaoAtual, posicaoAtual + maxCaracteres).LastIndexOf(" ");
                            linhasPosTexto++;
                        }
                        else
                        {
                            posicaoUltimoEspaco = posicaoAtual + maxCaracteres;
                        }
                    }

                    if (_textoOriginal.Substring(posicaoAtual, posicaoUltimoEspaco).Length > maxCaracteres)
                    {
                        if (_textoOriginal.Substring(posicaoAtual, maxCaracteres).Contains("."))
                        {
                            posicaoUltimoEspaco = _textoOriginal.Substring(posicaoAtual, maxCaracteres).IndexOf(".");
                        }
                        else
                        {
                            posicaoUltimoEspaco = _textoOriginal.Substring(posicaoAtual, maxCaracteres).LastIndexOf(" ");
                            linhasPosTexto++;
                        }
                    }

                    if (posicaoUltimoEspaco <= 0)
                    {
                        posicaoUltimoEspaco = posicaoAtual;
                    }

                    //Escreve o texto
                    if (linhas > 0)
                    {
                        for (int i = 0; i < linhas; i++)
                        {
                            textoResultado += "\n";
                        }
                    }
                    textoResultado += _textoOriginal.Substring(posicaoAtual, posicaoUltimoEspaco);
                    //Escreve o texto
                    if (linhasPosTexto > 0)
                    {
                        for (int i = 0; i < linhasPosTexto; i++)
                        {
                            textoResultado += "\n";
                        }
                    }
                }
                else
                {
                    if (_textoOriginal.Substring(posicaoAtual, maxCaracteres).Contains("\n"))
                    {
                        if (_textoOriginal.Substring(posicaoAtual, maxCaracteres).IndexOf("\n") > 0)
                        {
                            if ((_textoOriginal.Substring(posicaoAtual, maxCaracteres).IndexOf("\n") <= maxCaracteres))
                            {
                                posicaoUltimoEspaco = _textoOriginal.Substring(posicaoAtual, maxCaracteres).IndexOf("\n");
                            }
                            else
                            {
                                posicaoUltimoEspaco = _textoOriginal.Substring(posicaoAtual, maxCaracteres).LastIndexOf(" ");
                            }

                        }
                        else if (_textoOriginal.Substring(posicaoAtual, maxCaracteres).IndexOf(".\n") > 0)
                        {
                            if ((_textoOriginal.Substring(posicaoAtual, maxCaracteres).IndexOf(".\n") < maxCaracteres))
                            {
                                posicaoUltimoEspaco = _textoOriginal.Substring(posicaoAtual, maxCaracteres).IndexOf(".\n") + 1;
                            }
                            else
                            {
                                posicaoUltimoEspaco = _textoOriginal.Substring(posicaoAtual, maxCaracteres).LastIndexOf(" ");
                            }
                        }
                    }
                    else if (_textoOriginal.Substring(posicaoAtual, maxCaracteres).Contains("\n\n"))
                    {
                        if (_textoOriginal.Substring(posicaoAtual, maxCaracteres).IndexOf("\n\n") > 0)
                        {
                            if ((_textoOriginal.Substring(posicaoAtual, maxCaracteres).IndexOf("\n\n") <= maxCaracteres))
                            {
                                posicaoUltimoEspaco = _textoOriginal.Substring(posicaoAtual, maxCaracteres).IndexOf("\n\n");
                            }
                            else
                            {
                                posicaoUltimoEspaco = _textoOriginal.Substring(posicaoAtual, maxCaracteres).LastIndexOf(" ");
                            }
                        }
                        else if (_textoOriginal.Substring(posicaoAtual, maxCaracteres).IndexOf(".\n\n") > 0)
                        {
                            if ((_textoOriginal.Substring(posicaoAtual, maxCaracteres).IndexOf(".\n\n") < maxCaracteres))
                            {
                                posicaoUltimoEspaco = _textoOriginal.Substring(posicaoAtual, maxCaracteres).IndexOf(".\n\n") + 1;
                            }
                            else
                            {
                                posicaoUltimoEspaco = _textoOriginal.Substring(posicaoAtual, maxCaracteres).LastIndexOf(" ");
                            }
                        }
                        else if (_textoOriginal.Substring(posicaoAtual, maxCaracteres).IndexOf("\n\n\n\n") > 0)
                        {
                            if ((_textoOriginal.Substring(posicaoAtual, maxCaracteres).IndexOf("\n\n\n\n") <= maxCaracteres))
                            {
                                posicaoUltimoEspaco = _textoOriginal.Substring(posicaoAtual, maxCaracteres).IndexOf("\n\n\n\n");
                            }
                            else
                            {
                                posicaoUltimoEspaco = _textoOriginal.Substring(posicaoAtual, maxCaracteres).LastIndexOf(" ");
                            }
                        }
                    }
                    else
                    {
                        if (_textoOriginal.Substring(posicaoAtual, maxCaracteres).LastIndexOf(" ") > 0)
                        {
                            //Define a posição do ultimo espaço em branco no texto selecionado
                            posicaoUltimoEspaco = _textoOriginal.Substring(posicaoAtual, maxCaracteres).LastIndexOf(" ");
                            linhasPosTexto++;
                        }
                        else
                        {
                            posicaoUltimoEspaco = posicaoAtual + maxCaracteres;
                        }
                    }

                    if (_textoOriginal.Substring(posicaoAtual, posicaoUltimoEspaco).Length > maxCaracteres)
                    {
                        if (_textoOriginal.Substring(posicaoAtual, maxCaracteres).Contains("."))
                        {
                            posicaoUltimoEspaco = _textoOriginal.Substring(posicaoAtual, maxCaracteres).IndexOf(".");
                        }
                        else
                        {
                            posicaoUltimoEspaco = _textoOriginal.Substring(posicaoAtual, maxCaracteres).LastIndexOf(" ");
                            linhasPosTexto++;
                        }
                    }

                    if (posicaoUltimoEspaco <= 0)
                    {
                        posicaoUltimoEspaco = posicaoAtual;
                    }

                    //Escreve o texto
                    if (linhas > 0)
                    {
                        for (int i = 0; i < linhas; i++)
                        {
                            textoResultado += "\n";
                        }
                    }
                    textoResultado += _textoOriginal.Substring(posicaoAtual, posicaoUltimoEspaco);
                    //Escreve o texto
                    if (linhasPosTexto > 0)
                    {
                        for (int i = 0; i < linhasPosTexto; i++)
                        {
                            textoResultado += "\n";
                        }
                    }
                }

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
        }
        #endregion
    }

    public class FuncoesVariaveis
    {
        private BDCONN conexao = new BDCONN();

        #region Tarefas
        /// <summary>
        /// Método responsável por verificar como está os combobox na tela inicial e realizar os filtros. 
        /// </summary>
        /// <param name="posicaoCmbTipoTarefas">Posição da comboBox tipo tarefas na tela de tarefas</param>
        public string VerificaComboBoxTarefas(int posicaoCmbTipoTarefas)
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
        /// Método responsável por verificar como está os combobox na tela inicial e realizar os filtros. 
        /// </summary>
        /// <param name="posicaoCmbFiltros">Posição da comboBox filtro na tela de tarefas</param>
        /// <param name="posicaoCmbTipoTarefas">Posição da comboBox tipo tarefas na tela de tarefas</param>
        /// <returns></returns>
        public string VerificaComboBoxTarefas(int posicaoCmbFiltros, int posicaoCmbTipoTarefas)
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
        /// Método que retorna o id da tarefa desejada
        /// </summary>
        /// <param name="empresa">Nome da empresa informado na tarefa</param>
        /// <param name="funcionario">A quem a tarefa esta atribuida</param>
        /// <param name="assunto">O assunto da tarefa</param>
        /// <returns></returns>
        public int AbreTarefa(string empresa, string funcionario, string assunto)
        {
            int resultado = 0, idEmpresa = 0, idFuncionario = 0;
            string comando = null;

            try
            {
                comando = "Select ID from tbl_contato"
                    + " where nome = '" + empresa + "';";
                idEmpresa = Int32.Parse(conexao.ConsultaSimples(comando));

                comando = "Select ID from tbl_funcionarios"
                    + " where nome = '" + funcionario + "';";
                idFuncionario = Int32.Parse(conexao.ConsultaSimples(comando));

                comando = "Select ID from tbl_tarefas"
                   + " where empresa = '" + idEmpresa + "' AND funcionario = '" + idFuncionario +
                   "' AND assunto = '" + assunto + "';";

                resultado = Int32.Parse(conexao.ConsultaSimples(comando));
            }
            catch (Exception)
            {
                string erro = ListaErro.RetornaErro(15);
                int separador = erro.IndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }

            return resultado;
        }

        /// <summary>
        /// Método responsável por travar a tarefa
        /// </summary>
        /// <param name="idTarefa">ID da tarefa que deseja travar</param>
        /// <returns></returns>
        public bool TravaTarefa(int tarefa)
        {
            bool resultado = false;

            if (!conexao.TarefaBloqueada(tarefa))
            {
                conexao.ExecutaComando("Update tbl_tarefas set travar = 'S' where id = " + tarefa.ToString() + ";");
                resultado = true;
            }

            return resultado;
        }

        /// <summary>
        /// Método responsável por destravar a tarefa
        /// </summary>
        /// <param name="idTarefa">ID da tarefa que deseja destravar</param>
        /// <returns></returns>
        public void DestravaTarefa(int tarefa)
        {
            if (conexao.TarefaBloqueada(tarefa))
            {
                conexao.ExecutaComando("Update tbl_tarefas set travar = 'N' where id = " + tarefa.ToString() + ";");
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
        public int CadastrarTarefa(string empresa, string funcionario, int status, string assunto, string dataInicio, string dataFinal, int prioridade, string texto, bool travar)
        {
            int resultado = 0;

            string comando = null, textoTravar = "N";
            int idEmpresa = 0, idFuncionario = 0;

            try
            {
                comando = "Select ID from tbl_contato where nome = '" + empresa + "';";
                idEmpresa = Int32.Parse(conexao.ConsultaSimples(comando));

                comando = "Select ID from tbl_funcionarios where nome = '" + funcionario + "';";
                idFuncionario = Int32.Parse(conexao.ConsultaSimples(comando));
            }
            catch (Exception)
            {
                string erro = ListaErro.RetornaErro(16);
                int separador = erro.IndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            finally
            {
                if (travar)
                {
                    textoTravar = "S";
                }

                comando = "insert into tbl_tarefas values (0," + idEmpresa + "," + idFuncionario
                    + "," + status + ",'" + assunto + "','" + dataInicio + "', '" + dataFinal
                    + "'," + prioridade + ",'" + texto + "','" + textoTravar + "');";
                conexao.ExecutaComando(comando);

                comando = "Select ID from tbl_tarefas where empresa = '" + idEmpresa
                    + "' AND funcionario = '" + idFuncionario
                    + "' AND assunto = '" + assunto + "';";

                resultado = Int32.Parse(conexao.ConsultaSimples(comando));
            }

            return resultado;
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
        public bool AtualizarTarefa(int tarefa, string empresa, string funcionario, int status, string assunto, string dataInicio, string dataFinal, int prioridade, string texto)
        {
            bool resultado = false;

            string comando = null;
            int idEmpresa = 0, idFuncionario = 0;

            try
            {
                comando = "Select ID from tbl_contato where nome = '" + empresa + "';";
                idEmpresa = Int32.Parse(conexao.ConsultaSimples(comando));

                comando = "Select ID from tbl_funcionarios where nome = '" + funcionario + "';";
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
                            "datainicial = '" + dataInicio + "', datafinal = '" + dataFinal + "', " +
                            "prioridade = " + prioridade + ", texto = '" + texto + "' " +
                            "Where id = " + tarefa + ";";

                conexao.ExecutaComando(comando);

                resultado = true;
            }

            return resultado;
        }

        /// <summary>
        /// Método responsável por apagar uma tarefa
        /// </summary>
        /// <param name="tarefa">ID da tarefa que deseja apagar</param>
        public bool ApagarTarefa(int tarefa)
        {
            bool resultado = false;

            try
            {
                conexao.ExecutaComando("delete from tbl_tarefas where id = " + tarefa + ";");
            }
#pragma warning disable CS0168 // A variável "e" está declarada, mas nunca é usada
            catch (Exception e)
#pragma warning restore CS0168 // A variável "e" está declarada, mas nunca é usada
            {
                return resultado;
            }
            finally
            {
                resultado = true;
            }

            return resultado;
        }

        /// <summary>
        /// Método responsável por destravar todas as tarefas do banco de dados.
        /// Utilizar apenas se ocorrer algum desligamento inesperado de algum usuário.
        /// </summary>
        /// <returns>Se verdadeiro, a operação foi um sucesso.</returns>
        public bool DestravaTodasTarefas()
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

        #region Clientes
        public int AbreCliente(string nome)
        {
            int resultado = 0;
            string comando = null;

            try
            {
                comando = "Select ID from tbl_contato"
                   + " where nome = '" + nome + "';";
                resultado = Int32.Parse(conexao.ConsultaSimples(comando));
            }
            catch (Exception)
            {
                string erro = ListaErro.RetornaErro(20);
                int separador = erro.IndexOf(":");
                MessageBox.Show(erro.Substring((separador + 2)), erro.Substring(0, (separador - 1)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }

            return resultado;
        }
        #endregion

        #region Fornecedores
        public int CadastraFornecedor(string tipo, string dataCadastro, string dataNascimento, string documento, string nome,
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

        public void AtualizarFornecedor(string id, string tipo, string dataNascimento, string documento, string nome,
           string apelido, string cep, string endereco, string numero, string complemento, string bairro, string cidade, string estado,
           string pais, string telefone, string contato, string telefoneComercial, string contatoComercial, string celular, string contatoCelular,
           string email, string site, string inscEstadual, string inscMunicipal, string obs, string categoria1, string categoria2, string categoria3,
           string subcategoria1, string subcategoria2, string subcategoria3)
        {
            string comando = null, _dataNascimento = null, erro = "";
            int separador = 0;

            try
            {
                if (!string.IsNullOrEmpty(dataNascimento))
                {
                    _dataNascimento = dataNascimento.Substring(6, 4) + "-" + dataNascimento.Substring(3, 2) + "-" + dataNascimento.Substring(0, 2);
                }

                comando = "update tbl_fornecedor set tipo = '" + tipo + "', dataNascimento = '" +_dataNascimento + "', documento = '" + documento + "'," +
                    " nome = '" + nome + "', apelido = '" + apelido + "', cep = '" + cep + "', endereco = '" + endereco + "', numero = '" + numero + "', complemento = '" + complemento + "'," +
                    " bairro = '" + bairro + "', cidade = '" + cidade + "', estado = '" + estado + "', pais = '" + pais + "', telefone = '" + telefone + "', contato = '" + contato + "'," +
                    " telefoneComercial = '" + telefoneComercial + "', contatoComercial = '" + contatoComercial + "', celular = '" + celular + "', contatoCelular = '" + contatoCelular + "'," +
                    " email = '" + email + "', site = '" + site + "', inscricaoEstadual = '" + inscEstadual + "', inscricaoMunicipal = '" + inscMunicipal + "', observacoes = '" + obs + "'," +
                    " categoria1 = '" + categoria1 + "', categoria2 = '" + categoria2 + "', categoria3 = '" + categoria3 + "', subcategoria1 = '" + subcategoria1 + "'," +
                    " subcategoria2 = '" + subcategoria2 + "', subcategoria3 = '" + subcategoria3 + "' where id = '" + id + "';";
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
        public bool TravaFornecedor(int _idFornecedor)
        {
            bool resultado = false;

            if (!conexao.FornecedorBloqueado(_idFornecedor))
            {
                conexao.ExecutaComando("Update tbl_fornecedor set travar = 'S' where id = '" + _idFornecedor.ToString() + "';");
                resultado = true;
            }

            return resultado;
        }

        /// <summary>
        /// Método responsável por destravar o fornecedor
        /// </summary>
        /// <param name="_idFornecedor">ID da tarefa que deseja destravar</param>
        /// <returns></returns>
        public void DestravaFornecedor(int _idFornecedor)
        {
            if (conexao.FornecedorBloqueado(_idFornecedor))
            {
                conexao.ExecutaComando("Update tbl_fornecedor set travar = 'N' where id = '" + _idFornecedor.ToString() + "';");
            }
        }

        /// <summary>
        /// Método responsável por destravar todos os Fornecedores do banco de dados.
        /// Utilizar apenas se ocorrer algum desligamento inesperado de algum usuário.
        /// </summary>
        /// <returns>Se verdadeiro, a operação foi um sucesso.</returns>
        public bool DestravaTodosFornecedores()
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

        //Desativado
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

        public struct LayoutJson
        {
            private string _index, _value;

            public LayoutJson(String Index, String Value)
            {
                _index = Index;
                _value = Value;
            }

            public string Index
            {
                get { return _index; }
                set { _index = value; }
            }

            public string Value
            {
                get { return _value; }
                set { _value = value; }
            }
        }
    }
}
