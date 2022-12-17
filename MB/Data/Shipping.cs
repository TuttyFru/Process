using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MB.Data
{
    public class Shipping
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Cost { get; set; }
        [ForeignKey("Route_id")]
        public Route Route { get; set; }
        [DisplayName("Route")]
        public int Route_id { get; set; }
        [ForeignKey("Driver_id")]
        public Driver Driver { get; set; }
        [DisplayName("Driver")]
        public int Driver_id { get; set; }
        [ForeignKey("Cargo_id")]
        public Cargo Cargo { get; set; }
        [DisplayName("Cargo")]
        public int Cargo_id { get; set; }
    }
}
