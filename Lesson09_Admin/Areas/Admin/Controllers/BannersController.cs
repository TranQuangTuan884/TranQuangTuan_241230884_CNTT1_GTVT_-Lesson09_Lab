using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lesson09_Lab.Areas.Admin.Data;
using Lesson09_Lab.Areas.Admin.Models;

namespace Lesson09_Lab.Areas.Admin.Controllers;
[Area("Admin")]
public class BannersController(AdminDbContext db) : Controller {
 public async Task<IActionResult> Index() => View(await db.Banners.OrderByDescending(x=>x.Id).ToListAsync());
 public async Task<IActionResult> Create() { if ("Banner" == "Product") ViewBag.Categories = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await db.Categories.ToListAsync(), "Id", "Name"); return View(new Banner()); }
 [HttpPost, ValidateAntiForgeryToken] public async Task<IActionResult> Create(Banner item) { if(ModelState.IsValid) { db.Add(item); await db.SaveChangesAsync(); TempData["Success"]="Đã thêm dữ liệu thành công."; return RedirectToAction(nameof(Index)); }  return View(item); }
 public async Task<IActionResult> Edit(int id) { var item=await db.Banners.FindAsync(id); if(item==null) return NotFound();  return View(item); }
 [HttpPost, ValidateAntiForgeryToken] public async Task<IActionResult> Edit(int id, Banner item) { if(id!=item.Id) return NotFound(); if(ModelState.IsValid) { db.Update(item); await db.SaveChangesAsync(); TempData["Success"]="Đã cập nhật dữ liệu."; return RedirectToAction(nameof(Index)); }  return View(item); }
 public async Task<IActionResult> Delete(int id) { var item=await db.Banners.FindAsync(id); if(item==null) return NotFound(); return View(item); }
 [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken] public async Task<IActionResult> DeleteConfirmed(int id) { var item=await db.Banners.FindAsync(id); if(item!=null) { db.Remove(item); try { await db.SaveChangesAsync(); TempData["Success"]="Đã xóa dữ liệu."; } catch(DbUpdateException) { TempData["Error"]="Không thể xóa vì dữ liệu đang được tham chiếu."; } } return RedirectToAction(nameof(Index)); }
}