using System;
using System.Collections.Generic;
using YouTubeVideos;

class Program
{
    static void Main()
    {
        // Step 1: create 3-4 videos with realistic values
        var v1 = new Video("Product Guide A", "TeamA", 320);
        var v2 = new Video("Demos Bii", "TeamB", 285);
        var v3 = new Video("Tutorial C", "TeamC", 410);

        // Step 2: add 3-4 comments to each video
        v1.AddComment(new Comment("Ana", "Very useful, thanks!"));
        v1.AddComment(new Comment("Luis", "Could you expand on function X?"));
        v1.AddComment(new Comment("Maria", "Is there a Spanish version?"));

        v2.AddComment(new Comment("Carlos", "Excellent demonstration."));
        v2.AddComment(new Comment("Fernanda", "What about compatibility?"));
        v2.AddComment(new Comment("Diego", "Looking forward to more examples."));

        v3.AddComment(new Comment("Sofia", "Crystal clear, well explained."));
        v3.AddComment(new Comment("Miguel", "Can this be used with .NET 6?"));
        v3.AddComment(new Comment("Roberto", "Missed a initialization step."));

        // Step 3: put the videos in a list
        var videos = new List<Video> { v1, v2, v3 };

        // Step 4: iterate and display information for each video
        foreach (var video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Duration: {video.DurationSeconds} seconds");
            Console.WriteLine($"Number of comments: {video.GetCommentCount()}");
            Console.WriteLine("Comments:");
            foreach (var c in video.Comments)
            {
                Console.WriteLine($"- {c.Author}: {c.Text}");
            }
            Console.WriteLine(); // spacer between videos
        }
    }
}