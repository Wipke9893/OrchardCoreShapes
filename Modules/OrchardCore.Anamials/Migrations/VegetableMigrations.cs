using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentFields.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.GreenHouse.Indexes;
using YesSql.Sql;

namespace OrchardCore.GreenHouse.Migrations;
public class VegetableMigrations : DataMigration
{
    private readonly IContentDefinitionManager _contentDefinitionManager;

    public VegetableMigrations(IContentDefinitionManager contentDefinitionManager)
    {
        _contentDefinitionManager = contentDefinitionManager;
    }

    public async Task<int> CreateAsync()
    {
        await _contentDefinitionManager.AlterPartDefinitionAsync("VegetablePart", part => part
       .WithField<TextField>("GrowType", field => field
       .WithEditor("TextArea")
       .WithSettings(new TextFieldSettings
       {
           Hint = "Vegetable's Grow Type",
       }))
    );


        await SchemaBuilder.CreateMapIndexTableAsync<VegetablePartIndex>(table => table
            .Column<string>("ScientificName", column => column.WithLength(50))
            .Column<string>("CommonName", column => column.WithLength(50))
            .Column<string>("SunlightType", column => column.WithLength(50))
            .Column<string>("FruitType", column => column.WithLength(50))
            .Column<string>("LeafType", column => column.WithLength(50))
            .Column<string>("StemType", column => column.WithLength(50))
            .Column<string>("RootType", column => column.WithLength(50))
            .Column<string>("SoilType", column => column.WithLength(50))
            .Column<string>("WaterType", column => column.WithLength(50))
            .Column<string>("HumidityType", column => column.WithLength(50))
            .Column<string>("TemperatureType", column => column.WithLength(50))
            .Column<string>("FertilizerType", column => column.WithLength(50))
            .Column<string>("PesticideType", column => column.WithLength(50))
            .Column<string>("DiseaseType", column => column.WithLength(50))
            .Column<string>("PestType", column => column.WithLength(50))
            .Column<string>("ContentItemId", column => column.WithLength(26))
        );

        await _contentDefinitionManager.AlterTypeDefinitionAsync("Vegetable", type => type
            .Creatable()
            .Listable()
            .WithPart("VegetablePart")
        );
        return 1;
    }
}
