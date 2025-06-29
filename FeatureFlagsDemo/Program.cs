using FeatureFlagsDemo.Middleware;
using FeatureFlagsDemo.Services;
using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFeatureManagement();
builder.Services.AddScoped<IFeatureToggleService,FeatureToggleService>();

var app = builder.Build();

app.UseRouting();

// Example : block access to /legacy if the feature "BlockLegacyEndpoints" is enabled
app.Map("/legacy", legacyApp =>
{
    legacyApp.UseMiddleware<FeatureToggleMiddleware>("BlockLegacyEndpoints");
    legacyApp.Run(async context =>
    {
        await context.Response.WriteAsync("Legacy endpoint accessible.");
    });
});


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();
app.MapControllers();
await app.RunAsync();
