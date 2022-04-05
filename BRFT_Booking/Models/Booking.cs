using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BRTF_Booking.Models
{
    public class Booking : IValidatableObject
    {
        private DateTime startDate;
        private DateTime endDate;

        public TimeSpan timeSpan => new TimeSpan(EndDate.Ticks - StartDate.Ticks);

        public int ID { get; set; }

        [Display(Name = "User")]
        public int UserID { get; set; }
        public User User { get; set; }

        [Display(Name = "Room")]
        public int RoomID { get; set; }
        public Room Room { get; set; }

        [Required(ErrorMessage = "You must enter a day!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Requested")]
        public DateTime BookingRequested { get; set; }

        [Required(ErrorMessage = "You must enter a Start date!")]
        [Display(Name = "Start Time")]
        public DateTime StartDate
        {
            get
            {
                if (StartTime == TimeSpan.Parse("0:00:00"))
                {
                    return startDate;
                }
                else
                {
                    return startDate.Date.Add(StartTime);
                }
            }
            set => startDate = value;
        }

        [Display(Name = "Start Time")]
        public TimeSpan StartTime { get; set; }

        [Required(ErrorMessage = "You must enter a End date!")]
        [Display(Name = "End Time")]
        public DateTime EndDate
        {
            get
            {
                if (EndTime == TimeSpan.Parse("0:00:00"))
                {
                    return endDate;
                }
                else
                {
                    return endDate.Date.Add(EndTime);
                }
            }
            set => endDate = value;
        }

        [Display(Name = "End Time")]
        public TimeSpan EndTime { get; set; }

        [Display(Name = "Duration (Hours)")]
        public TimeSpan Duration => EndDate - StartDate;

        [Display(Name = "Status")]
        public string Status { get; set; }

        [ScaffoldColumn(false)]
        [Timestamp]
        public Byte[] RowVersion { get; set; }//Added for concurrency

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.EndDate.Minute != 30)
            {
                if (this.EndDate.Minute != 0)
                {
                    yield return new ValidationResult("The End time must be in 30 minute increments. ex. 9:00 or 9:30", new[] { "EndDate" });
                }
            }

            if (this.StartDate.Minute != 30)
            {
                if (this.StartDate.Minute != 0)
                {
                    yield return new ValidationResult("The Start time must be in 30 minute increments. ex. 9:00 or 9:30", new[] { "StartDate" });
                }
            }

            if (this.StartDate > this.EndDate)
            {
                yield return new ValidationResult("The Start time cannot be after the end time.", new[] { "StartTime" });
            }
            if (this.StartDate < DateTime.Today)
            {
                yield return new ValidationResult("The Start time cannot be after today.", new[] { "StartDate" });
            }

            if (this.StartTime > this.EndTime)
            {
                yield return new ValidationResult("The Start time cannot be after the end time.", new[] { "StartTime" });
            }
        }
    }
}
