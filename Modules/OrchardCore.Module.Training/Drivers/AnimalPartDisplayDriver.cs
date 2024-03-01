using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Module.Training.Models;
using OrchardCore.Module.Training.ViewModels;

namespace OrchardCore.Module.Training.Drivers;

public class AnimalPartDisplayDriver : ContentPartDisplayDriver<AnimalPart>
{
    public override IDisplayResult Display(AnimalPart part, BuildPartDisplayContext context) =>
         Initialize<AnimalPartViewModel>("AnimalPart", model =>
          {
              model.Species = part.Species;
              model.Habitat = part.Habitat;
          })
        .Location("Detail", "Content:1")
        .Location("SummaryAdmin", "Meta:1")
        .Location("Summary", "Content:1");

    public override IDisplayResult Edit(AnimalPart part, BuildPartEditorContext context) =>
        Initialize<AnimalPartViewModel>(GetEditorShapeType(context), viewModel => PopulateViewModel(part, viewModel));

    public override async Task<IDisplayResult> UpdateAsync(AnimalPart part, IUpdateModel updater, UpdatePartEditorContext context)
    {
        var viewModel = new AnimalPartViewModel();

        await updater.TryUpdateModelAsync(viewModel, Prefix);

        part.Species = viewModel.Species;
        part.Habitat = viewModel.Habitat;

        return Edit(part, context);
    }

    private static void PopulateViewModel(AnimalPart part, AnimalPartViewModel viewModel)
    {
        viewModel.AnimalPart = part;
        viewModel.Species = part.Species;
        viewModel.Habitat = part.Habitat;
    }
}


