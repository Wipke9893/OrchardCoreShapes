using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;

namespace OrchardCore.GreenHouse.Migrations;

public class PredefinedColorPartMigrations : DataMigration
{
    private readonly IContentDefinitionManager _contentDefinitionManager;

    public PredefinedColorPartMigrations(IContentDefinitionManager contentDefinitionManager)
    {
        _contentDefinitionManager = contentDefinitionManager;
    }

    public async Task<int> CreateAsync()
    {
        await _contentDefinitionManager.AlterPartDefinitionAsync("PredefinedColorPart", builder => builder
             .Attachable()
             .WithDescription("Predefined Color selection for content items."));

        return 1;
    }
}
