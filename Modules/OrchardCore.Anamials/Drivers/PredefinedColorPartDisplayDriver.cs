using Microsoft.AspNetCore.Mvc.Rendering;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.GreenHouse.Models;
using OrchardCore.GreenHouse.ViewModels;

namespace OrchardCore.GreenHouse.Drivers;

public class PredefinedColorPartDisplayDriver : ContentPartDisplayDriver<PredefinedColorPart>
{
    public override IDisplayResult Display(PredefinedColorPart part, BuildPartDisplayContext context) =>
        Initialize<ColorPartViewModel>("ColorPart", model =>
        {
            model.Color = part.Color;
        })
        .Location("Detail", "Content:5")
        .Location("Summary", "Content:5");

    public override IDisplayResult Edit(PredefinedColorPart part, BuildPartEditorContext context) =>
       Initialize<ColorPartViewModel>(GetEditorShapeType(context), viewModel =>
       {
           PopulateViewModel(part, viewModel);

           viewModel.ColorOptions = new List<SelectListItem>
           {
            new SelectListItem { Text = "Black", Value = "Black" },
            new SelectListItem { Text = "White", Value = "White" },
            new SelectListItem { Text = "Red", Value = "Red" },
            new SelectListItem { Text = "Green", Value = "Green" },
            new SelectListItem { Text = "Blue", Value = "Blue" }
           };
       });


    public override async Task<IDisplayResult> UpdateAsync(PredefinedColorPart part, IUpdateModel updater, UpdatePartEditorContext context)
    {
        var viewModel = new ColorPartViewModel();
        if (await updater.TryUpdateModelAsync(viewModel, Prefix))
        {
            part.Color = viewModel.Color;
        }
        return Edit(part, context);
    }

    private void PopulateViewModel(PredefinedColorPart part, ColorPartViewModel viewModel)
    {
        viewModel.Color = part.Color;
    }
}

