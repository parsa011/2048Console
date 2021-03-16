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

        public static Dictionary<string,(int,int)> FreeSpawns = new Dictionary<string,(int,int)>();

        static void Main(string[] args)
        {
            PrintBoard();
            GenerateRandomNumberOnBoard(3);
            while(GameGoesOn)
            {
                Console.Clear();
                PrintBoard();
                ReadKey();
                GenerateRandomNumberOnBoard();
            }
        }

        private static void ReadKey()
        {
            ConsoleKeyInfo keyinfo = Console.ReadKey();
            if (keyinfo.Key == ConsoleKey.UpArrow)
            {
                VerticalMove();
            }
            else if (keyinfo.Key == ConsoleKey.DownArrow)
            {
                VerticalMove(false);
            }
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

        private static void GenerateRandomNumberOnBoard(int count = 1)
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
            for(var i = 0; i < 4;i++)
            {
                Console.Write("\t\t\t");
                for(var j = 0; j < 4;j++)
                {
                    var value = Board[i,j];
                    if(value == 0)
                        FreeSpawns.Add(Guid.NewGuid().ToString(),(i,j));
                    else if(FreeSpawns.Any(a=>a.Value.Item1 == i && a.Value.Item2 == j))
                        FreeSpawns.Remove(FreeSpawns.FirstOrDefault(a=> a.Value.Item1 == i && a.Value.Item2 == j).Key);
                    if(value == 2)
                        Console.ForegroundColor = ConsoleColor.Green;
                    else if(value == 4)
                        Console.ForegroundColor = ConsoleColor.Red;
                    else if(value == 8)
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else if(value == 16)
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(value + "\t");
                    Console.ResetColor();
                }
                Console.WriteLine("\n\n");
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
