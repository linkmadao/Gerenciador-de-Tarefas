using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace Gerenciador_de_Tarefas.Classes
{
    public static class Cliente
    {
        #region Variaveis
        private static BDCONN conexao = new BDCONN();

        private static int id, contrato;
        private static string dataCadastro, nome, razaoSocial, telefoneComercial, contato, setor, cpf, rg, cnpj, inscricaoEstadual, inscricaoMunicipal,
            site, email, endereco, numero, bairro, cidade, estado, cep, complemento, pontoReferencia, obs;
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
        public static string TelefoneComercial
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

        #endregion

        #region Funcoes
        public static void CadastrarCliente()
        {
            string comando = string.Format("insert into tbl_contato values " +
            "(0,'{0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}');",
            DataCadastro,Nome,RazaoSocial,TelefoneComercial,Contato,Setor,CPF,RG,CNPJ,InscricaoEstadual,InscricaoMunicipal,Site,Email,Endereco, Numero, Bairro, 
            Cidade, Estado, CEP, Complemento, PontoReferencia, Obs);
            conexao.ExecutaComando(comando);

            comando = string.Format("Select id from tbl_contato where nome = '{0}';",Nome);
            ID = Int32.Parse(conexao.ConsultaSimples(comando));

            comando = string.Format("insert into tbl_contato_contrato values ({0},{1});", ID, Contrato);
            conexao.ExecutaComando(comando);

            Log.CadastrarCliente(ID);
        }

        public static void ApagarCliente()
        {
            conexao.ExecutaComando("delete from tbl_tarefas where empresa = " + ID + ";");
            conexao.ExecutaComando("delete from tbl_contato_subsubgrupo where contato = " + ID + ";");
            conexao.ExecutaComando("delete from tbl_contato_subgrupo where contato = " + ID + ";");
            conexao.ExecutaComando("delete from tbl_contato_telefone where contato = " + ID + ";");
            conexao.ExecutaComando("delete from tbl_contato_contrato where contato = " + ID + ";");
            conexao.ExecutaComando("delete from tbl_contato where id = " + ID + ";");

            Log.ApagarCliente(ID);
        }
        #endregion
    }
}
