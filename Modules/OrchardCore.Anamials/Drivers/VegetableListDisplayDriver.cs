using Microsoft.AspNetCore.Mvc.Rendering;
using OrchardCore.ContentManagement;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.GreenHouse.Indexes;
using OrchardCore.GreenHouse.Models;
using OrchardCore.GreenHouse.ViewModels;
using YesSql;

namespace OrchardCore.GreenHouse.Drivers;
public class VegetableListDisplayDriver : DisplayDriver<VegetableFilter>
{
    private readonly ISession _session;

    public VegetableListDisplayDriver(ISession session)
    {
        _session = session;
    }

    public override IDisplayResult Edit(VegetableFilter model)
    {
        return Initialize<VegetableListViewModel>("VegetableList_Edit", async m => // It is looking for the wrong name here
        {
            var FruitTypesQuery = _session.Query<ContentItem, VegetablePartIndex>();
            //Mikes Code
            var FruitTypes = await _session.Query<ContentItem, VegetablePartIndex>().ListAsync();

            m.FruitTypeOptions = FruitTypes.Select(x => x.As<VegetablePart>().FruitType)
                .Distinct()
                .Select(x => new SelectListItem(x, x))
                .ToList();
        }).Location("Body:5");
    }

    public override async Task<IDisplayResult> UpdateAsync(VegetableFilter model, IUpdateModel updater)
    {
        var viewModel = new VegetableListViewModel();

        if (await updater.TryUpdateModelAsync(viewModel, Prefix) && !string.IsNullOrEmpty(viewModel.SelectedVegetable))
        {
            model.Conditions.Add(x => x.With<VegetablePartIndex>(y => y.FruitType == viewModel.SelectedVegetable));
        }

        return Edit(model);
    }
}
