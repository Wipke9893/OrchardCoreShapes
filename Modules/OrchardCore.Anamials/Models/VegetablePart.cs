using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;

namespace OrchardCore.GreenHouse.Models;
public class VegetablePart : ContentPart
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

    public TextField GrowType { get; set; }

}

