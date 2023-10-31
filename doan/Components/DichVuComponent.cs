using Microsoft.AspNetCore.Mvc;
using doan.Models;

namespace doan.Areas.Admin.Components
{
    [ViewComponent(Name = "DichVu")]
    public class DichVuComponent : ViewComponent
    {
        private readonly DataContext _context;

        public DichVuComponent(DataContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var dsdv = (from dv in _context.DichVus
                              where (dv.TrangThai == true)
                              select dv).Take(6).ToList();

            return await Task.FromResult((IViewComponentResult)View("Default", dsdv));
        }
    }
}
