using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Records;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.Module.Training.Models;
using OrchardCore.Module.Training.ViewModels;
using YesSql;

namespace OrchardCore.Module.Training.Controllers;

public class ZooController : Controller
{
    private readonly ISession _session;
    private readonly IContentManager _contentManager;
    private readonly IContentItemDisplayManager _contentItemDisplayManager;
    private readonly IUpdateModelAccessor _updateModelAccessor;
    private readonly IContentDefinitionManager _contentDefinitionManager;

    public ZooController(
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
    public async Task<IActionResult> AllAnimals()
    {
        var animalViewModels = new List<AnimalPartViewModel>();

        //(x => x.ContentType == "Animal" && x.Published
        var animals = await _session.Query<ContentItem, ContentItemIndex>().ListAsync();

        foreach (var animal in animals)
        {
            var animalPart = animal.As<AnimalPart>();
            if (animalPart is not null)
            {
                animalViewModels.Add(new AnimalPartViewModel
                {
                    Shape = await _contentItemDisplayManager.BuildDisplayAsync(animal, _updateModelAccessor.ModelUpdater, "Summary"),
                    ContentItemId = animal.ContentItemId // ContentItemId is set
                });
            }
        }

        return View(animalViewModels);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAnimal(string contentItemId)
    {
        var animal = await _session.Query<ContentItem, ContentItemIndex>(x => x.ContentItemId == contentItemId).FirstOrDefaultAsync();

        /*var animal = await _contentManager.GetAsync(contentItemId, VersionOptions.AllVersions);*/

        if (animal is null)
        {
            return NotFound();
        }

        // await _contentManager.RemoveAsync(animal);
        _session.Delete(animal);
        await _session.SaveChangesAsync();

        return RedirectToAction(nameof(AllAnimals));
    }
}

