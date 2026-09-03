using System;

Console.Write("What is your grade percentage? ");
int percentage = int.Parse(Console.ReadLine());

string letter;

if (percentage >= 90)
{
    letter = "A";
}
else if (percentage >= 80)
{
    letter = "B";
}
else if (percentage >= 70)
{
    letter = "C";
}
else if (percentage >= 60)
{
    letter = "D";
}
else
{
    letter = "F";
}

Console.WriteLine(letter);

if (percentage >= 70)
{
    Console.WriteLine("Congratulations! You passed the course!");
}
else
{
    Console.WriteLine("Better luck next time.");
}
