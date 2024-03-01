using OrchardCore.ContentManagement;
using OrchardCore.Module.Training.Models;
using YesSql.Indexes;

namespace OrchardCore.Module.Training.Indexes;

public class AnimalPartIndex : MapIndex
{
    public string ContentItemId { get; set; }
    public string Species { get; set; }
    public string Habitat { get; set; }
}

public class AnimalPartIndexProvider : IndexProvider<ContentItem>
{
    public override void Describe(DescribeContext<ContentItem> context)
        => context.For<AnimalPartIndex>()
            .Map(contentItem =>

            {
                //this is inserting the AnimalPartIndex into the database
                var animalPart = contentItem.As<AnimalPart>();
                if (animalPart is null)
                {
                    return null;
                }
                return new AnimalPartIndex
                {
                    ContentItemId = contentItem.ContentItemId,
                    Species = animalPart.Species,
                    Habitat = animalPart.Habitat,
                };
            });

}

