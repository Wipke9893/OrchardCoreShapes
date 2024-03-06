using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.GreenHouse.Models;
using OrchardCore.GreenHouse.ViewModels;

namespace OrchardCore.GreenHouse.Drivers;
public class ReviewPartDisplayDriver : ContentPartDisplayDriver<ReviewPart>
{
    public override IDisplayResult Display(ReviewPart part, BuildPartDisplayContext context) =>
        Initialize<ReviewPartViewModel>("ReviewPart", viewModel =>
        {
            PopulateViewModel(part, viewModel);
        })
        .Location("Detail", "Content:10");

    public override IDisplayResult Edit(ReviewPart part, BuildPartEditorContext context) =>
        Initialize<ReviewPartViewModel>(GetEditorShapeType(context), viewModel =>
        {
            viewModel.Name = part.Name;
            viewModel.Email = part.Email;
            viewModel.Review = part.Review;
            viewModel.Rating = part.Rating;
        });

    public override async Task<IDisplayResult> UpdateAsync(ReviewPart part, IUpdateModel updater, UpdatePartEditorContext context)
    {
        var viewModel = new ReviewPartViewModel();

        if (await context.Updater.TryUpdateModelAsync(viewModel, Prefix))
        {
            part.Name = viewModel.Name;
            part.Email = viewModel.Email;
            part.Review = viewModel.Review;
            part.Rating = viewModel.Rating;
        }

        return Edit(part, context);
    }
    private static void PopulateViewModel(ReviewPart part, ReviewPartViewModel viewModel)
    {
        viewModel.Name = part.Name;
        viewModel.Email = part.Email;
        viewModel.Review = part.Review;
        viewModel.Rating = part.Rating;
    }
}

/*ReviewPartDisplayDriver*/
