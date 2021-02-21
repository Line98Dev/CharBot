using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineBot.Models
{
    class AdamSaying
    {
        private string Saying { get; }
        private TimeSpan Timestamp { get; }

        public AdamSaying(string saying, TimeSpan timestamp)
        {
            Saying = saying;
            this.Timestamp = timestamp;
        }
    }
}
