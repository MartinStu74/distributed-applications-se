using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Fitness_Shop.Models;

namespace Fitness_Shop.Models
{
    public class Product : BaseModel
    {


        [Required, MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public string PhotoURL { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        [JsonIgnore] //problem sus sesiqta(poluchavashe se loop)
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }

}
