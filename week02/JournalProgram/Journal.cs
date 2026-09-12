using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class Journal
{
    public List<Entry> _entries = new List<Entry>();

    public void AddEntry(Entry newEntry)
    {
        _entries.Add(newEntry);
    }

    public void DisplayAll()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("Your journal does not contain any entries yet.\n");
            return;
        }

        foreach (Entry entry in _entries)
        {
            entry.Display();
        }
    }

    public string EscapeCsvField(string field)
    {
        string safeField = field ?? string.Empty;
        return $"\"{safeField.Replace("\"", "\"\"")}\"";
    }

    public void SaveToFile(string file)
    {
        using (StreamWriter outputFile = new StreamWriter(file))
        {
            foreach (Entry entry in _entries)
            {
                string csvLine = string.Join(",",
                    EscapeCsvField(entry._date),
                    EscapeCsvField(entry._promptText),
                    EscapeCsvField(entry._entryText),
                    EscapeCsvField(entry._wordCount.ToString()));

                outputFile.WriteLine(csvLine);
            }
        }
    }

    public void LoadFromFile(string file)
    {
        string[] lines = File.ReadAllLines(file);
        List<Entry> loadedEntries = new List<Entry>();

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            List<string> fields = ParseCsvLine(line);
            if (fields.Count != 4)
            {
                throw new FormatException("The selected file contains an invalid journal entry.");
            }

            if (!int.TryParse(fields[3], out int wordCount))
            {
                throw new FormatException("The selected file contains an invalid word count.");
            }

            Entry entry = new Entry
            {
                _date = fields[0],
                _promptText = fields[1],
                _entryText = fields[2],
                _wordCount = wordCount
            };

            loadedEntries.Add(entry);
        }

        _entries = loadedEntries;
    }

    private static List<string> ParseCsvLine(string line)
    {
        List<string> fields = new List<string>();
        StringBuilder field = new StringBuilder();
        bool insideQuotes = false;

        for (int index = 0; index < line.Length; index++)
        {
            char currentCharacter = line[index];

            if (currentCharacter == '\"')
            {
                if (insideQuotes && index + 1 < line.Length && line[index + 1] == '\"')
                {
                    field.Append('\"');
                    index++;
                }
                else
                {
                    insideQuotes = !insideQuotes;
                }
            }
            else if (currentCharacter == ',' && !insideQuotes)
            {
                fields.Add(field.ToString());
                field.Clear();
            }
            else
            {
                field.Append(currentCharacter);
            }
        }

        if (insideQuotes)
        {
            throw new FormatException("The selected file has unmatched quotation marks.");
        }

        fields.Add(field.ToString());
        return fields;
    }
}
