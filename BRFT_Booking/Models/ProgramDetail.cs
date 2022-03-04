using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRTF_Booking.Models
{
    public class ProgramDetail
    {
        public ProgramDetail()
        {
            this.ProgramTermGroups = new HashSet<ProgramTermGroup>();
        }
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<ProgramTermGroup> ProgramTermGroups { get; set; }
    }
}
