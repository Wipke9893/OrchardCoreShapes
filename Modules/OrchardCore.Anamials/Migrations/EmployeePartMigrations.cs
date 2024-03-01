using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.GreenHouse.Indexes;
using YesSql.Sql;

namespace OrchardCore.GreenHouse.Migrations;
public class EmployeePartMigrations : DataMigration
{
    private readonly IContentDefinitionManager _contentDefinitionManager;

    public EmployeePartMigrations(IContentDefinitionManager contentDefinitionManager)
    {
        _contentDefinitionManager = contentDefinitionManager;
    }

    public async Task<int> CreateAsync()
    {
        await SchemaBuilder.CreateMapIndexTableAsync<EmployeePartIndex>(table => table
           .Column<string>("FirstName", column => column.WithLength(50))
           .Column<string>("LastName", column => column.WithLength(50))
           .Column<string>("Email", column => column.WithLength(50))
           .Column<string>("Phone", column => column.WithLength(50))
           .Column<string>("Address", column => column.WithLength(50))
           .Column<string>("City", column => column.WithLength(50))
           .Column<string>("State", column => column.WithLength(50))
           .Column<int>("Zip", column => column.WithLength(7))
           .Column<string>("Country", column => column.WithLength(50))
           .Column<string>("ContentItemId", column => column.WithLength(26))
         );
        await _contentDefinitionManager.AlterTypeDefinitionAsync("Employee", type => type
         .Creatable()
         .Listable()
        );
        // need to run a update to get the content part added to the content type
        // Attach EmployeePart to a new content type named "Employee"
        /*    _contentDefinitionManager.AlterTypeDefinition("Employee", type => type
                .WithPart("EmployeePart") // This attaches the EmployeePart to the Employee content type
                .Creatable() // Makes the content type creatable from the admin dashboard
                .Listable() // Makes the content type listable in the admin dashboard
            );*/

        return 1;
    }

    public async Task<int> UpdateFrom1()
    {
        await _contentDefinitionManager.AlterPartDefinitionAsync("EmployeePart", part => part
            .WithDisplayName("Employee Part")
            .WithField<TextField>("FirstName", field => field
                .WithDisplayName("First Name")
                .WithPosition("1")
            )
        );

        await _contentDefinitionManager.AlterTypeDefinitionAsync("Employee", type => type
            .WithPart("EmployeePart") // Attach the EmployeePart to the Employee content type
            .Creatable()
            .Listable()
        );

        return 2;
    }
}

//ContentPartSettingsDisplayDriver
