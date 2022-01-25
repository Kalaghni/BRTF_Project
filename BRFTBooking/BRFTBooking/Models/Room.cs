using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BRFTBooking.Models
{
    public class Room
    {
        public Room()
        {
            this.Bookings = new HashSet<Booking>();
        }

        public int ID { get; set; }

        [Display(Name="Room")]
        
        public string Area { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public int? Limit { get; set; }

        public bool Enabled { get; set; }

        public ICollection<Booking> Bookings { get; set; }

    }
}
