using System;
using System.Linq;


internal static class Program
{
    internal class NoDaArvore
    {
        public string valor { get; set; }
        public NoDaArvore esquerda { get; set; }
        public NoDaArvore direita { get; set; }

        public NoDaArvore(string sValor)
        {
            valor = sValor;
        }

        public override string ToString()
        {
            return "" + valor + "[" + esquerda + "][" + direita + "]";
        }
    }

    public class ArvoreBinaria
    {
        public NoDaArvore root;

        public void Inserir(string valor, string valorPai)
        {
            var noDA = new NoDaArvore(valor);
            if (root == null)
            {
                root = noDA;
            }
            else
            {
                InserirNoDaArvore(root, noDA, valorPai);
            }
        }

        private void InserirNoDaArvore(NoDaArvore noDA, NoDaArvore newNode, string valorPai)
        {
            if(noDA.esquerda == null)
            {
                noDA.esquerda = newNode;
            }
            else if (noDA.direita == null)
            {
                if (noDA.esquerda.ToString().Contains(valorPai))
                {
                    InserirNoDaArvore(noDA.esquerda, newNode, valorPai);
                }
                else
                {
                    noDA.direita = newNode;
                }
            }
            else if ((noDA.ToString().Contains(newNode.valor) == false)
                        && (noDA.esquerda.ToString().Contains(newNode.valor) == false)
                        && (noDA.direita.ToString().Contains(newNode.valor) == false))
            {
                if (noDA.esquerda.ToString().Contains(valorPai))
                {
                    InserirNoDaArvore(noDA.esquerda, newNode, valorPai);
                }
                else if (noDA.direita.ToString().Contains(valorPai))
                {
                    InserirNoDaArvore(noDA.direita, newNode, valorPai);
                }
            }
        }
    }

    public class NoDados
    {
        public string Valor { get; set; }
        public NoDados[] filhos { get; set; }
        public NoDados()
        {
            filhos = new NoDados[2];
        }
    }

    static void Main()
    {
        string[,] exemplo1 = new string[,]{
                                        { "A", "B" }
                                        , { "A", "C" }
                                        , { "B", "G" }
                                        , { "C", "H" }
                                        , { "E", "F" }
                                        , { "B", "D" }
                                        , { "C", "E" }
                                        };

        NoDados[] arvoreExemplo1 = organizaArray(exemplo1);

        ArvoreBinaria arvoreBinaria_Exemplo_1 = new ArvoreBinaria();
        foreach (NoDados itemPai in arvoreExemplo1.OrderBy(x => x.Valor))
        {
            arvoreBinaria_Exemplo_1.Inserir(itemPai.Valor, string.Empty);

            foreach (NoDados itemFilho in itemPai.filhos)
            {
                if (itemFilho != null)
                {
                    arvoreBinaria_Exemplo_1.Inserir(itemFilho.Valor, itemPai.Valor);
                }
            }
        }

        if (arvoreBinaria_Exemplo_1.root != null)
        {
            Console.WriteLine("Exemplo 1: " + arvoreBinaria_Exemplo_1.root.ToString().Substring(0,1) 
                                            + "[" 
                                            + (arvoreBinaria_Exemplo_1.root.ToString().Substring(1).Replace("[]", "")) 
                                            + "]");
            Console.WriteLine("");
        }

        string[,] exemplo2 = new string[,]{
                                        { "B", "D" }
                                        , { "D", "E" }
                                        , { "A", "B" }
                                        , { "C", "F" }
                                        , { "E", "G" }
                                        , { "A", "C" }
                                        };

        NoDados[] arvoreExemplo2 = organizaArray(exemplo2);

        ArvoreBinaria arvoreBinaria_Exemplo_2 = new ArvoreBinaria();
        foreach (NoDados itemPai in arvoreExemplo2.OrderBy(x => x.Valor))
        {
            arvoreBinaria_Exemplo_2.Inserir(itemPai.Valor, string.Empty);

            foreach (NoDados itemFilho in itemPai.filhos)
            {
                if (itemFilho != null)
                {
                    arvoreBinaria_Exemplo_2.Inserir(itemFilho.Valor, itemPai.Valor);
                }
            }
        }

        if (arvoreBinaria_Exemplo_2.root != null)
        {
            Console.WriteLine("Exemplo 2: " + arvoreBinaria_Exemplo_2.root.ToString().Substring(0, 1)
                                            + "["
                                            + (arvoreBinaria_Exemplo_2.root.ToString().Substring(1).Replace("[]", ""))
                                            + "]");
            Console.WriteLine("");
        }


        //precisa dar o erro E3
        string[,] exemplo3 = new string[,]{
                                        { "A", "B" }
                                        , { "A", "C" }
                                        , { "B", "D" }
                                        , { "D", "C" }
                                        };

        NoDados[] arvoreExemplo3 = organizaArray(exemplo3);

        ArvoreBinaria arvoreBinaria_Exemplo_3 = new ArvoreBinaria();
        foreach (NoDados itemPai in arvoreExemplo3.OrderBy(x => x.Valor))
        {
            arvoreBinaria_Exemplo_3.Inserir(itemPai.Valor, string.Empty);

            foreach (NoDados itemFilho in itemPai.filhos)
            {
                if (itemFilho != null)
                {
                    arvoreBinaria_Exemplo_3.Inserir(itemFilho.Valor, itemPai.Valor);
                }
            }
        }
        if (arvoreBinaria_Exemplo_3.root != null)
        {
            Console.WriteLine("Exemplo 3: " + arvoreBinaria_Exemplo_3.root.ToString().Substring(0, 1)
                                            + "["
                                            + (arvoreBinaria_Exemplo_3.root.ToString().Substring(1).Replace("[]", ""))
                                            + "]");
            Console.WriteLine("");
        }
    }

