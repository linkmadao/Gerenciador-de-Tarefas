using System;
using System.Windows.Forms;

namespace Gerenciador_de_Tarefas.Classes
{
    public static class Funcoes
    {
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

        public static string FormataNumeroTelefone(this long number)
        {
            return number.ToString(@"(000) 0000-0000");
        }

        public static string FormataNumeroTelefone(this string number)
        {
            return long.Parse(number).FormataNumeroTelefone();
        }

        public static string FormataNumeroCelular(this long number)
        {
            return number.ToString(@"(000) 00000-0000");
        }

        public static string FormataNumeroCelular(this string number)
        {
            return long.Parse(number).FormataNumeroCelular();
        }

        public static string FormataData(this long number)
        {
            return number.ToString(@"00/00/0000");
        }

        public static string FormataData(this string number)
        {
            return long.Parse(number).FormataData();
        }

        public static string FormataCNPJ(this long number)
        {
            return number.ToString(@"00,000,000/0000-00");
        }

        public static string FormataCNPJ(this string number)
        {
            return long.Parse(number).FormataCNPJ();
        }

        public static string FormataCPF(this long number)
        {
            return number.ToString(@"000,000,000-00");
        }

        public static string FormataCPF(this string number)
        {
            return long.Parse(number).FormataCPF();
        }

        public static string FormataCEP(this long number)
        {
            return number.ToString(@"00000-000");
        }

        public static string FormataCEP(this string number)
        {
            return long.Parse(number).FormataCEP();
        }
        
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
    }
}
