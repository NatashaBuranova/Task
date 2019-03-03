using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            // считываем матрицу из файла
            int[,] g = null;
            int N, P,Q;
            using (var reader = new StreamReader("input.txt"))
            {
                var line = reader.ReadLine();
                var values = line.Split(' ').Select(int.Parse).ToArray();
                N = values[0];
                P = values[1];
                Q = values[2];
                g = new int[N, N];
                

                for (int i = 0; i < N; i++)
                {
                    line = reader.ReadLine();
                    values = line.Split(' ').Select(int.Parse).ToArray();

                    g[values[0] - 1, values[1] - 1] = 1;
                    
                }
            }

            Queue<int> q = new Queue<int>();
            int[] p = new int[N+1];
            int u;


            bool[] used = new bool[N + 1];  
            
            used[N] = true;
            p[N] = -1;

            u = P-1;
            q.Enqueue(u);
           
            while (q.Count != 0)
            {
                u = q.Peek();
                q.Dequeue();

                for (int i = 0; i < N; i++)
                {
                    if (Convert.ToBoolean(g[u,i]))
                    {

                        if (!used[i])
                        {
                            used[i] = true;
                            q.Enqueue(i);
                            p[i] = u;
                           
                        }
                    }
                }
            }
                       

            if (!used[Q-1])
                Console.WriteLine("No way!");
            else
            {
                List<int> way = new List<int>();
                
                for (int v = Q-1; v != 1; v = p[v])
                    way.Add(v);

                string[] result = new string[way.Count+1];
                
                result[0] = String.Format("{0} ", P);

                for (int i = way.Count -1 ; i >=0; i--)
                    result[way.Count-i] += String.Format("{0} ", way[i] + 1);

               
                File.WriteAllLines("output.txt", result);

            }
                        
        }

    }
}
