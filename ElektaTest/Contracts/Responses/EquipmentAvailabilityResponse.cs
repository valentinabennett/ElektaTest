using System;

namespace ElektaTest.Contracts.Responses
{
    public class EquipmentAvailabilityResponse
    {
        public int EequipmentId { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime Date { get; set; }
    }
}
