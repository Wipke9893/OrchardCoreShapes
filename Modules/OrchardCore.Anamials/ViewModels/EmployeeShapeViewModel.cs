
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.Views;

namespace OrchardCore.GreenHouse.ViewModels;
public class EmployeeShapeViewModel : ShapeViewModel
{
    public IShape Filters { get; set; }

    public IList<IShape> ContentItems { get; } = [];

    public IShape Header { get; set; }

    public EmployeeShapeViewModel()
        : base("EmployeeShape")  // employeeShape.cshtml or employeeShape.Edit.cshtml
    {

    }
}
