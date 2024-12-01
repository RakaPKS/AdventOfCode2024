const string fileName = "input.txt";
var (left, right) = ParseInput(fileName);
ComputeDistance(left, right);
ComputeSimilarity(left, right);
return;

static void ComputeDistance(List<int> leftList, List<int> rightList)
{
        var distance = leftList.Zip(rightList, (left, right) => Math.Abs(left - right)).Sum();
        Console.WriteLine($"Distance: {distance}");
}

static void ComputeSimilarity(List<int> leftList, List<int> rightList)
{
        var frequencyDictionary = rightList.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
        var similarity = leftList.Sum(num => num * frequencyDictionary.GetValueOrDefault(num,0));
        Console.WriteLine($"Similarity: {similarity}");
}

static (List<int> leftList, List<int> rightList) ParseInput(string fileName)
{
        var leftList = new List<int>();
        var rightList = new List<int>();
        
        foreach(var line in File.ReadLines(fileName))
        {
                var numbers = line.Split([' '], StringSplitOptions.RemoveEmptyEntries);
                leftList.Add(int.Parse(numbers[0]));
                rightList.Add(int.Parse(numbers[1]));
        }
        
        leftList.Sort();
        rightList.Sort();
        
        return (leftList, rightList);
}

