using Matthewpbaileydesigns.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Matthewpbaileydesigns.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategory;

        public ProductCategoryRepository()
        {
            productCategory = cache["productCategory"] as List<ProductCategory>;

            if (productCategory == null)
            {
                productCategory = new List<ProductCategory>();
            }
        }

        public void Commit()
        {
            cache["productCategory"] = productCategory;
        }

        public void Insert(ProductCategory p)
        {
            productCategory.Add(p);
        }

        public void Update(ProductCategory prod)
        {
            ProductCategory productCategoryToUpdate = productCategory.Find(p => p.Id == prod.Id);

            if (productCategoryToUpdate != null)
            {
                productCategoryToUpdate = prod;
            }
            else
            {
                throw new Exception("Product category Category Not Found");
            }
        }

        public ProductCategory Find(ProductCategory prod)
        {
            ProductCategory product = productCategory.Find(p => p.Id == prod.Id);

            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product category category Not Found");
            }
        }

        public ProductCategory Find(string id)
        {
            ProductCategory product = productCategory.Find(p => p.Id == id);

            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product category category Not Found");
            }
        }

        public IQueryable<ProductCategory> ProductCategoryCollection()
        {
            return productCategory.AsQueryable();
        }

        public void Delete(string id)
        {
            ProductCategory productCategoryToDelete = productCategory.Find(p => p.Id == id);

            if (productCategoryToDelete != null)
            {
                productCategory.Remove(productCategoryToDelete);
            }
            else
            {
                throw new Exception("Product category Not Found");
            }
        }
    }
}
