using OrchardCore.ContentManagement;
using OrchardCore.GreenHouse.Models;
using YesSql.Indexes;

namespace OrchardCore.GreenHouse.Indexes;
public class FertilizerTeaIndex : MapIndex
{
    public string ContentType { get; set; }
    public string ContentItemId { get; set; }
    public string FertilizerTeaType { get; set; }

}

public class FertilizerTeaIndexProvider : IndexProvider<ContentItem>
{
    public override void Describe(DescribeContext<ContentItem> context)
    {
        context.For<FertilizerTeaIndex>()
            .Map(contentItem =>
            {
                var fertilizerTeaPart = contentItem.As<FertilizerTeaPart>();
                if (fertilizerTeaPart != null && fertilizerTeaPart.FertilizerTeaType != null)
                {
                    return new FertilizerTeaIndex
                    {
                        ContentItemId = contentItem.ContentItemId,
                        ContentType = contentItem.ContentType,
                        FertilizerTeaType = fertilizerTeaPart.FertilizerTeaType.Text,
                    };
                }
                else
                {
                    return new FertilizerTeaIndex
                    {
                        ContentItemId = contentItem.ContentItemId,
                        ContentType = contentItem.ContentType,
                        // Set FertilizerTeaType to a default value or null if it's nullable
                    };
                }
            });
    }


}
