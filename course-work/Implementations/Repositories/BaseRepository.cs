using System.Linq.Expressions;
using Fitness_Shop.Models;
using Fitness_Shop.Repositories;
using Microsoft.EntityFrameworkCore;


namespace Fitness_Shop.Repositories
{
    public class BaseRepository<T>
        where T : BaseModel
    {
        protected DbContext Context { get; set; }
        protected DbSet<T> Items { get; set; }

        public BaseRepository()
        {
            Context = new OnlineShopDbContext();
            Items = Context.Set<T>();
        }
        public List<T> GetAll(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> orderBy = null)
        {
            IQueryable<T> query = Items;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (orderBy != null)
            {
                query = query.OrderBy(orderBy);
            }
            return query
                .ToList();
        }
        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = Items;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query.FirstOrDefault();
        }


        public void Save(T item)
        {
            if (item.Id > 0)
            {
                Items.Update(item);
            }
            else
            {
                Items.Add(item);

                Context.SaveChanges();
            }
        }
        public void Delete(T item)
        {
            Items.Remove(item);
            Context.SaveChanges();
        }
    }
}
