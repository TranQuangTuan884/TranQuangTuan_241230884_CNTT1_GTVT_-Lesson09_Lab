using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lesson09_Lab.Areas.Admin.Data;
using Lesson09_Lab.Areas.Admin.Models;

namespace Lesson09_Lab.Areas.Admin.Controllers;
[Area("Admin")]
public class CategoriesController(AdminDbContext db) : Controller {
 public async Task<IActionResult> Index() => View(await db.Categories.OrderByDescending(x=>x.Id).ToListAsync());
 public async Task<IActionResult> Create() { if ("Category" == "Product") ViewBag.Categories = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await db.Categories.ToListAsync(), "Id", "Name"); return View(new Category()); }
 [HttpPost, ValidateAntiForgeryToken] public async Task<IActionResult> Create(Category item) { if(ModelState.IsValid) { db.Add(item); await db.SaveChangesAsync(); TempData["Success"]="Đã thêm dữ liệu thành công."; return RedirectToAction(nameof(Index)); }  return View(item); }
 public async Task<IActionResult> Edit(int id) { var item=await db.Categories.FindAsync(id); if(item==null) return NotFound();  return View(item); }
 [HttpPost, ValidateAntiForgeryToken] public async Task<IActionResult> Edit(int id, Category item) { if(id!=item.Id) return NotFound(); if(ModelState.IsValid) { db.Update(item); await db.SaveChangesAsync(); TempData["Success"]="Đã cập nhật dữ liệu."; return RedirectToAction(nameof(Index)); }  return View(item); }
 public async Task<IActionResult> Delete(int id) { var item=await db.Categories.FindAsync(id); if(item==null) return NotFound(); return View(item); }
 [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken] public async Task<IActionResult> DeleteConfirmed(int id) { var item=await db.Categories.FindAsync(id); if(item!=null) { db.Remove(item); try { await db.SaveChangesAsync(); TempData["Success"]="Đã xóa dữ liệu."; } catch(DbUpdateException) { TempData["Error"]="Không thể xóa vì dữ liệu đang được tham chiếu."; } } return RedirectToAction(nameof(Index)); }
}