using Fitness_Shop.Models;
using Microsoft.EntityFrameworkCore;


namespace Fitness_Shop.Repositories
{
    public class OnlineShopDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        public OnlineShopDbContext()
        {
            this.Orders = this.Set<Order>();
            this.Users = this.Set<User>();
            this.Reviews = this.Set<Review>();
            this.Categories = this.Set<Category>();
            this.Products = this.Set<Product>();
            this.OrderProducts = this.Set<OrderProduct>();

        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies()
                .UseSqlServer("Server=localhost;Database=FitneessShopDataBase;Trusted_Connection=True;TrustServerCertificate=true")
               ;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			// Seeding Supplement Categories
			modelBuilder.Entity<Category>().HasData(
				new Category { Id = 1, Name = "Protein Supplements" },
				new Category { Id = 2, Name = "Pre-Workout Boosters" },
				new Category { Id = 3, Name = "Recovery & BCAA" },
				new Category { Id = 4, Name = "Vitamins & Minerals" }
			);



			// Seeding Supplement Products
			modelBuilder.Entity<Product>().HasData(
				new Product
				{
					Id = 1,
					Name = "Whey Protein Isolate",
					Description = "High-quality whey isolate for lean muscle growth",
					Price = 55,
					CategoryId = 1,
					PhotoURL = "/photos/wheyprotein.jpg"
				},
				new Product
				{
					Id = 2,
					Name = "Creatine Monohydrate",
					Description = "Premium creatine formula for strength and endurance",
					Price = 30,
					CategoryId = 2,
					PhotoURL = "/photos/creatine.jpg"
				},
				new Product
				{
					Id = 3,
					Name = "BCAA Powder",
					Description = "Essential amino acids for muscle recovery and hydration",
					Price = 40,
					CategoryId = 3,
					PhotoURL = "/photos/bcaa.jpg"
				},
				new Product
				{
					Id = 4,
					Name = "Omega-3 Fish Oil",
					Description = "Supports heart health and joint recovery",
					Price = 25,
					CategoryId = 4,
					PhotoURL = "/photos/omega3.jpg"
				},
				new Product
				{
					Id = 5,
					Name = "Multivitamin Complex",
					Description = "Daily vitamins for optimal health and immune support",
					Price = 20,
					CategoryId = 4,
					PhotoURL = "/photos/multivitamin.jpg"
				}
			);

			// Seeding Users
			modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "JohnyDep",
                    Password = "1234"
                },
                new User
                {
                    Id = 2,
                    Username = "Brata",
                    Password = "1234"
                }
            );





            // Seeding Reviews
            modelBuilder.Entity<Review>().HasData(
                new Review
                {
                    Id = 1,
                    UserId = 1,
                    ProductId = 1,
                    Rating = 5,
                    Comment = "Amazing product! Highly recommend."
                },
                new Review
                {
                    Id = 2,
                    UserId = 2,
                    ProductId = 2,
                    Rating = 4,
                    Comment = "Great."
                },
                new Review
                {
                    Id = 3,
                    UserId = 1,
                    ProductId = 3,
                    Rating = 4,
                    Comment = "Good."
                }
            );

            base.OnModelCreating(modelBuilder);
        }



    }
}
