using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineBot.Models
{
    class AdamSaying
    {
        public string Saying { get; }
        public DateTime Timestamp { get; }

        public AdamSaying(string saying, DateTime timestamp)
        {
            Saying = saying;
            this.Timestamp = timestamp;
        }
    }
}
