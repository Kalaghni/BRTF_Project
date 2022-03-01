using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BRTF_Booking.Models
{
    public class Booking : IValidatableObject
    {
        public int ID { get; set; }

        [Display(Name = "User")]
        public int UserID { get; set; }
        public User User { get; set; }

        [Display(Name = "Room")]
        public int RoomID { get; set; }
        public Room Room { get; set; }

        [Required(ErrorMessage = "You must enter a day.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Requested")]
        public DateTime BookingRequested { get; set; }


        [Required(ErrorMessage = "You must enter a Start date.")]
        [Display(Name = "Start")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "You must enter a End date.")]
        [Display(Name = "End")]
        public DateTime EndDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (this.StartDate > this.EndDate)
                {
                yield return new ValidationResult("The Start time cannot be after the end time.", new[] { "StartTime" });
            }
        }
    }
}
