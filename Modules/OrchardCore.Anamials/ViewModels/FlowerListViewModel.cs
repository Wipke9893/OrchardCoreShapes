using Microsoft.AspNetCore.Mvc.Rendering;

namespace OrchardCore.GreenHouse.ViewModels;
public class FlowerListViewModel
{
    public List<FlowerPartViewModel> Flowers { get; set; } = new List<FlowerPartViewModel>();
    public string SelectedFlower { get; set; }
    public IEnumerable<SelectListItem> FlowerOptions { get; set; }
}
