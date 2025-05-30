using Fitness_Shop.Models;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Fitness_Shop.Models
{
    public class User : BaseModel
    {


        [Required, MaxLength(100)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        // public virtual ICollection<Order> Orders { get; set; }


        [JsonIgnore]
        public virtual ICollection<Review> Reviews { get; set; }

    }
}
