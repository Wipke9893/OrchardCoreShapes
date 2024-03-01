using Microsoft.AspNetCore.Mvc.Rendering;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.GreenHouse.Models;
using OrchardCore.GreenHouse.ViewModels;

namespace OrchardCore.GreenHouse.Drivers;
public class PollinatorPartDisplayDriver : ContentPartDisplayDriver<PollinatorPart>
{
    public override IDisplayResult Display(PollinatorPart part, BuildPartDisplayContext context) =>
        Initialize<PollinatorPartViewModel>("PollinatorPart", model =>
        {
            model.Pollinator = part.Pollinator;
        })
        .Location("Detail", "Content:5")
        .Location("Summary", "Content:5");

    public override IDisplayResult Edit(PollinatorPart part, BuildPartEditorContext context) =>
       Initialize<PollinatorPartViewModel>(GetEditorShapeType(context), viewModel =>
       {
           PopulateViewModel(part, viewModel);

           viewModel.PollinatorTypeOptions = new List<SelectListItem>
           {
            new SelectListItem { Text = "Bee", Value = "Bee" },
            new SelectListItem { Text = "Butterfly", Value = "Butterfly" },
            new SelectListItem { Text = "Hummingbird", Value = "Hummingbird" },
            new SelectListItem { Text = "Bat", Value = "Bat" },
            new SelectListItem { Text = "Beetle", Value = "Beetle" }
           };
       });

    public override async Task<IDisplayResult> UpdateAsync(PollinatorPart part, IUpdateModel updater, UpdatePartEditorContext context)
    {
        var viewModel = new PollinatorPartViewModel();
        if (await updater.TryUpdateModelAsync(viewModel, Prefix))
        {
            part.Pollinator = viewModel.Pollinator;
        }
        return Edit(part, context);
    }

    private void PopulateViewModel(PollinatorPart part, PollinatorPartViewModel viewModel)
    {
        viewModel.Pollinator = part.Pollinator;
    }
}
