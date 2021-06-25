using System.ComponentModel.DataAnnotations;

namespace EMONAPI.Persistance.Entities
{
    public class TempratureModel
    {
        [Required]
        [Key]
        public string Id { get; set; }

        [Required]
        public string timeStamp { get; set; }

        public string value { get; set; }

        public TempratureModel()
        {

        }

    }
}
