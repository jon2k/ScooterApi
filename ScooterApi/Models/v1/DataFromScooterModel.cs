using System;
using System.ComponentModel.DataAnnotations;

namespace ScooterApi.Models.v1
{
    public class Coordinate
    {
        public float X { get; set; }
        public float Y { get; set; }
    }
    public class DataFromScooterModel
    {
        [Required]
        public int ScooterId { get; set; }
        [Required] 
        public bool InUse { get; set; }
        [Required] 
        public byte ChargePercent { get; set; }
        [Required] 
        public Coordinate Coordinate { get; set; } 
        [Required]
        public DateTime Time { get; set; }
    }
}