using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public class Game
    {
        //Determination numbers of rows and columns in fixed values
        public const int ROWS = 5;
        public const int COLUMNS = 5;        
        
        // Create matrix in fixed values size
        private Letter[,] board = new Letter[ROWS, COLUMNS];
        private string randomWord;
        private int turn;
       
        public Game(string connStr)
        {
            InitBoard();
            BL.BL bl = new BL.BL(connStr);
            this.randomWord = bl.GetRandomWord();
            this.turn = 0;
        }

        // Board initialize
        // Each cell of the board is initialized with a "Letter" instance
        public void InitBoard()
        {
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLUMNS; j++)
                {
                    Letter le = new Letter('_',ConsoleColor.Black);
                    this.board[i, j] = le;                    
                }
            }
        }
        
        // Prints the board to the console
        public void PrintBoard()
        {
            Console.WriteLine();
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLUMNS; j++)
                {
                    Letter le = this.board[i, j];
                    Console.BackgroundColor = le.Background;
                    Console.Write(string.Concat(le.Value,' '));
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.WriteLine();
            }
        }

        // Gets the user guess, runs on the board and checks if the user letter is the same letter on the random word    
        // Sets the background according to the user guess.
        public void SaveGuess(string guess)
        {
            Dictionary<char, List<int>> dicPermutedChars = new Dictionary<char, List<int>>();
            StringBuilder updatedWord=new StringBuilder(randomWord);

            for (int i = 0; i < COLUMNS; i++)
            {
                //save to the board a letter of the guess
                this.board[this.turn, i].Value = guess[i];

                if (randomWord[i] == this.board[this.turn, i].Value)
                {
                    this.board[this.turn, i].Background = ConsoleColor.Green;
                    //Convert.ToChar(wordWithoutGreen[i])='#';
                    updatedWord[i] = '#';
                }
            }

            for (int i = 0; i < COLUMNS; i++)
            {
                // If current char is an exact match => continue with loop
                if (updatedWord[i] == '#')
                    continue;
                
                // Sets the current char of board
                char currentCharInGuess = this.board[this.turn, i].Value;

                // Check if the letter doesn't exist in the dictionary
                // If it doesn't - a new item is added to the dictionary:
                // Key - current char in guess
                // Value - an empty list
                if (!dicPermutedChars.ContainsKey(currentCharInGuess))
                    dicPermutedChars.Add(currentCharInGuess, new List<int>());

                //  dicPermutedChars[currentCharInGuess] - gets the list (Value) of the currentCharInGuess (Key)
                // .Add(i) - adds the current index to the list
                dicPermutedChars[currentCharInGuess].Add(i);
            }

            for (int i = 0; i < COLUMNS; i++)
            {
                if (updatedWord[i] == '#')
                    continue;

                char currentCharInRandom = updatedWord[i];

                if (!dicPermutedChars.ContainsKey(currentCharInRandom))
                    continue;

                // If current char in random word exists in the dictionary - that means the user guessed it somewhere (not in the right position)
                List<int> indexesOfCurrentChar = dicPermutedChars[currentCharInRandom];
                int matchIndexInGuess = indexesOfCurrentChar.First();
                this.board[this.turn, matchIndexInGuess].Background = ConsoleColor.Yellow;
                indexesOfCurrentChar.RemoveAt(0);
                if (indexesOfCurrentChar.Count == 0)
                    dicPermutedChars.Remove(currentCharInRandom);
            }

            this.turn++;
        }

        public bool IsWin()
        {
            for (int i = 0; i < ROWS; i++)
            {
                bool isWin = true;

                for (int j = 0; j < COLUMNS; j++)
                {
                    if (this.board[i, j].Background != ConsoleColor.Green)
                    {
                        isWin = false;
                        break;
                    }
                }

                if (isWin)
                    return true;
            }

            return false;
        }

        public bool IsLose()
        {
            //if (this.turn == ROWS)
            //    return true
            //return false;

            return this.turn == ROWS;
        }

        public void Run()
        {
            bool gameOver = false;
            while (!gameOver)
            {
                Console.WriteLine("\nPlease enter your guess");
                string guess = Console.ReadLine();
                SaveGuess(guess);
                PrintBoard();
                bool isWin = IsWin();
                if (isWin)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("\nYou WON !");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.ReadLine();                    
                }
            
                bool isLose = IsLose();
                if (isLose)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\nGame over!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.ReadLine();
                }

                gameOver = isWin || isLose;
            }
        }
    }
}
