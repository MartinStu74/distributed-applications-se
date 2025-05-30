using Fitness_Shop.Models;


namespace Fitness_Shop.ViewModels.Home
{
    public class IndexVM
    {
        
        
            public List<Category> Categories { get; set; }
            public List<Product> Products { get; set; }
            public string? SelectedCategory { get; set; }
            public string? SearchQuery { get; set; } 
        

    }
}
