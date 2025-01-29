using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebappFoodsale.Data;
using WebappFoodsale.Models;

namespace WebappFoodsale.Controllers
{
    public class FoodController : Controller
    {
        private readonly ApplicationDBContext _db;
        public FoodController(ApplicationDBContext db)
        {
            _db = db;
        }
        

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Food obj)
        {
            if (ModelState.IsValid) { 
                _db.Foods.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Edit(int? id) 
        {
            if(id == null || id == 0) 
            { 
                return NotFound();
            }
            var obj = _db.Foods.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Food obj)
        {
            if (ModelState.IsValid)
            {
                _db.Foods.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Foods.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Foods.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index(string sortOrder, string searchString, DateTime? startDate, DateTime? endDate)
        {
            ViewData["OrderDateSort"] = sortOrder == "OrderDate_asc" ? "OrderDate_desc" : "OrderDate_asc";
            ViewData["RegionSort"] = sortOrder == "Region_asc" ? "Region_desc" : "Region_asc";
            ViewData["CitySort"] = sortOrder == "City_asc" ? "City_desc" : "City_asc";
            ViewData["CategorySort"] = sortOrder == "Category_asc" ? "Category_desc" : "Category_asc";
            ViewData["ProductSort"] = sortOrder == "Product_asc" ? "Product_desc" : "Product_asc";
            ViewData["QuantitySort"] = sortOrder == "Quantity_asc" ? "Quantity_desc" : "Quantity_asc";
            ViewData["UnitPriceSort"] = sortOrder == "UnitPrice_asc" ? "UnitPrice_desc" : "UnitPrice_asc";
            ViewData["TotalPriceSort"] = sortOrder == "TotalPrice_asc" ? "TotalPrice_desc" : "TotalPrice_asc";
            ViewData["CurrentFilter"] = searchString;
            ViewData["StartDate"] = startDate?.ToString("yyyy-MM-dd"); 
            ViewData["EndDate"] = endDate?.ToString("yyyy-MM-dd");
            var foods = from s in _db.Foods
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                foods = foods.Where(s => s.Region.Contains(searchString)
                                       || s.City.Contains(searchString) || s.Category.Contains(searchString) || s.Product.Contains(searchString) );
            }

            if (startDate.HasValue && endDate.HasValue)
            {
                foods = foods.Where(s => s.OrderDate >= startDate && s.OrderDate <= endDate);
            }
            switch (sortOrder)
            {
                case "OrderDate_asc":
                    foods = foods.OrderBy(f => f.OrderDate);
                    break;
                case "OrderDate_desc":
                    foods = foods.OrderByDescending(f => f.OrderDate);
                    break;
                case "Region_asc":
                    foods = foods.OrderBy(f => f.Region);
                    break;
                case "Region_desc":
                    foods = foods.OrderByDescending(f => f.Region);
                    break;
                case "City_asc":
                    foods = foods.OrderBy(f => f.City);
                    break;
                case "City_desc":
                    foods = foods.OrderByDescending(f => f.City);
                    break;
                case "Category_asc":
                    foods = foods.OrderBy(f => f.Category);
                    break;
                case "Category_desc":
                    foods = foods.OrderByDescending(f => f.Category);
                    break;
                case "Product_asc":
                    foods = foods.OrderBy(f => f.Product);
                    break;
                case "Product_desc":
                    foods = foods.OrderByDescending(f => f.Product);
                    break;
                case "Quantity_asc":
                    foods = foods.OrderBy(f => f.Quantity);
                    break;
                case "Quantity_desc":
                    foods = foods.OrderByDescending(f => f.Quantity);
                    break;
                case "UnitPrice_asc":
                    foods = foods.OrderBy(f => f.UnitPrice);
                    break;
                case "UnitPrice_desc":
                    foods = foods.OrderByDescending(f => f.UnitPrice);
                    break;
                case "TotalPrice_asc":
                    foods = foods.OrderBy(f => f.TotalPrice);
                    break;
                case "TotalPrice_desc":
                    foods = foods.OrderByDescending(f => f.TotalPrice);
                    break;
                default:
                    foods = foods.OrderBy(f => f.OrderDate); 
                    break;
            }
            return View(await foods.AsNoTracking().ToListAsync());
        }
    }
}
