using Fitness_Shop.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Fitness_Shop.Models
{
    public class Category : BaseModel
    {


        [Required, MaxLength(100)]
        public string Name { get; set; }


        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}
