using Microsoft.AspNetCore.Mvc;
using doan.Models;

namespace doan.Areas.Admin.Components
{
    [ViewComponent(Name = "Rooms")]
    public class RoomsComponent : ViewComponent
    {
        private readonly DataContext _context;

        public RoomsComponent(DataContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var rooms = _context.Phongs.Take(3).ToList();

            return await Task.FromResult((IViewComponentResult)View("Default", rooms));
        }
    }
}
