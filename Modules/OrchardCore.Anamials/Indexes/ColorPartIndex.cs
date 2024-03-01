using OrchardCore.ContentManagement;
using OrchardCore.GreenHouse.Models;
using YesSql.Indexes;

namespace OrchardCore.GreenHouse.Indexes;

public class ColorPartIndex : MapIndex
{
    public string ContentType { get; set; }

    public string ContentItemId { get; set; }

    public string Color { get; set; }
}

public class ColorPartIndexProvider : IndexProvider<ContentItem>
{
    public override void Describe(DescribeContext<ContentItem> context)
    {
        context.For<ColorPartIndex>()
            .Map(contentItem =>
            {
                var colorPart = contentItem.As<ColorPart>();
                if (colorPart is not null && colorPart.Color != null)
                {
                    return new ColorPartIndex
                    {
                        ContentItemId = contentItem.ContentItemId,
                        ContentType = contentItem.ContentType,
                        Color = colorPart.Color.Text,
                    };
                }
                else
                {
                    return new ColorPartIndex
                    {
                        ContentItemId = contentItem.ContentItemId,
                        ContentType = contentItem.ContentType,
                        // Set Color to a default value or null if it's nullable
                    };
                }
            });
    }

}
