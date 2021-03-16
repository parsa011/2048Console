using System;
using System.Collections.Generic;
using System.Linq;

namespace _2048Console
{
    class Program
    {
        public static bool GameGoesOn = true;
        public static int MovesCount = 0;
        public static int[,] Board = new int[4,4]
        {
            {0,0,0,0},
            {0,0,0,0},
            {0,0,0,0},
            {0,0,0,0},
        };
        public static int Score = 0;
        public static Dictionary<string,(int,int)> FreeSpawns = new Dictionary<string,(int,int)>();
        public static bool UserWone  = true;

        static void Main(string[] args)
        {
            PrintBoard();
            GenerateRandomNumberOnBoard(3);
            while(GameGoesOn)
            {
                Console.Clear();
                PrintBoard();
                ReadKey();
                //CheckForLose();
                GenerateRandomNumberOnBoard();
                CalculateScore();
            }
            Console.WriteLine("You" + (UserWone ? "Wone" : "Lose"));
            Console.ReadKey();
        }

        private static void CheckForLose()
        {
            if(FreeSpawns.Count == 0)
            {
                UserWone = false;
                GameGoesOn = false;
            }
        }

        private static void ReadKey()
        {
            ConsoleKeyInfo keyinfo = Console.ReadKey();
            if (keyinfo.Key == ConsoleKey.UpArrow)
                VerticalMove();
            else if (keyinfo.Key == ConsoleKey.DownArrow)
                VerticalMove(false);
            else if(keyinfo.Key == ConsoleKey.RightArrow)
                HorizontalMove(false);
            else if(keyinfo.Key == ConsoleKey.LeftArrow)
                HorizontalMove();
            else   
                Console.WriteLine("Plese Press Valid key");
            
        }

        private static void VerticalMove(bool downToUp = true)
        {
            var iIndexValue = downToUp ? 0 : 3;
            for(var j = 0; j < 4;j++)
            {
                for(var loop = 0; loop < 4;loop++)
                {
                    for(var i = iIndexValue; downToUp ? i < 3 : i > 0;)
                    {
                        var nextI = downToUp ? i + 1 : i - 1;
                        if(Board[nextI,j] == Board[i,j] || Board[i,j] == 0)
                        {           
                            var next = Board[nextI,j];
                            Board[nextI,j] = 0;
                            Board[i,j] += next;    
                        }
                        i = downToUp ? i + 1 : i - 1;
                    }
                }
               
            }
        }

        private static void HorizontalMove(bool leftToRight = true)
        {
            var jIndexValue = leftToRight ? 0 : 3;
            for(var i = 0; i < 4;i++)
            {
                for(var loop = 0; loop < 4;loop++)
                {
                    for(var j = jIndexValue; leftToRight ? j < 3 : j > 0;)
                    {
                        var nextJ = leftToRight ? j + 1 : j - 1;
                        if(Board[i,nextJ] == Board[i,j] || Board[i,j] == 0)
                        {           
                            var next = Board[i,nextJ];
                            Board[i,nextJ] = 0;
                            Board[i,j] += next;
                        }
                        j = leftToRight ? j + 1 : j - 1;
                    }
                }
            }
        }

        private static void GenerateRandomNumberOnBoard(int count = 1)
        {
            if(FreeSpawns.Count != 0)
            {
                var allowedNumber = new int[]{2,4};
                Random rnd;
                for(var i = 0; i < count;i++)
                {
                    rnd = new Random(DateTime.Now.Second);
                    int targetIndex = 0,forWrite;
                    forWrite = rnd.Next(0,2);
                    targetIndex = rnd.Next(0,FreeSpawns.Count);
                    int x = FreeSpawns.ToList()[targetIndex].Value.Item1;
                    int y = FreeSpawns.ToList()[targetIndex].Value.Item2;
                    GetIndexValue(x,y,out var thisIndex);
                    if(thisIndex == 0)
                    {
                        WriteOnBoard(x,y,allowedNumber[forWrite]);
                        continue;
                    }
                    i--;
                }
            }
        }

        private static void GetIndexValue(int x,int y,out int res)
        {
            res = Board[x,y];
        }

        private static void WriteOnBoard(int x,int y, int value)
        {
            Board[x,y] = value;
        }

        private static void PrintBoard()
        {
            Console.Write("\n\n");
            Console.Write($"\t\t\tscore is : {Score}\n\n");
            for(var i = 0; i < 4;i++)
            {
                Console.Write("\t\t\t");
                for(var j = 0; j < 4;j++)
                {
                    var value = Board[i,j];
                    if(value == 2)
                        Console.ForegroundColor = ConsoleColor.Green;
                    else if(value == 4)
                        Console.ForegroundColor = ConsoleColor.Red;
                    else if(value == 8)
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else if(value == 16)
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    else if(value == 32)
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    else if(value == 64)
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                    else if(value == 128)
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    else if(value == 256)
                        Console.ForegroundColor = ConsoleColor.Gray;
                    else if(value == 512)
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(value + "\t");
                    Console.ResetColor();
                }
                Console.WriteLine("\n\n");
            }
            CollectZeroIndexes();
        }

        private static void CollectZeroIndexes()
        {
            FreeSpawns.Clear();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    var value = Board[i,j];
                    if(value == 0) FreeSpawns.Add(Guid.NewGuid().ToString(),(i,j));
                }
            }
        }

        public static void CalculateScore()
        {
            Score = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Score += Board[i,j];
                }
            }
        }

        private static string GiveMeSpace(int count = 1)
        {
            var finall = "";
            for(var i =0 ;i < count;i++)
            {
                finall += "\t";
            }
            return finall;
        }
    }
}