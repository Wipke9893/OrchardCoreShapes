using Microsoft.AspNetCore.Mvc.Rendering;

namespace OrchardCore.GreenHouse.ViewModels;
public class VegetableListViewModel
{
    public List<VegetablePartViewModel> Vegetables { get; set; } = new List<VegetablePartViewModel>();
    public string SelectedVegetable { get; set; }
    public IEnumerable<SelectListItem> FruitTypeOptions { get; set; }
}
