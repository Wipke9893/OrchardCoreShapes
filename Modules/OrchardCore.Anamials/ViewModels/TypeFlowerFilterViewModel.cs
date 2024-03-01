using Microsoft.AspNetCore.Mvc.Rendering;

namespace OrchardCore.GreenHouse.ViewModels;

public class TypeFlowerFilterViewModel
{
    public string Type { get; set; }

    public IEnumerable<SelectListItem> Types { get; set; }
}
