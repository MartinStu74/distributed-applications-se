using Fitness_Shop.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Fitness_Shop.Models
{
    public class Review : BaseModel
    {


        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        [JsonIgnore]
        public virtual Product Product { get; set; }

        [Required]
        public int Rating { get; set; }

        [MaxLength(500)]
        public string Comment { get; set; }
    }
}
