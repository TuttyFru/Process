using System.ComponentModel.DataAnnotations;

namespace MB.Data
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public int Weight { get; set; }
        [Required]
        public int Volume { get; set; }
    }
}
