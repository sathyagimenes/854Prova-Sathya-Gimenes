using System.Text.RegularExpressions;


int quantidadeJogadores = 0;
string player1 = "";
string player2 = "";
while (quantidadeJogadores != 1 && quantidadeJogadores != 2)
{
    Console.WriteLine("Qual a quantidade de jogadores? (1/2)");
    int.TryParse(Console.ReadLine(), out quantidadeJogadores);
    Console.Clear();
    if (quantidadeJogadores != 1 && quantidadeJogadores != 2)
        Console.WriteLine("Número inválido. Escolha entre 1 ou 2 jogadores.");
}
if (quantidadeJogadores == 2)
{
    Console.WriteLine("Qual o nome do(a) primeiro(a) jogador(a)?");
    player1 = Console.ReadLine();
    Console.Clear();
    Console.WriteLine("Qual o nome do(a) segundo(a) jogador(a)?");
    player2 = Console.ReadLine();
    Console.Clear();
}
else if (quantidadeJogadores == 1)
{
    Console.WriteLine("Qual o nome do(a) jogador(a)?");
    player1 = Console.ReadLine();
    player2 = "Computador";
    Console.Clear();
}
string embarcacao;
string jogadorDaVez;
List<string> posicoesJogador1 = new();
List<string> posicoesJogador2 = new();
List<string> validacaoPosicoes = new();
string posicaoNoMapa = "\0";
for (int n = 0; n < quantidadeJogadores; n++)
{
    bool PS = false;
    bool NT = false;
    bool DS = false;
    bool SB = false;
    if (n == 0)
        jogadorDaVez = player1;
    else
        jogadorDaVez = player2;

    while (!PS || !NT || !DS || !SB)
    {
        embarcacao = "\0";
        Console.Clear();
        Console.WriteLine($"--- Posicionamento das embarcações do(a) {jogadorDaVez} ---");
        while (embarcacao != "PS" && embarcacao != "NT" && embarcacao != "DS" && embarcacao != "SB")
        {
            Console.WriteLine($"{jogadorDaVez}, qual o tipo de embarcação a ser posicionada? (PS/NT/DS/SB)");
            embarcacao = Console.ReadLine();
            Console.Clear();
            if ((embarcacao == "PS" && PS) || (embarcacao == "NT" && NT) || (embarcacao == "DS" && DS) || (embarcacao == "SB" && SB))
            {
                Console.WriteLine($"As embarcações do tipo {embarcacao} já foram posicionadas. " +
                    "Escolha outro tipo de embarcação entre as opções abaixo.");
                Console.WriteLine("PS - Porta-Aviões\nNT - Navio-Tanque\nDS - Destroyers\nSB - Submarinos\n");
                embarcacao = "\0";
            }
            else if (embarcacao != "PS" && embarcacao != "NT" && embarcacao != "DS" && embarcacao != "SB")
            {
                Console.WriteLine("Tipo inválido. Escolha o tipo de embarcação entre as opções abaixo.");
                Console.WriteLine("PS - Porta-Aviões\nNT - Navio-Tanque\nDS - Destroyers\nSB - Submarinos\n");
            }
        }
        int tamanhoEmbarcacao = 0;
        int quantidadeEbarcacao = 0;
        posicaoNoMapa = "\0";
        if (embarcacao == "PS" && !PS)
        {
            tamanhoEmbarcacao = 5;
            quantidadeEbarcacao = 1;
            PS = true;
        }
        else if (embarcacao == "NT" && !NT)
        {
            tamanhoEmbarcacao = 4;
            quantidadeEbarcacao = 2;
            NT = true;
        }
        else if (embarcacao == "DS" && !DS)
        {
            tamanhoEmbarcacao = 3;
            quantidadeEbarcacao = 3;
            DS = true;
        }
        else if (embarcacao == "SB" && !SB)
        {
            tamanhoEmbarcacao = 2;
            quantidadeEbarcacao = 4;
            SB = true;
        }
        while (posicaoNoMapa == "\0" || quantidadeEbarcacao > 0)
        {
            Console.WriteLine($"--- A Embarcação {embarcacao} ocupa {tamanhoEmbarcacao} quadrantes na posição vertical ou horizontal " +
                $"(ex. H1H{tamanhoEmbarcacao}) ---");
            if (quantidadeEbarcacao > 1)
                Console.WriteLine($"Falta posicionar {quantidadeEbarcacao} embarcações do tipo {embarcacao}. Escolha a posição");
            else
                Console.WriteLine($"Falta posicionar {quantidadeEbarcacao} embarcação do tipo {embarcacao}. Escolha a posição");
            posicaoNoMapa = Console.ReadLine();
            char[] divisaoLinhaColuna = posicaoNoMapa.ToArray();
            var validarPadrao = @"\b([A-J])([1-9]|(10))([A-J])([1-9]|(10))\b";
            Regex regex = new Regex(validarPadrao);
            var validarPosicaoVertical10 = @"\b([A-J])(10)([A-J])(10)\b";
            Regex regex2 = new Regex(validarPosicaoVertical10);
            var validarPosicaoFinal10 = @"\b([A-J])([1-9])([A-J])(10)\b";
            Regex regex3 = new Regex(validarPosicaoFinal10);
            if (!regex.Match(posicaoNoMapa).Success)
            {
                Console.Clear();
                Console.WriteLine($"*** {posicaoNoMapa} É UMA POSIÇÃO INVÁLIDA PARA A EMBARCAÇÃO {embarcacao} ***");
                posicaoNoMapa = "\0";
            }
            else if (regex2.Match(posicaoNoMapa).Success || regex3.Match(posicaoNoMapa).Success)
            {
                if (regex2.Match(posicaoNoMapa).Success && divisaoLinhaColuna[3] != (divisaoLinhaColuna[0] + (tamanhoEmbarcacao - 1)) && !regex3.Match(posicaoNoMapa).Success)
                {
                    Console.Clear();
                    Console.WriteLine($"*** {posicaoNoMapa} É UMA POSIÇÃO INVÁLIDA PARA A EMBARCAÇÃO {embarcacao} ***");
                    posicaoNoMapa = "\0";
                }
                else if (regex3.Match(posicaoNoMapa).Success && divisaoLinhaColuna[2] != divisaoLinhaColuna[0] && !regex2.Match(posicaoNoMapa).Success)
                {
                    Console.Clear();
                    Console.WriteLine($"*** {posicaoNoMapa} É UMA POSIÇÃO INVÁLIDA PARA A EMBARCAÇÃO {embarcacao} ***");
                    posicaoNoMapa = "\0";
                }
            }
            else if (divisaoLinhaColuna[3] != (divisaoLinhaColuna[1] + (tamanhoEmbarcacao - 1)) && divisaoLinhaColuna[2] != (divisaoLinhaColuna[0] + (tamanhoEmbarcacao - 1)))
            {
                Console.Clear();
                Console.WriteLine($"*** {posicaoNoMapa} É UMA POSIÇÃO INVÁLIDA PARA A EMBARCAÇÃO {embarcacao} ***");
                posicaoNoMapa = "\0";
            }
            else if (divisaoLinhaColuna[3] == (divisaoLinhaColuna[1] + (tamanhoEmbarcacao - 1)) && divisaoLinhaColuna[2] == (divisaoLinhaColuna[0] + (tamanhoEmbarcacao - 1)))
            {
                Console.Clear();
                Console.WriteLine($"*** {posicaoNoMapa} É UMA POSIÇÃO INVÁLIDA PARA A EMBARCAÇÃO {embarcacao} ***\n--- Não é possível posicionar a embarcação na diagonal ---");
                posicaoNoMapa = "\0";
            }
            if (posicaoNoMapa != "\0")
            {
                validacaoPosicoes.Clear();
                for (int i = 0; i < tamanhoEmbarcacao; i++)
                {
                    string posicao = "";
                    if (divisaoLinhaColuna[2] == '0')
                    {
                        int ascii = ((int)divisaoLinhaColuna[0] + i);
                        char voltaLetra = (char)ascii;
                        posicao = voltaLetra.ToString() + divisaoLinhaColuna[1].ToString() + divisaoLinhaColuna[2].ToString();
                    }
                    else if (divisaoLinhaColuna[0] == divisaoLinhaColuna[2])
                    {
                        int tranformaEmInt = divisaoLinhaColuna[1] - '0';
                        posicao = divisaoLinhaColuna[0].ToString() + (tranformaEmInt + i).ToString();
                    }
                    else
                    {
                        int ascii = ((int)divisaoLinhaColuna[0] + i);
                        char voltaLetra = (char)ascii;
                        posicao = voltaLetra.ToString() + divisaoLinhaColuna[1].ToString();
                    }
                    if (jogadorDaVez == player1)
                    {
                        if (posicoesJogador1.Contains(posicao))
                        {
                            Console.Clear();
                            Console.WriteLine($"*** A posição {posicao} já está sendo ocupada por outra embarcação ***");
                            validacaoPosicoes.Clear();
                            posicaoNoMapa = "\0";
                            break;
                        }
                        else
                            validacaoPosicoes.Add(posicao);
                    }
                    else if (jogadorDaVez == player2)
                    {
                        if (posicoesJogador2.Contains(posicao))
                        {
                            Console.Clear();
                            Console.WriteLine($"*** A posição {posicao} já está sendo ocupada por outra embarcação ***");
                            validacaoPosicoes.Clear();
                            posicaoNoMapa = "\0";
                            break;
                        }
                        else
                            validacaoPosicoes.Add(posicao);
                    }
                }
                if (validacaoPosicoes.Count > 0)
                {
                    quantidadeEbarcacao--;
                    Console.Clear();
                }
                if (jogadorDaVez == player1)
                    posicoesJogador1.AddRange(validacaoPosicoes);
                else if (jogadorDaVez == player2)
                    posicoesJogador2.AddRange(validacaoPosicoes);
            }
        }
    }
}
if (quantidadeJogadores == 1)
{
    string posicao;
    for (int a = 0; a < 9; a++)
    {
        posicao = "B" + (2 + a).ToString();
        posicoesJogador2.Add(posicao);
    }
    for (int b = 0; b < 8; b++)
    {
        posicao = "D" + (2 + b).ToString();
        posicoesJogador2.Add(posicao);
    }
    for (int c = 0; c < 8; c++)
    {
        posicao = "J" + (1 + c).ToString();
        posicoesJogador2.Add(posicao);
    }
    for (int d = 0; d < 5; d++)
    {
        posicao = "F" + (5 + d).ToString();
        posicoesJogador2.Add(posicao);
    }
}

