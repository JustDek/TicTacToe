using System;
using System.Threading;

namespace TicTacToe
{
    public class Program
    {
        private bool _playerDone;
        public Program()
        {
            _playerDone = false;
        }

        public bool PlayerDone
        {
            get
            {
                return _playerDone;
            }
            set
            {
                _playerDone = value;
            }
        }

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

            if (gameOver(Board, ref player1) == true || gameOver(Board, ref player2) == true)
            {
                Console.WriteLine($"Player {player1} wins!!!");
            }
            else
            {
                Console.WriteLine($"Player {player2} wins!!!");
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
                    board[i, j] = "."; // Adds a placeholder for slot until it's filled
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
            setPos(board, player1, player2); // Calls setPos method to allow the player to choose their move and makes a move for the ai after the player's move
        }

        // setPos method (User based)
        public static void setPos(string[,] board, string player, string ai)
        {

            Program finished = new Program();
            finished.PlayerDone = false;

            // Ints to hold player's row and column position
            int playerRow = 0;
            int playerCol = 0;

            string tempR = "";
            string tempC = "";

            bool tpRow = false;
            bool tpCol = false;

            char r;
            char c;


            while (!finished.PlayerDone)
            {

                // Asks user for choice in the row
                Console.WriteLine($"Player {player} choose a row (1 - 3): ");
                tempR = Console.ReadLine().ToUpper();

                r = Convert.ToChar(tempR);

                tpRow = Char.IsNumber(r);

                if (tpRow == true)
                {
                    playerRow = Convert.ToInt32(tempR) - 1; // Adds response to an int to be used by the board
                }
                else
                {
                    Console.WriteLine("Please enter a number between 1 and 3: ");
                }



                // Asks user for choice in the column
                Console.WriteLine($"Player {player} choose a column (1 - 3): ");
                tempC = Console.ReadLine();

                c = Convert.ToChar(tempC);

                tpCol = Char.IsNumber(c);

                if (tpCol == true)
                {
                    playerCol = Convert.ToInt32(tempC) - 1; // Adds response to an int to be used by the board
                }
                else
                {
                    Console.WriteLine("Please enter a number between 1 and 3: ");
                }


                if (board[playerRow, playerCol] == ".")
                {
                    board[playerRow, playerCol] = player; // Adds player's choices to board
                    finished.PlayerDone = true;
                }
                else
                {
                    Console.WriteLine("Please choose another position: ");
                }
            }


            // ---------- vvv Ai Position code starts here vvv ---------- \\


            // Cosmetic attribute in place to make sure AI's turn is after player's
            Console.Write("Ai is thinking");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
            Console.WriteLine();

            // Ints used to deternmine ai position
            int aiRNum = 0;
            int aiCNum = 0;

            // Lists for the board's rows and columns
            List<int> rowList = new List<int>();
            List<int> colList = new List<int>();

            // Adds values to row list | Values represent board position (Left -> Right)
            rowList.Add(0);
            rowList.Add(1);
            rowList.Add(2);

            // Adds values to column list | Values represent board position (Left -> Right)
            colList.Add(0);
            colList.Add(1);
            colList.Add(2);

            // Creates random int to select a posistion on the board
            Random rng = new Random();
            int aiRow = rng.Next(0, rowList.Count - 1); // Int to store random number for row selection
            int aiCol = rng.Next(0, colList.Count - 1); // Int to store random number for column selection

            // Adds random item to Ints from their correlating list
            aiRNum = rowList[aiRow];
            aiCNum = colList[aiCol];


            // --- vv AI position check starts here vv --- \\


            // If statement that deternmines if AI's row position is unique
            if (aiRNum == playerRow)
            {
                aiRNum = 0;
                aiRow = rng.Next(0, rowList.Count);

                aiRNum = rowList[aiRow];
            }

            // If statement that deternmines if AI's column position is unique
            if (aiCNum == playerCol)
            {
                aiCNum = 0;
                aiCol = rng.Next(0, colList.Count);

                aiCNum = colList[aiCol];
            }
            
            board[rowWin(board, aiRNum, aiCNum, ai), colWin(board, aiRNum, aiCNum, ai)] = ai; // Adds AI's random choices to board
            
        }

