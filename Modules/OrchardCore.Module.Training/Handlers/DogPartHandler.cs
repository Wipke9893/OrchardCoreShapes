using OrchardCore.ContentManagement.Handlers;
using OrchardCore.DisplayManagement.Notify;
using OrchardCore.Module.Training.Models;
using Microsoft.Extensions.Localization;


namespace OrchardCore.Module.Training.Handlers;

public class DogPartHandler : ContentPartHandler<DogPart>
{
    private readonly INotifier _notifier;
    private readonly IStringLocalizer<DogPartHandler> T;

    public DogPartHandler(INotifier notifier, IStringLocalizer<DogPartHandler> localizer)
    {
        _notifier = notifier;
        T = localizer;
    }

    public override Task PublishedAsync(PublishContentContext context, DogPart part)
    {
        return Task.CompletedTask;
    }

    public override Task UpdatedAsync(UpdateContentContext context, DogPart part)
    {
        //Show dog name on Content part
        context.ContentItem.DisplayText = part.Name;
        return Task.CompletedTask;
    }

    public override Task LoadingAsync(LoadContentContext context, DogPart part)
    {

        if (part.Age > 5)
        {
            part.HealthStatus = "Needs a check-up";
        }
        else
        {
            part.HealthStatus = "Healthy";
        }

        return Task.CompletedTask;
    }

    // Content Handlers
}

