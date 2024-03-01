using OrchardCore.ContentManagement;

namespace OrchardCore.GreenHouse.Models;
public class EmployeePart : ContentPart
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
}

