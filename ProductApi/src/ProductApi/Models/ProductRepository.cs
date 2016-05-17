using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApi.Models
{
    public class ProductRepository : IProductRepository
    {
        static ConcurrentDictionary<string, ProductItem> _products = new ConcurrentDictionary<string, ProductItem>();
        ProductItem item1 = new ProductItem();
        public ProductRepository()
        {
            Add(new ProductItem
            {
                Name = "Iphone6S",
                User = "Sudeep",
                Password = "1234",
                IsAvailable = "False",
                Price = 45000,
                Discount = "12%",
                Color = "Gold",
                Token = 1
            }, 1, "1234");
            Add(new ProductItem
            {
                Name = "Iphone5S",
                User = "Sumit",
                Password = "1234",
                IsAvailable = "True",
                Price = 25000,
                Discount = "12%",
                Color = "Grey",
                Token = 2
            }, 2, "1234");
            Add(new ProductItem
            {
                Name = "Iphone4S",
                User = "Sudeep",
                Password = "1234",
                IsAvailable = "False",
                Price = 15000,
                Discount = "12%",
                Color = "Silver",
                Token = 1
            }, 1, "1234");
            Add(new ProductItem
            {
                Name = "IphoneSE",
                User = "Sudeep",
                Password = "1234",
                IsAvailable = "True",
                Price = 25000,
                Discount = "12%",
                Color = "Gold",
                Token = 1
            }, 1, "1234");
            Add(new ProductItem
            {
                Name = "Samsung Edge",
                User = "Sumit",
                Password = "1234",
                IsAvailable = "True",
                Price = 35000,
                Discount = "12%",
                Color = "Silver",
                Token = 2
            }, 2, "1234");
            Add(new ProductItem
            {
                Name = "Red Mi Note 4g",
                User = "Sumit",
                Password = "1234",
                IsAvailable = "True",
                Price = 15000,
                Discount = "16%",
                Color = "Black",
                Token = 2
            }, 2, "1234");
        }

        public IEnumerable<ProductItem> GetAll(int Token, string User)
        {
            if (Token != 0)
            {
                var keysWithMatchingValues = _products.Where(p => (p.Value.User).Equals(User));
                return keysWithMatchingValues.Where(p => p.Value.Token.Equals(Token)).Select(p=>p.Value);
            }
            else
                return null;
        }

        public int GetToken(string User)
        {
            var keysWithMatchingValues = _products.Where(p => (p.Value.User).Equals(User)).FirstOrDefault();
            if (keysWithMatchingValues.Value != null)
            {
                return keysWithMatchingValues.Value.Token;
            }
            return -1;
        }

        public bool Add(ProductItem item, int Token, string Password)
        {
            var keysWithMatchingValues = _products.Where(p => (p.Value.Name + p.Value.Color).Equals(item.Name + item.Color)).FirstOrDefault();
            if ((keysWithMatchingValues.Value == null) || (_products.Count == 0) && (keysWithMatchingValues.Value.Token.Equals(Token)) && (keysWithMatchingValues.Value.Password.Equals(Password)))
            {
                item.Key = Guid.NewGuid().ToString();
                _products[item.Key] = item;
                return true;
            }
            return false;
        }

        public ProductItem Find(string key, int Token, string Password)
        {
            ProductItem item2 = null;
            _products.TryGetValue(key, out item2);
            if (item2 != null)
            {
                var keysWithMatchingValues = _products.Where(p => p.Value.Key.Equals(key)).FirstOrDefault();
                if (keysWithMatchingValues.Value == null)
                {
                    if ((keysWithMatchingValues.Value.Token == Token) && (keysWithMatchingValues.Value.Password.Equals(Password)))
                    {
                        _products.TryGetValue(key, out item2);
                    }
                }
            }
            return item2;
        }

        public ProductItem Remove(string key, int Token, string Password)
        {
            item1 = null;
            ProductItem item2;
            _products.TryGetValue(key, out item2);
            if (item2 != null)
            {
                var keysWithMatchingValues = _products.Where(p => (p.Value.Name + p.Value.Color).Equals(item2.Name + item2.Color)).FirstOrDefault();
                if ((keysWithMatchingValues.Value.Token == Token) && (keysWithMatchingValues.Value.Password.Equals(Password)))
                {
                    _products.TryRemove(key, out item2);
                    item1 = item2;
                }
                return item1;
            }
            return null;
        }

        public bool Update(ProductItem item, int Token, string Password)
        {
            //var keysWithMatchingValues = _products.Where(p => p.Value.Name.Equals(item.Name)).FirstOrDefault();
            var keysWithMatchingValues = _products.Where(p => (p.Value.Name + p.Value.Color).Equals(item.Name + item.Color)).FirstOrDefault();
            if (keysWithMatchingValues.Value == null)
            {
                if ((item.Token == Token) && (item.Password.Equals(Password)))
                {
                    _products[item.Key] = item;
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
