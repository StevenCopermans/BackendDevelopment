using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBackend.Models
{
    public class UserAnswer
    {
        public Guid ID { get; set; }
        public string UserID { get; set; }
        public Guid AnswerID { get; set; }
        public Answer Answer { get; set; }
    }
}
