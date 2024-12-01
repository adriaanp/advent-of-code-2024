public class Office
{
    private List<Location> _groupOneList = new();
    private List<Location> _groupTwoList = new();

    public Office()
    {
    }

    public void FoundLists(List<Location> groupOneList, List<Location> groupTwoList)
    {
        _groupOneList = groupOneList;
        _groupTwoList = groupTwoList;
    }

    public int FindTotalDistance()
    {
      var listOne = _groupOneList
          .Order(new LocationComparer())
          .ToList();
      var listTwo = _groupTwoList
          .Order(new LocationComparer())
          .ToList();

      int total = 0;
      foreach(var location in listOne)
      {
          total += location.DistanceFrom(listTwo.ElementAt(listOne.IndexOf(location)));
      }

      return total;
    }
}

public class NotesFinder
{
    public static (List<Location> GroupOneList, List<Location> GroupTwoList) FindNotes(string fileName)
    {
        var listOne = new List<Location>();
        var listTwo = new List<Location>();

        foreach (var line in File.ReadLines(fileName, System.Text.Encoding.UTF8))
        {
            var split = line.Split("   ");
            listOne.Add(new Location(int.Parse(split[0])));
            listTwo.Add(new Location(int.Parse(split[1])));
        }

        return (listOne, listTwo);
    }
}

public class LocationComparer : Comparer<Location>
{
    public override int Compare(Location? x, Location? y)
    {
        if (x is null && y is not null) return -1;
        if (x is not null && y is null) return 1;
        if (x is null && y is null) return 0;
        return x!.Id.CompareTo(y!.Id);
    }
}

public record Location(int Id)
{
    public int DistanceFrom(Location location)
    {
        return Math.Abs(Id - location.Id);
    }
};

