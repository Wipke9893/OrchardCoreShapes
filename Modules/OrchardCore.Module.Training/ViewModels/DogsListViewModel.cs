using Microsoft.AspNetCore.Mvc.Rendering;

namespace OrchardCore.Module.Training.ViewModels
{
    public class DogsListViewModel
    {
        public List<DogPartViewModel> Dogs { get; set; } = new List<DogPartViewModel>();
        public string SelectedColor { get; set; }
        public IEnumerable<SelectListItem> ColorOptions { get; set; }
    }
}
