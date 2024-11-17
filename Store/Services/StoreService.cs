using Store.Models;
using System.Linq;

namespace Store.Services
{
    public class StoreService
    {
        static List<Product> products = new List<Product>
        {
            new Product{Id=1,Name="cercei",Price=15},
            new Product{Id=2,Name="colier",Price=50},
            new Product{Id=3,Name="brosa",Price=25}
        };

        public StoreService() { }
        public Product CreateProd(Product product)
        {
            products.Add(product);
            return product;
        }

        public bool UpdateProd(int id, Product product)
        {
            var prod = products.FirstOrDefault(x => x.Id == id);
            if (prod != null) 
            {
                prod.Name=product.Name;
                prod.Price=product.Price;
                return true;
            }

            return false;
        }

        public List<Product> GetProducts() 
        { 
            return products;
        }

        public Product GetProductById(int id)
        {
            var prod = products.FirstOrDefault(p => p.Id == id);
            return prod;
        }

        public bool DeleteProductById(int id)
        {
            var prod= products.FirstOrDefault(x => x.Id == id);
            if (prod != null) 
            { 
                products.Remove(prod);
                return true;
            }

            return false;
        }
    }
}
