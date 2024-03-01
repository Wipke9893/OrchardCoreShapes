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


namespace OrchardCore.GreenHouse.Controllers;

public class FlowerController : Controller
{
    private readonly ISession _session;
    private readonly IContentManager _contentManager;
    private readonly IContentItemDisplayManager _contentItemDisplayManager;
    private readonly IUpdateModelAccessor _updateModelAccessor;
    private readonly IContentDefinitionManager _contentDefinitionManager;

    public FlowerController(
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
    [Route("FlowersForever")]
    public async Task<IActionResult> FlowersForever(string flowerFilter = null)
    {
        var flowerTypes = await FlowerTypes();
        var flowerTypeOptions = flowerTypes.Select(c => new SelectListItem { Value = c, Text = c }).ToList();

        var flowerQuery = _session.Query<ContentItem, FlowerPartIndex>();
        if (!string.IsNullOrEmpty(flowerFilter))
        {
            flowerQuery = flowerQuery.Where(x => x.FlowerType == flowerFilter);
        }

        var flowers = await flowerQuery.ListAsync();
        var flowerViewModels = new List<FlowerPartViewModel>();
        foreach (var flower in flowers)
        {
            if (flower.TryGet<FlowerPart>(out var flowerPart))
            {
                var viewModel = new FlowerPartViewModel
                {
                    Shape = await _contentItemDisplayManager.BuildDisplayAsync(flower, _updateModelAccessor.ModelUpdater, "Summary"),
                    ContentItemId = flower.ContentItemId,
                };
                flowerViewModels.Add(viewModel);
            }
        }

        var listViewModel = new FlowerListViewModel
        {
            Flowers = flowerViewModels,
            SelectedFlower = flowerFilter,
            FlowerOptions = flowerTypeOptions
        };

        return View(listViewModel);
    }

    [HttpGet]
    public async Task<List<string>> FlowerTypes()
    {
        var flowerTypes = await _session.Query<ContentItem, FlowerPartIndex>().ListAsync();
        var flowerTypeList = flowerTypes.Select(x => x.As<FlowerPart>().FlowerType)
            .Distinct()
            .ToList();
        return flowerTypeList;
    }
}
