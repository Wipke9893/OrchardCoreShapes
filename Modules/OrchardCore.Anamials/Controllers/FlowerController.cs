using OrchardCore.ContentManagement.Display;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement;
using OrchardCore.DisplayManagement.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using YesSql;
using OrchardCore.GreenHouse.ViewModels;
using OrchardCore.GreenHouse.Models;
using OrchardCore.GreenHouse.Indexes;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrchardCore.ContentManagement.Records;
using OrchardCore.DisplayManagement;


namespace OrchardCore.GreenHouse.Controllers;

public class FlowerController : Controller
{
    private readonly ISession _session;
    private readonly IContentManager _contentManager;
    private readonly IContentItemDisplayManager _contentItemDisplayManager;
    private readonly IUpdateModelAccessor _updateModelAccessor;
    private readonly IContentDefinitionManager _contentDefinitionManager;

    private readonly IDisplayManager<FlowerFilter> _flowerFilterDisplayManager;

    public FlowerController(
       ISession session,
       IContentManager contentManager,
       IContentItemDisplayManager contentItemDisplayManager,

       IDisplayManager<FlowerFilter> flowerDisplayManager,

       IUpdateModelAccessor updateModelAccessor,
       IContentDefinitionManager contentDefinitionManager)
    {
        _session = session;
        _contentManager = contentManager;
        _contentItemDisplayManager = contentItemDisplayManager;
        _updateModelAccessor = updateModelAccessor;
        _contentDefinitionManager = contentDefinitionManager;

        _flowerFilterDisplayManager = flowerDisplayManager;

    }

    // if you use [HTTPGET] it will never use it as a Post even if it has the power to do so.
    [Route("FlowersForever")]
    public async Task<IActionResult> FlowersForever()
    {
        // Initialize a new instance of the FlowerFilter class to hold any filter conditions.
        var filters = new FlowerFilter();
        // Create a new instance of the FlowersShapeViewModel.
        // The Filters property of the model is populated using the _flowerFilterDisplayManager.
        // This involves creating an editor shape for the filters which can be displayed in the view.
        var model = new FlowersShapeViewModel()
        {
            Filters = await _flowerFilterDisplayManager.UpdateEditorAsync(filters, _updateModelAccessor.ModelUpdater, false, string.Empty, string.Empty),
        };
        // Start a new query for ContentItem objects.
        var query = _session.Query<ContentItem>();
        // If there are any conditions in the filters (i.e., user has selected some filters),
        // modify the query to include these conditions.
        if (filters.Conditions.Count > 0)
        {
            query = query.All(filters.Conditions.ToArray());
        }
        // Further refine the query to only include ContentItems of ContentType "Flower".
        query = query.With<ContentItemIndex>(x => x.ContentType == "Flower");

        // Execute the query and get the list of content items.
        var contentItems = await query.ListAsync();

        // If there are any content items returned from the query:
        if (contentItems.Any())
        {
            // Loop through each content item.
            foreach (var contentItem in contentItems)
            {
                // For each content item, build its display shape using the _contentItemDisplayManager.
                // This involves creating a shape that can be rendered in the view, using the "Summary" display type.
                // Add this shape to the model's ContentItems list, which will be used in the view to render each content item.
                model.ContentItems.Add(await _contentItemDisplayManager.BuildDisplayAsync(contentItem, _updateModelAccessor.ModelUpdater, "Summary"));
            }
        }
        // Return the constructed model to the view.
        // The view will use this model to render the UI, including any filters and content items.
        return View(model);
    }
}
