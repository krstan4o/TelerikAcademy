using System;
using System.Linq;
using InventoryDatabaseData;

namespace InventoryViewModel
{
    public class Persister
    {
        public static ProductViewModel GetProductById(int id) 
        {
            var context = new InventoryDatabaseEntities();
            using (context)
            {
                var productViewModels =
                    from products in context.Products
                    where products.Id == id
                    select new ProductViewModel()
                    {
                        Id = products.Id,
                        Category = products.Category.Name,
                        ProductNumber = products.ModelNumber,
                        ProductModel = products.ModelName,
                        UnitCost = products.UnitCost,
                        Description = products.Description
                    };

                return productViewModels.FirstOrDefault();
            }
        }
    }
}
