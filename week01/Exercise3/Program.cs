using System;

Random randomGenerator = new Random();
int magicNumber = randomGenerator.Next(1, 101);
int guess = 0;

while (guess != magicNumber)
{
    Console.Write("What is your guess? ");
    guess = int.Parse(Console.ReadLine());

    if (guess == 0)
    {
        break;
    }
    else if (guess < magicNumber)
    {
        Console.WriteLine("Higher");
    }
    else if (guess > magicNumber)
    {
        Console.WriteLine("Lower");
    }
    else
    {
        Console.WriteLine("You guessed it!");
    }
}
