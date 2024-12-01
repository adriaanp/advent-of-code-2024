
void DayOne()
{
    var office = new Office();
    var (listOne, listTwo) = NotesFinder.FindNotes("locations-input.txt");
    Console.WriteLine(listOne.Count);
    Console.WriteLine(listTwo.Count);
    office.FoundLists(listOne, listTwo);

    Console.WriteLine($"Total Distance: {office.FindTotalDistance()}");
}

DayOne();
