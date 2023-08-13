using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project.Entities;

namespace project.Controllers
{
	[Authorize(Roles = "administrator")]
    public class CategoryAccountController : Controller
    {
        private readonly QuanAnContext _db;
        public CategoryAccountController(QuanAnContext quanAnContext) {
            _db = quanAnContext;
        }
        public IActionResult Index()
        {
			var model = _db.LoaiTaiKhoans.ToList();
            return View(model);
        }

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(LoaiTaiKhoan loaiTaiKhoan)
		{
			if (!ModelState.IsValid) return View(loaiTaiKhoan);
			try
            {
                loaiTaiKhoan.CreatedBy = new Guid(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
                loaiTaiKhoan.MaLoaiTaiKhoan = Guid.NewGuid();
				loaiTaiKhoan.NgayTao = DateTime.Now;
				await _db.AddAsync(loaiTaiKhoan);
				await _db.SaveChangesAsync();
				return Redirect("/CategoryAccount");
			}
			catch (Exception ex)
			{
				ViewBag.Message = "Thêm loại món ăn thất bại";
				return View(loaiTaiKhoan);
			}
		}
		[HttpGet]
		public IActionResult Edit(Guid id)
		{
			try
			{
				var model = _db.LoaiTaiKhoans.Find(id);
				return View(model);
			}
			catch (Exception ex)
			{
				return Redirect("/CategoryAccount");
			}
		}
		[HttpPost]
		public async Task<IActionResult> Edit(LoaiTaiKhoan loaiTaiKhoan)
		{
			if (!ModelState.IsValid) { return View(loaiTaiKhoan); }
			try
			{
				loaiTaiKhoan.NgayUpdate = DateTime.Now;
				_db.Update(loaiTaiKhoan);
				await _db.SaveChangesAsync();
				return Redirect("/CategoryAccount");
			}
			catch (Exception ex)
			{
				ViewBag.Message = "Chỉnh sửa thất bại";
				return View(loaiTaiKhoan);
			}
		}
        [HttpPost]
        public async Task<JsonResult> Delete(Guid id)
        {
            try
            {
                var check = await _db.LoaiTaiKhoans.SingleOrDefaultAsync(x => x.MaLoaiTaiKhoan == id);
                if (check != null)
                {
                    _db.LoaiTaiKhoans.Remove(check);
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
