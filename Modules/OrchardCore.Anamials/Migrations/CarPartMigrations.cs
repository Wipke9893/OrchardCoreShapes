using OrchardCore.ContentFields.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.GreenHouse.Indexes;
using YesSql.Sql;

namespace OrchardCore.GreenHouse.Migrations;
public class CarPartMigrations : DataMigration
{
    private readonly IContentDefinitionManager _contentDefinitionManager;

    public CarPartMigrations(IContentDefinitionManager contentDefinitionManager)
    {
        _contentDefinitionManager = contentDefinitionManager;
    }

    public async Task<int> CreateAsync()
    {
        await _contentDefinitionManager.AlterPartDefinitionAsync("CarPart", builder => builder
       .Attachable()
       .WithDescription("Select your Car.")
       );

        await SchemaBuilder.CreateMapIndexTableAsync<CarPartIndex>(table => table
        .Column<string>("ContentType")
        .Column<string>("ContentItemId", column => column.WithLength(26))
        .Column<string>("CarType", column => column.WithLength(15))
        );

        return 1;
    }
}
