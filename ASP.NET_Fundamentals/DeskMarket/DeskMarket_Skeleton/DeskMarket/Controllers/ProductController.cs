using DeskMarket.Data;
using DeskMarket.Data.Models;
using DeskMarket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;

namespace DeskMarket.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext context;

        public ProductController(ApplicationDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await context.Products
                .Where(p => p.IsDeleted == false)
                .Select(p => new ProductInfoViewModel
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    IsSeller = p.SellerId == GetCurrentUserId(),
                    HasBought = false,
                })
                .AsNoTracking()
                .ToListAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new ProductViewModel();
            model.Categories = await GetCategories();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategories();
                return View(model);
            }

            DateTime addedOn;

            if (!DateTime.TryParseExact(model.AddedOn, "dd-MM-yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out addedOn))
            {
                ModelState.AddModelError(nameof(model.AddedOn), "Invalid date format");
                model.Categories = await GetCategories();
                return View(model);
            }

            var product = new Product()
            {
                ProductName = model.ProductName,
                Description = model.Description,
                Price = model.Price,
                ImageUrl = model.ImageUrl,
                AddedOn = addedOn,
                CategoryId = model.CategoryId,
            };

            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await context.Products
                .Where(p => p.Id == id && p.IsDeleted == false)
                .AsNoTracking()
                .Select(p => new ProductViewModel
                {
                    ProductName = p.ProductName,
                    Description = p.Description,
                    CategoryId = p.CategoryId,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    AddedOn = p.AddedOn.ToString("dd-MM-yyyy")
                })
                .FirstOrDefaultAsync();

            model.Categories = await GetCategories();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategories();
                return View(model);
            }

            DateTime addedOn;

            if (!DateTime.TryParseExact(model.AddedOn, "dd-MM-yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out addedOn))
            {
                ModelState.AddModelError(nameof(model.AddedOn), "Invalid date format");
                model.Categories = await GetCategories();
                return View(model);
            }

            var entity = await context.Products.FindAsync(id);

            if (entity == null || entity.IsDeleted)
            {
                throw new ArgumentException("Invalid id");
            }

            string currentUserId = GetCurrentUserId() ?? string.Empty;
            if (entity.SellerId != currentUserId)
            {
                return RedirectToAction(nameof(Index));
            }

            entity.ProductName = model.ProductName;
            entity.Description = model.Description;
            entity.Price = model.Price;
            entity.ImageUrl = model.ImageUrl;
            entity.AddedOn = addedOn;
            entity.CategoryId = model.CategoryId;

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            string currentUserId = GetCurrentUserId() ?? string.Empty;

            var model = await context.Products
                .Where(p => p.IsDeleted == false)
                .Where(p => p.ProductsClients.Any(pc => pc.ClientId == currentUserId))
                .Select(p => new ProductInfoViewModel()
                {
                    Id = p.Id,
                    Category = p.Category.Name,
                    ImageUrl = p.ImageUrl,
                    Seller = p.Seller.UserName ?? string.Empty,
                    AddedOn = p.AddedOn.ToString("dd-MM-yyyy"),
                    ProductName = p.ProductName
                })
                .AsNoTracking()
                .ToListAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddToCart(int id)
        {
            Product? entity = await context.Products
                .Where(p => p.Id == id)
                .Include(p => p.ProductsClients)
                .FirstOrDefaultAsync();

            if (entity == null || entity.IsDeleted)
            {
                throw new ArgumentException("Invalid id");
            }

            string currentUserId = GetCurrentUserId() ?? string.Empty;

            if (entity.ProductsClients.Any(gr => gr.ClientId == currentUserId))
            {
                RedirectToAction(nameof(Index));
            }

            entity.ProductsClients.Add(new ProductClient()
            {
                ClientId = currentUserId,
                ProductId = entity.Id,
            });

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Cart));
        }

        [HttpGet]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            Product? entity = await context.Products
                .Where(g => g.Id == id)
                .Include(g => g.ProductsClients)
                .FirstOrDefaultAsync();

            if (entity == null || entity.IsDeleted)
            {
                throw new ArgumentException("Invalid id");
            }

            string currentUserId = GetCurrentUserId() ?? string.Empty;
            ProductClient? gamerGame = entity.ProductsClients
                .FirstOrDefault(gr => gr.ClientId == currentUserId);

            if (gamerGame != null)
            {
                entity.ProductsClients.Remove(gamerGame);

                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Cart));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await context.Products
                .Where(p => p.Id == id)
                .Where(p => p.IsDeleted == false)
                .AsNoTracking()
                .Select(p => new ProductDetailsViewModel()
                {
                    Id = p.Id,
                    Description = p.Description,
                    Category = p.Category.Name,
                    ImageUrl = p.ImageUrl,
                    AddedOn = p.AddedOn.ToString("yyyy-MM-dd"),
                    ProductName = p.ProductName,
                    Seller = p.Seller.UserName ?? string.Empty,
                })
                .FirstOrDefaultAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await context.Products
                .Where(p => p.Id == id)
                .Where(p => p.IsDeleted == false)
                .AsNoTracking()
                .Select(p => new DelateViewModel()
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    SellerId = p.SellerId,
                    Seller = p.Seller.UserName ?? string.Empty
                })
                .FirstOrDefaultAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(ProductViewModel model)
        {
            Product? product = await context.Products
                .Where(p => p.Id == model.Id)
                .Where(p => p.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (product != null)
            {
                product.IsDeleted = true;

                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private string? GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        private async Task<List<Category>> GetCategories()
        {
            return await context.Categories.ToListAsync();
        }
    }
}
