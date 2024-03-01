using OrchardCore.ContentManagement;
using OrchardCore.GreenHouse.Models;
using YesSql.Indexes;

namespace OrchardCore.GreenHouse.Indexes;
public class VegetablePartIndex : MapIndex
{
    public string ScientificName { get; set; }
    public string CommonName { get; set; }
    public string SunlightType { get; set; }
    public string FruitType { get; set; }
    public string LeafType { get; set; }
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

public class VegetablePartIndexProvider : IndexProvider<ContentItem>
{
    public override void Describe(DescribeContext<ContentItem> context)
    {
        context.For<VegetablePartIndex>()
            .Map(contentItem =>
            {
                var vegetablePart = contentItem.As<VegetablePart>();
                if (vegetablePart is null)
                {
                    return null;
                }
                return new VegetablePartIndex
                {
                    ContentItemId = contentItem.ContentItemId,

                    ScientificName = vegetablePart.ScientificName,
                    CommonName = vegetablePart.CommonName,
                    SunlightType = vegetablePart.SunlightType,
                    FruitType = vegetablePart.FruitType,
                    LeafType = vegetablePart.LeafType,
                    StemType = vegetablePart.StemType,
                    RootType = vegetablePart.RootType,
                    SoilType = vegetablePart.SoilType,
                    WaterType = vegetablePart.WaterType,
                    HumidityType = vegetablePart.HumidityType,
                    TemperatureType = vegetablePart.TemperatureType,
                    FertilizerType = vegetablePart.FertilizerType,
                    PesticideType = vegetablePart.PesticideType,
                    DiseaseType = vegetablePart.DiseaseType,
                    PestType = vegetablePart.PestType
                };
            });
    }
}
