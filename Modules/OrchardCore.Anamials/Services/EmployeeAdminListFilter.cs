using OrchardCore.ContentManagement;
using OrchardCore.Contents.Services;
using OrchardCore.GreenHouse.Indexes;
using YesSql;
using YesSql.Filters.Query;

namespace OrchardCore.GreenHouse.Services;
public class EmployeeAdminListFilter : IContentsAdminListFilterProvider
{
    public void Build(QueryEngineBuilder<ContentItem> builder)
    {
        builder
            .WithNamedTerm("FirstName", builder => builder
                .OneCondition((val, query, ctx) =>
                {
                    var context = (ContentQueryContext)ctx;
                    query.With<EmployeePartIndex>(x => x.FirstName == val);
                    return new ValueTask<IQuery<ContentItem>>(query);
                })
            );

        builder
            .WithNamedTerm("Zip", builder => builder
                .OneCondition((val, query, ctx) =>
                {
                    if (int.TryParse(val, out var zipCode))
                    {
                        // If valid apply the filter.
                        query = query.With<EmployeePartIndex>(x => x.Zip == zipCode);
                    }
                    return new ValueTask<IQuery<ContentItem>>(query);
                })
            );
    }
}
