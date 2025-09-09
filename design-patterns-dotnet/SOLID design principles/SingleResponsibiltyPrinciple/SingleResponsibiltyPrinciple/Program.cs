using System.Diagnostics;

namespace SingleRepositoryPrinciple
{
    /// <summary>
    /// Journal Class is responsible ONLY for handling the entries
    /// </summary>
    public class Journal
    {
        private readonly List<string> entries = new();

        private static int count = 0;

        public int AddEntry(string entry)
        {
            entries.Add($"{++count}: {entry}");
            return count; // Memento
        }

        public void RemoveEntry(int index)
        {
            entries.RemoveAt(index);
            count--;
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }
    }

    /// <summary>
    /// Persistence class is responsible ONLY for handling persistence of the journal
    /// </summary>
    public class Persistence
    {

        public void SaveToFile(Journal journal, string fileName, bool overwrite = false)
        {
            if (overwrite || !File.Exists(fileName))
            {
                File.WriteAllText(fileName, journal.ToString());
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var journal = new Journal();
            journal.AddEntry("I cried today :(");
            journal.AddEntry("I ate a bug.");
            Console.WriteLine(journal);

            var persistence = new Persistence();
            string fileName = @"c:\temp\journal.txt";
            persistence.SaveToFile(journal, fileName, true);
            Process.Start("notepad.exe", fileName); 
        }
    }
}