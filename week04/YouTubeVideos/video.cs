using System.Collections.Generic;

namespace YouTubeVideos
{
    public class Video
    {
        public string Title { get; }
        public string Author { get; }
        public int DurationSeconds { get; }
        public List<Comment> Comments { get; } = new List<Comment>();

        public Video(string title, string author, int durationSeconds)
        {
            Title = title;
            Author = author;
            DurationSeconds = durationSeconds;
        }

        public int GetCommentCount()
        {
            return Comments.Count;
        }

        public void AddComment(Comment c)
        {
            Comments.Add(c);
        }
    }
}