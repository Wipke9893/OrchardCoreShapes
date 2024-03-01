using Microsoft.AspNetCore.Mvc.Rendering;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.GreenHouse.Models;
using OrchardCore.GreenHouse.ViewModels;

namespace OrchardCore.GreenHouse.Drivers;
public class VegetablePartDisplayDriver : ContentPartDisplayDriver<VegetablePart>
{
    public override IDisplayResult Display(VegetablePart part, BuildPartDisplayContext context) =>
        Initialize<VegetablePartViewModel>(GetDisplayShapeType(context), viewModel =>
        {
            PopulateViewModel(part, viewModel);
        })
        .Location("Detail", "Content:2")
        .Location("SummaryAdmin", "Meta:10")
        .Location("Summary", "Content:3");

    public override IDisplayResult Edit(VegetablePart part, BuildPartEditorContext context) =>
       Initialize<VegetablePartViewModel>(GetEditorShapeType(context), viewModel =>
       {
           PopulateViewModel(part, viewModel);
           viewModel.SunlightTypes =
         [
          new SelectListItem { Text = "Full Sun", Value = "Full Sun" },
            new SelectListItem { Text = "Partial Sun", Value = "Partial Sun" },
            new SelectListItem { Text = "Full Shade", Value = "Full Shade" },
            new SelectListItem { Text = "Partial Shade", Value = "Partial Shade" },
           ];

           viewModel.TemperatureTypes =
           [
            new SelectListItem { Text = "Cold", Value = "Cold" },
            new SelectListItem { Text = "Cool", Value = "Cool" },
            new SelectListItem { Text = "Warm", Value = "Warm" },
            new SelectListItem { Text = "Hot", Value = "Hot" },
           ];
       });

    public override async Task<IDisplayResult> UpdateAsync(VegetablePart part, IUpdateModel updater, UpdatePartEditorContext context)
    {
        var viewModel = new VegetablePartViewModel();

        if (await updater.TryUpdateModelAsync(viewModel, Prefix))
        {
            part.ScientificName = viewModel.ScientificName;
            part.CommonName = viewModel.CommonName;
            part.SunlightType = viewModel.SunlightType;
            part.FruitType = viewModel.FruitType;
            part.LeafType = viewModel.LeafType;
            part.StemType = viewModel.StemType;
            part.RootType = viewModel.RootType;
            part.SoilType = viewModel.SoilType;
            part.WaterType = viewModel.WaterType;
            part.HumidityType = viewModel.HumidityType;
            part.TemperatureType = viewModel.TemperatureType;
            part.FertilizerType = viewModel.FertilizerType;
            part.PesticideType = viewModel.PesticideType;
            part.DiseaseType = viewModel.DiseaseType;
            part.PestType = viewModel.PestType;

        }
        return Edit(part, context);
    }

    private void PopulateViewModel(VegetablePart part, VegetablePartViewModel viewModel)
    {
        viewModel.VegetablePart = part;
        viewModel.ScientificName = part.ScientificName;
        viewModel.CommonName = part.CommonName;
        viewModel.SunlightType = part.SunlightType;
        viewModel.FruitType = part.FruitType;
        viewModel.LeafType = part.LeafType;
        viewModel.StemType = part.StemType;
        viewModel.RootType = part.RootType;
        viewModel.SoilType = part.SoilType;
        viewModel.WaterType = part.WaterType;
        viewModel.HumidityType = part.HumidityType;
        viewModel.TemperatureType = part.TemperatureType;
        viewModel.FertilizerType = part.FertilizerType;
        viewModel.PesticideType = part.PesticideType;
        viewModel.DiseaseType = part.DiseaseType;
        viewModel.PestType = part.PestType;
        viewModel.ContentItemId = part.ContentItem.ContentItemId;

    }

}
