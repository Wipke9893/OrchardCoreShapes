using Microsoft.Extensions.Localization;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.DisplayManagement.Notify;
using OrchardCore.GreenHouse.Models;

namespace OrchardCore.GreenHouse.Handlers;
public class FlowerPartHandler : ContentPartHandler<FlowerPart>
{

    private readonly INotifier _notifier;
    private readonly IStringLocalizer<FlowerPartHandler> _t;

    public FlowerPartHandler(INotifier notifier, IStringLocalizer<FlowerPartHandler> localizer)
    {
        _notifier = notifier;
        _t = localizer;
    }

    public override Task PublishedAsync(PublishContentContext context, FlowerPart part)
    {
        return Task.CompletedTask;
    }

    public override Task UpdatedAsync(UpdateContentContext context, FlowerPart part)
    {
        context.ContentItem.DisplayText = part.CommonName;
        return Task.CompletedTask;
    }

    public override Task LoadingAsync(LoadContentContext context, FlowerPart part)
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
