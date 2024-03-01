
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

public class TypeFlowerFilterDisplayDriver : DisplayDriver<FlowerFilter>
{
    private readonly ISession _session;

    public TypeFlowerFilterDisplayDriver(ISession session)
    {
        _session = session;
    }

    public override IDisplayResult Edit(FlowerFilter model)
    {
        return Initialize<TypeFlowerFilterViewModel>("TypeFlowerFilter_Edit", async m =>
        {
            var flowerTypes = await _session.Query<ContentItem, FlowerPartIndex>().ListAsync();

            m.Types = flowerTypes.Select(x => x.As<FlowerPart>().FlowerType)
                .Distinct()
                .Select(x => new SelectListItem(x, x))
                .ToList();
        }).Location("Body:5");
    }

    public override async Task<IDisplayResult> UpdateAsync(FlowerFilter model, IUpdateModel updater)
    {
        var viewModel = new TypeFlowerFilterViewModel();

        if (await updater.TryUpdateModelAsync(viewModel, Prefix) && !string.IsNullOrEmpty(viewModel.Type))
        {
            model.Conditions.Add(x => x.With<FlowerPartIndex>(y => y.FlowerType == viewModel.Type));
        }

        return Edit(model);
    }
}