        public static int rowWin(string[,] board, int row, int col, string ai)
        {
            Program finished = new Program();
            finished.PlayerDone = false;

            Random rng = new Random();
            int rand = rng.Next(1, 4);

            int aiRNum = 0;
            int aiCNum = 0;

            bool done = false;

            row = aiRNum;
            col = aiCNum;


            // Row wins --------------------

            while (!done)
            {
                if (rand == 1)
                {
                    // First row win (L - R)
                    if (aiRNum == 0 && aiCNum == 0)
                    {


                        if (board[0, 1] == ".")
                        {
                            board[aiRNum, aiCNum + 1] = ai;
                            return row;
                            done = true;
                            finished.PlayerDone = false;

                        }
                        if (board[0, 2] == ".")
                        {
                            board[aiRNum, aiCNum + 2] = ai;
                            return row;
                            done = true;
                            finished.PlayerDone = false;

                        }


                    }
                    // First row win (R - L)
                    if (aiRNum == 0 && aiCNum == 2)
                    {
                        if (board[0, 0] == ".")
                        {
                            board[aiRNum, aiCNum - 2] = ai;
                            return row;
                            done = true;
                            finished.PlayerDone = false;

                        }
                        if (board[0, 1] == ".")
                        {
                            board[aiRNum, aiCNum - 1] = ai;
                            return row;
                            done = true;
                            finished.PlayerDone = false;

                        }

                    }
                }

                if (rand == 2)
                {
                    // Second row win (L - R)
                    if (aiRNum == 1 && aiCNum == 0)
                    {

                        if (board[1, 1] == ".")
                        {
                            board[aiRNum, aiCNum + 1] = ai;
                            return row;
                            done = true;
                            finished.PlayerDone = false;

                        }
                        if (board[1, 2] == ".")
                        {
                            board[aiRNum, aiCNum + 2] = ai;
                            return row;
                            done = true;
                            finished.PlayerDone = false;

                        }


                    }
                    // Second row win (R - L)
                    if (aiRNum == 1 && aiCNum == 2)
                    {
                        if (board[1, 0] == ".")
                        {
                            board[aiRNum, aiCNum - 2] = ai;
                            return row;
                            done = true;
                            finished.PlayerDone = false;

                        }
                        if (board[1, 1] == ".")
                        {
                            board[aiRNum, aiCNum - 1] = ai;
                            return row;
                            done = true;
                            finished.PlayerDone = false;

                        }

                    }
                }

                if (rand == 3)
                {
                    // Third row win (L - R)
                    if (aiRNum == 2 && aiCNum == 0)
                    {

                        if (board[2, 1] == ".")
                        {
                            board[aiRNum, aiCNum + 1] = ai;
                            return row;
                            done = true;
                            finished.PlayerDone = false;

                        }
                        if (board[2, 2] == ".")
                        {
                            board[aiRNum, aiCNum + 2] = ai;
                            return row;
                            done = true;
                            finished.PlayerDone = false;

                        }


                    }
                    // Third row win (R - L)
                    if (aiRNum == 2 && aiCNum == 2)
                    {
                        if (board[2, 0] == ".")
                        {
                            board[aiRNum, aiCNum - 2] = ai;
                            return row;
                            done = true;
                            finished.PlayerDone = false;

                        }
                        if (board[2, 1] == ".")
                        {
                            board[aiRNum, aiCNum - 1] = ai;
                            return row;
                            done = true;
                            finished.PlayerDone = false;

                        }

                    }
                }

                // Diagonal wins --------------------

                if (rand == 4)
                {
                    // Diagonal win
                    if (aiRNum == 0 && aiCNum == 0)
                    {

                        if (board[1, 1] == ".")
                        {
                            board[aiRNum + 1, aiCNum + 1] = ai;
                            return row;
                            done = true;
                            finished.PlayerDone = false;

                        }
                        else
                        {
                            if (board[2, 2] == ".")
                            {
                                board[aiRNum + 2, aiCNum + 2] = ai;
                                return row;
                                done = true;
                                finished.PlayerDone = false;

                            }
                        }

                    }
                    // Reverse Diagonal win
                    if (aiRNum == 2 && aiCNum == 2)
                    {
                        if (board[0, 0] == ".")
                        {
                            board[aiRNum - 2, aiCNum - 2] = ai;
                            return row;
                            done = true;
                            finished.PlayerDone = false;

                        }
                        else
                        {
                            if (board[1, 1] == ".")
                            {
                                board[aiRNum - 1, aiCNum - 1] = ai;
                                return row;
                                done = true;
                                finished.PlayerDone = false;

                            }
                        }
                    }
                }
            }

            return row;
        }

