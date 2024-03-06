using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.Views;

namespace OrchardCore.GreenHouse.ViewModels;

public class FlowersShapeViewModel : ShapeViewModel
{
    public IShape Filters { get; set; }

    public IList<IShape> ContentItems { get; } = [];

    public IShape Header { get; set; }

    public FlowersShapeViewModel()
        : base("FlowersShape")  // FlowersShape.cshtml or FlowersShape.Edit.cshtml
    {

    }
}
