
public record Level(int Value)
{
    public bool IsInRangeOf(Level level)
    {
        return Math.Abs(Value - level.Value) switch
        {
            >= 1 and <= 3 => true,
            _ => false
        };
    }
};

public interface ILevelsAnalyser
{
    bool IsSafe(IList<Level> levels);
}

public class AscendingDescendingLevelAnalyser : ILevelsAnalyser
{
    public virtual bool IsSafe(IList<Level> levels)
    {
        var isAscending = levels[0].Value < levels[1].Value;
        var current = levels.First();
        foreach (var level in levels.Skip(1))
        {
            if (current.IsInRangeOf(level))
            {
                if (isAscending && level.Value > current.Value)
                {
                    current = level;
                    continue;
                }
                if (!isAscending && level.Value < current.Value)
                {
                    current = level;
                    continue;
                }
                return false;
            }
            else
            {
                return false;
            }
        }
        return true;
    }
}

public class Solved : ILevelsAnalyser
{
    public virtual bool IsSafe(IList<Level> levels)
    {
        var deltas = levels.Zip(levels.Skip(1), (a, b) => a.Value - b.Value);

        return deltas.All(delta => delta >= 1 && delta <= 3) || deltas.All(delta => delta <= -1 && delta >= -3);
    }
}

public class Solved2 : Solved
{
    public override bool IsSafe(IList<Level> levels)
    {
        return Enumerable.Range(0, levels.ToArray().Length)
        .Any(indexToRemove =>
            base.IsSafe(levels.Where((_, index) => index != indexToRemove).ToList()));
    }
}

public class ProblemDampnerLevelAnalyser : AscendingDescendingLevelAnalyser
{
    public override bool IsSafe(IList<Level> levels)
    {
        var isSafe = base.IsSafe(levels);
        if (!isSafe)
        {
            foreach (var level in levels)
            {
                var alteredLevels = levels.ToList();
                alteredLevels.RemoveAt(levels.IndexOf(level));
                //alteredLevels.Remove(level);
                if (base.IsSafe(alteredLevels))
                {
                    return true;
                }
            }
        }
        return isSafe;
    }
}

public record Report(List<Level> Levels)
{
    public bool IsSafe(ILevelsAnalyser analyser)
    {
        return analyser.IsSafe(Levels);
    }

    public bool IsAscending => Levels[0].Value < Levels[1].Value;
    public bool IsDescending => Levels[0].Value > Levels[1].Value;
}


public static class ReportLoader
{
    public static IEnumerable<Report> LoadReports(string fileName)
    {
        foreach (var line in File.ReadLines(fileName))
        {
            var levels = line.Split(" ").Select(v => new Level(int.Parse(v))).ToList();
            var report = new Report(levels);
            yield return report;
        }
    }
}
