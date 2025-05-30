using System.ComponentModel.DataAnnotations;

namespace Fitness_Shop.Models
{
    public abstract class BaseModel
    {
        [Key]
        public int Id { get; set; }
    }
}
