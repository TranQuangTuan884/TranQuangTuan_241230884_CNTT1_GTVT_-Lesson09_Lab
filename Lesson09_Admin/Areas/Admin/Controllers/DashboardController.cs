using Microsoft.AspNetCore.Mvc;
using Lesson09_Lab.Areas.Admin.Data;
namespace Lesson09_Lab.Areas.Admin.Controllers;
[Area("Admin")] public class DashboardController(AdminDbContext db) : Controller { public IActionResult Index() { ViewBag.CategoryCount=db.Categories.Count(); ViewBag.ProductCount=db.Products.Count(); ViewBag.BannerCount=db.Banners.Count(); ViewBag.BlogCount=db.Blogs.Count(); return View(); } }