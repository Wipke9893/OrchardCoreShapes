using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrchardCore.GreenHouse.Models;

namespace OrchardCore.GreenHouse.ViewModels;
public class FlowerPartViewModel
{
    /*    [Required]*/
    [MaxLength(50)]
    public string ScientificName { get; set; }

    /*    [Required]*/
    [MaxLength(50)]
    public string CommonName { get; set; }

    /*   [Required]*/
    [MaxLength(50)]
    public string SunlightType { get; set; }

    /*[Required]*/
    [MaxLength(50)]
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
    public dynamic Shape { get; set; }
    public FlowerPart FlowerPart { get; set; }


    [BindNever]
    public IEnumerable<SelectListItem> SunlightTypes { get; set; }

    [BindNever]
    public IEnumerable<SelectListItem> TemperatureTypes { get; set; }
}
