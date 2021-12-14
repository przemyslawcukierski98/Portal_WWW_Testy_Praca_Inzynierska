using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTesty_Library.Models
{
    public class LecturerProfileDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        [Required(ErrorMessage = "Imię jest polem wymaganym")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Nazwisko jest polem wymaganym")]
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
    }
}
