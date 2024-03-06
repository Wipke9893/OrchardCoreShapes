using Microsoft.AspNetCore.Mvc.Rendering;

namespace OrchardCore.GreenHouse.ViewModels;
public class EmployeeListViewModel
{
    public string SelectedEmployee { get; set; }
    public IEnumerable<SelectListItem> CountryOptions { get; set; }
}
