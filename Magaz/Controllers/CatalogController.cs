using Magaz.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication1.Controllers
{
    public class CatalogController : Controller
    {
        private readonly TestContext db;

        public CatalogController(TestContext context)
        {
            this.db = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.Products.ToListAsync());
        }

        public async Task<IActionResult> DetailsProduct(int id)
        {
            var product = await db.Products.FirstOrDefaultAsync(p => p.IdProduct == id); // Заменено на запрос к базе данных
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> ToggleFavorite(int id)
        {
            var product = await db.Products.FirstOrDefaultAsync(p => p.IdProduct == id); // Запрос к базе данных
            if (product != null)
            {
                product.IsFavorite = !product.IsFavorite;
                await db.SaveChangesAsync(); // Сохранение изменений
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Favorites()
        {
            var favoriteProducts = await db.Products.Where(p => p.IsFavorite).ToListAsync(); // Запрос к базе данных
            return View(favoriteProducts);
        }
    }
}