        public static int colWin(string[,] board, int row, int col, string ai)
        {
            Program finished = new Program();
            finished.PlayerDone = false;

            Random rng = new Random();
            int rand = rng.Next(1, 4);

            int aiRNum = 0;
            int aiCNum = 0;

            bool done = false;

            row = aiRNum;
            col = aiCNum;

            // Column wins --------------------

            while (!done)
            {
                if (rand == 1)
                {
                    // First Column win (U - D)
                    if (aiRNum == 0 && aiCNum == 0)
                    {
                        if (board[1, 0] == ".")
                        {
                            board[aiRNum + 1, aiCNum] = ai;
                            return row;
                            done = true;
                            finished.PlayerDone = false;

                        }
                        if (board[2, 0] == ".")
                        {
                            board[aiRNum + 2, aiCNum] = ai;
                            return row;
                            done = true;
                            finished.PlayerDone = false;

                        }

                    }
                    // First Column win (D - U)
                    if (aiRNum == 2 && aiCNum == 0)
                    {
                        if (board[0, 0] == ".")
                        {
                            board[aiRNum - 2, aiCNum] = ai;
                            return col;
                            done = true;
                            finished.PlayerDone = false;

                        }
                        if (board[1, 0] == ".")
                        {
                            board[aiRNum - 1, aiCNum] = ai;
                            return col;
                            done = true;
                            finished.PlayerDone = false;

                        }

                    }
                }

                if (rand == 2)
                {
                    // Second Column win (U - D)
                    if (aiRNum == 0 && aiCNum == 1)
                    {

                        if (board[1, 1] == ".")
                        {
                            board[aiRNum + 1, aiCNum] = ai;
                            return col;
                            done = true;
                            finished.PlayerDone = false;

                        }
                        if (board[2, 1] == ".")
                        {
                            board[aiRNum + 2, aiCNum] = ai;
                            return col;
                            done = true;
                            finished.PlayerDone = false;

                        }


                    }
                    // Second Column win (D - U)
                    if (aiRNum == 2 && aiCNum == 1)
                    {
                        if (board[0, 1] == ".")
                        {
                            board[aiRNum - 2, aiCNum] = ai;
                            return col;
                            done = true;
                            finished.PlayerDone = false;

                        }
                        if (board[1, 1] == ".")
                        {
                            board[aiRNum - 1, aiCNum] = ai;
                            return col;
                            done = true;
                            finished.PlayerDone = false;

                        }
                    }
                }

                if (rand == 3)
                {
                    // Third Column win (U - D)
                    if (aiRNum == 0 && aiCNum == 2)
                    {

                        if (board[1, 2] == ".")
                        {
                            board[aiRNum + 1, aiCNum] = ai;
                            return col;
                            done = true;
                            finished.PlayerDone = false;

                        }
                        if (board[2, 2] == ".")
                        {
                            board[aiRNum + 2, aiCNum] = ai;
                            return col;
                            done = true;
                            finished.PlayerDone = false;

                        }


                    }
                    // Third Column win (D - U)
                    if (aiRNum == 2 && aiCNum == 2)
                    {
                        if (board[0, 2] == ".")
                        {
                            board[aiRNum - 2, aiCNum] = ai;
                            return col;
                            done = true;
                            finished.PlayerDone = false;

                        }
                        if (board[1, 2] == ".")
                        {
                            board[aiRNum - 1, aiCNum] = ai;
                            return col;
                            done = true;
                            finished.PlayerDone = false;

                        }

                    }
                }


                // Diagonal wins --------------------

                if (rand == 4)
                {
                    // Diagonal win
                    if (aiRNum == 0 && aiCNum == 0)
                    {
                        if (board[1, 1] == ".")
                        {
                            board[aiRNum + 1, aiCNum + 1] = ai;
                            return col;
                            done = true;
                        }
                        if (board[2, 2] == ".")
                        {
                            board[aiRNum + 2, aiCNum + 2] = ai;
                            return col;
                            done = true;
                        }

                    }
                    // Reverse Diagonal win
                    if (aiRNum == 2 && aiCNum == 2)
                    {
                        if (board[0, 0] == ".")
                        {
                            board[aiRNum - 2, aiCNum - 2] = ai;
                            return col;
                            done = true;
                            finished.PlayerDone = false;
                        }
                        if (board[1, 1] == ".")
                        {
                            board[aiRNum - 1, aiCNum - 1] = ai;
                            return col;
                            done = true;
                            finished.PlayerDone = false;
                        }
                    }
                }
            }


            return col;
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
                || firCol.Equals(tripPlayer)
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