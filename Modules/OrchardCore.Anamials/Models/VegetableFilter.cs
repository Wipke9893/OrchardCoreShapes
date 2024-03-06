using OrchardCore.ContentManagement;
using YesSql;

namespace OrchardCore.GreenHouse.Models;
public class VegetableFilter
{

    // A list of conditions where each condition is a function that takes an IQuery<ContentItem> and returns an IQuery<ContentItem>.
    // This allows for adding any number of conditions to filter ContentItem queries.
    public List<Func<IQuery<ContentItem>, IQuery<ContentItem>>> Conditions { get; } = [];
}
