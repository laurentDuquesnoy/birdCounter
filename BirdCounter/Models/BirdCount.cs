namespace BirdCounter.Models
{
    public class BirdCount
    {
        public int Id { get; set; }
        public int BirdId { get; set; }
        public int SessionId { get; set; }
        public int Count { get; set; }
    }
}