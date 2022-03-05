using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BRTF_Booking.Models
{
    public class Room
    {
        public int ID { get; set; }

        [Display(Name = "Area")]
        public Area Area { get; set; }
        public int AreaID { get; set; }

        [Display(Name = "Room")]
        [Required(ErrorMessage = "Room name is required!")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [StringLength(2000, ErrorMessage = "Description cannot be more than 2000 characters long!")]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        public string? Rule { get; set; }

        [Range(1, 99, ErrorMessage = "Room limit between 1 and 99 only!")]
        public int? Limit { get; set; }

        public int? MaxNumofBookings { get; set; }

        public bool Enabled { get; set; }

        public ICollection<Booking> Bookings { get; set; }

        public Room()
        {
            this.Bookings = new HashSet<Booking>();
        }
    }
}
