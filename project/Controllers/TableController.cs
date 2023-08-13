using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project.Entities;

namespace project.Controllers
{
    [Authorize]
    public class TableController : Controller
    {
        private readonly QuanAnContext _db;
        public TableController(QuanAnContext quanAnContext) {
            _db = quanAnContext;
        }
        public IActionResult Index()
        {
            var model = _db.BanAns.ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BanAn banAn)
        {
            if (!ModelState.IsValid) return View(banAn);
            try
            {
                banAn.MaBanAn = Guid.NewGuid();
                await _db.AddAsync(banAn);
                await _db.SaveChangesAsync();
                return Redirect("/Table");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Thêm loại món ăn thất bại";
                return View(banAn);
            }
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            try
            {
                var model = _db.BanAns.Find(id);
                return View(model);
            }
            catch (Exception ex)
            {
                return Redirect("/Table");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BanAn banAn)
        {
            if (!ModelState.IsValid) { return View(banAn); }
            try
            {
                _db.Update(banAn);
                await _db.SaveChangesAsync();
                return Redirect("/Table");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Chỉnh sửa thất bại";
                return View(banAn);
            }
        }
        [HttpPost]
        public async Task<JsonResult> Delete(Guid id)
        {
            try
            {
                var check = await _db.BanAns.SingleOrDefaultAsync(x => x.MaBanAn == id);
                if (check != null)
                {
                    _db.BanAns.Remove(check);
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
