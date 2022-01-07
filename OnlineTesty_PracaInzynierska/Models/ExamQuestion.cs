using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTesty_Library.Models
{
    public class ExamQuestion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        [Required(ErrorMessage = "Numer pytania jest polem wymaganym")]
        public int Order { get; set; }
        [Required(ErrorMessage = "Treść pytania jest polem wymaganym")]
        public string Question { get; set; }
        [Required(ErrorMessage = "Dodatkowy opis pytania jest polem wymaganym")]
        public string AdditiondalDescription { get; set; }
        [Required(ErrorMessage = "Wariant odpowiedzi A jest polem wymaganym")]
        public string AnswerA { get; set; }
        [Required(ErrorMessage = "Wariant odpowiedzi B jest polem wymaganym")]
        public string AnswerB { get; set; }
        [Required(ErrorMessage = "Wariant odpowiedzi C jest polem wymaganym")]
        public string AnswerC { get; set; }
        [Required(ErrorMessage = "Wariant odpowiedzi D jest polem wymaganym")]
        public string AnswerD { get; set; }
        public string CorrectAnswerChar { get; set; }

        public Guid ExamID { get; set; }
    }
}
