using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BRTF_Booking.Models
{
    public class Term
    {
        public int ID { get; set; }
        [Display(Name = "Start of Term")]
        public DateTime Start { get; set; }
        [Display(Name = "End of Term")]
        public DateTime End { get; set; }

    }
}
