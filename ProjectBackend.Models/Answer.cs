using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBackend.Models
{
    public class Answer
    {
        public Guid ID { get; set; } = Guid.NewGuid();

        public string Label { get; set; }

        public bool? CorrectAnswer { get; set; }

        public Guid QuestionID { get; set; }
        public Question Question { get; set; }

        public ICollection<UserAnswer> UserAnswers { get; set; }
    }
}
