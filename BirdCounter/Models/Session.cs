using System;

namespace BirdCounter.Models
{
    public class Session
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}