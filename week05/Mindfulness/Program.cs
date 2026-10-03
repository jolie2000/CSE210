using System;

public class Program
{
    public static void Main(string[] args)
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness Program");
            Console.WriteLine("1. Start breathing activity");
            Console.WriteLine("2. Start reflection activity");
            Console.WriteLine("3. Start listing activity");
            Console.WriteLine("4. Quit");
            Console.Write("Select a choice from the menu: ");
            string choice = Console.ReadLine();

            Activity activity = null;
            switch (choice)
            {
                case "1":
                    activity = new BreathingActivity();
                    break;
                case "2":
                    activity = new ReflectionActivity();
                    break;
                case "3":
                    activity = new ListingActivity();
                    break;
                case "4":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Please choose 1, 2, 3, or 4.");
                    Console.WriteLine("Press Enter to return to the menu.");
                    Console.ReadLine();
                    break;
            }

            if (activity != null)
            {
                activity.Run();
                Console.WriteLine();
                Console.WriteLine("Press Enter to return to the menu.");
                Console.ReadLine();
            }
        }

        Console.WriteLine("Thank you for taking time to be mindful.");
    }

    // Creativity: each prompt and reflection-question deck is shuffled without repeats until that deck has been fully used.
    // Timed entry and elapsed-time countdowns also keep the activities close to the duration selected by the user.
}
