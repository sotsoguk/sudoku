#include <iostream>
#include <fstream>
#include <string>
#include <vector>

using namespace std;
typedef vector<vector<int>> GRID;

bool number_used_in_row(int row, int num, const GRID &s)
{

    for (auto e : s.at(row))
        if (e == num)
            return true;
    return false;
}

bool number_used_in_col(int col, int num, const GRID &s)
{

    for (int row = 0; row < 9; row++)
        if (s[row][col] == num)
            return true;
    return false;
}

bool number_used_in_strait(int row, int col, int num, const GRID &s)
{
    // compute grid
    
    return false;
}

pair<int, int> get_empty_spot(const GRID &s)
{
    for (int row = 0; row < 9; row++)
        for (int col = 0; col < 9; col++)
        {
            if (s[row][col] == 0)
            {
                return make_pair(row, col);
            }
        }
    return make_pair(9, 9);
}

void printStrait(const GRID &s)
{

    int cnt = 0;
    for (auto &l : s)
    {
        if (cnt % 3 == 0)
            cout << "-------------------------" << endl;
        int col = 0;
        // cout <<"|";
        for (auto &e : l)
        {
            if (col % 3 == 0)
                cout << "| ";
            if (e != 0)
                cout << e << " ";
            else
            {
                cout << ". ";
            }

            col++;
        }
        cout << "|";
        cout << endl;
        cnt++;
    }
    cout << "-------------------------" << endl;
}

bool number_can_be_placed(int row, int col, int num, const GRID &s)
{

    return !number_used_in_col(col, num, s) &&
           !number_used_in_row(row, num, s) &&
           !number_used_in_strait(row, col, num, s);
}
bool solveStrait(GRID &s)
{

    // check if sudoku is solved
    pair<int, int> nextPos = get_empty_spot(s);
    if (nextPos == make_pair(9, 9))
        return true;

    // solve
    int row = nextPos.first;
    int col = nextPos.second;
    for (int num = 1; num <= 9; num++)
    {
        if (number_can_be_placed(row, col, num, s))
        {
            s[row][col] = num;

            if (solveSudoku(s))
                return true;
            else
            {
                s[row][col] = 0;
            }
        }
    }
    return false;
}
int main(int argc, char *argv[])
{

    // check cli commands
    if (argc != 2)
    {
        cout << "Use:\n strait file\n";
        return -1;
    }
    vector<vector<int>> s(
        9,
        vector<int>(9, 0));

    // read sudkou

    string sFilename = argv[1];
    string line;
    ifstream sFile(sFilename);
    if (sFile.is_open())
    {
        int i = 0;
        while (getline(sFile, line))
        {

            for (int j = 0; j < 9; ++j){
                int tmp = line.at((j)) - 48;
                if (tmp ==-2)
                    tmp = 0;
                if (tmp == 88 || tmp == 120)
                    tmp =-1;
                s[i][j] = tmp;
                //s.at(i).at(j) = line.at((j)) - 48;
            }
            i++;
        }
    }
    else
    {
        cout << "Could not open input file.\n";
        return -1;
    }

    cout << "Empty Sudoku:\n\n";
    printStrait(s);
    // cout << "\n\n";
    // solveStrait(s);
    // cout << "Solution:\n\n";
    // printStrait(s);
    return 0;
}
