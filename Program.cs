using System;
using System.IO;
using System.Text;

namespace sudoku
{

    class Program
    {

        public class SudokuSolver
        {
            private static int rows = 9, cols = 9;
            int[,] grid = new int[rows, cols];
            private bool number_used_in_row(int row, int num, ref int[,] s)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (s[row, c] == num)
                        return true;
                }
                return false;
            }

            private bool number_used_in_col(int col, int num, ref int[,] s)
            {
                for (int r = 0; r < rows; r++)
                {
                    if (s[r, col] == num)
                        return true;
                }
                return false;
            }

            private bool number_used_in_square(int row, int col, int num, ref int[,] s)
            {
                int gridCol = col / 3;
                int gridRow = row / 3;

                for (int r = 0; r < 3; r++)
                {
                    for (int c = 0; c < 3; c++)
                    {
                        if (s[gridRow * 3 + r, gridCol * 3 + c] == num)
                            return true;
                    }
                }
                return false;
            }
            private Tuple<int, int> get_empty_spot(ref int[,] s)
            {
                for (int r = 0; r < rows; r++)
                    for (int c = 0; c < cols; c++)
                        if (s[r, c] == 0)
                            return new Tuple<int, int>(r, c);

                return new Tuple<int, int>(9, 9);
            }

            private bool number_can_be_placed(int row, int col, int num, ref int[,] s)
            {
                return !number_used_in_col(col, num, ref s) &&
                        !number_used_in_row(row, num, ref s) &&
                        !number_used_in_square(row, col, num, ref s);
            }

            public void solveSudoku()
            {
                this.solveSudoku(ref this.grid);
            }
            private bool solveSudoku(ref int[,] s)
            {
                var nextPos = get_empty_spot(ref s);
                if (nextPos.Item1 == 9 && nextPos.Item2 == 9)
                    return true;

                // solve
                int row = nextPos.Item1;
                int col = nextPos.Item2;

                for (int num = 1; num <= 9; num++)
                {
                    if (number_can_be_placed(row, col, num, ref s))
                    {
                        s[row, col] = num;
                        if (solveSudoku(ref s))
                        {
                            return true;
                        }
                        else
                        {
                            s[row, col] = 0;
                        }
                    }
                }
                return false;
            }
            public void printSudoku()
            {
                this.printSudoku(ref this.grid);
            }


            private void printSudoku(ref int[,] s)
            {
                for (int r = 0; r < rows; r++)
                {
                    if (r % 3 == 0)
                        Console.WriteLine("-------------------------");
                    for (int c = 0; c < cols; c++)
                    {
                        if (c % 3 == 0)
                            Console.Write("| ");
                        if (s[r, c] != 0)
                            Console.Write($"{s[r, c]} ");
                        else
                        {
                            Console.Write(". ");
                        }
                        // Console.Write(s[r, c] + " ");
                    }
                    // Console.WriteLine();
                    Console.WriteLine("|");
                }
                Console.WriteLine("-------------------------");
            }
            public void loadSudoku(string sFilename)


            {
                if (!File.Exists(sFilename))
                {
                    Console.WriteLine("Sudoku file not found.");
                    Environment.Exit(-1);

                }
                // if file exists read file
                else
                {
                    using (StreamReader sr = File.OpenText(sFilename))
                    {
                        int lineCnt = 0;
                        string l = "";
                        while ((l = sr.ReadLine()) != null)
                        {
                            // Console.WriteLine(l);

                            int rowCnt = 0;
                            foreach (var e in l)
                            {

                                int val = 0;
                                if (!(e == ','))
                                    val = Convert.ToInt32(e) - 48;

                                // Console.Write(val);
                                this.grid[lineCnt, rowCnt++] = val;
                            }
                            // Console.WriteLine();
                            lineCnt++;
                        }
                    }
                }
            }
            public SudokuSolver()
            {

            }
            public SudokuSolver(string file)
            {
                this.loadSudoku(file);
            }
        }

        static void Main(string[] args)
        {
            SudokuSolver ssolver = new SudokuSolver();
            ssolver.loadSudoku("s2.txt");
            ssolver.solveSudoku();
            ssolver.printSudoku();
        }
    }
}
