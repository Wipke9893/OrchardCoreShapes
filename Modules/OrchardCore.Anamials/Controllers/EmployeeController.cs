using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Records;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.GreenHouse.Models;
using OrchardCore.GreenHouse.ViewModels;
using YesSql;

namespace OrchardCore.GreenHouse.Controllers;
public class EmployeeController : Controller
{
    private readonly ISession _session;
    private readonly IContentManager _contentManager;
    private readonly IContentItemDisplayManager _contentItemDisplayManager;
    private readonly IUpdateModelAccessor _updateModelAccessor;
    private readonly IContentDefinitionManager _contentDefinitionManager;

    private readonly IDisplayManager<EmployeeFilter> _employeeFilterDisplayManager;

    public EmployeeController(
       ISession session,
       IContentManager contentManager,
       IContentItemDisplayManager contentItemDisplayManager,
       IUpdateModelAccessor updateModelAccessor,
       IContentDefinitionManager contentDefinitionManager,

       IDisplayManager<EmployeeFilter> employeeDisplayManager)
    {
        _session = session;
        _contentManager = contentManager;
        _contentItemDisplayManager = contentItemDisplayManager;
        _updateModelAccessor = updateModelAccessor;
        _contentDefinitionManager = contentDefinitionManager;

        _employeeFilterDisplayManager = employeeDisplayManager;
    }

    [Route("Employees")]
    public async Task<IActionResult> FilteredEmployees()
    {
        var filters = new EmployeeFilter();

        var model = new EmployeeShapeViewModel()
        {
            Filters = await _employeeFilterDisplayManager.UpdateEditorAsync(filters, _updateModelAccessor.ModelUpdater, false, string.Empty, string.Empty),
        };

        var query = _session.Query<ContentItem>();

        if (filters.Conditions.Count > 0)
        {
            query = query.All(filters.Conditions.ToArray());
        }

        query = query.With<ContentItemIndex>(x => x.ContentType == "Employee");

        var contentItems = await query.ListAsync();

        if (contentItems.Any())
        {
            foreach (var contentItem in contentItems)
            {
                model.ContentItems.Add(await _contentItemDisplayManager.BuildDisplayAsync(contentItem, _updateModelAccessor.ModelUpdater, "Summary"));
            }
        }
        return View(model);
    }
}

