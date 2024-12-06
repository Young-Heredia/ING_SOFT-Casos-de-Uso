using GESTIONAR_PROMOCIONES.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GESTIONAR_PROMOCIONES.Controllers
{
    public class ReportsController : Controller
    {
        private readonly DB_ManagePromotionsContext _context;

        public ReportsController(DB_ManagePromotionsContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductReport()
        {
            LoadProductFilters();
            return View();
        }

        [HttpPost]
        public IActionResult ProductReport(int categoryId, string sortOrder, byte? status)
        {
            LoadProductFilters();

            var products = _context.Products.AsQueryable();

            if (categoryId != 0)
            {
                products = products.Where(p => p.CategoryID == categoryId);
            }

            if (status.HasValue)
            {
                products = products.Where(p => p.Status == status.Value);
            }

            switch (sortOrder)
            {
                case "price_desc":
                    products = products.OrderByDescending(p => p.Price);
                    break;
                case "price_asc":
                    products = products.OrderBy(p => p.Price);
                    break;
                default:
                    products = products.OrderBy(p => p.Name);
                    break;
            }

            return View(products.ToList());
        }

        public IActionResult PromotionReport()
        {
            LoadPromotionFilters();
            return View();
        }

        [HttpPost]
        public IActionResult PromotionReport(int productId, string sortOrder, byte? status, int? daysToExpire)
        {
            LoadPromotionFilters();

            var promotions = _context.Promions.AsQueryable();

            if (productId != 0)
            {
                promotions = promotions.Where(p => p.ProductID == productId);
            }

            if (status.HasValue)
            {
                promotions = promotions.Where(p => p.Status == status.Value);
            }

            if (daysToExpire.HasValue)
            {
                var currentDate = DateTime.Now;
                var endDateLimit = currentDate.AddDays(daysToExpire.Value);
                promotions = promotions.Where(p => p.EndDate <= endDateLimit);
            }

            switch (sortOrder)
            {
                case "price_desc":
                    promotions = promotions.OrderByDescending(p => p.Price);
                    break;
                case "price_asc":
                    promotions = promotions.OrderBy(p => p.Price);
                    break;
                default:
                    promotions = promotions.OrderBy(p => p.Name);
                    break;
            }

            return View(promotions.ToList());
        }

        private void LoadProductFilters()
        {
            var categories = _context.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            ViewBag.SortOrder = new SelectList(new[]
            {
                new { Value = "price_asc", Text = "Price: Low to High" },
                new { Value = "price_desc", Text = "Price: High to Low" }
            }, "Value", "Text");

            ViewBag.Status = new SelectList(new[]
            {
                new { Value = (byte)1, Text = "Valid" },
                new { Value = (byte)0, Text = "Invalid" }
            }, "Value", "Text");
        }

        private void LoadPromotionFilters()
        {
            var products = _context.Products.ToList();
            ViewBag.Products = new SelectList(products, "Id", "Name");

            ViewBag.SortOrder = new SelectList(new[]
            {
                new { Value = "price_asc", Text = "Price: Low to High" },
                new { Value = "price_desc", Text = "Price: High to Low" }
            }, "Value", "Text");

            ViewBag.Status = new SelectList(new[]
            {
                new { Value = (byte)1, Text = "Valid" },
                new { Value = (byte)0, Text = "Invalid" }
            }, "Value", "Text");

            ViewBag.DaysToExpire = new SelectList(new[]
            {
                new { Value = 7, Text = "Next 7 days" },
                new { Value = 14, Text = "Next 14 days" },
                new { Value = 30, Text = "Next 30 days" }
            }, "Value", "Text");
        }
    }
}
