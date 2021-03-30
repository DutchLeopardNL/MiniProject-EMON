using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EMONAPI.Persistance.Entities
{
    public class FullDatagram
    {
        [Required][Key]
        public string Id { get; set; }
        [Required]
        public string timeStamp { get; set; }
        [Required]
        public double currentUsage { get; set; }
        [Required]
        public double totalLow { get; set; }
        [Required]
        public double totalHigh { get; set; }
        [Required]
        public double returnLow { get; set; }
        [Required]
        public double returnHigh { get; set; }
        [Required]
        public double gasUsage { get; set; }
        [Required]
        public string signature { get; set; }

        public FullDatagram()
        {
            
        }
    }
}
