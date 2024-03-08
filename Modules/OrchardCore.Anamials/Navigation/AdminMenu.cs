using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;

namespace OrchardCore.GreenHouse.Navigation;
public class AdminMenu : INavigationProvider
{
    private readonly IStringLocalizer _t;
    // if this isn't working cuz I changed the T to _t 
    public AdminMenu(IStringLocalizer<AdminMenu> localizer)
    {
        _t = localizer;
    }

    public Task BuildNavigationAsync(string name, NavigationBuilder builder)
    {
        builder
            .Add(_t["Employees"], "6", item => item
            .Action("FilteredEmployees", "Employee", new { area = "OrchardCore.GreenHouse" }));

        return Task.CompletedTask;
    }
}