    public static NoDados[] organizaArray(string[,] arrayDados)
    {
        try
        {
            //Cria um array de No de Dados aonde contem o Valor e um No de Dados de no MAX 2 filhos
            NoDados[] arrayOrganizado = new NoDados[(arrayDados.Length / 2)];

            int count = 0;
            //for passando pelos campos pai primeiro
            for (var i = 0; i < (arrayDados.Length / 2); i++)
            {
                bool paiJaExiste = false;
                //for para passar pelo array, validar se pai já existe no No de Dados
                for (var j = 0; j < i; j++)
                {
                    //validação para objeto nullo
                    if (arrayOrganizado[j] != null)
                    {
                        //Caso pai já existe entra no if e faz adicionar segundo filho.
                        if (arrayOrganizado[j].Valor == (arrayDados[i, 0]))
                        {
                            paiJaExiste = true;

                            if (arrayOrganizado[j].filhos[1] == null)
                            {
                                if (arrayOrganizado[j].filhos[0].Valor != arrayDados[i, 1])
                                {
                                    if (validarFilhoTemPai(arrayDados[i, 1], arrayOrganizado))
                                    {
                                        Console.WriteLine("Raízes múltiplas");
                                        NoDados[] noDVazio = new NoDados[0];
                                        return noDVazio;
                                    }
                                    arrayOrganizado[j].filhos[1] = new NoDados();
                                    arrayOrganizado[j].filhos[1].Valor = arrayDados[i, 1];
                                }
                                else
                                {
                                    //Ciclo presente - ciclo já existe no sistema, assim não podendo adicionar novamente.
                                    Console.WriteLine("Ciclo presente");
                                    NoDados[] noDVazio = new NoDados[0];
                                    return noDVazio;
                                }
                            }
                            else
                            {
                                //Mais de 2 filhos - Quando no array o pai tiver mais de 2 filhos.
                                Console.WriteLine("Mais de 2 filhos");
                                NoDados[] noDVazio = new NoDados[0];
                                return noDVazio;
                            }
                        }
                    }
                }
                //Caso pai não exista ainda adiciona ele e seu primeiro filho.
                if (!paiJaExiste)
                {
                    arrayOrganizado[count] = new NoDados();
                    arrayOrganizado[count].Valor = arrayDados[i, 0];

                    if (validarFilhoTemPai(arrayDados[i, 1], arrayOrganizado))
                    {
                        Console.WriteLine("Raízes múltiplas");
                        NoDados[] noDVazio = new NoDados[0];
                        return noDVazio;
                    }
                    arrayOrganizado[count].filhos[0] = new NoDados();
                    arrayOrganizado[count].filhos[0].Valor = arrayDados[i, 1];
                    count++;
                }
            }

            return arrayOrganizado.Where(x => x != null).ToArray();

        }
        catch//(Exception ex)
        {
            //Caso ocorre algum erro no processo cairia aqui, pode-se colocar a Exception dentro do catch
            Console.WriteLine("Qualquer outro erro");
            NoDados[] noDVazio = new NoDados[0];
            return noDVazio;
        }
    }

    private static bool validarFilhoTemPai(string valorFilho, NoDados[] arrayOrganizado)
    {
        //Validar se filho já tem pai, caso já tenha retorna true.
        foreach (NoDados item in arrayOrganizado)
        {
            if (item != null)
            {
                if (item.filhos[0] != null || item.filhos[1] != null)
                {
                    foreach (NoDados itemFilho in item.filhos)
                    {
                        if (itemFilho != null)
                        {
                            if (itemFilho.Valor == valorFilho)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            else
            {
                break;
            }
        }

        return false;
    }

}
