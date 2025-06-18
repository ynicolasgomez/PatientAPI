
using System.ComponentModel.DataAnnotations;

namespace PatientAPI.Models
{
    public class Exam
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [MaxLength(255)]
        public string Descripcion { get; set; }
    }
}
