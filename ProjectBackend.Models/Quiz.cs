using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ProjectBackend.Models
{
    public class Quiz
    {
        [Required(ErrorMessage = "ID verplicht!")]
        public Guid ID { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Label verplicht!")]
        public string Label { get; set; }

        #nullable enable
        public string? Description { get; set; }
#nullable disable

        [Required(ErrorMessage = "Difficulty verplicht!")]
        public Guid DifficultyID { get; set; }

        [Required(ErrorMessage = "Difficulty verplicht!")]
        public Difficulty Difficulty { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
