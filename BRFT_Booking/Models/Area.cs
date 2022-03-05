using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BRTF_Booking.Models
{
    public class Area
    {
        public int ID { get; set; }

        [Display(Name="Area")]
        [Required(ErrorMessage = "Name of an area is required!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "First name need to be between 2 and 100 characters!")]
        public string Name { get; set; }
    }
}
