using Lombiq.TrainingDemo.Migrations;
using Lombiq.TrainingDemo.Models;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.Data.Migration;
using OrchardCore.Data;
using OrchardCore.Module.Training.Indexes;
using OrchardCore.Modules;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using Lombiq.TrainingDemo.Drivers;
using OrchardCore.Module.Training.Models;
using OrchardCore.Module.Training.Drivers;
using OrchardCore.Module.Training.Migrations;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.Module.Training.Handlers;
using OrchardCore.Users.Events;
using OrchardCore.Module.Training.Events;
using OrchardCore.Contents.Services;
using OrchardCore.Module.Training.Services;
using OrchardCore.Security.Permissions;
using OrchardCore.Module.Training.Permissions;
using OrchardCore.Navigation;
using OrchardCore.Module.Training.Navigation;
using OrchardCore.ResourceManagement;

namespace OrchardCore.Module.Training;

public class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        // Person Part
        services.AddContentPart<PersonPart>()
         .UseDisplayDriver<PersonPartDisplayDriver>();
        services.AddDataMigration<PersonMigrations>();
        services.AddIndexProvider<PersonPartIndexProvider>();

        // Animal Part
        services.AddContentPart<AnimalPart>()
            .UseDisplayDriver<AnimalPartDisplayDriver>();
        services.AddDataMigration<AnimalMigrations>();
        services.AddIndexProvider<AnimalPartIndexProvider>();


        // Dog Part
        //this is all one service
        services.AddContentPart<DogPart>()
            .UseDisplayDriver<DogPartDisplayDriver>()
            .AddHandler<DogPartHandler>();
        //this is all one service
        // Still DogPartServices
        services.AddDataMigration<DogMigrations>();
        services.AddIndexProvider<DogPartIndexProvider>();
        services.AddScoped<ILoginFormEvent, LoginGreeting>();
        services.AddScoped<IContentPartHandler, DogPartHandler>();

        //dog Permissions
        services.AddScoped<IPermissionProvider, DogPagePermissions>();
        //dog Permissions
        services.AddScoped<DogAdminListFilter>();


        // dog age Filter admin View
        services.AddTransient<IContentsAdminListFilterProvider, DogAdminListFilter>();

        // dog age Filter admin View

        //Navigation Menu
        services.AddScoped<INavigationProvider, AdminMenu>();
        //Navigation Menu



    }
}
