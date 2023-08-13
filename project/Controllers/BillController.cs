using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project.Entities;
using project.Models;

namespace project.Controllers
{
	public class BillController : Controller
	{
		private readonly QuanAnContext _db;
		public BillController(QuanAnContext quanAnContext) {
			_db = quanAnContext;
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
			if (entity.Count() == 0)
			{
				_db.Remove(_db.HoaDons.FirstOrDefault(x => x.MaHoaDon == id));
				_db.SaveChanges();
				return Redirect("/Bill");
			}
			return View(entity);
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
                        newItem.Tong = listMonAn.Find(x => x.MaMonAn == newItem.MonAnID).Gia * item.Amount;
                        newEntity.ThongTinHoaHons.Add(newItem);
						total += newItem.Tong;
                    }
					newEntity.TongTien = total - total * (float)(newEntity.VAT/100);
                    await _db.HoaDons.AddAsync(newEntity);
                    await _db.SaveChangesAsync();
                    return Json(new
                    {
                        result = true
                    });
                }
				catch(Exception ex)
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
