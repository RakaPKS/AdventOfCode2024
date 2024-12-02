const string fileName = "input";
var reports = ParseInput(fileName);

var pureAmount = reports.Count(IsValidReport);
Console.WriteLine($"Valid Reports: {pureAmount}");

var boostedAmount = reports.Count(IsValidReportWithDampener);
Console.WriteLine($"Valid Reports with Problem Dampener: {boostedAmount}");

return;

static List<List<int>> ParseInput(string fileName)
{
    return File.ReadLines(fileName).Select(line => line.Split(' ').Select(int.Parse).ToList()).ToList();
}

static bool IsValidReport(List<int> report)
{
    var isIncreasing = report[1] > report[0];

    return report.Zip(report.Skip(1), (curr, next) => (curr, next, diff: next - curr))
        .All(pair =>
            pair.next > pair.curr == isIncreasing &&
            Math.Abs(pair.diff) is >= 1 and <= 3);
}

static bool IsValidReportWithDampener(List<int> report)
{
    if (IsValidReport(report)) return true;
    return report.Select((_, i) => report.Take(i).Concat(report.Skip(i + 1)).ToList())
        .Any(IsValidReport);
}