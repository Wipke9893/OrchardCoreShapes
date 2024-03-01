using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace OrchardCore.GreenHouse.ViewModels;
public class GreenHouseViewModel
{
    public string SelectedSunlightType { get; set; }
    public IEnumerable<SelectListItem> SunlightTypes { get; set; }
    public List<FlowerPartViewModel> Flowers { get; set; }
    public List<VegetablePartViewModel> Vegetables { get; set; }
}
