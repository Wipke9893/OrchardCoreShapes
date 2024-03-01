using OrchardCore.ContentManagement;

namespace OrchardCore.GreenHouse.Models;

public class PredefinedColorPart : ContentPart
{
    // Display driver
    // register this in the container
    // view
    // migration
    public string Color { get; set; }
}
