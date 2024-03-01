using Microsoft.Extensions.Localization;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.DisplayManagement.Notify;
using OrchardCore.GreenHouse.Models;

namespace OrchardCore.GreenHouse.Handlers;
public class VegetablePartHandler : ContentPartHandler<VegetablePart>
{
    private readonly INotifier _notifier;
    private readonly IStringLocalizer<VegetablePartHandler> _t;

    public VegetablePartHandler(INotifier notifier, IStringLocalizer<VegetablePartHandler> localizer)
    {
        _notifier = notifier;
        _t = localizer;
    }

    public override Task PublishedAsync(PublishContentContext context, VegetablePart part)
    {
        return Task.CompletedTask;
    }

    public override Task UpdatedAsync(UpdateContentContext context, VegetablePart part)
    {
        context.ContentItem.DisplayText = part.CommonName;
        return Task.CompletedTask;
    }

    public override Task LoadingAsync(LoadContentContext context, VegetablePart part)
    {
        if (part.SunlightType == "Full Sun")
        {
            part.SunlightType = "Full Sun";
        }
        else
        {
            part.SunlightType = "Partial Sun";
        }

        return Task.CompletedTask;
    }
}
