using Microsoft.AspNetCore.Mvc;
using doan.Models;

namespace doan.Areas.Admin.Components
{
    [ViewComponent(Name = "Slider")]
    public class SliderComponent : ViewComponent
    {
        private readonly DataContext _context;

        public SliderComponent(DataContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var mnlist = (from mn in _context.Sliders
                              where (mn.TrangThai == true)
                              select mn).ToList();

            return await Task.FromResult((IViewComponentResult)View("Default", mnlist));
        }
    }
}
