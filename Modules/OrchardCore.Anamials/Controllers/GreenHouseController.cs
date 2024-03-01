using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using OrchardCore.GreenHouse.ViewModels;
using YesSql;
using OrchardCore.GreenHouse.Models;
using OrchardCore.ContentManagement.Records;

namespace OrchardCore.GreenHouse.Controllers
{
    public class GreenHouseController : Controller
    {
        private readonly ISession _session;

        public GreenHouseController(ISession session)
        {
            _session = session;
        }

        private async Task<List<FlowerPartViewModel>> GetAllFlowersAsync()
        {
            var query = _session.Query<ContentItem, ContentItemIndex>(x => x.ContentType == "Flower");
            var flowers = await query.ListAsync();

            return flowers.Select(f => new FlowerPartViewModel
            {
                CommonName = f.As<FlowerPart>().CommonName,
                FertilizerType = f.As<FlowerPart>().FertilizerType
            }).ToList();
        }

        private async Task<List<VegetablePartViewModel>> GetAllVegetablesAsync()
        {
            var query = _session.Query<ContentItem, ContentItemIndex>(x => x.ContentType == "Vegetable");
            var vegetables = await query.ListAsync();

            return vegetables.Select(v => new VegetablePartViewModel
            {
                CommonName = v.As<VegetablePart>().CommonName,
                FertilizerType = v.As<VegetablePart>().FertilizerType
            }).ToList();
        }

        [HttpGet]
        [Route("GreenHouse")]
        public async Task<IActionResult> DisplayAllPlants()
        {
            var viewModel = new GreenHouseViewModel
            {
                Flowers = await GetAllFlowersAsync(),
                Vegetables = await GetAllVegetablesAsync()
            };

            return View(viewModel);
        }
    }
}





