using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MB.Data
{
    public class Driver
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Full_name { get; set; }
        [Required]
        public int Experience { get; set; }
        [ForeignKey("Car_id")]
        public Car Car { get; set; }
        [DisplayName("Car")]
        public int Car_id { get; set; }
    }
}
