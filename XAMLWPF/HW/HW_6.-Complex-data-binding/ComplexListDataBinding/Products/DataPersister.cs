using Products.Data;
using System.Linq;

namespace Products
{
    public static class DataPersister
    {
        public static Product GetProductById(int id)
        {
            var context = new ProductsEntities();
            using (context)
            {
                return context.Products.FirstOrDefault(x => x.Id == id);
            }
        }
    }
}
