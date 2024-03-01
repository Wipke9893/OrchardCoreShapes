using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.GreenHouse.Indexes;
using OrchardCore.GreenHouse.ViewModels;
using YesSql;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrchardCore.GreenHouse.Models;

namespace OrchardCore.GreenHouse.Controllers;

public class VegetableController : Controller
{
    private readonly ISession _session;
    private readonly IContentManager _contentManager;
    private readonly IContentItemDisplayManager _contentItemDisplayManager;
    private readonly IUpdateModelAccessor _updateModelAccessor;
    private readonly IContentDefinitionManager _contentDefinitionManager;

    public VegetableController(
       ISession session,
       IContentManager contentManager,
       IContentItemDisplayManager contentItemDisplayManager,
       IUpdateModelAccessor updateModelAccessor,
       IContentDefinitionManager contentDefinitionManager)
    {
        _session = session;
        _contentManager = contentManager;
        _contentItemDisplayManager = contentItemDisplayManager;
        _updateModelAccessor = updateModelAccessor;
        _contentDefinitionManager = contentDefinitionManager;
    }

    [HttpGet]
    [Route("Vegetables")]
    public async Task<IActionResult> Vegetables(string fruitFilter = null)
    {
        // Get list of fruit types for dropdown
        var fruitTypes = await VegetableTypes();
        var fruitTypeOptions = fruitTypes.Select(c => new SelectListItem { Value = c, Text = c }).ToList();

        // Query for vegetables, optionally filtering by fruit type
        var vegetableQuery = _session.Query<ContentItem, VegetablePartIndex>();
        if (!string.IsNullOrEmpty(fruitFilter))
        {
            // look at the naming on the View side
            // this was selectedVegetable twice <select id="selectedVegetable" name="fruitFilter" class="form-control" onchange="this.form.submit()">
            // needed to be fruitFilter
            vegetableQuery = vegetableQuery.Where(x => x.FruitType == fruitFilter);
        }

        // Fetch the filtered (or all) vegetables
        var vegetables = await vegetableQuery.ListAsync();
        var vegetableViewModels = new List<VegetablePartViewModel>();
        foreach (var vegetable in vegetables)
        {
            if (vegetable.TryGet<VegetablePart>(out var vegetablePart))
            {
                // Build a display shape for each vegetable and map to view model
                var viewModel = new VegetablePartViewModel
                {
                    Shape = await _contentItemDisplayManager.BuildDisplayAsync(vegetable, _updateModelAccessor.ModelUpdater, "Summary"),
                    ContentItemId = vegetable.ContentItemId,
                };
                vegetableViewModels.Add(viewModel);
            }
        }

        // Prepare and return the list view model to the view
        var listViewModel = new VegetableListViewModel
        {
            Vegetables = vegetableViewModels,
            SelectedVegetable = fruitFilter, // Ensure this is set to maintain filter selection in dropdown
            FruitTypeOptions = fruitTypeOptions
        };

        return View(listViewModel);
    }

    [HttpGet]
    public async Task<List<string>> VegetableTypes()
    {
        var fruitTypes = await _session.Query<ContentItem, VegetablePartIndex>().ListAsync();
        var fruitTypeList = fruitTypes.Select(x => x.As<VegetablePart>().FruitType)
            .Distinct()
            .OrderBy(x => x) // Adding order
            .ToList();

        return fruitTypeList;
    }
}
