using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.DisplayManagement;
using OrchardCore.Module.Training.Models;
using System.ComponentModel.DataAnnotations;


namespace OrchardCore.Module.Training.ViewModels;

public class AnimalPartViewModel
{
    [Required]
    public string Species { get; set; }

    [MaxLength(100)]
    public string Habitat { get; set; }

    [BindNever]
    public AnimalPart AnimalPart { get; set; }

    public string ContentItemId { get; set; }

    public IShape Shape { get; set; }
}

