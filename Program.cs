
void DayOne()
{
    var (leftList, rightList) = NotesFinder.FindNotes("locations-input.txt");

    Console.WriteLine($"Total Distance: {leftList.TotalDistanceFrom(rightList)}");
    Console.WriteLine($"Similarity Score: {leftList.CalculateTheSimilarityScore(rightList)}");
}

DayOne();
