using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CodeChef_Easy_Omax
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] iMatrixHeader = new int[2];

            //read in stdin.txt
            StreamReader sr = new StreamReader(@"C:\Users\jl\Documents\Visual Studio 2013\Projects\CodeChef_Easy_Omax\CodeChef_Easy_Omax\stdin.txt");
            while (!sr.EndOfStream)
            {
                //read in Matrix
                iMatrixHeader = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();
                int[,] iMatrix = new int[iMatrixHeader[0], iMatrixHeader[1]];
                for (int pos_m = 0; pos_m < iMatrixHeader[0]; pos_m++)
                {
                    int[] iTmpArr = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();
                    for (int pos_n = 0; pos_n < iMatrixHeader[1]; pos_n++)
                    {
                        iMatrix[pos_m, pos_n] = iTmpArr[pos_n];
                    }
                }

                //Solve Matrix
                Console.WriteLine(SolveMatrix(iMatrix).ToString());
            }
            sr.Close();
            Console.ReadLine();
        }

        public static int SolveMatrix(int[,] iSrcMatrix)
        {
            int iRetVal = 0;

            //First determine how big the 'O' can be.
            int iMaxO_Y = iSrcMatrix.GetLength(0);
            int iMaxO_X = iSrcMatrix.GetLength(1);
            int iO_Y = iMaxO_Y;
            int iO_X = iMaxO_X;

            //Determines smallest
            while (iO_X > 2 && iO_Y > 2)
            {
                iO_X = iO_X - 2;
                iO_Y = iO_Y - 2;
            }

            //Add up values
            for (int iTmpYPos = 0; iTmpYPos < iMaxO_Y; iTmpYPos++)
            {
                for (int iTmpXPos = 0; iTmpXPos < iMaxO_X; iTmpXPos++)
                {
                    if (iTmpYPos == 0 || iTmpYPos == iMaxO_Y - 1)
                    {
                        iRetVal += iSrcMatrix[iTmpYPos, iTmpXPos];
                    }
                    else if (iTmpXPos == 0 || iTmpXPos == iMaxO_X - 1)
                    {
                        iRetVal += iSrcMatrix[iTmpYPos, iTmpXPos];
                    }
                    else
                    {
                        iRetVal += 0;
                    }
                }
            }
                
            return iRetVal;
        }
    }
}
