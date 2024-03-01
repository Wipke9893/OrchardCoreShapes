using OrchardCore.ContentManagement;
using OrchardCore.Module.Training.Models;
using YesSql.Indexes;

namespace OrchardCore.Module.Training.Indexes;

public class DogPartIndex : MapIndex
{
    public string Name { get; set; }
    public string Breed { get; set; }
    public string Color { get; set; }
    public string Size { get; set; }
    public int Age { get; set; }
    public bool Stripe { get; set; }
    public string ContentItemId { get; set; }
}

public class DogPartIndexProvider : IndexProvider<ContentItem>
{
    public override void Describe(DescribeContext<ContentItem> context)
    {
        context.For<DogPartIndex>()
            .Map(contentItem =>
            {
                var dogPart = contentItem.As<DogPart>();
                if (dogPart is null)
                {
                    return null;
                }
                return new DogPartIndex
                {
                    ContentItemId = contentItem.ContentItemId,
                    Name = dogPart.Name,
                    Breed = dogPart.Breed,
                    Color = dogPart.Color,
                    Size = dogPart.Size,
                    Age = dogPart.Age,
                    Stripe = dogPart.Stripe
                };
            });
    }
}
