using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRTF_Booking.ViewModels
{
    public class EventViewModel
    {
        public Int64 id { get; set; }

        public String title { get; set; }

        public String start { get; set; }

        public String end { get; set; }

        public String url { get; set; }
        public String backgroundColor { get; set; }
        public String borderColor { get; set; }

        public bool allDay { get; set; }
    }
}
