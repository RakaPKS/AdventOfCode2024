const string fileName = "input";
var grid = ReadGrid(fileName);
var xMasOccurrences = CountOccurrences(grid);
var crossMasOccurrences = CountWordInXShapeOccurrences(grid);

Console.WriteLine($"Occurrences XMAS: {xMasOccurrences}");
Console.WriteLine($"Occurrences CrossMas: {crossMasOccurrences}");
return;

static int CountOccurrences(char[,] grid, string word = "XMAS")
{
    var (rows, cols) = (grid.GetLength(0), grid.GetLength(1));
    var directions = new[]
    {
        (0, 1),
        (1, 0),
        (1, 1),
        (-1, 1),
        (-1, 0),
        (0, -1),
        (-1, -1),
        (1, -1)
    };

    return Enumerable.Range(0, rows)
        .SelectMany(r => Enumerable.Range(0, cols)
            .SelectMany(c => directions
                .Where(d => CanMakeWord(r, c, d.Item1, d.Item2))
                .Select(_ => 1)))
        .Count();

    bool CanMakeWord(int r, int c, int dr, int dc)
    {
        return Enumerable.Range(0, word.Length)
            .All(i => IsInBounds(r + i * dr, c + i * dc, rows, cols) &&
                      grid[r + i * dr, c + i * dc] == word[i]);
    }
}

static int CountWordInXShapeOccurrences(char[,] grid, string word = "MAS")
{
    var (rows, cols) = (grid.GetLength(0), grid.GetLength(1));
    var diagonalPairs = new[]
    {
        ((1, 1), (-1, 1))
    };

    return Enumerable.Range(1, rows - 2)
        .SelectMany(r => Enumerable.Range(1, cols - 2)
            .SelectMany(c => diagonalPairs
                .Where(pair => IsMasPattern(r, c, pair.Item1, pair.Item2))
                .Select(_ => 1)))
        .Count();

    bool IsMasPattern(int centerR, int centerC, (int dr, int dc) dir1, (int dr, int dc) dir2)
    {
        return grid[centerR, centerC] == 'A' &&
               ((IsMasInDirection(centerR, centerC, dir1) && IsMasInDirection(centerR, centerC, dir2)) || // MAS/MAS
                (IsMasInDirection(centerR, centerC, dir1) && IsSamInDirection(centerR, centerC, dir2)) || // MAS/SAM
                (IsSamInDirection(centerR, centerC, dir1) && IsMasInDirection(centerR, centerC, dir2)) || // SAM/MAS
                (IsSamInDirection(centerR, centerC, dir1) && IsSamInDirection(centerR, centerC, dir2))); // SAM/SAM
    }

    bool IsMasInDirection(int r, int c, (int dr, int dc) dir)
    {
        var (dr, dc) = dir;
        return IsInBounds(r - dr, c - dc, rows, cols) && grid[r - dr, c - dc] == 'M' &&
               IsInBounds(r + dr, c + dc, rows, cols) && grid[r + dr, c + dc] == 'S';
    }

    bool IsSamInDirection(int r, int c, (int dr, int dc) dir)
    {
        var (dr, dc) = dir;
        return IsInBounds(r - dr, c - dc, rows, cols) && grid[r - dr, c - dc] == 'S' &&
               IsInBounds(r + dr, c + dc, rows, cols) && grid[r + dr, c + dc] == 'M';
    }
}

static bool IsInBounds(int r, int c, int rows, int cols)
{
    return r >= 0 && r < rows && c >= 0 && c < cols;
}

static char[,] ReadGrid(string filePath)
{
    var lines = File.ReadAllLines(filePath);
    var grid = new char[lines.Length, lines[0].Length];

    for (var i = 0; i < lines.Length; i++)
    for (var j = 0; j < lines[i].Length; j++)
        grid[i, j] = lines[i][j];

    return grid;
}