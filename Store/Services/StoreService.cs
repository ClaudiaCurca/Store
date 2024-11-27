using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Models;
using System.Linq;

namespace Store.Services
{
    public class StoreService : IServices<Product>
    {
        //static List<Product> products = new List<Product>
        //{
        //    new Product{Id=1,Name="cercei",Price=15},
        //    new Product{Id=2,Name="colier",Price=50},
        //    new Product{Id=3,Name="brosa",Price=25}
        //};
        StoreContext _context;
        public StoreService(StoreContext context) 
        {
           _context = context;
        }
        public Product Create(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public bool Update(int id, Product product)
        {
            var prod = GetById(id);
            if (prod != null) 
            {
                prod.Name=product.Name;
                prod.Price=product.Price;
                _context.SaveChanges();
                return true;
            }
            
            return false;
        }

        public List<Product> GetAll() 
        { 
            List<Product> prods = _context.Products.ToList();

            return prods;
        }

        public Product GetById(int id)
        {
            var prod = _context.Products.FirstOrDefault(x => x.Id == id);

            return prod;
        }

        public bool DeleteById(int id)
        {
            var prod= GetById(id);
            if (prod != null) 
            { 
                _context.Products.Remove(prod);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

    }
}
