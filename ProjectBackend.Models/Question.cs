using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBackend.Models
{
    public class Question
    {
        public Guid ID { get; set; } = Guid.NewGuid();

        public string Label { get; set; }

        #nullable enable   
        public string? Image { get; set; }
        #nullable disable

        public Guid QuizID{ get; set; }
        public Quiz Quiz { get; set; }

        public ICollection<Answer> Answers { get; set; }
    }
}
