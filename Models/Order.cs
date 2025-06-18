
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientAPI.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public DateTime FechaOrden { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
    }
}
