using System;

namespace JogoDaVelha
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "### JOGO DA VELHA ###";

            MenuPrincipal();
        }

        static void MenuPrincipal()
        {
            string opcao;


            do
            {
                Console.Clear();
                Console.WriteLine("OLÁ JOGADOR!\n");
                Console.WriteLine("DENTRE AS OPÇÕES NO MENU, QUAL DESEJA EXECUTAR?");
                Console.WriteLine("\t|°°°° MENU  PRINCIPAL °°°°|");
                Console.WriteLine("\t|   opção 0 : sair        |");
                Console.WriteLine("\t|                         |");
                Console.WriteLine("\t|   opção 1 : novo jogo   |");
                Console.WriteLine("\t|_________________________|");

                Console.Write("\nInforme a opcao: ");
                opcao = Console.ReadLine();

                if (opcao != "0" && opcao != "1")
                {
                    Console.WriteLine("'" + opcao + "' é uma opcao INVALIDA! Para voltar ao menu, pressione QUALQUER TECLA!");
                    Console.ReadKey();
                    Console.Clear();
                }

                else
                {
                    switch (opcao)
                    {
                        case "0":// dps criar um metodo sair...
                            Console.WriteLine("vc escolheu sair");
                            break;
                        case "1":
                            Console.Clear();
                            NovoJogo();
                            break;
                    }
                }
            } while (opcao != "0");
        }

        static void NovoJogo()
        {
            string opcao;

            do
            {
                Console.WriteLine("\t|°°°°°°° NOVO  JOGO °°°°°°°|");
                Console.WriteLine("\t|   opção 0 : com emoção   |");
                Console.WriteLine("\t|                          |");
                Console.WriteLine("\t|   opção 1 : sem emoção   |");
                Console.WriteLine("\t|__________________________|");

                Console.Write("\nJOGADOR, você deseja jogar com emoção ou sem emoção? ");
                opcao = Console.ReadLine();

                if (opcao != "0" && opcao != "1")
                {
                    Console.WriteLine("'" + opcao + "' é uma opcao INVALIDA! Para voltar ao menu, pressione QUALQUER TECLA!");
                    Console.ReadKey();
                    Console.Clear();
                }

                else
                {
                    switch (opcao)
                    {
                        case "0":
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case "1":
                            Console.Clear();
                            break;
                    }
                }
            } while (opcao != "0" && opcao != "1");

            Jogadores();
            Console.Clear();
            Tabuleiro();
            string linha = "", coluna = "";
            Jogada(linha, coluna);
            // .
            // .
            // .
        }

        static void Jogadores()
        {
            string jogadorX, jogadorO;

            Console.Write("Informe o apelido do jogador X: ");
            jogadorX = Console.ReadLine();

            Console.Write("Informe o apelido do jogador O: ");
            jogadorO = Console.ReadLine();

        }

        static void Tabuleiro()
        {
            char[,] jogoDaVelha = new char[3, 3];
            Console.WriteLine("");
            for (int linha = 0; linha < jogoDaVelha.GetLength(0); linha++)
            {
                if (linha != 2)
                {
                    for (int coluna = 0; coluna < jogoDaVelha.GetLength(1); coluna++)
                    {
                        if (coluna != 2)
                            Console.Write("\t|{0}, {1}| = {2}\t|", linha, coluna, jogoDaVelha[linha, coluna]);
                        else
                            Console.Write("\t|{0}, {1}| = {2}\t", linha, coluna, jogoDaVelha[linha, coluna]);
                    }

                    Console.WriteLine("\n-------------------------------------------------------------------------");
                }
                else
                {
                    for (int coluna = 0; coluna < jogoDaVelha.GetLength(1); coluna++)
                    {
                        if (coluna != 2)
                            Console.Write("\t|{0}, {1}| = {2}\t|", linha, coluna, jogoDaVelha[linha, coluna]);
                        else
                            Console.Write("\t|{0}, {1}| = {2}\t", linha, coluna, jogoDaVelha[linha, coluna]);
                    }
                }
            }

            Console.WriteLine("\n\naperte qq tecla p continuar");
            Console.ReadKey();
        }

        static void Empate()
        {
            Console.WriteLine("o jogo empatou!");
        }

        static void Jogada(string linha, string coluna)
        {
            linha = Linha(linha);
            coluna = Coluna(coluna);
        }

        static string Linha(string linha)
        {
            do
            {
                Console.WriteLine("\tInforme a linha de sua escolha");
                linha = Console.ReadLine();
                if (linha != "0" && linha != "1" && linha != "2")
                {
                    Console.WriteLine("'" + linha + "' é uma linha INVALIDA!");
                }
            } while (linha != "0" && linha != "1" && linha != "2");

            return linha;
        }

        static string Coluna(string coluna)
        {
            do
            {
                Console.WriteLine("\tInforme a coluna de sua escolha");
                coluna = Console.ReadLine();
                if (coluna != "0" && coluna != "1" && coluna != "2")
                {
                    Console.WriteLine("'" + coluna + "' é uma coluna INVALIDA!");
                }
            } while (coluna != "0" && coluna != "1" && coluna != "2");

            return coluna;
        }
    }
}