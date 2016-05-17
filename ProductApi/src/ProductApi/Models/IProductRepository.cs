using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApi.Models
{
    public interface IProductRepository
    {
        bool Add(ProductItem item, int Token, string Password);
        IEnumerable<ProductItem> GetAll(int Token, string User);
        int GetToken(string User);
        ProductItem Find(string key, int Token, string Password);
        ProductItem Remove(string key, int Token, string Password);
        bool Update(ProductItem item, int Token, string Password);
    }
}
