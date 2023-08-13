using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project.Entities;
using System.Reflection.Metadata.Ecma335;

namespace project.Controllers
{
	[Authorize]
	public class CategoryFoodController : Controller
	{
		private readonly QuanAnContext _db;
		public CategoryFoodController(QuanAnContext quanAnContext)
		{
			_db = quanAnContext;
		}
		[HttpGet]
		public IActionResult Index()
		{
			var model = _db.LoaiMonAns.ToList();
			return View(model);
		}
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(LoaiMonAn loaiMonAn)
		{
			if (!ModelState.IsValid) return View(loaiMonAn);
			try
			{
                loaiMonAn.MaLoaiMonAn = Guid.NewGuid();
				await _db.AddAsync(loaiMonAn);
				await _db.SaveChangesAsync();
				return Redirect("/CategoryFood");
			}
			catch(Exception ex)
			{
				ViewBag.Message = "Thêm loại món ăn thất bại";
				return View(loaiMonAn);
			}
		}
		[HttpGet]
		public IActionResult Edit(Guid id)
		{
			try
			{
				var model = _db.LoaiMonAns.Find(id);
				return View(model);
			}
			catch(Exception ex)
			{
				return Redirect("/CategoryFood");
			}
		}
		[HttpPost]
		public async Task<IActionResult> Edit(LoaiMonAn loaiMonAn) { 
			if (!ModelState.IsValid) { return View(loaiMonAn); }
			try
			{
				_db.Update(loaiMonAn);
				await _db.SaveChangesAsync();
                return Redirect("/CategoryFood");
            }
            catch (Exception ex)
			{
				ViewBag.Message = "Chỉnh sửa thất bại";
				return View(loaiMonAn);
            }
        }
        [HttpPost]
        public async Task<JsonResult> Delete(Guid id)
        {
            try
            {
                var check = await _db.LoaiMonAns.SingleOrDefaultAsync(x => x.MaLoaiMonAn == id);
                if (check != null)
				{
                    _db.LoaiMonAns.Remove(check);
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
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false
                });
            }

        }
    }
}
