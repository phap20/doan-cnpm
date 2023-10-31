using Microsoft.AspNetCore.Mvc;
using doan.Models;

namespace doan.Areas.Admin.Components
{
    [ViewComponent(Name = "Booking")]
    public class BookingComponent : ViewComponent
    {
        private readonly DataContext _context;

        public BookingComponent(DataContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.dsTang = _context.Tangs.ToList();
            ViewBag.dsLoaiPhong = _context.LoaiPhongs.ToList();

            return await Task.FromResult((IViewComponentResult)View("Default"));
        }
    }
}
