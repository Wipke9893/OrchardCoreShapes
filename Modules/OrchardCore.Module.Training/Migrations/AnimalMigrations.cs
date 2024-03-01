using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentFields.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.Module.Training.Indexes;
using OrchardCore.Module.Training.Models;


namespace OrchardCore.Module.Training.Migrations;

public class AnimalMigrations : DataMigration
{
    private readonly IContentDefinitionManager _contentDefinitionManager;

    public AnimalMigrations(IContentDefinitionManager contentDefinitionManager) =>
        _contentDefinitionManager = contentDefinitionManager;

    public async Task<int> CreateAsync()
    {
        //Adjusting the field definition to match expected parameters
        await _contentDefinitionManager.AlterPartDefinitionAsync(nameof(AnimalPart), part => part
            .WithField("AnimalType", field => field
                .OfType(nameof(TextField))
                .WithDisplayName("Animal Type")
                .WithEditor("TextArea")
                .WithSettings(new TextFieldSettings
                {
                    Hint = "Type of the animal",
                }))
        );

        await SchemaBuilder.CreateMapIndexTableAsync(typeof(AnimalPartIndex), table => table
            .Column<string>(nameof(AnimalPartIndex.Species), column => column.WithLength(400))
            .Column<string>(nameof(AnimalPartIndex.Habitat), column => column.WithLength(400))
            .Column<string>("ContentItemId", column => column.WithLength(26)),
            "AnimalPartIndex");

        await _contentDefinitionManager.AlterTypeDefinitionAsync("Animal", type => type
            .Creatable()
            .Listable()
            .WithPart(nameof(AnimalPart))
        );

        return 3;
    }


    public async Task<int> UpdateFrom3Async()
    {
        await _contentDefinitionManager.AlterTypeDefinitionAsync("Animal", type => type
                    .WithPart("PredefinedColorPart"));

        return 4;
    }

}

