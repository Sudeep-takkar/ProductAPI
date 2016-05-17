using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using ProductApi.Models;

namespace SimpleApi.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        [FromServices]
        public IProductRepository ProductItems { get; set; }


        [HttpGet("{User}")]
        public int GetToken(string User)
        {
            return ProductItems.GetToken(User);
        }


        [HttpGet("{User}/{Token}")]
        public IEnumerable<ProductItem> GetAll(int Token, string User)
        {
            return ProductItems.GetAll(Token, User);

        }

        [HttpGet("{Token}/{id}/{Password}", Name = "GetProduct")]
        public IActionResult GetById(string id, int Token, string Password)
        {
            if (id != "id")
            {
                var item = ProductItems.Find(id, Token, Password);
                if (item != null)
                    return new ObjectResult(item);
                else
                {
                    string error1 = "Product Not found";
                    return new ObjectResult(error1);
                }
            }
            string error = "Enter UserName";
            return new ObjectResult(error);
        }

        [HttpPost("{Token}/{Password}")]
        public IActionResult Create([FromBody] ProductItem item, int Token, string Password)
        {
            if (item == null)
            {
                return HttpBadRequest();
            }
            bool x = ProductItems.Add(item, Token, Password);
            if (x)
                return CreatedAtRoute("GetProduct", new { controller = "Product", id = item.Key }, item);
            else
            {
                string error = "Invalid or duplicate entry";
                return new ObjectResult(error);
            }
        }

        [HttpPut("{Token}/{id}/{Password}")]
        public IActionResult Update(string id, [FromBody] ProductItem item, int Token, string Password)
        {
            if (item == null || item.Key != id)
            {
                return HttpBadRequest();
            }

            var Product = ProductItems.Find(id, Token, Password);
            if (Product == null)
            {
                return HttpNotFound();
            }
            bool x = ProductItems.Update(item, Token, Password);
            string msg = string.Empty;
            if (x)
            {
                msg = "Successfully updated";
                return new ObjectResult(msg);
            }
            else
            {
                msg = "Invalid entry";
                return new ObjectResult(msg);
            }
        }

        [HttpDelete("{Token}/{id}/{Password}")]
        public IActionResult Delete(string id, int Token, string Password)
        {
            string str = string.Empty;
            ProductItem DeletedItem = ProductItems.Remove(id, Token, Password);
            if (DeletedItem != null)
            {
                str = "Successfully removed";
            }
            else
            {
                str = "Deletion Operation failed";
            }
            return new ObjectResult(str);

        }

    }

}