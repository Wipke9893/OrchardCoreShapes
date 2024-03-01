using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;

namespace OrchardCore.Module.Training.Models;

public class DogPart : ContentPart
{
    public string Name { get; set; }

    public string Color { get; set; }

    public string Breed { get; set; }

    public bool Stripe { get; set; }

    public string Size { get; set; }

    public int Age { get; set; }

    // Handlers
    public string HealthStatus { get; set; }
}
