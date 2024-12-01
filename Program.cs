
void DayOne()
{
    var office = new Office();
    var (listOne, listTwo) = NotesFinder.FindNotes("locations-input.txt");
    office.FoundLists(listOne, listTwo);

    Console.WriteLine($"Total Distance: {office.FindTotalDistance()}");
    Console.WriteLine($"Similarity Score: {office.CalculateTheSimilarityScore()}");
}

DayOne();
