using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace CharBot.Models
{
    static class Countdown
    {
        private static Dictionary<string, TimeSpan> CommencementCountDownTimers()
        {
            var countdown = new Dictionary<string, TimeSpan>();
            var collegeArchitecturePlanningCollegeCommunicationInformationMediaTeachersCollege = new DateTime(2021, 05, 08, 8, 0, 0);
            var collegeBusiness = new DateTime(2021, 05, 08, 12, 0, 0);
            var collegeFineArtsCollegeHealth = new DateTime(2021, 05, 08, 16, 0, 0);
            var collegeScienceHumanities = new DateTime(2021, 05, 08, 20, 0, 0);
            var now = DateTime.Now;
            countdown.Add("R. Wayne Estopinal College of Architecture and Planning; College of Communication, Information, and Media; Teachers College", Difference(now, collegeArchitecturePlanningCollegeCommunicationInformationMediaTeachersCollege));
            countdown.Add("Miller College of Business", Difference(now, collegeBusiness));
            countdown.Add("College of Fine Arts and College of Health", Difference(now, collegeFineArtsCollegeHealth));
            countdown.Add("College of Sciences and Humanities", Difference(now, collegeScienceHumanities));
            return countdown;
        }

        private static TimeSpan Difference(DateTime now, DateTime commencement)
        {
            return commencement - now;
        }

        public static Dictionary<string, string> CommencementCountDown()
        {
            var countdowns = CommencementCountDownTimers();
            var messages = new Dictionary<string, string>();
            foreach  (var (school, countdown) in countdowns)
            {
                messages.Add(school, StringBuilder(school, countdown));
            }

            return messages;
        }

        private static string StringBuilder(string school, TimeSpan countdown)
        {
            return "There are **" + countdown.Days + "** days and **" + countdown.Hours + "** hours until the commencement ceremony begins!";
        }


    }
}
