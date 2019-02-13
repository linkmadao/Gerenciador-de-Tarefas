
using System.Collections;
using System.Drawing;

namespace Gerenciador_de_Tarefas.Classes
{
    public static class Impressao
    {
        #region Variaveis
        private static ArrayList arrColunasRestantes = new ArrayList(), arrLarguraColunas = new ArrayList();

        private static int alturaCabecalho = 0, alturaCelula = 0, larguraTemp = 0, larguraTotal = 0, 
            linhaAtual = 0, linhas = 0, margemEsquerda = 0, margemSuperior = 0, posTexto = 0,
            posUltimoEspaco = 0, qtdMaximaLinhas = 60, qtdMaximaCaracteres = 100;

        #endregion

        #region Propriedades
        #region ArrayList
        /// <summary>
        /// Usado para salvar as coordenadas das colunas restantes
        /// </summary>
        public static ArrayList ArrColunasRestantes
        {
            get
            {
                return arrColunasRestantes;
            }
            set
            {
                arrColunasRestantes = value;
            }
        }
        
        /// <summary>
        /// Usado para salvar as larguras das colunas
        /// </summary>
        public static ArrayList ArrLarguraColunas
        {
            get
            {
                return arrLarguraColunas;
            }
            set
            {
                arrLarguraColunas = value;
            }
        }
        #endregion

        #region bool
        /// <summary>
                /// Usado para verificar se esta imprimindo uma nova página
                /// </summary>
        public static bool NovaPagina
        {
            get; set;
        }

        /// <summary>
        /// Usado para verificar se esta imprimindo a primeira página
        /// </summary>
        public static bool PrimeiraPagina
        {
            get; set;
        }

        /// <summary>
        /// Verifica se há mais páginas para imprimir
        /// </summary>
        public static bool TemMaisPaginasParaImprimir
        {
            get; set;
        }
        #endregion

        #region int
        /// <summary>
        /// Usado para armazenar o tamanho do cabeçalho
        /// </summary>
        public static int AlturaCabecalho
        {
            get
            {
                return alturaCabecalho;
            }
            set
            {
                alturaCabecalho = value;
            }
        }

        /// <summary>
        /// Usado para armazenar/pegar a altura da célula do DataGridView
        /// </summary>
        public static int AlturaCelula
        {
            get
            {
                return alturaCelula;
            }
            set
            {
                alturaCelula = value;
            }
        }

        /// <summary>
        /// Conta a quantidade de linhas que o texto possui
        /// </summary>
        public static int ContadorLinhas
        {
            get
            {
                return linhas;
            }
            set
            {
                linhas = value;
            }
        }
        
        /// <summary>
        /// Usado para armazenar a largura temporária para gerar a margem
        /// </summary>
        public static int LarguraTemporaria
        {
            get
            {
                return larguraTemp;
            }
            set
            {
                larguraTemp = value;
            }
        }

        /// <summary>
        /// Usado para armazenar a largura total das colunas
        /// </summary>
        public static int LarguraTotal
        {
            get
            {
                return larguraTotal;
            }
            set
            {
                larguraTotal = value;
            }
        }

        /// <summary>
        /// Usado para armazenar a largura total das colunas
        /// </summary>
        public static int LinhaAtual
        {
            get
            {
                return linhaAtual;
            }
            set
            {
                linhaAtual = value;
            }
        }

        /// <summary>
        /// Seta a margem da folha do lado esquerdo.
        /// </summary>
        public static int MargemEsquerda
        {
            get
            {
                return margemEsquerda;
            }
            set
            {
                margemEsquerda = value;
            }
        }

        /// <summary>
        /// Seta a margem da folha da parte superior.
        /// </summary>
        public static int MargemSuperior
        {
            get
            {
                return margemSuperior;
            }
            set
            {
                margemSuperior = value;
            }
        }
        
        /// <summary>
        /// Máximo de caracteres que poderá ter em uma linha durante a impressão de um documento
        /// </summary>
        public static int MaximoCaracteres
        {
            get
            {
                return qtdMaximaCaracteres;
            }
            set
            {
                qtdMaximaCaracteres = value;
            }
        }

        /// <summary>
        /// Máximo de caracteres que poderá ter em uma linha durante a impressão de um documento
        /// </summary>
        public static int MaximoLinhas
        {
            get
            {
                return qtdMaximaLinhas;
            }
        }

        /// <summary>
        /// Usado para armazenar/retornar do último espaço do texto informado
        /// </summary>
        public static int PosicaoUltimoEspaco
        {
            get
            {
                return posUltimoEspaco;
            }
            set
            {
                posUltimoEspaco = value;
            }
        }

        /// <summary>
        /// Usado para armazenar/retornar a posição onde o texto parou
        /// </summary>
        public static int PosicaoTexto
        {
            get
            {
                return posTexto;
            }
            set
            {
                posTexto = value;
            }
        }
        #endregion

        #region Font
        /// <summary>
        /// Retorna a fonte utilizada para fazer imprimir o título do texto.
        /// </summary>
        public static Font FonteCabecalho
        {
            get
            {
                return new Font("Arial", 12, FontStyle.Bold);
            }
        }
        /// <summary>
        /// Retorna a fonte utilizada para fazer imprimir o título do texto.
        /// </summary>
        public static Font FonteTexto
        {
            get
            {
                return new Font("Arial", 10);
            }
        }
        /// <summary>
        /// Retorna a fonte utilizada para fazer imprimir o título do texto.
        /// </summary>
        public static Font FonteTextoAlternativo
        {
            get
            {
                return new Font("Arial", 9);
            }
        }
        #endregion

        #region Graphics
        public static Graphics Graficos
        {
            get; set;
        }
        #endregion

        #region Pen
        /// <summary>
        /// Usado para gerar uma linha preta na impressão
        /// </summary>
        public static Pen LinhaPreta
        {
            get
            {
                return new Pen(Color.Black, 3);
            }
        }
        #endregion

        #region SolidBrush
        /// <summary>
        /// Usado para setar a cor da fonte como preta
        /// </summary>
        public static SolidBrush FonteCorPreta
        {
            get
            {
                return new SolidBrush(Color.Black);
            }
        }
        /// <summary>
        /// Usado para setar a cor da fonte como cinza claro
        /// </summary>
        public static SolidBrush FonteCorCinzaClaro
        {
            get
            {
                return new SolidBrush(Color.LightGray);
            }
        }
        #endregion

        #region string
        public static string LinhaTemporaria
        {
            get; set;
        }
        #endregion

        #region StringFormat
        /// <summary>
        /// Usado para formatar as linhas da tabela
        /// </summary>
        public static StringFormat FormatoString
        {
            get; set;
        }
        #endregion
        #endregion

        #region Funcoes
        #endregion
    }
}
