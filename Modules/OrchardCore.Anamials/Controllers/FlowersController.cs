using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement.Display;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement;
using OrchardCore.GreenHouse.Indexes;
using OrchardCore.GreenHouse.Models;
using OrchardCore.GreenHouse.ViewModels;
using YesSql;
using OrchardCore.ContentManagement.Records;

namespace OrchardCore.GreenHouse.Controllers;

public class FlowersController : Controller
{
    private readonly ISession _session;
    private readonly IContentManager _contentManager;
    private readonly IContentItemDisplayManager _contentItemDisplayManager;
    private readonly IUpdateModelAccessor _updateModelAccessor;
    private readonly IDisplayManager<FlowerFilter> _flowerFilterDisplayManager;
    private readonly IContentDefinitionManager _contentDefinitionManager;

    public FlowersController(
       ISession session,
       IContentManager contentManager,
       IContentItemDisplayManager contentItemDisplayManager,
       IUpdateModelAccessor updateModelAccessor,
       IDisplayManager<FlowerFilter> flowerDisplayManager,
       IContentDefinitionManager contentDefinitionManager)
    {
        _session = session;
        _contentManager = contentManager;
        _contentItemDisplayManager = contentItemDisplayManager;
        _updateModelAccessor = updateModelAccessor;
        _flowerFilterDisplayManager = flowerDisplayManager;
        _contentDefinitionManager = contentDefinitionManager;
    }

    [Route("Flowers")]
    public async Task<IActionResult> Index()
    {
        var filters = new FlowerFilter();
        var model = new FlowersShapeViewModel()
        {
            Filters = await _flowerFilterDisplayManager.UpdateEditorAsync(filters, _updateModelAccessor.ModelUpdater, false, string.Empty, string.Empty),
        };

        var query = _session.Query<ContentItem>();

        if (filters.Conditions.Count > 0)
        {
            query = query.All(filters.Conditions.ToArray());
        }

        query = query.With<ContentItemIndex>(x => x.ContentType == "Flower");

        var contentItems = await query.ListAsync();

        if (contentItems.Any())
        {
            var firstItems = contentItems.First();

            // model.Header = await _contentItemDisplayManager.BuildDisplayAsync(firstItems, _updateModelAccessor.ModelUpdater, "Summary");

            foreach (var contentItem in contentItems)
            {
                model.ContentItems.Add(await _contentItemDisplayManager.BuildDisplayAsync(contentItem, _updateModelAccessor.ModelUpdater, "Summary"));
            }
        }

        return View(model);
    }
}
