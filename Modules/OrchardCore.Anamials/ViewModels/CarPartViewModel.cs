
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OrchardCore.GreenHouse.ViewModels;
public class CarPartViewModel
{
    public string CarType { get; set; }

    [BindNever]
    public List<SelectListItem> CarOptions { get; set; }
}