List<string> listaTentativasJogador1 = new();
List<string> listaTentativasJogador2 = new();
List<string> listaTentativasPassadas = new();
string[,] mapaDaVez = new string[11, 11];
string[,] mapaAlvoDoJogador1 = new string[11, 11];
string[,] mapaAlvoDoJogador2 = new string[11, 11];
string tentativaJogador;
jogadorDaVez = player1;
int ascii2;
int ascii3 = 'A';
char voltaChar = 'A';
int num = 1;
for (int n = 0; n < 2; n++)
{
    if (n == 0)
        mapaDaVez = mapaAlvoDoJogador1;
    else if (n == 1)
        mapaDaVez = mapaAlvoDoJogador2;
    for (int i = 0; i < 11; i++)
    {
        for (int j = 0; j < 11; j++)
        {
            if (i == 0 && j > 0)
                mapaDaVez[i, j] = " " + j + " ";
            else if (j == 0 && i > 0)
            {
                ascii2 = ((int)'A' + (i - 1));
                char voltaLetra = (char)ascii2;
                mapaDaVez[i, j] = " " + voltaLetra + " ";
            }
            else
                mapaDaVez[i, j] = " - ";
        }
    }
}
Console.Clear();
Console.WriteLine("--- Vamos começar o Jogo! ---\n");

