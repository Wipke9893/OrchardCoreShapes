using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentFields.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.GreenHouse.Indexes;
using YesSql.Sql;

namespace OrchardCore.GreenHouse.Migrations;
public class FertilizerTeaMigrations : DataMigration
{
    private readonly IContentDefinitionManager _contentDefinitionManager;

    public FertilizerTeaMigrations(IContentDefinitionManager contentDefinitionManager)
    {
        _contentDefinitionManager = contentDefinitionManager;
    }

    public async Task<int> CreateAsync()
    {
        await _contentDefinitionManager.AlterPartDefinitionAsync("FertilizerTeaPart", builder => builder
        .Attachable()
        .WithDescription("Select your FertilizerTea.")
        .WithField<TextField>("FertilizerTeaType", field => field
        .WithEditor("PredefinedList")
        .WithSettings(new TextFieldPredefinedListEditorSettings()
        {
            Editor = EditorOption.Dropdown,
            Options =
            [
                new ListValueOption(){
                    Name = "Compost",
                    Value = "Compost"
                },
                new ListValueOption(){
                    Name = "Manure",
                    Value = "Manure"
                },
                new ListValueOption
                {
                    Name = "Seaweed",
                    Value = "Seaweed"
                },
            ],
        })
      ));

        await SchemaBuilder.CreateMapIndexTableAsync<FertilizerTeaIndex>(table => table
        .Column<string>("ContentType")
        .Column<string>("ContentItemId", column => column.WithLength(26))
        .Column<string>("FertilizerTeaType")
        // only need one column for the FertilizerTeaType to be stored
         );
        return 1;

    }
}
//  contentpartDriver
