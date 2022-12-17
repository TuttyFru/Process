using System.ComponentModel.DataAnnotations;

namespace MB.Data
{
    public class Cargo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Weight { get; set; }
        [Required]
        public int Volume { get; set; }
    }
}
