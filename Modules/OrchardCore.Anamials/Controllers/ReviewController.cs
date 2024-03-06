using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.GreenHouse.Indexes;
using OrchardCore.GreenHouse.Models;
using OrchardCore.GreenHouse.ViewModels;
using YesSql;

namespace OrchardCore.GreenHouse.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ISession _session;
        private readonly IContentManager _contentManager;
        private readonly IContentItemDisplayManager _contentItemDisplayManager;
        private readonly IUpdateModelAccessor _updateModelAccessor;

        public ReviewController(
            ISession session,
            IContentManager contentManager,
            IContentItemDisplayManager contentItemDisplayManager,
            IUpdateModelAccessor updateModelAccessor)
        {
            _session = session;
            _contentManager = contentManager;
            _contentItemDisplayManager = contentItemDisplayManager;
            _updateModelAccessor = updateModelAccessor;
        }

        [Route("Reviews")]
        public async Task<IActionResult> FilteredReviews()
        {
            var query = _session.Query<ContentItem, ReviewPartIndex>();

            var contentItems = await query.ListAsync();

            var model = new ReviewShapeViewModel();

            if (contentItems.Any())
            {
                foreach (var contentItem in contentItems)
                {
                    model.ContentItems.Add(await _contentItemDisplayManager.BuildDisplayAsync(contentItem, _updateModelAccessor.ModelUpdater, "Summary"));
                }
            }

            return View(model);
        }

        [HttpPost]
        [Route("SubmitReview")]
        public async Task<IActionResult> SubmitReview(ReviewPartViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Return to the form with validation messages if model state is invalid.
                return View(model);
            }

            // Create a new content item of type "Review"
            var review = await _contentManager.NewAsync("Review");
            var reviewPart = review.As<ReviewPart>();

            // Update the ReviewPart with data from the form
            reviewPart.Name = model.Name;
            reviewPart.Email = model.Email;
            reviewPart.Review = model.Review;
            reviewPart.Rating = model.Rating;

            // Save the content item
            await _contentManager.CreateAsync(review);
            await _contentManager.PublishAsync(review); // This line is review visible

            TempData["ReviewSubmitted"] = "Your review has been submitted successfully.";

            // Redirect after successful submission
            return RedirectToAction("FilteredReviews");
        }

    }
}
