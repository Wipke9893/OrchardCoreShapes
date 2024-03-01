using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OrchardCore.GreenHouse.ViewModels;

public class ColorPartViewModel
{
    [BindNever]
    public List<SelectListItem> ColorOptions { get; set; }

    public string Color { get; set; }
}
