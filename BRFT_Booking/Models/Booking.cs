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
        public DateTime? BookingRequested { get; set; }

        [Required(ErrorMessage = "You must enter a Start Time.")]
        [DataType(DataType.Time)]
        [Display(Name = "Start Time")]
        public DateTime? StartTime { get; set; }

        [Required(ErrorMessage = "You must enter a End Time.")]
        [DataType(DataType.Time)]
        [Display(Name = "End Time")]
        public DateTime? EndTime { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (BookingRequested.GetValueOrDefault() < DateTime.Today)
            {
                yield return new ValidationResult("Cannot book dates from the past.", new[] { "BookingRequested" });
            }

            if (StartTime.GetValueOrDefault() > EndTime.GetValueOrDefault())
                {
                yield return new ValidationResult("The Start time cannot be after the end time.", new[] { "StartTime" });
            }
        }
    }
}
