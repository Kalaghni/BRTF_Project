using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BRFT_Booking.Models
{
    public class Booking
    {
        public int ID { get; set; }

        [Display(Name = "User")]
        [Required(ErrorMessage = "You must select a user.")]
        public int UserID { get; set; }
        public User User { get; set; }

        [Display(Name = "Room")]
        [Required(ErrorMessage = "You must select a room.")]
        public int RoomID { get; set; }
        public Room Room { get; set; }
    }
}
