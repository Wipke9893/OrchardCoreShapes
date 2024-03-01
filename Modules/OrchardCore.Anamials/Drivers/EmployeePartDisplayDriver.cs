
using Microsoft.AspNetCore.Mvc.Rendering;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.GreenHouse.Models;
using OrchardCore.GreenHouse.ViewModels;

namespace OrchardCore.GreenHouse.Drivers;
public class EmployeePartDisplayDriver : ContentPartDisplayDriver<EmployeePart>
{
    public override IDisplayResult Display(EmployeePart part, BuildPartDisplayContext context) =>
        Initialize<EmployeePartViewModel>(GetDisplayShapeType(context), viewModel =>
        {
            PopulateViewModel(part, viewModel);
        })
        .Location("Detail", "Content:2")
        .Location("SummaryAdmin", "Meta:10")
        .Location("Summary", "Content:3");

    public override IDisplayResult Edit(EmployeePart part, BuildPartEditorContext context) =>
       Initialize<EmployeePartViewModel>(GetEditorShapeType(context), viewModel =>
       {
           PopulateViewModel(part, viewModel);
           viewModel.Countries =
           [
                new SelectListItem { Text = "United States", Value = "United States" },
                new SelectListItem { Text = "Canada", Value = "Canada" },
                new SelectListItem { Text = "Mexico", Value = "Mexico" },
                new SelectListItem { Text = "United Kingdom", Value = "United Kingdom" },
                new SelectListItem { Text = "Germany", Value = "Germany" },
                new SelectListItem { Text = "France", Value = "France" },
                new SelectListItem { Text = "Italy", Value = "Italy" },
                new SelectListItem { Text = "Spain", Value = "Spain" },
                new SelectListItem { Text = "Japan", Value = "Japan" },
                new SelectListItem { Text = "China", Value = "China" },
                new SelectListItem { Text = "India", Value = "India" },
                new SelectListItem { Text = "Australia", Value = "Australia" },
                new SelectListItem { Text = "Brazil", Value = "Brazil" },
                new SelectListItem { Text = "South Africa", Value = "South Africa" },
                new SelectListItem { Text = "Russia", Value = "Russia" },
                new SelectListItem { Text = "Other", Value = "Other" },
              ];
       });

    public override async Task<IDisplayResult> UpdateAsync(EmployeePart part, IUpdateModel updater, UpdatePartEditorContext context)
    {
        var viewModel = new EmployeePartViewModel();

        if (await updater.TryUpdateModelAsync(viewModel, Prefix))
        {
            part.FirstName = viewModel.FirstName;
            part.LastName = viewModel.LastName;
            part.Email = viewModel.Email;
            part.Phone = viewModel.Phone;
            part.Address = viewModel.Address;
            part.City = viewModel.City;
            part.State = viewModel.State;
            part.Zip = viewModel.Zip;
            part.Country = viewModel.Country;
        }
        return Edit(part, context);
    }

    private static void PopulateViewModel(EmployeePart part, EmployeePartViewModel viewModel)
    {
        viewModel.FirstName = part.FirstName;
        viewModel.LastName = part.LastName;
        viewModel.Email = part.Email;
        viewModel.Phone = part.Phone;
        viewModel.Address = part.Address;
        viewModel.City = part.City;
        viewModel.State = part.State;
        viewModel.Zip = part.Zip;
        viewModel.Country = part.Country;
        viewModel.ContentItemId = part.ContentItem.ContentItemId;
        viewModel.employeePart = part;
    }
}

