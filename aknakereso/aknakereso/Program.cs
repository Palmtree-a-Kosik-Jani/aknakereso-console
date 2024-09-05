using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aknakereso
{
    internal class Program
    {
        static int[,] palya = new int[10, 10];
        static string[,] FelsoPalya = new string[10, 10];
        static Boolean Talalt = false;
        static int BombJel;
        static void Main(string[] args)
        {
            kezdes();
            BombJel = 0;
            Talalt = false;
            palyaGen();


            Console.ReadKey();
        }
        static string[] tipp;
        private static void palyaGen()
        {
            while(Talalt == false)
            {
                Console.Clear();
                for (int i = 0; i < palya.GetLength(0); i++)
                {
                    for (int j = 0; j < palya.GetLength(1); j++)
                    {
                        
                        Console.Write($"{FelsoPalya[i, j],3} ");
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    
                }
                Console.WriteLine();
                Console.Write("Adj meg egy koordinatat (pl.: 1,2, ha aknát szeretnél jelölni akkor: a,1,2): ");
                tipp = Console.ReadLine().Split(',');
                Console.WriteLine();
              
                MezoNyit();
            }
            
        }

        private static void MezoNyit()
        {
            try
            {
                if (tipp.Length==2)
                {
                    if (FelsoPalya[int.Parse(tipp[0]), int.Parse(tipp[1])] == "X" || FelsoPalya[int.Parse(tipp[0]), int.Parse(tipp[1])] == "-1")
                    {
                        if (palya[int.Parse(tipp[0]), int.Parse(tipp[1])] == -1)
                        {
                            Console.Clear();
                            for (int i = 0; i < palya.GetLength(0); i++)
                            {
                                for (int j = 0; j < palya.GetLength(1); j++)
                                {

                                    Console.Write($"{palya[i, j],3} ");

                                }
                                Console.WriteLine();
                                Console.WriteLine();
                            }
                            Console.WriteLine();
                            Console.WriteLine("Vesztettél!");
                            Talalt = true;
                        }
                        if (palya[int.Parse(tipp[0]), int.Parse(tipp[1])] > 0)
                        {
                            FelsoPalya[int.Parse(tipp[0]), int.Parse(tipp[1])] = Convert.ToString(palya[int.Parse(tipp[0]), int.Parse(tipp[1])]);
                        }
                        if (palya[int.Parse(tipp[0]), int.Parse(tipp[1])] == 0)
                        {
                            FedezFel(int.Parse(tipp[0]), int.Parse(tipp[1]));

                        }
                    }
                }
                else
                {
                    if (FelsoPalya[int.Parse(tipp[1]), int.Parse(tipp[2])] == "X")
                    {
                        FelsoPalya[int.Parse(tipp[1]), int.Parse(tipp[2])] = "-1";
                        
                        if(palya[int.Parse(tipp[1]), int.Parse(tipp[2])] == -1) BombJel++;
                        if(BombJel == bombC)
                        {
                            Console.Clear();
                            for (int i = 0; i < palya.GetLength(0); i++)
                            {
                                for (int j = 0; j < palya.GetLength(1); j++)
                                {

                                    Console.Write($"{palya[i, j],3} ");

                                }
                                Console.WriteLine();
                                Console.WriteLine();
                            }
                            Console.WriteLine();
                            Console.WriteLine("Nyertél!");
                            Talalt = true;
                        }
                    }
                }
                
            }
            catch 
            {
                palyaGen();
            }
            
        }

        private static void FedezFel(int x, int y)
        {
            if (palya[x, y] == 9)   return;

            FelsoPalya[x, y] = palya[x, y].ToString();

            if (palya[x, y] != 0)   return;

            palya[x, y] = 9;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0 )
                        continue;

                    if (x + i >= 0 && x + i < palya.GetLength(0) && y + j >= 0 && y + j < palya.GetLength(1))
                    {
                        FedezFel(x + i, y + j);
                    }
                }
            }
        }
        static int bombC = 0;
        private static void kezdes()
        {
            for (int i = 0; i < palya.GetLength(0); i++)
            {
                for (int j = 0; j < palya.GetLength(1); j++)
                {
                    FelsoPalya[i, j] = "X";
                }
                
            }
            Console.Write("hany bomba? (max. 10): ");
            bombC = int.Parse(Console.ReadLine());
            if (bombC > 10)
            {
                Console.Write("Kisebb szám kell!: ");
                bombC = int.Parse(Console.ReadLine());
            }
            if (bombC <= 0)
            {
                Console.Write("Nagyobb szám kell!: ");
                bombC = int.Parse(Console.ReadLine());
            }
            BombGen(bombC);
        }

        private static void BombGen(int C)
        {
            Random rand = new Random();


            for (int i = 0; i < C; i++)
            {
                int xbomb = rand.Next(10);
                int ybomb = rand.Next(10);

                if (palya[xbomb,ybomb] == -1)
                {
                    while (palya[xbomb, ybomb] == -1)
                    {
                        xbomb = rand.Next(10);
                        ybomb = rand.Next(10);
                    }
                    palya[xbomb, ybomb] = -1;
                }
                else palya[xbomb, ybomb] = -1;
            }
            BombCountPalyan();
        }

        private static void BombCountPalyan()
        {
            for (int i = 0;i < 10; i++)
            {
                for (int a = 0; a < 10; a++)
                {
                    if (palya[i,a] == -1)
                    {
                        KorbeJar(i,a);
                    }
                }
            }
        }

        private static void KorbeJar(int x, int y)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int a = -1; a <= 1; a++)
                {
                    if (x + i >= 0 && x + i < palya.GetLength(0) && y + a >= 0 && y + a < palya.GetLength(1))
                    { 
                        if (palya[x + i, y + a] != -1)
                        {
                            palya[x + i, y + a] += 1; 
                        }
                    }
                }
            }
        }

        
    }
}
