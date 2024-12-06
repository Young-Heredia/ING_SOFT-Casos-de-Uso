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

        // GET: Categories
        public async Task<IActionResult> Index(string? searchName)
        {
            // Si se proporciona un valor de búsqueda, filtrar las categorías por nombre
            var categories = string.IsNullOrEmpty(searchName)
                ? await _context.Categories.ToListAsync()  // Si no hay búsqueda, traemos todas las categorías
                : await _context.Categories
                    .Where(c => c.Name.Contains(searchName))  // Filtramos las categorías por nombre
                    .ToListAsync();

            // Pasamos las categorías filtradas (o todas si no se filtra) a la vista
            ViewData["SearchName"] = searchName; // Para mostrar el texto de búsqueda en la vista
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

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Status")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.Status = true; // Establecer el estado predeterminado
                _context.Add(category);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Category created successfully!";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "There was an error creating the category.";
            return View(category);
        }


        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var category = await FindCategoryAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Edit/5
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

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var category = await FindCategoryAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
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
