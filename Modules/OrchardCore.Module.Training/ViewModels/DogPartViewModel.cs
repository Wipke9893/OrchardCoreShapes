using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrchardCore.DisplayManagement;
using OrchardCore.Module.Training.Models;
using System.ComponentModel.DataAnnotations;


namespace OrchardCore.Module.Training.ViewModels;

public class DogPartViewModel
{
    [Required]
    [MaxLength(25)]
    public string Name { get; set; }

    [Required]
    [MaxLength(20)]
    public string Color { get; set; }

    [Required]
    [MaxLength(25)]
    public string Breed { get; set; }

    [MaxLength(10)]
    public string Size { get; set; }

    public int Age { get; set; }

    public bool Stripe { get; set; }

    public string ContentItemId { get; set; }

    public dynamic Shape { get; set; }

    public DogPart DogPart { get; set; }

    public string Temperament { get; set; }



    // Handlers
    public string HealthStatus { get; set; }
    // handlers

    // whether the dog can be edited
    public bool CanEdit { get; set; }
    // whether the dog can be edited

    [BindNever]
    public IEnumerable<SelectListItem> Colors { get; set; }

}
