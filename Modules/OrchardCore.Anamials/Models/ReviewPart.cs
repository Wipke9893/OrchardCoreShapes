using OrchardCore.ContentManagement;

namespace OrchardCore.GreenHouse.Models;
public class ReviewPart : ContentPart
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Review { get; set; }
    public int Rating { get; set; }
}
