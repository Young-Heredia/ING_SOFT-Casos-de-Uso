using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GESTIONAR_PROMOCIONES.Data;
using GESTIONAR_PROMOCIONES.Models;

namespace GESTIONAR_PROMOCIONES.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly DB_ManagePromotionsContext _context;

        public CategoriesController(DB_ManagePromotionsContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index(string? searchName)
        {
            
            var categories = string.IsNullOrEmpty(searchName)
                ? await _context.Categories.ToListAsync()  
                : await _context.Categories
                    .Where(c => c.Name.Contains(searchName))  
                    .ToListAsync();

            
            ViewData["SearchName"] = searchName; 
            return View(categories);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var category = await FindCategoryAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Status")] Category category)
        {
            if (ModelState.IsValid)
            {
                
                _context.Add(category);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Category created successfully!";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "There was an error creating the category.";
            return View(category);
        }


        
        public async Task<IActionResult> Edit(int? id)
        {
            var category = await FindCategoryAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Status")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

       
        public async Task<IActionResult> Delete(int? id)
        {
            var category = await FindCategoryAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<Category> FindCategoryAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }

            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
