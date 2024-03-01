using OrchardCore.ContentManagement;
using OrchardCore.GreenHouse.Models;
using YesSql.Indexes;

namespace OrchardCore.GreenHouse.Indexes;
public class EmployeePartIndex : MapIndex
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
}

public class EmployeePartIndexProvider : IndexProvider<ContentItem>
{
    public override void Describe(DescribeContext<ContentItem> context)
    {
        context.For<EmployeePartIndex>()
            .Map(contentItem =>
            {
                var employeePart = contentItem.As<EmployeePart>();
                if (employeePart is null)
                {
                    return null;
                }
                return new EmployeePartIndex
                {
                    ContentItemId = contentItem.ContentItemId,
                    FirstName = employeePart.FirstName,
                    LastName = employeePart.LastName,
                    Email = employeePart.Email,
                    Phone = employeePart.Phone,
                    Address = employeePart.Address,
                    City = employeePart.City,
                    State = employeePart.State,
                    Zip = employeePart.Zip,
                    Country = employeePart.Country
                };
            });
    }
}
