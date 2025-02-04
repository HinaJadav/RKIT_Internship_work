using ActionMethodDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace ActionMethodDemo.Controllers
{
    public class ProductController : Controller
    {
        // Simulate a product database
        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 999.99 },
            new Product { Id = 2, Name = "Smartphone", Price = 799.99 },
            new Product { Id = 3, Name = "Tablet", Price = 499.99 },
            new Product { Id = 4, Name = "Smartwatch", Price = 199.99 },
            new Product { Id = 5, Name = "Headphones", Price = 129.99 },
            new Product { Id = 6, Name = "Camera", Price = 349.99 },
            new Product { Id = 7, Name = "Keyboard", Price = 89.99 },
            new Product { Id = 8, Name = "Mouse", Price = 29.99 }
        };

        /// <summary>
        /// Displays a list of all products.
        /// Returns the list of products as a View to the user.
        /// https://localhost:7202/Product/Index
        /// </summary>
        public ActionResult Index()
        {
            return View(products);
        }

        /// <summary>
        /// Displays the details of a specific product based on its ID.
        /// If the product is not found, returns a 404 Not Found result.
        /// https://localhost:7202/Product/Details/5
        /// </summary>
        public IActionResult Details(int id)
        {
            var product = products.Find(p => p.Id == id);
            if (product == null)
            {
                return NotFound();  // Return 404 if product not found
            }
            return View(product);
        }

        /// <summary>
        /// Simulates updating a product and returns a status message.
        /// If the product is found, return a success message; otherwise, returns a not found message.
        /// https://localhost:7202/Product/UpdateProductStatus/5
        /// </summary>
        public ContentResult UpdateProductStatus(int id)
        {
            var product = products.Find(p => p.Id == id);
            if (product != null)
            {
                return Content($"Product {product.Name} has been updated successfully!");
            }
            return Content("Product not found.");
        }

        /// <summary>
        /// Returns product data in JSON format, useful for API requests.
        /// If the product is found, it returns the product details, otherwise a not found message in JSON.
        /// https://localhost:7202/Product/GetProductJson/5
        /// </summary>
        public JsonResult GetProductJson(int id)
        {
            var product = products.Find(p => p.Id == id);
            if (product == null)
            {
                return Json(new { message = "Product not found" });
            }
            return Json(product);
        }

        /// <summary>
        /// Returns a partial view showing a summary of the product.
        /// If the product is found, it returns the product summary; otherwise, it returns a "not found" partial view.
        /// https://localhost:7202/Product/GetProductSummary/5
        /// </summary>
        public PartialViewResult GetProductSummary(int id)
        {
            var product = products.Find(p => p.Id == id);
            if (product == null)
            {
                return PartialView("_ProductNotFound");
            }
            return PartialView("_ProductSummary", product);
        }

        /// <summary>
        /// Returns a full view displaying the product's name and price.
        /// This example uses dynamic data for a single product.
        /// https://localhost:7202/Product/GetProductView/5
        /// </summary>
        public ViewResult GetProductView()
        {
            var product = new Product { Name = "Headphones", Price = 129.99 };
            return View("ProductDetails", product);
        }


        /// <summary>
        /// Provides an API endpoint to return a filtered list of products based on query parameters.
        /// Supports filtering by name, price range, sorting, and pagination.
        /// https://localhost:7202/Product/GetProducts/products?search=phone&sortBy=Price&sortOrder=desc&page=1&pageSize=5
        /// </summary>
        [HttpGet]
        public IActionResult GetProducts([FromQuery] string search = null, [FromQuery] string sortBy = "Name", [FromQuery] string sortOrder = "asc", [FromQuery] int page = 1, [FromQuery] int pageSize = 5, [FromQuery] double? minPrice = null, [FromQuery] double? maxPrice = null)
        {
            var query = products.AsQueryable();

            // Filter by search term
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Name.Contains(search, System.StringComparison.OrdinalIgnoreCase));
            }

            // Filter by price range
            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }
            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            // Sort by given field
            if (sortBy == "Price")
            {
                query = sortOrder.ToLower() == "desc" ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price);
            }
            else
            {
                query = sortOrder.ToLower() == "desc" ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name);
            }

            // Pagination: Skip and Take for paging
            var pagedResult = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Return result with pagination details
            return Ok(new
            {
                TotalCount = query.Count(),
                Page = page,
                PageSize = pageSize,
                Products = pagedResult
            });
        }
    }
}
