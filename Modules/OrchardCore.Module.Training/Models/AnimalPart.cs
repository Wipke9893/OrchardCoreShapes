using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;

namespace OrchardCore.Module.Training.Models;

public class AnimalPart : ContentPart
{
    public string Species { get; set; }
    public string Habitat { get; set; }
    public TextField AnimalType { get; set; } = new();

}


