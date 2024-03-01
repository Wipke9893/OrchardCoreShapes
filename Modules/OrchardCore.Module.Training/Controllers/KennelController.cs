using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Records;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.Module.Training.Indexes;
using OrchardCore.Module.Training.Models;
using OrchardCore.Module.Training.ViewModels;
using YesSql;
using OrchardCore.Module.Training.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OrchardCore.Module.Training.Controllers;

public class KennelController : Controller
{
    private readonly ISession _session;
    private readonly IContentManager _contentManager;
    private readonly IContentItemDisplayManager _contentItemDisplayManager;
    private readonly IUpdateModelAccessor _updateModelAccessor;
    private readonly IContentDefinitionManager _contentDefinitionManager;
    private readonly IAuthorizationService _authorizationService; // Injected AuthorizationService

    public KennelController(
         ISession session,
         IContentManager contentManager,
         IContentItemDisplayManager contentItemDisplayManager,
         IUpdateModelAccessor updateModelAccessor,
         IContentDefinitionManager contentDefinitionManager,
         IAuthorizationService authorizationService)
    {
        _session = session;
        _contentManager = contentManager;
        _contentItemDisplayManager = contentItemDisplayManager;
        _updateModelAccessor = updateModelAccessor;
        _contentDefinitionManager = contentDefinitionManager;
        _authorizationService = authorizationService; // Initialize AuthorizationService
    }



    [HttpGet]
    [Route("AllDogs")]
    public async Task<IActionResult> AllDogs(string colorFilter = null)
    {
        // distinct colors and creating SelectListItems for the dropdown.
        var colors = await DogColors();
        var colorOptions = colors.Select(c => new SelectListItem { Value = c, Text = c }).ToList();

        // query for fetching dogs.
        var dogFilter = _session.Query<ContentItem, DogPartIndex>();
        if (!string.IsNullOrEmpty(colorFilter))
        {
            dogFilter = dogFilter.Where(x => x.Color == colorFilter);
        }

        var dogs = await dogFilter.ListAsync();
        var dogViewModels = new List<DogPartViewModel>();
        foreach (var dog in dogs)
        {
            if (dog.TryGet<DogPart>(out var dogPart))
            {
                var canEdit = await _authorizationService.AuthorizeAsync(User, DogPagePermissions.EditDog, dog);
                var viewModel = new DogPartViewModel
                {
                    Shape = await _contentItemDisplayManager.BuildDisplayAsync(dog, _updateModelAccessor.ModelUpdater, "Summary"),
                    ContentItemId = dog.ContentItemId,
                    CanEdit = canEdit // Set CanEdit based on authorization
                };
                dogViewModels.Add(viewModel);
            }
        }
        // list view model with dogs and color filter options.
        var listViewModel = new DogsListViewModel
        {
            Dogs = dogViewModels,
            SelectedColor = colorFilter,
            ColorOptions = colorOptions
        };

        return View(listViewModel);
    }

    [HttpGet]
    public async Task<List<string>> DogColors()
    {
        var colors = await _session.Query<ContentItem, DogPartIndex>().ListAsync();
        var distinctColors = colors.Select(x => x.As<DogPart>().Color)
            .Distinct()
            .OrderBy(color => color)
            .ToList();

        return distinctColors;
    }

    [HttpPost]
    public async Task<IActionResult> DeleteDog(string contentItemId)
    {
        var dog = await _session.Query<ContentItem, ContentItemIndex>(x => x.ContentItemId == contentItemId).FirstOrDefaultAsync();
        if (dog is null)
        {
            return NotFound();
        }
        _session.Delete(dog);
        await _session.SaveChangesAsync();
        return RedirectToAction(nameof(AllDogs));
    }

    /*  [HttpGet]
      [Route("AllDogs")]
      public async Task<IActionResult> AllDogs(string colorFilter = null)
      {
          var dogViewModels = new List<DogPartViewModel>();
          var colors = await DogColors();
          ViewBag.Colors = colors;

          var dogFilter = _session.Query<ContentItem, DogPartIndex>();
          if (!string.IsNullOrEmpty(colorFilter))
          {
              dogFilter = dogFilter.Where(x => x.Color == colorFilter);
          }

          var dogs = await dogFilter.ListAsync();
          foreach (var dog in dogs)
          {
              if (dog.TryGet<DogPart>(out var dogPart))
              {
                  var canEdit = await _authorizationService.AuthorizeAsync(User, DogPagePermissions.EditDog, dog);
                  var viewModel = new DogPartViewModel
                  {
                      Shape = await _contentItemDisplayManager.BuildDisplayAsync(dog, _updateModelAccessor.ModelUpdater, "Summary"),
                      ContentItemId = dog.ContentItemId,
                      CanEdit = canEdit // Set CanEdit based on authorization
                  };
                  dogViewModels.Add(viewModel);
              }
          }
          ViewBag.ColorFilter = colorFilter;
          return View(dogViewModels);
      }*/
}
