using Microsoft.AspNetCore.Mvc;
using doan.Models;

namespace doan.Areas.Admin.Components
{
    [ViewComponent(Name = "Menu")]
    public class MenuComponent : ViewComponent
    {
        private readonly DataContext _context;

        public MenuComponent(DataContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var mnlist = (from mn in _context.Menus
                              where (mn.TrangThai == true)
                              select mn).ToList();

            return await Task.FromResult((IViewComponentResult)View("Default", mnlist));
        }
    }
}
