using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.ContentManagement;
using OrchardCore.Data.Migration;
using OrchardCore.Data;
using OrchardCore.Modules;
using OrchardCore.GreenHouse.Models;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.GreenHouse.Drivers;
using OrchardCore.GreenHouse.Migrations;
using OrchardCore.GreenHouse.Indexes;
using OrchardCore.GreenHouse.Handlers;
using OrchardCore.Navigation;
using OrchardCore.GreenHouse.Navigation;
using OrchardCore.Contents.Services;
using OrchardCore.GreenHouse.Services;
using OrchardCore.DisplayManagement.Handlers;

namespace OrchardCore.GreenHouse;

public class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        // VegetablePart
        //this is all one service
        services.AddContentPart<VegetablePart>()
            .UseDisplayDriver<VegetablePartDisplayDriver>()
        .AddHandler<VegetablePartHandler>();
        //this is all one service
        services.AddDataMigration<VegetableMigrations>();
        services.AddIndexProvider<VegetablePartIndexProvider>();
        services.AddScoped<IContentPartHandler, VegetablePartHandler>();
        services.AddScoped<IContentPartDisplayDriver, VegetablePartDisplayDriver>();
        // VegetablePart

        //FlowerPart
        //this is all one service
        services.AddContentPart<FlowerPart>()
    .UseDisplayDriver<FlowerPartDisplayDriver>()
    .AddHandler<FlowerPartHandler>();
        //this is all one service
        //FlowerPart
        services.AddDataMigration<FlowerMigrations>();
        services.AddIndexProvider<FlowerPartIndexProvider>();
        services.AddScoped<IContentPartHandler, FlowerPartHandler>();
        services.AddScoped<IContentPartDisplayDriver, FlowerPartDisplayDriver>();
        //FlowerPart

        //EmployeePart
        //this is all one service
        services.AddContentPart<EmployeePart>()
            .UseDisplayDriver<EmployeePartDisplayDriver>()
            .AddHandler<EmployeePartHandler>();
        //this is all one service
        services.AddDataMigration<EmployeePartMigrations>();
        services.AddIndexProvider<EmployeePartIndexProvider>();
        services.AddScoped<IContentPartHandler, EmployeePartHandler>();
        services.AddScoped<IContentPartDisplayDriver, EmployeePartDisplayDriver>();
        //EmployeePart

        //PredefinedColorPartDisplayDriver
        //this is all one service
        services.AddContentPart<PredefinedColorPart>()
          .UseDisplayDriver<PredefinedColorPartDisplayDriver>();
        //this is all one service
        services.AddDataMigration<PredefinedColorPartMigrations>();
        // PredefinedColorPartDisplayDriver

        //ColorPart
        services.AddDataMigration<ColorPartMigrations>();
        services.AddIndexProvider<ColorPartIndexProvider>();
        services.AddContentPart<ColorPart>();
        services.AddScoped<IDataMigration, ColorPartMigrations>();
        //ColorPart

        //PollinatorPart
        //this is all one service
        services.AddContentPart<PollinatorPart>()
          .UseDisplayDriver<PollinatorPartDisplayDriver>();
        //this is all one service
        services.AddDataMigration<PollinatorPartMigrations>();
        services.AddIndexProvider<PollinatorPartIndexProvider>();
        //PollinatorPart

        // CarPart
        services.AddDataMigration<CarPartMigrations>();
        services.AddIndexProvider<CarPartIndexProvider>();
        //one service
        services.AddContentPart<CarPart>()
            .UseDisplayDriver<CarPartDisplayDriver>();
        // one service
        services.AddScoped<IDataMigration, CarPartMigrations>();

        //CarPart

        // FertilizerTeaPart
        services.AddDataMigration<FertilizerTeaMigrations>();
        services.AddIndexProvider<FertilizerTeaIndexProvider>();
        services.AddContentPart<FertilizerTeaPart>();
        services.AddScoped<IDataMigration, FertilizerTeaMigrations>();
        // FertilizerTeaPart


        //services that can be Universal
        //Navigation Menu
        services.AddScoped<INavigationProvider, AdminMenu>();
        //Navigation Menu

        // AdminListFilter
        services.AddTransient<IContentsAdminListFilterProvider, EmployeeAdminListFilter>();
        services.AddTransient<IContentsAdminListFilterProvider, VegetableAdminListFilter>();
        // AdminListFilter
        //services that can be Universal

        services.AddScoped<IDisplayDriver<FlowerFilter>, TypeFlowerFilterDisplayDriver>();


    }
}


[Feature("OrchardCore.GreenHouse.Status")]
public class StatusStartup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IDisplayDriver<FlowerFilter>, StatusFlowerFilterDisplayDriver>();
    }
}
