using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentFields.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.Module.Training.Indexes;
using YesSql.Sql;

namespace OrchardCore.Module.Training.Migrations;
public class DogMigrations : DataMigration
{
    private readonly IContentDefinitionManager _contentDefinitionManager;

    public DogMigrations(IContentDefinitionManager contentDefinitionManager)
    {
        _contentDefinitionManager = contentDefinitionManager;
    }

    public async Task<int> CreateAsync()
    {
        await _contentDefinitionManager.AlterPartDefinitionAsync("DogInfoPart", part => part
            .WithField<TextField>("Temperament", field => field
                .WithEditor("TextArea")
                .WithSettings(new TextFieldSettings
                {
                    Hint = "Dog's Temperament",
                }))
        );




        await SchemaBuilder.CreateMapIndexTableAsync<DogPartIndex>(table => table
            .Column<string>("Name", column => column.WithLength(50))
            .Column<string>(nameof(DogPartIndex.Color), column => column.WithLength(50))
            .Column<string>(nameof(DogPartIndex.Breed), column => column.WithLength(50))
            .Column<string>(nameof(DogPartIndex.Size), column => column.WithLength(10))
            .Column<int>(nameof(DogPartIndex.Age)) // Change the type to int
            .Column<bool>(nameof(DogPartIndex.Stripe))
            .Column<string>(nameof(DogPartIndex.ContentItemId), column => column.WithLength(26))
        );

        await _contentDefinitionManager.AlterTypeDefinitionAsync("Dog", type => type
            .Creatable()
            .Listable()
            .WithPart("DogPart")
            .WithPart("DogInfoPart")
        );

        return 2;
    }

    public async Task<int> UpdateFrom1Async()
    {
        await _contentDefinitionManager.AlterPartDefinitionAsync("DogInfoPart", part => part
            .WithField<TextField>("Temperament", field => field
                .WithEditor("TextArea")
                .WithSettings(new TextFieldSettings
                {
                    Hint = "Dog's Temperament",
                }))
        );

        await _contentDefinitionManager.AlterTypeDefinitionAsync("Dog", type => type
            .WithPart("DogInfoPart")
        );

        return 2;
    }
}

