using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using project.Entities;
using project.Models;

namespace project.Controllers
{
    public class BillController : Controller
    {
        private readonly QuanAnContext _db;
        private readonly IConfiguration _configuration;
        public BillController(QuanAnContext quanAnContext, IConfiguration configuration)
        {
            _db = quanAnContext;
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["ListAccount"] = _db.TaiKhoans.ToList();
            var entity = _db.HoaDons.Include(x => x.BanAn).Include(x => x.ThongTinHoaHons).ToList();
            //var deleteEntity = entity.Select(x => x.ThongTinHoaHons.Where(y => y.HoaDonID == x.MaHoaDon));
            return View(entity);
        }
        [HttpGet]
        public IActionResult Infor(Guid id)
        {
            var entity = _db.ThongTinHoaHons.Where(x => x.HoaDonID == id).Include(x => x.MonAn).ToList();
            return View(entity);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteInfor(Guid id)
        {
            try
            {
                var entity = await _db.ThongTinHoaHons.SingleOrDefaultAsync(x => x.MaThongTinHoaDon == id);
                var listEntity = await _db.ThongTinHoaHons.Where(x => x.HoaDonID == entity.HoaDonID).ToListAsync();
                _db.ThongTinHoaHons.Remove(entity);
                await _db.SaveChangesAsync();
                if (listEntity.Count == 1)
                {
					return Json(new
                    {
                        status = true,
                        isReload = true
                    });
                }
                return Json(new
                {
                    status = true,
                    isReload = false
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
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["ListMonAn"] = _db.MonAns.OrderBy(x => x.TenMonAn).ToList();
            ViewData["ListBanAn"] = _db.BanAns.OrderBy(x => x.Ten).ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BillViewModel billViewModel)
        {
            if (billViewModel != null)
            {
                try
                {
                    var listMonAn = await _db.MonAns.Where(x => x.Status).ToListAsync();
                    var newEntity = new HoaDon();
                    float total = 0;
                    newEntity.NgayTao = DateTime.Now;
                    newEntity.CreatedBy = new Guid(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
                    newEntity.BanAnID = billViewModel.BanAnID;
                    newEntity.VAT = billViewModel.VAT;
                    foreach (var item in billViewModel.ObjectDatas)
                    {
                        var newItem = new ThongTinHoaHon();
                        newItem.SoLuong = item.Amount;
                        newItem.MonAnID = item.Item;
                        newEntity.ThongTinHoaHons.Add(newItem);
                        total += listMonAn.Find(x => x.MaMonAn == newItem.MonAnID).Gia * item.Amount;
                    }
                    newEntity.TongTien = total - total * (float)(newEntity.VAT / 100);
                    await _db.HoaDons.AddAsync(newEntity);
                    await _db.SaveChangesAsync();
                    return Json(new
                    {
                        result = true
                    });
                }
                catch (Exception ex)
                {
                    return Json(new
                    {
                        result = false
                    });
                }
            }
            return Json(new
            {
                result = false
            });
        }
        [HttpPost]
        public async Task<JsonResult> Delete(Guid id)
        {
            try
            {
                var check = await _db.HoaDons.SingleOrDefaultAsync(x => x.MaHoaDon == id);
                if (check != null)
                {
                    _db.HoaDons.Remove(check);
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
