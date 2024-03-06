using Microsoft.AspNetCore.Mvc.Rendering;

namespace OrchardCore.GreenHouse.ViewModels;
public class VegetableListViewModel
{
    public string SelectedVegetable { get; set; }
    public IEnumerable<SelectListItem> FruitTypeOptions { get; set; }
}
