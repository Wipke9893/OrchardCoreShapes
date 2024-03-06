using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.DisplayManagement.ModelBinding;
using YesSql;
using OrchardCore.GreenHouse.Models;
using OrchardCore.DisplayManagement;
using OrchardCore.GreenHouse.ViewModels;
using OrchardCore.ContentManagement.Records;

namespace OrchardCore.GreenHouse.Controllers;

public class VegetableController : Controller
{
    private readonly ISession _session;
    private readonly IContentManager _contentManager;
    private readonly IContentItemDisplayManager _contentItemDisplayManager;
    private readonly IUpdateModelAccessor _updateModelAccessor;
    private readonly IContentDefinitionManager _contentDefinitionManager;

    private readonly IDisplayManager<VegetableFilter> _vegetableFilterDisplayManager;

    public VegetableController(
       ISession session,
       IContentManager contentManager,
       IContentItemDisplayManager contentItemDisplayManager,
       IUpdateModelAccessor updateModelAccessor,
       IContentDefinitionManager contentDefinitionManager,

       IDisplayManager<VegetableFilter> vegetableDisplayManager)
    {
        _session = session;
        _contentManager = contentManager;
        _contentItemDisplayManager = contentItemDisplayManager;
        _updateModelAccessor = updateModelAccessor;
        _contentDefinitionManager = contentDefinitionManager;

        _vegetableFilterDisplayManager = vegetableDisplayManager;
    }
    // if you use [HTTPGET] it will never use it as a Post even if it has the power to do so.
    [Route("Vegetables")]
    public async Task<IActionResult> Vegetables()
    {
        var filters = new VegetableFilter();

        var model = new VegetableShapeViewModel()
        {
            Filters = await _vegetableFilterDisplayManager.UpdateEditorAsync(filters, _updateModelAccessor.ModelUpdater, false, string.Empty, string.Empty),
        };

        var query = _session.Query<ContentItem>();

        if (filters.Conditions.Count > 0)
        {
            query = query.All(filters.Conditions.ToArray());
        }

        query = query.With<ContentItemIndex>(x => x.ContentType == "Vegetable");

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
