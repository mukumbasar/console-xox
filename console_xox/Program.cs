namespace console_xox
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[,] board = new string[3, 3];
            try
            {
                CreateBoard(board);
                ShowBoard(board);
                RunGame(board);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex);
            }
        }
        public static void CreateBoard(string[,] board)
        {

            int count = 0;
            int upperBoundZero = board.GetUpperBound(0);
            int upperBoundOne = board.GetUpperBound(1);

            for (int i = 0; i <= upperBoundZero; i++)
            {
                for (int j = 0; j <= upperBoundOne; j++)
                {
                    count++;
                    board[i, j] = count.ToString();
                }
            }


        }

        private static void ShowBoard(string[,] board)
        {
            Console.Clear();

            int upperBoundZero = board.GetUpperBound(0);
            int upperBoundOne = board.GetUpperBound(1);

            for (int i = 0; i <= upperBoundZero; i++)
            {
                for (int j = 0; j <= upperBoundOne; j++)
                {
                    Console.Write("|" + board[i, j]);
                    if (j == upperBoundOne) Console.WriteLine("|");
                }
                Console.WriteLine("-------");
            }
        }

        private static void RunGame(string[,] board)
        {
            char[] pointers = SetPointers();
            char playerPointer = pointers[0];
            char opponentPointer = pointers[1];

            int sayac = 1;

            bool playerWon = false;
            bool opponentWon = false;
            while (sayac < 5)
            {
                if (playerPointer == 'X')
                {
                    RunPlayerGameplay(board, playerPointer, sayac);
                    ShowBoard(board);

                    if (CheckForWinner(board, playerPointer) == true)
                    {
                        playerWon = true;
                        goto PlayerWon;

                    }
                    Thread.Sleep(1000);
                    RunOpponentGameplay(board, opponentPointer);
                    ShowBoard(board);
                    sayac++;
                }
                else
                {
                    Thread.Sleep(1000);
                    RunOpponentGameplay(board, opponentPointer);
                    ShowBoard(board);

                    if (CheckForWinner(board, opponentPointer) == true)
                    {
                        opponentWon = true;
                        goto PlayerWon;
                    }

                    RunPlayerGameplay(board, playerPointer, sayac);
                    ShowBoard(board);
                    sayac++;
                }

                if (CheckForWinner(board, playerPointer) == true)
                {
                    playerWon = true;
                    goto PlayerWon;

                }
                else if (CheckForWinner(board, opponentPointer) == true)
                {
                    opponentWon = true;
                    goto PlayerWon;
                }

            }

        PlayerWon:
            if (playerWon)
            {
                Console.WriteLine("Oyuncu kazandı!");
            }
            else if (opponentWon)
            {
                Console.WriteLine("Bilgisayar kazandı!");
            }
            else
            {
                Console.WriteLine("Berabere!");
            }


        }

        private static char[] SetPointers()
        {
            char[] pointers = { 'p', 'p' };
            Random rnd = new Random();
            int coin = rnd.Next(1, 3);

            if (coin == 2)
            {
                pointers[0] = 'X';
                pointers[1] = 'O';

                Console.WriteLine("Oyuncu ilk hamleyi yapacak! Oyuncunun işaretçisi: X");

                Thread.Sleep(3000);
            }
            else
            {
                pointers[0] = 'O';
                pointers[1] = 'X';

                Console.WriteLine("Bilgisayar ilk hamleyi yapacak! Oyuncunun işaretçisi: O");

                Thread.Sleep(5000);
            }

            return pointers;

        }

        private static void RunPlayerGameplay(string[,] board, char playerPointer, int sayac)
        {
            Console.Write("Bölge seçiniz: ");
        sec:
            string area = Console.ReadLine();
            switch (area)
            {
                case "1":
                    board[0, 0] = playerPointer.ToString();
                    break;
                case "2":
                    board[0, 1] = playerPointer.ToString();
                    break;
                case "3":
                    board[0, 2] = playerPointer.ToString();
                    break;
                case "4":
                    board[1, 0] = playerPointer.ToString();
                    break;
                case "5":
                    board[1, 1] = playerPointer.ToString();
                    break;
                case "6":
                    board[1, 2] = playerPointer.ToString();
                    break;
                case "7":
                    board[2, 0] = playerPointer.ToString();
                    break;
                case "8":
                    board[2, 1] = playerPointer.ToString();
                    break;
                case "9":
                    board[2, 2] = playerPointer.ToString();
                    break;
                default:
                    goto sec;
            }

        }

        private static void RunOpponentGameplay(string[,] board, char opponentPointer)
        {
            int upperBoundZero = board.GetUpperBound(0);
            int upperBoundOne = board.GetUpperBound(1);

            Random rnd = new Random();
            int dice = rnd.Next(1, 3);

            int x = 0, y = 0;

            bool computerPlayed = false;

            while (!computerPlayed)
            {
                x = rnd.Next(0, 3);
                y = rnd.Next(0, 3);

                if ((int)char.Parse(board[x, y]) < 59)
                {
                    board[x, y] = opponentPointer.ToString();
                    computerPlayed = true;
                }
            }

        //if(dice == 2) 
        //{
        //    for (int i = 0; i <= upperBoundZero; i++)
        //    {
        //        for (int j = 0; j <= upperBoundOne; j++)
        //        {
        //            Console.WriteLine((int)char.Parse(board[i, j]));
        //            if ((int)char.Parse(board[i, j]) < 59)
        //            {
        //                board[i, j] = opponentPointer.ToString();
        //                goto endTurn;
        //            }
        //        }
        //    }
        //}
        //else
        //{
        //    for (int i = upperBoundZero; i >= 0; i--)
        //    {
        //        for (int j = upperBoundOne; j >=0 ; j--)
        //        {
        //            Console.WriteLine((int)char.Parse(board[i, j]));
        //            if ((int)char.Parse(board[i, j]) < 59)
        //            {
        //                board[i, j] = opponentPointer.ToString();
        //                goto endTurn;
        //            }
        //        }
        //    }
        //}

        endTurn:
            Console.WriteLine();
        }

        private static bool CheckForWinner(string[,] board, char pointer)
        {

            bool winner = false;
            string p = pointer.ToString();

            if (((board[0, 0] == p) && (board[0, 1] == p) && (board[0, 2] == p)) ||
                ((board[1, 0] == p) && (board[1, 1] == p) && (board[1, 2] == p)) ||
                ((board[2, 0] == p) && (board[2, 1] == p) && (board[2, 2] == p)) ||
                ((board[0, 0] == p) && (board[1, 0] == p) && (board[2, 0] == p)) ||
                ((board[0, 1] == p) && (board[1, 1] == p) && (board[2, 1] == p)) ||
                ((board[0, 2] == p) && (board[1, 2] == p) && (board[2, 2] == p)) ||
                ((board[0, 0] == p) && (board[1, 1] == p) && (board[2, 2] == p)) ||
                ((board[2, 0] == p) && (board[1, 1] == p) && (board[0, 2] == p)))
            {
                winner = true;
                return winner;
            }
            else return winner;
        }
    }

}