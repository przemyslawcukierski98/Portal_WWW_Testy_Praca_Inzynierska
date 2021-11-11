using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTesty_Library.Models
{
    public class Exam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        public string UserEmail { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string StudentGroupName { get; set; }

        [Required]
        public string ExamStatus { get; set; } 
    }
}
