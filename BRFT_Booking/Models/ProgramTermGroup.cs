using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BRTF_Booking.Models
{
    public class ProgramTermGroup
    {
        public int ID { get; set; }

        [Display(Name = "Group Name")]
        public string Name { get; set; }

        [Display(Name = "Program")]
        public int? ProgramDetailID { get; set; }
        public ProgramDetail ProgramDetail { get; set; }

        [Display(Name = "Level")]
        public int Level { get; set; }

        //public ProgramTermGroupArea ProgramTermGroupArea { get; set; }
        //public int? ProgramTermGroupAreaID { get; set; }

    }
}
