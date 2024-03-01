using OrchardCore.GreenHouse.Models;
using YesSql.Indexes;

namespace OrchardCore.GreenHouse.Indexes;

public class PredefinedColorPartIndex : MapIndex
{
    public string Color { get; set; }
}

public class PredefinedColorPartIndexProvider : IndexProvider<PredefinedColorPart>
{
    public override void Describe(DescribeContext<PredefinedColorPart> context)
    {
        context.For<PredefinedColorPartIndex>()
            .Map(colorPart =>
            {
                return new PredefinedColorPartIndex
                {
                    Color = colorPart.Color
                };
            });
    }
}

