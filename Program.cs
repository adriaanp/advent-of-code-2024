void DayOne()
{
    var (leftList, rightList) = NotesFinder.FindNotes("locations-input.txt");

    Console.WriteLine($"Total Distance: {leftList.TotalDistanceFrom(rightList)}");
    Console.WriteLine($"Similarity Score: {leftList.CalculateTheSimilarityScore(rightList)}");
}

void DayTwo()
{
    var reports = ReportLoader.LoadReports("two-input.txt");

    Console.WriteLine($"Safe reports: {reports.Count(r => r.IsSafe(new AscendingDescendingLevelAnalyser()))}");
    Console.WriteLine($"Safe reports (with problem dampner): {reports.Count(r => r.IsSafe(new ProblemDampnerLevelAnalyser()))}");
    Console.WriteLine($"Safe reports: {reports.Count(r => r.IsSafe(new Solved()))}");
    Console.WriteLine($"Safe reports (with problem dampner): {reports.Count(r => r.IsSafe(new Solved2()))}");

    var solved = reports.Where(r => r.IsSafe(new Solved2())).Select(m => string.Join(",", m.Levels.Select(l => l.Value)));
    var mine = reports.Where(r => r.IsSafe(new ProblemDampnerLevelAnalyser())).Select(m => string.Join(",", m.Levels.Select(l => l.Value)));

    foreach(var report in solved.Except(mine))
    {
        Console.WriteLine(report);
    }
}

Dictionary<int, Action> AdventOfCode = new() { { 1, DayOne }, { 2, DayTwo } };

var CurrentDay = 2;
Console.WriteLine($"Advent of Code day {CurrentDay}");
Console.WriteLine("==================");
AdventOfCode[CurrentDay]();

