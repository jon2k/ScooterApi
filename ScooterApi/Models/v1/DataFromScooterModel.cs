using System;
using System.ComponentModel.DataAnnotations;
using ScooterApi.Domain.Entities;

namespace ScooterApi.Models.v1
{
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