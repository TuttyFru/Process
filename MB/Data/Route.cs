using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MB.Data
{
    public class Route
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Distance { get; set; }
        [Required]
        public int Time { get; set; }
        [ForeignKey("Address_id_departure")]
        public Address Address_departure { get; set; }
        [DisplayName("Address Departure")]
        public int Address_departure_id { get; set; }
        [ForeignKey("Address_id_arrival")]
        public Address Address_arrival { get; set; }
        [DisplayName("Address Arrival")]
        public int Address_arrival_id { get; set; }
    }
}
