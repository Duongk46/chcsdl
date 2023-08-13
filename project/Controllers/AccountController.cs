using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using project.Entities;

namespace project.Controllers
{
    [Authorize(Roles = "administrator")]
    public class AccountController : Controller
    {
        private readonly QuanAnContext _db;
        public AccountController(QuanAnContext quanAnContext) {
            _db = quanAnContext;
        }
        public IActionResult Index()
        {
            var model = _db.TaiKhoans.Include(x => x.LoaiTaiKhoan).ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["ListCategoryAccount"] = _db.LoaiTaiKhoans.Where(x => x.Status);
            ViewData["ListGender"] = new List<object>()
            {
                new {
                    Name = "Nam",
                    ID = 1
                },
                new {
                    Name = "Nữ",
                    ID = 2
                },
                new {
                    Name = "Khác",
                    ID = 3
                },
            };
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TaiKhoan taiKhoan)
        {
            ViewData["ListGender"] = new List<object>()
            {
                new {
                    Name = "Nam",
                    ID = 1
                },
                new {
                    Name = "Nữ",
                    ID = 2
                },
                new {
                    Name = "Khác",
                    ID = 3
                },
            };
            ViewData["ListCategoryAccount"] = _db.LoaiTaiKhoans.Where(x => x.Status);
            if (!ModelState.IsValid) return View(taiKhoan);
            try
            {
                taiKhoan.MaTaiKhoan = Guid.NewGuid();
                taiKhoan.NgayUpdate = DateTime.Now;
                taiKhoan.NgayTao = DateTime.Now;
                await _db.TaiKhoans.AddAsync(taiKhoan);
                await _db.SaveChangesAsync();
                return Redirect("/Account");
            }
            catch(Exception ex)
            {
                ViewBag.Message = "Thêm mới tài khoản thất bại";
                return View(taiKhoan);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                ViewData["ListCategoryAccount"] = _db.LoaiTaiKhoans.Where(x => x.Status);
                ViewData["ListGender"] = new List<object>()
                {
                    new {
                        Name = "Nam",
                        ID = 1
                    },
                    new {
                        Name = "Nữ",
                        ID = 2
                    },
                    new {
                        Name = "Khác",
                        ID = 3
                    },
                };
                var entity = await _db.TaiKhoans.SingleOrDefaultAsync(x => x.MaTaiKhoan == id);
				return View(entity);
			}
            catch(Exception ex)
            {
                return Redirect("/Account");
            }
		}
        [HttpPost]
        public async Task<IActionResult> Edit(TaiKhoan taiKhoan)
        {
            ViewData["ListCategoryAccount"] = _db.LoaiTaiKhoans.Where(x => x.Status);
            ViewData["ListGender"] = new List<object>()
				{
					new {
						Name = "Nam",
						ID = 1
					},
					new {
						Name = "Nữ",
						ID = 2
					},
					new {
						Name = "Khác",
						ID = 3
					},
				};
			if (!ModelState.IsValid) return View(taiKhoan);
            try
            {
				taiKhoan.NgayUpdate = DateTime.Now;
				_db.Attach(taiKhoan);
				_db.Entry(taiKhoan).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return Redirect("/Account");
            }
            catch(Exception ex)
            {
                ViewBag.Message = "";
                return View(taiKhoan);
            }
        }
        [HttpPost]
        public async Task<JsonResult> Delete(Guid id)
        {
            try
            {
                var check = await _db.TaiKhoans.SingleOrDefaultAsync(x => x.MaTaiKhoan == id);
                if (check != null)
                {
                    _db.TaiKhoans.Remove(check);
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
