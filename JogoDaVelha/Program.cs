using System;
using System.Runtime.Intrinsics.Arm;

namespace JogoDaVelha
{
    internal class Program
    {
        static void Main(string[] args) // chamada do menu
        {
            Console.Title = "### JOGO DA VELHA ###";

            Console.WriteLine();

            Console.WriteLine(@"
                         d8b                                           ##    ##
                         Y8P                                           ##    ## 
                                                                  ##################
                        8888  .d88b.   .d88b.   .d88b.                 ##    ##
                        °888 d88°88b. d88P°°88 d88°88b.                ##    ##  
                         888 888  888 888  888 888  888           ##################     
                         888 Y88..88P Y88b 888 Y88..88P                ##    ##       
                         888  °Y88P'  '°Y88888 '°Y88P°'                ##    ##
                         888               888                                        
                        d88P          Y8b d88P                                       
                      888P°'           ºY88Pº'                                        
            888                                  888 888                                
            888                                  888 888               
            888                                  888 888               
        .d88888   8888b.       888  888  .d88b.  888 88888b.    8888b.
       d88  888      88b       888  888 d8P  Y8b 888 888  88b      88b
       888  888 .d888888       Y88  88P 88888888 888 888  888 .d888888
       Y88b 888 888  888        Y8bd8P  Y8b.     888 888  888 888  888
         Y88888  Y888888:        Y88P    Y88888  888 888  888  Y888888:");

            Console.WriteLine("\n\n\n\t\t      Aperte [ENTER] para [INICIAR O JOGO]");
            Console.ReadKey();

            MenuPrincipal();
        }

        static void MenuPrincipal() // funcao de criacao do menu, a partir dela o jogo é desenvolvido
        {
            string opcao;

            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nDENTRE AS OPÇÕES NO MENU, QUAL DESEJA EXECUTAR?\n");
                Console.WriteLine("\t|°°°° MENU  PRINCIPAL °°°°|");
                Console.WriteLine("\t|   opção 0 : sair        |");
                Console.WriteLine("\t|                         |");
                Console.WriteLine("\t|   opção 1 : novo jogo   |");
                Console.WriteLine("\t|_________________________|");

                Console.Write("\n\tInforme a opcao: ");
                opcao = Console.ReadLine();
                Console.Beep();

                if (opcao != "0" && opcao != "1")
                {
                    Console.WriteLine("'" + opcao + "' é uma opcao INVALIDA! Para voltar ao MENU, pressione QUALQUER TECLA!");
                    Console.ReadKey();
                    Console.Clear();
                }

                else
                {
                    switch (opcao)
                    {
                        case "0":
                            Console.WriteLine("\nVOCÊ ESCOLHEU SAIR.");
                            break;
                        case "1":
                            Console.Clear();
                            NovoJogo();
                            break;
                    }
                }
            } while (opcao != "0");
        }

