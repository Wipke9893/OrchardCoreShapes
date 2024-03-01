using OrchardCore.ContentManagement;
using OrchardCore.GreenHouse.Models;
using YesSql.Indexes;

namespace OrchardCore.GreenHouse.Indexes;
public class CarPartIndex : MapIndex
{
    public string ContentType { get; set; }
    public string ContentItemId { get; set; }
    public string CarType { get; set; }
}

public class CarPartIndexProvider : IndexProvider<ContentItem>
{
    public override void Describe(DescribeContext<ContentItem> context)
    {
        context.For<CarPartIndex>()
            .Map(contentItem =>
            {
                var carPart = contentItem.As<CarPart>();
                if (carPart != null && carPart.CarType != null)
                {
                    return new CarPartIndex
                    {
                        ContentItemId = contentItem.ContentItemId,
                        ContentType = contentItem.ContentType,
                        CarType = carPart.CarType,
                    };
                }
                else
                {
                    return new CarPartIndex
                    {
                        ContentItemId = contentItem.ContentItemId,
                        ContentType = contentItem.ContentType,
                        // Set CarType to a default value or null if it's nullable
                    };
                }
            });
    }
}
