using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.Views;

namespace OrchardCore.GreenHouse.ViewModels;
public class VegetableShapeViewModel : ShapeViewModel
{
    public IShape Filters { get; set; }

    public IList<IShape> ContentItems { get; } = [];

    public IShape Header { get; set; }

    public VegetableShapeViewModel()
        : base("VegetableShape")  // VegetableShape.cshtml or VegetableShape.Edit.cshtml
    {

    }
}
