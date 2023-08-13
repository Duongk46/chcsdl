using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project.Entities;

namespace project.Controllers
{
	[Authorize]
	public class FoodController : Controller
	{
		private readonly QuanAnContext _db;
		public FoodController(QuanAnContext quanAnContext) {
			_db = quanAnContext;
		}
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var model = _db.MonAns.Include(x => x.LoaiMonAn).ToList();
            return View(model);
		}
		[HttpGet]
		public IActionResult Create()
		{
			ViewData["ListCategoryFood"] = _db.LoaiMonAns.ToList();
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(MonAn monAn)
		{
            ViewData["ListCategoryFood"] = _db.LoaiMonAns.ToList();
            if (!ModelState.IsValid) return View(monAn);
			try
			{
				monAn.MaMonAn = Guid.NewGuid();
				monAn.NgayTao = DateTime.Now;
                await _db.MonAns.AddAsync(monAn);
				await _db.SaveChangesAsync();
                return Redirect("/Food");
			}
			catch (Exception ex)
			{
				ViewBag.Message = "Thêm thức ăn thất bại";
                return View(monAn);
			}
		}
		[HttpGet]
		public IActionResult Edit(Guid id)
		{
			try
			{
				var model = _db.MonAns.Find(id);
				ViewData["ListCategoryFood"] = _db.LoaiMonAns.ToList();
				return View(model);
			}
			catch(Exception ex)
			{
				return Redirect("/Food");
			}
		}
		[HttpPost]
		public async Task<IActionResult> Edit(MonAn monAn)
		{
            ViewData["ListCategoryFood"] = _db.LoaiMonAns.ToList();
			if (!ModelState.IsValid) return View(monAn);
			try
			{
				monAn.NgayUpdate = DateTime.Now;
				_db.Update(monAn);
				await _db.SaveChangesAsync();
				return Redirect("/Food");
			}
			catch(Exception ex)
			{
                ViewBag.Message = "Chỉnh sửa thức ăn thất bại";
                return View(monAn);
			}
        }
		[HttpPost]
		public async Task<JsonResult> Delete(Guid id)
		{
			try
			{
				var check = await _db.MonAns.SingleOrDefaultAsync(x => x.MaMonAn == id);
				if (check != null) {
					_db.MonAns.Remove(check);
					await _db.SaveChangesAsync();
					return Json(new
					{
						status = true
					});
				}
				return Json(new
				{
					status = false
				});
			}
			catch(Exception ex)
			{
				return Json(new
				{
					status = false
				});
			}

		}
    }
}
