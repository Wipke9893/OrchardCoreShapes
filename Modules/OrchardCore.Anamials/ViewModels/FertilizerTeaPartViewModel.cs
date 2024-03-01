using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OrchardCore.GreenHouse.ViewModels;
public class FertilizerTeaPartViewModel
{
    public string FertilizerTeaType { get; set; }

    [BindNever]
    public List<SelectListItem> FertilizerTeaOptions { get; set; }

}
