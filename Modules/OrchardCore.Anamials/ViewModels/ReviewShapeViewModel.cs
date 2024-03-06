using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.Views;

namespace OrchardCore.GreenHouse.ViewModels
{
    public class ReviewShapeViewModel : ShapeViewModel
    {
        public IShape Filters { get; set; }

        public IList<IShape> ContentItems { get; } = [];

        public IShape Header { get; set; }

        public ReviewShapeViewModel()
            : base("ReviewShape")  // reviewShape.cshtml or reviewShape.Edit.cshtml
        {

        }
    }
}
