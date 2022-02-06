using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BRFT_Booking.Models
{
    public class Area
    {
        public int ID { get; set; }

        [Display(Name="Area")]
        public string Name { get; set; }
    }
}
