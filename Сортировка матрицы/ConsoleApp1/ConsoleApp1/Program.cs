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
            int[,] result = null;
            int n, m;
            using (var reader = new StreamReader("input.txt"))
            {
                var line = reader.ReadLine();
                var values = line.Split(' ').Select(int.Parse).ToArray();
                m = values[0];
                n = values[1];
                result = new int[m, n];

                for (int i = 0; i < m; i++)
                {
                    line = reader.ReadLine();
                    values = line.Split(' ').Select(int.Parse).ToArray();

                    for (int j = 0; j < n; j++)
                        result[i, j] = values[j];
                }             
            }

            int[] M = new int[m];
            int[] MaxM = new int[m];

            int[] N= new int [n];
            int []MaxN= new int [n];

            int count = 0;
            int maxCount = 0;

            // находим длину последовательности по строкам
            int diff = 0;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    
                    if (j == 0) continue;

                    var diffCurrent = result[i, j] - result[i, j-1];

                    if (diff == 0 && diffCurrent != 0) 
                    {
                        diff = diffCurrent;
                        count = 2;
                        continue;
                    }

                    if ((diffCurrent * diff > 0)  && (j == (n-1))) 
                    {
                        count++;
                        maxCount = count;
                        continue;
                    }

                    if (diffCurrent * diff > 0) 
                    {
                        count++;
                        continue;
                    }

                                   
                    diff = diffCurrent;
                    if (count > maxCount)
                    {
                        maxCount = count;
                    }
                    count = diffCurrent == 0 ? 1 : 2;                 
                }
                
                M[i] = i;
                MaxM[i] = maxCount;
                maxCount = 0;
                diff = 0;
            }

            int a = 0;
            for( int i=0; i<m-1; i++)
            {
                if (MaxM[i] < MaxM[i + 1])
                {
                    a = MaxM[i];
                    MaxM[i] = MaxM[i + 1];
                    MaxM[i + 1] = a;
                    a = M[i];
                    M[i] = M[i + 1];
                    M[i + 1] = a;
                }
            }

            int[,] result1 = new int[m,n];

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result1[i, j] = result[M[i], j];
                }
            }



            // находим длину последовательности по столбцам
            diff = 0;
            for (int j = 0; j < n; j++)
            {
                for (int i = 0; i < m; i++)
                {

                    if (i == 0) continue;

                    var diffCurrent = result1[i, j] - result1[i-1, j ];

                    if (diff == 0 && diffCurrent != 0) 
                    {
                        diff = diffCurrent;
                        count = 2;
                        continue;
                    }

                    if ((diffCurrent * diff > 0) && (i == (m - 1))) 
                    {
                        count++;
                        maxCount = count;
                        continue;
                    }

                    if (diffCurrent * diff > 0) 
                    {
                        count++;
                        continue;
                    }

                                   
                    diff = diffCurrent;
                    if (count > maxCount)
                    {
                        maxCount = count;
                    }
                    count = diffCurrent == 0 ? 1 : 2;                
                }
               
                N[j] = j;
                MaxN[j] = maxCount;
                maxCount = 0;
                diff = 0;
            }

            
            for (int i = 0; i < n - 1; i++)
            {
                if (MaxN[i] < MaxN[i + 1])
                {
                    a = MaxN[i];
                    MaxN[i] = MaxN[i + 1];
                    MaxN[i + 1] = a;
                    a = N[i];
                    N[i] = N[i + 1];
                    N[i + 1] = a;
                }
            }

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[i, j] = result1[i, N[j]];
                }
            }

            // записываем итоговую матрицу в файл
            string[] text = new string[n];
            for (int i = 0; i < m; i++)
            {
                text[i] = "";
                for (int j = 0; j < n; j++)
                    text[i] += String.Format("{0} ", result[i,j]);

            }

            File.WriteAllLines("output.txt", text);
            
        }
    }

}