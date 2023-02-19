using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SimpleRazorCRUD.Models.Cars
{
    public class CarFormModel : CarModel
    {
        public List<SelectListItem> DoorsOptions
        {
            get => new()
            {
                new SelectListItem("2", "2"),
                new SelectListItem("3", "3"),
                new SelectListItem("4", "4"),
                new SelectListItem("5", "5")
            };
        }
    }
}
