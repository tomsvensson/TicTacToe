using System;

char[] board = new char[9];
int player1Score = 0;
int player2Score = 0;

static void DrawBoardExample()
{
    Console.WriteLine(" ____ ____ ____ ");
    Console.WriteLine("| 1  | 2  | 3  |");
    Console.WriteLine("|____|____|____|");
    Console.WriteLine("| 4  | 5  | 6  |");
    Console.WriteLine("|____|____|____|");
    Console.WriteLine("| 7  | 8  | 9  |");
    Console.WriteLine("|____|____|____|\n");
}

static void TicTacToeArt()
{
    Console.WriteLine(" _____  _         _____              _____            ");
    Console.WriteLine("|_   _|(_)       |_   _|            |_   _|           ");
    Console.WriteLine("  | |   _   ___    | |  __ _   ___    | |  ___    ___ ");
    Console.WriteLine("  | |  | | / __|   | | / _` | / __|   | | / _ |  / _ |");
    Console.WriteLine("  | |  | || (__    | || (_| || (__    | || (_)| |  __/");
    Console.WriteLine("  |_/  |_| |___|   |_/ |__,_| |___|   |_/ |___/  |___|");
    Console.WriteLine("                                                      ");
}

static void DrawBoard(char[] board)
{
    Console.WriteLine(" ____ ____ ____ ");
    Console.WriteLine($"| {(board[0] == '\0' ? ' ' : board[0])}  | {(board[1] == '\0' ? ' ' : board[1])}  | {(board[2] == '\0' ? ' ' : board[2])}  |");
    Console.WriteLine("|____|____|____|");
    Console.WriteLine($"| {(board[3] == '\0' ? ' ' : board[3])}  | {(board[4] == '\0' ? ' ' : board[4])}  | {(board[5] == '\0' ? ' ' : board[5])}  |");
    Console.WriteLine("|____|____|____|");
    Console.WriteLine($"| {(board[6] == '\0' ? ' ' : board[6])}  | {(board[7] == '\0' ? ' ' : board[7])}  | {(board[8] == '\0' ? ' ' : board[8])}  |");
    Console.WriteLine("|____|____|____|\n");
}

Random rnd = new Random(Guid.NewGuid().GetHashCode());
int dice = 0;
int move;
bool gameWon = false;
char currentMarker = 'X';
bool isPlayer1Turn = true;
Console.WriteLine("Welcome to\n");
TicTacToeArt();
Console.WriteLine("Player 1, please enter your name:\n");
string player1 = Console.ReadLine();
Console.WriteLine("\nPlayer 2, please enter your name:\n");
string player2 = Console.ReadLine();

void InitializeGame()
{
    Array.Clear(board, 0, board.Length);
    gameWon = false;
    currentMarker = 'X';
    isPlayer1Turn = rnd.Next(1, 3) == 1;
    if (isPlayer1Turn)
    {
        Console.WriteLine($" \n{player1} was randomly chosen to start first!");
    }
    else
    {
        Console.WriteLine($"\n{player2} was randomly chosen to start first!");
    }
    DrawBoardExample();
    Console.WriteLine("Please enter the moves on the board using number 1-9.\nPress any key to start the game...");
    Console.ReadKey();
}

void DisplayScores()
{
    Console.WriteLine("\nCurrent Scores:");
    Console.WriteLine($"{player1}: {player1Score}");
    Console.WriteLine($"{player2}: {player2Score}");
}

bool AskToPlayAgain()
{
    Console.WriteLine("\nDo you want to play again? (y/n)");
    string response = Console.ReadLine().ToLower();
    return response == "y";
}

do
{
    InitializeGame();

    do
    {
        Console.Clear();
        DrawBoard(board);
        Console.WriteLine($"{(isPlayer1Turn ? player1 : player2)}, please enter which square you'd like to place your ({currentMarker}) in.");
        if (int.TryParse(Console.ReadLine(), out move) && move >= 1 && move <= 9)
        {
            if (board[move - 1] != 'X' && board[move - 1] != 'O')
            {
                board[move - 1] = currentMarker;
                gameWon = CheckWin(board);
                if (gameWon)
                {
                    if (isPlayer1Turn)
                    {
                        player1Score++;
                    }
                    else
                    {
                        player2Score++;
                    }
                }
                currentMarker = (currentMarker == 'X') ? 'O' : 'X';
                isPlayer1Turn = !isPlayer1Turn;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("That square is already taken! Try again.");
                Console.ReadKey();
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Invalid input! Please enter a number between 1 and 9.");
            Console.ReadKey();
        }
    } while (!gameWon && !IsBoardFull(board));

    Console.Clear();
    DrawBoard(board);

    if (gameWon)
    {
        Console.WriteLine($"{(isPlayer1Turn ? player2 : player1)} wins!");
    }
    else
    {
        Console.WriteLine("It's a draw!");
    }

    DisplayScores();
} while (AskToPlayAgain());

static bool CheckWin(char[] board)
{
    return (board[0] == board[1] && board[1] == board[2] && board[0] != '\0') ||
           (board[3] == board[4] && board[4] == board[5] && board[3] != '\0') ||
           (board[6] == board[7] && board[7] == board[8] && board[6] != '\0') ||
           (board[0] == board[3] && board[3] == board[6] && board[0] != '\0') ||
           (board[1] == board[4] && board[4] == board[7] && board[1] != '\0') ||
           (board[2] == board[5] && board[5] == board[8] && board[2] != '\0') ||
           (board[0] == board[4] && board[4] == board[8] && board[0] != '\0') ||
           (board[2] == board[4] && board[4] == board[6] && board[2] != '\0');
}

static bool IsBoardFull(char[] board)
{
    foreach (char c in board)
    {
        if (c != 'X' && c != 'O')
        {
            return false;
        }
    }
    return true;
}