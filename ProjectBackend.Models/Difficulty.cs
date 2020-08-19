using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBackend.Models
{
    public class Difficulty
    {
        public Guid ID { get; set; } = Guid.NewGuid();

        public string Label { get; set; }

        public ICollection<Quiz> Quizes { get; set; }
    }
}
