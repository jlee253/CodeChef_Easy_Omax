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
            //read in stdin.txt
            //StreamReader sr = new StreamReader(@"C:\Users\jl\Documents\Visual Studio 2013\Projects\CodeChef_Easy_Omax\CodeChef_Easy_Omax\stdin.txt"); //work
            StreamReader sr = new StreamReader(@"C:\Users\Jordan Lee\Documents\Visual Studio 2013\Projects\CodeChef_Easy_Omax\stdin.txt"); //home
            while (!sr.EndOfStream)
            {
                //read in Matrix
                int[] iMatrixHeader = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();
                if (iMatrixHeader[0] == 0 && iMatrixHeader[1] == 0) break;
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
                Console.WriteLine(SolveMatrices(iMatrix).ToString());
            }
            sr.Close();
            Console.ReadLine();
        }

        public static int SolveMatrices(int[,] iSrcMatrix)
        {
            int iRetMaxVal = 0;

            //First determine how big the matrix is.
            int iMaxO_X = iSrcMatrix.GetLength(0);
            int iMaxO_Y = iSrcMatrix.GetLength(1);
            int iO_X_Len = 0, iO_Y_Len = 0;
            int iO_X_Pos = 0, iO_Y_Pos = 0;

            //Matrix size check
            if (iMaxO_X < 3 || iMaxO_Y < 3) return 0;

            for (int iSrcMatrixPos = 0; iSrcMatrixPos < iSrcMatrix.Length; iSrcMatrixPos++)
            {
                iO_X_Pos = iSrcMatrixPos / iMaxO_X;
                iO_Y_Pos = iSrcMatrixPos % iMaxO_X;

                iO_X_Len = 3;
                while (iO_X_Pos + iO_X_Len <= iMaxO_X)
                {
                    iO_Y_Len = 3;
                    while (iO_Y_Pos + iO_Y_Len <= iMaxO_Y)
                    {
                        int[,] iTmpMatrix = new int[iO_X_Len, iO_Y_Len];

                        //fill temp matrix
                        for (int iTmpX = 0; iTmpX < iO_X_Len; iTmpX++)
                        {
                            for (int iTmpY = 0; iTmpY < iO_Y_Len; iTmpY++)
                            {
                                iTmpMatrix[iTmpX, iTmpY] = iSrcMatrix[iO_X_Pos + iTmpX, iO_Y_Pos + iTmpY];
                            }
                        }

                        int iTmpMatrixVal = SolveForAllOCombinationsInMatrix(iTmpMatrix);
                        if (iTmpMatrixVal > iRetMaxVal) iRetMaxVal = iTmpMatrixVal;

                        iO_Y_Len++;
                    }
                    iO_X_Len++;
                }
            }

            return iRetMaxVal;
        }

        public static int SolveForAllOCombinationsInMatrix(int[,] iSrcMatrix)
        {
            int iRetMaxVal = 0;

            //First determine how big the 'O' can be.
            int iMaxO_X = iSrcMatrix.GetLength(0) - 2; //doubles as length
            int iMaxO_Y = iSrcMatrix.GetLength(1) - 2; //doubles as length
            int iO_X_Len = 0, iO_Y_Len = 0;
            int iO_X_Pos = 0, iO_Y_Pos = 0;

            //Matrix size check
            if (iMaxO_X < 1 || iMaxO_Y < 1) return 0;

            //initialize temporary matrix
            int[,] iTmpMatrix = new int[iSrcMatrix.GetLength(0), iSrcMatrix.GetLength(1)];

            while (iO_Y_Pos < iMaxO_Y && iO_X_Pos < iMaxO_X)
            {
                //increment O search base
                iO_X_Pos++;
                iO_Y_Pos++;

                //search horizontally
                iO_X_Len = 1;
                while (iO_X_Len <= iMaxO_X) //iMaxO_X being used and length
                {
                    iO_Y_Len = 1;
                    while (iO_Y_Len <= iMaxO_Y)
                    {
                        Array.Copy(iSrcMatrix, iTmpMatrix, iSrcMatrix.Length);

                        //update temp matrix
                        for (int iTmpX = iO_X_Pos; iTmpX <= iO_X_Len; iTmpX++)
                        {
                            for (int iTmpY = iO_Y_Pos; iTmpY <= iO_Y_Len; iTmpY++)
                            {
                                iTmpMatrix[iTmpX, iTmpY] = 0;
                            }
                        }

                        //get score and update if a new high is found.
                        int iTmpMatrixVal = ReturnMatrixScore(iTmpMatrix);
                        if (iTmpMatrixVal > iRetMaxVal) iRetMaxVal = iTmpMatrixVal;

                        iO_Y_Len++; //extend length
                    }
                    iO_X_Len++; //extend length
                }

                //search vertically
                iO_Y_Len = 1;
                while (iO_Y_Len <= iMaxO_Y)
                {
                    iO_X_Len = 1;
                    while (iO_X_Len <= iMaxO_X)
                    {
                        Array.Copy(iSrcMatrix, iTmpMatrix, iSrcMatrix.Length);

                        //update temp matrix
                        for (int iTmpY = iO_Y_Pos; iTmpY <= iO_Y_Len; iTmpY++)
                        {
                            for (int iTmpX = iO_X_Pos; iTmpX <= iO_X_Len; iTmpX++)
                            {
                                iTmpMatrix[iTmpX, iTmpY] = 0;
                            }
                        }

                        //get score and update if a new high is found.
                        int iTmpMatrixVal = ReturnMatrixScore(iTmpMatrix);
                        if (iTmpMatrixVal > iRetMaxVal) iRetMaxVal = iTmpMatrixVal;

                        iO_X_Len++; //extend length
                    }
                    iO_Y_Len++; //extend length
                }
            }
                
            return iRetMaxVal;
        }

        public static int ReturnMatrixScore(int[,] iSrcMatrix)
        {
            int iRetVal = 0;

            //First determine how big the matrix is.
            int iMaxO_Y = iSrcMatrix.GetLength(0);
            int iMaxO_X = iSrcMatrix.GetLength(1);

            //Add up values
            for (int iTmpYPos = 0; iTmpYPos < iMaxO_Y; iTmpYPos++)
            {
                for (int iTmpXPos = 0; iTmpXPos < iMaxO_X; iTmpXPos++)
                {
                    iRetVal += iSrcMatrix[iTmpYPos, iTmpXPos];
                }
            }

            return iRetVal;
        }
    }
}
