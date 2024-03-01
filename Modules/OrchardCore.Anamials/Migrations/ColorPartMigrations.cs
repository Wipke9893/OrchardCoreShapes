using OrchardCore.ContentFields.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.GreenHouse.Indexes;
using YesSql.Sql;
using OrchardCore.ContentFields.Fields;

namespace OrchardCore.GreenHouse.Migrations;

public class ColorPartMigrations : DataMigration
{
    private readonly IContentDefinitionManager _contentDefinitionManager;

    public ColorPartMigrations(IContentDefinitionManager contentDefinitionManager)
    {
        _contentDefinitionManager = contentDefinitionManager;
    }

    public async Task<int> CreateAsync()
    {
        await _contentDefinitionManager.AlterPartDefinitionAsync("ColorPart", builder => builder
             .Attachable()
             .WithDescription("Color selection for content items.")
             .WithField<TextField>("Color", field => field
                .WithEditor("PredefinedList")
                .WithSettings(new TextFieldPredefinedListEditorSettings()
                {
                    Editor = EditorOption.Dropdown,
                    Options =
                    [
                        new ListValueOption(){
                            Name = "Black",
                            Value = "black"
                        },
                        new ListValueOption(){
                            Name = "White",
                            Value = "White"
                        },
                        new ListValueOption
                        {
                            Name = "Red",
                            Value = "Red"
                        },
                    ],
                })
             ));

        await SchemaBuilder.CreateMapIndexTableAsync<ColorPartIndex>(table => table
                    .Column<string>("ContentType")
                    .Column<string>("ContentItemId", column => column.WithLength(26))
                    .Column<string>("Color")
                );

        return 2;
    }

    public async Task<int> UpdateFrom1Async()
    {
        await _contentDefinitionManager.AlterPartDefinitionAsync("ColorPart", builder => builder
             .Attachable()
             .WithDescription("Color selection for content items.")
             .WithField<TextField>("Color", field => field
                .WithEditor("PredefinedList")
                .WithSettings(new TextFieldPredefinedListEditorSettings()
                {
                    Editor = EditorOption.Dropdown,
                    Options =
                    [
                        new ListValueOption(){
                            Name = "Black",
                            Value = "black"
                        },
                        new ListValueOption(){
                            Name = "White",
                            Value = "White"
                        },
                        new ListValueOption
                        {
                            Name = "Red",
                            Value = "Red"
                        },
                    ],
                }
             )));

        await SchemaBuilder.CreateMapIndexTableAsync<ColorPartIndex>(table => table
                    .Column<string>("ContentType")
                    .Column<string>("ContentItemId", column => column.WithLength(26))
                    .Column<string>("Color")
                );

        return 2;
    }
}
