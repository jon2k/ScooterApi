using System;

namespace ScooterApi.Domain.Entities
{
    public class Scooter
    {
        public int Id { get; set; }
        public int ScooterId { get; set; }
        public bool InUse { get; set; }
        public byte ChargePercent { get; set; }
        public Coordinate Coordinate { get; set; }
        public DateTime Time { get; set; }
    }
}
