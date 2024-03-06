using Microsoft.AspNetCore.Mvc.Rendering;
using OrchardCore.ContentManagement;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.GreenHouse.Indexes;
using OrchardCore.GreenHouse.Models;
using OrchardCore.GreenHouse.ViewModels;
using YesSql;


namespace OrchardCore.GreenHouse.Drivers;
public class EmployeeListDisplayDriver : DisplayDriver<EmployeeFilter>
{
    private readonly ISession _session;

    public EmployeeListDisplayDriver(ISession session)
    {
        _session = session;
    }

    public override IDisplayResult Edit(EmployeeFilter model)
    {
        return Initialize<EmployeeListViewModel>("EmployeeList_Edit", async m =>
        {
            var employeeTypesQuery = _session.Query<ContentItem, EmployeePartIndex>();

            var employeeCountry = await _session.Query<ContentItem, EmployeePartIndex>().ListAsync();

            m.CountryOptions = employeeCountry.Select(x => x.As<EmployeePart>().Country)
                .Distinct()
                .Select(x => new SelectListItem(x, x))
                .ToList();
        }).Location("Body:5");
    }

    public override async Task<IDisplayResult> UpdateAsync(EmployeeFilter model, IUpdateModel updater)
    {
        var viewModel = new EmployeeListViewModel();

        if (await updater.TryUpdateModelAsync(viewModel, Prefix) && !string.IsNullOrEmpty(viewModel.SelectedEmployee))
        {
            model.Conditions.Add(x => x.With<EmployeePartIndex>(y => y.Country == viewModel.SelectedEmployee));
        }

        return Edit(model);
    }
}

