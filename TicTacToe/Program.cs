using System;

namespace TicTacToe
{
    class Program
    {

        // Main method
        static void Main(string[] args)
        {
            string[,] Board = new string[3, 3]; // Initial Board
            // Initial Params
            int row = 3; 
            int col = 3;
            string player1 = ".";
            string player2 = ".";

            // Call startGame method
            startGame(Board, row, col, ref player1, ref player2);
            // Call createBoard method
            createBoard(Board, row, col);

            // Checks to see if board isn't finish or if a player won/loss
            while (!gameOver(Board, ref player1) && !gameOver(Board, ref player2))
            {
                playGame(Board, row, col, ref player1, ref player2);
                createBoard(Board, row, col);
            }
        }

        // startGame method
        public static void startGame(string[,] board, int row, int col, ref string player1, ref string player2)
        {
            // Build board w/ 'empty' (.) spaces
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    board[i, j] = ".";
                }
            }

            // Asks player for their choice in being x or o
            Console.WriteLine("Choose either 'x' or 'o':");
            player1 = Console.ReadLine().ToUpper(); // Puts response into a string then makes it uppercase

            // Checks to see which choice player one made to prevent player 2 being the same choice
            if (player1 == "X")
            {
                player2 = "o".ToUpper(); // Adds to string then makes it uppercase
            }
            else
            {
                player2 = "x".ToUpper(); // Adds to string then makes it uppercase
            }
        }

        // createBoard method
        public static void createBoard(string[,] board, int row, int col)
        {
            // Prints the board
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Console.Write(board[i, j]);
                }
                Console.WriteLine();
            } 
        }

        // playGame method
        public static void playGame(string[,] board, int row, int col, ref string player1, ref string player2)
        {
            setPos(board, player1); // Calls setPos method to allow the player to choose their move
            setAIPos(board, player2); // Calls setAIPos method to make a move for the ai after the player's move
        }

        // setPos method (User based)
        public static void setPos(string[,] board, string player)
        {
            // Asks user for choice in the row
            Console.WriteLine($"Player {player} choose a row (1 - 3): ");
            int playerRow = Convert.ToInt32(Console.ReadLine()) - 1; // Adds response to an int to be used by the board
            
            // Asks user for choice in the column
            Console.WriteLine($"Player {player} choose a column (1 - 3): ");
            int playerCol = Convert.ToInt32(Console.ReadLine()) - 1; // Adds response to an int to be used by the board

            // Adds player's choices to board
            board[playerRow, playerCol] = player;
        }

        // setAIPos method (automation for AI)
        public static void setAIPos(string[,] board, string ai)
        {
            // Creates random int to select a posistion on the board
            Random rng = new Random();
            int aiRow = rng.Next(0, 2);
            int aiCol = rng.Next(0, 2);

            // Adds AI's random choices to board
            board[aiRow, aiCol] = ai;
        }

        // gameOver method
        public static bool gameOver(string[,] board, ref string player)
        {
            // Strings that deternime where the placement of the slots on the board are
            string topRow = board[0, 0] + board[0, 1] + board[0, 2]; // Top row
            string midRow = board[1, 0] + board[1, 1] + board[1, 2]; // Middle row
            string botRow = board[2, 0] + board[2, 1] + board[2, 2]; // Bottom(last) row
            string firCol = board[0, 0] + board[0, 1] + board[0, 2]; // First Column
            string secCol = board[1, 0] + board[1, 1] + board[1, 2]; // Second Column
            string lastCol = board[2, 0] + board[2, 1] + board[2, 2]; // Third(last) column
            string diag = board[0, 0] + board[1, 1] + board[2, 2]; // Diagonal
            string revDiag = board[0, 2] + board[1, 1] + board[2, 0]; // Reversed Diagonal

            // String that is used to determine if slots are the same value
            string tripPlayer = player + player + player;

            // If statement to check if the slots have the same value
            if (topRow.Equals(tripPlayer) 
                || midRow.Equals(tripPlayer) 
                || botRow.Equals(tripPlayer) 
                ||firCol.Equals(tripPlayer) 
                || secCol.Equals(tripPlayer) 
                || lastCol.Equals(tripPlayer) 
                || diag.Equals(tripPlayer) 
                || revDiag.Equals(tripPlayer))
            {
                return true; // Returns true if slots are equal
            }
            else
            {
                return false; // Returns false if slots aren't equal
            }
        }

        
    }
}