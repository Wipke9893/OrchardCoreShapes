using Microsoft.Extensions.Localization;
using OrchardCore.Module.Training.Permissions;
using OrchardCore.Navigation;
using System;
using System.Threading.Tasks;


namespace OrchardCore.Module.Training.Navigation;
public class AdminMenu : INavigationProvider
{
    private readonly IStringLocalizer T;

    public AdminMenu(IStringLocalizer<AdminMenu> localizer)
    {
        T = localizer;
    }

    public Task BuildNavigationAsync(string name, NavigationBuilder builder)
    {
        if (!string.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
            return Task.CompletedTask;
        {
            builder
           .Add(T["Dog Training"], "5", item => item
           // the name is Dog Training
           // 5 spots down from the top
           // AllDogs API // Kennel is the controller
           // Area is the module
           .Action("AllDogs", "Kennel", new { area = "OrchardCore.Module.Training" })
           .Permission(DogPagePermissions.ManageDogs));
            return Task.CompletedTask;
        }
    }
}

/*builder
    .Add(T["Dog Training"], "5", training => training
        .Action("AllDogs", "Kennel", new { area = "OrchardCore.Module.Training" })
        .Permission(DogPagePermissions.ManageDogs)
        .Add(T["Add Dog"], "1", add => add
            .Action("AddDog", "Kennel", new { area = "OrchardCore.Module.Training" })
            .Permission(DogPagePermissions.ManageDogs)
        )
        .Add(T["Training Sessions"], "2", sessions => sessions
            .Action("Sessions", "Kennel", new { area = "OrchardCore.Module.Training" })
            .Permission(DogPagePermissions.ManageDogs)
        )
    );*/

