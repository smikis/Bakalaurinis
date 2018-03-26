﻿namespace TinkloProblemos.API.Contracts
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public int ProblemId { get; set; }

        public Problem Problem { get; set; }
        public User User { get; set; }
    }
}
