using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElektaTest.Domain
{
    [Table("Appointment")]
    public class Appointment
    {
        [Key]
        public Guid PatientId { get; set; }
        public DateTime AppointmentTime { get; set; }

        public int EequipmentId { get; set; }

        public bool Canceled { get; set; }
    }
}
