using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRTF_Booking.ViewModels
{
    public class SettingsViewModel
    {
        public int ID { get; set; }

        public string OfficeStartHours { get; set; }
        public string OfficeEndHours { get; set; }
        public string EmailExtension { get; set; }
        public string TermStart { get; set; }
        public string TermEnd { get; set; }
    }
}
