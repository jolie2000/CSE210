using System;
using System.Diagnostics;

public class BreathingActivity : Activity
{
    public BreathingActivity()
        : base("Breathing Activity", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    protected override void RunActivity()
    {
        Stopwatch activityTimer = Stopwatch.StartNew();
        bool breatheIn = true;
        while (activityTimer.Elapsed.TotalSeconds < DurationSeconds)
        {
            Console.Write(breatheIn ? "Breathe in... " : "Breathe out... ");
            int remainingSeconds = Math.Min(3, Math.Max(1, DurationSeconds - (int)activityTimer.Elapsed.TotalSeconds));
            ShowTimedCountdown(remainingSeconds, activityTimer);
            Console.WriteLine();
            breatheIn = !breatheIn;
        }
    }
}
