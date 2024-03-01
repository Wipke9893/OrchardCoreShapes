using OrchardCore.GreenHouse.Models;
using YesSql.Indexes;

namespace OrchardCore.GreenHouse.Indexes;
public class PollinatorPartIndex : MapIndex
{
    public string Pollinator { get; set; }
}

public class PollinatorPartIndexProvider : IndexProvider<PollinatorPart>
{
    public override void Describe(DescribeContext<PollinatorPart> context)
    {
        context.For<PollinatorPartIndex>()
            .Map(PollinatorPart =>
            {
                return new PollinatorPartIndex
                {
                    Pollinator = PollinatorPart.Pollinator
                };
            });
    }
}
