using Microsoft.Extensions.Localization;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.DisplayManagement.Notify;
using OrchardCore.GreenHouse.Models;

namespace OrchardCore.GreenHouse.Handlers;
public class EmployeePartHandler : ContentPartHandler<EmployeePart>
{
    private readonly INotifier _notifier;
    private readonly IStringLocalizer<VegetablePartHandler> _t;

    public EmployeePartHandler(INotifier notifier, IStringLocalizer<VegetablePartHandler> localizer)
    {
        _notifier = notifier;
        _t = localizer;
    }

    public override Task PublishedAsync(PublishContentContext context, EmployeePart part)
    {
        return Task.CompletedTask;
    }

    public override Task UpdatedAsync(UpdateContentContext context, EmployeePart part)
    {
        context.ContentItem.DisplayText = part.FirstName + " " + part.LastName;
        return Task.CompletedTask;
    }

    public override Task LoadingAsync(LoadContentContext context, EmployeePart part)
    {
        return Task.CompletedTask;
    }

}
