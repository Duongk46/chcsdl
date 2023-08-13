using Microsoft.AspNetCore.Mvc;
using project.Entities;

namespace project.Views.Shared.Components.Header
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly QuanAnContext _db;
        public HeaderViewComponent(QuanAnContext quanAnContext)
        {
            _db = quanAnContext;
        }
        public IViewComponentResult Invoke()
        {

            return View();
        }
    }
}
