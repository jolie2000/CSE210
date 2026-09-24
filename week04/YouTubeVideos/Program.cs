using System;
using System.Collections.Generic;

public class Program
{
    public static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        Video video1 = new Video("Building a Simple Wooden Desk", "Crafted at Home", 642);
        video1.AddComment(new Comment("Maya R.", "The step-by-step instructions were easy to follow."));
        video1.AddComment(new Comment("Jordan Lee", "I used this desk design in my home office."));
        video1.AddComment(new Comment("Sam K.", "What finish did you use on the tabletop?"));
        videos.Add(video1);

        Video video2 = new Video("Five Quick Weeknight Meals", "Everyday Kitchen", 518);
        video2.AddComment(new Comment("Amina", "The lentil bowl was my favorite."));
        video2.AddComment(new Comment("Chris P.", "Could you make a vegetarian version of the pasta?"));
        video2.AddComment(new Comment("Nora B.", "I made these with my family. Thanks for sharing!"));
        video2.AddComment(new Comment("Eli", "Please share more recipes like these."));
        videos.Add(video2);

        Video video3 = new Video("How to Start a Container Garden", "Green Corner", 735);
        video3.AddComment(new Comment("Taylor W.", "This helped me choose the right pots for my balcony."));
        video3.AddComment(new Comment("Dee", "How often should basil be watered?"));
        video3.AddComment(new Comment("Morgan S.", "The sunlight explanation was very helpful."));
        videos.Add(video3);

        foreach (Video video in videos)
        {
            Console.WriteLine($"Title: {video.GetTitle()}");
            Console.WriteLine($"Author: {video.GetAuthor()}");
            Console.WriteLine($"Length: {video.GetLengthSeconds()} seconds");
            Console.WriteLine($"Comments: {video.GetCommentCount()}");
            Console.WriteLine("Comment list:");

            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine($"- {comment.GetCommenterName()}: {comment.GetText()}");
            }

            Console.WriteLine();
        }
    }
}
