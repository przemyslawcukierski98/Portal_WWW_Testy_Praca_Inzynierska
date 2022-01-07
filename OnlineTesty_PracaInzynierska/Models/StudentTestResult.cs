using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTesty_Library.Models
{
    public class StudentTestResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }

        public Guid ExamId { get; set; }
        public string StudentEmail { get; set; }
        public string LecturerEmail { get; set; }
        public string ExamTitle { get; set; }

        [Required(ErrorMessage = "Podaj liczbę punktów zdobytych przez studenta")]
        public int StudentNumberOfPoints { get; set; }
        public int ExamNumberOfPoints { get; set; }
        public string Evaluation { get; set; }
    }
}
