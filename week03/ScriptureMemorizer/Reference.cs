// Represents the location of a scripture, such as John 3:16 or Proverbs 3:5-6.
public class Reference
{
    // Private fields keep the details of the reference encapsulated in this class.
    private string _book;
    private int _chapter;
    private int _startVerse;
    private int _endVerse;

    public Reference(string book, int chapter, int verse)
    {
        // A single-verse reference has the same starting and ending verse.
        _book = book;
        _chapter = chapter;
        _startVerse = verse;
        _endVerse = verse;
    }

    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        // This constructor supports a range of verses.
        _book = book;
        _chapter = chapter;
        _startVerse = startVerse;
        _endVerse = endVerse;
    }

    public override string ToString()
    {
        // Build the verse portion differently for a single verse versus a range.
        string verseText = _startVerse.ToString();

        if (_startVerse != _endVerse)
        {
            verseText += $"-{_endVerse}";
        }

        return $"{_book} {_chapter}:{verseText}";
    }
}
