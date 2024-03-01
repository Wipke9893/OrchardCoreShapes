using Microsoft.AspNetCore.Mvc.Rendering;

namespace OrchardCore.GreenHouse.ViewModels;

public class StatusFlowerFilterViewModel
{
    public string Status { get; set; }

    public IEnumerable<SelectListItem> Statuses { get; set; }
}
