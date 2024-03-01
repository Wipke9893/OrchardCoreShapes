using System.Linq.Expressions;
using OrchardCore.ContentManagement;
using OrchardCore.Contents.Services;
using OrchardCore.Module.Training.Indexes;
using YesSql;
using YesSql.Filters.Query;

namespace OrchardCore.Module.Training.Services;

public class DogAdminListFilter : IContentsAdminListFilterProvider
{
    public void Build(QueryEngineBuilder<ContentItem> builder)
    {
        builder
      .WithNamedTerm("Age", builder => builder
          .OneCondition((val, query, ctx) =>
          {
              var isRange = val.EndsWith("+") || val.EndsWith("-");
              var numberPart = val.TrimEnd('+', '-');

              if (int.TryParse(numberPart, out var age))
              {
                  Expression<Func<DogPartIndex, bool>> expression = val.EndsWith("+") ?
                      (x => x.Age >= age) :
                      val.EndsWith("-") ?
                      (x => x.Age <= age) :
                      (x => x.Age == age);

                  query.With<DogPartIndex>(expression);
              }

              return new ValueTask<IQuery<ContentItem>>(query);
          })
      );


        /*        builder.WithNamedTerm("Age", builder => builder
        .OneCondition((val, query, ctx) =>
        {
          var isRange = val.EndsWith("+") || val.EndsWith("-");
          var numberPart = val.TrimEnd('+', '-');

          if (int.TryParse(numberPart, out var age))
          {
              if (val.EndsWith("+"))
              {
                    // sort by age in ascending order for "age+" queries
                  query = query.With<DogPartIndex>(x => x.Age >= age).OrderBy(x => x.Age);
              }
              else if (val.EndsWith("-"))
              {
                    // ages less than or equal to specified age without sorting
                  query.With<DogPartIndex>(x => x.Age <= age).OrderBy(x => x.Age);
              }
              else
              {
                    // exact age match without sorting
                  query.With<DogPartIndex>(x => x.Age == age);
              }
          }

          return new ValueTask<IQuery<ContentItem>>(query);
        })
        );*/


        /*builder
            .WithNamedTerm("age", builder => builder
                .OneCondition((val, query, ctx) =>
                {
                    var context = ( )ctx;

                    // age:10
                    // age:10+
                    // age:10-
                    // age:10-20

                    if (val.EndsWith("+") && int.TryParse(val.Substring(0, val.Length - 1), out var minAge))
                    {
                        query.With<DogPartIndex>(x => x.Age >= minAge);
                    }
                    else if (val.EndsWith("-") && int.TryParse(val.Substring(0, val.Length - 1), out var maxAge))
                    {
                        query.With<DogPartIndex>(x => x.Age <= maxAge);
                    }
                    else if (int.TryParse(val, out var age))
                    {
                        query.With<DogPartIndex>(x => x.Age == age);
                    }

                    return new ValueTask<IQuery<ContentItem>>(query);
                })
            );*/
    }
}


