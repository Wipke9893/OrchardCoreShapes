using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using OrchardCore.Module.Training.Models;
using OrchardCore.Module.Training.Permissions;
using OrchardCore.Module.Training.ViewModels;
using YesSql;

namespace OrchardCore.Module.Training.Controllers;

[Authorize]
public class DogAdminController : Controller
{
    private readonly IAuthorizationService _authorizationService;
    private readonly ISession _session;

    public DogAdminController(IAuthorizationService authorizationService,
        ISession session)
    {
        _authorizationService = authorizationService;
        _session = session;
    }

    [HttpGet]
    public async Task<IActionResult> EditDog(string contentItemId)
    {
        var dog = await _session.Query<ContentItem, ContentItemIndex>()
            .Where(x => x.ContentItemId == contentItemId)
            .FirstOrDefaultAsync();
        if (dog is null)
        {
            return NotFound();
        }

        var isAuthorized = await _authorizationService.AuthorizeAsync(User, DogPagePermissions.EditDog, dog);
        if (!isAuthorized)
        {
            return Forbid();
        }
        // If authorized, proceed to show the edit view
        return View(dog);
    }

    [HttpPost]
    public async Task<IActionResult> EditDog(string contentItemId, DogPartViewModel model)
    {
        var dog = await _session.Query<ContentItem, ContentItemIndex>()
            .Where(x => x.ContentItemId == contentItemId)
            .FirstOrDefaultAsync();

        if (dog == null)
        {
            return NotFound();
        }

        var isAuthorized = await _authorizationService.AuthorizeAsync(User, DogPagePermissions.EditDog, dog);
        if (!isAuthorized)
        {
            return Forbid();
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var dogPart = dog.As<DogPart>();
        if (dogPart is not null)
        {
            dogPart.Name = model.Name;
            dogPart.Age = model.Age;
            dogPart.Breed = model.Breed;

            await _session.SaveAsync(dog);
        }
        return RedirectToAction("Index");
    }
}

