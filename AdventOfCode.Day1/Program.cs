const string pathname = "input.txt";

var (leftList, rightList) = ParseInput(pathname);

var distance = leftList.Zip(rightList, (left, right) => Math.Abs(left - right)).Sum();
Console.WriteLine($"Distance: {distance}");
return;

(List<int> left, List<int> right) ParseInput(string filename)
{
        var left = new List<int>();
        var right = new List<int>();
        
        foreach(var line in File.ReadLines(filename))
        {
                var numbers = line.Split([' '], StringSplitOptions.RemoveEmptyEntries);
                left.Add(int.Parse(numbers[0]));
                right.Add(int.Parse(numbers[1]));
        }
        
        left.Sort();
        right.Sort();
        
        return (left, right);
}
