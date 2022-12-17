using System.ComponentModel.DataAnnotations;

namespace MB.Data
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string House { get; set; }
        [Required]
        public int Stops { get; set; }
    }
}
