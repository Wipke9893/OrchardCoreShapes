using OrchardCore.ContentManagement;
using OrchardCore.GreenHouse.Models;
using YesSql.Indexes;

namespace OrchardCore.GreenHouse.Indexes
{
    public class ReviewPartIndex : MapIndex
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Review { get; set; }
        public int Rating { get; set; }
        public string ContentItemId { get; set; }
    }

    public class ReviewPartIndexProvider : IndexProvider<ContentItem>
    {
        public override void Describe(DescribeContext<ContentItem> context)
        {
            context.For<ReviewPartIndex>()
                .Map(contentItem =>
                {
                    var reviewPart = contentItem.As<ReviewPart>();

                    if (reviewPart is null)
                    {
                        return null;
                    }

                    return new ReviewPartIndex
                    {
                        ContentItemId = contentItem.ContentItemId,
                        Name = reviewPart.Name,
                        Email = reviewPart.Email,
                        Review = reviewPart.Review,
                        Rating = reviewPart.Rating
                    };
                });
        }
    }
}