while (posicoesJogador1.Count != 0 && posicoesJogador2.Count != 0)
{
    if (jogadorDaVez == player1)
    {
        mapaDaVez = mapaAlvoDoJogador1;
        validacaoPosicoes = posicoesJogador2;
        listaTentativasPassadas = listaTentativasJogador1;
    }
    else if (jogadorDaVez == player2)
    {
        mapaDaVez = mapaAlvoDoJogador2;
        validacaoPosicoes = posicoesJogador1;
        listaTentativasPassadas = listaTentativasJogador2;
    }
    Console.WriteLine($"--- {jogadorDaVez}, é a sua vez ---");
    tentativaJogador = "\0";
    while (tentativaJogador == "\0")
    {
        for (int i = 0; i < 11; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                Console.Write(mapaDaVez[i, j]);
            }
            Console.Write("\n");
        }
        if (quantidadeJogadores == 1 && jogadorDaVez == player2)
        {
            if (num == 11)
            {
                ascii3 = (int)ascii3 + 1;
                voltaChar = (char)ascii3;
                num = 1;
            }
            tentativaJogador = voltaChar + num.ToString();
            num++;
            Console.WriteLine($"O {player2} escolheu bombardear a posição {tentativaJogador}");
            Console.Write("Pressione uma tecla para continuar...");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine($"\n> {jogadorDaVez}, escolha uma posição no mapa para bombardear (exemplo: A1)");
            tentativaJogador = Console.ReadLine();
            var validarInput = @"\b([A-J])([1-9]|(10))\b";
            Regex regex = new Regex(validarInput);
            if (!regex.Match(tentativaJogador).Success)
            {
                Console.Clear();
                Console.WriteLine($"*** {tentativaJogador} É UMA POSIÇÃO INVÁLIDA. ESCOLHA UMA POSIÇÃO DENTRO DO MAPA (A-J)(1-10) ***");
                tentativaJogador = "\0";
            }
            else if (listaTentativasPassadas.Contains(tentativaJogador))
            {
                Console.Clear();
                Console.WriteLine($"*** A POSIÇÃO {tentativaJogador} JÁ FOI BOMBARDEADA. ESCOLHA OUTRA POSIÇÃO DENTRO DO MAPA (A-J)(1-10) ***");
                tentativaJogador = "\0";
            }
        }
    }
    int linha = 0;
    int coluna;
    char[] divisaoLinhaColuna = tentativaJogador.ToArray();
    if (divisaoLinhaColuna.Count() == 3)
        coluna = 10;
    else
        coluna = divisaoLinhaColuna[1] - '0';
    switch (divisaoLinhaColuna[0])
    {
        case 'A':
            linha = 1;
            break;
        case 'B':
            linha = 2;
            break;
        case 'C':
            linha = 3;
            break;
        case 'D':
            linha = 4;
            break;
        case 'E':
            linha = 5;
            break;
        case 'F':
            linha = 6;
            break;
        case 'G':
            linha = 7;
            break;
        case 'H':
            linha = 8;
            break;
        case 'I':
            linha = 9;
            break;
        case 'J':
            linha = 10;
            break;
    }
    if (validacaoPosicoes.Contains(tentativaJogador))
    {
        mapaDaVez[linha, coluna] = " X ";
        Console.Clear();
        Console.WriteLine($"{jogadorDaVez} ACERTOU o alvo");
        validacaoPosicoes.Remove(tentativaJogador);
    }
    else
    {
        mapaDaVez[linha, coluna] = " A ";
        Console.Clear();
        Console.WriteLine($"{jogadorDaVez} ERROU o alvo");
    }
    if (jogadorDaVez == player1)
    {
        listaTentativasJogador1.Add(tentativaJogador);
        mapaAlvoDoJogador1 = mapaDaVez;
        posicoesJogador2 = validacaoPosicoes;
        jogadorDaVez = player2;
    }
    else if (jogadorDaVez == player2)
    {
        listaTentativasJogador2.Add(tentativaJogador);
        mapaAlvoDoJogador2 = mapaDaVez;
        posicoesJogador1 = validacaoPosicoes;
        jogadorDaVez = player1;
    }
    Console.WriteLine("\n--- Pontuação  ---\n");
    Console.WriteLine($"{player1}, falta(m) {posicoesJogador2.Count} alvo(s) para você vencer o jogo");
    Console.WriteLine($"{player2}, falta(m) {posicoesJogador1.Count} alvo(s) para você vencer o jogo");
    Console.Write("Pressione uma tecla para continuar...");
    Console.ReadKey();
    Console.Clear();
}
string vencedor = "";
if (posicoesJogador1.Count == 0)
    vencedor = player2;
else if (posicoesJogador2.Count == 0)
    vencedor = player1;
Console.WriteLine("--- GAME OVER ---");
Console.WriteLine($"O vencedor foi o(a) {vencedor}");

