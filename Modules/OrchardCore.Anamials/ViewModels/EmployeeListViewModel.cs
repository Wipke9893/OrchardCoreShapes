using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OrchardCore.GreenHouse.ViewModels;
public class EmployeeListViewModel
{
    public List<EmployeePartViewModel> Employees { get; set; } = new List<EmployeePartViewModel>();
    public string SelectedEmployee { get; set; }
    public IEnumerable<SelectListItem> CountryOptions { get; set; }
}
