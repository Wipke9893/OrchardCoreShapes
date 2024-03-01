using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrchardCore.GreenHouse.Models;

namespace OrchardCore.GreenHouse.ViewModels;
public class EmployeePartViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public int Zip { get; set; }
    public string Country { get; set; }
    public string ContentItemId { get; set; }
    public EmployeePart employeePart { get; set; }
    public dynamic Shape { get; set; }

    [BindNever]
    public IEnumerable<SelectListItem> Countries { get; set; }
}
