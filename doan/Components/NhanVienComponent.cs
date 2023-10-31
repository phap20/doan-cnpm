using Microsoft.AspNetCore.Mvc;
using doan.Models;

namespace doan.Areas.Admin.Components
{
    [ViewComponent(Name = "NhanVien")]
    public class NhanVienComponent : ViewComponent
    {
        private readonly DataContext _context;

        public NhanVienComponent(DataContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var mnlist = (from nv in _context.NhanViens
                              select nv).Take(4).ToList();

            return await Task.FromResult((IViewComponentResult)View("Default", mnlist));
        }
    }
}
