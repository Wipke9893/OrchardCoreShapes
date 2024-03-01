using OrchardCore.Modules.Manifest;

[assembly: Module(
    Author = "The RDI Team",
    Website = "https://orchardcore.net",
    Version = "0.0.1"
)]



[assembly: Feature(
    Name = "GreenHouse Garden",
    Id = "OrchardCore.GreenHouse",
    Description = "OrchardCore.GreenHouse Garden",
    Category = "Content Management",
    Dependencies =
    [
        "OrchardCore.Contents"
    ]
)]


[assembly: Feature(
    Name = "GreenHouse Garden Status",
    Id = "OrchardCore.GreenHouse.Status",
    Description = "OrchardCore.GreenHouse Garden",
    Category = "Content Management",
    Dependencies =
    [
        "OrchardCore.Contents"
    ]
)]
