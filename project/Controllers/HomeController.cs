using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project.Entities;
using project.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace project.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly QuanAnContext _db;
        public HomeController(ILogger<HomeController> logger, QuanAnContext quanAnContext)
        {
            _logger = logger;
            _db = quanAnContext;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
			float total = 0;
            var listBill = await _db.HoaDons.ToListAsync();
            foreach(var item in listBill)
            {
                total += (float)item.TongTien;
            }
			ViewData["CountAccount"] = await _db.TaiKhoans.CountAsync();
            ViewData["CountCategoryFood"] = await _db.LoaiMonAns.CountAsync();
            ViewData["CountFood"] = await _db.MonAns.CountAsync();
            ViewData["TotalBill"] = total;
            return View();
        }
        [HttpGet]
        public async Task<JsonResult> GetDataBill()
        {
            try
            {
                var listDoanhThu = new List<float>(12);
                var listNhanVien = new List<int>(12);
                var listMonAn = new List<int>(12);
                listDoanhThu.AddRange(Enumerable.Repeat<float>(0, 12));
				listNhanVien.AddRange(Enumerable.Repeat<int>(0, 12));
				listMonAn.AddRange(Enumerable.Repeat<int>(0, 12));
				var listBill = await _db.HoaDons.Where(x => x.Status).ToListAsync();
                var listEmployee = await _db.TaiKhoans.Where(x => x.Status).ToListAsync();
				var listFood = await _db.MonAns.Where(x => x.Status).ToListAsync();
				foreach (var item in listBill)
                {
                    listDoanhThu[item.NgayTao.Value.Month] += (float)item.TongTien;
                }
				foreach (var item in listEmployee)
				{
					listNhanVien[item.NgayTao.Value.Month]++;
				}
				foreach (var item in listFood)
				{
					listMonAn[item.NgayTao.Value.Month]++;
				}
				return Json(new
                {
                    Result = true,
                    ListDoanhThu = listDoanhThu,
                    ListMonAn = listMonAn,
                    ListNhanVien = listNhanVien
                });
            }
            catch(Exception ex)
            {
				return Json(new
				{
					Result = false
				});
			}
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (loginModel == null) return View(loginModel);
            try
            {
                var entity = await _db.TaiKhoans.SingleOrDefaultAsync(x => x.UserName == loginModel.UserName && x.Status);
                if (entity == null)
                {
                    ViewBag.Message = "Tên đăng nhập không tồn tại";
                    return View(loginModel);
                }
                if (entity.Password != loginModel.Password)
                {
					ViewBag.Message = "Mật khẩu không chính xác";
                    return View(loginModel);
				}
                var role = await  _db.LoaiTaiKhoans.SingleOrDefaultAsync(x => x.MaLoaiTaiKhoan == entity.LoaiTaiKhoanID && x.Status);
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, loginModel.UserName),
                    new Claim(ClaimTypes.Name, entity.HoTen),
                    new Claim(ClaimTypes.Role, role.TenLoai),
                    new Claim("Id", entity.MaTaiKhoan.ToString())
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var princial = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, princial, null);
                return Redirect("/home");
			}
            catch(Exception ex)
            {
                ViewBag.Message = "Đăng nhập thất bại";
                return View(loginModel);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Home/Login");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}