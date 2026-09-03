using System;
using System.Collections.Generic;

List<int> numbers = new List<int>();

int number;

do
{
    Console.Write("Enter number: ");
    number = int.Parse(Console.ReadLine());

    if (number != 0)
    {
        numbers.Add(number);
    }
} while (number != 0);

if (numbers.Count == 0)
{
    Console.WriteLine("The sum is: 0");
    Console.WriteLine("The average is: 0");
    Console.WriteLine("The largest number is: 0");
    return;
}

int sum = 0;
int largest = numbers[0];

foreach (int value in numbers)
{
    sum += value;

    if (value > largest)
    {
        largest = value;
    }
}

double average = (double)sum / numbers.Count;

Console.WriteLine($"The sum is: {sum}");
Console.WriteLine($"The average is: {average}");
Console.WriteLine($"The largest number is: {largest}");
