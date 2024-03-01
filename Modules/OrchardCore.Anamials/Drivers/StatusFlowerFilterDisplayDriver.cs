using Microsoft.AspNetCore.Mvc.Rendering;
using OrchardCore.ContentManagement.Records;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.GreenHouse.Models;
using OrchardCore.GreenHouse.ViewModels;
using YesSql;

namespace OrchardCore.GreenHouse.Drivers;

public class StatusFlowerFilterDisplayDriver : DisplayDriver<FlowerFilter>
{
    private readonly ISession _session;

    public StatusFlowerFilterDisplayDriver(ISession session)
    {
        _session = session;
    }

    public override IDisplayResult Edit(FlowerFilter model)
    {
        var result = Initialize<StatusFlowerFilterViewModel>("StatusFlowerFilter_Edit", m =>
        {
            m.Statuses = [
                new SelectListItem("Published", "published"),
                new SelectListItem("Draft", "draft"),
                new SelectListItem("Latest", ""),

             ];
        }).Location("Body:4");


        var headerResult = View("StatusFlowerFilterHeader_Edit", model)
            .Location("Header:1");

        var footResult = View("StatusFlowerFilterFooter_Edit", model)
            .Location("Footer:1");

        return Combine(result, headerResult, footResult);
    }

    public override async Task<IDisplayResult> UpdateAsync(FlowerFilter model, IUpdateModel updater)
    {
        var viewModel = new StatusFlowerFilterViewModel();

        if (await updater.TryUpdateModelAsync(viewModel, Prefix))
        {
            if (viewModel.Status == "published")
            {
                model.Conditions.Add(x => x.With<ContentItemIndex>(y => y.Published));
            }
            else if (viewModel.Status == "draft")
            {
                model.Conditions.Add(x => x.With<ContentItemIndex>(y => !y.Published && y.Latest));
            }
            else
            {
                model.Conditions.Add(x => x.With<ContentItemIndex>(y => y.Latest));
            }
        }

        return Edit(model);
    }
}
