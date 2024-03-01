using OrchardCore.ContentManagement;
using OrchardCore.Contents.Services;
using OrchardCore.GreenHouse.Indexes;
using YesSql;
using YesSql.Filters.Query;

namespace OrchardCore.GreenHouse.Services;
public class VegetableAdminListFilter : IContentsAdminListFilterProvider
{
    public void Build(QueryEngineBuilder<ContentItem> builder)
    {
        builder
        .WithNamedTerm("CommonName", builder => builder
        .OneCondition((val, query, ctx) =>
          {
              var context = (ContentQueryContext)ctx;
              query.With<VegetablePartIndex>(x => x.CommonName == val);
              return new ValueTask<IQuery<ContentItem>>(query);
          })
        );
    }
}
