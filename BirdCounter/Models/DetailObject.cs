using System.Collections.Generic;

namespace BirdCounter.Models
{
    public class DetailObject
    {
        public Session session { get; set; }
        public List<BirdSession> birds { get; set; }
    }
}