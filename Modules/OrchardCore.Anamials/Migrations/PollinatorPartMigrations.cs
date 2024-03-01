using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;

namespace OrchardCore.GreenHouse.Migrations;
public class PollinatorPartMigrations : DataMigration
{
    private readonly IContentDefinitionManager _contentDefinitionManager;

    public PollinatorPartMigrations(IContentDefinitionManager contentDefinitionManager)
    {
        _contentDefinitionManager = contentDefinitionManager;
    }

    public async Task<int> CreateAsync()
    {
        await _contentDefinitionManager.AlterPartDefinitionAsync("PollinatorPart", builder => builder
                    .Attachable()
                    .WithDescription("Pollinator selection for content items."));

        return 1;
    }
}