        static void NovoJogo() // funçao para criar um novo jogo. pedindo as jogadas
        {
            string opcao;
            char[,] jogoDaVelha = new char[3, 3];

            do
            {
                Console.WriteLine();
                Console.WriteLine("\t|°°°°°° NOVO  JOGO °°°°°°|");
                Console.WriteLine("\t|   opção 0 : colirido   |");
                Console.WriteLine("\t|                        |");
                Console.WriteLine("\t|   opção 1 : padrão     |");
                Console.WriteLine("\t|________________________|");

                Console.Write("\nJOGADOR, você deseja jogar colorido?\nInforme a opção: ");
                opcao = Console.ReadLine();

                if (opcao != "0" && opcao != "1")
                {
                    Console.WriteLine("'" + opcao + "' é uma opcao INVALIDA! Para voltar ao MENU, pressione QUALQUER TECLA!");
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
            Console.WriteLine("\t\t          O TABULEIRO ESTÁ VAZIO\n");

            ImprimeTabuleiro(jogoDaVelha);

            Console.Clear();

            Console.WriteLine("O jogo está prestes a iniciar! Prepare-se!\n");
            Console.WriteLine("O primeiro jogador será representado por (X)");
            Console.WriteLine("O segundo jogador será representado por  (O)\n");
            Console.WriteLine("\nAperte [ENTER] para continuar");
            Console.ReadKey();
            Console.Clear();

            Jogada();
        }

        static void Jogada() // funcao que move o jogo ate que ele acabe, por vitoria ou empate
        {
            int contador = 0, linha, coluna, verificaVitoria = 0;
            bool verificadorPosicaoOcupada;
            char x = 'X', o = 'O';
            char[,] jogoDaVelha = new char[3, 3];

            do
            {
                do
                {
                    Console.WriteLine("\tJogador X!");
                    linha = Linha(); // chama funcao que pede linha
                    coluna = Coluna(); // chama funcao que pede coluna

                    if (VerificaPosicaoMatriz(linha, coluna, jogoDaVelha)) // verifica se a posicao esta disponivel para ser ocupada
                    {
                        jogoDaVelha[linha, coluna] = x; // como esta disponivel a posicao recebe X
                        Console.WriteLine();
                        ImprimeTabuleiro(jogoDaVelha);

                        contador++; // conta as jogadas do X (maximo de 5 pois é o primeiro a jogar)

                        verificadorPosicaoOcupada = false; // recebe "false" pois nao estava ocupada

                        verificaVitoria = Vitoria(jogoDaVelha, linha, coluna); // verifica se alguem ganhou (caso vitoria, verificaVitoria = 1)
                    }

                    else
                    {
                        verificadorPosicaoOcupada = true;
                        Console.WriteLine("\tPosição já ocupada!");
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
                        Console.WriteLine("\tJogador O!");
                        linha = Linha(); // chama funcao que pede linha
                        coluna = Coluna(); // chama funcao que pede coluna

                        if (VerificaPosicaoMatriz(linha, coluna, jogoDaVelha)) // ver se a linha e a coluna que o O digitou esta disponivel
                        {
                            jogoDaVelha[linha, coluna] = o; // se esta disponivel, recebe O
                            Console.WriteLine();
                            ImprimeTabuleiro(jogoDaVelha);

                            contador++; // conta as jogadas do O (maximo de 4 pois é o segundo a jogar)

                            verificadorPosicaoOcupada = false; // como a posicao estava livre, deve sair do laço e recebe falso para poder sair do laço

                            verificaVitoria = Vitoria(jogoDaVelha, linha, coluna); // verifica se alguem ganhou (caso vitoria, verificaVitoria = 1)
                        }
                        else
                        {
                            verificadorPosicaoOcupada = true; // se a posicao estiver ocupada, "true" nao pode sair do laço, executa novamente
                            Console.WriteLine("\tPosição já ocupada!");
                        }
                    } while (verificadorPosicaoOcupada); // enquanto a posicao esta ocupada == true, pede posicao novamente ate que nao esteja ocupada (verificaPosicaoOcupada == false)
                }

            } while (contador < 9 && verificaVitoria == 0); // enquanto nao atingiu o maximo de jogadas ainda (velha) ou ninguem ganhou o jogo ainda (vitoria). Passa a vez para o proximo jogador ate que de velha ou alguem ganhe
        }

        static bool VerificaPosicaoMatriz(int linha, int coluna, char[,] jogoDaVelha) // retorna a disponibilidade de ocupacão da matriz
        {
            if (jogoDaVelha[linha, coluna] == 0)
                return true;
            else
                return false;
        }

        static void ImprimeTabuleiro(char[,] jogoDaVelha) // imprime o tabuleiro
        {
            Console.WriteLine();

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

            Console.WriteLine("\n\n\n\t\t      Aperte [ENTER] para continuar\n");
            Console.Beep();
            Console.ReadKey();
        }

        static void DarVelha() // verifica empate, a velha so acontece depois que o X joga pois é ele quem começa e termina o jogo, 5 jogadas
        {
            Console.Clear();

            Console.WriteLine(@"
8888888888 888b     d888 8888888b.     d8888 88888888888 8888888888    888 
888        8888b   d8888 888   Y88b   d88888     888     888           888 
888        88888b.d88888 888    888  d88P888     888     888           888 
8888888    888Y88888P888 888   d88P d88P 888     888     8888888       888 
888        888 Y888P 888 8888888Pº d88P  888     888     888           888 
888        888  Y8Y  888 888      d88P   888     888     888           Y8Y 
888        888       888 888     d8888888888     888     888              
8888888888 888       888 888    d88P     888     888     8888888888    888 
                                                                           
                                                                           
                                                                           ");

            Console.WriteLine("\n\n\nAperte [ENTER] para voltar ao [MENU PRINCIPAL]");
            Console.ReadKey();
        }

        static int Vitoria(char[,] jogoDaVelha, int linha, int coluna) // funçao que verifica as vitorias possiveis e retorna caso de vitoria
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

        static void GanhadorX() // imprime o ganhador X
        {
            Console.Clear();

            Console.WriteLine(@"
       888     888 8888888 88888888888 .d88888b.  8888888b.  8888888        d8888                    
       888     888   888       888    d88P° °Y88b 888   Y88b   888         d88888                    
       888     888   888       888    888     888 888    888   888        d88P888                    
       Y88b   d88P   888       888    888     888 888   d88P   888       d88P 888                    
        Y88b d88P    888       888    888     888 8888888P°    888      d88P  888                    
         Y88o88P     888       888    888     888 888 T88b     888     d88P   888                    
          Y888P      888       888    Y88b. .d88P 888  T88b    888    d8888888888                    
           Y8P     8888888     888     'Y88888P'  888   T88b 8888888 d88P     888                    
                                                                                                     
                                                                                                     
                                                                                                     
  888888  .d88888b.   .d8888b.         d8888 8888888b.   .d88888b.  8888888b.     Y88b   d88P    888 
    '88b d88P° °Y88b d88P  Y88b       d88888 888  'Y88b d88P° °Y88b 888   Y88b     Y88b d88P     888 
     888 888     888 888    888      d88P888 888    888 888     888 888    888      Y88o88P      888 
     888 888     888 888            d88P 888 888    888 888     888 888   d88P       Y888P       888 
     888 888     888 888  88888    d88P  888 888    888 888     888 8888888P°        d888b       888 
     888 888     888 888    888   d88P   888 888    888 888     888 888 T88b        d88888b      Y8Y 
     88P Y88b. .d88P Y88b  d88P  d8888888888 888  .d88P Y88b. .d88P 888  T88b      d88P Y88b        
     888  'Y88888P'   'Y8888P88 d88P     888 8888888P'   'Y88888P'  888   T88b    d88P   Y88b    888 
   .d88P                                                                                             
 .d88P'                                                                                              
888P'                                                                                                ");

            Console.WriteLine("\n\n\n\tAperte [ENTER] para voltar ao [MENU PRINCIPAL]");
            Console.ReadKey();
        }

        static void GanhadorO() // imprime o ganhador O
        {
            Console.Clear();

            Console.WriteLine(@"
       888     888 8888888 88888888888 .d88888b.  8888888b.  8888888        d8888                    
       888     888   888       888    d88P° °Y88b 888   Y88b   888         d88888                    
       888     888   888       888    888     888 888    888   888        d88P888                    
       Y88b   d88P   888       888    888     888 888   d88P   888       d88P 888                    
        Y88b d88P    888       888    888     888 8888888P°    888      d88P  888                    
         Y88o88P     888       888    888     888 888 T88b     888     d88P   888                    
          Y888P      888       888    Y88b. .d88P 888  T88b    888    d8888888888                    
           Y8P     8888888     888     'Y88888P'  888   T88b 8888888 d88P     888                    
                                                                                                     
                                                                                                     
                                                                                                     
  888888  .d88888b.   .d8888b.         d8888 8888888b.   .d88888b.  8888888b.      .d88888b.     888 
    '88b d88P° °Y88b d88P  Y88b       d88888 888  'Y88b d88P° °Y88b 888   Y88b    d88P° °Y88b    888 
     888 888     888 888    888      d88P888 888    888 888     888 888    888    888     888    888 
     888 888     888 888            d88P 888 888    888 888     888 888   d88P    888     888    888 
     888 888     888 888  88888    d88P  888 888    888 888     888 8888888P°     888     888    888 
     888 888     888 888    888   d88P   888 888    888 888     888 888 T88b      888     888    Y8Y 
     88P Y88b. .d88P Y88b  d88P  d8888888888 888  .d88P Y88b. .d88P 888  T88b     Y88b. .d88P       
     888  'Y88888P'   'Y8888P88 d88P     888 8888888P'   'Y88888P'  888   T88b     'Y88888P'     888 
   .d88P                                                                                             
 .d88P'                                                                                              
888P'                                                                                                ");

            Console.WriteLine("\n\n\n\tAperte [ENTER] para voltar ao [MENU PRINCIPAL]");
            Console.ReadKey();
        }

        static int Linha() // funçao para pedir a linha para o usuario
        {
            int linha;

            do
            {
                Console.Write("\tInforme a linha de sua escolha:  ");
                linha = int.Parse(Console.ReadLine());

                if (linha != 0 && linha != 1 && linha != 2)
                    Console.WriteLine("\t'" + linha + "' é uma linha INVALIDA!");

            } while (linha != 0 && linha != 1 && linha != 2);

            return linha;
        }

        static int Coluna() // funçao para pedir a coluna para o usuario
        {
            int coluna;

            do
            {
                Console.Write("\tInforme a coluna de sua escolha: ");
                coluna = int.Parse(Console.ReadLine());

                if (coluna != 0 && coluna != 1 && coluna != 2)
                    Console.WriteLine("\t'" + coluna + "' é uma coluna INVALIDA!");

            } while (coluna != 0 && coluna != 1 && coluna != 2);

            return coluna;
        }
    }
}