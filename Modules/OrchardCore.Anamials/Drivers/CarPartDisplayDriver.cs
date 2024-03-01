using Microsoft.AspNetCore.Mvc.Rendering;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.GreenHouse.Models;
using OrchardCore.GreenHouse.ViewModels;

namespace OrchardCore.GreenHouse.Drivers;
public class CarPartDisplayDriver : ContentPartDisplayDriver<CarPart>
{
    public override IDisplayResult Display(CarPart part, BuildPartDisplayContext context) =>
        Initialize<CarPartViewModel>("CarPart", model =>
        {
            model.CarType = part.CarType;
        })
        .Location("Detail", "Content:5")
        .Location("Summary", "Content:5");

    public override IDisplayResult Edit(CarPart part, BuildPartEditorContext context) =>
        Initialize<CarPartViewModel>(GetEditorShapeType(context), viewModel =>
        {
            PopulateViewModel(part, viewModel);

            viewModel.CarOptions = new List<SelectListItem>
              {
                new SelectListItem { Text = "Sedan", Value = "Sedan" },
                new SelectListItem { Text = "SUV", Value = "SUV" },
                new SelectListItem { Text = "Truck", Value = "Truck" },
                new SelectListItem { Text = "Van", Value = "Van" },
              };
        });

    public override async Task<IDisplayResult> UpdateAsync(CarPart part, IUpdateModel updater, UpdatePartEditorContext context)
    {
        var viewModel = new CarPartViewModel();
        if (await updater.TryUpdateModelAsync(viewModel, Prefix))
        {
            part.CarType = viewModel.CarType;
        }
        return Edit(part, context);
    }

    public void PopulateViewModel(CarPart part, CarPartViewModel viewModel)
    {
        viewModel.CarType = part.CarType;
    }
}
