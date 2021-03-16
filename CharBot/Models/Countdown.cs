using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharBot.Models
{
    static class Countdown
    {
        public static TimeSpan CommencementCountDown()
        {
            var graduation = new DateTime(2021, 05, 08, 21, 0, 0);
            var now = DateTime.Now;
            var difference = graduation - now;
            return difference;
        }
    }
}
