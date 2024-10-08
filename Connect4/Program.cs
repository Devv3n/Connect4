#pragma warning disable // :)

class Program {
    static int[,] grid = new int[6,7];
    static bool gameLoop = true;

    static void DrawGrid() {
        string draw = "| ";
        for (int y = 0; y < 6; y++) {
            for (int x = 0; x < 7; x++) {
                switch (grid[y,x]) {
                    case 0:
                        draw += "  ";
                        break;

                    case 1:
                        draw += "X ";
                        break;

                    case 2:
                        draw += "O ";
                        break;                    
                }
            }
            draw += "|\n| ";
        }
        draw += "= = = = = = = |\n| 1 2 3 4 5 6 7 |";
        Console.WriteLine(draw);
    }

    static void CheckWin(int row, int column, int plrNumber, string plrName) { // i hate this i hate this i hate this i hate this i hate this 
        int horizontal = 0;
        for (int x = column-1; x >= 0; x--) {
            if (grid[row, x] == plrNumber)
                horizontal += 1;
            else
                break;
        }
        for (int x = column; x < 7; x++) {
            if (grid[row, x] == plrNumber)
                horizontal += 1;
            else
                break;
        }

        int vertical = 0;
        for (int y = row-1; y >= 0; y--) {
            if (grid[y, column] == plrNumber)
                vertical += 1;
            else
                break;
        }
        for (int y = row; y < 6; y++) {
            if (grid[y, column] == plrNumber)
                vertical += 1;
            else
                break;
        }

        int diagonalForward = 0;
        for (int i = Math.Min(5-row, column); i >= 0; i--) {
            if (grid[row+i, column-i] == plrNumber)
                diagonalForward += 1;
            else
                break;
        }
        for (int i = Math.Min(row, 6-column); i >= 0; i--) {
            if (grid[row-i, column+i] == plrNumber)
                diagonalForward += 1;
            else
                break;
        }

        int diagonalBackwards = 0;
        for (int i = Math.Min(row, column)-1; i >= 0; i--) {
            if (grid[row-i, column-i] == plrNumber)
                diagonalBackwards += 1;
            else
                break;
        }
        for (int i = Math.Min(5-row, 6-column); i >= 0; i--) {
            if (grid[row+i, column+i] == plrNumber)
                diagonalBackwards += 1;
            else
                break;
        }

        if (horizontal >= 4 || vertical >= 4 || diagonalForward >= 4 || diagonalBackwards >= 4) {
            gameLoop = false;

            Console.Clear();
            DrawGrid();
            Console.WriteLine($"\nPlayer {plrNumber} ({plrName}) won!");
        }
    }

    static void GetPlayerInput(string plrName, char plrSymbol, int plrNumber) {
        string message = null;
        while (gameLoop) {
            Console.Clear();
            DrawGrid();

            Console.WriteLine($"\n{message}");
            Console.Write($"{plrName} ({plrSymbol}), enter a column between 1 and 7: ");

            if (int.TryParse(Console.ReadLine(), out int column) && 1 <= column && column <= 7) {
                for (int y = 5; y >= 0; y--) {
                    if (grid[y, column-1] == 0) {
                        grid[y, column-1] = plrNumber;
                        CheckWin(y, column-1, plrNumber, plrName);
                        return;
                    }
                    else {
                        message = "This column is full!!";
                    }
                }
            }
            else {
                message = "Invalid input please follow the instructions!!!";
            }
        }
    }

    static string GetPlayerName(int plrNumber, char plrSymbol) {
        while (true) {
            Console.Write($"Enter player {plrNumber}'s ({plrSymbol}) name: ");
            string name = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(name)) {
                return name;
            }
        }
    }

    static void Main(string[] args) {
        for (int y = 0; y < 6; y++) {
            for (int x = 0; x < 7; x++) {
                grid[y,x] = 0;
            }
        }

        string plr1Name, plr2Name;
        plr1Name = GetPlayerName(1, 'X');
        plr2Name = GetPlayerName(2, 'O');

        while (gameLoop) {
            GetPlayerInput(plr1Name, 'X', 1);
            GetPlayerInput(plr2Name, 'O', 2);
        }
        
        Console.ReadKey();
    }
}

