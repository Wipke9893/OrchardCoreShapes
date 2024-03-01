using OrchardCore.ContentManagement;
using OrchardCore.GreenHouse.Models;
using YesSql.Indexes;

namespace OrchardCore.GreenHouse.Indexes;
public class FlowerPartIndex : MapIndex
{
    public string ScientificName { get; set; }
    public string CommonName { get; set; }
    public string SunlightType { get; set; }
    public string FlowerType { get; set; }
    public int Petals { get; set; }
    public string StemType { get; set; }
    public string RootType { get; set; }
    public string SoilType { get; set; }
    public string WaterType { get; set; }
    public string HumidityType { get; set; }
    public string TemperatureType { get; set; }
    public string FertilizerType { get; set; }
    public string PesticideType { get; set; }
    public string DiseaseType { get; set; }
    public string PestType { get; set; }
    public string ContentItemId { get; set; }
}

public class FlowerPartIndexProvider : IndexProvider<ContentItem>
{
    public override void Describe(DescribeContext<ContentItem> context)
    {
        context.For<FlowerPartIndex>()
            .Map(contentItem =>
            {
                var flowerPart = contentItem.As<FlowerPart>();
                if (flowerPart is null)
                {
                    return null;
                }
                return new FlowerPartIndex
                {
                    ContentItemId = contentItem.ContentItemId,

                    ScientificName = flowerPart.ScientificName,
                    CommonName = flowerPart.CommonName,
                    SunlightType = flowerPart.SunlightType,
                    FlowerType = flowerPart.FlowerType,
                    Petals = flowerPart.Petals,
                    StemType = flowerPart.StemType,
                    RootType = flowerPart.RootType,
                    SoilType = flowerPart.SoilType,
                    WaterType = flowerPart.WaterType,
                    HumidityType = flowerPart.HumidityType,
                    TemperatureType = flowerPart.TemperatureType,
                    FertilizerType = flowerPart.FertilizerType,
                    PesticideType = flowerPart.PesticideType,
                    DiseaseType = flowerPart.DiseaseType,
                    PestType = flowerPart.PestType
                };
            });
    }
}
