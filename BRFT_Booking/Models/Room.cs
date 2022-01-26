using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BRFT_Booking.Models
{
    public class Room
    {
        public Room()
        {
            this.Bookings = new HashSet<Booking>();
        }

        public int ID { get; set; }

        
        public string Area { get; set; }

        [Display(Name = "Room")]
        public string Name { get; set; }

        public string? Description { get; set; }

        public int? Limit { get; set; }

        public bool Enabled { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
