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
                        case "0":
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
            char[,] jogoDaVelha = new char[3, 3];

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

            Console.Clear();
            ImprimeTabuleiro(jogoDaVelha);

            Console.WriteLine("O primeiro jogador será representado por (X)");
            Console.WriteLine("O segundo jogador será representado por  (O)\n");

            Jogada();

        }

        static void Jogada()
        {
            int contador = 0, linha, coluna, verificaVitoria = 0;
            bool verificadorPosicaoOcupada;
            char x = 'X', o = 'O';
            char[,] jogoDaVelha = new char[3, 3];


            do
            {
                do
                {
                    Console.WriteLine("Jogador X:");
                    linha = Linha(); // chama funcao que pede linha
                    coluna = Coluna(); // chama funcao que pede coluna

                    if (VerificaPosicaoMatriz(linha, coluna, jogoDaVelha)) // verifica se a posicao esta disponivel para ser ocupada
                    {
                        jogoDaVelha[linha, coluna] = x; // como esta disponivel a posicao recebe X
                        Console.WriteLine("\tA ESCOLHA FOI: POSICAO [{0}, {1}] = {2}", linha, coluna, jogoDaVelha[linha, coluna]);
                        Console.WriteLine();
                        ImprimeTabuleiro(jogoDaVelha);

                        contador++; // conta as jogadas do X (maximo de 5 pois é o primeiro a jogar)
                        verificadorPosicaoOcupada = false; // recebe "false" pois nao estava ocupada
                        verificaVitoria = Vitoria(jogoDaVelha, linha, coluna); // verifica se alguem ganhou (caso vitoria, verificaVitoria = 1)
                    }
                    else
                    {
                        verificadorPosicaoOcupada = true;
                        Console.WriteLine("posicao ocupada");
                    }
                } while (verificadorPosicaoOcupada); // enquanto for uma opc ocupada "true" manda digitar de novo ate q nao seja ocupado "false"

                if (contador == 9) // a funçao dar velha so pode ser chamada na ultima jogada, ou seja, count = 9 
                {
                    DarVelha();
                }
                else if (contador < 9 && verificaVitoria == 0) // o jogo não atingiu o maximo de rodadas ainda (9), entao o X ja jogou e nao ganhou, entao falta o O jogar
                {
                    do
                    {
                        Console.WriteLine("Jogador O:");
                        linha = Linha(); // chama funcao que pede linha
                        coluna = Coluna(); // chama funcao que pede coluna

                        if (VerificaPosicaoMatriz(linha, coluna, jogoDaVelha)) // ver se a linha e a coluna que o O digitou esta disponivel
                        {
                            jogoDaVelha[linha, coluna] = o; // se esta disponivel, recebe O

                            Console.WriteLine("\tA ESCOLHA FOI: POSICAO [{0}, {1}] = {2}", linha, coluna, jogoDaVelha[linha, coluna]);
                            Console.WriteLine();

                            ImprimeTabuleiro(jogoDaVelha);

                            contador++; // conta as jogadas do O (maximo de 4 pois é o segundo a jogar)
                            verificadorPosicaoOcupada = false; // como a posicao estava livre, deve sair do laço e recebe falso para poder sair do laço
                            verificaVitoria = Vitoria(jogoDaVelha, linha, coluna); // verifica se alguem ganhou (caso vitoria, verificaVitoria = 1)
                        }
                        else
                        {
                            verificadorPosicaoOcupada = true; // se a posicao estiver ocupada, "true" nao pode sair do laço, executa novamente
                            Console.WriteLine("posicao ocupada");
                        }
                    } while (verificadorPosicaoOcupada); // enquanto a posicao esta ocupada == true, pede posicao novamente ate que nao esteja ocupada (verificaPosicaoOcupada == false)
                }

            } while (contador < 9 && verificaVitoria == 0); // enquanto nao atingiu o maximo de jogadas ainda (velha) ou ninguem ganhou o jogo ainda (vitoria). Passa a vez para o proximo jogador ate que de velha ou alguem ganhe
        }

        static bool VerificaPosicaoMatriz(int linha, int coluna, char[,] jogoDaVelha)
        {
            if (jogoDaVelha[linha, coluna] == 0)
                return true;
            else
                return false;
        }

        static void ImprimeTabuleiro(char[,] jogoDaVelha)
        {
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

        static void DarVelha() // a velha so acontece depois que o X joga pois é ele quem começa e termina o jogo, 5 jogadas
        {
            Console.Clear();
            Console.WriteLine("o jogo empatou!");
            Console.ReadKey();
        }

        static int Vitoria(char[,] jogoDaVelha, int linha, int coluna)
        {
            int vitoria = 0;
            // verificacao X -------------------------------------------------------------------------
            // linhas
            if (jogoDaVelha[linha, 0] == 'X' && jogoDaVelha[linha, 1] == 'X' && jogoDaVelha[linha, 2] == 'X')
            {
                GanhadorX();
                vitoria = 1;
            }

            // colunas
            else if (jogoDaVelha[0, coluna] == 'X' && jogoDaVelha[1, coluna] == 'X' && jogoDaVelha[2, coluna] == 'X')
            {
                GanhadorX();
                vitoria = 1;

            }
            // diagonais
            else if (jogoDaVelha[0, 0] == 'X' && jogoDaVelha[1, 1] == 'X' && jogoDaVelha[2, 2] == 'X')
            {
                GanhadorX();
                vitoria = 1;

            }
            else if (jogoDaVelha[2, 0] == 'X' && jogoDaVelha[1, 1] == 'X' && jogoDaVelha[0, 2] == 'X')
            {
                GanhadorX();
                vitoria = 1;

            }
            // verificacao O -------------------------------------------------------------------------
            // linhas
            if (jogoDaVelha[linha, 0] == 'O' && jogoDaVelha[linha, 1] == 'O' && jogoDaVelha[linha, 2] == 'O')
            {
                GanhadorO();
                vitoria = 1;

            }
            // colunas
            else if (jogoDaVelha[0, coluna] == 'O' && jogoDaVelha[1, coluna] == 'O' && jogoDaVelha[2, coluna] == 'O')
            {
                GanhadorO();
                vitoria = 1;

            }
            // diagonais
            else if (jogoDaVelha[0, 0] == 'O' && jogoDaVelha[1, 1] == 'O' && jogoDaVelha[2, 2] == 'O')
            {
                GanhadorO();
                vitoria = 1;

            }
            else if (jogoDaVelha[2, 0] == 'O' && jogoDaVelha[1, 1] == 'O' && jogoDaVelha[0, 2] == 'O')
            {
                GanhadorO();
                vitoria = 1;

            }
            return vitoria;
        }
        static void GanhadorX()
        {
            Console.WriteLine("Jogador X ganhou!");
            Console.ReadKey();
        }
        static void GanhadorO()
        {
            Console.WriteLine("Jogador O ganhou!");
            Console.ReadKey();
        }

        static int Linha()
        {
            int linha;

            do
            {
                Console.WriteLine("\tInforme a linha de sua escolha");
                linha = int.Parse(Console.ReadLine());
                if (linha != 0 && linha != 1 && linha != 2)
                {
                    Console.WriteLine("'" + linha + "' é uma linha INVALIDA!");
                }
            } while (linha != 0 && linha != 1 && linha != 2);

            return linha;
        }

        static int Coluna()
        {
            int coluna;

            do
            {
                Console.WriteLine("\tInforme a coluna de sua escolha");
                coluna = int.Parse(Console.ReadLine());
                if (coluna != 0 && coluna != 1 && coluna != 2)
                {
                    Console.WriteLine("'" + coluna + "' é uma coluna INVALIDA!");
                }
            } while (coluna != 0 && coluna != 1 && coluna != 2);

            return coluna;
        }
    }
}