using OrchardCore.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOrchardCms();
builder.WebHost.UseNLogWeb();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseOrchardCore();

app.Run();
