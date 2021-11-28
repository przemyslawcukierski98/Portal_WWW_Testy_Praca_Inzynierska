using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTesty_Library.Models
{
    public class StudentTestSolution
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }

        public Guid ExamId { get; set; }
        public string ExamTitle { get; set; }
        public string StudentEmail { get; set; }
        public string LecturerEmail { get; set; }
        public string StudentAnswersCode { get; set; }
        public bool IsEvaluated { get; set; }
    }
}
