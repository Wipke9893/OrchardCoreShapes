using Microsoft.AspNetCore.Mvc.Rendering;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Module.Training.Models;
using OrchardCore.Module.Training.ViewModels;

namespace OrchardCore.Module.Training.Drivers;

public class DogPartDisplayDriver : ContentPartDisplayDriver<DogPart>
{
    public override IDisplayResult Display(DogPart part, BuildPartDisplayContext context) =>
        Initialize<DogPartViewModel>(GetDisplayShapeType(context), viewModel =>
        {
            PopulateViewModel(part, viewModel);
        })
           .Location("Detail", "Content:2") // For detail view
            .Location("SummaryAdmin", "Meta:10") // For admin summary view
            .Location("Summary", "Content:3"); // For front-end summary view


    public override IDisplayResult Edit(DogPart part, BuildPartEditorContext context) =>
        Initialize<DogPartViewModel>(GetEditorShapeType(context), viewModel =>
        {
            PopulateViewModel(part, viewModel);

            viewModel.Colors =
            [
                new SelectListItem { Text = "Black", Value = "Black" },
                new SelectListItem { Text = "White", Value = "White" },
                new SelectListItem { Text = "Brown", Value = "Brown" },
                new SelectListItem { Text = "Grey", Value = "Grey" },
                new SelectListItem { Text = "Yellow", Value = "Yellow" }
            ];
        });

    public override async Task<IDisplayResult> UpdateAsync(DogPart part, IUpdateModel updater, UpdatePartEditorContext context)
    {
        var viewModel = new DogPartViewModel();

        if (await updater.TryUpdateModelAsync(viewModel, Prefix))
        {
            part.Name = viewModel.Name;
            part.Color = viewModel.Color;
            part.Breed = viewModel.Breed;
            part.Size = viewModel.Size;
            part.Age = viewModel.Age;
            part.Stripe = viewModel.Stripe;
        }

        return Edit(part, context);
    }

    private void PopulateViewModel(DogPart part, DogPartViewModel viewModel)
    {
        viewModel.DogPart = part;
        viewModel.Name = part.Name;
        viewModel.Color = part.Color;
        viewModel.Breed = part.Breed;
        viewModel.Size = part.Size;
        viewModel.Age = part.Age;
        viewModel.Stripe = part.Stripe;

        //Handlers
        viewModel.HealthStatus = part.HealthStatus;
    }
}
