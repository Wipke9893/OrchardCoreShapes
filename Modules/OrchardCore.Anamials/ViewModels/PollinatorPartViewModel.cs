using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OrchardCore.GreenHouse.ViewModels;

public class PollinatorPartViewModel
{
    public string Pollinator { get; set; }

    [BindNever]
    public List<SelectListItem> PollinatorTypeOptions { get; set; }
}
