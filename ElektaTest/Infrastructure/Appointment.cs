using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElektaTest.Infrastructure
{
    [Table("Appointment")]
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public Guid PatientId { get; set; }
        public DateTime AppointmentTime { get; set; }

        public int EequipmentId { get; set; }

        public bool Canceled { get; set; }
    }
}
