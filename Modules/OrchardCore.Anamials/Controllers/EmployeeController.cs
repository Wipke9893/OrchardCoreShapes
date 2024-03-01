using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.GreenHouse.Indexes;
using OrchardCore.GreenHouse.Models;
using OrchardCore.GreenHouse.ViewModels;
using YesSql;

namespace OrchardCore.GreenHouse.Controllers;
public class EmployeeController : Controller
{
    private readonly ISession _session;
    private readonly IContentItemDisplayManager _contentItemDisplayManager;
    private readonly IUpdateModelAccessor _updateModelAccessor;

    public EmployeeController(
         ISession session,
         IContentItemDisplayManager contentItemDisplayManager,
         IUpdateModelAccessor updateModelAccessor)
    {
        _session = session;
        _contentItemDisplayManager = contentItemDisplayManager;
        _updateModelAccessor = updateModelAccessor;
    }

    [HttpGet]
    [Route("Employees")]
    public async Task<IActionResult> FilteredEmployees(string EmployeeFilter = null)
    {
        // get the list of employees by countries
        var countryList = await EmployeeCountries();
        var countryOptions = countryList.Select(c => new SelectListItem { Value = c, Text = c }).ToList();

        // query for employees, optionally filtering by country
        var employeeQuery = _session.Query<ContentItem, EmployeePartIndex>();
        if (!string.IsNullOrEmpty(EmployeeFilter))
        {
            // find the employees by country
            employeeQuery = employeeQuery.Where(x => x.Country == EmployeeFilter);
        }

        // fetch the filtered Employees
        var employees = await employeeQuery.ListAsync();
        var employeeViewModels = new List<EmployeePartViewModel>();
        foreach (var employee in employees)
        {
            if (employee.TryGet<EmployeePart>(out var employeePart))
            {
                // build the view model
                var viewModel = new EmployeePartViewModel
                {
                    Shape = await _contentItemDisplayManager.BuildDisplayAsync(employee, _updateModelAccessor.ModelUpdater, "Summary"),
                    ContentItemId = employee.ContentItemId,
                };
                employeeViewModels.Add(viewModel);
            }
        }

        // build the view model
        var listViewModel = new EmployeeListViewModel
        {
            Employees = employeeViewModels,
            SelectedEmployee = EmployeeFilter,
            CountryOptions = countryOptions
        };
        return View(listViewModel);
    }

    [HttpGet]
    public async Task<List<string>> EmployeeCountries()
    {
        // get the list of countries
        var countryList = await _session.Query<ContentItem, EmployeePartIndex>().ListAsync();
        var countryLocations = countryList.Select(x => x.As<EmployeePart>().Country)
            .Distinct()
            .OrderBy(x => x)
            .ToList();

        return countryLocations;
    }
}

